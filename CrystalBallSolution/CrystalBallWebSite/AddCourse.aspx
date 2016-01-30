<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="AddCourse" MasterPageFile="~/Site.master" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div>    
    <asp:ObjectDataSource ID="CourseList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCourseList" TypeName="CrystalBallSystem.BLL.StudentController"></asp:ObjectDataSource>
    </div>

    <div>

        <asp:GridView ID="GV_Course" runat="server" AutoGenerateColumns="false" ShowFooter="true">
            <Columns>
                <asp:TemplateField>
                    <FooterTemplate>
                        <asp:LinkButton ID="Add_Btn" runat="server" Font-Underline="false"
                            OnClick="Add_Btn_Click" CssClass="wizard-course-buttons hvr-ripple-out add-align"  
                            CausesValidation="false"><i aria-hidden="true" class="glyphicon glyphicon-plus"></i></asp:LinkButton>             
                    </FooterTemplate>
                    <ItemTemplate>

                        <asp:DropDownList ID="DL_Course" runat="server" DataSourceID="CourseList" CssClass="form-control"
                                        DataTextField="HighSchoolCourseDescription"
                                        DataValueField="HighSchoolCourseID"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="">Select a Course &amp; Level</asp:ListItem>
                        </asp:DropDownList>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enter Your Marks">
                                
                    <ItemTemplate>
                        <asp:TextBox ID="TB_EnterMarks" runat="server" CssClass="form-control ent-req-input"
                            Width="50px"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter a number between 1 - 100" 
                            ControlToValidate="TB_EnterMarks" MinimumValue="1" MaximumValue="100" ForeColor="Maroon" 
                            Type="Integer" Font-Size="Smaller" Display="Dynamic"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You must enter a grade value."
                            ControltoValidate="TB_EnterMarks" ForeColor="Maroon" Font-Size="Smaller" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                         <asp:LinkButton ID="Remove_Btn" runat="server" Font-Underline="false"
                            CausesValidation="false"
                            CssClass="wizard-course-buttons hvr-ripple-out add-align"
                            OnClick="Remove_Btn_Click"><i aria-hidden="true" class="glyphicon glyphicon-minus"></i></asp:LinkButton>
                    </ItemTemplate>                   
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>

    </div>

</asp:Content>