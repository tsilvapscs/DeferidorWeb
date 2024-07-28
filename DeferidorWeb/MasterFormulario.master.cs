using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    [Serializable]
    public partial class MasterFormulario : System.Web.UI.MasterPage
    {

        public event EventHandler contentCallEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            TituloTemplate.InnerText = MasterTituloTemplate;
    }
        public string MasterTituloTemplate { get; set; }

        public void ExibirMensagem(string mensagem)
        {
            if (!string.IsNullOrEmpty(mensagem))
            {
                modalMsg.InnerText = mensagem;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
            }

        }



    }
