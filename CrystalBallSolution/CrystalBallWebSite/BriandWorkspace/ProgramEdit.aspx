<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProgramEdit.aspx.cs" Inherits="Briand_Workspace_ProgramEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" CssClass="clearfix">
    <h1>Edit Program</h1>
     <%-- ----------------------------- SEARCH ---------------------------------------%>
    <div runat="server" id="search" class="search-bar" >
        <asp:TextBox ID="Search_Box" runat="server"></asp:TextBox>
        <asp:Button ID="Search_Button" runat="server" Text="Search" OnClick="Program_Search"/>
    </div>

     <%-- ----------------------------- PROGRAM LIST ---------------------------------------%>
    <div runat="server" id="ProgramList" visible="true" class="col-12 searchbox">
        <asp:ListView ID="Program_List" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Program Name</th>
                    <th runat="server">Entrance Requirement</th>
                    <th runat="server">Total Credits</th>
                    <th runat="server">Program Length</th>
                    <th runat="server">Competitive Advantage</th>
                    <th runat="server">Active</th>
                    <th runat="server">Program Link</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="EditButton" runat="server" CommandArgument='<%#Eval("ProgramID") %>' OnClick="Populate_Program_Info" Text="Edit" CssClass="admin_button" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramNameLabel" runat="server" Text='<%# Eval("ProgramName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramDescriptionLabel" runat="server" Text='<%# Eval("ProgramDescription") %>' />
                    </td>
                    <td>
                        <asp:Label ID="TotalCreditsLabel" runat="server" Text='<%# Eval("TotalCredits") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLengthLabel" runat="server" Text='<%# Eval("ProgramLength") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CompetitiveAdvantageLabel" runat="server" Text='<%# Eval("CompetitiveAdvantage") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="ProgramLinkLabel" runat="server" Text='<%# Eval("ProgramLink") %>' />
                    </td>
                </tr>
            </ItemTemplate>
    </asp:ListView>
    </div>

     <%-- ----------------------------- BUTTONS ---------------------------------------%>
    <div runat="server" id="buttons" visible="false" class="col-12 searchbox"> 
    <asp:LinkButton ID="B_ProgramInfo" runat="server" OnClick="ProgramInfo_Show" CssClass="button submit button-long">Program Info</asp:LinkButton>
    <asp:LinkButton ID="B_Categories" runat="server" OnClick="Categories_Show" CssClass="button submit">Categories</asp:LinkButton>
    <asp:LinkButton ID="B_EntranceReq" runat="server" OnClick="EntranceReq_Show"  CssClass="button submit button-long">Entrance Requirements</asp:LinkButton>
    <asp:LinkButton ID="B_Courses" runat="server" OnClick="Courses_Show" CssClass="button submit" >Courses</asp:LinkButton>
    <asp:LinkButton ID="B_CourseEquivalencies" runat="server" OnClick="CourseEquivalencies_Show" CssClass="button submit button-long">Equivalencies</asp:LinkButton>
    <asp:LinkButton ID="B_ProgramPreferences" runat="server" OnClick="ProgramPreferences_Show" CssClass="button submit">Preferences</asp:LinkButton>
    </div>
     <%-- ----------------------------- PROGRAM INFO ---------------------------------------%>

    <div runat="server" id="ProgramInfo" visible="false">
