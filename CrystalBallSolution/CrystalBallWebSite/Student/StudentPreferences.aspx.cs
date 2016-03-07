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
    /*
    protected override object SaveViewState()
    {
        // create object array for Item count + 1
        object[] allStates = new object[this.Items.Count + 1];

        // the +1 is to hold the base info
        object baseState = base.SaveViewState();
        allStates[0] = baseState;

        Int32 i = 1;
        // now loop through and save each Style attribute for the List
        foreach (ListItem li in this.Items)
        {
            Int32 j = 0;
            string[][] attributes = new string[li.Attributes.Count][];
            foreach (string attribute in li.Attributes.Keys)
            {
                attributes[j++] = new string[] { attribute, li.Attributes[attribute] };
            }
            allStates[i++] = attributes;
        }
        return allStates;
    }

    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            object[] myState = (object[])savedState;

            // restore base first
            if (myState[0] != null)
                base.LoadViewState(myState[0]);

            Int32 i = 1;
            foreach (ListItem li in this.Items)
            {
                // loop through and restore each style attribute
                foreach (string[] attribute in (string[][])myState[i++])
                {
                    li.Attributes[attribute[0]] = attribute[1];
                }
            }
        }
    }
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //on page load access the controller and pull the courseid, coursename, category, and highestCategory
        //loop through each value and bind to the check box list using custom attributes
        /*
        StudentController sysmgr = new StudentController();
        List<GetHSCourses> hsCourses = new List<GetHSCourses>();
        var courseList = new ListItem();
        hsCourses = sysmgr.GetCourseList();
        CB_CourseList.DataSource = hsCourses;
        CB_CourseList.DataBind();
        //reference page for forcing attributes on ListItems http://aspnet.4guysfromrolla.com/articles/110205-1.aspx
        //http://stackoverflow.com/questions/1313447/listitems-attributes-in-a-dropdownlist-are-lost-on-postback
        ListItem test = new ListItem("test");
        foreach(ListItem item in CB_CourseList.Items)
        {
            courseList.Attributes.Add("test", "test");
        }
         */
        /*foreach (GetHSCourses item in hsCourses)
        {
            //look for highestCategory and append that value to that particular checkbox
            foreach (ListItem list in CB_CourseList.Items)
            {
                if (list.Value == item.HighSchoolCourseID.ToString() && item.HighSchoolHighestCourse == true)
                {
                    list.Attributes.Add("highest", "1");
                }
            }
        }*/




    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        int? programCategoryID, programID, programChange, semester;
        StudentController sysmgr = new StudentController();
        int tempInt;
        //gather the question ids and answer values for each value on the page and send it
        //to the database for evaluation (packaged with the initial questions)
        //hide previous steps and do requisite computations to get results
        stepThree.Visible = false;
        //log entries from steps 1-3 and then use those entries to query the database
        //step 1 - program information - metrics gather stuff - use program and semester to pull back courses - use method in student controller (prefill courses)

        //add exception handling for program stream not being selected - also have category auto refresh based on first value in the category DDL
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
        
        //step 2 - preference questions
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

        //step 3 - student courses - use method near the bottom of student controller entrancereq-prefmatch
        //for each option selected in the check box field, add that to a list
        //**ATTEMPT TO ADD FUNCTIONALITY TO AUTO CHECK PREVIOUS COURSES IF A HIGHER LEVEL COURSE IS SELECTED**
        List<int> hsCourses = new List<int>();
        List<GetHSCourses> courseList = new List<GetHSCourses>();
        courseList = sysmgr.GetCourseList();
        /* TEST CODE
        foreach (GetHSCourses testItem in test)
        {
            foreach (ListItem item in CB_CourseList.Items)
            {
                if (item.Selected && item.Value == testItem.HighSchoolCourseID.ToString() && testItem.HighSchoolHighestCourse == true)
                {
         * //add a variable to capture the the parent category
         * //eg. int[] childCourses = sysmgr.GetParentCategory();
         * //create a list that will contain the id's of courses that are part of parent category
         * //eg. List<int> courseCodes = new List<int>();
         * // int[] childCourses = sysmgr.GetParentCategory();
         * //collect all the courseCodes of a given category and then add them to the HSCourseList
                    hsCourses.Add(Convert.ToInt32(item.Value));
                    foreach (ListItem demo in CB_CourseList.Items)
                    {
                        //if the current iteration falls on a checkbox that is part of the parent group
                        //add it to the list
                        //most likely need to achieve this in linq
                        if (demo.Selected == false && )
                        {

                        }
                    }
                }
            }
        }
         */
        /*
         GetParent simply returns the parent category for a given coursecode
         * 
         */
        

        //NEW TESTING CODE
        foreach (GetHSCourses testItem in courseList)
        {
            foreach (ListItem item in CB_CourseList.Items)
            {
                if(item.Selected && item.Value == testItem.HighSchoolCourseID.ToString() && testItem.HighSchoolHighestCourse == true)
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


        /* GOOD CODE
        foreach (ListItem item in CB_CourseList.Items)
        {
            if(item.Selected)
            {
                hsCourses.Add(Convert.ToInt32(item.Value));
            }
        }
         */
        //send information to BLL for processing and narrow down possible results
        List<ProgramResult> programResults = new List<ProgramResult>();
        programResults = StudentController.FindProgramMatches(hsCourses);

        List<ProgramResult> finalResults = new List<ProgramResult>();
        finalResults = StudentController.EntranceReq_Pref_Match(preferenceResults, programResults);
        //display results once queries are complete
        ResultsView.DataSource = finalResults;
        ResultsView.DataBind();
        results.Visible = true;
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
            //AshleyTestController sysmgr = new AshleyTestController();
            //int reportid = sysmgr.ReportingDataAddProgramInfo(programid, semester, switchProgram);
            //ReportLabel.Text = reportid.ToString();
        }
    }
    protected void onPreviousClick(object sender, EventArgs e)
    {
        step2.Visible = false;
        stepOne.Visible = true;
    }
    protected void Populate_Program(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
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