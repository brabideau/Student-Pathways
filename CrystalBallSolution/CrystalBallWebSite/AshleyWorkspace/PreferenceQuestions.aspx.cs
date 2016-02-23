using CrystalBallSystem.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AshleyWorkspace_PreferenceQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Credential_Click(object sender, EventArgs e)
    {
        enterCourses.Visible = true;
        if (int.Parse(CertificationDropDown.SelectedValue) == 3)
        {
            EnglishList.Enabled = false;
            MathList.Enabled = false;
            ScienceList.Enabled = false;
            SocialList.Enabled = false;
            OtherList.Enabled = false;
        }
        else
        {
            EnglishList.Enabled = true;
            MathList.Enabled = true;
            ScienceList.Enabled = true;
            SocialList.Enabled = true;
            OtherList.Enabled = true;
        }        
    }

    protected void submitCourseButton_Click(object sender, EventArgs e)
    {
        //clear the old cache
        List<string> keys = new List<string>();
        //retrieve application cache enumerator
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            keys.Add(enumerator.Key.ToString());
        }
        for (int k = 0; k < keys.Count; k++)
        {
            Cache.Remove(keys[k]);
        }
        HttpCookie test = new HttpCookie("DemoCookie");

        for (int count = 0; count < EnglishList.Items.Count; count++)
        {
            if (EnglishList.Items[count].Selected)
            {
                test.Values["Courses" + count] = EnglishList.Items[count].Text;
                Response.Cookies.Add(test);
            }

            //get all the cookie data
            if (Request.Cookies["DemoCookie"] != null)
            {
                string userCourses = "";
                int x = count;
                if (Request.Cookies["DemoCookie"]["Courses" + x] != null)
                {
                    while (x >= 0)
                    {
                        userCourses += Request.Cookies["DemoCookie"]["Courses" + x];
                        x--;
                    }
                }

                //remove the cookie data
                //HttpCookie myCookie = new HttpCookie("DemoCookie");
                test.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Add(test);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        step1.Visible = false;
        if (AnswerQuestions.Checked)
        {
            stepAlmost2.Visible = true;
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

    protected void ButtonAlmost2_Click(object sender, EventArgs e)
    {
        stepAlmost2.Visible = false;
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
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<int> questionIDs = new List<int>();
        List<bool> questionAnswers = new List<bool>();        

        //Test getting correct data
        MessageUserControl.TryRun(() =>
        {
            string y = "";
            bool check;
            //int reportid = int.Parse(ReportLabel.Text);
            AshleyTestController sysmgr = new AshleyTestController();
            
            foreach (GridViewRow row in QuestionGridview.Rows)
            {
                var questionID = int.Parse(row.Cells[0].Text);
                var isChecked = row.FindControl("Check") as CheckBox;
                check = isChecked.Checked;
                questionIDs.Add(questionID);
                questionAnswers.Add(check);
                //sysmgr.ReportingDataAddQuestionInfo(questionID, check, reportid);
            }  

            ////test - display results
            //foreach (int x in questionIDs)
            //{
            //    y += x;
            //}
            //foreach (bool a in questionAnswers)
            //{
            //    z += a;
            //}
            //MessageUserControl.ShowInfo(y + z);
            
            List<int> tally = sysmgr.QuestionTally(questionIDs, questionAnswers);
            //test - display results
            foreach (int x in tally)
            {
                y += x;
            }
            MessageUserControl.ShowInfo(y);
        });


    }
}