using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bParecer : DBInteractionBase
    {
        #region Variables
        private string _T005_PROTOCOLO = "";
        private int _t080_id_parecer = 0;
        private string _t080_cpf_solicitante = "";
        private string _t080_tx_solicitado = "";
        private string _t080_cpf_parecer = "";
        private string _t080_tx_resposta = "";
        private string _t005_protocolo_OR = "";
        private DateTime _t080_dt_resposta;
        private DateTime _t080_dt_solicitacao;
        private string _t080_nome_solicitante = "";
        private string _t080_nome_pessoa_parecer = "";
        private string _t080_nr_parecer = "";

        private string _secaoOrigem = "";
        private string _secaoDestino = "";
        private int _sqFuncionario = 0;

        private List<bParecer> _listParecer = new List<bParecer>();

        private List<bSecaoParecer> _listSecoesEnvio = new List<bSecaoParecer>();
        #endregion

        #region Class Member Declarations
        public string protocolo_OR
        {
            get { return _t005_protocolo_OR; }
            set { _t005_protocolo_OR = value; }
        }
        public string PROTOCOLO
        {
            get { return _T005_PROTOCOLO; }
            set { _T005_PROTOCOLO = value; }
        }
        public int id_parecer
        {
            get { return _t080_id_parecer; }
            set { _t080_id_parecer = value; }
        }
        public string cpf_solicitante
        {
            get { return _t080_cpf_solicitante; }
            set { _t080_cpf_solicitante = value; }
        }
        public string texto_solicitado
        {
            get { return _t080_tx_solicitado; }
            set { _t080_tx_solicitado = value; }
        }
        public string cpf_parecer
        {
            get { return _t080_cpf_parecer; }
            set { _t080_cpf_parecer = value; }
        }
        public string tx_resposta
        {
            get { return _t080_tx_resposta; }
            set { _t080_tx_resposta = value; }
        }
        public List<bParecer> ListParecer
        {
            get { return _listParecer; }
            
        }
        public DateTime DataResposta
        {
            get { return _t080_dt_resposta; }
            set { _t080_dt_resposta = value; }
        }
        public DateTime DataSolicitacao
        {
            get { return _t080_dt_solicitacao; }
            set { _t080_dt_solicitacao = value; }
        }
        public string NomeSolicitante
        {
            get { return _t080_nome_solicitante; }
            set { _t080_nome_solicitante = value; }
        }
        public string NomePessoaParecer
        {
            get { return _t080_nome_pessoa_parecer; }
            set { _t080_nome_pessoa_parecer = value; }
        }
        public string NrParecer
        {
            get { return _t080_nr_parecer; }
            set { _t080_nr_parecer = value; }
        }
        public string SecaoOrigem
        {
            get { return _secaoOrigem; }
            set { _secaoOrigem = value; }
        }
        public string SecaoDestino
        {
            get { return _secaoDestino; }
            set { _secaoDestino = value; }
        }
        
        public int SqFuncionario
        {
            get { return _sqFuncionario; }
            set { _sqFuncionario = value; }
        }

        #endregion


         public bParecer()
        {
        }

        public bParecer(int pId)
            : this()
        {
            _t080_id_parecer = pId;
            Populate();
        }

        public bParecer(string pProtocolo)
            : this()
        {
            _T005_PROTOCOLO = pProtocolo;
            PopulateByProtocolo();
        }
        private void Populate()
        {

            using (dParecer obj = new dParecer())
            {
                obj.id_parecer = _t080_id_parecer;
                DataTable dt = obj.Query();
                foreach (DataRow dr in dt.Rows)
                {

                    _T005_PROTOCOLO = dr["T005_PROTOCOLO"].ToString();
                    _t080_id_parecer = Int32.Parse(dr["t080_id_parecer"].ToString());
                    _t080_cpf_solicitante = dr["t080_cpf_solicitante"].ToString();
                    _t080_tx_solicitado = dr["t080_tx_solicitado"].ToString();
                    _t080_cpf_parecer = dr["t080_cpf_parecer"].ToString();
                    _t080_tx_resposta = dr["t080_tx_resposta"].ToString();
                    _t005_protocolo_OR = dr["t005_protocolo_OR"].ToString();
                    if(!string.IsNullOrEmpty(dr["t080_dt_resposta"].ToString())) 
                        _t080_dt_resposta = DateTime.Parse(dr["t080_dt_resposta"].ToString());

                    _t080_dt_solicitacao = DateTime.Parse(dr["t080_dt_solicitacao"].ToString());


                    _t080_nome_solicitante = dr["t080_nome_solicitante"].ToString();
                    _t080_nome_pessoa_parecer = dr["t080_nome_pessoa_parecer"].ToString();
                    _t080_nr_parecer = dr["t080_nr_parecer"].ToString();
                    _secaoOrigem = dr["t080_secao_origem"].ToString();
                    _secaoDestino = dr["t080_secao_destino"].ToString();
                    _sqFuncionario = Int32.Parse(dr["t080_sq_funcionario"].ToString());
                    
                }

            }
           

        }

        private void PopulateByProtocolo()
        {

            using (dParecer obj = new dParecer())
            {
                obj.PROTOCOLO = _T005_PROTOCOLO;
                DataTable dt = obj.QueryAll();
                foreach (DataRow dr in dt.Rows)
                {
                    bParecer _parecer = new bParecer();
                    _parecer.PROTOCOLO = dr["T005_PROTOCOLO"].ToString();
                    _parecer.id_parecer = Int32.Parse(dr["t080_id_parecer"].ToString());
                    _parecer.cpf_solicitante = dr["t080_cpf_solicitante"].ToString();
                    _parecer.texto_solicitado = dr["t080_tx_solicitado"].ToString();
                    _parecer.cpf_parecer = dr["t080_cpf_parecer"].ToString();
                    _parecer.tx_resposta = dr["t080_tx_resposta"].ToString();
                    _parecer.protocolo_OR = dr["t005_protocolo_OR"].ToString();
                    
                    if (!string.IsNullOrEmpty(dr["t080_dt_resposta"].ToString()))
                        _parecer.DataResposta = DateTime.Parse(dr["t080_dt_resposta"].ToString());

                    _parecer.DataSolicitacao= DateTime.Parse(dr["t080_dt_solicitacao"].ToString());


                    _parecer.NomeSolicitante = dr["t080_nome_solicitante"].ToString();
                    _parecer.NomePessoaParecer = dr["t080_nome_pessoa_parecer"].ToString();
                    _parecer.NrParecer = dr["t080_nr_parecer"].ToString();
                    _secaoOrigem = dr["t080_secao_origem"].ToString();
                    _sqFuncionario = Int32.Parse(dr["t080_sq_funcionario"].ToString());
                    _secaoDestino = dr["t080_secao_destino"].ToString();
                    _listParecer.Add(_parecer);

                }

            }


        }

        public void CriaSolicitacao()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dParecer fp = new dParecer())
                    {
                        fp.MainConnectionProvider = cp;

                        fp.PROTOCOLO = _T005_PROTOCOLO;
                        fp.cpf_solicitante = _t080_cpf_solicitante;
                        fp.texto_solicitado = _t080_tx_solicitado;
                        fp.protocolo_OR = _t005_protocolo_OR;
                        fp.SecaoOrigem = _secaoOrigem;
                        fp.SecaoDestino = _secaoDestino;
                        fp.SqFuncionario = _sqFuncionario;
                        fp.GravaSoliicitacao();

                    }


                    cp.CommitTransaction();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
           
        }

        public void GravaRespostaSoliicitacao()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dParecer fp = new dParecer())
                    {
                        fp.MainConnectionProvider = cp;

                        string nr_parecer = fp.GetCorrelativo();

                        fp.PROTOCOLO = _T005_PROTOCOLO;
                        fp.id_parecer = _t080_id_parecer;
                        fp.cpf_parecer = _t080_cpf_parecer;
                        fp.tx_resposta = _t080_tx_resposta;
                        fp.DataResposta = DateTime.Now;
                        fp.T080_nome_pessoa_parecer = _t080_nome_pessoa_parecer;
                        fp.T080_nr_parecer = nr_parecer;
                        fp.GravaRespostaSoliicitacao();

                    }


                    cp.CommitTransaction();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public static DataTable QueryProcessosAnalisar(string _protocolo_or, string _pREquerimento)
        {
            using (dParecer fp = new dParecer())
            {
                if (_protocolo_or != "")
                    fp.protocolo_OR = _protocolo_or;

                if (_pREquerimento != "")
                    fp.PROTOCOLO = _pREquerimento;

                fp.ListaNaoAnalisados = true;
                return fp.QueryAll();
            }
        }

        public static DataTable QueryConsultaParecer(string _nrParecer, string _solicitacao, string _resposta)
        {
            using (dParecer fp = new dParecer())
            {
                if (_nrParecer != "")
                    fp.T080_nr_parecer = _nrParecer;

                if (_solicitacao != "")
                    fp.texto_solicitado = _solicitacao;

                if (_resposta != "")
                    fp.tx_resposta = _resposta;

                return fp.QueryConsultaParecer();

            }
        }

        public static bool ExisteRespostaParecer(string _protocolo)
        {
            using (dParecer dal = new dParecer())
            {
                return dal.ExisteRespostaParecer(_protocolo);
            }
        }

        public List<bSecaoParecer> ListaSecaoEnvio(string _secaoOrigem)
        {
             using (dParecer fp = new dParecer())
            {

                DataTable dt = fp.QuerySecoesDestino(_secaoOrigem);
                foreach (DataRow dr in dt.Rows)
                {
                    bSecaoParecer sp = new bSecaoParecer();
                    sp.SecaoOrigem = dr["SecaoOrigem"].ToString();
                    sp.SecaoDestino = dr["secaoDestino"].ToString();
                    sp.NomeSecaoDestino = dr["NomeSecaoDestino"].ToString();

                    _listSecoesEnvio.Add(sp);

                }
            
             }

             return _listSecoesEnvio;
        }
    }
}
