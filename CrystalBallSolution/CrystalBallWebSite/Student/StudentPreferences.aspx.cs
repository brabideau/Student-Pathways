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
    List<int> finalResults;
    List<GetCourseCredits> completeResults;

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

                ViewState["BackupTable"] = CoursesSelected;
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
        int? programCategoryID, programID, semester;
        bool? programChange;
        StudentController sysmgr = new StudentController();
        int tempInt;
        stepFour.Visible = false;

        if (RBL_NAIT_Student.SelectedValue == "1" && int.TryParse((CategoryDropDown.SelectedValue), out tempInt) && int.TryParse(ProgramDropDown.SelectedValue, out tempInt) && int.TryParse(SemesterDropDown.SelectedValue, out tempInt))
        {
            programCategoryID = Convert.ToInt32(CategoryDropDown.SelectedValue);
            programID = Convert.ToInt32(ProgramDropDown.SelectedValue);
            programChange = Convert.ToBoolean(RBL_SwapPrograms.SelectedValue);
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

            //foreach (ListItem item in PrefQuestions.Items)
            //{
            //    myPreferences.Add(new StudentPreference(
            //        Convert.ToInt32(item.Value),
            //        item.Selected));
            //}
            foreach (GridViewRow row in prefGridView.Rows)
            {
                RadioButtonList rlist = row.FindControl("prefSelection") as RadioButtonList;
                int prefchoice = Convert.ToInt32(rlist.SelectedValue);

                myPreferences.Add(new StudentPreference(
                                Convert.ToInt32((row.FindControl("QuestionID") as Label).Text),

                                prefchoice
                ));
            }

            //foreach (GridViewRow row in PrefQuestions.Rows)
            //{
            //    myPreferences.Add(new StudentPreference(
            //               Convert.ToInt32(row.Cells[0].Text),
            //               (row.FindControl("RBL_YN") as CheckBox).Checked)
            //               );
            //}

            //prior to first step towards getting results log the relevant data for metrics gathering
            //NEW STUDENT - QuestionID/AnswerValue
            ReportController report = new ReportController();
            int currentProgID, currentSemester;
            bool changeProgram = true;
            if (RBL_NAIT_Student.SelectedValue == "1")
            {
                currentProgID = Convert.ToInt32(ProgramDropDown.SelectedValue);
                currentSemester = Convert.ToInt32(SemesterDropDown.SelectedValue);
                changeProgram = Convert.ToBoolean(RBL_SwapPrograms.SelectedValue);
                report.InsertCurrentStudentMetrics(myPreferences, currentProgID, currentSemester, changeProgram);
            }
            else
            {
                report.InsertNewStudentMetrics(myPreferences);
            }

            //send preferences to the BLL for initial results
            //List<int> preferenceResults = new List<int>();
            //preferenceResults = StudentController.FindPreferenceMatches(myPreferences);

            //step 3 - Gather the selected courses provided by the student. Determine if any selected courses are the highest in a particular course group
            List<int> hsCourses = new List<int>();
            List<GetHSCourses> courseList = new List<GetHSCourses>();
            courseList = sysmgr.GetCourseList();

            //Search for checked items in the check box list - if a checked item is a 30 level that
            //is flagged in the database as being the highest of a category
            //add all other courses in that category to hsCourses
            //foreach (GetHSCourses testItem in courseList)
            //{
            //    foreach (ListItem item in CB_CourseList.Items)
            //    {
            //        if (item.Selected && item.Value == testItem.HighSchoolCourseID.ToString() && testItem.HighSchoolHighestCourse == true)
            //        {
            //            int[] childCourses = sysmgr.GetHighestCourseLevel(Convert.ToInt32(item.Value));
            //            for (int i = 0; i < childCourses.Length; i++)
            //            {
            //                hsCourses.Add(Convert.ToInt32(childCourses[i]));
            //            }
            //        }
            //        else if (item.Selected)
            //        {
            //            hsCourses.Add(Convert.ToInt32(item.Value));
            //        }
            //    }
            //}

            //new code
            List<int> demoCourses = new List<int>();
            foreach (ListItem item in CB_CourseList.Items)
            {
                if (item.Selected)
                    demoCourses.Add(Convert.ToInt32(item.Value));
            }
            //call method to get a list of HSCourses that will then be sent to the HighSchoolCourses method
            List<GetHSCourses> hsCoursesTwo = new List<GetHSCourses>();
            hsCoursesTwo = sysmgr.FindHSCourses(demoCourses);
            //pass list of hs courses to get the final list of ints
            List<int> fullCourses = new List<int>();
            fullCourses = sysmgr.GetHighestCourseLevel(hsCoursesTwo);

            //step 4 - Determine initial program results and then filter those results based on student preference questions. Display final results
            //send information to BLL for processing and narrow down possible results
            List<int> programResults = new List<int>();
            programResults = StudentController.FindProgramMatches(fullCourses);

            //finalResults = new List<int>();
            //finalResults = StudentController.EntranceReq_Pref_Match(preferenceResults, programResults);

            //send list of course codes to the db and retrieve courseids
            DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
            List<int> courseIDs = new List<int>();
            foreach (DataRow x in CoursesSelected.Rows)
            {
                courseIDs.Add(Convert.ToInt32(x["CourseID"]));
            }
            //List<int> programIDs = sysmgr.GetProgramIDs(finalResults);

            //send list of courseids and list of programids to the db for final results
            //completeResults = new List<GetCourseCredits>();
            //completeResults = sysmgr.GetCourseCredits(courseIDs, finalResults);
            List<ProgramResult> finalProgramResults = StudentController.EntranceReq_Pref_Match(myPreferences, programResults, courseIDs);

            finalProgramResults = (from x in finalProgramResults.AsEnumerable()
                                   where x.MatchPercent > 60
                                   orderby x.MatchPercent descending
                                   select x).ToList();

            Session["finalProgramResults"] = finalProgramResults;

            ResultsView.DataSource = finalProgramResults;
            ResultsView.DataBind();


            //insert program results to db
            //report.InsertProgramResults(completeResults);


            //display results once queries are complete
            //ResultsView.DataSource = completeResults;
            //ResultsView.DataBind();
            results.Visible = true;
        }, "Success!", "Here are the pathways available to you!");


    }
    protected void CurrentStudent_CheckedChanged(object sender, EventArgs e)
    {
        if (RBL_NAIT_Student.SelectedValue == "1")
        {
            chooseProgram.Visible = true;
        }
        else
        {
            chooseProgram.Visible = false;
        }
        //if (CurrentStudent.Checked)
        //{
        //    chooseProgram.Visible = true;
        //}
        //else
        //{
        //    chooseProgram.Visible = false;
        //}
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
        if (RBL_GraduatedPostSecondary.SelectedValue == "true" && TB_GPA.Text == "")
        {
            MessageUserControl.ShowInfo("You must enter your GPA.");
        }
        else
        {
            stepOne.Visible = false;
            stepTwo.Visible = true;
            PrefQuestions.DataBind();
        }
        //while (ProgramDropDown.SelectedValue == "0")
        //{
        //    MessageUserControl.ShowInfo("You must select a program");
        //}

        //int programid;
        //int semester;
        //bool switchProgram;

        //if (CurrentStudent.Checked == true && int.TryParse(ProgramDropDown.SelectedValue, out programid) && int.TryParse(SemesterDropDown.SelectedValue, out semester))
        //{
        //    programid = int.Parse(ProgramDropDown.SelectedValue);
        //    semester = int.Parse(SemesterDropDown.SelectedValue);
        //    if (ChangeProgram.Checked == true)
        //    {
        //        switchProgram = true;
        //    }
        //    else
        //    {
        //        switchProgram = false;
        //    }
        //}
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
        DataTable CoursesSelected;
        if (RBL_NAIT_Student.SelectedValue == "0")
        {
            //skip the nait course selection page
            Submit_Click(sender, e);
            stepThree.Visible = false;
        }
        else if (RBL_NAIT_Student.SelectedValue == "1" && CategoryDropDown.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("If you are a current student you must select a program.");
        }
        else
        {
            stepThree.Visible = false;
            SelectNaitCourseController course = new SelectNaitCourseController();
            int programID, semester;
            List<NAITCourse> courses = new List<NAITCourse>();
            //add code to populate drop down list and auto add courses
            //run the search for for the program automatically and fill the basket based on prefill results
            if (RBL_NAIT_Student.SelectedValue == "1")
            {
                programID = Convert.ToInt32(ProgramDropDown.SelectedValue);
                semester = Convert.ToInt32(SemesterDropDown.SelectedValue);
                courses = StudentController.Prefill_Courses(programID, semester);

                foreach (var item in courses)
                {
                    //add items to the basket
                    //step 1 add a new datatable row
                    int id = item.CourseID;
                    double CCredits = item.CourseCredits;
                    string CCode = item.CourseCode, CName = item.CourseName;
                    DataRow dr;
                    CoursesSelected = (DataTable)ViewState["CoursesSelected"];
                    dr = CoursesSelected.NewRow();
                    dr["CourseID"] = id;
                    dr["CourseCode"] = CCode;
                    dr["CourseName"] = CName;
                    dr["CourseCredits"] = CCredits;

                    //find duplicates and add if there are none
                    DataRow findRow = CoursesSelected.Rows.Find(id);
                    if (findRow == null)
                    {
                        CoursesSelected.Rows.Add(dr);
                    }
                    ViewState["CoursesSelected"] = CoursesSelected;

                    //int count = 0;
                    //foreach (DataRow row1 in CoursesSelected.Rows)
                    //{
                    //count++;
                    //}
                    ViewState["CoursesSelected"] = CoursesSelected;

                    rptCourse.DataSource = CoursesSelected;
                    rptCourse.DataBind();

                    //TotalCourseLabel.Text = "Total courses : " + count;

                }
                //set drop down list to programid
                //filter search results based on programid
                ProgramDropDownList.DataBind();
                ProgramDropDownList.SelectedValue = programID.ToString();

                CourseGridView.DataSource = course.SearchNaitCourses(null, programID);
                CourseGridView.DataBind();
                CourseGridView.Visible = true;
                CoursesSelected = (DataTable)ViewState["CoursesSelected"];
                int count = 0;
                foreach (DataRow row1 in CoursesSelected.Rows)
                {
                    count++;
                }
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

            stepFour.Visible = true;
        }
    }
    //=============4
    protected void stepFourPrevious_Click(object sender, EventArgs e)
    {
        //clear the repeater
        DataTable BackupTable = (DataTable)ViewState["BackupTable"];
        Session["CoursesSelected"] = BackupTable;
        ViewState["CoursesSelected"] = BackupTable;

        CoursesSelected = null;
        CourseGridView.DataSource = null;
        CourseGridView.DataBind();
        rptCourse.DataBind();
        TotalCourseLabel.Text = "Total courses : 0";
        stepFour.Visible = false;
        stepThree.Visible = true;
    }


    //should I be resetting the values when they click search again? It was mentioned that they wanted this stuff to be saved.
    protected void searchAgain_Click(object sender, EventArgs e)
    {
        //clear the repeater
        DataTable BackupTable = (DataTable)ViewState["BackupTable"];
        Session["CoursesSelected"] = BackupTable;
        ViewState["CoursesSelected"] = BackupTable;

        CoursesSelected = null;
        CourseGridView.DataSource = null;
        CourseGridView.DataBind();
        rptCourse.DataBind();
        TotalCourseLabel.Text = "Total courses : 0";
        results.Visible = false;
        stepOne.Visible = true;
        //add code to reset all fields for steps 1 -3



        //foreach (ListItem item in PrefQuestions.Items)
        //{
        //    item.Selected = true;
        //}
        //foreach (ListItem item in CB_CourseList.Items)
        //{
        //    item.Selected = false;
        //}
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
        //else
        //{
        //    CoursesSelected.Rows.Find(id).Delete();
        //    CoursesSelected.Rows.Add(dtrow);
        //}
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
        SelectNaitCourseController course = new SelectNaitCourseController();
        List<NAITCourse> courses = new List<NAITCourse>();
        CourseGridView.DataSource = course.SearchNaitCourses(SearchTextBox.Text, int.Parse(ProgramDropDownList.SelectedValue));
        CourseGridView.DataBind();
        int count = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            count++;
        }
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
    protected void Search_Click(object sender, EventArgs e)
    {
        SelectNaitCourseController course = new SelectNaitCourseController();
        List<NAITCourse> courses = new List<NAITCourse>();
        CourseGridView.DataSource = course.SearchNaitCourses(SearchTextBox.Text, 0);
        CourseGridView.DataBind();
        CourseGridView.Visible = true;
        SearchTextBox.Text = null;

    }
    protected void reset_Click(object sender, EventArgs e)
    {
        DataTable BackupTable = (DataTable)ViewState["BackupTable"];
        Session["CoursesSelected"] = BackupTable;
        ViewState["CoursesSelected"] = BackupTable;

        CoursesSelected = null;
        SelectNaitCourseController course = new SelectNaitCourseController();
        List<NAITCourse> courses = new List<NAITCourse>();
        CourseGridView.DataSource = course.SearchNaitCourses(SearchTextBox.Text, 0);
        CourseGridView.DataBind();
        CourseGridView.Visible = true;
        SearchTextBox.Text = null;
        //CourseGridView.DataSource = null;
        //CourseGridView.DataBind();
        rptCourse.DataBind();
        TotalCourseLabel.Text = "Total courses : 0";


        //Response.Redirect("../Student/SelectNaitCourses.aspx");
    }
    protected void List_Change(object sender, EventArgs e)
    {
        SelectNaitCourseController course = new SelectNaitCourseController();
        List<NAITCourse> courses = new List<NAITCourse>();
        CourseGridView.DataSource = course.SearchNaitCourses(null, Convert.ToInt32(ProgramDropDownList.SelectedValue));
        CourseGridView.DataBind();
    }
    protected void RBL_GraduatedPostSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBL_GraduatedPostSecondary.SelectedValue == "true")
            graduated.Visible = true;
        else
            graduated.Visible = false;
    }

    protected void ResultsView_PagePropertiesChanged(object sender, PagePropertiesChangingEventArgs e)
    {

        //var finalProgramResults = ViewState["finalProgramResults"];
        List<ProgramResult> finalProgramResults = (List<ProgramResult>)Session["finalProgramResults"];

        (ResultsView.FindControl("DataPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

        ResultsView.DataSource = finalProgramResults;
        ResultsView.DataBind();
        //ListViewUpdatePanel.Update();


    }
}