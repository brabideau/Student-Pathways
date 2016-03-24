using CrystalBallSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditSubjectRequirement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void SRRemove_Button_Click(object sender, EventArgs e)
    {
        int sReqID = Convert.ToInt32(SubjectRequirement.SelectedValue);
        testController sysmgr = new testController();
        sysmgr.SR_Delete(sReqID);
    }

    protected void SubjectRequirement_SelectedIndexChanged(object sender, EventArgs e)
    {
        SRCourses.DataBind();
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        testController sysmgr = new testController();
        int hsID = Convert.ToInt32(DL_Course.SelectedValue);
        int srID = Convert.ToInt32(SubjectRequirement.SelectedValue);        
        if (TB_Mark.Text.Trim() == "")
        {
            sysmgr.AddEntranceRequirement_NPIDNM(hsID, srID);
        }
        else
        {
            int mark = Int32.Parse(TB_Mark.Text);
            sysmgr.AddEntranceRequirementNPID(hsID, srID, mark);
        }
        SRCourses.DataBind();
    }
}