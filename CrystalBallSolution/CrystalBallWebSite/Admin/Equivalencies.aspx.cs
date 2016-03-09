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

    //VALIDATION
    protected void CheckforException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Populate_Program(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        AdminController sysmgr = new AdminController();
        int category = Convert.ToInt32(CategoryDropdownList.SelectedValue);
        ProgramDropdownList.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramDropdownList.DataBind();
        int program = Convert.ToInt32(ProgramDropdownList.SelectedValue);
        EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(program);
        EquivalenciesGrid.DataBind();
    }

    protected void Populate_EquivalenciesGrid(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        AdminController sysmgr = new AdminController();
        int program = Convert.ToInt32(ProgramDropdownList.SelectedValue);
        EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(program);
        EquivalenciesGrid.DataBind();

    }

    protected void AddNew_Click(object sender, EventArgs e)
    {        
        equivalencyInformation.Visible = false;
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
            MessageUserControl.ShowInfo("Program Course Code and Equivalent Course Code are Required.");
        }       
    }

    protected void Enter_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            AdminController sysmgr = new AdminController();
            int programID = int.Parse(ProgramDropdownList.SelectedValue);
            int courseID = int.Parse(CurrentCourseID.Text);
            int destinationCourseID = int.Parse(EquivalentCourseID.Text);
            sysmgr.AddEquivalency(programID, courseID, destinationCourseID);
            EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(programID);
            EquivalenciesGrid.DataBind();

            equivalencyInformation.Visible = true;
            addNewEquivalency.Visible = false;

            //reset add equivalency screen
            EmptyCurrentDropdown.Items.Clear();
            CurrentCourseName.Text = null;
            CurrentCourseID.Text = null;

            EmptyEquivalentTextBox.Text = null;
            EquivalentCourseName.Text = null;
            EquivalentCourseID.Text = null;

            Enter.Enabled = false;

        }, "", "Equivalency Successfully Added");        
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        equivalencyInformation.Visible = true;
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
        int programID = int.Parse(ProgramDropdownList.SelectedValue);
        int equivalencyid = Convert.ToInt32(EquivalenciesGrid.DataKeys[e.RowIndex].Value);
        AdminController sysmgr = new AdminController();
        sysmgr.Equivalency_Delete(equivalencyid);
        EquivalenciesGrid.DataSource = sysmgr.GetEquivalencies(programID);
        EquivalenciesGrid.DataBind();
    }
}