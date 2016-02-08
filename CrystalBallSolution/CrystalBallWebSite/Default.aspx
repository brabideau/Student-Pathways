<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        html {
            height:100%;
        }

        body {
          
          background-size: cover;
        }

        h1.home-heading {
            font-size:60px;
            color:lightblue;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 1);
            text-align:center;
        }
        .circle-button {
            border-radius: 50%;
            width: 280px;
            height: 280px;
            background: rgba(255, 255, 255, 0.75);
            margin-left: auto;
            margin-right:auto;
            margin-top: 20px;
            margin-bottom:20px;
            -webkit-box-shadow: 4px 4px 5px 0px rgba(0, 0, 0, 0.75);
            -moz-box-shadow:    4px 4px 5px 0px rgba(0, 0, 0, 0.75);
            box-shadow:         4px 4px 5px 0px rgba(0, 0, 0, 0.75);
            text-align:center;
            margin-bottom:40px;
        }
        .circle-button a {
            color: #225899;
            text-decoration:none;
        }
        .circle-button a img {
            padding-top: 20px;
        }
        .circle-button a h4 {
            font-size: 20px;
        }
        .circle-button:hover {
            background: rgba(255, 255, 255, 1);
            -webkit-box-shadow: 0px 0px 17px 0px rgba(0, 0, 0, 0.75);
            -moz-box-shadow:    0px 0px 17px 0px rgba(0, 0, 0, 0.75);
            box-shadow:         0px 0px 17px 0px rgba(0, 0, 0, 0.75);
        }
        .main-desc {
            font-family: "Open Sans Light";
            padding:10px 0;
            font-size: 16px;
            color:#225899;
            text-align: justify;
            font-weight:100 !important;

        }
        .col-centered {
            display:inline-block;
            float:none;
            text-align:left;
            margin-right:-4px;
        }    
        .row-centered {
            text-align:center;
        }
        .row-info {
        background-color: rgba(255, 255, 255, 0.90);
            margin-top:40px;
            margin-bottom: 60px;
            border-radius: 4px;
            -webkit-box-shadow: 2px 2px 5px 0px rgba(0, 0, 0, 0.75);
            -moz-box-shadow:    2px 2px 5px 0px rgba(0, 0, 0, 0.75);
            box-shadow:         2px 2px 5px 0px rgba(0, 0, 0, 0.75);
        }

        .arrow-box {
	        position: relative;
	        background: rgba(255, 255, 255, 0.85);
            -webkit-box-shadow: 2px 2px 5px 0px rgba(0, 0, 0, 0.75);
            -moz-box-shadow:    2px 2px 5px 0px rgba(0, 0, 0, 0.75);
            box-shadow:         2px 2px 5px 0px rgba(0, 0, 0, 0.75);
        }
        .arrow-box:after {
	        bottom: 100%;
	        left: 50%;
	        border: solid transparent;
	        content: " ";
	        height: 0;
	        width: 0;
	        position: absolute;
	        pointer-events: none;
	        border-color: rgba(255, 255, 255, 0);
	        border-bottom-color: rgba(255, 255, 255, 0.85);
	        border-width: 30px;
	        margin-left: -30px;
        }

    </style>
    <div class="container-fluid">
        <div class="row">
            <h1 class="home-heading">Find Your Academic Pathway</h1>
        </div>
    </div>
       
    <div class="jumbotron">
        <div class="col-md-6">
            <div class="circle-button hvr-grow-shadow">
                <a href="LearnerPathway.aspx"><img src="images/nait-student5.png" alt="learner pathway" /> 
                    <h4>NAIT Student</h4>    
                </a>
            </div>
        </div>
        <div class="col-md-6">
            <div class="circle-button hvr-grow-shadow">
                <a href="Admin/MenuCategory.aspx"><img src="images/new-student.png" alt="learner pathway" />
                    <h4>Administrator</h4>
                </a>
            </div>
        </div>
    </div>

    <div class="row row-centered">
        <div class="col-md-6">
            <h2>Nait Student</h2>
            <p>
                Choosing this option will allow NAIT Student to start building a profile, giving students calculated suggestions as to what academic programs they can take or translate.
            </p>

        </div>
        <div class="col-md-6">
            <h2>Administrator</h2>
            <p>
                Choosing this option will take new Student to our category search, and find the program they're looking for.
            </p>
        </div>
        
    </div>
</asp:Content>
