using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;

public partial class Admin_Equivalencies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Populate_Program(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        AdminController sysmgr = new AdminController();
        int category = Convert.ToInt32(CategoryDropdownList.SelectedValue);
        int program = -5;
        ProgramDropdownList.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramDropdownList.DataBind();
        EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(program, category);
        EquivalenciesGrid.DataBind();

    }

    protected void Populate_EquivalenciesGrid(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        AdminController sysmgr = new AdminController();
        int program = Convert.ToInt32(ProgramDropdownList.SelectedValue);
        int category = Convert.ToInt32(CategoryDropdownList.SelectedValue);
        EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(program, category);
        EquivalenciesGrid.DataBind();
    }

    protected void AddNew_Click(object sender, EventArgs e)
    {
        equivalencyInformation.Visible = false;
        addNewEquivalency.Visible = true;
    }
    protected void CheckIDs_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        string courseCode = EmptyCurrentTextBox.Text;
        NAITCourse courseInfo = sysmgr.GetCourseName(courseCode);
        //if 
        CurrentCourseName.Text = courseInfo.CourseName;
        CurrentCourseID.Text = courseInfo.CourseID.ToString();

        courseCode = EmptyEquivalentTextBox.Text;
        courseInfo = sysmgr.GetCourseName(courseCode);
        EquivalentCourseName.Text = courseInfo.CourseName;
        EquivalentCourseID.Text = courseInfo.CourseID.ToString();
    }


    protected void Enter_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        int programID = int.Parse(ProgramDropdownList.SelectedValue); 
        int courseID = int.Parse(CurrentCourseID.Text);
        int destinationCourseID = int.Parse(EquivalentCourseID.Text);
        sysmgr.AddEquivalency(programID, courseID, destinationCourseID);
    }
}