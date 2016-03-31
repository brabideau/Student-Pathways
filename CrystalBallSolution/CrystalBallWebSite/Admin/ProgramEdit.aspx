<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProgramEdit.aspx.cs" Inherits="Briand_Workspace_ProgramEdit" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >

    <h1>Manage Programs</h1>
    <asp:UpdatePanel runat="server">
        
        <ContentTemplate>

       
   <div runat="server" id="Buttons" visible="false" >
       <h2>
            <asp:Label ID="ProgramNameLabel" runat="server" Text=""></asp:Label></h2>
        
     <%-- ----------------------------- BUTTONS ---------------------------------------%>

        <asp:RadioButtonList ID="Tab_Labels" runat="server" RepeatLayout="OrderedList" CssClass="tabs clearfix" OnSelectedIndexChanged="Change_Tab" AutoPostBack="true">
            <asp:ListItem Value="1" Text="Program Info" Selected="true"></asp:ListItem>
            <asp:ListItem Value="2" Text="Categories"></asp:ListItem>
            <asp:ListItem Value="3" Text="Entrance Requirements"></asp:ListItem>
            <asp:ListItem Value="4" Text="Courses"></asp:ListItem>
            <asp:ListItem Value="5" Text="Transfer Credits"></asp:ListItem>
            <asp:ListItem Value="6" Text="Program Preferences"></asp:ListItem>
        </asp:RadioButtonList>
       <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    </div>

    <div runat="server" id="ProgramEditDiv" visible="false">
     <%-- -------------------------------ADD NEW PROGRAM------------------------------------------ --%>
        <div runat="server" id="AddNewProgram" visible="false" class="clearfix">
       <asp:Label ID="NewProgramIDLabel" runat="server" Text="" Visible="false"></asp:Label>
    
       <p class="clearfix"><span class="label col-3">Program Name:</span> <asp:TextBox ID="NewProgramNameTextBox" runat="server" CssClass="col-4"/></p> 

        <p class="clearfix"><span class="label col-3">Credential Type:</span><asp:DropDownList ID="CredentialDropDownList" runat="server" 
                                                                  DataSourceID="ODSCredentialType" 
                                                                  DataTextField="CredentialTypeName" 
                                                                  DataValueField="CredentialTypeID"
                                                                  CssClass="col-2"
                                                                  />   
        </p>

        <p class="clearfix"><span class="label col-3">Description:</span> <asp:TextBox ID="NewProgramDescription" TextMode="multiline" runat="server" CssClass="col-6"/></p>
        <p class="clearfix"><span class="label col-3">Total Credits:</span> <asp:TextBox ID="NewProgramTotalCredits" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Program Length: </span><asp:DropDownList ID="lengthDropDownList" runat="server" CssClass="col-2">
                                                        <asp:ListItem Value="0">[Select Length]</asp:ListItem>
                                                        <asp:ListItem>3 months</asp:ListItem>
                                                        <asp:ListItem>6 months</asp:ListItem>
                                                        <asp:ListItem>1 Year(s)</asp:ListItem>
                                                        <asp:ListItem>2 Year(s)</asp:ListItem>
                                                        <asp:ListItem>3 Year(s)</asp:ListItem>
                                                        <asp:ListItem>4 Year(s)</asp:ListItem>
                                                    </asp:DropDownList></p>

        <p class="clearfix"><span class="label col-3">
            Competitive Average (Competitive Advantage): </span><asp:TextBox ID="NewProgramCompetitive" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Check if Active: </span> <asp:Checkbox ID="NewProgramActive" runat="server" CssClass="col-1"/></p> 

        <p class="clearfix"><span class="label col-3">Program Link: </span><asp:TextBox ID="NewProgramLink" runat="server"  CssClass="col-4"/></p>
        <asp:LinkButton ID="SaveButton" runat="server" OnClick="Add_Program" CssClass="button next button-long">Save & Continue</asp:LinkButton>
      </div>
           
     <%-- ----------------------------- PROGRAM INFO ---------------------------------------%>
       <div runat="server" id="ProgramInfo" visible="false" class="clearfix">
       <asp:Label ID="ProgramIDLabel" runat="server" Text="" Visible="false"></asp:Label>
    
       <p class="clearfix"><span class="label col-3">Program Name:</span> <asp:TextBox ID="TB_ProgramName" runat="server" CssClass="col-4"/></p> 

        <p class="clearfix"><span class="label col-3">Credential Type:</span><asp:DropDownList ID="DL_CredentialType" runat="server" 
                                                                  DataSourceID="ODSCredentialType" 
                                                                  DataTextField="CredentialTypeName" 
                                                                  DataValueField="CredentialTypeID"
                                                                  CssClass="col-2"
                                                                  />   </p>

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="A short (500 character) description of your program to get students excited about it" />Description:</span> <asp:TextBox ID="TB_Description" TextMode="multiline" runat="server" CssClass="col-6"/></p>
        <p class="clearfix"><span class="label col-3">Total Credits:</span> <asp:TextBox ID="TB_Credits" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Program Length: </span><asp:TextBox ID="TB_Length" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="The minimum high school GPA that students generally need to enter" />Competitive Average (Competitive Advantage): </span><asp:TextBox ID="TB_CompetitiveAdvantage" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="Active programs will show up in student's search results" />Check if Active: </span> <asp:Checkbox ID="CB_Active" runat="server" CssClass="col-1"/></p> 

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="Link to the main program page on the NAIT website" />Program Link: </span><asp:TextBox ID="TB_Link" runat="server"  CssClass="col-4"/></p>
        <asp:LinkButton ID="Program_Save" runat="server" OnClick="Save_Program" CssClass="button next button-long">Save & Continue</asp:LinkButton>
      
    </div>

    <%-- ----------------------------- CATEGORIES ---------------------------------------%>
    
    <div runat="server" id="Categories" visible="false" class="clearfix">
        <p>Select the categories that this program belongs to:</p>
        <asp:CheckBoxList ID="CB_Categories" runat="server" DataSourceID="CategoryList" DataTextField="CategoryDescription" DataValueField="CategoryID" >
        </asp:CheckBoxList>
        <asp:LinkButton ID="Categories_Save" runat="server" OnClick="Save_Categories" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>


    <%-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------%>
     
    <div runat="server" id="EntranceRequirements" class="clearfix">

        <%--HIGH SCHOOL ENTRANCE REQUIREMENTS--%>        
        <p><asp:Label runat="server" CssClass="tooltip" ToolTip="Courses in the same Subject are considered equivalent. A student only needs one course in each group to enter." />What high school courses does this program require?</p>
        <div runat="server" id="HSRequirements">
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
                <asp:LinkButton ID="SubjectButton" runat="server" CssClass="button submit button-long" OnClick="SubjectButton_Click">Add Requirement</asp:LinkButton>
            </div>

            <div id="manualER" runat="server" visible="false">
                <asp:TextBox ID="SubReqDesc" runat="server" />
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
        </div>


        <%--POST SECONDARY ENTRANCE REQUIREMENT--%>
        <p>Does entry to this program require any previous post-secondary work?</p>
        <div runat="server" id="PSRequirements">
            <asp:GridView ID="GV_DegreeEntranceReq" runat="server" AutoGenerateColumns="False" ItemType="CrystalBallSystem.DAL.POCOs.GetDegEntReqs" DataKeyNames="DegreeEntranceRequirementID" OnRowDeleting="GV_DegReq_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Degree Requirement ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="DegreeEntranceRequirementID" runat="server" Text='<%# Item.DegreeEntranceRequirementID %>' />
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Credential Name">
                        <ItemTemplate>
                            <asp:Label ID="CredentialName" runat="server" Text='<%# Item.CredentialName %>' />
                        </ItemTemplate>  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="Category" runat="server" Text='<%# Item.Category %>' />
                        </ItemTemplate>  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GPA">
                        <ItemTemplate>
                            <asp:Label ID="GPA" runat="server" Text='<%# Item.GPA %>' />
                        </ItemTemplate>  
                    </asp:TemplateField>
                    <asp:ButtonField Text="Remove" CommandName="Delete"/> 
                </Columns>

                <EmptyDataTemplate>
                    No Existing Entrance Requirements.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:LinkButton ID="addDER" runat="server" CssClass="button submit button-long" OnClick="addDER_Click" >Add Requirement</asp:LinkButton>

            <div runat="server" id="AddRequirements" class="clearfix" visible="false">
                <p>Credential Type: </p>
                <asp:DropDownList ID="DL_Credential" runat="server" DataSourceID="CredentialODS" DataTextField="CredentialTypeName" DataValueField="CredentialTypeID"></asp:DropDownList>
                <p>Category: </p>
                <asp:DropDownList ID="DL_Category" runat="server" DataSourceID="CategoryODS" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList>
                <p>GPA: </p>
                <asp:TextBox ID="TB_GPA" runat="server"></asp:TextBox>        
        
                <asp:LinkButton ID="Add_DER" runat="server" CssClass="button submit button-long" OnClick="Add_DER_Click">Add Equivalency</asp:LinkButton>
            </div>
        </div>
     </div>

    <%-- ----------------------------- PROGRAM COURSES ---------------------------------------%>
     
    <div runat="server" id="ProgramCourses" visible="false" class="clearfix">
        <p>What courses are part of this program?</p>
        <h4>Level One</h4>
        <div class="nait-courses">
         <asp:ListView ID="LV_ProgramCourses_One" runat="server" OnItemCommand="Remove_Program_Course">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>

                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Remove" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
            </div>
        <h4>Level Two</h4>
            <div class="nait-courses">
         <asp:ListView ID="LV_ProgramCourses_Two" runat="server" OnItemCommand="Remove_Program_Course">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Remove" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
            </div>
        <h4>Level Three</h4>
        <div class="nait-courses">
        <asp:ListView ID="LV_ProgramCourses_Three" runat="server" OnItemCommand="Remove_Program_Course">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>'  Visible="false"/>
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Remove" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
            </div>

        <h4>Level Four</h4>
        <div class="nait-courses">
                 <asp:ListView ID="LV_ProgramCourses_Four" runat="server" OnItemCommand="Remove_Program_Course">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>'  Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Remove" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
            </div>

        <h4>Other:</h4>
        <div class="nait-courses">
       <asp:ListView ID="LV_ProgramCourses_More" runat="server"
           OnItemCommand="Remove_Program_Course">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Remove" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
        </div>
        <div runat="server" class="search-bar">
            <label>Search for a course to add:</label>

            <asp:TextBox ID="TB_ProgramCoursesSearch" runat="server"></asp:TextBox>
            <asp:LinkButton ID="LinkButton1" runat="server" Text="Search" OnClick="ProgramCourses_Search" />
        </div>



        <div class="nait-courses">
         <asp:ListView ID="LV_ProgramCoursesSearch" runat="server"
                            OnItemCommand="Add_Program_Course" >
        <LayoutTemplate>
            <table class="nait-courses">
                <tr>
                    <th></th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                    <th></th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                     <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                    <td>
                        <asp:DropDownList ID="DL_Semester" runat="server">
                            <asp:ListItem Value="-1">---</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" Text="Add to Program" CssClass="admin_button" />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
            </div>


        <asp:LinkButton ID="Courses_Save" runat="server" OnClick="Save_Courses" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>


    <%-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------%>
     
    <div runat="server" id="CourseEquivalencies" visible="false" class="clearfix">
        <p>Does this program accept transfer credit/advanced credit for any other NAIT courses?</p>

        <!--Add new equivalency to empty gridview--> 
   
        <asp:GridView ID="GV_Equivalencies" runat="server" AutoGenerateColumns="False"  OnRowDeleting="EquivalenciesGrid_RowDeleting" CssClass="equivalency-grid clearfix" ItemType="CrystalBallSystem.DAL.POCOs.GetEquivalencyNames" ShowFooter="True" DataKeyNames="CourseEquivalencyID">
            <Columns>
                <asp:TemplateField HeaderText="CourseEquivalencyID">
                    <ItemTemplate>
                        <asp:Label ID="CourseEquivalencyID" runat="server" Text='<%# Item.CourseEquivalencyID %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CourseCode">
                    <ItemTemplate>
                        <asp:Label ID="CourseCode" runat="server" Text='<%# Item.CourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CourseName">
                    <ItemTemplate>
                        <asp:Label ID="CourseName" runat="server" Text='<%# Item.CourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="DestinationCourseCode">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseCode" runat="server" Text='<%# Item.DestinationCourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DestinationCourseName">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseName" runat="server" Text='<%# Item.DestinationCourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>      
                  
                <asp:ButtonField Text="Remove" CommandName="Delete"/>
            </Columns>          
            
            <EmptyDataTemplate>
                No Existing Equivalencies.
            </EmptyDataTemplate>
                       
        </asp:GridView>    

        <div runat="server" id="addNewEquivalency" visible="true"  CssClass="add-equivalency-block">
        <asp:Label ID="EmptyCurrent" runat="server" Text="Current Program Course ID:" CssClass="label col-3"></asp:Label>
        <asp:DropDownList ID="EmptyCurrentDropdown" runat="server" DataSourceID="EmptyCurrentDropdownODS" 
            DataTextField="CourseName" DataValueField="CourseID" AppendDataBoundItems="True" CssClass="col-2">
             <asp:ListItem Value="-1">[Select Course]</asp:ListItem>
        </asp:DropDownList>
        
        <asp:Label ID="EmptyEquivalent" runat="server" Text="Program: "  CssClass="label col-3"></asp:Label>
        <asp:DropDownList ID="EmptyEquivalentProgram" runat="server" CssClass="col-3" DataSourceID="ProgramODS" DataTextField="ProgramName" DataValueField="ProgramID" OnSelectedIndexChanged="EmptyEquivalentProgram_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true">
            <asp:ListItem Value="-1">[Select Program]</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="EquivalentCourseName" runat="server" Text="Course: "></asp:Label>
        <asp:DropDownList ID="EquivalentCourseID" runat="server" DataSourceID="CourseByProgramODS" DataTextField="CourseName" DataValueField="CourseID" AppendDataBoundItems="True" AutoPostBack="true">
            <asp:ListItem Value="-1">[Select Course]</asp:ListItem>
        </asp:DropDownList>
            <p class="clearfix">
        <asp:LinkButton ID="Enter" runat="server" OnClick="Enter_Click"  CssClass="button button-long next">Enter Equivalency</asp:LinkButton>
             </p>
    </div>

         <asp:LinkButton ID="CourseEquivalencies_Save" runat="server" OnClick="Save_CourseEquivalencies" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
    

    <%-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------%>
   
    <div runat="server" id="ProgramPreferences" visible="false" class="clearfix">
        <p>Answer the questions below so that students can be better matched with this program:</p>
        <asp:GridView ID="GV_Questions" DataSourceID="QuestionsList" runat="server" AutoGenerateColumns="False" CssClass="prefQuestionsCSS">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="QuestionID" runat="server" Visible="false" Text='<%# Eval("QuestionID") %>'></asp:Label>
                   </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButtonList ID="RB_Preference" runat="server" RepeatLayout="OrderedList">
                            <asp:ListItem Value="1" Text="Definitely Not"></asp:ListItem>
                            <asp:ListItem Value="2" Text="No"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Don't Know" Selected="true"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Definitely"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinkButton ID="Questions_Save" runat="server" OnClick="Save_Questions" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
        <hr />
    </div> <%--end program edit div--%>
      <%-- ----------------------------- SEARCH ---------------------------------------%>
    <div runat="server" id="search" class="search-bar" >
        <asp:TextBox ID="Search_Box" runat="server" Width="200px" placeholder="Search Programs"></asp:TextBox> in
        <asp:DropDownList ID="CategoryDropDowList" runat="server" Height="32px" DataSourceID="CategoryList" DataTextField="CategoryDescription" DataValueField="CategoryID" AppendDataBoundItems="true">
            <asp:ListItem Value="0">[All Subjects]</asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton ID="Search_Button" runat="server" Text="Search" OnClick="Program_Search" CssClass="admin_button2"/>
        <asp:LinkButton ID="Add_Program_Button" runat="server" Text="Add New Program" CssClass="admin_button2" OnClick="Add_Program_Button_Click"/>
    </div>

     <%-- ----------------------------- PROGRAM LIST ---------------------------------------%>
    <div runat="server" id="ProgramList" visible="true" class="col-12 searchbox">
        <asp:ListView ID="Program_List" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th class="col-1"></th>
                    <th runat="server">Program Name</th>
                    <th runat="server">Description</th>
                    <th runat="server">Active?</th>
                    <th runat="server">Link</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>        
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" CommandArgument='<%#Eval("ProgramID") %>' OnClick="Populate_Program_Info" Text="Edit" CssClass="admin_button" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLinkLabel" runat="server" Text='<%# Eval("ProgramLink") %>' />
                    </td>
                </tr>
            </ItemTemplate>
    </asp:ListView>
    </div>

             </ContentTemplate>
    </asp:UpdatePanel>


    <%-- -----------------------------ODS---------------------------------------%>

     <asp:ObjectDataSource ID="CategoryList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ODSCredentialType" runat="server" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="QuestionsList" runat="server" SelectMethod="Question_List" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource> 

    <asp:ObjectDataSource ID="EmptyCurrentDropdownODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProgramIDLabel" Name="programID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CredentialODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>    
    
    <asp:ObjectDataSource ID="CategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ProgramODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_All_Programs" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="CourseByProgramODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="EmptyEquivalentProgram" Name="programID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ODS_SubjectRequirement" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_SubjectRequirements" TypeName="CrystalBallSystem.BLL.testController"></asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>
</asp:Content>