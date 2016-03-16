using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
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
    protected void EntranceReq_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = true;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
    }

    protected void Populate_EntranceReqs(int programID) {
        AdminController sysmgr = new AdminController();
        //populate High School Entrance Requirements
        List<SubjectRequirement> subjectList = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataSource = subjectList;
        LV_SubjectReq.DataBind();

        List<EntranceRequirement> ereqList = new List<EntranceRequirement> {};
        int subjectID;

        //and the classes for each subject

        foreach (ListViewItem x in LV_SubjectReq.Items)
        {
            subjectID = Convert.ToInt32((x.FindControl("SubjectIDLabel") as Label).Text);
            var courses = x.FindControl("GV_EntranceReqs") as GridView;
            ereqList = sysmgr.Get_EntReq_ByProgram_Subject(programID, subjectID);
            courses.DataSource = ereqList;
            courses.DataBind();
            
        }




        //populate Degree Entrance Requirements
        
        List<DegreeEntranceRequirement> degEntList = sysmgr.Get_DegEntReq_ByProgram(programID);

        LV_DegreeEntranceReq.DataSource = degEntList;
        LV_DegreeEntranceReq.DataBind();

        
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

    protected void AddNew_Click(object sender, EventArgs e)
    {
        addNewEquivalency.Visible = true;
        EmptyCurrentDropdown.DataBind();
    }
    protected void CheckIDs_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        StudentController sys = new StudentController();
        string courseCode = EmptyCurrentDropdown.SelectedValue;
        string courseCode2 = EmptyEquivalentTextBox.Text;

        if (EmptyCurrentDropdown.SelectedValue != "-1"
            && !string.IsNullOrWhiteSpace(EmptyEquivalentTextBox.Text))
        {
            NAITCourse courseInfo = sysmgr.GetCourseName(courseCode);
            CurrentCourseName.Text = courseInfo.CourseName;
            CurrentCourseID.Text = courseInfo.CourseID.ToString();

            courseInfo = sysmgr.GetCourseName(courseCode2);
            EquivalentCourseName.Text = courseInfo.CourseName;
            EquivalentCourseID.Text = courseInfo.CourseID.ToString();
            Enter.Enabled = true;
        }
        else
        {
            //MessageUserControl.ShowInfo("Program Course Code and Equivalent Course Code are Required.");
        }
    }

    protected void Enter_Click(object sender, EventArgs e)
    {
        //MessageUserControl.TryRun(() =>
        //{
            AdminController sysmgr = new AdminController();
            int programID = Int32.Parse(ProgramIDLabel.Text);
            int courseID = int.Parse(CurrentCourseID.Text);
            int destinationCourseID = int.Parse(EquivalentCourseID.Text);
            sysmgr.AddEquivalency(programID, courseID, destinationCourseID);
            GV_Equivalencies.DataSource = sysmgr.GetEquivalencies(programID);
            GV_Equivalencies.DataBind();

            //equivalencyInformation.Visible = true;
            addNewEquivalency.Visible = false;

            //reset add equivalency screen
            EmptyCurrentDropdown.Items.Clear();
            CurrentCourseName.Text = null;
            CurrentCourseID.Text = null;

            EmptyEquivalentTextBox.Text = null;
            EquivalentCourseName.Text = null;
            EquivalentCourseID.Text = null;

            Enter.Enabled = false;

        //}, "", "Equivalency Successfully Added");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        addNewEquivalency.Visible = false;

        //reset add equivalency screen
        EmptyCurrentDropdown.Items.Clear();
        CurrentCourseName.Text = null;
        CurrentCourseID.Text = null;

        EmptyEquivalentTextBox.Text = null;
        EquivalentCourseName.Text = null;
        EquivalentCourseID.Text = null;

        Enter.Enabled = false;
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
                var DL_List = GV_Questions.Rows[i].FindControl("DL_Preference") as DropDownList;
                DL_List.SelectedValue = "noPref";

                var idbox = GV_Questions.Rows[i].FindControl("QuestionID") as Label;
                q_ID = Convert.ToInt32(idbox.Text);
                
                if (questionAns.Contains(q_ID))
                {
                question = (from x in progQuestions
                            where x.QuestionID == q_ID
                            select x).FirstOrDefault();

                    if (question != null)
                    {
                        if (question.Answer == true)
                        {
                            DL_List.SelectedValue = "Yes";
                        }
                        else
                        {
                            DL_List.SelectedValue = "No";
                        }
                    }
                }

            }
        }




        
    }
    protected void Save_Questions(object sender, EventArgs e)
    {

    }
    #endregion
}