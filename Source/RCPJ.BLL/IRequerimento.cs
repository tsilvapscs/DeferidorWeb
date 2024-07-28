using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public abstract class IRequerimento
    {
        #region  Property Declarations
        private string _CNPJ_ORGAO_REGISTRO = "";
        private string _NroProtocolo = "";
        private string _requerimento = "";
        private int _SqPessoa = 0;
        private string _nomeEmpresa = "";
        private string _t005_uf_origem = string.Empty;

        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string NomeEmpresa
        {
            get { return _nomeEmpresa; }
            set { _nomeEmpresa = value; }
        }       

        public string CnpjOrgaoRegistro
        {
            get { return _CNPJ_ORGAO_REGISTRO; }
            set { _CNPJ_ORGAO_REGISTRO = value; }
        }
        public string Protocolo
        {
            get { return _NroProtocolo; }
            set { _NroProtocolo = value; }
        }
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }
        public int SqPessoa
        {
            get { return _SqPessoa; }
            set { _SqPessoa = value; }
        }
        public string T005_uf_origem
        {
            get { return _t005_uf_origem; }
            set { _t005_uf_origem = value; }
        }
        #endregion 
    }
}
