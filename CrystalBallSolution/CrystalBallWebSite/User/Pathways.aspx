<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pathways.aspx.cs" Inherits="User_Pathways" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .margin {
            margin-top: 50px;
        }
    </style>
    <div class="margin">
        <details>
            <summary>Health Sciences Pathway</summary>
            <asp:Image ID="HealthSciences" runat="server" imageurl="../Pathways/HealthSciencesPathway.jpg" width="100%"/>
        </details>
        <details>
            <summary>Business Pathway</summary>
            <asp:Image ID="Business" runat="server" imageurl="../Pathways/BusinessPathway.jpg" width="100%"/>
        </details>    
        <details>
            <summary>Computers and IT / Engineering Technology Pathway</summary>
            <asp:Image ID="Computers" runat="server" imageurl="../Pathways/ComputersEngineeringPathway.jpg" width="100%"/>
        </details>
        <details>
            <summary>Construction Pathway</summary>
            <asp:Image ID="Construction" runat="server" imageurl="../Pathways/ConstructionPathway.jpg" width="100%"/>
        </details>
        <details>
            <summary>Environment and Natural Resources Pathway</summary>
            <asp:Image ID="Environment" runat="server" imageurl="../Pathways/EnvironmentalPathway.jpg" width="100%"/>
        </details>
        <details>
            <summary>Hospitality and Culinary Pathway</summary>
            <asp:Image ID="Hospitality" runat="server" imageurl="../Pathways/HospitalityCulinaryPathway.jpg" width="100%"/>
        </details>
    </div>
    
</asp:Content>

