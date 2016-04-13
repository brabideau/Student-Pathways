using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_ManageCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            //NO
            Response.Redirect("~/Account/Login.aspx");
        }

    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void CategoryList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();
        try
        {
            Label CategoryIDBox = (Label)CategoryList.EditItem.FindControl("CategoryIDLabel");
            TextBox CategoryDescriptionText = (TextBox)CategoryList.EditItem.FindControl("CategoryDescriptionTextBox");

            var category = new Category();

            category.CategoryID = int.Parse(CategoryIDBox.Text);
            category.CategoryDescription = CategoryDescriptionText.Text;

            sysmr.UpdateCategory(category);
            CategoryList.DataBind();
        }
        catch(Exception error)
        {
            MessageUserControl.ShowInfo(error.Message);
        }       
    }
}