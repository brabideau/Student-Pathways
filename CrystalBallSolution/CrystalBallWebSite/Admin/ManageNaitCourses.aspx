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
        
        
    </style>
    <div runat="server">
        <h1 align="center">Manage Nait Courses</h1>

        <div class="search-bar">
            <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                      DataSourceID="ODSCategory" 
                                      DataTextField="CategoryDescription" 
                                      DataValueField="CategoryID"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">[Select Category]</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click">Search</asp:LinkButton>
        </div>


        <div class="programs-column">
                <h4>Programs</h4>
            <div class="col-md-4">
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
                            <tr style="width:100%">
                                <td style="width:10%">
                                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' visible="false"/>
                                </td>
                                <td style="width:90%">
                                        <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">
                                            <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                                        </asp:LinkButton></td></tr></ItemTemplate><LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="width:350px">
                                            <tr runat="server" style="" hidden="hidden">
                                                <th runat="server">ProgramID</th><th runat="server">ProgramName</th></tr><tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </td>
                                </tr>
                                
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' visible="false"/>
                                </td>
                                <td>
                                    <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">
                                            <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
            </div>
        </div>

        <div class="courses-column">
            <h4>Courses</h4>


        



            
        <div class="col-md-8" align="center">
                    <asp:ListView ID="NaitCoursesListViewByProgram" runat="server" 
                                  ItemType="CrystalBallSystem.DAL.Entities.NaitCours" 
                                  InsertItemPosition="LastItem" 
                                  OnItemEditing="NaitCoursesListViewByProgram_ItemEditing" 
                                  OnItemCanceling="NaitCoursesListViewByProgram_ItemCanceling" 
                                  OnItemUpdating="NaitCoursesListViewByProgram_ItemUpdating" 
                                  DataKeyNames="CourseID"                                
                                  OnSelectedIndexChanging="NaitCoursesListViewByProgram_SelectedIndexChanging" 
                                  OnPagePropertiesChanging ="NaitCoursesListViewByProgram_PagePropertiesChanging" 
                                  OnItemInserting="NaitCoursesListViewByProgram_ItemInserting">
                        <AlternatingItemTemplate>
                            <tr> <%--style="background-color:#efefef; color: #284775;"--%>
                                <td><asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" /></td>
                                <td><asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td><asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td align="center"><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td colspan="6">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <th>
                                                    CourseID: </th><td>
                                                    <asp:Label ID="CourseIDTextBox" runat="server" Text='<%# Bind("CourseID") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Code: </th><td>
                                                    <asp:TextBox ID="CourseCodeTextBox" runat="server" Text='<%# Bind("CourseCode") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Name: </th><td>
                                                    <asp:TextBox ID="CourseNameTextBox" runat="server" Text='<%# Bind("CourseName") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Credits: </th><td>
                                                    <asp:TextBox ID="CourseCreditsTextBox" runat="server" Text='<%# Bind("CourseCredits") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Active: </th><td>
                                                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <td>
                                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                           </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td></tr><tr runat="server">
                                    <td runat="server" style="text-align:left">
                                        <asp:LinkButton ID="NewButton2" runat="server" Text="Add New" OnClick="NewButton2_Click"></asp:LinkButton></td></tr></table></EmptyDataTemplate><InsertItemTemplate>
                            <tr style="">
                                <td colspan="6">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <th>
                                                    CourseID: </th><td>
                                                    <asp:Label ID="CourseIDTextBox" runat="server" Text='<%# Bind("CourseID") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Code: </th><td>
                                                    <asp:TextBox ID="CourseCodeTextBox" runat="server" Text='<%# Bind("CourseCode") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Name: </th><td>
                                                    <asp:TextBox ID="CourseNameTextBox" runat="server" Text='<%# Bind("CourseName") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Course Credits: </th><td>
                                                    <asp:TextBox ID="CourseCreditsTextBox" runat="server" Text='<%# Bind("CourseCredits") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Active: </th><td>
                                                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <td>
                                                    <asp:LinkButton ID="InsertButton" CommandName="Insert" runat="server" Text="Save" >
                                                    </asp:LinkButton><asp:LinkButton ID="CancelButton" CommandName="Cancel" runat="server" Text="Cancel">
                                                    </asp:LinkButton></td></tr></tbody></table></td></tr></InsertItemTemplate><ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" /></td>
                                <td>
                                    <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td align="center"><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>

                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" class="table align-fix">
                                            <tr runat="server" class="listview-heading align-fix">
                                                <th></th>
                                                <th runat="server">Course Code</th><th runat="server">Course Name</th><th runat="server">Course Credits</th><th runat="server">Active</th></tr><tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server"> <%--style="text-align:left"--%>
                                        <asp:LinkButton ID="NewButton" runat="server" Text="Add New" OnClick="NewButton_Click"></asp:LinkButton></td></tr><tr runat="server">
                                    <td runat="server"> <%--style="font-family: Verdana, Arial, Helvetica, sans-serif;color: #00ccff; text-align:center"--%>
                                        <asp:DataPager ID="DataPager1" runat="server" PageSize="10">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="False" ShowNextPageButton="False" ShowPreviousPageButton="true" />
                                                <asp:NumericPagerField  ButtonType="Link"/>
                                                <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr>  <%--style="background-color:#E2DED6; font-weight: bold;color: #333333;"--%>
                                <td>
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" /></td>
                                <td>
                                    <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
            </div>
            </div>
    
        <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSNaitCourses" runat="server" SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}" DataObjectTypeName="CrystalBallSystem.DAL.Entities.NaitCours" InsertMethod="AddCourse" UpdateMethod="UpdateNaitCourse">
            <SelectParameters>
                <asp:ControlParameter ControlID="ProgramList" DefaultValue="" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSProgramByCategory" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropdownList" DefaultValue="0" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

