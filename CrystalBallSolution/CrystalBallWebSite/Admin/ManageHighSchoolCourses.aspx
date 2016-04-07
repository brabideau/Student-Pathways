<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageHighSchoolCourses.aspx.cs" Inherits="Admin_ManageHighSchoolCourses" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server">
        <h1>Manage High School Courses</h1>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ListView ID="HighSchoolCoursesList" 
        runat="server" 
        InsertItemPosition="LastItem"
        ItemType="CrystalBallSystem.DAL.POCOs.GetHighSchoolCourses"
        OnItemCanceling="HighSchoolCoursesList_ItemCanceling" 
        OnItemEditing="HighSchoolCoursesList_ItemEditing" 
        OnItemUpdating="HighSchoolCoursesList_ItemUpdating" 
        OnItemInserting="HighSchoolCoursesList_ItemInserting" 
        OnPagePropertiesChanging="HighSchoolCoursesList_PagePropertiesChanging">

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
                            <tr>t
                                <th>
                                    Course Group:
                                </th>
                                <td>
                                    <asp:DropDownList ID="DDL_CourseGroup_Edit" runat="server" 
                                                                                DataSourceID="ODSCourseGroup" 
                                                                                DataValueField="CourseGroupID" 
                                                                                DataTextField="CourseGroupDescription"
                                                                                AppendDataBoundItems="true" 
                                                                                >
                                        <asp:ListItem Value="0">Select Course Group</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    CourseLevel:
                                </th>
                                <td>
                                    <asp:DropDownList ID="DDL_CourseLevel_Edit" runat="server" SelectedValue='<%# Item.CourseLevel %>'>
                                        <asp:ListItem Value="0">Select Course Level</asp:ListItem>
                                        <asp:ListItem Value ="1">Level 10</asp:ListItem>
                                        <asp:ListItem Value ="2">Level 20</asp:ListItem>
                                        <asp:ListItem Value ="3">Level 30</asp:ListItem>
                                    </asp:DropDownList>
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
                    <asp:DropDownList ID="DDL_CourseGroup_Insert" 
                                      runat="server" 
                                      DataSourceID="ODSCourseGroup"
                                      DataValueField="CourseGroupID" 
                                      DataTextField="CourseGroupDescription"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select Course Group</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                     <asp:DropDownList ID="DDL_CourseLevel_Insert" runat="server">
                            <asp:ListItem Value="0">Select Course Level</asp:ListItem>
                            <asp:ListItem Value ="1">Level 10</asp:ListItem>
                            <asp:ListItem Value ="2">Level 20</asp:ListItem>
                            <asp:ListItem Value ="3">Level 30</asp:ListItem>
                     </asp:DropDownList>
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
                    <asp:Label ID="CourseGroupIDLabel" runat="server" Text='<%# Eval("CourseGroup") %>' />
                </td>
                <td>
                    <asp:Label ID="CourseLevelLabel" runat ="server" Text='<%# Eval("CourseLevel") %>' />
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
                                <th runat="server">Course Level</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" >
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="true" ShowPreviousPageButton="False" />
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
    <%--<asp:ObjectDataSource ID="ODSHighSchoolCourses" runat="server" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.HighSchoolCours" InsertMethod="AddHighSchoolCourse" OldValuesParameterFormatString="original_{0}" UpdateMethod="HighSchoolCourse_Update" OnInserted="CheckForException" OnUpdated="CheckForException"></asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="ODSCourseGroup" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CourseGroup_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

