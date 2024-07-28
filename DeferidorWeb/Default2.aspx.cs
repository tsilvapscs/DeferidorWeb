using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
        data.Add("usuario", "52090353287");
        //data.Add("instituicao", "30594477000103"); //RJ - Sao Gonzalo
//        data.Add("instituicao", "09280442000103"); //RJ
        data.Add("instituicao", "27079821000111");
        data.Add("NumeroServentia", "093245");
        //data.Add("NumeroServentia", "089581");
        //data.Add("NumeroServentia", "090167");//RJ - Sao Gonzalo
        data.Add("adress", "EntradaDeferidor.aspx");
        //HttpHelper.RedirectAndPOST(this.Page, "http://localhost/DeferidorWeb/RedirectV2Post.aspx", data);
        HttpHelper.RedirectAndPOST(this.Page, "http://localhost:11265/DeferidorWeb/RedirectV2Post.aspx", data);
    
        //HttpHelper.RedirectAndPOST(this.Page, "http://regin.jucees.es.gov.br/DeferidorWeb/RedirectV2Post.aspx", data);
        return;
    }
}
