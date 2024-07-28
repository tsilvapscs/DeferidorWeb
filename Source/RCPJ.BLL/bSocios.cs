using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using System.Globalization;
namespace RCPJ.BLL
{
    [Serializable]
    public class bSocios : DBInteractionBase 
    {
        #region Class Member Declarations
        private bEndereco _endereco = new bEndereco();
        private List<bSocios> _RepresentantesAtivos = new List<bSocios>();
        private String _SQPessoa;
        private int _SQPessoaPai;
        private String _ObrigacoesSociais;
        private String _TipoPessoa;
        private String _Qualificacao;
        private String _Qualificacao_Descricao;
        private String _Fundador;
        private String _Email;
        private String _Nome;
        private string _nome_pai;
        private string _nome_mae;
        private String _Mae;
        private String _CPFCNPJ = "";
        private String _TipoIdentidade;
        private String _OrgaoExpedidor;
        private String _OrgaoExpedidorUF;
        private String _RG;
        private String _Nacionalidade;
        private String _Profissao;
        private String _Profissao_Descricao;
        private decimal _QuotaCapitalSocial = 0;
        private decimal _AporteSocio = 0;
        private decimal _CapitalIntegralizado = 0;
        private decimal _Capital_a_Integralizar = 0;
        private Nullable<DateTime> _DataIntegralizacao;
        private Nullable<DateTime> _r001_dt_entrada_vinculo;
        private Nullable<DateTime> _r001_dt_saida_vinculo;
        
        private String _EndCEP;
        private String _EndPais;
        private String _EndUF;
        private String _EndMunicipio;
        private String _EndMunicipioDescricao;
        private String _EndBairro;
        private String _EndTipoLogradouro;
        private string _EndDsTipoLogradouro;
        private String _EndLogradouro;
        private String _EndNumero;
        private String _EndComplemento;
        private String _DDD;
        private String _Telefone;

        private String _EstadoCivil;
        private String _EstadoCivilDescricao;
        private String _EstadoCivilRegime;
        private String _RegimeBens;
        private String _TipoAssistido;
        private String _TipoEmancipado;
        private string _TipoEmancipadoDS;
        private String _NaturalidadeCodigo;
        private String _in_Sexo;
        private string _Analfabeto;
        private Nullable<DateTime> _DataNascimento;
        private int _NacionalidadeCodigo;
        private bool _Valido;
        private int _flagNovoEndereco;
        private int _rep_legal;
        private string _tipo_visto;
        private Nullable<DateTime> _dt_emissao_visto;
        private Nullable<DateTime> _dt_validade_visto;
        private List<bSocios> _Representantes = new List<bSocios>();

        private Nullable<DateTime> _DataInicioMandato;
        private Nullable<DateTime> _DataTerminoMandato;

        private string _representante;
        private string _Situacao = "A";
        private string _justificativa_visto;
        private int _tipoacao = 3;
        private string _descrAcao = string.Empty;
        //1 = Inclusão de Sócio; 3 = Alteração; 5 = Baixa de Sócio
        private Nullable<DateTime> _dataSaidaSocio;
        private bTipoRepresentante _cTipoRepresentante = new bTipoRepresentante();
        private Nullable<DateTime> _dataObito;
        private string _nire;
        private string _t002_ds_orgao_expedidor;
        private decimal _T002_IN_ENTRADA_CAPITAL;

        private string _t002_cpf_outorgante = "";
        private string _t002_ds_outorgante = "";

        private int _adminstracaoIsoladamente = 2;
        private int _adminstracaoConjuntamente = 2;
        private int _adminstracaoTodos = 2;
        private List<bAdminstracao> _admnistracaoConjunto = new List<bAdminstracao>();
        private int _tipoOrgaoRegistro = 1;
        private int _existeNoSiarco = 1;
        private int _indDBE = 0;
        private int _indEventoJunta = 1;
        private decimal _percentualCapital = 0;
        private decimal _percentualCapitalReq = 0;


