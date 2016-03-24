<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditSubjectRequirement.aspx.cs" Inherits="Admin_EditSubjectRequirement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <!-- Select Subject Requirement-->
    <asp:DropDownList ID="SubjectRequirement" runat="server" DataSourceID="SubjectReqODS" DataTextField="SubjectDescription" DataValueField="SubjectRequirementID" OnSelectedIndexChanged="SubjectRequirement_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>

    <!--Display Courses in Subject Requirement-->
    <asp:GridView ID="SRCourses" runat="server" AutoGenerateColumns="False" DataSourceID="SRCourseODS">
        <Columns>
            <asp:BoundField DataField="HSCourseID" HeaderText="Course ID" SortExpression="HSCourseID" />
            <asp:BoundField DataField="HSCourseName" HeaderText="Course Name" SortExpression="HSCourseName" />
            <asp:BoundField DataField="Mark" HeaderText="Mark" NullDisplayText="None" SortExpression="Mark" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton ID="SRRemove_Button" runat="server" OnClick="SRRemove_Button_Click">Remove</asp:LinkButton>

    <!-- Add a Course-->
    <asp:Label ID="CourseLabel" runat="server" Text="Course: " CssClass="label col-3"></asp:Label>
        <asp:DropDownList ID="DL_Course" runat="server" AppendDataBoundItems="True" CssClass="col-2" DataSourceID="HSCourseODS" DataTextField="HighSchoolCourseName" DataValueField="HighSchoolCourseID">
             <asp:ListItem Value="-1">[Select Course]</asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="MarkLabel" runat="server" Text="Mark: "  CssClass="label col-3"></asp:Label>
        <asp:TextBox ID="TB_Mark" runat="server" CssClass="col-3"></asp:TextBox>

        <asp:LinkButton ID="Add" runat="server" CssClass="button button-long next" OnClick="Add_Click">Add Course</asp:LinkButton>
        <asp:LinkButton ID="Cancel" runat="server" CssClass="button button-long back">Cancel</asp:LinkButton>



    <asp:ObjectDataSource ID="SubjectReqODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_SubjectRequirements" TypeName="CrystalBallSystem.BLL.testController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SRCourseODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_CoursesBySubjectRequirement" TypeName="CrystalBallSystem.BLL.testController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SubjectRequirement" Name="subjectReqID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="HSCourseODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="HighSchoolCourse_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

