using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NovoTopoV2 : System.Web.UI.UserControl
{
    public string OrgaoRegistro
    {
        get { return ConfigurationManager.AppSettings["pInstituicaoDefault"].ToString(); }
    }
    public string pLogoCabecalho
    {
        get { return "img/" + OrgaoRegistro + "Cabecalho.gif"; }

    }
    public string pUFDefault
    {
        get
        {
            return ConfigurationManager.AppSettings["UFPrincipal"].ToString();
        }
    }
    public string Titulo { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        ilogoPrincipal.ImageUrl = pLogoCabecalho;
        
    }
}