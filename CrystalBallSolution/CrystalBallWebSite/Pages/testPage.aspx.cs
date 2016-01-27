using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


#region Additional namespace
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
#endregion

public partial class Pages_testPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string email = TextBox1.Text;
        string password = TextBox2.Text;
        Label1.Text = email;
        Label2.Text = password;
        int sID=11 ;
        Student item = new Student();
        item.StudentID = sID;
        item.Email = email;
        item.Password = password;
        StudentController sc = new StudentController();
        sc.Registration(item);
        TextBox1.Text = "";
        TextBox2.Text = "";
    }
}