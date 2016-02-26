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

        List<int> myCourses = new List<int> { };

        for (int count = 0; count < CB_CourseList.Items.Count; count++)
        {
            if (CB_CourseList.Items[count].Selected)
            {

                myCourses.Add(Convert.ToInt32(CB_CourseList.Items[count].Value));

            }
        }



        Session["CourseArray"] = myCourses;
        Response.Redirect("../KyleWorkspace/Results.aspx");
        


    }
}