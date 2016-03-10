<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageNaitCourses.aspx.cs" Inherits="Admin_ManageNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

        <h1>Manage Nait Courses</h1>

        <div class="search-bar col-9 clearfix">
            <div class="col-8">
                <asp:Label ID="Label1" runat="server" Text="Select a Category: "></asp:Label>
                        <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                      DataSourceID="ODSCategory" 
                                      DataTextField="CategoryDescription" 
                                      DataValueField="CategoryID"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">[Select Category]</asp:ListItem>
                        </asp:DropDownList>
            </div>
            <div class="col-2">
                        <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click" CssClass="button submit">Search</asp:LinkButton>
             </div>                    
        </div>

        <div class="col-4 center">
                <h4>Programs</h4>

                    <asp:ListView ID="ProgramList" runat="server" DataSourceID="ODSProgramByCategory" 
                                                                  OnSelectedIndexChanging="ProgramList_SelectedIndexChanging"
                                                                  DataKeyNames="ProgramID"
                                                                  
                                                                  >
                        
                        <EmptyDataTemplate>
                            <table runat="server">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' visible="false"/>
                                </td>
                                <td>
                                        <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">
                                            <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                                        </asp:LinkButton></td></tr></ItemTemplate><LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="width:350px">
                                            <tr runat="server" style="" hidden="hidden">
                                                <th runat="server">ProgramID</th>
                                                <th runat="server">ProgramName</th>
                                            </tr><tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </td>
                                </tr>
                                
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr>
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

        <div class="col-8 center">
            <h4>Courses</h4>       
        <div>
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
                            <tr>
                                <td><asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/></td>
                                <td><asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td><asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td align="center"><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>
                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr>
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
                                                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="admin_button"/>
                                                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="admin_button"/>
                                                    
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
                                        <asp:LinkButton ID="NewButton2" runat="server" Text="Add New" OnClick="NewButton2_Click" CssClass="admin_button2"></asp:LinkButton>

                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr>
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
                                                    <asp:LinkButton ID="InsertButton" CommandName="Insert" runat="server" Text="Save" CssClass="admin_button" >
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="CancelButton" CommandName="Cancel" runat="server" Text="Cancel" CssClass="admin_button">
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>                                                                                                                                                                                    
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button" /></td>
                                <td>
                                    <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' /></td>
                                <td>
                                    <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' /></td>
                                <td ><asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' /></td>

                                <td><asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" /></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server">
                                            <tr runat="server" class="listview-heading align-fix">
                                                <th></th>
                                                <th runat="server">Course Code</th>
                                                <th runat="server">Course Name</th>
                                                <th runat="server">Course Credits</th>
                                                <th runat="server">Active</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <asp:LinkButton ID="NewButton" runat="server" Text="Add New" OnClick="NewButton_Click" CssClass="admin_button2"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server"> 
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
                            <tr> 
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

</asp:Content>

