﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuCategory.aspx.cs" Inherits="Admin_MenuCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="admin-menu">  

        <div class="col-4 center">
            <a href="ProgramEdit.aspx">
            <div class="cat-img">
                <img src="../images/update_program.png" alt="manage programs"> 
           </div>
            <h5>Manage Programs</h5>
            </a> 
       </div>

     <div class="col-4 center">
            <a href="Reports.aspx">
                <div class="cat-img">
                <img src="../images/reports-icon.png" alt="View user data"> 
                </div>
            <h5>View Useage Data</h5>
            </a> 
       </div>
        
        <div class="col-4 center">
            <a href="ManageCategory.aspx">
            <div class="cat-img"><img src="../images/managethree.png" alt="Manage program categories"> 
            </div>
            <h5>Program Categories</h5>
            </a>
        </div>


        <div class="col-4 center">
            <a href="ManageNaitCourses.aspx">
             <div class="cat-img"><img src="../images/geometry.png" alt="Manage Nait Courses">   </div> 

             <h5>Manage Nait Courses</h5>
            </a>
       </div>

        <div class="col-4 center">
            <a href="ManageHighSchoolCourses.aspx">
            <div class="cat-img">
                <img src="../images/blackboard.png" alt="update high shool courses"> 
            </div>
            <h5>High School Courses</h5>
            </a>
        </div>


        <div class="col-4 center">
            <a href="ManagePreferenceQuestion.aspx">
            <div class="cat-img">
                <img src="../images/question.png" alt="Manage preference questions"> 
            </div>
            <h5>Preference Questions</h5>
             </a> 
       </div>


<%--

    <div class="col-4 center">
        <a href="Equivalencies.aspx">
            <div class="cat-img">
                <img src="../images/equivalency.png" alt="Manage Equivalencies"> 
            </div>
            <h5>Manage Equivalencies</h5>
        </a> 
    </div>--%>
</div>
</asp:Content>

