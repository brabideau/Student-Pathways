using CrystalBallSystem.BLL;
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
        if (!Request.IsAuthenticated)
        {
            //NO
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    /* ----------------------------------- SEARCH --------------------*/
    #region main
    protected void Program_Search(object sender, EventArgs e)
    {
        string search = Search_Box.Text;
        AdminController sysmgr = new AdminController();
        int category = int.Parse(CategoryDropDowList.SelectedValue.ToString());
        var programData = sysmgr.findProgram(search, category);
        //var programData = sysmgr.Program_Search(search, null);

        Program_List.DataSource = programData;
        Program_List.DataBind();
        ProgramList.Visible = true;
        ProgramEditDiv.Visible = false;
        Buttons.Visible = false;
        Add_Program_Button.Visible = true;
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
        Populate_DER(programID);

        // show the appropriate info
        ProgramEditDiv.Visible = true;
        Buttons.Visible = true;
        ProgramInfo_Show(sender, e);
        ProgramList.Visible = false;
        Tab_Labels.SelectedValue = "1";
    }

    #endregion
    protected void Change_Tab(object sender, EventArgs e)
    {
        string value = Tab_Labels.SelectedValue;

        switch (value)
        {
            case "1":
                ProgramInfo_Show(sender, e);
                break;
            case "2":
                Categories_Show(sender, e);
                break;
            case "3":
                EntranceReq_Show(sender, e);
                break;
            case "4":
                Courses_Show(sender, e);
                break;
            case "5":
                CourseEquivalencies_Show(sender, e);
                break;
            case "6":
                ProgramPreferences_Show(sender, e);
                break;
        }
    }


    /* -----------------------------------PROGRAM INFO --------------------*/
    #region program info
    protected void ProgramInfo_Show(object sender, EventArgs e)
    {
        Program_Add.Visible = false;
        ProgramInfo.Visible = true;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "1";
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

        string length = TB_Length.SelectedValue;
        if (length != "0")
        {
            program.ProgramLength = length;
        }
        
        string competitiveAdvantage = TB_CompetitiveAdvantage.Text;
        if (string.IsNullOrEmpty(competitiveAdvantage))
        {
            program.CompetitiveAdvantage = null;
        }
        else
        {
            program.CompetitiveAdvantage = int.Parse(competitiveAdvantage);
        }

        program.Active = CB_Active.Checked;
        program.ProgramLink = TB_Link.Text;

        if (string.IsNullOrEmpty(TB_ProgramName.Text))
        {
            MessageUserControl.ShowInfo("The Program Name is required.");
        }
        else if(TB_Length.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("The program length is required.");
        }
        else
        {
            MessageUserControl.TryRun(() => sysmr.Program_Update(program), "Updated Success.", "You uppdated the program");
            Categories_Show(sender, e);
        }
        
    }
    #endregion
    /*-- ----------------------------- CATEGORIES ---------------------------------------*/

    #region categories
    protected void Categories_Show(object sender, EventArgs e)
    {
        Add_Program_Button.Visible = false;
        ProgramInfo.Visible = false;
        Categories.Visible = true;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "2";

    }

    protected void Populate_Categories(int programID)
    {
        AdminController sysmgr = new AdminController();
        List<int> catList = sysmgr.Get_Categories_By_Program(programID);

        int catID;

        if (CB_Categories.Items.Count > 0)
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
        List<int> categories = new List<int>();
        for (int i = 0; i < CB_Categories.Items.Count; i++)
        {
            if (CB_Categories.Items[i].Selected)
            {
                categories.Add(int.Parse(CB_Categories.Items[i].Value));
            }
        }

        AdminController sysmr = new AdminController();
        int programid = sysmr.GetProgramIDByName(TB_ProgramName.Text);
        //int programid = Convert.ToInt32(ProgramIDLabel.Text);

        sysmr.AddProgramInCategories(categories, programid);

        EntranceReq_Show(sender, e);
    }
    #endregion

    /*-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------*/
    #region entrance requirements

    #region high school
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
    #endregion

    #region for existing entrance requirements
    protected void Populate_EntranceReqs(int programID)
    {
        AdminController sysmgr = new AdminController();
        var entReq = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataSource = entReq;
        LV_SubjectReq.DataBind();
    }

    protected void LV_SubjectReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int entReqID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        int programID = Int32.Parse(ProgramIDLabel.Text);
        sysmgr.ER_Delete(entReqID);
        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }
    #endregion

    #region for adding an entrance requirement
    //pre-populate subject requirement courses
    protected void SubjectButton_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        manualER.Visible = true;
        SubReqDesc.Visible = true;
        PopulateManual();
    }
    #endregion

    protected void AddNew_Click(object sender, EventArgs e)
    {
        AddNewRowToCourse();
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
        }
    }

    protected void addMSubjectButton_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
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
        manualER.Visible = false;
    }

    #region entrance requirements
    protected void EntranceReq_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = true;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "3";
    }
    #endregion

    #region post-secondary
    protected void Populate_DER(int programID)
    {
        AdminController sysmgr = new AdminController();
        GV_DegreeEntranceReq.DataSource = sysmgr.Get_DERByProgram(programID);
        GV_DegreeEntranceReq.DataBind();
    }

    protected void GV_DegReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int degReqID = Convert.ToInt32(GV_DegreeEntranceReq.DataKeys[e.RowIndex].Value);
        int programID = Int32.Parse(ProgramIDLabel.Text);
        sysmgr.DER_Delete(degReqID);
        Populate_DER(programID);
    }

    protected void addDER_Click(object sender, EventArgs e)
    {
        AddRequirements.Visible = true;
    }
    protected void Add_DER_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int programID = Int32.Parse(ProgramIDLabel.Text);
        int credentialID = int.Parse(DL_Credential.SelectedValue);
        int categoryID = int.Parse(DL_Category.SelectedValue);
        decimal gpa = decimal.Parse(TB_GPA.Text);
        sysmgr.AddDER(programID, credentialID, categoryID, gpa);
        Populate_DER(programID);
        AddRequirements.Visible = false;
    }
    #endregion

    protected void Save_EntranceReq(object sender, EventArgs e)
    {

        Courses_Show(sender, e);
    }
    #endregion
    /*-- ----------------------------- COURSES ---------------------------------------*/
    #region courses
    protected void Courses_Show(object sender, EventArgs e)
    {
        Add_Program_Button.Visible = false;
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = true;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "4";
    }

    protected void Populate_Courses(int programID)
    {
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
        Add_Program_Button.Visible = false;
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = true;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "5";
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
        Add_Program_Button.Visible = false;
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = true;
        Tab_Labels.SelectedValue = "6";
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
        List<ProgramPreference> prefs = new List<ProgramPreference> { };
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
                    prefs.Add(new ProgramPreference
                    {
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

    /* -----------------------------------ADD NEW PROGRAM --------------------*/
    #region AddProgram
    protected void Add_Program_Button_Click(object sender, EventArgs e)
    {
        ProgramEditDiv.Visible = true;
        Buttons.Visible = true;
        ProgramList.Visible = false;
        Program_Save.Visible = false;
        Program_Add.Visible = true;
        ProgramInfo.Visible = true;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Add_Program_Button.Visible = false;

        ProgramNameLabel.Text = "";
        ProgramIDLabel.Text = "";
        TB_ProgramName.Text = "";
        DL_CredentialType.SelectedValue = "1";
        TB_Description.Text = "";
        TB_Credits.Text = "";
        TB_Length.SelectedValue = "0";
        TB_CompetitiveAdvantage.Text = "";
        CB_Active.Checked = false;
        TB_Link.Text = "";

    }

    protected void Add_Program(object sender, EventArgs e)
    {
        string CredentialTypeId = DL_CredentialType.SelectedValue.ToString();
        string length = TB_Length.SelectedValue;
        string credits = TB_Credits.Text;
        string competitiveAdvantage = TB_CompetitiveAdvantage.Text;

        var program = new Program();
        program.CredentialTypeID = int.Parse(CredentialTypeId);
        program.ProgramName = TB_ProgramName.Text;
        program.ProgramDescription = TB_Description.Text;

        if (string.IsNullOrEmpty(credits))
        {
            program.TotalCredits = null;

        }
        else
        {
            program.TotalCredits = double.Parse(credits);
        }

        if (TB_Length.SelectedValue != "0")
        {
            program.ProgramLength = length;
        }
        


        if (string.IsNullOrEmpty(competitiveAdvantage))
        {
            program.CompetitiveAdvantage = null;
        }
        else
        {
            program.CompetitiveAdvantage = int.Parse(competitiveAdvantage);
        }

        program.Active = CB_Active.Checked;
        program.ProgramLink = TB_Link.Text;

        List<Program> NewProgram = new List<Program>();
        NewProgram.Add(program);


        AdminController sysmr = new AdminController();

        if (string.IsNullOrEmpty(TB_ProgramName.Text))
        {
            MessageUserControl.ShowInfo("The Program Name is required.");
        }
        else if(TB_Length.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("The program length is required.");
        }
        else
        {
            MessageUserControl.TryRun(() => sysmr.AddProgram(NewProgram), "Add Success.", "You added new program");
            Categories_Show(sender, e);
            ProgramNameLabel.Text = TB_ProgramName.Text;
        }



    }

    #endregion


}