        private string _eventoDBE;
        private Nullable<DateTime> _T002_DT_SAIDA_ADM;
        private string _situacaoDescricao;
        private int _resp_guarda_livro = 2;
        private int _resp_ativo_passivo = 2;
        private int _escolaridadeCodigo = 0;

       
        #endregion

        #region Class Property Declarations
        public int SQPessoaPai
        {
            get { return _SQPessoaPai; }
            set { _SQPessoaPai = value; }
        }
        public string DescrAcao
        {
            get { return _descrAcao; }
            set { _descrAcao = value; }
        }
        public bTipoRepresentante CTipoRepresentante
        {
            get { return _cTipoRepresentante; }
            set { _cTipoRepresentante = value; }
        }

        public string representante
        {
            get { return _representante; }
            set { _representante = value; }
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
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
        public string tipoacaodescricao
        {
            get
            {
                if (_r001_dt_saida_vinculo != null)
                {
                    return "Saida de Socio";
                }
                if (_tipoacao == 1)
                {
                    return "Novo";
                }
                if (_tipoacao == 2)
                {
                    return "Atual";
                }
                if (_tipoacao == 3)
                {
                    return "Atual";
                }
                if (_tipoacao == 5)
                {
                    return "Baixa";
                }
                return "Atual";
            }
        }
        public int tipoacao
        {
            get { return _tipoacao; }
            set { _tipoacao = value; }
        }
        public string tipo_visto
        {
            get { return _tipo_visto; }
            set { _tipo_visto = value; }
        }
        public Nullable<DateTime> emissao_visto
        {
            get { return _dt_emissao_visto; }
            set { _dt_emissao_visto = value; }
        }
        public Nullable<DateTime> validade_visto
        {
            get { return _dt_validade_visto; }
            set { _dt_validade_visto = value; }
        }
        public int flagNovoEndereco
        {
            get { return _flagNovoEndereco; }
            set { _flagNovoEndereco = value; }
        }
        public decimal CapitalIntegralizado
        {
            get { return _CapitalIntegralizado; }
            set { _CapitalIntegralizado = value; }
        }

        public decimal Capital_a_Integralizar
        {
            get { return _Capital_a_Integralizar; }
            set { _Capital_a_Integralizar = value; }
        }
        public Nullable<DateTime> DataIntegralizacao
        {
            get { return _DataIntegralizacao; }
            set { _DataIntegralizacao = value; }
        }

        public string Analfabeto
        {
            get { return _Analfabeto; }
            set { _Analfabeto = value; }

        }
        public string EndDsTipoLogradouro
        {
            get { return _EndDsTipoLogradouro; }
            set { _EndDsTipoLogradouro = value; }
        }
        public string EndPais
        {
            get { return _EndPais; }
            set { _EndPais = value; }
        }
        public String DDD
        {
            get { return _DDD; }
            set { _DDD = value; }
        }
        public String Telefone
        {
            get { return _Telefone; }
            set{_Telefone = value;}
        }
        public int rep_legal
        {
            get { return _rep_legal; }
            set { _rep_legal = value; }
        }
        public String in_Sexo
        {
            get { return _in_Sexo; }
            set { _in_Sexo = value; }
        }

        public List<bSocios> Representantes
        {
            get { return (List<bSocios>)_Representantes; }
            set { _Representantes = value; }
        }

        public bool Valido
        {
            get { return (bool)_Valido; }
            set { _Valido = value; }
        }

        public int NacionalidadeCodigo
        {
            get { return (int)_NacionalidadeCodigo; }
            set { _NacionalidadeCodigo = value; }
        }

        public String NaturalidadeCodigo
        {
            get { return (String)_NaturalidadeCodigo; }
            set { _NaturalidadeCodigo = value; }
        }

        public Nullable<DateTime> DataNascimento
        {
            get { return _DataNascimento; }
            set { _DataNascimento = value; }
        }

        public String SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }

        //private DateTime dataNascimento;
        public String TipoPessoa
        {
            get { return _TipoPessoa; }
            set { _TipoPessoa = value; }
        }

