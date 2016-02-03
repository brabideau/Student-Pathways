<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuCategory.aspx.cs" Inherits="Admin_MenuCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        html {
            height:100%;
        }

        body {
          -webkit-background-size: cover;
          -moz-background-size: cover;
          -o-background-size: cover;
          background-size: cover;
        }

        div.polaroid {
          margin-top:100px;
          width: 250px;
          margin-left:50px;
          box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
          text-align: center;
        }

        div.container {
          padding: 10px;
          width:auto;
        }

        div.container p{
           font-size:16px;
           font-weight:bold;
           color:gray;
           text-align:center;
        }
  </style>

    
        <div class="col-md-4">
            <div class="polaroid">
                <a href="ManageCategory.aspx"><img src="../images/managethree.png" alt="add"> 
                </a>
                
            </div>
            <div class="container">
                    <p>Manage Category</p>
            </div>
        </div>
        <div class="col-md-4">
                <div class="polaroid">
                    <a href="ManageNaitCourses.aspx"><img src="../images/managetwo.png" alt="add"> 
                    </a>   
                </div>
            <div class="container">
                <p>Manage Course</p>
            </div>
       </div>
        <div class="col-md-4">
                <div class="polaroid">
                    <a href="InsertProgram.aspx"><img src="../images/add.png" alt="add"> 
                    </a>   
                </div>
            <div class="container">
                <p>Insert Program</p>
            </div>
       </div>

</asp:Content>

