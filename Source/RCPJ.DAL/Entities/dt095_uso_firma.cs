using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dt095_uso_firma : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations

        protected string _t005_protocolo;
        protected int _t095_sq_pessoa_adm;
        protected int _t095_sq_pessoa_conjunto;

        #endregion

        #region Class Member Declarations
        public string t005_protocolo
        {
            get { return _t005_protocolo; }

            set { _t005_protocolo = value; }
        }

        public int t095_sq_pessoa_adm
        {
            get { return _t095_sq_pessoa_adm; }

            set { _t095_sq_pessoa_adm = value; }
        }

        public int t095_sq_pessoa_conjunto
        {
            get { return _t095_sq_pessoa_conjunto; }

            set { _t095_sq_pessoa_conjunto = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into t095_uso_firma");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t005_protocolo, ");
            SqlI.AppendLine("	t095_sq_pessoa_adm, ");
            SqlI.AppendLine("	t095_sq_pessoa_conjunto ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t005_protocolo, ");
            SqlI.AppendLine("	@v_t095_sq_pessoa_adm, ");
            SqlI.AppendLine("	@v_t095_sq_pessoa_conjunto ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t095_sq_pessoa_adm", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t095_sq_pessoa_adm));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t095_sq_pessoa_conjunto", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t095_sq_pessoa_conjunto));

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
        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"SELECT 
                                    u.T005_PROTOCOLO,
                                    u.t095_sq_pessoa_adm,
                                    p.T001_DS_PESSOA NomeAdm,
                                    pf.T002_NR_CPF CpfAdm,
                                    u.t095_sq_pessoa_conjunto,
                                    p2.T001_DS_PESSOA NomeAdmConj,
                                    pf2.T002_NR_CPF CpfAdmConj

                            FROM
                             t095_uso_firma u
                              inner join t001_pessoa p
                              on p.T001_SQ_PESSOA = u.T095_SQ_PESSOA_ADM
                              inner join t002_pessoa_fisica pf
                              on pf.T001_SQ_PESSOA = u.T095_SQ_PESSOA_ADM
                              inner join t001_pessoa p2
                              on p2.T001_SQ_PESSOA = u.t095_sq_pessoa_conjunto
                              inner join t002_pessoa_fisica pf2
                              on pf2.T001_SQ_PESSOA = u.t095_sq_pessoa_conjunto
                         Where   1 = 1");
            Sql.AppendLine(" And	T005_PROTOCOLO = '" + _t005_protocolo + "'");
            Sql.AppendLine(" And	t095_sq_pessoa_adm = " + _t095_sq_pessoa_adm.ToString() );




            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t095_uso_firma");
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

        public void Delete()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"Delete from t095_uso_firma
                          Where T005_PROTOCOLO = @v_T005_PROTOCOLO
                                And t095_sq_pessoa_adm = @v_t095_sq_pessoa_adm
                        ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t095_sq_pessoa_adm", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t095_sq_pessoa_adm));

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
        #endregion
    }
} 
