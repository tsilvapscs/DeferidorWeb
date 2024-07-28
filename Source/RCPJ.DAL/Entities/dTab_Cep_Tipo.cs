using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Cep_Tipo : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _tti_clav;
        protected string _tti_nome;
        protected string _tti_abrev;
        protected string _tti_origem;
        #endregion

        #region Class Member Declarations
        public decimal tti_clav
        {
            get { return _tti_clav; }

            set { _tti_clav = value; }
        }

        public string tti_nome
        {
            get { return _tti_nome; }

            set { _tti_nome = value; }
        }

        public string tti_abrev
        {
            get { return _tti_abrev; }

            set { _tti_abrev = value; }
        }

        public string tti_origem
        {
            get { return _tti_origem; }

            set { _tti_origem = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_cep_tipo");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tti_clav, ");
            SqlI.AppendLine("	tti_nome, ");
            SqlI.AppendLine("	tti_abrev, ");
            SqlI.AppendLine("	tti_origem");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tti_clav, ");
            SqlI.AppendLine("	@v_tti_nome, ");
            SqlI.AppendLine("	@v_tti_abrev, ");
            SqlI.AppendLine("	@v_tti_origem");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     Tab_Cep_Tipo Set ");
            SqlU.AppendLine("		tti_clav = @v_tti_clav, ");
            SqlU.AppendLine("		tti_nome = @v_tti_nome, ");
            SqlU.AppendLine("		tti_abrev = @v_tti_abrev, ");
            SqlU.AppendLine("		tti_origem = @v_tti_origem");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tti_clav", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tti_clav));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tti_nome", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tti_nome));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tti_abrev", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tti_abrev));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tti_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tti_origem));
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
            Sql.AppendLine("		tti_clav, ");
            Sql.AppendLine("		tti_nome, ");
            Sql.AppendLine("		tti_abrev, ");
            Sql.AppendLine("		tti_origem");
            Sql.AppendLine(" From	Tab_Cep_Tipo");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	tti_clav = _tti_clav");
            Sql.AppendLine(" And	tti_nome = _tti_nome");
            Sql.AppendLine(" And	tti_abrev = _tti_abrev");
            Sql.AppendLine(" And	tti_origem = _tti_origem");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Cep_Tipo");
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

        public string DevolveCodigoTipoLogradouro(string wTipo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		tti_clav, ");
            Sql.AppendLine("		tti_nome, ");
            Sql.AppendLine("		tti_abrev, ");
            Sql.AppendLine("		tti_origem");
            Sql.AppendLine(" From	Tab_Cep_Tipo");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	tti_nome = '" + wTipo.ToUpper() + "'");
            //Sql.AppendLine(" And	tti_nome = _tti_nome");
            //Sql.AppendLine(" And	tti_abrev = _tti_abrev");
            //Sql.AppendLine(" And	tti_origem = _tti_origem");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Cep_Tipo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                //_mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["tti_clav"].ToString();
                else
                    return "0";

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


