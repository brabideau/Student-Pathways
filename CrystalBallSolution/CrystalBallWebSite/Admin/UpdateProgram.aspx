<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpdateProgram.aspx.cs" Inherits="Admin_UpdateProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" align="center">
    <h1>Update Program</h1>
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
                    <asp:LinkButton ID="SearchButton" runat="server">Search</asp:LinkButton>
                </td>
            </tr>
        </table>

        <asp:ListView ID="ProgramListView" runat="server" DataSourceID="ODSProgramByCategory" align="center" DataKeyNames="ProgramID">
            <AlternatingItemTemplate>
                <tr style="background-color:#efefef; color: #284775; align-content:center">
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
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="ProgramIDTextBox" runat="server" Text='<%# Bind("ProgramID") %>' />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="CredentialTypeIDTextBox" runat="server" Text='<%# Bind("CredentialTypeID") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramNameTextBox" runat="server" Text='<%# Bind("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramDescriptionTextBox" runat="server" Text='<%# Bind("ProgramDescription") %>' />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="TotalCreditsTextBox" runat="server" Text='<%# Bind("TotalCredits") %>' />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="ProgramLengthTextBox" runat="server" Text='<%# Bind("ProgramLength") %>' />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="CompetitiveAdvantageTextBox" runat="server" Text='<%# Bind("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramLinkTextBox" runat="server" Text='<%# Bind("ProgramLink") %>' />
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
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramIDTextBox" runat="server" Text='<%# Bind("ProgramID") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="CredentialTypeIDTextBox" runat="server" Text='<%# Bind("CredentialTypeID") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramNameTextBox" runat="server" Text='<%# Bind("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramDescriptionTextBox" runat="server" Text='<%# Bind("ProgramDescription") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="TotalCreditsTextBox" runat="server" Text='<%# Bind("TotalCredits") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramLengthTextBox" runat="server" Text='<%# Bind("ProgramLength") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="CompetitiveAdvantageTextBox" runat="server" Text='<%# Bind("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="ProgramLinkTextBox" runat="server" Text='<%# Bind("ProgramLink") %>' />
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
                <tr style="background-color:#E2DED6; font-weight: bold;color: #333333;">
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
        <asp:ObjectDataSource ID="ODSProgramByCategory" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" DataObjectTypeName="CrystalBallSystem.DAL.Entities.Program" OldValuesParameterFormatString="original_{0}" UpdateMethod="Program_Update">
            <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropdownList" Name="categoryID" PropertyName="SelectedValue" Type="Int32" DefaultValue="" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
        
     </div>
</div>
</asp:Content>

