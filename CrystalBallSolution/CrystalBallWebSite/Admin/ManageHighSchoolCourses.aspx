<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageHighSchoolCourses.aspx.cs" Inherits="Admin_ManageHighSchoolCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
        <h1>Manage HighSchool Courses</h1>
    <asp:ListView ID="HighSchoolCoursesList" runat="server" DataSourceID="ODSHighSchoolCourses" InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr <%--style="background-color:#efefef; color: #284775;" --%> align="center">
                <td>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td <%--style="align-content:center"--%>>
                    <asp:Label ID="HighSchoolCourseNameLabel" runat="server" Text='<%# Eval("HighSchoolCourseName") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="HighSchoolCourseNameTextBox" runat="server" Text='<%# Bind("HighSchoolCourseName") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="HighSchoolCourseNameTextBox" runat="server" Text='<%# Bind("HighSchoolCourseName") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="" align="center">
                <td>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="HighSchoolCourseNameLabel" runat="server" Text='<%# Eval("HighSchoolCourseName") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" class="table align-fix">
                            <tr runat="server" class="listview-heading align-fix">
                                <th runat="server"></th>
                                <th runat="server">High School Course Name</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" ><%--style="text-align: center;background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;"--%>
                        <asp:DataPager ID="DataPager1" runat="server" align="center">
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
            <tr ><%--style="background-color:#E2DED6; font-weight: bold;color: #333333;"--%>
                <td>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="HighSchoolCourseNameLabel" runat="server" Text='<%# Eval("HighSchoolCourseName") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
        </div>
    <asp:ObjectDataSource ID="ODSHighSchoolCourses" runat="server" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.HighSchoolCours" InsertMethod="AddHighSchoolCourse" OldValuesParameterFormatString="original_{0}" UpdateMethod="HighSchoolCourse_Update"></asp:ObjectDataSource>
</asp:Content>

