<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NovoTopoV2.ascx.cs" Inherits="NovoTopoV2" %>
<%@ Register TagPrefix="pwc" Namespace="psc.Blocks.UI.WebForms" Assembly="psc.Blocks.UI.WebForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="ajax" %>
<head>
    <title>REGIN</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />  
      
    
  
</head>
<body>    
      <nav class="navbar navbar-expand-lg shadow-sm" style="margin-bottom:3px; background-image: url('img/viabilidadetopofundo2.jpg');background-repeat:no-repeat;
background-position:center;-webkit-background-size:cover;
-moz-background-size:cover;
-o-background-size:cover;
background-size:cover;" >
        <div class="container-fluid">
            
           
            <a class="navbar-brand" href="Servicos.aspx"> <asp:Image ID="ilogoPrincipal" runat="server" Height="60px" ImageUrl="~/img/regin.gif" /></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"><i class="fa-solid fa-bars"></i></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">               
                <ul class="navbar-nav ml-auto ">
                      <li class="nav-item text-center ">
                        <a target="_blank" class="nav-link" href="http://www.redesim.gov.br/">
                            <i class="fa fa-network-wired"></i>
                            <br />
                            <span>Redesim</span>
                        </a>
                    </li>
                    <li class="nav-item text-center ">
                        <a target="_blank" class="nav-link " href="http://www4.planalto.gov.br/legislacao/">
                            <i class="fa fa-gavel"></i>
                            <br />
                            <span>Legislação</span>
                        </a>
                    </li>
                    <li class="nav-item text-center "  id="divIN" runat="server">
                        <a target="_blank" class="nav-link " href="https://www.gov.br/economia/pt-br/assuntos/drei/legislacao/instrucoes-normativas">
                            <i class="fa fa-file-contract"></i>
                            <br />
                            <span>Instruções Normativas</span>
                        </a>
                    </li>                                   
                </ul>
            </div>
        </div>
    </nav>
   
     
</body>

