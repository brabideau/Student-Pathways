<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SelectNaitCourses.aspx.cs" Inherits="User_SelectNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Select NAIT Course</h1>
    <div>
        <div style="width: 350px;float:left" >
            <label>Please select a program</label>
            <br />
            <asp:DropDownList runat="server" ID="ProgramDropDownList" 
                              DataSourceID="SelectProgramODB" 
                              DataTextField="ProgramName" 
                              DataValueField="ProgramID"
                              AppendDataBoundItems="true" >
                <asp:ListItem  Value=0 Text="[Select All]" />

            </asp:DropDownList>
        </div>
        <div style="width: 350px;float:left">

            <label >Please search the NAIT course you want.</label>
            <br />
            <asp:TextBox ID="SearchTextBox" runat="server" Width="200px"></asp:TextBox><asp:Button ID="Search" runat="server" Text="Search" />
        </div>
        <div style="clear:both"></div>
    </div>
    <br />
    <div>
        <asp:GridView ID="CourseGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="NaitCourseODB"
            width="80%" Align="Center">
            <Columns>
                <asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" Visible="false"/>
                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
                <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
                <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EmptyDataTemplate>
                No data found.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <div>
        <asp:Repeater  runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <div>
        <asp:ObjectDataSource ID="SelectProgramODB" runat="server" SelectMethod="Program_List" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="NaitCourseODB" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SearchNaitCourses" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchInfo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ProgramDropDownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SelectedCourseODB" runat="server" SelectMethod="SelectedNaitCourses" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CourseGridView" Name="courseID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

