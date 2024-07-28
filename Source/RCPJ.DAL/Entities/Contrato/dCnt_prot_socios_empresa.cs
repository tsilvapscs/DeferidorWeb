using System;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_Prot_Socios_Empresa : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        private string _cpf_cnpj_socio_empresa;
        private string _cpf_cnpj_socio;        
        private string _cod_protocolo;
        private string _reque_protocolo;
        private string _nome_socio;
        protected int _sexo;
        private string _nome_pai;
        private string _nome_mae;
        private decimal _tipo_socio;
        private string _nacionalidade;
        private string _naturalidade;
        private string _estado_civil;
        private string _regime_bens;
        private DateTime _data_nasc;
        protected string _tipo_emancipacao;
        private string _tipo_doc_ident;
        private string _no_doc_ident;
        private string _orgao_exped;
        private string _tipo_logradouro;
        private string _logradouro;
        private string _no_logradouro;
        private string _comp_logradouro;
        private string _bairro;
        private string _municipio;
        private string _cep;
        private string _uf;
        private string _profissao;
        private string _uf_orgao_exped;
        private decimal _qtd_cotas;
        private decimal _adm;
        private string _email;
        private string _telefone;
        private string _qualificacao;        

        #endregion

        #region Class Member Declarations


        public string cpf_cnpj_socio_empresa
        {
            get { return _cpf_cnpj_socio_empresa; }

            set { _cpf_cnpj_socio_empresa = value; }
        }

        public string cpf_cnpj_socio
        {
            get { return _cpf_cnpj_socio; }

            set { _cpf_cnpj_socio = value; }
        }

        public string cod_protocolo
        {
            get { return _cod_protocolo; }

            set { _cod_protocolo = value; }
        }

        public string nome_socio
        {
            get { return _nome_socio; }

            set { _nome_socio = value; }
        }

        public string sexo
        {
            get { return Convert.ToString(_sexo); }

            set { _sexo = Convert.ToInt32(value); }
        }
        public string nome_mae
        {
            get { return _nome_mae; }

            set { _nome_mae = value; }
        }

        public decimal tipo_socio
        {
            get { return _tipo_socio; }

            set { _tipo_socio = value; }
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

        public string tipo_emancipacao
        {
            get { return _tipo_emancipacao; }

            set { _tipo_emancipacao = value; }
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

        public string comp_logradouro
        {
            get { return _comp_logradouro; }

            set { _comp_logradouro = value; }
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

        public string uf_orgao_exped
        {
            get { return _uf_orgao_exped; }

            set { _uf_orgao_exped = value; }
        }

        public decimal qtd_cotas
        {
            get { return _qtd_cotas; }

            set { _qtd_cotas = value; }
        }

        public decimal adm
        {
            get { return _adm; }

            set { _adm = value; }
        }


        public string email
        {
            get { return _email; }

            set { _email = value; }
        }
        public string nome_pai
        {
            get { return _nome_pai; }

            set { _nome_pai = value; }
        }
        public string telefone
        {
            get { return _telefone; }

            set { _telefone = value; }
        }

        public string qualificacao
        {
            get { return _qualificacao; }

            set { _qualificacao = value; }
        }

        public string reque_protocolo
        {
            get { return _reque_protocolo; }

            set { _reque_protocolo = value; }
        }
        
        
        #endregion


        #region Implements
        /// <summary>
        /// Inseri ou  Atualiza o socio na base
        /// </summary>
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_prot_socios_empresa");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	cpf_cnpj_socio_empresa, ");
            SqlI.AppendLine("	cpf_cnpj_socio, ");
            SqlI.AppendLine("	cod_protocolo, ");
            SqlI.AppendLine("	nome_socio, ");
            SqlI.AppendLine("	nome_mae, ");
            SqlI.AppendLine("	tipo_socio, ");
            SqlI.AppendLine("	nacionalidade, ");
            SqlI.AppendLine("	naturalidade, ");
            SqlI.AppendLine("	estado_civil, ");
            SqlI.AppendLine("	regime_bens, ");
            SqlI.AppendLine("	data_nasc, ");
            SqlI.AppendLine("	tipo_doc_ident, ");
            SqlI.AppendLine("	no_doc_ident, ");
            SqlI.AppendLine("	orgao_exped, ");
            SqlI.AppendLine("	tipo_logradouro, ");
            SqlI.AppendLine("	logradouro, ");
            SqlI.AppendLine("	no_logradouro, ");
            SqlI.AppendLine("	compl_logradouro, ");
            SqlI.AppendLine("	bairro, ");
            SqlI.AppendLine("	municipio, ");
            SqlI.AppendLine("	cep, ");
            SqlI.AppendLine("	uf, ");
            SqlI.AppendLine("	profissao, ");
            SqlI.AppendLine("	uf_orgao_exped, ");
            SqlI.AppendLine("	qtd_cotas, ");
            SqlI.AppendLine("	adm, ");
            SqlI.AppendLine("	e_mail, ");
            SqlI.AppendLine("	nome_pai, ");
            SqlI.AppendLine("	sexo, ");
            SqlI.AppendLine("	tipo_emancipacao, ");
            SqlI.AppendLine("	telefone, ");
            SqlI.AppendLine("	qualificacao, ");
            SqlI.AppendLine("	reque_protocolo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_cpf_cnpj_socio_empresa, ");
            SqlI.AppendLine("	@v_cpf_cnpj_socio, ");
            SqlI.AppendLine("	@v_cod_protocolo, ");
            SqlI.AppendLine("	@v_nome_socio, ");
            SqlI.AppendLine("	@v_nome_mae, ");
            SqlI.AppendLine("	@v_tipo_socio, ");
            SqlI.AppendLine("	@v_nacionalidade, ");
            SqlI.AppendLine("	@v_naturalidade, ");
            SqlI.AppendLine("	@v_estado_civil, ");
            SqlI.AppendLine("	@v_regime_bens, ");
            SqlI.AppendLine("	@v_data_nasc, ");
            SqlI.AppendLine("	@v_tipo_doc_ident, ");
            SqlI.AppendLine("	@v_no_doc_ident, ");
            SqlI.AppendLine("	@v_orgao_exped, ");
            SqlI.AppendLine("	@v_tipo_logradouro, ");
            SqlI.AppendLine("	@v_logradouro, ");
            SqlI.AppendLine("	@v_no_logradouro, ");
            SqlI.AppendLine("	@v_compl_logradouro, ");
            SqlI.AppendLine("	@v_bairro, ");
            SqlI.AppendLine("	@v_municipio, ");
            SqlI.AppendLine("	@v_cep, ");
            SqlI.AppendLine("	@v_uf, ");
            SqlI.AppendLine("	@v_profissao, ");
            SqlI.AppendLine("	@v_uf_orgao_exped, ");
            SqlI.AppendLine("	@v_qtd_cotas, ");
            SqlI.AppendLine("	@v_adm,");
            SqlI.AppendLine("	@v_email, ");
            SqlI.AppendLine("	@v_nome_pai, ");
            SqlI.AppendLine("	@v_sexo, ");
            SqlI.AppendLine("	@v_tipo_emancipacao, ");
            SqlI.AppendLine("	@v_telefone, ");
            SqlI.AppendLine("	@v_qualificacao,  ");
            SqlI.AppendLine("	@v_reque_protocolo ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_prot_socios_empresa Set ");
            SqlU.AppendLine("	    cpf_cnpj_socio_empresa = @v_cpf_cnpj_socio_empresa, ");
            SqlU.AppendLine("		cpf_cnpj_socio = @v_cpf_cnpj_socio, ");
            SqlU.AppendLine("		cod_protocolo = @v_cod_protocolo, ");
            SqlU.AppendLine("		nome_socio = @v_nome_socio, ");
            SqlU.AppendLine("		nome_mae = @v_nome_mae, ");
            SqlU.AppendLine("		tipo_socio = @v_tipo_socio, ");
            SqlU.AppendLine("		nacionalidade = @v_nacionalidade, ");
            SqlU.AppendLine("		naturalidade = @v_naturalidade, ");
            SqlU.AppendLine("		estado_civil = @v_estado_civil, ");
            SqlU.AppendLine("		regime_bens = @v_regime_bens, ");
            SqlU.AppendLine("		data_nasc = @v_data_nasc, ");
            SqlU.AppendLine("		tipo_doc_ident = @v_tipo_doc_ident, ");
            SqlU.AppendLine("		no_doc_ident = @v_no_doc_ident, ");
            SqlU.AppendLine("		orgao_exped = @v_orgao_exped, ");
            SqlU.AppendLine("		tipo_logradouro = @v_tipo_logradouro, ");
            SqlU.AppendLine("		logradouro = @v_logradouro, ");
            SqlU.AppendLine("		no_logradouro = @v_no_logradouro, ");
            SqlU.AppendLine("	    compl_logradouro = @v_compl_logradouro, ");
            SqlU.AppendLine("		bairro = @v_bairro, ");
            SqlU.AppendLine("		municipio = @v_municipio, ");
            SqlU.AppendLine("		cep = @v_cep, ");
            SqlU.AppendLine("		uf = @v_uf, ");
            SqlU.AppendLine("		profissao = @v_profissao, ");
            SqlU.AppendLine("		uf_orgao_exped = @v_uf_orgao_exped, ");
            SqlU.AppendLine("		qtd_cotas = @v_qtd_cotas, ");
            SqlU.AppendLine("		adm = @v_adm,");
            SqlU.AppendLine("	    e_mail = @v_email, ");
            SqlU.AppendLine("	    nome_pai = @v_nome_pai, ");
            SqlU.AppendLine("	    sexo = @v_sexo, ");
            SqlU.AppendLine("	    tipo_emancipacao = @v_tipo_emancipacao, ");
            SqlU.AppendLine("	    telefone = @v_telefone, ");
            SqlU.AppendLine("   	qualificacao = @v_qualificacao, ");
            SqlU.AppendLine("   	reque_protocolo = @v_reque_protocolo ");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!
            SqlU.AppendLine(" And	cpf_cnpj_socio = '" + _cpf_cnpj_socio + "'");
            SqlU.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            SqlU.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_cnpj_socio_empresa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_cnpj_socio_empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cpf_cnpj_socio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpf_cnpj_socio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_socio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nome_socio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_mae", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nome_mae));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_socio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_socio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nacionalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nacionalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_naturalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _naturalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_estado_civil", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _estado_civil));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_regime_bens", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _regime_bens));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_data_nasc", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _data_nasc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_doc_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_doc_ident));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_no_doc_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _no_doc_ident));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_orgao_exped", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _orgao_exped));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_no_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _no_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_compl_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comp_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_municipio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_profissao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _profissao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_orgao_exped", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf_orgao_exped));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_qtd_cotas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _qtd_cotas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_adm", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _adm));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_email", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _email));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_pai", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nome_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_sexo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sexo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_emancipacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_emancipacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_telefone", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _telefone));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_qualificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _qualificacao));
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
            Sql.AppendLine("		cpf_cnpj_socio_empresa, ");
            Sql.AppendLine("		cpf_cnpj_socio, ");
            Sql.AppendLine("		cod_protocolo, ");
            Sql.AppendLine("		nome_socio, ");
            Sql.AppendLine("		nome_mae, ");
            Sql.AppendLine("		tipo_socio, ");
            Sql.AppendLine("		nacionalidade, ");
            Sql.AppendLine("		naturalidade, ");
            Sql.AppendLine("		estado_civil, ");
            Sql.AppendLine("        ds_estado_civil, ");
            Sql.AppendLine("		ds_regime_bens, ");
            Sql.AppendLine("		regime_bens, ");
            Sql.AppendLine("		data_nasc, ");
            Sql.AppendLine("        tipo_emancipacao, ");
            Sql.AppendLine("		tipo_doc_ident, ");
            Sql.AppendLine("		no_doc_ident, ");
            Sql.AppendLine("		orgao_exped, ");
            Sql.AppendLine("		tipo_logradouro, ");
            Sql.AppendLine("		logradouro, ");
            Sql.AppendLine("		no_logradouro, ");
            Sql.AppendLine("		compl_logradouro, ");
            Sql.AppendLine("		bairro, ");
            Sql.AppendLine("		municipio, ");
            Sql.AppendLine("        ds_municipio, ");
            Sql.AppendLine("		cep, ");
            Sql.AppendLine("		uf, ");
            Sql.AppendLine("		profissao, ");
            Sql.AppendLine("        ds_profissao, ");
            Sql.AppendLine("		uf_orgao_exped, ");
            Sql.AppendLine("		qtd_cotas, ");
            Sql.AppendLine("		adm,");
            Sql.AppendLine("		e_mail,");
            Sql.AppendLine("		nome_pai,");
            Sql.AppendLine("		Sexo,");
            Sql.AppendLine("		telefone,");
            Sql.AppendLine("		qualificacao");
            Sql.AppendLine(" From	vw_dcnt_prot_socios_empresa");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            if (!string.IsNullOrEmpty(_cpf_cnpj_socio_empresa))
            {
                Sql.AppendLine(" And	cpf_cnpj_socio_empresa = '" + _cpf_cnpj_socio_empresa + "'");
            }
          
              Sql.AppendLine(" And	cpf_cnpj_socio = '" + _cpf_cnpj_socio + "'");
          
            Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            /*  Sql.AppendLine(" And	nome_socio = _nome_socio");
              Sql.AppendLine(" And	nome_mae = _nome_mae");
              Sql.AppendLine(" And	tipo_socio = _tipo_socio");
              Sql.AppendLine(" And	nacionalidade = _nacionalidade");
              Sql.AppendLine(" And	naturalidade = _naturalidade");
              Sql.AppendLine(" And	estado_civil = _estado_civil");
              Sql.AppendLine(" And	regime_bens = _regime_bens");
              Sql.AppendLine(" And	data_nasc = _data_nasc");
              Sql.AppendLine(" And	tipo_doc_ident = _tipo_doc_ident");
              Sql.AppendLine(" And	no_doc_ident = _no_doc_ident");
              Sql.AppendLine(" And	orgao_exped = _orgao_exped");
              Sql.AppendLine(" And	tipo_logradouro = _tipo_logradouro");
              Sql.AppendLine(" And	logradouro = _logradouro");
              Sql.AppendLine(" And	no_logradouro = _no_logradouro");
              Sql.AppendLine(" And	bairro = _bairro");
              Sql.AppendLine(" And	municipio = _municipio");
              Sql.AppendLine(" And	cep = _cep");
              Sql.AppendLine(" And	uf = _uf");
              Sql.AppendLine(" And	profissao = _profissao");
              Sql.AppendLine(" And	uf_orgao_exped = _uf_orgao_exped");
              Sql.AppendLine(" And	qtd_cotas = _qtd_cotas");
              Sql.AppendLine(" And	adm = _adm"); */


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("cnt_prot_socios_empresa");
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
        // Codigo QueryAdm ******************
        #region
        public DataTable QueryAdm()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		cpf_cnpj_socio_empresa, ");
            Sql.AppendLine("		cpf_cnpj_socio, ");
            Sql.AppendLine("		cod_protocolo, ");
            Sql.AppendLine("		nome_socio, ");
            Sql.AppendLine("        nome_pai, ");
            Sql.AppendLine("		nome_mae, ");
            Sql.AppendLine("		tipo_socio, ");
            Sql.AppendLine("		nacionalidade, ");
            Sql.AppendLine("		naturalidade, ");
            Sql.AppendLine("		estado_civil, ");
            Sql.AppendLine("		regime_bens, ");
            Sql.AppendLine("		data_nasc, ");
            Sql.AppendLine("		tipo_doc_ident, ");
            Sql.AppendLine("		no_doc_ident, ");
            Sql.AppendLine("		orgao_exped, ");
            Sql.AppendLine("		tipo_logradouro, ");
            Sql.AppendLine("		logradouro, ");
            Sql.AppendLine("		no_logradouro, ");
            Sql.AppendLine("		bairro, ");
            Sql.AppendLine("		municipio, ");
            Sql.AppendLine("		cep, ");
            Sql.AppendLine("		uf, ");
            Sql.AppendLine("        e_mail, ");
            Sql.AppendLine("		profissao, ");
            Sql.AppendLine("		uf_orgao_exped, ");
            Sql.AppendLine("		qtd_cotas, ");
            Sql.AppendLine("		adm,");
            Sql.AppendLine("		qualificacao");
            Sql.AppendLine(" From	cnt_prot_socios_empresa");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 

            Sql.AppendLine(" And (cod_protocolo = '" + _cod_protocolo + "')");
            Sql.AppendLine(" And	adm = " + _adm);
            /*  Sql.AppendLine(" And	nome_socio = _nome_socio");
              Sql.AppendLine(" And	nome_mae = _nome_mae");
              Sql.AppendLine(" And	tipo_socio = _tipo_socio");
              Sql.AppendLine(" And	nacionalidade = _nacionalidade");
              Sql.AppendLine(" And	naturalidade = _naturalidade");
              Sql.AppendLine(" And	estado_civil = _estado_civil");
              Sql.AppendLine(" And	regime_bens = _regime_bens");
              Sql.AppendLine(" And	data_nasc = _data_nasc");
              Sql.AppendLine(" And	tipo_doc_ident = _tipo_doc_ident");
              Sql.AppendLine(" And	no_doc_ident = _no_doc_ident");
              Sql.AppendLine(" And	orgao_exped = _orgao_exped");
              Sql.AppendLine(" And	tipo_logradouro = _tipo_logradouro");
              Sql.AppendLine(" And	logradouro = _logradouro");
              Sql.AppendLine(" And	no_logradouro = _no_logradouro");
              Sql.AppendLine(" And	bairro = _bairro");
              Sql.AppendLine(" And	municipio = _municipio");
              Sql.AppendLine(" And	cep = _cep");
              Sql.AppendLine(" And	uf = _uf");
              Sql.AppendLine(" And	profissao = _profissao");
              Sql.AppendLine(" And	uf_orgao_exped = _uf_orgao_exped");
              Sql.AppendLine(" And	qtd_cotas = _qtd_cotas");
              Sql.AppendLine(" And	adm = _adm"); */


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("cnt_prot_socios_empresa");
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
        // Codigo QueryMostraTodos
        #region
        public DataTable QueryProtocoloCpf()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		cpf_cnpj_socio, ");
            Sql.AppendLine("		cod_protocolo, ");
            Sql.AppendLine("		nome_socio, ");
            Sql.AppendLine("        nome_pai, ");
            Sql.AppendLine("		nome_mae, ");
            Sql.AppendLine("		tipo_socio, ");
            Sql.AppendLine("		nacionalidade, ");
            Sql.AppendLine("		naturalidade, ");
            Sql.AppendLine("		estado_civil, ");
            Sql.AppendLine("		regime_bens, ");
            Sql.AppendLine("		data_nasc, ");
            Sql.AppendLine("        tipo_emancipacao, ");
            Sql.AppendLine("        compl_logradouro, ");
            Sql.AppendLine("		tipo_doc_ident, ");
            Sql.AppendLine("		no_doc_ident, ");
            Sql.AppendLine("		orgao_exped, ");
            Sql.AppendLine("		tipo_logradouro, ");
            Sql.AppendLine("		logradouro, ");
            Sql.AppendLine("		no_logradouro, ");
            Sql.AppendLine("		bairro, ");
            Sql.AppendLine("		municipio, ");
            Sql.AppendLine("		cep, ");
            Sql.AppendLine("		uf, ");
            Sql.AppendLine("        e_mail, ");
            Sql.AppendLine("		profissao, ");
            Sql.AppendLine("		uf_orgao_exped, ");
            Sql.AppendLine("		qtd_cotas, ");
            Sql.AppendLine("		adm, ");
            Sql.AppendLine("		reque_protocolo"); 
            Sql.AppendLine(" From	cnt_prot_socios_empresa");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	cpf_cnpj_socio = '" + _cpf_cnpj_socio + "'");
            Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            Sql.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");


            /*  Sql.AppendLine(" And	nome_socio = _nome_socio");
              Sql.AppendLine(" And	nome_mae = _nome_mae");
              Sql.AppendLine(" And	tipo_socio = _tipo_socio");
              Sql.AppendLine(" And	nacionalidade = _nacionalidade");
              Sql.AppendLine(" And	naturalidade = _naturalidade");
              Sql.AppendLine(" And	estado_civil = _estado_civil");
              Sql.AppendLine(" And	regime_bens = _regime_bens");
              Sql.AppendLine(" And	data_nasc = _data_nasc");
              Sql.AppendLine(" And	tipo_doc_ident = _tipo_doc_ident");
              Sql.AppendLine(" And	no_doc_ident = _no_doc_ident");
              Sql.AppendLine(" And	orgao_exped = _orgao_exped");
              Sql.AppendLine(" And	tipo_logradouro = _tipo_logradouro");
              Sql.AppendLine(" And	logradouro = _logradouro");
              Sql.AppendLine(" And	no_logradouro = _no_logradouro");
              Sql.AppendLine(" And	bairro = _bairro");
              Sql.AppendLine(" And	municipio = _municipio");
              Sql.AppendLine(" And	cep = _cep");
              Sql.AppendLine(" And	uf = _uf");
              Sql.AppendLine(" And	profissao = _profissao");
              Sql.AppendLine(" And	uf_orgao_exped = _uf_orgao_exped");
              Sql.AppendLine(" And	qtd_cotas = _qtd_cotas");
              Sql.AppendLine(" And	adm = _adm"); */


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("cnt_prot_socios_empresa");
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


