<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProgramSearch.aspx.cs" Inherits="Briand_Workspace_ProgramSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Search for a Program</h1>
    <asp:DropDownList ID="DL_Category" runat="server" 
                                          DataSourceID="ListCategories" 
                                          DataTextField="CategoryDescription" 
                                          DataValueField="CategoryID"
                                          AppendDataBoundItems="true"
                                          autopostback="true"
                                          OnSelectedIndexChanged="Populate_Program"                                       
        >
                            <asp:ListItem Value="0">[Select Category]</asp:ListItem>
     
    </asp:DropDownList>

    <asp:TextBox ID="SearchTextBox" runat="server" AutoPostBack="true"></asp:TextBox>
    
    <asp:Button ID="Search" runat="server" Text="Search" SelectMethod="Populate_Program" TypeName="CrystalBallSystem.BLL.AdminController"/>

    <asp:GridView ID="ProgramGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ListPrograms">
        <Columns>
            <asp:ButtonField Text="Edit"></asp:ButtonField>

            <asp:BoundField DataField="CredentialTypeID" HeaderText="CredentialTypeID" SortExpression="CredentialTypeID"></asp:BoundField>
            <asp:BoundField DataField="ProgramName" HeaderText="ProgramName" SortExpression="ProgramName"></asp:BoundField>
            <asp:BoundField DataField="ProgramDescription" HeaderText="ProgramDescription" SortExpression="ProgramDescription"></asp:BoundField>
            <asp:BoundField DataField="TotalCredits" HeaderText="TotalCredits" SortExpression="TotalCredits"></asp:BoundField>
            <asp:BoundField DataField="ProgramLength" HeaderText="ProgramLength" SortExpression="ProgramLength"></asp:BoundField>
            <asp:BoundField DataField="CompetitiveAdvantage" HeaderText="CompetitiveAdvantage" SortExpression="CompetitiveAdvantage"></asp:BoundField>
            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active"></asp:CheckBoxField>
            <asp:BoundField DataField="ProgramLink" HeaderText="ProgramLink" SortExpression="ProgramLink"></asp:BoundField>


        </Columns>
    </asp:GridView>


    <asp:ObjectDataSource ID="ListPrograms" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Program_Search" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:FormParameter FormField="SearchTextBox" Name="searchTerm" Type="String"></asp:FormParameter>
            <asp:FormParameter FormField="DL_Category" Name="catID" Type="Int32"></asp:FormParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ListCategories" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

</asp:Content>


