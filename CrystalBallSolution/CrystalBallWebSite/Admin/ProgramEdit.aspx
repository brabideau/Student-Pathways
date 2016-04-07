<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProgramEdit.aspx.cs" Inherits="Admin_ProgramEdit" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >

    <h1>Manage Programs</h1>

    <asp:UpdatePanel runat="server">
    <ContentTemplate>


        <%-- ----------------------------- BUTTONS ---------------------------------------%>
   <div runat="server" id="Buttons" visible="false" >
       <h2>
            <asp:Label ID="ProgramNameLabel" runat="server" Text=""></asp:Label></h2>

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
     <%-- ----------------------------- PROGRAM INFO ---------------------------------------%>
       <div runat="server" id="BasicProgramInfo" visible="false" class="clearfix">
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

        <p class="clearfix"><span class="label col-3">Program Length: </span><asp:DropDownList ID="TB_Length" runat="server" CssClass="col-2">
                                                        <asp:ListItem Value="0">[Select Length]</asp:ListItem>
                                                        <asp:ListItem>3 months</asp:ListItem>
                                                        <asp:ListItem>6 months</asp:ListItem>
                                                        <asp:ListItem>1 Year(s)</asp:ListItem>
                                                        <asp:ListItem>2 Year(s)</asp:ListItem>
                                                        <asp:ListItem>3 Year(s)</asp:ListItem>
                                                        <asp:ListItem>4 Year(s)</asp:ListItem>
                                                    </asp:DropDownList></p>

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="The minimum high school GPA that students generally need to enter" />Competitive Average (Competitive Advantage): </span><asp:TextBox ID="TB_CompetitiveAdvantage" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="Active programs will show up in student's search results" />Check if Active: </span> <asp:Checkbox ID="CB_Active" runat="server" CssClass="col-1"/></p> 

        <p class="clearfix"><span class="label col-3"><asp:Label runat="server" CssClass="tooltip" ToolTip="Link to the main program page on the NAIT website" />Program Link: </span><asp:TextBox ID="TB_Link" runat="server"  CssClass="col-4"/></p>
        <asp:LinkButton ID="Program_Save" runat="server" OnClick="Save_Program" CssClass="button next button-long">Save & Continue</asp:LinkButton>
        <%--<asp:LinkButton ID="Program_Add" runat="server" OnClick="Add_Program" CssClass="button next button-long">Save & Continue</asp:LinkButton>--%>
      
    </div>

    <%-- ----------------------------- CATEGORIES ---------------------------------------%>
    
    <div runat="server" id="Categories" visible="false" class="clearfix">
        <p>Select the categories that this program belongs to:</p>
        <asp:CheckBoxList ID="CB_Categories" runat="server" DataSourceID="CategoryList" DataTextField="CategoryDescription" DataValueField="CategoryID"  CssClass="radiochecks" >
        </asp:CheckBoxList>
        <asp:LinkButton ID="Categories_Save" runat="server" OnClick="Save_Categories" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>


    <%-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------%>
     
    <div runat="server" id="EntranceRequirements" visible="false" class="clearfix">
       
       <h3>High School</h3> 
        <p>Please indicate which high school classes a student must have to enter this program.</p>
        <div runat="server" id="HighSchoolentReqs" class="entreqcss">

             <h5>Current Entrance Requirements</h5>
        <asp:ListView ID="LV_SubjectReq" runat="server">

         <LayoutTemplate>
            <table>
                <tr>
                    <th runat="server">Subject <asp:Label runat="server" CssClass="tooltip" ToolTip="High school courses in the same subject grouping will be considered equivalent." /></th>
                    <th runat="server">Classes</th>
                    <th></th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>

            <tr>
                <td runat="server" visible="false">
                    <asp:Label ID="SubjectIDLabel" runat="server" Text='<%# Eval("SubjectRequirementID") %>'></asp:Label>
                </td>
                <td runat="server" >
                    <h6><asp:Label ID="Label1" runat="server" Text='<%# Eval("SubjectDescription") %>'></asp:Label></h6>
                </td>
                <td runat="server">
                    <asp:ListView ID="LV_EntranceReq" runat="server" DataSource='<%# Eval("EntranceReqs") %>' ItemPlaceholderID="EntrancePlaceHolder"  OnItemCommand="Ent_Req_Commands">
                        <LayoutTemplate>
                            <table>
                         
                                <tr id="EntrancePlaceHolder" runat="server"></tr>
                           </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td runat="server" visible="false">
                                     <asp:Label ID="EntIDLabel" runat="server" Text='<%# Eval("EntranceRequirementID") %>'></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:Label ID="Label4" runat="server" ><asp:TextBox ID="Ent_Marks" runat="server" Width="50px" Text='<%# Eval("Mark") %>' /> % in </asp:Label>
                                
                                    <asp:DropDownList ID="DL_HS_Course" runat="server" DataSourceID="CourseList"
                                                DataTextField="HighSchoolCourseDescription"
                                                DataValueField="HighSchoolCourseID"
                                                SelectedValue='<%# Eval("HSCourseID") %>'>
                                               
                                </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:LinkButton ID="EditButton" runat="server" Text="Save" CommandArgument="Save" CssClass="admin_button" /> - or -
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="Remove" Text="Remove" CssClass="admin_button" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </ItemTemplate>
        </asp:ListView>

        <h5>Add a New Entrance Requirement</h5>
        Subject: 
        <asp:DropDownList ID="DL_New_Subject" runat="server" DataSourceID="ODS_SubjectRequirement"
                                                DataTextField="SubjectDescription"
                                                DataValueField="SubjectRequirementID">
                                               
                                </asp:DropDownList>
        - - - - -
        <asp:TextBox ID="NewMark" runat="server" Width="50px" /> % in 
        <asp:DropDownList ID="DL_New_EntReq" runat="server" DataSourceID="CourseList"
                                                DataTextField="HighSchoolCourseDescription"
                                                DataValueField="HighSchoolCourseID"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="-3">Select a Course</asp:ListItem>
                                </asp:DropDownList>
        <asp:LinkButton ID="Add_EntReq_Button" runat="server" Text="Add" CssClass="admin_button" OnClick ="Add_Ent_Req"/>
        </div>

        <hr />
        <%--POST SECONDARY ENTRANCE REQUIREMENT--%>
        <uc1:MessageUserControl runat="server" id="PSMessageUserControl" />
        <h3>Post-Secondary</h3>
        <p>If this program requires any previous post-secondary credientials, such as a diploma or certificate, add them here.</p>
        <div runat="server" id="PSRequirements">
            <h5>Current Entrance Requirements</h5>
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

            <h5>Add a New Entrance Requirement</h5>
            <div runat="server" id="AddRequirements" class="clearfix">
                <p>Credential Type: </p>
                <asp:DropDownList ID="DL_Credential" runat="server" DataSourceID="CredentialODS" DataTextField="CredentialTypeName" DataValueField="CredentialTypeID"></asp:DropDownList>
                <p>Industry Category: </p>
                <asp:DropDownList ID="DL_Category" runat="server" DataSourceID="CategoryODS" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList>
                <p>Overall GPA: </p>
                <asp:TextBox ID="TB_GPA" runat="server"></asp:TextBox>        
        
                <asp:LinkButton ID="Add_DER" runat="server" CssClass="admin_button2" OnClick="Add_DER_Click">Add Requirement</asp:LinkButton>
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
                <asp:TemplateField HeaderText="CourseEquivalencyID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="CourseEquivalencyID" runat="server" Text='<%# Item.CourseEquivalencyID %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Program Course Code">
                    <ItemTemplate>
                        <asp:Label ID="CourseCode" runat="server" Text='<%# Item.CourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Program Course Name">
                    <ItemTemplate>
                        <asp:Label ID="CourseName" runat="server" Text='<%# Item.CourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Transfer Course Code">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseCode" runat="server" Text='<%# Item.DestinationCourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transfer Course">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseName" runat="server" Text='<%# Item.DestinationCourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>      
                  
                <asp:ButtonField Text="Remove" CommandName="Delete" ControlStyle-CssClass="admin_button"/>
            </Columns>          
            
            <EmptyDataTemplate>
                No Existing Equivalencies.
            </EmptyDataTemplate>
                       
        </asp:GridView>    

        <div runat="server" id="addNewEquivalency" visible="true"  class="add-equivalency-block clearfix">
            <div class="clearfix">
                <asp:Label ID="EmptyCurrent" runat="server" Text="Program course " CssClass="col-4 label"></asp:Label>
        
                <asp:DropDownList ID="EmptyCurrentDropdown" runat="server" DataSourceID="EmptyCurrentDropdownODS" 
                    DataTextField="CourseName" DataValueField="CourseID" AppendDataBoundItems="True" CssClass="col-5">
                     <asp:ListItem Value="-1">[Select Course]</asp:ListItem>
                </asp:DropDownList> 
                <div class="clearfix">
                    <asp:Label ID="EmptyEquivalent" runat="server" Text="--Gets Credit From--" CssClass="col-12 center-bold"></asp:Label>  
                </div>
                     
                <div class="clearfix">     
                    <asp:Label ID="ProgEquivalent" runat="server" Text="Program: " CssClass="col-4 label"></asp:Label>

                    <asp:DropDownList ID="EmptyEquivalentProgram" runat="server" DataSourceID="ProgramODS" DataTextField="ProgramName" DataValueField="ProgramID" OnSelectedIndexChanged="EmptyEquivalentProgram_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true" CssClass="col-5">
                        <asp:ListItem Value="-1">[Select Program]</asp:ListItem>
                    </asp:DropDownList>
                </div>
                     
                <div class="clearfix"> 

                    <asp:Label ID="CourseEquivalent" runat="server" Text="Course: " CssClass="col-4 label"></asp:Label>
                    <asp:DropDownList ID="EquivalentCourseID" runat="server" DataSourceID="CourseByProgramODS" DataTextField="CourseName" DataValueField="CourseID" AppendDataBoundItems="True" AutoPostBack="true" CssClass="col-5">
                        <asp:ListItem Value="-1">[Select Course]</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-12">
                    <asp:LinkButton ID="Enter" runat="server" OnClick="Enter_Click" CssClass="admin_button admin_button_centered submit">Add Equivalency</asp:LinkButton>
                </div>
            </div>
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
        
    </div> <%--end program edit div--%>
      <%-- ----------------------------- SEARCH ---------------------------------------%>
     <hr />
    <div runat="server" id="search" class="search-bar" >
        Program Search: <asp:TextBox ID="Search_Box" runat="server" Width="200px"></asp:TextBox> in
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
                        <asp:LinkButton ID="EditButton" runat="server" CommandArgument='<%#Eval("ProgramID") %>' OnClick="Get_Program_Info" Text="Edit" CssClass="admin_button" />
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

    <asp:ObjectDataSource ID="ODS_SubjectRequirement" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_SubjectRequirements" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>
</asp:Content>