using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR002_Vinculo_Endereco : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected Int32 _r002_id_seq_vinc_end;
        protected Int32 _t001_sq_pessoa;
        protected Int32 _t001_sq_pessoa_pai;
        protected decimal _a015_co_tipo_logradouro;
        protected string _a015_ds_tipo_logradouro;
        protected string _r002_ds_logradouro;
        protected string _r002_nr_logradouro;
        protected string _r002_ds_complemento;
        protected string _r002_ds_bairro;
        protected Nullable<decimal> _a005_co_municipio;
        protected decimal _a004_co_pais;
        protected string _r002_nr_cep;
        protected string _r002_ds_referencia;
        protected int _flagNovoEndereco;
        private string _r002_uf = string.Empty;

       
        #endregion

        #region Class Member Declarations

        public int flagNovoEndereco
        {
            get { return _flagNovoEndereco; }
            set {_flagNovoEndereco = value;}
        }
        public Int32 r002_id_seq_vinc_end
        {
            get { return _r002_id_seq_vinc_end; }

            set { _r002_id_seq_vinc_end = value; }
        }

        public Int32 t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }

            set { _t001_sq_pessoa = value; }
        }

        public Int32 t001_sq_pessoa_pai
        {
            get { return _t001_sq_pessoa_pai; }

            set { _t001_sq_pessoa_pai = value; }
        }

        public decimal a015_co_tipo_logradouro
        {
            get { return _a015_co_tipo_logradouro; }

            set { _a015_co_tipo_logradouro = value; }
        }
        public string a015_ds_tipo_logradouro
        {
            get { return _a015_ds_tipo_logradouro; }
            set { _a015_ds_tipo_logradouro = value; }
        }

        public string r002_ds_logradouro
        {
            get { return _r002_ds_logradouro; }

            set { _r002_ds_logradouro = value; }
        }

        public string r002_nr_logradouro
        {
            get { return _r002_nr_logradouro; }

            set { _r002_nr_logradouro = value; }
        }

        public string r002_ds_complemento
        {
            get { return _r002_ds_complemento; }

            set { _r002_ds_complemento = value; }
        }

        public string r002_ds_bairro
        {
            get { return _r002_ds_bairro; }

            set { _r002_ds_bairro = value; }
        }

        public Nullable<decimal> a005_co_municipio
        {
            get { return _a005_co_municipio; }

            set { _a005_co_municipio = value; }
        }

        public decimal a004_co_pais
        {
            get { return _a004_co_pais; }

            set { _a004_co_pais = value; }
        }

        public string r002_nr_cep
        {
            get { return _r002_nr_cep; }

            set { _r002_nr_cep = value; }
        }

        public string r002_ds_referencia
        {
            get { return _r002_ds_referencia; }

            set { _r002_ds_referencia = value; }
        }
        public string R002_uf
        {
            get { return _r002_uf; }
            set { _r002_uf = value; }
        }
        #endregion


        #region Implements
        public int Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r002_vinculo_endereco");
            SqlI.AppendLine("  (");
            //SqlI.AppendLine("	r002_id_seq_vinc_end, ");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	t001_sq_pessoa_pai, ");
            SqlI.AppendLine("	a015_co_tipo_logradouro, ");
            SqlI.AppendLine("	a015_ds_tipo_logradouro, ");
            SqlI.AppendLine("	r002_ds_logradouro, ");
            SqlI.AppendLine("	r002_nr_logradouro, ");
            SqlI.AppendLine("	r002_ds_complemento, ");
            SqlI.AppendLine("	r002_ds_bairro, ");
            SqlI.AppendLine("	a005_co_municipio, ");
            SqlI.AppendLine("	a004_co_pais, ");
            SqlI.AppendLine("	r002_nr_cep, ");
            SqlI.AppendLine("	r002_ds_referencia,");
            SqlI.AppendLine("   r002_uf ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            // SqlI.AppendLine("	@v_r002_id_seq_vinc_end, ");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	@v_t001_sq_pessoa_pai, ");
            SqlI.AppendLine("	@v_a015_co_tipo_logradouro, ");
            SqlI.AppendLine("	@v_a015_ds_tipo_logradouro, ");
            SqlI.AppendLine("	@v_r002_ds_logradouro, ");
            SqlI.AppendLine("	@v_r002_nr_logradouro, ");
            SqlI.AppendLine("	@v_r002_ds_complemento, ");
            SqlI.AppendLine("	@v_r002_ds_bairro, ");
            SqlI.AppendLine("	@v_a005_co_municipio, ");
            SqlI.AppendLine("	@v_a004_co_pais, ");
            SqlI.AppendLine("	@v_r002_nr_cep, ");
            SqlI.AppendLine("	@v_r002_ds_referencia,");
            SqlI.AppendLine("   @v_r002_uf ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R002_Vinculo_Endereco Set ");
            //SqlU.AppendLine("		r002_id_seq_vinc_end = @v_r002_id_seq_vinc_end, ");
            SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");
            SqlU.AppendLine("		t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai, ");
            SqlU.AppendLine("		a015_co_tipo_logradouro = @v_a015_co_tipo_logradouro, ");
            SqlU.AppendLine("		a015_ds_tipo_logradouro = @v_a015_ds_tipo_logradouro, ");
            SqlU.AppendLine("		r002_ds_logradouro = @v_r002_ds_logradouro, ");
            SqlU.AppendLine("		r002_nr_logradouro = @v_r002_nr_logradouro, ");
            SqlU.AppendLine("		r002_ds_complemento = @v_r002_ds_complemento, ");
            SqlU.AppendLine("		r002_ds_bairro = @v_r002_ds_bairro, ");
            SqlU.AppendLine("		a005_co_municipio = @v_a005_co_municipio, ");
            SqlU.AppendLine("		a004_co_pais = @v_a004_co_pais, ");
            SqlU.AppendLine("		r002_nr_cep = @v_r002_nr_cep, ");
            SqlU.AppendLine("		r002_ds_referencia = @v_r002_ds_referencia,");
            SqlU.AppendLine("       r002_uf = @v_r002_uf");
            SqlU.AppendLine(" Where	");
            if (_t001_sq_pessoa == 0)
                SqlU.AppendLine(" t001_sq_pessoa = -1");
            else
                SqlU.AppendLine(" t001_sq_pessoa = " + _t001_sq_pessoa);
            //if (_r002_id_seq_vinc_end == 0)
            //    SqlU.AppendLine(" r002_id_seq_vinc_end = -1 ");
            //else
            //    SqlU.AppendLine(" r002_id_seq_vinc_end = @v_r002_id_seq_vinc_end ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_id_seq_vinc_end", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_id_seq_vinc_end));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a015_co_tipo_logradouro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a015_co_tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_ds_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_ds_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a015_ds_tipo_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a015_ds_tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_nr_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_nr_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_ds_complemento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_ds_complemento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_ds_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_ds_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a005_co_municipio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a005_co_municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_pais", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_nr_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_nr_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_ds_referencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_ds_referencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r002_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r002_uf));

                
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
                int Ret = 0;
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();
                    cmdToExecute.CommandText = "SELECT LAST_INSERT_ID()";
                    Ret = int.Parse(cmdToExecute.ExecuteScalar().ToString());

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
            Sql.AppendLine("		r002_id_seq_vinc_end, ");
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		t001_sq_pessoa_pai, ");
            Sql.AppendLine("		a015_co_tipo_logradouro, ");
            Sql.AppendLine("		a015_ds_tipo_logradouro, ");
            Sql.AppendLine("		r002_ds_logradouro, ");
            Sql.AppendLine("		r002_nr_logradouro, ");
            Sql.AppendLine("		r002_ds_complemento, ");
            Sql.AppendLine("		r002_ds_bairro, ");
            Sql.AppendLine("		a005_co_municipio, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("		r002_nr_cep, ");
            Sql.AppendLine("		r002_ds_referencia,");
            Sql.AppendLine("        r002_uf");
            Sql.AppendLine(" From	R002_Vinculo_Endereco");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	r002_id_seq_vinc_end = _r002_id_seq_vinc_end");
            Sql.AppendLine(" And	t001_sq_pessoa = _t001_sq_pessoa");
            Sql.AppendLine(" And	t001_sq_pessoa_pai = _t001_sq_pessoa_pai");
            Sql.AppendLine(" And	a015_co_tipo_logradouro = _a015_co_tipo_logradouro");
            Sql.AppendLine(" And	a015_ds_tipo_logradouro = _a015_ds_tipo_logradouro");
            Sql.AppendLine(" And	r002_ds_logradouro = _r002_ds_logradouro");
            Sql.AppendLine(" And	r002_nr_logradouro = _r002_nr_logradouro");
            Sql.AppendLine(" And	r002_ds_complemento = _r002_ds_complemento");
            Sql.AppendLine(" And	r002_ds_bairro = _r002_ds_bairro");
            Sql.AppendLine(" And	a005_co_municipio = _a005_co_municipio");
            Sql.AppendLine(" And	a004_co_pais = _a004_co_pais");
            Sql.AppendLine(" And	r002_nr_cep = _r002_nr_cep");
            Sql.AppendLine(" And	r002_ds_referencia = _r002_ds_referencia");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("R002_Vinculo_Endereco");
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
                if (_mainConnectionIsCreatedLocal)
                {

                    // Close connection. 
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void Deleta(int wsqPessoa)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
            
            Sql.AppendLine(" From	R002_Vinculo_Endereco");
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


