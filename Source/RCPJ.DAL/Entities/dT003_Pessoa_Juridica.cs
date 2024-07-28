using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace RCPJ.DAL.Entities
{
    public class dT003_Pessoa_Juridica : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _t003_DBE;
        protected string _t003_prot_viab;
        protected decimal _t001_sq_pessoa;
        protected string _t004_nr_cnpj_org_reg;
        protected string _t003_nr_matricula;
        protected string _t003_nr_cnpj;
        protected string _t003_nr_inscricao_estadual;
        protected string _t003_nr_inscricao_municipal;
        protected string _t006_nr_cnpj_org_reg_ant;
        protected string _t003_nr_matricula_anterior;
        protected decimal _a006_co_natureza_juridica;
        protected decimal _a011_co_porte;
        protected decimal _a007_co_situacao;
        protected decimal _a008_co_status_atual;
        protected decimal _t003_vl_capital;
        //Incluido por Flavio
        protected decimal _t003_vl_capital_social;
        protected decimal _t003_vl_valor_cota;
        protected decimal _t003_vl_qtd_cotas;
        protected int _t003_dia_fim_exercicio;
        protected string _t003_mes_fim_exercicio;
        protected string _t003_distribuicao_proporcional;
        protected string _t003_tipo_sociedade;
        protected int _t003_tipo_enquadramento;
        protected string _t003_socios_obrigacoes_sociais;
        //Fim
        protected decimal _t003_vl_capital_integralizado;
        protected decimal _t003_vl_capital_nao_integralizado;
        protected Nullable<DateTime> _t003_data_limite_integralizacao;
        protected string _t003_ds_capital_nao_integralizado;
        protected string _t003_moeda_corrente;

        protected decimal _t003_co_tipo_pes_jur;
        protected decimal _t003_co_forma_atuacao;
        protected Nullable<DateTime> _t003_dt_constituicao;
        protected Nullable<DateTime> _t003_dt_inicio_atividade;
        protected Nullable<DateTime> _t003_dt_termino_ativ;
        protected Nullable<DateTime> _t003_dt_prazo_determinado;
        protected string _t003_ds_objeto_social;
        private int _T003_IND_FILIAL_SEDE_FORA;
        private int _T003_IND_CNAE_DESTACADA;
        private int _T003_IND_UNIPESSOAL;

        private string _t003_ds_reducao_capital;
        private int _t003_in_reducao_capital = 2;
        private string _T003_DS_SITUACAO = "";
        private string _t003_uf_origem = "";
        private int _t003_ind_integralizando_cap = 2;
        private int _t003_ind_spe = 2;
        private int _t003_ind_matriz = 1;
        private int _t003_in_end_estab = 1;
        private string _t003_iptu = "";
        private decimal _t003_area_utilizada = 0;

        #endregion

        #region Class Member Declarations
        public String t003_socios_obrigacoes_sociais
        {
            get { return _t003_socios_obrigacoes_sociais; }
            set { _t003_socios_obrigacoes_sociais = value; }
        }
        public string t003_ds_capital_nao_integralizado
        {
            get { return _t003_ds_capital_nao_integralizado; }
            set { _t003_ds_capital_nao_integralizado = value; }
        }
        public string t003_DBE
        {
            get { return _t003_DBE; }
            set { _t003_DBE = value; }
        }
        public string t003_prot_viab
        {
            get { return _t003_prot_viab; }
            set { _t003_prot_viab = value; }
        }
        public int t003_tipo_enquadramento
        {
            get { return _t003_tipo_enquadramento; }
            set { _t003_tipo_enquadramento = value; }
        }
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }

            set { _t001_sq_pessoa = value; }
        }

        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string t003_nr_matricula
        {
            get { return _t003_nr_matricula; }

            set { _t003_nr_matricula = value; }
        }

        public string t003_nr_cnpj
        {
            get { return _t003_nr_cnpj; }

            set { _t003_nr_cnpj = value; }
        }

        public string t003_nr_inscricao_estadual
        {
            get { return _t003_nr_inscricao_estadual; }

            set { _t003_nr_inscricao_estadual = value; }
        }

        public string t003_nr_inscricao_municipal
        {
            get { return _t003_nr_inscricao_municipal; }

            set { _t003_nr_inscricao_municipal = value; }
        }

        public string t006_nr_cnpj_org_reg_ant
        {
            get { return _t006_nr_cnpj_org_reg_ant; }

            set { _t006_nr_cnpj_org_reg_ant = value; }
        }

        public string t003_nr_matricula_anterior
        {
            get { return _t003_nr_matricula_anterior; }

            set { _t003_nr_matricula_anterior = value; }
        }

        public decimal a006_co_natureza_juridica
        {
            get { return _a006_co_natureza_juridica; }

            set { _a006_co_natureza_juridica = value; }
        }

        public decimal a011_co_porte
        {
            get { return _a011_co_porte; }

            set { _a011_co_porte = value; }
        }

        public decimal a007_co_situacao
        {
            get { return _a007_co_situacao; }

            set { _a007_co_situacao = value; }
        }

        public decimal a008_co_status_atual
        {
            get { return _a008_co_status_atual; }

            set { _a008_co_status_atual = value; }
        }

        public decimal t003_vl_capital
        {
            get { return _t003_vl_capital; }

            set { _t003_vl_capital = value; }
        }
        //Incluido por Flavio
        public decimal t003_vl_capital_social
        {
            get { return _t003_vl_capital_social; }
            set { _t003_vl_capital_social = value; }
        }
        public decimal t003_vl_valor_cota
        {
            get { return _t003_vl_valor_cota; }
            set { _t003_vl_valor_cota = value; }
        }
        public decimal t003_vl_qtd_cotas
        {
            get { return _t003_vl_qtd_cotas; }
            set { _t003_vl_qtd_cotas = value; }
        }
        public string t003_moeda_corrente
        {
            get { return _t003_moeda_corrente; }
            set { _t003_moeda_corrente = value; }
        }
        public int t003_dia_fim_exercicio
        {
            get { return _t003_dia_fim_exercicio; }
            set { _t003_dia_fim_exercicio = value; }
        }
        public string t003_mes_fim_exercicio
        {
            get { return _t003_mes_fim_exercicio; }
            set { _t003_mes_fim_exercicio = value; }
        }
        public string t003_distribuicao_proporcional
        {
            get { return _t003_distribuicao_proporcional; }
            set { _t003_distribuicao_proporcional = value; }
        }
        public string t003_tipo_sociedade
        {
            get { return _t003_tipo_sociedade; }
            set { _t003_tipo_sociedade = value; }
        }
        // Fim da Inclusão
        public decimal t003_vl_capital_integralizado
        {
            get { return _t003_vl_capital_integralizado; }

            set { _t003_vl_capital_integralizado = value; }
        }
        public decimal t003_vl_capital_nao_integralizado
        {
            get { return _t003_vl_capital_nao_integralizado; }
            set { _t003_vl_capital_nao_integralizado = value; }
        }
        public Nullable<DateTime> t003_data_limite_integralizacao
        {
            get { return _t003_data_limite_integralizacao; }
            set { _t003_data_limite_integralizacao = value; }
        }

        public decimal t003_co_tipo_pes_jur
        {
            get { return _t003_co_tipo_pes_jur; }

            set { _t003_co_tipo_pes_jur = value; }
        }

        public decimal t003_co_forma_atuacao
        {
            get { return _t003_co_forma_atuacao; }

            set { _t003_co_forma_atuacao = value; }
        }

        public Nullable<DateTime> t003_dt_constituicao
        {
            get { return _t003_dt_constituicao; }

            set { _t003_dt_constituicao = value; }
        }

        public Nullable<DateTime> t003_dt_inicio_atividade
        {
            get { return _t003_dt_inicio_atividade; }

            set { _t003_dt_inicio_atividade = value; }
        }

        public Nullable<DateTime> t003_dt_termino_ativ
        {
            get { return _t003_dt_termino_ativ; }

            set { _t003_dt_termino_ativ = value; }
        }

        public Nullable<DateTime> t003_dt_prazo_determinado
        {
            get { return _t003_dt_prazo_determinado; }

            set { _t003_dt_prazo_determinado = value; }
        }

        public string t003_ds_objeto_social
        {
            get { return _t003_ds_objeto_social; }

            set { _t003_ds_objeto_social = value; }
        }
        public int T003_IND_FILIAL_SEDE_FORA
        {
            get { return _T003_IND_FILIAL_SEDE_FORA; }
            set { _T003_IND_FILIAL_SEDE_FORA = value; }
        }
        public int T003_IND_CNAE_DESTACADA
        {
            get { return _T003_IND_CNAE_DESTACADA; }
            set { _T003_IND_CNAE_DESTACADA = value; }
        }
        public int T003_IND_UNIPESSOAL
        {
            get { return _T003_IND_UNIPESSOAL; }
            set { _T003_IND_UNIPESSOAL = value; }
        }
        public string T003_ds_reducao_capital
        {
            get { return _t003_ds_reducao_capital; }
            set { _t003_ds_reducao_capital = value; }
        }

        public int T003_in_reducao_capital
        {
            get { return _t003_in_reducao_capital; }
            set { _t003_in_reducao_capital = value; }
        }

        public string T003_DS_SITUACAO
        {
            get { return _T003_DS_SITUACAO; }
            set { _T003_DS_SITUACAO = value; }
        }
        public string T003_uf_origem
        {
            get { return _t003_uf_origem; }
            set { _t003_uf_origem = value; }
        }
        public int T003_ind_integralizando_cap
        {
            get { return _t003_ind_integralizando_cap; }
            set { _t003_ind_integralizando_cap = value; }
        }
        public int t003_ind_spe
        {
            get { return _t003_ind_spe; }
            set { _t003_ind_spe = value; }
        }
        public int T003_ind_matriz
        {
            get { return _t003_ind_matriz; }
            set { _t003_ind_matriz = value; }
        }
        public int T003_in_end_estab
        {
            get { return _t003_in_end_estab; }
            set { _t003_in_end_estab = value; }
        }
        public string T003_iptu
        {
            get { return _t003_iptu; }
            set { _t003_iptu = value; }
        }

        public decimal T003_area_utilizada
        {
            get { return _t003_area_utilizada; }
            set { _t003_area_utilizada = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(@" Insert into t003_pessoa_juridica
                              (
            	                t001_sq_pessoa, 
            	                t004_nr_cnpj_org_reg, 
            	                t003_nr_matricula, 
            	                t003_nr_cnpj, 
            	                t003_nr_inscricao_estadual, 
            	                t003_nr_inscricao_municipal, 
            	                t006_nr_cnpj_org_reg_ant, 
            	                t003_nr_matricula_anterior, 
            	                a006_co_natureza_juridica, 
            	                a011_co_porte, 
            	                a007_co_situacao, 
            	                a008_co_status_atual, 
            	                t003_vl_capital, 
                               t003_vl_capital_social, 
                               t003_vl_valor_cota, 
                               t003_vl_qtd_cotas, 
                               t003_tipo_enquadramento, 
                               t003_DBE, 
                               t003_prot_viab, 
                               t003_socios_obrigacoes_sociais, 
            	                t003_vl_capital_integralizado, 
                               t003_vl_capital_nao_integralizado, 
                               t003_data_limite_integralizacao, 
                               t003_ds_capital_nao_integralizado, 
                               t003_moeda_corrente, 
            	                t003_co_tipo_pes_jur, 
            	                t003_co_forma_atuacao, 
            	                t003_dt_constituicao, 
            	                t003_dt_inicio_atividade, 
            	                t003_dt_termino_ativ, 
            	                t003_dt_prazo_determinado, 
            	                t003_ds_objeto_social,
            	                T003_IND_FILIAL_SEDE_FORA,
            	                T003_IND_CNAE_DESTACADA,
            	                T003_IND_UNIPESSOAL,
            	                T003_IN_REDUCAO_CAPITAL,
            	                T003_DS_REDUCAO_CAPITAL,
            	                T003_DS_SITUACAO,
            	                T003_UF_ORIGEM,
            	                T003_IND_INTEGRALIZANDO_CAP,
            	                T003_IND_SPE,
            	                T003_IND_MATRIZ,
            	                T003_IN_END_ESTAB,
                                T003_iptu,
                                T003_AREA_UTILIZADA
                              )
                             Values 
                              (
            	                @v_t001_sq_pessoa, 
            	                @v_t004_nr_cnpj_org_reg, 
            	                @v_t003_nr_matricula, 
            	                @v_t003_nr_cnpj, 
            	                @v_t003_nr_inscricao_estadual, 
            	                @v_t003_nr_inscricao_municipal, 
            	                @v_t006_nr_cnpj_org_reg_ant, 
            	                @v_t003_nr_matricula_anterior, 
            	                @v_a006_co_natureza_juridica, 
            	                @v_a011_co_porte, 
            	                @v_a007_co_situacao, 
            	                @v_a008_co_status_atual, 
            	                @v_t003_vl_capital, 
                               @v_t003_vl_capital_social, 
                               @v_t003_vl_valor_cota, 
                               @v_t003_vl_qtd_cotas, 
                               @v_t003_tipo_enquadramento, 
                               @v_t003_DBE, 
                               @v_t003_prot_viab, 
                               @v_t003_socios_obrigacoes_sociais,
            	                @v_t003_vl_capital_integralizado, 
                               @v_t003_vl_capital_nao_integralizado, 
                               evaldate(@v_t003_data_limite_integralizacao), 
                               @v_t003_ds_capital_nao_integralizado, 
                               @v_t003_moeda_corrente, 
            	                @v_t003_co_tipo_pes_jur, 
            	                @v_t003_co_forma_atuacao, 
            	                evaldate(@v_t003_dt_constituicao), 
            	                evaldate(@v_t003_dt_inicio_atividade), 
            	                evaldate(@v_t003_dt_termino_ativ), 
            	                evaldate(@v_t003_dt_prazo_determinado), 
            	                @v_t003_ds_objeto_social,
            	                @v_T003_IND_FILIAL_SEDE_FORA,
            	                @v_T003_IND_CNAE_DESTACADA,
            	                @v_T003_IND_UNIPESSOAL,
            	                @v_t003_in_reducao_capital,
            	                @v_t003_ds_reducao_capital,
            	                @v_T003_DS_SITUACAO,
            	                @V_T003_UF_ORIGEM,
            	                @V_t003_ind_integralizando_cap,
            	                @V_t003_ind_spe,
            	                @V_t003_ind_matriz,
                                @V_T003_IN_END_ESTAB,
                                @V_T003_iptu,
                                @V_T003_area_utilizada
                              )");



            // Codigo Update ******************* 
            SqlU.AppendLine(@" Update   T003_Pessoa_Juridica Set 
                                        t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, 
                                        t003_nr_matricula = @v_t003_nr_matricula, 
                                        t003_nr_cnpj = @v_t003_nr_cnpj, 
                                        t003_nr_inscricao_estadual = @v_t003_nr_inscricao_estadual, 
                                        t003_nr_inscricao_municipal = @v_t003_nr_inscricao_municipal, 
                                        t006_nr_cnpj_org_reg_ant = @v_t006_nr_cnpj_org_reg_ant, 
                                        t003_nr_matricula_anterior = @v_t003_nr_matricula_anterior, 
                                        a006_co_natureza_juridica = @v_a006_co_natureza_juridica, 
                                        a011_co_porte = @v_a011_co_porte, 
                                        a007_co_situacao = @v_a007_co_situacao, 
                                        a008_co_status_atual = @v_a008_co_status_atual, 
                                        t003_vl_capital = @v_t003_vl_capital, 
                                        t003_vl_capital_social = @v_t003_vl_capital_social, 
                                        t003_vl_valor_cota = @v_t003_vl_valor_cota, 
                                        t003_vl_qtd_cotas = @v_t003_vl_qtd_cotas, 
                                        t003_tipo_enquadramento = @v_t003_tipo_enquadramento, 
                                        t003_DBE = @v_t003_DBE, 
                                        t003_prot_viab = @v_t003_prot_viab, 
                                        t003_socios_obrigacoes_sociais = @v_t003_socios_obrigacoes_sociais, 
                                        t003_vl_capital_integralizado = @v_t003_vl_capital_integralizado, 
                                        t003_vl_capital_nao_integralizado = @v_t003_vl_capital_nao_integralizado, 
                                        t003_data_limite_integralizacao = evaldate(@v_t003_data_limite_integralizacao), 
                                        t003_ds_capital_nao_integralizado = @v_t003_ds_capital_nao_integralizado, 
                                        t003_moeda_corrente = @v_t003_moeda_corrente, 
                                        t003_co_tipo_pes_jur = @v_t003_co_tipo_pes_jur, 
                                        t003_co_forma_atuacao = @v_t003_co_forma_atuacao, 
                                        t003_dt_constituicao = evaldate(@v_t003_dt_constituicao), 
                                        t003_dt_inicio_atividade = evaldate(@v_t003_dt_inicio_atividade), 
                                        t003_dt_termino_ativ = evaldate(@v_t003_dt_termino_ativ), 
                                        t003_dt_prazo_determinado = evaldate(@v_t003_dt_prazo_determinado), 
                                        t003_ds_objeto_social = @v_t003_ds_objeto_social,
                                        T003_IND_FILIAL_SEDE_FORA = @v_T003_IND_FILIAL_SEDE_FORA,
                                        T003_IND_CNAE_DESTACADA = @v_T003_IND_CNAE_DESTACADA,
                                        T003_IND_UNIPESSOAL = @v_T003_IND_UNIPESSOAL,
                                        T003_IN_REDUCAO_CAPITAL = @v_t003_in_reducao_capital,
                                        T003_DS_REDUCAO_CAPITAL = @v_t003_ds_reducao_capital,
                                        T003_DS_SITUACAO = @v_T003_DS_SITUACAO,
                                        T003_UF_ORIGEM = @V_T003_UF_ORIGEM,
                                        T003_IND_INTEGRALIZANDO_CAP = @V_t003_ind_integralizando_cap,
                                        T003_IND_SPE = @V_t003_ind_spe,
                                        T003_IND_MATRIZ = @V_t003_ind_matriz,
                                        T003_IN_END_ESTAB = @V_T003_IN_END_ESTAB,
                                        T003_iptu = @V_T003_iptu,
                                        T003_area_utilizada = @V_t003_area_utilizada");
            
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_nr_matricula", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_nr_matricula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_nr_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_nr_cnpj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_nr_inscricao_estadual", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_nr_inscricao_estadual));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_nr_inscricao_municipal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_nr_inscricao_municipal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cnpj_org_reg_ant", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cnpj_org_reg_ant));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_nr_matricula_anterior", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_nr_matricula_anterior));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a006_co_natureza_juridica", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a006_co_natureza_juridica));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a011_co_porte", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a011_co_porte));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a007_co_situacao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a007_co_situacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a008_co_status_atual", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a008_co_status_atual));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital));
                //Incluido por Flavio
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital_social", MySqlDbType.Decimal, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_valor_cota", MySqlDbType.Decimal, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_valor_cota));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_qtd_cotas", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_qtd_cotas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_tipo_enquadramento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_tipo_enquadramento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_DBE", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_DBE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_prot_viab", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_prot_viab));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_socios_obrigacoes_sociais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_socios_obrigacoes_sociais));
                //Fim
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital_integralizado", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital_nao_integralizado", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital_nao_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_data_limite_integralizacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_data_limite_integralizacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ds_capital_nao_integralizado", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ds_capital_nao_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_moeda_corrente", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_moeda_corrente));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_co_tipo_pes_jur", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_co_tipo_pes_jur));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_co_forma_atuacao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_co_forma_atuacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_dt_constituicao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_dt_constituicao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_dt_inicio_atividade", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_dt_inicio_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_dt_termino_ativ", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_dt_termino_ativ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_dt_prazo_determinado", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_dt_prazo_determinado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ds_objeto_social", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ds_objeto_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IND_FILIAL_SEDE_FORA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T003_IND_FILIAL_SEDE_FORA));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IND_CNAE_DESTACADA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T003_IND_CNAE_DESTACADA));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IND_UNIPESSOAL", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T003_IND_UNIPESSOAL));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IN_REDUCAO_CAPITAL", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_in_reducao_capital));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_DS_REDUCAO_CAPITAL", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ds_reducao_capital));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_DS_SITUACAO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T003_DS_SITUACAO));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_T003_UF_ORIGEM", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_uf_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ind_integralizando_cap", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ind_integralizando_cap));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ind_spe", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ind_spe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ind_matriz", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ind_matriz));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IN_END_ESTAB", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_in_end_estab));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_iptu", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_iptu));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_area_utilizada", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_area_utilizada));
                

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

        public void UpdateObjetoSocial()
        {
            
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(@" Update t003_pessoa_juridica 
                                set t003_ds_objeto_social = @v_t003_ds_objeto_social
                               Where	t001_sq_pessoa = @v_t001_sq_pessoa");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_ds_objeto_social", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_ds_objeto_social));
               

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

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	T003_Pessoa_Juridica");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	t001_sq_pessoa = " + _t001_sq_pessoa);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T003_Pessoa_Juridica");
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

        public DataTable QueryXmlWs(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" SELECT  ");
            Sql.AppendLine("        IFNULL (p.T001_DS_PESSOA,'') as Nome ,  ");
            Sql.AppendLine("        '' as EMAIL, ");
            Sql.AppendLine("        IFNULL (pj.T003_NR_MATRICULA,'') as Matricula ,  ");
            Sql.AppendLine("        IFNULL (pj.T003_NR_CNPJ,'') as CNPJ ,  ");
            Sql.AppendLine("        IFNULL (pj.T003_NR_INSCRICAO_ESTADUAL,'') as NRO_IE ,  ");
            Sql.AppendLine(" convert (IFNULL (pj.A006_CO_NATUREZA_JURIDICA,''), Char(8)) as Natureza ,  ");
            Sql.AppendLine(" (select n.A006_DS_NATUREZA_JURIDICA from a006_natureza_juridica n where n.A006_CO_NATUREZA_JURIDICA = pj.A006_CO_NATUREZA_JURIDICA ) as DescricaoNatureza,   ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_CO_TIPO_PES_JUR,''), char(250)) as TipoDePessoaJuridica ,    ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_DT_CONSTITUICAO,'') , char(250)) as DTA_REGISTRO ,    ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_DT_CONSTITUICAO,'') , char(250)) as DataDeContituicao ,    ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_DT_INICIO_ATIVIDADE,'') , char(250)) as DataInicioAtividade ,    ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_DT_TERMINO_ATIV,''), char(250)) as DTA_TERMINO_ATIVIDADE ,    ");
            Sql.AppendLine(" convert (IFNULL (pj.T003_VL_CAPITAL,''), char(250)) as VAL_CAPITAL_NOMINAL ,    ");
            //Incluido por Flavio
            //Sql.AppendLine(" convert (IFNULL (pj.T003_VL_CAPITAL_SOCIAL,''), char(250)) as VAL_CAPITAL_SOCIAL ,    ");
            //Sql.AppendLine(" convert (IFNULL (pj.T003_VL_NO_COTAS,''), char(250)) as VAL_NO_COTAS ,    ");
            //Fim
            Sql.AppendLine(" convert (IFNULL (pj.T003_VL_CAPITAL_INTEGRALIZADO,''), char(250)) as VAL_CAPITAL_INTEGRADO ,    ");
            Sql.AppendLine("            IFNULL (pj.T003_DS_OBJETO_SOCIAL,'') as ObjetoSocial,  ");
            Sql.AppendLine(" convert (IFNULL (e.A015_CO_TIPO_LOGRADOURO,''), char(250)) as cod_logradouro , ");
            Sql.AppendLine("        IFNULL (e.R002_DS_LOGRADOURO,'') as Logradoro ,  ");
            Sql.AppendLine(" convert ( IFNULL (e.R002_NR_LOGRADOURO,''), char(250)) as NRO_ENDERECO ,  ");
            Sql.AppendLine("        IFNULL (e.R002_DS_COMPLEMENTO,'') as Complemento ,  ");
            Sql.AppendLine("        IFNULL (e.R002_DS_BAIRRO,'') as Bairro ,   ");
            Sql.AppendLine(" convert (IFNULL (e.A005_CO_MUNICIPIO,''), char(250)) as MUNICIPIO, ");
            Sql.AppendLine("        IFNULL (e.R002_NR_CEP,'') as Cep ,  ");
            Sql.AppendLine("        (select m.TMU_TUF_UF from tab_munic m where m.TMU_COD_MUN = e.A005_CO_MUNICIPIO) as UF ,  ");
            Sql.AppendLine(" convert ( IFNULL (e.A004_CO_PAIS,''), char(250)) as COD_PAIS ");
            Sql.AppendLine(" FROM   T003_PESSOA_JURIDICA pj, ");
            Sql.AppendLine("        T001_PESSOA p,");
            Sql.AppendLine("        T005_PROTOCOLO prot,");
            Sql.AppendLine("        R002_VINCULO_ENDERECO e");
            Sql.AppendLine(" where  1 = 1");
            Sql.AppendLine(" and  prot.T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            Sql.AppendLine(" and    prot.T001_SQ_PESSOA = p.T001_SQ_PESSOA");
            Sql.AppendLine(" and    p.T001_SQ_PESSOA = pj.T001_SQ_PESSOA");
            Sql.AppendLine(" and    e.T001_SQ_PESSOA = pj.T001_SQ_PESSOA");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Empresa");
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

        public DataTable QueryVinculoXmlWs(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select  ");
            Sql.AppendLine(" pf.T002_NR_CPF as CNPJ,    ");
            Sql.AppendLine(" p.T001_DS_PESSOA as Nome,    ");
            Sql.AppendLine(" pf.T002_IN_SEXO as sexo,    ");
            Sql.AppendLine(" pf.T002_DT_NASCIMENTO as DataNacimento,    ");
            Sql.AppendLine(" pf.T002_NR_DOCUMENTO as IDENTIDADE,    ");
            Sql.AppendLine(" pf.T002_DS_EMISSOR_DOCUMENTO as OrgaoEmissor,    ");
            Sql.AppendLine(" null as UFEMISOR,    ");
            Sql.AppendLine(" pf.A004_CO_PAIS as PAIS,    ");
            Sql.AppendLine(" e.A015_CO_TIPO_LOGRADOURO as codLogradouro,    ");
            Sql.AppendLine(" e.R002_DS_LOGRADOURO as LOGRADOURO,    ");
            Sql.AppendLine(" e.R002_NR_LOGRADOURO,    ");
            Sql.AppendLine(" e.R002_DS_COMPLEMENTO as COMPLEMENTO,    ");
            Sql.AppendLine(" e.R002_DS_BAIRRO as BAIRRO,    ");
            Sql.AppendLine(" e.R002_NR_CEP as CEP,    ");
            Sql.AppendLine(" 'RJ' as UF,    ");
            Sql.AppendLine(" null as NOMEPAI,    ");
            Sql.AppendLine(" null as NOMEMAE   ");
            Sql.AppendLine(" FROM  t005_protocolo protocolo, T003_PESSOA_JURIDICA pj     ");
            Sql.AppendLine(" inner join R001_VINCULO v           on v.T001_SQ_PESSOA_PAI = pj.T001_SQ_PESSOA    ");
            Sql.AppendLine(" inner join T001_PESSOA p            on v.T001_SQ_PESSOA = p.T001_SQ_PESSOA    ");
            Sql.AppendLine(" inner join T002_PESSOA_FISICA pf    on v.T001_SQ_PESSOA = pf.T001_SQ_PESSOA    ");
            Sql.AppendLine(" left join  R002_VINCULO_ENDERECO e  on e.T001_SQ_PESSOA = v.T001_SQ_PESSOA   ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  protocolo.T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            Sql.AppendLine(" and  protocolo.T004_NR_CNPJ_ORG_REG = pj.T004_NR_CNPJ_ORG_REG  ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("SOCIO");
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

        public DataTable QuerySociedade(String Matricula,
                                        String NomeSociedade,
                                        String Protocolo,
                                        String CNAE)
        {

            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select ");
            Sql.AppendLine(" rtrim(ltrim(ifnull(pj.t003_nr_matricula,''))) as matricula ,   ");
            Sql.AppendLine(" rtrim(ltrim(ifnull(p.t001_ds_pessoa,''))) as nomesociedade , ");
            Sql.AppendLine(" pj.t003_nr_cnpj as CNPJ , ");
            Sql.AppendLine(" ifnull(nj.a006_ds_natureza_juridica ,'') as naturezajuridica, ");
            Sql.AppendLine(" ifnull(ae.tae_cod_actvd,'') as cnaecodigo, ");
            Sql.AppendLine(" ifnull(ae.tae_desc,'') as cnaedescricao ");
            Sql.AppendLine(" from	t003_pessoa_juridica pj ");
            Sql.AppendLine(" inner join t001_pessoa p ");
            Sql.AppendLine("    on pj.t001_sq_pessoa = p.t001_sq_pessoa    ");
            Sql.AppendLine(" inner join r002_vinculo_endereco e  ");
            Sql.AppendLine("    on e.t001_sq_pessoa = pj.t001_sq_pessoa ");
            Sql.AppendLine(" inner  join a006_natureza_juridica nj ");
            Sql.AppendLine("    on pj.a006_co_natureza_juridica = nj.a006_co_natureza_juridica ");
            Sql.AppendLine(" left join r004_atuacao c ");
            Sql.AppendLine(" on c.t001_sq_pessoa = pj.t001_sq_pessoa ");
            Sql.AppendLine(" left join tab_actv_econ ae ");
            Sql.AppendLine(" on ae.tae_cod_actvd = c.a001_co_atividade ");

            Sql.AppendLine(" Where	1 = 1 ");
            if (Matricula != "")
            {
                Sql.AppendLine(" And	pj.t003_nr_matricula = '" + Matricula + "'");
            }

            if (NomeSociedade != "")
            {
                Sql.AppendLine(" And	p.t001_ds_pessoa like '%" + NomeSociedade + "%'");
            }
            if (CNAE != "")
            {
                if (IsNumeric(CNAE))
                {
                    Sql.AppendLine(" And	ae.TAE_COD_ACTVD = '" + CNAE + "'");
                }
                else
                {
                    Sql.AppendLine(" And	ae.TAE_DESC like '%" + CNAE + "%'");

                }
            }
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T003_Pessoa_Juridica");
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

            Sql.AppendLine(" From	T003_Pessoa_Juridica");
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
        public Boolean ViabilidadeDisponivel(String wProtocoloViabilidade)
        {
            Boolean wChave = true;
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t003_pessoa_juridica");
            string wProtocoloRequerimento = String.Empty;
            Sql.AppendLine("SELECT t003_prot_viab from t003_pessoa_juridica WHERE t003_prot_viab = '" + wProtocoloViabilidade + "'");

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
                if (toReturn.Rows.Count == 0)
                    wChave = false;
                return wChave;
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
        #endregion

        private static Regex _isNumber = new Regex(@"^\d+$");

        public static bool IsNumeric(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        } //IsInteger

        public void UpdateCapitalIntegralizado()
        {
            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T003_Pessoa_Juridica Set ");
            //SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //Fim
            SqlU.AppendLine("		t003_vl_capital_integralizado = @v_t003_vl_capital_integralizado, ");
            SqlU.AppendLine("       t003_vl_capital_nao_integralizado = @v_t003_vl_capital_nao_integralizado, ");
            SqlU.AppendLine("       t003_data_limite_integralizacao = evaldate(@v_t003_data_limite_integralizacao) ");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital_integralizado", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_vl_capital_nao_integralizado", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_vl_capital_nao_integralizado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_data_limite_integralizacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t003_data_limite_integralizacao));
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

        public void GravaOutrosEventos(int _sqPessoa, int _eventoconsolidacao, int _eventoreativacao, int _eventoreratificacao)
        {
            StringBuilder SqlU = new StringBuilder();


            SqlU.AppendLine(@" Update     T003_Pessoa_Juridica Set 
            		                        T003_IN_CONSOLIDACAO = @v_T003_IN_CONSOLIDACAO, 
                                           T003_IN_REATIVACAO = @v_T003_IN_REATIVACAO, 
                                           T003_IN_RERATIFICACAO = @v_T003_IN_RERATIFICACAO 
                             Where  t001_sq_pessoa = @v_t001_sq_pessoa ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _sqPessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IN_CONSOLIDACAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _eventoconsolidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IN_REATIVACAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _eventoreativacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T003_IN_RERATIFICACAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _eventoreratificacao));
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
    }

}


