<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PreferenceQuestions.aspx.cs" Inherits="AshleyWorkspace_PreferenceQuestions" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div runat="server">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <h2>Preference Questions</h2>
        <!--Check if student will answer questions-->
        <div runat="server" id="step1">
            <p>By answering the following questions, your results can be further narrowed down to display results that better suit your interests.</p>
            <p>Answer questions?<span style="margin-right: 15px;"></span><asp:CheckBox ID="AnswerQuestions" runat="server" checked="true"/></p>
            <asp:LinkButton ID="Button1" runat="server" OnClick="Button1_Click">Next</asp:LinkButton>
        </div>

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

