<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SelectNaitCourses.aspx.cs" Inherits="User_SelectNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Select NAIT Course</h1>
    <div>
        <label>Please select a program</label>
        <br />
        <asp:DropDownList runat="server" ID="ProgramDropDownList" DataSourceID="SelectProgramODB" DataTextField="ProgramName" DataValueField="ProgramID">
            <asp:ListItem Text="text1" />
            <asp:ListItem Text="text2" />
        </asp:DropDownList>
    </div>
    <div>

        <asp:Label ID="Label1" runat="server" Text="Please search the NAIT course you want."></asp:Label>
        <asp:TextBox ID="SearchTextBox" runat="server" Width="200px"></asp:TextBox><asp:Button ID="Search" runat="server" Text="Search" />
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="NaitCourseODB"
            width="80%" Align="Center">
            <Columns>
                <asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" />
                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
                <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
                <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />
            </Columns>
            <EmptyDataTemplate>
                No data found.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <div>
        
    </div>
    
    <div>
        <asp:ObjectDataSource ID="SelectProgramODB" runat="server" SelectMethod="Program_List" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="NaitCourseODB" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SearchNaitCourses" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchInfo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ProgramDropDownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

