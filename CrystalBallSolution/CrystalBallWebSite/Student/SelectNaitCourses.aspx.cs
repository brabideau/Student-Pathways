using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Addtional namespace
using CrystalBallSystem.BLL;
#endregion

public partial class User_SelectNaitCourses : System.Web.UI.Page
{
    DataTable CoursesSelected;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DataColumn CourseID;
            DataColumn CourseCode;
            DataColumn CourseName;
            DataColumn CourseCredits;
            CoursesSelected = new DataTable();
            if (Session["CoursesSelected"] == null)
            {
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

            CourseName = new DataColumn();
            CourseName.DataType = System.Type.GetType("System.String");
            CourseName.ColumnName = "CourseName";
            CourseName.Caption = "CourseName";
            CoursesSelected.Columns.Add(CourseName);

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
            }
            else
            {
                CoursesSelected = (DataTable)Session["CoursesSelected"];
                ViewState["CoursesSelected"] = CoursesSelected;
            }
            //Session["table"] = CoursesSelected;
            int count = 0;
            foreach (DataRow row1 in CoursesSelected.Rows)
            {
                count++;
            }
            TotalCourseLabel.Text = "Total courses : " + count;
            rptCourse.DataSource = CoursesSelected;
            rptCourse.DataBind();
            CourseGridView.Visible = false;
        }
    }

    protected void SelectCourses(object sender, GridViewSelectEventArgs e)
    {
        
        GridViewRow row = CourseGridView.Rows[e.NewSelectedIndex];
        string CCode = (row.FindControl("CourseCode") as Label).Text;
        string CName = (row.FindControl("CourseName") as Label).Text;
        int  id = int.Parse((row.FindControl("CourseID") as Label).Text);
        double CCredits = double.Parse((row.FindControl("CourseCredits") as Label).Text);
        //CourseRepeater.CourseCodeLabel.Text = id;
        //MultiSelect(CCode, id, CCredits);
        DataRow dtrow;
        DataTable CoursesSelected = (DataTable)ViewState["CoursesSelected"];
        dtrow = CoursesSelected.NewRow();
        dtrow["CourseID"] = id;
        dtrow["CourseCode"] = CCode;
        dtrow["CourseName"] = CName;
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
        int count = 0;
        foreach (DataRow row1 in CoursesSelected.Rows)
        {
            count++;
        }        
        ViewState["CoursesSelected"] = CoursesSelected;

        rptCourse.DataSource = CoursesSelected;
        rptCourse.DataBind();

        TotalCourseLabel.Text = "Total courses : " + count;
        for (int i = 0; i < CourseGridView.Rows.Count; i++)
        {
            CourseGridView.Rows[i].Font.Bold = false;
            for (int j = 0; j < CoursesSelected.Rows.Count; j++)
            {
                if (CourseGridView.DataKeys[i]["CourseID"].ToString() == CoursesSelected.Rows[j]["CourseID"].ToString())
                {
                    CourseGridView.Rows[i].BackColor = System.Drawing.Color.FromName("#D1DDF1");
                    CourseGridView.Rows[i].Font.Bold = true;
                    CourseGridView.Rows[i].ForeColor = System.Drawing.Color.FromName("#333333");
                }
            }
        }  

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
    protected void Search_Click(object sender, EventArgs e)
    {
        CourseGridView.Visible = true;
        CourseGridView.DataBind();

    }
    protected void reset_Click(object sender, EventArgs e)
    {
        Session["CoursesSelected"] = null;
        ViewState["CoursesSelected"] = null;
        Response.Redirect("../Student/SelectNaitCourses.aspx");
    }
    protected void ProgramDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectNaitCourseController scc = new SelectNaitCourseController();
        string SearchInfo = SearchTextBox.Text;
        int programID = int.Parse(ProgramDropDownList.SelectedValue);


        scc.SearchNaitCourses(SearchInfo, programID);
        CourseGridView.Visible = true;
        CourseGridView.DataBind();
        //ProgramDropDownList.AutoPostBack;
        
    }
}
//problem feed back not using ispostback so that everytime it creat a new datatable