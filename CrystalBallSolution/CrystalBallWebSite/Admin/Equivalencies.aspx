<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equivalencies.aspx.cs" Inherits="Admin_Equivalencies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" id="equivalencyInformation">
        <asp:Label ID="Label1" runat="server" Text="Select a Category: "></asp:Label>
        <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                DataSourceID="CategoryODS" 
                                DataTextField="CategoryDescription" 
                                DataValueField="CategoryID"
                                AppendDataBoundItems="True">
            <asp:ListItem Value="-3">[Select Category]</asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton ID="CategoryButton" runat="server">Next</asp:LinkButton>  
        <br />
        <asp:Label ID="Label2" runat="server" Text="Select a Program: "></asp:Label>
        <asp:DropDownList ID="ProgramDropdownList" runat="server" DataSourceID="ProgramODS" DataTextField="ProgramName" DataValueField="ProgramID">
            <asp:ListItem Value="-3">[Select Program]</asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton ID="ProgramButton" runat="server">Next</asp:LinkButton>    
    
        <asp:GridView ID="EquivalenciesGrid" runat="server" AutoGenerateColumns="False" DataSourceID="EquivalencyGridODS">
            <Columns>
                <asp:BoundField DataField="CourseEquivalencyID" HeaderText="CourseEquivalencyID" SortExpression="CourseEquivalencyID" />
                <asp:BoundField DataField="ProgramID" HeaderText="ProgramID" SortExpression="ProgramID" />
                <asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" />
                <asp:BoundField DataField="DestinationCourseID" HeaderText="DestinationCourseID" SortExpression="DestinationCourseID" />
            </Columns>
            <EmptyDataTemplate>
                No Existing Equivalencies.
                <asp:Button ID="AddNew" runat="server" Text="Add An Equivalency" OnClick="AddNew_Click"/>
            </EmptyDataTemplate>
        </asp:GridView>    
    </div>
          
    <div runat="server" id="addNewEquivalency" visible="false">
        <asp:Label ID="EmptyCurrent" runat="server" Text="Current Program Course ID: "></asp:Label>
        <asp:TextBox ID="EmptyCurrentTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="CurrentCourseName" runat="server"></asp:Label>
        <asp:Label ID="CurrentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="EmptyEquivalent" runat="server" Text="Equivalent Course ID: "></asp:Label>
        <asp:TextBox ID="EmptyEquivalentTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="EquivalentCourseName" runat="server"></asp:Label>
        <asp:Label ID="EquivalentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Button ID="CheckIDs" runat="server" Text="Check Equivalency" OnClick="CheckIDs_Click" />
        <asp:Button ID="Enter" runat="server" Text="Enter Equivalency" OnClick="Enter_Click" />
    </div>

    <asp:ObjectDataSource ID="CategoryODS" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropdownList" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="EquivalencyGridODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEquivalencies" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
</asp:Content>

