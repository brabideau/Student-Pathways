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
            if (!Request.IsAuthenticated)
            {
                //NO
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    private void BindList()
    {
        try
        {
            string pid = ProgramList.SelectedDataKey.Value.ToString();
            int proId = Convert.ToInt32(pid);
            AdminController sysmr = new AdminController();
            var courseData = sysmr.GetCoursesByProgram(proId);

            NaitCoursesListViewByProgram.DataSource = courseData;
            NaitCoursesListViewByProgram.DataBind();
        }
        catch (Exception e)
        {
            MessageUserControl.ShowInfo(e.Message);
        }
        
    }

    protected void ProgramList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        try
        {
            ProgramList.SelectedIndex = e.NewSelectedIndex;
            string pid = ProgramList.SelectedDataKey.Value.ToString();
            int proId = Convert.ToInt32(pid);
            AdminController sysmr = new AdminController();
            var courseData = sysmr.GetCoursesByProgram(proId);
            programName.InnerText = (sysmr.findProgramById(proId)).ToString();
            NaitCoursesListViewByProgram.Visible = true;
            NaitCoursesListViewByProgram.DataSource = courseData;
            NaitCoursesListViewByProgram.DataBind();

            ProgramList.DataBind();
            NaitCoursesListViewByProgram.InsertItemPosition = InsertItemPosition.None;
            BindList();
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }
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
        try
        {
            TextBox courseCodeBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCodeTextBox");
            TextBox courseNameBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseNameTextBox");
            TextBox courseCreditsBox = (TextBox)NaitCoursesListViewByProgram.InsertItem.FindControl("CourseCreditsTextBox");
            CheckBox activity = (CheckBox)NaitCoursesListViewByProgram.InsertItem.FindControl("ActiveCheckBox");

            double credits;
            string courseCodeText = courseCodeBox.Text;
            string courseNameText = courseNameBox.Text;
            if (courseCreditsBox.Text.Trim() == "")
            {
                MessageUserControl.ShowInfo("Course Credits is required.");
            }
            else if (double.TryParse(courseCreditsBox.Text.Trim(), out credits))
            {
                if (credits <= 1 || credits > 100)
                    MessageUserControl.ShowInfo("Credits must be between 1 - 100");
                else
                {
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
            }
            else
            {
                MessageUserControl.ShowInfo("Course Credits must be a decimal value.");
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }

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
        try
        {
            Label courseIDBox = (Label)NaitCoursesListViewByProgram.EditItem.FindControl("CourseIDTextBox");
            TextBox courseCodeBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseCodeTextBox");
            TextBox courseNameBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseNameTextBox");
            TextBox courseCreditsBox = (TextBox)NaitCoursesListViewByProgram.EditItem.FindControl("CourseCreditsTextBox");
            CheckBox activity = (CheckBox)NaitCoursesListViewByProgram.EditItem.FindControl("ActiveCheckBox");

            var course = new NaitCours();
            double credits;

            //course.CourseID = Convert.ToInt16(NaitCoursesListViewByProgram.SelectedDataKey.Value.ToString());
            course.CourseID = int.Parse(courseIDBox.Text);
            course.CourseCode = courseCodeBox.Text;
            course.CourseName = courseNameBox.Text;
            if (courseCreditsBox.Text.Trim() == "")
            {
                MessageUserControl.ShowInfo("Course Credits is required.");
            }
            else if (double.TryParse(courseCreditsBox.Text.Trim(), out credits))
            {
                if (credits <= 1 || credits > 100)
                    MessageUserControl.ShowInfo("Credits must be between 1 - 100");
                else
                {
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
            }
            else
            {
                MessageUserControl.ShowInfo("Course Credits must be a decimal value.");
            }
        }
        catch (Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }        
    }
    protected void NaitCoursesListViewByProgram_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (NaitCoursesListViewByProgram.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        this.BindList();
    }

}