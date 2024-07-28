using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bAndamento
    {
        #region Class Member Declarations

        private string _protocoloRequerimento = string.Empty;
        private string _protocolo = string.Empty;
        private int _sqAndamento = 0;
        private string _coDespacho = string.Empty;
        private string _sqFuncionarioAnalista = string.Empty;
        private string _cpfAnalista = string.Empty;
        private string _co_sequencial = string.Empty;
        private string _sqFuncionario = string.Empty;
        private string _secaoorigem = string.Empty;
        private string _secaoDestino = string.Empty;
        private DateTime _dataAndamento;
        private string _coJuntaComercial = string.Empty;

        private string _SecaoFCN = "";

        private string _secaoUltimaConfirmacao = string.Empty;
        private int _sequenciaUltimaConfirmacao = 0;
        private int _tipoSecao = 0;
        private string _nomeSecao = string.Empty;
        private bool _status = false;

        private List<bTipoSecaoAndamento> _listTipoSecao = new List<bTipoSecaoAndamento>();
        #endregion

        #region Class Property Declarations
        public string ProtocoloRequerimento
        {
            get { return _protocoloRequerimento; }
            set { _protocoloRequerimento = value; }
        }
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }
        public int SequenciaAndamento
        {
            get { return _sqAndamento; }
            set { _sqAndamento = value; }
        }
        public string Coddespacho
        {
            get { return _coDespacho; }
            set { _coDespacho = value; }
        }
        public string CpfAnalista
        {
            get { return _cpfAnalista; }
            set { _cpfAnalista = value; }
        }
        public string SqfAnalista
        {
            get { return _sqFuncionarioAnalista; }
            set { _sqFuncionarioAnalista = value; }
        }
        public string CodSequencial
        {
            get { return _co_sequencial; }
            set { _co_sequencial = value; }
        }
        public string CpfFuncionario
        {
            get { return _sqFuncionario; }
            set { _sqFuncionario = value; }
        }
        public string SecaoOrigem
        {
            get { return _secaoorigem; }
            set { _secaoorigem = value; }
        }

        public string SeccaoDestino
        {
            get { return _secaoDestino; }
            set { _secaoDestino = value; }
        }
        public DateTime NomeAdmConjunto
        {
            get { return _dataAndamento; }
            set { _dataAndamento = value; }
        }
        public string CodJuntaComercial
        {
            get { return _coJuntaComercial; }
            set { _coJuntaComercial = value; }
        }
        public string SecaoFCN
        {
            get { return _SecaoFCN; }
            set { _SecaoFCN = value; }
        }

        public string SecaoUltimaConfirmacao
        {
            get { return _secaoUltimaConfirmacao; }
            set { _secaoUltimaConfirmacao = value; }
        }
        public int SequenciaUltimaConfirmacao
        {
            get { return _sequenciaUltimaConfirmacao; }
            set { _sequenciaUltimaConfirmacao = value; }
        }
        public int TipoSecao
        {
            get { return _tipoSecao; }
            set { _tipoSecao = value; }
        }
        public string NomeSecao
        {
            get { return _nomeSecao; }
            set { _nomeSecao = value; }
        }
         public List<bTipoSecaoAndamento> ListTipoSecao
        {
            get { return _listTipoSecao; }
            set { _listTipoSecao = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion

        #region metodos
        public void CarregaAndamento()
        {
            DataTable dt = dHelperORACLE.GetUltimoAndamento(_protocolo);

            if (dt.Rows.Count > 0)
            {
                _sqAndamento = Int32.Parse(dt.Rows[0]["sq_andamento"].ToString());
                _sqFuncionarioAnalista = dt.Rows[0]["sq_funcionario_analista"].ToString();
                _co_sequencial = dt.Rows[0]["co_sequencial"].ToString();
                _sqFuncionario = dt.Rows[0]["sq_funcionario"].ToString();
                _secaoorigem = dt.Rows[0]["si_secao_origem"].ToString();
                _secaoDestino = dt.Rows[0]["si_secao_destino"].ToString();
                _dataAndamento = DateTime.Parse(dt.Rows[0]["dt_andamento"].ToString());
                _coJuntaComercial = dt.Rows[0]["co_junta_comercial"].ToString();
                _tipoSecao = Int32.Parse(dt.Rows[0]["In_tipo_secao"].ToString());
                _nomeSecao = dt.Rows[0]["no_secao"].ToString();
                _cpfAnalista = dt.Rows[0]["cpf_analista"].ToString();
                
            }

            DataTable dtTipoSecao = bTipoSecaoAndamento.Query();
            foreach(DataRow row in dtTipoSecao.Rows)
            {
                bTipoSecaoAndamento a = new bTipoSecaoAndamento();
                a.Codigo = row["A027_CO_SECAO"].ToString();
                a.Tipo = Int32.Parse(row["A027_TIPO"].ToString());
                _listTipoSecao.Add(a);
            }

            //recupera a secao de andamento de FCN(DBE)
            _SecaoFCN = bTabelasAuxiliares.GetConfigRegin(28);

            //Valida se o protocolo está no andamento de analise
            _status = false;
            foreach (bTipoSecaoAndamento o in _listTipoSecao)
            {
                if (o.Codigo == _secaoDestino)
                {
                    _status = true;
                    break;
                }
            }

            getUltimaSequenciaConfirmacao();


        }

        public static DataTable getUltimaSequenciaConfirmacao(string pProtocolo, string pSecao)
        {
            DataTable dtReturn = new DataTable();
            using (dT017_Protocolo_Confirmacao dal = new dT017_Protocolo_Confirmacao())
            {
                dtReturn = dal.GetUltimaConfirmacao(pProtocolo, pSecao);

            }
            return dtReturn;
        }

        public void getUltimaSequenciaConfirmacao()
        {
            //Recupero da tabela t017_protocolo_confirmacao a ultima sequencia e andamento feito
            using (dT017_Protocolo_Confirmacao dal = new dT017_Protocolo_Confirmacao())
            {
                DataTable dtDal = dal.GetUltimaConfirmacao(_protocoloRequerimento);
                if (dtDal.Rows.Count > 0)
                {
                    _secaoUltimaConfirmacao = dtDal.Rows[0]["T017_ANDAMENTO_SECAO"].ToString();
                    _sequenciaUltimaConfirmacao = Int32.Parse(dtDal.Rows[0]["T017_ANDAMENTO_SEQ"].ToString() == "" ? "0" : dtDal.Rows[0]["T017_ANDAMENTO_SEQ"].ToString());
                }

            }
        }

        public string GravaAndamento(string pRequerimento, string pViabilidade, string pCpf, string pXML, string pSecaoDestino )
        {
            //Procedure GravaAndamentoDoRequerimento (p_nr_protocolo in varchar2,       -- Protocolo
            //                                  p_nr_requerimento in varchar2,          -- Requrimento
            //                                  p_nr_protocolo_viabilidade in varchar2, -- Viabilidade
            //                                  p_cpf_funcionario in varchar2,          -- SQ_Funcionario -- CPF do usuario que fez o exame
            //                                  p_cpf_analista in varchar2,             -- SQ_Funcionario_Analista -- CPF do usuario que fez o exame
            //                                  p_co_sequencial in char,                -- CO_Sequencial '000'
            //                                  p_sq_andamento in varchar2,             -- SQ_Andamento Sequencia andamento atual 1,2,3,...
            //                                  p_despacho    in varchar2,              -- CO_Despacho '001'
            //                                  p_secao_origem in varchar2,             -- Secao_origem - A secão onde estou
            //                                  p_secao_destino in varchar2,            -- Secao_Destino - A seçao para onde vou
            //                                  p_xml           in clob,                -- XML
            //                                  p_retorno       out varchar2) is  
            try
            {

                //using (ConnectionProvider cp = new ConnectionProvider())
                //{
                //    cp.OpenConnection();
                //    cp.BeginTransaction();
                    string retorno = dHelperORACLE.GravaAndamento(_protocolo, pRequerimento, pViabilidade, pCpf,
                                                    pCpf, _co_sequencial, _sqAndamento.ToString(), "001", _secaoDestino, pSecaoDestino, pXML);

                //    cp.CommitTransaction();
                //}
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static string GetUsuarioAndamento(string protocolo, string pSecao)
        {
            using (dT017_Protocolo_Confirmacao dal = new dT017_Protocolo_Confirmacao())
            {

                return dal.GetUsuarioAndamento(protocolo, pSecao);

            }
        }

        public string getSecaoAnalise()
        {
            string _ret = "";
            foreach (bTipoSecaoAndamento o in _listTipoSecao)
            {
                if (o.Tipo == 2)
                {
                    _ret = o.Codigo;
                    break;
                }
            }
            return _ret;
        }
        #endregion
    }
}
