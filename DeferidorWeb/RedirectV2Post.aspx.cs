using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using psc.Framework;
using psc.ApplicationBlocks.SessionState;
using System.Xml;

using psc.Cryptography;
using System.Security.Cryptography;
using System.Web.Security;
using System.Configuration;
using System.Collections.Specialized;


namespace RCPJ.Application
{
    [Serializable]
    public partial class RedirectV2Post : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ComandoJavaString(this, "openWindowWithPost('http://192.168.41.146/ES/regin.junta/MAN_ProtocoloPago2.aspx','_self', 'toolbar=no,menubar=no,statusbar=no,location=no,resizable=yes,scrollbars=yes,width=790,height=480', { 'usuario' : '08711367750', 'Nire' : '32300024769'})");

            //HtmlInputHidden txtUsuario = new HtmlInputHidden();
            //txtUsuario.ID = "usuario";
            //txtUsuario.Value = "08711367750";
            //form1.Controls.Add(txtUsuario);
            ////form1.Method = "post";
            //form1.Target = "_self";
            //form1.Action = "http://192.168.41.146/ES/regin.junta/MAN_ProtocoloPago2.aspx";

            if (Request.Form.Count > 0)
            {
                if (Request.Form["usuario"] != null && Request.Form["usuario"].ToString() != ""
                    && Request.Form["adress"] != null && Request.Form["adress"].ToString() != "")
                {
                    pUsuarioSistema = Request.Form["usuario"].ToString();
                    pCNPJIntituicaoUsuario = Request.Form["instituicao"] == null ? "" : Request.Form["instituicao"].ToString();
                    pNumeroServentia = Request.Form["NumeroServentia"] == null ? "" : Request.Form["NumeroServentia"].ToString();
                    Response.Redirect(Request.Form["adress"].ToString(), false);
                    //Popup(this, Request.Form["adress"].ToString(), "PaginaNormal", 500, 700);
                    return;
                }
            }
            if (Request.QueryString["Pagina"] != null && Request.QueryString["Pagina"].ToString() != "")
            {
                NameValueCollection data = new NameValueCollection();
                data.Add("usuario", pUsuarioSistema);
                data.Add("instituicao", pCNPJIntituicaoUsuario);
                data.Add("NumeroServentia", pNumeroServentia);
                data.Add("adress", Request.QueryString["Pagina"].ToString());
                HttpHelper.RedirectAndPOST(this.Page, Request.QueryString["PaginaPost"].ToString(), data);
            }
        }
    }
}