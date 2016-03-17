using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentPreferences : System.Web.UI.Page
{
    DataTable CoursesSelected;
    List<ProgramResult> finalResults;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataColumn CourseID;
            DataColumn CourseCode;
            DataColumn CourseName;
            DataColumn CourseCredits;
            CoursesSelected = new DataTable();
            if (Session["CoursesSelected"] == null)
            {
                CourseID = new DataColumn();
                CourseID.DataType = System.Type.GetType("System.Int32");
                CourseID.ColumnName = "CourseID";
                CourseID.Caption = "CourseID";
                CoursesSelected.Columns.Add(CourseID);

                CourseCode = new DataColumn();
                CourseCode.DataType = System.Type.GetType("System.String");
                CourseCode.ColumnName = "CourseCode";
                CourseCode.Caption = "CourseCode";
                CoursesSelected.Columns.Add(CourseCode);

                CourseName = new DataColumn();
                CourseName.DataType = System.Type.GetType("System.String");
                CourseName.ColumnName = "CourseName";
                CourseName.Caption = "CourseName";
                CoursesSelected.Columns.Add(CourseName);

                CourseCredits = new DataColumn();
                CourseCredits.DataType = System.Type.GetType("System.Double");
                CourseCredits.ColumnName = "CourseCredits";
                CourseCredits.Caption = "CourseCredits";
                CoursesSelected.Columns.Add(CourseCredits);
                //DataColumn CrsCode = new DataColumn("CrsCode");
                //CrsCode.DataType = Type.GetType("System.String");
                //CoursesSelected.Columns.Add(CrsCode);
                DataColumn[] pCol = new DataColumn[1];
                pCol[0] = CourseID;
                CoursesSelected.PrimaryKey = pCol;
                ViewState["CoursesSelected"] = CoursesSelected;
            }
            else
            {
                CoursesSelected = (DataTable)Session["CoursesSelected"];
                ViewState["CoursesSelected"] = CoursesSelected;
            }
            //Session["table"] = CoursesSelected;
            int count = 0;
            foreach (DataRow row1 in CoursesSelected.Rows)
            {
                count++;
            }
            TotalCourseLabel.Text = "Total courses : " + count;
            rptCourse.DataSource = CoursesSelected;
            rptCourse.DataBind();
            CourseGridView.Visible = false;
        }
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
        stepFour.Visible = false;

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

            finalResults = new List<ProgramResult>();
            finalResults = StudentController.EntranceReq_Pref_Match(preferenceResults, programResults);

            //grab the list of courses selected by the student in the NAIT course repeater
            //send that list and the list of final program matches to the student controller for comparison
            //if any courses selected match both programIDs add the course credit to that particular program result
            List<GetCourseCredits> courseCredits = new List<GetCourseCredits>();
            foreach (RepeaterItem rptItem in rptCourse.Items)
            {
                //courseCredits.Add(rptItem.FindControl("course-id"));
            }

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


    protected void Populate_Program(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int category = Convert.ToInt32(CategoryDropDown.SelectedValue);
        ProgramDropDown.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramDropDown.DataBind();
    }
    //======1
    protected void stepOneNext_Click(object sender, EventArgs e)
    {
        while (ProgramDropDown.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("You must select a program");
        }
        stepOne.Visible = false;
        stepTwo.Visible = true;
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
    /*protected void onPreviousClick(object sender, EventArgs e)
    {
        stepTwo.Visible = false;
        stepOne.Visible = true;
    }
     */
    //==========2
    protected void stepTwoNext_Click(object sender, EventArgs e)
    {
        stepTwo.Visible = false;
        stepThree.Visible = true;
    }
    protected void stepTwoPrevious_Click(object sender, EventArgs e)
    {
        stepTwo.Visible = false;
        stepOne.Visible = true;
    }

    //===========3
    protected void stepThreePrevious_Click(object sender, EventArgs e)
    {
        stepThree.Visible = false;
        stepTwo.Visible = true;
    }
    protected void stepThreeNext_Click(object sender, EventArgs e)
    {
        stepThree.Visible = false;
        stepFour.Visible = true;
    }
    //=============4
    protected void stepFourPrevious_Click(object sender, EventArgs e)
    {
        stepFour.Visible = false;
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
    //===============================================select nait course==================================================================================
    protected void SelectCourses(object sender, GridViewSelectEventArgs e)
    {

        GridViewRow row = CourseGridView.Rows[e.NewSelectedIndex];
        string CCode = (row.FindControl("CourseCode") as Label).Text;
        string CName = (row.FindControl("CourseName") as Label).Text;
        int id = int.Parse((row.FindControl("CourseID") as Label).Text);
        double CCredits = double.Parse((row.FindControl("CourseCredits") as Label).Text);
        //CourseRepeater.CourseCodeLabel.Text = id;
        //MultiSelect(CCode, id, CCredits);
        DataRow dtrow;
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        dtrow = CoursesSelected.NewRow();
        dtrow["CourseID"] = id;
        dtrow["CourseCode"] = CCode;
        dtrow["CourseName"] = CName;
        dtrow["CourseCredits"] = CCredits;

        //how to delete duplicate value
        DataRow findRow = CoursesSelected.Rows.Find(id);
        if (findRow == null)
        {
            CoursesSelected.Rows.Add(dtrow);
        }
        else
        {
            CoursesSelected.Rows.Find(id).Delete();
            CoursesSelected.Rows.Add(dtrow);
        }
        int count = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            count++;
        }
        ViewState["CoursesSelected"] = CoursesSelected;

        rptCourse.DataSource = CoursesSelected;
        rptCourse.DataBind();

        TotalCourseLabel.Text = "Total courses : " + count;
        for (int i = 0; i < CourseGridView.Rows.Count; i++)
        {
            CourseGridView.Rows[i].Font.Bold = false;
            for (int j = 0; j < CoursesSelected.Rows.Count; j++)
            {
                if (CourseGridView.DataKeys[i]["CourseID"].ToString() == CoursesSelected.Rows[j]["CourseID"].ToString())
                {
                    CourseGridView.Rows[i].BackColor = System.Drawing.Color.FromName("#D1DDF1");
                    CourseGridView.Rows[i].Font.Bold = true;
                    CourseGridView.Rows[i].ForeColor = System.Drawing.Color.FromName("#333333");
                }
            }
        }

    }
    protected void Next_Click(object sender, EventArgs e)
    {
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        Session["CoursesSelected"] = CoursesSelected;
        Response.Redirect("../Student/testpage.aspx");
    }


    protected void rptCourse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int courserId = Convert.ToInt32(e.CommandArgument);
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        if (e.CommandName == "Delete" && e.CommandArgument.ToString() != "")
        {
            CoursesSelected.Rows.Find(courserId).Delete();
            rptCourse.DataSource = CoursesSelected;
            rptCourse.DataBind();
        }

        CourseGridView.DataBind();
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        CourseGridView.Visible = true;
        CourseGridView.DataBind();

    }
    protected void reset_Click(object sender, EventArgs e)
    {
        //Session["CoursesSelected"] = null;
        //ViewState["CoursesSelected"] = null;
        CoursesSelected = null;
        CourseGridView.DataSource = null;
        CourseGridView.DataBind();
        rptCourse.DataBind();
        TotalCourseLabel.Text = "Total courses : 0";
        //Response.Redirect("../Student/SelectNaitCourses.aspx");
    }

    //========================================================================================================================================


}