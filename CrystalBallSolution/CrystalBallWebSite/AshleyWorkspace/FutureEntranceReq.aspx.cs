using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AshleyWorkspace_FutureEntranceReq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int programID = 2046;
        Populate_EntranceReqs(programID);
    }

    protected void Populate_EntranceReqs(int programID)
    {
        testController sysmgr = new testController();
        var entReq = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataSource = entReq;
        LV_SubjectReq.DataBind();
    }

    protected void Save_EntranceReq(object sender, EventArgs e)
    {

    }
    protected void LV_SubjectReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        testController sysmgr = new testController();
        int entReqID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        int programID = 2046;
        sysmgr.Equivalency_Delete(entReqID);
        LV_SubjectReq.DataSource = sysmgr.Get_SubjectReq_ByProgram(programID);
        LV_SubjectReq.DataBind();
    }
}