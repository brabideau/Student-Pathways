<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equivalencies.aspx.cs" Inherits="Admin_Equivalencies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Select a Category: "></asp:Label>
    <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                            DataSourceID="ODSCategoryList" 
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
    
    <asp:GridView ID="EquivalenciesGrid" runat="server"></asp:GridView>          

    <asp:ObjectDataSource ID="CategoryODS" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropdownList" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="EquivalencyGridODS" runat="server"></asp:ObjectDataSource>
</asp:Content>

