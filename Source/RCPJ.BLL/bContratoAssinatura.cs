using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoAssinatura
    {
        #region Variables
        private string _nome;
        private string _cpf;
        private string _nomeRepresentante;
        private string _cpfRepresentante;

       

        private List<bContratoAssinatura> _representantes = new List<bContratoAssinatura>();

        #endregion

        #region Declarations


        public List<bContratoAssinatura> Representantes
        {
            get { return _representantes; }
            set { _representantes = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        public string NomeRepresentante
        {
            get { return _nomeRepresentante; }
            set { _nomeRepresentante = value; }
        }
        public string CpfRepresentante
        {
            get { return _cpfRepresentante; }
            set { _cpfRepresentante = value; }
        }
        #endregion
    }
}
