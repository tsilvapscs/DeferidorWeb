using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dParecer : DBInteractionBase
    {

        #region Variables
        private string _T005_PROTOCOLO = "";
        private int _t080_id_parecer = 0;
        private string _t080_cpf_solicitante = "";
        private string _t080_tx_solicitado = "";
        private string _t080_cpf_parecer = "";
        private string _t080_tx_resposta = "";
        private bool _listaNaoAnalisados = false;
        private string _t005_protocolo_OR = "";
        private DateTime _t080_dt_resposta;
        private string _t080_nome_solicitante = "";
        private string _t080_nome_pessoa_parecer = "";
        private string _t080_nr_parecer = "";
        private string _t080_secao_origem = "";
        private int _t080_sq_funcionario = 0;
        private string _t080_secao_destino = "";
        #endregion

        #region Class Member Declarations
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
        public bool ListaNaoAnalisados
        {
            get { return _listaNaoAnalisados; }
            set { _listaNaoAnalisados = value; }
        }
        public string protocolo_OR
        {
            get { return _t005_protocolo_OR; }
            set { _t005_protocolo_OR = value; }
        }
        public DateTime DataResposta
        {
            get { return _t080_dt_resposta; }
            set { _t080_dt_resposta = value; }
        }
        public string T080_nome_solicitante
        {
            get { return _t080_nome_solicitante; }
            set { _t080_nome_solicitante = value; }
        }
        public string T080_nome_pessoa_parecer
        {
            get { return _t080_nome_pessoa_parecer; }
            set { _t080_nome_pessoa_parecer = value; }
        }
        public string T080_nr_parecer
        {
            get { return _t080_nr_parecer; }
            set { _t080_nr_parecer = value; }
        }

        public string SecaoOrigem
        {
            get { return _t080_secao_origem; }
            set { _t080_secao_origem = value; }
        }
        public string SecaoDestino
        {
            get { return _t080_secao_destino; }
            set { _t080_secao_destino = value; }
        }
        public int SqFuncionario
        {
            get { return _t080_sq_funcionario; }
            set { _t080_sq_funcionario = value; }
        }
        #endregion

        public bool ExisteRespostaParecer(string _protocolo)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT Count(*) FROM t080_parecer_processo ");
            Sql.AppendLine(" WHERE t005_protocolo_OR = '" + _protocolo + "' and t080_dt_resposta is not null");
            
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("parecer_processo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                return (int.Parse(toReturn.Rows[0][0].ToString()) > 0);

            }
            catch (Exception ex)
            {

                // some error occured. Bubble it to caller and encapsulate Exception object 

                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

        }

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select *   ");
            Sql.AppendLine(" from t080_parecer_processo   ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  t080_id_parecer = '" + _t080_id_parecer + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {

                // some error occured. Bubble it to caller and encapsulate Exception object 

                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable QueryAll()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select  *  ");
            Sql.AppendLine(" from t080_parecer_processo   ");
            Sql.AppendLine(" where 1 = 1   ");
            if(_T005_PROTOCOLO != "")
                Sql.AppendLine(" and  T005_PROTOCOLO = '" + _T005_PROTOCOLO + "'");

            if (_t005_protocolo_OR != "")
                Sql.AppendLine(" and  t005_protocolo_OR = '" + _t005_protocolo_OR + "'");

            if(_listaNaoAnalisados)
                Sql.AppendLine(" and  t080_tx_resposta is null or t080_tx_resposta = ''");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {

                // some error occured. Bubble it to caller and encapsulate Exception object 

                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void GravaSoliicitacao()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into t080_parecer_processo");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T005_PROTOCOLO ");
            SqlI.AppendLine("	,t080_cpf_solicitante ");
            SqlI.AppendLine("	,t080_tx_solicitado ");
            SqlI.AppendLine("	,t005_protocolo_OR ");
            SqlI.AppendLine("	,t080_nome_solicitante ");
            SqlI.AppendLine("   ,t080_secao_origem ");
            SqlI.AppendLine("   ,t080_secao_destino ");
            SqlI.AppendLine("   ,t080_sq_funcionario ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T005_PROTOCOLO ");
            SqlI.AppendLine("	,@t080_cpf_solicitante ");
            SqlI.AppendLine("	,@t080_tx_solicitado ");
            SqlI.AppendLine("	,@t005_protocolo_OR ");
            SqlI.AppendLine("	,@t080_nome_solicitante ");
            SqlI.AppendLine("   ,@t080_secao_origem ");
            SqlI.AppendLine("   ,@t080_secao_destino ");
            SqlI.AppendLine("   ,@t080_sq_funcionario ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_cpf_solicitante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_cpf_solicitante));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_tx_solicitado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_tx_solicitado));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t005_protocolo_OR", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo_OR));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_nome_solicitante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_nome_solicitante));

                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_secao_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_secao_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_secao_destino", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_secao_destino));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_sq_funcionario", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_sq_funcionario));


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

                // Execute query.

                cmdToExecute.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }

        }

        public void GravaRespostaSoliicitacao()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Update t080_parecer_processo");
            SqlI.AppendLine("  SET ");
            SqlI.AppendLine("	t080_cpf_parecer = @t080_cpf_parecer");
            SqlI.AppendLine("	,t080_tx_resposta = @t080_tx_resposta");
            SqlI.AppendLine("	,t080_dt_resposta = @t080_dt_resposta");
            SqlI.AppendLine("	,t080_nome_pessoa_parecer = @t080_nome_pessoa_parecer");
            SqlI.AppendLine("	,t080_nr_parecer = @t080_nr_parecer");

            // SqlI.AppendLine(" where T005_PROTOCOLO = @T005_PROTOCOLO ");
            SqlI.AppendLine("	where  t080_id_parecer = @t080_id_parecer ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T005_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_id_parecer", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_id_parecer));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_cpf_parecer", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_cpf_parecer));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_tx_resposta", MySqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_tx_resposta));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_dt_resposta", MySqlDbType.DateTime, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_dt_resposta));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_nome_pessoa_parecer", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_nome_pessoa_parecer));
                cmdToExecute.Parameters.Add(new MySqlParameter("@t080_nr_parecer", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t080_nr_parecer));

                
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

                // Execute query.

                cmdToExecute.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }

        }

        public string GetCorrelativo()
        {

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "GetNumeroParecer";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                int _tipoCorrelativo = 55;

                cmdToExecute.Parameters.Add(new MySqlParameter("pTipo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipoCorrelativo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_vpv_cod_protocolo", MySqlDbType.VarChar, 20, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, ""));

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
                
                return cmdToExecute.Parameters["v_vpv_cod_protocolo"].Value.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();

            }



           
        }

        public DataTable QueryConsultaParecer()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select  p.*   ");
            Sql.AppendLine(" from t080_parecer_processo p  ");
            Sql.AppendLine(" where 1 = 1   ");
            if (_t080_nr_parecer != "")
                Sql.AppendLine(" and  p.T080_NR_PARECER = '" + _t080_nr_parecer + "'");

            if (!string.IsNullOrEmpty(_t080_tx_solicitado))
                Sql.AppendLine(" and  p.t080_tx_solicitado like '%" + _t080_tx_solicitado + "%'");

            if (!string.IsNullOrEmpty(_t080_tx_resposta))
                Sql.AppendLine(" and  p.t080_tx_resposta like '%" + _t080_tx_resposta + "%'");

            Sql.AppendLine(" and  p.T080_NR_PARECER <> ''");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {

                // some error occured. Bubble it to caller and encapsulate Exception object 

                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable QuerySecoesDestino(string _secaoOrigem)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" Select a028_cod_secao_origem SecaoOrigem, a028_co_secao_destino secaoDestino , a028_ds_secao_destino NomeSecaoDestino
                              From a028_secao_envio_parecer
                              Where a028_cod_secao_origem = '" + _secaoOrigem + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {

                // some error occured. Bubble it to caller and encapsulate Exception object 

                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }
    }
}
