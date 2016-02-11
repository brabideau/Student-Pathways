using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AshleyWorkspace_CourseSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submitCourseButton_Click(object sender, EventArgs e)
    {
        //clear the old cache
        List<string> keys = new List<string>();
        //retrieve application cache enumerator
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            keys.Add(enumerator.Key.ToString());
        }
        for (int k = 0; k < keys.Count; k++)
        {
            Cache.Remove(keys[k]);
        }
        HttpCookie test = new HttpCookie("DemoCookie");

        for (int count = 0; count < EnglishList.Items.Count; count++)
        {
            if(EnglishList.Items[count].Selected)
            {
                test.Values["Courses"]
                Response.Cookies.Add(test);
            }
        }
    }
}