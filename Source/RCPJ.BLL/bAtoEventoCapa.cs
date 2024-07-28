using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{   
    [Serializable]
    public class bAtoEventoCapa
    {
        #region Class Member Declarations
        private string _cnpjOrgaoRegistro;
        private string _requerimento;
        private string _protocoloOR;
        private string _CodigoEvento = "";
        private string _CodigoAto = "";
        private string _descricaoAto = "";
        private string _descricaoEvento = "";
        private int _qtdEvento = 0;
        private int _sqPessoa = 0;

       
        
        
        #endregion

        #region Class Property Declarations
        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }
        public string ProtocoloOR
        {
            get { return _protocoloOR; }
            set { _protocoloOR = value; }
        }
        public string CodigoEvento
        {
            get { return _CodigoEvento; }
            set { _CodigoEvento = value; }
        }
        public string CodigoAto
        {
            get { return _CodigoAto; }
            set { _CodigoAto = value; }
        }
        public string DescricaoAto
        {
            get { return _descricaoAto; }
            set { _descricaoAto = value; }
        }
        public string DescricaoEvento
        {
            get { return _descricaoEvento; }
            set { _descricaoEvento = value; }
        }
        public int QtdEvento
        {
            get { return _qtdEvento; }
            set { _qtdEvento = value; }
        }
        public int SqPessoa
        {
            get { return _sqPessoa; }
            set { _sqPessoa = value; }
        }
        #endregion

        public void Update()
        {
            using (dr013_Requerimento_Evento c = new dr013_Requerimento_Evento())
            {
                c.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;
                c.T005_nr_protocolo = _requerimento;
                c.R013_nr_protocolo_or = _protocoloOR;
                c.R013_cod_ato = _CodigoAto;
                c.R013_cod_evento = _CodigoEvento;
                c.QtdEvento = _qtdEvento;
                c.Update();
            }
        }

        public DataTable QueryAtos()
        {
            using (dr013_Requerimento_Evento c = new dr013_Requerimento_Evento())
            {
                
                c.T005_nr_protocolo = _requerimento;

                return c.QueryAtos();
            }
        }
    }
}
