<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equivalencies.aspx.cs" Inherits="Admin_Equivalencies" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl"%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl"  Visible="true"/>
    <div runat="server" id="equivalencyInformation">
        
        <asp:Label ID="Label1" runat="server" Text="Select a Category: "></asp:Label>
        <asp:DropDownList ID="CategoryDropdownList" 
                                runat="server" 
                                DataSourceID="CategoryODS" 
                                DataTextField="CategoryDescription" 
                                DataValueField="CategoryID"
                                AppendDataBoundItems="True" 
                                OnSelectedIndexChanged="Populate_Program" 
                                AutoPostBack="true">
            <asp:ListItem Value="-3">[All]</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="Select a Program: "></asp:Label>
        <asp:DropDownList ID="ProgramDropdownList" 
                            runat="server" 
                            DataTextField="ProgramName" 
                            DataValueField="ProgramID" 
                            OnSelectedIndexChanged="Populate_EquivalenciesGrid" 
                            AutoPostBack="true">
        </asp:DropDownList>
     
        <asp:GridView ID="EquivalenciesGrid" runat="server" AutoGenerateColumns="False"  CssClass="equivalency-grid" ItemType="CrystalBallSystem.DAL.POCOs.GetEquivalencyNames" ShowFooter="True" DataKeyNames="CourseEquivalencyID" OnRowDeleting="EquivalenciesGrid_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="CourseEquivalencyID">
                    <ItemTemplate>
                        <asp:Label ID="CourseEquivalencyID" runat="server" Text='<%# Item.CourseEquivalencyID %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CourseCode">
                    <ItemTemplate>
                        <asp:Label ID="CourseCode" runat="server" Text='<%# Item.CourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CourseName">
                    <ItemTemplate>
                        <asp:Label ID="CourseName" runat="server" Text='<%# Item.CourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="DestinationCourseCode">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseCode" runat="server" Text='<%# Item.DestinationCourseCode %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DestinationCourseName">
                    <ItemTemplate>
                        <asp:Label ID="DestinationCourseName" runat="server" Text='<%# Item.DestinationCourseName %>' />
                    </ItemTemplate>
                </asp:TemplateField>      
                <asp:TemplateField>
                    <FooterTemplate>
                        <asp:LinkButton ID="Add" runat="server" Text="Add Equivalency" OnClick="AddNew_Click" CssClass="button button-long"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>                    
                <asp:ButtonField Text="Remove" CommandName="Delete"/>
            </Columns>          
            
            <EmptyDataTemplate>
                No Existing Equivalencies.
                <asp:Button ID="AddNew" runat="server" Text="Add An Equivalency" OnClick="AddNew_Click" CssClass="button button-long"/>
            </EmptyDataTemplate>
                       
        </asp:GridView>    
    </div>
         
    <!--Add new equivalency to empty gridview--> 
    <div runat="server" id="addNewEquivalency" visible="false"  CssClass="add-equivalency-block">
        <asp:Label ID="EmptyCurrent" runat="server" Text="Current Program Course ID: "></asp:Label>
        <asp:DropDownList ID="EmptyCurrentDropdown" runat="server" DataSourceID="EmptyCurrentDropdownODS" DataTextField="CourseCode" DataValueField="CourseCode" AppendDataBoundItems="true">
             <asp:ListItem Value="-1">[Select Course Code]</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="CurrentCourseName" runat="server"></asp:Label>
        <asp:Label ID="CurrentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="EmptyEquivalent" runat="server" Text="Equivalent Course Code: "></asp:Label>
        <asp:TextBox ID="EmptyEquivalentTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="EquivalentCourseName" runat="server"></asp:Label>
        <asp:Label ID="EquivalentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Button ID="CheckIDs" runat="server" Text="Check Equivalency" OnClick="CheckIDs_Click" CssClass="button button-long" />
        <asp:Button ID="Enter" runat="server" Text="Enter Equivalency" OnClick="Enter_Click"  CssClass="button button-long" Enabled="false"/>
        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click"  CssClass="button button-long"/>
    </div>

    <asp:ObjectDataSource ID="CategoryODS" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropdownList" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="EmptyCurrentDropdownODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProgramDropdownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

