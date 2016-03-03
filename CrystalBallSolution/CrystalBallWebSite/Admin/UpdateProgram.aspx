<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpdateProgram.aspx.cs" Inherits="Admin_UpdateProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
    <h1>Manage Program</h1>
    <div>
        <table style="width: 70%; margin:5px">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="Select a Category: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropdownList" runat="server" 
                                      DataSourceID="ODSCategoryList" 
                                      DataTextField="CategoryDescription" 
                                      DataValueField="CategoryID"
                                      AppendDataBoundItems="true">
                        <asp:ListItem Value="0">[Select Category]</asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td>
                    <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click">Search</asp:LinkButton>
                </td>
            </tr>
        </table>

        <asp:ListView ID="ProgramListView" runat="server" 
                      align="center" 
                      DataKeyNames="ProgramID"
                      InsertItemPosition="LastItem"
                      ItemType="CrystalBallSystem.DAL.Entities.Program"
                      OnItemCanceling="ProgramListView_ItemCanceling" 
                      OnItemEditing="ProgramListView_ItemEditing" 
                      OnItemUpdating="ProgramListView_ItemUpdating" 
                      OnItemInserting="ProgramListView_ItemInserting">
            <AlternatingItemTemplate>
                <tr <%--style="background-color:#efefef; color: #284775; align-content:center"--%>>
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                    <td align="center">
                        <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="CredentialTypeIDLabel" runat="server" Text='<%# Eval("CredentialTypeID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="TotalCreditsLabel" runat="server" Text='<%# Eval("TotalCredits") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="ProgramLengthLabel" runat="server" Text='<%# Eval("ProgramLength") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="CompetitiveAdvantageLabel" runat="server" Text='<%# Eval("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLinkLabel" runat="server" Text='<%# Eval("ProgramLink") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style=" ">
                    <td colspan="10">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <th style="visibility:hidden">
                                                    ProgramID: 
                                                </th>
                                                <td>
                                                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Bind("ProgramID") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Credential Type: 

                                                </th>
                                                <td>
                                                    <%--<asp:TextBox ID="CredentialTypeIDTextBox" runat="server" Text='<%# Bind("CredentialTypeID") %>' />--%>
                                                    <asp:DropDownList ID="CredentialTypeDropdownList" runat="server" 
                                                          DataSourceID="ODSCredentialType" 
                                                          DataTextField="CredentialTypeName" 
                                                          DataValueField="CredentialTypeID"
                                                    />                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Program Name: 

                                                </th>
                                                <td>
                                                    <asp:TextBox ID="ProgramNameTextBox" runat="server" Text='<%# Bind("ProgramName") %>' />
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Entrance Requirement:
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="EntranceRequirementTextBox" runat="server" TextMode="multiline" Columns="20" Rows="3" Text='<%# Bind("ProgramDescription") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Total Credits:</th>
                                                <td>
                                                    <asp:TextBox ID="TotalCreditsTextBox" runat="server" Text='<%# Bind("TotalCredits") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Program Length:</th>
                                                <td>
                                                    <asp:TextBox ID="ProgramLengthTextBox" runat="server" Text='<%# Bind("ProgramLength") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Competive Advantage:</th>
                                                <td>
                                                    <asp:TextBox ID="CompetiveAdvantageTextBox" runat="server" Text='<%# Bind("CompetitiveAdvantage") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Active:</th>
                                                <td>
                                                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Text='<%# Bind("Active") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Program Link:</th>
                                                <td>
                                                    <asp:TextBox ID="ProgramLinkTextBox" runat="server" Text='<%# Bind("ProgramLink") %>' />
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
                                <td colspan="10">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <th>
                                                    ProgramID: 
                                                </th>
                                                <td>
                                                    <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Bind("ProgramID") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Credential Type: 

                                                </th>
                                                <td>
                                                    <%--<asp:TextBox ID="CredentialTypeIDTextBox" runat="server" Text='<%# Bind("CredentialTypeID") %>' />--%>
                                                    <asp:DropDownList ID="CredentialTypeDropdownList" runat="server" 
                                                          DataSourceID="ODSCredentialType" 
                                                          DataTextField="CredentialTypeName" 
                                                          DataValueField="CredentialTypeID"
                                                    />                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Program Name: 

                                                </th>
                                                <td>
                                                    <asp:TextBox ID="ProgramNameTextBox" runat="server" Text='<%# Bind("ProgramName") %>' />
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Entrance Requirement:
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="EntranceRequirementTextBox" runat="server" TextMode="multiline" Columns="20" Rows="3" Text='<%# Bind("ProgramDescription") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Total Credits:</th>
                                                <td>
                                                    <asp:TextBox ID="TotalCreditsTextBox" runat="server" Text='<%# Bind("TotalCredits") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Program Length:</th>
                                                <td>
                                                    <asp:TextBox ID="ProgramLengthTextBox" runat="server" Text='<%# Bind("ProgramLength") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Competive Advantage:</th>
                                                <td>
                                                    <asp:TextBox ID="CompetiveAdvantageTextBox" runat="server" Text='<%# Bind("CompetitiveAdvantage") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Active:</th>
                                                <td>
                                                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Text='<%# Bind("Active") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Program Link:</th>
                                                <td>
                                                    <asp:TextBox ID="ProgramLinkTextBox" runat="server" Text='<%# Bind("ProgramLink") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <td>
                                                    <asp:LinkButton ID="InsertButton" CommandName="Insert" runat="server" Text="Save" ></asp:LinkButton>
                                                    <asp:LinkButton ID="CancelButton" CommandName="Cancel" runat="server" Text="Cancel"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </InsertItemTemplate>
            <ItemTemplate>
                <tr style=" ">
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                    <td align="center">
                        <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="CredentialTypeIDLabel" runat="server" Text='<%# Eval("CredentialTypeID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="TotalCreditsLabel" runat="server" Text='<%# Eval("TotalCredits") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="ProgramLengthLabel" runat="server" Text='<%# Eval("ProgramLength") %>' />
                    </td>
                    <td align="center">
                        <asp:Label ID="CompetitiveAdvantageLabel" runat="server" Text='<%# Eval("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLinkLabel" runat="server" Text='<%# Eval("ProgramLink") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" class="table align-fix">
                                <tr runat="server" style="">
                                    <th runat="server"></th>
                                    <th runat="server">ProgramID</th>
                                    <th runat="server">Credential TypeID</th>
                                    <th runat="server">Program Name</th>
                                    <th runat="server">Entrance Requirement</th>
                                    <th runat="server">Total Credits</th>
                                    <th runat="server">Program Length</th>
                                    <th runat="server">Competitive Advantage</th>
                                    <th runat="server">Active</th>
                                    <th runat="server">Program Link</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align:left">
                            <asp:LinkButton ID="NewButton" runat="server" Text="Add New" OnClick="NewButton_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server"> <%--style="text-align: center;background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;"--%>
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
                <tr ><%--style="background-color:#E2DED6; font-weight: bold;color: #333333;"--%>
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="listview-buttons"/>
                    </td>
                    <td>
                        <asp:Label ID="ProgramIDLabel" runat="server" Text='<%# Eval("ProgramID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CredentialTypeIDLabel" runat="server" Text='<%# Eval("CredentialTypeID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                    </td>
                    <td>
                        <asp:Label ID="TotalCreditsLabel" runat="server" Text='<%# Eval("TotalCredits") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLengthLabel" runat="server" Text='<%# Eval("ProgramLength") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CompetitiveAdvantageLabel" runat="server" Text='<%# Eval("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLinkLabel" runat="server" Text='<%# Eval("ProgramLink") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <asp:ObjectDataSource ID="ODSCategoryList" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        
        
        <asp:ObjectDataSource ID="ODSCredentialType" runat="server" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        
        
     </div>
</div>
</asp:Content>

