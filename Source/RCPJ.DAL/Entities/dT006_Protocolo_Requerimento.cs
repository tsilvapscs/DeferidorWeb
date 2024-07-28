using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT006_Protocolo_Requerimento : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected string _t005_nr_protocolo;
        protected decimal _t006_nr_fundadores_diretores;
        protected string _t006_ds_artigo_estatuto;
        //protected int _t006_in_forma_convocacao;
        protected string _t006_nr_art_estatuto_convocacao;
        protected string _t006_ds_quorum_deliberacao;
        protected string _t006_ds_quorum_alteracao;
        protected string _t006_ds_quorum_dissolucao;
        protected string _t006_ds_outro_quorum_deliberacao;
        protected string _t006_ds_outro_quorum_alteracao;
        protected string _t006_ds_outro_quorum_dissolucao;
        protected string _t006_ds_destino_patrimonio;
        protected string _t006_in_obrigacoes_sociais;
        protected string _t006_in_possui_fundo_social;
        protected String _t006_recurso_mensalidade;
        protected String _t006_recurso_doacao;
        protected String _t006_recurso_governamental;
        protected string _t006_nr_art_estatuto_associacao;
        protected string _t006_ds_nome_advogado;
        protected string _t006_nr_cpf_advogado;
        protected string _t006_nr_inscr_oab;
        protected string _t006_ds_uf_oab_advogado;
        protected string _t006_ds_nome_contador;
        protected string _t006_nr_cpf_contador;
        protected string _a004_co_uf;
        protected string _t006_nr_crc_contador;
        protected decimal _t006_nr_num_vias;
        protected decimal _t006_nr_num_paginas;
        protected string _t006_in_ata_mesmo_instrumento;
        protected string _t006_nire;
        
        protected string _t006_nr_art_deliberacao;
        protected string _t006_nr_art_alteracao;
        protected string _t006_nr_art_dissolucao;
        protected string _t006_nr_art_obrigacoes_sociais;
        protected string _t006_nr_art_fundo_social;
        protected Nullable<DateTime> _t006_dt_decretacao_falencia;
        protected Nullable<DateTime> _t006_dt_interrupcao_atividade;
        protected Nullable<DateTime> _t006_dt_inicio_liquidacao;
        protected Nullable<DateTime> _t006_dt_reinicio_atividades;
        protected Nullable<DateTime> _t006_dt_termino_liquidacao;
        protected decimal _a021_co_motivo_baixa;
        protected string _t006_nr_sucessora;
        protected string _t006_nr_cnpj_sucessora;
        protected string _t006_edital_fixado_sede;
        protected  string _t006_edital_publicado_jornal;
        protected  string _t006_edital_outros;
        protected string _t006_contrato_padrao;
        private int _t006_tipo_propriedade;

        
       
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }

            set { _t005_nr_protocolo = value; }
        }
        public string t006_contrato_padrao
        {
            get { return _t006_contrato_padrao; }
            set { _t006_contrato_padrao = value; }
        }
        public decimal t006_nr_fundadores_diretores
        {
            get { return _t006_nr_fundadores_diretores; }

            set { _t006_nr_fundadores_diretores = value; }
        }

        public string t006_ds_artigo_estatuto
        {
            get { return _t006_ds_artigo_estatuto; }

            set { _t006_ds_artigo_estatuto = value; }
        }

        //public int t006_in_forma_convocacao
        //{
        //    get { return _t006_in_forma_convocacao; }

        //    set { _t006_in_forma_convocacao = value; }
        //}
        public string t006_edital_fixado_sede
        {
            get { return _t006_edital_fixado_sede; }
            set { _t006_edital_fixado_sede = value; }
        }
        public string t006_edital_publicado_jornal
        {
            get { return _t006_edital_publicado_jornal; }
            set {_t006_edital_publicado_jornal = value;}
        }
        public string t006_edital_outros
        {
            get { return _t006_edital_outros; }
            set { _t006_edital_outros = value; }
        }
        public string t006_nr_art_estatuto_convocacao
        {
            get { return _t006_nr_art_estatuto_convocacao; }

            set { _t006_nr_art_estatuto_convocacao = value; }
        }

        public string t006_ds_quorum_deliberacao
        {
            get { return _t006_ds_quorum_deliberacao; }

            set { _t006_ds_quorum_deliberacao = value; }
        }

        public string t006_ds_quorum_alteracao
        {
            get { return _t006_ds_quorum_alteracao; }

            set { _t006_ds_quorum_alteracao = value; }
        }

        public string t006_ds_quorum_dissolucao
        {
            get { return _t006_ds_quorum_dissolucao; }

            set { _t006_ds_quorum_dissolucao = value; }
        }

        public string t006_ds_outro_quorum_deliberacao
        {
            get { return _t006_ds_outro_quorum_deliberacao; }

            set { _t006_ds_outro_quorum_deliberacao = value; }
        }

        public string t006_ds_outro_quorum_alteracao
        {
            get { return _t006_ds_outro_quorum_alteracao; }

            set { _t006_ds_outro_quorum_alteracao = value; }
        }

        public string t006_ds_outro_quorum_dissolucao
        {
            get { return _t006_ds_outro_quorum_dissolucao; }

            set { _t006_ds_outro_quorum_dissolucao = value; }
        }

        public string t006_ds_destino_patrimonio
        {
            get { return _t006_ds_destino_patrimonio; }

            set { _t006_ds_destino_patrimonio = value; }
        }

        public string t006_in_obrigacoes_sociais
        {
            get { return _t006_in_obrigacoes_sociais; }

            set { _t006_in_obrigacoes_sociais = value; }
        }

        public string t006_in_possui_fundo_social
        {
            get { return _t006_in_possui_fundo_social; }

            set { _t006_in_possui_fundo_social = value; }
        }

        public String t006_recurso_mensalidade
        {
            get { return _t006_recurso_mensalidade; }
            set { _t006_recurso_mensalidade = value; }
        }

        public String t006_recurso_doacao
        {
            get { return _t006_recurso_doacao; }
            set { _t006_recurso_doacao = value; }
        }

        public String t006_recurso_governamental
        {
            get { return _t006_recurso_governamental; }
            set { _t006_recurso_governamental = value; }
        }

        public string t006_nr_art_estatuto_associacao
        {
            get { return _t006_nr_art_estatuto_associacao; }

            set { _t006_nr_art_estatuto_associacao = value; }
        }

        public string t006_ds_nome_advogado
        {
            get { return _t006_ds_nome_advogado; }

            set { _t006_ds_nome_advogado = value; }
        }

        public string t006_nr_cpf_advogado
        {
            get { return _t006_nr_cpf_advogado; }

            set { _t006_nr_cpf_advogado = value; }
        }

        public string t006_nr_inscr_oab
        {
            get { return _t006_nr_inscr_oab; }

            set { _t006_nr_inscr_oab = value; }
        }
        public string t006_ds_uf_oab_advogado
        {
            get { return _t006_ds_uf_oab_advogado; }
            set { _t006_ds_uf_oab_advogado = value; }
        }

        public string t006_ds_nome_contador
        {
            get { return _t006_ds_nome_contador; }

            set { _t006_ds_nome_contador = value; }
        }

        public string t006_nr_cpf_contador
        {
            get { return _t006_nr_cpf_contador; }

            set { _t006_nr_cpf_contador = value; }
        }

        public string a004_co_uf
        {
            get { return _a004_co_uf; }

            set { _a004_co_uf = value; }
        }

        public string t006_nr_crc_contador
        {
            get { return _t006_nr_crc_contador; }

            set { _t006_nr_crc_contador = value; }
        }

        public decimal t006_nr_num_paginas
        {
            get { return _t006_nr_num_paginas; }

            set { _t006_nr_num_paginas = value; }
        }
        public decimal t006_nr_num_vias
        {
            get { return _t006_nr_num_vias; }

            set { _t006_nr_num_vias = value; }
        }
        public string t006_in_ata_mesmo_instrumento
        {
            get { return _t006_in_ata_mesmo_instrumento; }

            set { _t006_in_ata_mesmo_instrumento = value; }
        }

        public string t006_nire
        {
            get { return _t006_nire; }

            set { _t006_nire = value; }
        }

        public Nullable<DateTime> t006_dt_decretacao_falencia
        {
            get { return _t006_dt_decretacao_falencia; }

            set { _t006_dt_decretacao_falencia = value; }
        }

        public Nullable<DateTime> t006_dt_interrupcao_atividade
        {
            get { return _t006_dt_interrupcao_atividade; }

            set { _t006_dt_interrupcao_atividade = value; }
        }

        public Nullable<DateTime> t006_dt_inicio_liquidacao
        {
            get { return _t006_dt_inicio_liquidacao; }

            set { _t006_dt_inicio_liquidacao = value; }
        }

        public Nullable<DateTime> t006_dt_reinicio_atividades
        {
            get { return _t006_dt_reinicio_atividades; }

            set { _t006_dt_reinicio_atividades = value; }
        }

        public Nullable<DateTime> t006_dt_termino_liquidacao
        {
            get { return _t006_dt_termino_liquidacao; }

            set { _t006_dt_termino_liquidacao = value; }
        }

        public decimal a021_co_motivo_baixa
        {
            get { return _a021_co_motivo_baixa; }

            set { _a021_co_motivo_baixa = value; }
        }

        public string t006_nr_sucessora
        {
            get { return _t006_nr_sucessora; }

            set { _t006_nr_sucessora = value; }
        }

        public string t006_nr_cnpj_sucessora
        {
            get { return _t006_nr_cnpj_sucessora; }

            set { _t006_nr_cnpj_sucessora = value; }
        }
        public string t006_nr_art_deliberacao
        {
            get { return _t006_nr_art_deliberacao; }
            set { _t006_nr_art_deliberacao = value; }
        }
        public string t006_nr_art_alteracao
        {
            get { return _t006_nr_art_alteracao; }
            set { _t006_nr_art_alteracao = value; }
        }
        public string t006_nr_art_dissolucao
        {
            get { return _t006_nr_art_dissolucao; }
            set { _t006_nr_art_dissolucao = value; }
        }
        public string t006_nr_art_obrigacoes_sociais
        {
            get { return _t006_nr_art_obrigacoes_sociais; }
            set { _t006_nr_art_obrigacoes_sociais = value; }
        }
        public string t006_nr_art_fundo_social
        {
            get { return _t006_nr_art_fundo_social; }
            set { _t006_nr_art_fundo_social = value; }
        }
        public int T006_tipo_propriedade
        {
            get { return _t006_tipo_propriedade; }
            set { _t006_tipo_propriedade = value; }
        }
        #endregion


        #region Implements
        // Update Sociedade
        public void UpdateSociedade()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t006_protocolo_requerimento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t005_nr_protocolo, ");
            SqlI.AppendLine("	t006_nr_fundadores_diretores, ");
            SqlI.AppendLine("	t006_ds_artigo_estatuto, ");
            //SqlI.AppendLine("	t006_in_forma_convocacao, ");
            SqlI.AppendLine("   t006_edital_fixado_sede, ");
            SqlI.AppendLine("   t006_edital_publicado_jornal, ");
            SqlI.AppendLine("   t006_edital_outros, ");
            SqlI.AppendLine("	t006_nr_art_estatuto_convocacao, ");
            SqlI.AppendLine("	t006_ds_quorum_deliberacao, ");
            SqlI.AppendLine("	t006_ds_quorum_alteracao, ");
            SqlI.AppendLine("	t006_ds_quorum_dissolucao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_dissolucao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_alteracao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_deliberacao, ");
            SqlI.AppendLine("	t006_ds_destino_patrimonio, ");
            SqlI.AppendLine("	t006_in_obrigacoes_sociais, ");
            SqlI.AppendLine("	t006_in_possui_fundo_social, ");
            SqlI.AppendLine("	t006_recurso_mensalidade, ");
            SqlI.AppendLine("	t006_recurso_doacao, ");
            SqlI.AppendLine("	t006_recurso_governamental, ");
            SqlI.AppendLine("	t006_nr_art_estatuto_associacao, ");
            //SqlI.AppendLine("	t006_ds_nome_advogado, ");
            //SqlI.AppendLine("	t006_nr_cpf_advogado, ");
            //SqlI.AppendLine("	t006_nr_inscr_oab, ");
            //SqlI.AppendLine("   t006_ds_uf_oab_advogado, ");
            SqlI.AppendLine("	t006_ds_nome_contador, ");
            SqlI.AppendLine("	t006_nr_cpf_contador, ");
            SqlI.AppendLine("	a004_co_uf, ");
            SqlI.AppendLine("	t006_nr_crc_contador, ");
            SqlI.AppendLine("	t006_nr_num_paginas, ");
            SqlI.AppendLine("	t006_in_ata_mesmo_instrumento, ");
            SqlI.AppendLine("	t006_nire, ");
            SqlI.AppendLine("	t006_dt_decretacao_falencia, ");
            SqlI.AppendLine("	t006_dt_interrupcao_atividade, ");
            SqlI.AppendLine("	t006_dt_inicio_liquidacao, ");
            SqlI.AppendLine("	t006_dt_reinicio_atividades, ");
            SqlI.AppendLine("	t006_dt_termino_liquidacao, ");
            SqlI.AppendLine("	a021_co_motivo_baixa, ");
            SqlI.AppendLine("	t006_nr_sucessora, ");
            SqlI.AppendLine("	t006_nr_cnpj_sucessora, ");
            SqlI.AppendLine("   t006_tipo_propriedade ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t005_nr_protocolo, ");
            SqlI.AppendLine("	@v_t006_nr_fundadores_diretores, ");
            SqlI.AppendLine("	@v_t006_ds_artigo_estatuto, ");
            //SqlI.AppendLine("	@v_t006_in_forma_convocacao, ");
            SqlI.AppendLine("   @v_t006_edital_fixado_sede, ");
            SqlI.AppendLine("   @v_t006_edital_publicado_jornal, ");
            SqlI.AppendLine("   @v_t006_edital_outros, ");
            SqlI.AppendLine("	@v_t006_nr_art_estatuto_convocacao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_deliberacao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_alteracao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_dissolucao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_dissolucao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_alteracao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_deliberacao, ");
            SqlI.AppendLine("	@v_t006_ds_destino_patrimonio, ");
            SqlI.AppendLine("	@v_t006_in_obrigacoes_sociais, ");
            SqlI.AppendLine("	@v_t006_in_possui_fundo_social, ");
            SqlI.AppendLine("	@v_t006_recurso_mensalidade, ");
            SqlI.AppendLine("	@v_t006_recurso_doacao, ");
            SqlI.AppendLine("	@v_t006_recurso_governamental, ");
            SqlI.AppendLine("	@v_t006_nr_art_estatuto_associacao, ");
            //SqlI.AppendLine("	@v_t006_ds_nome_advogado, ");
            //SqlI.AppendLine("	@v_t006_nr_cpf_advogado, ");
            //SqlI.AppendLine("	@v_t006_nr_inscr_oab, ");
            //SqlI.AppendLine("   @v_t006_ds_uf_oab_advogado, ");
            SqlI.AppendLine("	@v_t006_ds_nome_contador, ");
            SqlI.AppendLine("	@v_t006_nr_cpf_contador, ");
            SqlI.AppendLine("	@v_a004_co_uf, ");
            SqlI.AppendLine("	@v_t006_nr_crc_contador, ");
            SqlI.AppendLine("	@v_t006_nr_num_paginas, ");
            SqlI.AppendLine("	@v_t006_in_ata_mesmo_instrumento, ");
            SqlI.AppendLine("	@v_t006_nire, ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_decretacao_falencia), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_interrupcao_atividade), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_inicio_liquidacao), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_reinicio_atividades), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_termino_liquidacao), ");
            SqlI.AppendLine("	@v_a021_co_motivo_baixa, ");
            SqlI.AppendLine("	@v_t006_nr_sucessora, ");
            SqlI.AppendLine("	@v_t006_nr_cnpj_sucessora, ");
            SqlI.AppendLine("	@v_t006_tipo_propriedade ");
            SqlI.AppendLine("  )");
            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T006_Protocolo_Requerimento Set ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //SqlU.AppendLine("		t005_nr_protocolo = @v_t005_nr_protocolo, ");
            SqlU.AppendLine("		t006_nr_fundadores_diretores = @v_t006_nr_fundadores_diretores, ");
            SqlU.AppendLine("		t006_ds_artigo_estatuto = @v_t006_ds_artigo_estatuto, ");
            //SqlU.AppendLine("		t006_in_forma_convocacao = @v_t006_in_forma_convocacao, ");
            SqlU.AppendLine("       t006_edital_fixado_sede = @v_t006_edital_fixado_sede, ");
            SqlU.AppendLine("       t006_edital_publicado_jornal = @v_t006_edital_publicado_jornal, ");
            SqlU.AppendLine("       t006_edital_outros = @v_t006_edital_outros, ");
            SqlU.AppendLine("		t006_nr_art_estatuto_convocacao = @v_t006_nr_art_estatuto_convocacao, ");
            SqlU.AppendLine("		t006_ds_quorum_deliberacao = @v_t006_ds_quorum_deliberacao, ");
            SqlU.AppendLine("		t006_ds_quorum_alteracao = @v_t006_ds_quorum_alteracao, ");
            SqlU.AppendLine("		t006_ds_quorum_dissolucao = @v_t006_ds_quorum_dissolucao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_dissolucao = @v_t006_ds_outro_quorum_dissolucao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_alteracao = @v_t006_ds_outro_quorum_alteracao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_deliberacao = @v_t006_ds_outro_quorum_deliberacao, ");
            SqlU.AppendLine("		t006_ds_destino_patrimonio = @v_t006_ds_destino_patrimonio, ");
            SqlU.AppendLine("		t006_in_obrigacoes_sociais = @v_t006_in_obrigacoes_sociais, ");
            SqlU.AppendLine("		t006_in_possui_fundo_social = @v_t006_in_possui_fundo_social, ");
            SqlU.AppendLine("		t006_recurso_mensalidade = @v_t006_recurso_mensalidade, ");
            SqlU.AppendLine("		t006_recurso_doacao = @v_t006_recurso_doacao, ");
            SqlU.AppendLine("		t006_recurso_governamental = @v_t006_recurso_governamental, ");
            SqlU.AppendLine("		t006_nr_art_estatuto_associacao = @v_t006_nr_art_estatuto_associacao, ");
            //SqlU.AppendLine("		t006_ds_nome_advogado = @v_t006_ds_nome_advogado, ");
            //SqlU.AppendLine("		t006_nr_cpf_advogado = @v_t006_nr_cpf_advogado, ");
            //SqlU.AppendLine("		t006_nr_inscr_oab = @v_t006_nr_inscr_oab, ");
            //SqlU.AppendLine("       t006_ds_uf_oab_advogado = @v_t006_ds_uf_oab_advogado, ");
            SqlU.AppendLine("		t006_ds_nome_contador = @v_t006_ds_nome_contador, ");
            SqlU.AppendLine("		t006_nr_cpf_contador = @v_t006_nr_cpf_contador, ");
            SqlU.AppendLine("		a004_co_uf = @v_a004_co_uf, ");
            SqlU.AppendLine("		t006_nr_crc_contador = @v_t006_nr_crc_contador, ");
            SqlU.AppendLine("		t006_nr_num_paginas = @v_t006_nr_num_paginas, ");
            SqlU.AppendLine("		t006_in_ata_mesmo_instrumento = @v_t006_in_ata_mesmo_instrumento, ");
            SqlU.AppendLine("		t006_nire = @v_t006_nire, ");
            SqlU.AppendLine("		t006_dt_decretacao_falencia = evaldate(@v_t006_dt_decretacao_falencia), ");
            SqlU.AppendLine("		t006_dt_interrupcao_atividade = evaldate(@v_t006_dt_interrupcao_atividade), ");
            SqlU.AppendLine("		t006_dt_inicio_liquidacao = evaldate(@v_t006_dt_inicio_liquidacao), ");
            SqlU.AppendLine("		t006_dt_reinicio_atividades = evaldate(@v_t006_dt_reinicio_atividades), ");
            SqlU.AppendLine("		t006_dt_termino_liquidacao = evaldate(@v_t006_dt_termino_liquidacao), ");
            SqlU.AppendLine("		a021_co_motivo_baixa = @v_a021_co_motivo_baixa, ");
            SqlU.AppendLine("		t006_nr_sucessora = @v_t006_nr_sucessora, ");
            SqlU.AppendLine("		t006_nr_cnpj_sucessora = @v_t006_nr_cnpj_sucessora, ");
            SqlU.AppendLine("		t006_tipo_propriedade = @v_t006_tipo_propriedade");
           

            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine("	and	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");

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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_fundadores_diretores", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_fundadores_diretores));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_artigo_estatuto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_artigo_estatuto));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_forma_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_forma_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_fixado_sede", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_fixado_sede));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_publicado_jornal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_publicado_jornal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_outros", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_outros));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_destino_patrimonio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_destino_patrimonio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_obrigacoes_sociais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_obrigacoes_sociais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_possui_fundo_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_possui_fundo_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_mensalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_mensalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_doacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_doacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_governamental", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_governamental));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_associacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_associacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_inscr_oab", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_inscr_oab));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_uf_oab_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_crc_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_crc_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_num_paginas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_num_paginas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_ata_mesmo_instrumento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_ata_mesmo_instrumento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nire));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_decretacao_falencia", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_decretacao_falencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_interrupcao_atividade", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_interrupcao_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_inicio_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_inicio_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_reinicio_atividades", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_reinicio_atividades));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_termino_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_termino_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a021_co_motivo_baixa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a021_co_motivo_baixa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cnpj_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cnpj_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_tipo_propriedade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_tipo_propriedade));
                

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

        //Update Fundação Associação
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t006_protocolo_requerimento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t005_nr_protocolo, ");
            SqlI.AppendLine("	t006_nr_fundadores_diretores, ");
            SqlI.AppendLine("	t006_ds_artigo_estatuto, ");
            //SqlI.AppendLine("	t006_in_forma_convocacao, ");
            SqlI.AppendLine("   t006_edital_fixado_sede, ");
            SqlI.AppendLine("   t006_edital_publicado_jornal, ");
            SqlI.AppendLine("   t006_edital_outros, ");
            SqlI.AppendLine("	t006_nr_art_estatuto_convocacao, ");
            SqlI.AppendLine("	t006_ds_quorum_deliberacao, ");
            SqlI.AppendLine("	t006_ds_quorum_alteracao, ");
            SqlI.AppendLine("	t006_ds_quorum_dissolucao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_dissolucao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_alteracao, ");
            SqlI.AppendLine("	t006_ds_outro_quorum_deliberacao, ");
            SqlI.AppendLine("	t006_ds_destino_patrimonio, ");
            SqlI.AppendLine("	t006_in_obrigacoes_sociais, ");
            SqlI.AppendLine("	t006_in_possui_fundo_social, ");
            SqlI.AppendLine("	t006_recurso_mensalidade, ");
            SqlI.AppendLine("	t006_recurso_doacao, ");
            SqlI.AppendLine("	t006_recurso_governamental, ");
            SqlI.AppendLine("	t006_nr_art_estatuto_associacao, ");
            SqlI.AppendLine("	t006_ds_nome_advogado, ");
            SqlI.AppendLine("	t006_nr_cpf_advogado, ");
            SqlI.AppendLine("	t006_nr_inscr_oab, ");
            SqlI.AppendLine("   t006_ds_uf_oab_advogado, ");
            SqlI.AppendLine("	t006_ds_nome_contador, ");
            SqlI.AppendLine("	t006_nr_cpf_contador, ");
            SqlI.AppendLine("	a004_co_uf, ");
            SqlI.AppendLine("	t006_nr_crc_contador, ");
            SqlI.AppendLine("	t006_nr_num_paginas, ");
            SqlI.AppendLine("	t006_in_ata_mesmo_instrumento, ");
            SqlI.AppendLine("	t006_nire, ");
            SqlI.AppendLine("	t006_dt_decretacao_falencia, ");
            SqlI.AppendLine("	t006_dt_interrupcao_atividade, ");
            SqlI.AppendLine("	t006_dt_inicio_liquidacao, ");
            SqlI.AppendLine("	t006_dt_reinicio_atividades, ");
            SqlI.AppendLine("	t006_dt_termino_liquidacao, ");
            SqlI.AppendLine("	a021_co_motivo_baixa, ");
            SqlI.AppendLine("	t006_nr_sucessora, ");
            SqlI.AppendLine("   t006_nr_art_deliberacao, ");
            SqlI.AppendLine("   t006_nr_art_alteracao, ");
            SqlI.AppendLine("   t006_nr_art_dissolucao, ");
            SqlI.AppendLine("   t006_nr_art_obrigacoes_sociais, ");
            SqlI.AppendLine("   t006_nr_art_fundo_social, ");
            SqlI.AppendLine("	t006_nr_cnpj_sucessora, ");
            SqlI.AppendLine("   t006_tipo_propriedade ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t005_nr_protocolo, ");
            SqlI.AppendLine("	@v_t006_nr_fundadores_diretores, ");
            SqlI.AppendLine("	@v_t006_ds_artigo_estatuto, ");
            //SqlI.AppendLine("	@v_t006_in_forma_convocacao, ");
            SqlI.AppendLine("   @v_t006_edital_fixado_sede, ");
            SqlI.AppendLine("   @v_t006_edital_publicado_jornal, ");
            SqlI.AppendLine("   @v_t006_edital_outros, ");
            SqlI.AppendLine("	@v_t006_nr_art_estatuto_convocacao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_deliberacao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_alteracao, ");
            SqlI.AppendLine("	@v_t006_ds_quorum_dissolucao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_dissolucao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_alteracao, ");
            SqlI.AppendLine("	@v_t006_ds_outro_quorum_deliberacao, ");
            SqlI.AppendLine("	@v_t006_ds_destino_patrimonio, ");
            SqlI.AppendLine("	@v_t006_in_obrigacoes_sociais, ");
            SqlI.AppendLine("	@v_t006_in_possui_fundo_social, ");
            SqlI.AppendLine("	@v_t006_recurso_mensalidade, ");
            SqlI.AppendLine("	@v_t006_recurso_doacao, ");
            SqlI.AppendLine("	@v_t006_recurso_governamental, ");
            SqlI.AppendLine("	@v_t006_nr_art_estatuto_associacao, ");
            SqlI.AppendLine("	@v_t006_ds_nome_advogado, ");
            SqlI.AppendLine("	@v_t006_nr_cpf_advogado, ");
            SqlI.AppendLine("	@v_t006_nr_inscr_oab, ");
            SqlI.AppendLine("   @v_t006_ds_uf_oab_advogado, ");
            SqlI.AppendLine("	@v_t006_ds_nome_contador, ");
            SqlI.AppendLine("	@v_t006_nr_cpf_contador, ");
            SqlI.AppendLine("	@v_a004_co_uf, ");
            SqlI.AppendLine("	@v_t006_nr_crc_contador, ");
            SqlI.AppendLine("	@v_t006_nr_num_paginas, ");
            SqlI.AppendLine("	@v_t006_in_ata_mesmo_instrumento, ");
            SqlI.AppendLine("	@v_t006_nire, ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_decretacao_falencia), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_interrupcao_atividade), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_inicio_liquidacao), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_reinicio_atividades), ");
            SqlI.AppendLine("	evaldate(@v_t006_dt_termino_liquidacao), ");
            SqlI.AppendLine("	@v_a021_co_motivo_baixa, ");
            SqlI.AppendLine("   @v_t006_nr_art_deliberacao, ");
            SqlI.AppendLine("   @v_t006_nr_art_alteracao, ");
            SqlI.AppendLine("   @v_t006_nr_art_dissolucao, ");
            SqlI.AppendLine("   @v_t006_nr_art_obrigacoes_sociais, ");
            SqlI.AppendLine("   @v_t006_nr_art_fundo_social, ");
            SqlI.AppendLine("	@v_t006_nr_sucessora, ");
            SqlI.AppendLine("	@v_t006_nr_cnpj_sucessora, ");
            SqlI.AppendLine("	@v_t006_tipo_propriedade ");
            SqlI.AppendLine("  )");
            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T006_Protocolo_Requerimento Set ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //SqlU.AppendLine("		t005_nr_protocolo = @v_t005_nr_protocolo, ");
            SqlU.AppendLine("		t006_nr_fundadores_diretores = @v_t006_nr_fundadores_diretores, ");
            SqlU.AppendLine("		t006_ds_artigo_estatuto = @v_t006_ds_artigo_estatuto, ");
            //SqlU.AppendLine("		t006_in_forma_convocacao = @v_t006_in_forma_convocacao, ");
            SqlU.AppendLine("       t006_edital_fixado_sede = @v_t006_edital_fixado_sede, ");
            SqlU.AppendLine("       t006_edital_publicado_jornal = @v_t006_edital_publicado_jornal, ");
            SqlU.AppendLine("       t006_edital_outros = @v_t006_edital_outros, ");
            SqlU.AppendLine("		t006_nr_art_estatuto_convocacao = @v_t006_nr_art_estatuto_convocacao, ");
            SqlU.AppendLine("		t006_ds_quorum_deliberacao = @v_t006_ds_quorum_deliberacao, ");
            SqlU.AppendLine("		t006_ds_quorum_alteracao = @v_t006_ds_quorum_alteracao, ");
            SqlU.AppendLine("		t006_ds_quorum_dissolucao = @v_t006_ds_quorum_dissolucao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_dissolucao = @v_t006_ds_outro_quorum_dissolucao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_alteracao = @v_t006_ds_outro_quorum_alteracao, ");
            SqlU.AppendLine("		t006_ds_outro_quorum_deliberacao = @v_t006_ds_outro_quorum_deliberacao, ");
            SqlU.AppendLine("		t006_ds_destino_patrimonio = @v_t006_ds_destino_patrimonio, ");
            SqlU.AppendLine("		t006_in_obrigacoes_sociais = @v_t006_in_obrigacoes_sociais, ");
            SqlU.AppendLine("		t006_in_possui_fundo_social = @v_t006_in_possui_fundo_social, ");
            SqlU.AppendLine("		t006_recurso_mensalidade = @v_t006_recurso_mensalidade, ");
            SqlU.AppendLine("		t006_recurso_doacao = @v_t006_recurso_doacao, ");
            SqlU.AppendLine("		t006_recurso_governamental = @v_t006_recurso_governamental, ");
            SqlU.AppendLine("		t006_nr_art_estatuto_associacao = @v_t006_nr_art_estatuto_associacao, ");
            SqlU.AppendLine("		t006_ds_nome_advogado = @v_t006_ds_nome_advogado, ");
            SqlU.AppendLine("		t006_nr_cpf_advogado = @v_t006_nr_cpf_advogado, ");
            SqlU.AppendLine("		t006_nr_inscr_oab = @v_t006_nr_inscr_oab, ");
            SqlU.AppendLine("       t006_ds_uf_oab_advogado = @v_t006_ds_uf_oab_advogado, ");
            SqlU.AppendLine("		t006_ds_nome_contador = @v_t006_ds_nome_contador, ");
            SqlU.AppendLine("		t006_nr_cpf_contador = @v_t006_nr_cpf_contador, ");
            SqlU.AppendLine("		a004_co_uf = @v_a004_co_uf, ");
            SqlU.AppendLine("		t006_nr_crc_contador = @v_t006_nr_crc_contador, ");
            SqlU.AppendLine("		t006_nr_num_paginas = @v_t006_nr_num_paginas, ");
            SqlU.AppendLine("		t006_in_ata_mesmo_instrumento = @v_t006_in_ata_mesmo_instrumento, ");
            SqlU.AppendLine("		t006_nire = @v_t006_nire, ");
            SqlU.AppendLine("		t006_dt_decretacao_falencia = evaldate(@v_t006_dt_decretacao_falencia), ");
            SqlU.AppendLine("		t006_dt_interrupcao_atividade = evaldate(@v_t006_dt_interrupcao_atividade), ");
            SqlU.AppendLine("		t006_dt_inicio_liquidacao = evaldate(@v_t006_dt_inicio_liquidacao), ");
            SqlU.AppendLine("		t006_dt_reinicio_atividades = evaldate(@v_t006_dt_reinicio_atividades), ");
            SqlU.AppendLine("		t006_dt_termino_liquidacao = evaldate(@v_t006_dt_termino_liquidacao), ");
            SqlU.AppendLine("		a021_co_motivo_baixa = @v_a021_co_motivo_baixa, ");
            SqlU.AppendLine("       t006_nr_art_deliberacao=  @v_t006_nr_art_deliberacao, ");
            SqlU.AppendLine("       t006_nr_art_alteracao = @v_t006_nr_art_alteracao, ");
            SqlU.AppendLine("       t006_nr_art_dissolucao = @v_t006_nr_art_dissolucao, ");
            SqlU.AppendLine("       t006_nr_art_obrigacoes_sociais = @v_t006_nr_art_obrigacoes_sociais, ");
            SqlU.AppendLine("       t006_nr_art_fundo_social = @v_t006_nr_art_fundo_social, ");
            SqlU.AppendLine("		t006_nr_sucessora = @v_t006_nr_sucessora, ");
            SqlU.AppendLine("		t006_nr_cnpj_sucessora = @v_t006_nr_cnpj_sucessora,");
            SqlU.AppendLine("		t006_tipo_propriedade = @v_t006_tipo_propriedade");


            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine("	and	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");

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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_fundadores_diretores", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_fundadores_diretores));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_artigo_estatuto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_artigo_estatuto));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_forma_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_forma_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_fixado_sede", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_fixado_sede));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_publicado_jornal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_publicado_jornal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_edital_outros", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_edital_outros));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_outro_quorum_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_outro_quorum_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_destino_patrimonio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_destino_patrimonio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_obrigacoes_sociais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_obrigacoes_sociais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_possui_fundo_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_possui_fundo_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_mensalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_mensalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_doacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_doacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_governamental", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_governamental));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_associacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_associacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_inscr_oab", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_inscr_oab));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_uf_oab_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_crc_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_crc_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_num_paginas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_num_paginas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_ata_mesmo_instrumento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_ata_mesmo_instrumento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nire));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_decretacao_falencia", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_decretacao_falencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_interrupcao_atividade", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_interrupcao_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_inicio_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_inicio_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_reinicio_atividades", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_reinicio_atividades));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_termino_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_termino_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a021_co_motivo_baixa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a021_co_motivo_baixa));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,_t006_nr_art_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_obrigacoes_sociais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_obrigacoes_sociais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_fundo_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_fundo_social));

                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cnpj_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cnpj_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_tipo_propriedade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_tipo_propriedade));
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
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		t005_nr_protocolo, ");
            Sql.AppendLine("		t006_nr_fundadores_diretores, ");
            Sql.AppendLine("		t006_ds_artigo_estatuto, ");
            //Sql.AppendLine("		t006_in_forma_convocacao, ");
            Sql.AppendLine("        t006_edital_fixado_sede, ");
            Sql.AppendLine("        t006_edital_publicado_jornal, ");
            Sql.AppendLine("        t006_edital_outros, ");
            Sql.AppendLine("		t006_nr_art_estatuto_convocacao, ");
            Sql.AppendLine("		t006_ds_quorum_deliberacao, ");
            Sql.AppendLine("		t006_ds_quorum_alteracao, ");
            Sql.AppendLine("		t006_ds_quorum_dissolucao, ");
            Sql.AppendLine("		t006_ds_outro_quorum_dissolucao, ");
            Sql.AppendLine("		t006_ds_outro_quorum_alteracao, ");
            Sql.AppendLine("		t006_ds_outro_quorum_deliberacao, ");
            Sql.AppendLine("		t006_ds_destino_patrimonio, ");
            Sql.AppendLine("		t006_in_obrigacoes_sociais, ");
            Sql.AppendLine("		t006_in_possui_fundo_social, ");
            Sql.AppendLine("		t006_recurso_mensalidade, ");
            Sql.AppendLine("		t006_recurso_doacao, ");
            Sql.AppendLine("		t006_recurso_governamental, ");
            Sql.AppendLine("		t006_nr_art_estatuto_associacao, ");
            Sql.AppendLine("		t006_ds_nome_advogado, ");
            Sql.AppendLine("		t006_nr_cpf_advogado, ");
            Sql.AppendLine("		t006_nr_inscr_oab, ");
            Sql.AppendLine("        t006_ds_uf_oab_advogado, ");
            Sql.AppendLine("		t006_ds_nome_contador, ");
            Sql.AppendLine("		t006_nr_cpf_contador, ");
            Sql.AppendLine("		a004_co_uf, ");
            Sql.AppendLine("		t006_nr_crc_contador, ");
            Sql.AppendLine("        t006_nr_num_vias, ");
            Sql.AppendLine("		t006_nr_num_paginas, ");
            Sql.AppendLine("		t006_in_ata_mesmo_instrumento, ");
            Sql.AppendLine("		t006_nire, ");
            Sql.AppendLine("		t006_dt_decretacao_falencia, ");
            Sql.AppendLine("		t006_dt_interrupcao_atividade, ");
            Sql.AppendLine("		t006_dt_inicio_liquidacao, ");
            Sql.AppendLine("		t006_dt_reinicio_atividades, ");
            Sql.AppendLine("		t006_dt_termino_liquidacao, ");
            Sql.AppendLine("		a021_co_motivo_baixa, ");
            Sql.AppendLine("        t006_nr_art_deliberacao, ");
            Sql.AppendLine("        t006_nr_art_alteracao, ");
            Sql.AppendLine("        t006_nr_art_dissolucao, ");
            Sql.AppendLine("        t006_nr_art_obrigacoes_sociais, ");
            Sql.AppendLine("        t006_nr_art_fundo_social, ");
            Sql.AppendLine("		t006_nr_sucessora, ");
            Sql.AppendLine("		t006_nr_cnpj_sucessora, ");
            Sql.AppendLine("		t006_tipo_propriedade ");
            Sql.AppendLine(" From	T006_Protocolo_Requerimento");
            Sql.AppendLine(" Where	1 = 1 ");

            Sql.AppendLine(" And	t005_nr_protocolo ='" + _t005_nr_protocolo + "'");
            

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T006_Protocolo_Requerimento");
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

        public void UpdateArtigoEstatuto()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("Update t006_Protocolo_Requerimento Set t006_ds_artigo_estatuto = '" + _t006_ds_artigo_estatuto + "' ");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine("	and	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T006_Protocolo_Requerimento");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;
            try
            {
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
        public string QueryArtigoEstatuto()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("Select t006_ds_artigo_estatuto from t006_Protocolo_Requerimento ");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine("	and	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T006_Protocolo_Requerimento");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["t006_ds_artigo_estatuto"].ToString();
                else
                    return "";
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

          Sql.AppendLine(" select     ");
          Sql.AppendLine("   proReq.T004_NR_CNPJ_ORG_REG as CNPJ_RCPJ,    ");
          Sql.AppendLine("   proReq.T005_NR_PROTOCOLO as ProtocoloRequerimento,    ");
          Sql.AppendLine(" protocolo.T001_SQ_PESSOA as SequencialPessoa,   ");
          Sql.AppendLine(" protocolo.T005_DT_ENTRADA as DataEntrada,   ");
          Sql.AppendLine(" protocolo.T005_DT_AVERBACAO as DataAverbacao,   ");
          Sql.AppendLine(" protocolo.T005_NR_PROTOCOLO_VIABILIDADE as NumeroProtocoloViabilidade,   ");
          Sql.AppendLine(" protocolo.T005_NR_DOCAD as NumeroDocad,   ");
          Sql.AppendLine(" protocolo.T005_NR_DBE as NumeroDbe,   ");
          Sql.AppendLine(" protocolo.T005_VALOR_PROTOCOLO as ValorProtocolo,   ");
          Sql.AppendLine(" proReq.T006_NR_FUNDADORES_DIRETORES as FundadoresDiretores,    ");
          Sql.AppendLine(" proReq.T006_DS_ARTIGO_ESTATUTO as ArtigoEstatuto,    ");
          Sql.AppendLine(" proReq.T006_IN_FORMA_CONVOCACAO as FormaConvocacao,    ");
          Sql.AppendLine(" proReq.T006_NR_ART_ESTATUTO_CONVOCACAO as ArtEstatutoConvocacao,    ");
          Sql.AppendLine(" proReq.T006_DS_QUORUM_DELIBERACAO as QuorumDeliberacao,    ");
          Sql.AppendLine(" proReq.T006_DS_QUORUM_ALTERACAO as QuorumAlteracao,    ");
          Sql.AppendLine(" proReq.T006_DS_QUORUM_DISSOLUCAO as QuorumDissolucao,    ");
          Sql.AppendLine(" proReq.T006_DS_DESTINO_PATRIMONIO as DestinoPatrimonio,    ");
          Sql.AppendLine(" proReq.T006_IN_OBRIGACOES_SOCIAIS as ObrigacoesSociais,    ");
          Sql.AppendLine(" proReq.T006_IN_POSSUI_FUNDO_SOCIAL as PossuiFundoSocial,    ");
          Sql.AppendLine(" proReq.T006_RECURSO_MENSALIDADE as RecursoMensalidade,    ");
          Sql.AppendLine(" proReq.T006_RECURSO_DOACAO as RecursoDoacao,    ");
          Sql.AppendLine(" proReq.T006_RECURSO_GOVERNAMENTAL as RecursoGovernamental,    ");
          Sql.AppendLine(" proReq.T006_NR_ART_ESTATUTO_ASSOCIACAO as EstatutoAssociacao,    ");
          Sql.AppendLine(" proReq.T006_DS_NOME_ADVOGADO as NomeAdvogado,    ");
          Sql.AppendLine(" proReq.T006_NR_CPF_ADVOGADO as CPFAdvogado,    ");
          Sql.AppendLine(" proReq.T006_NR_INSCR_OAB as InscricaoAob,    ");
          Sql.AppendLine(" proReq.T006_DS_NOME_CONTADOR as NomeContador,    ");
          Sql.AppendLine(" proReq.T006_NR_CPF_CONTADOR as CpfContador,    ");
          Sql.AppendLine(" proReq.A004_CO_UF as UnidadeFederal,    ");
          Sql.AppendLine(" proReq.T006_NR_CRC_CONTADOR as CrcContador,    ");
          Sql.AppendLine(" proReq.t006_nr_num_vias as NumVias,   ");
          Sql.AppendLine(" proReq.T006_NR_NUM_PAGINAS as NumeroPaginas,    ");
          Sql.AppendLine(" proReq.T006_IN_ATA_MESMO_INSTRUMENTO as AtaMesmoInstrumento,    ");
          Sql.AppendLine(" proReq.T006_NIRE as Nire,    ");
          Sql.AppendLine(" proReq.T006_DT_DECRETACAO_FALENCIA as DataDecretacaoFalencia,    ");
          Sql.AppendLine(" proReq.T006_DT_INTERRUPCAO_ATIVIDADE as DataInterrupcaoAtividade,    ");
          Sql.AppendLine(" proReq.T006_DT_INICIO_LIQUIDACAO as DataInicioLiquidacao,    ");
          Sql.AppendLine(" proReq.T006_DT_REINICIO_ATIVIDADES as DataReinicioAtividades,    ");
          Sql.AppendLine(" proReq.T006_DT_TERMINO_LIQUIDACAO as DataTerminoLiquidacao,    ");
          Sql.AppendLine(" proReq.A021_CO_MOTIVO_BAIXA as MotivoBaixa,    ");
          Sql.AppendLine(" proReq.T006_NR_SUCESSORA as NrSucessora,    ");
          Sql.AppendLine(" proReq.T006_NR_CNPJ_SUCESSORA as NrCnpjSucessora    ");
          Sql.AppendLine(" from t006_protocolo_requerimento proReq, t005_protocolo   protocolo    ");
          Sql.AppendLine(" where 1=1    ");
          Sql.AppendLine(" and  proReq.T005_NR_PROTOCOLO = '" + pProtocolo + "'");
          Sql.AppendLine(" and  proReq.T005_NR_PROTOCOLO = protocolo.T005_NR_PROTOCOLO" );

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("ProtocoloRequerimento");
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
        public void UpdateAdvogado()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            
            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T006_Protocolo_Requerimento Set ");
            //SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            //SqlU.AppendLine("		t005_nr_protocolo = @v_t005_nr_protocolo, ");
            SqlU.AppendLine("		t006_ds_nome_advogado = @v_t006_ds_nome_advogado, ");
            SqlU.AppendLine("		t006_nr_cpf_advogado = @v_t006_nr_cpf_advogado, ");
            SqlU.AppendLine("		t006_nr_inscr_oab = @v_t006_nr_inscr_oab, ");
            SqlU.AppendLine("       t006_ds_uf_oab_advogado = @v_t006_ds_uf_oab_advogado, ");
            SqlU.AppendLine("       t006_nr_num_paginas = @v_t006_nr_num_paginas, ");
            SqlU.AppendLine("       t006_contrato_padrao = @v_t006_contrato_padrao, ");
            SqlU.AppendLine("       T006_NR_NUM_VIAS = @v_T006_NR_NUM_VIAS   ");

         
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine("	and	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");

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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_fundadores_diretores", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_fundadores_diretores));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_artigo_estatuto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_artigo_estatuto));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_forma_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_forma_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_convocacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_convocacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_deliberacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_deliberacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_alteracao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_quorum_dissolucao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_quorum_dissolucao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_destino_patrimonio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_destino_patrimonio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_obrigacoes_sociais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_obrigacoes_sociais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_possui_fundo_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_possui_fundo_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_mensalidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_mensalidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_doacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_doacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_recurso_governamental", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_recurso_governamental));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_art_estatuto_associacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_art_estatuto_associacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_inscr_oab", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_inscr_oab));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_uf_oab_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a004_co_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a004_co_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_crc_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_crc_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_num_vias", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_num_vias));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_num_paginas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_num_paginas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_in_ata_mesmo_instrumento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_in_ata_mesmo_instrumento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nire));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_decretacao_falencia", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_decretacao_falencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_interrupcao_atividade", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_interrupcao_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_inicio_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_inicio_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_reinicio_atividades", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_reinicio_atividades));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_dt_termino_liquidacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_dt_termino_liquidacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a021_co_motivo_baixa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a021_co_motivo_baixa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cnpj_sucessora", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cnpj_sucessora));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_contrato_padrao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_contrato_padrao));

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

        public void UpdateAdvogado_new()
        {
            StringBuilder SqlU = new StringBuilder();


            // Codigo Update ******************* 
            SqlU.AppendLine(" Update  T006_Protocolo_Requerimento Set ");
            SqlU.AppendLine("		  t006_ds_nome_advogado   = @v_t006_ds_nome_advogado, ");
            SqlU.AppendLine("		  t006_nr_cpf_advogado    = @v_t006_nr_cpf_advogado, ");
            SqlU.AppendLine("		  t006_nr_inscr_oab       = @v_t006_nr_inscr_oab, ");
            SqlU.AppendLine("         t006_ds_uf_oab_advogado = @v_t006_ds_uf_oab_advogado ");
            SqlU.AppendLine(" Where	t005_nr_protocolo   = '" + _t005_nr_protocolo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_nome_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_nome_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_cpf_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_cpf_advogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_nr_inscr_oab", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_nr_inscr_oab));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t006_ds_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t006_ds_uf_oab_advogado));

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
                //    cmdToExecute.CommandText = SqlI.ToString();
                //    cmdToExecute.ExecuteNonQuery();
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


