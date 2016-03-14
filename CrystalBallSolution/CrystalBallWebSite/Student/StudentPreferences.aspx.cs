using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentPreferences : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*
     * When user clicks the submit button the program will log all of the options selected and send them to the database step by step to determine the final results that should be returned.
     * Once those results are gathered the final view will be displayed with the returned values from the databased bound to a gridview.
     */
    protected void Submit_Click(object sender, EventArgs e)
    {
        //step 1 - Gather program information from the student. This is primarily used in metrics data to see what programs see students looking to transfer.
        int? programCategoryID, programID, programChange, semester;
        StudentController sysmgr = new StudentController();
        int tempInt;
        stepThree.Visible = false;

        if (CurrentStudent.Checked == true && int.TryParse((CategoryDropDown.SelectedValue), out tempInt) && int.TryParse(ProgramDropDown.SelectedValue, out tempInt) && int.TryParse(SemesterDropDown.SelectedValue, out tempInt))
        {
            programCategoryID = Convert.ToInt32(CategoryDropDown.SelectedValue);
            programID = Convert.ToInt32(ProgramDropDown.SelectedValue);
            programChange = Convert.ToInt32(ChangeProgram.Checked);
            semester = Convert.ToInt32(SemesterDropDown.SelectedValue);
        }
        else
        {
            programCategoryID = null;
            programID = null;
            programChange = null;
            semester = null;
        }


        MessageUserControl.TryRun(() =>
        {
            //step 2 - Gather the answers to the student preference questions
            List<StudentPreference> myPreferences = new List<StudentPreference>();
            foreach (GridViewRow row in PrefQuestions.Rows)
            {
                myPreferences.Add(new StudentPreference(
                           Convert.ToInt32(row.Cells[0].Text),
                           Convert.ToInt32((row.FindControl("RBL_YN") as RadioButtonList).SelectedValue)
                                ));
            }
            //send preferences to the BLL for initial results
            List<ProgramResult> preferenceResults = new List<ProgramResult>();
            preferenceResults = StudentController.FindPreferenceMatches(myPreferences);

            //step 3 - Gather the selected courses provided by the student. Determine if any selected courses are the highest in a particular course group
            List<int> hsCourses = new List<int>();
            List<GetHSCourses> courseList = new List<GetHSCourses>();
            courseList = sysmgr.GetCourseList();

            //Search for checked items in the check box list - if a checked item is a 30 level that
            //is flagged in the database as being the highest of a category
            //add all other courses in that category to hsCourses
            foreach (GetHSCourses testItem in courseList)
            {
                foreach (ListItem item in CB_CourseList.Items)
                {
                    if (item.Selected && item.Value == testItem.HighSchoolCourseID.ToString() && testItem.HighSchoolHighestCourse == true)
                    {
                        int[] childCourses = sysmgr.GetParentCategory(Convert.ToInt32(item.Value));
                        for (int i = 0; i < childCourses.Length; i++)
                        {
                            hsCourses.Add(Convert.ToInt32(childCourses[i]));
                        }
                    }
                    else if (item.Selected)
                    {
                        hsCourses.Add(Convert.ToInt32(item.Value));
                    }
                }
            }

            //step 4 - Determine initial program results and then filter those results based on student preference questions. Display final results
            //send information to BLL for processing and narrow down possible results
            List<ProgramResult> programResults = new List<ProgramResult>();
            programResults = StudentController.FindProgramMatches(hsCourses);

            List<ProgramResult> finalResults = new List<ProgramResult>();
            finalResults = StudentController.EntranceReq_Pref_Match(preferenceResults, programResults);
            //display results once queries are complete
            ResultsView.DataSource = finalResults;
            ResultsView.DataBind();
            results.Visible = true;
        }, "Success!", "Here are the pathways available to you!");


    }
    protected void CurrentStudent_CheckedChanged(object sender, EventArgs e)
    {
        if (CurrentStudent.Checked)
        {
            chooseProgram.Visible = true;
        }
        else
        {
            chooseProgram.Visible = false;
        }
    }

    protected void stepOneNext_Click(object sender, EventArgs e)
    {
        while (ProgramDropDown.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("You must select a program");
        }
        stepOne.Visible = false;
        step2.Visible = true;
        int programid;
        int semester;
        bool switchProgram;

        if (CurrentStudent.Checked == true && int.TryParse(ProgramDropDown.SelectedValue, out programid) && int.TryParse(SemesterDropDown.SelectedValue, out semester))
        {
            programid = int.Parse(ProgramDropDown.SelectedValue);
            semester = int.Parse(SemesterDropDown.SelectedValue);
            if (ChangeProgram.Checked == true)
            {
                switchProgram = true;
            }
            else
            {
                switchProgram = false;
            }
        }
    }
    protected void onPreviousClick(object sender, EventArgs e)
    {
        step2.Visible = false;
        stepOne.Visible = true;
    }
    protected void Populate_Program(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int category = Convert.ToInt32(CategoryDropDown.SelectedValue);
        ProgramDropDown.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramDropDown.DataBind();
    }
    protected void stepThreePrevious_Click(object sender, EventArgs e)
    {
        stepThree.Visible = false;
        step2.Visible = true;
    }
    protected void stepTwoNext_Click(object sender, EventArgs e)
    {
        step2.Visible = false;
        stepThree.Visible = true;
    }
    //should I be resetting the values when they click search again? It was mentioned that they wanted this stuff to be saved.
    protected void searchAgain_Click(object sender, EventArgs e)
    {
        results.Visible = false;
        stepOne.Visible = true;
        //add code to reset all fields for steps 1 -3
        foreach (GridViewRow row in PrefQuestions.Rows)
        {
            (row.FindControl("RBL_YN") as RadioButtonList).SelectedValue = "1";
        }
        foreach (ListItem item in CB_CourseList.Items)
        {
            item.Selected = false;
        }
    }
}