<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentPreferences.aspx.cs" Inherits="Student_StudentPreferences" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <!-- step 1 - high school courses -->
    <!-- student course selection section -->
    <div id="HighSchoolCourses" runat="server" visible="true">
    <h1>Your Coursework</h1>
         <p>This will help us show you programs for which you meet the minimum entrance requirements.</p>

        <h3>High School Coursework</h3>
       
       <p>Please choose all the high school courses you've taken.</p>
        <asp:CheckBoxList ID="CB_CourseList" runat="server" DataSourceID="CourseList" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID" CssClass="courseCSS clearfix">
        </asp:CheckBoxList>

          <h3>Post-Secondary Credentials</h3>  
        <p>Have you graduated from a post-secondary institution?</p>
        <asp:RadioButtonList ID="RBL_GraduatedPostSecondary" runat="server" OnSelectedIndexChanged="RBL_GraduatedPostSecondary_SelectedIndexChanged" AutoPostBack="True" RepeatLayout="OrderedList" CssClass="radiochecks clearfix">
            <asp:ListItem Value="true">Yes</asp:ListItem>
            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        
        <div id="graduated" runat="server" visible="false">

        <p>What field was your previous program in? <asp:DropDownList ID="DDL_ProgramCategory" runat="server" DataSourceID="ODS_Category" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList></p>

            <asp:ObjectDataSource ID="ODS_Category" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

        <p>What kind of credential did you earn? <asp:DropDownList ID="DDL_CredentialType" runat="server" DataSourceID="ODS_Credential" DataTextField="CredentialTypeName" DataValueField="CredentialTypeID"></asp:DropDownList></p>
        
        
        <asp:ObjectDataSource ID="ODS_Credential" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

        <p>Your GPA in that program: <asp:TextBox ID="TB_GPA" runat="server"></asp:TextBox></p>
        </div>
        <div>
        <hr />
        <asp:LinkButton ID="goToMetrics" runat="server" OnClick="Goto_Metrics" CssClass="button next">Next</asp:LinkButton>
            </div>
        </div>

    <!-- step 2 - metrics / nait student questions -->
    <!--Get student's program information-->
        <div runat="server" id="ProgramMetrics" visible="false" class="clearfix">
            <p>If you have taken any courses at NAIT, you may be eligible for advanced or transfer credit to other programs.</p>

            <p>Are you a current or former NAIT student?<asp:RadioButtonList ID="RBL_NAIT_Student" runat="server" OnSelectedIndexChanged ="CurrentStudent_CheckedChanged" AutoPostBack="True" RepeatLayout="OrderedList" CssClass="radiochecks clearfix"><asp:ListItem Value="1" Selected="True">Yes</asp:ListItem><asp:ListItem Value="0">No</asp:ListItem></asp:RadioButtonList>
                <p>
                </p>
                <div id="chooseProgram" runat="server" class="clearfix">
                    <p>
                        What field is your NAIT program in?
                        <asp:DropDownList ID="CategoryDropDown" runat="server" AppendDataBoundItems="True" AutoPostBack="true" DataSourceID="GetProgramCategory" DataTextField="CategoryDescription" DataValueField="CategoryID" OnSelectedIndexChanged="Populate_Program">
                            <asp:ListItem Selected="True" Value="0">[Select a Category] </asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="GetProgramCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
                    </p>
                    <p>
                        Select your current or most recent program:
                        <asp:DropDownList ID="ProgramDropDown" runat="server" DataTextField="ProgramName" DataValueField="ProgramID">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="GetProgram" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="CategoryDropDown" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </p>
                    <p>
                        What year of studies are you in?
                        <asp:DropDownList ID="SemesterDropDown" runat="server">
                            <asp:ListItem Text="First" Value="1" />
                            <asp:ListItem Text="Second" Value="2" />
                            <asp:ListItem Text="Third" Value="3" />
                            <asp:ListItem Text="Fourth" Value="4" />
                        </asp:DropDownList>
                    </p>
                    <p>
                        Do you wish to continue in your current field, or are you looking to switch to something new?<span style="margin-right: 15px;"></span><asp:RadioButtonList ID="RBL_SwapPrograms" runat="server" CssClass="radiochecks clearfix" RepeatLayout="OrderedList">
                            <asp:ListItem Selected="True" Value="true">Continue</asp:ListItem>
                            <asp:ListItem Value="false">Switch</asp:ListItem>
                        </asp:RadioButtonList>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                    </p>
                </div>
                <asp:LinkButton ID="goToNaitCourses" runat="server" CssClass="button next" OnClick="Goto_NAITCourse">Next</asp:LinkButton>
                <asp:LinkButton ID="MetricsToHSCourse" runat="server" CssClass="button back" OnClick="Show_HSCourses">Previous</asp:LinkButton>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
            
        </div>

    <!-- step 3 - nait courses -->
    <div id="NaitCourses" runat="server" visible="false">
        <h1>Select NAIT Course</h1>

        <div class="search-bar" >
            <label>Filter courses by program</label>
            <br />
            <asp:DropDownList runat="server" ID="ProgramDropDownList" 
                              DataSourceID="SelectProgramODB" 
                              DataTextField="ProgramName" 
                              DataValueField="ProgramID"
                              AppendDataBoundItems="True"
                              OnSelectedIndexChanged="List_Change" AutoPostBack="True" >
                <asp:ListItem  Value=0 Text="[Select All]" />

            </asp:DropDownList>
        </div>
        <div class="search-bar">
            <label >Search by course name or course code</label>
            <br />
            <asp:TextBox ID="SearchTextBox" runat="server" Width="200px"></asp:TextBox><asp:LinkButton ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
        </div>
    <div class="col-6 nait-courses">
        <h3>Nait Course Search</h3>
        <asp:GridView ID="CourseGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseID" OnSelectedIndexChanging="SelectCourses">
            
            
            <Columns>
                <asp:TemplateField HeaderText="CourseID" Visible ="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseID" Text='<%# Eval("CourseID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Course Code" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseCode" Text='<%# Eval("CourseCode") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseName" Text='<%# Eval("CourseName") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credits" Visible ="false" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseCredits" Text='<%# Eval("CourseCredits") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowSelectButton="True" />
            </Columns>
           
            <EmptyDataTemplate>
                No data found.

            </EmptyDataTemplate>
        </asp:GridView>
   </div>

    <div class ="col-6 nait-courses">
        <h3>Your Courses</h3>
        
        <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand" >
        <ItemTemplate>    
            <div class="inner-rpt-div">
                <span><h6><%# Eval("CourseCode") %></h6></span>
                <span><%# Eval("CourseName") %></span>
                <span><asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CourseID") %>'>Remove</asp:LinkButton></span>
            </div>      
        </ItemTemplate>
        </asp:Repeater>
    </div>
    
     
        
    <div class="col-12">
        
        <asp:Label ID="TotalCourseLabel" runat="server" Text="Total courses : " Font-Size="Larger"></asp:Label>
         <p><asp:LinkButton ID="reset" runat="server"  CssClass="button back button-long" OnClick="reset_Click" >Clear Courses</asp:LinkButton></p>
