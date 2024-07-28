using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT001_Pessoa : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t001_sq_pessoa = 0;
        protected string _t001_in_tipo_pessoa;
        protected string _t001_ds_pessoa;
        protected string _t001_in_dados_atualizados;
        protected Nullable<DateTime> _t001_dt_ult_atualizacao;
        protected string _t001_ddd = string.Empty;
        protected string _t001_email = string.Empty ;
        protected string _t001_tel_1 = string.Empty;
        protected string _t001_tel_2 = string.Empty;
        protected string _t001_nome_fantasia = string.Empty;
        #endregion

        #region Class Member Declarations
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }
            set { _t001_sq_pessoa = value; }
        }

        public string t001_in_tipo_pessoa
        {
            get { return _t001_in_tipo_pessoa; }
            set { _t001_in_tipo_pessoa = value; }
        }

        public string t001_ds_pessoa
        {
            get { return _t001_ds_pessoa; }
            set { _t001_ds_pessoa = value; }
        }

        public string t001_in_dados_atualizados
        {
            get { return _t001_in_dados_atualizados; }
            set { _t001_in_dados_atualizados = value; }
        }

        public Nullable<DateTime> t001_dt_ult_atualizacao
        {
            get { return _t001_dt_ult_atualizacao; }
            set { _t001_dt_ult_atualizacao = value; }
        }

        public string t001_email
        {
            get { return _t001_email; }
            set { _t001_email = value; }
        }
        public string t001_ddd
        {
            get{return _t001_ddd;}
            set{_t001_ddd=value;}
        }

        public string t001_tel_1
        {
            get { return _t001_tel_1; }
            set { _t001_tel_1 = value; }
        }

        public string t001_tel_2
        {
            get { return _t001_tel_2; }
            set { _t001_tel_2 = value; }
        }
        public string t001_nome_fantasia
        {
            get { return _t001_nome_fantasia; }
            set { _t001_nome_fantasia = value; }
        }
        #endregion


        #region Implements
        public int Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t001_pessoa");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	t001_in_tipo_pessoa, ");
            SqlI.AppendLine("	t001_ds_pessoa, ");
            SqlI.AppendLine("	t001_nome_fantasia, ");
            SqlI.AppendLine("	t001_in_dados_atualizados, ");
            SqlI.AppendLine("	t001_dt_ult_atualizacao, ");
            SqlI.AppendLine("	t001_email, ");
            SqlI.AppendLine("   t001_DDD, ");
            SqlI.AppendLine("	t001_tel_1, ");
            SqlI.AppendLine("	t001_tel_2 ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	@v_t001_in_tipo_pessoa, ");
            SqlI.AppendLine("	@v_t001_ds_pessoa, ");
            SqlI.AppendLine("	@v_t001_nome_fantasia, ");
            SqlI.AppendLine("	@v_t001_in_dados_atualizados, ");
            SqlI.AppendLine("	evaldate(@v_t001_dt_ult_atualizacao), ");
            SqlI.AppendLine("	@v_t001_email, ");
            SqlI.AppendLine("   @v_t001_ddd, ");
            SqlI.AppendLine("	@v_t001_tel_1, ");
            SqlI.AppendLine("	@v_t001_tel_2");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T001_Pessoa Set ");
            //SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");
            SqlU.AppendLine("		t001_in_tipo_pessoa = @v_t001_in_tipo_pessoa, ");
            SqlU.AppendLine("		t001_ds_pessoa = @v_t001_ds_pessoa, ");
            SqlU.AppendLine("		t001_nome_fantasia = @v_t001_nome_fantasia, ");
            SqlU.AppendLine("		t001_in_dados_atualizados = @v_t001_in_dados_atualizados, ");
            SqlU.AppendLine("		t001_dt_ult_atualizacao = evaldate(@v_t001_dt_ult_atualizacao), ");
            SqlU.AppendLine("		t001_email = @v_t001_email, ");
            SqlU.AppendLine("       t001_ddd = @v_t001_ddd, ");
            SqlU.AppendLine("		t001_tel_1 = @v_t001_tel_1, ");
            SqlU.AppendLine("		t001_tel_2 = @v_t001_tel_2 ");
            SqlU.AppendLine(" Where	 ");
            if (_t001_sq_pessoa == 0)
                SqlU.AppendLine(" t001_sq_pessoa = -1");
            else
                SqlU.AppendLine(" t001_sq_pessoa = " + _t001_sq_pessoa);


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_in_tipo_pessoa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_in_tipo_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_ds_pessoa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_ds_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_nome_fantasia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_nome_fantasia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_in_dados_atualizados", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_in_dados_atualizados));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_dt_ult_atualizacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_dt_ult_atualizacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_email", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_email));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_ddd", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_ddd));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_tel_1", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_tel_1));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_tel_2", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_tel_2));


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
                int Ret = Convert.ToInt32(_t001_sq_pessoa);
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();
                    if (_t001_sq_pessoa == 0)
                    {
                        cmdToExecute.CommandText = "SELECT LAST_INSERT_ID()";
                        Ret = int.Parse(cmdToExecute.ExecuteScalar().ToString());
                    }
                    
                }
                return Ret;
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
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		t001_in_tipo_pessoa, ");
            Sql.AppendLine("		t001_ds_pessoa, ");
            Sql.AppendLine("		t001_nome_fantasia, ");
            Sql.AppendLine("		t001_in_dados_atualizados, ");
            Sql.AppendLine("		t001_dt_ult_atualizacao, ");
            Sql.AppendLine("		t001_email, ");
            Sql.AppendLine("        t001_ddd, ");
            Sql.AppendLine("		t001_tel_1, ");
            Sql.AppendLine("		t001_tel_2");
            Sql.AppendLine(" From	T001_Pessoa");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	t001_sq_pessoa = _t001_sq_pessoa");
            Sql.AppendLine(" And	t001_in_tipo_pessoa = _t001_in_tipo_pessoa");
            Sql.AppendLine(" And	t001_ds_pessoa = _t001_ds_pessoa");
            Sql.AppendLine(" And	t001_in_dados_atualizados = _t001_in_dados_atualizados");
            Sql.AppendLine(" And	t001_dt_ult_atualizacao = _t001_dt_ult_atualizacao");
            Sql.AppendLine(" And	t001_email = _t001_email");
            Sql.AppendLine(" And    t001_ddd = _t001_ddd");
            Sql.AppendLine(" And	t001_tel_1 = _t001_tel_1");
            Sql.AppendLine(" And	t001_tel_2 = _t001_tel_2");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T001_Pessoa");
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

        public void Deleta(int wsqPessoa)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
            Sql.AppendLine(" From	T001_Pessoa");
            Sql.AppendLine(" Where	t001_sq_pessoa = " + wsqPessoa);

            //TODO: Implements Where Clause Here!!!! 
           


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            //DataTable toReturn = new DataTable("T001_Pessoa");
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
                //adapter.Fill(toReturn);
                //return toReturn;
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
                adapter.Dispose();
            }
           
        }
        /// <summary>
        /// Exclui todos os socios da empresa
        /// </summary>
        /// <param name="wsqPessoa"></param>
        public void DeletaTodos(int wsqPessoaPai)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
            Sql.AppendLine(" From	T001_Pessoa");
            Sql.AppendLine(" Where	t001_sq_pessoa = " + wsqPessoaPai);

            //TODO: Implements Where Clause Here!!!! 



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            //DataTable toReturn = new DataTable("T001_Pessoa");
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
                //adapter.Fill(toReturn);
                //return toReturn;
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
                adapter.Dispose();
            }

        }

        public void DeletaVinculo(int wsqPessoaPai)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");

            Sql.AppendLine(" From	R001_Vinculo");
            Sql.AppendLine(" WHERE T001_SQ_PESSOA_pai = " + wsqPessoaPai + " and A009_CO_CONDICAO <> 500 and A009_CO_CONDICAO <> 501");
          

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

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

                cmdToExecute.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

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
                adapter.Dispose();
            }
        }
        #endregion
    }
}


