using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT002_Pessoa_Fisica : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t001_sq_pessoa;
        protected string _t002_nr_cpf;
        protected decimal _a012_co_estado_civil;
        protected decimal _a013_co_regime_bens;
        protected string _t002_nome_pai;
        protected string _t002_nome_mae;
        protected string _t002_nr_documento;
        protected decimal _a010_co_tipo_documento;
        protected string _t002_ds_emissor_documento;
        protected string _a004_uf_org_exped;
        protected Nullable<DateTime> _t002_dt_emissao_documento;
        protected decimal _a004_co_pais;
        protected string _t002_in_sexo;
        private string _t002_ds_nacionalidade;
        protected string _a004_co_uf_naturalidade;
        protected Nullable<DateTime> _t002_dt_nascimento;
        protected decimal _a014_co_emancipacao;
        // Flavio
        protected decimal _a020_co_profissao;
        protected string _t002_ds_profissao;
        protected decimal _t002_aporte_socio = 0;
        protected decimal _t002_nr_qtd_cotas = 0;
        private decimal _t002_capital_integralizado = 0;
        private decimal _t002_capital_a_integralizar = 0;
        private decimal _t002_perc_capital = 0;
        private Nullable<DateTime> _t002_data_final_integralizacao;
        protected string _t002_analfabeto;
        protected string _t002_tipo_visto;
        protected Nullable<DateTime> _t002_dt_emissao_visto;
        protected Nullable<DateTime> _t002_dt_validade_visto;
        private Nullable<DateTime> _t002_dt_obito;
        private string _t002_nire;
        private string _t002_ds_orgao_expedidor;
        private string _t002_cpf_outorgante;
        private string _t002_ds_outorgante;
        private int _adminstracaoIsoladamente = 2;
        private int _adminstracaoConjuntamente = 2;
        private int _adminstracaoTodos = 2;
        private int _t002_tip_orgao_registro = 1;
        private int _t002_in_siarco = 1;
        private int _t002_in_div_dbe = 1;
        private int _t002_tipo_acao = 1;
        private Nullable<DateTime> _t002_dt_saida_adm;
        private int _t002_in_resp_livro = 2;
        private int _t002_in_resp_ativo_passivo = 2;
        private int _t002_co_escolaridade = 0;
       
        #endregion

        #region Class Member Declarations
        //Flavio
        public string t002_tipo_visto
        {
            get {return _t002_tipo_visto;}
            set{_t002_tipo_visto = value;}
        }
        public Nullable<DateTime> t002_emissao_visto
        {
            get{return _t002_dt_emissao_visto;}
            set{_t002_dt_emissao_visto = value;}
        }
        public Nullable<DateTime> t002_dt_validade_visto
        {
            get {return _t002_dt_validade_visto;}
            set {_t002_dt_validade_visto = value;}
        }

        public decimal t002_capital_integralizado
        {
            get { return _t002_capital_integralizado; }
            set { _t002_capital_integralizado = value; }
        }
        public decimal t002_capital_a_integralizar
        {
            get { return _t002_capital_a_integralizar; }
            set { _t002_capital_a_integralizar = value; }
        }
        public decimal t002_perc_capital
        {
            get { return _t002_perc_capital; }
            set { _t002_perc_capital = value; }
        }
        
        public Nullable<DateTime> t002_data_final_integralizacao
        {
            get { return _t002_data_final_integralizacao; }
            set { _t002_data_final_integralizacao = value; }
        }

        public string t002_analfabeto
        {
            get { return _t002_analfabeto; }
            set { _t002_analfabeto = value; }
        }
        public string t002_nome_pai
        {
            get { return _t002_nome_pai; }
            set { _t002_nome_pai = value; }
        }
        public string t002_nome_mae
        {
            get { return _t002_nome_mae; }
            set { _t002_nome_mae = value; }
        }
        public string t002_ds_profissao
        {
            get { return _t002_ds_profissao; }
            set { _t002_ds_profissao = value; }
        }
        public decimal a020_co_profissao
        {
            get { return _a020_co_profissao; }
            set { _a020_co_profissao = value; }
        }
        public decimal t002_nr_qtd_cotas
        {
            get { return _t002_nr_qtd_cotas; }
            set { _t002_nr_qtd_cotas = value; }
        }
        public decimal t002_aportesocio
        {
            get { return _t002_aporte_socio; }
            set { _t002_aporte_socio = value; }
        }
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }

            set { _t001_sq_pessoa = value; }
        }

        public string t002_nr_cpf
        {
            get { return _t002_nr_cpf; }

            set { _t002_nr_cpf = value; }
        }

        public decimal a012_co_estado_civil
        {
            get { return _a012_co_estado_civil; }

            set { _a012_co_estado_civil = value; }
        }

        public decimal a013_co_regime_bens
        {
            get { return _a013_co_regime_bens; }

            set { _a013_co_regime_bens = value; }
        }

        public string t002_nr_documento
        {
            get { return _t002_nr_documento; }

            set { _t002_nr_documento = value; }
        }

        public decimal a010_co_tipo_documento
        {
            get { return _a010_co_tipo_documento; }

            set { _a010_co_tipo_documento = value; }
        }

        public string t002_ds_emissor_documento // era datetime?
        {
            get { return _t002_ds_emissor_documento; }

            set { _t002_ds_emissor_documento = value; }
        }
        public string a004_uf_org_exped
        {
            get { return _a004_uf_org_exped; }

            set { _a004_uf_org_exped = value; }
        }
        public Nullable<DateTime> t002_dt_emissao_documento
        {
            get { return _t002_dt_emissao_documento; }

            set { _t002_dt_emissao_documento = value; }
        }

        public decimal a004_co_pais
        {
            get { return _a004_co_pais; }

            set { _a004_co_pais = value; }
        }

        public string t002_ds_nacionalidade
        {
            get { return _t002_ds_nacionalidade; }
            set { _t002_ds_nacionalidade = value; }
        }

        public string t002_in_sexo
        {
            get { return _t002_in_sexo; }

            set { _t002_in_sexo = value; }
        }

        public string a004_co_uf_naturalidade
        {
            get { return _a004_co_uf_naturalidade; }

            set { _a004_co_uf_naturalidade = value; }
        }

        public Nullable<DateTime> t002_dt_nascimento
        {
            get { return _t002_dt_nascimento; }

            set { _t002_dt_nascimento = value; }
        }

        public decimal a014_co_emancipacao
        {
            get { return _a014_co_emancipacao; }

            set { _a014_co_emancipacao = value; }
        }
        public Nullable<DateTime> t002_dt_obito
        {
            get { return _t002_dt_obito; }
            set { _t002_dt_obito = value; }
        }
        public string t002_nire
        {
            get { return _t002_nire; }
            set { _t002_nire = value; }
        }
        public string t002_ds_orgao_expedidor
        {
            get { return _t002_ds_orgao_expedidor; }
            set { _t002_ds_orgao_expedidor = value; }
        }
        public string t002_cpf_outorgante
        {
            get { return _t002_cpf_outorgante; }
            set { _t002_cpf_outorgante = value; }
        }
        public string t002_ds_outorgante
        {
            get { return _t002_ds_outorgante; }
            set { _t002_ds_outorgante = value; }
        }
        public int AdminstracaoIsoladamente
        {
            get { return _adminstracaoIsoladamente; }
            set { _adminstracaoIsoladamente = value; }
        }
        public int AdminstracaoConjuntamente
        {
            get { return _adminstracaoConjuntamente; }
            set { _adminstracaoConjuntamente = value; }
        }
        public int AdminstracaoTodos
        {
            get { return _adminstracaoTodos; }
            set { _adminstracaoTodos = value; }
        }
        public int t002_tip_orgao_registro
        {
            get { return _t002_tip_orgao_registro; }
            set { _t002_tip_orgao_registro = value; }
        }

        public int t002_in_siarco
        {
            get { return _t002_in_siarco; }
            set { _t002_in_siarco = value; }
        }

        public int t002_in_div_dbe
        {
            get { return _t002_in_div_dbe; }
            set { _t002_in_div_dbe = value; }
        }
        public int t002_tipo_acao
        {
            get { return _t002_tipo_acao; }
            set { _t002_tipo_acao = value; }
        }

        public Nullable<DateTime> t002_dt_saida_adm
        {
            get { return _t002_dt_saida_adm; }
            set { _t002_dt_saida_adm = value; }
        }
        public int t002_in_resp_livro
        {
            get { return _t002_in_resp_livro; }
            set { _t002_in_resp_livro = value; }
        }
        public int t002_in_resp_ativo_passivo
        {
            get { return _t002_in_resp_ativo_passivo; }
            set { _t002_in_resp_ativo_passivo = value; }
        }
        public int T002_co_escolaridade
        {
            get { return _t002_co_escolaridade; }
            set { _t002_co_escolaridade = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t002_pessoa_fisica");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	t002_nr_cpf, ");
            SqlI.AppendLine("	a012_co_estado_civil, ");
            SqlI.AppendLine("	a013_co_regime_bens, ");
            SqlI.AppendLine("   t002_nome_pai, ");
            SqlI.AppendLine("   t002_nome_mae, ");
            SqlI.AppendLine("	t002_nr_documento, ");
            SqlI.AppendLine("	a010_co_tipo_documento, ");
            SqlI.AppendLine("	t002_ds_emissor_documento, ");
            SqlI.AppendLine("   a004_uf_org_exped, ");
            SqlI.AppendLine("	t002_dt_emissao_documento, ");
            SqlI.AppendLine("	a004_co_pais, ");
            SqlI.AppendLine("   t002_ds_nacionalidade, ");
            SqlI.AppendLine("	t002_in_sexo, ");
            SqlI.AppendLine("	a004_co_uf_naturalidade, ");
            SqlI.AppendLine("	t002_dt_nascimento, ");
            SqlI.AppendLine("	a020_co_profissao, ");
            SqlI.AppendLine("   t002_ds_profissao, ");
            SqlI.AppendLine("	t002_nr_qtd_cotas, ");
            SqlI.AppendLine("   t002_capital_integralizado, ");
            SqlI.AppendLine("   t002_capital_a_integralizar, ");
            SqlI.AppendLine("   t002_data_final_integralizacao, ");
            SqlI.AppendLine("   t002_analfabeto, ");
            SqlI.AppendLine("   t002_tipo_visto, ");
            SqlI.AppendLine("   t002_dt_emissao_visto, ");
            SqlI.AppendLine("   t002_dt_validade_visto, ");
            SqlI.AppendLine("	a014_co_emancipacao, ");
            SqlI.AppendLine("	t002_dt_obito, ");
            SqlI.AppendLine("	t002_nire, ");
            SqlI.AppendLine("	t002_ds_orgao_expedidor,");
            SqlI.AppendLine("	t002_cpf_outorgante,");
            SqlI.AppendLine("	t002_ds_outorgante,");
            SqlI.AppendLine("	T002_IN_ADM_ISOLADAMENTE,");
            SqlI.AppendLine("	T002_IN_ADM_CONJUNTAMENTE,");
            SqlI.AppendLine("	T002_IN_ADM_TODOS,");
            SqlI.AppendLine("	T002_TIP_ORGAO_REGISTRO,");
            SqlI.AppendLine("	T002_IN_SIARCO,");
            SqlI.AppendLine("	T002_IN_DIV_DBE,");
            SqlI.AppendLine("	T002_PERC_CAPITAL,");
            SqlI.AppendLine("	T002_DT_SAIDA_ADM,");
            SqlI.AppendLine("	T002_IN_RESP_LIVRO,");
            SqlI.AppendLine("	T002_IN_RESP_ATIVO_PASSIVO,");
            SqlI.AppendLine("	T002_CO_ESCOLARIDADE");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	@v_t002_nr_cpf, ");
            SqlI.AppendLine("	@v_a012_co_estado_civil, ");
            SqlI.AppendLine("	@v_a013_co_regime_bens, ");
            SqlI.AppendLine("   @v_t002_nome_pai, ");
            SqlI.AppendLine("   @v_t002_nome_mae, ");
            SqlI.AppendLine("	@v_t002_nr_documento, ");
            SqlI.AppendLine("	@v_a010_co_tipo_documento, ");
            SqlI.AppendLine("	@v_t002_ds_emissor_documento, ");
            SqlI.AppendLine("   @v_a004_uf_org_exped, ");
            SqlI.AppendLine("	evaldate(@v_t002_dt_emissao_documento), ");
            SqlI.AppendLine("	@v_a004_co_pais, ");
            SqlI.AppendLine("   @v_t002_ds_nacionalidade, ");
            SqlI.AppendLine("	@v_t002_in_sexo, ");
            SqlI.AppendLine("	@v_a004_co_uf_naturalidade, ");
            SqlI.AppendLine("	evaldate(@v_t002_dt_nascimento), ");
            SqlI.AppendLine("	@v_a020_co_profissao, ");
            SqlI.AppendLine("   @v_t002_ds_profissao, ");
            SqlI.AppendLine("	@v_t002_nr_qtd_cotas, ");
            SqlI.AppendLine("   @v_t002_capital_integralizado, ");
            SqlI.AppendLine("   @v_t002_capital_a_integralizar, ");
            SqlI.AppendLine("   evaldate(@v_t002_data_final_integralizacao), ");
            SqlI.AppendLine("   @v_t002_analfabeto, ");
            SqlI.AppendLine("   @v_t002_tipo_visto, ");
            SqlI.AppendLine("   @v_t002_dt_emissao_visto, ");
            SqlI.AppendLine("   @v_t002_dt_validade_visto, ");
            SqlI.AppendLine("	@v_a014_co_emancipacao, ");
            SqlI.AppendLine("	@v_t002_dt_obito,");
            SqlI.AppendLine("	@v_t002_nire, ");
            SqlI.AppendLine("   @v_t002_ds_orgao_expedidor, ");
            SqlI.AppendLine("   @v_t002_cpf_outorgante, ");
            SqlI.AppendLine("   @v_t002_ds_outorgante, ");
            SqlI.AppendLine("	@v_T002_IN_ADM_ISOLADAMENTE,");
            SqlI.AppendLine("	@v_T002_IN_ADM_CONJUNTAMENTE,");
            SqlI.AppendLine("	@v_T002_IN_ADM_TODOS,");
            SqlI.AppendLine("	@v_t002_tip_orgao_registro,");
            SqlI.AppendLine("	@v_T002_IN_SIARCO,");
            SqlI.AppendLine("	@v_T002_IN_DIV_DBE,");
            SqlI.AppendLine("	@v_T002_PERC_CAPITAL,");
            SqlI.AppendLine("	@v_T002_DT_SAIDA_ADM,");
            SqlI.AppendLine("	@v_T002_IN_RESP_LIVRO,");
            SqlI.AppendLine("	@v_T002_IN_RESP_ATIVO_PASSIVO,");
            SqlI.AppendLine("	@v_T002_CO_ESCOLARIDADE");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T002_Pessoa_Fisica Set ");
            //SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");
            SqlU.AppendLine("		t002_nr_cpf = @v_t002_nr_cpf, ");
            SqlU.AppendLine("		a012_co_estado_civil = @v_a012_co_estado_civil, ");
            SqlU.AppendLine("		a013_co_regime_bens = @v_a013_co_regime_bens, ");
            SqlU.AppendLine("       t002_nome_pai = @v_t002_nome_pai, ");
            SqlU.AppendLine("       t002_nome_mae = @v_t002_nome_mae, ");
            SqlU.AppendLine("		t002_nr_documento = @v_t002_nr_documento, ");
            SqlU.AppendLine("		a010_co_tipo_documento = @v_a010_co_tipo_documento, ");
            SqlU.AppendLine("		t002_ds_emissor_documento = @v_t002_ds_emissor_documento, ");
            SqlU.AppendLine("       a004_uf_org_exped = @v_a004_uf_org_exped, ");
            SqlU.AppendLine("		t002_dt_emissao_documento = evaldate(@v_t002_dt_emissao_documento), ");
            SqlU.AppendLine("		a004_co_pais = @v_a004_co_pais, ");
            SqlU.AppendLine("       t002_ds_nacionalidade = @v_t002_ds_nacionalidade, ");
            SqlU.AppendLine("		t002_in_sexo = @v_t002_in_sexo, ");
            SqlU.AppendLine("		a004_co_uf_naturalidade = @v_a004_co_uf_naturalidade, ");
            SqlU.AppendLine("		t002_dt_nascimento = evaldate(@v_t002_dt_nascimento), ");
            SqlU.AppendLine("		a020_co_profissao = @v_a020_co_profissao, ");
            SqlU.AppendLine("       t002_ds_profissao = @v_t002_ds_profissao, ");
            SqlU.AppendLine("		t002_nr_qtd_cotas = @v_t002_nr_qtd_cotas, ");
            SqlU.AppendLine("       t002_capital_integralizado = @v_t002_capital_integralizado, ");
            SqlU.AppendLine("       t002_capital_a_integralizar = @v_t002_capital_a_integralizar, ");
            SqlU.AppendLine("       t002_data_final_integralizacao = evaldate(@v_t002_data_final_integralizacao), ");
            SqlU.AppendLine("       t002_analfabeto = @v_t002_analfabeto, ");
            SqlU.AppendLine("       t002_tipo_visto =@v_t002_tipo_visto, ");
            SqlU.AppendLine("       t002_dt_emissao_visto = evaldate(@v_t002_dt_emissao_visto), ");
            SqlU.AppendLine("	    t002_dt_validade_visto = evaldate(@v_t002_dt_validade_visto), " );
            SqlU.AppendLine("		a014_co_emancipacao = @v_a014_co_emancipacao, ");
            SqlU.AppendLine("       t002_dt_obito = evaldate(@v_t002_dt_obito),");
            SqlU.AppendLine("       t002_nire = @v_t002_nire, ");
            SqlU.AppendLine("       t002_ds_orgao_expedidor = @v_t002_ds_orgao_expedidor,");
            SqlU.AppendLine("       t002_cpf_outorgante = @v_t002_cpf_outorgante,");
            SqlU.AppendLine("       t002_ds_outorgante = @v_t002_ds_outorgante,");
            SqlU.AppendLine("       T002_IN_ADM_ISOLADAMENTE = @v_T002_IN_ADM_ISOLADAMENTE,");
            SqlU.AppendLine("       T002_IN_ADM_CONJUNTAMENTE = @v_T002_IN_ADM_CONJUNTAMENTE,");
            SqlU.AppendLine("       T002_IN_ADM_TODOS = @v_T002_IN_ADM_TODOS,");
            SqlU.AppendLine("       T002_TIP_ORGAO_REGISTRO = @v_t002_tip_orgao_registro,");
            SqlU.AppendLine("	    T002_IN_SIARCO = @v_T002_IN_SIARCO,");
            SqlU.AppendLine("	    T002_IN_DIV_DBE = @v_T002_IN_DIV_DBE,");
            SqlU.AppendLine("	    T002_PERC_CAPITAL = @v_T002_PERC_CAPITAL,");
            SqlU.AppendLine("	    T002_DT_SAIDA_ADM = @v_T002_DT_SAIDA_ADM,");
            SqlU.AppendLine("	    T002_IN_RESP_LIVRO = @v_T002_IN_RESP_LIVRO,");
            SqlU.AppendLine("	    T002_IN_RESP_ATIVO_PASSIVO = @v_T002_IN_RESP_ATIVO_PASSIVO,");
            SqlU.AppendLine("	    T002_CO_ESCOLARIDADE = @v_T002_CO_ESCOLARIDADE");
            
            SqlU.AppendLine(" Where	");
            if (_t001_sq_pessoa != 0)
                SqlU.AppendLine(" t001_sq_pessoa = @v_t001_sq_pessoa ");
            else
                SqlU.AppendLine(" t001_sq_pessoa = -1 ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nr_cpf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nr_cpf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a012_co_estado_civil", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a012_co_estado_civil));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a013_co_regime_bens", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a013_co_regime_bens));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nome_pai", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nome_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nome_mae", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nome_mae));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nr_documento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nr_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a010_co_tipo_documento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a010_co_tipo_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_ds_emissor_documento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_ds_emissor_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_uf_org_exped", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_uf_org_exped));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_dt_emissao_documento", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_emissao_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_pais", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_ds_nacionalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_ds_nacionalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_in_sexo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_in_sexo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_uf_naturalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_uf_naturalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_dt_nascimento", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_nascimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a020_co_profissao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a020_co_profissao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_ds_profissao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_ds_profissao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nr_qtd_cotas", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nr_qtd_cotas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_integralizado", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_a_integralizar", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_a_integralizar));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_data_final_integralizacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_data_final_integralizacao));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_analfabeto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_analfabeto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_tipo_visto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_tipo_visto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_dt_emissao_visto", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_emissao_visto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_dt_validade_visto", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_validade_visto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a014_co_emancipacao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a014_co_emancipacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_dt_obito", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_obito));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nire));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_ds_orgao_expedidor", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_ds_orgao_expedidor));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_cpf_outorgante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_cpf_outorgante));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_ds_outorgante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_ds_outorgante));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_IN_ADM_ISOLADAMENTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _adminstracaoIsoladamente));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_IN_ADM_CONJUNTAMENTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _adminstracaoConjuntamente));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_IN_ADM_TODOS", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _adminstracaoTodos));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_T002_TIP_ORGAO_REGISTRO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_tip_orgao_registro));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_IN_SIARCO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_in_siarco));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_IN_DIV_DBE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_in_div_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T002_DT_SAIDA_ADM", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_dt_saida_adm));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_perc_capital", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_perc_capital));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_in_resp_livro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_in_resp_livro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_in_resp_ativo_passivo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_in_resp_ativo_passivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_co_escolaridade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_co_escolaridade));
                 
                
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
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		t002_nr_cpf, ");
            Sql.AppendLine("		a012_co_estado_civil, ");
            Sql.AppendLine("		a013_co_regime_bens, ");
            Sql.AppendLine("        t002_nome_pai, ");
            Sql.AppendLine("        t002_nome_mae, ");
            Sql.AppendLine("		t002_nr_documento, ");
            Sql.AppendLine("		a010_co_tipo_documento, ");
            Sql.AppendLine("		t002_ds_emissor_documento, ");
            Sql.AppendLine("        a004_uf_org_exped, ");
            Sql.AppendLine("		t002_dt_emissao_documento, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("        t002_ds_nacionalidade, ");
            Sql.AppendLine("		t002_in_sexo, ");
            Sql.AppendLine("		a004_co_uf_naturalidade, ");
            Sql.AppendLine("		t002_dt_nascimento, ");
            Sql.AppendLine("		a020_co_profissao, ");
            Sql.AppendLine("        t002_ds_profissao, ");
            Sql.AppendLine("		a014_co_emancipacao, ");
            Sql.AppendLine("		t002_nr_qtd_cotas, ");
            Sql.AppendLine("        t002_capital_integralizado, ");
            Sql.AppendLine("        t002_capital_a_integralizar, ");
            Sql.AppendLine("        t002_data_final_integralizacao, ");

            Sql.AppendLine("        t002_analfabeto");
            Sql.AppendLine("        t002_tipo_visto, ");
            Sql.AppendLine("        t002_dt_emissao_visto, ");
            Sql.AppendLine("        t002_dt_validade_visto, ");
            Sql.AppendLine(" From	T002_Pessoa_Fisica");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	t001_sq_pessoa = _t001_sq_pessoa");
            Sql.AppendLine(" And	t002_nr_cpf = _t002_nr_cpf");
            Sql.AppendLine(" And	a012_co_estado_civil = _a012_co_estado_civil");
            Sql.AppendLine(" And	a013_co_regime_bens = _a013_co_regime_bens");
            Sql.AppendLine(" And    t002_nome_pai = _t002_nome_pai");
            Sql.AppendLine(" And    t002_nome_mae = _t002_nome_mae");
            Sql.AppendLine(" And	t002_nr_documento = _t002_nr_documento");
            Sql.AppendLine(" And	a010_co_tipo_documento = _a010_co_tipo_documento");
            Sql.AppendLine(" And	t002_ds_emissor_documento = _t002_ds_emissor_documento");
            Sql.AppendLine(" And	t002_dt_emissao_documento = _t002_dt_emissao_documento");
            Sql.AppendLine(" And	a004_co_pais = _a004_co_pais");
            Sql.AppendLine(" And	t002_in_sexo = _t002_in_sexo");
            Sql.AppendLine(" And	a004_co_uf_naturalidade = _a004_co_uf_naturalidade");
            Sql.AppendLine(" And	t002_dt_nascimento = _t002_dt_nascimento");
            Sql.AppendLine(" And	a020_co_profissao = _a020_co_profissao");
            Sql.AppendLine(" And    t002_ds_profissao = _t002_ds_profissao");
            Sql.AppendLine(" And	a014_co_emancipacao = _a014_co_emancipacao");
            Sql.AppendLine(" And	t002_analfabeto = _t002_analfabeto");
            Sql.AppendLine(" And    t002_tipo_visto = _t002_tipo_visto, ");
            Sql.AppendLine(" And    t002_dt_emissao_visto = _t002_dt_emissao_visto, ");
            Sql.AppendLine(" And    t002_dt_validade_visto = _t002_dt_validade_visto, ");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T002_Pessoa_Fisica");
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
        public DataTable QueryCpf_aaaa(string wCpf)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		t002_nr_cpf, ");
            Sql.AppendLine("		a012_co_estado_civil, ");
            Sql.AppendLine("		a013_co_regime_bens, ");
            Sql.AppendLine("        t002_nome_pai, ");
            Sql.AppendLine("        t002_nome_mae, ");
            Sql.AppendLine("		t002_nr_documento, ");
            Sql.AppendLine("		a010_co_tipo_documento, ");
            Sql.AppendLine("		t002_ds_emissor_documento, ");
            Sql.AppendLine("        a004_uf_org_exped, ");
            Sql.AppendLine("		t002_dt_emissao_documento, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("        t002_ds_nacionalidade, ");
            Sql.AppendLine("		t002_in_sexo, ");
            Sql.AppendLine("		a004_co_uf_naturalidade, ");
            Sql.AppendLine("		t002_dt_nascimento, ");
            Sql.AppendLine("		a020_co_profissao, ");
            Sql.AppendLine("        t002_ds_profissao, ");
            Sql.AppendLine("		a014_co_emancipacao, ");
            Sql.AppendLine("		t002_nr_qtd_cotas, ");
            Sql.AppendLine("        t002_capital_integralizado, ");
            Sql.AppendLine("        t002_capital_a_integralizar, ");
            Sql.AppendLine("        t002_data_final_integralizacao, ");
            Sql.AppendLine("        t002_analfabeto");
            Sql.AppendLine("        t002_tipo_visto, ");
            Sql.AppendLine("        t002_dt_emissao_visto, ");
            Sql.AppendLine("        t002_dt_validade_visto, ");
            Sql.AppendLine("        t002_Nire ");
            Sql.AppendLine(" From	T002_Pessoa_Fisica");
            Sql.AppendLine(" Where	1 = 1 ");

            Sql.AppendLine(" And	t002_nr_cpf = '" + wCpf.Trim() + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T002_Pessoa_Fisica");
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
        public DataTable QueryXmlWs(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine("   SELECT      ");
            Sql.AppendLine("       pf.T002_NR_CPF as CNPJ,       ");
            Sql.AppendLine("       p.T001_DS_PESSOA as Nome,       ");
            Sql.AppendLine("   pf.T002_IN_SEXO as sexo,       ");
            Sql.AppendLine("   pf.T002_DT_NASCIMENTO as DataNacimento,       ");
            Sql.AppendLine("   pf.T002_NR_DOCUMENTO as IDENTIDADE,       ");
            Sql.AppendLine("   pf.T002_DS_EMISSOR_DOCUMENTO as OrgaoEmissor,     ");
            Sql.AppendLine("   null as UFEMISOR,       ");
            Sql.AppendLine("   pf.A004_CO_PAIS as PAIS,       ");
            Sql.AppendLine("   e.A015_CO_TIPO_LOGRADOURO as codLogradouro,       ");
            Sql.AppendLine("   e.R002_DS_LOGRADOURO as LOGRADOURO,       ");
            Sql.AppendLine("   e.R002_NR_LOGRADOURO,       ");
            Sql.AppendLine("   e.R002_DS_COMPLEMENTO as COMPLEMENTO,       ");
            Sql.AppendLine("   e.R002_DS_BAIRRO as BAIRRO,       ");
            Sql.AppendLine("   e.R002_NR_CEP as CEP,       ");
            Sql.AppendLine("   'RJ' as UF,       ");
            Sql.AppendLine("   null as NOMEPAI,       ");
            Sql.AppendLine("   null as NOMEMAE      ");
            Sql.AppendLine("     from t005_protocolo protocolo,     ");
            Sql.AppendLine("   r001_vinculo vinculo,     ");
            Sql.AppendLine("   T002_PESSOA_FISICA pf,      ");
            Sql.AppendLine("   R002_VINCULO_ENDERECO e,     ");
            Sql.AppendLine("   t001_pessoa p     ");
            Sql.AppendLine("   where 1 = 1      ");
            Sql.AppendLine(" and  protocolo.T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            Sql.AppendLine("   and protocolo.T001_SQ_PESSOA = vinculo.T001_SQ_PESSOA_PAI     ");
            Sql.AppendLine("   and vinculo.A009_CO_CONDICAO = \"2066\"     ");
            Sql.AppendLine("   and vinculo.T001_SQ_PESSOA = pf.T001_SQ_PESSOA       ");
            Sql.AppendLine("   and p.T001_SQ_PESSOA = pf.T001_SQ_PESSOA       ");
            Sql.AppendLine("   and e.T001_SQ_PESSOA = vinculo.T001_SQ_PESSOA     ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Requerente");
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

            Sql.AppendLine(" From	T002_Pessoa_Fisica");
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
                if (_mainConnection.State !=ConnectionState.Open)
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
                // Close connection. 
                if (_mainConnectionIsCreatedLocal)
                {

                    // Close connection. 
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void UpdateDadosSocios()
        {
             
            StringBuilder SqlU = new StringBuilder();

           

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T002_Pessoa_Fisica Set ");
            SqlU.AppendLine("		t002_nr_qtd_cotas = @v_t002_nr_qtd_cotas ");
            //SqlU.AppendLine("       t002_capital_integralizado = @v_t002_capital_integralizado, ");
            //SqlU.AppendLine("       t002_capital_a_integralizar = @v_t002_capital_a_integralizar, ");
           
            SqlU.AppendLine(" Where	");
            if (_t001_sq_pessoa != 0)
                SqlU.AppendLine(" t001_sq_pessoa = @v_t001_sq_pessoa ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
               
                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nr_qtd_cotas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nr_qtd_cotas));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_integralizado", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_integralizado));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_a_integralizar", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_a_integralizar));
               
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
            }
        }

        public void UpdateCapitalSocialSocios()
        {

            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Update T002_Pessoa_Fisica Set ");
            SqlU.AppendLine("		 t002_nr_qtd_cotas = @v_t002_nr_qtd_cotas ");
            SqlU.AppendLine("       ,t002_capital_integralizado = @v_t002_capital_integralizado ");
            SqlU.AppendLine("       ,t002_capital_a_integralizar = @v_t002_capital_a_integralizar ");

            SqlU.AppendLine(" Where	t001_sq_pessoa = @v_t001_sq_pessoa ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_nr_qtd_cotas", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_nr_qtd_cotas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_integralizado", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_capital_a_integralizar", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t002_capital_a_integralizar));

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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public void UpdateRespLivroAtivoPassivo()
        {

            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Update T002_Pessoa_Fisica Set ");
            SqlU.AppendLine("		 T002_IN_RESP_LIVRO = @v_t002_in_resp_livro, ");
            SqlU.AppendLine("		 T002_IN_RESP_ATIVO_PASSIVO = @v_t002_in_resp_ativo_passivo ");
            SqlU.AppendLine(" Where	t001_sq_pessoa = @v_t001_sq_pessoa ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_in_resp_livro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, t002_in_resp_livro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t002_in_resp_ativo_passivo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, t002_in_resp_ativo_passivo));

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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        #endregion



    }
}