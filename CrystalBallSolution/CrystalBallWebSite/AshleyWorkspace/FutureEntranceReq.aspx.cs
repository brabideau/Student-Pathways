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

    }

    protected void Populate_EntranceReqs(int programID)
    {
        AdminController sysmgr = new AdminController();
        var entReq = sysmgr.Get_SubjectReq_ByProgram(programID);
        //LV_SubjectReq.DataSource = entReq;
        //LV_SubjectReq.DataBind();

        //var courses = 

        //AdminController sysmgr = new AdminController();
        ////populate High School Entrance Requirements
        //List<SubjectRequirement> subjectList = sysmgr.Get_SubjectReq_ByProgram(programID);
        //LV_SubjectReq.DataSource = subjectList;
        //LV_SubjectReq.DataBind();

        //List<GetHSCourseCode> ereqList = new List<GetHSCourseCode> { };
        //int subjectID;

        ////and the classes for each subject

        //foreach (ListViewItem x in LV_SubjectReq.Items)
        //{
        //    subjectID = Convert.ToInt32((x.FindControl("SubjectIDLabel") as Label).Text);
        //    var courses = x.FindControl("GV_EntranceReqs") as GridView;
        //    ereqList = sysmgr.Get_EntReq_ByProgram_Subject(programID, subjectID);
        //    courses.DataSource = ereqList;
        //    courses.DataBind();
        //}




        ////populate Degree Entrance Requirements

        //List<DegreeEntranceRequirement> degEntList = sysmgr.Get_DegEntReq_ByProgram(programID);

        //LV_DegreeEntranceReq.DataSource = degEntList;
        //LV_DegreeEntranceReq.DataBind();
    }

    protected void GV_EntranceReqs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int subjectID = Convert.ToInt32(LV_SubjectReq.DataKeys[e.RowIndex].Value);
        //int subjectID = Convert.ToInt32((x.FindControl("SubjectIDLabel") as Label).Text);
        //int programID = Int32.Parse(ProgramIDLabel.Text);
        //int eReqid = Convert.ToInt32(GV_EntranceReqs.DataKeys[e.RowIndex].Value);
        //AdminController sysmgr = new AdminController();
        //sysmgr.EntranceReq_Delete(eReqid);
        //GV_EntranceReqs.DataSource = sysmgr.Get_EntReq_ByProgram_Subject(programID, subjectID);
        //GV_EntranceReqs.DataBind();
    }

    protected void Save_EntranceReq(object sender, EventArgs e)
    {

    }
}