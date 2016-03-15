using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;

public partial class Admin_ManagePreferenceQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ProgramList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        ProgramList.SelectedIndex = e.NewSelectedIndex;
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var questionData = sysmr.GetQuestionsByProgram(proId);
        QuestionListView.DataSource = questionData;
        QuestionListView.Visible = true;
        QuestionListView.DataBind();
    }

    private void BindList()
    {
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var questionData = sysmr.GetQuestionsByProgram(proId);

        QuestionListView.DataSource = questionData;
        QuestionListView.DataBind();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        QuestionListView.DataSource = null;
        QuestionListView.Visible = false;
    }

    protected void QuestionListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);

        Label questionId = (Label)QuestionListView.EditItem.FindControl("QuestionIDTextLabel");
        RadioButtonList answer = (RadioButtonList)QuestionListView.EditItem.FindControl("AnswerRadioButtons");
        Label question = (Label)QuestionListView.EditItem.FindControl("QuestionTextLabel");

        var programPreference = new GetProgramPreferenceQuestions();

        programPreference.ProgramID = proId;
        programPreference.QuestionID = int.Parse(questionId.Text);
        programPreference.Question = question.Text;
      
        programPreference.Answer = Convert.ToBoolean(answer.SelectedValue);


        sysmr.UpdateProgramPreferenceQuestion(programPreference);
        QuestionListView.EditIndex = -1;

        BindList();
    }

    protected void QuestionListView_ItemEditing(object sender, ListViewEditEventArgs e)
    {
       QuestionListView.EditIndex = e.NewEditIndex;
        BindList();
    }

    protected void QuestionListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        
        QuestionListView.EditIndex = -1;
        BindList();
    }

    protected void QuestionListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {

        AdminController sysmr = new AdminController();
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);

        DropDownList questionList = (DropDownList)QuestionListView.InsertItem.FindControl("QuestionDropDownList");
        int questionId = int.Parse(questionList.SelectedValue);
        RadioButtonList answer = (RadioButtonList)QuestionListView.InsertItem.FindControl("AnswerRadioButtons");

        var newQuestion = new ProgramPreference();

        newQuestion.QuestionID = questionId;
        newQuestion.ProgramID = proId;
        newQuestion.Answer= Convert.ToBoolean(answer.SelectedValue);

        if (questionId != 0)
        {
            sysmr.AddProgramPreferenceQuestion(newQuestion);
        }

        BindList();
    }
}