using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

<<<<<<< HEAD
using CrystalBallSystem.BLL;

=======
>>>>>>> origin/master
public partial class Admin_ManageNaitCourses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
<<<<<<< HEAD

    //protected void ProgramList_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        Button btn = e.Item.FindControl("SelectButton") as Button;
    //        string script = this.ClientScript.GetPostBackClientHyperlink(btn, "", true);
    //        Panel p = e.Item.FindControl("Panel1") as Panel;
    //        p.Attributes.Add("onclick", script);
    //    }


    //}

    protected void ProgramList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        //NaitCoursesListViewByProgram.DataSourceID = ODSNaitCourses.ID;
        //ListViewItem item = ProgramList.Items[ProgramList.SelectedIndex];
        //Label program = (Label)item.FindControl("ProgramIDLabel");
        ProgramList.SelectedIndex = e.NewSelectedIndex;
        string pid = ProgramList.SelectedDataKey.Value.ToString();
        int proId = Convert.ToInt32(pid);
        AdminController sysmr = new AdminController();
        sysmr.GetCoursesByProgram(proId);

        ProgramList.DataBind();

    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        NaitCoursesListViewByProgram.DataBind();
    }
=======
>>>>>>> origin/master
}