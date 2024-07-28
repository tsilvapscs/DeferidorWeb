using System;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_Repr_Socio_Menor : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _cod_protocolo;
        private string _reque_protocolo;
        protected string _cpf_cnpj_socio;
        protected string _nome_responsavel;
        protected string _cpf_responsavel;
        protected string _nacionalidade;
        protected string _naturalidade;
        protected string _estado_civil;
        protected string _email;
        protected string _regime_bens;
        protected DateTime _data_nasc;
        protected string _tipo_doc_ident;
        protected string _no_doc_ident;
        protected string _orgao_exped;
        protected string _uf_orgao_exped;
        protected string _tipo_logradouro;
        protected string _logradouro;
        protected string _no_logradouro;
        protected string _bairro;
        protected string _municipio;
        protected string _cep;
        protected string _uf;
        protected string _profissao;
        protected string _tipo_representante;
        protected string _comp_logradouro;
        protected string _sexo;
        protected int _sq_pai;
     

     
        #endregion

        #region Class Member Declarations
        public int sq_pai
        {
            get { return _sq_pai; }
            set { _sq_pai = value; }
        }
        public string cod_protocolo
        {
            get { return _cod_protocolo; }

            set { _cod_protocolo = value; }
        }

        public string cpf_cnpj_socio
        {
            get { return _cpf_cnpj_socio; }

            set { _cpf_cnpj_socio = value; }
        }

        public string nome_responsavel
        {
            get { return _nome_responsavel; }

            set { _nome_responsavel = value; }
        }

        public string cpf_responsavel
        {
            get { return _cpf_responsavel; }

            set { _cpf_responsavel = value; }
        }

        public string nacionalidade
        {
            get { return _nacionalidade; }

            set { _nacionalidade = value; }
        }

        public string naturalidade
        {
            get { return _naturalidade; }

            set { _naturalidade = value; }
        }

        public string estado_civil
        {
            get { return _estado_civil; }

            set { _estado_civil = value; }
        }
        public string email
        {
            get { return _email; }

            set { _email = value; }
        }
        public string regime_bens
        {
            get { return _regime_bens; }

            set { _regime_bens = value; }
        }

        public DateTime data_nasc
        {
            get { return _data_nasc; }

            set { _data_nasc = value; }
        }

        public string tipo_doc_ident
        {
            get { return _tipo_doc_ident; }

            set { _tipo_doc_ident = value; }
        }

        public string no_doc_ident
        {
            get { return _no_doc_ident; }

            set { _no_doc_ident = value; }
        }

        public string orgao_exped
        {
            get { return _orgao_exped; }

            set { _orgao_exped = value; }
        }

        public string uf_orgao_exped
        {
            get { return _uf_orgao_exped; }

            set { _uf_orgao_exped = value; }
        }

        public string tipo_logradouro
        {
            get { return _tipo_logradouro; }

            set { _tipo_logradouro = value; }
        }

        public string logradouro
        {
            get { return _logradouro; }

            set { _logradouro = value; }
        }

        public string no_logradouro
        {
            get { return _no_logradouro; }

            set { _no_logradouro = value; }
        }

        public string bairro
        {
            get { return _bairro; }

            set { _bairro = value; }
        }

        public string municipio
        {
            get { return _municipio; }

            set { _municipio = value; }
        }

        public string cep
        {
            get { return _cep; }

            set { _cep = value; }
        }

        public string uf
        {
            get { return _uf; }

            set { _uf = value; }
        }

        public string profissao
        {
            get { return _profissao; }

            set { _profissao = value; }
        }

        public string tipo_representante
        {
            get { return _tipo_representante; }

            set { _tipo_representante = value; }
        }
        public string comp_logradouro
        {
            get { return _comp_logradouro; }

            set { _comp_logradouro = value; }
        }
        public string sexo
        {
            get { return _sexo; }

            set { _sexo = value; }
        }
        public string reque_protocolo
        {
            get { return _reque_protocolo; }
            set { _reque_protocolo = value; }
        }

    
        
        

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_repr_socio_menor");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	cod_protocolo, ");
            SqlI.AppendLine("	cpf_cnpj_socio, ");
            SqlI.AppendLine("	nome_responsavel, ");
            SqlI.AppendLine("	cpf_responsavel, ");
            SqlI.AppendLine("	nacionalidade, ");
            SqlI.AppendLine("	naturalidade, ");
            SqlI.AppendLine("	estado_civil, ");
            SqlI.AppendLine("	regime_bens, ");
            SqlI.AppendLine("	data_nasc, ");
            SqlI.AppendLine("	tipo_doc_ident, ");
            SqlI.AppendLine("	no_doc_ident, ");
            SqlI.AppendLine("	orgao_exped, ");
            SqlI.AppendLine("	uf_orgao_exped, ");
            SqlI.AppendLine("	tipo_logradouro, ");
            SqlI.AppendLine("	logradouro, ");
            SqlI.AppendLine("	no_logradouro, ");
            SqlI.AppendLine("	bairro, ");
            SqlI.AppendLine("	municipio, ");
            SqlI.AppendLine("	cep, ");
            SqlI.AppendLine("	uf, ");
            SqlI.AppendLine("	profissao, ");
            SqlI.AppendLine("	tipo_representante, ");
            SqlI.AppendLine("	comp_logradouro, ");
            SqlI.AppendLine("	email,");
            SqlI.AppendLine("	sexo,");
            SqlI.AppendLine("	reque_protocolo");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_cod_protocolo, ");
            SqlI.AppendLine("	@v_cpf_cnpj_socio, ");
            SqlI.AppendLine("	@v_nome_responsavel, ");
            SqlI.AppendLine("	@v_cpf_responsavel, ");
            SqlI.AppendLine("	@v_nacionalidade, ");
            SqlI.AppendLine("	@v_naturalidade, ");
            SqlI.AppendLine("	@v_estado_civil, ");
            SqlI.AppendLine("	@v_regime_bens, ");
            SqlI.AppendLine("	@v_data_nasc, ");
            SqlI.AppendLine("	@v_tipo_doc_ident, ");
            SqlI.AppendLine("	@v_no_doc_ident, ");
            SqlI.AppendLine("	@v_orgao_exped, ");
            SqlI.AppendLine("	@v_uf_orgao_exped, ");
            SqlI.AppendLine("	@v_tipo_logradouro, ");
            SqlI.AppendLine("	@v_logradouro, ");
            SqlI.AppendLine("	@v_no_logradouro, ");
            SqlI.AppendLine("	@v_bairro, ");
            SqlI.AppendLine("	@v_municipio, ");
            SqlI.AppendLine("	@v_cep, ");
            SqlI.AppendLine("	@v_uf, ");
            SqlI.AppendLine("	@v_profissao, ");
            SqlI.AppendLine("	@v_tipo_representante, ");
            SqlI.AppendLine("	@v_comp_logradouro, ");
            SqlI.AppendLine("	@v_email, ");
            SqlI.AppendLine("	@v_sexo, ");
            SqlI.AppendLine("	@v_reque_protocolo ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_repr_socio_menor Set ");  
            SqlU.AppendLine("		nome_responsavel = @v_nome_responsavel, ");
            
            SqlU.AppendLine("		nacionalidade = @v_nacionalidade, ");
            SqlU.AppendLine("		naturalidade = @v_naturalidade, ");
            SqlU.AppendLine("		estado_civil = @v_estado_civil, ");
            SqlU.AppendLine("		regime_bens = @v_regime_bens, ");
            SqlU.AppendLine("		data_nasc = @v_data_nasc, ");
            SqlU.AppendLine("		tipo_doc_ident = @v_tipo_doc_ident, ");
            SqlU.AppendLine("		no_doc_ident = @v_no_doc_ident, ");
            SqlU.AppendLine("		orgao_exped = @v_orgao_exped, ");
            SqlU.AppendLine("		uf_orgao_exped = @v_uf_orgao_exped, ");
            SqlU.AppendLine("		tipo_logradouro = @v_tipo_logradouro, ");
            SqlU.AppendLine("		logradouro = @v_logradouro, ");
            SqlU.AppendLine("		no_logradouro = @v_no_logradouro, ");
            SqlU.AppendLine("		bairro = @v_bairro, ");
            SqlU.AppendLine("		municipio = @v_municipio, ");
            SqlU.AppendLine("		cep = @v_cep, ");
            SqlU.AppendLine("		uf = @v_uf, ");
            SqlU.AppendLine("		profissao = @v_profissao, ");
            SqlU.AppendLine("		tipo_representante = @v_tipo_representante, ");
            SqlU.AppendLine("		comp_logradouro = @v_comp_logradouro, ");
            SqlU.AppendLine("		email = @v_email, ");
            SqlU.AppendLine("		sexo = @v_sexo, ");
            SqlU.AppendLine("		reque_protocolo = @v_reque_protocolo");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" and	cod_protocolo = @v_cod_protocolo ");
            SqlU.AppendLine(" and	cpf_cnpj_socio = @v_cpf_cnpj_socio ");
            SqlU.AppendLine(" and	cpf_responsavel = @v_cpf_responsavel ");
            SqlU.AppendLine(" and	reque_protocolo = @v_reque_protocolo ");
            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_cnpj_socio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_cnpj_socio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nome_responsavel));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_responsavel));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nacionalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nacionalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_naturalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _naturalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_estado_civil", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _estado_civil));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_regime_bens", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _regime_bens));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_data_nasc", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _data_nasc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_doc_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_doc_ident));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_no_doc_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _no_doc_ident));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_orgao_exped", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orgao_exped));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_orgao_exped", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf_orgao_exped));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_no_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _no_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_municipio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_profissao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _profissao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_representante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_representante));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_comp_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comp_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_email", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _email));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_sexo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sexo));
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

        public DataTable Query1()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            //Sql.AppendLine("		cod_protocolo, ");
            //Sql.AppendLine("		cpf_responsavel, ");
            //Sql.AppendLine("		nome_responsavel, ");
            //Sql.AppendLine("		nacionalidade, ");
            //Sql.AppendLine("		naturalidade, ");
            //Sql.AppendLine("		estado_civil, ");
            //Sql.AppendLine("		ds_estado_civil, ");
            //Sql.AppendLine("		regime_bens, ");
            //Sql.AppendLine("		ds_regime_bens, ");
            //Sql.AppendLine("		data_nasc, ");
            //Sql.AppendLine("		tipo_doc_ident, ");
            //Sql.AppendLine("		no_doc_ident, ");
            //Sql.AppendLine("		orgao_exped, ");
            //Sql.AppendLine("		uf_orgao_exped, ");
            //Sql.AppendLine("		tipo_logradouro, ");
            //Sql.AppendLine("		logradouro, ");
            //Sql.AppendLine("		no_logradouro, ");
            //Sql.AppendLine("		bairro, ");
            //Sql.AppendLine("		municipio, ");
            //Sql.AppendLine("		cep, ");
            //Sql.AppendLine("		uf, ");
            //Sql.AppendLine("		profissao, ");
            //Sql.AppendLine("        ds_profissao, ");
            //Sql.AppendLine("		tipo_representante, ");
            //Sql.AppendLine("		ds_tipo_representante, ");
            //Sql.AppendLine("        comp_logradouro, ");
            //Sql.AppendLine("        ds_condicao, ");
            //Sql.AppendLine("        email, ");
            //Sql.AppendLine("        sexo, ");
            //Sql.AppendLine("        sq_pai, ");
            //Sql.AppendLine("        reque_protocolo ");
            Sql.AppendLine(" From	vw_dcnt_repr_socios");
            Sql.AppendLine(" Where	1 = 1 ");

            
            if (_cod_protocolo != string.Empty)
            {
                Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            }

            if (_reque_protocolo != string.Empty && _reque_protocolo != null)
            {
                Sql.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");
            }
            if ( _sq_pai != 0)
            {
                Sql.AppendLine(" And	sq_pai = '" + _sq_pai + "'");
            }
            if (!string.IsNullOrEmpty(_cpf_cnpj_socio) && _cpf_cnpj_socio != string.Empty)
            {
                Sql.AppendLine(" And	cpf_cnpj_socio = '" + _cpf_cnpj_socio + "'");
            }
            if (!string.IsNullOrEmpty(_cpf_responsavel) &&  _cpf_responsavel != string.Empty)
            {
                Sql.AppendLine(" and	cpf_responsavel = '" + _cpf_responsavel + "' ");
            }            
            
          /*  Sql.AppendLine(" And	nome_responsavel = _nome_responsavel");
            Sql.AppendLine(" And	cpf_responsavel = _cpf_responsavel");
            Sql.AppendLine(" And	nacionalidade = _nacionalidade");
            Sql.AppendLine(" And	naturalidade = _naturalidade");
            Sql.AppendLine(" And	estado_civil = _estado_civil");
            Sql.AppendLine(" And	regime_bens = _regime_bens");
            Sql.AppendLine(" And	data_nasc = _data_nasc");
            Sql.AppendLine(" And	tipo_doc_ident = _tipo_doc_ident");
            Sql.AppendLine(" And	no_doc_ident = _no_doc_ident");
            Sql.AppendLine(" And	orgao_exped = _orgao_exped");
            Sql.AppendLine(" And	uf_orgao_exped = _uf_orgao_exped");
            Sql.AppendLine(" And	tipo_logradouro = _tipo_logradouro");
            Sql.AppendLine(" And	logradouro = _logradouro");
            Sql.AppendLine(" And	no_logradouro = _no_logradouro");
            Sql.AppendLine(" And	bairro = _bairro");
            Sql.AppendLine(" And	municipio = _municipio");
            Sql.AppendLine(" And	cep = _cep");
            Sql.AppendLine(" And	uf = _uf");
            Sql.AppendLine(" And	profissao = _profissao");
            Sql.AppendLine(" And	tipo_representante = _tipo_representante");*/


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("cnt_repr_socio_menor");
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



