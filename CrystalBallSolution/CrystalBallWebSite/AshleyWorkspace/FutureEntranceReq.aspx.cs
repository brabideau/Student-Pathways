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
        //hardCoding programID for testing = CHANGE THIS LATER
        int programID = 2046;
        Populate_EntranceReqs(programID);
    }

    //CREATE NO SUBJECTREQUIREMENT GRIDVIEW
    private void PopulateManual()
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
         GV_ManualNewEntrReq.DataSource = dt;
         GV_ManualNewEntrReq.DataBind();
 
     }

    #region for existing entrance requirements
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
    #endregion

    #region for adding an entrance requirement
    //pre-populate subject requirement courses
    protected void SubjectButton_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        int subjectReq = Convert.ToInt32(DL_SubjDesc.SelectedValue);
        if (subjectReq != 0)
        {
            prePopulatedER.Visible = true;
            manualER.Visible = false;
            GV_NewEntrReq.DataSource = sysmgr.Get_CoursesBySubjectRequirement(subjectReq);
            GV_NewEntrReq.DataBind();
        }
        else
        {
            manualER.Visible = true;
            prePopulatedER.Visible = false;
            PopulateManual();
        }
        
    }

    //remove course from requirement list
    protected void GV_NewEntrReq_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView ER_GridView = sender as GridView;
        
        List<GetEntranceReq> items = new List<GetEntranceReq>();
        for (int index = 0; index < ER_GridView.Rows.Count; index++)
        {
            if (index != e.NewSelectedIndex)
            {
                GridViewRow keepRow = ER_GridView.Rows[index];
                var id = keepRow.FindControl("ID") as Label;
                var course = keepRow.FindControl("Course") as Label;
                var mark = keepRow.FindControl("Mark") as TextBox;
                if (mark.Text.Trim() == "")
                {
                    mark.Text = "0";
                }
                items.Add(new GetEntranceReq()
                {
                    HSCourseID = Convert.ToInt32(id.Text),
                    HSCourseName = course.Text,
                    Mark = Convert.ToInt32(mark.Text),
                });
            }
        }

        ER_GridView.DataSource = items;
        ER_GridView.DataBind();
    }    
    #endregion




    protected void AddNew_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in GV_ManualNewEntrReq.Rows)
        //{
            //var course = row.FindControl("DL_Course") as DropDownList;
            //var poQtyLabel = row.FindControl("Marks") as TextBox;
            AddNewRowToCourse();
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
                    DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1Course"] = courseList.Text;
                    dtCurrentTable.Rows[i - 1]["Column2Course"] = marks.Text;

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTableCourse"] = dtCurrentTable;

                GV_ManualNewEntrReq.DataSource = dtCurrentTable;
                GV_ManualNewEntrReq.DataBind();
            }
        }

        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousCourseData();
    }    

    protected void GV_ManualNewEntrReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                GV_ManualNewEntrReq.DataSource = dt;
                GV_ManualNewEntrReq.DataBind();

                for (int i = 0; i < GV_ManualNewEntrReq.Rows.Count - 1; i++)
                {
                    GV_ManualNewEntrReq.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousCourseData();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
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
                    DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                    TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

                    courseList.Text = dt.Rows[i]["Column1Course"].ToString();
                    marks.Text = dt.Rows[i]["Column2Course"].ToString();

                    rowIndex++;
                }
            }
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
                DropDownList courseList = (DropDownList)GV_ManualNewEntrReq.Rows[rowIndex].Cells[1].FindControl("DL_Course");
                TextBox marks = (TextBox)GV_ManualNewEntrReq.Rows[rowIndex].Cells[2].FindControl("Marks");

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

    protected void addPPSubjectButton_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        List<AddEntranceRequirements> er = new List<AddEntranceRequirements>();

        int programID = 2046;
        int subReqID = Convert.ToInt32(DL_SubjDesc.SelectedValue);

        foreach (GridViewRow row in GV_NewEntrReq.Rows)
        {
            int hsID = Convert.ToInt32(row.Cells[0].Text);
            int mark = Convert.ToInt32(row.Cells[2].Text);
            //er.Add(new AddEntranceRequirements()
            //{
            //    hsID, 
            //    subReqID, 
            //    programID, 
            //    mark
            //});
            sysmgr.AddEntranceRequirement(er);
        }
    }
    //SAVE
    protected void Save_EntranceReq(object sender, EventArgs e)
    {

    }
    
}