using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Actv_Desc : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _tad_cod_atividade;
        protected string _tad_desc_atividade;
        protected decimal _tad_sequencia;
        protected string _tad_tin_cnpj;
        #endregion

        #region Class Member Declarations
        public string tad_cod_atividade
        {
            get { return _tad_cod_atividade; }

            set { _tad_cod_atividade = value; }
        }

        public string tad_desc_atividade
        {
            get { return _tad_desc_atividade; }

            set { _tad_desc_atividade = value; }
        }

        public decimal tad_sequencia
        {
            get { return _tad_sequencia; }

            set { _tad_sequencia = value; }
        }

        public string tad_tin_cnpj
        {
            get { return _tad_tin_cnpj; }

            set { _tad_tin_cnpj = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_actv_desc");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tad_cod_atividade, ");
            SqlI.AppendLine("	tad_desc_atividade, ");
            SqlI.AppendLine("	tad_sequencia, ");
            SqlI.AppendLine("	tad_tin_cnpj");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tad_cod_atividade, ");
            SqlI.AppendLine("	@v_tad_desc_atividade, ");
            SqlI.AppendLine("	@v_tad_sequencia, ");
            SqlI.AppendLine("	@v_tad_tin_cnpj");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     Tab_Actv_Desc Set ");
            SqlU.AppendLine("		tad_cod_atividade = @v_tad_cod_atividade, ");
            SqlU.AppendLine("		tad_desc_atividade = @v_tad_desc_atividade, ");
            SqlU.AppendLine("		tad_sequencia = @v_tad_sequencia, ");
            SqlU.AppendLine("		tad_tin_cnpj = @v_tad_tin_cnpj");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tad_cod_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tad_cod_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tad_desc_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tad_desc_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tad_sequencia", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tad_sequencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tad_tin_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tad_tin_cnpj));
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
            Sql.AppendLine("		tad_cod_atividade, ");
            Sql.AppendLine("		tad_desc_atividade, ");
            Sql.AppendLine("		tad_sequencia, ");
            Sql.AppendLine("		tad_tin_cnpj");
            Sql.AppendLine(" From	Tab_Actv_Desc");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	tad_cod_atividade = _tad_cod_atividade");
            Sql.AppendLine(" And	tad_desc_atividade = _tad_desc_atividade");
            Sql.AppendLine(" And	tad_sequencia = _tad_sequencia");
            Sql.AppendLine(" And	tad_tin_cnpj = _tad_tin_cnpj");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Actv_Desc");
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


