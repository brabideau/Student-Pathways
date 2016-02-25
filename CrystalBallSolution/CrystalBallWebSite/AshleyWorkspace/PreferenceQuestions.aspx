<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PreferenceQuestions.aspx.cs" Inherits="AshleyWorkspace_PreferenceQuestions" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        body
        {
            width: 964px;
            margin: 0 auto;
        }
        .start
        {
            margin-top: 50px;
        }
        .box
        {
            width:160px;
            height: 350px;
            margin:10px;
            float:left;
            border-right: 1px solid rgba(128, 128, 128, 0.76);
        }
        .box2
        {
            width:220px;
            margin:10px;
            float:left;
        }
        .button
        {            
            float:right;
            margin-right: 40px;
        }
        h2
        {
            margin-left: 20px;
        }
        h1
        {
            text-align: center;
            margin-bottom: 40px;
        }
        label
        {
            font-weight: normal;
        }
        table
        {
            margin-right: 0;
        }
    </style>

    <div id="start" runat="server">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <div runat="server" id="programLevel">
            <p>Select Desired Certification Level: 
    <asp:DropDownList ID="CertificationDropDown" runat="server" DataSourceID="CredentialODS" DataTextField="CredentialTypeName" DataValueField="CredentialTypeID"></asp:DropDownList>*High School Courses are not required if aiming for a degree</p>
            <asp:LinkButton ID="CredentialButton" runat="server" OnClick="Credential_Click">Next</asp:LinkButton>
        </div>

        <!-- Enter Courses Taken-->
        <div runat="server" id="enterCourses" visible="false"> 
            <!--HS Courses-->          
            <h1>Select the Courses You've Taken</h1>                
            <div class="box">
                <h2>English</h2>
                <asp:CheckBoxList ID="EnglishList" runat="server" DataSourceID="EnglishListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
            </div>
            <div class="box">
                <h2>Math</h2>
                <asp:CheckBoxList ID="MathList" runat="server" DataSourceID="MathListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
            </div>
            <div class="box">
                <h2>Science</h2>
                <asp:CheckBoxList ID="ScienceList" runat="server" DataSourceID="ScienceListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
            </div>
            <div class="box">
                <h2>Social</h2>
                <asp:CheckBoxList ID="SocialList" runat="server" DataSourceID="SocialListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
            </div>
            <div class="box2">
                <h2>Other</h2>
                <asp:CheckBoxList ID="OtherList" runat="server" DataSourceID="OtherListODS" DataTextField="HighSchoolCourseDescription" DataValueField="HighSchoolCourseID">
                </asp:CheckBoxList>
            </div>
                <br />
                <br />
            <div runat='server' style="clear:both" id="degree">
                 <p>Select Type of Degree/Diploma You Have: 
                     <asp:DropDownList ID="DiplomaCategoryDropdown" runat="server" DataSourceID="DiplomaCategoryODS" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList></p>
                 <p>Enter Your GPA: 
                     <asp:TextBox ID="GPAText" runat="server"></asp:TextBox></p>
            </div>
            <div style="clear:both"></div>
            <div class="button">
                <asp:Button ID="submitCourseButton" runat="server" Text="Submit Courses" OnClick="submitCourseButton_Click" />
            </div>
            <br />
        </div>  
        <div runat="server" id="step1" visible="false">
            <h2>Preference Questions</h2>
            <!--Check if student will answer questions-->
       
            <p>By answering the following questions, your results can be further narrowed down to display results that better suit your interests.</p>
            <p>Answer questions?<span style="margin-right: 15px;"></span><asp:CheckBox ID="AnswerQuestions" runat="server" checked="true"/></p>
            <asp:LinkButton ID="Button1" runat="server" OnClick="Button1_Click">Next</asp:LinkButton>
        </div>        
        <!--Get desired program level-->


        <!--Get student's program information-->
        <div runat="server" id="stepAlmost2" visible="false">
            <p>Are you a current NAIT student?<span style="margin-right: 15px;"></span><asp:CheckBox ID="CurrentStudent" runat="server" OnCheckedChanged="CurrentStudent_CheckedChanged" autopostback="true" Checked="true"/></p>
            
            <div runat="server" id="chooseProgram">
                <p>Select Program Category: 
        <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList></p>
                <p>Select Current Program: 
        <asp:DropDownList ID="ProgramDropDown" runat="server" DataSourceID="ProgramODS" DataTextField="ProgramName" DataValueField="ProgramID"></asp:DropDownList></p>
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
            <asp:LinkButton ID="ButtonAlmost2" runat="server" OnClick="ButtonAlmost2_Click">Next</asp:LinkButton>
        </div>

        <!--Ask Preference Questions-->
        <div runat="server" id="step2" visible="false">            
            <asp:GridView ID="QuestionGridview" runat="server" AutoGenerateColumns="False" DataSourceID="QuestionODS" BorderStyle="None" GridLines="Horizontal" ShowHeader="False">
                <Columns>
                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" SortExpression="QuestionID" Visible="True" />
                    <asp:BoundField DataField="Question" HeaderText="Question" SortExpression="Question" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="Check" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>            
            <asp:LinkButton ID="Button2" runat="server" OnClick="Button2_Click">Next</asp:LinkButton>
            
        </div>
        <asp:ObjectDataSource ID="CredentialODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCredentialLevels" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="DiplomaCategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="EnglishListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEnglishCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="MathListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetMathCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ScienceListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetScienceCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SocialListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSocialCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OtherListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetOtherCourseList" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="QuestionODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetQuestions" TypeName="CrystalBallSystem.BLL.AshleyTestController"></asp:ObjectDataSource> 
        <asp:ObjectDataSource ID="CategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ProgramODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController">
            <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropDown" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="ReportLabel" runat="server" Visible="false"></asp:Label>
        <br />
    </div>
</asp:Content>

