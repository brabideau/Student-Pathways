using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Reports : System.Web.UI.Page
{

    protected void Page_Init()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        Program_Year_Label.Text = year.ToString();
        Program_Month_Label.Text = month.ToString();

        DL_Year.SelectedValue = year.ToString();
        DL_Month.SelectedValue = month.ToString();

        ReportController sysmgr = new ReportController();

        List<StudentsDroppingSummary> dropping = sysmgr.StudentsDropping_by_Program(year, month);

        GV_Program_Dropping.DataSource = dropping;
        GV_Program_Dropping.DataBind();

        List<ProgramFrequency> frequency = sysmgr.Get_Program_Frequency(year, month);

        GV_ProgramFrequency.DataSource = frequency;
        GV_ProgramFrequency.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<StudentPreferenceSummary> leftData = new List<StudentPreferenceSummary> { };
            ViewState["leftData"] = leftData;
            List<StudentPreferenceSummary> rightData = new List<StudentPreferenceSummary> { };
            ViewState["rightData"] = rightData;

        }
    }


    protected void Program_Button_Click(object sender, EventArgs e)
    {
        ProgramData.Visible = true;
        StudentData.Visible = false;
    }

    protected void Student_Button_Click(object sender, EventArgs e)
    {
        ProgramData.Visible = false;
        StudentData.Visible = true;
    }

    #region program data
    protected void Program_Submit_Click(object sender, EventArgs e)
    {

        Program_Year_Label.Text = DL_Year.SelectedItem.Text;
        Program_Month_Label.Text = DL_Month.SelectedItem.Text;

        int? year = Convert.ToInt32(DL_Year.SelectedValue);
        int? month = Convert.ToInt32(DL_Month.SelectedValue);
        
        if(year == -1)
        {
            year = null;
        }

        if (month == -1)
        {
            month = null;
        }

        ReportController sysmgr = new ReportController();

        List<StudentsDroppingSummary> dropping = sysmgr.StudentsDropping_by_Program(year, month);

        GV_Program_Dropping.DataSource = dropping;
        GV_Program_Dropping.DataBind();

        List<ProgramFrequency> frequency = sysmgr.Get_Program_Frequency(year, month);

        GV_ProgramFrequency.DataSource = frequency;
        GV_ProgramFrequency.DataBind();

    }

    #endregion

    #region student data
    protected void Submit_Click(object sender, EventArgs e)
    {
        //retrieve stored data
       List<StudentPreferenceSummary> leftData = (List<StudentPreferenceSummary>)ViewState["leftData"];
       List<StudentPreferenceSummary> rightData = (List<StudentPreferenceSummary>)ViewState["rightData"];

        
        

        if (((LinkButton)sender).ID == "Search_Left")
        {
            leftData = Get_Data();
            GV_PreferenceSummaries_Left.DataSource = leftData;
            GV_PreferenceSummaries_Left.DataBind();
            ViewState["leftData"] = leftData;

            //fill out filter info
            Year_Left.Text = DL_Year.SelectedItem.Text;
            Month_Left.Text = DL_Month.SelectedItem.Text;
            Program_Left.Text = DL_Program.SelectedItem.Text;
            Semester_Left.Text = DL_Semester.SelectedItem.Text;
            Dropping_Left.Text = DL_Change.SelectedItem.Text;
        }
        else
        {
            rightData = Get_Data();
            GV_PreferenceSummaries_Right.DataSource = rightData;
            GV_PreferenceSummaries_Right.DataBind();
            ViewState["rightData"] = rightData;

            //fill out filter info
            Year_Right.Text = DL_Year.SelectedItem.Text;
            Month_Right.Text = DL_Month.SelectedItem.Text;
            Program_Right.Text = DL_Program.SelectedItem.Text;
            Semester_Right.Text = DL_Semester.SelectedItem.Text;
            Dropping_Right.Text = DL_Change.SelectedItem.Text;
        }

        if (leftData != null && rightData != null)
        {
            Compare_Results(leftData, rightData);
        }
    }

    protected void Compare_Results(List<StudentPreferenceSummary> leftData, List<StudentPreferenceSummary> rightData)
    {
        if (leftData.Any() && rightData.Any())
        {
            DataTable compareData = new DataTable { };
            int? diff;

            //DataRow dr = compareData.NewRow();

            compareData.Columns.Add("Difference");

            for (int i = 0; i < leftData.Count; i++ )
            {
                if (leftData[i] != null && rightData[i] != null)
                {
                    diff = rightData[i].PercentYes - leftData[i].PercentYes;
                }
                else
                {
                    diff = null;
                }

             //   dr["Difference"] = diff;
                compareData.Rows.Add(compareData.NewRow()["Difference"] = diff);
                
            }

            GV_Compare.DataSource = compareData;
            GV_Compare.DataBind();
        }
    }

    protected List<StudentPreferenceSummary> Get_Data() //object sender, EventArgs e
    {
        DataTable myData = new DataTable();

        ReportController controller = new ReportController();

        //initialize the data set

        if (Convert.ToInt32(DL_Program.SelectedValue) == -1)
        {
            myData = controller.Get_NewStudent_Data();
        }
        else
        {
            myData = controller.Get_CurrentStudent_Data();
        }

        List<StudentPreferenceSummary> results = new List<StudentPreferenceSummary>();

            results = Filter_Data(myData);
 
        return results;
    }

    protected List<StudentPreferenceSummary> Filter_Data(DataTable myData)
    {
        ReportController controller = new ReportController();
        //Apply filters
        int year = Convert.ToInt32(DL_Year.SelectedValue);
        int month = Convert.ToInt32(DL_Month.SelectedValue);
        int programID = Convert.ToInt32(DL_Program.SelectedValue);
        int sem = Convert.ToInt32(DL_Semester.SelectedValue);
        int change = Convert.ToInt32(DL_Change.SelectedValue);


        if (year != -1 && myData.Rows.Count > 0)
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("SearchYear") == year
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (month != -1 && myData.Rows.Count > 0)
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("SearchMonth") == month
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (programID > 0 && myData.Rows.Count > 0) //only if a program was selected
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("ProgramID") == programID
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (programID > -1 && myData.Rows.Count > 0) //only if new students were NOT selected
        {
            if (sem != -1 && myData.Rows.Count > 0) //if a semester was selected
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<int>("Semester") == sem
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }
            
            if (change == 1 && myData.Rows.Count > 0)
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<bool>("ChangeProgram") == true
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }
            else if (change == 0 )
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<bool>("ChangeProgram") == false
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }

            
        }
        
         var summaryList = controller.Get_Summary_Data(myData);
        return summaryList;
    }
    #endregion
}
