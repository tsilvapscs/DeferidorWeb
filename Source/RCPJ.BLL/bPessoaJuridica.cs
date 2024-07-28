using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    class bPessoaJuridica : bPessoa
    {
        #region Class Member Declarations
        private string _t004_nr_cnpj_org_reg;
        private string _t003_nr_matricula;
        private string _t003_nr_cnpj;
        private string _t003_nr_inscricao_estadual;
        private string _t003_nr_inscricao_municipal;
        private string _t006_nr_cnpj_org_reg_ant;
        private string _t003_nr_matricula_anterior;
        private string _a006_co_natureza_juridica;
        private string _a011_co_porte;
        private string _a007_co_situacao;
        private string _t003_ds_situacao;

        private string _a008_co_status_atual;
        private decimal _t003_vl_capital;
        private decimal _t003_vl_capital_social;
        private decimal _t003_vl_valor_cota;
        private decimal _t003_vl_qtd_cotas;
        private decimal _t003_vl_capital_integralizado;
        private int _t003_co_tipo_pes_jur;
        private int _t003_co_forma_atuacao;
        private Nullable<DateTime> _t003_dt_constituicao;
        private Nullable<DateTime> _t003_dt_inicio_atividade;
        private Nullable<DateTime> _t003_dt_termino_ativ;
        private Nullable<DateTime> _t003_dt_prazo_determinado;
        private string _t003_ds_objeto_social;
        private int _t003_dia_fim_exercicio;
        private int _t003_mes_fim_exercicio;
        private string _t003_distribuicao_proporcional;
        private string _t003_tipo_sociedade;
        private int _t003_tipo_enquadramento;
        private string _t003_dbe;
        private string _t003_prot_viab;
        private Nullable<DateTime> _t003_data_limite_integralizacao;
        private decimal _t003_vl_capital_nao_integralizado;
        private string _t003_ds_capital_nao_integralizado;
        private string _t003_moeda_corrente;
        private string _t003_socios_obrigacoes_sociais;
        private int _t003_ind_filial_sede_fora;
        private int _t003_ind_cnae_destacada;
        private int _t003_ind_unipessoal;
        private string _t003_ds_reducao_capital;
        private int _t003_in_reducao_capital =2;

       
        #endregion

        #region Class Property Declarations
        public string T004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }
            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string T003_nr_matricula
        {
            get { return _t003_nr_matricula; }
            set { _t003_nr_matricula = value; }
        }

        public string T003_nr_cnpj
        {
            get { return _t003_nr_cnpj; }
            set { _t003_nr_cnpj = value; }
        }

        public string T003_nr_inscricao_estadual
        {
            get { return _t003_nr_inscricao_estadual; }
            set { _t003_nr_inscricao_estadual = value; }
        }

        public string T003_nr_inscricao_municipal
        {
            get { return _t003_nr_inscricao_municipal; }
            set { _t003_nr_inscricao_municipal = value; }
        }

        public string T006_nr_cnpj_org_reg_ant
        {
            get { return _t006_nr_cnpj_org_reg_ant; }
            set { _t006_nr_cnpj_org_reg_ant = value; }
        }

        public string T003_nr_matricula_anterior
        {
            get { return _t003_nr_matricula_anterior; }
            set { _t003_nr_matricula_anterior = value; }
        }

        public string A006_co_natureza_juridica
        {
            get { return _a006_co_natureza_juridica; }
            set { _a006_co_natureza_juridica = value; }
        }
        
        public string A011_co_porte
        {
            get { return _a011_co_porte; }
            set { _a011_co_porte = value; }
        }

        public string A007_co_situacao
        {
            get { return _a007_co_situacao; }
            set { _a007_co_situacao = value; }
        }

        public string A008_co_status_atual
        {
            get { return _a008_co_status_atual; }
            set { _a008_co_status_atual = value; }
        }

        public decimal T003_vl_capital
        {
            get { return _t003_vl_capital; }
            set { _t003_vl_capital = value; }
        }
        
        public decimal T003_vl_capital_social
        {
            get { return _t003_vl_capital_social; }
            set { _t003_vl_capital_social = value; }
        }

        public decimal T003_vl_valor_cota
        {
            get { return _t003_vl_valor_cota; }
            set { _t003_vl_valor_cota = value; }
        }

        public decimal T003_vl_qtd_cotas
        {
            get { return _t003_vl_qtd_cotas; }
            set { _t003_vl_qtd_cotas = value; }
        }

        public decimal T003_vl_capital_integralizado
        {
            get { return _t003_vl_capital_integralizado; }
            set { _t003_vl_capital_integralizado = value; }
        }

        public int T003_co_tipo_pes_jur
        {
            get { return _t003_co_tipo_pes_jur; }
            set { _t003_co_tipo_pes_jur = value; }
        }

        public int T003_co_forma_atuacao
        {
            get { return _t003_co_forma_atuacao; }
            set { _t003_co_forma_atuacao = value; }
        }

        public Nullable<DateTime> T003_dt_constituicao
        {
            get { return _t003_dt_constituicao; }
            set { _t003_dt_constituicao = value; }
        }

        public Nullable<DateTime> T003_dt_inicio_atividade
        {
            get { return _t003_dt_inicio_atividade; }
            set { _t003_dt_inicio_atividade = value; }
        }

        public Nullable<DateTime> T003_dt_termino_ativ
        {
            get { return _t003_dt_termino_ativ; }
            set { _t003_dt_termino_ativ = value; }
        }
        
        public Nullable<DateTime> T003_dt_prazo_determinado
        {
            get { return _t003_dt_prazo_determinado; }
            set { _t003_dt_prazo_determinado = value; }
        }
        
        public string T003_ds_objeto_social
        {
            get { return _t003_ds_objeto_social; }
            set { _t003_ds_objeto_social = value; }
        }
       
        public int T003_dia_fim_exercicio
        {
            get { return _t003_dia_fim_exercicio; }
            set { _t003_dia_fim_exercicio = value; }
        }
        

        public int T003_mes_fim_exercicio
        {
            get { return _t003_mes_fim_exercicio; }
            set { _t003_mes_fim_exercicio = value; }
        }
        

        public string T003_distribuicao_proporcional
        {
            get { return _t003_distribuicao_proporcional; }
            set { _t003_distribuicao_proporcional = value; }
        }
        

        public string T003_tipo_sociedade
        {
            get { return _t003_tipo_sociedade; }
            set { _t003_tipo_sociedade = value; }
        }
        

        public int T003_tipo_enquadramento
        {
            get { return _t003_tipo_enquadramento; }
            set { _t003_tipo_enquadramento = value; }
        }
        

        public string T003_dbe
        {
            get { return _t003_dbe; }
            set { _t003_dbe = value; }
        }
        
        public string T003_prot_viab
        {
            get { return _t003_prot_viab; }
            set { _t003_prot_viab = value; }
        }

        public Nullable<DateTime> T003_data_limite_integralizacao
        {
            get { return _t003_data_limite_integralizacao; }
            set { _t003_data_limite_integralizacao = value; }
        }
       

        public decimal T003_vl_capital_nao_integralizado
        {
            get { return _t003_vl_capital_nao_integralizado; }
            set { _t003_vl_capital_nao_integralizado = value; }
        }
        
        public string T003_ds_capital_nao_integralizado
        {
            get { return _t003_ds_capital_nao_integralizado; }
            set { _t003_ds_capital_nao_integralizado = value; }
        }
       

        public string T003_moeda_corrente
        {
            get { return _t003_moeda_corrente; }
            set { _t003_moeda_corrente = value; }
        }
        
        public string T003_socios_obrigacoes_sociais
        {
            get { return _t003_socios_obrigacoes_sociais; }
            set { _t003_socios_obrigacoes_sociais = value; }
        }
        

        public int T003_ind_filial_sede_fora
        {
            get { return _t003_ind_filial_sede_fora; }
            set { _t003_ind_filial_sede_fora = value; }
        }
        

        public int T003_ind_cnae_destacada
        {
            get { return _t003_ind_cnae_destacada; }
            set { _t003_ind_cnae_destacada = value; }
        }
        

        public int T003_ind_unipessoal
        {
            get { return _t003_ind_unipessoal; }
            set { _t003_ind_unipessoal = value; }
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

        public string T003_ds_situacao
        {
            get { return _t003_ds_situacao; }
            set { _t003_ds_situacao = value; }
        }
        #endregion
    }
}
