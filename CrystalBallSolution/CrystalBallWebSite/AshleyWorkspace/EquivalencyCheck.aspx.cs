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

    }
    protected void CButton_Click(object sender, EventArgs e)
    {
        ProgramDropDown.DataBind();
    }
    protected void PButton_Click(object sender, EventArgs e)
    {
        CourseDropDown.DataBind();
    }
    protected void Button_Click(object sender, EventArgs e)
    {
        //GV_Equivalents.DataBind();
    }
}