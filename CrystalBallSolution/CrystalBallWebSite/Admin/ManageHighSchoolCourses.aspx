<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageHighSchoolCourses.aspx.cs" Inherits="Admin_ManageHighSchoolCourses" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server">
        <h1>Manage High School Courses</h1>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ListView ID="HighSchoolCoursesList" runat="server" DataSourceID="ODSHighSchoolCourses" InsertItemPosition="LastItem">

        <EditItemTemplate>
            <tr style="">
                <td colspan="2">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    High School CourseID:
                                </th>
                                <td>
                                    <asp:Label ID="HighSchoolCourseIDLabel" runat="server" Text='<%# Bind("HighSchoolCourseID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    High School Course Name:
                                </th>
                                <td>
                                    <asp:TextBox ID="HighSchoolCourseNameTextBox" runat="server" Text='<%# Bind("HighSchoolCourseName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Gourse Group:
                                </th>
                                <td>
                                    <asp:TextBox ID="CourseGroupLabel" runat="server" Text='<%# Bind("CourseGroup") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Highest:
                                </th>
                                <td>
                                    <asp:CheckBox ID="HighestCheckBox" runat ="server" Checked='<%# Bind("Highest") %>' />
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
            <tr>
                <td>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="admin_button2"/>
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="admin_button2"/>
                </td>
                <td>
                    <asp:TextBox ID="HighSchoolCourseNameTextBox" runat="server" Text='<%# Bind("HighSchoolCourseName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="CourseGroupLabel" runat="server" Text='<%# Bind("CourseGroup") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="HighestCheckBox" runat ="server" Checked='<%# Bind("Highest") %>' />
                </td>
            </tr>
        </InsertItemTemplate>


        <ItemTemplate>
            <tr>
                <td>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
                </td>
                <td>
                    <asp:Label ID="HighSchoolCourseNameLabel" runat="server" Text='<%# Eval("HighSchoolCourseName") %>' />
                </td>
                <td>
                    <asp:Label ID="CourseGroupLabel" runat="server" Text='<%# Eval("CourseGroup") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="HighestCheckBox" runat ="server" Checked='<%# Eval("Highest") %>' Enabled="false"/>
                </td>
            </tr>
        </ItemTemplate>


        <LayoutTemplate>
            <table runat="server" class="center">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server">
                            <tr runat="server">
                                <th runat="server"></th>
                                <th runat="server">High School Course Name</th>
                                <th runat="server">Course Group</th>
                                <th runat="server">Highest</th>
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
        </div>
    <asp:ObjectDataSource ID="ODSHighSchoolCourses" runat="server" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.HighSchoolCours" InsertMethod="AddHighSchoolCourse" OldValuesParameterFormatString="original_{0}" UpdateMethod="HighSchoolCourse_Update" OnInserted="CheckForException" OnUpdated="CheckForException"></asp:ObjectDataSource>
</asp:Content>