        public String TipoEmancipado
        {
            get { return _TipoEmancipado; }
            set 
            {
                if (value == null)
                {
                    _TipoEmancipado = "";
                }
                else
                {
                    _TipoEmancipado = value;
                }

                if (_TipoEmancipado.Trim() != "")
                {
                    using (TAB_GENERICA emancipado = new TAB_GENERICA())
                    {
                        _TipoEmancipadoDS = emancipado.QueryEmancipacao(_TipoEmancipado);
                    }
                }
            }
        }

        public String TipoEmancipadoDS
        {
            get { return _TipoEmancipadoDS; }
            
        }

        public String CPFCNPJ
        {
            get { return _CPFCNPJ.Trim(); }
            set { _CPFCNPJ = value; }
        }

        public string fCPFCNPJ

        {
            get
            {
                if (_CPFCNPJ == "")
                {
                    return "";
                }
                else
                {
                    if (_TipoPessoa=="J")
                    {
                        return Convert.ToUInt64(_CPFCNPJ).ToString(@"00\.000\.000\/0000\-00");
                    }
                    else
                    {
                        return Convert.ToUInt64(_CPFCNPJ).ToString(@"000\.000\.000\-00");
                    }
                }

            }
        }

        public String OrgaoExpedidor
        {
            get { return _OrgaoExpedidor; }
            set { _OrgaoExpedidor = value; }
        }

        public String OrgaoExpedidorUF
        {
            get { return _OrgaoExpedidorUF; }
            set { _OrgaoExpedidorUF = value; }
        }

        public String EstadoCivilRegime
        {
            get { return _EstadoCivilRegime; }
            set { _EstadoCivilRegime = value; }
        }
        public String RegimeBens
        {
            get { return _RegimeBens; }
            set { _RegimeBens = value; }
        }
        

        public String TipoAssistido
        {
            get { return _TipoAssistido; }
            set { _TipoAssistido = value; }
        }


        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public String Mae111
        {
            get { return _Mae; }
            set { _Mae = value; }
        }


        public String Nacionalidade
        {
            get { return _Nacionalidade; }
            set { _Nacionalidade = value; }
        }

        public String TipoIdentidade
        {
            get { return _TipoIdentidade; }
            set { _TipoIdentidade = value; }
        }


        public String RG
        {
            get { return _RG; }
            set { _RG = value; }
        }


        public String EstadoCivil
        {
            get { return _EstadoCivil; }
            set { _EstadoCivil = value; }
        }
        public String EstadoCivilDescricao
        {
            get { return _EstadoCivilDescricao; }
            set { _EstadoCivilDescricao = value; }
        }

        public String Profissao
        {
            get { return _Profissao; }
            set { _Profissao = value; }
        }

        public String Profissao_Descricao
        {
            get { return _Profissao_Descricao; }
            set { _Profissao_Descricao = value; }
        }


        public decimal QuotaCapitalSocial
        {
            get { return _QuotaCapitalSocial; }
            set { _QuotaCapitalSocial = value; }
        }
        public decimal AporteSocio
        {
            get { return _AporteSocio; }
            set { _AporteSocio = value; }
        }

        public String EndUF
        {
            get { return _EndUF; }
            set { _EndUF = value; }
        }


        public String EndMunicipio
        {
            get { return _EndMunicipio; }
            set { _EndMunicipio = value; }
        }

        public String EndMunicipioDescricao
        {
            get { return _EndMunicipioDescricao; }
            set { _EndMunicipioDescricao = value; }
        }

        public String EndBairro
        {
            get { return _EndBairro; }
            set { _EndBairro = value; }
        }


        public String EndTipoLogradouro
        {
            get { return _EndTipoLogradouro; }
            set { _EndTipoLogradouro = value; }
        }

        public String EndLogradouro
        {
            get { return _EndLogradouro; }
            set { _EndLogradouro = value; }
        }

        public String EndNumero
        {
            get { return _EndNumero; }
            set { _EndNumero = value; }
        }


        public String EndComplemento
        {
            get { return _EndComplemento; }
            set { _EndComplemento = value; }
        }
        public String EndCEP
        {
            get { return _EndCEP; }
            set { _EndCEP = value; }
        }

        public String ObrigacoesSociais
        {
            get { return _ObrigacoesSociais; }
            set { _ObrigacoesSociais = value; }
        }

