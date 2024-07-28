using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR004_Atuacao : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t001_sq_pessoa;
        protected string _a001_co_atividade;
        protected string _r004_in_principal_secundario;
        protected string _r004_exercida;
        #endregion

        #region Class Member Declarations
        public string r004_exercida
        {
            get { return _r004_exercida; }
            set { _r004_exercida = value; }
        }
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }

            set { _t001_sq_pessoa = value; }
        }

        public string a001_co_atividade
        {
            get { return _a001_co_atividade; }

            set { _a001_co_atividade = value; }
        }

        public string r004_in_principal_secundario
        {
            get { return _r004_in_principal_secundario; }

            set { _r004_in_principal_secundario = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r004_atuacao");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	a001_co_atividade, ");
            SqlI.AppendLine("	r004_in_principal_secundario, ");
            SqlI.AppendLine("	r004_exercida");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	@v_a001_co_atividade, ");
            SqlI.AppendLine("	@v_r004_in_principal_secundario, ");
            SqlI.AppendLine("	@v_r004_exercida");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R004_Atuacao Set ");
            SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");
            SqlU.AppendLine("		a001_co_atividade = @v_a001_co_atividade, ");
            SqlU.AppendLine("		r004_in_principal_secundario = @v_r004_in_principal_secundario, ");
            SqlU.AppendLine("		r004_exercida = @v_r004_exercida");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!
            SqlU.AppendLine(" And	t001_sq_pessoa = " + _t001_sq_pessoa);
            SqlU.AppendLine(" And	a001_co_atividade = " + _a001_co_atividade);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a001_co_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_co_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r004_in_principal_secundario", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r004_in_principal_secundario));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r004_exercida", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r004_exercida));

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
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();
                }
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

        public void Insert()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into r004_atuacao");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	a001_co_atividade, ");
            SqlI.AppendLine("	r004_in_principal_secundario, ");
            SqlI.AppendLine("	r004_exercida");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	@v_a001_co_atividade, ");
            SqlI.AppendLine("	@v_r004_in_principal_secundario, ");
            SqlI.AppendLine("	@v_r004_exercida");
            SqlI.AppendLine("  )");

          
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a001_co_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_co_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r004_in_principal_secundario", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r004_in_principal_secundario));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r004_exercida", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r004_exercida));

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
        
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		a001_co_atividade, ");
            Sql.AppendLine("		r004_in_principal_secundario, ");
            Sql.AppendLine("		r004_exercida");
            Sql.AppendLine(" From	R004_Atuacao");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	t001_sq_pessoa = _t001_sq_pessoa");
            Sql.AppendLine(" And	a001_co_atividade = _a001_co_atividade");
            Sql.AppendLine(" And	r004_in_principal_secundario = _r004_in_principal_secundario");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("R004_Atuacao");
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
        public void Deleta(int wsqPessoa)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");

            Sql.AppendLine(" From	R004_Atuacao");
            Sql.AppendLine(" Where	t001_sq_pessoa = " + wsqPessoa);


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

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


            try
            {
                // Open connection. 
                if (_mainConnection.State != ConnectionState.Open)
                    _mainConnection.Open();

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
                // Close connection. 
                if (_mainConnectionIsCreatedLocal)
                {

                    // Close connection. 
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

           
        }

        #endregion
    }
}


