using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR001_Vinculo : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t001_sq_pessoa;
        protected decimal _t001_sq_pessoa_pai;
        protected decimal _a009_co_condicao;
        protected string _r001_in_fundador;
        protected Nullable<DateTime> _r001_dt_entrada_vinculo;
        protected Nullable<DateTime> _r001_dt_saida_vinculo;
        protected string _r001_ds_cargo_direcao;
        protected string _r001_in_situacao;
        protected string _r001_in_gerente_uso_firma;
        protected decimal _r001_vl_participacao;
        protected Nullable<DateTime> _r001_dt_inicio_mandato;
        protected Nullable<DateTime> _r001_dt_termino_mandato;
        protected decimal _t001_sq_pessoa_rep_legal;
        protected decimal _a030_co_tipo_assistido;
        private int _r001_acao;
        private int _r001_ordem_filial_contrato = 0;
        private string _t001_cpf_cnpj_pessoa;

       
        static int[] tbCodigoSocios = new int[22] { 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 37, 38, 44, 45, 46, 47, 48, 49, 55, 56, 57, 58 };

        #endregion

        #region Class Member Declarations
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }

            set { _t001_sq_pessoa = value; }
        }

        public decimal t001_sq_pessoa_pai
        {
            get { return _t001_sq_pessoa_pai; }

            set { _t001_sq_pessoa_pai = value; }
        }

        public decimal a009_co_condicao
        {
            get { return _a009_co_condicao; }

            set { _a009_co_condicao = value; }
        }
        public string r001_in_fundador
        {
            get { return _r001_in_fundador; }
            set { _r001_in_fundador = value; }
        }

        public Nullable<DateTime> r001_dt_entrada_vinculo
        {
            get { return _r001_dt_entrada_vinculo; }

            set { _r001_dt_entrada_vinculo = value; }
        }

        public Nullable<DateTime> r001_dt_saida_vinculo
        {
            get { return _r001_dt_saida_vinculo; }

            set { _r001_dt_saida_vinculo = value; }
        }

        public string r001_ds_cargo_direcao
        {
            get { return _r001_ds_cargo_direcao; }

            set { _r001_ds_cargo_direcao = value; }
        }

        public string r001_in_situacao
        {
            get { return _r001_in_situacao; }

            set { _r001_in_situacao = value; }
        }

        public string r001_in_gerente_uso_firma
        {
            get { return _r001_in_gerente_uso_firma; }

            set { _r001_in_gerente_uso_firma = value; }
        }

        public decimal r001_vl_participacao
        {
            get { return _r001_vl_participacao; }

            set { _r001_vl_participacao = value; }
        }

        public Nullable<DateTime> r001_dt_inicio_mandato
        {
            get { return _r001_dt_inicio_mandato; }

            set { _r001_dt_inicio_mandato = value; }
        }

        public Nullable<DateTime> r001_dt_termino_mandato
        {
            get { return _r001_dt_termino_mandato; }

            set { _r001_dt_termino_mandato = value; }
        }

        public decimal t001_sq_pessoa_rep_legal
        {
            get { return _t001_sq_pessoa_rep_legal; }

            set { _t001_sq_pessoa_rep_legal = value; }
        }
        public decimal a030_co_tipo_assistido
        {
            get {return _a030_co_tipo_assistido;}
            set { _a030_co_tipo_assistido = value; }
        }
        public int r001_acao
        {
            get { return _r001_acao; }
            set { _r001_acao = value; }
        }

        public int R001_ordem_filial_contrato
        {
            get { return _r001_ordem_filial_contrato; }
            set { _r001_ordem_filial_contrato = value; }
        }
        public string T001_cpf_cnpj_pessoa
        {
            get { return _t001_cpf_cnpj_pessoa; }
            set { _t001_cpf_cnpj_pessoa = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r001_vinculo");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t001_sq_pessoa ");
            SqlI.AppendLine("	,t001_sq_pessoa_pai ");
            SqlI.AppendLine("	,a030_co_tipo_assistido ");
            SqlI.AppendLine("	,a009_co_condicao ");
            SqlI.AppendLine("   ,r001_in_fundador ");
            SqlI.AppendLine("	,r001_dt_entrada_vinculo ");
            SqlI.AppendLine("	,r001_dt_saida_vinculo ");
            SqlI.AppendLine("	,r001_ds_cargo_direcao ");
            SqlI.AppendLine("	,r001_in_situacao ");
            SqlI.AppendLine("	,r001_in_gerente_uso_firma ");
            SqlI.AppendLine("	,r001_vl_participacao ");
            SqlI.AppendLine("	,r001_dt_inicio_mandato ");
            SqlI.AppendLine("	,r001_dt_termino_mandato ");
            SqlI.AppendLine("	,t001_sq_pessoa_rep_legal ");
            SqlI.AppendLine("   ,r001_acao ");
            SqlI.AppendLine("   ,t001_cpf_cnpj_pessoa");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t001_sq_pessoa ");
            SqlI.AppendLine("	,@v_t001_sq_pessoa_pai ");
            SqlI.AppendLine("	,@v_a030_co_tipo_assistido ");
            SqlI.AppendLine("	,@v_a009_co_condicao ");
            SqlI.AppendLine("   ,@v_r001_in_fundador ");
            SqlI.AppendLine("	,evaldate(@v_r001_dt_entrada_vinculo) ");
            SqlI.AppendLine("	,evaldate(@v_r001_dt_saida_vinculo) ");
            SqlI.AppendLine("	,@v_r001_ds_cargo_direcao ");
            SqlI.AppendLine("	,@v_r001_in_situacao ");
            SqlI.AppendLine("	,@v_r001_in_gerente_uso_firma ");
            SqlI.AppendLine("	,@v_r001_vl_participacao ");
            SqlI.AppendLine("	,evaldate(@v_r001_dt_inicio_mandato) ");
            SqlI.AppendLine("	,evaldate(@v_r001_dt_termino_mandato) ");
            SqlI.AppendLine("	,@v_t001_sq_pessoa_rep_legal ");
            SqlI.AppendLine("	,@v_r001_acao");
            SqlI.AppendLine("   ,@v_t001_cpf_cnpj_pessoa");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R001_Vinculo Set ");
            //SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa ");
            //SqlU.AppendLine("		,t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");
            SqlU.AppendLine("		a030_co_tipo_assistido = @v_a030_co_tipo_assistido ");
            SqlU.AppendLine("		,a009_co_condicao = @v_a009_co_condicao ");
            SqlU.AppendLine("       ,r001_in_fundador = @v_r001_in_fundador ");
            SqlU.AppendLine("		,r001_dt_entrada_vinculo = evaldate(@v_r001_dt_entrada_vinculo) ");
            SqlU.AppendLine("		,r001_dt_saida_vinculo = evaldate(@v_r001_dt_saida_vinculo) ");
            SqlU.AppendLine("		,r001_ds_cargo_direcao = @v_r001_ds_cargo_direcao ");
            SqlU.AppendLine("		,r001_in_situacao = @v_r001_in_situacao ");
            SqlU.AppendLine("		,r001_in_gerente_uso_firma = @v_r001_in_gerente_uso_firma ");
            SqlU.AppendLine("		,r001_vl_participacao = @v_r001_vl_participacao ");
            SqlU.AppendLine("		,r001_dt_inicio_mandato = evaldate(@v_r001_dt_inicio_mandato) ");
            SqlU.AppendLine("		,r001_dt_termino_mandato = evaldate(@v_r001_dt_termino_mandato) ");
            SqlU.AppendLine("		,t001_sq_pessoa_rep_legal = @v_t001_sq_pessoa_rep_legal ");
            SqlU.AppendLine("		,r001_acao = @v_r001_acao");
            SqlU.AppendLine("		,t001_cpf_cnpj_pessoa = @v_t001_cpf_cnpj_pessoa");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t001_sq_pessoa = @v_t001_sq_pessoa ");
            SqlU.AppendLine(" And	t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");

            
            
            //alterado em 06/12/2014 para gravar registro de socio e administrador
            //SqlU.AppendLine(" And	a009_co_condicao = @v_a009_co_condicao ");

            //essa parte foi comentada
            //if (tbCodigoSocios.ToString().Contains(_a009_co_condicao.ToString()))
            //    SqlU.AppendLine(" And	a009_co_condicao in(22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 37, 38, 44, 45, 46, 47, 48, 49, 55, 56, 57, 58)");

            //if (_a009_co_condicao == 5 || _a009_co_condicao == 504) // || _a009_co_condicao == 22)
            //    SqlU.AppendLine(" And	a009_co_condicao = @v_a009_co_condicao ");
            //--------------------------------------------------


            //SqlU.AppendLine(" And	a009_co_condicao = @v_a009_co_condicao ");
            //SqlU.AppendLine(" And	r001_dt_entrada_vinculo = @v_r001_dt_entrada_vinculo ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a030_co_tipo_assistido", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a030_co_tipo_assistido));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a009_co_condicao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a009_co_condicao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_in_fundador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_in_fundador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_dt_entrada_vinculo", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_dt_entrada_vinculo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_dt_saida_vinculo", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_dt_saida_vinculo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_ds_cargo_direcao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_ds_cargo_direcao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_in_situacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_in_situacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_in_gerente_uso_firma", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_in_gerente_uso_firma));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_vl_participacao", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_vl_participacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_dt_inicio_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_dt_inicio_mandato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_dt_termino_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_dt_termino_mandato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_rep_legal", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_acao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_acao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_cpf_cnpj_pessoa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_cpf_cnpj_pessoa));

                
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
                if ((_t001_sq_pessoa < _t001_sq_pessoa_pai) && _a009_co_condicao < 500 )
                {
                    throw new Exception("Problema na conexão. Tente mais tarde (3.1.4) " + _t001_sq_pessoa.ToString() + " - " + _t001_sq_pessoa_pai.ToString());
                }

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

        public void UpdateCapital()
        {
            StringBuilder SqlU = new StringBuilder();
            
            SqlU.AppendLine(" Update  R001_Vinculo Set ");
            SqlU.AppendLine("		  r001_vl_participacao = @v_r001_vl_participacao ");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t001_sq_pessoa = @v_t001_sq_pessoa ");
            SqlU.AppendLine(" And	t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_vl_participacao", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_vl_participacao));

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

        public void UpdateTipoAcao()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Update  R001_Vinculo Set ");
            SqlU.AppendLine("		  r001_acao = @v_r001_acao ");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t001_sq_pessoa = @v_t001_sq_pessoa ");
            SqlU.AppendLine(" And	t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r001_acao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_acao));

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

        public void UpdateAdministrador()
        {
           
            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R001_Vinculo Set ");
            SqlU.AppendLine("		t001_sq_pessoa_rep_legal = @v_t001_sq_pessoa_rep_legal ");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t001_sq_pessoa = @v_t001_sq_pessoa ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_rep_legal", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_rep_legal));
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
        
        public void AtualizaDataBaixa()
        {
            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R001_Vinculo Set ");
            SqlU.AppendLine("		r001_dt_saida_vinculo= @v_r001_dt_saida_vinculo ");
            SqlU.AppendLine(" Where	1 = 1 ");

            SqlU.AppendLine(" And	t001_sq_pessoa = @v_t001_sq_pessoa ");
            SqlU.AppendLine(" And	t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");
            SqlU.AppendLine(" And	a009_co_condicao = @v_a009_co_condicao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a009_co_condicao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a009_co_condicao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_R001_DT_SAIDA_VINCULO", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_dt_saida_vinculo));
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


        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t001_sq_pessoa, ");
            Sql.AppendLine("		t001_sq_pessoa_pai, ");
            Sql.AppendLine("		a030_co_tipo_assistido, ");
            Sql.AppendLine("		a009_co_condicao, ");
            Sql.AppendLine("        r001_in_fundador, ");
            Sql.AppendLine("		r001_dt_entrada_vinculo, ");
            Sql.AppendLine("		r001_dt_saida_vinculo, ");
            Sql.AppendLine("		r001_ds_cargo_direcao, ");
            Sql.AppendLine("		r001_in_situacao, ");
            Sql.AppendLine("		r001_in_gerente_uso_firma, ");
            Sql.AppendLine("		r001_vl_participacao, ");
            Sql.AppendLine("		r001_dt_inicio_mandato, ");
            Sql.AppendLine("		r001_dt_termino_mandato, ");
            Sql.AppendLine("		t001_sq_pessoa_rep_legal");
            Sql.AppendLine(" From	R001_Vinculo");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	t001_sq_pessoa = _t001_sq_pessoa");
            Sql.AppendLine(" And	t001_sq_pessoa_pai = _t001_sq_pessoa_pai");
            Sql.AppendLine(" And	a030_co_tipo_assistido = _a030_co_tipo_assistido");
            Sql.AppendLine(" And	a009_co_condicao = _a009_co_condicao");
            Sql.AppendLine(" And    r001_in_fundador = _r001_in_fundador");
            Sql.AppendLine(" And	r001_dt_entrada_vinculo = _r001_dt_entrada_vinculo");
            Sql.AppendLine(" And	r001_dt_saida_vinculo = _r001_dt_saida_vinculo");
            Sql.AppendLine(" And	r001_ds_cargo_direcao = _r001_ds_cargo_direcao");
            Sql.AppendLine(" And	r001_in_situacao = _r001_in_situacao");
            Sql.AppendLine(" And	r001_in_gerente_uso_firma = _r001_in_gerente_uso_firma");
            Sql.AppendLine(" And	r001_vl_participacao = _r001_vl_participacao");
            Sql.AppendLine(" And	r001_dt_inicio_mandato = _r001_dt_inicio_mandato");
            Sql.AppendLine(" And	r001_dt_termino_mandato = _r001_dt_termino_mandato");
            Sql.AppendLine(" And	t001_sq_pessoa_rep_legal = _t001_sq_pessoa_rep_legal");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("R001_Vinculo");
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
        //Carrega Tabela com Dados do Requerente e dos Sócios
        public DataTable PreencheTabelaAuxiliar(String wPai)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("r001_vinculo");
            Sql.AppendLine("SELECT t001_sq_pessoa,a009_co_condicao,r001_in_fundador from r001_vinculo WHERE t001_sq_pessoa_pai = '" + wPai + "'");

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
                
                return toReturn;
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

        public void Deleta(int wsqPessoa, int wsqPessoaPai, int wsqQualificacao)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
           
            Sql.AppendLine(" From	R001_Vinculo");
            Sql.AppendLine(" Where	t001_sq_pessoa = " + wsqPessoa);
            if (wsqPessoaPai > 0)
            {
                Sql.AppendLine(" and    T001_SQ_PESSOA_PAI = " + wsqPessoaPai);
            }
            if (wsqQualificacao > 0)
            {
                Sql.AppendLine(" and    A009_CO_CONDICAO = " + wsqQualificacao);
            }


            //TODO: Implements Where Clause Here!!!! 
            

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
        
        public Int32 ContaPessoaVinculo(int wSqPessoa)
        {

            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("R001_Vinculo");
                Sql.AppendLine("SELECT COUNT(T001_SQ_PESSOA)as QtdPessoa FROM R001_VINCULO WHERE T001_SQ_PESSOA = " + wSqPessoa);

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);
                    return Int32.Parse(toReturn.Rows[0]["QtdPessoa"].ToString());
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public string RetornaSequenciaPessoaFundador(string SeqEmpresa, string cpf, out string squalificacao, out string snome)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("R001_Vinculo");
                Sql.AppendLine(@"   SELECT  v.T001_SQ_PESSOA As SQPessoa 
                                            ,v.A009_CO_CONDICAO AS QUALIFICACAO
                                            ,pe.T001_DS_PESSOA as NOME
                                        FROM r001_vinculo v
                                        INNER JOIN t002_pessoa_fisica p ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                        INNER JOIN t001_pessoa pe on pe.t001_sq_pessoa = p.t001_sq_pessoa
                                        WHERE
                                    p.T002_NR_CPF = '" + cpf + "'");
                Sql.AppendLine("   AND v.T001_SQ_PESSOA_PAI = " + SeqEmpresa);
                Sql.AppendLine("   AND (v.R001_DS_CARGO_DIRECAO LIKE '%FUNDADOR%'");
                Sql.AppendLine("   or v.R001_DS_CARGO_DIRECAO LIKE '%DIRETOR%'");
                Sql.AppendLine("   or v.R001_DS_CARGO_DIRECAO LIKE '%PRESIDENTE%'");

                Sql.AppendLine("        OR v.R001_DS_CARGO_DIRECAO LIKE '%ADMINISTRADOR%')");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["SQPessoa"].ToString() != "")
                        {
                            squalificacao = toReturn.Rows[0]["QUALIFICACAO"].ToString();
                            snome = toReturn.Rows[0]["NOME"].ToString();
                            return toReturn.Rows[0]["SQPessoa"].ToString();
                        }
                    squalificacao = string.Empty;
                    snome = string.Empty;
                    return "0";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }

        }


        public string RetornaSequenciaPessoa(string SeqEmpresa, string cpf, out string squalificacao, out string snome)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("R001_Vinculo");
                Sql.AppendLine(@"   SELECT  v.T001_SQ_PESSOA As SQPessoa 
                                            ,v.A009_CO_CONDICAO AS QUALIFICACAO
                                            ,pe.T001_DS_PESSOA as NOME
                                        FROM r001_vinculo v
                                        INNER JOIN t002_pessoa_fisica p ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                        INNER JOIN t001_pessoa pe on pe.t001_sq_pessoa = p.t001_sq_pessoa
                                        WHERE
                                    p.T002_NR_CPF = '" + cpf + "'");
                Sql.AppendLine("   AND v.T001_SQ_PESSOA_PAI = " + SeqEmpresa);
                Sql.AppendLine("   AND (v.R001_DS_CARGO_DIRECAO LIKE '%SÓCIO%'");
                Sql.AppendLine("        OR v.R001_DS_CARGO_DIRECAO LIKE '%ADMINISTRADOR%'");
                Sql.AppendLine("        OR v.R001_DS_CARGO_DIRECAO LIKE '%TITULAR%')");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["SQPessoa"].ToString() != "")
                        {
                            squalificacao = toReturn.Rows[0]["QUALIFICACAO"].ToString();
                            snome = toReturn.Rows[0]["NOME"].ToString();
                            return toReturn.Rows[0]["SQPessoa"].ToString();
                        }
                    squalificacao = string.Empty;
                    snome = string.Empty;
                    return "0";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }

        }
        public string RetornaSequenciaPessoaRepresentanteFundador(string SeqEmpresa, string cpf)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("R001_Vinculo");
                Sql.AppendLine(@"   SELECT v.T001_SQ_PESSOA AS SQPessoa
                                    FROM
                                      r001_vinculo v
                                    INNER JOIN t002_pessoa_fisica p
                                    ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                    WHERE
                                      v.T001_SQ_PESSOA_PAI in(

                                    SELECT v.T001_SQ_PESSOA 
                                    FROM r001_vinculo v
                                    INNER JOIN t002_pessoa_fisica p ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                    WHERE");
                                    
                Sql.AppendLine("   v.T001_SQ_PESSOA_PAI = " + SeqEmpresa);
                Sql.AppendLine("   AND v.T001_SQ_PESSOA_PAI = " + SeqEmpresa);
                Sql.AppendLine("   AND (v.R001_DS_CARGO_DIRECAO LIKE '%FUNDADOR%'");
                Sql.AppendLine("   or v.R001_DS_CARGO_DIRECAO LIKE '%DIRETOR%'");
                Sql.AppendLine("   or v.R001_DS_CARGO_DIRECAO LIKE '%PRESIDENTE%'");
                Sql.AppendLine("       AND v.R001_DS_CARGO_DIRECAO LIKE '%ADMINISTRADOR%'))");
                Sql.AppendLine("   and p.T002_NR_CPF = '" + cpf + "'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["SQPessoa"].ToString() != "")
                            return toReturn.Rows[0]["SQPessoa"].ToString();
                    return "0";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }

        }

        public string RetornaSequenciaPessoaRepresentante(string SeqEmpresa, string cpf)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("R001_Vinculo");
                Sql.AppendLine(@"   SELECT v.T001_SQ_PESSOA AS SQPessoa
                                    FROM
                                      r001_vinculo v
                                    INNER JOIN t002_pessoa_fisica p
                                    ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                    WHERE
                                      v.T001_SQ_PESSOA_PAI in(

                                    SELECT v.T001_SQ_PESSOA 
                                    FROM r001_vinculo v
                                    INNER JOIN t002_pessoa_fisica p ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                    WHERE");

                Sql.AppendLine("   v.T001_SQ_PESSOA_PAI = " + SeqEmpresa);
                Sql.AppendLine("   AND (v.R001_DS_CARGO_DIRECAO LIKE '%SÓCIO%'");
                Sql.AppendLine("        OR v.R001_DS_CARGO_DIRECAO LIKE '%ADMINISTRADOR%'");
                Sql.AppendLine("        OR v.R001_DS_CARGO_DIRECAO LIKE '%TITULAR%'))");
                Sql.AppendLine("   and p.T002_NR_CPF = '" + cpf + "'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["SQPessoa"].ToString() != "")
                            return toReturn.Rows[0]["SQPessoa"].ToString();
                    return "0";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }

        }

        public int VerificaSeDuplicoQSA(int wSqPessoaEmpresa)
        {
            /*
             * Verifica se o mesmo Socio esta duplicado na tabela de vinculo
             */
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
//            Sql.AppendLine(@"   SELECT  count(*) Qtd, v.T001_SQ_PESSOA_PAI, pf.T002_NR_CPF
//                                FROM    r001_vinculo v, t002_pessoa_fisica pf, t003_pessoa_juridica pj
//                                WHERE   1 = 1
//                                AND     v.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
//                                AND     v.T001_SQ_PESSOA_PAI = " + wSqPessoaEmpresa + @"
//                                AND     v.T001_SQ_PESSOA_PAI = pj.T001_SQ_PESSOA
//                                AND     v.A009_CO_CONDICAO IN ( SELECT  r.A009_CO_CONDICAO
//                                                                FROM   t008_tipo_natureza_juridica r
//                                                                WHERE r.A006_CO_NATUREZA_JURIDICA = pj.A006_CO_NATUREZA_JURIDICA
//                                                              )
//                                GROUP BY  v.T001_SQ_PESSOA_PAI, pf.T002_NR_CPF
//                                HAVING  count(*) > 1");



            Sql.AppendLine(@"   SELECT  count(*) Qtd, v.T001_SQ_PESSOA_PAI, pf.T002_NR_CPF
                                FROM    r001_vinculo v, t002_pessoa_fisica pf, t003_pessoa_juridica pj
                                WHERE   1 = 1
                                AND     v.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
                                AND     v.T001_SQ_PESSOA_PAI = " + wSqPessoaEmpresa + @"
                                AND     v.T001_SQ_PESSOA_PAI = pj.T001_SQ_PESSOA
                                GROUP BY  v.T001_SQ_PESSOA_PAI, pf.T002_NR_CPF, v.A009_CO_CONDICAO
                                HAVING  count(*) > 1");


            cmdToExecute.CommandText = Sql.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("ContaSociosNoVinculo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
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
                adapter.Fill(toReturn);

                return toReturn.Rows.Count;
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

        public int VerificaDBEDuplicado(string pRequerimento, string pDBE)
        {
            /*
             * Verifica se o DBE está em outro requerimento
             */
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(@"   SELECT count(*) as qtd
                                FROM
                                  t005_protocolo p
                                WHERE
                                  p.T005_NR_DBE = '" + pDBE + "'");
            Sql.AppendLine("       AND p.T005_NR_PROTOCOLO <> '" + pRequerimento + "'");
            Sql.AppendLine("       AND fnGetStatusProcesso(p.t005_nr_protocolo) <> 9");


            cmdToExecute.CommandText = Sql.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("ContaDBEDuplicado");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
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
                adapter.Fill(toReturn);

                return Int32.Parse(toReturn.Rows[0][0].ToString());
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

        public Int32 ContaSociosNoVinculo(int wSqPessoaEmpresa)
        {

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@"SELECT count(*) AS qtd
                            FROM
                              r001_vinculo v
                            INNER JOIN t003_pessoa_juridica pj
                            ON pj.T001_SQ_PESSOA = v.T001_SQ_PESSOA_PAI
                            INNER JOIN t002_pessoa_fisica pf
                            On v.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
                            INNER JOIN r002_vinculo_endereco ve
                            ON ve.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
                            WHERE
                              v.T001_SQ_PESSOA_PAI = " + wSqPessoaEmpresa);
            Sql.AppendLine(@"  AND v.A009_CO_CONDICAO
                              IN (
                              SELECT r.A009_CO_CONDICAO
                              FROM
                                t008_tipo_natureza_juridica r
                              WHERE
                                r.A006_CO_NATUREZA_JURIDICA = pj.A006_CO_NATUREZA_JURIDICA)
                              AND v.A009_CO_CONDICAO != 5 ");

            cmdToExecute.CommandText = Sql.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("ContaSociosNoVinculo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
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
                adapter.Fill(toReturn);

                return Int32.Parse(toReturn.Rows[0][0].ToString());
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

        public void ExcluiVinculo(string sqPessoa)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "ExcluiVinculo";
            cmdToExecute.CommandType = CommandType.StoredProcedure;


            _mainConnection.Open();

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("vsqpessoa", MySqlDbType.Int32, 20, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, sqPessoa));

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

            }
        }
        #endregion

        #region Sequencia Filial
        public void UpdateSeqFilial()
        {
            StringBuilder SqlU = new StringBuilder();


            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R001_Vinculo Set ");
            SqlU.AppendLine("		R001_ORDEM_FILIAL_CONTRATO = @v_R001_ORDEM_FILIAL_CONTRATO ");
            SqlU.AppendLine(" Where	t001_sq_pessoa = @v_t001_sq_pessoa ");
            SqlU.AppendLine(" And	t001_sq_pessoa_pai = @v_t001_sq_pessoa_pai ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_pai", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa_pai));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_R001_ORDEM_FILIAL_CONTRATO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r001_ordem_filial_contrato));

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
                    throw new Exception("Não atualizou a filial.");
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
        #endregion
    }
}


