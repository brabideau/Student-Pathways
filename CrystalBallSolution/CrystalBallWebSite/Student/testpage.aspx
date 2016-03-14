<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="Student_testpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div>
        <asp:Repeater ID="rptProgram" runat="server"  >
        <ItemTemplate>    
            <div >
                <h1>
                    <%--<span><%# Eval("ProgramID") %></span>--%>
                    <span><%# Eval("ProgramName") %></span>
                    
                </h1>
                
                <asp:Repeater ID="rptCourse" runat="server" DataSource ='<%# Eval("ProgramCourseMatch") %>'>
                    
                    <ItemTemplate>    
                        <div >
                            <h5>
                                <%--<span><%# Eval("CourseID") %></span>--%>
                                <%--<span><%# Eval("CourseCode") %></span>--%>
                                <span><%# Eval("CourseName") %></span>
                                <asp:Label ID="Label1" runat="server" Text=""><%# Eval("CourseCredits") %></asp:Label>
                                <%--<span></span>--%> 
                            </h5>
                                                     
                        </div>      
                    </ItemTemplate>
                </asp:Repeater>
                <h3>
                    Total Credit you got for this program:
                    <span><%# Eval("CreditTatol") %></span>/<span><%# Eval("ProgramCreditTotal") %></span>
                </h3>
                
            </div>      
        </ItemTemplate>
        </asp:Repeater>
    </div>
    
    
    <asp:Button ID="Button1" runat="server" Text="Back" OnClick="Button1_Click" />
</asp:Content>

