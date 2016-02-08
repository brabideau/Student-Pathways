<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudentPreferences.aspx.cs" Inherits="Student_StudentPreferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

        <div runat="server" align="center">
        <h1 align="center">Your Preferences</h1>

          
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="QuestionDataSource">
                <Columns>
                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" SortExpression="QuestionID"></asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                </Columns>
            </asp:GridView>

            </div>




            <asp:ObjectDataSource ID="QuestionDataSource"
                SelectMethod="GetQuestions"
                TypeName="crystalBallSystem.BLL.StudentController"
                runat="server" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

</asp:Content>

