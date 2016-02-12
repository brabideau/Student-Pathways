<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CourseSelectB.aspx.cs" Inherits="BriandWorkspace_CourseSelectB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        body
        {
            width: 964px;
            margin: 0 auto;
        }
        .box
        {
            width:160px;
            height: 350px;
            margin:10px;
            float:left;
            border-right: 1px solid rgba(128, 128, 128, 0.76);
        }
        .box2
        {
            width:220px;
            margin:10px;
            float:left;
        }
        .clear
        {
            clear: both;
        }
        .button
        {            
            float:right;
            margin-right: 40px;
        }
        h2
        {
            margin-left: 20px;
        }
        h1
        {
            text-align: center;
            margin-bottom: 40px;
        }
        label
        {
            font-weight: normal;
        }
        table
        {
            margin-right: 0;
        }

    </style>
<div class="body">
    <h1>Select the Courses You've Taken</h1>

        <asp:CheckBoxList ID="CB_CourseList" runat="server" DataSourceID="CourseList" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID" RepeatColumns="4" CellPadding="5">
        </asp:CheckBoxList>

            <br />
            <br />
        <div class="clear"></div>
        <div class="button">
            <div class="col-md-3"></div>
            <asp:Button ID="submitCourseButton" runat="server" Text="Submit Courses" OnClick="submitCourseButton_Click" />

            
        <asp:GridView ID="ResultsView" runat="server"></asp:GridView>


        </div>
        <br />
          <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController" ></asp:ObjectDataSource>

</div>
</asp:Content>

