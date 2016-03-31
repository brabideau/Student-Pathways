using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AshleyWorkspace_EquivalencyCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Reports.pdf");
        Response.TransmitFile(Server.MapPath("~/PDFs/myPdf.pdf"));
        Response.End();
        Response.Redirect("../Admin/Reports.aspx");
    }
    //protected void CButton_Click(object sender, EventArgs e)
    //{
    //    ProgramDropDown.DataBind();
    //}
    //protected void PButton_Click(object sender, EventArgs e)
    //{
    //    CourseDropDown.DataBind();
    //}
    //protected void Button_Click(object sender, EventArgs e)
    //{
    //    //GV_Equivalents.DataBind();
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        

    }
}