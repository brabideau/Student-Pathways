<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InsertProgram.aspx.cs" Inherits="Admin_InsertProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .panel-heading{
            background-color:cornflowerblue;
            color:white;
            font-size:22px;
            padding:5px;
        }
        .auto-style1 {
            width: 70%;
            margin:10px;
        }
        .auto-style2 {
            height: 15px;
        }
        .auto-style3 {
            width: 299px;
        }
        .auto-style4 {
            height: 15px;
            width: 299px;
        }
        .auto-style6 {
            width: 626px;
        }
        .auto-style7 {
            width: 427px;
        }
        .auto-style8 {
            margin-right: 107px;
        }
        .auto-style9 {
            width: 496px;
        }
    </style>
    <h1>Insert Program</h1>
    <table class="auto-style1" align="center">
        <tr>
            <td colspan="2" class="panel-heading">Program Information</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label1" runat="server" Text="Program Nmae:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label2" runat="server" Text="Category:"></asp:Label>
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="CategoryDropdownList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSCategory" DataTextField="CategoryDescription" DataValueField="CategoryID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label3" runat="server" Text="Credential Type:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="CredentialDropdownList" runat="server">
                    <asp:ListItem Value="0">[Select Credential]</asp:ListItem>
                    <asp:ListItem>Certification</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    <asp:ListItem>Bachelor</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label4" runat="server" Text="Program Length:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramLength" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label5" runat="server" Text="Program Link"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramLink" runat="server" Width="332px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label6" runat="server" Text="Is this program currently in operation?"></asp:Label>&nbsp</td>
            <td>
                <asp:CheckBox ID="Activated" runat="server" />
            </td>
        </tr>
    </table>

    <table class="auto-style1" align="center">
        <tr>
            <td colspan="3" class="panel-heading">
                <asp:Label ID="Label7" runat="server" Text="Entrance Requirements"></asp:Label>
            </td>
            
        </tr>

        <tr>
            <td class="auto-style7" colspan="3">
                <asp:Label ID="Label9" runat="server" Text="Would you like to add entrance requirement courses?"></asp:Label>
                <asp:CheckBox ID="ShowHousesView" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="auto-style7">
                <asp:Label ID="Label8" runat="server" Text="Competitive Entrance:"></asp:Label>
            </td>
            <td class="auto-style9">
                <asp:TextBox ID="CompetitiveEntrance" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr align="center">
            <td class="auto-style7">
                <asp:Label ID="Label10" runat="server" Text="Subject Name"></asp:Label>
            </td>
            <td class="auto-style9">
                <asp:Label ID="Label11" runat="server" Text="Course Name"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Required Mark"></asp:Label>
            </td>
        </tr>
        <tr align="center"; style="margin-top:20px;font-style:">
            <td class="auto-style7">
                <asp:Label ID="Subject" runat="server"></asp:Label>
            </td>
            <td class="auto-style9">
                <asp:DropDownList ID="HighSchoolCourseList" runat="server" CssClass="auto-style8" 
                                  DataSourceID="ODSHSCourses" 
                                  DataTextField="HighSchoolCourseName" 
                                  DataValueField="HighSchoolCourseID"
                                  AppendDataBoundItems="true">
                    <asp:ListItem Value="0">[Select Course]</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="Mark" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style9">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style9">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style9">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSHSCourses" runat="server" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

