using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bProcessoOR
    {
        #region  Property Declarations
        private string _protocolo = "";
        private string _nomeEmpresa = "";
        private bRequerente _requerente = new bRequerente();
        private string _naturezaJuridica = "";
        private string _ato = "";
        private string _descricaoAto = "";

        private string _protocoloVinculado = "";
        private string _protocoloVinculadoAto = "";
        private string _protocoloVinculadoDescricaoAto = "";
        private string _protocoloPai = "";

       
        #endregion

        #region Class Member Declarations
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }

        public string NomeEmpresa
        {
            get { return _nomeEmpresa; }
            set { _nomeEmpresa = value; }
        }

        public bRequerente Requerente
        {
            get { return _requerente; }
            set { _requerente = value; }
        }

        public string NaturezaJuridica
        {
            get { return _naturezaJuridica; }
            set { _naturezaJuridica = value; }
        }

        public string Ato
        {
            get { return _ato; }
            set { _ato = value; }
        }
        public string DescricaoAto
        {
            get { return _descricaoAto; }
            set { _descricaoAto = value; }
        }
        public string ProtocoloVinculado
        {
            get { return _protocoloVinculado; }
            set { _protocoloVinculado = value; }
        }

        public string ProtocoloVinculadoAto
        {
            get { return _protocoloVinculadoAto; }
            set { _protocoloVinculadoAto = value; }
        }

        public string ProtocoloVinculadoDescricaoAto
        {
            get { return _protocoloVinculadoDescricaoAto; }
            set { _protocoloVinculadoDescricaoAto = value; }
        }
        public string ProtocoloPai
        {
            get { return _protocoloPai; }
            set { _protocoloPai = value; }
        }
        #endregion 

         #region Constructors
        public bProcessoOR()
        {
            //InitClass();
        }

        public bProcessoOR(string pProtocolo)
            : this()
        {
            _protocolo = pProtocolo;
            Populate();
        }
        #endregion

        private void Populate()
        {
            DataTable dt = Query();
            
            foreach (DataRow dr in dt.Rows)
            {
                
                _protocolo = dr["PROTOCOLO"].ToString();
                _nomeEmpresa = dr["NO_EMPRESARIAL"].ToString();
                _requerente.Nome = dr["NO_TITULAR"].ToString();
                _requerente.Cpf = dr["CPF_RESP"].ToString();
                _requerente.DDD = "";
                _requerente.Telefone = "";
                _naturezaJuridica = retProt(dr["COD_NATUREZA"].ToString());
                _ato = dr["COD_ATO"].ToString();
                _descricaoAto = dr["DESCRICAO_ATO"].ToString();

                GetProtocoloVinculado();
                GetProtocoloPai();
            }
        }

        private DataTable Query()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"Select  s.nr_protocolo          PROTOCOLO 
                                    ,S.CO_ATO                COD_ATO 
                                    ,ATO.NO_ATO              DESCRICAO_ATO
                                    ,p.co_natureza_juridica  COD_NATUREZA  
                                    ,p.nr_cpf_titular_fi     CPF_RESP 
                                    ,s.dt_solicitacao        DATA_SOLICITACAO 
                                    ,p.no_empresarial        NO_EMPRESARIAL 
                                    ,p.NO_TITULAR_FI         NO_TITULAR 
                                    ,p.NO_MAE_TITULAR_FI     NO_MAE
                             From   solicitacao s
                                    INNER JOIN processo p on s.nr_protocolo = p.nr_protocolo 
                                    INNER JOIN ATO ato on ato.CO_ATO = S.CO_ATO
                             Where  s.nr_protocolo = '" + _protocolo + "'");

            sql.AppendLine(@"   And    s.sq_solicitacao = (Select max(Ss.Sq_Solicitacao) 
                                                    From solicitacao Ss Where Ss.Nr_Protocolo = '" + _protocolo + "')");
            sql.AppendLine("  Order By s.sq_solicitacao Desc ");

            return RCPJ.DAL.Helper.dHelperORACLE.ExecuteQuery(sql.ToString());
            
            
            
        }

        private void GetProtocoloVinculado()
        {
            string ret = "";
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlV = new StringBuilder();
            
            //procura para ver se existe protocolo vinculado
            sqlV.AppendLine(@"Select nr_protocolo_filho from processo_vinculado where nr_protocolo_pai= '" + _protocolo + "'");
            ret = RCPJ.DAL.Helper.dHelperORACLE.ExecuteScalar(sqlV.ToString());

            
            if (!string.IsNullOrEmpty(ret))
            {

                sql.AppendLine(@"Select  s.nr_protocolo          PROTOCOLO 
                                    ,S.CO_ATO                COD_ATO 
                                    ,ATO.NO_ATO              DESCRICAO_ATO
                                    ,p.co_natureza_juridica  COD_NATUREZA  
                                    ,p.nr_cpf_titular_fi     CPF_RESP 
                                    ,s.dt_solicitacao        DATA_SOLICITACAO 
                                    ,p.no_empresarial        NO_EMPRESARIAL 
                                    ,p.NO_TITULAR_FI         NO_TITULAR 
                                    ,p.NO_MAE_TITULAR_FI     NO_MAE
                             From   solicitacao s
                                    INNER JOIN processo p on s.nr_protocolo = p.nr_protocolo 
                                    INNER JOIN ATO ato on ato.CO_ATO = S.CO_ATO
                             Where  s.nr_protocolo = '" + ret + "'");

                sql.AppendLine(@"   And    s.sq_solicitacao = (Select max(Ss.Sq_Solicitacao) 
                                                    From solicitacao Ss Where Ss.Nr_Protocolo = '" + ret + "')");
                sql.AppendLine("  Order By s.sq_solicitacao Desc ");

                dt = RCPJ.DAL.Helper.dHelperORACLE.ExecuteQuery(sql.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    _protocoloVinculado = dr["PROTOCOLO"].ToString();
                    _protocoloVinculadoAto = dr["COD_ATO"].ToString();
                    _protocoloVinculadoDescricaoAto = dr["DESCRICAO_ATO"].ToString();

                }
            }

        }

        private void GetProtocoloPai()
        {
            string ret = "";
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlV = new StringBuilder();

            //procura para ver se existe protocolo vinculado
            sqlV.AppendLine(@"Select nr_protocolo_pai from processo_vinculado where nr_protocolo_filho= '" + _protocolo + "'");
            ret = RCPJ.DAL.Helper.dHelperORACLE.ExecuteScalar(sqlV.ToString());


            if (!string.IsNullOrEmpty(ret))
            {
                _protocoloPai = ret;
            }

        }

        private string retProt(string valor)
        {
            string temp = valor;
            temp = temp.Replace("-", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("_", "");
            temp = temp.Replace(".", "");
            return temp;

        }
    }
}
