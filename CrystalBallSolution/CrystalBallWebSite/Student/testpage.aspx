<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="Student_testpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div>
         <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button ID="Button2" runat="server" Text="Generated" OnClick="Button2_Click" />
    </div>
    <div>
        <h1>All the programs</h1>
        <asp:GridView ID="ProgramGV" runat="server"></asp:GridView>
    </div>
    <div>
        <asp:Repeater ID="rptCourse" runat="server" >
        <ItemTemplate>    
            <div >
                <span><%# Eval("Program") %></span>
                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            </div>      
        </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:GridView ID="pcMatchGv" runat="server"></asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="Back" OnClick="Button1_Click" />
</asp:Content>

