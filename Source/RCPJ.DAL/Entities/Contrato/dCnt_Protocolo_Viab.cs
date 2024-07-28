using System;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_Protocolo_Viab : DBInteractionBase
    {


        //TODO: c.`NOME_ADVOG`, c.`OAB_ADVOGADO`, c.`UF_OAB_ADVOGADO`

        // Variables ******************* 
        #region  Property Declarations
        private string _cod_protocolo;
        private string _reque_protocolo;

        private DateTime _fec_solicitud;
        private string _tmu_tuf_uf;
        private decimal _tmu_cod_mun;
        private string _vsv_cpf_cnpj_solic;
        private decimal _tip_registro;
        private string _ttl_tip_logradoro;
        private string _logradoro;
        private string _bairro;
        private string _cep;
        private string _nom_solic;
        private string _email_solic;
        private string _num_logradouro;
        private string _comp_logradouro;
        private string _objetivo;
        private decimal _area;
        private decimal _iptu;
        private decimal _status_proc;
        private decimal _tipo_inscricao;
        private string _cnpj_matriz;
        private decimal _vistoria_cnae;
        private decimal _vistoria_encerrada;
        private string _juztificativa_nome;
        private string _refer;
        private string _error_processo;
        private string _observacoes;
        private string _cnpj;
        private string _nire;
        private string _inscrmunicipal;
        private string _viabilidade;
        private string _inscrestadual;
        private string _uf_origem;
        private string _cnpj_cartorio;
        private string _protocolo_cartorio;
        private string _vrs_razao_social;
        private decimal _capital_social;
        private decimal _qtd_cotas;
        private decimal _valor_cota;
        private DateTime _data_inicio_ativ;
        private string _foro;
        private string _nire_filial;
        private string _codigoDBE;
        private string _codigoDOCAD;
        private string _telefone;
        private string _tipoEnquadramento;
        private string _nomeAdvog;
        private string _oabAdvogado;
        private string _ufOabAdvogado;

        private string _tipoOrgaoRegistro;
        private bool _requerimentoNovo = false;

        private string _num_vias_contrato;

        


        

        #endregion

        #region Class Member Declarations



        /// <summary>
        /// Gets or sets a value indicating whether [requerimento novo]. false para indicar que não é novo
        /// </summary>
        /// <value><c>true</c> if [requerimento novo]; otherwise, <c>false</c>.</value>
        public bool requerimentoNovo
        {
            get { return _requerimentoNovo; }
            set { _requerimentoNovo = value; }
        }

        public string cod_protocolo
        {
            get { return _cod_protocolo; }

            set { _cod_protocolo = value; }
        }

        public string reque_protocolo
        {
            get { return _reque_protocolo; }
            set { _reque_protocolo = value; }
        }

        public DateTime fec_solicitud
        {
            get { return _fec_solicitud; }

            set { _fec_solicitud = value; }
        }

        public string tmu_tuf_uf
        {
            get { return _tmu_tuf_uf; }

            set { _tmu_tuf_uf = value; }
        }

        public decimal tmu_cod_mun
        {
            get { return _tmu_cod_mun; }

            set { _tmu_cod_mun = value; }
        }

        public string vsv_cpf_cnpj_solic
        {
            get { return _vsv_cpf_cnpj_solic; }

            set { _vsv_cpf_cnpj_solic = value; }
        }

        public decimal tip_registro
        {
            get { return _tip_registro; }

            set { _tip_registro = value; }
        }

        public string ttl_tip_logradoro
        {
            get { return _ttl_tip_logradoro; }

            set { _ttl_tip_logradoro = value; }
        }

        public string logradoro
        {
            get { return _logradoro; }

            set { _logradoro = value; }
        }

        public string bairro
        {
            get { return _bairro; }

            set { _bairro = value; }
        }

        public string cep
        {
            get { return _cep; }

            set { _cep = value; }
        }

        public string nom_solic
        {
            get { return _nom_solic; }

            set { _nom_solic = value; }
        }

        public string email_solic
        {
            get { return _email_solic; }

            set { _email_solic = value; }
        }

        public string num_logradouro
        {
            get { return _num_logradouro; }

            set { _num_logradouro = value; }
        }

        public string comp_logradouro
        {
            get { return _comp_logradouro; }

            set { _comp_logradouro = value; }
        }

        public string objetivo
        {
            get { return _objetivo; }

            set { _objetivo = value; }
        }

        public decimal area
        {
            get { return _area; }

            set { _area = value; }
        }

        public decimal iptu
        {
            get { return _iptu; }

            set { _iptu = value; }
        }

        public decimal status_proc
        {
            get { return _status_proc; }

            set { _status_proc = value; }
        }

        public decimal tipo_inscricao
        {
            get { return _tipo_inscricao; }

            set { _tipo_inscricao = value; }
        }

        public string cnpj_matriz
        {
            get { return _cnpj_matriz; }

            set { _cnpj_matriz = value; }
        }

        public decimal vistoria_cnae
        {
            get { return _vistoria_cnae; }

            set { _vistoria_cnae = value; }
        }

        public decimal vistoria_encerrada
        {
            get { return _vistoria_encerrada; }

            set { _vistoria_encerrada = value; }
        }

        public string juztificativa_nome
        {
            get { return _juztificativa_nome; }

            set { _juztificativa_nome = value; }
        }

        public string refer
        {
            get { return _refer; }

            set { _refer = value; }
        }

        public string error_processo
        {
            get { return _error_processo; }

            set { _error_processo = value; }
        }

        public string observacoes
        {
            get { return _observacoes; }

            set { _observacoes = value; }
        }

        public string cnpj
        {
            get { return _cnpj; }

            set { _cnpj = value; }
        }

        public string nire
        {
            get { return _nire; }

            set { _nire = value; }
        }

        public string inscrmunicipal
        {
            get { return _inscrmunicipal; }

            set { _inscrmunicipal = value; }
        }

        public string viabilidade
        {
            get { return _viabilidade; }

            set { _viabilidade = value; }
        }

        public string inscrestadual
        {
            get { return _inscrestadual; }

            set { _inscrestadual = value; }
        }

        public string uf_origem
        {
            get { return _uf_origem; }

            set { _uf_origem = value; }
        }

        public string cnpj_cartorio
        {
            get { return _cnpj_cartorio; }

            set { _cnpj_cartorio = value; }
        }

        public string protocolo_cartorio
        {
            get { return _protocolo_cartorio; }

            set { _protocolo_cartorio = value; }
        }

        public string vrs_razao_social
        {
            get { return _vrs_razao_social; }

            set { _vrs_razao_social = value; }
        }

        public decimal capital_social
        {
            get { return _capital_social; }

            set { _capital_social = value; }
        }

        public decimal qtd_cotas
        {
            get { return _qtd_cotas; }

            set { _qtd_cotas = value; }
        }

        public decimal valor_cota
        {
            get { return _valor_cota; }

            set { _valor_cota = value; }
        }

        public DateTime data_inicio_ativ
        {
            get { return _data_inicio_ativ; }

            set { _data_inicio_ativ = value; }
        }

        public string foro
        {
            get { return _foro; }

            set { _foro = value; }
        }

        public string nire_filial
        {
            get { return _nire_filial; }

            set { _nire_filial = value; }
        }


        public string codigoDBE
        {
            get { return _codigoDBE; }

            set { _codigoDBE = value; }
        }
        public string codigoDOCAD
        {
            get { return _codigoDOCAD; }

            set { _codigoDOCAD = value; }
        }
        public string telefone
        {
            get { return _telefone; }

            set { _telefone = value; }
        }
        public string tipoEnquadramento
        {
            get { return _tipoEnquadramento; }

            set { _tipoEnquadramento = value; }
        }

        public string nomeAdvog
        {
            get { return _nomeAdvog; }
            set { _nomeAdvog = value; }
        }

        public string oabAdvogado
        {
            get { return _oabAdvogado; }
            set { _oabAdvogado = value; }
        }


        public string ufOabAdvogado
        {
            get { return _ufOabAdvogado; }
            set { _ufOabAdvogado = value; }
        }

        public string tipoOrgaoRegistro
        {
            get { return _tipoOrgaoRegistro; }
            set { _tipoOrgaoRegistro = value; }
        }

        public string num_vias_contrato
        {
            get { return _num_vias_contrato; }
            set { _num_vias_contrato = value; }
        }

        #endregion


        #region Implements
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_protocolo_viab");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	cod_protocolo, ");
            SqlI.AppendLine("	reque_protocolo, ");
            SqlI.AppendLine("	fec_solicitud, ");
            SqlI.AppendLine("	tmu_tuf_uf, ");
            SqlI.AppendLine("	tmu_cod_mun, ");
            SqlI.AppendLine("	vsv_cpf_cnpj_solic, ");
            SqlI.AppendLine("	tip_registro, ");//
            SqlI.AppendLine("	ttl_tip_logradoro, ");
            SqlI.AppendLine("	logradoro, ");
            SqlI.AppendLine("	bairro, ");
            SqlI.AppendLine("	cep, ");
            SqlI.AppendLine("	nom_solic, ");
            SqlI.AppendLine("	email_solic, ");
            SqlI.AppendLine("	num_logradouro, ");
            SqlI.AppendLine("	comp_logradouro, ");
            SqlI.AppendLine("	objetivo, ");
         //   SqlI.AppendLine("	area, ");
            SqlI.AppendLine("	iptu, ");
          //  SqlI.AppendLine("	status_proc, ");
            SqlI.AppendLine("	tipo_inscricao, ");
            SqlI.AppendLine("	cnpj_matriz, ");
           // SqlI.AppendLine("	vistoria_cnae, ");
          //  SqlI.AppendLine("	vistoria_encerrada, ");
           // SqlI.AppendLine("	juztificativa_nome, ");
            SqlI.AppendLine("	refer, ");
         //   SqlI.AppendLine("	error_processo, ");
            SqlI.AppendLine("	observacoes, ");
            SqlI.AppendLine("	cnpj, ");
            //SqlI.AppendLine("	nire, ");
           // SqlI.AppendLine("	inscrmunicipal, ");
            SqlI.AppendLine("	viabilidade, ");
           // SqlI.AppendLine("	inscrestadual, ");
            SqlI.AppendLine("	uf_origem, ");
           // SqlI.AppendLine("	cnpj_cartorio, ");
            SqlI.AppendLine("	protocolo_cartorio, ");
            SqlI.AppendLine("	vrs_razao_social, ");
            SqlI.AppendLine("	capital_social, ");
            SqlI.AppendLine("	qtd_cotas, ");
            SqlI.AppendLine("	valor_cota, ");
            SqlI.AppendLine("	data_inicio_ativ, ");
            SqlI.AppendLine("	foro, ");
           // SqlI.AppendLine("	nire_filial,");
            SqlI.AppendLine("	cod_dbe,");
         //   SqlI.AppendLine("	cod_docad,");
            SqlI.AppendLine("	telefone,");
            SqlI.AppendLine("	tipo_enquadramento,");
            SqlI.AppendLine("	nome_advog,");
            SqlI.AppendLine("	oab_advogado,");
            SqlI.AppendLine("	uf_oab_advogado");            
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_cod_protocolo, ");
            SqlI.AppendLine("	@v_reque_protocolo, ");
            SqlI.AppendLine("	@v_fec_solicitud, ");
            SqlI.AppendLine("	@v_tmu_tuf_uf, ");
            SqlI.AppendLine("	@v_tmu_cod_mun, ");
            SqlI.AppendLine("	@v_vsv_cpf_cnpj_solic, ");
            SqlI.AppendLine("	@v_tip_registro, ");
            SqlI.AppendLine("	@v_ttl_tip_logradoro, ");
            SqlI.AppendLine("	@v_logradoro, ");
            SqlI.AppendLine("	@v_bairro, ");
            SqlI.AppendLine("	@v_cep, ");
            SqlI.AppendLine("	@v_nom_solic, ");
            SqlI.AppendLine("	@v_email_solic, ");
            SqlI.AppendLine("	@v_num_logradouro, ");
            SqlI.AppendLine("	@v_comp_logradouro, ");
            SqlI.AppendLine("	@v_objetivo, ");
           // SqlI.AppendLine("	@v_area, ");
            SqlI.AppendLine("	@v_iptu, ");
          //  SqlI.AppendLine("	@v_status_proc, ");
            SqlI.AppendLine("	@v_tipo_inscricao, ");
            SqlI.AppendLine("	@v_cnpj_matriz, ");
           // SqlI.AppendLine("	@v_vistoria_cnae, ");
            //SqlI.AppendLine("	@v_vistoria_encerrada, ");
           // SqlI.AppendLine("	@v_juztificativa_nome, ");
            SqlI.AppendLine("	@v_refer, ");
          //  SqlI.AppendLine("	@v_error_processo, ");
            SqlI.AppendLine("	@v_observacoes, ");
            SqlI.AppendLine("	@v_cnpj, ");
           // SqlI.AppendLine("	@v_nire, ");
           // SqlI.AppendLine("	@v_inscrmunicipal, ");
            SqlI.AppendLine("	@v_viabilidade, ");
           // SqlI.AppendLine("	@v_inscrestadual, ");
            SqlI.AppendLine("	@v_uf_origem, ");
           // SqlI.AppendLine("	@v_cnpj_cartorio, ");
            SqlI.AppendLine("	@v_protocolo_cartorio, ");
            SqlI.AppendLine("	@v_vrs_razao_social, ");
            SqlI.AppendLine("	@v_capital_social, ");
            SqlI.AppendLine("	@v_qtd_cotas, ");
            SqlI.AppendLine("	@v_valor_cota, ");
            SqlI.AppendLine("	@v_data_inicio_ativ, ");
            SqlI.AppendLine("	@v_foro, ");
          //  SqlI.AppendLine("	@v_nire_filial,");
            SqlI.AppendLine("	@v_cod_dbe,");
           // SqlI.AppendLine("	@v_cod_docad,");
            SqlI.AppendLine("	@v_telefone,");
            SqlI.AppendLine("	@v_tipo_enqu,");
            SqlI.AppendLine("	@v_nome_advog,");
            SqlI.AppendLine("	@v_oab_Advogado,");
            SqlI.AppendLine("	@v_uf_oab_advogado");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_protocolo_viab Set ");
            SqlU.AppendLine("		cod_protocolo = @v_cod_protocolo, ");
            SqlU.AppendLine("		reque_protocolo = @v_reque_protocolo, ");
            SqlU.AppendLine("		fec_solicitud = @v_fec_solicitud, ");
            SqlU.AppendLine("		tmu_tuf_uf = @v_tmu_tuf_uf, ");
            SqlU.AppendLine("		tmu_cod_mun = @v_tmu_cod_mun, ");
            SqlU.AppendLine("		vsv_cpf_cnpj_solic = @v_vsv_cpf_cnpj_solic, ");
            SqlU.AppendLine("		tip_registro = @v_tip_registro, ");
            SqlU.AppendLine("		ttl_tip_logradoro = @v_ttl_tip_logradoro, ");
            SqlU.AppendLine("		logradoro = @v_logradoro, ");
            SqlU.AppendLine("		bairro = @v_bairro, ");
            SqlU.AppendLine("		cep = @v_cep, ");
            SqlU.AppendLine("		nom_solic = @v_nom_solic, ");
            SqlU.AppendLine("		email_solic = @v_email_solic, ");
            SqlU.AppendLine("		num_logradouro = @v_num_logradouro, ");
            SqlU.AppendLine("		comp_logradouro = @v_comp_logradouro, ");
            SqlU.AppendLine("		objetivo = @v_objetivo, ");
         //   SqlU.AppendLine("		area = @v_area, ");
            SqlU.AppendLine("		iptu = @v_iptu, ");
           // SqlU.AppendLine("		status_proc = @v_status_proc, ");
            SqlU.AppendLine("		tipo_inscricao = @v_tipo_inscricao, ");
            SqlU.AppendLine("		cnpj_matriz = @v_cnpj_matriz, ");
          //  SqlU.AppendLine("		vistoria_cnae = @v_vistoria_cnae, ");
          //  SqlU.AppendLine("		vistoria_encerrada = @v_vistoria_encerrada, ");
           // SqlU.AppendLine("		juztificativa_nome = @v_juztificativa_nome, ");
            SqlU.AppendLine("		refer = @v_refer, ");
           // SqlU.AppendLine("		error_processo = @v_error_processo, ");
            SqlU.AppendLine("		observacoes = @v_observacoes, ");
            SqlU.AppendLine("		cnpj = @v_cnpj, ");
            //SqlU.AppendLine("		nire = @v_nire, ");
           // SqlU.AppendLine("		inscrmunicipal = @v_inscrmunicipal, ");
            SqlU.AppendLine("		viabilidade = @v_viabilidade, ");
          //  SqlU.AppendLine("		inscrestadual = @v_inscrestadual, ");
            SqlU.AppendLine("		uf_origem = @v_uf_origem, ");
           // SqlU.AppendLine("		cnpj_cartorio = @v_cnpj_cartorio, ");
            SqlU.AppendLine("		protocolo_cartorio = @v_protocolo_cartorio, ");
            SqlU.AppendLine("		vrs_razao_social = @v_vrs_razao_social, ");
            SqlU.AppendLine("		capital_social = @v_capital_social, ");
            SqlU.AppendLine("		qtd_cotas = @v_qtd_cotas, ");
            SqlU.AppendLine("		valor_cota = @v_valor_cota, ");
            SqlU.AppendLine("		data_inicio_ativ = @v_data_inicio_ativ, ");
            SqlU.AppendLine("		foro = @v_foro, ");
            //SqlU.AppendLine("		nire_filial = @v_nire_filial,");
            SqlU.AppendLine("	    cod_dbe = @v_cod_dbe,");
          //  SqlU.AppendLine("	    cod_docad = @v_cod_docad,");
            SqlU.AppendLine("	    telefone = @v_telefone,");
            SqlU.AppendLine("	    tipo_enquadramento = @v_tipo_enqu,");
            SqlU.AppendLine("	    nome_advog = @v_nome_advog,");
            SqlU.AppendLine("	    oab_advogado = @v_oab_Advogado,");
            SqlU.AppendLine("	    uf_oab_advogado = @v_uf_oab_advogado");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!
            SqlU.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");            


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reque_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_fec_solicitud", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _fec_solicitud));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_tuf_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_tuf_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_mun", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_mun));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_vsv_cpf_cnpj_solic", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _vsv_cpf_cnpj_solic));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tip_registro", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tip_registro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_ttl_tip_logradoro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ttl_tip_logradoro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_logradoro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _logradoro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nom_solic", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nom_solic));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_email_solic", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _email_solic));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_comp_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comp_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_objetivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _objetivo));
             //   cmdToExecute.Parameters.Add(new MySqlParameter("v_area", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _area));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_iptu", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _iptu));
            //    cmdToExecute.Parameters.Add(new MySqlParameter("v_status_proc", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _status_proc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_inscricao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_inscricao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cnpj_matriz", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cnpj_matriz));
              //  cmdToExecute.Parameters.Add(new MySqlParameter("v_vistoria_cnae", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _vistoria_cnae));
              //  cmdToExecute.Parameters.Add(new MySqlParameter("v_vistoria_encerrada", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _vistoria_encerrada));
              //  cmdToExecute.Parameters.Add(new MySqlParameter("v_juztificativa_nome", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _juztificativa_nome));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_refer", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _refer));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_error_processo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _error_processo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_observacoes", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _observacoes));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cnpj));
               // cmdToExecute.Parameters.Add(new MySqlParameter("v_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nire));
               // cmdToExecute.Parameters.Add(new MySqlParameter("v_inscrmunicipal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _inscrmunicipal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _viabilidade));
               // cmdToExecute.Parameters.Add(new MySqlParameter("v_inscrestadual", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _inscrestadual));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf_origem));
               // cmdToExecute.Parameters.Add(new MySqlParameter("v_cnpj_cartorio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cnpj_cartorio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_protocolo_cartorio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _protocolo_cartorio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_vrs_razao_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _vrs_razao_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_capital_social", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _capital_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_qtd_cotas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _qtd_cotas));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_valor_cota", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _valor_cota));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_data_inicio_ativ", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _data_inicio_ativ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_foro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _foro));
              //  cmdToExecute.Parameters.Add(new MySqlParameter("v_nire_filial", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nire_filial));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_dbe", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _codigoDBE));
              //  cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_docad", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _codigoDOCAD));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_telefone", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _telefone));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_enqu", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipoEnquadramento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_advog", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nomeAdvog));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_oab_Advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _oabAdvogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ufOabAdvogado));
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

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <returns></returns>
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		* ");
            Sql.AppendLine(" From	vw_dcnt_protocolo_viab");
            Sql.AppendLine(" Where	1 = 1 ");

            if (_cod_protocolo != "")
                Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            Sql.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("vw_dcnt_protocolo_viab");
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


        public void UpdateAdvogadoViasSocios()
        {
            //StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

           
            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     vw_cnt_protocolo_viab Set ");        
            SqlU.AppendLine("	    nome_advog = @v_nome_advog,");
            SqlU.AppendLine("	    oab_advogado = @v_oab_Advogado,");
            SqlU.AppendLine("	    uf_oab_advogado = @v_uf_oab_advogado,");
            SqlU.AppendLine("	    NUM_VIAS_CONTRATO = @v_num_vias_contrato");
            SqlU.AppendLine(" Where	1 = 1 ");

           
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
               cmdToExecute.Parameters.Add(new MySqlParameter("v_nome_advog", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nomeAdvog));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_oab_Advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _oabAdvogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf_oab_advogado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ufOabAdvogado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_vias_contrato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_vias_contrato));
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
                   // cmdToExecute.CommandText = SqlI.ToString();
                  //  cmdToExecute.ExecuteNonQuery();
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
        


        /// <summary>
        /// Obtem o protocolo caso ele exista na base e caso ele não exista o sistema vai gerar um.
        /// </summary>
        /// <returns></returns>
        public string ObterProtocolo()
        {
            StringBuilder Sql = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();
            
            Sql.AppendLine(" Select ");
            Sql.AppendLine("		cod_protocolo, ");
            Sql.AppendLine("		reque_protocolo ");
            Sql.AppendLine(" From	vw_cnt_protocolo_viab");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            Sql.AppendLine(" And	reque_protocolo is not null");



        



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("vw_cnt_protocolo_viab");
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
                {
                    if (toReturn.Rows[0]["reque_protocolo"].ToString() == "")
                    {
                        _requerimentoNovo = true;
                        GerarProtocolo();
                        if (_reque_protocolo != String.Empty)
                        {
                            SqlU.AppendLine(" Update     vw_cnt_protocolo_viab Set ");
                            SqlU.AppendLine("	    reque_protocolo = '" + _reque_protocolo + "' ");
                            SqlU.AppendLine(" Where	1 = 1 ");
                            SqlU.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
                            cmdToExecute.CommandText = SqlU.ToString();
                            if (_mainConnection.State != ConnectionState.Open)
                            {
                                _mainConnection.Open();
                            }
                            cmdToExecute.ExecuteNonQuery();
                            return _reque_protocolo;
                        }
                    }
                    else
                    {
                        _reque_protocolo = toReturn.Rows[0]["reque_protocolo"].ToString();
                        return _reque_protocolo;
                    }
                }
                else {
                    _requerimentoNovo = true;
                    GerarProtocolo();
                    if (_reque_protocolo != String.Empty)
                    {
                        SqlU.AppendLine(" Update     vw_cnt_protocolo_viab Set ");
                        SqlU.AppendLine("	    reque_protocolo = '" + _reque_protocolo + "' ");
                        SqlU.AppendLine(" Where	1 = 1 ");
                        SqlU.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
                        cmdToExecute.CommandText = SqlU.ToString();
                        if (_mainConnection.State != ConnectionState.Open)
                        {
                            _mainConnection.Open();
                        }
                        cmdToExecute.ExecuteNonQuery();
                        return _reque_protocolo;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return string.Empty;
                throw ex;
            }
            finally
            {
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
              
            }
        }
        /// <summary>
        /// Gerars the protocolo.
        /// </summary>
        public void GerarProtocolo()
        {

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "contrato.GetNumeroCorrelativo";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("pTipo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, "80"));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, ""));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection. 
                    if (_mainConnection.State != ConnectionState.Open)
                    {
                        _mainConnection.Open();
                    }
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                reque_protocolo = cmdToExecute.Parameters["v_reque_protocolo"].Value.ToString();
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