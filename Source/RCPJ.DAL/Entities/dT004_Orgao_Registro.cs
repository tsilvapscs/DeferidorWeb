using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT004_Orgao_Registro : DBInteractionBase
    {
        //noni
        // Variables ******************* 
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected string _t004_in_org_reg = "1";
        protected string _t004_ds_org_reg;
        protected decimal _a015_co_tipo_logradouro;
        protected string _t004_ds_logradouro;
        protected string _t004_nr_logradouro;
        protected string _t004_ds_complemento;
        protected string _t004_ds_bairro;
        protected decimal _a005_co_municipio;
        protected decimal _a004_co_pais;
        protected string _t004_nr_cep;
        protected string _t004_ds_referencia;
        private string _t004_uf;
        private string _t004_ws_regin;
        private string _t004_DS_SIGLA_ORG_REG;
        private int _t004_gera_protocolo;
        private string _t004_ws_viabilidade;
        private int _t004_in_carrega_dbe = 1;

        private int _t004_gera_capa;
        private int _t004_grava_preparo;
        private int _t004_imp_obj;
        private int _t004_tip_doc;
        private int _t004_in_imp_qsa;
        private string Ws_viabilidade;
        private string _t004_dbe_org;

        public string T004_ws_viabilidade
        {
            get { return _t004_ws_viabilidade; }
            set { _t004_ws_viabilidade = value; }
        }

        

        public int T004_gera_protocolo
        {
            get { return _t004_gera_protocolo; }
            set { _t004_gera_protocolo = value; }
        }
        

        public int T004_gera_capa
        {
            get { return _t004_gera_capa; }
            set { _t004_gera_capa = value; }
        }
       

        public int T004_imp_obj
        {
            get { return _t004_imp_obj; }
            set { _t004_imp_obj = value; }
        }
       

        public int T004_grava_preparo
        {
            get { return _t004_grava_preparo; }
            set { _t004_grava_preparo = value; }
        }
       

        public int T004_tip_doc
        {
            get { return _t004_tip_doc; }
            set { _t004_tip_doc = value; }
        }
       

        public int T004_in_carrega_dbe
        {
            get { return _t004_in_carrega_dbe; }
            set { _t004_in_carrega_dbe = value; }
        }

        
      

        public int T004_in_imp_qsa
        {
            get { return _t004_in_imp_qsa; }
            set { _t004_in_imp_qsa = value; }
        }
        

        public string Ws_viabilidade1
        {
            get { return Ws_viabilidade; }
            set { Ws_viabilidade = value; }
        }
       
        public string T004_dbe_org
        {
            get { return _t004_dbe_org; }
            set { _t004_dbe_org = value; }
        }







        public string T004_DS_SIGLA_ORG_REG
        {
            get { return _t004_DS_SIGLA_ORG_REG; }
            set { _t004_DS_SIGLA_ORG_REG = value; }
        }
     
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string t004_in_org_reg
        {
            get { return _t004_in_org_reg; }

            set { _t004_in_org_reg = value; }
        }

        public string t004_ds_org_reg
        {
            get { return _t004_ds_org_reg; }

            set { _t004_ds_org_reg = value; }
        }

        public decimal a015_co_tipo_logradouro
        {
            get { return _a015_co_tipo_logradouro; }

            set { _a015_co_tipo_logradouro = value; }
        }

        public string t004_ds_logradouro
        {
            get { return _t004_ds_logradouro; }

            set { _t004_ds_logradouro = value; }
        }

        public string t004_nr_logradouro
        {
            get { return _t004_nr_logradouro; }

            set { _t004_nr_logradouro = value; }
        }

        public string t004_ds_complemento
        {
            get { return _t004_ds_complemento; }

            set { _t004_ds_complemento = value; }
        }

        public string t004_ds_bairro
        {
            get { return _t004_ds_bairro; }

            set { _t004_ds_bairro = value; }
        }

        public decimal a005_co_municipio
        {
            get { return _a005_co_municipio; }

            set { _a005_co_municipio = value; }
        }

        public decimal a004_co_pais
        {
            get { return _a004_co_pais; }

            set { _a004_co_pais = value; }
        }

        public string t004_nr_cep
        {
            get { return _t004_nr_cep; }

            set { _t004_nr_cep = value; }
        }

        public string t004_ds_referencia
        {
            get { return _t004_ds_referencia; }

            set { _t004_ds_referencia = value; }
        }
        public string t004_uf
        {
            get { return _t004_uf; }
            set { _t004_uf = value; }
        }
        public string T004_ws_regin
        {
            get { return _t004_ws_regin; }
            set { _t004_ws_regin = value; }
        }
        #endregion


        #region Implements
        public void Update_registro()
        {
          
            StringBuilder SqlU = new StringBuilder();

          

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T004_Orgao_Registro Set ");
            SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            SqlU.AppendLine("		t004_in_org_reg = @v_t004_in_org_reg, ");
            SqlU.AppendLine("		t004_ds_org_reg = @v_t004_ds_org_reg, ");
            SqlU.AppendLine("		a015_co_tipo_logradouro = @v_a015_co_tipo_logradouro, ");
            SqlU.AppendLine("		t004_ds_logradouro = @v_t004_ds_logradouro, ");
            SqlU.AppendLine("		t004_nr_logradouro = @v_t004_nr_logradouro, ");
            SqlU.AppendLine("		t004_ds_complemento = @v_t004_ds_complemento, ");
            SqlU.AppendLine("		t004_ds_bairro = @v_t004_ds_bairro, ");
            SqlU.AppendLine("		a005_co_municipio = @v_a005_co_municipio, ");
            SqlU.AppendLine("		a004_co_pais = @v_a004_co_pais, ");
            SqlU.AppendLine("		t004_nr_cep = @v_t004_nr_cep, ");
            SqlU.AppendLine("		t004_ds_referencia = @v_t004_ds_referencia,");
            SqlU.AppendLine("		t004_uf = @v_t004_uf, ");

            SqlU.AppendLine("	  T004_IN_GERA_PROTOCOLO=@v_T004_IN_GERA_PROTOCOLO, ");
            SqlU.AppendLine("	   T004_IN_GERA_CAPA=@v_T004_IN_GERA_CAPA, ");
            SqlU.AppendLine("	  T004_IN_IMP_OBJ = @v_T004_IN_IMP_OBJ, ");
            SqlU.AppendLine("	  T004_IN_GRAVA_PREPARO = @v_T004_IN_GRAVA_PREPARO, ");
            SqlU.AppendLine("	  T004_TIP_DOC = @v_T004_TIP_DOC, ");
            SqlU.AppendLine("	  T004_IN_CARREGA_DBE = @v_T004_IN_CARREGA_DBE, ");
            SqlU.AppendLine("	  T004_IN_IMP_QSA = @v_T004_IN_IMP_QSA, ");
            SqlU.AppendLine("	  T004_WS_VIABILIDADE = @v_T004_WS_VIABILIDADE, ");
            SqlU.AppendLine("	  T004_DBE_ORG = @v_T004_DBE_ORG, ");
            SqlU.AppendLine("	  t004_ws_regin = @v_t004_ws_regin ");


            SqlU.AppendLine(" Where t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg 	 ");

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_in_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a015_co_tipo_logradouro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a015_co_tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_complemento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_complemento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a005_co_municipio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a005_co_municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_pais", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_referencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_referencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GERA_PROTOCOLO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_gera_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GERA_CAPA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_gera_capa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_IMP_OBJ", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_imp_obj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GRAVA_PREPARO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_grava_preparo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_TIP_DOC", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_tip_doc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_CARREGA_DBE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_carrega_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_IMP_QSA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_imp_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_WS_VIABILIDADE", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ws_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_DBE_ORG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_dbe_org));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ws_regin", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ws_regin));
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
                cmdToExecute.ExecuteNonQuery();
                // Execute query. 
              
                
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

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t004_orgao_registro");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t004_in_org_reg, ");
            SqlI.AppendLine("	t004_ds_org_reg, ");
            SqlI.AppendLine("	a015_co_tipo_logradouro, ");
            SqlI.AppendLine("	t004_ds_logradouro, ");
            SqlI.AppendLine("	t004_nr_logradouro, ");
            SqlI.AppendLine("	t004_ds_complemento, ");
            SqlI.AppendLine("	t004_ds_bairro, ");
            SqlI.AppendLine("	a005_co_municipio, ");
            SqlI.AppendLine("	a004_co_pais, ");
            SqlI.AppendLine("	t004_nr_cep, ");
            SqlI.AppendLine("	t004_ds_referencia,");
            SqlI.AppendLine("	t004_uf");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t004_in_org_reg, ");
            SqlI.AppendLine("	@v_t004_ds_org_reg, ");
            SqlI.AppendLine("	@v_a015_co_tipo_logradouro, ");
            SqlI.AppendLine("	@v_t004_ds_logradouro, ");
            SqlI.AppendLine("	@v_t004_nr_logradouro, ");
            SqlI.AppendLine("	@v_t004_ds_complemento, ");
            SqlI.AppendLine("	@v_t004_ds_bairro, ");
            SqlI.AppendLine("	@v_a005_co_municipio, ");
            SqlI.AppendLine("	@v_a004_co_pais, ");
            SqlI.AppendLine("	@v_t004_nr_cep, ");
            SqlI.AppendLine("	@v_t004_ds_referencia,");
            SqlI.AppendLine("	@v_t004_uf");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T004_Orgao_Registro Set ");
            SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            SqlU.AppendLine("		t004_in_org_reg = @v_t004_in_org_reg, ");
            SqlU.AppendLine("		t004_ds_org_reg = @v_t004_ds_org_reg, ");
            SqlU.AppendLine("		a015_co_tipo_logradouro = @v_a015_co_tipo_logradouro, ");
            SqlU.AppendLine("		t004_ds_logradouro = @v_t004_ds_logradouro, ");
            SqlU.AppendLine("		t004_nr_logradouro = @v_t004_nr_logradouro, ");
            SqlU.AppendLine("		t004_ds_complemento = @v_t004_ds_complemento, ");
            SqlU.AppendLine("		t004_ds_bairro = @v_t004_ds_bairro, ");
            SqlU.AppendLine("		a005_co_municipio = @v_a005_co_municipio, ");
            SqlU.AppendLine("		a004_co_pais = @v_a004_co_pais, ");
            SqlU.AppendLine("		t004_nr_cep = @v_t004_nr_cep, ");
            SqlU.AppendLine("		t004_ds_referencia = @v_t004_ds_referencia,");
            SqlU.AppendLine("		t004_uf = @v_t004_uf");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_in_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a015_co_tipo_logradouro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a015_co_tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_complemento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_complemento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a005_co_municipio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a005_co_municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_pais", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_referencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_referencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_uf));
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

        public void Insert() {

            StringBuilder SqlIn = new StringBuilder();
        

            SqlIn.AppendLine(" Insert into t004_orgao_registro");
            SqlIn.AppendLine("  (");
            SqlIn.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlIn.AppendLine("	t004_in_org_reg, ");
            SqlIn.AppendLine("	t004_ds_org_reg, ");
            SqlIn.AppendLine("	a015_co_tipo_logradouro, ");
            SqlIn.AppendLine("	t004_ds_logradouro, ");
            SqlIn.AppendLine("	t004_nr_logradouro, ");
            SqlIn.AppendLine("	t004_ds_complemento, ");
            SqlIn.AppendLine("	t004_ds_bairro, ");
            SqlIn.AppendLine("	a005_co_municipio, ");
            SqlIn.AppendLine("	a004_co_pais, ");
            SqlIn.AppendLine("	t004_nr_cep, ");
            SqlIn.AppendLine("	t004_ds_referencia,");        
            SqlIn.AppendLine("	t004_ds_sigla_org_reg,");
            SqlIn.AppendLine("	t004_uf,");
            SqlIn.AppendLine("	t004_ws_regin,");
            SqlIn.AppendLine("	T004_IN_GERA_PROTOCOLO,");
            SqlIn.AppendLine("	 T004_IN_GERA_CAPA,");
            SqlIn.AppendLine("	T004_IN_IMP_OBJ,");
            SqlIn.AppendLine("	T004_IN_GRAVA_PREPARO,");
            SqlIn.AppendLine("	T004_TIP_DOC,");
            SqlIn.AppendLine("	T004_IN_CARREGA_DBE,");
            SqlIn.AppendLine("	T004_IN_IMP_QSA,");
            SqlIn.AppendLine("	T004_WS_VIABILIDADE,");
            SqlIn.AppendLine("	T004_DBE_ORG");
           

            SqlIn.AppendLine("  )");
            SqlIn.AppendLine(" Values ");
            SqlIn.AppendLine("  (");
            SqlIn.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlIn.AppendLine("	@v_t004_in_org_reg, ");
            SqlIn.AppendLine("	@v_t004_ds_org_reg, ");
            SqlIn.AppendLine("	@v_a015_co_tipo_logradouro, ");
            SqlIn.AppendLine("	@v_t004_ds_logradouro, ");
            SqlIn.AppendLine("	@v_t004_nr_logradouro, ");
            SqlIn.AppendLine("	@v_t004_ds_complemento, ");
            SqlIn.AppendLine("	@v_t004_ds_bairro, ");
            SqlIn.AppendLine("	@v_a005_co_municipio, ");
            SqlIn.AppendLine("	@v_a004_co_pais, ");
            SqlIn.AppendLine("	@v_t004_nr_cep, ");
            SqlIn.AppendLine("	@v_t004_ds_referencia,");
             SqlIn.AppendLine("	@v_t004_ds_sigla_org_reg,");
            SqlIn.AppendLine("	@v_t004_uf,");
            SqlIn.AppendLine("	@v_t004_ws_regin,");
            SqlIn.AppendLine("	@v_T004_IN_GERA_PROTOCOLO,");
            SqlIn.AppendLine("	 @v_T004_IN_GERA_CAPA,");
            SqlIn.AppendLine("	@v_T004_IN_IMP_OBJ,");
            SqlIn.AppendLine("	@v_T004_IN_GRAVA_PREPARO,");
            SqlIn.AppendLine("	@v_T004_TIP_DOC,");
            SqlIn.AppendLine("	@v_T004_IN_CARREGA_DBE,");
            SqlIn.AppendLine("	@v_T004_IN_IMP_QSA,");
            SqlIn.AppendLine("	@v_T004_WS_VIABILIDADE,");
            SqlIn.AppendLine("	@v_T004_DBE_ORG");
           
            
            SqlIn.AppendLine("  )");

        
            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlIn.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_in_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a015_co_tipo_logradouro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a015_co_tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_complemento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_complemento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a005_co_municipio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a005_co_municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_pais", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_referencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ds_referencia));
                 cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ds_sigla_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_DS_SIGLA_ORG_REG));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_ws_regin", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ws_regin));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GERA_PROTOCOLO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_gera_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GERA_CAPA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_gera_capa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_IMP_OBJ", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_imp_obj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_GRAVA_PREPARO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_grava_preparo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_TIP_DOC", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_tip_doc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_CARREGA_DBE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_carrega_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_IN_IMP_QSA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_in_imp_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_WS_VIABILIDADE", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_ws_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T004_DBE_ORG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_dbe_org));
               
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


        public void Delete()
        {

            StringBuilder SqlDel = new StringBuilder();


         
            SqlDel.AppendLine(" delete From	T004_Orgao_Registro ");
            SqlDel.AppendLine(" Where ");
            SqlDel.AppendLine(" t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg ");
           
           

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlDel.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
           
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









        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		t004_in_org_reg, ");
            Sql.AppendLine("		t004_ds_org_reg, ");
            Sql.AppendLine("		a015_co_tipo_logradouro, ");
            Sql.AppendLine("		t004_ds_logradouro, ");
            Sql.AppendLine("		t004_nr_logradouro, ");
            Sql.AppendLine("		t004_ds_complemento, ");
            Sql.AppendLine("		t004_ds_bairro, ");
            Sql.AppendLine("		a005_co_municipio, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("		t004_nr_cep, ");
            Sql.AppendLine("		t004_ds_referencia,");
            Sql.AppendLine("		t004_cod_nire,");
            Sql.AppendLine("		t004_uf,");
            Sql.AppendLine("		T004_IN_GERA_PROTOCOLO,");
            Sql.AppendLine("		T004_IN_GERA_CAPA,");
            Sql.AppendLine("		T004_IN_IMP_OBJ,");
            Sql.AppendLine("        T004_IN_GRAVA_PREPARO,");
            Sql.AppendLine("        T004_TIP_DOC,");
            Sql.AppendLine("        T004_ws_regin");
            Sql.AppendLine(" From	T004_Orgao_Registro");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	t004_nr_cnpj_org_reg = '" + _t004_nr_cnpj_org_reg + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T004_Orgao_Registro");
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

        public DataTable QueryParametro()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		t004_in_org_reg, ");
            Sql.AppendLine("		t004_ds_org_reg, ");
            Sql.AppendLine("		a015_co_tipo_logradouro, ");
            Sql.AppendLine("		t004_ds_logradouro, ");
            Sql.AppendLine("		t004_nr_logradouro, ");
            Sql.AppendLine("		t004_ds_complemento, ");
            Sql.AppendLine("		t004_ds_bairro, ");
            Sql.AppendLine("		a005_co_municipio, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("		t004_nr_cep, ");
            Sql.AppendLine("		t004_ds_referencia,");
            Sql.AppendLine("		t004_cod_nire,");
            Sql.AppendLine("		t004_uf,");
            Sql.AppendLine("		T004_IN_GERA_PROTOCOLO,");
            Sql.AppendLine("		T004_IN_GERA_CAPA,");
            Sql.AppendLine("		T004_IN_IMP_OBJ,");
            Sql.AppendLine("        T004_IN_GRAVA_PREPARO,");
            Sql.AppendLine("        T004_TIP_DOC,");
            Sql.AppendLine("        T004_ws_regin");
            Sql.AppendLine(" From	T004_Orgao_Registro");
            Sql.AppendLine(" Where	1 = 1 ");
            


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T004_Orgao_Registro");
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
        public DataTable QueryDados(string cnpj)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		t004_in_org_reg, ");
            Sql.AppendLine("		t004_ds_org_reg, ");
            Sql.AppendLine("		a015_co_tipo_logradouro, ");
            Sql.AppendLine("		t004_ds_logradouro, ");
            Sql.AppendLine("		t004_nr_logradouro, ");
            Sql.AppendLine("		t004_ds_complemento, ");
            Sql.AppendLine("		t004_ds_bairro, ");
            Sql.AppendLine("		a005_co_municipio, ");
            Sql.AppendLine("		a004_co_pais, ");
            Sql.AppendLine("		t004_nr_cep, ");
            Sql.AppendLine("		t004_ds_referencia,");
            Sql.AppendLine("		t004_cod_nire,");
            Sql.AppendLine("		t004_uf,");
            Sql.AppendLine("		T004_IN_GERA_PROTOCOLO,");
            Sql.AppendLine("		T004_IN_GERA_CAPA,");
            Sql.AppendLine("		T004_IN_IMP_OBJ,");
            Sql.AppendLine("        T004_IN_GRAVA_PREPARO,");
            Sql.AppendLine("        T004_TIP_DOC,");
            Sql.AppendLine("        T004_ws_regin");
            Sql.AppendLine(" From	T004_Orgao_Registro");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	t004_nr_cnpj_org_reg = '" + cnpj + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T004_Orgao_Registro");
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



        public DataTable QueryUF()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		TUF_UF UF, ");
            Sql.AppendLine("		TUF_NOME Estado ");

            Sql.AppendLine(" From	tab_cep_uf ");
           
           


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_cep_uf");
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


        public DataTable QueryMunicipios(string cidade)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		TMU_COD_MUN codigo_municipio, ");
            Sql.AppendLine("		TMU_NOM_MUN ds_municipio ");

            Sql.AppendLine(" From	tab_munic ");

            Sql.AppendLine(" where  TMU_NOM_MUN ='" + cidade + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_munic");
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

        public DataTable QueryCepEndereco(string cep)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		 TLG_NOME, ");
            Sql.AppendLine("		TLG_TBA_TMU_TUF_UF, ");
            Sql.AppendLine("		TLG_COMPL,");
            Sql.AppendLine("		TLG_TBA_TMU_COD_MUN");

            Sql.AppendLine(" From	tab_cep_logr ");

            Sql.AppendLine(" where  TLG_CEP8='" + cep + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_cep_logr");
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

        public DataTable QueryTipoLogradouro(string NomeVia)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Select ");

            Sql.AppendLine("		 TTI_CLAV, ");
            Sql.AppendLine("		 TTI_NOME, ");
            Sql.AppendLine("		 TTI_ABREV, ");
            Sql.AppendLine("		 TTI_ORIGEM ");

            Sql.AppendLine(" From	tab_cep_tipo");
            Sql.AppendLine(" where  TTI_NOME = '" + NomeVia + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_cep_tipo");
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


        public DataTable QueryTipoNumLogradouro(int NumVia)
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Select ");

            Sql.AppendLine("		 TTI_CLAV, ");
            Sql.AppendLine("		 TTI_NOME, ");
            Sql.AppendLine("		 TTI_ABREV, ");
            Sql.AppendLine("		 TTI_ORIGEM ");

            Sql.AppendLine(" From	tab_cep_tipo");
            Sql.AppendLine(" where  TTI_CLAV = '" + NumVia + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_cep_tipo");
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























        public DataTable QueryTipoLogradouro()
        {
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Select ");
   
            Sql.AppendLine("		 TTI_CLAV, ");
            Sql.AppendLine("		 TTI_NOME, ");
            Sql.AppendLine("		 TTI_ABREV, ");
            Sql.AppendLine("		 TTI_ORIGEM ");

            Sql.AppendLine(" From	tab_cep_tipo");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_cep_tipo");
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









        public DataTable BuscarOrgaoRegistro()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine("  SELECT ");
            Sql.AppendLine("        o.T004_NR_CNPJ_ORG_REG as cnpj ");
            Sql.AppendLine("        ,o.T004_IN_ORG_REG ");
            Sql.AppendLine("        ,o.T004_DS_ORG_REG as descricao ");
            Sql.AppendLine("        ,o.T004_DS_SIGLA_ORG_REG as sigla ");
            Sql.AppendLine("        ,t.TMU_TUF_UF as uf ");
            Sql.AppendLine("        ,o.A005_CO_MUNICIPIO as cod_municipio ");
            Sql.AppendLine("        ,t.TMU_NOM_MUN as ds_municipio ");
            Sql.AppendLine("        ,o.T004_DS_BAIRRO as ds_bairro ");
            Sql.AppendLine("        ,o.A015_CO_TIPO_LOGRADOURO as tipo_logradouro ");
            Sql.AppendLine("        ,o.T004_DS_LOGRADOURO as ds_logradouro ");
            Sql.AppendLine("        ,o.T004_NR_LOGRADOURO as numero ");
            Sql.AppendLine("        ,o.T004_DS_COMPLEMENTO as complemento ");
            Sql.AppendLine("        ,o.T004_NR_CEP as cep ");
            Sql.AppendLine("        ,o.T004_DS_REFERENCIA as ds_referencia ");
            Sql.AppendLine("        ,o.t004_cod_nire ");
            Sql.AppendLine("        ,o.t004_uf ");
            Sql.AppendLine("        ,o.T004_IN_GERA_PROTOCOLO ");
            Sql.AppendLine("        ,o.T004_IN_GERA_CAPA ");
            Sql.AppendLine("        ,o.T004_IN_IMP_OBJ");
            Sql.AppendLine("        ,o.T004_IN_GRAVA_PREPARO");
            Sql.AppendLine("        ,o.T004_TIP_DOC");
            Sql.AppendLine("        ,o.T004_IN_CARREGA_DBE ");
            Sql.AppendLine("        ,o.T004_IN_IMP_QSA ");
            Sql.AppendLine("        ,o.T004_WS_VIABILIDADE ");
            Sql.AppendLine("        ,o.T004_URL_CONSULTA_SIARCO ");
            Sql.AppendLine("        ,o.T004_DBE_ORG ");
            Sql.AppendLine("        ,o.T004_ws_regin");
            Sql.AppendLine(" FROM   t004_orgao_registro o ");
            Sql.AppendLine("        LEFT JOIN tab_munic t ");
            Sql.AppendLine("            ON o.A005_CO_MUNICIPIO = t.TMU_COD_MUN ");
            Sql.AppendLine(" Where	1 = 1 ");

            if (!string.IsNullOrEmpty(_t004_nr_cnpj_org_reg))
            {
                Sql.AppendLine(" And	t004_nr_cnpj_org_reg = '" + _t004_nr_cnpj_org_reg + "'");
            }
            if (!string.IsNullOrEmpty(_t004_uf))
            {
                Sql.AppendLine(" And	t004_uf = '" + _t004_uf + "'");
            }
            if (_t004_in_org_reg != "")
            {
                Sql.AppendLine(" And	T004_IN_ORG_REG = " + _t004_in_org_reg);
            }

            Sql.AppendLine(" ORDER BY t.TMU_NOM_MUN");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T004_Orgao_Registro");
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

        public DataTable GetParametros(string pCnpj)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" SELECT T004_NR_CNPJ_ORG_REG cnpj
                                   , T019_COD_PARAMETRO codigo
                                   , T019_DS_PARAMETRO descricao
                                   , T019_VL_PARAMETRO valor
                            FROM t019_parametro 
                            Where	1 = 1 ");
            Sql.AppendLine(" And	T004_NR_CNPJ_ORG_REG = '" + pCnpj + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a011_parametro");
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

        public DataTable QueryWsViabilidade(string pUF)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" SELECT T004_WS_VIABILIDADE
                            FROM t004_orgao_registro 
                            Where	1 = 1 ");
            Sql.AppendLine(" And	t004_uf = '" + pUF + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t004_orgao_registro");
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

        public DataTable QueryWsRegin(string pUF)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" SELECT T004_WS_REGIN
                            FROM t004_orgao_registro 
                            Where	1 = 1 ");
            if(!string.IsNullOrEmpty(pUF))
                Sql.AppendLine(" And	t004_uf = '" + pUF + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t004_orgao_registro");
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

        public DataTable QueryUrlConsultaEmpresa(string pUf)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" SELECT T004_URL_CONSULTA_SIARCO
                            FROM t004_orgao_registro 
                            Where	1 = 1 ");
            Sql.AppendLine(" And	t004_uf= '" + pUf + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t004_orgao_registro");
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

        public DataTable GetListaEndereco()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Select o.t004_uf as uf , o.T004_WS_ENDERECO url from t004_orgao_registro o where o.T004_IN_ORG_REG =1");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t004_orgao_registro");
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


