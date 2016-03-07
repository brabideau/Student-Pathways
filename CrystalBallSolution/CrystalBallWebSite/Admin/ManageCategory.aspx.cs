using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;

public partial class Admin_ManageCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void CategoryList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        AdminController sysmr = new AdminController();

        Label CategoryIDBox = (Label)CategoryList.EditItem.FindControl("CategoryIDLabel");
        TextBox CategoryDescriptionText = (TextBox)CategoryList.EditItem.FindControl("CategoryDescriptionTextBox");

        var category = new Category();

        category.CategoryID = int.Parse(CategoryIDBox.Text);
        category.CategoryDescription = CategoryDescriptionText.Text;

        sysmr.UpdateCategory(category);
        CategoryList.DataBind();

    }
}