</div>
<div class="col-12">
        
        <asp:LinkButton ID="NAITCourseToMetrics" runat="server" OnClick="Show_StudentPrefs" CssClass="button next button-long" >Next</asp:LinkButton>
        <asp:LinkButton ID="NAITCourseToPrefs" runat="server" OnClick="Goto_Metrics_ClearData" CssClass="button back">Previous</asp:LinkButton>
       
    </div>

        <asp:ObjectDataSource ID="SelectProgramODB" runat="server" 
            SelectMethod="GetProgram" TypeName="CrystalBallSystem.BLL.StudentController" 
            OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="NaitCourseODB" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="SearchNaitCourses"
             TypeName="CrystalBallSystem.BLL.StudentController">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchInfo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ProgramDropDownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SelectedCourseODB" 
            runat="server" SelectMethod="SelectedNaitCourses"
             TypeName="CrystalBallSystem.BLL.StudentController" 
            OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CourseGridView" Name="courseID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <!-- step 4 - preference questions -->
    <!-- get student preference questions -->
        <div runat="server" id="StudentPrefs" visible="false">
        <h1>Your Preferences</h1>
            <p>This will help us match you with programs you'll enjoy!</p>
          <asp:RadioButtonList ID="PrefQuestions" 
              runat="server" 
              DataSourceID="QuestionDataSource" 
              DataTextField="Description" 
              DataValueField="QuestionID"
              Visible="false">
              <asp:ListItem Text="1" Value="1">

              </asp:ListItem>
              <asp:ListItem Text="2" Value="2">

              </asp:ListItem>
              <asp:ListItem Text="3" Value="3">

              </asp:ListItem>
              <asp:ListItem Text="4" Value="4">

              </asp:ListItem>
              <asp:ListItem Text="5" Value="5">

              </asp:ListItem>
        </asp:RadioButtonList>
            <asp:GridView ID="prefGridView" runat="server" DataSourceID="QuestionDataSource" AutoGenerateColumns="False" CssClass="prefQuestionsCSS clearfix">
                <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="QuestionID" runat="server" Visible="false" Text='<%# Eval("QuestionID") %>'></asp:Label>
                   </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" SortExpression="Description" />
                    <asp:TemplateField AccessibleHeaderText="Preference">
                        <ItemTemplate>
                        <asp:RadioButtonList ID="prefSelection" runat="server" RepeatLayout="OrderedList">
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
            <asp:LinkButton ID="PrefsToNAITCourse" runat="server" OnClick="Prefs_To_NAITCourse" CssClass ="button back">Previous</asp:LinkButton>
            <asp:LinkButton ID="PrefsToResults" runat="server" OnClick="Submit_Click" CssClass="button next">Next</asp:LinkButton>
            </div>



            <asp:ObjectDataSource ID="QuestionDataSource"
                SelectMethod="GetQuestions"
                TypeName="CrystalBallSystem.BLL.StudentController"
                runat="server" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

    <!-- step 5 - results -->
    <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController" ></asp:ObjectDataSource>
        <div id="ResultsList" runat="server" visible="false" >
            <h1>Your Program Matches</h1>
            <asp:LinkButton ID="searchAgain" runat="server" OnClick="searchAgain_Click" CssClass="button submit button-long">Search Again</asp:LinkButton>
            <div class="resultsviewscroll clearfix">
            <asp:ListView ID="ResultsView" runat="server" OnPagePropertiesChanging="ResultsView_PagePropertiesChanged">
                        <LayoutTemplate>
                            <table>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                            <asp:DataPager runat="server" ID="DataPager" PageSize="8">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                            ShowNextPageButton="false" />
                        <asp:NumericPagerField ButtonType="Link" />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />
                                </Fields>
                            </asp:DataPager>
                        </LayoutTemplate>
                <ItemTemplate>
                    <tr class="program-search-results">
                        <td>
                           <h3><asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' /></h3>
                        </td>
                        <td class="clearfix">
                            <ul>
                                <li><asp:Label ID="MatchPercentLabel" runat="server" Text='<%# Eval("MatchPercent") %>' />% Match to your preferences</li>
                                <li><asp:Label ID="CredentialTypeLabel" runat="server" Text='<%# Eval("CredType") %>' /></li>
                            </ul>
                        </td>
                        <td>
                            <asp:Label ID="ResultsCreditsLabel" runat="server" Visible=<%# Convert.ToBoolean(Eval("Credits")) %>> <p>You may qualify for <asp:Label ID="Label3" runat="server" Text='<%# Eval("Credits") %>' /> credits towards this program</p></asp:Label></td><td>
                           <p><asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' /></p>
                        </td>
                        <td>
                            <asp:HyperLink ID="ProgramLinkButton" NavigateUrl='<%# Eval("ProgramLink") %>' runat="server" Target="_blank">
                <span class="button submit button-long">Learn More</span></asp:HyperLink></td></tr></ItemTemplate></asp:ListView></div></div></ContentTemplate></asp:UpdatePanel></asp:Content>