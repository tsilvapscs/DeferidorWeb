using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dt094_div_dbe : DBInteractionBase
    {
        #region  Property Declarations
        protected string _t094_NR_CNPJ_ORG_REG;
        protected string _t094_T005_NR_PROTOCOLO;
        protected int _t094_cod_divergencia;
        protected string _t094_ds_divergencia;
        #endregion

        #region Class Member Declarations
        public string NumeroOrgaoRegistro
        {
            get { return _t094_NR_CNPJ_ORG_REG; }
            set { _t094_NR_CNPJ_ORG_REG = value; }
        }
        public string NumeroProtocolo
        {
            get { return _t094_T005_NR_PROTOCOLO; }

            set { _t094_T005_NR_PROTOCOLO = value; }
        }

        public int Item
        {
            get { return _t094_cod_divergencia; }

            set { _t094_cod_divergencia = value; }
        }
        public string Texto
        {
            get { return _t094_ds_divergencia; }

            set { _t094_ds_divergencia = value; }
        }
        #endregion

        #region Implements
        public DataTable Query()
        {
            string sql = "";
            sql = "Select * from t094_div_dbe_viab where t094_T005_NR_PROTOCOLO = '" + _t094_T005_NR_PROTOCOLO + "'";

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t094_div_dbe_viab");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
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

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t094_div_dbe_viab");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t094_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	t094_T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	t094_cod_divergencia, ");
            SqlI.AppendLine("	t094_ds_divergencia ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t094_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	@v_t094_T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	@v_t094_cod_divergencia, ");
            SqlI.AppendLine("	@v_t094_ds_divergencia ");
            SqlI.AppendLine("  )");

            SqlU.AppendLine(" Update     t094_div_dbe_viab Set ");
            SqlU.AppendLine("		t094_NR_CNPJ_ORG_REG = @v_t094_NR_CNPJ_ORG_REG, ");
            SqlU.AppendLine("		t094_T005_NR_PROTOCOLO = @v_t094_T005_NR_PROTOCOLO, ");
            SqlU.AppendLine("		t094_cod_divergencia = @v_t094_cod_divergencia, ");
            SqlU.AppendLine("		t094_ds_divergencia = @v_t094_ds_divergencia ");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t094_NR_CNPJ_ORG_REG = @v_t094_NR_CNPJ_ORG_REG ");
            SqlU.AppendLine(" And	t094_T005_NR_PROTOCOLO = @v_t094_T005_NR_PROTOCOLO ");
            SqlU.AppendLine(" And	t094_cod_divergencia = @v_t094_cod_divergencia ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t094_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t094_NR_CNPJ_ORG_REG));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t094_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t094_T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t094_cod_divergencia", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t094_cod_divergencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t094_ds_divergencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t094_ds_divergencia));

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
        #endregion
    }
}