        public String Qualificacao
        {
            get { return _Qualificacao; }
            set { _Qualificacao = value; }
        }
        public String Qualificacao_Descricao
        {
            get { return _Qualificacao_Descricao; }
            set { _Qualificacao_Descricao = value; }
        }
        public String Fundador
        {
            get { return _Fundador; }
            set { _Fundador = value; }
        }

        public Nullable<DateTime> DataInicioMandato
        {
            get { return _DataInicioMandato; }
            set { _DataInicioMandato = value; }
        }
        public Nullable<DateTime> DataTerminoMandato
        {
            get { return _DataTerminoMandato; }
            set { _DataTerminoMandato = value; }
        }
        public string Nome_Pai
        {
            get { return _nome_pai; }
            set { _nome_pai = value; }
        }
        public string Nome_Mae
        {
            get { return _nome_mae; }
            set { _nome_mae = value; }
        }
        public string Justificativa_Visto
        {
            get { return _justificativa_visto; }
            set { _justificativa_visto = value; }
        }
        public string Situacao
        {
            get { return _Situacao; }
            set { _Situacao = value; }
        }

        public Nullable<DateTime> DataSaidaSocio
        {
            get { return _dataSaidaSocio; }
            set { _dataSaidaSocio = value; }
        }
        public string TipoRepresentante
        {
            get { return GetTipoRepresentante(); }
        }
        public Nullable<DateTime> DataObito
        {
            get { return _dataObito; }
            set { _dataObito = value; }
        }
        public string Nire
        {
            get { return _nire; }
            set { _nire = value; }
        }
        public string OrgaoExpedidorNome
        {
            get { return _t002_ds_orgao_expedidor; }
            set { _t002_ds_orgao_expedidor = value; }
        }
        public decimal IndicadorAporteCapital
        {
            get { return _T002_IN_ENTRADA_CAPITAL; }
            set { _T002_IN_ENTRADA_CAPITAL = value; }
        }
        public string CpfOutorgante
        {
            get { return _t002_cpf_outorgante; }
            set { _t002_cpf_outorgante = value; }
        }
        public string NomeOutorgante
        {
            get { return _t002_ds_outorgante; }
            set { _t002_ds_outorgante = value; }
        }
        public List<bAdminstracao> ListAdmnistracaoConjunto
        {
            get { return _admnistracaoConjunto; }
            set { _admnistracaoConjunto = value; }
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
        public int TipoOrgaoRegistro
        {
            get { return _tipoOrgaoRegistro; }
            set { _tipoOrgaoRegistro = value; }
        }
        public int ExisteNoSiarco
        {
            get { return _existeNoSiarco; }
            set { _existeNoSiarco = value; }
        }
        public int IndicadorDBE
        {
            get { return _indDBE; }
            set { _indDBE = value; }
        }
        public int IndicadorEventoJunta
        {
            get { return _indEventoJunta; }
            set { _indEventoJunta = value; }
        }
        public decimal PercentualCapital
        {
            get { return _percentualCapital; }
            set { _percentualCapital = value; }
        }
        public decimal PercentualCapitalReq
        {
            get { return _percentualCapitalReq; }
            set { _percentualCapitalReq = value; }
        }

        public string EventoDBE
        {
            get { return _eventoDBE; }
            set { _eventoDBE = value; }
        }

        public Nullable<DateTime> DataSaidaAdm
        {
            get { return _T002_DT_SAIDA_ADM; }
            set { _T002_DT_SAIDA_ADM = value; }
        }
        /// <summary>
        /// Status do socio na empresa Atual ou Novo
        /// </summary>
       
        public string SituacaoSocio
        {
            get
            {
                if (_tipoacao == 1)
                {
                    return "Novo";
                }
                if (_tipoacao == 2)
                {
                    return "Atual";
                }
                if (_tipoacao == 3)
                {
                    return "Atual";
                }
                if (_tipoacao == 5)
                {
                    return "Atual";
                }
                return "Atual";
            }
        }

        public string SituacaoDescricao
        {
            get { return _situacaoDescricao; }
            set { _situacaoDescricao = value; }
        }
        public int RespGuardaLivro
        {
            get { return _resp_guarda_livro; }
            set { _resp_guarda_livro = value; }
        }
        public int RespAtivoPassivo
        {
            get { return _resp_ativo_passivo; }
            set { _resp_ativo_passivo = value; }
        }
        public int EscolaridadeCodigo
        {
            get { return _escolaridadeCodigo; }
            set { _escolaridadeCodigo = value; }
        }
        public string DescRespGuardaLivro
        {
            get { return _resp_guarda_livro == 1? "Sim" : "Não"; }
        }
        public string DescRespAtivoPassivo
        {
            get { return _resp_ativo_passivo == 1 ? "Sim" : "Não"; }
        }
        #endregion

        private string GetTipoRepresentante()
        {
            DataTable dt = dHelperQuery.getTipoRepresentante(Int32.Parse(_TipoAssistido)); ;
            string _descricao = "";
            foreach (DataRow dr in dt.Rows)
            {
                //_codigo = Int32.Parse(dr["t015_co_tipo_assistido_representado"].ToString());
                _descricao = dr["t015_ds_tipo_assistido_representado"].ToString();
            }
            return _descricao;
        }

        private List<bSocios> GetRepresentantesAtivos()
        {
            _RepresentantesAtivos.Clear();
            foreach (bSocios rep in _Representantes)
            {
                if (rep.Situacao == "A" )
                {
                    _RepresentantesAtivos.Add(rep);
                }
            }
            return _RepresentantesAtivos;
        }
        public List<bSocios> RepresentantesAtivos
        {
            get { return GetRepresentantesAtivos(); }
        }

        public void Update(int SqEmpresa)
        {
            try
            {
                int SqSocio = 0;
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    //tRATAR SE A SQ_PESSOA < SQ_PEEOSA_PAI
                    //1 - EXCLUIR NO BANCO ESSA PESSOA
                    //2 COLOCAR ZARE DA SQ_PESOSA DA CLASSE

                    if (Int32.Parse(_SQPessoa) < _SQPessoaPai)
                    {
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.Deleta(Int32.Parse(_SQPessoa), _SQPessoaPai , Int32.Parse(_Qualificacao));
                        }
                        _SQPessoa = "0";
                    }

                    using (dT001_Pessoa p = new dT001_Pessoa())
                    {

                        #region 8. Gravando Pessoa
                        p.MainConnectionProvider = cp;
                        p.t001_in_tipo_pessoa = "F";
                        _TipoPessoa = "F"; ;
                        p.t001_ds_pessoa = _Nome;
                        p.t001_in_dados_atualizados = "S";
                        p.t001_dt_ult_atualizacao = DateTime.Now;
                        if (_DDD != string.Empty)
                            p.t001_ddd = _DDD;
                        if (_Telefone != string.Empty)
                            p.t001_tel_1 = _Telefone;
                        p.t001_email = _Email == null ? "" : _Email;

                        if (_SQPessoa != null)
                            p.t001_sq_pessoa = Convert.ToDecimal(_SQPessoa);
                        else
                            p.t001_sq_pessoa = 0;

                        SqSocio = p.Update();

                        if (SqSocio != 0)
                            _SQPessoa = SqSocio.ToString();
                        else
                            SqSocio = Convert.ToInt32(_SQPessoa);

                        #endregion

                        #region 12 Gravando Vinculo Sócio com a PJ (Vinculo Sócio)
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.t001_sq_pessoa = SqSocio;
                            v.t001_sq_pessoa_pai = SqEmpresa;

                           
                            v.a009_co_condicao = decimal.Parse(_Qualificacao);
                            v.T001_cpf_cnpj_pessoa = _CPFCNPJ;

                            if (v.a009_co_condicao == 22)
                                v.r001_ds_cargo_direcao = "Sócio";
                            else
                                v.r001_ds_cargo_direcao = _Qualificacao_Descricao;

                            if (_Qualificacao != null && _Qualificacao != string.Empty)
                            {
                                v.a009_co_condicao = decimal.Parse(_Qualificacao);
                                v.r001_dt_inicio_mandato = _DataInicioMandato;
                                v.r001_dt_termino_mandato = _DataTerminoMandato;
                                v.t001_sq_pessoa_rep_legal = _rep_legal;
                            }
                            else
                            {

                                v.r001_dt_inicio_mandato = null;
                                v.r001_dt_termino_mandato = null; ;
                                v.t001_sq_pessoa_rep_legal = 0;
                            }

                            //v.r001_in_situacao = "A";
                            if (string.IsNullOrEmpty(_Situacao))
                                v.r001_in_situacao = "A";
                            else
                                v.r001_in_situacao = _Situacao; // "A";

                            v.r001_in_fundador = _Fundador;
                            v.r001_vl_participacao = _CapitalIntegralizado + _Capital_a_Integralizar;
                            v.t001_sq_pessoa_rep_legal = _rep_legal;

                            v.r001_dt_entrada_vinculo = DateTime.Now;
                            v.r001_acao = _tipoacao;
                            if (_dataSaidaSocio != null)
                                v.r001_dt_saida_vinculo = _dataSaidaSocio;

                            v.Update();

                        }
                        #endregion

                        #region 9.Gravando Pessoa Fisica
                            //10.Gravando Socios - pessoa fisica
                            //(Dados dos Socios do requerimento)
                        using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                        {
                            pf.MainConnectionProvider = cp;

                            pf.t001_sq_pessoa = SqSocio;
                            pf.t002_nr_cpf = _CPFCNPJ;
                            if (_TipoIdentidade != null && _TipoIdentidade != String.Empty)
                                pf.a010_co_tipo_documento = decimal.Parse(_TipoIdentidade);

                            pf.t002_nr_documento = _RG;
                            pf.t002_ds_emissor_documento = _OrgaoExpedidor;
                            pf.a004_uf_org_exped = _OrgaoExpedidorUF;
                            pf.t002_ds_orgao_expedidor = _t002_ds_orgao_expedidor;

                            if (!string.IsNullOrEmpty(_NacionalidadeCodigo.ToString()))
                            {
                                pf.a004_co_pais = _NacionalidadeCodigo;
                                if (pf.a004_co_pais != 154)
                                {
                                    pf.t002_tipo_visto = _tipo_visto;
                                    pf.t002_emissao_visto = _dt_emissao_visto;
                                    pf.t002_dt_validade_visto = _dt_validade_visto;
                                }
                                else
                                {
                                    pf.t002_tipo_visto = "";
                                    pf.t002_emissao_visto = null;
                                    pf.t002_dt_validade_visto = null;
                                }
                            }

                            pf.t002_ds_nacionalidade = _Nacionalidade;
                            pf.a004_co_uf_naturalidade = _NaturalidadeCodigo;
                            if (_EstadoCivil != null && _EstadoCivil != String.Empty)
                                pf.a012_co_estado_civil = decimal.Parse(_EstadoCivil);
                            if (_EstadoCivilRegime != null && _EstadoCivilRegime != String.Empty)
                            {
                                pf.a013_co_regime_bens = decimal.Parse(_EstadoCivilRegime);
                            }

                            if (_Profissao_Descricao != null && _Profissao_Descricao != string.Empty)
                                pf.t002_ds_profissao = _Profissao_Descricao;


                            pf.t002_dt_nascimento = _DataNascimento;
                            pf.t002_in_sexo = _in_Sexo;


                            if (_TipoEmancipado != null && _TipoEmancipado != String.Empty)
                            {
                                pf.a014_co_emancipacao = decimal.Parse(_TipoEmancipado);
                            }

                            pf.t002_nr_qtd_cotas = _QuotaCapitalSocial;
                            pf.t002_capital_integralizado = _CapitalIntegralizado;
                            pf.t002_capital_a_integralizar = _Capital_a_Integralizar;
                            pf.t002_perc_capital = _percentualCapital;
                            pf.t002_data_final_integralizacao = _DataIntegralizacao;

                            pf.t002_dt_nascimento = _DataNascimento;
                            pf.t002_nome_pai = _nome_pai;
                            pf.t002_nome_mae = _nome_mae;

                            pf.t002_analfabeto = _Analfabeto;
                            pf.t002_dt_obito = _dataObito;

                            pf.t002_nire = _nire;
                            pf.t002_tip_orgao_registro = _tipoOrgaoRegistro;

                            pf.t002_cpf_outorgante = _t002_cpf_outorgante;
                            pf.t002_ds_outorgante = _t002_ds_outorgante;

                            pf.AdminstracaoIsoladamente = _adminstracaoIsoladamente;
                            pf.AdminstracaoConjuntamente = _adminstracaoConjuntamente;
                            pf.AdminstracaoTodos = _adminstracaoTodos;

                            pf.t002_in_siarco = _existeNoSiarco;
                            pf.t002_in_div_dbe = _indDBE;
                            pf.t002_tipo_acao = _indEventoJunta;
                            pf.t002_dt_saida_adm = _T002_DT_SAIDA_ADM;
                            pf.t002_in_resp_livro = _resp_guarda_livro;
                            pf.Update();

                        }

                        #endregion

                        #region 11 Gravando Socios - Endereço
                        //(dados dos endereços dos sócios)

                        using (dR002_Vinculo_Endereco es = new dR002_Vinculo_Endereco())
                        {
                            int SqVinculoEndereco;
                            es.MainConnectionProvider = cp;
                            es.t001_sq_pessoa = SqSocio;
                            es.t001_sq_pessoa_pai = SqEmpresa;
                            if (_EndTipoLogradouro != null && _EndTipoLogradouro != String.Empty)
                            {
                                es.a015_co_tipo_logradouro = decimal.Parse(_EndTipoLogradouro);
                                es.a015_ds_tipo_logradouro = _EndDsTipoLogradouro;
                            }
                            es.r002_ds_logradouro = _EndLogradouro;
                            es.r002_nr_logradouro = _EndNumero;
                            es.r002_ds_complemento = _EndComplemento;
                            es.r002_ds_bairro = _EndBairro;
                            if (_EndMunicipio != null && _EndMunicipio != String.Empty)
                                es.a005_co_municipio = decimal.Parse(_EndMunicipio);//;
                            if (!string.IsNullOrEmpty(_EndPais))
                                es.a004_co_pais = Convert.ToInt32(_EndPais);
                            es.r002_nr_cep = _EndCEP;
                            SqVinculoEndereco = es.Update();
                        }
                        #endregion

                    }

                    VerificaSocioDuploOuquantidade(cp, SqEmpresa);


                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

        private void VerificaSocioDuploOuquantidade(ConnectionProvider cv, int SqEmpresa)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.MainConnectionProvider = cv;
                int qtd = 0;


                qtd = v.VerificaSeDuplicoQSA(SqEmpresa);
                if (qtd > 0)
                {
                    //Socio esta duplicado por favor tentar novamente
                    throw new Exception("Problema na conexão. Tente mais tarde (3.1.3) " + SqEmpresa.ToString());
                }
            }
        }

