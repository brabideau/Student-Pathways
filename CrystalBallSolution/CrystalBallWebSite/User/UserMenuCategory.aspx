<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserMenuCategory.aspx.cs" Inherits="User_UserMenuCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        html {
            height:100%;
        }

        body {
          background-size: cover;
        }

        div.polaroid {
          margin: 0 auto;
          margin-top:100px;
          width: 200px;
          height:200px;
          box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
          text-align: center;
        }

        div.polaroid img{
            margin-top:15px;
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

    
        <div class="col-md-6">
            <div class="polaroid">
                <a href=""><img src="../images/path-button.png" alt="Pathways Map"> 
                </a>                
            </div>
            <div class="container">
                    <p>View Pathways</p>
            </div>
        </div>
        <div class="col-md-6">
                <div class="polaroid">
                    <a href=""><img src="../images/temp-course-icon.png" alt="Find Program Matches"> 
                    </a>   
                </div>
            <div class="container">
                <p>Find Program Matches</p>
            </div>
       </div>           

</asp:Content>

