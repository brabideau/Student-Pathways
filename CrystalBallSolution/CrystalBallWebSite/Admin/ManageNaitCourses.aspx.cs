using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_ManageNaitCourses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    private void BindList()
    {
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var courseData = sysmr.GetCoursesByProgram(proId);

        NaitCoursesListViewByProgram.DataSource = courseData;
        NaitCoursesListViewByProgram.DataBind();
    }

    protected void ProgramList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        //NaitCoursesListViewByProgram.DataSourceID = ODSNaitCourses.ID;
        //ListViewItem item = ProgramList.Items[ProgramList.SelectedIndex];
        //Label program = (Label)item.FindControl("ProgramIDLabel");
        ProgramList.SelectedIndex = e.NewSelectedIndex;
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var courseData = sysmr.GetCoursesByProgram(proId);

        NaitCoursesListViewByProgram.Visible = true;
        NaitCoursesListViewByProgram.DataSource = courseData;
        NaitCoursesListViewByProgram.DataBind();
        
        ProgramList.DataBind();
        BindList();

    }

    private void CloseInsert()
    {
        NaitCoursesListViewByProgram.InsertItemPosition = InsertItemPosition.None;
        ((LinkButton)NaitCoursesListViewByProgram.FindControl("NewButton")).Visible = true;
    }

    protected void NewButton_Click(object sender, EventArgs e)
    {
        NaitCoursesListViewByProgram.EditIndex = -1;
        NaitCoursesListViewByProgram.InsertItemPosition = InsertItemPosition.LastItem;
        ((LinkButton)sender).Visible = false;
        BindList();
    }

    protected void NewButton2_Click(object sender, EventArgs e)
    {
        NaitCoursesListViewByProgram.EditIndex = -1;
        NaitCoursesListViewByProgram.InsertItemPosition = InsertItemPosition.LastItem;
        ((LinkButton)sender).Visible = false;
        BindList();
        ((LinkButton)NaitCoursesListViewByProgram.FindControl("NewButton")).Visible = false;
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        NaitCoursesListViewByProgram.InsertItemPosition = InsertItemPosition.None;
        NaitCoursesListViewByProgram.DataSource = null;
        NaitCoursesListViewByProgram.Visible=false;
        if (CategoryDropdownList.SelectedIndex == 0)
            MessageUserControl.ShowInfo("Please select a category before clicking Search.");
        
    }


    protected void NaitCoursesListViewByProgram_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        //NaitCoursesListViewByProgram.DataSourceID = ODSNaitCourses.ID;
        //ListViewItem item = NaitCoursesListViewByProgram.InsertItem;

        TextBox courseCodeBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCodeTextBox");
        TextBox courseNameBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseNameTextBox");
        TextBox courseCreditsBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCreditsTextBox");
        CheckBox activity = (CheckBox)NaitCoursesListViewByProgram.InsertItem.FindControl("ActiveCheckBox");

        string courseCodeText = courseCodeBox.Text;
        string courseNameText = courseNameBox.Text;
        string courseCreditsText = courseCreditsBox.Text;
        bool activityTF = activity.Checked;

        List<NaitCours> NewCourse = new List<NaitCours>();

        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);

        AdminController sysmr = new AdminController();
        if (string.IsNullOrEmpty(courseCodeText))
        {
            MessageUserControl.ShowInfo("The Course Code is required.");
        }
        else if (string.IsNullOrEmpty(courseNameText))
        {
            MessageUserControl.ShowInfo("The Course Name is required.");
        }
        else
        {
            NewCourse.Add(
                new NaitCours()
                {
                    CourseCode = courseCodeText,
                    CourseName = courseNameText,
                    CourseCredits = double.Parse(courseCreditsText),
                    Active = activityTF
                });

            sysmr.AddNaitCourse(NewCourse, proId);
            CloseInsert();
            BindList();
        }
             
    }


    //protected void NaitCoursesListViewByProgram_ItemCommand(object sender, ListViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Insert")
    //    {
    //        TextBox courseCodeBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCodeTextBox");
    //        TextBox courseNameBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseNameTextBox");
    //        TextBox courseCreditsBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCreditsTextBox");
    //        CheckBox activity = (CheckBox)NaitCoursesListViewByProgram.InsertItem.FindControl("ActiveCheckBox");

    //        string courseCodeText = courseCodeBox.Text;
    //        string courseNameText = courseNameBox.Text;
    //        string courseCreditsText = courseCreditsBox.Text;
    //        bool activityTF = activity.Checked;

    //        List<NaitCours> NewCourse = new List<NaitCours>();
    //        NewCourse.Add(
    //            new NaitCours()
    //            {
    //                CourseCode = courseCodeText,
    //                CourseName = courseNameText,
    //                CourseCredits = double.Parse(courseCreditsText),
    //                Active = activityTF
    //            });

    //        string pid = ProgramList.SelectedDataKey.Value.ToString();
    //        int proId = Convert.ToInt32(pid);
    //        AdminController sysmr = new AdminController();
    //        sysmr.AddNaitCourse(NewCourse, proId);
    //        CloseInsert();
    //        BindList();
    //    }

    //}

    protected void NaitCoursesListViewByProgram_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        CloseInsert();
        NaitCoursesListViewByProgram.EditIndex = e.NewEditIndex;

        BindList();
    }

    protected void NaitCoursesListViewByProgram_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        if (e.CancelMode == ListViewCancelMode.CancelingInsert)
        {
            CloseInsert();
        }
        else
        {
            NaitCoursesListViewByProgram.EditIndex = -1;
        }

        BindList();

    }

    protected void NaitCoursesListViewByProgram_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        NaitCoursesListViewByProgram.SelectedIndex = e.NewSelectedIndex;

    }

    protected void NaitCoursesListViewByProgram_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();

        Label courseIDBox = (Label)NaitCoursesListViewByProgram.EditItem.FindControl("CourseIDTextBox");
        TextBox courseCodeBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseCodeTextBox");
        TextBox courseNameBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseNameTextBox");
        TextBox courseCreditsBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseCreditsTextBox");
        CheckBox activity = (CheckBox)NaitCoursesListViewByProgram.EditItem.FindControl("ActiveCheckBox");

        var course = new NaitCours();

        //course.CourseID = Convert.ToInt16(NaitCoursesListViewByProgram.SelectedDataKey.Value.ToString());
        course.CourseID = int.Parse(courseIDBox.Text);
        course.CourseCode = courseCodeBox.Text;
        course.CourseName = courseNameBox.Text;
        course.CourseCredits = double.Parse(courseCreditsBox.Text);
        course.Active = activity.Checked;

        if (string.IsNullOrEmpty(courseCodeBox.Text))
        {
            MessageUserControl.ShowInfo("The Course Code is required.");
        }
        else if (string.IsNullOrEmpty(courseNameBox.Text))
        {
            MessageUserControl.ShowInfo("The Course Name is required.");
        }
        else
        {
            sysmr.UpdateNaitCourse(course);
            NaitCoursesListViewByProgram.EditIndex = -1;
        }
        
        BindList();

    }



    protected void NaitCoursesListViewByProgram_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (NaitCoursesListViewByProgram.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        this.BindList();
    }

}