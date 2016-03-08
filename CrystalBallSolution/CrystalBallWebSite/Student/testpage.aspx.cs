using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


#region Additional namespace

using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.POCOs;
using CrystalBallSystem.BLL;
#endregion




public partial class Student_testpage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable CoursesSelected = (DataTable)Session["CoursesSelected"];
            GridView1.DataSource = CoursesSelected;
            GridView1.DataBind();
            ViewState["CoursesSelected"] = CoursesSelected;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.Redirect("../Student/SelectNaitCourses.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        List<int> listCID = new List<int>();
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            int cid =int.Parse( (row1[0]).ToString());
            listCID.Add(cid);
        }
        SelectNaitCourseController NCC = new SelectNaitCourseController();
        var pcMatch = NCC.PCMatch(listCID);

        pcMatchGv.DataSource = pcMatch;
        pcMatchGv.DataBind();
        
        
    }
}