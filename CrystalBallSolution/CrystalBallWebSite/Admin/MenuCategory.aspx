<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuCategory.aspx.cs" Inherits="Admin_MenuCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   

    
        <div class="cat-box">

            <div class="cat-img"><a href="ManageCategory.aspx"><img src="../images/managethree.png" alt="Manage Category"> </a>
            </div>
            <h5>Manage Category</h5>
        </div>


        <div class="cat-box">

             <div class="cat-img"><a href="ManageNaitCourses.aspx"><img src="../images/managetwo.png" alt="Manage Nait Courses"> </a>  </div> 

             <h5>Manage Nait Course</h5>
       </div>


        <div class="cat-box">

             <div class="cat-img"><a href="InsertProgram.aspx"><img src="../images/add.png" alt="add Program"> </a>
            </div>
             <h5>Insert Program</h5>

       </div>


        <div class="cat-box">
            <div class="cat-img">
                    <a href="UpdateProgram.aspx"><img src="../images/update_program.png" alt="manage program"> 
                    </a>   
           </div>
            <h5>Manage Program</h5>
       </div>


        <div class="cat-box">
            <div class="cat-img">
                <a href="ManageHighSchoolCourses.aspx"><img src="../images/course_logo.png" alt="update high shool courses"> 
                </a>   
            </div>
            <h5>Manage High School Courses</h5>
        </div>


        <div class="cat-box">
            <div class="cat-img">
                <a href="ManagePreferenceQuestion.aspx"><img src="../images/question.png" alt="Manage preference questions"> 
                </a>   
            </div>
            <h5>Manage Preference Questions</h5>
       </div>
        

</asp:Content>

