<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentPreferences.aspx.cs" Inherits="Student_StudentPreferences" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <!-- step 1 - high school courses -->
    <!-- student course selection section -->
    <div id="stepOne" runat="server" visible="true">
    <h1>Your High School Courses</h1>
        <h3>Check off all the high school courses you've taken</h3>
        <p>This will help us show you programs for which you meet the minimum entrance requirements.</p>
        <asp:CheckBoxList ID="CB_CourseList" runat="server" DataSourceID="CourseList" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID" CssClass="courseCSS clearfix">
        </asp:CheckBoxList>

            <asp:LinkButton ID="stepOneNext" runat="server" OnClick="stepOneNext_Click" CssClass="button next">Next</asp:LinkButton>
        <p>Have you graduated from a post secondary institution?</p>
        <asp:RadioButtonList ID="RBL_GraduatedPostSecondary" runat="server" OnSelectedIndexChanged="RBL_GraduatedPostSecondary_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="true">Yes</asp:ListItem>
            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        <div id="graduated" runat="server" visible="false">
        <asp:DropDownList ID="DDL_ProgramCategory" runat="server" DataSourceID="ODS_Category" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList>
        <asp:ObjectDataSource ID="ODS_Category" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:DropDownList ID="DDL_CredentialType" runat="server" DataSourceID="ODS_Credential" DataTextField="CredentialTypeName" DataValueField="CredentialTypeID"></asp:DropDownList>
        <asp:ObjectDataSource ID="ODS_Credential" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:TextBox ID="TB_GPA" runat="server"></asp:TextBox>
        </div>
        
        </div>
    <!-- step 2 - preference questions -->
    <!-- get student preference questions -->
        <div runat="server" id="stepTwo" visible="false">
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

            <asp:LinkButton ID="stepTwoPrevious" runat="server" OnClick="stepTwoPrevious_Click" CssClass ="button back">Previous</asp:LinkButton>
            <asp:LinkButton ID="stepTwoNext" runat="server" OnClick="stepTwoNext_Click" CssClass="button next">Next</asp:LinkButton>
            </div>



            <asp:ObjectDataSource ID="QuestionDataSource"
                SelectMethod="GetQuestions"
                TypeName="CrystalBallSystem.BLL.StudentController"
                runat="server" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <!-- step 3 - metrics / nait student questions -->
    <!--Get student's program information-->
        <div runat="server" id="stepThree" visible="false" class="clearfix">
            <p>Have you previously taken courses at NAIT?<asp:RadioButtonList ID="RBL_NAIT_Student" runat="server" OnSelectedIndexChanged ="CurrentStudent_CheckedChanged" AutoPostBack="True"><asp:ListItem Value="1" Selected="True">Yes</asp:ListItem><asp:ListItem Value="0">No</asp:ListItem></asp:RadioButtonList></p>
            
            <div runat="server" id="chooseProgram" class="clearfix">
                <p>If you have taken courses at NAIT, you may be eligible for advanced or transfer credit to other programs.</p>
                <p>Select Program Category: 
        <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="GetProgramCategory" DataTextField="CategoryDescription" DataValueField="CategoryID" OnSelectedIndexChanged="Populate_Program" AutoPostBack="true" AppendDataBoundItems="True">

            
            <asp:ListItem Selected="True" Value="0">[Select a Category] </asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="GetProgramCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
                </p>

                <p>Select Current Program: 
        <asp:DropDownList ID="ProgramDropDown" runat="server" DataTextField="ProgramName" DataValueField="ProgramID">
            
        </asp:DropDownList>
                    <asp:ObjectDataSource ID="GetProgram" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CategoryDropDown" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </p>
                <p>Select Which Year You Are In: 
        <asp:DropDownList ID="SemesterDropDown" runat="server">
            <asp:ListItem Text="First" Value="1" />
            <asp:ListItem Text="Second" Value="2" />
            <asp:ListItem Text="Third" Value="3" />
            <asp:ListItem Text="Fourth" Value="4" />
        </asp:DropDownList></p>
                <p>Do you wish to continue your chosen program stream?<span style="margin-right: 15px;"></span><asp:RadioButtonList ID="RBL_SwapPrograms" runat="server"><asp:ListItem Value="true" Selected="True">Continue</asp:ListItem><asp:ListItem Value="false">Switch</asp:ListItem></asp:RadioButtonList></p>
            </div>
            
            <asp:LinkButton ID="stepThreeNext" runat="server" OnClick="stepThreeNext_Click" CssClass="button next">Next</asp:LinkButton>
            <asp:LinkButton ID="stepThreePrevious" runat="server" OnClick="stepThreePrevious_Click" CssClass="button back">Previous</asp:LinkButton>
        </div>
    <!-- step 4 - nait courses -->
    <div id="stepFour" runat="server" visible="false">
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
                <%--<asp:ListItem  Value=-1 Text="[---------------]" />--%>
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
        <!-- DataSourceID="NaitCourseODB" -->
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

                <%--<asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" Visible="false"/>
                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
                <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
                <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />--%>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
           
            <EmptyDataTemplate>
                No data found.

            </EmptyDataTemplate>
            <%--<PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="--&gt;" PageButtonCount="5" PreviousPageText="&lt;--" />--%>
        </asp:GridView>
   </div>

    <div class ="col-6 nait-courses">
        <h3>My Courses</h3>
        <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand" >
        <ItemTemplate>    
            <div class="inner-rpt-div">
                <span><h6><%# Eval("CourseCode") %></h6></span>
                <span><%# Eval("CourseName") %></span>
                <%--<span>credit: <%# Eval("CourseCredits") %></span>--%>
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
        
        <asp:LinkButton ID="Next" runat="server" OnClick="Submit_Click" CssClass="button next button-long" >Next: See Results</asp:LinkButton>
        <asp:LinkButton ID="stepFourPrevious" runat="server" OnClick="stepFourPrevious_Click" CssClass="button back">Previous</asp:LinkButton>
       
    </div>

        <asp:ObjectDataSource ID="SelectProgramODB" runat="server" 
            SelectMethod="GetProgram" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" 
            OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="NaitCourseODB" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="SearchNaitCourses"
             TypeName="CrystalBallSystem.BLL.SelectNaitCourseController">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchInfo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ProgramDropDownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SelectedCourseODB" 
            runat="server" SelectMethod="SelectedNaitCourses"
             TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" 
            OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CourseGridView" Name="courseID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <!-- step 5 - results -->
    <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController" ></asp:ObjectDataSource>
        <div id="results" runat="server" visible="false" >
            <h1>Results</h1>
            <asp:LinkButton ID="searchAgain" runat="server" OnClick="searchAgain_Click" CssClass="button submit button-long">Search Again</asp:LinkButton>
            <asp:ListView ID="ResultsView" runat="server" >
                        <LayoutTemplate>
                            <table>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>
                <ItemTemplate>
                    <tr class="program-search-results">
                        <td>
                           <h3><asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' /></h3>
                        </td>
                        <td class="clearfix">
                            <ul>
                                <li><asp:Label ID="MatchPercentLabel" runat="server" Text='<%# Eval("MatchPercent") %>' />% match</li>
                                <li><asp:Label ID="CredentialTypeLabel" runat="server" Text='<%# Eval("CredType") %>' /></li>
                            </ul>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Visible=<%# Eval("Credits") != null %>> <p>You may qualify for <asp:Label ID="Label3" runat="server" Text='<%# Eval("Credits") %>' /> credits towards this program</p></asp:Label></td><td>
                           <p><asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' /></p>
                        </td>
                        <td>
                            <asp:HyperLink ID="ProgramLinkButton" NavigateUrl='<%# Eval("ProgramLink") %>' runat="server" Target="_blank"><span class="button submit button-long">Learn More</span></asp:HyperLink></td></tr></ItemTemplate></asp:ListView></div></ContentTemplate></asp:UpdatePanel></asp:Content>