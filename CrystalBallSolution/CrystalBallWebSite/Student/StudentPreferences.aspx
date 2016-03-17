<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentPreferences.aspx.cs" Inherits="Student_StudentPreferences" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <!-- step 1 - high school courses -->
    <!-- student course selection section -->
    <div id="stepOne" runat="server" visible="true">
    <h1>Select the Courses You've Taken</h1>

        <asp:CheckBoxList ID="CB_CourseList" runat="server" DataSourceID="CourseList" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID" RepeatColumns="4" CellPadding="5" CssClass="courseCSS">
        </asp:CheckBoxList>

            <asp:LinkButton ID="stepOneNext" runat="server" OnClick="stepOneNext_Click" CssClass="button submit">Next</asp:LinkButton>

        </div>
    <!-- step 2 - preference questions -->
    <!-- get student preference questions -->
        <div runat="server" id="stepTwo" visible="false">
        <h1>Your Preferences</h1>

          
            <asp:GridView ID="PrefQuestions" runat="server" AutoGenerateColumns="False" DataSourceID="QuestionDataSource" CssClass="prefQuestionsCSS">
                <Columns>
                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" SortExpression="QuestionID"></asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <!-- <asp:DropDownList ID="DL_StudentPreference" runat="server">
                                <asp:ListItem Value="">(no preference)</asp:ListItem>
                                <asp:ListItem Value ="True">Yes</asp:ListItem>
                                <asp:ListItem Value ="False">No</asp:ListItem>
                            </asp:DropDownList> -->
                            <asp:RadioButtonList ID="RBL_YN" runat="server" CssClass="radioButtonList">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
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
                TypeName="crystalBallSystem.BLL.StudentController"
                runat="server" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <!-- step 3 - metrics / nait student questions -->
    <!--Get student's program information-->
        <div runat="server" id="stepThree" visible="false" class="clearfix">
            <p>Are you a current NAIT student?<asp:CheckBox ID="CurrentStudent" runat="server" OnCheckedChanged="CurrentStudent_CheckedChanged" autopostback="true" Checked="true"/></p>
            
            <div runat="server" id="chooseProgram" class="clearfix">
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
                <p>Select Which Semester You Are In: 
        <asp:DropDownList ID="SemesterDropDown" runat="server">
            <asp:ListItem Text="First" Value="1" />
            <asp:ListItem Text="Second" Value="2" />
            <asp:ListItem Text="Third" Value="3" />
            <asp:ListItem Text="Fourth" Value="4" />
            <asp:ListItem Text="Other" Value="5" />
        </asp:DropDownList></p>
                <p>Are you considering switching programs?<span style="margin-right: 15px;"></span><asp:CheckBox ID="ChangeProgram" runat="server" /></p>
            </div>
            
            <asp:LinkButton ID="stepThreeNext" runat="server" OnClick="stepThreeNext_Click" CssClass="button next">Next</asp:LinkButton>
            <asp:LinkButton ID="stepThreePrevious" runat="server" OnClick="stepThreePrevious_Click" CssClass="button next">Previous</asp:LinkButton>
        </div>
    <!-- step 4 - nait courses -->
    <div id="stepFour" runat="server" visible="false">
        <h1>Select NAIT Course</h1>

        <div class="search-bar" >
            <label>Please select a program</label>
            <br />
            <asp:DropDownList runat="server" ID="ProgramDropDownList" 
                              DataSourceID="SelectProgramODB" 
                              DataTextField="ProgramName" 
                              DataValueField="ProgramID"
                              AppendDataBoundItems="True" >
                <%--<asp:ListItem  Value=-1 Text="[---------------]" />--%>
                <asp:ListItem  Value=0 Text="[Select All]" />

            </asp:DropDownList>
        </div>
        <div class="search-bar">
            <label >Please search the NAIT course you want.</label>
            <br />
            <asp:TextBox ID="SearchTextBox" runat="server" Width="200px"></asp:TextBox><asp:LinkButton ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
        </div>
    <div class="col-6 nait-courses">
        <asp:GridView ID="CourseGridView" runat="server" AutoGenerateColumns="False" DataSourceID="NaitCourseODB"
            DataKeyNames="CourseID" OnSelectedIndexChanging="SelectCourses">
            
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
        <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand" >
        <ItemTemplate>    
            <div class="inner-rpt-div">
                <span><%# Eval("CourseCode") %></span>
                <span><%# Eval("CourseName") %></span>
                <%--<span>credit: <%# Eval("CourseCredits") %></span>--%>
                <span><asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CourseID") %>' Text="Delete" /></span>
            </div>      
        </ItemTemplate>
        </asp:Repeater>
    </div>
    
     
        
    <div class="col-12">
        
        <asp:Label ID="TotalCourseLabel" runat="server" Text="Total courses : " Font-Size="Larger"></asp:Label>
        <hr />
        
        <asp:LinkButton ID="Next" runat="server" OnClick="Submit_Click" CssClass="button next" >Submit</asp:LinkButton>
        <asp:LinkButton ID="stepFourPrevious" runat="server" OnClick="stepFourPrevious_Click" CssClass="button next">Previous</asp:LinkButton>
        <asp:LinkButton ID="reset" runat="server"  CssClass="button next" OnClick="reset_Click" >Reset</asp:LinkButton>
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
                        <td>
                           <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Visible=<%# Eval("Credits") != null %>> You may qualify for <asp:Label ID="Label3" runat="server" Text='<%# Eval("Credits") %>' /> credits in this program</asp:Label>
                        </td>
                        <td>
                            <asp:HyperLink ID="ProgramLinkButton" NavigateUrl='<%# Eval("ProgramLink") %>' runat="server"><span class="button submit">More Info</span></asp:HyperLink>
                        </td>
                    </tr>

               </ItemTemplate>
            </asp:ListView>
            <asp:LinkButton ID="searchAgain" runat="server" OnClick="searchAgain_Click" CssClass="button submit button-long">Search Again</asp:LinkButton>
        </div>

</asp:Content>

