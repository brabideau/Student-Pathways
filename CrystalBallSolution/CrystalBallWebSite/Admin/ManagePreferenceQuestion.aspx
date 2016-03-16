<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManagePreferenceQuestion.aspx.cs" Inherits="Admin_ManagePreferenceQuestion" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
    <h4 class="table_header">Manage Preference Questions</h4>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
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
                                    QuestionID: </th><td>
                                    <asp:Label ID="QuestionIDLabel" runat="server" Text='<%# Bind("QuestionID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Question Description: </th><td>
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
                    <td>No data was returned.</td></tr></table></EmptyDataTemplate><InsertItemTemplate>
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
                                <th runat="server">Description</th></tr><tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
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

    <div style="width:100%;text-align:center;">
        <h4 class="table_header">Manage Questions for Each Program</h4>
        <div class="search">

            <asp:Label ID="Label1" runat="server" Text="Select a category: "></asp:Label>
            <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                              DataSourceID="ODSCategory" 
                                              DataTextField="CategoryDescription" 
                                              DataValueField="CategoryID"
                                              AppendDataBoundItems="true">
                                <asp:ListItem Value="0">[Select Category]</asp:ListItem>
            </asp:DropDownList>
               
            <asp:LinkButton ID="SearchButton" runat="server" CssClass="button2" OnClick="SearchButton_Click">Search</asp:LinkButton>
        </div>  
    </div>  

    <div style="clear:both;width:100%;margin-top:15px;">  
    <div style="float:left;width:40%">
        <h4 class="table_header">Programs</h4>

                    <asp:ListView ID="ProgramList" runat="server" DataSourceID="ODSProgramByCategory" 
                                                                  OnSelectedIndexChanging="ProgramList_SelectedIndexChanging"
                                                                  DataKeyNames="ProgramID" 
                                                                  style="margin-right: 45px"                      
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
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="width:100%">
                                            <tr runat="server" style="" hidden="hidden">
                                                <th runat="server">ProgramID</th><th runat="server">ProgramName</th></tr><tr id="itemPlaceholder" runat="server"></tr>
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
                                    </asp:LinkButton></td></tr></SelectedItemTemplate></asp:ListView></div><div style="width:55%;float:right">
        <h4 class="table_header">Questions</h4><asp:ListView ID="QuestionListView" runat="server" DataKeyNames="QuestionID" 
                                                               OnItemUpdating="QuestionListView_ItemUpdating" 
                                                               OnItemEditing="QuestionListView_ItemEditing" 
                                                               OnItemCanceling="QuestionListView_ItemCanceling"
                                                               InsertItemPosition="LastItem" OnItemInserting="QuestionListView_ItemInserting"><AlternatingItemTemplate>
                <tr style="">
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
                    </td>
                    <td>
                        <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                    </td>
                    <td>
                        <asp:RadioButtonList ID="AnswerRadioButtons" runat="server" SelectedValue='<%# Eval("Answer") %>' Enabled="false" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></td></tr></AlternatingItemTemplate><EditItemTemplate>
                <tr style="">
                    <td colspan="3">
                        <table>
                            <tbody>
                                <tr>
                                    <th>QuestionID:</th><td>
                                        <asp:Label ID="QuestionIDTextLabel" runat="server" Text='<%# Bind("QuestionID") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Question:</th><td>
                                        <asp:Label ID="QuestionTextLabel" runat="server" Text='<%# Bind("Question") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Answer:</th><td>
                                        <asp:RadioButtonList ID="AnswerRadioButtons" runat="server" SelectedValue='<%# Eval("Answer") %>' RepeatDirection="Horizontal">
                                            <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></td></tr><tr>
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
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td></tr></table></EmptyDataTemplate><InsertItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="admin_button2" />
                        <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="admin_button2" />
                    </td>
                    <td>
                        <asp:DropDownList ID="QuestionDropDownList" runat="server" AppendDataBoundItems="true" DataSourceID="ODSQuestions" DataTextField="Description" DataValueField="QuestionID">
                            <asp:ListItem Value="0">[Select Question]</asp:ListItem></asp:DropDownList></td><td>
                        <asp:RadioButtonList ID="AnswerRadioButtons" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False" Selected="true">No</asp:ListItem></asp:RadioButtonList></td></tr></InsertItemTemplate><ItemTemplate>
                <tr style="">
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="admin_button"/>
                    </td>
                    <td>
                        <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                    </td>
                    <td>
                        <%--<asp:CheckBox ID="AnswerCheckBox" runat="server" Text='<%# Eval("Answer") %>' Enabled="false"/>--%>
                        <asp:RadioButtonList ID="AnswerRadioButtons" runat="server" SelectedValue='<%# Eval("Answer") %>' Enabled="false" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">Yes</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></td></tr></ItemTemplate><LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">
                                    <th></th>
                                    <th runat="server">Question</th><th runat="server">Answer</th></tr><tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="">
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
</div>
    
</div>
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
    <asp:ObjectDataSource ID="ODSProgramByCategory" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropdownList" DefaultValue="0" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
    </asp:ObjectDataSource>
    </asp:Content>

