using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_SelectNaitCourses : System.Web.UI.Page
{
    DataTable CoursesSelected;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DataColumn CourseID;
            DataColumn CourseCode;
            DataColumn CourseCredits;
            CoursesSelected = new DataTable();
            //if (Session["CoursesSelected"] == null)
            //{
            CourseID = new DataColumn();
            CourseID.DataType = System.Type.GetType("System.Int32");
            CourseID.ColumnName = "CourseID";
            CourseID.Caption = "CourseID";
            CoursesSelected.Columns.Add(CourseID);

            CourseCode = new DataColumn();
            CourseCode.DataType = System.Type.GetType("System.String");
            CourseCode.ColumnName = "CourseCode";
            CourseCode.Caption = "CourseCode";
            CoursesSelected.Columns.Add(CourseCode);

            CourseCredits = new DataColumn();
            CourseCredits.DataType = System.Type.GetType("System.Double");
            CourseCredits.ColumnName = "CourseCredits";
            CourseCredits.Caption = "CourseCredits";
            CoursesSelected.Columns.Add(CourseCredits);
            //DataColumn CrsCode = new DataColumn("CrsCode");
            //CrsCode.DataType = Type.GetType("System.String");
            //CoursesSelected.Columns.Add(CrsCode);
            DataColumn[] pCol = new DataColumn[1];
            pCol[0] = CourseID;
            CoursesSelected.PrimaryKey = pCol;
            ViewState["CoursesSelected"] = CoursesSelected;
            //}
            //else
            //{
            //    CoursesSelected = (DataTable)Session["CoursesSelected"];
            //    Session["CoursesSelected"] = null;
            //    //CourseCodeLabel.Text = CoursesSelected.Rows.ToString();
            //}
            //Session["table"] = CoursesSelected;
            rptCourse.DataSource = CoursesSelected;
            rptCourse.DataBind();
        }
    }

    protected void SelectCourses(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = CourseGridView.Rows[e.NewSelectedIndex];
        string CCode = (row.FindControl("CourseCode") as Label).Text;
        int  id = int.Parse((row.FindControl("CourseID") as Label).Text);
        double CCredits = double.Parse((row.FindControl("CourseCredits") as Label).Text);
        //CourseRepeater.CourseCodeLabel.Text = id;
        //MultiSelect(CCode, id, CCredits);
        DataRow dtrow;
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        dtrow = CoursesSelected.NewRow();
        dtrow["CourseID"] = id;
        dtrow["CourseCode"] = CCode;
        dtrow["CourseCredits"] = CCredits;
        
        //how to delete duplicate value
        DataRow findRow = CoursesSelected.Rows.Find(id);
        if (findRow==null)
        {
            CoursesSelected.Rows.Add(dtrow);
        }
        else
        {
            CoursesSelected.Rows.Find(id).Delete();
            CoursesSelected.Rows.Add(dtrow);
        }
        double credit = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            credit =credit + double.Parse(row1[2].ToString());

        }        
        ViewState["CoursesSelected"] = CoursesSelected;

        rptCourse.DataSource = CoursesSelected;
        rptCourse.DataBind();

        //for (int i = 0; i < CourseGridView.Rows.Count; i++)
        //{
        //    CourseGridView.Rows[i].Font.Bold = false;
        //    for (int j = 0; j < CoursesSelected.Rows.Count; j++)
        //    {
        //        if (GridView1.DataKeys[i]["CategoryID"].ToString() == tblSelected.Rows[j]["catID"].ToString())
        //        {
        //            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#D1DDF1");
        //            GridView1.Rows[i].Font.Bold = true;
        //            GridView1.Rows[i].ForeColor = System.Drawing.Color.FromName("#333333");
        //        }
        //    }
        //}  

    }
    protected void Next_Click(object sender, EventArgs e)
    {
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        Session["CoursesSelected"] = CoursesSelected;
        Response.Redirect("../Student/testpage.aspx");
    }


    protected void rptCourse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int courserId = Convert.ToInt32(e.CommandArgument);
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        if (e.CommandName == "Delete" && e.CommandArgument.ToString() != "")
        {
            CoursesSelected.Rows.Find(courserId).Delete();
            rptCourse.DataSource = CoursesSelected;
            rptCourse.DataBind();
        }

    }
}
//problem feed back not using ispostback so that everytime it creat a new datatable