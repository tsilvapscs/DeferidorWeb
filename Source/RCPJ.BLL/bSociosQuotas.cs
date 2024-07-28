using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bSociosQuotas : DBInteractionBase
    {
        #region Class Member Declarations
        private string _nrProtocolo;
        private int _idSeq = 0;
        private int _SQPessoa;
        private int _SQPessoaRecebe;
        private decimal _qtdQuota;
        private string _motivo;
        private string _cpfCedente;
        private string _cpfRecebedor;
        private string _nomeCedente;
        private string _nomeRecebedor;
        #endregion

        #region Class Property Declarations
        public string NrProtocolo
        {
            get { return _nrProtocolo; }
            set { _nrProtocolo = value; }
        }
        public int IdSeq
        {
            get { return _idSeq; }
            set { _idSeq = value; }
        }
        public int SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }
        public int SQPessoaRecebe
        {
            get { return _SQPessoaRecebe; }
            set { _SQPessoaRecebe = value; }
        }
        public decimal QtdQuota
        {
            get { return _qtdQuota; }
            set { _qtdQuota = value; }
        }
        public string Motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }
        public string CpfCedente
        {
            get { return _cpfCedente; }
            set { _cpfCedente = value; }
        }
        public string CpfRecebedor
        {
            get { return _cpfRecebedor; }
            set { _cpfRecebedor = value; }
        }

        public string NomeCedente
        {
            get { return _nomeCedente; }
            set { _nomeCedente = value; }
        }
        public string NomeRecebedor
        {
            get { return _nomeRecebedor; }
            set { _nomeRecebedor = value; }
        }

        #endregion

        #region Implements
        public void Update(ConnectionProvider cp)
        {
            using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
            {
                p.MainConnectionProvider = cp;
                p.T005_NR_PROTOCOLO = _nrProtocolo;
                p.T098_SQ_PESSOA_CEDENTE = _SQPessoa;
                p.T098_SQ_PESSOA_CESSIONARIO = _SQPessoaRecebe;
                p.T098_NR_QTD_COTAS = _qtdQuota;
                p.T098_DS_MOTIVO = _motivo;

                p.Update();
            }
        }
        public void Update()
        {
            using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
            {
                
                p.T005_NR_PROTOCOLO = _nrProtocolo;
                p.T098_SQ_PESSOA_CEDENTE = _SQPessoa;
                p.T098_SQ_PESSOA_CESSIONARIO = _SQPessoaRecebe;
                p.T098_NR_QTD_COTAS = _qtdQuota;
                p.T098_DS_MOTIVO = _motivo;

                p.Update();

                _idSeq = p.T098_SQ_TRANSF;
            }
        }
        public static void DeleteAll(string nrProtocolo, ConnectionProvider cp)
        {
            using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
            {
                p.MainConnectionProvider = cp;
                p.T005_NR_PROTOCOLO = nrProtocolo;
                p.DeleteAll();
            }
        }
        public void Delete()
        {
            using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
            {
                p.T005_NR_PROTOCOLO = _nrProtocolo;
                p.T098_SQ_PESSOA_CEDENTE = _SQPessoa;
                p.T098_SQ_PESSOA_CESSIONARIO = _SQPessoaRecebe;
                p.Delete();
            }
        }

        public void DeleteById()
        {
            using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
            {
                p.T098_SQ_TRANSF = _idSeq;
                p.DeleteById();
            }
        }

        public DataTable Query()
        {
            using (dT098_Transf_Quotas dal = new dT098_Transf_Quotas())
            {
                dal.T005_NR_PROTOCOLO = _nrProtocolo;
                return dal.Query();
            }
        }


        #endregion
    }
}
