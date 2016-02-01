using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        if (!Page.IsPostBack)
        {
            SetInitialCourse();
        }
    }
    private void SetInitialCourse()
    {
        //Create DataTable
        DataTable dt = new DataTable();
        DataRow dr = null;

        //Add initail values to DataTable
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1Course", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2Course", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1Course"] = string.Empty;
        dr["Column2Course"] = string.Empty;

        dt.Rows.Add(dr);
        dr = dt.NewRow();

        //Store the DataTable in ViewState
        ViewState["CurrentTableCourse"] = dt;
        GV_Course.DataSource = dt;
        GV_Course.DataBind();

    }
    protected void Add_Btn_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int i;
            bool trigger = true;
            if (GV_Course.Rows.Count < 8)
            {                
                foreach (GridViewRow row in GV_Course.Rows)
                {
                    var course = row.FindControl("DL_Course") as DropDownList;
                    var poQtyLabel = row.FindControl("TB_EnterMarks") as TextBox;
                    if (string.IsNullOrEmpty((row.FindControl("TB_EnterMarks") as TextBox).Text))
                    {
                        MessageUserControl.ShowInfo("Must Enter a Quantity");
                        trigger = false;
                    }
                    else
                    {
                        if (Int32.TryParse((row.FindControl("TB_EnterMarks") as TextBox).Text, out i) == false)
                        {
                            MessageUserControl.ShowInfo("Quantity must be an integer");
                            trigger = false;
                        }
                        else
                        {
                            if (int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) <= 0 || int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) > 100)
                            {
                                MessageUserControl.ShowInfo("Quantity must be greater than 0");
                                trigger = false;
                            }
                            else
                            {
                                AddNewRowToCourse();
                            }
                        }
                    }                    
                }
                //if (trigger == true)
                //{
                //    AddNewRowToCourse();
                //}
            }
            else
            {
                
            }
        });
    }
    private void SetPreviousCourseData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableCourse"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableCourse"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList courseList = (DropDownList)GV_Course.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_Course.Rows[rowIndex].Cells[2].FindControl("TB_EnterMarks");

                    courseList.Text = dt.Rows[i]["Column1Course"].ToString();
                    marks.Text = dt.Rows[i]["Column2Course"].ToString();

                    rowIndex++;
                }
            }
        }
    }
    private void AddNewRowToCourse()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableCourse"] != null)
        {
            //create new datatable, cast datatable of viewstate
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableCourse"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the values
                    DropDownList courseList = (DropDownList)GV_Course.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_Course.Rows[rowIndex].Cells[2].FindControl("TB_EnterMarks");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1Course"] = courseList.Text;
                    dtCurrentTable.Rows[i - 1]["Column2Course"] = marks.Text;

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTableCourse"] = dtCurrentTable;

                GV_Course.DataSource = dtCurrentTable;
                GV_Course.DataBind();
            }
        }

        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousCourseData();
    }

    protected void GVCourse_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        
        if (ViewState["CurrentTableCourse"] != null)
        {
            SetRowData();
            DataTable dt = (DataTable)ViewState["CurrentTableCourse"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTableCourse"] = dt;
                GV_Course.DataSource = dt;
                GV_Course.DataBind();

                for (int i = 0; i < GV_Course.Rows.Count - 1; i++)
                {
                    GV_Course.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousCourseData();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableCourse"];
        DataRow drCurrentRow = null;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {
                DropDownList courseList = (DropDownList)GV_Course.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                TextBox marks = (TextBox)GV_Course.Rows[rowIndex].Cells[2].FindControl("TB_EnterMarks");

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = i + 1;
                dtCurrentTable.Rows[i - 1]["Column1Course"] = courseList.SelectedValue;
                dtCurrentTable.Rows[i - 1]["Column2Course"] = marks.Text;

                rowIndex++;
            }

            ViewState["CurrentTableCourse"] = dtCurrentTable;
            //grvStudentDetails.DataSource = dtCurrentTable;
            //grvStudentDetails.DataBind();
        }
        //SetPreviousData();
    }
}
