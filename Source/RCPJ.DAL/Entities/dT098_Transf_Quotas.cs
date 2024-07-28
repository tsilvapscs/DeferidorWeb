using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;
namespace RCPJ.DAL.Entities
{
    public class dT098_Transf_Quotas : DBInteractionBase
    {
        #region Class Member Declarations

        private string _T005_NR_PROTOCOLO;
        private int _T098_SQ_PESSOA_CEDENTE;
        private int _T098_SQ_PESSOA_CESSIONARIO;
        private decimal _T098_NR_QTD_COTAS;
        private string _T098_DS_MOTIVO;
        private int _T098_SQ_TRANSF;

        #endregion

        #region Class Property Declarations
        public string T005_NR_PROTOCOLO
        {
            get { return _T005_NR_PROTOCOLO; }
            set { _T005_NR_PROTOCOLO = value; }
        }
        public int T098_SQ_PESSOA_CEDENTE
        {
            get { return _T098_SQ_PESSOA_CEDENTE; }
            set { _T098_SQ_PESSOA_CEDENTE = value; }
        }
        public int T098_SQ_PESSOA_CESSIONARIO
        {
            get { return _T098_SQ_PESSOA_CESSIONARIO; }
            set { _T098_SQ_PESSOA_CESSIONARIO = value; }
        }
        public decimal T098_NR_QTD_COTAS
        {
            get { return _T098_NR_QTD_COTAS; }
            set { _T098_NR_QTD_COTAS = value; }
        }
        public string T098_DS_MOTIVO
        {
            get { return _T098_DS_MOTIVO; }
            set { _T098_DS_MOTIVO = value; }
        }
        public int T098_SQ_TRANSF
        {
            get { return _T098_SQ_TRANSF; }
            set { _T098_SQ_TRANSF = value; }
        }
        #endregion

        #region Implements
        public void DeleteAll()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"Delete from t098_transferencia_quotas
                          Where T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));

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

        public void Delete()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"Delete from t098_transferencia_quotas
                          Where T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO
                                And T098_SQ_PESSOA_CEDENTE = @v_T098_SQ_PESSOA_CEDENTE
                                And T098_SQ_PESSOA_CESSIONARIO = @v_T098_SQ_PESSOA_CESSIONARIO
                        ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_SQ_PESSOA_CEDENTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_SQ_PESSOA_CEDENTE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_SQ_PESSOA_CESSIONARIO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_SQ_PESSOA_CESSIONARIO));

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

        public void DeleteById()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"Delete from t098_transferencia_quotas
                          Where T098_SQ_TRANSF = @v_T098_SQ_TRANSF
                        ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_SQ_TRANSF", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_SQ_TRANSF));

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

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.Append(@"INSERT INTO t098_transferencia_quotas 
                          (
                          T005_NR_PROTOCOLO, 
                          T098_SQ_PESSOA_CEDENTE, 
                          T098_SQ_PESSOA_CESSIONARIO, 
                          T098_NR_QTD_COTAS, 
                          T098_DS_MOTIVO
                          ) 
                          VALUES 
                          (
                          @v_T005_NR_PROTOCOLO, 
                          @v_T098_SQ_PESSOA_CEDENTE, 
                          @v_T098_SQ_PESSOA_CESSIONARIO, 
                          @v_T098_NR_QTD_COTAS, 
                          @v_T098_DS_MOTIVO
                          ) ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_SQ_PESSOA_CEDENTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_SQ_PESSOA_CEDENTE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_SQ_PESSOA_CESSIONARIO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_SQ_PESSOA_CESSIONARIO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_NR_QTD_COTAS", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_NR_QTD_COTAS));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T098_DS_MOTIVO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T098_DS_MOTIVO));


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
                cmdToExecute.CommandText = "SELECT LAST_INSERT_ID()";
                _T098_SQ_TRANSF = int.Parse(cmdToExecute.ExecuteScalar().ToString());
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

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();
            //q.T098_SQ_TRANSF

            Sql.AppendLine(@" SELECT 
                                      q.T005_NR_PROTOCOLO
                                      ,q.T098_DS_MOTIVO
                                      ,q.T098_NR_QTD_COTAS
                                      ,q.T098_SQ_PESSOA_CEDENTE
                                      ,q.T098_SQ_PESSOA_CESSIONARIO
                                      ,ps.T001_DS_PESSOA nomeCedente
                                      ,ps1.T001_DS_PESSOA NomeCessionario
                                      ,pf1.T002_NR_CPF cpfCedente
                                      ,pf2.T002_NR_CPF cpfCessionario
                                      ,q.T098_SQ_TRANSF
                              
                        FROM
                          t098_transferencia_quotas q
                        INNER JOIN t001_pessoa ps
                        ON q.T098_SQ_PESSOA_CEDENTE = ps.T001_SQ_PESSOA
                        INNER JOIN t001_pessoa ps1
                        ON q.T098_SQ_PESSOA_CESSIONARIO = ps1.T001_SQ_PESSOA
                          INNER JOIN t002_pessoa_fisica PF1
                          ON PF1.T001_SQ_PESSOA = ps.T001_SQ_PESSOA
                        INNER JOIN t002_pessoa_fisica PF2
                          ON PF2.T001_SQ_PESSOA = ps1.T001_SQ_PESSOA
                         Where   1 = 1");
            Sql.AppendLine(" And	T005_NR_PROTOCOLO = '" + _T005_NR_PROTOCOLO + "'");



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t098_transferencia_quotas");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
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
        #endregion
    }
}
