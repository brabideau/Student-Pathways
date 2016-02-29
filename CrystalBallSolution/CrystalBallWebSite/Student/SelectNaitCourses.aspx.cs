using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_SelectNaitCourses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SelectCourses(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = CourseGridView.Rows[e.NewSelectedIndex];
        string id = (row.FindControl("CourseCode") as Label).Text;
        //CourseRepeater.CourseCodeLabel.Text = id;
    }
}