<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="testPage.aspx.cs" Inherits="Pages_testPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>test</h1>
    <p>add student</p>

        <table class="nav-justified" id="table1">
            <tr>
                <td>email</td>
                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td>Password</td>
                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>                
            </tr>
    </table>


    <asp:Button ID="Button1" runat="server" Text="submit" OnClick="Button1_Click" />

    </asp:Content>

