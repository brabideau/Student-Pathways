<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FutureEntranceReq.aspx.cs" Inherits="AshleyWorkspace_FutureEntranceReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server" id="EntranceRequirements" class="clearfix">

        <p>What high school courses does this program require?</p>
        <asp:GridView ID="LV_SubjectReq" runat="server"  AutoGenerateColumns="False" ItemType="CrystalBallSystem.DAL.DTOs.SubjectRequirementAndCourses" DataKeyNames="EntranceReqID" OnRowDeleting="LV_SubjectReq_RowDeleting" ShowFooter="true">
            <Columns>
                <asp:TemplateField HeaderText="Entrance Requirement ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="EntranceIDLabel" runat="server" Text='<%# Item.EntranceReqID %>' />
                    </ItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Subject Requirement ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="SubjectIDLabel" runat="server" Text='<%# Item.SubjectReqID %>' />
                    </ItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Subject Description">
                    <ItemTemplate>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Item.SubjectDesc %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Course Code">
                    <ItemTemplate>
                        <asp:Label ID="CourseLabel" runat="server" Text='<%# Item.GetHSCourseCode %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField>
                    <FooterTemplate>
                        <asp:LinkButton ID="Add" runat="server" Text="Add Requirement" OnClick="AddNew_Click" CssClass="button button-long"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>   
                <asp:ButtonField Text="Remove" CommandName="Delete"/>                
            </Columns>               

            <EmptyDataTemplate>
                No Existing Entrance Requirements.
            </EmptyDataTemplate>
            
        </asp:GridView>

        <div id="addRequirement" runat="server">
            <asp:GridView ID="GV_Course" runat="server" align="center" AutoGenerateColumns="False" ShowFooter="True" OnRowDeleting="GVCourse_RowDeleting">     
                <Columns>
                    <asp:BoundField DataField="RowNumber" Visible="false"/>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="Add_Btn" runat="server" Font-Underline="false"
                                OnClick="AddNew_Click" CssClass="wizard-course-buttons hvr-ripple-out add-align"  
                                CausesValidation="false"><i aria-hidden="true" class="glyphicon glyphicon-plus"></i></asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>

                            <asp:DropDownList ID="DL_Course" runat="server" DataSourceID="CourseList" CssClass="form-control"
                                            DataTextField="HighSchoolCourseDescription"
                                            DataValueField="HighSchoolCourseID"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="">Select a Course &amp; Level</asp:ListItem>
                            </asp:DropDownList>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enter Required Mark (Optional)">                                
                        <ItemTemplate>
                            <asp:TextBox ID="TB_EnterMarks" runat="server" CssClass="form-control ent-req-input"
                                Width="50px"></asp:TextBox>
                            <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter a number between 1 - 100" 
                                ControlToValidate="TB_EnterMarks" MinimumValue="1" MaximumValue="100" ForeColor="Maroon" 
                                Type="Integer" Font-Size="Smaller" Display="Dynamic"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You must enter a grade value."
                                ControltoValidate="TB_EnterMarks" ForeColor="Maroon" Font-Size="Smaller" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>               
                    <asp:CommandField ShowDeleteButton="True" />                
                </Columns>
            </asp:GridView>
        </div>

        <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>


        <p>Does entry to this program require any previous post-secondary work?</p>

        <%--<asp:ListView ID="LV_DegreeEntranceReq" runat="server">
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
        </asp:ListView>--%>
        <asp:LinkButton ID="EntranceReq_Save" runat="server" OnClick="Save_EntranceReq" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
</asp:Content>

