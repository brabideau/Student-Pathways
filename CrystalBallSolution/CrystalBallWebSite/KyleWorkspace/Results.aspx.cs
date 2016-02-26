using CrystalBallSystem.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KyleWorkspace_Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get list from previous page and use it to load results
        List<int> myCourses = (List<int>)Session["CourseArray"];

        //pass the list to the database and get results
        var myMatches = StudentController.FindProgramMatches(myCourses);

        ResultsView.DataSource = myMatches;
        ResultsView.DataBind();
    }
}