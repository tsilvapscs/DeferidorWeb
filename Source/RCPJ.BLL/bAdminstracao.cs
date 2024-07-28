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
    public class bAdminstracao : DBInteractionBase
    {
        #region Class Member Declarations
        private string _nrProtocolo;
        private int _SQPessoa;
        private int _SQPessoaConjunto;
        private string _cpfAdm;
        private string _cpfAdmConjunto;
        private string _nomeAdm;
        private string _nomeAdmConjunto;
        #endregion

        #region Class Property Declarations
        public string NrProtocolo
        {
            get { return _nrProtocolo; }
            set { _nrProtocolo = value; }
        }
        public int SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }
        public int SQPessoaConjunto
        {
            get { return _SQPessoaConjunto; }
            set { _SQPessoaConjunto = value; }
        }
        public string CpfAdm
        {
            get { return _cpfAdm; }
            set { _cpfAdm = value; }
        }
        public string CpfAdmConjunto
        {
            get { return _cpfAdmConjunto; }
            set { _cpfAdmConjunto = value; }
        }

        public string NomeAdm
        {
            get { return _nomeAdm; }
            set { _nomeAdm = value; }
        }
        public string NomeAdmConjunto
        {
            get { return _nomeAdmConjunto; }
            set { _nomeAdmConjunto = value; }
        }

        #endregion

        #region Implements
        public void Update()
        {
            using (dt095_uso_firma p = new dt095_uso_firma())
            {
                p.MainConnectionProvider = MainConnectionProvider;

                p.t005_protocolo= _nrProtocolo;
                p.t095_sq_pessoa_adm = _SQPessoa;
                p.t095_sq_pessoa_conjunto = _SQPessoaConjunto;

                p.Update();
            }
        }

        public void Delete()
        {
            using (dt095_uso_firma p = new dt095_uso_firma())
            {
                p.t005_protocolo = _nrProtocolo;
                p.t095_sq_pessoa_adm = _SQPessoa;
                p.Delete();
            }
        }

        //public static void DeleteAll(string nrProtocolo, ConnectionProvider cp)
        //{
        //    using (dT098_Transf_Quotas p = new dT098_Transf_Quotas())
        //    {
        //        p.MainConnectionProvider = cp;
        //        p.T005_NR_PROTOCOLO = nrProtocolo;
        //        p.DeleteAll();
        //    }
        //}
        

        //public DataTable Query()
        //{
        //    using (dT098_Transf_Quotas dal = new dT098_Transf_Quotas())
        //    {
        //        dal.T005_NR_PROTOCOLO = _nrProtocolo;
        //        return dal.Query();
        //    }
        //}


        #endregion
    }
}
