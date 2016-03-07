<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equivalencies.aspx.cs" Inherits="Admin_Equivalencies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
     
        <asp:GridView ID="EquivalenciesGrid" runat="server" AutoGenerateColumns="False"  CssClass="equivalency-grid" ItemType="CrystalBallSystem.DAL.POCOs.GetEquivalencyNames">
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
            </Columns>
            <EmptyDataTemplate>
                No Existing Equivalencies.
                <asp:Button ID="AddNew" runat="server" Text="Add An Equivalency" OnClick="AddNew_Click" CssClass="button"/>
            </EmptyDataTemplate>
        </asp:GridView>    
    </div>
          
    <div runat="server" id="addNewEquivalency" visible="false"  CssClass="add-equivalency-block">
        <asp:Label ID="EmptyCurrent" runat="server" Text="Current Program Course ID: "></asp:Label>
        <asp:TextBox ID="EmptyCurrentTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="CurrentCourseName" runat="server"></asp:Label>
        <asp:Label ID="CurrentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="EmptyEquivalent" runat="server" Text="Equivalent Course ID: "></asp:Label>
        <asp:TextBox ID="EmptyEquivalentTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="EquivalentCourseName" runat="server"></asp:Label>
        <asp:Label ID="EquivalentCourseID" runat="server" Visible="false"></asp:Label>
        <br />
        <asp:Button ID="CheckIDs" runat="server" Text="Check Equivalency" OnClick="CheckIDs_Click" CssClass="button" />
        <asp:Button ID="Enter" runat="server" Text="Enter Equivalency" OnClick="Enter_Click"  CssClass="button" Enabled="false"/>
    </div>

    <asp:ObjectDataSource ID="CategoryODS" runat="server" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropdownList" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="EquivalencyGridODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEquivalencies" TypeName="CrystalBallSystem.BLL.AshleyTestController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProgramDropdownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

