using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL;

public partial class BriandWorkspace_CourseSelectB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitCourseButton_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("CourseID", typeof(int)));

        for (int count = 0; count < CB_CourseList.Items.Count; count++)
        {
            if (CB_CourseList.Items[count].Selected)
            {
                dr = dt.NewRow();
                dr["CourseID"] = CB_CourseList.Items[count].Value;
                dt.Rows.Add(dr);
            }
        }


        StudentController something = new StudentController();

        DataTable matches = StudentController.FindProgramMatches(dt);

        ResultsView.DataSource = matches;
        ResultsView.DataBind();


    }
}