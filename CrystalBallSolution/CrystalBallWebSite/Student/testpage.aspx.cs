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
    DataTable CoursesSelected;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CoursesSelected = (DataTable)Session["CoursesSelected"];
            //GridView1.DataSource = CoursesSelected;
            //GridView1.DataBind();
            ViewState["CoursesSelected"] = CoursesSelected;
            List<int> listCID = new List<int>();
            foreach (DataRow row1 in CoursesSelected.Rows)
            {
                int cid = int.Parse((row1[0]).ToString());
                listCID.Add(cid);
            }
            SelectNaitCourseController NCC = new SelectNaitCourseController();
            var pcMatch = NCC.PCMatch(listCID);


            rptProgram.DataSource = pcMatch;
            rptProgram.DataBind();
            //ViewState["CoursesSelected"] = CoursesSelected;
        }
        //DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        Session["CoursesSelected"] = CoursesSelected;


        Response.Redirect("../Student/SelectNaitCourses.aspx");

    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
        


    //    //int credit = 0;
    //    //Repeater outrepeater = rptProgram;
    //    //Repeater intrepeater = (Repeater) rptProgram.FindControl("rptCourse");

    //    //foreach (RepeaterItem i in intrepeater.Items)
    //    //{
    //    //    var IDLabel = i.FindControl("Label1") as Label;
    //    //    credit += int.Parse(IDLabel.ToString());
            
    //    //}
    //    //Label a = (Label)rptProgram.FindControl("Label2");
    //    //a.Text = credit + "";
    //    //{

    //    //    TextBox txtExample = (TextBox)i.FindControl("txtExample");
    //    //    if (txtExample != null)
    //    //    {
    //    //        litResults.Text += txtExample.Text + "<br />";
    //    //    }
    //    //}
        
        
    //}
}