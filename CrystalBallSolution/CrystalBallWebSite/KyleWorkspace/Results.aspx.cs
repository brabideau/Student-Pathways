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
        //get datatable from previous page and use it to load results
        DataTable dt = (DataTable)Session["CourseArray"];

        //pass the datatable to the database and get results
        StudentController.FindProgramMatches(dt);
    }
}