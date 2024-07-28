using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dt093_Contador:DBInteractionBase
    {
        #region Menbros
        private string _CNPJ_Orgao_Registro = string.Empty;
        private string _nr_Protocolo = string.Empty;
        private string _ds_Pessoa = string.Empty;
        private string _cpfCnpj = string.Empty;
        private int _tip_Class_Empressa = int.MinValue;
        private string _uf_CRC_Empresa = string.Empty;
        private string _co_CRC_empresa = string.Empty;
        private string _tip_CRC_Empresa = string.Empty;
        private string _cpf_Resp = string.Empty;
        private int _tip_Class_Resp = int.MinValue;
        private string _uf_CRC_Resp = string.Empty;
        private string _co_CRC_Resp = string.Empty;
        private string _tip_CRC_Resp = string.Empty;
        private Nullable<DateTime> _dataInscricao;
        #endregion
        #region Propriedades
        public string CNPJ_Orgao_Registro
        {
            get { return _CNPJ_Orgao_Registro; }
            set { _CNPJ_Orgao_Registro = value; }
        }
        public string nr_Protocolo
        {
            get { return _nr_Protocolo; }
            set { _nr_Protocolo = value; }
        }
        public string ds_Pessoa
        {
            get { return _ds_Pessoa; }
            set { _ds_Pessoa = value; }
        }
        public string cpfCnpj
        {
            get { return _cpfCnpj; }
            set { _cpfCnpj = value; }
        }
        public int tip_Class_Empresa
        {
            get { return _tip_Class_Empressa; }
            set { _tip_Class_Empressa = value; }
        }
        public string uf_CRC_Empresa
        {
            get { return _uf_CRC_Empresa; }
            set { _uf_CRC_Empresa = value; }
        }
        public string co_CRC_Empresa
        {
            get { return _co_CRC_empresa; }
            set { _co_CRC_empresa = value; }
        }
        public string tip_CRC_Empresa
        {
            get { return _tip_CRC_Empresa; }
            set { _tip_CRC_Empresa = value; }
        }
        public string cpf_resp
        {
            get { return _cpf_Resp; }
            set { _cpf_Resp = value; }
        }
        public int tip_Class_Resp
        {
            get { return _tip_Class_Resp; }
            set { _tip_Class_Resp = value; }
        }
        public string uf_CRC_Resp
        {
            get { return _uf_CRC_Resp; }
            set { _uf_CRC_Resp = value; }
        }
        public string co_CRC_Resp
        {
            get { return _co_CRC_Resp; }
            set { _co_CRC_Resp = value; }
        }
        public string tip_CRC_Resp
        {
            get { return _tip_CRC_Resp; }
            set { _tip_CRC_Resp = value; }
        }
        public Nullable<DateTime> DataInscricao
        {
            get { return _dataInscricao; }
            set { _dataInscricao = value; }
        }
        #endregion
        #region Implementação
        public DataTable Query()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select * from t093_contador ");
            sql.AppendLine("Where T005_NR_PROTOCOLO ='" + nr_Protocolo + "' ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T093_CONTADOR");
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
            #region inclusão
                SqlI.AppendLine(" Insert into t093_contador");
                SqlI.AppendLine("  (");
                SqlI.AppendLine("	T004_NR_CNPJ_ORG_REG, ");
                SqlI.AppendLine("	T005_NR_PROTOCOLO, ");
                SqlI.AppendLine("	T093_DS_PESSOA, ");
                SqlI.AppendLine("	T093_CPFCNPJ, ");
                SqlI.AppendLine("	T093_TIP_CLASS_EMPRESA, ");
                SqlI.AppendLine("	T093_UF_CRC_EMPRESA, ");
                SqlI.AppendLine("	T093_CO_CRC_EMPRESA, ");
                SqlI.AppendLine("   T093_TIP_CRC_EMPRESA, ");
                SqlI.AppendLine("	T093_CPF_RESP, ");
                SqlI.AppendLine("   T093_TIP_CLASS_RESP, ");
                SqlI.AppendLine("	T093_UF_CRC_RESP, ");
                SqlI.AppendLine("   T093_CO_CRC_RESP, ");
                SqlI.AppendLine("	T093_TIP_CRC_RESP, ");
                SqlI.AppendLine("	T093_DT_INSCR_CRC ");
                SqlI.AppendLine("  )");
                SqlI.AppendLine(" Values ");
                SqlI.AppendLine("  (");
                SqlI.AppendLine("	@v_CNPJ_Orgao_Registro, ");
                SqlI.AppendLine("	@v_nr_Protocolo, ");
                SqlI.AppendLine("	@v_ds_Pessoa, ");
                SqlI.AppendLine("	@v_cpfCnpj, ");
                SqlI.AppendLine("	@v_tip_Class_Empressa, ");
                SqlI.AppendLine("	@v_uf_CRC_Empresa, ");
                SqlI.AppendLine("   @v_co_CRC_empresa, ");
                SqlI.AppendLine("	@v_tip_CRC_Empresa, ");
                SqlI.AppendLine("	@v_cpf_Resp, ");
                SqlI.AppendLine("   @v_tip_Class_Resp, ");
                SqlI.AppendLine("	@v_uf_CRC_Resp, ");
                SqlI.AppendLine("	@v_co_CRC_Resp, ");
                SqlI.AppendLine("   @v_tip_CRC_Resp, ");
                SqlI.AppendLine("   evaldate(@v_dataInscricao) ");
                SqlI.AppendLine("  )");
            #endregion
                #region Update
                SqlU.AppendLine(" Update  t093_contador set ");
                SqlU.AppendLine("	T004_NR_CNPJ_ORG_REG = @v_CNPJ_Orgao_Registro, ");
                SqlU.AppendLine("	T005_NR_PROTOCOLO = @v_nr_Protocolo,  ");
                SqlU.AppendLine("	T093_DS_PESSOA = @v_ds_Pessoa, ");
                SqlU.AppendLine("	T093_CPFCNPJ = @v_cpfCnpj, ");
                SqlU.AppendLine("	T093_TIP_CLASS_EMPRESA = @v_tip_Class_Empressa, ");
                SqlU.AppendLine("	T093_UF_CRC_EMPRESA = @v_uf_CRC_Empresa, ");
                SqlU.AppendLine("	T093_CO_CRC_EMPRESA = @v_co_CRC_empresa, ");
                SqlU.AppendLine("   T093_TIP_CRC_EMPRESA = @v_tip_CRC_Empresa, ");
                SqlU.AppendLine("	T093_CPF_RESP = @v_cpf_Resp, ");
                SqlU.AppendLine("   T093_TIP_CLASS_RESP = @v_tip_Class_Resp, ");
                SqlU.AppendLine("	T093_UF_CRC_RESP = @v_uf_CRC_Resp, ");
                SqlU.AppendLine("   T093_CO_CRC_RESP = @v_co_CRC_Resp, ");
                SqlU.AppendLine("	T093_TIP_CRC_RESP = @v_tip_CRC_Resp, ");
                SqlU.AppendLine("	T093_DT_INSCR_CRC = evaldate(@v_dataInscricao) ");
                SqlU.AppendLine(" Where	 ");
                SqlU.AppendLine(" T005_NR_PROTOCOLO = '" +  _nr_Protocolo + "' ");
                #endregion
                MySqlCommand cmdToExecute = new MySqlCommand();
                cmdToExecute.CommandText = SqlU.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _mainConnection;
             try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_CNPJ_Orgao_Registro", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CNPJ_Orgao_Registro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nr_Protocolo", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nr_Protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_ds_Pessoa", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ds_Pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpfCnpj", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpfCnpj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tip_Class_Empressa", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tip_Class_Empressa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_CRC_Empresa", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf_CRC_Empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_co_CRC_empresa", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _co_CRC_empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tip_CRC_Empresa", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tip_CRC_Empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_Resp", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_Resp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tip_Class_Resp", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tip_Class_Resp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_CRC_Resp", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf_CRC_Resp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_co_CRC_Resp", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _co_CRC_Resp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tip_CRC_Resp", MySqlDbType.String, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tip_CRC_Resp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_dataInscricao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataInscricao));
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Open();
                }
                else
                {

                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        #endregion
    }
}
