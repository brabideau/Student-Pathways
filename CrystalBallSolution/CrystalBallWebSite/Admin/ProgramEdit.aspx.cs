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


public partial class Admin_ProgramEdit : System.Web.UI.Page
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

    protected void Get_Program_Info(object sender, EventArgs e)
    {
        // Get the program
        try
        {
            LinkButton button = (LinkButton)(sender);

            int programID = Convert.ToInt32(button.CommandArgument);


            // show the appropriate info
            Add_Program_Button.Visible = true;
            ProgramEditDiv.Visible = true;
            Buttons.Visible = true;

            ProgramList.Visible = false;
            Tab_Labels.SelectedValue = "1";

            Populate_Program_Info(programID);

            ProgramInfo_Show(sender, e);
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }


    protected void Populate_Program_Info(int programID)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            Program myProgram = sysmgr.Get_Program(programID);

            // Populate the program info
            ProgramIDLabel.Text = myProgram.ProgramID.ToString();
            TB_ProgramName.Text = myProgram.ProgramName;
            ProgramNameLabel.Text = myProgram.ProgramName;
            DL_CredentialType.SelectedValue = myProgram.CredentialTypeID.ToString();
            TB_Description.Text = myProgram.ProgramDescription;

            TB_Credits.Text = myProgram.TotalCredits.ToString();

            // TB_Length.Text = myProgram.ProgramLength;
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
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }
    #endregion

    #region navigation
    protected void Change_Tab(object sender, EventArgs e)
    {
        string value = Tab_Labels.SelectedValue;
        if (!string.IsNullOrEmpty(ProgramIDLabel.Text))
        {
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
        else
        {
            ProgramInfo_Show(sender, e);
            MessageUserControl.ShowInfo("Program does not found, please add new program first.");
        }
            
    }


    protected void ProgramInfo_Show(object sender, EventArgs e)
    {
        //Program_Add.Visible = false;
        BasicProgramInfo.Visible = true;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "1";
    }


    protected void Categories_Show(object sender, EventArgs e)
    {
        BasicProgramInfo.Visible = false;
        Categories.Visible = true;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "2";

    }


    protected void EntranceReq_Show(object sender, EventArgs e)
    {
        BasicProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = true;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "3";
    }


    protected void CourseEquivalencies_Show(object sender, EventArgs e)
    {
        Add_Program_Button.Visible = false;
        BasicProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = true;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "5";
    }


    protected void Courses_Show(object sender, EventArgs e)
    {
        Add_Program_Button.Visible = false;
        BasicProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = true;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
        Tab_Labels.SelectedValue = "4";
    }


    protected void ProgramPreferences_Show(object sender, EventArgs e)
    {
        Add_Program_Button.Visible = false;
        BasicProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = false;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = true;
        Tab_Labels.SelectedValue = "6";
    }

    #endregion


    /* -----------------------------------PROGRAM INFO --------------------*/
    #region program info


    protected void Save_Program(object sender, EventArgs e)
    {
        try
        {
            AdminController sysmr = new AdminController();


            if (ProgramIDLabel.Text != "")
            {
                var program = new Program();
                program.ProgramID = int.Parse(ProgramIDLabel.Text);
                program.CredentialTypeID = int.Parse(DL_CredentialType.SelectedValue);
                program.ProgramDescription = TB_Description.Text;
                program.Active = CB_Active.Checked;
                program.ProgramLink = TB_Link.Text;
                string credits = TB_Credits.Text;
                //string competitiveAdvantage;
                double dCredits;
                int cAdvantage;

                if (string.IsNullOrEmpty(TB_ProgramName.Text))
                {
                    MessageUserControl.ShowInfo("The Program Name is required.");
                }
                else
                {
                    program.ProgramName = TB_ProgramName.Text;
                    if (string.IsNullOrEmpty(credits))
                    {
                        program.TotalCredits = null;
                        if (TB_Length.SelectedValue == "0")
                        {
                            MessageUserControl.ShowInfo("The program length is required.");
                        }
                        else
                        {
                            program.ProgramLength = TB_Length.SelectedValue;
                            if (string.IsNullOrEmpty(TB_CompetitiveAdvantage.Text))
                            {
                                program.CompetitiveAdvantage = null;
                                MessageUserControl.TryRun(() => sysmr.Program_Update(program), "Updated Success.", "You updated the program");
                                Categories_Show(sender, e);
                                //need add things DONE!
                            }
                            else
                            {
                                if (int.TryParse(TB_CompetitiveAdvantage.Text, out cAdvantage))
                                {
                                    program.CompetitiveAdvantage = cAdvantage;
                                    MessageUserControl.TryRun(() => sysmr.Program_Update(program), "Updated Success.", "You updated the program");
                                    Categories_Show(sender, e);

                                }
                                else
                                {
                                    MessageUserControl.ShowInfo("Competitive advantage need to be a integer.");
                                }
                            }
                        }

                        //need fill in info.DONE!
                    }
                    else
                    {
                        if (!double.TryParse(credits, out dCredits))
                        {
                            MessageUserControl.ShowInfo("Credit must be a number.");
                        }
                        else
                        {
                            program.TotalCredits = dCredits;
                            if (TB_Length.SelectedValue == "0")
                            {
                                MessageUserControl.ShowInfo("The program length is required.");
                            }
                            else
                            {
                                program.ProgramLength = TB_Length.SelectedValue;
                                if (string.IsNullOrEmpty(TB_CompetitiveAdvantage.Text))
                                {
                                    program.CompetitiveAdvantage = null;
                                    MessageUserControl.TryRun(() => sysmr.Program_Update(program), "Updated Success.", "You updated the program");
                                    Categories_Show(sender, e);
                                    //need add things DONE!
                                }
                                else
                                {
                                    if (int.TryParse(TB_CompetitiveAdvantage.Text, out cAdvantage))
                                    {
                                        program.CompetitiveAdvantage = cAdvantage;
                                        MessageUserControl.TryRun(() => sysmr.Program_Update(program), "Updated Success.", "You updated the program");
                                        Categories_Show(sender, e);

                                    }
                                    else
                                    {
                                        MessageUserControl.ShowInfo("Competitive advantage need to be a integer.");
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                Add_Program(sender, e);
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
    }
    #endregion
    /*-- ----------------------------- CATEGORIES ---------------------------------------*/

    #region categories
  

    protected void Populate_Categories(int programID)
    {
        try
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
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void Save_Categories(object sender, EventArgs e)
    {
        try
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
            if (categories.Count != 0)
            {
                sysmr.AddProgramInCategories(categories, programid);
            }
            EntranceReq_Show(sender, e);
            MessageUserControl.ShowInfoPass("Category save success.");
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
        
    }
    #endregion

    /*-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------*/

    #region high schoolentrance requirements

    protected void Populate_EntranceReqs(int programID)
    {
        AdminController sysmgr = new AdminController();
        var entReq = sysmgr.Get_Subject_Requirement_Details(programID);
        LV_SubjectReq.DataSource = entReq;
        LV_SubjectReq.DataBind();
    }

    protected void Ent_Req_Commands(object sender, ListViewCommandEventArgs args)
    {
        try
        {
            string arg = args.CommandArgument.ToString();
            ListViewItem item = args.Item;

            int programID = int.Parse(ProgramIDLabel.Text);

            EntranceRequirement req = new EntranceRequirement();

            req.ProgramID = programID;

            req.HighSchoolCourseID = Convert.ToInt32((item.FindControl("DL_HS_Course") as DropDownList).SelectedValue);
            int subID = Convert.ToInt32(((sender as ListView).Parent.FindControl("SubjectIDLabel") as Label).Text);
            req.SubjectRequirementID = subID;


            string mark = (item.FindControl("Ent_Marks") as TextBox).Text;

            if (mark != "")
            {
                req.RequiredMark = int.Parse(mark);
            }

            int entID = Convert.ToInt32((item.FindControl("EntIDLabel") as Label).Text);
            req.EntranceRequirementID = entID;

            if (arg == "Save")
            {
                Ent_Req_Save(programID, req);
                MessageUserControl.ShowInfoPass("Entrance Requirement Successfully Saved!");
            }
            else if (arg == "Remove")
            {
                Ent_Req_Remove(programID, req);
                MessageUserControl.ShowInfoPass("Entrance Requirement Successfully Removed!");
            }
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }
    }

    protected void Ent_Req_Save(int programID, EntranceRequirement req)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            sysmgr.EntranceRequirement_Update(req);

            Populate_EntranceReqs(programID);
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void Ent_Req_Remove(int programID, EntranceRequirement req)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            sysmgr.EntranceReq_Delete(req);

            Populate_EntranceReqs(programID);
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void Add_Ent_Req(object sender, EventArgs e)
    {
        try
        {
            EntranceRequirement newReq = new EntranceRequirement();

            //newReq.EntranceRequirementID = int.Parse(DL_CredentialType.SelectedValue);
            int programID = int.Parse(ProgramIDLabel.Text);

            if (DL_New_EntReq.SelectedValue == "-3")
            {
                MessageUserControl.ShowInfo("A course must selected to add a high school entrance requirement.");
            }
            else
            {
                int mark;
                newReq.ProgramID = programID;
                newReq.HighSchoolCourseID = Convert.ToInt32(DL_New_EntReq.SelectedValue);
                newReq.SubjectRequirementID = Convert.ToInt32(DL_New_Subject.SelectedValue);

                if (NewMark.Text.Trim() == "")
                {
                    MessageUserControl.ShowInfo("A Mark must be entered.");
                }
                else
                {
                    if (int.TryParse(NewMark.Text.Trim(), out mark))
                    {
                        if (mark >= 50 && mark <= 100)
                        {
                            //newReq.RequiredMark = int.Parse(NewMark.Text);
                            newReq.RequiredMark = mark;
                            AdminController sysmgr = new AdminController();

                            bool added = sysmgr.AddEntranceRequirement(newReq);
                            if (added)
                            {
                                MessageUserControl.ShowInfoPass("Entrance Requirement Successfully Added!");
                                Populate_EntranceReqs(programID);
                            }
                            else
                            {
                                MessageUserControl.ShowInfo("The Entrance Requirement Already Existing.");
                            }
                        }
                        else
                        {
                            MessageUserControl.ShowInfo("Mark must be between 50-100.");
                        }
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("Mark must be a number.");
                    }
                }

            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
    }

    #endregion  
    
    #region post-secondary
    protected void GV_DegReq_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            decimal gpa;

            AdminController sysmgr = new AdminController();
            DegreeEntranceRequirement deg = new DegreeEntranceRequirement();

            deg.DegreeEntranceReqID = Convert.ToInt32(GV_DegreeEntranceReq.DataKeys[e.NewEditIndex].Value);
            deg.ProgramID = Convert.ToInt32(ProgramIDLabel.Text);

            string credentialID = (GV_DegreeEntranceReq.Rows[e.NewEditIndex].FindControl("CredentialName") as DropDownList).SelectedValue;
            deg.CredentialTypeID = Convert.ToInt32(credentialID);

            string categoryID = (GV_DegreeEntranceReq.Rows[e.NewEditIndex].FindControl("Category") as DropDownList).SelectedValue;
            deg.CategoryID = Convert.ToInt32(categoryID);

            string mark = (GV_DegreeEntranceReq.Rows[e.NewEditIndex].FindControl("GPA") as TextBox).Text;
            if (mark.Trim() == "")
            {
                PSMessageUserControl.ShowInfo("A GPA must be entered to add a post-secondary entrance requirement.");
            }
            else if (Decimal.TryParse(mark.Trim(), out gpa))
            {
                if (gpa > 4)
                {
                    PSMessageUserControl.ShowInfo("GPA cannot be greater than 4.0 to add a post-secondary entrance requirement.");
                }
                else if (gpa < 0)
                {
                    PSMessageUserControl.ShowInfo("GPA cannot be less than 0 to add a post-secondary entrance requirement.");
                }
                else
                {
                    deg.GPA = gpa;
                }
            }
            else
            {
                PSMessageUserControl.ShowInfo("GPA must be a decimal value to add a post-secondary entrance requirement.");
            }
            sysmgr.Deg_EntranceRequirement_Update(deg);
            PSMessageUserControl.ShowInfoPass("Entrance Requirement Successfully Updated!");
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

    protected void GV_DegReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            int degReqID = Convert.ToInt32(GV_DegreeEntranceReq.DataKeys[e.RowIndex].Value);
            int programID = Int32.Parse(ProgramIDLabel.Text);
            sysmgr.DER_Delete(degReqID);
            Populate_DER(programID);
            MessageUserControl.ShowInfoPass("Entrance Requirement Successfully Removed!");
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

     protected void Populate_DER(int programID)
    {
        AdminController sysmgr = new AdminController();
        GV_DegreeEntranceReq.DataSource = sysmgr.Get_DERByProgram(programID);
        GV_DegreeEntranceReq.DataBind();
    }
    
    protected void Add_DER_Click(object sender, EventArgs e)
    {
        try
        {
            AdminController sysmgr = new AdminController();

            DegreeEntranceRequirement req = new DegreeEntranceRequirement();
            int programID = Int32.Parse(ProgramIDLabel.Text);
            decimal gpa;

            if (TB_GPA.Text.Trim() == "")
            {
                PSMessageUserControl.ShowInfo("A GPA must be entered to add a post-secondary entrance requirement.");
            }
            else if (Decimal.TryParse(TB_GPA.Text.Trim(), out gpa))
            {
                if (Decimal.Parse(TB_GPA.Text.Trim()) > 4)
                {
                    PSMessageUserControl.ShowInfo("GPA cannot be greater than 4.0 to add a post-secondary entrance requirement.");
                }
                else if (Decimal.Parse(TB_GPA.Text.Trim()) < 0)
                {
                    PSMessageUserControl.ShowInfo("GPA cannot be less than 0 to add a post-secondary entrance requirement.");
                }
                else
                {
                    req.ProgramID = programID;
                    req.CredentialTypeID = int.Parse(DL_Credential.SelectedValue);
                    req.CategoryID = int.Parse(DL_Category.SelectedValue);
                    req.GPA = decimal.Parse(TB_GPA.Text.Trim());

                    bool exist = sysmgr.AddDER(req);
                    if (!exist)
                    {
                        PSMessageUserControl.ShowInfoPass("Entrance Requirement Successfully Added!");
                        Populate_DER(programID);
                    }
                    else
                    {
                        PSMessageUserControl.ShowInfo("The Entrance Requirement Already Existing.");
                    }
                }
            }
            else
            {
                PSMessageUserControl.ShowInfo("GPA must be a decimal value to add a post-secondary entrance requirement.");
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

    #endregion

    /*-- ----------------------------- COURSES ---------------------------------------*/
    #region courses

    protected void Populate_Courses(int programID)
    {
        try
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
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void ProgramCourses_Search(object sender, EventArgs e)
    {
        try
        {
            StudentController sysmgr = new StudentController();
            List<NAITCourse> naitcourses = sysmgr.SearchNaitCourses(TB_ProgramCoursesSearch.Text, 0, true);
            //maybe ask for if it is active????
            LV_ProgramCoursesSearch.DataSource = naitcourses;
            LV_ProgramCoursesSearch.DataBind();
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

    protected void Add_Program_Course(object sender, ListViewCommandEventArgs args)
    {
        try
        {
            ListViewItem item = args.Item;

            int programID = Int32.Parse(ProgramIDLabel.Text);
            int courseID = Convert.ToInt32((item.FindControl("CourseIDLabel") as Label).Text);
            int? semester = Convert.ToInt32((item.FindControl("DL_Semester") as DropDownList).SelectedValue);
            //if (semester == -1)
            //{
            //    semester = null;
            //}

            MessageUserControl.TryRun(() =>
            {
                ProgramCourse progCourse = new ProgramCourse()
                {
                    CourseID = courseID,
                    ProgramID = programID,
                    Semester = semester
                };

                AdminController sysmgr = new AdminController();
                sysmgr.AddProgramCourse(progCourse);

                Populate_Courses(programID);
                MessageUserControl.ShowInfoPass("Added successfully!");

            }
                );
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }   
    }


    protected void Remove_Program_Course(object sender, ListViewCommandEventArgs args)
    {
        try
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
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void Save_Courses(object sender, EventArgs e)
    {


        CourseEquivalencies_Show(sender, e);
    }

    #endregion
    /*-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------*/
    #region equivalencies

    protected void Populate_Equivalencies(int programID)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            var equivalencies = sysmgr.GetEquivalencies(programID);
            GV_Equivalencies.DataSource = equivalencies;
            GV_Equivalencies.DataBind();

            EmptyCurrentDropdown.Items.Clear();
            EmptyCurrentDropdown.DataBind();
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }

    protected void EmptyEquivalentProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            int progID = Convert.ToInt32(EmptyEquivalentProgram.SelectedValue);
            EquivalentCourseID.Items.Clear();
            EquivalentCourseID.DataSource = sysmgr.GetCoursesByProgram(progID);
            EquivalentCourseID.DataBind();  
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }            
    }

    protected void Enter_Click(object sender, EventArgs e)
    {
        try
        {
            AdminController sysmgr = new AdminController();
            int programID = Int32.Parse(ProgramIDLabel.Text);
            int courseID = int.Parse(EmptyCurrentDropdown.SelectedValue);
            int destinationCourseID = int.Parse(EquivalentCourseID.SelectedValue);

            if (courseID == -1 || destinationCourseID == -1)
            {
                MessageUserControl.ShowInfo("Current program course and equivalent program course must be selected to add a course equivalency.");
            }
            else
            {
                bool exist = sysmgr.AddEquivalency(programID, courseID, destinationCourseID);
                if (exist)
                {
                    MessageUserControl.ShowInfo("Current equivalency is already exist.");
                }
                else
                {
                    GV_Equivalencies.DataSource = sysmgr.GetEquivalencies(programID);
                    GV_Equivalencies.DataBind();
                    MessageUserControl.ShowInfoPass("Equivalency Successfully Added");
                }
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
    }

    protected void EquivalenciesGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int programID = Int32.Parse(ProgramIDLabel.Text);
            int equivalencyid = Convert.ToInt32(GV_Equivalencies.DataKeys[e.RowIndex].Value);
            AdminController sysmgr = new AdminController();
            sysmgr.Equivalency_Delete(equivalencyid);
            GV_Equivalencies.DataSource = sysmgr.GetEquivalencies(programID);
            GV_Equivalencies.DataBind();
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

    protected void Save_CourseEquivalencies(object sender, EventArgs e)
    {
        ProgramPreferences_Show(sender, e);
    }
    #endregion
    /*-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------*/
    #region preferences

    protected void Populate_Preferences(int programID)
    {
        try
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
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }        
    }
    protected void Save_Questions(object sender, EventArgs e)
    {
        try
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
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }
    #endregion

    /* -----------------------------------ADD NEW PROGRAM --------------------*/
    #region AddProgram
    protected void Add_Program_Button_Click(object sender, EventArgs e)
    {
        ProgramEditDiv.Visible = true;
        Buttons.Visible = true;
        ProgramList.Visible = false;
        //Program_Save.Visible = false;
        //Program_Add.Visible = true;
        //BasicProgramInfo.Visible = true;
        //Categories.Visible = false;
        //EntranceRequirements.Visible = false;
        //ProgramCourses.Visible = false;
        //CourseEquivalencies.Visible = false;
        //ProgramPreferences.Visible = false;
        //Add_Program_Button.Visible = false;
        //Tab_Labels.Visible = false;
        ProgramInfo_Show(sender, e);

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

       // CB_Categories.DataBind();

    }

    protected void Add_Program(object sender, EventArgs e)
    {
        try
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
            else if (double.Parse(credits) < 0 || double.Parse(credits) > 100)
                MessageUserControl.ShowInfo("Credits must be between 0 - 100");
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

            string errors = "";

            if (string.IsNullOrEmpty(TB_ProgramName.Text))
            {
                errors += "The Program Name is required.\n";
            }
            if (TB_Length.SelectedValue == "0")
            {
                errors += "The program length is required.\n";
            }


            if (errors == "")
            {
                MessageUserControl.TryRun(() => sysmr.AddProgram(NewProgram), "Add Success.", "You added new program");
                Categories_Show(sender, e);
                ProgramNameLabel.Text = TB_ProgramName.Text;



                int programid = sysmr.GetProgramIDByName(TB_ProgramName.Text);

                Populate_Program_Info(programid);
            }
            else
            {
                MessageUserControl.ShowInfo(errors);
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }
    #endregion
}