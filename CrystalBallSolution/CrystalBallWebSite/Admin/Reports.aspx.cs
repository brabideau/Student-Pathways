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

    }

    protected void Submit_Click(object sender, EventArgs e)
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

        GV_PreferenceSummaries.DataSource = summaryList;
        GV_PreferenceSummaries.DataBind();
    }


}