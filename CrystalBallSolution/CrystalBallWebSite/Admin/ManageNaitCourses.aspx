<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageNaitCourses.aspx.cs" Inherits="Admin_ManageNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        tr.myItem td{
            width:200px;
            height:20px;
            border:2px solid;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 530px;
        }
        .auto-style3 {
            width: 415px;
        }
        .auto-style4 {
            height: 400px;
            width: 415px;
        }
    </style>
    <div runat="server">
        <h1 align="center">Manage Nait Courses</h1>
        <table class="auto-style1">
            <tr>
                <td class="auto-style3" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Select a category: "></asp:Label>
                </td>
                <td class="auto-style2" colspan="2">
                    <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                      DataSourceID="ODSCategory" 
                                      DataTextField="CategoryDescription" 
                                      DataValueField="CategoryID"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">[Select Category]</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click">Search</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center" style="background-color:Highlight"><h4>Program</h4></td>
                <td align="center" style="background-color:Highlight"><h4>Courses</h4></td>
            </tr>
            <tr>
                <td class="auto-style4" align="center">
                    <asp:ListView ID="ProgramList" runat="server" DataSourceID="ODSProgramByCategory" 
                                                                  OnSelectedIndexChanging="ProgramList_SelectedIndexChanging"
                                                                  DataKeyNames="ProgramID"
                                                                  >
                        
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td align="center">No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="height:30px;">
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
                                            <tr runat="server" style="" hidden="hidden">
                                                <th runat="server">ProgramID</th><th runat="server">ProgramName</th></tr><tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
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
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </td>
                <td colspan="2" align="center">
                    <asp:ListView ID="NaitCoursesListViewByProgram" runat="server" DataSourceID="ODSNaitCourses"><AlternatingItemTemplate>
                            <tr style="background-color:#efefef; color: #284775;">
                                <td><asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' /></td>
                                <td><asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td><asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="CourseIDTextBox" runat="server" Text='<%# Bind("CourseID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CourseCodeTextBox" runat="server" Text='<%# Bind("CourseCode") %>' /></td>
                                <td>
                                    <asp:TextBox ID="CourseNameTextBox" runat="server" Text='<%# Bind("CourseName") %>' /></td>
                                <td>
                                    <asp:TextBox ID="CourseCreditsTextBox" runat="server" Text='<%# Bind("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' /></td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate><InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="CourseIDTextBox" runat="server" Text='<%# Bind("CourseID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CourseCodeTextBox" runat="server" Text='<%# Bind("CourseCode") %>' /></td>
                                <td>
                                    <asp:TextBox ID="CourseNameTextBox" runat="server" Text='<%# Bind("CourseName") %>' /></td>
                                <td>
                                    <asp:TextBox ID="CourseCreditsTextBox" runat="server" Text='<%# Bind("CourseCredits") %>' /></td>
                                <td>
                                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' /></td>
                           </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr class=".myItem">
                                <td>
                                    <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>

                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" class="table align-fix">
                                            <tr runat="server" style="">
                                                <th runat="server">CourseID</th><th runat="server">Course Code</th><th runat="server">Course Name</th><th runat="server">Course Credits</th><th runat="server">Active</th></tr><tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center;background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;">
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
                            <tr style="">
                                <td>
                                    <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSNaitCourses" runat="server" SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}"><SelectParameters><asp:ControlParameter ControlID="ProgramList" DefaultValue="" Name="programID" PropertyName="SelectedValue" Type="Int32" /></SelectParameters></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSProgramByCategory" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropdownList" DefaultValue="0" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

