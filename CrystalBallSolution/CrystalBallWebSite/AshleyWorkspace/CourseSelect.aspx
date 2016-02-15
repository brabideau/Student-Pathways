<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CourseSelect.aspx.cs" Inherits="AshleyWorkspace_CourseSelect" %>

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
        <div class="box">
            <h2>English</h2>
                <asp:CheckBoxList ID="EnglishList" runat="server" DataSourceID="EnglishListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
        </div>
        <div class="box">
            <h2>Math</h2>
            <asp:CheckBoxList ID="MathList" runat="server" DataSourceID="MathListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
            </asp:CheckBoxList>
        </div>
        <div class="box">
            <h2>Science</h2>
            <asp:CheckBoxList ID="ScienceList" runat="server" DataSourceID="ScienceListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
            </asp:CheckBoxList>
        </div>
        <div class="box">
            <h2>Social</h2>
            <asp:CheckBoxList ID="SocialList" runat="server" DataSourceID="SocialListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
            </asp:CheckBoxList>
        </div>
        <div class="box2">
            <h2>Other</h2>
            <asp:CheckBoxList ID="OtherList" runat="server" DataSourceID="OtherListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
            </asp:CheckBoxList>
        </div>

            <br />
            <br />
        <div class="clear"></div>
        <div class="button">
            <asp:Button ID="submitCourseButton" runat="server" Text="Submit Courses" OnClick="submitCourseButton_Click" />
        </div>
        <br />
        <asp:ObjectDataSource ID="EnglishListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEnglishCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="MathListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMathCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ScienceListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetScienceCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SocialListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSocialCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OtherListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetOtherCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
</div>
</asp:Content>