        /// <summary>
        /// Atualiza a data de saida do vinculo da pessoa
        /// true coloca data baixa coma data atual, false limpa o campo data de saida
        /// </summary>
        /// <param name="Baixa"></param>
        public void AtualizaDataBaixa(bool Baixa)
        {
            
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
               
                v.t001_sq_pessoa = Decimal.Parse(_SQPessoa);
                v.t001_sq_pessoa_pai = _SQPessoaPai;
                v.a009_co_condicao = Decimal.Parse(_Qualificacao);
                if (Baixa)
                {
                    v.r001_dt_saida_vinculo = DateTime.Now;
                }
                else
                {
                    v.r001_dt_saida_vinculo = null;
                }
                

                v.AtualizaDataBaixa();
               
            }
           
        }

        public void Delete()
        {
            /* 1 - contar quantas qualificações da pessoa
             * 2 - se existir mais de uma qualificação 
             *          exclui da vinculo 
             *          não exclui o vinculo_endereço , pessoa , pessoa fisica ou juridica
             *     se existir uma qualificação
             *          eclui da vinculo endereco, vinculo , pessoa fisica/juridica e pessoa
            */
            int wsqPessoa = Int32.Parse(_SQPessoa);
            int wsqPai = _SQPessoaPai;
            int wSqRepresentante = 0;

            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();

                int _qtdPessoa = ContaPessoaNoVinculo(wsqPessoa, cv);

                //Verifica se tem representante
                DataTable dtRepr = dHelperQuery.CarregaRepresentantes(wsqPessoa);
                foreach (DataRow rr in dtRepr.Rows)
                {
                    wSqRepresentante = Int32.Parse(rr["SQ_REPRESENTANTE"].ToString());
                    ApagaRepresentante(wSqRepresentante, wsqPessoa, cv);
                }


                //

                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cv;
                    v.Deleta(wsqPessoa, wsqPai, 0);
                }

                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cv;
                    pj.Deleta(wsqPessoa);
                }

                if (_qtdPessoa == 1)
                {
                    using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                    {
                        ve.MainConnectionProvider = cv;
                        ve.Deleta(wsqPessoa);
                    }

                    using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                    {
                        pf.MainConnectionProvider = cv;
                        pf.Deleta(wsqPessoa);
                    }

                    using (dT001_Pessoa p = new dT001_Pessoa())
                    {
                        p.MainConnectionProvider = cv;
                        p.Deleta(wsqPessoa);
                    }
                }


                cv.CommitTransaction();
            }

        }

        public void ApagaRepresentante(int wsqPessoa, int wsqPai, ConnectionProvider cv)
        {
            /* 1 - apaga represente dos socios
            */
            int qtdPessoa = ContaPessoaNoVinculo(wsqPessoa, cv);


            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.MainConnectionProvider = cv;
                v.Deleta(wsqPessoa, wsqPai, 0);
            }
            //Só exclui o restante se o representante só estiver para um sócio
            if (qtdPessoa == 1)
            {
                using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                {
                    ve.MainConnectionProvider = cv;
                    ve.Deleta(wsqPessoa);
                }

                using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                {
                    pf.MainConnectionProvider = cv;
                    pf.Deleta(wsqPessoa);
                }


                using (dT001_Pessoa p = new dT001_Pessoa())
                {
                    p.MainConnectionProvider = cv;
                    p.Deleta(wsqPessoa);
                }
            }
        }

        public int ContaPessoaNoVinculo(int wSqPessoa, ConnectionProvider cv)
        {

            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.MainConnectionProvider = cv;

                return v.ContaPessoaVinculo(wSqPessoa);
            }

        }

        /// <summary>
        /// Atualiza valor de quota e Capital Social
        /// </summary>
        public void UpdateCapitalSocial()
        {
            using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
            {
                pf.t001_sq_pessoa = Convert.ToDecimal(_SQPessoa);
                pf.t002_nr_qtd_cotas = _QuotaCapitalSocial;
                pf.t002_capital_integralizado = _CapitalIntegralizado;
                pf.t002_capital_a_integralizar = _Capital_a_Integralizar;

                pf.UpdateCapitalSocialSocios();

            }
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.t001_sq_pessoa = Convert.ToDecimal(_SQPessoa);
                v.t001_sq_pessoa_pai = _SQPessoaPai;
                v.r001_vl_participacao = _CapitalIntegralizado + _Capital_a_Integralizar;

                v.UpdateCapital();
            }
        }
        public void UpdateRespLivroAtivoPassivo()
        {
            using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
            {
                pf.t001_sq_pessoa = Convert.ToDecimal(_SQPessoa);
                pf.t002_in_resp_livro = _resp_guarda_livro;
                pf.t002_in_resp_ativo_passivo = _resp_ativo_passivo;
                pf.UpdateRespLivroAtivoPassivo();

            }
        }

        public void UpdateTipoAcao(int _acao)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.t001_sq_pessoa = Convert.ToDecimal(_SQPessoa);
                v.t001_sq_pessoa_pai = _SQPessoaPai;
                v.r001_acao = _acao;
                v.UpdateTipoAcao();
            }
        }
    }
}
