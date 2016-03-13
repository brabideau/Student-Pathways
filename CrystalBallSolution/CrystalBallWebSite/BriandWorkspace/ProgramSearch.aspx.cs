using CrystalBallSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Briand_Workspace_ProgramSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Search_Click(object sender, EventArgs e)
    {

    }

    protected void Populate_Program(object sender, EventArgs e)
    {
        //get the value of the selected value in the drop down list and populate the program
        //list data source based on that value
        //AdminController sysmgr = new AdminController();
       // int category = Convert.ToInt32(DL_Category.SelectedValue);
        //ProgramGridView.DataSource = sysmgr.GetProgramByCategory(category);
        ProgramGridView.DataBind();
        DL_Category.SelectedValue = "0";
    }
}