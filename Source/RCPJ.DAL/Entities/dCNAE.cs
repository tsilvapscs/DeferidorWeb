using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.ConnectionBase;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dCNAE : DBInteractionBase
    {
        #region Class Member Declarations
        private string _CodigoCNAE;
        private int _TipoAtividade;
        private string _Descricao = "";
        private string _ProtocoloRequerimento;
        private int _sqPessoa;
        #endregion

        #region Class Property Declarations
        public String CodigoCNAE
        {
            get
            {
                return (String)_CodigoCNAE;
            }
            set
            {
                _CodigoCNAE = value;
            }
        }
        public int TipoAtividade
        {
            get
            {
                return (int)_TipoAtividade;
            }
            set
            {
                _TipoAtividade = value;
            }
        }
        public String Descricao
        {
            get { return _Descricao; }
        }
        public string ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }
        public int SqPessoa
        {
            get { return _sqPessoa; }
            set { _sqPessoa = value; }
        }
        #endregion

        public DataTable Query()
        {
            //Fazer a query

            return new DataTable();

        }
        public void Delete()
        {

            StringBuilder SqlD = new StringBuilder();

            SqlD.AppendLine(" Delete from r004_atuacao");
            SqlD.AppendLine(" Where  ");
            SqlD.AppendLine("	T001_SQ_PESSOA = " + _sqPessoa);
            SqlD.AppendLine("	And A001_CO_ATIVIDADE = " + _CodigoCNAE);


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection. 
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
    }
}
