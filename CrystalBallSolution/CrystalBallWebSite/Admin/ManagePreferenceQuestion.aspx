<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManagePreferenceQuestion.aspx.cs" Inherits="Admin_ManagePreferenceQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
    <h1>Manage Preference Question</h1>
    <asp:ListView ID="QuestionList" runat="server" DataSourceID="ODSQuestions" InsertItemPosition="LastItem" DataKeyNames="QuestionID">
        <AlternatingItemTemplate>
            <tr >
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" CssClass="admin_button"/>
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr>
                <td colspan="2">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    QuestionID:
                                </th>
                                <td>
                                    <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Bind("QuestionID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Question Description:
                                </th>
                                <td>
                                    <asp:TextBox ID="QuestionDescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
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
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" CssClass="admin_button" />
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button" />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
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
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr runat="server" align="center">
                    <td runat="server">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>--%>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr>  
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" CssClass="admin_button" />
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button" />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
</div>
    <asp:ObjectDataSource ID="ODSQuestions" runat="server" 
                          DataObjectTypeName="CrystalBallSystem.DAL.Entities.PreferenceQuestion" 
                          DeleteMethod="PreferenceQuestion_Delete" 
                          InsertMethod="AddPreferenceQuestion" 
                          OldValuesParameterFormatString="original_{0}" 
                          SelectMethod="Question_List" 
                          TypeName ="CrystalBallSystem.BLL.AdminController" 
                          UpdateMethod="UpdatePreferenceQuestion">
        <DeleteParameters>
            <asp:Parameter Name="questionId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>

