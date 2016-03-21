using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional namespace

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.DTOs;
using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
#endregion



public partial class Admin_AddNewNaitCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AddLinkButton_Click(object sender, EventArgs e)
    {
        NaitCours nc = new NaitCours();
        string courseCode = CourseCodeTB.Text;
        nc.CourseCode = courseCode;

        string courseName = CourseNameTB.Text;
        nc.CourseName = courseName;

        double courseCredit = double.Parse(CourseCreditsTB.Text);
        nc.CourseCredits = courseCredit;

        if(ActiveCheckBox.Checked==true)
        {
            bool active = true;
            nc.Active = active;
        }
        else
        {
            bool active = false;
            nc.Active = active;
        }
        SelectNaitCourseController sncc = new SelectNaitCourseController();
        sncc.AddCourse(nc);
        CourseGridView.DataBind();
        
        

    }
    protected void ProgramDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProgramIDLabel.Text = ProgramDDL.SelectedValue;
        SelectNaitCourseController sncc = new SelectNaitCourseController();
        sncc.NaitCourse_List(int.Parse(ProgramDDL.SelectedValue));
        CourseGridView.DataBind();
    }
}