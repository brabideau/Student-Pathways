<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="home clearfix">

     <h1>Find Your Academic Pathway</h1>

        <div class="center home-col">
            <div class="circle-button">
                <a href="Student/StudentPreferences"><img src="images/nait-student5.png" alt="learner pathway" /> </a>
             </div>
 
                    <h2>Nait Student</h2>
                    <p>
                        Answer some quick questions and find the perfect program for you!
                    </p>   
        </div>

        <div class="center home-col">
            <div class="circle-button">
                <a href="Admin/MenuCategory.aspx"><img src="images/new-student.png" alt="learner pathway" /></a></div>

                <h2>Administrator</h2>
                <p>
                    Log in to access administrator options.
                </p>
                
        </div>


        
    
</div>

</asp:Content>
