using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT011_Protocolo_Status : DBInteractionBase
    {
        #region Propriedades

        protected string _T004_NR_CNPJ_ORG_REG;
        protected string _T005_NR_PROTOCOLO;
        protected string _T011_IN_SITUACAO;
        protected DateTime _T011_DT_SITUACAO;
        protected string _T011_USUARIO;
        protected Boolean _atualizaStatus = true;

        #endregion

        #region Membros

        public string T004_NR_CNPJ_ORG_REG
        {
            get { return _T004_NR_CNPJ_ORG_REG; }
            set { _T004_NR_CNPJ_ORG_REG = value; }
        }

        public string T005_NR_PROTOCOLO
        {
            get { return _T005_NR_PROTOCOLO; }
            set { _T005_NR_PROTOCOLO = value; }
        }
        public string T011_IN_SITUACAO
        {
            get { return _T011_IN_SITUACAO; }
            set { _T011_IN_SITUACAO = value; }
        }
        public DateTime T011_DT_SITUACAO
        {
            get { return _T011_DT_SITUACAO; }
            set { _T011_DT_SITUACAO = value; }
        }
        public string T011_USUARIO
        {
            get { return _T011_USUARIO; }
            set { _T011_USUARIO = value; }
        }
        public Boolean AtualizaStatus
        {
            get { return _atualizaStatus; }
            set { _atualizaStatus = value; }
        }
        #endregion

        #region Implementacao

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t011_protocolo_status");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T004_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	T011_IN_SITUACAO, ");
            SqlI.AppendLine("	T011_DT_SITUACAO, ");
            SqlI.AppendLine("	T011_USUARIO ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_T004_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	@v_T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	@v_T011_IN_SITUACAO, ");
            SqlI.AppendLine("	@v_T011_DT_SITUACAO, ");
            SqlI.AppendLine("	@v_T011_USUARIO ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     t011_protocolo_status Set ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //SqlU.AppendLine("		t005_nr_protocolo = @v_t005_nr_protocolo, ");
            SqlU.AppendLine("		T011_IN_SITUACAO = @v_T011_IN_SITUACAO, ");
            SqlU.AppendLine("		T011_DT_SITUACAO = @v_T011_DT_SITUACAO, ");
            SqlU.AppendLine("		T011_USUARIO = @v_T011_USUARIO ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg");
            SqlU.AppendLine(" And t005_nr_protocolo = @v_t005_nr_protocolo");
            if (_atualizaStatus)
                SqlU.AppendLine(" And T011_IN_SITUACAO = @v_T011_IN_SITUACAO");
            //SqlU.AppendLine(" And T011_DT_SITUACAO = @v_T011_DT_SITUACAO");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T004_NR_CNPJ_ORG_REG));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T011_IN_SITUACAO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T011_IN_SITUACAO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T011_DT_SITUACAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T011_DT_SITUACAO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T011_USUARIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T011_USUARIO));

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

                public string AchaProtocoloSeHouver(String wProtocoloViabilidade)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty ;
            Sql.AppendLine("SELECT t005_nr_protocolo from t005_protocolo WHERE T005_NR_PROTOCOLO_VIABILIDADE = '" + wProtocoloViabilidade + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
               // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                wProtocoloRequerimento = toReturn.Rows[0]["t005_nr_protocolo"].ToString();
                return wProtocoloRequerimento;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		T004_NR_CNPJ_ORG_REG, ");
            Sql.AppendLine("		T005_NR_PROTOCOLO, ");
            Sql.AppendLine("		T011_IN_SITUACAO;, ");
            Sql.AppendLine("		T011_DT_SITUACAO, ");
            Sql.AppendLine("		T011_USUARIO ");
            Sql.AppendLine(" From	T011_Protocolo_Status");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	T004_NR_CNPJ_ORG_REG = _T004_NR_CNPJ_ORG_REG");
            Sql.AppendLine(" And	T005_NR_PROTOCOLO = _T005_NR_PROTOCOLO");
            Sql.AppendLine(" And	T011_IN_SITUACAO = _T011_IN_SITUACAO");
            Sql.AppendLine(" Order By T011_DT_SITUACAO DESC");
            //Sql.AppendLine(" And	T011_DT_SITUACAO = _T011_DT_SITUACAO");
            //Sql.AppendLine(" And	T011_USUARIO = _T011_USUARIO");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T011_PROTOCOLO_STATUS");
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
