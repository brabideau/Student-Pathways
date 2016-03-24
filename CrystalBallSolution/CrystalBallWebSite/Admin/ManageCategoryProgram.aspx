<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageCategoryProgram.aspx.cs" Inherits="Admin_ManageCategoryProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Find your Program: "></asp:Label>
    <asp:TextBox ID="InputTextbox" runat="server"></asp:TextBox>
    in
    <asp:DropDownList ID="CategoryDropDowList" runat="server" DataSourceID="ODSCategories" DataTextField="CategoryDescription" DataValueField="CategoryID" AppendDataBoundItems="true">
        <asp:ListItem Value="0">[All Categories]</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
    <asp:ListView ID="ProgramListView" runat="server" OnSelectedIndexChanging="ProgramListView_SelectedIndexChanging" DataKeyNames="ProgramID">       
        
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">
                                            <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </asp:LinkButton></td></tr></ItemTemplate><LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">ProgramID</th><th runat="server">ProgramName</th></tr><tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style=""></td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </asp:LinkButton></td></tr></SelectedItemTemplate></asp:ListView><asp:ListView ID="categoryListview" runat="server">
        <AlternatingItemTemplate>
            <tr style="">
                <td><asp:Label ID="CategoryIDLabel" runat="server" Text='<%# Eval("CategoryID") %>' /></td>
                <td><asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Eval("CategoryDescription") %>' /></td>             
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />

                </td>
                <td>
                    <asp:TextBox ID="CategoryIDTextBox" runat="server" Text='<%# Bind("CategoryID") %>' />

                </td>
                <td>
                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td></tr></table></EmptyDataTemplate><InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="CategoryIDTextBox" runat="server" Text='<%# Bind("CategoryID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="CategoryDescriptionTextBox" runat="server" Text='<%# Bind("CategoryDescription") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="CategoryIDLabel" runat="server" Text='<%# Eval("CategoryID") %>' />
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
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">CategoryID</th><th runat="server">CategoryDescription</th></tr><tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style=""></td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="ODSCategories" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSFindProgram" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="findProgram" TypeName="CrystalBallSystem.BLL.AdminController"><SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropDowList" Name="searchTerm" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter Name="catID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<asp:ObjectDataSource ID="ODSGetCategories" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCategoryByProgram" TypeName="CrystalBallSystem.BLL.AdminController"><SelectParameters><asp:ControlParameter ControlID="ProgramListView" Name="programid" PropertyName="SelectedValue" Type="Int32" /></SelectParameters></asp:ObjectDataSource></asp:Content>

