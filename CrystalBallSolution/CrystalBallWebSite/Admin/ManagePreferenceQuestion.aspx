<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManagePreferenceQuestion.aspx.cs" Inherits="Admin_ManagePreferenceQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
    <h1>Manage Preference Question</h1>
    <asp:ListView ID="QuestionList" runat="server" DataSourceID="ODSQuestions" InsertItemPosition="LastItem" DataKeyNames="QuestionID">
        <AlternatingItemTemplate>
            <tr <%--style="background-color:#efefef; color: #284775;"--%>>
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" />
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td align="center">
                    <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Eval("QuestionID") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
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
                    <asp:TextBox ID="QuestionIDTextBox" runat="server" Text='<%# Bind("QuestionID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
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
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="QuestionIDTextBox" runat="server" Text='<%# Bind("QuestionID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" />
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td align="center">
                    <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Eval("QuestionID") %>' />
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
                        <table id="itemPlaceholderContainer" runat="server" border="0" class="table align-fix">
                            <tr runat="server" class="listview-heading align-fix">
                                <th runat="server"></th>
                                <th runat="server">QuestionID</th>
                                <th runat="server">Description</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" align="center">  <%--style="text-align: center;background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;"--%>
                    <td runat="server">
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
            <tr>  <%--style="background-color:#E2DED6; font-weight: bold;color: #333333;"--%>
                <td>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Remove" />
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Eval("QuestionID") %>' />
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

