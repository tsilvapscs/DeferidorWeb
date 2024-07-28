using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR015_Evento_Filial : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected string _t005_nr_protocolo;
        protected string _a003_co_evento;
        protected decimal _t001_sq_pessoa;
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }

            set { _t005_nr_protocolo = value; }
        }

        public string a003_co_evento
        {
            get { return _a003_co_evento; }

            set { _a003_co_evento = value; }
        }
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }
            set { _t001_sq_pessoa = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r015_evento_filial");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t005_nr_protocolo, ");
            SqlI.AppendLine("	a003_co_evento, ");
            SqlI.AppendLine("   t001_sq_pessoa ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t005_nr_protocolo, ");
            SqlI.AppendLine("	@v_a003_co_evento, ");
            SqlI.AppendLine("   @v_t001_sq_pessoa ");
            SqlI.AppendLine("  )");

            //// Codigo Update ******************* 
            //SqlU.AppendLine(" Update     R015_Evento_Filial Set ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //SqlU.AppendLine("		t005_nr_protocolo = @v_t005_nr_protocolo, ");
            //SqlU.AppendLine("		a003_co_evento = @v_a003_co_evento, ");
            //SqlU.AppendLine("       t001_sq_pessoa = @v_t001_sq_pessoa ");
            //SqlU.AppendLine(" Where	1 = 2 ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a003_co_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a003_co_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));

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

        public void Delete(int wsqPessoa)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");

            Sql.AppendLine(" From	R015_evento_filial");
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
