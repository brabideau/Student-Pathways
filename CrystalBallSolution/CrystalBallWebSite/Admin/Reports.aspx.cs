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

    protected void Submit_Click(object sender, EventArgs e)
    {
       List<StudentPreferenceSummary> leftData = (List<StudentPreferenceSummary>)ViewState["leftData"];
       List<StudentPreferenceSummary> rightData = (List<StudentPreferenceSummary>)ViewState["rightData"];
        if (((LinkButton)sender).ID == "Search_Left")
        {
            leftData = Get_Data();
            GV_PreferenceSummaries_Left.DataSource = leftData;
            GV_PreferenceSummaries_Left.DataBind();
            ViewState["leftData"] = leftData;
        }
        else
        {
            rightData = Get_Data();
            GV_PreferenceSummaries_Right.DataSource = rightData;
            GV_PreferenceSummaries_Right.DataBind();
            ViewState["rightData"] = rightData;
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
                    diff = rightData[i].Yes - leftData[i].Yes;
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


        //Apply filters
        if (Convert.ToInt32(DL_Year.SelectedValue) != -1)
        {
            int year = Convert.ToInt32(DL_Year.SelectedValue);
            var rows = myData.Select("SearchYear <>" + year);

            foreach (var row in rows)
            {
                row.Delete();
            }
        }

        if (Convert.ToInt32(DL_Month.SelectedValue) != -1)
        {
            int month = Convert.ToInt32(DL_Month.SelectedValue);
            var rows = myData.Select("SearchMonth <>" + month);

            foreach (var row in rows)
            {
                row.Delete();
            }
        }

        if (Convert.ToInt32(DL_Program.SelectedValue) != -1)
        {
            int programID = Convert.ToInt32(DL_Program.SelectedValue);

            if (programID > 0)
            {
                var rows = myData.Select("ProgramID <> " +programID);

                foreach (var row in rows)
                {
                    row.Delete();
                }
            }

            if (Convert.ToInt32(DL_Semester.SelectedValue) != -1)
            {
                int sem = Convert.ToInt32(DL_Semester.SelectedValue);

                var rows = myData.Select("Semester <>" + sem);

                foreach (var row in rows)
                {
                    row.Delete();
                }
            }

            if (Convert.ToInt32(DL_Change.SelectedValue) != -1)
            {
                if (Convert.ToInt32(DL_Change.SelectedValue) == 1)
                {
                    int sem = Convert.ToInt32(DL_Semester.SelectedValue);

                    var rows = myData.Select("ChangeProgram = false");

                    foreach (var row in rows)
                    {
                        row.Delete();
                    }
                }
                else
                {
                    int sem = Convert.ToInt32(DL_Semester.SelectedValue);

                    var rows = myData.Select("ChangeProgram = true");

                    foreach (var row in rows)
                    {
                        row.Delete();
                    }
                }
            }
        }
         var summaryList = controller.Get_Summary_Data(myData);
        return summaryList;
    }
}