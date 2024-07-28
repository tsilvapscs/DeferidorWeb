using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Cep_Uf : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _tuf_uf;
        protected string _tuf_nome;
        #endregion

        #region Class Member Declarations
        public string tuf_uf
        {
            get { return _tuf_uf; }

            set { _tuf_uf = value; }
        }

        public string tuf_nome
        {
            get { return _tuf_nome; }

            set { _tuf_nome = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_cep_uf");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tuf_uf, ");
            SqlI.AppendLine("	tuf_nome");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tuf_uf, ");
            SqlI.AppendLine("	@v_tuf_nome");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     Tab_Cep_Uf Set ");
            SqlU.AppendLine("		tuf_uf = @v_tuf_uf, ");
            SqlU.AppendLine("		tuf_nome = @v_tuf_nome");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tuf_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tuf_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tuf_nome", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tuf_nome));
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
        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		tuf_uf, ");
            Sql.AppendLine("		tuf_nome");
            Sql.AppendLine(" From	Tab_Cep_Uf");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	tuf_uf = _tuf_uf");
            Sql.AppendLine(" And	tuf_nome = _tuf_nome");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Cep_Uf");
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
        public DataTable CarregaUF()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		tuf_uf, ");
            Sql.AppendLine("		tuf_nome");
            Sql.AppendLine(" From	Tab_Cep_Uf");
            Sql.AppendLine(" Where	1 = 1 Order by tuf_nome");

            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	tuf_uf = _tuf_uf");
            //Sql.AppendLine(" And	tuf_nome = _tuf_nome");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Cep_Uf");
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


