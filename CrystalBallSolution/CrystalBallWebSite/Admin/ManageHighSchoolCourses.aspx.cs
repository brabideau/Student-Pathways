using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_ManageHighSchoolCourses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            //NO
            Response.Redirect("~/Account/Login.aspx");
        }
        if(!IsPostBack)
        {
            BindList();
        }
    }
    //Handle any expections that occur
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }
    //Render data
    private void BindList()
    {
      
        AdminController sysmr = new AdminController();
        var highSchoolCourseData = sysmr.HighSchoolCourse_List();

        HighSchoolCoursesList.DataSource = highSchoolCourseData;
        HighSchoolCoursesList.DataBind();
    }
    protected void HighSchoolCoursesList_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        HighSchoolCoursesList.EditIndex = e.NewEditIndex;
        BindList();
    }
    protected void HighSchoolCoursesList_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        HighSchoolCoursesList.EditIndex = -1;
        BindList();
    }
    protected void HighSchoolCoursesList_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        AdminController sysmr = new AdminController();
        try
        {
            DropDownList courseGroup = (DropDownList)HighSchoolCoursesList.InsertItem.FindControl("DDL_CourseGroup_Insert");
            string courseGroupId = courseGroup.SelectedItem.Value;
            TextBox courseNameText = (TextBox)HighSchoolCoursesList.InsertItem.FindControl("HighSchoolCourseNameTextBox");
            DropDownList courseLevel = (DropDownList)HighSchoolCoursesList.InsertItem.FindControl("DDL_CourseLevel_Insert");
            string courseLevelId = courseLevel.SelectedItem.Value;

            var highschoolCourse = new HighSchoolCours();

            highschoolCourse.HighSchoolCourseName = courseNameText.Text;

            List<HighSchoolCours> NewHighSchoolCourse = new List<HighSchoolCours>();


            if (string.IsNullOrEmpty(courseNameText.Text))
            {
                MessageUserControl.ShowInfo("High school name is required.");
            }
            if (courseLevel.SelectedValue == "0")
            {
                MessageUserControl.ShowInfo("Please select a course level.");
            }
            else
            {
                highschoolCourse.CourseLevel = int.Parse(courseLevelId);
            }

            if (courseGroup.SelectedValue == "0")
            {
                MessageUserControl.ShowInfo("Please select a course group.");
            }
            else
            {
                highschoolCourse.CourseGroupID = int.Parse(courseGroupId);
            }

            if (string.IsNullOrEmpty(courseNameText.Text) == false && courseLevelId != "0" && courseGroupId != "0")
            {
                NewHighSchoolCourse.Add(highschoolCourse);
                MessageUserControl.TryRun(() => sysmr.AddHighSchoolCourse(NewHighSchoolCourse), "Add Success", "You added new course: " + courseNameText.Text);
                BindList();
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
    }

    protected void HighSchoolCoursesList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();
        try
        {
            Label courseId = (Label)HighSchoolCoursesList.EditItem.FindControl("HighSchoolCourseIDLabel");
            DropDownList courseGroup = (DropDownList)HighSchoolCoursesList.EditItem.FindControl("DDL_CourseGroup_Edit");
            string courseGroupId = courseGroup.SelectedItem.Value;
            TextBox courseNameText = (TextBox)HighSchoolCoursesList.EditItem.FindControl("HighSchoolCourseNameTextBox");
            DropDownList courseLevel = (DropDownList)HighSchoolCoursesList.EditItem.FindControl("DDL_CourseLevel_Edit");
            string courseLevelId = courseLevel.SelectedItem.Value;

            var highschoolCourse = new HighSchoolCours();
            highschoolCourse.HighSchoolCourseID = int.Parse(courseId.Text);

            highschoolCourse.HighSchoolCourseName = courseNameText.Text;

            if (string.IsNullOrEmpty(courseNameText.Text))
            {
                MessageUserControl.ShowInfo("High school name is required.");
            }
            if (courseLevel.SelectedValue == "0")
            {
                MessageUserControl.ShowInfo("Please select a course level.");
            }
            else
            {
                highschoolCourse.CourseLevel = int.Parse(courseLevelId);
            }

            if (courseGroup.SelectedValue == "0")
            {
                MessageUserControl.ShowInfo("Please select a course Group.");
            }
            else
            {
                highschoolCourse.CourseGroupID = int.Parse(courseGroupId);
            }

            if (string.IsNullOrEmpty(courseNameText.Text) == false && courseLevelId != "0" && courseGroupId != "0")
            {
                MessageUserControl.TryRun(() => sysmr.HighSchoolCourse_Update(highschoolCourse), "Update Success.", "You updated the course: " + courseNameText.Text);
                HighSchoolCoursesList.EditIndex = -1;
                BindList();
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }      
    }
    protected void HighSchoolCoursesList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (HighSchoolCoursesList.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        this.BindList();
    }
}