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

using psc.Framework;
using RCPJ.DAL.Helper;


namespace RCPJ.Application
{
    /// <summary>
    ///		Summary description for Template.
    /// </summary>
     
    public partial class TemplateBodyExtV3 : System.Web.UI.UserControl
    {        
        protected void Page_Load(object sender, EventArgs e)
        {


            using (PageBase c = new PageBase())
            {

                if (!IsPostBack)
                {
                    //Image1.ImageUrl = "img\\logo_topoV3_" + c.pUFJuntaDefault + ".jpg";
                    Image1.ImageUrl = "img\\LogoRegin.jpg";
                    //logoPequeno.Style.Add("background-image", "url(img/logomarcagoverno_" + c.pUFJuntaDefault + ".gif)");                    
                    //Ambiente.InnerHtml = c.pAmbiente;

                    #region Buscar Titulo Pantalla
                    viab2.InnerHtml = Server.HtmlDecode(c.pTituloSistemaJUCESC);
                    Session.Remove("pTituloSistemaJUCESC");
                    #endregion
                    DateTime pDate = DateTime.Now;

                    LblDia.Text = pDate.Day.ToString();
                    LblMes.Text = pDate.ToString("MMM").ToUpper();
                    lblAno.Text = pDate.Year.ToString();

                    lblHoraAtual.Value = pDate.ToString("HH:mm").ToString();

                    lblVersao.Text = PageBase.VersaoSistema;
                    lblDataVersao.Text = PageBase.VersaoDataSistema;

                    //if ((Request.QueryString["codMunic"] != null && Request.QueryString["codMunic"].ToString()!="") || (Request.QueryString["idMunicipio"] != null && Request.QueryString["idMunicipio"].ToString()!=""))
                    //{
                    //    string codigoMunicipio = (Request.QueryString["codMunic"] != null) ? Request.QueryString["codMunic"].ToString() : Request.QueryString["idMunicipio"].ToString();

                    //    try
                    //    {
                    //        using (TAB_INSTITUICAO instituicao = new TAB_INSTITUICAO())
                    //        //{
                    //        //    instituicao.TIN_TMU_COD_MUN = decimal.Parse(codigoMunicipio);
                    //        //    instituicao.TIN_TIP_INSTITUICAO = 2;
                    //        //    DataTable Dt = instituicao.Query();

                    //        //    if (Dt.Rows.Count>0 && Dt.Rows[0]["TIN_IMG_LOGO_BINARIE"] != DBNull.Value)
                    //        //    {
                    //        //        byte[] imgBinario = (byte[])Dt.Rows[0]["TIN_IMG_LOGO_BINARIE"];


                    //        //        //   testeDePagina.Attributes["src"] = "img/imagem.aspx";
                    //        //        Session.Remove("logoInstituicao");
                    //        //        Session.Add("logoInstituicao", imgBinario);
                    //        //        //Ambiente.InnerHtml = "";
                    //        //        //Ambiente.Visible = false;
                    //        //        //logoMunic.Visible = true;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        Session.Remove("logoInstituicao");
                    //        //    }

                    //        //    //Response.Write(Request.QueryString["codMunic"].ToString());
                               
                    //        //}
                    //    }
                    //    catch { 
                            
                    //    }
                    //}



                }
            }
        }
    }
}