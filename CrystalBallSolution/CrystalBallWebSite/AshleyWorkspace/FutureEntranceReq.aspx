<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FutureEntranceReq.aspx.cs" Inherits="AshleyWorkspace_FutureEntranceReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" id="EntranceRequirements" visible="false" class="clearfix">

        <p>What high school courses does this program require?</p>
        <asp:GridView ID="LV_SubjectReq" runat="server"  AutoGenerateColumns="False">
            <%--ItemType="CrystalBallSystem.DAL.POCOs.SubjectRequirementAndCourses" DataKeyNames="SubjectRequirementID"--%>
            <%--<Columns>
                <asp:TemplateField HeaderText="SubjectRequirementID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="SubjectIDLabel" runat="server" Text='<%# Item.SubjectRequirementID %>' />
                    </ItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="SubjectDescription">
                    <ItemTemplate>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Item.SubjectDescription %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
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
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>   --%> 
            <%--<ItemTemplate>
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
            </ItemTemplate>--%>
        </asp:GridView>


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

