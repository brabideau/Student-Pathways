using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentPreferences : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void SubmitPrefs_Click(object sender, EventArgs e)
    {
        //gather the question ids and answer values for each value on the page and send it
        //to the database for evaluation (packaged with the initial questions)
        List<StudentPreference> myPreferences = new List<StudentPreference> { };

        foreach (GridViewRow row in PrefQuestions.Rows)
        {
                myPreferences.Add(new StudentPreference(
                           Convert.ToInt32(row.Cells[0].Text),
                           Convert.ToInt32((row.FindControl("RBL_YN") as RadioButtonList).SelectedValue)

                                ));
        }
    }
    protected void CurrentStudent_CheckedChanged(object sender, EventArgs e)
    {
        if (CurrentStudent.Checked)
        {
            chooseProgram.Visible = true;
        }
        else
        {
            chooseProgram.Visible = false;
        }
    }

    protected void stepOneNext_Click(object sender, EventArgs e)
    {
        stepOne.Visible = false;
        step2.Visible = true;
        int programid;
        int semester;
        bool switchProgram;

        if (CurrentStudent.Checked == true)
        {
            programid = int.Parse(ProgramDropDown.SelectedValue);
            semester = int.Parse(SemesterDropDown.SelectedValue);
            if (ChangeProgram.Checked == true)
            {
                switchProgram = true;
            }
            else
            {
                switchProgram = false;
            }
            //AshleyTestController sysmgr = new AshleyTestController();
            //int reportid = sysmgr.ReportingDataAddProgramInfo(programid, semester, switchProgram);
            //ReportLabel.Text = reportid.ToString();
        }
    }
    protected void onPreviousClick(object sender, EventArgs e)
    {
        step2.Visible = false;
        stepOne.Visible = true;
    }
    protected void Populate_Program(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        AdminController sysmgr = new AdminController();
        int category = Convert.ToInt32(CategoryDropDown.SelectedValue);
        ProgramDropDown.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramDropDown.DataBind();
    }
    protected void stepThreePrevious_Click(object sender, EventArgs e)
    {
        stepThree.Visible = false;
        step2.Visible = true;
    }
}