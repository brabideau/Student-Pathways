using CrystalBallSystem.BLL;
using System;
using System.Collections;
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
                    if (string.IsNullOrEmpty((row.FindControl("DL_Course") as DropDownList).SelectedValue))
                    {
                        MessageUserControl.ShowInfo("Please make a course selection");
                        trigger = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty((row.FindControl("TB_EnterMarks") as TextBox).Text))
                        {
                            MessageUserControl.ShowInfo("Please enter a mark");
                            trigger = false;
                        }
                        else
                        {
                            if (Int32.TryParse((row.FindControl("TB_EnterMarks") as TextBox).Text, out i) == false)
                            {
                                MessageUserControl.ShowInfo("mark must be a integer");
                                trigger = false;
                            }
                            else
                            {

                                if (int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) <= 0 || int.Parse((row.FindControl("TB_EnterMarks") as TextBox).Text) > 100)
                                {
                                    MessageUserControl.ShowInfo("mark must between 0 to 100.");
                                    trigger = false;
                                }
                                else
                                {
                                    MessageUserControl.ShowInfo("Successed!");
                                }
                            }     
                        }                        
                    }
                                   
                }
                if (trigger == true)
                {
                    AddNewRowToCourse();
                }
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

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void search_Click(object sender, EventArgs e)
    {
        //clear the old cache
        List<string> keys = new List<string>();
        //retrieve application cache enumerator
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            keys.Add(enumerator.Key.ToString());
        }
        for (int k = 0; k < keys.Count; k++ )
        {
            Cache.Remove(keys[k]);
        }

        HttpCookie test = new HttpCookie("DemoCookie");
        //once cache is cleared, add new items to the cache
        for (int i = 0; i < GV_Course.Rows.Count; i++)
        {
            //get the appropriate cell and read the value so it can be passed to the cache.
            /*
            DropDownList course = (DropDownList)GV_Course.Rows[i].FindControl("DL_Course");
            string courseID = course.SelectedValue;

            Cache.Insert(i.ToString(), courseID, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(14,0,0,0));
             */
            //delete old cookies from the cache            
            DropDownList courseID = (DropDownList)GV_Course.Rows[i].FindControl("DL_Course");
            TextBox mark = (TextBox)GV_Course.Rows[i].FindControl("TB_EnterMarks");
            test.Values["CourseID" + i] = courseID.SelectedValue;
            test.Values["Mark" + i] = mark.Text;
            Response.Cookies.Add(test);

            if (Request.Cookies["DemoCookie"] != null)
            {
                //get all the cookie data
                string userCourses = "";
                string userMarks = "";
                int x = i;
                if (Request.Cookies["DemoCookie"]["CourseID" + i] != null)                               
                {
                    while (x >= 0)
                    {
                        userCourses += Request.Cookies["DemoCookie"]["CourseID" + x];
                        userMarks += Request.Cookies["DemoCookie"]["Mark" + x];
                        x--;
                    }
                }

                //write the cookie data to the page
                MessageUserControl.ShowInfo(userCourses + userMarks);

                //remove the cookie data
                //HttpCookie myCookie = new HttpCookie("DemoCookie");
                test.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Add(test);
            }
        }
        //call the query method in the controller to access the database for a list of results
        //needs to receive the required mark for each program and loop through to ensure parameters are met
        //for each row in the gridview log the courseid and mark
        StudentController sysmgr = new StudentController();
        List<int> entranceID = new List<int>();
        foreach (GridViewRow row in GV_Course.Rows)
        {
            //loop through each row and query the db, logging each result to a list
            var listData = row.FindControl("DL_Course") as DropDownList;
            var markBox = row.FindControl("TB_EnterMarks") as TextBox;
            int tempInt = Convert.ToInt32(listData.SelectedValue);
            int tempMark = Convert.ToInt32(markBox.Text);
            int temp = sysmgr.GetProgramList(tempInt, tempMark);
            entranceID.Add(temp);
        }
        //log the EntranceRequirementID to a table or listview
        int[] entranceIDArray = entranceID.ToArray();

        for (int a = 0; a < entranceIDArray.Length; a++)
        {
            ListItem item = new ListItem(entranceIDArray[a].ToString());
            demoList.Items.Add(item);
        }  
    }
}
