﻿using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Briand_Workspace_ProgramEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /* ----------------------------------- SEARCH --------------------*/
    #region main
    protected void Program_Search(object sender, EventArgs e)
    {
        string search = Search_Box.Text;
        AdminController sysmgr = new AdminController();
        var programData = sysmgr.Program_Search(search, null);

        Program_List.DataSource = programData;
        Program_List.DataBind();
        ProgramList.Visible = true;
        ProgramEditDiv.Visible = false;
    }

    protected void Populate_Program_Info(object sender, EventArgs e)
    {
        // Get the program
        LinkButton button = (LinkButton)(sender);
        
        int programID = Convert.ToInt32(button.CommandArgument);
        AdminController sysmgr = new AdminController();
        Program myProgram = sysmgr.Get_Program(programID);

        // Populate the program info
        ProgramIDLabel.Text = myProgram.ProgramID.ToString();
        TB_ProgramName.Text = myProgram.ProgramName;
        ProgramNameLabel.Text = myProgram.ProgramName;
        DL_CredentialType.SelectedValue = myProgram.CredentialTypeID.ToString();
        TB_Description.Text = myProgram.ProgramDescription;
        TB_Credits.Text = myProgram.TotalCredits.ToString();
        TB_Length.Text = myProgram.ProgramLength;
        TB_CompetitiveAdvantage.Text = myProgram.CompetitiveAdvantage.ToString();
        CB_Active.Checked = myProgram.Active;
        TB_Link.Text = myProgram.ProgramLink;

        // populate the other info       
        CB_Categories.DataBind();
        Populate_Categories(programID);

        Populate_Courses(programID);
        Populate_Equivalencies(programID);

        GV_Questions.DataBind();
        Populate_Preferences(programID);

        Populate_EntranceReqs(programID);

        // show the appropriate info
        ProgramEditDiv.Visible = true;
        buttons.Visible = true;
        ProgramInfo_Show(sender, e);
        ProgramList.Visible = false;
    }

    #endregion

    /* -----------------------------------PROGRAM INFO --------------------*/
    #region program info
    protected void ProgramInfo_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = true;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
    }

    protected void Save_Program(object sender, EventArgs e)
    {
        AdminController sysmr = new AdminController();

        var program = new Program();
        program.ProgramID = int.Parse(ProgramIDLabel.Text);
        program.CredentialTypeID = int.Parse(DL_CredentialType.SelectedValue);
        program.ProgramName = TB_ProgramName.Text;
        program.ProgramDescription = TB_Description.Text;
        string credits = TB_Credits.Text;

        if (string.IsNullOrEmpty(credits))
        {
            program.TotalCredits = null;

        }
        else
        {
            program.TotalCredits = double.Parse(credits);
        }

        program.ProgramLength = TB_Length.Text;
        program.CompetitiveAdvantage = int.Parse(TB_CompetitiveAdvantage.Text);
        program.Active = CB_Active.Checked;
        program.ProgramLink = TB_Link.Text;

        sysmr.Program_Update(program);

        // go to next
        Categories_Show(sender, e);
    }
    #endregion
    /*-- ----------------------------- CATEGORIES ---------------------------------------*/

    #region categories
    protected void Categories_Show(object sender, EventArgs e)
    {

        ProgramInfo.Visible = false;
        Categories.Visible = true;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;

    }

    protected void Populate_Categories(int programID)
    {
        AdminController sysmgr = new AdminController();
        List<int> catList = sysmgr.Get_Categories_By_Program(programID);

        int catID;
    
        if(CB_Categories.Items.Count > 0)
        {
            foreach (ListItem x in CB_Categories.Items)
            {
                catID = Convert.ToInt32(x.Value);
                if (catList.Contains(catID))
                {
                    x.Selected = true;
                }
            }
        }        
    }
    protected void Save_Categories(object sender, EventArgs e)
    {


        EntranceReq_Show(sender, e);
    }
    #endregion

    /*-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------*/
        #region entrance requirements


    //CREATE NO SUBJECTREQUIREMENT GRIDVIEW
    private void PopulateManual()
     {
         //Create DataTable
         DataTable dt = new DataTable();
         DataRow dr = null;
 
         //Add initail values to DataTable
         dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
         dt.Columns.Add(new DataColumn("Column1Course", typeof(string)));
         dt.Columns.Add(new DataColumn("Column2Course", typeof(string)));
 
         dr = dt.NewRow();
         dr["RowNumber"] = 1;
         dr["Column1Course"] = string.Empty;
         dr["Column2Course"] = string.Empty;
 
         dt.Rows.Add(dr);
         dr = dt.NewRow();
 
         //Store the DataTable in ViewState
         ViewState["CurrentTableCourse"] = dt;
         GV_ManualNewEntrReq.DataSource = dt;
         GV_ManualNewEntrReq.DataBind();
 
     }

    #region for existing entrance requirements
    protected void Populate_EntranceReqs(int programID)
    {
        testController sysmgr = new testController();
        var entReq = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataSource = entReq;
        LV_SubjectReq.DataBind();
    }

    protected void LV_SubjectReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        testController sysmgr = new testController();
        int entReqID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        int programID = 2046;
        sysmgr.ER_Delete(entReqID);
        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }
    #endregion

    #region for adding an entrance requirement
    //pre-populate subject requirement courses
    protected void SubjectButton_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        int subjectReq = Convert.ToInt32(DL_SubjDesc.SelectedValue);
        if (subjectReq != 0)
        {
            prePopulatedER.Visible = true;
            manualER.Visible = false;
            SubReqDesc.Visible = false;
            GV_NewEntrReq.DataSource = sysmgr.Get_CoursesBySubjectRequirement(subjectReq);
            GV_NewEntrReq.DataBind();
        }
        else
        {
            manualER.Visible = true;
            SubReqDesc.Visible = true;
            prePopulatedER.Visible = false;
            PopulateManual();
        }
        
    }

    //remove course from requirement list
    protected void GV_NewEntrReq_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView ER_GridView = sender as GridView;
        
        List<GetEntranceReq> items = new List<GetEntranceReq>();
        for (int index = 0; index < ER_GridView.Rows.Count; index++)
        {
            if (index != e.NewSelectedIndex)
            {
                GridViewRow keepRow = ER_GridView.Rows[index];
                var id = keepRow.FindControl("ID") as Label;
                var course = keepRow.FindControl("Course") as Label;
                var mark = keepRow.FindControl("Mark") as TextBox;
                if (mark.Text.Trim() == "")
                {
                    mark.Text = "0";
                }
                items.Add(new GetEntranceReq()
                {
                    HSCourseID = Convert.ToInt32(id.Text),
                    HSCourseName = course.Text,
                    Mark = Convert.ToInt32(mark.Text),
                });
            }
        }

        ER_GridView.DataSource = items;
        ER_GridView.DataBind();
    }    
    #endregion




    protected void AddNew_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in GV_ManualNewEntrReq.Rows)
        //{
            //var course = row.FindControl("DL_Course") as DropDownList;
            //var poQtyLabel = row.FindControl("Marks") as TextBox;
            AddNewRowToCourse();
        //}
    }

    private void AddNewRowToCourse()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableCourse"] != null)
        {
            //create new datatable, cast datatable of viewstate
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableCourse"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the values
                    DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1Course"] = courseList.Text;
                    dtCurrentTable.Rows[i - 1]["Column2Course"] = marks.Text;

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTableCourse"] = dtCurrentTable;

                GV_ManualNewEntrReq.DataSource = dtCurrentTable;
                GV_ManualNewEntrReq.DataBind();
            }
        }

        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousCourseData();
    }    

    protected void GV_ManualNewEntrReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentTableCourse"] != null)
        {
            SetRowData();
            DataTable dt = (DataTable)ViewState["CurrentTableCourse"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTableCourse"] = dt;
                GV_ManualNewEntrReq.DataSource = dt;
                GV_ManualNewEntrReq.DataBind();

                for (int i = 0; i < GV_ManualNewEntrReq.Rows.Count - 1; i++)
                {
                    GV_ManualNewEntrReq.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousCourseData();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void SetPreviousCourseData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableCourse"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableCourse"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

                    courseList.Text = dt.Rows[i]["Column1Course"].ToString();
                    marks.Text = dt.Rows[i]["Column2Course"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableCourse"];
        DataRow drCurrentRow = null;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {
                DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = i + 1;
                dtCurrentTable.Rows[i - 1]["Column1Course"] = courseList.SelectedValue;
                dtCurrentTable.Rows[i - 1]["Column2Course"] = marks.Text;

                rowIndex++;
            }

            ViewState["CurrentTableCourse"] = dtCurrentTable;
            //grvStudentDetails.DataSource = dtCurrentTable;
            //grvStudentDetails.DataBind();
        }
        //SetPreviousData();
    }

    protected void addPPSubjectButton_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        List<AddEntranceRequirements> er = new List<AddEntranceRequirements>();

        int programID = Int32.Parse(ProgramIDLabel.Text);
        int subReqID = Convert.ToInt32(DL_SubjDesc.SelectedValue);
        int mark;

        foreach (GridViewRow row in GV_NewEntrReq.Rows)
        {
            int hsID = Convert.ToInt32((row.FindControl("ID") as Label).Text);
            if ((row.FindControl("Mark") as TextBox).Text.Trim() != "")
            {
                mark = Convert.ToInt32((row.FindControl("Mark") as TextBox).Text);
            }
            else
            {
                mark = 0;
            }
            er.Add(new AddEntranceRequirements(
                hsID,
                subReqID,
                programID,
                mark));
        }
            sysmgr.AddEntranceRequirement(er);

            LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
            LV_SubjectReq.DataBind();
    }

    protected void addMSubjectButton_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        List<AddEntranceRequirements> er = new List<AddEntranceRequirements>();
        string description = SubReqDesc.Text;

        int subReqID = sysmgr.AddSubjectRequirement(description);

        int programID = Int32.Parse(ProgramIDLabel.Text);       
        int mark;

        foreach (GridViewRow row in GV_ManualNewEntrReq.Rows)
        {
            int hsID = Convert.ToInt32((row.FindControl("DL_Course") as DropDownList).SelectedValue);
            if ((row.FindControl("Marks") as TextBox).Text.Trim() != "")
            {
                mark = Convert.ToInt32((row.FindControl("Marks") as TextBox).Text);
            }
            else
            {
                mark = 0;
            }
            er.Add(new AddEntranceRequirements(
                hsID,
                subReqID,
                programID,
                mark));
        }
        sysmgr.AddEntranceRequirement(er);

        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }

    //#region entrance requirements
    protected void EntranceReq_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = true;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
    }

    //protected void Populate_EntranceReqs(int programID) 
    //{
    //    AdminController sysmgr = new AdminController();
    //    //populate High School Entrance Requirements
    //    List<SubjectRequirement> subjectList = sysmgr.Get_SubjectReq_ByProgram(programID);
    //    LV_SubjectReq.DataSource = subjectList;
    //    LV_SubjectReq.DataBind();

    //    List<GetHSCourseIDName> ereqList = new List<GetHSCourseIDName> { };
    //    int subjectID;

    //    //and the classes for each subject

    //    foreach (ListViewItem x in LV_SubjectReq.Items)
    //    {
    //        subjectID = Convert.ToInt32((x.FindControl("SubjectIDLabel") as Label).Text);
    //        var courses = x.FindControl("GV_EntranceReqs") as GridView;
    //        ereqList = sysmgr.Get_EntReq_ByProgram_Subject(programID, subjectID);
    //        courses.DataSource = ereqList;
    //        courses.DataBind();            
    //    }




    //    //populate Degree Entrance Requirements
        
    //    List<DegreeEntranceRequirement> degEntList = sysmgr.Get_DegEntReq_ByProgram(programID);

    //    LV_DegreeEntranceReq.DataSource = degEntList;
    //    LV_DegreeEntranceReq.DataBind();        
    //}

    protected void GV_EntranceReqs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int subjectID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        //int subjectID = Convert.ToInt32((x.FindControl("SubjectIDLabel") as Label).Text);
        //int programID = Int32.Parse(ProgramIDLabel.Text);
        //int eReqid = Convert.ToInt32(GV_EntranceReqs.DataKeys[e.RowIndex].Value);
        //AdminController sysmgr = new AdminController();
        //sysmgr.EntranceReq_Delete(eReqid);
        //GV_EntranceReqs.DataSource = sysmgr.Get_EntReq_ByProgram_Subject(programID, subjectID);
        //GV_EntranceReqs.DataBind();
    }

    protected void Save_EntranceReq(object sender, EventArgs e)
    {

        Courses_Show(sender, e);
    }
 #endregion
    /*-- ----------------------------- COURSES ---------------------------------------*/
    #region courses
    protected void Courses_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = true;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
    }

    protected void Populate_Courses (int programID){
        AdminController sysmgr = new AdminController();
        var courseData = sysmgr.GetCoursesByProgramSemester(programID, 1);

        LV_ProgramCourses_One.DataSource = courseData;
        LV_ProgramCourses_One.DataBind();

        courseData = sysmgr.GetCoursesByProgramSemester(programID, 2);

        LV_ProgramCourses_Two.DataSource = courseData;
        LV_ProgramCourses_Two.DataBind();

        courseData = sysmgr.GetCoursesByProgramSemester(programID, 3);

        LV_ProgramCourses_Three.DataSource = courseData;
        LV_ProgramCourses_Three.DataBind();

        courseData = sysmgr.GetCoursesByProgramSemester(programID, 4);

        LV_ProgramCourses_Four.DataSource = courseData;
        LV_ProgramCourses_Four.DataBind();

        courseData = sysmgr.GetCoursesByProgramSemester(programID, 5);

        LV_ProgramCourses_More.DataSource = courseData;
        LV_ProgramCourses_More.DataBind();
    }

    protected void ProgramCourses_Search(object sender, EventArgs e)
    {
        SelectNaitCourseController sysmgr = new SelectNaitCourseController();
        List<NAITCourse> naitcourses = sysmgr.SearchNaitCourses(TB_ProgramCoursesSearch.Text, 0);
        LV_ProgramCoursesSearch.DataSource = naitcourses;
        LV_ProgramCoursesSearch.DataBind();
    }

    protected void Add_Program_Course(object sender, ListViewCommandEventArgs args)
    {
        ListViewItem item = args.Item;

        int programID = Int32.Parse(ProgramIDLabel.Text);
        int courseID = Convert.ToInt32((item.FindControl("CourseIDLabel") as Label).Text);
        int? semester = Convert.ToInt32((item.FindControl("DL_Semester") as DropDownList).SelectedValue);
        if (semester == -1)
        {
            semester = null;
        }

        ProgramCourse progCourse = new ProgramCourse()
        {
            CourseID = courseID,
            ProgramID = programID,
            Semester = semester
        };

        AdminController sysmgr = new AdminController();
        sysmgr.AddProgramCourse(progCourse);

        Populate_Courses(programID);
    }


    protected void Remove_Program_Course(object sender, ListViewCommandEventArgs args)
    {
        ListViewItem item = args.Item;

        int programID = Int32.Parse(ProgramIDLabel.Text);
        int courseID = Convert.ToInt32((item.FindControl("CourseIDLabel") as Label).Text);

        ProgramCourse progCourse = new ProgramCourse()
        {
            CourseID = courseID,
            ProgramID = programID
        };

        AdminController sysmgr = new AdminController();
        sysmgr.DeleteProgramCourse(progCourse);

        Populate_Courses(programID);
    }

    protected void Save_Courses(object sender, EventArgs e)
    {


        CourseEquivalencies_Show(sender, e);
    }

    #endregion
    /*-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------*/
    #region equivalencies
    protected void CourseEquivalencies_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = true;
        ProgramPreferences.Visible = false;
    }

    protected void Populate_Equivalencies(int programID)
    {
        AdminController sysmgr = new AdminController();
        var equivalencies = sysmgr.GetEquivalencies(programID);
        GV_Equivalencies.DataSource = equivalencies;
        GV_Equivalencies.DataBind();
    }

    protected void EmptyEquivalentProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        EquivalentCourseID.DataBind();
    }    

    protected void Enter_Click(object sender, EventArgs e)
    {
        //MessageUserControl.TryRun(() =>
        //{
            AdminController sysmgr = new AdminController();
            int programID = Int32.Parse(ProgramIDLabel.Text);
            int courseID = int.Parse(EmptyCurrentDropdown.SelectedValue);
            int destinationCourseID = int.Parse(EquivalentCourseID.SelectedValue);
            sysmgr.AddEquivalency(programID, courseID, destinationCourseID);
            GV_Equivalencies.DataSource = sysmgr.GetEquivalencies(programID);
            GV_Equivalencies.DataBind();

            //reset add equivalency screen
            EmptyCurrentDropdown.Items.Clear();
            EmptyCurrentDropdown.DataBind();
            EmptyEquivalentProgram.Items.Clear();
            EmptyEquivalentProgram.DataBind();
            EquivalentCourseID.Items.Clear();
            EquivalentCourseID.DataBind();

        //}, "", "Equivalency Successfully Added");
    }

    protected void EquivalenciesGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int programID = Int32.Parse(ProgramIDLabel.Text);
        int equivalencyid = Convert.ToInt32(GV_Equivalencies.DataKeys[e.RowIndex].Value);
        AdminController sysmgr = new AdminController();
        sysmgr.Equivalency_Delete(equivalencyid);
        GV_Equivalencies.DataSource = sysmgr.GetEquivalencies(programID);
        GV_Equivalencies.DataBind();
    }

    protected void Save_CourseEquivalencies(object sender, EventArgs e)
    {
        ProgramPreferences_Show(sender, e);
    }
    #endregion
    /*-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------*/
    #region preferences
    protected void ProgramPreferences_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = true;
    }

    protected void Populate_Preferences(int programID)
    {
        AdminController sysmgr = new AdminController();
        var progQuestions = sysmgr.GetQuestionsByProgram(programID);
        GetProgramPreferenceQuestions question = new GetProgramPreferenceQuestions();
        List<int> questionAns = new List<int>();
        int q_ID;

        questionAns = (from x in progQuestions
                       select x.QuestionID).ToList();

        if (GV_Questions.Rows.Count > 0)
        {
            for (int i = 0; i < GV_Questions.Rows.Count; i++)
            {
                var RB_List = GV_Questions.Rows[i].FindControl("RB_Preference") as RadioButtonList;
                var idbox = GV_Questions.Rows[i].FindControl("QuestionID") as Label;
                q_ID = Convert.ToInt32(idbox.Text);

                if (questionAns.Contains(q_ID))
                {
                    question = (from x in progQuestions
                                where x.QuestionID == q_ID
                                select x).FirstOrDefault();

                    if (question != null)
                    {

                        RB_List.SelectedValue = question.Answer.ToString();
                    }
                }
            }

        }
    }
    protected void Save_Questions(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        List<ProgramPreference> prefs = new List<ProgramPreference> {};
        int programID = int.Parse(ProgramIDLabel.Text);


       if (GV_Questions.Rows.Count > 0)
        {
            foreach (GridViewRow row in GV_Questions.Rows)
            {
                var RB_List = row.FindControl("RB_Preference") as RadioButtonList;
                var idbox = row.FindControl("QuestionID") as Label;
                int q_ID = Convert.ToInt32(idbox.Text);

                if (RB_List.SelectedIndex > -1)
                {
                    prefs.Add(new ProgramPreference {
                        QuestionID = q_ID,
                        ProgramID = programID,
                        Answer = Convert.ToInt32(RB_List.SelectedValue)
                    });
                }
            }

        }

       sysmgr.UpdateProgramPreferences(prefs);
    }
    #endregion

}