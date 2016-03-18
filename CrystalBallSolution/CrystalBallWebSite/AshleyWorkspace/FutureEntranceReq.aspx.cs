using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AshleyWorkspace_FutureEntranceReq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int programID = 2046;
        Populate_EntranceReqs(programID);
    }

    protected void Populate_EntranceReqs(int programID)
    {
        testController sysmgr = new testController();
        var entReq = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataSource = entReq;
        LV_SubjectReq.DataBind();
    }

    protected void LV_SubjectReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        testController sysmgr = new testController();
        int entReqID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        int programID = 2046;
        sysmgr.Equivalency_Delete(entReqID);
        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }

    //CODE THIS POINT ON IS COPY PASTE AND NEEDS EDITING
    protected void AddNew_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GV_Course.Rows)
        {
            var course = row.FindControl("DL_Course") as DropDownList;
            var poQtyLabel = row.FindControl("TB_EnterMarks") as TextBox;
            if (string.IsNullOrEmpty((row.FindControl("DL_Course") as DropDownList).SelectedValue))
            {
                //MessageUserControl.ShowInfo("Please make a course selection");
                //trigger = false;
            }
            else
            {
                if (string.IsNullOrEmpty((row.FindControl("TB_EnterMarks") as TextBox).Text))
                {
                    //MessageUserControl.ShowInfo("Please enter a mark");
                    //trigger = false;
                }
                else
                {
                    AddNewRowToCourse();
                    //if (Int32.TryParse((row.FindControl("TB_EnterMarks") as TextBox).Text, out i) == false)
                    //{
                    //    MessageUserControl.ShowInfo("mark must be a integer");
                    //    trigger = false;
                    //}
                    //else
                    //{

                    //    if (int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) <= 0 || int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) > 100)
                    //    {
                    //        MessageUserControl.ShowInfo("mark must between 0 to 100.");
                    //        trigger = false;
                    //    }
                    //    else
                    //    {
                    //        MessageUserControl.ShowInfo("Successed!");
                    //    }
                    //}     
                }                        
            }                                   
        }
        //if (trigger == true)
        //{
        //    AddNewRowToCourse();
        //}
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

    protected void GVCourse_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        testController sysmgr = new testController();
        int entReqID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        int programID = 2046;
        sysmgr.Equivalency_Delete(entReqID);
        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }    
    
    protected void Save_EntranceReq(object sender, EventArgs e)
    {

    }
}