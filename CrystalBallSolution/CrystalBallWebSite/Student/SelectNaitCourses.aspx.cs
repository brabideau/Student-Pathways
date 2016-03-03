using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_SelectNaitCourses : System.Web.UI.Page
{
    //DataTable CoursesSelected = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DataColumn CourseID;
            DataColumn CourseCode;
            DataColumn CourseCredits;
            DataTable CoursesSelected = new DataTable();
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
        
        // delete duplicate value
        
        CoursesSelected.Rows.Add(dtrow);
       
        double credit = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            credit =credit + double.Parse(row1[2].ToString());

        }
        Label1.Text = credit + "";
        ViewState["CoursesSelected"] = CoursesSelected;

        rptCourse.DataSource = CoursesSelected;
        rptCourse.DataBind();

    }
    public void MultiSelect(string courseCode ,int courseId, double courseCredits)
    {

        DataRow row;
        DataTable CoursesSelected = (DataTable)ViewState["table"];
        row = CoursesSelected.NewRow();
        row["CourseID"] = courseId;
        row["CourseCode"] = courseCode;
        row["CourseCredits"] = courseCredits;
        // delete duplicate value
        if (!CoursesSelected.Rows.Contains(courseId))
        {
            CoursesSelected.Rows.Add(row);
        }
        else
        {
            CoursesSelected.Rows.Find(courseId).Delete();
        }

        
        foreach(DataRow row1 in CoursesSelected.Rows)
        {
            CourseCodeLabel.Text += " " + row1[1].ToString();
        }
        //double credit = 0;
        int count = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            //credit =credit + double.Parse(row1[2].ToString());
            count++;
        }
        Label1.Text = count + "";
        ViewState["table"] = CoursesSelected;

        rptCourse.DataSource = CoursesSelected;
        rptCourse.DataBind();
        //CourseCodeLabel.Text += " " + CoursesSelected.Rows.ToString();

        //Session["CoursesSelected"] = CoursesSelected;

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
}
//problem feed back not using ispostback so that everytime it creat a new datatable