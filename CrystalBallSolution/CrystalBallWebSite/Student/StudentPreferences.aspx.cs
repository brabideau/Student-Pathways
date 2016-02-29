using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentPreferences : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<StudentPreference> myPreferences = new List<StudentPreference> { };

        foreach (GridViewRow row in PrefQuestions.Rows)
        {
            if (!string.IsNullOrEmpty((row.FindControl("DL_StudentPreference") as DropDownList).SelectedValue))
                {
                    myPreferences.Add(new StudentPreference(
                               Convert.ToInt32(row.Cells[0].Text),
                               Convert.ToInt32((row.FindControl("DL_StudentPreference") as DropDownList).SelectedValue)
                
                                    ));
                }
        }
    }
}