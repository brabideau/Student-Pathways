<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Reports</h1>


    <asp:UpdatePanel ID="ButtonsPanel" runat="server">
        <ContentTemplate>

     <asp:RadioButtonList ID="Tab_Labels" runat="server" RepeatLayout="OrderedList" CssClass="tabs clearfix" OnSelectedIndexChanged="Change_Tab" AutoPostBack="true">
            <asp:ListItem Value="1" Text="Program Data" Selected="true"></asp:ListItem>
            <asp:ListItem Value="2" Text="Student Data"></asp:ListItem>
            
        </asp:RadioButtonList>


        <div class="search-bar col-6">

             Month:
        <asp:DropDownList ID="DL_Month" runat="server">
            <asp:ListItem Text="Any Month" Value="-1"/>
            <asp:ListItem Text="January" Value="1" />
            <asp:ListItem Text="February" Value="2" />
            <asp:ListItem Text="March" Value="3" />
            <asp:ListItem Text="April" Value="4" />
            <asp:ListItem Text="May" Value="5" />
            <asp:ListItem Text="June" Value="6" />
            <asp:ListItem Text="July" Value="7" />
            <asp:ListItem Text="August" Value="8" />
            <asp:ListItem Text="September" Value="9" />
            <asp:ListItem Text="October" Value="10" />
            <asp:ListItem Text="November" Value="11" />
            <asp:ListItem Text="December" Value="12" />
        </asp:DropDownList>

         Year:
        <asp:DropDownList ID="DL_Year" runat="server">
            <asp:ListItem Text="Any Year" Value="-1"/>
            <asp:ListItem Text="2015" Value="2015" />
            <asp:ListItem Text="2016" Value="2016" />
            <asp:ListItem Text="2017" Value="2017" />
        </asp:DropDownList>


    </div><%-- end search bar--%>

       <div id="ProgramData" runat="server" class="clearfix" visible="true">
        <div class="col-12">
       <asp:LinkButton ID="Program_Search_Button" runat="server" OnClick="Program_Submit_Click" CssClass="button submit">Search</asp:LinkButton>
            <p></td>You are viewing data for: <asp:Label ID="Program_Month_Label" runat="server" Text=""></asp:Label>, <asp:Label ID="Program_Year_Label" runat="server" Text=""></asp:Label></p>

        </div>
 
        <div class="col-6 nait-courses">
            <asp:ListView ID="LV_ProgramFrequency" runat="server">
                <LayoutTemplate>
                    <table>
                        <thead>
                            <th>Program</th>
                            <th>Frequency</th>
                        </thead>
                        <tbody>
                        <tr id="itemPlaceholder" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>        
                <ItemTemplate>
                        <tr>
                            <td><asp:Label ID="ProgramLabel" runat="server" Text='<%# Eval("Program") %>' /></td>
                            <td><asp:Label ID="FrequencyLabel" runat="server" Text='<%# Eval("Frequency") %>' /></td>
                        </tr>
                </ItemTemplate>
            </asp:ListView>

        </div>

        <div class="col-6 nait-courses">
            <asp:GridView ID="GV_Program_Dropping" runat="server"></asp:GridView>
        </div>
        <div class="col-12">
           <asp:LinkButton ID="Program_Pdf_Button" runat="server" OnClick="Program_PDF_Download">Get PDF of results</asp:LinkButton>
            </div>

     </div><%-- end ProgramData--%>
            
     

       <div id="StudentData" runat="server" class="clearfix" visible="false">
  <%----------------------------------- Filters -------------------------------------%>
    <div class="clearfix">
     <div class="search-bar">
       

        Program:
        <asp:DropDownList ID="DL_Program" runat="server" DataSourceID="ProgramListDataSource" DataTextField="ProgramName" DataValueField="ProgramID" AppendDataBoundItems="true" OnSelectedIndexChanged="NewStudents_Check" AutoPostBack="true">
            <asp:ListItem  Value=-1>Prospective Students</asp:ListItem>
            <asp:ListItem  Value=0>All Programs</asp:ListItem>
        </asp:DropDownList>
        </div>

    <div class="search-bar">
       Year of Study:
       <asp:DropDownList ID="DL_Semester" runat="server" Enabled="false">
            <asp:ListItem Text="Any Year of Study" Value="0" />
            <asp:ListItem Text="1" Value="1" />
            <asp:ListItem Text="2" Value="2" />
            <asp:ListItem Text="3" Value="3" />
            <asp:ListItem Text="4" Value="4" />
            <asp:ListItem Text="Graduated" Value="-1" />
        </asp:DropDownList>

        Switching Programs:
        <asp:DropDownList ID="DL_Change" runat="server" Enabled="false">
            <asp:ListItem Text="Unspecified" Value="-1"/>
            <asp:ListItem Text="Yes" Value="1" />
            <asp:ListItem Text="No" Value="0" />
        </asp:DropDownList>
    </div>


 <%----------------------------------- DATA -------------------------------------%>
    <div class="clearfix">        
        
        <asp:LinkButton ID="Search_Left" runat="server" OnClick="Submit_Click" CssClass="button submit">Search</asp:LinkButton>

        <p>You are viewing data from: <asp:Label ID="Month_Left" runat="server" Text=""></asp:Label>, <asp:Label ID="Year_Left" runat="server" Text=""></asp:Label></p>
        <p>For <asp:Label ID="Program_Left" runat="server" Text=""></asp:Label></p>
        
        <p><asp:Label ID="Semester_Left" runat="server" Text=""></asp:Label></p>
        <p><asp:Label ID="Dropping_Left" runat="server" Text=""></asp:Label></p>
    </div>


    <div>
        <asp:ListView ID="LV_PreferenceSummaries_Left" runat="server">
               <LayoutTemplate>
                    <table>
                        <tr>
                            <th></th>
                            <th>Definitely Not</th>
                            <th>No</th>
                            <th>Neutral</th>
                            <th>Yes</th>
                            <th>Definitely</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
               </LayoutTemplate>
               <ItemTemplate> 
                    <tr>
                        <td runat="server">
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DefinitelyNot") %>'></asp:Label> %
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("No") %>'></asp:Label> %
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("DontKnow") %>'></asp:Label> %
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Yes") %>'></asp:Label> %
                        </td>
                        <td runat="server">
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Definitely") %>'></asp:Label> %
                        </td>
                    </tr>
                </ItemTemplate>
           </asp:ListView>
    </div>

      
</div> <%--end clearfix div--%>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>


  <%----------------------------------- ODS -------------------------------------%>

    <asp:ObjectDataSource ID="ProgramListDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetPrograms" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="QuestionDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Question_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

</asp:Content>



