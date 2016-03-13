using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
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

    protected void Program_Search(object sender, EventArgs e)
    {
        string search = Search_Box.Text;
        AdminController sysmgr = new AdminController();
        var programData = sysmgr.Program_Search(search, null);

        Program_List.DataSource = programData;
        Program_List.DataBind();
        ProgramList.Visible = true;
    }

    protected void Populate_Program_Info(object sender, EventArgs e)
    {
        // Get the program
        LinkButton button = (LinkButton)(sender);
        
        int programID = Convert.ToInt32(button.CommandArgument);
        AdminController sysmgr = new AdminController();
        Program myProgram = sysmgr.Get_Program(programID);

        // Populate the program info
        TB_ProgramName.Text = myProgram.ProgramName;
        DL_CredentialType.SelectedValue = myProgram.CredentialTypeID.ToString();
        TB_Description.Text = myProgram.ProgramDescription;
        TB_Credits.Text = myProgram.TotalCredits.ToString();
        TB_Length.Text = myProgram.ProgramLength;
        TB_CompetitiveAdvantage.Text = myProgram.CompetitiveAdvantage.ToString();
        CB_Active.Checked = myProgram.Active;
        TB_Link.Text = myProgram.ProgramLink;

        // populate the other info
        Populate_Categories(programID);
        Populate_Courses(programID);
        Populate_Equivalencies(programID);
        Populate_Preferences(programID);

        buttons.Visible = true;
        ProgramInfo.Visible = true;
        ProgramList.Visible = false;
    }

    /* -----------------------------------PROGRAM INFO --------------------*/

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

    }

    /*-- ----------------------------- CATEGORIES ---------------------------------------*/
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
         // Why isn't this working??????

        foreach (ListItem x in CB_Categories.Items)
        {
            catID = Convert.ToInt32(x.Value);
            if (catList.Contains(catID))
            {
                x.Selected = true;
            }
        }
    }
    protected void Save_Categories(object sender, EventArgs e)
    {

    }



    /*-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------*/
    protected void EntranceReq_Show(object sender, EventArgs e)
    {
        ProgramInfo.Visible = false;
        Categories.Visible = false;
        EntranceRequirements.Visible = true;
        ProgramCourses.Visible = false;
        CourseEquivalencies.Visible = false;
        ProgramPreferences.Visible = false;
    }
    protected void Save_EntranceReq(object sender, EventArgs e)
    {

    }

    /*-- ----------------------------- COURSES ---------------------------------------*/
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

    }


    /*-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------*/
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

    protected void Save_CourseEquivalencies(object sender, EventArgs e)
    {

    }

    /*-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------*/
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
        var questions = sysmgr.GetQuestionsByProgram(programID);
        GV_Questions.DataSource = questions;
        GV_Questions.DataBind();
    }
    protected void Save_Questions(object sender, EventArgs e)
    {

    }
}