using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        List<NewStudentData> newStudents;
        List<CurrentStudentData> currentStudents;

        //List<StudentPreferenceSummary> summaryList;

        ReportController controller = new ReportController();

        
        // initialize the data set
        
        if (Convert.ToInt32(DL_Program.SelectedValue) == -1)
        {
            newStudents = controller.Get_NewStudent_Data();
            var myData = newStudents;

            if (Convert.ToInt32(DL_Year.SelectedValue) != -1)
            {
                myData = controller.Filter_by_Year(Convert.ToInt32(DL_Year.SelectedValue), myData);
            }

            if (Convert.ToInt32(DL_Month.SelectedValue) != -1)
            {
                myData = controller.Filter_by_Month(Convert.ToInt32(DL_Month.SelectedValue), myData);
            }

            var summaryList = controller.Get_Summary_Data(myData);

            GV_PreferenceSummaries.DataSource = summaryList;
            GV_PreferenceSummaries.DataBind();
        }

        else
        {
            currentStudents = controller.Get_CurrentStudent_Data();
            var myData = currentStudents;

            if (Convert.ToInt32(DL_Year.SelectedValue) != -1)
            {
                myData = controller.Filter_by_Year(Convert.ToInt32(DL_Year.SelectedValue), myData);
            }

            if (Convert.ToInt32(DL_Month.SelectedValue) != -1)
            {
                myData = controller.Filter_by_Month(Convert.ToInt32(DL_Month.SelectedValue), myData);
            }

            if (Convert.ToInt32(DL_Program.SelectedValue) > 0)
            {
                myData = controller.Filter_by_Program(Convert.ToInt32(DL_Program.SelectedValue), myData);
            }

            if (Convert.ToInt32(DL_Program.SelectedValue) != -1)
            {
                if (Convert.ToInt32(DL_Semester.SelectedValue) != -1)
                {
                    myData = controller.Filter_by_Semester(Convert.ToInt32(DL_Semester.SelectedValue), myData);
                }

                if (Convert.ToInt32(DL_Change.SelectedValue) != -1)
                {
                    if (Convert.ToInt32(DL_Change.SelectedValue) == 1)
                    {
                        myData = controller.Filter_by_ChangeProgram(true, myData);
                    }

                    else
                    {
                        myData = controller.Filter_by_ChangeProgram(false, myData);
                    }
                }
                
            }

            var summaryList = controller.Get_Summary_Data(myData);

            GV_PreferenceSummaries.DataSource = summaryList;
            GV_PreferenceSummaries.DataBind();
        }

        
        //// filter the data set

        //if (Convert.ToInt32(DL_Year.SelectedValue) != -1)
        //{
        //    myData = controller.Filter_by_Year(Convert.ToInt32(DL_Year.SelectedValue), myData);
        //}

        //if (Convert.ToInt32(DL_Month.SelectedValue) != -1)
        //{
        //    myData = controller.Filter_by_Month(Convert.ToInt32(DL_Month.SelectedValue), myData);
        //}

        //if (Convert.ToInt32(DL_Program.SelectedValue) > 0)
        //{
        //    myData = controller.Filter_by_Program(Convert.ToInt32(DL_Program.SelectedValue), myData);
        //}

        //if (Convert.ToInt32(DL_Program.SelectedValue) != -1)
        //{
        //    if (Convert.ToInt32(DL_Semester.SelectedValue) != -1)
        //    {
        //        myData = controller.Filter_by_Semester(Convert.ToInt32(DL_Semester.SelectedValue), myData);
        //    }

        //    if (Convert.ToInt32(DL_Change.SelectedValue) != -1)
        //    {
        //        if (Convert.ToInt32(DL_Change.SelectedValue) == 1)
        //        {
        //            myData = controller.Filter_by_ChangeProgram(true, myData);
        //        }

        //        else
        //        {
        //            myData = controller.Filter_by_ChangeProgram(false, myData);
        //        }
        //    }
        //}

        //// Bind to the grid view

        //var summaryList = controller.Get_Summary_Data(myData);

        //GV_PreferenceSummaries.DataSource = summaryList;
        //GV_PreferenceSummaries.DataBind();

    }
}