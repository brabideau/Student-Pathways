<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InsertProgram.aspx.cs" Inherits="Admin_InsertProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <h1>Insert Program</h1>

   <h3>Program Information</h3>

        <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Program Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Category:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="CategoryDropdownList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSCategory" DataTextField="CategoryDescription" DataValueField="CategoryID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
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
            <td>
                <asp:Label ID="Label4" runat="server" Text="Program Length:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramLength" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Program Link"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProgramLink" runat="server" Width="332px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Is this program currently in operation?"></asp:Label>&nbsp</td>
            <td>
                <asp:CheckBox ID="Activated" runat="server" />
            </td>
        </tr>
    </table>



                <asp:Label ID="Label7" runat="server" Text="<h3>Entrance Requirements</h3>"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text="Would you like to add entrance requirement courses?"></asp:Label>
                <asp:CheckBox ID="ShowHousesView" runat="server" />

<table>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Competitive Entrance:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CompetitiveEntrance" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Subject Name"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Course Name"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Required Mark"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Subject" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="HighSchoolCourseList" runat="server"
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

    </table>

    <asp:ObjectDataSource ID="ODSCategory" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSHSCourses" runat="server" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

