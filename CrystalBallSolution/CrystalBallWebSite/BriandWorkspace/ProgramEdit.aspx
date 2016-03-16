<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProgramEdit.aspx.cs" Inherits="Briand_Workspace_ProgramEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" CssClass="clearfix">
    <h1>Edit Program</h1>
   <div runat="server" id="ProgramEditDiv" visible="false">
       <h2>
            <asp:Label ID="ProgramNameLabel" runat="server" Text=""></asp:Label></h2>
        
     <%-- ----------------------------- BUTTONS ---------------------------------------%>
    <div runat="server" id="buttons" visible="false" class="col-12 searchbox"> 
        <h5>
    <asp:LinkButton ID="B_ProgramInfo" runat="server" OnClick="ProgramInfo_Show">Program Info</asp:LinkButton> >>> 
    <asp:LinkButton ID="B_Categories" runat="server" OnClick="Categories_Show">Categories</asp:LinkButton> >>> 
    <asp:LinkButton ID="B_EntranceReq" runat="server" OnClick="EntranceReq_Show">Entrance Requirements</asp:LinkButton> >>> 
    <asp:LinkButton ID="B_Courses" runat="server" OnClick="Courses_Show" >Courses</asp:LinkButton> >>> 
    <asp:LinkButton ID="B_CourseEquivalencies" runat="server" OnClick="CourseEquivalencies_Show">Equivalencies</asp:LinkButton> >>> 
    <asp:LinkButton ID="B_ProgramPreferences" runat="server" OnClick="ProgramPreferences_Show" >Preferences</asp:LinkButton>
            </h5>
    </div>
     <%-- ----------------------------- PROGRAM INFO ---------------------------------------%>
       <div runat="server" id="ProgramInfo" visible="false" class="clearfix">
       <asp:Label ID="ProgramIDLabel" runat="server" Text="" Visible="false"></asp:Label>
    
       <p class="clearfix"><span class="label col-3">Program Name:</span> <asp:TextBox ID="TB_ProgramName" runat="server" CssClass="col-4"/></p> 

        <p class="clearfix"><span class="label col-3">Credential Type:</span><asp:DropDownList ID="DL_CredentialType" runat="server" 
                                                                  DataSourceID="ODSCredentialType" 
                                                                  DataTextField="CredentialTypeName" 
                                                                  DataValueField="CredentialTypeID"
                                                                  CssClass="col-2"
                                                                  />   </p>

        <p class="clearfix"><span class="label col-3">Description:</span> <asp:TextBox ID="TB_Description" TextMode="multiline" runat="server" CssClass="col-6"/></p>
        <p class="clearfix"><span class="label col-3">Total Credits:</span> <asp:TextBox ID="TB_Credits" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Program Length: </span><asp:TextBox ID="TB_Length" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Competitive Average (Competitive Advantage): </span><asp:TextBox ID="TB_CompetitiveAdvantage" runat="server" CssClass="col-1"/></p>

        <p class="clearfix"><span class="label col-3">Check if Active: </span> <asp:Checkbox ID="CB_Active" runat="server" CssClass="col-1"/></p> 

        <p class="clearfix"><span class="label col-3">Program Link: </span><asp:TextBox ID="TB_Link" runat="server"  CssClass="col-4"/></p>
        <asp:LinkButton ID="Program_Save" runat="server" OnClick="Save_Program" CssClass="button next button-long">Save & Continue</asp:LinkButton>
      
    </div>

    <%-- ----------------------------- CATEGORIES ---------------------------------------%>
    
    <div runat="server" id="Categories" visible="false" class="clearfix">
        <p>Select the categorie(s) that this program belongs to:</p>
        <asp:CheckBoxList ID="CB_Categories" runat="server" DataSourceID="CategoryList" DataTextField="CategoryDescription" DataValueField="CategoryID" >
        </asp:CheckBoxList>
        <asp:LinkButton ID="Categories_Save" runat="server" OnClick="Save_Categories" CssClass="button next button-long">Save & Continue</asp:LinkButton>

    </div>


    <%-- ----------------------------- ENTRANCE REQUIREMENTS ---------------------------------------%>
     
     <div runat="server" id="EntranceRequirements" visible="false" class="clearfix">

         <p>What high school courses does this program require?</p>
          <asp:ListView ID="LV_SubjectReq" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Subject</th>
                    <th runat="server">Courses</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>        
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="SubjectIDLabel" runat="server" Text='<%# Eval("SubjectRequirementID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("SubjectDescription") %>' />
                    </td>
                    <td>
                        <asp:GridView ID="GV_EntranceReqs" runat="server"></asp:GridView>
                    </td>
                </tr>
            </ItemTemplate>
    </asp:ListView>




         <p>Does entry to this program require any previous post-secondary work?</p>

         <asp:ListView ID="LV_DegreeEntranceReq" runat="server">
        <LayoutTemplate>
            <table>
                <tr>
                    <th></th>
                    <th runat="server">Credential Type</th>
                    <th runat="server">Category</th>
                    <th runat="server">GPA</th>
                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>        
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="DegreeEntranceReqLabel" runat="server" Text='<%# Eval("DegreeEntranceReqID") %>' />
                    </td>
                    <td>
                        <asp:DropDownList ID="DL_DegEnt_CredType" runat="server" DataSourceID="ODSCredentialType" 
                                                                  DataTextField="CredentialTypeName" 
                                                                  DataValueField="CredentialTypeID" SelectedValue='<%# Eval("CredentialTypeID") %>'></asp:DropDownList>

                    </td>
                    <td>
                        <asp:DropDownList ID="DL_DegEnt_Cat" runat="server" DataSourceID="CategoryList" 
                                                                  DataTextField="CategoryDescription" 
                                                                  DataValueField="CategoryID" SelectedValue='<%# Eval("CategoryID") %>'></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="GPA_TextBox" runat="server" Text='<%# Eval("GPA") %>'></asp:TextBox>
                    </td>
                    
                </tr>
            </ItemTemplate>
    </asp:ListView>
         <asp:LinkButton ID="EntranceReq_Save" runat="server" OnClick="Save_EntranceReq" CssClass="button next button-long">Save & Continue</asp:LinkButton>
     </div>


    <%-- ----------------------------- PROGRAM COURSES ---------------------------------------%>
     
    <div runat="server" id="ProgramCourses" visible="false" class="clearfix">
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
        <asp:LinkButton ID="Courses_Save" runat="server" OnClick="Save_Courses" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>


    <%-- ----------------------------- COURSE EQUIVALENCIES ---------------------------------------%>
     
    <div runat="server" id="CourseEquivalencies" visible="false" class="clearfix">
        <p>Does this program accept transfer credit/advanced credit for any other NAIT courses?</p>
        <asp:GridView ID="GV_Equivalencies" runat="server"></asp:GridView>
         <asp:LinkButton ID="CourseEquivalencies_Save" runat="server" OnClick="Save_CourseEquivalencies" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
    

    <%-- ----------------------------- PROGRAM PREFERENCES ---------------------------------------%>
   
    <div runat="server" id="ProgramPreferences" visible="false" class="clearfix">
        <p>Answer the questions below so that students can be better matched with this program:</p>
        <asp:GridView ID="GV_Questions" DataSourceID="QuestionsList" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="QuestionID" runat="server"  Text='<%# Eval("QuestionID") %>'></asp:Label>
                   </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:DropDownList ID="DL_Preference" runat="server">
                            <asp:ListItem Selected="True" Value="noPref">---</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:LinkButton ID="Questions_Save" runat="server" OnClick="Save_Questions" CssClass="button next button-long">Save & Continue</asp:LinkButton>
    </div>
        <hr />
    </div> <%--end program edit div--%>
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
                    <th runat="server">Description</th>
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



    <%-- -----------------------------ODS---------------------------------------%>

    <asp:ObjectDataSource ID="CategoryList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ODSCredentialType" runat="server" SelectMethod="CredentialType_List" TypeName="CrystalBallSystem.BLL.AdminController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="QuestionsList" runat="server" SelectMethod="Question_List" TypeName="CrystalBallSystem.BLL.AdminController" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource> 

</asp:Content>


