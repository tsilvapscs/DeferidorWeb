using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR008_Ato_Evento : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected string _t007_nr_protocolo;
        protected decimal _a003_co_evento;
        protected decimal _a002_co_ato;
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string t007_nr_protocolo
        {
            get { return _t007_nr_protocolo; }

            set { _t007_nr_protocolo = value; }
        }

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
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r005_protocolo_evento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t007_nr_protocolo, ");
            SqlI.AppendLine("	a003_co_evento, ");
            SqlI.AppendLine("   a002_co_ato ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t007_nr_protocolo, ");
            SqlI.AppendLine("	@v_a003_co_evento, ");
            SqlI.AppendLine("   @v_a002_co_ato ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R005_Protocolo_Evento Set ");
            SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            SqlU.AppendLine("		t007_nr_protocolo = @v_t007_nr_protocolo, ");
            SqlU.AppendLine("		a003_co_evento = @v_a003_co_evento, ");
            SqlU.AppendLine("       a002_co_ato = @v_a002_co_ato ");
            SqlU.AppendLine(" Where	1 = 2 ");

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t007_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t007_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a003_co_evento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a003_co_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));

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
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		t007_nr_protocolo, ");
            Sql.AppendLine("		a003_co_evento, ");
            Sql.AppendLine("        a002_co_ato ");
            Sql.AppendLine(" From	R005_Protocolo_Evento");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	t004_nr_cnpj_org_reg = _t004_nr_cnpj_org_reg");
            Sql.AppendLine(" And	t007_nr_protocolo = _t007_nr_protocolo");
            Sql.AppendLine(" And	a003_co_evento = _a003_co_evento");
            Sql.AppendLine(" And    a002_co_ato = _a002_co_ato");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("R005_Protocolo_Evento");
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

