<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentPreferences.aspx.cs" Inherits="Student_StudentPreferences" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />


    <!--Get student's program information-->
        <div runat="server" id="stepOne" visible="true" class="clearfix">
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
            <asp:LinkButton ID="stepOneNext" runat="server" OnClick="stepOneNext_Click" CssClass="button next">Next</asp:LinkButton>
        </div>

    <!-- get student preference questions -->
        <div runat="server" id="step2" visible="false">
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
            <asp:LinkButton ID="previous" runat="server" OnClick="onPreviousClick" CssClass ="button back">Previous</asp:LinkButton>
            <asp:LinkButton ID="stepTwoNext" runat="server" OnClick="stepTwoNext_Click" CssClass="button next">Next</asp:LinkButton>
            </div>



            <asp:ObjectDataSource ID="QuestionDataSource"
                SelectMethod="GetQuestions"
                TypeName="crystalBallSystem.BLL.StudentController"
                runat="server" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

    <!-- student course selection section -->
    <div id="stepThree" runat="server" visible="false">
    <h1>Select the Courses You've Taken</h1>

        <asp:CheckBoxList ID="CB_CourseList" runat="server" DataSourceID="CourseList" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID" RepeatColumns="4" CellPadding="5" CssClass="courseCSS">
        </asp:CheckBoxList>

            <asp:LinkButton ID="stepThreePrevious" runat="server" OnClick="stepThreePrevious_Click" CssClass="button back">Previous</asp:LinkButton>
            <!--<asp:Button ID="submit" runat="server" Text="Submit" OnClick="Submit_Click" />
            <script type="text/javascript">
                $("#submit").click(function (e){
                    e.preventDefault();
                });
            </script>-->        


            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Submit_Click" CssClass="button submit">Submit</asp:LinkButton>

        </div>


          <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController" ></asp:ObjectDataSource>
        <div id="results" runat="server" visible="false" >
            <h1>Results</h1>
            <asp:GridView ID="ResultsView" runat="server" CssClass="program-search-results"></asp:GridView>
            <asp:LinkButton ID="searchAgain" runat="server" OnClick="searchAgain_Click" CssClass="button submit button-long">Search Again</asp:LinkButton>
        </div>
        


</asp:Content>

