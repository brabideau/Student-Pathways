<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManagePreferenceQuestion.aspx.cs" Inherits="Admin_ManagePreferenceQuestion" maintainScrollPositionOnPostBack="true" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate> 
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
               
            <div runat="server" align="center">
                <h4>Manage Preference Questions</h4>
        
                <asp:ListView ID="QuestionList" runat="server" DataSourceID="ODSQuestions" InsertItemPosition="LastItem" DataKeyNames="QuestionID">
                    <AlternatingItemTemplate>
                        <tr >
                            <td>
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
                            </td>
                            <td>
                                Do you want <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tbody>
                                        <tr hidden="hidden">
                                            <th>QuestionID: </th>
                                            <td>
                                                <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Bind("QuestionID") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Question Description: </th>
                                            <td>
                                                Do you want <asp:TextBox ID="QuestionDescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Active:
                                            </th>
                                            <td>
                                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
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
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="admin_button2" />
                                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="admin_button2" />
                            </td>
                            <td>
                                Do you want <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />       
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button" />
                            </td>
                            <td>
                                Do you want <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server">
                                        <tr runat="server">
                                            <th runat="server"></th>
                                            <th runat="server">Description</th>
                                            <th runat="server">Active</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr>  
                            <td>
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button" />
                            </td>
                            <td>
                                Do you want <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />        
                            </td>
                            <td>
                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false"/>
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ObjectDataSource ID="ODSQuestions" runat="server" 
                          DataObjectTypeName="CrystalBallSystem.DAL.Entities.PreferenceQuestion" 
                          DeleteMethod="PreferenceQuestion_Delete" 
                          InsertMethod="AddPreferenceQuestion" 
                          OldValuesParameterFormatString="original_{0}" 
                          SelectMethod="Question_List" 
                          TypeName ="CrystalBallSystem.BLL.AdminController" 
                          UpdateMethod="UpdatePreferenceQuestion" 
                          OnDeleted="CheckForException" 
                          OnInserted="CheckForException" 
                          OnUpdated="CheckForException">
        <DeleteParameters>
            <asp:Parameter Name="questionId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

