using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.BLL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


public partial class Admin_Reports : System.Web.UI.Page
{
    #region general stuff
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            List<StudentPreferenceSummary> leftData = new List<StudentPreferenceSummary> { };
            Session["leftData"] = leftData;


            //Fills out the program data with current month/year on first page load

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            DL_Year.SelectedValue = year.ToString();
            DL_Month.SelectedValue = month.ToString();

            Program_Year_Label.Text = year.ToString();
            Program_Month_Label.Text = DL_Month.SelectedItem.Text;

            ReportController sysmgr = new ReportController();

            List<StudentsDroppingSummary> dropping = sysmgr.StudentsDropping_by_Program(year, month);

            GV_Program_Dropping.DataSource = dropping;
            GV_Program_Dropping.DataBind();

            Session["StudentsDropping"] = dropping;

            List<ProgramFrequency> frequency = sysmgr.Get_Program_Frequency(year, month);

            Session["ProgramFrequency"] = frequency;

            LV_ProgramFrequency.DataSource = frequency;
            LV_ProgramFrequency.DataBind();

        }
    }

    protected void Change_Tab(object sender, EventArgs e)
    {
        string value = Tab_Labels.SelectedValue;

        switch (value)
        {
            case "1":
                Program_Button_Click(sender, e);
                break;
            case "2":
                Student_Button_Click(sender, e);
                break;

        }
    }

    protected void Program_Button_Click(object sender, EventArgs e)
    {
        ProgramData.Visible = true;
        StudentData.Visible = false;
    }

    protected void Student_Button_Click(object sender, EventArgs e)
    {
        ProgramData.Visible = false;
        StudentData.Visible = true;
    }
    #endregion


    #region program data
    protected void Program_Submit_Click(object sender, EventArgs e)
    {
        Program_Year_Label.Text = DL_Year.SelectedItem.Text;
        Program_Month_Label.Text = DL_Month.SelectedItem.Text;

        int? year = Convert.ToInt32(DL_Year.SelectedValue);
        int? month = Convert.ToInt32(DL_Month.SelectedValue);
        
        if(year == -1)
        {
            year = null;
        }

        if (month == -1)
        {
            month = null;
        }


        ReportController sysmgr = new ReportController();


        List<ProgramFrequency> frequency = sysmgr.Get_Program_Frequency(year, month);

        Session["ProgramFrequency"] = frequency;

        LV_ProgramFrequency.DataSource = frequency;
        LV_ProgramFrequency.DataBind();

        
        List<StudentsDroppingSummary> dropping = sysmgr.StudentsDropping_by_Program(year, month);

        Session["StudentsDropping"] = dropping;

        GV_Program_Dropping.DataSource = dropping;
        GV_Program_Dropping.DataBind();

    }

    #endregion

    #region student data
    protected void Submit_Click(object sender, EventArgs e)
    {
 
        List<StudentPreferenceSummary> leftData = Get_Data();
        LV_PreferenceSummaries_Left.DataSource = leftData;
        LV_PreferenceSummaries_Left.DataBind();
        Session["leftData"] = leftData;

        //fill out filter info
        Year_Left.Text = DL_Year.SelectedItem.Text;
        Month_Left.Text = DL_Month.SelectedItem.Text;

        if (Convert.ToInt32(DL_Program.SelectedItem.Value) > 0)
        {
            Program_Left.Text = "students in " + DL_Program.SelectedItem.Text;
        }
        else
        {
            Program_Left.Text = DL_Program.SelectedItem.Text;
        }

        if (DL_Program.SelectedItem.Value != "-1")
        {
            if (DL_Semester.SelectedItem.Value != "0")
            {
                Semester_Left.Text = "In their " + DL_Semester.SelectedItem.Value + " year of study";
            }
            else
            {
                Semester_Left.Text = "In any year of study";
            }

            if (DL_Change.SelectedItem.Value == "1")
            {
                Dropping_Left.Text = "Who want to change programs";
            }
            else if (DL_Change.SelectedItem.Value == "0")
            {
                Dropping_Left.Text = "Who want to stay in their current program";
            }
        }
        else
        {
            Semester_Left.Text = "";
            Dropping_Left.Text = "";
        }
    }



        protected void NewStudents_Check(object sender, EventArgs e)
        {
            if (DL_Program.SelectedValue == "-1")
            {
                DL_Semester.Enabled = false;
                DL_Change.Enabled = false;
            }
            else
            {
                DL_Semester.Enabled = true;
                DL_Change.Enabled = true;
            }
        }

    

    

    protected List<StudentPreferenceSummary> Get_Data() //object sender, EventArgs e
    {
        DataTable myData = new DataTable();

        ReportController controller = new ReportController();

        //initialize the data set

        if (Convert.ToInt32(DL_Program.SelectedValue) == -1)
        {
            myData = controller.Get_NewStudent_Data();
        }
        else
        {
            myData = controller.Get_CurrentStudent_Data();
        }

        List<StudentPreferenceSummary> results = new List<StudentPreferenceSummary>();

            results = Filter_Data(myData);
 
        return results;
    }

    protected List<StudentPreferenceSummary> Filter_Data(DataTable myData)
    {
        ReportController controller = new ReportController();
        //Apply filters
        int year = Convert.ToInt32(DL_Year.SelectedValue);
        int month = Convert.ToInt32(DL_Month.SelectedValue);
        int programID = Convert.ToInt32(DL_Program.SelectedValue);
        int sem = Convert.ToInt32(DL_Semester.SelectedValue);
        int change = Convert.ToInt32(DL_Change.SelectedValue);


        if (year != -1 && myData.Rows.Count > 0)
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("SearchYear") == year
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (month != -1 && myData.Rows.Count > 0)
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("SearchMonth") == month
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (programID > 0 && myData.Rows.Count > 0) //only if a program was selected
        {
            var rows = from x in myData.AsEnumerable()
                       where x.Field<int>("ProgramID") == programID
                       select x;
            if (rows.Count() > 0)
            {
                myData = rows.CopyToDataTable();
            }
            else
                {
                    myData.Clear();
                }
        }

        if (programID > -1 && myData.Rows.Count > 0) //only if new students were NOT selected
        {
            if (sem != 0 && myData.Rows.Count > 0) //if a semester was selected
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<int>("Semester") == sem
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }
            
            if (change == 1 && myData.Rows.Count > 0)
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<bool>("ChangeProgram") == true
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }
            else if (change == 0 )
            {
                var rows = from x in myData.AsEnumerable()
                           where x.Field<bool>("ChangeProgram") == false
                           select x;
                if (rows.Count() > 0)
                {
                    myData = rows.CopyToDataTable();
                }
                else
                {
                    myData.Clear();
                }
            }

            
        }
        
         var summaryList = controller.Get_Summary_Data(myData);
        return summaryList;
    }
    #endregion

    #region pdf
    protected void Program_PDF_Download(object sender, EventArgs e)
    {
        
        Document myPdf = new Document(); // Default size is 8.5" x 11" (standard printer paper size)

        Font h1 = FontFactory.GetFont("Arial", 28);

        Font h2 = FontFactory.GetFont("Arial", 18);

        string path = Server.MapPath("../PDFs");


        // Requires 'using System.IO'  -- this allows you to create files
        PdfWriter.GetInstance(myPdf, new FileStream(path + "/Pathways_ProgramReport.pdf", FileMode.Create));

        // Open the pdf so you can start working on it
        myPdf.Open();

        // Table designed for pdfs. Value in () is number of columns.
        PdfPTable programFreq = new PdfPTable(2);
        programFreq.WidthPercentage = 80;//a4 paper width is 595.0 and 80% of it is 476f
        float[] cellWidth = new float[2];
        cellWidth[0] = 375f;
        cellWidth[1] = 119f;
        programFreq.SetWidths(cellWidth);
        

        // Create and add the header and subheader
        PdfPCell header = new PdfPCell(new Phrase("Frequency of programs being displayed in results in " + DL_Month.SelectedItem + " " + DL_Year.SelectedItem, h1));
        header.Padding = 10;
        header.Colspan = 2;
        header.HorizontalAlignment = 1;
        //the PdfPTable doesn't support that natively. However, the PdfPCell supports a property that takes a custom implementation of IPdfPCellEvent which will get called whenever a cell layout happens.
        //http://stackoverflow.com/questions/10231770/itextsharp-table-cell-spacing-possible 
        programFreq.DefaultCell.Padding = 10;
        programFreq.AddCell(header);
        //pdf cell guideline http://www.coderanch.com/how-to/javadoc/itext-2.1.7/com/lowagie/text/pdf/PdfCell.html
        PdfPCell secondHeader1 = new PdfPCell(new Phrase("Program Name", h2));
        secondHeader1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        secondHeader1.Padding = 5;
        programFreq.AddCell(secondHeader1);
        PdfPCell secondHeader2 = new PdfPCell(new Phrase("# of times shown", h2));
        secondHeader2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        secondHeader2.Padding = 5;
        programFreq.AddCell(secondHeader2);

        // Get data to put in this table
        List<ProgramFrequency> frequency = (List<ProgramFrequency>)Session["ProgramFrequency"];

        //align paragraph.Alignment = Element.ALIGN_CENTER
        //iterate through the data and put it in the table
        foreach(var item in frequency)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(item.Program.ToString()));
            //cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.Padding = 5;
            programFreq.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase(item.Frequency.ToString()));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Padding = 5;
            programFreq.AddCell(cell2);
            //------------how to set up cell-------------------------------------
            //PdfPTable table = new PdfPTable(3);
            //table.AddCell("Cell 1");
            //PdfPCell cell = new PdfPCell(new Phrase("Cell 2", new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.YELLOW)));
            //cell.BackgroundColor = new Color(0, 150, 0);
            //cell.BorderColor = new Color(255, 242, 0);
            //cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            //cell.BorderWidthBottom = 3f;
            //cell.BorderWidthTop = 3f;
            //cell.PaddingBottom = 10f;
            //cell.PaddingLeft = 20f;
            //cell.PaddingTop = 4f;
            //table.AddCell(cell);
            //table.AddCell("Cell 3");
            //doc.Add(table);

        }

        

        // Add the table to the pdf
        myPdf.Add(programFreq);

        string now = DateTime.Now.ToString("MMM d, yyyy h:mm tt");

        myPdf.Add(new Paragraph("Report generated on " + now));
        // Close the pdf when you are finished with it
        myPdf.Close();
        Response.Redirect("../PDFs/Pathways_ProgramReport.pdf");


    }


    #endregion
}
