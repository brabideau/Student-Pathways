<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EquivalencyCheck.aspx.cs" Inherits="AshleyWorkspace_EquivalencyCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="CategoryDescription" DataValueField="CategoryID"></asp:DropDownList>
    <asp:LinkButton ID="CategoryButton" runat="server" OnClick="CButton_Click">Get Programs</asp:LinkButton>
    <asp:DropDownList ID="ProgramDropDown" runat="server" DataSourceID="ProgramODS" DataTextField="ProgramName" DataValueField="ProgramID"></asp:DropDownList>
    <asp:LinkButton ID="ProgramButton" runat="server" OnClick="PButton_Click">Get Courses</asp:LinkButton>
    <asp:DropDownList ID="CourseDropDown" runat="server" DataSourceID="CourseODS" DataTextField="CourseCode" DataValueField="CourseID"></asp:DropDownList>
    <asp:LinkButton ID="Button" runat="server" OnClick="Button_Click">Check Equivalencies</asp:LinkButton>
    
    <asp:GridView ID="GV_Equivalents" runat="server" DataSourceID="EquivalentODS" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" />
            <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
            <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
            <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />
        </Columns>
    </asp:GridView>
        
    
    <asp:ObjectDataSource ID="EquivalentODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Equivalent_Courses" TypeName="CrystalBallSystem.BLL.testController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CourseDropDown" Name="courseids" PropertyName="SelectedValue" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CourseODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCoursesByProgram" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProgramDropDown" Name="programID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetProgramByCategory" TypeName="CrystalBallSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDropDown" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>--%>

    <%--<div>
        <object><embed src="~/PDFs/myPdf.pdf"  type='application/pdf'/></object> 

    </div>--%>
    <object src="~/PDFs/myPdf.pdf" type="application/pdf">
        <embed src="~/PDFs/myPdf.pdf" type="application/pdf" />
    </object>

    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Save</asp:LinkButton>
    </div>
</asp:Content>