<%--        <p>ProgramID: <asp:Label ID="ProgramIDLabel" runat="server" /></p>--%>

        <p>Program Name: <asp:TextBox ID="TB_ProgramName" runat="server" /></p>

        <p>Credential Type: <asp:DropDownList ID="DL_CredentialType" runat="server" 
                                                                  DataSourceID="ODSCredentialType" 
                                                                  DataTextField="CredentialTypeName" 
                                                                  DataValueField="CredentialTypeID"
                                                            />   </p>

        <p>Description: <asp:TextBox ID="TB_Description" runat="server" /></p>

        <p>Total Credits: <asp:TextBox ID="TB_Credits" runat="server" /></p>

        <p>Program Length: <asp:TextBox ID="TB_Length" runat="server" /></p>

        <p>Competitive Average (Competitive Advantage): <asp:TextBox ID="TB_CompetitiveAdvantage" runat="server" /></p>

        <p>Check if Active: <asp:Checkbox ID="CB_Active" runat="server" /></p>

        <p>Program Link: <asp:TextBox ID="TB_Link" runat="server" /></p>

        <asp:Button ID="Program_Save" runat="server" Text="Save" OnClick ="Save_Program"/>
               
    </div>

    <%-- ----------------------------- CATEGORIES ---------------------------------------%>
    
    <div runat="server" id="Categories" visible="false">
        <p>Select the categorie(s) that this program belongs to:</p>
        <asp:CheckBoxList ID="CB_Categories" runat="server" DataSourceID="CategoryList" DataTextField="CategoryDescription" DataValueField="CategoryID" >
        </asp:CheckBoxList>
        <asp:Button ID="Categories_Save" runat="server" Text="Save" OnClick ="Save_Categories"/>
    </div>


        <%-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------%>
     
     <div runat="server" id="EntranceRequirements" visible="false">
         <p>What high school courses does this program require?</p>


         <p>Does entry to this program require any previous post-secondary work?</p>


         <asp:Button ID="EntranceReq_Save" runat="server" Text="Save" OnClick ="Save_EntranceReq"/>

     </div>


        <%-- ----------------------------- PROGRAM COURSES ---------------------------------------%>
     
    <div runat="server" id="ProgramCourses" visible="false">
        <p>What courses are part of this program?</p>
        <h4>Level One</h4>
         <asp:ListView ID="LV_ProgramCourses_One" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">CourseID</th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>

        <h4>Level Two</h4>
         <asp:ListView ID="LV_ProgramCourses_Two" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">CourseID</th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>

        <h4>Level Three</h4>

                 <asp:ListView ID="LV_ProgramCourses_Three" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">CourseID</th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>


        <h4>Level Four</h4>
                 <asp:ListView ID="LV_ProgramCourses_Four" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">CourseID</th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>

        <h4>Level 5 and above:</h4>
                 <asp:ListView ID="LV_ProgramCourses_More" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">CourseID</th>
                    <th runat="server">Course Code</th>
                    <th runat="server">Course Name</th>
                    <th runat="server">Course Credits</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="CourseIDLabel" runat="server" Text='<%# Eval("CourseID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCodeLabel" runat="server" Text='<%# Eval("CourseCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseNameLabel" runat="server" Text='<%# Eval("CourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CourseCreditsLabel" runat="server" Text='<%# Eval("CourseCredits") %>' />
                    </td>
                 </tr>
            </ItemTemplate>
    </asp:ListView>
        <asp:Button ID="Courses_Save" runat="server" Text="Save" OnClick ="Save_Courses"/>
    </div>


        <%-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------%>
     
    <div runat="server" id="CourseEquivalencies" visible="false">
        <p>Does this program accept transfer credit/advanced credit for any other NAIT courses?</p>
        <asp:GridView ID="GV_Equivalencies" runat="server"></asp:GridView>
        
        <asp:Button ID="CourseEquivalencies_Save" runat="server" Text="Save" OnClick ="Save_CourseEquivalencies"/>
    </div>
    

        <%-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------%>
   
    <div runat="server" id="ProgramPreferences" visible="false">
        <p>Answer the questions below so that students can be better matched with this program:</p>
        <asp:GridView ID="GV_Questions" runat="server"></asp:GridView>
        <asp:Button ID="Questions_Save" runat="server" Text="Save" OnClick ="Save_Questions"/>
    </div>

   <%-- -----------------------------ODS---------------------------------------%>

    <asp:ObjectDataSource ID="CategoryList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ODSCredentialType" runat="server" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource> 

</asp:Content>


