using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Actv_Econ : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _tae_cod_actvd;
        protected string _tae_desc;
        protected string _tae_taf_form_ativ;
        protected decimal _tae_status;
        protected decimal _tae_desc_adicional;
        protected string _tae_versao;
        protected decimal _tae_flag_mei;
        protected decimal _tae_flag_mei_fora_lei;
        #endregion

        #region Class Member Declarations
        public string tae_cod_actvd
        {
            get { return _tae_cod_actvd; }

            set { _tae_cod_actvd = value; }
        }

        public string tae_desc
        {
            get { return _tae_desc; }

            set { _tae_desc = value; }
        }

        public string tae_taf_form_ativ
        {
            get { return _tae_taf_form_ativ; }

            set { _tae_taf_form_ativ = value; }
        }

        public decimal tae_status
        {
            get { return _tae_status; }

            set { _tae_status = value; }
        }

        public decimal tae_desc_adicional
        {
            get { return _tae_desc_adicional; }

            set { _tae_desc_adicional = value; }
        }

        public string tae_versao
        {
            get { return _tae_versao; }

            set { _tae_versao = value; }
        }

        public decimal tae_flag_mei
        {
            get { return _tae_flag_mei; }

            set { _tae_flag_mei = value; }
        }

        public decimal tae_flag_mei_fora_lei
        {
            get { return _tae_flag_mei_fora_lei; }

            set { _tae_flag_mei_fora_lei = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_actv_econ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tae_cod_actvd, ");
            SqlI.AppendLine("	tae_desc, ");
            SqlI.AppendLine("	tae_taf_form_ativ, ");
            SqlI.AppendLine("	tae_status, ");
            SqlI.AppendLine("	tae_desc_adicional, ");
            SqlI.AppendLine("	tae_versao, ");
            SqlI.AppendLine("	tae_flag_mei, ");
            SqlI.AppendLine("	tae_flag_mei_fora_lei");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tae_cod_actvd, ");
            SqlI.AppendLine("	@v_tae_desc, ");
            SqlI.AppendLine("	@v_tae_taf_form_ativ, ");
            SqlI.AppendLine("	@v_tae_status, ");
            SqlI.AppendLine("	@v_tae_desc_adicional, ");
            SqlI.AppendLine("	@v_tae_versao, ");
            SqlI.AppendLine("	@v_tae_flag_mei, ");
            SqlI.AppendLine("	@v_tae_flag_mei_fora_lei");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     tab_actv_econ Set ");
           // SqlU.AppendLine("		tae_cod_actvd = @v_tae_cod_actvd, ");
            SqlU.AppendLine("		tae_desc = @v_tae_desc, ");
            SqlU.AppendLine("		tae_taf_form_ativ = @v_tae_taf_form_ativ, ");
            SqlU.AppendLine("		tae_status = @v_tae_status, ");
            SqlU.AppendLine("		tae_desc_adicional = @v_tae_desc_adicional, ");
            SqlU.AppendLine("		tae_versao = @v_tae_versao, ");
            SqlU.AppendLine("		tae_flag_mei = @v_tae_flag_mei, ");
            SqlU.AppendLine("		tae_flag_mei_fora_lei = @v_tae_flag_mei_fora_lei");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" and  tae_cod_actvd = '" + _tae_cod_actvd + "'");
            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_cod_actvd", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_cod_actvd));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_desc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_desc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_taf_form_ativ", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_taf_form_ativ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_status", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_status));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_desc_adicional", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_desc_adicional));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_versao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_versao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_flag_mei", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_flag_mei));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tae_flag_mei_fora_lei", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tae_flag_mei_fora_lei));
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
            Sql.AppendLine("		tae_cod_actvd, ");
            Sql.AppendLine("		tae_desc, ");
            Sql.AppendLine("		tae_taf_form_ativ, ");
            Sql.AppendLine("		tae_status, ");
            Sql.AppendLine("		tae_desc_adicional, ");
            Sql.AppendLine("		tae_versao, ");
            Sql.AppendLine("		tae_flag_mei, ");
            Sql.AppendLine("		tae_flag_mei_fora_lei");
            Sql.AppendLine(" From	Tab_Actv_Econ");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	tae_cod_actvd = _tae_cod_actvd");
            Sql.AppendLine(" And	tae_desc = _tae_desc");
            Sql.AppendLine(" And	tae_taf_form_ativ = _tae_taf_form_ativ");
            Sql.AppendLine(" And	tae_status = _tae_status");
            Sql.AppendLine(" And	tae_desc_adicional = _tae_desc_adicional");
            Sql.AppendLine(" And	tae_versao = _tae_versao");
            Sql.AppendLine(" And	tae_flag_mei = _tae_flag_mei");
            Sql.AppendLine(" And	tae_flag_mei_fora_lei = _tae_flag_mei_fora_lei");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Actv_Econ");
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


