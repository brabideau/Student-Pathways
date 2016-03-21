<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddNewNaitCourse.aspx.cs" Inherits="Admin_AddNewNaitCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <hr />
    <div class="col-12 nait-courses">
        <asp:DropDownList ID="ProgramDDL" runat="server"
            AppendDataBoundItems="True" DataSourceID="ProgramODS"
             DataTextField="ProgramName" DataValueField="ProgramID" OnSelectedIndexChanged="ProgramDDL_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem  Value=0 Text="[Select All]"/>
            <asp:ListItem  Value=-1 Text="Other"/>
        </asp:DropDownList><asp:LinkButton ID="ViewLinkButton" runat="server">View</asp:LinkButton>

    </div>
    <div class="col-12 nait-courses">
    <asp:GridView ID="CourseGridView" runat="server" AutoGenerateColumns="False" DataSourceID="CourseODS">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" />
            <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
            <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
            <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
        </Columns>
        <EmptyDataTemplate>
            No data found.
        </EmptyDataTemplate>
    </asp:GridView>
    </div>


    <div>
        <table align="center" style="width: 66%">
            
            <tr>
                <td>&nbsp;</td>
                <td>Course Code</td>
                <td>
                    <asp:TextBox ID="CourseCodeTB" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Course Name</td>
                <td>
                    <asp:TextBox ID="CourseNameTB" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Course Credits</td>
                <td>
                    <asp:TextBox ID="CourseCreditsTB" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Active</td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td></td>
                <td>
                   
                    <asp:LinkButton ID="AddLinkButton" runat="server" OnClick="AddLinkButton_Click">Add Course</asp:LinkButton>
                    
                <td><asp:LinkButton ID="AddWithProgramLB" runat="server">Add Course With program</asp:LinkButton></td>
            </tr>
        </table>
    </div>
    <div>
        <table align="center" style="width: 69%">
            <tr>
                <td>&nbsp;</td>
                <td>ProgramID</td>
                <td>
                    <asp:Label ID="ProgramIDLabel" runat="server" Text="" Visible ="false"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
    <div>
    <asp:ObjectDataSource ID="ProgramODS" runat="server" SelectMethod="GetProgram" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CourseODS" runat="server" SelectMethod="NaitCourse_List" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProgramDDL" Name="programid" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
</asp:Content>

