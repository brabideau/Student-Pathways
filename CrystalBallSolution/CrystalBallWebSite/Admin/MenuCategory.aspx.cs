using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

public partial class Admin_MenuCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            //NO
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            currentLogin.Text = User.Identity.GetUserName();
        }
    }
}