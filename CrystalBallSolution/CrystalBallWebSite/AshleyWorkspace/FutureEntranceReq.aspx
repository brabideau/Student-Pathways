<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FutureEntranceReq.aspx.cs" Inherits="AshleyWorkspace_FutureEntranceReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" id="EntranceRequirements" visible="false" class="clearfix">

        <p>What high school courses does this program require?</p>
        <asp:ListView ID="LV_SubjectReq" runat="server" ItemType="CrystalBallSystem.DAL.Entities.SubjectRequirement">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th></th>
                        <th runat="server">Subject</th>
                        <th runat="server">Courses</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>        
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="SubjectIDLabel" runat="server" Text='<%# Eval("SubjectRequirementID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("SubjectDescription") %>' />
                    </td>
                    <td>
                        <asp:GridView ID="GV_EntranceReqs" runat="server" OnRowDeleting="GV_EntranceReqs_RowDeleting" DataKeyNames="CourseID" ItemType="CrystalBallSystem.DAL.POCOs.GetHSCourseCode">
                            <Columns>
                                <asp:TemplateField HeaderText="CourseID">
                                    <ItemTemplate>
                                        <asp:Label ID="CourseID" runat="server" Text='<%# Item.CourseID %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CourseCode">
                                    <ItemTemplate>
                                        <asp:Label ID="CourseCode" runat="server" Text='<%# Item.CourseCode %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField Text="Remove" CommandName="Delete"/>
                            </Columns>          
            
                            <EmptyDataTemplate>
                                No Existing Entrance Requirements.
                            </EmptyDataTemplate>

                        </asp:GridView>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>




        <p>Does entry to this program require any previous post-secondary work?</p>

        <asp:ListView ID="LV_DegreeEntranceReq" runat="server">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th></th>
                        <th runat="server">Credential Type</th>
                        <th runat="server">Category</th>
                        <th runat="server">GPA</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>        
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="DegreeEntranceReqLabel" runat="server" Text='<%# Eval("DegreeEntranceReqID") %>' />
                    </td>
                    <td>
                        <asp:DropDownList ID="DL_DegEnt_CredType" runat="server" DataSourceID="ODSCredentialType" 
                                                                    DataTextField="CredentialTypeName" 
                                                                    DataValueField="CredentialTypeID" SelectedValue='<%# Eval("CredentialTypeID") %>'></asp:DropDownList>

                    </td>
                    <td>
                        <asp:DropDownList ID="DL_DegEnt_Cat" runat="server" DataSourceID="CategoryList" 
                                                                    DataTextField="CategoryDescription" 
                                                                    DataValueField="CategoryID" SelectedValue='<%# Eval("CategoryID") %>'></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="GPA_TextBox" runat="server" Text='<%# Eval("GPA") %>'></asp:TextBox>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:LinkButton ID="EntranceReq_Save" runat="server" OnClick="Save_EntranceReq" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
</asp:Content>

