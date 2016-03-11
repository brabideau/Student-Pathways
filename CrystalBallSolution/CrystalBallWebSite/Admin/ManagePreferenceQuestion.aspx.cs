using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

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

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        QuestionListView.DataSource = null;
        QuestionListView.Visible = false;
    }
}