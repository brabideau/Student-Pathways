<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageCategory.aspx.cs" Inherits="Admin_ManageCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Manage Category</h1>
    <asp:ListView ID="CategoryList" runat="server" DataSourceID="ODSCategoryList" InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="listview-buttons" />
                </td>
                <td>
                    <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Eval("CategoryDescription") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="listview-buttons"/>
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="listview-buttons"/>
                </td>
             
                <td>
                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="listview-buttons"/>
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="listview-buttons"/>
                </td>
                <td>
                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="listview-buttons"/>
                </td>
                <td>
                    <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Eval("CategoryDescription") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" >
                            <tr runat="server">
                                <th runat="server"></th>
                                <th runat="server">Category Name</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" >
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="listview-buttons"/>
                </td>
                <td>
                    <asp:Label ID="CategoryIDLabel" runat="server" Text='<%# Eval("CategoryID") %>' />
                </td>
                <td>
                    <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Eval("CategoryDescription") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>

    <asp:ObjectDataSource ID="ODSCategoryList" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.Category" InsertMethod="AddCategory" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateCategory"></asp:ObjectDataSource>
</asp:Content>

