<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageNaitCourses.aspx.cs" Inherits="Admin_ManageNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
        <h1>Manage Nait Courses</h1>
        <table style="width: 70%">
            <tr align="center">
                <td style="width: 863px">
                    <asp:Label ID="Label1" runat="server" Text="Select a category: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                      DataSourceID="ODSCategory" 
                                      DataTextField="CategoryDescription" 
                                      DataValueField="CategoryID"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">[Select Category]</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:ListView ID="NaitCoursesListView" runat="server"></asp:ListView>
        <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSNaitCourses" runat="server"></asp:ObjectDataSource>
    </div>
</asp:Content>

