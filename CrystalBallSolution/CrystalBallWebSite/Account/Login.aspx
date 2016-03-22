<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>

            <section id="loginForm" class="form-group">
                
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="red-panel">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-4 label">Username</asp:Label>
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="UserName"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="The user name field is required." />
                        </div>

                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-4 label">Password</asp:Label>
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                  
                        <div class="search-bar">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me</asp:Label>
                        </div>
                  
                        <div class="search-bar">
                            <asp:LinkButton runat="server" OnClick="LogIn" CssClass="button submit" >Log In</asp:LinkButton>
                        </div>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                    if you don't have a local account.
                </p>
            </section>


        <%--<div class="col-md-4">
            <section id="socialLoginForm">
                <uc:openauthproviders runat="server" id="OpenAuthLogin" />
            </section>
        </div>--%>

</asp:Content>

