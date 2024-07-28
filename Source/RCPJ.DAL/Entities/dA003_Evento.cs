using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dA003_Evento : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _a003_co_evento;
        protected decimal _a002_co_ato;
        protected string _a003_nr_evento;
        protected string _a003_ds_evento;
        #endregion

        #region Class Member Declarations
        public decimal a003_co_evento
        {
            get { return _a003_co_evento; }

            set { _a003_co_evento = value; }
        }

        public decimal a002_co_ato
        {
            get { return _a002_co_ato; }

            set { _a002_co_ato = value; }
        }

        public string a003_nr_evento
        {
            get { return _a003_nr_evento; }

            set { _a003_nr_evento = value; }
        }

        public string a003_ds_evento
        {
            get { return _a003_ds_evento; }

            set { _a003_ds_evento = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into a003_evento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	a003_co_evento, ");
            SqlI.AppendLine("	a002_co_ato, ");
            SqlI.AppendLine("	a003_nr_evento, ");
            SqlI.AppendLine("	a003_ds_evento");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_a003_co_evento, ");
            SqlI.AppendLine("	@v_a002_co_ato, ");
            SqlI.AppendLine("	@v_a003_nr_evento, ");
            SqlI.AppendLine("	@v_a003_ds_evento");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     A003_Evento Set ");
            SqlU.AppendLine("		a003_co_evento = @v_a003_co_evento, ");
            SqlU.AppendLine("		a002_co_ato = @v_a002_co_ato, ");
            SqlU.AppendLine("		a003_nr_evento = @v_a003_nr_evento, ");
            SqlU.AppendLine("		a003_ds_evento = @v_a003_ds_evento");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a003_co_evento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a003_co_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a003_nr_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a003_nr_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a003_ds_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a003_ds_evento));
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
            Sql.AppendLine("		a003_co_evento, ");
            Sql.AppendLine("		a002_co_ato, ");
            Sql.AppendLine("		a003_nr_evento, ");
            Sql.AppendLine("		a003_ds_evento");
            Sql.AppendLine(" From	A003_Evento");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	a003_co_evento = _a003_co_evento");
            Sql.AppendLine(" And	a002_co_ato = _a002_co_ato");
            Sql.AppendLine(" And	a003_nr_evento = _a003_nr_evento");
            Sql.AppendLine(" And	a003_ds_evento = _a003_ds_evento");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A003_Evento");
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


