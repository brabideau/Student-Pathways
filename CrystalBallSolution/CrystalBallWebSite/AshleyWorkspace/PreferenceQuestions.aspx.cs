using CrystalBallSystem.BLL;
using System;
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
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<int> questionIDs = new List<int>();
        List<bool> questionAnswers = new List<bool>();

        //Test getting correct data
        MessageUserControl.TryRun(() =>
        {
            string y = "", z = "";
            bool check;
            
            foreach (GridViewRow row in QuestionGridview.Rows)
            {
                var questionID = int.Parse(row.Cells[0].Text);
                var isChecked = row.FindControl("Check") as CheckBox;
                check = isChecked.Checked;
                questionIDs.Add(questionID);
                questionAnswers.Add(check);
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
            
            AshleyTestController sysmgr = new AshleyTestController();
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