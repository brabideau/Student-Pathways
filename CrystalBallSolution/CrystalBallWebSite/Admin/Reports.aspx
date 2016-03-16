<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Reports</h1>



        <div class="search-bar col-6">
         Year:
        <asp:DropDownList ID="DL_Year" runat="server">
            <asp:ListItem Text="Year" Value="-1"/>
            <asp:ListItem Text="2015" Value="2015" />
            <asp:ListItem Text="2016" Value="2016" />
            <asp:ListItem Text="2017" Value="2017" />
        </asp:DropDownList>

        Month:
        <asp:DropDownList ID="DL_Month" runat="server">
            <asp:ListItem Text="Month" Value="-1"/>
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

    </div>

    <div id="ProgramData" runat="server" visible="true" class="clearfix">
        <div class="col-12">
       <asp:LinkButton ID="Program_Search_Button" runat="server" OnClick="Program_Submit_Click" CssClass="button submit">Search</asp:LinkButton>
        </div>
        <div class="col-6 nait-courses">
            <asp:GridView ID="GV_Program_Dropping" runat="server"></asp:GridView>
        </div>

        <div class="col-6 nait-courses">
            <asp:GridView ID="GV_ProgramFrequency" runat="server"></asp:GridView>
        </div>
     </div>
            



    <div id="UserData" runat="server" visible="false">
  <%----------------------------------- Filters -------------------------------------%>
    <div class="clearfix">
     <div class="search-bar">
       

        Program:
        <asp:DropDownList ID="DL_Program" runat="server" DataSourceID="ProgramListDataSource" DataTextField="ProgramName" DataValueField="ProgramID" AppendDataBoundItems="true">
            <asp:ListItem  Value=-1>Prospective Students</asp:ListItem>
            <asp:ListItem  Value=0>All Programs</asp:ListItem>
        </asp:DropDownList>
        </div>

    <div class="search-bar">
       Semester:
       <asp:DropDownList ID="DL_Semester" runat="server">
            <asp:ListItem Text="Unspecified" Value="-1" />
            <asp:ListItem Text="1" Value="1" />
            <asp:ListItem Text="2" Value="2" />
            <asp:ListItem Text="3" Value="3" />
            <asp:ListItem Text="4" Value="4" />
            <asp:ListItem Text="5" Value="5" />
            <asp:ListItem Text="6" Value="6" />
            <asp:ListItem Text="7" Value="7" />
            <asp:ListItem Text="8" Value="8" />
        </asp:DropDownList>

        Switching Programs:
        <asp:DropDownList ID="DL_Change" runat="server">
            <asp:ListItem Text="Unspecified" Value="-1"/>
            <asp:ListItem Text="Yes" Value="1" />
            <asp:ListItem Text="No" Value="0" />
        </asp:DropDownList>
    </div>
    <div class="col-5">
        <asp:LinkButton ID="Search_Left" runat="server" OnClick="Submit_Click" CssClass="button submit">Search</asp:LinkButton>
        <h3>Results</h3>
    </div>
    <div class="col-5">
        <asp:LinkButton ID="Search_Right" runat="server" OnClick="Submit_Click" CssClass="button submit">Search</asp:LinkButton>
        <h3>Results</h3>
    </div>
    </div>

 <%----------------------------------- DATA -------------------------------------%>
    <div class="col-5">
        
        <asp:GridView ID="GV_PreferenceSummaries_Left" runat="server"></asp:GridView>
    </div>
    <div class="col-5">
        
        <asp:GridView ID="GV_PreferenceSummaries_Right" runat="server"></asp:GridView>
    </div>
    <div class="col-2">
        <asp:GridView ID="GV_Compare" runat="server"></asp:GridView>
    </div>
</div>

  <%----------------------------------- ODS -------------------------------------%>

    <asp:ObjectDataSource ID="ProgramListDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetPrograms" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>



</asp:Content>



