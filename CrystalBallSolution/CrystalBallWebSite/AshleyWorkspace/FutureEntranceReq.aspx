<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FutureEntranceReq.aspx.cs" Inherits="AshleyWorkspace_FutureEntranceReq" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
                        <asp:Label ID="CourseLabel" runat="server" Text='<%# Item.HSCourseCode %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mark">
                    <ItemTemplate>
                        <asp:Label ID="MarkLabel" runat="server" Text='<%# Item.HSCourseMark %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:ButtonField Text="Remove" CommandName="Delete"/>                
            </Columns>               

            <EmptyDataTemplate>
                No Existing Entrance Requirements.
            </EmptyDataTemplate>
            
        </asp:GridView>

        <div id="addRequirement" runat="server">
            <!--Dropdown to select SubjectDescription and prepopulate gridview-->
            <asp:DropDownList ID="DL_SubjDesc" runat="server" DataSourceID="ODS_SubjectRequirement" DataTextField="SubjectDescription" DataValueField="SubjectRequirementID" AppendDataBoundItems="true">
                <asp:ListItem Value="0">[Create New Subject Requirement]</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="SubReqDesc" runat="server" Visible="false" />
            <asp:LinkButton ID="SubjectButton" runat="server" OnClick="SubjectButton_Click">Select</asp:LinkButton>
        </div>

        <div id="prePopulatedER" runat="server" visible="false">
            <!--Pre-Populated Course Gridview-->
            <asp:GridView ID="GV_NewEntrReq" runat="server" AutoGenerateColumns="False" ItemType="CrystalBallSystem.DAL.POCOs.GetEntranceReq" DataKeyNames="HSCourseID" OnSelectedIndexChanging="GV_NewEntrReq_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Course ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Item.HSCourseID %>' Visible="false"/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <asp:Label ID="Course" runat="server" Text='<%# Item.HSCourseName %>' />
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Mark (Optional)">
                        <ItemTemplate>
                            <asp:TextBox ID="Mark" runat="server" Width="50px" Text=<%# Item.Mark %> />
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:ButtonField Text="Remove" CommandName="Select"/>                
                </Columns>
            </asp:GridView>
            <asp:LinkButton ID="addPPSubjectButton" runat="server" OnClick="addPPSubjectButton_Click">Add Requirement</asp:LinkButton>
        </div>

        <div id="manualER" runat="server" visible="true">
            <asp:GridView ID="GV_ManualNewEntrReq" runat="server" AutoGenerateColumns="False" OnRowDeleting="GV_ManualNewEntrReq_RowDeleting" ShowFooter="true">            
                <Columns>
                    <asp:BoundField DataField="RowNumber" Visible="false"/>
                    <asp:TemplateField HeaderText="Course">
                        <FooterTemplate>
                            <asp:LinkButton ID="Add_Btn" runat="server" Font-Underline="false"
                                OnClick="AddNew_Click" CssClass="wizard-course-buttons hvr-ripple-out add-align"  
                                CausesValidation="false">Add Alternative Course</asp:LinkButton>
                        </FooterTemplate>   
                        <ItemTemplate>
                            <asp:DropDownList ID="DL_Course" runat="server" DataSourceID="CourseList"
                                            DataTextField="HighSchoolCourseDescription"
                                            DataValueField="HighSchoolCourseID"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="">Select a Course</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mark (Optional)">                                
                        <ItemTemplate>
                            <asp:TextBox ID="Marks" runat="server" Width="50px" />                            
                        </ItemTemplate>
                    </asp:TemplateField>                               
                    <asp:CommandField ShowDeleteButton="True" />                
                </Columns>
            </asp:GridView>
            <asp:LinkButton ID="addMSubjectButton" runat="server" OnClick="addMSubjectButton_Click">Add Requirement</asp:LinkButton>
        </div>

        <!--ODS-->
        <asp:ObjectDataSource ID="ODS_SubjectRequirement" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_SubjectRequirements" TypeName="CrystalBallSystem.BLL.testController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>

    </div>
</asp:Content>--%>


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
<%--    <asp:LinkButton ID="EntranceReq_Save" runat="server" OnClick="Save_EntranceReq" CssClass="button next button-long">Save & Continue</asp:LinkButton>--%>
</div>
</asp:Content>