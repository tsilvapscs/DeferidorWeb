using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;
namespace RCPJ.DAL.Entities
{
    public class dt018_alerta_requerimento : DBInteractionBase
    {
        #region  Property Declarations
        private string _t018_T004_NR_CNPJ_ORG_REG;
        private string _t018_T005_NR_PROTOCOLO;
        private int _t018_t097_id_alerta;
        private string _t018_valor;
        private int _t018_status = 0;
        private string _t018_usr_desativo = "";
        private DateTime _t018_dt_desativo = DateTime.Now;
        private string _descricao = "";

        #endregion

        #region Class Member Declarations



        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public string T018_T004_NR_CNPJ_ORG_REG
        {
            get { return _t018_T004_NR_CNPJ_ORG_REG; }
            set { _t018_T004_NR_CNPJ_ORG_REG = value; }
        }
        public string T018_T005_NR_PROTOCOLO
        {
            get { return _t018_T005_NR_PROTOCOLO; }
            set { _t018_T005_NR_PROTOCOLO = value; }
        }
        public int T018_t097_id_alerta
        {
            get { return _t018_t097_id_alerta; }
            set { _t018_t097_id_alerta = value; }
        }
        public string T018_valor
        {
            get { return _t018_valor; }
            set { _t018_valor = value; }
        }
        public int T018_status
        {
            get { return _t018_status; }
            set { _t018_status = value; }
        }
        public string T018_usr_desativo
        {
            get { return _t018_usr_desativo; }
            set { _t018_usr_desativo = value; }
        }
        public DateTime T018_dt_desativo
        {
            get { return _t018_dt_desativo; }
            set { _t018_dt_desativo = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t018_alerta_requerimento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t018_T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,t018_T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,t018_t097_id_alerta ");
            SqlI.AppendLine("   ,t018_valor ");
            SqlI.AppendLine("   ,t018_status ");
            SqlI.AppendLine("   ,t018_usr_desativo ");
            SqlI.AppendLine("   ,t018_dt_desativo ");

            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t018_T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,@v_t018_T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,@v_t018_t097_id_alerta ");
            SqlI.AppendLine("   ,@v_t018_valor ");
            SqlI.AppendLine("   ,@v_t018_status ");
            SqlI.AppendLine("   ,@v_t018_usr_desativo ");
            SqlI.AppendLine("   ,@v_t018_dt_desativo ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update    t018_alerta_requerimento Set ");
            SqlU.AppendLine("           t018_status = @v_t018_status, ");
            SqlU.AppendLine("           t018_usr_desativo = @v_t018_usr_desativo, ");
            SqlU.AppendLine("           t018_dt_desativo = @v_t018_dt_desativo ");
            SqlU.AppendLine(" Where	    1 = 1  ");
            SqlU.AppendLine(" And       t018_T004_NR_CNPJ_ORG_REG = @v_t018_T004_NR_CNPJ_ORG_REG");
            SqlU.AppendLine(" And       t018_T005_NR_PROTOCOLO = @v_t018_T005_NR_PROTOCOLO");
            SqlU.AppendLine(" And       t018_t097_id_alerta = @v_t018_t097_id_alerta");
            SqlU.AppendLine(" And       t018_valor = @v_t018_valor");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_T004_NR_CNPJ_ORG_REG));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_t097_id_alerta", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_t097_id_alerta));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_valor", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_valor));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_status", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_status));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_usr_desativo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_usr_desativo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t018_dt_desativo", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t018_dt_desativo));

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

            Sql.AppendLine(@"SELECT
                                t018_T004_NR_CNPJ_ORG_REG,
                                t018_T005_NR_PROTOCOLO,
                                t018_t097_id_alerta,
                                t018_valor,
                                t018_status,
                                t018_usr_desativo,
                                t018_dt_desativo
                                From t018_alerta_requerimento
                             WHERE 1 = 1");
            Sql.AppendLine(" And	t018_T004_NR_CNPJ_ORG_REG = '" + _t018_T004_NR_CNPJ_ORG_REG + "'");
            Sql.AppendLine(" And	t018_T005_NR_PROTOCOLO = '" + _t018_T005_NR_PROTOCOLO + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t018_alerta_requerimento");
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

        public DataTable QueryAlertas()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"select  a.t018_t097_id_alerta ID
                                    ,t.t097_ds_alerta Descricao
                                    ,a.t018_valor Valor
                                    ,t.t097_grupo_alerta Grupo
                                from t018_alerta_requerimento a
                                inner join t097_alerta_tipo t on t.t097_grupo_alerta = a.t018_t097_id_alerta
                             WHERE 1 = 1");
            Sql.AppendLine(" And	t018_T004_NR_CNPJ_ORG_REG = '" + _t018_T004_NR_CNPJ_ORG_REG + "'");
            Sql.AppendLine(" And	t018_T005_NR_PROTOCOLO = '" + _t018_T005_NR_PROTOCOLO + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t018_alerta_requerimento");
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
