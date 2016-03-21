<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SelectNaitCourses.aspx.cs" Inherits="User_SelectNaitCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Select NAIT Course</h1>

        <div class="search-bar" >
            <label>Please select a program</label>
            <br />
            <asp:DropDownList runat="server" ID="ProgramDropDownList" 
                              DataSourceID="SelectProgramODB" 
                              DataTextField="ProgramName" 
                              DataValueField="ProgramID"
                              AppendDataBoundItems="True" AutoPostBack="True" 
                              OnSelectedIndexChanged="ProgramDropDownList_SelectedIndexChanged"
                               >
                <%--<asp:ListItem  Value=-1 Text="[---------------]" />--%>
                <asp:ListItem  Value=0 Text="[Select All]" />

            </asp:DropDownList>
        </div>
        <div class="search-bar">
            <label >Please search the NAIT course you want.</label>
            <br />
            <asp:TextBox ID="SearchTextBox" runat="server" Width="200px"></asp:TextBox><asp:LinkButton ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
        </div>
    <div class="col-6 nait-courses">
        <asp:GridView ID="CourseGridView" runat="server" 
            AutoGenerateColumns="False" DataSourceID="NaitCourseODB"
            DataKeyNames="CourseID" 
            OnSelectedIndexChanging="SelectCourses"
            >
            
            <Columns>
                <asp:TemplateField HeaderText="CourseID" Visible ="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseID" Text='<%# Eval("CourseID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Course Code" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseCode" Text='<%# Eval("CourseCode") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseName" Text='<%# Eval("CourseName") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credits" Visible ="false" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CourseCredits" Text='<%# Eval("CourseCredits") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:BoundField DataField="CourseID" HeaderText="CourseID" SortExpression="CourseID" Visible="false"/>
                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" />
                <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
                <asp:BoundField DataField="CourseCredits" HeaderText="CourseCredits" SortExpression="CourseCredits" />--%>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
           
            <EmptyDataTemplate>
                No data found.

            </EmptyDataTemplate>
            <%--<PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="--&gt;" PageButtonCount="5" PreviousPageText="&lt;--" />--%>
        </asp:GridView>
   </div>

    <div class ="col-6 nait-courses">
        <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand" >
        <ItemTemplate>    
            <div class="inner-rpt-div">
                <span><%# Eval("CourseCode") %></span>
                <span><%# Eval("CourseName") %></span>
                <%--<span>credit: <%# Eval("CourseCredits") %></span>--%>
                <span><asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CourseID") %>' Text="Delete" /></span>
            </div>      
        </ItemTemplate>
        </asp:Repeater>
    </div>
    
     
        
    <div class="col-12">
        
        <asp:Label ID="TotalCourseLabel" runat="server" Text="Total courses : " Font-Size="Larger"></asp:Label>
        <hr />
        
        <asp:LinkButton ID="Next" runat="server" OnClick="Next_Click" CssClass="button next" >Next</asp:LinkButton>
        <asp:LinkButton ID="reset" runat="server"  CssClass="button next" OnClick="reset_Click" >reset</asp:LinkButton>
    </div>

        <asp:ObjectDataSource ID="SelectProgramODB" runat="server" 
            SelectMethod="GetProgram" TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" 
            OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="NaitCourseODB" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="SearchNaitCourses"
             TypeName="CrystalBallSystem.BLL.SelectNaitCourseController">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchInfo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ProgramDropDownList" Name="programID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="SelectedCourseODB" 
            runat="server" SelectMethod="SelectedNaitCourses"
             TypeName="CrystalBallSystem.BLL.SelectNaitCourseController" 
            OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="CourseGridView" Name="courseID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

</asp:Content>

