<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageCategory.aspx.cs" Inherits="Admin_ManageCategory" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Manage Category</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ListView ID="CategoryList" runat="server" DataSourceID="ODSCategoryList" 
                                                   InsertItemPosition="LastItem" 
                                                   OnItemUpdating="CategoryList_ItemUpdating">
        <EditItemTemplate>
            <tr>
                <td colspan="2">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    CategroyID:
                                </th>
                                <td>
                                    <asp:Label ID="CategoryIDLabel" runat="server" Text='<%# Bind("CategoryID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Categroy Description:
                                </th>
                                <td>
                                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="admin_button"/>
                                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="admin_button"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="admin_button2"/>
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="admin_button2"/>
                </td>
                <td>
                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                </td>
            </tr>
        </InsertItemTemplate>


        <ItemTemplate>
            <tr>
                <td>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
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

    </asp:ListView>

    <asp:ObjectDataSource ID="ODSCategoryList" runat="server" SelectMethod="Category_List"
         TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.Category"
         InsertMethod="AddCategory" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateCategory"
         OnInserted="CheckForException" OnUpdated="CheckForException"></asp:ObjectDataSource>
</asp:Content>

