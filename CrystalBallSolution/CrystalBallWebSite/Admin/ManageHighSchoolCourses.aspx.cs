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

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

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

        DropDownList courseGroup = (DropDownList)HighSchoolCoursesList.InsertItem.FindControl("DDL_CourseGroup_Insert");
        string courseGroupId = courseGroup.SelectedValue;
        TextBox courseNameText = (TextBox)HighSchoolCoursesList.InsertItem.FindControl("HighSchoolCourseNameTextBox");
        DropDownList courseLevel = (DropDownList)HighSchoolCoursesList.InsertItem.FindControl("DDL_CourseLevel_Insert");
        string courseLevelId = courseLevel.SelectedValue;

        var highschoolCourse = new HighSchoolCours();

        highschoolCourse.HighSchoolCourseName = courseNameText.Text;
        highschoolCourse.CourseLevel = int.Parse(courseLevelId);
        highschoolCourse.CourseGroupID = int.Parse(courseGroupId);

        List<HighSchoolCours> NewHighSchoolCourse = new List<HighSchoolCours>();
        NewHighSchoolCourse.Add(highschoolCourse);

        sysmr.AddHighSchoolCourse(NewHighSchoolCourse);
        BindList();



    }


    protected void HighSchoolCoursesList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();
        Label courseId = (Label)HighSchoolCoursesList.EditItem.FindControl("HighSchoolCourseIDLabel");
        DropDownList courseGroup = (DropDownList)HighSchoolCoursesList.EditItem.FindControl("DDL_CourseGroup_Edit");
        string courseGroupId = courseGroup.SelectedItem.Value;
        TextBox courseNameText = (TextBox)HighSchoolCoursesList.EditItem.FindControl("HighSchoolCourseNameTextBox");
        DropDownList courseLevel = (DropDownList)HighSchoolCoursesList.EditItem.FindControl("DDL_CourseLevel_Edit");
        string courseLevelId = courseLevel.SelectedItem.Value;

        var highschoolCourse = new HighSchoolCours();
        highschoolCourse.HighSchoolCourseID = int.Parse(courseId.Text);
        
        highschoolCourse.HighSchoolCourseName = courseNameText.Text;

        highschoolCourse.CourseLevel = int.Parse(courseLevelId);
        highschoolCourse.CourseGroupID = int.Parse(courseGroupId);
        sysmr.HighSchoolCourse_Update(highschoolCourse);

        //if (string.IsNullOrEmpty(courseNameText.Text))
        //{
        //    MessageUserControl.ShowInfo("The High School Name is required.");
        //}
        //if (courseLevel.SelectedValue == "0")
        //{
        //    MessageUserControl.ShowInfo("Please select a course level.");
        //}
        //else
        //{
        //    highschoolCourse.CourseLevel = int.Parse(courseLevelId);
        //}

        //if (courseGroup.SelectedValue == "0")
        //{
        //    MessageUserControl.ShowInfo("Please select a course Group.");
        //}
        //else
        //{
        //    highschoolCourse.CourseGroupID = int.Parse(courseGroupId);
        //}

        //if(string.IsNullOrEmpty(courseNameText.Text) == false && courseLevelId != "0" && courseGroupId != "0")
        //{
        //    MessageUserControl.TryRun(() => sysmr.HighSchoolCourse_Update(highschoolCourse), "Update Success.", "You updated the course: " + courseNameText.Text);
        //    HighSchoolCoursesList.EditIndex = -1;
        //    BindList();
        //}
       

    }
    protected void HighSchoolCoursesList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (HighSchoolCoursesList.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        this.BindList();
    }
}