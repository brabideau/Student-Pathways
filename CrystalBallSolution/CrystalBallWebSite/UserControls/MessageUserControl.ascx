<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageUserControl.ascx.cs" Inherits="UserControls_MessageUserControl" %>
<asp:Panel ID="MessagePanel" runat="server"  style="display:none !important;">
        <asp:Repeater ID="MessageDetailsRepeater" runat="server" EnableViewState="false">       
            <itemtemplate>
                <%Response.Write("<script type=\"text/javascript\">alert('");%><%# Eval("Error")%>  <%Response.Write("');</script>");%>
            </itemtemplate>
        </asp:Repeater>
</asp:Panel>