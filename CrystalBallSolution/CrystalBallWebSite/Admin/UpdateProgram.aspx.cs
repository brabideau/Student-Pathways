using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_UpdateProgram : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void BindList()
    {
        string pid = CategoryDropdownList.SelectedValue.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var programData = sysmr.GetProgramByCategory(proId);

        ProgramListView.DataSource = programData;
        ProgramListView.DataBind();
    }

    protected void ProgramListView_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        ProgramListView.EditIndex = e.NewEditIndex;
        CloseInsert();
        BindList();
    }

    protected void ProgramListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        ProgramListView.EditIndex = -1;
        BindList();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        ProgramListView.InsertItemPosition = InsertItemPosition.None;
        BindList();

    }

    protected void ProgramListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();

        Label programId = (Label)ProgramListView.EditItem.FindControl("ProgramIDLabel");
        DropDownList CredentialType = (DropDownList)ProgramListView.EditItem.FindControl("CredentialTypeDropdownList");
        string CredentialTypeId = CredentialType.SelectedValue.ToString();

        TextBox ProgramNameBox = (TextBox)ProgramListView.EditItem.FindControl("ProgramNameTextBox");
        TextBox EntranceRequirementBox = (TextBox)ProgramListView.EditItem.FindControl("EntranceRequirementTextBox");
        TextBox TotalCreditsBox = (TextBox)ProgramListView.EditItem.FindControl("TotalCreditsTextBox");
        TextBox ProgramLengthBox = (TextBox)ProgramListView.EditItem.FindControl("ProgramLengthTextBox");
        TextBox CompetiveAdvantageBox = (TextBox)ProgramListView.EditItem.FindControl("CompetiveAdvantageTextBox");
        CheckBox Active = (CheckBox)ProgramListView.EditItem.FindControl("ActiveCheckBox");
        TextBox ProgramLinkBox = (TextBox)ProgramListView.EditItem.FindControl("ProgramLinkTextBox");

        var program = new Program();
        program.ProgramID = int.Parse(programId.Text);
        program.CredentialTypeID = int.Parse(CredentialTypeId);
        program.ProgramName = ProgramNameBox.Text;
        program.ProgramDescription = EntranceRequirementBox.Text;
        string credits = TotalCreditsBox.Text;

        if (string.IsNullOrEmpty(credits))
        {
            program.TotalCredits = null;

        }
        else
        {
            program.TotalCredits = double.Parse(credits);
        }

        program.ProgramLength = ProgramLengthBox.Text;
        program.CompetitiveAdvantage = int.Parse(CompetiveAdvantageBox.Text);
        program.Active = Active.Checked;
        program.ProgramLink = ProgramLinkBox.Text;

        sysmr.Program_Update(program);
        ProgramListView.EditIndex = -1;
        BindList();
    }

    private void CloseInsert()
    {
        ProgramListView.InsertItemPosition = InsertItemPosition.None;
        ((LinkButton)ProgramListView.FindControl("NewButton")).Visible = true;
    }

    protected void ProgramListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {


        DropDownList CredentialType = (DropDownList)ProgramListView.InsertItem.FindControl("CredentialTypeDropdownList");
        string CredentialTypeId = CredentialType.SelectedValue.ToString();

        TextBox ProgramNameBox = (TextBox)ProgramListView.InsertItem.FindControl("ProgramNameTextBox");
        TextBox EntranceRequirementBox = (TextBox)ProgramListView.InsertItem.FindControl("EntranceRequirementTextBox");
        TextBox TotalCreditsBox = (TextBox)ProgramListView.InsertItem.FindControl("TotalCreditsTextBox");
        TextBox ProgramLengthBox = (TextBox)ProgramListView.InsertItem.FindControl("ProgramLengthTextBox");
        TextBox CompetiveAdvantageBox = (TextBox)ProgramListView.InsertItem.FindControl("CompetiveAdvantageTextBox");
        CheckBox Active = (CheckBox)ProgramListView.InsertItem.FindControl("ActiveCheckBox");
        TextBox ProgramLinkBox = (TextBox)ProgramListView.InsertItem.FindControl("ProgramLinkTextBox");

        var program = new Program();
        program.CredentialTypeID = int.Parse(CredentialTypeId);
        program.ProgramName = ProgramNameBox.Text;
        program.ProgramDescription = EntranceRequirementBox.Text;
        string credits = TotalCreditsBox.Text;

        if (string.IsNullOrEmpty(credits))
        {
            program.TotalCredits = null;

        }
        else
        {
            program.TotalCredits = double.Parse(credits);
        }

        program.ProgramLength = ProgramLengthBox.Text;
        program.CompetitiveAdvantage = int.Parse(CompetiveAdvantageBox.Text);
        program.Active = Active.Checked;
        program.ProgramLink = ProgramLinkBox.Text;

        List<Program> NewProgram = new List<Program>();
        NewProgram.Add(program);

        string categoryid = CategoryDropdownList.SelectedValue.ToString();
        int cateid = Convert.ToInt32(categoryid);

        AdminController sysmr = new AdminController();
        sysmr.AddProgram(NewProgram, cateid);

        CloseInsert();
        BindList();

    }

    protected void NewButton_Click(object sender, EventArgs e)
    {
        ProgramListView.EditIndex = -1;
        ProgramListView.InsertItemPosition = InsertItemPosition.LastItem;
        ((LinkButton)sender).Visible = false;
        BindList();
    }
}