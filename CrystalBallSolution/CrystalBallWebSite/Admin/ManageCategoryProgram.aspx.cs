using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_ManageCategoryProgram : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string typeIn = InputTextbox.Text.ToString();

        int category = int.Parse(CategoryDropDowList.SelectedValue.ToString());

        AdminController sysmr = new AdminController();


        ProgramListView.DataSource = sysmr.findProgram(typeIn, category);
        ProgramListView.DataBind();
      
    }
    protected void ProgramListView_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        ProgramListView.SelectedIndex = e.NewSelectedIndex;
        string pid = ProgramListView.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        var categoryData = sysmr.GetCategoryByProgram(proId);
        categoryListview.DataSource = categoryData;
        categoryListview.DataBind();
    }
}