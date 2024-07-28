using System;//
using System.Collections.Generic;
using System.Text;//
using MySql.Data.MySqlClient;//
using System.Data;//
using RCPJ.DAL.ConnectionBase;//
using psc.Framework;//
//using psc.Application.Contrato.ConnectionBase;




namespace RCPJ.DAL.Entities
{
    public class dCnt_prot_testemunha : DBInteractionBase
    {
        private string _cod_protocolo;     
        private string _cpf_testemunha;     
        private string _nome_testemunha;
        private string _num_identidade;
        private string _orgao_exp;
        private string _uf;
        private string _reque_protocolo;



        public string cod_protocolo
        {
            get { return _cod_protocolo; }
            set { _cod_protocolo = value; }
        }

        public string cpf_testemunha
        {
            get { return _cpf_testemunha; }
            set { _cpf_testemunha = value; }
        }
        public string nome_testemunha
        {
            get { return _nome_testemunha; }
            set { _nome_testemunha = value; }
        }
        public string num_identidade
        {
            get { return _num_identidade; }
            set { _num_identidade = value; }
        }
        public string orgao_exp
        {
            get { return _orgao_exp; }
            set { _orgao_exp = value; }
        }
        public string uf
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public string reque_protocolo
        {
            get { return _reque_protocolo; }
            set { _reque_protocolo = value; }
        }

        #region Implements
        /// <summary>
        /// Inseri ou  Atualiza o socio na base
        /// </summary>
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_prot_testemunha");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("		cod_protocolo, ");
            SqlI.AppendLine("		cpf_testemunha, ");
            SqlI.AppendLine("		nome_testemunha, ");
            SqlI.AppendLine("		num_identidade, ");
            SqlI.AppendLine("		orgao_exp, ");
            SqlI.AppendLine("		uf, ");
            SqlI.AppendLine("		reque_protocolo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("		@v_cod_protocolo, ");
            SqlI.AppendLine("		@v_cpf_testemunha, ");
            SqlI.AppendLine("		@v_nome_testemunha, ");
            SqlI.AppendLine("		@v_num_identidade, ");
            SqlI.AppendLine("		@v_orgao_exp, ");
            SqlI.AppendLine("		@v_uf, ");
            SqlI.AppendLine("		@v_reque_protocolo ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_prot_testemunha Set ");
            SqlU.AppendLine("		cpf_testemunha = @v_cpf_testemunha, ");
            SqlU.AppendLine("		nome_testemunha = @v_nome_testemunha, ");
            SqlU.AppendLine("		num_identidade = @v_num_identidade, ");
            SqlU.AppendLine("		orgao_exp = @v_orgao_exp, ");
            SqlU.AppendLine("		uf = @v_uf,");
            SqlU.AppendLine("		reque_protocolo = @v_reque_protocolo"); 
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!
            SqlU.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            SqlU.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");
            if (!string.IsNullOrEmpty(_cpf_testemunha))
            {
                SqlU.AppendLine(" And	cpf_testemunha = '" + _cpf_testemunha + "'");
            }

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_testemunha", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_testemunha));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_testemunha", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nome_testemunha));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_identidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_identidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_orgao_exp", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orgao_exp));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reque_protocolo));
                
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
            Sql.AppendLine(" * ");
            Sql.AppendLine(" From	vw_dcnt_prot_testemunha");
            Sql.AppendLine(" Where	1 = 1 ");
           
            //TODO: Implements Where Clause Here!!!! 
            //if (!string.IsNullOrEmpty(_cod_protocolo))
            //{
            //    Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            //}
            if (!string.IsNullOrEmpty(_reque_protocolo))
            {
                Sql.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");
            }

            if (!string.IsNullOrEmpty(_cpf_testemunha))
            {
                Sql.AppendLine(" And	cpf_testemunha = '" + _cpf_testemunha + "'");
            }
          


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("vw_dcnt_prot_testemunha");
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
