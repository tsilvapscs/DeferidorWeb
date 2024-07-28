using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using RCPJ.DAL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Ruc;
using psc.Application.Siarco;


namespace RCPJ.BLL
{
    [Serializable]
    public class bRequerimento : IRequerimento // DBInteractionBase 
    {
        #region Class Member Declarations

        private string _requerimentoAnterior = "";

        protected string[] tbEnquadramento = new string[12] { " - ME ", " ME.", "- ME ", " -ME ", "-ME ", " ME ", " - EPP", "- EPP ", " -EPP ", "-EPP ", " EPP ", " EPP." };

        #region Contrato Padrao
        private bContratoPadrao _contratoPadrao = new bContratoPadrao();


        #endregion

        #region Controles
        private String _pUsuarioLogadoDaJunta = "";
        private List<string> _listErro = new List<string>();
        private List<string> _listAlerta = new List<string>();
        private List<string> _listAlertaDbeViab = new List<string>();


        private string _UsuarioRegin = "";
        private String _TipoGravacao;
        private int _CodigoEmpresa = 0;
        private int _controleExisteRequerimento = 0;

        private bool _OrigemViabilidade;
        private Boolean _ehValido = false;
        #endregion

        #region Constantes
        private const string CODEVENTO_CONSTITUICAO_EMPRESA = "101";
        private const string CODEVENTO_CONSTITUICAO_FILIAL = "102";
        private const string CODEVENTO_ALTERACAO_NOME_EMPRESARIAL = "220";
        private const string CODEVENTO_ALTERACAO_ENDERECO_MUNICIPIOS_DENTRO_DO_ESTADO = "209";
        private const string CODEVENTO_ALTERACAO_ENDERECO_DENTRO_DO_MESMO_MUNICIPIO = "211";
        private const string CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS = "244";
        private const string CODEVENTO_ALTERACAO_ENDERECO_ENTRE_ESTADOS = "210";
        private const string CODEVENTO_ALTERACAO_DATA_INICIO_ATIVIDADES = "415";
        private const string CODEVENTO_INSCRICAO_NO_MUNICIPIO = "801";
        private const string CODEVENTO_CAPITAL_SOCIAL = "247";
        private const string CODEVENTO_NATUREZA_JURIDICA = "225";
        private const string CODEVENTO_ALTERPORTE_EMPRESA = "222";
        private const string CODEVENTO_ALTER_QSA = "202";
        private const string CODEVENTO_CLAUSULA_ESPECIAL = "999";
        private const string CODEVENTO_BAIXA = "517";
        #endregion

        protected string _nomeMunicipioSede;

        #region Andamento
        public bAndamento bAndamento = new bAndamento();


        #endregion
        /// <summary>
        /// TipoEmpresa 1- Matriz, 2 -Filial
        /// </summary>
        private int _tipoEmpresa = 1;

        /// <summary>
        ///  1- Matriz, 2 -Filial
        /// </summary>
        private int _indicadorMatriz = 1;

        private int _codMunicipioInscMunicipal = 0;

        public int CodMunicipioInscMunicipal
        {
            get { return _codMunicipioInscMunicipal; }
            set { _codMunicipioInscMunicipal = value; }
        }
        private string _tipoRegistroViab = "0";

        public string TipoRegistroViab
        {
            get { return _tipoRegistroViab; }
            set { _tipoRegistroViab = value; }
        }

        private List<bDivergenciaDBE> _listDivergenciaDBE = new List<bDivergenciaDBE>();
        private bDivergenciaDBE _divDBE = new bDivergenciaDBE();
        public bDivergenciaDBE DivergenciaDBE
        {
            get { return _divDBE; }
            set { _divDBE = value; }
        }

        private bRequerimento _dbe;

        public bRequerimento DBE
        {
            get { return _dbe; }
            set { _dbe = value; }
        }
        private bRequerimento _viabilidade;

        public bRequerimento Viabilidade
        {
            get { return _viabilidade; }
            set { _viabilidade = value; }
        }

        private string _xmlRequerimento;
        private string _mensagemErro;

        private List<dr013_Requerimento_Evento> _reqEventos = new List<dr013_Requerimento_Evento>();

        private string _tipoCorrelativo = string.Empty;

        private int _tipoRequerimento = 1;

        private string _numeroDARE = "";

        private string _dbeAtual = string.Empty;


        private int _NaturezaJuridicaCodigo;
        private String _NaturezaJuridicaDescricao = string.Empty;
        private int _TipoPessoaJuridicaCodigo;
        private String _TipoPessoaJuridicaDescricao = string.Empty;
        private String _ProtocoloViabilidade = string.Empty;
        private String _ProtocoloRequerimento = string.Empty;
        private string _ProtocoloPrefeitura = string.Empty;
        private string _ProtocoloEnquadramento = string.Empty;
        private int _IsProtocoloNaJunta = 0;

        #region Dados da Empresa
        private String _nrMatricula = string.Empty;
        private String _nrEmpresaCNPJ = string.Empty;
        private string _DDDEmpresa = string.Empty;
        private string _TelefoneEmpresa = string.Empty;
        private int _Enquadramento;
        private string _porte = string.Empty;
        private String _SedeNome = string.Empty;
        private string _SedeEmail = string.Empty;
        private string _Nome_Fantasia = string.Empty;
        private String _ObjetoSocial = string.Empty;
        private String _ArtigoEstatuto = string.Empty;
        private String _CNPJ_Orgao_Registro = string.Empty;
        private decimal _CapitalSocial;
        private decimal _ValorCota;
        private decimal _QtdCotas;
        private decimal _capital_integralizado;
        private decimal _capital_nao_integralizado;
        private Nullable<DateTime> _data_limite_integralizacao;
        private string _ds_capital_nao_integralizado;
        private string _descricaoReducaoCapital = "";


        private int _SedeSituacao;
        private string _SedeSituacaoDescricao;
        private int _naturezaJuridicaOR = 1;


        private Nullable<DateTime> _DataInicioSociedade;
        private Nullable<DateTime> _DuracaoSociedade;
        private Nullable<DateTime> _DataConstituicao;
        private Nullable<DateTime> _dataTerminoAtividade;
        private int _indicadorSPE = 2;


        private string _t005_protocolo_orgao_origem = string.Empty;
        private String _ProtocoloRCPJ = string.Empty;
        private int _T003_IND_CNAE_DESTACADA = 2;
        private string _t005_uf_origem = string.Empty;
        private int _empresaUnipessoal = 2;
        private int _integralizandoCapital = 2;

        private string _t003_iptu = "";
        private decimal _t003_area_utilizada = 0;
        #endregion

        private string _CodigoAto;
        private int _CodigoEvento;
        private String _CodigoDOCAD = string.Empty;
        private String _CodigoDBE = string.Empty;

        #region Requerente
        private bRequerente _Requerente = new bRequerente();

        private String _RequerenteNome = string.Empty;
        private int _RequerenteCodigo = 0;
        private String _RequerenteCPF = string.Empty;
        private String _RequerenteDDD = string.Empty;
        private String _RequerenteTelefone = string.Empty;
        private String _RequerenteEmail = string.Empty;
        #endregion
        
        #region Empresa
        private bEmpresa _empresa = new bEmpresa();

        #endregion

        #region Endereco Sede
        private String _SedeCEP = string.Empty;
        private String _SedeUF = string.Empty;
        private string _SedeDsTipoLogradouro = string.Empty;
        private String _SedeMunicipio = string.Empty;
        private String _SedeBairro = string.Empty;
        private String _SedeTipoLogradouro = string.Empty;
        private String _SedeLogradouro = string.Empty;
        private String _SedeNumero = string.Empty;
        private String _SedeComplemento = string.Empty;
        private String _SedeDDD = string.Empty;
        private String _SedeTelefone = string.Empty;
        #endregion

        #region Fundação
        private int _AssociacaoPorte;
        private Nullable<DateTime> _AssociacaoTempoDuracao;
        private String _Obrigacoes_Sociais = string.Empty;
        private int _Num_Fundadores_Diretores;
        //private int _Forma_Convocacao;
        private string _Edital_Fixado_Sede = string.Empty;
        private string _Edital_Publicado_Jornal = string.Empty;
        private string _Edital_Outros = string.Empty;
        private String _Num_Artigo_Estatuto_Convocacao = string.Empty;
        private String _Quorum_Deliberacao = string.Empty;
        private String _Quorum_Alteracao = string.Empty;
        private String _Quorum_Dissolucao = string.Empty;
        private String _Outro_Quorum_Deliberacao = string.Empty;
        private String _Outro_Quorum_Alteracao = string.Empty;
        private String _Outro_Quorum_Dissolucao = string.Empty;
        private String _Destino_Patrimonio = string.Empty;
        private String _Num_Artigo_Estatuto_Deliberacao = string.Empty;
        private String _Num_Artigo_Estatuto_Alteracao = string.Empty;
        private String _Num_Artigo_Estatuto_Dissolucao = string.Empty;
        private String _Num_Artigo_Estatuto_Obrigacoes_Sociais = string.Empty;
        private String _Num_Artigo_Estatuto_Fundo_Social = string.Empty;
        private String _Possui_Fundo_Social = string.Empty;
        private String _Fonte_Contribuicoes_Mensais = string.Empty;
        private String _Fonte_Contribuicoes_Doacao = string.Empty;
        private String _Fonte_Recursos_Governamentais = string.Empty;
        private String _Num_Artigo_Estatuto_Associacao = string.Empty;
        #endregion

        #region Advogado
        private bAdvogado _advogado = new bAdvogado();

        private String _Nome_Advogado = string.Empty;
        private String _CPF_Advogado = string.Empty;
        private String _Inscricao_OAB_Advogado = string.Empty;
        private String _UF_OAB_Advogado = string.Empty;
        #endregion

        #region Contador
        private String _Nome_Contador = string.Empty;
        private String _CPF_Contador = string.Empty;
        private String _CRC_Contador = string.Empty;
        private String _UF_CRC = string.Empty;
        #endregion

        private String _Ata_Mesmo_Instrumento = string.Empty;

        #region Constantes
        private const int TIPO_SOCIEDADE = 1264;
        private String CONST_PESSOA_FISICA = "F";
        private String CONST_PESSOA_JURIDICA = "J";
        private String CONST_PROTOCOLO_CORRELATIVO = "81";
        //private String CONST_PESSOA_CORRELATIVO = "51";
        //private String CONST_VINCULO_ENDERECO_CORRELATIVO = "52";
        #endregion

        #region Status Requerimento
        protected string _status_protocolo = "";
        protected DateTime _status_data;
        protected string _status_usuario = string.Empty;
        protected string _status_viabilidade = string.Empty;
        protected string _status_ds_viabilidade = string.Empty;
        protected bool _status_atualizado = false;
        #endregion

        #region Coleções
        private List<bCNAE> _CNAEs = new List<bCNAE>();
        private List<bCNAE> _cnaeSec = new List<bCNAE>();
        private List<bSocios> _Socios = new List<bSocios>();
        private List<bSocios> _SociosTodos = new List<bSocios>();
        private List<bSocios> _SociosAtivos = new List<bSocios>();
        private List<bSocios> _SociosAtivosExaminador = new List<bSocios>();
        private List<bSocios> _SociosAtivosSemSaida = new List<bSocios>();
        private List<bSocios> _SociosSiarco = new List<bSocios>();
        private List<bSocios> _AdminSiarco = new List<bSocios>();
        private List<bSocios> _SociosAtuais = new List<bSocios>();
        private List<bSocios> _AdministradoresAtual = new List<bSocios>();

        private List<bSocios> _AdministradoresAtivos = new List<bSocios>();
        private List<bSocios> _AdministradoresIsoladamente = new List<bSocios>();
        private List<bSocios> _AdministradoresConjuntamente = new List<bSocios>();
        private List<bSocios> _AdministradoresTodos = new List<bSocios>();

        private List<bSocios> _listRepresentanteCapa = new List<bSocios>();


        private List<bSocios> _TodoQSA = new List<bSocios>();
        private List<bFilial> _Filiais = new List<bFilial>();
        private List<bAto> _Atos = new List<bAto>();
        private List<bProtocoloEvento> _bProtocoloEvento = new List<bProtocoloEvento>();
        private List<bSocios> _FundadorDiretor = new List<bSocios>();
        private List<bExigencias> _ExigenciasOutras = new List<bExigencias>();
        private List<bExigencias> _ExigenciasConfirmacao = new List<bExigencias>();
        private List<bExigencias> _Exigencias = new List<bExigencias>();
        private List<bProtocoloConfirmacao> _Confirmacoes = new List<bProtocoloConfirmacao>();
        private List<dcnt_clausulas_adicionais> _ClausulasAdicionais = new List<dcnt_clausulas_adicionais>();

        private List<bSociosQuotas> _transferenciaQuotas = new List<bSociosQuotas>();
        private List<bAlertaRequerimento> _alertaRequerimento = new List<bAlertaRequerimento>();

        private List<bAtoEventoCapa> _eventoCapa = new List<bAtoEventoCapa>();
        private bParametro _parametro = new bParametro();



        #endregion

        private bReqGenProtocolo _reqGenprotocolo = new bReqGenProtocolo();

        private decimal _Salario_Minimo_Vigente;
        private string _wTipoEmpresa = string.Empty;
        private string _tipo_visto = string.Empty;
        private Nullable<DateTime> _dt_emissao_visto;
        private Nullable<DateTime> _dt_validade_visto;

        private bool _Examinador;


        private int _qi;

        #region Capa do Processo
        private string _Foro = string.Empty;
        private string _clausulaAbritral = string.Empty;
        private int _Num_Paginas;
        private int _Num_Vias;
        private string _representanteCapa = string.Empty;
        private string _dddTelefoneCapa = string.Empty;
        private string _telefoneCapa = string.Empty;
        private string _emailCapa = string.Empty;
        private Nullable<DateTime> _Data_Assinatura;
        private Nullable<DateTime> _Data_Averbacao;



        private string _Local_Assinatura = string.Empty;
        #endregion

        private string _EventosRFB = "";
        private string _nomeSiarco = string.Empty;
        private int _numeroAlteracao = 0;

        private string _moeda_corrente = string.Empty;

        private string _justificativa_visto = string.Empty;
        private string _obsExigencia = "";
        private String _UsuarioCPF = string.Empty;
        private bool _Consulta;
        private bool _ehUltimaTela;
        private int _FilialComSedeFora = 2;
        private int _novoExame = 0;

        private bOrgaoRegistro _orgaoRegistro = new bOrgaoRegistro();

        private List<bErros> _Erros = new List<bErros>();
        private bErros Erro = new bErros();
        private string _ehIntegralizado = "S"; // Flavio 14/03/2013
        private string _ehInicioDataRegistro;

        private int _localEntregaprocesso;
        private string _codUnidadeEntrega = string.Empty;

        private List<bAlertaRequerimento> _Alertas = new List<bAlertaRequerimento>();

        private int _tipoPropriedade = 1;

        private int _eventoConsolidacao = 2;
        private int _eventoReativacao = 2;
        private int _eventoReRatificacao = 2;

        private int _indTransfUnipessoal = 2;
        private int _indDbeCarregado = 2;

        private string _t003_ds_reducao_capital;
        private int _t003_in_reducao_capital = 0;
        private string _clausualArbitral = "";
        private string _textoRestituicaoBaixa = "";

        private string _ds_status_protocolo = "";

        //Dados do Usuario

        private String _UsuarioLOGIN;
        private String _UsuarioSENHA;

        private String _UsuarioTIPO;
        private String _UsuarioCODIGO;
        private String _SessionID;

        private bool _apagaCNANES = false;
        private bool _apagaSocios = false;
        private bool _apagaEventos = false;

        private bContabilista _contaabilista = new bContabilista();

        private bTestemunha _testemunha = new bTestemunha();
        private int _t005_in_clausila_adm = 2;
        private string _t005_tx_clausula_adm = "";
        private int _t005_in_alt_admin = 2;
        private int _prossuiEstabelecimento = 1;


        #endregion

        #region SEFAZ
        private List<bTransporte> _veiculos = new List<bTransporte>();

        public List<bTransporte> Veiculos
        {
            get { return _veiculos; }
            set { _veiculos = value; }
        }
        #endregion

        #region Class Property Declarations
        public bEmpresa Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public int ProssuiEstabelecimento
        {
            get { return _prossuiEstabelecimento; }
            set { _prossuiEstabelecimento = value; }
        }
        public int IsProtocoloNaJunta
        {
            get { return _IsProtocoloNaJunta; }
            set { _IsProtocoloNaJunta = value; }
        }
        public bContratoPadrao ContratoPadrao
        {
            get { return _contratoPadrao; }
            set { _contratoPadrao = value; }
        }
        public int IndicadorMatriz
        {
            get { return _indicadorMatriz; }
            set { _indicadorMatriz = value; }
        }

        public List<string> ListAlertaDbeViab
        {
            get { return _listAlertaDbeViab; }
            set { _listAlertaDbeViab = value; }
        }
        public bRequerente Requerente
        {
            get { return _Requerente; }
            set { _Requerente = value; }
        }

        public int ControleExisteRequerimento
        {
            get { return _controleExisteRequerimento; }
            set { _controleExisteRequerimento = value; }
        }
        public bAdvogado Advogado
        {
            get { return _advogado; }
            set { _advogado = value; }
        }

        public string DbeAtual
        {
            get { return _dbeAtual; }
            set { _dbeAtual = value; }
        }

        public int T005_in_alt_admin
        {
            get { return _t005_in_alt_admin; }
            set { _t005_in_alt_admin = value; }
        }

        public string NumeroDARE
        {
            get { return _numeroDARE; }
            set { _numeroDARE = value; }
        }

        public int T005_in_clausila_adm
        {
            get { return _t005_in_clausila_adm; }
            set { _t005_in_clausila_adm = value; }
        }


        public string T005_tx_clausula_adm
        {
            get { return _t005_tx_clausula_adm; }
            set { _t005_tx_clausula_adm = value; }
        }
        public bTestemunha Testemunha
        {
            get { return _testemunha; }
            set { _testemunha = value; }
        }

        public bool ApagaCnae
        {
            get { return _apagaCNANES; }
            set { _apagaCNANES = value; }
        }
        public bool ApagaEventos
        {
            get { return _apagaEventos; }
            set { _apagaEventos = value; }
        }
        public bool ApagaSocios
        {
            get { return _apagaSocios; }
            set { _apagaSocios = value; }
        }

        public string ds_protocolo_status
        {
            get { return _ds_status_protocolo; }
            set { _ds_status_protocolo = value; }
        }

        public int NaturezaJuridicaOR
        {
            get { return _naturezaJuridicaOR; }
            set { _naturezaJuridicaOR = value; }
        }

        public int NovoExame
        {
            get { return _novoExame; }
            set { _novoExame = value; }
        }
        public string clausulaArbitral
        {
            get { return _clausualArbitral; }
            set { _clausualArbitral = value; }
        }
        public string TextoRestituicaoBaixa
        {
            get { return _textoRestituicaoBaixa; }
            set { _textoRestituicaoBaixa = value; }
        }

        public bParametro Parametros
        {
            get { return _parametro; }
        }

        public List<bExigencias> ExigenciasLista1
        {
            get
            {
                List<bExigencias> eTemp = new List<bExigencias>();

                foreach (bExigencias e in _Exigencias)
                {
                    e.Origem = 1;
                    eTemp.Add(e);
                }

                foreach (bExigencias e in _ExigenciasOutras)
                {
                    if (string.IsNullOrEmpty(e.FundamentoLegal)) // != "Gerado Automaticamente")
                    {
                        e.Origem = 2;
                        eTemp.Add(e);
                    }
                }
                return (List<bExigencias>)eTemp;

            }

        }

        public List<bExigencias> ExigenciasLista2
        {
            get
            {
                List<bExigencias> eTemp = new List<bExigencias>();
                foreach (bExigencias e in _ExigenciasOutras)
                {
                    if (!string.IsNullOrEmpty(e.Grupo))
                    {
                        eTemp.Add(e);
                    }
                    //}
                }
                return (List<bExigencias>)eTemp;

            }

        }



        public int SedeSituacao
        {
            get { return _SedeSituacao; }
            set { _SedeSituacao = value; }
        }

        public string SedeSituacaoDescricao
        {
            get { return _SedeSituacaoDescricao; }
            set { _SedeSituacaoDescricao = value; }
        }


        public int TipoPropriedade
        {
            get { return _tipoPropriedade; }
            set { _tipoPropriedade = value; }
        }
        public Nullable<DateTime> Data_Averbacao
        {
            get { return _Data_Averbacao; }
            set { _Data_Averbacao = value; }
        }
        public Nullable<DateTime> Data_Assinatura
        {
            get { return _Data_Assinatura; }
            set { _Data_Assinatura = value; }
        }
        public string Local_Assinatura
        {
            get { return _Local_Assinatura; }
            set { _Local_Assinatura = value; }
        }
        public Boolean ehValido
        {
            get { return _ehValido; }
            set { _ehValido = value; }
        }
        public string MensagemErro
        {
            get { return _mensagemErro; }
            set { _mensagemErro = value; }
        }

        public string XmlRequerimento
        {
            get { return _xmlRequerimento; }

        }


        // Novo 26/03/2013

        public string ehInicioDataRegistro
        {
            get { return _ehInicioDataRegistro; }
            set { _ehInicioDataRegistro = value; }
        }
        public string ehIntegralizado
        {
            get { return _ehIntegralizado; }
            set { _ehIntegralizado = value; }
        }
        public List<bErros> Erros
        {
            get { return (List<bErros>)_Erros; }
            set { _Erros = value; }
        }



        public bOrgaoRegistro orgaoRegistro
        {
            get { return getOrgaoRegistro(); }
        }
        public string EventosRFB
        {
            get { return _EventosRFB; }
            set { _EventosRFB = value; }
        }
        public int QI
        {
            get { return _qi; }
            set { _qi = value; }
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
        public Nullable<DateTime> dt_validade_visto
        {
            get { return _dt_validade_visto; }
            set { _dt_validade_visto = value; }
        }
        public string wTipoEmpresa
        {
            get { return _wTipoEmpresa; }
            set { _wTipoEmpresa = value; }
        }
        public bool Examinador
        {
            get { return _Examinador; }
            set { _Examinador = value; }
        }
        public string ds_capital_nao_integralizado
        {
            get { return _ds_capital_nao_integralizado; }
            set { _ds_capital_nao_integralizado = value; }
        }
        public decimal Salario_Minimo_Vigente
        {
            get { return _Salario_Minimo_Vigente; }
            set { _Salario_Minimo_Vigente = value; }
        }
        public string UsuarioRegin
        {
            get { return _UsuarioRegin; }
            set { _UsuarioRegin = value; }
        }
        public List<string> ListErro
        {
            get { return _listErro; }
        }
        public List<string> ListAlerta
        {
            get { return _listAlerta; }
        }
        public String ProtocoloRCPJ
        {
            get { return _ProtocoloRCPJ; }
            set { _ProtocoloRCPJ = value; }
        }

        public string SedeDsTipoLogradouro
        {
            get { return _SedeDsTipoLogradouro; }
            set { _SedeDsTipoLogradouro = value; }
        }



        //public string _tlg_compl; // complemento do logradouro
        public string ProtocoloPrefeitura
        {
            get { return _ProtocoloPrefeitura; }
            set { _ProtocoloPrefeitura = value; }
        }



        //Fim endereço

        public decimal Capital_Integralizado
        {
            get { return _capital_integralizado; }
            set { _capital_integralizado = value; }
        }
        public decimal Capital_Nao_Integralizado
        {
            get { return _capital_nao_integralizado; }
            set { _capital_nao_integralizado = value; }
        }
        public Nullable<DateTime> Data_limite_Integralizacao
        {
            get { return _data_limite_integralizacao; }
            set { _data_limite_integralizacao = value; }
        }

        public string Status_DS_Viabilidade
        {
            get { return _status_ds_viabilidade; }
            set { _status_ds_viabilidade = value; }
        }
        public string Status_Viabilidade
        {
            get { return _status_viabilidade; }
            set { _status_viabilidade = value; }
        }
        public string Status_Protocolo
        {
            get { return _status_protocolo; }
            set { _status_protocolo = value; }
        }
        public DateTime Status_Data
        {
            get { return _status_data; }
            set { _status_data = value; }
        }
        public string Status_Usuario
        {
            get { return _status_usuario; }
            set { _status_usuario = value; }
        }
        public Boolean Status_Atualizado
        {
            get { return _status_atualizado; }
            set { _status_atualizado = value; }
        }
        public int Enquadramento
        {
            get { return _Enquadramento; }
            set { _Enquadramento = value; }
        }
        public string Porte
        {
            get { return _porte; }
            set { _porte = value; }
        }
        public string EnquadramentoDESCRICAO
        {
            get
            {
                if (_Enquadramento != 0)
                {
                    bTabelasAuxiliares g = new bTabelasAuxiliares();

                    return g.getEnquadramentoPorCodigo(_Enquadramento.ToString());

                }
                else
                {
                    return "";
                }
            }
        }
        public Boolean AlteracaoDeEmpresa
        {
            get
            {
                return !IsProtocoloIncorporacao();
            }
        }
        public int CodigoEmpresa
        {
            get { return _CodigoEmpresa; }
            set { _CodigoEmpresa = value; }
        }
        public String SedeDDD
        {
            get { return _SedeDDD; }
            set { _SedeDDD = value; }
        }
        public String SedeTelefone
        {
            get { return _SedeTelefone; }
            set { _SedeTelefone = value; }
        }
        public Nullable<DateTime> DataInicioSociedade
        {
            get { return _DataInicioSociedade; }
            set { _DataInicioSociedade = value; }
        }
        public Nullable<DateTime> DuracaoSociedade
        {
            get { return _DuracaoSociedade; }
            set { _DuracaoSociedade = value; }
        }
        public String TipoGravacao
        {
            get { return _TipoGravacao; }
            set { _TipoGravacao = value; }
        }
        public bool OrigemViabilidade
        {
            get { return _OrigemViabilidade; }
            set { _OrigemViabilidade = value; }
        }

        public int NaturezaJuridicaCodigo
        {
            get { return (int)_NaturezaJuridicaCodigo; }
            set { _NaturezaJuridicaCodigo = value; }
        }

        public String NaturezaJuridicaDescricao
        {
            get { return (String)_NaturezaJuridicaDescricao; }
            set { _NaturezaJuridicaDescricao = value; }
        }

        public int TipoPessoaJuridicaCodigo
        {
            get { return (int)_TipoPessoaJuridicaCodigo; }
            set { _TipoPessoaJuridicaCodigo = value; }
        }

        public String TipoPessoaJuridicaDescricao
        {
            get { return (String)_TipoPessoaJuridicaDescricao; }
            set { _TipoPessoaJuridicaDescricao = value; }
        }

        public String ProtocoloViabilidade
        {
            get { return _ProtocoloViabilidade; }
            set { _ProtocoloViabilidade = value; }
        }
        public String nrMatricula
        {
            get { return _nrMatricula; }
            set { _nrMatricula = value; }
        }
        public string CodigoAto
        {
            get { return _CodigoAto; }
            set { _CodigoAto = value; }
        }
        public int CodigoEvento
        {
            get { return _CodigoEvento; }
            set { _CodigoEvento = value; }
        }
        public String nrEmpresaCNPJ
        {
            get { return _nrEmpresaCNPJ; }
            set { _nrEmpresaCNPJ = value; }
        }

        public String CodigoDOCAD
        {
            get { return _CodigoDOCAD; }
            set { _CodigoDOCAD = value; }
        }
        public String CodigoDBE
        {
            get { return _CodigoDBE; }
            set { _CodigoDBE = value; }
        }
        public int RequerenteCodigo
        {
            get { return _RequerenteCodigo; }
            set { _RequerenteCodigo = value; }
        }
        public String RequerenteNome
        {
            get { return _RequerenteNome; }
            set { _RequerenteNome = value; }
        }
        public String RequerenteCPF
        {
            get { return _RequerenteCPF; }
            set { _RequerenteCPF = value; }
        }
        public String RequerenteDDD
        {
            get { return _RequerenteDDD; }
            set { _RequerenteDDD = value; }
        }
        public String RequerenteTelefone
        {
            get { return _RequerenteTelefone; }
            set { _RequerenteTelefone = value; }
        }
        public String RequerenteEmail
        {
            get { return _RequerenteEmail; }
            set { _RequerenteEmail = value; }
        }
        public String SedeNome
        {
            get { return _SedeNome; }
            set { _SedeNome = value; }
        }

        public int AssociacaoPorte
        {
            get { return _AssociacaoPorte; }
            set { _AssociacaoPorte = value; }
        }
        public Nullable<DateTime> AssociacaoTempoDuracao
        {
            get { return _AssociacaoTempoDuracao; }
            set { _AssociacaoTempoDuracao = value; }
        }
        public String SedeCEP
        {
            get { return _SedeCEP; }
            set { _SedeCEP = value; }
        }
        public String SedeUF
        {
            get { return _SedeUF; }
            set { _SedeUF = value; }
        }
        public String SedeMunicipio
        {
            get { return _SedeMunicipio; }
            set { _SedeMunicipio = value; }
        }
        public String SedeNomeMunicipio
        {
            get { return _nomeMunicipioSede; }
            set { _nomeMunicipioSede = value; }
        }
        public String SedeBairro
        {
            get { return _SedeBairro; }
            set { _SedeBairro = value; }
        }
        public String SedeTipoLogradouro
        {
            get { return _SedeTipoLogradouro; }
            set { _SedeTipoLogradouro = value; }
        }
        public String SedeLogradouro
        {
            get { return _SedeLogradouro; }
            set { _SedeLogradouro = value; }
        }
        public String SedeNumero
        {
            get { return _SedeNumero; }
            set { _SedeNumero = value; }
        }
        public String SedeComplemento
        {
            get { return _SedeComplemento; }
            set { _SedeComplemento = value; }
        }

        public String ObjetoSocial
        {
            get { return _ObjetoSocial; }
            set { _ObjetoSocial = value; }
        }
        public String ArtigoEstatuto
        {
            get { return _ArtigoEstatuto; }
            set { _ArtigoEstatuto = value; }
        }
        public String Num_Artigo_Estatuto_Deliberacao
        {
            get { return _Num_Artigo_Estatuto_Deliberacao; }
            set { _Num_Artigo_Estatuto_Deliberacao = value; }
        }
        public String Num_Artigo_Estatuto_Alteracao
        {
            get { return _Num_Artigo_Estatuto_Alteracao; }
            set { _Num_Artigo_Estatuto_Alteracao = value; }
        }
        public String Num_Artigo_Estatuto_Dissolucao
        {
            get { return _Num_Artigo_Estatuto_Dissolucao; }
            set { _Num_Artigo_Estatuto_Dissolucao = value; }
        }
        public string Num_Artigo_Estatuto_Obrigacoes_Sociais
        {
            get { return _Num_Artigo_Estatuto_Obrigacoes_Sociais; }
            set { _Num_Artigo_Estatuto_Obrigacoes_Sociais = value; }
        }
        public String Num_Artigo_Estatuto_Fundo_Social
        {
            get { return _Num_Artigo_Estatuto_Fundo_Social; }
            set { _Num_Artigo_Estatuto_Fundo_Social = value; }
        }
        public String ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }

        public String CNPJ_Orgao_Registro
        {
            get { return _CNPJ_Orgao_Registro; }
            set
            {
                _CNPJ_Orgao_Registro = value;
                _orgaoRegistro.cnpj = _CNPJ_Orgao_Registro;
                if (!string.IsNullOrEmpty(_orgaoRegistro.cnpj))
                {

                    _parametro = new bParametro(_CNPJ_Orgao_Registro);
                    _orgaoRegistro.Populate();
                }
            }
        }

        public List<bSocios> SociosSiarco
        {
            get { return (List<bSocios>)_SociosSiarco; }
            set { _Socios = value; }
        }
        public List<bSocios> AdminSiarco
        {
            get { return (List<bSocios>)_AdminSiarco; }
            set { _Socios = value; }
        }

        public List<bSocios> Socios
        {
            get { return (List<bSocios>)_Socios; }
            set { _Socios = value; }
        }
        public List<bSocios> SociosTodos
        {
            get { return GetSociosTodos(); }
        }
        public List<bSocios> SociosAtuais
        {
            get { return GetSociosAtuais(); }
        }
        public List<bSocios> SociosAtivos
        {
            get { return GetSociosAtivos(); }
        }
        public List<bSocios> SociosAtivosExaminador
        {
            get { return GetSociosAtivosExaminador(); }
        }
        public List<bSocios> SociosAtivosSemSaida
        {
            get { return GetSociosAtivosSemSaida(); }
        }
        public List<bSocios> AdministradoresAtivos
        {
            get { return GetAdministradoresAtivos(); }
        }
        public List<bSocios> AdministradoresTodos
        {
            get { return GetAdministradoresTodos(); }
        }
        public List<bSocios> AdministradoresIsoladamente
        {
            get { return GetAdministradoresIsoladamente(); }
        }
        public List<bSocios> AdministradoresConjuntamente
        {
            get { return GetAdministradoresConjuntamente(); }
        }

        public List<bSocios> AdministradoresAtual
        {
            get { return GetAdministradoresAtual(); }
        }
        public List<bSocios> AdministradoresSomente
        {
            get { return GetAdministradoresSomente(); }
        }
        public List<bSocios> SociosAdminstradorAtivos
        {
            get { return GetSociosAdminstradorAtivos(); }
        }
        public List<bSocios> ListRepresentanteCapa
        {
            get { return GetRepresentanteCapa(); }
        }



        public Nullable<DateTime> DataConstituicao
        {
            get { return _DataConstituicao; }
            set { _DataConstituicao = value; }
        }
        public Nullable<DateTime> DataTerminoAtividade
        {
            get { return _dataTerminoAtividade; }
            set { _dataTerminoAtividade = value; }
        }

        public List<bSocios> TodoQSA
        {
            get { return (List<bSocios>)_TodoQSA; }
            set { _TodoQSA = value; }
        }
        public List<bFilial> Filiais
        {
            get { return (List<bFilial>)_Filiais; }
            set { _Filiais = value; }
        }

        public List<bCNAE> CNAEs
        {
            get { return (List<bCNAE>)_CNAEs; }
            set { _CNAEs = value; }
        }
        public List<bCNAE> CnaeSec
        {
            get { return (List<bCNAE>)_cnaeSec; }
            set { _cnaeSec = value; }
        }
        public List<bSocios> FundadorDiretor
        {
            get { return (List<bSocios>)_FundadorDiretor; }
            set { _FundadorDiretor = value; }
        }

        public int Num_Fundadores_Diretores
        {
            get { return (int)_Num_Fundadores_Diretores; }
            set { _Num_Fundadores_Diretores = value; }
        }

        //public int Forma_Convocacao
        //{
        //    get { return _Forma_Convocacao; }
        //    set { _Forma_Convocacao = value; }
        //}

        public string Edital_Fixado_Sede
        {
            get { return _Edital_Fixado_Sede; }
            set { _Edital_Fixado_Sede = value; }
        }
        public string Edital_Publicado_Jornal
        {
            get { return _Edital_Publicado_Jornal; }
            set { _Edital_Publicado_Jornal = value; }
        }
        public string Edital_Outros
        {
            get { return _Edital_Outros; }
            set { _Edital_Outros = value; }
        }
        public String Num_Artigo_Estatuto_Convocacao
        {
            get { return _Num_Artigo_Estatuto_Convocacao; }
            set { _Num_Artigo_Estatuto_Convocacao = value; }
        }



        public String Quorum_Deliberacao
        {
            get { return _Quorum_Deliberacao; }
            set { _Quorum_Deliberacao = value; }
        }



        public String Quorum_Alteracao
        {
            get { return _Quorum_Alteracao; }
            set { _Quorum_Alteracao = value; }
        }



        public String Quorum_Dissolucao
        {
            get { return _Quorum_Dissolucao; }
            set { _Quorum_Dissolucao = value; }
        }

        public String Outro_Quorum_Deliberacao
        {
            get { return _Outro_Quorum_Deliberacao; }
            set { _Outro_Quorum_Deliberacao = value; }
        }

        public String Outro_Quorum_Alteracao
        {
            get { return _Outro_Quorum_Alteracao; }
            set { _Outro_Quorum_Alteracao = value; }
        }

        public String Outro_Quorum_Dissolucao
        {
            get { return _Outro_Quorum_Dissolucao; }
            set { _Outro_Quorum_Dissolucao = value; }
        }

        public String Destino_Patrimonio
        {
            get { return _Destino_Patrimonio; }
            set { _Destino_Patrimonio = value; }
        }

        public String Obrigacoes_Sociais
        {
            get { return _Obrigacoes_Sociais; }
            set { _Obrigacoes_Sociais = value; }
        }

        public String Possui_Fundo_Social
        {
            get { return _Possui_Fundo_Social; }
            set { _Possui_Fundo_Social = value; }
        }

        public String Fonte__Contribuicoes_Mensais
        {
            get { return _Fonte_Contribuicoes_Mensais; }
            set { _Fonte_Contribuicoes_Mensais = value; }
        }

        public String Fonte__Contribuicoes_Doacao
        {
            get { return _Fonte_Contribuicoes_Doacao; }
            set { _Fonte_Contribuicoes_Doacao = value; }
        }

        public String Fonte__Recursos_Governamentais
        {
            get { return _Fonte_Recursos_Governamentais; }
            set { _Fonte_Recursos_Governamentais = value; }
        }

        public String Num_Artigo_Estatuto_Associacao
        {
            get { return _Num_Artigo_Estatuto_Associacao; }
            set { _Num_Artigo_Estatuto_Associacao = value; }
        }

        public String Nome_Advogado
        {
            get { return _Nome_Advogado; }
            set { _Nome_Advogado = value; }
        }

        public String CPF_Advogado
        {
            get { return _CPF_Advogado; }
            set { _CPF_Advogado = value; }
        }

        public String Inscricao_OAB_Advogado
        {
            get { return _Inscricao_OAB_Advogado; }
            set { _Inscricao_OAB_Advogado = value; }
        }

        public String UF_OAB_Advogado
        {
            get { return _UF_OAB_Advogado; }
            set { _UF_OAB_Advogado = value; }
        }

        public String Nome_Contador
        {
            get { return _Nome_Contador; }
            set { _Nome_Contador = value; }
        }

        public String CPF_Contador
        {
            get { return _CPF_Contador; }
            set { _CPF_Contador = value; }
        }

        public String CRC_Contador
        {
            get { return _CRC_Contador; }
            set { _CRC_Contador = value; }
        }
        public String UF_CRC
        {
            get { return _UF_CRC; }
            set { _UF_CRC = value; }
        }
        public int Num_Paginas
        {
            get { return (int)_Num_Paginas; }
            set { _Num_Paginas = value; }
        }

        public int Num_Vias
        {
            get { return (int)_Num_Vias; }
            set { _Num_Vias = value; }
        }
        public String pUsuarioLogadoDaJunta
        {
            get
            {
                return _pUsuarioLogadoDaJunta == "" ? "" : _pUsuarioLogadoDaJunta;
            }
            set { _pUsuarioLogadoDaJunta = value; }
        }
        public String Ata_Mesmo_Instrumento
        {
            get { return _Ata_Mesmo_Instrumento; }
            set { _Ata_Mesmo_Instrumento = value; }
        }
        public decimal CapitalSocial
        {
            get { return _CapitalSocial; }
            set { _CapitalSocial = value; }
        }
        public decimal ValorCota
        {
            get { return _ValorCota; }
            set { _ValorCota = value; }
        }
        public decimal QtdCotas
        {
            get { return _QtdCotas; }
            set { _QtdCotas = value; }
        }

        public List<bAto> Atos
        {
            get { return (List<bAto>)_Atos; }
            set { _Atos = value; }
        }
        public List<bProtocoloEvento> ProtocoloEvento
        {
            get { return (List<bProtocoloEvento>)_bProtocoloEvento; }
            set { _bProtocoloEvento = value; }
        }

        public List<bExigencias> Exigencias
        {
            get { return (List<bExigencias>)_Exigencias; }
            set { _Exigencias = value; }
        }


        public string Foro
        {
            get { return _Foro; }
            set { _Foro = value; }
        }

        public string ClausulaAbritral
        {
            get { return _clausulaAbritral; }
            set { _clausulaAbritral = value; }
        }

        public string RepresentanteCapa
        {
            get { return _representanteCapa; }
            set { _representanteCapa = value; }
        }
        public string DddTelefoneCapa
        {
            get { return _dddTelefoneCapa; }
            set { _dddTelefoneCapa = value; }
        }

        public string TelefoneCapa
        {
            get { return _telefoneCapa; }
            set { _telefoneCapa = value; }
        }

        public string EmailCapa
        {
            get { return _emailCapa; }
            set { _emailCapa = value; }
        }



        public List<bExigencias> ExigenciasOutras
        {
            get { return (List<bExigencias>)_ExigenciasOutras; }
            set { _ExigenciasOutras = value; }
        }
        public List<bExigencias> ExigenciasConfirmacao
        {
            get { return (List<bExigencias>)_ExigenciasConfirmacao; }
            set { _ExigenciasConfirmacao = value; }
        }
        public List<bProtocoloConfirmacao> Confirmacoes
        {
            get { return (List<bProtocoloConfirmacao>)_Confirmacoes; }
            set { _Confirmacoes = value; }
        }

        public List<dcnt_clausulas_adicionais> ClausulasAdicionais
        {
            get { return (List<dcnt_clausulas_adicionais>)_ClausulasAdicionais; }
            set { _ClausulasAdicionais = value; }
        }
        public string NomeSiarco
        {
            get { return _nomeSiarco; }
            set { _nomeSiarco = value; }
        }
        public int NumeroAlteracao
        {
            get { return _numeroAlteracao; }
            set { _numeroAlteracao = value; }
        }
        public bReqGenProtocolo ReqGenprotocolo
        {
            get { return _reqGenprotocolo; }
            set { _reqGenprotocolo = value; }
        }
        public bContabilista Contabilista
        {
            get { return _contaabilista; }
            set { _contaabilista = value; }
        }
        public bool ehUltimaTela
        {
            get { return _ehUltimaTela; }
            set { _ehUltimaTela = value; }
        }
        public bool Consulta
        {
            get { return _Consulta; }
            set { _Consulta = value; }
        }
        public String UsuarioCPF
        {
            get { return _UsuarioCPF; }
            set { _UsuarioCPF = value; }
        }
        public string ObsExigencia
        {
            get { return _obsExigencia; }
            set { _obsExigencia = value; }
        }
        public string Justificativa_Visto
        {
            get { return _justificativa_visto; }
            set { _justificativa_visto = value; }
        }
        public string Nome_Fantasia
        {
            get { return _Nome_Fantasia; }
            set { _Nome_Fantasia = value; }
        }
        public string SedeEmail
        {
            get { return _SedeEmail; }
            set { _SedeEmail = value; }
        }
        public string Moeda_Corrente
        {
            get { return _moeda_corrente; }
            set { _moeda_corrente = value; }
        }
        public List<bSociosQuotas> TransferenciaQuotas
        {
            get { return _transferenciaQuotas; }
            set { _transferenciaQuotas = value; }
        }
        public int FilialComSedeFora
        {
            get { return _FilialComSedeFora; }
            set { _FilialComSedeFora = value; }
        }
        public List<bAlertaRequerimento> Alertas
        {
            get { return _Alertas; }
            set { _Alertas = value; }
        }
        public string T005_protocolo_orgao_origem
        {
            get { return _t005_protocolo_orgao_origem; }
            set { _t005_protocolo_orgao_origem = value; }
        }
        public int T003_IND_CNAE_DESTACADA
        {
            get { return _T003_IND_CNAE_DESTACADA; }
            set { _T003_IND_CNAE_DESTACADA = value; }
        }
        public string ProtocoloEnquadramento
        {
            get { return _ProtocoloEnquadramento; }
            set { _ProtocoloEnquadramento = value; }
        }
        public string T005_uf_origem
        {
            get { return _t005_uf_origem; }
            set { _t005_uf_origem = value; }
        }
        public int EmpresaUnipessoal
        {
            get { return _empresaUnipessoal; }
            set { _empresaUnipessoal = value; }
        }
        public int LocalEntregaProcesso
        {
            get { return _localEntregaprocesso; }
            set { _localEntregaprocesso = value; }
        }
        public string CodUnidadeRntrega
        {
            get { return _codUnidadeEntrega; }
            set { _codUnidadeEntrega = value; }
        }
        public int EventoConsolidacao
        {
            get { return _eventoConsolidacao; }
            set { _eventoConsolidacao = value; }
        }
        public int EventoReativacao
        {
            get { return _eventoReativacao; }
            set { _eventoReativacao = value; }
        }
        public int EventoReRatificacao
        {
            get { return _eventoReRatificacao; }
            set { _eventoReRatificacao = value; }
        }


        public List<bAtoEventoCapa> EventoCapa
        {
            get { return _eventoCapa; }
            set { _eventoCapa = value; }
        }

        public string TipoCorrelativo
        {
            get { return _tipoCorrelativo; }
            set { _tipoCorrelativo = value; }
        }

        public int TipoRequerimento
        {
            get { return _tipoRequerimento; }
            set { _tipoRequerimento = value; }
        }
        public int IndTransfUnipessoal
        {
            get { return _indTransfUnipessoal; }
            set { _indTransfUnipessoal = value; }
        }
        public int IndDbeCarregado
        {
            get { return _indDbeCarregado; }
            set { _indDbeCarregado = value; }
        }
        public string DescricaoReducaoCapital
        {
            get { return _descricaoReducaoCapital; }
            set { _descricaoReducaoCapital = value; }
        }
        public string TextoReducaoCapital
        {
            get { return _t003_ds_reducao_capital; }
            set { _t003_ds_reducao_capital = value; }
        }

        public int IndicadorReducaoCapital
        {
            get { return _t003_in_reducao_capital; }
            set { _t003_in_reducao_capital = value; }
        }

        public List<bDivergenciaDBE> ListDivergenciaDBE
        {
            get { return _listDivergenciaDBE; }
            set { _listDivergenciaDBE = value; }
        }
        public int IntegralizandoCapital
        {
            get { return _integralizandoCapital; }
            set { _integralizandoCapital = value; }
        }
        public int IndicadorSPE
        {
            get { return _indicadorSPE; }
            set { _indicadorSPE = value; }
        }
        public int TipoEmpresa
        {
            get { return _tipoEmpresa; }
            set { _tipoEmpresa = value; }
        }

        public string Iptu
        {
            get { return _t003_iptu; }
            set { _t003_iptu = value; }
        }

        public decimal AreaUtilizada
        {
            get { return _t003_area_utilizada; }
            set { _t003_area_utilizada = value; }
        }

        #region JUCERJA
        public string RequerimentoAnterior
        {
            get { return _requerimentoAnterior; }
            set { _requerimentoAnterior = value; }

        }
        public String SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        public String UsuarioLOGIN
        {
            get { return _UsuarioLOGIN; }
            set { _UsuarioLOGIN = value; }
        }
        public String UsuarioSENHA
        {
            get { return _UsuarioSENHA; }
            set { _UsuarioSENHA = value; }
        }

        public String UsuarioTIPO
        {
            get { return _UsuarioTIPO; }
            set { _UsuarioTIPO = value; }
        }
        public String UsuarioCODIGO
        {
            get { return _UsuarioCODIGO; }
            set { _UsuarioCODIGO = value; }
        }
        #endregion

        #endregion

        #region Campos Resposta Comparacao DBE x Viabilidade

        public List<bool> _DBEViab_Erro_Evento = new List<bool>();
        public bool _DBEViab_Erro_NomeEmpresa = false;
        public bool _DBEViab_Erro_TipoLogradouro = false;
        public bool _DBEViab_Erro_Logradouro = false;
        public bool _DBEViab_Erro_Numero = false;
        public bool _DBEViab_Erro_Complemento = false;
        public bool _DBEViab_Erro_Municipio = false;
        public bool _DBEViab_Erro_Bairro = false;
        public bool _DBEViab_Erro_CEP = false;
        public bool _DBEViab_Erro_UF = false;
        public List<bool> _DBEViab_Erro_CNAE = new List<bool>();
        public List<bool> _DBEViab_Erro_Socios = new List<bool>();
        public List<string> _DBEViab_Mensagens_Erro = new List<string>();
        public bool _DBEViab_Erro_Natureza = false;
        #endregion

        #region Implements
        public bRequerimento()
        {
            //_dbe = new bRequerimento();
            //_viabilidade = new bRequerimento();
        }

        public void updateEventosRequerimento1()
        {
            using (dr013_Requerimento_Evento c = new dr013_Requerimento_Evento())
            {
                dr013_Requerimento_Evento ev = new dr013_Requerimento_Evento();
                ev.t004_nr_cnpj_org_reg = _orgaoRegistro.cnpj;
                ev.T005_nr_protocolo = _ProtocoloRequerimento;

                //ev.Delete();

                foreach (dr013_Requerimento_Evento ev1 in _reqEventos)
                {
                    ev1.Update();
                }
            }
        }

        public void UpdateCanes()
        {
            using (dR004_Atuacao a = new dR004_Atuacao())
            {
                a.Deleta(_CodigoEmpresa);

                foreach (bCNAE c in _CNAEs)
                {
                    a.t001_sq_pessoa = _CodigoEmpresa;
                    a.a001_co_atividade = c.CodigoCNAE;
                    a.r004_in_principal_secundario = c.TipoAtividade.ToString();
                    a.Update();
                }
            }

        }
        ///
        ///
        ///
        public void UpdateProtocoloOrgaoRegistro()
        {
            using (dT005_Protocolo c = new dT005_Protocolo())
            {

                c.UpdateProtocoloOrgaoRegistro(_CNPJ_Orgao_Registro, _ProtocoloRequerimento, _ProtocoloRCPJ);
            }
        }

        public void UpdateClausulaArbitral()
        {

            using (dT005_Protocolo c = new dT005_Protocolo())
            {

                c.UpdateClausulaArbitral(_ProtocoloRequerimento, _clausualArbitral);
            }
        }
        public void GravaTipoRegistroViab(string _tipoRegistro)
        {
            using (dT005_Protocolo c = new dT005_Protocolo())
            {

                c.GravaTipoRegistroViab(_tipoRegistro ,_ProtocoloRequerimento);
            }
        }

        public void UpdateTextoRestituicaoBaixa()
        {

            using (dT005_Protocolo c = new dT005_Protocolo())
            {

                c.UpdateTextoRestituicaoBaixa(_ProtocoloRequerimento, _textoRestituicaoBaixa);
            }
        }
        public void UpdateProtocoloEnquadramento()
        {

            using (dT005_Protocolo c = new dT005_Protocolo())
            {

                c.UpdateProtocoloEnquadramento(_CNPJ_Orgao_Registro, _ProtocoloRequerimento, _ProtocoloEnquadramento);
            }
        }
        /// <summary>
        /// Gera o número do protocolo do órgão de registro
        /// </summary>
        public void GeraProtocoloJunta()
        {
            //string _numProtocoloJunta = string.Empty;
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dT005_Protocolo c = new dT005_Protocolo())
                    {
                        c.MainConnectionProvider = cp;
                        _ProtocoloRCPJ = c.GeraProtocoloJunta();

                        c.UpdateProtocoloOrgaoRegistro(_CNPJ_Orgao_Registro, _ProtocoloRequerimento, _ProtocoloRCPJ);

                        cp.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

        /// <summary>
        /// Gera o número do protocolo do órgão de registro
        /// </summary>
        public void GeraProtocoloEnquadramento()
        {
            //string _numProtocoloJunta = string.Empty;
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dT005_Protocolo c = new dT005_Protocolo())
                    {
                        c.MainConnectionProvider = cp;
                        _ProtocoloEnquadramento = c.GeraProtocoloJunta();

                        c.UpdateProtocoloEnquadramento(_CNPJ_Orgao_Registro, _ProtocoloRequerimento, _ProtocoloEnquadramento);

                        cp.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

        private bOrgaoRegistro getOrgaoRegistro()
        {
            bOrgaoRegistro c = new bOrgaoRegistro(_CNPJ_Orgao_Registro);
            return c;
        }

        public void LoadCNAE()
        {
            //using (dA001_Atividade a = new dA001_Atividade())
            //{


            //}
        }
        public void UpdateNumeroDBE()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.UpdateNumeroDBE(_ProtocoloRequerimento, _CodigoDBE);

                p.UpdateNumeroDBEPJ(_CodigoEmpresa, _CodigoDBE);
            }
        }

        public void UpdateNumeroDARE()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.UpdateNumeroDARE(_ProtocoloRequerimento, _numeroDARE);
            }
        }


        public void UpdateViabiIncorporacaoExcluir()
        {
            int SqEmpresa = _CodigoEmpresa;
            int SqRequerente = _RequerenteCodigo;


            if (_ProtocoloRequerimento == String.Empty)
            {
                using (dCorrelativo c = new dCorrelativo())
                {
                    c.Tipo = Int32.Parse(CONST_PROTOCOLO_CORRELATIVO);
                    _ProtocoloRequerimento = c.GetCorrelativo();
                }
            }



            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                #region Requerente
                using (dT001_Pessoa p = new dT001_Pessoa())
                {
                    p.MainConnectionProvider = cp;
                    p.t001_in_tipo_pessoa = CONST_PESSOA_JURIDICA;
                    p.t001_ds_pessoa = SedeNome;
                    p.t001_in_dados_atualizados = "S";
                    p.t001_dt_ult_atualizacao = DateTime.Now;
                    p.t001_email = "";
                    p.t001_ddd = _SedeDDD;
                    p.t001_tel_1 = _SedeTelefone;
                    p.t001_email = _SedeEmail;
                    p.t001_tel_2 = "";
                    p.t001_sq_pessoa = SqEmpresa;
                    SqEmpresa = p.Update();
                    _CodigoEmpresa = SqEmpresa;
                }

                //2. Gravando Pessoa Jurídica (Requerimento)
                //(dados da pessoa juridica Empresa)

                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cp;
                    pj.t001_sq_pessoa = SqEmpresa;
                    pj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    if (_CodigoEvento == 102)
                    {
                        pj.t003_nr_matricula = _nrMatricula;
                        pj.t003_nr_cnpj = _nrEmpresaCNPJ;
                    }
                    else
                    {
                        pj.t003_nr_matricula = ""; // _nrMatricula;
                        pj.t003_nr_cnpj = ""; // nrEmpresaCNPJ;
                    }
                    //if (_NaturezaJuridicaCodigo == 2240)
                    if (_Enquadramento != 0)
                        pj.t003_tipo_enquadramento = _Enquadramento;
                    pj.t003_nr_inscricao_estadual = "";
                    pj.t003_nr_inscricao_municipal = "";
                    pj.t006_nr_cnpj_org_reg_ant = "";
                    pj.t003_nr_matricula_anterior = "";
                    pj.a006_co_natureza_juridica = _NaturezaJuridicaCodigo;
                    pj.a011_co_porte = 0;
                    pj.a007_co_situacao = 0;
                    pj.a008_co_status_atual = 0;
                    pj.t003_vl_capital_social = _CapitalSocial;
                    pj.t003_vl_qtd_cotas = _QtdCotas;
                    pj.t003_vl_valor_cota = _ValorCota;
                    pj.t003_vl_capital_integralizado = _capital_integralizado;
                    pj.t003_vl_capital_nao_integralizado = _capital_nao_integralizado;
                    pj.t003_data_limite_integralizacao = _data_limite_integralizacao;
                    pj.t003_ds_capital_nao_integralizado = _ds_capital_nao_integralizado;
                    pj.t003_co_tipo_pes_jur = _TipoPessoaJuridicaCodigo;
                    pj.t003_dt_inicio_atividade = _DataInicioSociedade;
                    pj.t003_dt_prazo_determinado = (_AssociacaoTempoDuracao);
                    pj.t003_ds_objeto_social = _ObjetoSocial;
                    pj.t003_prot_viab = _ProtocoloViabilidade;
                    pj.t003_DBE = _CodigoDBE;
                    pj.Update();
                }


                //3.Gravando Vinculo Endereço
                //(dados do endereço da Empresa)

                using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                {
                    int SqVinculoEndereco;
                    ve.MainConnectionProvider = cp;
                    ve.t001_sq_pessoa = SqEmpresa;
                    if (IsNumtValidated(_SedeTipoLogradouro) && !string.IsNullOrEmpty(_SedeTipoLogradouro))
                    {
                        ve.a015_co_tipo_logradouro = Convert.ToDecimal(SedeTipoLogradouro);
                        ve.a015_ds_tipo_logradouro = SedeDsTipoLogradouro;
                    }
                    else
                    {
                        ve.a015_ds_tipo_logradouro = SedeDsTipoLogradouro;
                    }

                    ve.r002_ds_logradouro = _SedeLogradouro;
                    ve.r002_nr_logradouro = _SedeNumero;
                    ve.r002_ds_complemento = _SedeComplemento;
                    ve.r002_ds_bairro = _SedeBairro;
                    ve.a005_co_municipio = decimal.Parse(_SedeMunicipio);
                    ve.a004_co_pais = 154;
                    ve.r002_nr_cep = _SedeCEP;
                    SqVinculoEndereco = ve.Update();
                }

                //4.Gravando Protocolo
                //(dados do protocolo da Empresa)

                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    p.MainConnectionProvider = cp;
                    p.t005_dt_entrada = DateTime.Now;
                    p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    p.t005_nr_protocolo = _ProtocoloRequerimento;
                    p.t001_sq_pessoa = SqEmpresa;
                    p.t005_nr_protocolo_prefeitura = _ProtocoloPrefeitura;
                    p.t005_nr_protocolo_viabilidade = _ProtocoloViabilidade;
                    p.t005_nr_docad = _CodigoDOCAD;
                    p.t005_nr_dbe = _CodigoDBE;
                    p.T005_nr_protocolo_RCPJ = _ProtocoloRCPJ;
                    p.UpdateViabili();
                }

                //5.Gravando Protocolo Requerimento
                //(dados complementares Requerimento)

                using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                {
                    pr.MainConnectionProvider = cp;
                    pr.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    pr.t005_nr_protocolo = _ProtocoloRequerimento;
                    pr.t006_ds_artigo_estatuto = _ArtigoEstatuto;
                    pr.t006_edital_fixado_sede = _Edital_Fixado_Sede;
                    pr.t006_edital_publicado_jornal = _Edital_Publicado_Jornal;
                    pr.t006_edital_outros = _Edital_Outros;
                    pr.t006_nr_art_estatuto_convocacao = _Num_Artigo_Estatuto_Convocacao;
                    pr.t006_ds_quorum_deliberacao = _Quorum_Deliberacao;
                    pr.t006_ds_quorum_alteracao = _Quorum_Alteracao;
                    pr.t006_ds_quorum_dissolucao = _Quorum_Dissolucao;
                    pr.t006_ds_outro_quorum_deliberacao = _Outro_Quorum_Deliberacao;
                    pr.t006_ds_outro_quorum_alteracao = _Outro_Quorum_Alteracao;
                    pr.t006_ds_outro_quorum_dissolucao = _Outro_Quorum_Dissolucao;
                    pr.t006_ds_destino_patrimonio = _Destino_Patrimonio;
                    pr.t006_in_possui_fundo_social = _Possui_Fundo_Social;
                    pr.t006_in_obrigacoes_sociais = _Obrigacoes_Sociais;
                    pr.t006_recurso_mensalidade = _Fonte_Contribuicoes_Mensais;
                    pr.t006_recurso_doacao = _Fonte_Contribuicoes_Doacao;
                    pr.t006_recurso_governamental = _Fonte_Recursos_Governamentais;
                    pr.t006_nr_art_estatuto_associacao = _Num_Artigo_Estatuto_Associacao;
                    pr.t006_ds_nome_advogado = _Nome_Advogado;
                    pr.t006_nr_cpf_advogado = _CPF_Advogado;
                    pr.t006_nr_inscr_oab = _Inscricao_OAB_Advogado;
                    pr.t006_ds_uf_oab_advogado = _UF_OAB_Advogado;
                    pr.t006_ds_nome_contador = _Nome_Contador;
                    pr.t006_nr_cpf_contador = _CPF_Contador;
                    pr.t006_nr_crc_contador = _CRC_Contador;
                    pr.a004_co_uf = _UF_CRC;
                    pr.t006_in_ata_mesmo_instrumento = _Ata_Mesmo_Instrumento;
                    pr.t006_nr_art_deliberacao = _Num_Artigo_Estatuto_Deliberacao;
                    pr.t006_nr_art_alteracao = _Num_Artigo_Estatuto_Alteracao;
                    pr.t006_nr_art_dissolucao = _Num_Artigo_Estatuto_Dissolucao;
                    pr.t006_nr_art_obrigacoes_sociais = _Num_Artigo_Estatuto_Obrigacoes_Sociais;
                    pr.t006_nr_art_fundo_social = _Num_Artigo_Estatuto_Fundo_Social;
                    if (TipoPessoaJuridicaCodigo == 1264)
                        pr.UpdateSociedade();
                    else
                        pr.Update();
                }

                // Gravando Status do Protocolo
                using (dT011_Protocolo_Status ps = new dT011_Protocolo_Status())
                {
                    ps.MainConnectionProvider = cp;
                    ps.T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                    ps.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                    ps.T011_IN_SITUACAO = _status_protocolo;
                    ps.T011_DT_SITUACAO = DateTime.Now;

                    if (string.IsNullOrEmpty(UsuarioRegin))
                    {
                        UsuarioRegin = "REQUERENTE";
                    }
                    if (!string.IsNullOrEmpty(UsuarioCPF))
                    {
                        UsuarioRegin = _UsuarioCPF;
                    }
                    ps.T011_USUARIO = UsuarioRegin;
                    if (_status_protocolo == "0")
                    {
                        ps.Update();
                    }
                }
                //6. Gravando Requerente (Pessoa)
                //(Dados do Requerente)
                using (dT001_Pessoa p = new dT001_Pessoa())
                {

                    p.MainConnectionProvider = cp;
                    p.t001_in_tipo_pessoa = CONST_PESSOA_FISICA;
                    p.t001_ds_pessoa = _RequerenteNome;
                    p.t001_in_dados_atualizados = "S";
                    p.t001_dt_ult_atualizacao = DateTime.Now;
                    p.t001_email = _RequerenteEmail;
                    p.t001_ddd = _RequerenteDDD;
                    p.t001_tel_1 = _RequerenteTelefone;
                    p.t001_tel_2 = "";
                    p.t001_sq_pessoa = SqRequerente;
                    SqRequerente = p.Update();
                    _RequerenteCodigo = SqRequerente;


                }

                //7. Gravando Requerente (Pessoa Fisica)
                //(Dados do Requerente)

                using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                {
                    pf.MainConnectionProvider = cp;

                    pf.t001_sq_pessoa = SqRequerente;
                    pf.t002_nr_cpf = _RequerenteCPF;
                    pf.Update();
                }

                //Gravando Vinculo com a PJ (Vinculo Requerente)
                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cp;
                    v.t001_sq_pessoa = SqRequerente;
                    v.t001_sq_pessoa_pai = SqEmpresa;
                    v.a009_co_condicao = 500; // 2066 = Requerente
                    v.r001_dt_entrada_vinculo = DateTime.Now;
                    v.r001_ds_cargo_direcao = "REQUERENTE";
                    v.r001_in_situacao = "A";
                    v.r001_vl_participacao = 0;
                    v.Update();
                }
                // Gravando Ato e Evento
                //
                using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
                {
                    pe.MainConnectionProvider = cp;
                    foreach (bProtocoloEvento a in _bProtocoloEvento)
                    {
                        pe.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        pe.t007_nr_protocolo = _ProtocoloRequerimento;
                        pe.a003_co_evento = a.CodigoEvento;
                        //pe.a002_co_ato = a.CodigoAto;
                        pe.t001_sq_pessoa = _CodigoEmpresa;
                        pe.Update();
                    }
                }
                //8.Gravando CNAE
                //(Codigo de Atividade Economica do Requerimento)

                using (dR004_Atuacao a = new dR004_Atuacao())
                {
                    a.MainConnectionProvider = cp;
                    foreach (bCNAE c in _CNAEs)
                    {
                        a.t001_sq_pessoa = SqEmpresa;
                        a.a001_co_atividade = c.CodigoCNAE;
                        a.r004_in_principal_secundario = c.TipoAtividade.ToString();
                        a.r004_exercida = c.Exercida;
                        a.Update();
                    }
                }
                #endregion

                #region Socios LTDA
                using (dT001_Pessoa p = new dT001_Pessoa())
                {

                    int SqSocio;
                    foreach (bSocios s in _Socios)
                    {
                        p.MainConnectionProvider = cp;
                        p.t001_in_tipo_pessoa = "F";
                        s.TipoPessoa = "F"; ;
                        p.t001_ds_pessoa = s.Nome;
                        p.t001_in_dados_atualizados = "S";
                        p.t001_dt_ult_atualizacao = DateTime.Now;
                        if (s.DDD != string.Empty)
                            p.t001_ddd = s.DDD;
                        if (s.Telefone != string.Empty)
                            p.t001_tel_1 = s.Telefone;
                        if (s.SQPessoa != null)
                            p.t001_sq_pessoa = Convert.ToDecimal(s.SQPessoa);
                        else
                            p.t001_sq_pessoa = 0;
                        SqSocio = p.Update();
                        if (SqSocio != 0)
                            s.SQPessoa = SqSocio.ToString();
                        else
                            SqSocio = Convert.ToInt32(s.SQPessoa);
                        if (s.TipoPessoa == "F")
                        {
                            //10.Gravando Socios - pessoa fisica
                            //(Dados dos Socios do requerimento)
                            using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                            {
                                pf.MainConnectionProvider = cp;

                                pf.t001_sq_pessoa = SqSocio;
                                pf.t002_nr_cpf = s.CPFCNPJ;
                                if (s.TipoIdentidade != null && s.TipoIdentidade != String.Empty)
                                    pf.a010_co_tipo_documento = decimal.Parse(s.TipoIdentidade);
                                pf.t002_nr_documento = s.RG;
                                pf.t002_ds_emissor_documento = s.OrgaoExpedidor;
                                pf.a004_uf_org_exped = s.OrgaoExpedidorUF;
                                pf.a004_co_pais = s.NacionalidadeCodigo;
                                pf.t002_ds_nacionalidade = s.Nacionalidade;
                                pf.a004_co_uf_naturalidade = s.NaturalidadeCodigo;
                                if (s.EstadoCivil != null && s.EstadoCivil != String.Empty)
                                    pf.a012_co_estado_civil = decimal.Parse(s.EstadoCivil);
                                if (s.EstadoCivilRegime != null && s.EstadoCivilRegime != String.Empty)
                                {
                                    pf.a013_co_regime_bens = decimal.Parse(s.EstadoCivilRegime);
                                }
                                if (!string.IsNullOrEmpty(s.NacionalidadeCodigo.ToString()))
                                {
                                    pf.a004_co_pais = s.NacionalidadeCodigo;
                                    if (pf.a004_co_pais != 154)
                                    {

                                        pf.t002_tipo_visto = s.tipo_visto;
                                        pf.t002_emissao_visto = s.emissao_visto;
                                        pf.t002_dt_validade_visto = s.validade_visto;
                                    }
                                    else
                                    {
                                        pf.t002_tipo_visto = "";
                                        pf.t002_emissao_visto = null;
                                        pf.t002_dt_validade_visto = null;
                                    }
                                }
                                pf.t002_dt_nascimento = s.DataNascimento;
                                //if (s.Profissao != null && s.Profissao != String.Empty)
                                //    pf.a020_co_profissao = decimal.Parse(s.Profissao);
                                pf.t002_in_sexo = s.in_Sexo;
                                if (s.Profissao_Descricao != null && s.Profissao_Descricao != string.Empty)
                                    pf.t002_ds_profissao = s.Profissao_Descricao;
                                if (s.TipoEmancipado != null && s.TipoEmancipado != String.Empty)
                                {
                                    pf.a014_co_emancipacao = decimal.Parse(s.TipoEmancipado);
                                }
                                pf.t002_nr_qtd_cotas = s.QuotaCapitalSocial;
                                pf.t002_capital_integralizado = s.CapitalIntegralizado;
                                pf.t002_capital_a_integralizar = s.Capital_a_Integralizar;
                                pf.t002_data_final_integralizacao = s.DataIntegralizacao;
                                pf.t002_analfabeto = s.Analfabeto;

                                pf.Update();
                            }
                        }
                        else
                        {
                            //10.Gravando Socios - pessoa jurídica
                            //(Dados dos Socios do requerimento)

                            using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                            {
                                pj.MainConnectionProvider = cp;
                                pj.t001_sq_pessoa = SqSocio;
                                pj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                pj.t003_nr_matricula = "";
                                pj.t003_nr_cnpj = s.CPFCNPJ;
                                pj.t003_nr_inscricao_estadual = "";
                                pj.t003_nr_inscricao_municipal = "";
                                pj.t006_nr_cnpj_org_reg_ant = "";
                                pj.t003_nr_matricula_anterior = "";
                                pj.a011_co_porte = 0;
                                pj.a007_co_situacao = 0;
                                pj.a008_co_status_atual = 0;
                                pj.t003_vl_capital_social = 0;
                                pj.t003_vl_qtd_cotas = 0;
                                pj.t003_vl_valor_cota = 0;
                                pj.Update();
                            }
                        }

                        //11. Gravando Socios - Endereço
                        //(dados dos endereços dos sócios)

                        using (dR002_Vinculo_Endereco es = new dR002_Vinculo_Endereco())
                        {
                            int SqVinculoEndereco;
                            es.MainConnectionProvider = cp;
                            es.t001_sq_pessoa = SqSocio;
                            es.t001_sq_pessoa_pai = SqEmpresa;
                            if (s.EndTipoLogradouro != null && s.EndTipoLogradouro != String.Empty)
                            {
                                es.a015_co_tipo_logradouro = decimal.Parse(s.EndTipoLogradouro);
                                es.a015_ds_tipo_logradouro = s.EndDsTipoLogradouro;
                            }
                            es.r002_ds_logradouro = s.EndLogradouro;
                            es.r002_nr_logradouro = s.EndNumero;
                            es.r002_ds_complemento = s.EndComplemento;
                            es.r002_ds_bairro = s.EndBairro;
                            if (s.EndMunicipio != null && s.EndMunicipio != String.Empty)
                                es.a005_co_municipio = decimal.Parse(s.EndMunicipio);//;
                            if (!string.IsNullOrEmpty(s.EndPais))
                                es.a004_co_pais = Convert.ToInt32(s.EndPais);
                            es.r002_nr_cep = s.EndCEP;
                            SqVinculoEndereco = es.Update();
                        }



                        //12. Gravando Vinculo Sócio com a PJ (Vinculo Sócio)
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.t001_sq_pessoa = SqSocio;
                            v.t001_sq_pessoa_pai = SqEmpresa;
                            if (s.Qualificacao == null)
                                v.a009_co_condicao = 22;
                            else
                                v.a009_co_condicao = decimal.Parse(s.Qualificacao);
                            v.r001_dt_entrada_vinculo = DateTime.Now;
                            //v.r001_dt_saida_vinculo = DateTime.MinValue;
                            if (v.a009_co_condicao == 22)
                                v.r001_ds_cargo_direcao = "Sócio";
                            else
                                v.r001_ds_cargo_direcao = s.Qualificacao_Descricao;
                            v.r001_in_situacao = "A";
                            //v.r001_in_gerente_uso_firma = "";
                            v.r001_vl_participacao = 0;
                            v.r001_vl_participacao = s.QuotaCapitalSocial * _ValorCota;
                            v.t001_sq_pessoa_rep_legal = s.rep_legal;
                            v.Update();
                        }

                    }
                }
                #endregion

                #region ERELI/Individual
                using (dT001_Pessoa p = new dT001_Pessoa())
                {
                    int SqFundDiretor;
                    foreach (bSocios fd in _FundadorDiretor)
                    {
                        p.MainConnectionProvider = cp;

                        p.t001_in_tipo_pessoa = "F";
                        fd.TipoPessoa = "F"; ;
                        p.t001_ds_pessoa = fd.Nome;
                        p.t001_in_dados_atualizados = "S";
                        p.t001_dt_ult_atualizacao = DateTime.Now;
                        if (fd.SQPessoa != null)
                            p.t001_sq_pessoa = Convert.ToDecimal(fd.SQPessoa);
                        else
                            p.t001_sq_pessoa = 0;
                        SqFundDiretor = p.Update();
                        if (SqFundDiretor != 0)
                            fd.SQPessoa = SqFundDiretor.ToString();
                        else
                            SqFundDiretor = Convert.ToInt32(fd.SQPessoa);
                        if (fd.TipoPessoa == "F")
                        {
                            //10.Gravando Diretores Fundadores - pessoa fisica
                            //(Dados dos Diretores Fundadores do requerimento)
                            using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                            {
                                pf.MainConnectionProvider = cp;

                                pf.t001_sq_pessoa = SqFundDiretor;
                                pf.t002_nr_cpf = fd.CPFCNPJ;
                                if (fd.TipoIdentidade != null && fd.TipoIdentidade != String.Empty)
                                    pf.a010_co_tipo_documento = decimal.Parse(fd.TipoIdentidade);
                                pf.t002_nr_documento = fd.RG;
                                pf.t002_ds_emissor_documento = fd.OrgaoExpedidor;
                                pf.a004_uf_org_exped = fd.OrgaoExpedidorUF;
                                if (!string.IsNullOrEmpty(fd.NacionalidadeCodigo.ToString()))
                                {
                                    pf.a004_co_pais = fd.NacionalidadeCodigo;
                                    if (pf.a004_co_pais != 154)
                                    {
                                        pf.t002_tipo_visto = fd.tipo_visto;
                                        pf.t002_emissao_visto = fd.emissao_visto;
                                        pf.t002_dt_validade_visto = fd.validade_visto;
                                    }
                                    else
                                    {
                                        pf.t002_tipo_visto = "";
                                        pf.t002_emissao_visto = null;
                                        pf.t002_dt_validade_visto = null;
                                    }
                                }
                                pf.t002_ds_nacionalidade = fd.Nacionalidade;

                                //pf.a004_co_uf_naturalidade = fd.NaturalidadeCodigo;
                                if (fd.EstadoCivil != null && fd.EstadoCivil != String.Empty)
                                    pf.a012_co_estado_civil = decimal.Parse(fd.EstadoCivil);
                                if (fd.EstadoCivilRegime != null && fd.EstadoCivilRegime != String.Empty)
                                {
                                    pf.a013_co_regime_bens = decimal.Parse(fd.EstadoCivilRegime);
                                }

                                if (fd.Profissao_Descricao != null && fd.Profissao_Descricao != string.Empty)
                                    pf.t002_ds_profissao = fd.Profissao_Descricao;
                                if (TipoPessoaJuridicaCodigo == 6568 || TipoPessoaJuridicaCodigo == 6569)
                                {
                                    pf.t002_capital_integralizado = fd.CapitalIntegralizado;
                                    pf.t002_capital_a_integralizar = fd.Capital_a_Integralizar;

                                    pf.t002_dt_nascimento = fd.DataNascimento;
                                    pf.t002_nome_pai = fd.Nome_Pai;
                                    pf.t002_nome_mae = fd.Nome_Mae;
                                    //pf.t002_in_sexo = fd.Sexo;
                                    if (fd.TipoEmancipado == "")
                                        fd.TipoEmancipado = "0";
                                    pf.a014_co_emancipacao = Convert.ToInt32(fd.TipoEmancipado);
                                }
                                pf.t002_in_sexo = fd.in_Sexo;

                                pf.Update();
                            }
                        }
                        else
                        {
                            //10.Gravando Diretores Fundadores - pessoa jurídica
                            //(Dados dos Diretores Fundadores do requerimento)

                            using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                            {
                                pj.MainConnectionProvider = cp;
                                pj.t001_sq_pessoa = SqFundDiretor;
                                pj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                pj.t003_nr_matricula = "";
                                pj.t003_nr_cnpj = fd.CPFCNPJ;
                                pj.t003_nr_inscricao_estadual = "";
                                pj.t003_nr_inscricao_municipal = "";
                                pj.t006_nr_cnpj_org_reg_ant = "";
                                pj.t003_nr_matricula_anterior = "";
                                pj.a011_co_porte = 0;
                                pj.a007_co_situacao = 0;
                                pj.a008_co_status_atual = 0;
                                pj.Update();
                            }
                        }

                        //11. Gravando Diretores Fundadores - Endereço
                        //(dados dos endereços dos Fundadores Diretores)

                        using (dR002_Vinculo_Endereco efd = new dR002_Vinculo_Endereco())
                        {
                            int SqVinculoEndereco;
                            efd.MainConnectionProvider = cp;
                            efd.t001_sq_pessoa = SqFundDiretor;
                            efd.t001_sq_pessoa_pai = SqEmpresa;
                            if (fd.EndTipoLogradouro != null && fd.EndTipoLogradouro != String.Empty)
                                efd.a015_co_tipo_logradouro = decimal.Parse(fd.EndTipoLogradouro);
                            if (!string.IsNullOrEmpty(fd.EndDsTipoLogradouro))
                                efd.a015_ds_tipo_logradouro = fd.EndDsTipoLogradouro;
                            efd.r002_ds_logradouro = fd.EndLogradouro;
                            efd.r002_nr_logradouro = fd.EndNumero;
                            efd.r002_ds_complemento = fd.EndComplemento;
                            efd.r002_ds_bairro = fd.EndBairro;
                            if (fd.EndMunicipio != null && fd.EndMunicipio != String.Empty)
                                efd.a005_co_municipio = decimal.Parse(fd.EndMunicipio);//;
                            efd.a004_co_pais = 154;
                            efd.r002_nr_cep = fd.EndCEP;

                            SqVinculoEndereco = efd.Update();
                        }



                        //12. Gravando Vinculo com a PJ (Vinculo Diretor Fundador)
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.t001_sq_pessoa = SqFundDiretor;
                            v.t001_sq_pessoa_pai = SqEmpresa;

                            if (fd.Qualificacao != null && fd.Qualificacao != string.Empty)
                            {
                                v.a009_co_condicao = decimal.Parse(fd.Qualificacao);
                                v.r001_dt_inicio_mandato = fd.DataInicioMandato;
                                v.r001_dt_termino_mandato = fd.DataTerminoMandato;
                                v.t001_sq_pessoa_rep_legal = fd.rep_legal;
                            }
                            else
                            {
                                if (_NaturezaJuridicaCodigo == 2135) // CASO EMPRESARIO INDIVIDUAL
                                {
                                    fd.Qualificacao = "50";
                                    fd.Qualificacao_Descricao = "EMPRESÁRIO";
                                    v.a009_co_condicao = decimal.Parse(fd.Qualificacao);

                                }
                                else
                                {
                                    if (_NaturezaJuridicaCodigo == 2305) // CASO EIRELI
                                    {
                                        fd.Qualificacao = "65";
                                        fd.Qualificacao_Descricao = "TITULAR";
                                        v.a009_co_condicao = decimal.Parse(fd.Qualificacao);
                                    }
                                }
                                v.r001_dt_inicio_mandato = null;
                                v.r001_dt_termino_mandato = null; ;
                                v.t001_sq_pessoa_rep_legal = 0;
                            }

                            v.r001_in_fundador = fd.Fundador;
                            v.r001_dt_entrada_vinculo = DateTime.Now;
                            v.r001_ds_cargo_direcao = fd.Qualificacao_Descricao;
                            v.r001_in_situacao = "A";
                            v.Update();
                        }

                    }
                }
                #endregion

                VerificaSocioDuploOuquantidade(cp);
                cp.CommitTransaction();
            }
        }

        public void Update()
        {
            int SqEmpresa = 0;
            int SqRequerente = _RequerenteCodigo;
            int SqFilial;

            if (_controleExisteRequerimento == 0)
                SqEmpresa = 0;
            else
                SqEmpresa = _CodigoEmpresa;

            try
            {
                //1. Gravando Pessoa (Requerimento)
                //(incluir uma pessoa juridica da empresa do requerimento)
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    #region Aba_1
                    if (_TipoGravacao == "Requerente" || _TipoGravacao == "Tudo")
                    {
                        #region 1. GravandoPessoa Empresa
                        using (dT001_Pessoa p = new dT001_Pessoa())
                        {

                            p.MainConnectionProvider = cp;
                            p.t001_in_tipo_pessoa = CONST_PESSOA_JURIDICA;
                            p.t001_ds_pessoa = SedeNome;
                            p.t001_nome_fantasia = _Nome_Fantasia;
                            p.t001_in_dados_atualizados = "S";
                            p.t001_dt_ult_atualizacao = DateTime.Now;
                            p.t001_email = _SedeEmail;
                            p.t001_ddd = _SedeDDD;
                            p.t001_tel_1 = _SedeTelefone;
                            p.t001_tel_2 = "";
                            p.t001_sq_pessoa = SqEmpresa;
                            SqEmpresa = p.Update();
                            _CodigoEmpresa = SqEmpresa;
                        }
                        #endregion

                        #region 2. Gravando Pessoa Jurídica (Requerimento)
                        //(dados da pessoa juridica Empresa)

                        using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                        {
                            pj.MainConnectionProvider = cp;
                            pj.t001_sq_pessoa = SqEmpresa;
                            pj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;

                            if (_CodigoEvento != 101)
                            {
                                pj.t003_nr_matricula = _nrMatricula;
                                pj.t003_nr_cnpj = _nrEmpresaCNPJ;
                            }
                            else
                            {
                                pj.t003_nr_matricula = "";
                                pj.t003_nr_cnpj = "";
                            }
                            //if (_NaturezaJuridicaCodigo == 2240)
                            if (_Enquadramento != 0)
                            {
                                pj.t003_tipo_enquadramento = _Enquadramento;
                            }

                            pj.t003_nr_inscricao_estadual = "";
                            pj.t003_nr_inscricao_municipal = "";
                            pj.t006_nr_cnpj_org_reg_ant = "";
                            pj.t003_nr_matricula_anterior = "";
                            pj.a006_co_natureza_juridica = _NaturezaJuridicaCodigo;
                            pj.a011_co_porte = 0;
                            pj.a007_co_situacao = _SedeSituacao;
                            pj.T003_DS_SITUACAO = _SedeSituacaoDescricao;
                            pj.a008_co_status_atual = 0;
                            pj.t003_vl_capital_social = _CapitalSocial;
                            pj.t003_vl_qtd_cotas = _QtdCotas;
                            pj.t003_vl_valor_cota = _ValorCota;
                            pj.t003_vl_capital_integralizado = _capital_integralizado;
                            pj.t003_vl_capital_nao_integralizado = _capital_nao_integralizado;
                            pj.t003_data_limite_integralizacao = _data_limite_integralizacao;
                            pj.t003_ds_capital_nao_integralizado = _ds_capital_nao_integralizado;
                            pj.t003_moeda_corrente = _moeda_corrente;
                            pj.t003_co_tipo_pes_jur = _TipoPessoaJuridicaCodigo;
                            pj.t003_dt_inicio_atividade = _DataInicioSociedade;
                            pj.t003_dt_prazo_determinado = (_AssociacaoTempoDuracao);
                            pj.t003_dt_termino_ativ = _dataTerminoAtividade;
                            pj.t003_ds_objeto_social = _ObjetoSocial;
                            pj.t003_prot_viab = _ProtocoloViabilidade;
                            pj.t003_DBE = _CodigoDBE;
                            pj.t003_socios_obrigacoes_sociais = _Obrigacoes_Sociais;
                            pj.T003_IND_FILIAL_SEDE_FORA = _FilialComSedeFora;
                            pj.T003_IND_UNIPESSOAL = _empresaUnipessoal;
                            pj.t003_dt_constituicao = _DataConstituicao;
                            pj.T003_in_reducao_capital = _t003_in_reducao_capital;
                            pj.T003_ds_reducao_capital = _t003_ds_reducao_capital;
                            pj.T003_ind_integralizando_cap = _integralizandoCapital;
                            pj.t003_ind_spe = _indicadorSPE;
                            pj.T003_ind_matriz = _indicadorMatriz;
                            pj.T003_in_end_estab = _prossuiEstabelecimento;
                            pj.Update();
                        }
                        #endregion

                        #region 3.Gravando Vinculo Endereço
                        //(dados do endereço da Empresa)

                        using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                        {
                            int SqVinculoEndereco;
                            ve.MainConnectionProvider = cp;
                            ve.t001_sq_pessoa = SqEmpresa;
                            if (IsNumtValidated(_SedeTipoLogradouro) && !string.IsNullOrEmpty(_SedeTipoLogradouro))
                            {
                                ve.a015_co_tipo_logradouro = Convert.ToDecimal(SedeTipoLogradouro);
                                ve.a015_ds_tipo_logradouro = SedeDsTipoLogradouro;
                            }
                            else
                            {
                                ve.a015_ds_tipo_logradouro = SedeDsTipoLogradouro;
                            }

                            ve.r002_ds_logradouro = _SedeLogradouro;
                            ve.r002_nr_logradouro = _SedeNumero;
                            ve.r002_ds_complemento = _SedeComplemento;
                            ve.r002_ds_bairro = _SedeBairro;
                            if (_SedeMunicipio != "")
                                ve.a005_co_municipio = decimal.Parse(_SedeMunicipio);
                            ve.a004_co_pais = 154;
                            ve.r002_nr_cep = _SedeCEP;
                            ve.R002_uf = _SedeUF;

                            SqVinculoEndereco = ve.Update();
                        }
                        #endregion

                        #region 4.Gravando Protocolo
                        //(dados do protocolo da Empresa)

                        using (dT005_Protocolo p = new dT005_Protocolo())
                        {
                            p.MainConnectionProvider = cp;
                            if (_ProtocoloRequerimento == String.Empty)
                            {
                                using (dCorrelativo c = new dCorrelativo())
                                {
                                    if (_tipoCorrelativo == "")
                                    {
                                        c.Tipo = Int32.Parse(CONST_PROTOCOLO_CORRELATIVO);
                                    }
                                    else
                                    {
                                        c.Tipo = Int32.Parse(_tipoCorrelativo);
                                    }

                                    _ProtocoloRequerimento = c.GetCorrelativo();
                                    p.t005_dt_entrada = DateTime.Now;
                                }
                            }
                            p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                            p.t005_nr_protocolo = _ProtocoloRequerimento;
                            p.t001_sq_pessoa = SqEmpresa;
                            p.t005_nr_protocolo_prefeitura = _ProtocoloPrefeitura;
                            p.t005_nr_protocolo_viabilidade = _ProtocoloViabilidade;
                            p.t005_nr_docad = _CodigoDOCAD;
                            p.t005_nr_dbe = _CodigoDBE;
                            p.T005_nr_protocolo_RCPJ = _ProtocoloRCPJ;
                            p.T005_nr_alteracao = _numeroAlteracao;
                            p.T005_protocolo_orgao_origem = _t005_protocolo_orgao_origem;
                            p.T005_co_ato = _CodigoAto;
                            p.T005_uf_origem = _t005_uf_origem;
                            p.T005_tipo_doc_reque = _tipoRequerimento;
                            p.T005_in_dbe_carregado = _indDbeCarregado;
                            p.T005_codMunicipioInscMunicipal = _codMunicipioInscMunicipal;
                            p.Update();
                        }
                        #endregion

                        #region 5.Gravando Protocolo Requerimento
                        //(dados complementares Requerimento)

                        using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                        {
                            pr.MainConnectionProvider = cp;
                            pr.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                            pr.t005_nr_protocolo = _ProtocoloRequerimento;
                            pr.t006_ds_artigo_estatuto = _ArtigoEstatuto;
                            pr.t006_edital_fixado_sede = _Edital_Fixado_Sede;
                            pr.t006_edital_publicado_jornal = _Edital_Publicado_Jornal;
                            pr.t006_edital_outros = _Edital_Outros;
                            pr.t006_nr_art_estatuto_convocacao = _Num_Artigo_Estatuto_Convocacao;
                            pr.t006_ds_quorum_deliberacao = _Quorum_Deliberacao;
                            pr.t006_ds_quorum_alteracao = _Quorum_Alteracao;
                            pr.t006_ds_quorum_dissolucao = _Quorum_Dissolucao;
                            pr.t006_ds_outro_quorum_deliberacao = _Outro_Quorum_Deliberacao;
                            pr.t006_ds_outro_quorum_alteracao = _Outro_Quorum_Alteracao;
                            pr.t006_ds_outro_quorum_dissolucao = _Outro_Quorum_Dissolucao;
                            pr.t006_ds_destino_patrimonio = _Destino_Patrimonio;
                            pr.t006_in_possui_fundo_social = _Possui_Fundo_Social;
                            pr.t006_in_obrigacoes_sociais = _Obrigacoes_Sociais;
                            pr.t006_recurso_mensalidade = _Fonte_Contribuicoes_Mensais;
                            pr.t006_recurso_doacao = _Fonte_Contribuicoes_Doacao;
                            pr.t006_recurso_governamental = _Fonte_Recursos_Governamentais;
                            pr.t006_nr_art_estatuto_associacao = _Num_Artigo_Estatuto_Associacao;
                            pr.t006_ds_nome_advogado = _Nome_Advogado;
                            pr.t006_nr_cpf_advogado = _CPF_Advogado;
                            pr.t006_nr_inscr_oab = _Inscricao_OAB_Advogado;
                            pr.t006_ds_uf_oab_advogado = _UF_OAB_Advogado;
                            pr.t006_ds_nome_contador = _Nome_Contador;
                            pr.t006_nr_cpf_contador = _CPF_Contador;
                            pr.t006_nr_crc_contador = _CRC_Contador;
                            pr.a004_co_uf = _UF_CRC;
                            pr.t006_in_ata_mesmo_instrumento = _Ata_Mesmo_Instrumento;
                            pr.t006_nr_art_deliberacao = _Num_Artigo_Estatuto_Deliberacao;
                            pr.t006_nr_art_alteracao = _Num_Artigo_Estatuto_Alteracao;
                            pr.t006_nr_art_dissolucao = _Num_Artigo_Estatuto_Dissolucao;
                            pr.t006_nr_art_obrigacoes_sociais = _Num_Artigo_Estatuto_Obrigacoes_Sociais;
                            pr.t006_nr_art_fundo_social = _Num_Artigo_Estatuto_Fundo_Social;
                            pr.T006_tipo_propriedade = _tipoPropriedade;

                            if (TipoPessoaJuridicaCodigo == 1264)
                                pr.UpdateSociedade();
                            else
                                pr.Update();
                        }
                        #endregion

                        #region Gravando Status do Protocolo
                        if (_status_protocolo == "0" || _status_protocolo == "")
                        {
                            _status_protocolo = "0";

                            using (dT011_Protocolo_Status ps = new dT011_Protocolo_Status())
                            {
                                ps.MainConnectionProvider = cp;
                                ps.T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                                ps.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                                ps.T011_IN_SITUACAO = "0";
                                ps.T011_DT_SITUACAO = DateTime.Now;
                                if (string.IsNullOrEmpty(UsuarioRegin))
                                {
                                    UsuarioRegin = "REQUERENTE";
                                }
                                if (!string.IsNullOrEmpty(UsuarioCPF))
                                {
                                    UsuarioRegin = _UsuarioCPF;
                                }

                                ps.T011_USUARIO = UsuarioRegin;

                                ps.Update();
                            }


                            using (dT005_Protocolo p = new dT005_Protocolo())
                            {
                                p.t005_nr_protocolo = _ProtocoloRequerimento;
                                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;


                                p.UpdateStatusLog(Int32.Parse(_status_protocolo), UsuarioRegin, _CodigoDBE, _ProtocoloViabilidade);

                            }
                        }
                        #endregion

                        #region 6. Gravando Requerente (Pessoa)
                        //(Dados do Requerente)
                        using (dT001_Pessoa p = new dT001_Pessoa())
                        {

                            p.MainConnectionProvider = cp;
                            p.t001_in_tipo_pessoa = CONST_PESSOA_FISICA;
                            p.t001_ds_pessoa = _RequerenteNome;
                            p.t001_in_dados_atualizados = "S";
                            p.t001_dt_ult_atualizacao = DateTime.Now;
                            p.t001_email = _RequerenteEmail;
                            p.t001_ddd = _RequerenteDDD;
                            p.t001_tel_1 = _RequerenteTelefone;
                            p.t001_tel_2 = "";
                            p.t001_sq_pessoa = SqRequerente;
                            SqRequerente = p.Update();
                            _RequerenteCodigo = SqRequerente;


                        }
                        #endregion

                        #region 7. Gravando Requerente (Pessoa Fisica)
                        //(Dados do Requerente)

                        using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                        {
                            pf.MainConnectionProvider = cp;

                            pf.t001_sq_pessoa = SqRequerente;
                            pf.t002_nr_cpf = _RequerenteCPF;
                            pf.Update();
                        }
                        #endregion

                        #region Gravando Vinculo com a PJ (Vinculo Requerente)
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.t001_sq_pessoa = SqRequerente;
                            v.t001_sq_pessoa_pai = SqEmpresa;
                            v.a009_co_condicao = 500; // 2066 = Requerente
                            v.r001_dt_entrada_vinculo = DateTime.Now;
                            v.r001_ds_cargo_direcao = "REQUERENTE";
                            v.r001_in_situacao = "A";
                            v.r001_vl_participacao = 0;
                            v.T001_cpf_cnpj_pessoa = _RequerenteCPF;
                            v.Update();
                        }
                        #endregion

                        #region Gravando Ato e Evento
                        //
                        using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
                        {
                            pe.MainConnectionProvider = cp;
                            if (_apagaEventos)
                            {
                                pe.Deleta(SqEmpresa);
                            }

                            foreach (bProtocoloEvento a in _bProtocoloEvento)
                            {
                                pe.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                pe.t007_nr_protocolo = _ProtocoloRequerimento;
                                pe.a003_co_evento = a.CodigoEvento;
                                //pe.a002_co_ato = a.CodigoAto;
                                pe.t001_sq_pessoa = SqEmpresa;
                                a.SqPessoa = SqEmpresa;
                                pe.Update();
                            }
                        }
                        #endregion

                        #region 8.Gravando CNAE
                        //(Codigo de Atividade Economica do Requerimento)

                        using (dR004_Atuacao a = new dR004_Atuacao())
                        {
                            a.MainConnectionProvider = cp;
                            if (_apagaCNANES)
                            {
                                a.Deleta(SqEmpresa);
                            }

                            foreach (bCNAE c in _CNAEs)
                            {
                                a.t001_sq_pessoa = SqEmpresa;
                                a.a001_co_atividade = c.CodigoCNAE;
                                a.r004_in_principal_secundario = c.TipoAtividade.ToString();
                                a.Update();
                            }
                        }
                        #endregion

                        #region 8.1 Gravando Filiais
                        if (_TipoGravacao == "Filiais")
                        {
                            if (_Filiais.Count > 0)
                            {
                                foreach (bFilial fi in _Filiais)
                                {
                                    #region Gravando Pessoa Filial
                                    using (dT001_Pessoa fp = new dT001_Pessoa())
                                    {
                                        fp.MainConnectionProvider = cp;
                                        fp.t001_in_tipo_pessoa = CONST_PESSOA_JURIDICA;
                                        fp.t001_ds_pessoa = SedeNome;
                                        fp.t001_in_dados_atualizados = "S";
                                        fp.t001_dt_ult_atualizacao = DateTime.Now;
                                        fp.t001_email = "";
                                        fp.t001_tel_1 = "";
                                        fp.t001_tel_2 = "";
                                        fp.t001_sq_pessoa = fi.SqFilial;
                                        SqFilial = fp.Update();
                                        if (fi.SqFilial == 0)
                                            fi.SqFilial = SqFilial;

                                    }
                                    #endregion

                                    #region Gravando pessoa juridica Filial
                                    using (dT003_Pessoa_Juridica fpj = new dT003_Pessoa_Juridica())
                                    {
                                        fpj.MainConnectionProvider = cp;
                                        fpj.t001_sq_pessoa = SqFilial;
                                        fpj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                        fpj.t003_nr_matricula = fi.Nire;
                                        fpj.t003_nr_cnpj = fi.Cnpj;
                                        fpj.t003_DBE = fi.FilialDBE;
                                        fpj.t003_prot_viab = fi.FilialViabilidade;
                                        fpj.t003_vl_capital_social = fi.FilialCapitalDestacado;
                                        fpj.T003_IND_CNAE_DESTACADA = fi.FilialCnaeDestacado;
                                        fpj.t003_ds_objeto_social = fi.FilialOBJSocial;
                                        fpj.t003_dt_inicio_atividade = fi.DataInicioAtividade;
                                        fpj.T003_uf_origem = fi.FilialUFOrigem;
                                        fpj.T003_iptu = fi.IPTU;
                                        fpj.T003_area_utilizada = fi.AreaUtilizada;
                                        fpj.Update();
                                    }
                                    #endregion

                                    #region Gravando Vinculo Endereço
                                    //(dados do endereço da Empresa)
                                    using (dR002_Vinculo_Endereco fve = new dR002_Vinculo_Endereco())
                                    {
                                        int SqVinculoEnderecoFilial;
                                        fve.MainConnectionProvider = cp;
                                        fve.t001_sq_pessoa = SqFilial;
                                        fve.t001_sq_pessoa_pai = SqEmpresa;
                                        //_t001_sq_pessoa_pai = ;
                                        if (fi.FilialTipoLogradouro != "")
                                        {
                                            fve.a015_co_tipo_logradouro = decimal.Parse(AchaTipoLogradouro(fi.FilialTipoLogradouro));
                                        }
                                        fve.a015_ds_tipo_logradouro = fi.FilialTipoLogradouro;
                                        fve.r002_ds_logradouro = fi.FilialLogradouro;
                                        fve.r002_nr_logradouro = fi.FilialNumero;
                                        fve.r002_ds_complemento = fi.FilialComplemento;
                                        fve.r002_ds_bairro = fi.FilialBairro;
                                        fve.a004_co_pais = 154;
                                        fve.a005_co_municipio = fi.FilialCodMunicipio;
                                        fve.r002_nr_cep = fi.FilialCEP;
                                        fve.R002_uf = fi.FilialUF;
                                        SqVinculoEnderecoFilial = fve.Update();
                                    }
                                    #endregion

                                    #region Gravando Vinculo com a PJ (Vinculo Filial)
                                    using (dR001_Vinculo fv = new dR001_Vinculo())
                                    {
                                        fv.MainConnectionProvider = cp;
                                        fv.t001_sq_pessoa = SqFilial;
                                        fv.t001_sq_pessoa_pai = SqEmpresa;
                                        fv.a009_co_condicao = 501;
                                        fv.r001_dt_entrada_vinculo = DateTime.Now;
                                        fv.r001_ds_cargo_direcao = "FILIAL";
                                        fv.r001_in_situacao = "A";
                                        fv.r001_acao = fi.Acao;
                                        fv.T001_cpf_cnpj_pessoa = fi.Cnpj == "" ? SqFilial.ToString() : fi.Cnpj;
                                        fv.Update();
                                    }
                                    #endregion

                                    #region Gravando CNAE Filial
                                    using (dR004_Atuacao fa = new dR004_Atuacao())
                                    {
                                        fa.MainConnectionProvider = cp;
                                        foreach (bCNAE fc in fi.CNAEs)
                                        {
                                            fa.t001_sq_pessoa = SqFilial;
                                            fa.a001_co_atividade = fc.CodigoCNAE;
                                            fa.r004_in_principal_secundario = fc.TipoAtividade.ToString();
                                            fa.r004_exercida = fc.Exercida;
                                            fa.Update();
                                        }
                                    }
                                    #endregion

                                    #region Gravando Ato e Evento Filial

                                    using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
                                    {
                                        pe.MainConnectionProvider = cp;
                                        foreach (bProtocoloEvento a in fi.ProtocoloEvento)
                                        {
                                            pe.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                            pe.t007_nr_protocolo = _ProtocoloRequerimento;
                                            pe.a003_co_evento = a.CodigoEvento;
                                            //pe.a002_co_ato = a.CodigoAto;
                                            pe.t001_sq_pessoa = SqFilial;
                                            a.SqPessoa = SqFilial;
                                            pe.Update();
                                        }
                                    }
                                    #endregion
                                }
                            }

                        }
                        #endregion

                        #region 9. Gravando Genprotocolo
                        //Informações complentares da alteração
                        _reqGenprotocolo.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                        _reqGenprotocolo.Update();

                        #endregion

                    }
                    #endregion
                    #region Aba_2
                    #region Grava Sócios
                    if (_TipoGravacao == "Socio" || _TipoGravacao == "Diretor" || _TipoGravacao == "Tudo" && (_Socios.Count > 0 || _FundadorDiretor.Count > 0))
                    {

                        //(Dados dos Socios do requerimento)


                        int SqSocio = 0;
                        foreach (bSocios s in _Socios)
                        {
                            SqSocio = 0;

                            #region 8. Gravando Pessoa
                            using (dT001_Pessoa p = new dT001_Pessoa())
                            {
                                p.MainConnectionProvider = cp;

                                p.t001_in_tipo_pessoa = "F";
                                s.TipoPessoa = "F"; ;
                                p.t001_ds_pessoa = s.Nome;
                                p.t001_in_dados_atualizados = "S";
                                p.t001_dt_ult_atualizacao = DateTime.Now;
                                if (s.DDD != string.Empty)
                                    p.t001_ddd = s.DDD;
                                if (s.Telefone != string.Empty)
                                    p.t001_tel_1 = s.Telefone;
                                p.t001_email = s.Email == null ? "" : s.Email;

                                if (s.SQPessoa != null)
                                    p.t001_sq_pessoa = Convert.ToDecimal(s.SQPessoa);
                                else
                                    p.t001_sq_pessoa = 0;

                                SqSocio = p.Update();

                                //if (SqSocio != 0)
                                //    s.SQPessoa = SqSocio.ToString();
                                //else
                                //    SqSocio = Convert.ToInt32(s.SQPessoa);
                            }
                            #endregion

                            #region 9.Gravando Pessoa Fisica / Juridica
                            if (s.TipoPessoa == "F")
                            {
                                //10.Gravando Socios - pessoa fisica
                                //(Dados dos Socios do requerimento)
                                using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                                {
                                    pf.MainConnectionProvider = cp;

                                    pf.t001_sq_pessoa = SqSocio;
                                    pf.t002_nr_cpf = s.CPFCNPJ;
                                    if (s.TipoIdentidade != null && s.TipoIdentidade != String.Empty)
                                        pf.a010_co_tipo_documento = decimal.Parse(s.TipoIdentidade);
                                    pf.t002_nr_documento = s.RG;
                                    pf.t002_ds_emissor_documento = s.OrgaoExpedidor;
                                    pf.a004_uf_org_exped = s.OrgaoExpedidorUF;
                                    pf.t002_ds_orgao_expedidor = s.OrgaoExpedidorNome;

                                    if (!string.IsNullOrEmpty(s.NacionalidadeCodigo.ToString()))
                                    {
                                        pf.a004_co_pais = s.NacionalidadeCodigo;
                                        if (pf.a004_co_pais != 154)
                                        {
                                            pf.t002_tipo_visto = s.tipo_visto;
                                            pf.t002_emissao_visto = s.emissao_visto;
                                            pf.t002_dt_validade_visto = s.validade_visto;
                                        }
                                        else
                                        {
                                            pf.t002_tipo_visto = "";
                                            pf.t002_emissao_visto = null;
                                            pf.t002_dt_validade_visto = null;
                                        }
                                    }

                                    pf.t002_ds_nacionalidade = s.Nacionalidade;
                                    pf.a004_co_uf_naturalidade = s.NaturalidadeCodigo;
                                    if (s.EstadoCivil != null && s.EstadoCivil != String.Empty)
                                        pf.a012_co_estado_civil = decimal.Parse(s.EstadoCivil);
                                    if (s.EstadoCivilRegime != null && s.EstadoCivilRegime != String.Empty)
                                    {
                                        pf.a013_co_regime_bens = decimal.Parse(s.EstadoCivilRegime);
                                    }

                                    if (s.Profissao_Descricao != null && s.Profissao_Descricao != string.Empty)
                                        pf.t002_ds_profissao = s.Profissao_Descricao;


                                    pf.t002_dt_nascimento = s.DataNascimento;
                                    pf.t002_in_sexo = s.in_Sexo;
                                    // ok

                                    if (s.TipoEmancipado != null && s.TipoEmancipado != String.Empty)
                                    {
                                        pf.a014_co_emancipacao = decimal.Parse(s.TipoEmancipado);
                                    }

                                    pf.t002_nr_qtd_cotas = s.QuotaCapitalSocial;
                                    //pf.t002_aportesocio = s.AporteSocio;
                                    pf.t002_capital_integralizado = s.CapitalIntegralizado;
                                    pf.t002_capital_a_integralizar = s.Capital_a_Integralizar;
                                    pf.t002_perc_capital = s.PercentualCapital;
                                    pf.t002_data_final_integralizacao = s.DataIntegralizacao;

                                    pf.t002_dt_nascimento = s.DataNascimento;
                                    pf.t002_nome_pai = s.Nome_Pai;
                                    pf.t002_nome_mae = s.Nome_Mae;

                                    pf.t002_analfabeto = s.Analfabeto;
                                    pf.t002_dt_obito = s.DataObito;

                                    pf.t002_nire = s.Nire;
                                    pf.t002_tip_orgao_registro = s.TipoOrgaoRegistro;

                                    pf.t002_cpf_outorgante = s.CpfOutorgante;
                                    pf.t002_ds_outorgante = s.NomeOutorgante;

                                    pf.AdminstracaoIsoladamente = s.AdminstracaoIsoladamente;
                                    pf.AdminstracaoConjuntamente = s.AdminstracaoConjuntamente;
                                    pf.AdminstracaoTodos = s.AdminstracaoTodos;

                                    pf.t002_in_siarco = s.ExisteNoSiarco;
                                    pf.t002_in_div_dbe = s.IndicadorDBE;
                                    pf.t002_tipo_acao = s.IndicadorEventoJunta;
                                    pf.t002_dt_saida_adm = s.DataSaidaAdm;
                                    pf.t002_in_resp_livro = s.RespGuardaLivro;
                                    pf.t002_in_resp_ativo_passivo = s.RespAtivoPassivo;
                                    pf.T002_co_escolaridade = s.EscolaridadeCodigo;
                                    pf.Update();
                                }
                            }
                            else
                            {
                                //10.Gravando Socios - pessoa jurídica
                                //(Dados dos Socios do requerimento)

                                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                                {
                                    pj.MainConnectionProvider = cp;
                                    pj.t001_sq_pessoa = SqSocio;
                                    pj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                                    pj.t003_nr_matricula = "";
                                    pj.t003_nr_cnpj = s.CPFCNPJ;
                                    pj.t003_nr_inscricao_estadual = "";
                                    pj.t003_nr_inscricao_municipal = "";
                                    pj.t006_nr_cnpj_org_reg_ant = "";
                                    pj.t003_nr_matricula_anterior = "";
                                    pj.a011_co_porte = 0;
                                    pj.a007_co_situacao = 0;
                                    pj.a008_co_status_atual = 0;
                                    pj.t003_vl_capital_social = 0;
                                    pj.t003_vl_qtd_cotas = 0;
                                    pj.t003_vl_valor_cota = 0;
                                    pj.Update();
                                }
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
                                if (s.EndTipoLogradouro != null && s.EndTipoLogradouro != String.Empty)
                                {
                                    es.a015_co_tipo_logradouro = decimal.Parse(s.EndTipoLogradouro);
                                    es.a015_ds_tipo_logradouro = s.EndDsTipoLogradouro;
                                }
                                es.r002_ds_logradouro = s.EndLogradouro;
                                es.r002_nr_logradouro = s.EndNumero;
                                es.r002_ds_complemento = s.EndComplemento;
                                es.r002_ds_bairro = s.EndBairro;
                                es.R002_uf = s.EndUF;
                                if (s.EndMunicipio != null && s.EndMunicipio != String.Empty)
                                    es.a005_co_municipio = decimal.Parse(s.EndMunicipio);//;
                                if (!string.IsNullOrEmpty(s.EndPais))
                                    es.a004_co_pais = Convert.ToInt32(s.EndPais);
                                es.r002_nr_cep = s.EndCEP;
                                SqVinculoEndereco = es.Update();
                            }
                            #endregion

                            #region 12 Gravando Vinculo Sócio com a PJ (Vinculo Sócio)
                            using (dR001_Vinculo v = new dR001_Vinculo())
                            {
                                v.MainConnectionProvider = cp;
                                v.t001_sq_pessoa = SqSocio;
                                v.t001_sq_pessoa_pai = SqEmpresa;
                                v.T001_cpf_cnpj_pessoa = s.CPFCNPJ;

                                if (s.Qualificacao == null)
                                {
                                    if (_NaturezaJuridicaCodigo == 2305) // EIRELI
                                    {
                                        s.Qualificacao = "65";
                                        s.Qualificacao_Descricao = "TITULAR";
                                        v.a009_co_condicao = decimal.Parse(s.Qualificacao);
                                    }
                                    if (_NaturezaJuridicaCodigo == 2135) // EMPRESARIO INDIVIDUAL
                                    {
                                        s.Qualificacao = "50";
                                        s.Qualificacao_Descricao = "EMPRESÁRIO";
                                        v.a009_co_condicao = decimal.Parse(s.Qualificacao);

                                    }
                                    if (_NaturezaJuridicaCodigo == 2313) // CASO EIRELI-RCPJ
                                    {
                                        s.Qualificacao = "65";
                                        s.Qualificacao_Descricao = "TITULAR";
                                        v.a009_co_condicao = decimal.Parse(s.Qualificacao);
                                    }
                                    if (_NaturezaJuridicaCodigo == 1264) // SOCIEDADE
                                    {
                                        v.a009_co_condicao = 22;
                                    }
                                }
                                else
                                    v.a009_co_condicao = decimal.Parse(s.Qualificacao);


                                if (v.a009_co_condicao == 22)
                                    v.r001_ds_cargo_direcao = "Sócio";
                                else
                                    v.r001_ds_cargo_direcao = s.Qualificacao_Descricao;

                                if (s.Qualificacao != null && s.Qualificacao != string.Empty)
                                {
                                    v.a009_co_condicao = decimal.Parse(s.Qualificacao);
                                    v.r001_dt_inicio_mandato = s.DataInicioMandato;
                                    v.r001_dt_termino_mandato = s.DataTerminoMandato;
                                    v.t001_sq_pessoa_rep_legal = s.rep_legal;
                                }
                                else
                                {

                                    v.r001_dt_inicio_mandato = null;
                                    v.r001_dt_termino_mandato = null; ;
                                    v.t001_sq_pessoa_rep_legal = 0;
                                }

                                //v.r001_in_situacao = "A";
                                if (string.IsNullOrEmpty(s.Situacao))
                                    v.r001_in_situacao = "A";
                                else
                                    v.r001_in_situacao = s.Situacao; // "A";

                                v.r001_in_fundador = s.Fundador;
                                v.r001_vl_participacao = 0;
                                v.r001_vl_participacao = s.QuotaCapitalSocial * _ValorCota;
                                v.t001_sq_pessoa_rep_legal = s.rep_legal;

                                v.r001_dt_entrada_vinculo = DateTime.Now;
                                v.r001_acao = s.tipoacao;
                                //if (s.DataSaidaSocio != null)
                                v.r001_dt_saida_vinculo = s.DataSaidaSocio;

                                v.Update();

                            }
                            #endregion

                            #region 13 Gravando a Transferencia de quotas

                            //bSociosQuotas.DeleteAll(_ProtocoloRequerimento, cp);

                            //if (_transferenciaQuotas.Count > 0)
                            //{
                            //    foreach (bSociosQuotas sq in _transferenciaQuotas)
                            //    {
                            //        sq.Update(cp);
                            //    }
                            //}

                            #endregion

                            #region 14 Gravando adminstradores conjunto
                            //exclui todos

                            bAdminstracao cAdm = new bAdminstracao();
                            cAdm.NrProtocolo = _ProtocoloRequerimento;
                            cAdm.SQPessoa = SqSocio;
                            cAdm.Delete();

                            if (s.ListAdmnistracaoConjunto.Count > 0)
                            {

                                foreach (bAdminstracao sq in s.ListAdmnistracaoConjunto)
                                {
                                    sq.MainConnectionProvider = cp;
                                    sq.NrProtocolo = _ProtocoloRequerimento;
                                    sq.Update();
                                }
                            }

                            #endregion

                            #region Grava Representante
                            if (s.Representantes.Count > 0)
                            {
                                foreach (bSocios Repres in s.Representantes)
                                {
                                    GravaRepresentante(Repres, SqSocio.ToString(), cp);
                                }


                            }

                            #endregion

                        }


                    }
                    #endregion

                    #endregion Aba02

                    #region Aba03
                    #region Grava Adicionais
                    if (_TipoGravacao == "Adicionais")
                    {
                        //xx. Gravando Informações Adicionais)
                        using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                        {
                            pr.MainConnectionProvider = cp;
                            pr.t005_nr_protocolo = _ProtocoloRequerimento;
                            pr.t006_edital_fixado_sede = _Edital_Fixado_Sede;
                            pr.t006_edital_publicado_jornal = _Edital_Publicado_Jornal;
                            pr.t006_edital_outros = _Edital_Outros;
                            pr.t006_nr_art_estatuto_convocacao = _Num_Artigo_Estatuto_Convocacao;
                            pr.t006_ds_quorum_deliberacao = _Quorum_Deliberacao;
                            pr.t006_ds_quorum_alteracao = _Quorum_Alteracao;
                            pr.t006_ds_quorum_dissolucao = _Quorum_Dissolucao;
                            pr.t006_ds_outro_quorum_deliberacao = _Outro_Quorum_Deliberacao;
                            pr.t006_ds_outro_quorum_alteracao = _Outro_Quorum_Alteracao;
                            pr.t006_ds_outro_quorum_dissolucao = _Outro_Quorum_Dissolucao;
                            pr.t006_ds_destino_patrimonio = _Destino_Patrimonio;
                            pr.t006_in_possui_fundo_social = _Possui_Fundo_Social;
                            pr.t006_in_obrigacoes_sociais = _Obrigacoes_Sociais;
                            pr.t006_recurso_mensalidade = _Fonte_Contribuicoes_Mensais;
                            pr.t006_recurso_doacao = _Fonte_Contribuicoes_Doacao;
                            pr.t006_recurso_governamental = _Fonte_Recursos_Governamentais;
                            pr.t006_nr_art_estatuto_associacao = _Num_Artigo_Estatuto_Associacao;
                            pr.t006_ds_nome_advogado = _Nome_Advogado;
                            pr.t006_nr_cpf_advogado = _CPF_Advogado;
                            pr.t006_nr_inscr_oab = _Inscricao_OAB_Advogado;
                            pr.t006_ds_uf_oab_advogado = _UF_OAB_Advogado;
                            pr.t006_ds_nome_contador = _Nome_Contador;
                            pr.t006_nr_cpf_contador = _CPF_Contador;
                            pr.t006_nr_crc_contador = _CRC_Contador;
                            pr.a004_co_uf = _UF_CRC;
                            pr.t006_in_ata_mesmo_instrumento = _Ata_Mesmo_Instrumento;
                            pr.t006_nr_art_deliberacao = _Num_Artigo_Estatuto_Deliberacao;
                            pr.t006_nr_art_alteracao = _Num_Artigo_Estatuto_Alteracao;
                            pr.t006_nr_art_dissolucao = _Num_Artigo_Estatuto_Dissolucao;
                            pr.t006_nr_art_obrigacoes_sociais = _Num_Artigo_Estatuto_Obrigacoes_Sociais;
                            pr.t006_nr_art_fundo_social = _Num_Artigo_Estatuto_Fundo_Social;
                            pr.t006_ds_artigo_estatuto = _ArtigoEstatuto;
                            pr.Update();


                        }
                        //cp.CommitTransaction();
                    }
                    #endregion Grava Adicionais
                    #endregion Aba03

                    _contaabilista.CNPJ_Orgao_Registro = _CNPJ_Orgao_Registro;
                    _contaabilista.nr_Protocolo = _ProtocoloRequerimento;
                    _contaabilista.Update();

                    VerificaSocioDuploOuquantidade(cp);

                    VerificaDBEDuplicado(cp);

                    cp.CommitTransaction();

                    _controleExisteRequerimento = _CodigoEmpresa;
                    //Atualiza a classe bFilial
                    AtualizaFiliais();
                    AtualizaEvento();
                    AtualizaSocios();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento " + _ProtocoloRequerimento + ", por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }


        }
        private void AtualizaSocios()
        {
            #region Socios
            DataTable dt = dHelperQuery.CarregaSocioProtocolo(_ProtocoloRequerimento);

            _Socios.Clear();

            foreach (DataRow r in dt.Rows)
            {
                bSocios s = new bSocios();
                s.SQPessoaPai = _CodigoEmpresa;
                s.Nome = r["nome_socio"].ToString();
                s.SQPessoa = r["SQ_SOCIO"].ToString();
                s.TipoPessoa = r["TIPO_PESSOA"].ToString();
                //s.Qualificacao = r[""].ToString();
                s.CPFCNPJ = r["CPF_CNPJ_SOCIO"].ToString();
                s.TipoIdentidade = r["CO_TIPO_DOC_IDENT"].ToString();
                s.RG = r["NO_DOC_IDENT"].ToString();
                s.OrgaoExpedidor = r["ORGAO_EXPED"].ToString();
                s.OrgaoExpedidorNome = r["T002_DS_ORGAO_EXPEDIDOR"].ToString();
                s.OrgaoExpedidorUF = r["UF_ORGAO_EXPED"].ToString();

                s.RespGuardaLivro = Int32.Parse(r["T002_IN_RESP_LIVRO"].ToString());
                s.RespAtivoPassivo = Int32.Parse(r["T002_IN_RESP_ATIVO_PASSIVO"].ToString());

                if (!string.IsNullOrEmpty(r["CO_NACIONALIDADE"].ToString()))
                    s.NacionalidadeCodigo = Convert.ToInt32(r["CO_NACIONALIDADE"]);
                else
                    s.NacionalidadeCodigo = 154;

                if (!string.IsNullOrEmpty(r["NACIONALIDADE"].ToString()))
                    s.Nacionalidade = r["NACIONALIDADE"].ToString();
                else
                    s.Nacionalidade = "BRASILEIRA";

                s.NaturalidadeCodigo = r["NATURALIDADE"].ToString();
                s.DDD = r["T001_DDD"].ToString();
                s.Telefone = r["T001_TEL_1"].ToString();
                s.Email = r["T001_EMAIL"].ToString();

                if (!string.IsNullOrEmpty(r["TIPO_VISTO"].ToString()))
                    s.tipo_visto = r["TIPO_VISTO"].ToString();
                if (!string.IsNullOrEmpty(r["EMISSAO_VISTO"].ToString()))
                    s.emissao_visto = Convert.ToDateTime(r["EMISSAO_VISTO"].ToString());
                if (!string.IsNullOrEmpty(r["VALIDADE_VISTO"].ToString()))
                    s.validade_visto = Convert.ToDateTime(r["VALIDADE_VISTO"].ToString());

                if (!string.IsNullOrEmpty(r["JUSTIFICATIVA_VISTO"].ToString()))
                    s.Justificativa_Visto = r["JUSTIFICATIVA_VISTO"].ToString();


                if (r["SEXO"].ToString() == "M")
                    s.in_Sexo = "M";
                if (r["SEXO"].ToString() == "F")
                    s.in_Sexo = "F";
                if (r["ANALFABETO"].ToString() != string.Empty)
                    s.Analfabeto = r["ANALFABETO"].ToString();

                s.EstadoCivil = r["ESTADO_CIVIL"].ToString();
                s.EstadoCivilDescricao = r["DS_ESTADO_CIVIL"].ToString();

                s.EstadoCivilRegime = r["REGIME_BENS"].ToString();
                s.RegimeBens = r["DS_REGIME_BENS"].ToString();

                if (!String.IsNullOrEmpty(r["DATA_NASC"].ToString()))
                    s.DataNascimento = Convert.ToDateTime(r["DATA_NASC"].ToString());

                s.TipoEmancipado = r["CO_EMANCIPACAO"].ToString();
                s.rep_legal = Convert.ToInt32(r["ADM"]);

                //s.TipoAssistido = r[""].ToString();
                s.Profissao = r["PROFISSAO"].ToString();
                s.Profissao_Descricao = r["DS_PROFISSAO"].ToString();


                s.QuotaCapitalSocial = Convert.ToDecimal(r["QTD_COTAS"]);

                s.CapitalIntegralizado = Convert.ToDecimal(r["capital_integralizado"]);
                s.Capital_a_Integralizar = Convert.ToDecimal(r["capital_a_integralizar"]);

                if (!string.IsNullOrEmpty(r["data_final_integralizacao"].ToString()))
                    s.DataIntegralizacao = Convert.ToDateTime(r["data_final_integralizacao"]);

                s.Qualificacao = Convert.ToDecimal(r["Qualificacao"]).ToString();
                s.Qualificacao_Descricao = r["DS_QUALIFICACAO"].ToString();

                if (Convert.ToInt32(r["QUALIFICACAO"]) == 16 || Convert.ToInt32(r["QUALIFICACAO"]) == 5)
                    s.rep_legal = 1;
                else
                    s.rep_legal = 0;

                s.rep_legal = Convert.ToInt32(r["ADM"]);

                if (!string.IsNullOrEmpty(r["DT_INICIO_MANDATO"].ToString()))
                    s.DataInicioMandato = Convert.ToDateTime(r["DT_INICIO_MANDATO"]);
                if (!string.IsNullOrEmpty(r["DT_TERMINO_MANDATO"].ToString()))
                    s.DataTerminoMandato = Convert.ToDateTime(r["DT_TERMINO_MANDATO"]);

                s.Fundador = r["FUNDADOR"].ToString();
                s.Nome_Mae = r["Nome_mae"].ToString();
                s.Nome_Pai = r["Nome_pai"].ToString();
                s.CapitalIntegralizado = decimal.Parse(r["CAPITAL_INTEGRALIZADO"].ToString());
                s.Capital_a_Integralizar = decimal.Parse(r["CAPITAL_A_INTEGRALIZAR"].ToString());
                s.tipoacao = Int32.Parse(r["R001_ACAO"].ToString());
                s.Situacao = r["R001_IN_SITUACAO"].ToString();

                if (!string.IsNullOrEmpty(r["T002_DT_OBITO"].ToString()))
                    s.DataObito = Convert.ToDateTime(r["T002_DT_OBITO"]);

                if (!string.IsNullOrEmpty(r["R001_DT_SAIDA_VINCULO"].ToString()))
                    s.DataSaidaSocio = Convert.ToDateTime(r["R001_DT_SAIDA_VINCULO"]);

                s.Nire = r["t002_nire"].ToString();
                s.TipoOrgaoRegistro = Int32.Parse(r["T002_TIP_ORGAO_REGISTRO"].ToString());

                s.CpfOutorgante = r["T002_CPF_OUTORGANTE"].ToString();
                s.NomeOutorgante = r["T002_DS_OUTORGANTE"].ToString();

                s.AdminstracaoIsoladamente = Int32.Parse(r["T002_IN_ADM_ISOLADAMENTE"].ToString());
                s.AdminstracaoConjuntamente = Int32.Parse(r["T002_IN_ADM_CONJUNTAMENTE"].ToString());
                s.AdminstracaoTodos = Int32.Parse(r["T002_IN_ADM_TODOS"].ToString());

                //s.ExisteNoSiarco = Int32.Parse(r["T002_IN_SIARCO"].ToString());
                s.IndicadorDBE = Int32.Parse(r["T002_IN_DIV_DBE"].ToString());
                s.IndicadorEventoJunta = Int32.Parse(r["T002_TIPO_ACAO"].ToString());
                s.PercentualCapital = Decimal.Parse(r["T002_PERC_CAPITAL"].ToString());
                if (!string.IsNullOrEmpty(r["T002_DT_SAIDA_ADM"].ToString()))
                    s.DataSaidaAdm = Convert.ToDateTime(r["T002_DT_SAIDA_ADM"]);
                s.EscolaridadeCodigo = Int32.Parse(r["T002_CO_ESCOLARIDADE"].ToString());


                dt095_uso_firma class95 = new dt095_uso_firma();
                class95.t005_protocolo = _ProtocoloRequerimento;
                class95.t095_sq_pessoa_adm = Int32.Parse(s.SQPessoa);
                DataTable dt95 = class95.Query();
                foreach (DataRow row in dt95.Rows)
                {
                    bAdminstracao badm = new bAdminstracao();
                    badm.NrProtocolo = _ProtocoloRequerimento;
                    badm.SQPessoa = Int32.Parse(row["t095_sq_pessoa_adm"].ToString());
                    badm.NomeAdm = row["NomeAdm"].ToString();
                    badm.CpfAdm = row["CpfAdm"].ToString();
                    badm.SQPessoaConjunto = Int32.Parse(row["t095_sq_pessoa_conjunto"].ToString());
                    badm.NomeAdmConjunto = row["NomeAdmConj"].ToString();
                    badm.CpfAdmConjunto = row["CpfAdmConj"].ToString();
                    s.ListAdmnistracaoConjunto.Add(badm);
                }


                #region Endereço Sócio
                s.EndPais = r["PAIS_RESIDENCIA"].ToString();
                s.EndCEP = r["CEP"].ToString();
                s.EndUF = r["UF"].ToString();
                s.EndMunicipio = r["MUNICIPIO"].ToString();
                s.EndMunicipioDescricao = r["DS_MUNICIPIO"].ToString();

                s.EndBairro = r["BAIRRO"].ToString();
                s.EndTipoLogradouro = r["TIPO_LOGRADOURO"].ToString();
                if (s.EndTipoLogradouro == "")
                {
                    s.EndTipoLogradouro = "0";
                }
                if (!string.IsNullOrEmpty(r["DS_TIPO_LOGRADOURO"].ToString()))
                    s.EndDsTipoLogradouro = r["DS_TIPO_LOGRADOURO"].ToString();

                s.EndLogradouro = r["LOGRADOURO"].ToString();
                s.EndNumero = r["NO_LOGRADOURO"].ToString();
                s.EndComplemento = r["COMPL_LOGRADOURO"].ToString();
                #endregion



                #region Representante do Sócio
                s.Representantes.Clear();
                DataTable dtRepr = dHelperQuery.CarregaRepresentantes(Convert.ToInt32(s.SQPessoa));
                foreach (DataRow rr in dtRepr.Rows)
                {
                    bSocios re = new bSocios();

                    re.Nome = rr["nome_responsavel"].ToString();
                    re.SQPessoa = rr["SQ_REPRESENTANTE"].ToString();
                    re.TipoPessoa = rr["TIPO_PESSOA"].ToString();

                    if (!string.IsNullOrEmpty(rr["R001_DT_INICIO_MANDATO"].ToString()))
                        re.DataInicioMandato = Convert.ToDateTime(rr["R001_DT_INICIO_MANDATO"].ToString());

                    //s.Qualificacao = r[""].ToString();
                    re.CPFCNPJ = rr["CPF_RESPONSAVEL"].ToString();
                    re.rep_legal = Convert.ToInt32(rr["REP_LEGAL"]);
                    re.TipoIdentidade = rr["CO_TIPO_DOC_IDENT"].ToString();
                    re.RG = rr["NO_DOC_IDENT"].ToString();
                    re.OrgaoExpedidor = rr["ORGAO_EXPED"].ToString();
                    re.OrgaoExpedidorUF = rr["UF_ORGAO_EXPED"].ToString();
                    re.OrgaoExpedidorNome = rr["T002_DS_ORGAO_EXPEDIDOR"].ToString();
                    re.NacionalidadeCodigo = Convert.ToInt32(rr["CO_NACIONALIDADE"]);
                    re.Nacionalidade = rr["NACIONALIDADE"].ToString();
                    re.Analfabeto = rr["ANALFABETO"].ToString();
                    re.Situacao = rr["SITUACAO"].ToString();
                    if (!string.IsNullOrEmpty(rr["TIPO_VISTO"].ToString()))
                        re.tipo_visto = rr["TIPO_VISTO"].ToString();
                    if (!string.IsNullOrEmpty(rr["EMISSAO_VISTO"].ToString()))
                        re.emissao_visto = Convert.ToDateTime(rr["EMISSAO_VISTO"].ToString());
                    if (!string.IsNullOrEmpty(rr["VALIDADE_VISTO"].ToString()))
                        re.validade_visto = Convert.ToDateTime(rr["VALIDADE_VISTO"].ToString());

                    re.NaturalidadeCodigo = rr["NATURALIDADE"].ToString();
                    if (rr["SEXO"].ToString() == "M")
                        re.in_Sexo = "M";
                    if (rr["SEXO"].ToString() == "F")
                        re.in_Sexo = "F";
                    //if (rr["ADM"].ToString() != "0")
                    //    re.rep_legal = Convert.ToInt32(rr["REP_LEGAL"].ToString());
                    re.EstadoCivil = rr["ESTADO_CIVIL"].ToString();
                    re.EstadoCivilRegime = rr["REGIME_BENS"].ToString();

                    if (!string.IsNullOrEmpty(rr["DATA_NASC"].ToString()))
                        re.DataNascimento = Convert.ToDateTime(rr["DATA_NASC"].ToString());

                    re.TipoEmancipado = rr["CO_EMANCIPACAO"].ToString();
                    re.TipoAssistido = rr["TIPO_REPRESENTANTE"].ToString();
                    re.CTipoRepresentante = new bTipoRepresentante(Int32.Parse(rr["TIPO_REPRESENTANTE"].ToString()));

                    re.Profissao = rr["PROFISSAO"].ToString();
                    re.Profissao_Descricao = rr["DS_PROFISSAO"].ToString();
                    re.DDD = rr["DDD"].ToString();
                    re.Telefone = rr["TELEFONE"].ToString();
                    re.Email = rr["EMAIL"].ToString();
                    re.tipoacao = Int32.Parse(r["R001_ACAO"].ToString());

                    #region Endereço Representante
                    re.EndCEP = rr["CEP"].ToString();
                    re.EndPais = rr["PAIS"].ToString();
                    re.EndUF = rr["UF"].ToString();
                    re.EndMunicipio = rr["MUNICIPIO"].ToString();
                    re.EndBairro = rr["BAIRRO"].ToString();
                    re.EndTipoLogradouro = rr["TIPO_LOGRADOURO"].ToString();
                    if (!string.IsNullOrEmpty(rr["DS_TIPO_LOGRADOURO"].ToString()))
                        re.EndDsTipoLogradouro = rr["DS_TIPO_LOGRADOURO"].ToString();
                    re.EndLogradouro = rr["LOGRADOURO"].ToString();
                    re.EndNumero = rr["NO_LOGRADOURO"].ToString();
                    re.EndComplemento = rr["COMP_LOGRADOURO"].ToString();
                    //re.TipoRepresentante    = rr["ds_tipo_representante"].ToString();
                    #endregion

                    s.Representantes.Add(re);
                }
                #endregion

                _Socios.Add(s);

            }

            #endregion
        }
        public void UpdateSocio(int pSqPessoa, decimal pQuotas, decimal pAdmin)
        {

            try
            {
                //1. Gravando Pessoa (Requerimento)
                //(incluir uma pessoa juridica da empresa do requerimento)
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                    {
                        pf.MainConnectionProvider = cp;
                        pf.t001_sq_pessoa = pSqPessoa;
                        pf.t002_nr_qtd_cotas = pQuotas;
                        pf.UpdateDadosSocios();
                    }
                    using (dR001_Vinculo v = new dR001_Vinculo())
                    {
                        v.MainConnectionProvider = cp;
                        v.t001_sq_pessoa = pSqPessoa;
                        v.t001_sq_pessoa_rep_legal = pAdmin;
                        v.UpdateAdministrador();
                    }
                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }

        }
        public void UpdateSocio(int pSqPessoa, decimal pQuotas, decimal pAdmin, int pAcao)
        {

            try
            {
                //1. Gravando Pessoa (Requerimento)
                //(incluir uma pessoa juridica da empresa do requerimento)
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                    {
                        pf.MainConnectionProvider = cp;
                        pf.t001_sq_pessoa = pSqPessoa;
                        pf.t002_nr_qtd_cotas = pQuotas;
                        pf.UpdateDadosSocios();
                    }
                    using (dR001_Vinculo v = new dR001_Vinculo())
                    {
                        v.MainConnectionProvider = cp;
                        v.t001_sq_pessoa = pSqPessoa;
                        v.t001_sq_pessoa_rep_legal = pAdmin;
                        v.r001_acao = pAcao;
                        v.UpdateAdministrador();
                    }
                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }

        }
        private void AtualizaEvento()
        {
            _bProtocoloEvento.Clear();

            DataTable dtEvento = EncontraAtoEvento();
            if (dtEvento.Rows.Count > 0)
            {
                foreach (DataRow r in dtEvento.Rows)
                {
                    bProtocoloEvento ev = new bProtocoloEvento();
                    ev.CodigoEvento = decimal.Parse(r["a003_co_evento"].ToString());
                    ev.ProtocoloRequerimento = _ProtocoloRequerimento;
                    ev.CnpjOrgaoRegistro = _CNPJ_Orgao_Registro;
                    ev.CodigoAto = decimal.Parse(r["a002_co_ato"].ToString());
                    ev.DescricaoEvento = r["a003_co_evento"].ToString() + " - " + r["A002_DS_ATO"].ToString();
                    ev.DescricaoResumida = r["A002_DS_ATO"].ToString();
                    ev.SqPessoa = Int32.Parse(r["t001_sq_pessoa"].ToString());
                    _bProtocoloEvento.Add(ev);
                }
            }
        }

        private void VerificaSocioDuploOuquantidade(ConnectionProvider cv)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.MainConnectionProvider = cv;
                int qtd = 0;


                qtd = v.VerificaSeDuplicoQSA(_CodigoEmpresa);
                if (qtd > 0)
                {
                    //Socio esta duplicado por favor tentar novamente
                    throw new Exception("Problema na conexão. Tente mais tarde (3.1.3) " + _CodigoEmpresa.ToString());
                }
            }
        }

        private void VerificaDBEDuplicado(ConnectionProvider cv)
        {
            if (_CodigoDBE == "")
                return;

            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                v.MainConnectionProvider = cv;
                int qtd = 0;


                qtd = v.VerificaDBEDuplicado(_ProtocoloRequerimento, _CodigoDBE);

                if (qtd > 0)
                {
                    //Socio esta duplicado por favor tentar novamente
                    throw new Exception("DBE " + _CodigoDBE + " utilizado por outro requerimento ");
                }
            }
        }



        public string IsQuantidadeSociosOK()
        {
            string msg = "";
            int qtd = 0;
            qtd = SociosAtivosSemSaida.Count;

            if (_TipoPessoaJuridicaCodigo != 1265)
            {
                if (qtd == 0)
                {
                    msg = "Deve existir pelo menos 1 no QSA!";
                    return msg;
                }
            }
            else
            {
                qtd = _Socios.Count;
                if (_Socios.Count == 0)
                {
                    msg = "Deve existir pelo menos uma pessoa no QSA!";
                    return msg;
                }
            }
            switch (_TipoPessoaJuridicaCodigo)
            {
                case 1264: //- Sociedades
                    if (qtd < 2 && (_empresaUnipessoal == 2 && _indTransfUnipessoal == 2))
                        msg = "Deve existir pelo menos 2 sócios no QSA!";
                    break;
                case 1265: //- orgao publico
                    if (qtd == 0)
                        msg = "Deve existir pelo menos 1 pessoa no QSA!";
                    break;
                case 6568: //- Empresario
                    if (qtd > 1)
                        msg = "Para este tipo de empresa não é permitido mais de 1 sócio";
                    break;
                case 6569: //- EIRELI
                    if (SociosAtivosSemSaida.Count > 1)
                        msg = "Para este tipo de empresa não é permitido mais de 1 sócio";
                    break;


            }
            return msg;
        }


        public string IsQuantidadeSociosOKParaExcluir()
        {
            string msg = "";
            int qtd = SociosAtivos.Count - 1;
            
            if (qtd == 0)
            {
                msg = "Deve existir pelo menos uma pessoa informada no QSA";
                return msg;
            }

            switch (_TipoPessoaJuridicaCodigo)
            {
                case 1264: //- Sociedades
                    if (IsProtocoloIncorporacao() && qtd < 2)
                        msg = "Deve existir pelo menos 2 sócios no QSA!";
                    break;
                case 6568: //- Empresario
                    if (qtd > 1)
                        msg = "Para este tipo de empresa não é permitido mais de 1 sócio";
                    break;
                case 6569: //- EIRELI
                    if (qtd > 1)
                        msg = "Para este tipo de empresa não é permitido mais de 1 sócio";
                    break;


            }
            return msg;
        }


        public DataTable QueryAdministradores()
        {
            return dHelperQuery.QueryAdministradores(_CodigoEmpresa);
        }
        public Boolean CarregaInformacoesAdicionais() // Aba 3
        {
            Boolean wChave = false;
            DataTable dt = new DataTable();
            using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
            {
                pr.t005_nr_protocolo = _ProtocoloRequerimento;
                dt = pr.Query();
                if (dt.Rows.Count > 0)
                {
                    _Edital_Fixado_Sede = dt.Rows[0]["t006_edital_fixado_sede"].ToString();
                    _Edital_Publicado_Jornal = dt.Rows[0]["t006_edital_publicado_jornal"].ToString();
                    _Edital_Outros = dt.Rows[0]["t006_edital_outros"].ToString();
                    _Num_Artigo_Estatuto_Convocacao = dt.Rows[0]["t006_nr_art_estatuto_convocacao"].ToString();
                    _Quorum_Deliberacao = dt.Rows[0]["t006_ds_quorum_deliberacao"].ToString();
                    _Quorum_Alteracao = dt.Rows[0]["t006_ds_quorum_alteracao"].ToString();
                    _Quorum_Dissolucao = dt.Rows[0]["t006_ds_quorum_dissolucao"].ToString();
                    _Outro_Quorum_Deliberacao = dt.Rows[0]["t006_ds_outro_quorum_deliberacao"].ToString();
                    _Outro_Quorum_Alteracao = dt.Rows[0]["t006_ds_outro_quorum_alteracao"].ToString();
                    _Outro_Quorum_Dissolucao = dt.Rows[0]["t006_ds_outro_quorum_dissolucao"].ToString();
                    _Destino_Patrimonio = dt.Rows[0]["t006_ds_destino_patrimonio"].ToString();
                    _Possui_Fundo_Social = dt.Rows[0]["t006_in_possui_fundo_social"].ToString();
                    _Obrigacoes_Sociais = dt.Rows[0]["t006_in_obrigacoes_sociais"].ToString();
                    _Fonte_Contribuicoes_Mensais = dt.Rows[0]["t006_recurso_mensalidade"].ToString();
                    _Fonte_Contribuicoes_Doacao = dt.Rows[0]["t006_recurso_doacao"].ToString();
                    _Fonte_Recursos_Governamentais = dt.Rows[0]["t006_recurso_governamental"].ToString();
                    _Num_Artigo_Estatuto_Associacao = dt.Rows[0]["t006_nr_art_estatuto_associacao"].ToString();

                    _Nome_Advogado = dt.Rows[0]["t006_ds_nome_advogado"].ToString();
                    _CPF_Advogado = dt.Rows[0]["t006_nr_cpf_advogado"].ToString();
                    _Inscricao_OAB_Advogado = dt.Rows[0]["t006_nr_inscr_oab"].ToString();
                    _UF_OAB_Advogado = dt.Rows[0]["t006_ds_uf_oab_advogado"].ToString();

                    _Nome_Contador = dt.Rows[0]["t006_ds_nome_contador"].ToString();
                    _CPF_Contador = dt.Rows[0]["t006_nr_cpf_contador"].ToString();
                    _CRC_Contador = dt.Rows[0]["t006_nr_crc_contador"].ToString();
                    _UF_CRC = dt.Rows[0]["a004_co_uf"].ToString();

                    _Ata_Mesmo_Instrumento = dt.Rows[0]["t006_in_ata_mesmo_instrumento"].ToString();
                    _Num_Artigo_Estatuto_Deliberacao = dt.Rows[0]["T006_NR_ART_DELIBERACAO"].ToString();
                    _Num_Artigo_Estatuto_Alteracao = dt.Rows[0]["T006_NR_ART_ALTERACAO"].ToString();
                    _Num_Artigo_Estatuto_Dissolucao = dt.Rows[0]["T006_NR_ART_DISSOLUCAO"].ToString();
                    _Num_Artigo_Estatuto_Obrigacoes_Sociais = dt.Rows[0]["T006_NR_ART_OBRIGACOES_SOCIAIS"].ToString();
                    _Num_Artigo_Estatuto_Fundo_Social = dt.Rows[0]["T006_NR_ART_FUNDO_SOCIAL"].ToString();
                    _ArtigoEstatuto = dt.Rows[0]["T006_DS_ARTIGO_ESTATUTO"].ToString();
                    _Num_Vias = Int32.Parse(dt.Rows[0]["T006_NR_NUM_VIAS"].ToString());

                    #region Contador

                    _advogado.Nome = dt.Rows[0]["t006_ds_nome_advogado"].ToString();
                    _advogado.CPF = dt.Rows[0]["T006_NR_CPF_ADVOGADO"].ToString();
                    _advogado.Inscricao_OAB = dt.Rows[0]["t006_nr_inscr_oab"].ToString();
                    _advogado.UF_OAB = dt.Rows[0]["t006_ds_uf_oab_advogado"].ToString();

                    #endregion

                    #region Advogado
                    _advogado.Nome= dt.Rows[0]["t006_ds_nome_advogado"].ToString();
                    _advogado.CPF = dt.Rows[0]["t006_nr_cpf_advogado"].ToString();
                    _advogado.Inscricao_OAB = dt.Rows[0]["t006_nr_inscr_oab"].ToString();
                    _advogado.UF_OAB = dt.Rows[0]["t006_ds_uf_oab_advogado"].ToString();
                    #endregion


                    wChave = true;

                }

                return wChave;
            }
        }

        public Boolean CarregaProtocoloByProtocoloJunta(string pProtocolo)
        {
            _ProtocoloRequerimento = dHelperQuery.GetProtocoloByprotocoloJunta(pProtocolo);
            if (_ProtocoloRequerimento != "")
                return CarregaAba01Protocolo();
            else
                return false;
        }

        public Boolean CarregaAba01Protocolo()
        {

            DataTable dt = new DataTable();
            DataTable dtRepr = new DataTable();
            _Socios.Clear();

            if (!dHelperQuery.VerificaExisteRequerimento(_ProtocoloRequerimento))
            {
                return false;
            }



            dt = dHelperQuery.CarregaRequerenteComProtocolo(_ProtocoloRequerimento);
            if (dt.Rows.Count != 0)
            {
                #region CARREGA DADOS DO REQUERENTE

                _Requerente.Nome = dt.Rows[0]["T001_DS_PESSOA"].ToString();
                _Requerente.Cpf = dt.Rows[0]["T002_NR_CPF"].ToString();
                _Requerente.DDD = dt.Rows[0]["T001_DDD"].ToString();
                _Requerente.Telefone = dt.Rows[0]["T001_TEL_1"].ToString();
                _Requerente.Email = dt.Rows[0]["T001_EMAIL"].ToString();
                _Requerente.SqPessoa = int.Parse(dt.Rows[0]["T001_SQ_PESSOA"].ToString());
                _Requerente.SqEmpresa = int.Parse(dt.Rows[0]["T001_SQ_PESSOA_PAI"].ToString());

                _RequerenteNome = dt.Rows[0]["T001_DS_PESSOA"].ToString();
                _RequerenteCPF = dt.Rows[0]["T002_NR_CPF"].ToString();
                _RequerenteDDD = dt.Rows[0]["T001_DDD"].ToString();
                _RequerenteTelefone = dt.Rows[0]["T001_TEL_1"].ToString();
                _RequerenteEmail = dt.Rows[0]["T001_EMAIL"].ToString();
                _RequerenteCodigo = int.Parse(dt.Rows[0]["T001_SQ_PESSOA"].ToString());

                int wehValido = int.Parse(dt.Rows[0]["T005_IN_SITUACAO"].ToString());

                if (wehValido == 5 || wehValido == 8 || wehValido == 9)
                {
                    _ehValido = false;
                }
                else
                {
                    _ehValido = true;
                }

                #endregion
            }



            #region CARREGA DADOS DA EMPRESA
            DateTime wAuxx = DateTime.MinValue; ;
            dt = dHelperQuery.CarregaEmpresaComProtocolo(_ProtocoloRequerimento);

            #region Requerimento
            if (dt.Rows[0]["T005_TIPO_DOC_REQUE"] != null && dt.Rows[0]["T005_TIPO_DOC_REQUE"].ToString() != "")
            {
                _tipoRequerimento = Int32.Parse(dt.Rows[0]["T005_TIPO_DOC_REQUE"].ToString());
            }
            #endregion

            #region Classe Empresa
            _empresa.SqEmpresa = Convert.ToInt32(dt.Rows[0]["T001_SQ_PESSOA"].ToString());

            _empresa.CnpjOrgaoRegistro = dt.Rows[0]["T004_NR_CNPJ_ORG_REG"].ToString();
            _empresa.DBE = dt.Rows[0]["T005_NR_DBE"].ToString();
            _empresa.ProtocoloViabilidade = dt.Rows[0]["T005_NR_PROTOCOLO_VIABILIDADE"].ToString();

            _empresa.Nome = dt.Rows[0]["T001_DS_PESSOA"].ToString();
            _empresa.NomeFantasia = dt.Rows[0]["T001_NOME_FANTASIA"].ToString();
            _empresa.CNPJ = dt.Rows[0]["T003_NR_CNPJ"].ToString();
            _empresa.Matricula = dt.Rows[0]["T003_NR_MATRICULA"].ToString();
            _empresa.ObjetoSocial = dt.Rows[0]["T003_DS_OBJETO_SOCIAL"].ToString();

            _empresa.Ddd = dt.Rows[0]["T001_DDD"].ToString();
            _empresa.Telefone = dt.Rows[0]["T001_TEL_1"].ToString();
            _empresa.Email = dt.Rows[0]["T001_EMAIL"].ToString();

            _empresa.NaturezaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["A006_CO_NATUREZA_JURIDICA"].ToString());
            _empresa.NaturezaJuridicaDescricao = dt.Rows[0]["a009_DS_NATUREZA_JURIDICA"].ToString();
            _empresa.TipoPessoaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["T003_CO_TIPO_PES_JUR"].ToString());
            _empresa.TipoPessoaJuridicaDescricao = dt.Rows[0]["A018_DS_TIPO_PESSOA_JURIDICA"].ToString();

            _empresa.CapitalSocial = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_SOCIAL"].ToString());
            _empresa.QtdCotas = Convert.ToDecimal(dt.Rows[0]["T003_VL_QTD_COTAS"].ToString());
            _empresa.ValorCota = Convert.ToDecimal(dt.Rows[0]["T003_VL_VALOR_COTA"].ToString());
            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_VL_CAPITAL_INTEGRALIZADO"].ToString()))
                _empresa.CapitalIntegralizado = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_INTEGRALIZADO"].ToString());

            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_VL_CAPITAL_NAO_INTEGRALIZADO"].ToString()))
                _empresa.CapitalNaoItegralizado = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_NAO_INTEGRALIZADO"].ToString());

            _empresa.MoedaCorrente = dt.Rows[0]["T003_MOEDA_CORRENTE"].ToString() == "" ? "0" : dt.Rows[0]["T003_MOEDA_CORRENTE"].ToString();

            _empresa.DataLimiteIntegralizacao = null;
            if (!String.IsNullOrEmpty(dt.Rows[0]["T003_DATA_LIMITE_INTEGRALIZACAO"].ToString()))
                _empresa.DataLimiteIntegralizacao = Convert.ToDateTime(dt.Rows[0]["T003_DATA_LIMITE_INTEGRALIZACAO"].ToString());

            _empresa.DataConstituicao = null;
            if (!String.IsNullOrEmpty(dt.Rows[0]["T003_DT_CONSTITUICAO"].ToString()))
                _empresa.DataConstituicao = Convert.ToDateTime(dt.Rows[0]["T003_DT_CONSTITUICAO"].ToString());

            _empresa.DataTerminoAtividade = null;
            if (!String.IsNullOrEmpty(dt.Rows[0]["T003_DT_TERMINO_ATIV"].ToString()))
                _empresa.DataTerminoAtividade = Convert.ToDateTime(dt.Rows[0]["T003_DT_TERMINO_ATIV"].ToString());

            _empresa.DuracaoSociedade = null;
            _empresa.AssociacaoTempoDuracao = null;
            if (dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString() != string.Empty && dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString() != null)
            {
                _empresa.DuracaoSociedade = Convert.ToDateTime(dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString());
                _empresa.AssociacaoTempoDuracao = Convert.ToDateTime(dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString());
            }

            _empresa.Enquadramento = Convert.ToInt32(dt.Rows[0]["T003_TIPO_ENQUADRAMENTO"].ToString());
            _empresa.ObrigacoesSociais = dt.Rows[0]["T003_SOCIOS_OBRIGACOES_SOCIAIS"].ToString();
            _empresa.SituacaoDescricao = dt.Rows[0]["T003_DS_SITUACAO"].ToString();
  
            _empresa.IndicadorSPE = Int32.Parse(dt.Rows[0]["T003_IND_SPE"].ToString());
            _empresa.Iptu = dt.Rows[0]["T003_IPTU"].ToString();
            _empresa.AreaUtilizada = Decimal.Parse(dt.Rows[0]["T003_AREA_UTILIZADA"].ToString());

            #endregion

            CnpjOrgaoRegistro = dt.Rows[0]["T004_NR_CNPJ_ORG_REG"].ToString();
            Protocolo = dt.Rows[0]["T005_NR_PROTOCOLO_RCPJ"].ToString();
            Requerimento = _ProtocoloRequerimento;
            SqPessoa = Convert.ToInt32(dt.Rows[0]["T001_SQ_PESSOA"].ToString());
            _CodigoEmpresa = Convert.ToInt32(dt.Rows[0]["T001_SQ_PESSOA"].ToString());

            _controleExisteRequerimento = Convert.ToInt32(dt.Rows[0]["T001_SQ_PESSOA"].ToString());
            
            NomeEmpresa = dt.Rows[0]["T001_DS_PESSOA"].ToString();
            _SedeSituacaoDescricao = dt.Rows[0]["T003_DS_SITUACAO"].ToString();
            _CodigoDBE = dt.Rows[0]["T005_NR_DBE"].ToString();
            _dbeAtual = dt.Rows[0]["T005_NR_DBE"].ToString();
            _ProtocoloViabilidade = dt.Rows[0]["T005_NR_PROTOCOLO_VIABILIDADE"].ToString();
            _SedeNome = dt.Rows[0]["T001_DS_PESSOA"].ToString();
            _Nome_Fantasia = dt.Rows[0]["T001_NOME_FANTASIA"].ToString();
            _nrEmpresaCNPJ = dt.Rows[0]["T003_NR_CNPJ"].ToString();
            _nrMatricula = dt.Rows[0]["T003_NR_MATRICULA"].ToString();


            _NaturezaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["A006_CO_NATUREZA_JURIDICA"].ToString());
            _NaturezaJuridicaDescricao = dt.Rows[0]["a009_DS_NATUREZA_JURIDICA"].ToString();
            _TipoPessoaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["T003_CO_TIPO_PES_JUR"].ToString());
            _TipoPessoaJuridicaDescricao = dt.Rows[0]["A018_DS_TIPO_PESSOA_JURIDICA"].ToString();
            _NaturezaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["A006_CO_NATUREZA_JURIDICA"].ToString());
            _NaturezaJuridicaDescricao = dt.Rows[0]["a009_DS_NATUREZA_JURIDICA"].ToString();
            _TipoPessoaJuridicaCodigo = Convert.ToInt32(dt.Rows[0]["T003_CO_TIPO_PES_JUR"].ToString());
            _TipoPessoaJuridicaDescricao = dt.Rows[0]["A018_DS_TIPO_PESSOA_JURIDICA"].ToString();
            _Enquadramento = Convert.ToInt32(dt.Rows[0]["T003_TIPO_ENQUADRAMENTO"].ToString());
            _Obrigacoes_Sociais = dt.Rows[0]["T003_SOCIOS_OBRIGACOES_SOCIAIS"].ToString();
            _CapitalSocial = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_SOCIAL"].ToString());
            _QtdCotas = Convert.ToDecimal(dt.Rows[0]["T003_VL_QTD_COTAS"].ToString());
            _ValorCota = Convert.ToDecimal(dt.Rows[0]["T003_VL_VALOR_COTA"].ToString());
            _SedeDDD = dt.Rows[0]["T001_DDD"].ToString();
            _SedeTelefone = dt.Rows[0]["T001_TEL_1"].ToString();
            _SedeEmail = dt.Rows[0]["T001_EMAIL"].ToString();
            _SedeSituacao = Int32.Parse(dt.Rows[0]["A007_CO_SITUACAO"].ToString());
            _codMunicipioInscMunicipal = Int32.Parse(dt.Rows[0]["T005_CODMUNICIPIOINSCMUNICIPAL"].ToString());

            if (dt.Rows[0]["T003_DT_CONSTITUICAO"].ToString() == string.Empty || dt.Rows[0]["T003_DT_CONSTITUICAO"].ToString() == null)
            {
                _DataConstituicao = null;
            }
            else
            {
                _DataConstituicao = Convert.ToDateTime(dt.Rows[0]["T003_DT_CONSTITUICAO"].ToString());
            }
            if (dt.Rows[0]["T003_DT_TERMINO_ATIV"].ToString() == string.Empty || dt.Rows[0]["T003_DT_TERMINO_ATIV"].ToString() == null)
            {
                _dataTerminoAtividade = null;
            }
            else
            {
                _dataTerminoAtividade = Convert.ToDateTime(dt.Rows[0]["T003_DT_TERMINO_ATIV"].ToString());
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_VL_CAPITAL_INTEGRALIZADO"].ToString()))
                _capital_integralizado = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_INTEGRALIZADO"].ToString());

            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_VL_CAPITAL_NAO_INTEGRALIZADO"].ToString()))
                _capital_nao_integralizado = Convert.ToDecimal(dt.Rows[0]["T003_VL_CAPITAL_NAO_INTEGRALIZADO"].ToString());

            if (dt.Rows[0]["T003_DATA_LIMITE_INTEGRALIZACAO"].ToString() == string.Empty || dt.Rows[0]["T003_DATA_LIMITE_INTEGRALIZACAO"].ToString() == null)
            {
                _data_limite_integralizacao = null;
            }
            else
            {
                _data_limite_integralizacao = Convert.ToDateTime(dt.Rows[0]["T003_DATA_LIMITE_INTEGRALIZACAO"].ToString());
            }

            if (dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString() != string.Empty && dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString() != null)
            {
                _DuracaoSociedade = Convert.ToDateTime(dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString());
                _AssociacaoTempoDuracao = Convert.ToDateTime(dt.Rows[0]["T003_DT_PRAZO_DETERMINADO"].ToString());
            }
            else
            {
                _DuracaoSociedade = null;
                _AssociacaoTempoDuracao = null;
            }
            _indicadorSPE = Int32.Parse(dt.Rows[0]["T003_IND_SPE"].ToString());

            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_DS_CAPITAL_NAO_INTEGRALIZADO"].ToString()))
                _ds_capital_nao_integralizado = dt.Rows[0]["T003_DS_CAPITAL_NAO_INTEGRALIZADO"].ToString();

            _moeda_corrente = dt.Rows[0]["T003_MOEDA_CORRENTE"].ToString() == "" ? "0" : dt.Rows[0]["T003_MOEDA_CORRENTE"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["T003_DS_REDUCAO_CAPITAL"].ToString()))
                _t003_ds_reducao_capital = dt.Rows[0]["T003_DS_REDUCAO_CAPITAL"].ToString();

            _t003_in_reducao_capital = Int32.Parse(dt.Rows[0]["T003_IN_REDUCAO_CAPITAL"].ToString());

            if (dt.Rows[0]["T003_DT_INICIO_ATIVIDADE"].ToString() == string.Empty || dt.Rows[0]["T003_DT_INICIO_ATIVIDADE"].ToString() == null)
                _DataInicioSociedade = null;
            else
                _DataInicioSociedade = Convert.ToDateTime(dt.Rows[0]["T003_DT_INICIO_ATIVIDADE"].ToString());

            _ObjetoSocial = dt.Rows[0]["T003_DS_OBJETO_SOCIAL"].ToString();
            _FilialComSedeFora = Int32.Parse(dt.Rows[0]["T003_IND_FILIAL_SEDE_FORA"].ToString());
            _empresaUnipessoal = Int32.Parse(dt.Rows[0]["T003_IND_UNIPESSOAL"].ToString());
            _integralizandoCapital = Int32.Parse(dt.Rows[0]["T003_IND_INTEGRALIZANDO_CAP"].ToString());
            _indicadorMatriz = Int32.Parse(dt.Rows[0]["t003_ind_matriz"].ToString());
            _prossuiEstabelecimento = Int32.Parse(dt.Rows[0]["T003_IN_END_ESTAB"].ToString());
            _eventoConsolidacao = Int32.Parse(dt.Rows[0]["T003_IN_CONSOLIDACAO"].ToString());
            _eventoReativacao = Int32.Parse(dt.Rows[0]["T003_IN_REATIVACAO"].ToString());
            _eventoReRatificacao = Int32.Parse(dt.Rows[0]["T003_IN_RERATIFICACAO"].ToString());

            //incluido por causa do autonomo que pode ter varios estabelecimentos
            _t003_iptu = dt.Rows[0]["T003_IPTU"].ToString();
            _t003_area_utilizada = Decimal.Parse(dt.Rows[0]["T003_AREA_UTILIZADA"].ToString());

            _CNPJ_Orgao_Registro = dt.Rows[0]["T004_NR_CNPJ_ORG_REG"].ToString();
            CNPJ_Orgao_Registro = dt.Rows[0]["T004_NR_CNPJ_ORG_REG"].ToString();
            _ProtocoloRCPJ = dt.Rows[0]["T005_NR_PROTOCOLO_RCPJ"].ToString();
            _ProtocoloEnquadramento = dt.Rows[0]["T005_NR_PROTOCOLO_ENQUADRAMENTO"].ToString();
            _numeroAlteracao = Int32.Parse(dt.Rows[0]["T005_NR_ALTERACAO"].ToString());
            _CodigoAto = dt.Rows[0]["T005_CO_ATO"].ToString();
            _t005_uf_origem = dt.Rows[0]["T005_UF_ORIGEM"].ToString();

            _indTransfUnipessoal = Int32.Parse(dt.Rows[0]["T005_IN_TRANSF_UNIPESSOAL"].ToString());
            _indDbeCarregado = Int32.Parse(dt.Rows[0]["T005_IN_DBE_CARREGADO"].ToString());
            _t005_in_clausila_adm = Int32.Parse(dt.Rows[0]["t005_in_clausila_adm"].ToString());
            _t005_tx_clausula_adm = dt.Rows[0]["t005_tx_clausula_adm"].ToString();
            _numeroDARE = dt.Rows[0]["T005_NR_DAE"].ToString();
            _t005_protocolo_orgao_origem = dt.Rows[0]["t005_protocolo_orgao_origem"].ToString();
           
            _tipoRegistroViab = dt.Rows[0]["T005_TIP_REGISTRO_VIAB"].ToString();

            if (_CNPJ_Orgao_Registro != "27079821000111")
            {
                if (_NaturezaJuridicaCodigo == 2135) // && _TipoPessoaJuridicaCodigo == 1264)
                {
                    _TipoPessoaJuridicaCodigo = 6568;
                    _TipoPessoaJuridicaDescricao = "Empresário individual";
                }
                if (_NaturezaJuridicaCodigo == 2305) // && _TipoPessoaJuridicaCodigo == 1264) // CASO EIRELI
                {
                    _TipoPessoaJuridicaCodigo = 6569;
                    _TipoPessoaJuridicaDescricao = "Titular";
                }
            }
            else
            {
                if (_NaturezaJuridicaCodigo == 2135) // && _TipoPessoaJuridicaCodigo == 1264)
                {
                    _TipoPessoaJuridicaCodigo = 6568;
                    _TipoPessoaJuridicaDescricao = "Empresário individual";
                }
                if (_NaturezaJuridicaCodigo == 2305) // && _TipoPessoaJuridicaCodigo == 1264) // CASO EIRELI
                {
                    _TipoPessoaJuridicaCodigo = 6569;
                    _TipoPessoaJuridicaDescricao = "Titular";
                }
            }

            

           
            _Foro = dt.Rows[0]["T005_FORO"].ToString();
            _clausualArbitral = dt.Rows[0]["T005_TX_CLAUSULA_ARBITRAL"].ToString();
            
            _textoRestituicaoBaixa = dt.Rows[0]["T005_TX_CLAUSULA_RESTITUICAO"].ToString();



            _indDbeCarregado = Int32.Parse(dt.Rows[0]["T005_IN_DBE_CARREGADO"].ToString());
            
            #endregion

            #region contrato

            if (string.IsNullOrEmpty(dt.Rows[0]["T005_DT_AVERBACAO"].ToString()))
            {
                _Data_Averbacao = null;
            }
            else
            {
                _Data_Averbacao = Convert.ToDateTime(dt.Rows[0]["T005_DT_AVERBACAO"].ToString());
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["t005_data_assinatura"].ToString()))
            {
                _Data_Assinatura = null;
            }
            else
            {
                _Data_Assinatura = Convert.ToDateTime(dt.Rows[0]["t005_data_assinatura"].ToString());
            }

            if (_Data_Assinatura == null && _Data_Averbacao != null)
            {
                _Data_Assinatura = _Data_Averbacao;
            }

            if (string.IsNullOrEmpty(dt.Rows[0]["t005_local_assinatura"].ToString()))
            {
                _Local_Assinatura = "";
            }
            else
            {
                _Local_Assinatura = dt.Rows[0]["t005_Local_assinatura"].ToString();
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["t005_co_unidade_entrega"].ToString()))
            {
                _codUnidadeEntrega = "";
            }
            else
            {
                _codUnidadeEntrega = dt.Rows[0]["t005_co_unidade_entrega"].ToString();
            }
            #endregion

            #region Endereço Empresa
            _SedeCEP = dt.Rows[0]["R002_NR_CEP"].ToString();
            _SedeUF = dt.Rows[0]["r002_UF"].ToString();
            _SedeMunicipio = dt.Rows[0]["A005_CO_MUNICIPIO"].ToString();
            _SedeBairro = dt.Rows[0]["R002_DS_BAIRRO"].ToString();
            _SedeTipoLogradouro = dt.Rows[0]["A015_CO_TIPO_LOGRADOURO"].ToString();
            _SedeDsTipoLogradouro = dt.Rows[0]["A015_DS_TIPO_LOGRADOURO"].ToString();
            _SedeLogradouro = dt.Rows[0]["R002_DS_LOGRADOURO"].ToString();
            _SedeNumero = dt.Rows[0]["R002_NR_LOGRADOURO"].ToString();
            _SedeComplemento = dt.Rows[0]["R002_DS_COMPLEMENTO"].ToString();
            _nomeMunicipioSede = dt.Rows[0]["NOMEMUNICIPIO"].ToString();
            #endregion

            #region CARREGA PROTOCOLO REQUERIMENTO
            using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
            {
                pr.t005_nr_protocolo = _ProtocoloRequerimento;
                _ArtigoEstatuto = pr.QueryArtigoEstatuto();

                DataTable dtprotReq = pr.Query();
                _tipoPropriedade = Int32.Parse(dtprotReq.Rows[0]["t006_tipo_propriedade"].ToString());
            }

            #endregion

            #region Dados da tabela Protocolo_Requerimento

            Boolean wAux = CarregaInformacoesAdicionais();

            #endregion

            #region Socios
            AtualizaSocios();


            #endregion

            #region Transferencia de Quotas

            bSociosQuotas qt = new bSociosQuotas();
            qt.NrProtocolo = _ProtocoloRequerimento;
            DataTable dtQt = qt.Query();

            _transferenciaQuotas.Clear();
            foreach (DataRow row in dtQt.Rows)
            {
                qt = new bSociosQuotas();

                qt.NrProtocolo = row["T005_NR_PROTOCOLO"].ToString();
                qt.Motivo = row["T098_DS_MOTIVO"].ToString();
                qt.QtdQuota = Decimal.Parse(row["T098_NR_QTD_COTAS"].ToString());
                qt.SQPessoa = Int32.Parse(row["T098_SQ_PESSOA_CEDENTE"].ToString());
                qt.SQPessoaRecebe = Int32.Parse(row["T098_SQ_PESSOA_CESSIONARIO"].ToString());
                qt.NomeCedente = row["nomeCedente"].ToString();
                qt.NomeRecebedor = row["NomeCessionario"].ToString();
                qt.CpfCedente = row["cpfCedente"].ToString();
                qt.CpfRecebedor = row["cpfCessionario"].ToString();
                qt.IdSeq = Int32.Parse(row["T098_SQ_TRANSF"].ToString());
                _transferenciaQuotas.Add(qt);
            }
            #endregion

            #region CNAE'S DA EMPRESA
            _CNAEs.Clear();
            if (_NaturezaJuridicaCodigo == 9999)
            {
                //Busca pelo CBO
                dt = dHelperQuery.CarregaCBOComProtocolo(_CodigoEmpresa);
                foreach (DataRow r in dt.Rows)
                {
                    bCNAE c = new bCNAE();
                    c.CodigoCNAE = r["A001_CO_ATIVIDADE"].ToString();
                    c.Descricao = r["TAD_DESC_ATIVIDADE"].ToString();
                    c.TipoAtividade = decimal.Parse(r["R004_IN_PRINCIPAL_SECUNDARIO"].ToString());
                    _CNAEs.Add(c);

                    if (c.TipoAtividade == 37)
                        _cnaeSec.Add(c);

                }
            }
            else
            {
                dt = dHelperQuery.CarregaCnaeComProtocolo(_CodigoEmpresa);
                foreach (DataRow r in dt.Rows)
                {
                    bCNAE c = new bCNAE();
                    c.CodigoCNAE = r["A001_CO_ATIVIDADE"].ToString();
                    c.Descricao = r["TAD_DESC_ATIVIDADE"].ToString();
                    c.TipoAtividade = decimal.Parse(r["R004_IN_PRINCIPAL_SECUNDARIO"].ToString());
                    _CNAEs.Add(c);

                    if (c.TipoAtividade == 37)
                        _cnaeSec.Add(c);

                }
            }
            #endregion



            #region CARREGA FILIAIS DA EMPRESA
            AtualizaFiliais();
            #endregion

            #region STATUS DO PROTOCOLO
            DataTable dtPs = dHelperQuery.CarregaStatusDoProtocolo(_ProtocoloRequerimento);
            if (dtPs.Rows.Count > 0)
                _status_protocolo = dtPs.Rows[0]["t011_in_situacao"].ToString();
            else
            {
                // Gravando Status do Protocolo
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    using (dT011_Protocolo_Status ps = new dT011_Protocolo_Status())
                    {
                        _status_protocolo = "0";
                        ps.MainConnectionProvider = cp;
                        ps.T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                        ps.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                        ps.T011_IN_SITUACAO = _status_protocolo;
                        ps.T011_DT_SITUACAO = DateTime.Now;

                        if (string.IsNullOrEmpty(UsuarioRegin))
                        {
                            UsuarioRegin = "REQUERENTE";
                        }
                        if (!string.IsNullOrEmpty(UsuarioCPF))
                        {
                            UsuarioRegin = _UsuarioCPF;
                        }

                        ps.T011_USUARIO = UsuarioRegin;
                        if (_status_protocolo == "0" || _status_protocolo == null)
                        {
                            ps.T011_IN_SITUACAO = "1";
                            ps.Update();
                            cp.CommitTransaction();
                        }
                    }
                }
            }
            #endregion

            #region EXIGENCIAS
            dt = dHelperQuery.CarregaExigencias(_ProtocoloRequerimento);
            _Exigencias.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    bExigencias e = new bExigencias();
                    e.CodExigencia = r["codexigencia"].ToString();
                    e.Descricao = r["descricao"].ToString();
                    e.ProtocoloRequerimento = r["protocolorequerimento"].ToString();
                    _Exigencias.Add(e);
                }
            }
            #endregion

            #region EXIGENCIAS (OUTRAS)
            dt = null;
            dt = dHelperQuery.CarregaExigenciasOutras(_ProtocoloRequerimento);

            _ExigenciasOutras.Clear();
            _ExigenciasConfirmacao.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    bExigencias e = new bExigencias();
                    e.CodExigencia = r["codexigencia"].ToString();
                    e.Descricao = r["descricao"].ToString();
                    e.FundamentoLegal = r["fundamentolegal"].ToString();
                    e.ProtocoloRequerimento = r["protocolorequerimento"].ToString();
                    e.Grupo = r["grupo"].ToString();
                    if (!string.IsNullOrEmpty(r["T016_DT_INCLUSAO"].ToString()))
                    {
                        e.dtInclusao = DateTime.Parse(r["T016_DT_INCLUSAO"].ToString());
                    }
                    if (string.IsNullOrEmpty(r["grupo"].ToString()))
                    {
                        _ExigenciasOutras.Add(e);
                    }
                    else
                    {
                        _ExigenciasConfirmacao.Add(e);
                    }

                }
            }
            #endregion

            #region Lixo
            //dt = dHelperQuery.CarregaExigencias(_ProtocoloRequerimento);
            //_Exigencias.Clear();
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        bExigencias e = new bExigencias();
            //        e.CodExigencia = r["codexigencia"].ToString();
            //        e.Descricao = r["descricao"].ToString();
            //        e.ProtocoloRequerimento = r["protocolorequerimento"].ToString();
            //        _Exigencias.Add(e);
            //    }
            //}
            // FIM DA CARGA DE EXIGENCIAS
            #endregion

            #region Carrega Confirmações

            _Confirmacoes.Clear();
            DataTable dtConfirmacoes = new DataTable();
            using (dT017_Protocolo_Confirmacao pc = new dT017_Protocolo_Confirmacao())
            {
                pc.t005_nr_protocolo = _ProtocoloRequerimento;
                dtConfirmacoes = pc.Query();
                foreach (DataRow r in dtConfirmacoes.Rows)
                {
                    bProtocoloConfirmacao n = new bProtocoloConfirmacao();
                    n.T005_nr_protocolo = _ProtocoloRequerimento;
                    n.T017_sequencia = decimal.Parse(r["T017_sequencia"].ToString());
                    n.T017_grupo = decimal.Parse(r["T017_grupo"].ToString());
                    n.T017_item = decimal.Parse(r["T017_item"].ToString());
                    n.T017_status = decimal.Parse(r["T017_status"].ToString());
                    n.T017_confirma = decimal.Parse(r["T017_confirma"].ToString());
                    n.T017_dt_confirmacao = DateTime.Parse(r["T017_dt_confirmacao"].ToString());
                    n.T017_usuario = r["T017_usuario"].ToString();
                    n.t017_motivo = r["t017_motivo"].ToString();
                    n.T017_andamento_secao = r["T017_ANDAMENTO_SECAO"].ToString();
                    n.T017_andamento_seq = 0;
                    if (!string.IsNullOrEmpty(r["T017_ANDAMENTO_SEQ"].ToString()))
                        n.T017_andamento_seq = Int32.Parse(r["T017_ANDAMENTO_SEQ"].ToString());
                    _Confirmacoes.Add(n);

                }

            }


            #endregion

            #region Carrega Confirmações por Tipo de Andamewnto

            #endregion
            #region Carrega Clausulas Adicionais
            _ClausulasAdicionais.Clear();
            dcnt_clausulas_adicionais clausulas = new dcnt_clausulas_adicionais();
            clausulas.cod_protocolo = _ProtocoloViabilidade;
            clausulas.reque_protocolo = _ProtocoloRequerimento;
            DataTable dtClausulas = clausulas.Query();
            if (dtClausulas.Rows.Count > 0)
            {
                foreach (DataRow r in dtClausulas.Rows)
                {
                    dcnt_clausulas_adicionais ca = new dcnt_clausulas_adicionais();
                    ca.cod_protocolo = r["COD_PROTOCOLO"].ToString();
                    ca.num_clausula = r["NUM_CLAUSULA"].ToString();
                    ca.reque_protocolo = r["REQUE_PROTOCOLO"].ToString();
                    ca.titulo_clausula = r["TITULO_CLAUSULA"].ToString();
                    ca.clausula = r["CLAUSULA"].ToString();
                    _ClausulasAdicionais.Add(ca);
                }
            }
            #endregion
            #region Clausula Arbitral
            _clausualArbitral = "";
            dcnt_clausulas_adicionais clausulas1 = new dcnt_clausulas_adicionais();
            clausulas1.cod_protocolo = _ProtocoloViabilidade;
            clausulas1.reque_protocolo = _ProtocoloRequerimento;
            clausulas1.num_clausula = "99";
            DataTable dtArbitral = clausulas1.Query();
            if (dtArbitral.Rows.Count > 0)
            {
                _clausualArbitral = dtArbitral.Rows[0]["CLAUSULA"].ToString();
            }
            #endregion

            #region Carrega Evento
            _bProtocoloEvento.Clear();

            DataTable dtEvento = EncontraAtoEvento();
            if (dtEvento.Rows.Count > 0)
            {
                foreach (DataRow r in dtEvento.Rows)
                {
                    bProtocoloEvento ev = new bProtocoloEvento();
                    ev.CodigoEvento = decimal.Parse(r["a003_co_evento"].ToString());
                    ev.ProtocoloRequerimento = _ProtocoloRequerimento;
                    ev.CnpjOrgaoRegistro = _CNPJ_Orgao_Registro;
                    ev.CodigoAto = decimal.Parse(r["a002_co_ato"].ToString());
                    ev.DescricaoEvento = r["a003_co_evento"].ToString() + " - " + r["A002_DS_ATO"].ToString();
                    ev.DescricaoResumida = r["A002_DS_ATO"].ToString();
                    ev.SqPessoa = Int32.Parse(r["t001_sq_pessoa"].ToString() == null ? "0" : r["t001_sq_pessoa"].ToString());
                    _bProtocoloEvento.Add(ev);
                }
            }


            #endregion

            #region RucGenprotocolo
            _reqGenprotocolo = new bReqGenProtocolo(_ProtocoloRequerimento);
            #endregion


            #region Carrega Alertas

            _Alertas.Clear();
            _Alertas = bAlertaRequerimento.GetAlertas(_ProtocoloRequerimento, _CNPJ_Orgao_Registro);

            #endregion

            #region Carrega Info Adic Associação

            #endregion

            #region Contabilista

            Contabilista.CNPJ_Orgao_Registro = _CNPJ_Orgao_Registro;
            Contabilista.nr_Protocolo = _ProtocoloRequerimento;
            DataTable dtContab = Contabilista.Query();
            if (dtContab.Rows.Count > 0)
            {
                //Contabilista.CNPJ_Orgao_Registro = dtContab.Rows[0][""].ToString();
                Contabilista.cpfCnpj = dtContab.Rows[0]["T093_CPFCNPJ"].ToString();
                Contabilista.ds_Pessoa = dtContab.Rows[0]["T093_DS_PESSOA"].ToString();
                Contabilista.tip_Class_Empresa = Int32.Parse(dtContab.Rows[0]["T093_TIP_CLASS_EMPRESA"].ToString());
                Contabilista.uf_CRC_Empresa = dtContab.Rows[0]["T093_UF_CRC_EMPRESA"].ToString();
                Contabilista.co_CRC_Empresa = dtContab.Rows[0]["T093_CO_CRC_EMPRESA"].ToString();
                Contabilista.tip_CRC_Empresa = dtContab.Rows[0]["T093_TIP_CRC_EMPRESA"].ToString();
                Contabilista.cpf_resp = dtContab.Rows[0]["T093_CPF_RESP"].ToString();
                Contabilista.tip_Class_Resp = Int32.Parse(dtContab.Rows[0]["T093_TIP_CLASS_RESP"].ToString());
                Contabilista.uf_CRC_Resp = dtContab.Rows[0]["T093_UF_CRC_RESP"].ToString();
                Contabilista.co_CRC_Resp = dtContab.Rows[0]["T093_CO_CRC_RESP"].ToString();
                Contabilista.tip_CRC_Resp = dtContab.Rows[0]["T093_TIP_CRC_RESP"].ToString();
                if (!string.IsNullOrEmpty(dtContab.Rows[0]["T093_DT_INSCR_CRC"].ToString()))
                {
                    Contabilista.DataInscricao = DateTime.Parse(dtContab.Rows[0]["T093_DT_INSCR_CRC"].ToString());
                }
            }

            #endregion

            #region Carrega Divergencia DBE
            _listDivergenciaDBE.Clear();

            _divDBE.NumeroProtocolo = _ProtocoloRequerimento;
            DataTable dtDiv = _divDBE.Query();
            if (dtDiv.Rows.Count > 0)
            {
                foreach (DataRow r in dtDiv.Rows)
                {
                    bDivergenciaDBE ev = new bDivergenciaDBE();
                    ev.NumeroOrgaoRegistro = r["t094_NR_CNPJ_ORG_REG"].ToString();
                    ev.NumeroProtocolo = r["t094_T005_NR_PROTOCOLO"].ToString();
                    ev.Item = Int32.Parse(r["t094_cod_divergencia"].ToString());
                    ev.Texto = r["t094_ds_divergencia"].ToString();
                    _listDivergenciaDBE.Add(ev);
                }
            }


            #endregion

            #region Testemunha
            _testemunha.ProtocoloRequerimento = _ProtocoloRequerimento;
            _testemunha.getTestemunhas();

            #endregion
            return true;
        }

        public void CarregaSocios()
        {
            AtualizaSocios();
        }

        #region Carrega Confirmações por Tipo de Andamewnto
        public DataTable QueryPorTipoAndamento(string wProtocolo, string wAndamento)
        {
            DataTable dtConfirmacoes = new DataTable();
            using (dT017_Protocolo_Confirmacao pc = new dT017_Protocolo_Confirmacao())
            {
                pc.t005_nr_protocolo = wProtocolo;
                pc.T017_andamento_secao = wAndamento;
                dtConfirmacoes = pc.Query();
            }
            return dtConfirmacoes;
        }
        #endregion

        protected bool IsProtocoloIncorporacao()
        {
            foreach (bProtocoloEvento ev in _bProtocoloEvento)
            {
                if (ev.CodigoEvento == 101)
                {
                    return true;
                }
            }
            return false;
        }


        public void RefreshProtocoloEvento()
        {
            _bProtocoloEvento.Clear();

            DataTable dtEvento = EncontraAtoEvento();
            if (dtEvento.Rows.Count > 0)
            {
                foreach (DataRow r in dtEvento.Rows)
                {
                    bProtocoloEvento ev = new bProtocoloEvento();
                    ev.CodigoEvento = decimal.Parse(r["a003_co_evento"].ToString());
                    ev.ProtocoloRequerimento = _ProtocoloRequerimento;
                    ev.CnpjOrgaoRegistro = _CNPJ_Orgao_Registro;
                    ev.CodigoAto = decimal.Parse(r["a002_co_ato"].ToString());
                    ev.DescricaoEvento = r["a003_co_evento"].ToString() + " - " + r["A002_DS_ATO"].ToString();
                    ev.DescricaoResumida = r["A002_DS_ATO"].ToString();
                    _bProtocoloEvento.Add(ev);
                }
            }
        }
        private string AchaTipoLogradouro(string wAux)
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                using (dTab_Cep_Tipo p = new dTab_Cep_Tipo())
                {
                    p.MainConnectionProvider = cp;
                    return p.DevolveCodigoTipoLogradouro(wAux).ToString();
                }
            }
        }

        public DataTable QuerySociedades(String Matricula,
                                         String NomeSociedade,
                                         String Protocolo,
                                         String CNAE)
        {
            using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
            {
                return pj.QuerySociedade(Matricula, NomeSociedade, ProtocoloRequerimento, CNAE);
            }

        }

        #endregion

        #region Endereco Empresa
        public string getEnderecoEmpresa()
        {
            string _ende = "";

            _ende = _SedeDsTipoLogradouro.ToUpper() + " " + _SedeLogradouro.ToString().ToUpper() + ", ";
            _ende += _SedeNumero.ToUpper().Trim() + ((_SedeComplemento.Trim().ToUpper() != String.Empty) ? (", " + _SedeComplemento.Trim().ToUpper()) : " ");
            _ende += ", " + _SedeBairro.ToUpper() + ", " + _nomeMunicipioSede.ToUpper() + ", " + _SedeUF.ToUpper();
            if (_SedeCEP != "")
            {
                if (_SedeCEP.Length == 8)
                    _ende += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(_SedeCEP));
                else
                    _ende += ", CEP " + _SedeCEP;
            }
            return _ende;
        }
        #endregion

        #region Funcoes Socios
        private List<bSocios> GetSociosTodos()
        {
            _SociosTodos.Clear();
            foreach (bSocios soc in _Socios)
            {
                _SociosTodos.Add(soc);
            }
            return _SociosTodos;
        }

        private List<bSocios> GetRepresentanteCapa()
        {
            _listRepresentanteCapa.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.Qualificacao != "5"))
                {
                    _listRepresentanteCapa.Add(soc);
                }
            }
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao == "5" && SomenteAdm(soc.CPFCNPJ))
                {
                    _listRepresentanteCapa.Add(soc);
                }
            }
            return _listRepresentanteCapa;
        }



        private List<bSocios> GetSociosAdminstradorAtivos()
        {
            _SociosAtivos.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.Qualificacao != "5") || (soc.Qualificacao == "5" || soc.rep_legal != 0))
                {
                    _SociosAtivos.Add(soc);
                }
            }
            return _SociosAtivos;
        }

        private List<bSocios> GetSociosAtivosExaminador()
        {
            _SociosAtivosExaminador.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao != "5" || soc.IndicadorDBE > 0)
                {
                    _SociosAtivosExaminador.Add(soc);
                }
            }
            return _SociosAtivosExaminador;
        }

        private List<bSocios> GetSociosAtivos()
        {
            _SociosAtivos.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao != "5")
                {
                    _SociosAtivos.Add(soc);
                }
            }
            return _SociosAtivos;
        }

        private List<bSocios> GetSociosAtuais()
        {
            _SociosAtuais.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.tipoacao == 3 || soc.tipoacao == 0 || soc.tipoacao == 5)
                {
                    _SociosAtuais.Add(soc);
                }
            }
            return _SociosAtuais;
        }

        private List<bSocios> GetSociosAtivosSemSaida()
        {
            _SociosAtivosSemSaida.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao != "5" && soc.tipoacao != 5)
                {
                    _SociosAtivosSemSaida.Add(soc);
                }
            }
            return _SociosAtivosSemSaida;
        }
        /// <summary>
        /// Retorna uma lista de pessoas somente com qualificação de adminstrador
        /// </summary>
        /// <returns></returns>
        private List<bSocios> GetAdministradoresSomente()
        {
            _AdministradoresAtivos.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao == "5" && SomenteAdm(soc.CPFCNPJ))
                {
                    _AdministradoresAtivos.Add(soc);
                }
            }
            return _AdministradoresAtivos;
        }
        /// <summary>
        /// Valida se uma pessoa só tem a qualificação de Adminstrador
        /// </summary>
        /// <param name="cCpf"></param>
        /// <returns></returns>
        private bool SomenteAdm(string cCpf)
        {
            foreach (bSocios soc in _Socios)
            {
                if (soc.CPFCNPJ == cCpf && soc.Qualificacao != "5")
                {
                    return false;
                }
            }
            return true;
        }

        private List<bSocios> GetAdministradoresAtual()
        {
            _AdministradoresAtual.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.tipoacao == 3 || soc.tipoacao == 0) && (soc.Qualificacao == "5" || soc.rep_legal != 0) && soc.DataSaidaSocio == null)
                {
                    _AdministradoresAtual.Add(soc);
                }
            }
            return _AdministradoresAtual;
        }

        private List<bSocios> GetAdministradoresTodos()
        {
            _AdministradoresTodos.Clear();
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao == "5" || soc.rep_legal != 0)
                {
                    _AdministradoresTodos.Add(soc);
                }
            }
            return _AdministradoresTodos;
        }
        private List<bSocios> GetAdministradoresAtivos()
        {
            _AdministradoresAtivos.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.Qualificacao == "5" || soc.rep_legal != 0) && soc.DataSaidaSocio == null)
                {
                    _AdministradoresAtivos.Add(soc);
                }
            }
            return _AdministradoresAtivos;
        }

        private List<bSocios> GetAdministradoresIsoladamente()
        {
            _AdministradoresIsoladamente.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.Qualificacao == "5" || soc.rep_legal != 0) && soc.DataSaidaSocio == null && soc.AdminstracaoIsoladamente == 1)
                {
                    _AdministradoresIsoladamente.Add(soc);
                }
            }
            return _AdministradoresIsoladamente;
        }
        private List<bSocios> GetAdministradoresConjuntamente()
        {
            _AdministradoresConjuntamente.Clear();
            foreach (bSocios soc in _Socios)
            {
                if ((soc.Qualificacao == "5" || soc.rep_legal != 0) && soc.DataSaidaSocio == null && soc.AdminstracaoConjuntamente == 1)
                {
                    _AdministradoresConjuntamente.Add(soc);
                }
            }
            return _AdministradoresConjuntamente;
        }


        public List<bSocios> GetAdministradoresAtivos(int seqPessoa)
        {
            List<bSocios> listaAdm = new List<bSocios>();

            foreach (bSocios soc in _Socios)
            {
                if (soc.SQPessoa != seqPessoa.ToString())
                {
                    if ((soc.Qualificacao == "5" || soc.rep_legal != 0))
                    {
                        listaAdm.Add(soc);
                    }
                }
            }
            return listaAdm;
        }

        /// <summary>
        /// Usado no examinador para verficar se o admnistrator é sócio
        /// independente se o socio está baixando
        /// </summary>
        /// <param name="sqPessoa"></param>
        /// <param name="_Cpf"></param>
        /// <returns></returns>
        public bool AdminstradorSocioExame(string sqPessoa, string _Cpf)
        {
            bool ret = false;
            foreach (bSocios soc in _Socios)
            {
                if ((soc.CPFCNPJ == _Cpf) && (soc.SQPessoa != sqPessoa) && soc.Qualificacao != "5"
                    || (soc.CPFCNPJ == _Cpf) && (soc.SQPessoa == sqPessoa) && (soc.Qualificacao != "5" && soc.rep_legal > 0))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public bool AdminstradorSocio(string sqPessoa, string _Cpf)
        {
            bool ret = false;
            foreach (bSocios soc in _Socios)
            {
                if ((soc.CPFCNPJ == _Cpf) && (soc.SQPessoa != sqPessoa) && soc.Qualificacao != "5" && soc.DataSaidaSocio == null)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public bool AdminstradorSocio(int sqPessoa)
        {
            bool ret = false;
            foreach (bSocios soc in _Socios)
            {
                if (int.Parse(soc.SQPessoa) != sqPessoa && soc.Qualificacao != "5" && soc.DataSaidaSocio == null)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public string GetDadosQSA(bSocios s, bool comPontoFinal, bool comSocioNovo)
        {
            return GetDadosQSA(s, comPontoFinal, comSocioNovo, true, true);
        }
        public string GetDadosQSA(bSocios s, bool comPontoFinal, bool comSocioNovo, bool comNomeSocio, bool comRepresentante)
        {
            String IdentificaSocios = "";
            String wCnpj = "";
            string wCnpjAux = "";


            if (s != null)
            {

                #region Dados do Sócio
                if (comNomeSocio)
                {
                    IdentificaSocios = NomeProprio(s.Nome).ToUpper();
                }
                if (comSocioNovo)
                {
                    IdentificaSocios += " admitido neste ato,";
                }

                wCnpj = s.CPFCNPJ;
                if (wCnpj.Trim().Length == 11)
                {
                    IdentificaSocios += " nacionalidade " + s.Nacionalidade.ToString().ToUpper();
                    if (s.in_Sexo == "M")
                    {
                        IdentificaSocios += ", nascido em " + String.Format("{0:dd/MM/yyyy}", s.DataNascimento);
                    }
                    else
                    {
                        IdentificaSocios += ", nascida em " + String.Format("{0:dd/MM/yyyy}", s.DataNascimento);
                    }

                    if (!string.IsNullOrEmpty(s.TipoEmancipado) && s.TipoEmancipado != "0")
                    {
                        if (s.in_Sexo == "M")
                        {
                            IdentificaSocios += ", emancipado por " + ObterDescricaoEmancipado(s.TipoEmancipado.ToString()).ToUpper();
                        }
                        else
                        {
                            IdentificaSocios += ", emancipada por " + ObterDescricaoEmancipado(s.TipoEmancipado.ToString()).ToUpper();
                        }
                    }


                    if (ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper().Equals("CASADO"))
                    {
                        if (s.in_Sexo == "M")
                        {
                            IdentificaSocios += ", " + ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper();
                        }
                        else
                        {
                            IdentificaSocios += ", casada";
                        }
                    }
                    else
                    {
                        IdentificaSocios += ", " + VerificaGenero(ObterDescricaoEstadoCivil(s.EstadoCivil).ToString(), s.in_Sexo).ToUpper();
                    }

                    if (ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper().Equals("CASADO"))
                    {
                        IdentificaSocios += " em " + ObterDescricaoRegimeBens(s.EstadoCivilRegime).ToString().ToUpper();
                    }

                    IdentificaSocios += ", " + s.Profissao_Descricao.ToString().ToUpper();
                    wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                    IdentificaSocios += ", CPF/MF nº " + wCnpjAux;
                    IdentificaSocios += ", " + ObterDescricaoTipoDoc(s.TipoIdentidade).ToString().ToUpper() + " nº " + s.RG.ToString();
                    string wOrgaoExpedidor = s.OrgaoExpedidor.ToString();
                    if (Parametros.getValor(bParametro.Valores.IMPRIME_NOME_ORG_EXPED) == "1")
                    {
                        wOrgaoExpedidor = RetornaOrgaoRegistro(s.OrgaoExpedidor.ToString());
                    }


                    IdentificaSocios += ", órgão expedidor " + wOrgaoExpedidor.ToUpper();
                    if (s.OrgaoExpedidorUF.ToString() != "")
                    {
                        IdentificaSocios += " - " + s.OrgaoExpedidorUF.ToString().ToUpper();
                    }


                }
                else
                {
                    IdentificaSocios += " CNPJ " + wCnpj;
                    if (s.TipoOrgaoRegistro == 1)
                    {
                        IdentificaSocios += ", NIRE " + s.Nire;
                    }
                    else
                    {
                        IdentificaSocios += ", MATRICULA " + s.Nire;
                    }
                }

                if (s.tipo_visto != null && s.tipo_visto != "")
                {
                    IdentificaSocios += ", tipo de visto " + s.tipo_visto.ToUpper();
                    if (s.emissao_visto.ToString() != "")
                    {
                        IdentificaSocios += " emitido em " + String.Format("{0:dd/MM/yyyy}", s.emissao_visto);
                    }
                    if (s.validade_visto.ToString() != "")
                    {
                        IdentificaSocios += " com validade até " + String.Format("{0:dd/MM/yyyy}", s.validade_visto);
                    }
                }

                string tipoRuaAux = String.Empty;
                string RuaAux = String.Empty;

                if (s.EndDsTipoLogradouro != null)
                {
                    //tipoRuaAux = NomeCaixaBaixa(s.EndDsTipoLogradouro.ToString().ToUpper());
                    tipoRuaAux = s.EndDsTipoLogradouro.ToString().ToUpper();
                }
                //RuaAux = tipoRuaAux + " " + NomeCaixaBaixa(RetiraComplementoLogradouro(s.EndLogradouro.ToString().ToUpper()));
                RuaAux = tipoRuaAux + " " + TrimAll(RetiraComplementoLogradouro(s.EndLogradouro.ToString().ToUpper()));

                if (wCnpj.Trim().Length == 11)
                    IdentificaSocios += ", residente e domiciliado no(a) ";
                else
                    IdentificaSocios += ", com sede no(a) ";

                IdentificaSocios += " " + RuaAux.ToUpper();

                if (!string.IsNullOrEmpty(s.EndNumero.ToString()))
                    IdentificaSocios += ", " + s.EndNumero.ToString().ToUpper();

                if (!string.IsNullOrEmpty(s.EndComplemento.ToString().ToUpper()))
                {
                    //IdentificaSocios += ", " + NomeCaixaBaixa(s.EndComplemento.ToString().ToUpper());
                    IdentificaSocios += ", " + s.EndComplemento.ToString().ToUpper();
                }

                //IdentificaSocios += ", " + NomeCaixaBaixa(s.EndBairro.ToString().ToUpper());
                IdentificaSocios += ", " + s.EndBairro.ToString().ToUpper();

                if (s.EndPais.ToString().ToUpper() == "154")
                {

                    IdentificaSocios += ", " + ObterNomeMunicipio(s.EndMunicipio.ToString()).ToUpper();
                    IdentificaSocios += ", " + s.EndUF.ToString().ToUpper() + ", CEP ";
                    IdentificaSocios += String.Format(@"{0:00\.000\-000}", long.Parse(retProt(s.EndCEP.ToString())));

                }

                //IdentificaSocios += ", " + NomeCaixaBaixa(ObterDescricaoPais(s.EndPais.ToString()));
                IdentificaSocios += ", " + ObterDescricaoPais(s.EndPais.ToString());

                if (comPontoFinal)
                {
                    //para colocar ponto final se nao tiver representante
                    if (s.Representantes.Count == 0 && comPontoFinal)
                    {
                        IdentificaSocios += ".";
                    }

                }

                #endregion

                #region Representantes

                if (comRepresentante)
                {
                    int ii = 1;
                    string wSexoAux = "";

                    //Regra
                    //menor de 16 anos REPRESENTADO
                    //maior de 16 e menor de 18 ASSISTIDO
                    //maior de 18 REPRESENTADO

                    if (s.Representantes.Count > 0)
                    {
                        if (s.CPFCNPJ.Length != 14)
                        {
                            if (Menor18(Convert.ToDateTime(s.DataNascimento.ToString())))
                            {
                                if (!Menor16(Convert.ToDateTime(s.DataNascimento.ToString())))
                                {
                                    if (s.in_Sexo == "M")
                                        IdentificaSocios += ", assistido neste ato ";
                                    else
                                        IdentificaSocios += ", assistida neste ato";
                                }
                                else
                                {
                                    if (s.in_Sexo == "M")
                                        IdentificaSocios += ", representado neste ato ";
                                    else
                                        IdentificaSocios += ", representada neste ato ";
                                }
                            }
                            else
                            {
                                if (s.in_Sexo == "M")
                                    IdentificaSocios += ", representado neste ato ";
                                else
                                    IdentificaSocios += ", representada neste ato";
                            }
                        }
                        else
                        {
                            if (s.in_Sexo == "M")
                                IdentificaSocios += ", representado neste ato ";
                            else
                                IdentificaSocios += ", representada neste ato";
                        }



                        foreach (bSocios Repre in s.Representantes)
                        {

                            wSexoAux = Repre.in_Sexo.ToString();


                            //if (wSexoAux == "M")
                            //    IdentificaSocios += " por seu ";
                            //else
                            //    IdentificaSocios += " por sua ";


                            if (Repre.TipoRepresentante == "PROCURADOR")
                            {
                                if (Repre.in_Sexo.ToString() == "M")
                                {
                                    IdentificaSocios += " por seu " + Repre.TipoRepresentante;
                                }
                                else
                                {
                                    IdentificaSocios += "por sua PROCURADORA";
                                }
                            }
                            else
                            {
                                if (ii > 1)
                                    IdentificaSocios += " e por ";
                                else
                                    IdentificaSocios += " por ";

                                IdentificaSocios += Repre.TipoRepresentante;
                            }
                            //IdentificaSocios += Repre.CTipoRepresentante.Descricao; 

                            //if (ii > 1)
                            //    IdentificaSocios += " e por ";


                            IdentificaSocios += " " + Repre.Nome.ToString().ToUpper() + ", "; ;

                            ii = ii + 1;

                            IdentificaSocios += " nacionalidade " + Repre.Nacionalidade.ToString().ToUpper() + "";

                            if (Repre.in_Sexo.ToString() == "M")
                                IdentificaSocios += ", nascido em ";
                            else
                                IdentificaSocios += ", nascida em ";

                            IdentificaSocios += String.Format("{0:dd/MM/yyyy}", Repre.DataNascimento) + "";

                            if (!string.IsNullOrEmpty(Repre.TipoEmancipado.ToString()) && Repre.TipoEmancipado != "0")
                                if (wSexoAux == "M")
                                    IdentificaSocios += ", emancipado por " + ObterDescricaoEmancipado(Repre.TipoEmancipado.ToString()).ToUpper();
                                else
                                    IdentificaSocios += ", emancipada por " + ObterDescricaoEmancipado(Repre.TipoEmancipado.ToString()).ToUpper();

                            IdentificaSocios += ", " + VerificaGenero(ObterDescricaoEstadoCivil(Repre.EstadoCivil), Repre.in_Sexo).ToUpper() + "";

                            if (ObterDescricaoEstadoCivil(Repre.EstadoCivil).ToString().ToUpper() == "CASADO" && ObterDescricaoRegimeBens(Repre.EstadoCivilRegime).ToString().ToUpper() != String.Empty)
                            {
                                IdentificaSocios += " em " + ObterDescricaoRegimeBens(Repre.EstadoCivilRegime).ToString().ToUpper() + "";
                            }

                            IdentificaSocios += ", " + Repre.Profissao_Descricao.ToString().ToUpper() + "";
                            wCnpj = Repre.CPFCNPJ.ToString();
                            wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                            IdentificaSocios += ", CPF/MF nº " + wCnpjAux + "";
                            IdentificaSocios += ", " + ObterDescricaoTipoDoc(Repre.TipoIdentidade).ToString().ToUpper() + "";
                            IdentificaSocios += " nº " + Repre.RG.ToString().ToUpper() + "";
                            string wOrgaoExpedidor = Repre.OrgaoExpedidor.ToString();
                            if (Parametros.getValor(bParametro.Valores.IMPRIME_NOME_ORG_EXPED) == "1")
                            {
                                wOrgaoExpedidor = RetornaOrgaoRegistro(Repre.OrgaoExpedidor.ToString());
                            }

                            IdentificaSocios += ", Órgão Expedidor " + wOrgaoExpedidor.ToUpper() + "";
                            if (!string.IsNullOrEmpty(Repre.OrgaoExpedidorUF.ToString()))
                            {
                                IdentificaSocios += " - " + Repre.OrgaoExpedidorUF.ToString().ToUpper() + "";
                            }

                            if (Repre.tipo_visto != null && Repre.tipo_visto != "")
                            {
                                IdentificaSocios += ", tipo de visto " + Repre.tipo_visto;
                                if (Repre.emissao_visto.ToString() != "")
                                {
                                    IdentificaSocios += " emitido em " + String.Format("{0:dd/MM/yyyy}", Repre.emissao_visto);
                                }
                                if (Repre.validade_visto.ToString() != "")
                                {
                                    IdentificaSocios += " com validade até " + String.Format("{0:dd/MM/yyyy}", Repre.validade_visto);
                                }
                            }


                            string tipoRuaAuxSocio = String.Empty;
                            string RuaAuxSocio = String.Empty;

                            //tipoRuaAuxSocio = NomeCaixaBaixa(Repre.EndDsTipoLogradouro.ToString().ToUpper());
                            tipoRuaAuxSocio = Repre.EndDsTipoLogradouro.ToString().ToUpper();
                            RuaAuxSocio = RetiraComplementoLogradouro(Repre.EndLogradouro.ToString().ToUpper());

                            IdentificaSocios += ", endereço: " + tipoRuaAuxSocio; //.ToUpper();
                            IdentificaSocios += " " + RuaAuxSocio; //.ToUpper();
                            if (!string.IsNullOrEmpty(Repre.EndNumero.ToString()))
                                IdentificaSocios += ", " + Repre.EndNumero.ToString().ToUpper();


                            if (!string.IsNullOrEmpty(Repre.EndComplemento.ToString().ToUpper()))
                            {
                                //IdentificaSocios += ", " + NomeCaixaBaixa(Repre.EndComplemento.ToString().ToUpper()) + "";
                                IdentificaSocios += ", " + Repre.EndComplemento.ToString().ToUpper() + "";
                            }

                            //IdentificaSocios += ", " + NomeCaixaBaixa(Repre.EndBairro.ToString().ToUpper()) + "";
                            //IdentificaSocios += ", " + NomeCaixaBaixa(ObterNomeMunicipio(Repre.EndMunicipio.ToString()).ToUpper()) + "";

                            IdentificaSocios += ", " + Repre.EndBairro.ToString().ToUpper() + "";
                            IdentificaSocios += ", " + ObterNomeMunicipio(Repre.EndMunicipio.ToString()).ToUpper() + "";

                            IdentificaSocios += ", " + Repre.EndUF.ToString().ToUpper() + "";
                            IdentificaSocios += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(Repre.EndCEP.ToString().ToUpper())) + " ";

                        }

                        if (s.Representantes.Count > 0)
                        {
                            if (comPontoFinal)
                            {
                                IdentificaSocios += ".";

                            }

                        }

                    }
                }
                #endregion

            }

            return IdentificaSocios;
        }

        private static string ObterDescricaoEmancipado(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoTipoEmancipado(Codigo);
            }
            else
                return "";
        }

        private static string ObterDescricaoPais(string CodPais)
        {

            if (CodPais != "")
            {
                return dHelperQuery.BuscarDescricaoPais(CodPais);
            }
            else
                return "";
        }

        private Boolean ValidaMenorIdade(DateTime wData)
        {
            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 18)
                return true;
            else
                return false;
        }

        private Boolean Menor16(DateTime wData)
        {
            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 16)
                return true;
            else
                return false;
        }

        private Boolean Menor18(DateTime wData)
        {
            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 18)
                return true;
            else
                return false;
        }

        private static string ObterNomeMunicipio(string tmu_cod_mun)
        {
            string saida = "";
            //try
            //{
            if (!string.IsNullOrEmpty(tmu_cod_mun))
            {
                dTab_Munic municObj = new dTab_Munic();
                municObj.tmu_cod_mun = decimal.Parse(tmu_cod_mun);
                DataTable dtMunicipio = municObj.Query();
                DataTableReader dtrMunicipio = dtMunicipio.CreateDataReader();
                dtrMunicipio.Read();
                if (dtrMunicipio.HasRows)
                {
                    saida = dtrMunicipio["descricao"].ToString().ToUpper();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    ErroresDeSistema("Problemas no sistema ao gerar contrato.", ex, ref ErrorSummary);
            //}
            return saida;
        }

        private string VerificaGenero(string texto, string wGenero)
        {
            string wTexto = "";
            int _tamTexto = 0;
            int _pos = 0;
            if (texto.Trim() != "")
            {
                _tamTexto = texto.Length;
                _pos = texto.IndexOf(" ", 0);

                if (_pos > 0)
                {
                    if (wGenero == "M")
                        wTexto = texto.Substring(0, _pos - 1) + "O" + texto.Substring(_pos, texto.Length - _pos);
                    else
                        wTexto = texto.Substring(0, _pos - 1) + "A" + texto.Substring(_pos, texto.Length - _pos);
                }
                else
                {
                    if (wGenero == "M")
                        wTexto = texto.Substring(0, texto.Length - 1) + "O";
                    else
                        wTexto = texto.Substring(0, texto.Length - 1) + "A";
                }

            }
            return wTexto.ToUpper();
        }

        private static string ObterDescricaoEstadoCivil(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoEstadoCivil(Codigo);
            }
            else
                return "";
        }

        private static string ObterDescricaoRegimeBens(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoRegimeBens(Codigo);
            }
            else
                return "";
        }
        private string RetiraComplementoLogradouro(string wTexto)
        {
            string wAux = wTexto;
            int wInicio = 0;
            if (wAux.IndexOf(" (-") != -1)
            {
                wInicio = wAux.IndexOf(" (-");
                wAux = StringDireita(wAux, wInicio);
            }
            return wAux;
        }
        private static string StringDireita(string param, int tamanho)
        {
            string result = param.Substring(0, tamanho); //param.Length - tamanho);
            return result;

        }
        private string NomeCaixaBaixa(string value)
        {
            string Retorno = string.Empty;
            if (string.IsNullOrEmpty(value))
                return "";
            string[] wValue = value.Split(' ');
            for (int i = 0; i < wValue.Length; i++)
            {
                string aux = wValue[i].ToLower();
                if (aux.Length <= 3)
                {
                    if (aux == "da" || aux == "de" || aux == "do" || aux == "das" || aux == "dos" || aux == "e")
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = aux;
                        else
                            Retorno += " " + aux;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = NomeProprio(aux);
                        else
                            Retorno += " " + NomeProprio(aux);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Retorno))
                        Retorno = NomeProprio(aux);
                    else
                        Retorno += " " + NomeProprio(aux);
                }
            }
            return Retorno;

        }
        private string NomeProprio(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Length == 0) return value;
            value = value.ToLower();
            System.Text.StringBuilder result = new System.Text.StringBuilder(value);
            result[0] = char.ToUpper(result[0]);

            for (int i = 1; i < result.Length; ++i)
            {

                if (char.IsWhiteSpace(result[i - 1]))
                {
                    result[i] = char.ToUpper(result[i]);
                }
            }
            return result.ToString();
        }
        private static string ObterDescricaoTipoDoc(string CodDoc)
        {

            if (CodDoc != "")
            {
                return dHelperQuery.BuscarDescricaoTipoDocumento(CodDoc);
            }
            else
                return "";
        }

        public string ValidaQuotasSocios()
        {
            string ret = "";

            if (_NaturezaJuridicaCodigo == 2143)
                return ret;

            if (getEventoAvalia(CODEVENTO_CAPITAL_SOCIAL)
                || getEventoAvalia(CODEVENTO_ALTER_QSA)
                || getEventoAvalia(CODEVENTO_CONSTITUICAO_EMPRESA)
                || getEventoAvalia(CODEVENTO_BAIXA))
            {
                Decimal TotalCotas = 0;
                if (getEventoAvalia(CODEVENTO_BAIXA))
                {
                    return "";
                }

                if (TipoPessoaJuridicaCodigo == 1264)
                {
                    decimal wSomaCapitalIntegralizado = 0;
                    decimal wSomaCapitalNaoIntegralizado = 0;
                    foreach (bSocios socioAux1 in Socios)
                    {
                        //if (socioAux1.DataSaidaSocio != null && socioAux1.QuotaCapitalSocial > 0)
                        //{
                        //    ret = "O capital integralizado da empresa nâo confere com a soma do capital integralizado pelos sócios.";
                        //}
                        if (socioAux1.Qualificacao != "5" && socioAux1.tipoacao != 5)
                        {
                            if (socioAux1.QuotaCapitalSocial == 0)
                            {
                                ret = "Informe a quntidade de quotas do sócio " + socioAux1.Nome;
                                return ret;
                                
                            }
                        }
                        if (socioAux1.Situacao != "R" && socioAux1.Qualificacao != "5")
                        {
                            TotalCotas = TotalCotas + socioAux1.QuotaCapitalSocial; // +socioAux1.AporteSocio;
                            wSomaCapitalIntegralizado += socioAux1.CapitalIntegralizado;
                            wSomaCapitalNaoIntegralizado += socioAux1.Capital_a_Integralizar;

                        }
                    }

                    if (Capital_Integralizado != wSomaCapitalIntegralizado && wSomaCapitalIntegralizado != 0)
                    {
                        ret = "O capital integralizado da empresa nâo confere com a soma do capital integralizado pelos sócios.";
                    }
                    if (Capital_Nao_Integralizado != wSomaCapitalNaoIntegralizado)
                    {
                        ret = "O capital integralizado da empresa nâo confere com a soma do capital integralizado pelos sócios.";
                    }


                    if (TotalCotas != QtdCotas)
                    {
                        ret = "O somatório das Quotas dos Sócios não confere com o total de quotas da Empresa";
                    }
                }
            }
            return ret;
        }

        #endregion

        #region Outros
        /// <summary>
        /// Verifica se o sócio é adminstrador
        /// </summary>
        /// <param name="wCPF"></param>
        /// <returns></returns>
        public bool VerificaSocioAdm(string _sqPessoa)
        {
            bool ret = false;
            string _cpf = string.Empty;
            string _qualificacao = string.Empty;

            foreach (bSocios s in _Socios)
            {
                if (s.SQPessoa == _sqPessoa)
                {
                    _cpf = s.CPFCNPJ;
                    _qualificacao = s.Qualificacao;
                    break;
                }
            }
            if (_qualificacao == "5")
            {
                foreach (bSocios s in _Socios)
                {
                    if (s.CPFCNPJ == _cpf && s.SQPessoa != _sqPessoa && s.Qualificacao != _qualificacao)
                    {
                        ret = true;
                    }
                }
            }
            else
            {
                foreach (bSocios s in _Socios)
                {
                    if (s.CPFCNPJ == _cpf && s.SQPessoa != _sqPessoa && s.Qualificacao == "5")
                    {
                        ret = true;
                    }
                }
            }

            return ret;

        }

        public bool VerificaEireliTemAdm()
        {
            bool ret = false;
            string _cpfTitular = string.Empty;
            string _qualificacao = string.Empty;

            //Recupera o Titular
            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao != "5")
                {
                    _cpfTitular = soc.CPFCNPJ;
                }
            }

            //Verifica se existe um admnistrador não Titular

            foreach (bSocios soc in _Socios)
            {
                if (soc.Qualificacao == "5" && _cpfTitular != soc.CPFCNPJ)
                    ret = true;
            }
            

            return ret;

        }

        public bool VerificaAdministradorRepresentante(string wCPF) // Verifica se administrador é representante de sócio
        {
            for (int i = 0; i < Socios.Count; i++)
            {
                for (int j = 0; j < Socios[i].Representantes.Count; j++)
                {
                    if (wCPF == Socios[i].Representantes[j].CPFCNPJ)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsNumtValidated(string strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }


        public string AchaProtocoloRequerimento(string wAux)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.AchaProtocoloSeHouver(wAux).ToString();
            }

        }

        public DataTable BuscaPorProtocolo(String wProcessoJunta)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.BuscaPorProtocolo(wProcessoJunta);
            }
        }

        public string BuscaPorProcessoJunta(string wProcessoJunta)
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    p.MainConnectionProvider = cp;
                    return p.BuscaPorProcessoJunta(wProcessoJunta).ToString();
                }
            }
        }

        public Boolean VerificaViabilidadeDisponivel(string wAux)
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cp;
                    return pj.ViabilidadeDisponivel(wAux);

                }
            }

            //for (int i = 0; i < _Filiais.Count; i++)
            //{
            //    if (_Filiais[i].FilialViabilidade == wAux)
            //        return false;
            //}

        }

        public DataTable EncontraTipoPessoaJuridica(int wCodNatJur)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();
                using (dR006_Natureza_Juridica_Tipo njt = new dR006_Natureza_Juridica_Tipo())
                {
                    njt.MainConnectionProvider = cv;
                    return njt.EncontraTipoPessoaJuridica(wCodNatJur);
                }
            }
        }
        public Boolean NecessitaViabilidade(int Tipo)
        {
            Boolean wChave = false;
            using (dA002_Ato c = new dA002_Ato())
            {
                DataTable dt = new DataTable();
                dt = c.VerSeNecessitaViabilidade(Tipo);
                if (dt.Rows[0]["a002_in_viabilidade"].ToString() == "S")
                    wChave = true;
            }
            return wChave;
        }

        #region Validação de DBE e Viabilidade já utilizados
        /// <summary>
        /// Valida se o DBe está sendo usado por outro requerimento
        /// </summary>
        /// <param name="wDBE"></param>
        /// <returns></returns>
        public string VerificaDBEExistente(string wDBE)
        {
            string wAux = "";
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                DataTable dt = new DataTable();
                dt = p.VerificaExistenciaDBE(wDBE);
                if (dt.Rows.Count > 0)
                    wAux = dt.Rows[0]["t005_nr_protocolo_viabilidade"].ToString();
            }
            return wAux;
        }

        public int VerificaDbeUtilizado(string wDBE)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.VerificaExistenciaDBE(wDBE, _ProtocoloRequerimento);
            }
        }
        public string ValidaDbeUtilizado(string wDBE)
        {

            string ret = "";
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                string _numreq = p.ValidaDbeUtilizado(wDBE, _ProtocoloRequerimento);
                if (_numreq != "")
                {
                    ret = "DBE está sendo utilizado no Requerimento " + _numreq;
                }
            }
            return ret;
        }
        public string VerificaDbeUtilizadoFilial(string wDBE)
        {
            string ret = "";
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                DataTable dt = p.VerificaDbeUtilizadoFilial(wDBE, _ProtocoloRequerimento);
                if (dt.Rows.Count > 0)
                {
                    ret = "DBE está sendo utilizado no Requerimento " + dt.Rows[0]["requerimento"].ToString();
                }
            }
            return ret;
        }

        /// <summary>
        /// Validar se a Viabilidade está sendo utulizada em outro Requerimento
        /// </summary>
        /// <param name="wAux"></param>
        /// <returns></returns>
        public string ValidaViabilidadeUtilizada(string _Viabilidade)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.ValidaViabilidadeUtilizada(_Viabilidade, _ProtocoloRequerimento);
            }

        }
        #endregion

        public DataTable EncontraAtoEvento()
        {
            using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
            {
                pe.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                pe.t007_nr_protocolo = _ProtocoloRequerimento;
                pe.t001_sq_pessoa = _CodigoEmpresa;

                DataTable dt = new DataTable();
                dt = pe.Query();
                return dt;
            }

        }

        public void Atualiza_Status()
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();
                using (dT011_Protocolo_Status ps = new dT011_Protocolo_Status())
                {
                    ps.MainConnectionProvider = cv;
                    ps.T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                    ps.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                    ps.T011_IN_SITUACAO = _status_protocolo;
                    ps.T011_DT_SITUACAO = DateTime.Now;
                    ps.T011_USUARIO = _UsuarioRegin;

                    ps.Update();
                    cv.CommitTransaction();

                }
            }
        }

        public void apagaConfirmacao(decimal wSequencia, decimal wItem, string wProtocolo)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();
                using (dT017_Protocolo_Confirmacao pc = new dT017_Protocolo_Confirmacao())
                {
                    pc.MainConnectionProvider = cv;
                    pc.ApagaConfirmacao(wSequencia, wItem, wProtocolo);
                    cv.CommitTransaction();

                }
            }
        }

        public void ApagaViabilidade()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.ApagaViabilidadeCancelada(_ProtocoloRequerimento);
            }
        }

        public bool ExcluiAdminstrador(int _sqPessoa)
        {

            try
            {
                //procura na coleção de socio a qulificaçao de adminatrado da pessoa
                int _sqPessoaAdmin = getSqPessoaAdmin(_sqPessoa);
                if (_sqPessoaAdmin == 0)
                    return false;


                using (ConnectionProvider cv = new ConnectionProvider())
                {
                    cv.OpenConnection();
                    cv.BeginTransaction();


                    using (dR001_Vinculo v = new dR001_Vinculo())
                    {
                        v.MainConnectionProvider = cv;
                        v.Deleta(_sqPessoaAdmin, _CodigoEmpresa, 0);
                    }

                    using (dR004_Atuacao a = new dR004_Atuacao())
                    {
                        a.MainConnectionProvider = cv;
                        a.Deleta(_sqPessoaAdmin);
                    }
                    using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                    {
                        pj.MainConnectionProvider = cv;
                        pj.Deleta(_sqPessoaAdmin);
                    }

                    using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                    {
                        ve.MainConnectionProvider = cv;
                        ve.Deleta(_sqPessoaAdmin);
                    }

                    using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                    {
                        pf.MainConnectionProvider = cv;
                        pf.Deleta(_sqPessoaAdmin);
                    }

                    using (dT001_Pessoa p = new dT001_Pessoa())
                    {
                        p.MainConnectionProvider = cv;
                        p.Deleta(_sqPessoaAdmin);
                    }
                    cv.CommitTransaction();

                    //Exclui da coleção
                    for (int i = 0; i < _Socios.Count; i++)
                    {
                        if (_Socios[i].SQPessoa == _sqPessoaAdmin.ToString())
                        {
                            _Socios.RemoveAt(i);
                            break;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// retorna o sqPessoa da qualificação de adminstrador de um spocio
        /// </summary>
        /// <param name="_sqpessoa"></param>
        /// <returns></returns>
        public int getSqPessoaAdmin(int _sqPessoa)
        {
           
            string _cpf = string.Empty;
            string _qualificacao = string.Empty;

            //recupera o CFP do socio para achar o mesmo cpf omo qualiificacao de Adm
            foreach (bSocios s in _Socios)
            {
                if (s.SQPessoa == _sqPessoa.ToString())
                {
                    _cpf = s.CPFCNPJ;
                    _qualificacao = s.Qualificacao;
                    break;
                }
            }

            // Se não achou retorna 0 
            if (String.IsNullOrEmpty(_cpf))
                return 0;


            //Procura na coleção  de socio o cpf com qualificação de Administrador
            foreach (bSocios s in _Socios)
            {
                if ((s.CPFCNPJ == _cpf) && (s.SQPessoa != _sqPessoa.ToString()) && (s.Qualificacao == "5"))
                {
                    return Int32.Parse(s.SQPessoa);
                }
            }


            return 0;

        }

        public void ApagaPessoa(int wsqPessoa)
        {
            ApagaPessoa(wsqPessoa, -5, -5, true);
        }
        public void ApagaPessoa(int wsqPessoa, int wsqPessoaPai, string wsqQualificacao)
        {
            ApagaPessoa(wsqPessoa, wsqPessoaPai, Convert.ToInt32(wsqQualificacao), false);
        }

        public int ContaPessoaNoVinculo_old(int wSqPessoa)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();

                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cv;

                    return v.ContaPessoaVinculo(wSqPessoa);
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

        public void ApagaPessoa(int wsqPessoa, int wsqPai, int wQualificacao, bool apagatudo)
        {
            /* 1 - contar quantas qualificações da pessoa
             * 2 - se existir mais de uma qualificação 
             *          exclui da vinculo 
             *          não exclui o vinculo_endereço , pessoa , pessoa fisica ou juridica
             *     se existir uma qualificação
             *          eclui da vinculo endereco, vinculo , pessoa fisica/juridica e pessoa
            */

            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();

                int _qtdPessoa = ContaPessoaNoVinculo(wsqPessoa, cv);

                //Verifica se tem representante
                foreach (bSocios ss in _Socios)
                {
                    if (ss.SQPessoa == wsqPessoa.ToString())
                    {
                        if (ss.Representantes.Count > 0)
                        {
                            //deleta os representantes
                            foreach (bSocios rr in ss.Representantes)
                            {
                                ApagaRepresentante(Int32.Parse(rr.SQPessoa), wsqPessoa, cv);
                            }
                        }
                        break;
                    }
                }

                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cv;
                    v.Deleta(wsqPessoa, wsqPai, 0);
                }

                using (dR004_Atuacao a = new dR004_Atuacao())
                {
                    a.MainConnectionProvider = cv;
                    a.Deleta(wsqPessoa);
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

                using (dR005_Protocolo_Evento ev = new dR005_Protocolo_Evento())
                {
                    ev.Deleta(wsqPessoa);
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

        private string RetornsDescrAcao(int acao)
        {
            string oRetorno = string.Empty;
            switch (acao)
            {
                case 1:
                    {
                        oRetorno = "Abertura";
                        break;
                    }
                case 2:
                    {
                        oRetorno = "";
                        break;
                    }
                case 3:
                    {
                        oRetorno = "Alteração";
                        break;
                    }
                case 4:
                    {
                        oRetorno = "Transferência";
                        break;
                    }
                case 5:
                    {
                        oRetorno = "Baixa";
                        break;
                    }
                default:
                    {
                        oRetorno = "";
                        break;
                    }

            }
            return oRetorno;
        }
        private void AtualizaFiliais()
        {
            _Filiais.Clear();
            DataTable dt = new DataTable();

            dt = dHelperQuery.CarregaFiliaisComProtocolo(_CodigoEmpresa);
            foreach (DataRow r in dt.Rows)
            {

                bFilial f = new bFilial();
                f.SqEmpresa = _CodigoEmpresa;
                f.SqFilial = Convert.ToInt32(r["t001_sq_pessoa"]);
                f.Nire = r["T003_NR_MATRICULA"].ToString();
                f.Cnpj = r["T003_NR_CNPJ"].ToString();

                #region Endereço
                f.FilialTipoLogradouro = r["tipo_logradouro"].ToString();
                f.FilialLogradouro = r["logradouro"].ToString();
                f.FilialBairro = r["bairro"].ToString();
                f.FilialCEP = r["cep"].ToString();
                f.FilialNumero = r["num_logradouro"].ToString();
                f.FilialComplemento = r["comp_logradouro"].ToString();
                f.FilialUF = r["uf"].ToString();
                f.FilialCodMunicipio = Convert.ToInt32(r["tmu_cod_mun"].ToString());
                f.FilialMunicipio = r["tmu_des_mun"].ToString();
                #endregion

                f.FilialViabilidade = r["Num_Viabilidade"].ToString();
                f.FilialDBE = r["t003_DBE"].ToString();

                f.Acao = Int32.Parse(r["R001_ACAO"].ToString());
                f.DescrAcao = RetornsDescrAcao(f.Acao);

                f.FilialOBJSocial = r["T003_DS_OBJETO_SOCIAL"].ToString();
                f.FilialUFOrigem = r["T003_UF_ORIGEM"].ToString();

                if (!String.IsNullOrEmpty(r["T003_DT_INICIO_ATIVIDADE"].ToString()))
                {
                    f.DataInicioAtividade = Convert.ToDateTime(r["T003_DT_INICIO_ATIVIDADE"].ToString());
                }
                f.FilialCnaeDestacado = r["T003_IND_CNAE_DESTACADA"].ToString() == "" ? 2 : Int32.Parse(r["T003_IND_CNAE_DESTACADA"].ToString());
                f.FilialCapitalDestacado = r["T003_VL_CAPITAL_SOCIAL"].ToString() == "" ? 0 : decimal.Parse(r["T003_VL_CAPITAL_SOCIAL"].ToString());
                f.Ordem = Int32.Parse(r["R001_ORDEM_FILIAL_CONTRATO"].ToString() == "" ? "0" : r["R001_ORDEM_FILIAL_CONTRATO"].ToString());
                f.IPTU = r["T003_IPTU"].ToString();
                f.AreaUtilizada = Decimal.Parse(r["T003_AREA_UTILIZADA"].ToString());
                f.DDD = r["T001_DDD"].ToString();
                f.Telefone = r["T001_TEL_1"].ToString();
                
                #region  CARREGA CNAE'S DA FILIAL

                DataTable dtCnae = new DataTable();
                if (_NaturezaJuridicaCodigo == 9999)
                {
                    //Busca pelo CBO
                    dtCnae = dHelperQuery.CarregaCBOComProtocolo(f.SqFilial);
                }
                else
                {
                    dtCnae = dHelperQuery.CarregaCnaeComProtocolo(f.SqFilial);
                }

                foreach (DataRow rf in dtCnae.Rows)
                {
                    bCNAE c = new bCNAE();
                    c.CodigoCNAE = rf["A001_CO_ATIVIDADE"].ToString();
                    c.Descricao = rf["TAD_DESC_ATIVIDADE"].ToString();
                    c.TipoAtividade = decimal.Parse(rf["R004_IN_PRINCIPAL_SECUNDARIO"].ToString());
                    c.Exercida = "1";
                    f.CNAEs.Add(c);
                }

                #endregion

                DataTable dtEvento = EncontraAtoEvento();

                using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
                {
                    pe.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    pe.t007_nr_protocolo = _ProtocoloRequerimento;
                    pe.t001_sq_pessoa = f.SqFilial;
                    dtEvento = pe.Query();
                }

                if (dtEvento.Rows.Count > 0)
                {
                    foreach (DataRow row in dtEvento.Rows)
                    {
                        bProtocoloEvento ev = new bProtocoloEvento();
                        ev.CodigoEvento = decimal.Parse(row["a003_co_evento"].ToString());
                        ev.ProtocoloRequerimento = _ProtocoloRequerimento;
                        ev.CnpjOrgaoRegistro = _CNPJ_Orgao_Registro;
                        ev.CodigoAto = decimal.Parse(row["a002_co_ato"].ToString());
                        ev.DescricaoEvento = row["a003_co_evento"].ToString() + " - " + row["A002_DS_ATO"].ToString();
                        ev.DescricaoResumida = row["A002_DS_ATO"].ToString();

                        f.ProtocoloEvento.Add(ev);

                    }
                }
                _Filiais.Add(f);

            }
        }
        
        public Boolean VerificaSeNumerico(string wTexto)
        {
            foreach (char s in wTexto)
            {
                if (!char.IsDigit(s))
                    return false;
            }
            return true;
        }
        public string RetornaSequenciaPessoa(string SeqEmpresa, string cpf, out string squalificacao, out string snome)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                return v.RetornaSequenciaPessoa(SeqEmpresa, cpf, out squalificacao, out snome);
            }

        }
        public string RetornaSequenciaPessoaFundador(string SeqEmpresa, string cpf, out string squalificacao, out string snome)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                return v.RetornaSequenciaPessoaFundador(SeqEmpresa, cpf, out squalificacao, out snome);
            }

        }
        public string RetornaSequenciaPessoaRepresentante(string SeqEmpresa, string cpf)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                return v.RetornaSequenciaPessoaRepresentante(SeqEmpresa, cpf);
            }

        }
        public string RetornaSequenciaPessoaRepresentanteFundador(string SeqEmpresa, string cpf)
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
                return v.RetornaSequenciaPessoaRepresentanteFundador(SeqEmpresa, cpf);
            }

        }
        #endregion

        #region Examinador
        /// <summary>
        /// atualiza numero do protocolo, dbe, viabilidade informado no examinador
        /// </summary>
        public void UpdateExaminador()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.T005_nr_protocolo_RCPJ = _ProtocoloRCPJ;
                //p.t005_nr_protocolo_viabilidade = _ProtocoloViabilidade;
                //p.t005_nr_dbe = _CodigoDBE;
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;

                p.UpdateExaminador();
            }
        }

        public void CancelaRequerimentoAnterior(string reqAnterior)
        {
            UpdateStatus(9, reqAnterior);

        }
        public void UpdateStatus(int Status)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;

                if (string.IsNullOrEmpty(UsuarioRegin))
                {
                    UsuarioRegin = "REQUERENTE";
                }
                if (!string.IsNullOrEmpty(UsuarioCPF))
                {
                    UsuarioRegin = _UsuarioCPF;
                }

                p.AtualizaStatus = true;

                p.UpdateStatus(Status, _UsuarioRegin);
                p.UpdateStatusLog(Status, _UsuarioRegin, _CodigoDBE, _ProtocoloViabilidade);
                //if (Status == 2)
                //{
                //    _status_atualizado = true;
                //}
            }
        }
        public void UpdateStatus(int Status, string numRequerimentoAnterior)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = numRequerimentoAnterior;
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                if (_UsuarioRegin == "")
                {
                    p.UpdateStatus(Status, _UsuarioCPF, _ProtocoloRequerimento);
                }
                else
                {
                    p.UpdateStatus(Status, _UsuarioRegin, _ProtocoloRequerimento);
                }

            }
        }

        public void UpdateTransfUnipessoal()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.T005_IN_TRANSF_UNIPESSOAL = _indTransfUnipessoal;
                p.UpdateTransfUnipessoal();

            }
        }

        public void UpdateCodigoAto()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.UpdateCodigoAto(_CodigoAto, _ProtocoloRequerimento);

            }
        }

        public string CarregaRequerimentoSiarcoTeste()
        {
            string MsgErro = null;
            string retorno = "";

            // Gera XML Requerimento
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);

                if (_xmlRequerimento == "" || _xmlRequerimento == null)
                {
                    return "Informe ao suporte tecnico que o xml não foi gerado!" + _ProtocoloRCPJ + _ProtocoloRequerimento + MsgErro;
                }
                else
                {
                    retorno = dHelperORACLE.GravaRequerimentoSIARCO(_ProtocoloRCPJ, _ProtocoloRequerimento, _ProtocoloViabilidade, _xmlRequerimento, _UsuarioRegin);
                    if (retorno == "1")
                    {
                        return "OK";
                    }
                    else
                    {
                        return "NOK";
                    }

                }
            }

        }

        public bool Indeferir()
        {
            UpdateStatus(8);
            return true;
        }

        public bool IndeferirComAndamento()
        {
            string retorno = "";
            bool ok = false;
            int ultimostatus;
            ultimostatus = GetStatusProtocolo();

            try
            {
                //Atualiza status do requerimento para Indeferido (8)

                UpdateStatus(8);

                try
                {
                    retorno = dHelperORACLE.GravaAndamentoIndeferido(_ProtocoloRCPJ, _ProtocoloEnquadramento, _UsuarioRegin);
                    if (retorno == "1")
                    {
                        ok = true;
                        MensagemErro = "";
                    }
                    else
                    {
                        MensagemErro = retorno;
                        ok = false;
                    }
                }
                catch (Exception ex)
                {
                    MensagemErro = ex.Message;
                    ok = false;
                }

                if (!ok)
                {
                    dT005_Protocolo p = new dT005_Protocolo();
                    p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    p.t005_nr_protocolo = _ProtocoloRequerimento;
                    p.DeleteStatus(8);
                    //Atualiza status do requerimento para Deferido (4)
                    UpdateStatus(ultimostatus);
                }

                return ok;
            }
            catch (Exception ex)
            {
                dT005_Protocolo p = new dT005_Protocolo();
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.DeleteStatus(8);
                //Atualiza status do requerimento para Deferido (4)
                UpdateStatus(ultimostatus);
                MensagemErro = " ao tentar deferir o requerimento.";
                string aa = ex.Message;
                return false;
            }
        }

        public string GeraXMLFilial()
        {
            string _xmlRequerimento = "";
            int ultimostatus;
            string MsgErro = "";
            ultimostatus = GetStatusProtocolo();
            UpdateStatus(4);
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);
            }
            UpdateStatus(ultimostatus);
            return _xmlRequerimento;

        }

        public bool Deferir()
        {
            string MsgErro = null;
            string retorno = "";
            bool ok = false;
            int ultimostatus;
            ultimostatus = GetStatusProtocolo();

            try
            {
                //Atualiza status do requerimento para Deferido (4)

                UpdateStatus(4);
                // Gera XML Requerimento
                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);

                    //if (MsgErro == null)
                    //    return true;
                    //else
                    //    return false;

                    if (_xmlRequerimento == "" || _xmlRequerimento == null || _xmlRequerimento.Length < 10)
                    {
                        if (MsgErro != "")
                        {
                            MensagemErro = MsgErro;
                        }
                        else
                        {
                            MensagemErro = "Informe ao suporte tecnico que o xml não foi gerado! " + _xmlRequerimento.Length.ToString();
                        }
                    }
                    else
                    {
                        try
                        {
                            retorno = dHelperORACLE.GravaRequerimentoSIARCO(_ProtocoloRCPJ, _ProtocoloRequerimento, _ProtocoloViabilidade, _xmlRequerimento, _UsuarioRegin);
                            if (retorno == "1")
                            {
                                ok = true;
                                MensagemErro = "";
                            }
                            else
                            {
                                MensagemErro = retorno;
                                ok = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MensagemErro = ex.Message;
                            ok = false;
                        }

                    }

                    if (!ok)
                    {
                        p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        p.t005_nr_protocolo = _ProtocoloRequerimento;
                        p.DeleteStatus(4);
                        //Atualiza status do requerimento para Deferido (4)
                        UpdateStatus(ultimostatus);
                    }
                }
                return ok;
            }
            catch (Exception ex)
            {
                dT005_Protocolo p = new dT005_Protocolo();
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.DeleteStatus(4);
                //Atualiza status do requerimento para Deferido (4)
                UpdateStatus(ultimostatus);
                MensagemErro = " ao tentar deferir o requerimento.";
                string aa = ex.Message;
                return false;
            }


        }


        public bool DeferirRCPJ()
        {
            string MsgErro = null;
            string retorno = "";
            bool ok = false;
            int ultimostatus;
            ultimostatus = GetStatusProtocolo();

            try
            {
                //Atualiza status do requerimento para Deferido (4)

                UpdateStatus(4);
                // Gera XML Requerimento
                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);

                    //if (MsgErro == null)
                    //    return true;
                    //else
                    //    return false;

                    if (_xmlRequerimento == "" || _xmlRequerimento == null || _xmlRequerimento.Length < 10)
                    {
                        if (MsgErro != "")
                        {
                            MensagemErro = MsgErro;
                        }
                        else
                        {
                            MensagemErro = "Informe ao suporte tecnico que o xml não foi gerado! " + _xmlRequerimento.Length.ToString();
                        }
                    }
                    else
                    {
                        try
                        {
                            retorno = dHelperORACLE.GravaRequerimentoRCPJ(_ProtocoloRCPJ, _ProtocoloRequerimento, _ProtocoloViabilidade, _xmlRequerimento);
                            if (retorno == "1")
                            {
                                ok = true;
                                MensagemErro = "";
                            }
                            else
                            {
                                MensagemErro = retorno;
                                ok = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MensagemErro = ex.Message;
                            ok = false;
                        }

                    }

                    if (!ok)
                    {
                        p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        p.t005_nr_protocolo = _ProtocoloRequerimento;
                        p.DeleteStatus(4);
                        //Atualiza status do requerimento para Deferido (4)
                        UpdateStatus(ultimostatus);
                    }
                }
                return ok;
            }
            catch (Exception ex)
            {
                dT005_Protocolo p = new dT005_Protocolo();
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.DeleteStatus(4);
                //Atualiza status do requerimento para Deferido (4)
                UpdateStatus(ultimostatus);
                MensagemErro = " ao tentar deferir o requerimento.";
                string aa = ex.Message;
                return false;
            }


        }
        public bool AtualizarXML()
        {
            string MsgErro = null;
            string retorno = "";
            bool ok = false;
            int ultimostatus;
            ultimostatus = GetStatusProtocolo();

            try
            {


                UpdateStatus(4);
                // Gera XML Requerimento
                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);

                    if (_xmlRequerimento == "" || _xmlRequerimento == null || _xmlRequerimento.Length < 10)
                    {
                        if (MsgErro != "")
                        {
                            MensagemErro = MsgErro;
                        }
                        else
                        {
                            MensagemErro = "Informe ao suporte tecnico que o xml não foi gerado! " + _xmlRequerimento.Length.ToString();
                        }
                    }
                    else
                    {
                        try
                        {
                            retorno = dHelperORACLE.AtualizaXML(_ProtocoloRCPJ, _xmlRequerimento);
                            if (retorno == "")
                            {
                                ok = true;
                                MensagemErro = "";
                            }
                            else
                            {
                                MensagemErro = retorno;
                                ok = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MensagemErro = ex.Message;
                            ok = false;
                        }

                    }

                    if (!ok)
                    {
                        //p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        //p.t005_nr_protocolo = _ProtocoloRequerimento;
                        //p.DeleteStatus(4);

                        UpdateStatus(ultimostatus);
                    }
                }
                return ok;
            }
            catch (Exception ex)
            {
                dT005_Protocolo p = new dT005_Protocolo();
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.DeleteStatus(4);
                //Atualiza status do requerimento para Deferido (4)
                UpdateStatus(ultimostatus);
                MensagemErro = " ao tentar deferir o requerimento.";
                string aa = ex.Message;
                return false;
            }


        }

        public bool DeferirJucerja()
        {
            string MsgErro = null;
            bool ok = true;
            int ultimostatus;
            ultimostatus = GetStatusProtocolo();

            try
            {
                //Atualiza status do requerimento para Deferido (4)

                UpdateStatus(4);
                // Gera XML Requerimento
                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    _xmlRequerimento = p.geraXmlDoRequerimento(_ProtocoloRCPJ, _ProtocoloRequerimento, ref MsgErro);

                    if (_xmlRequerimento == "" || _xmlRequerimento == null || MsgErro != "")
                    {
                        if (MsgErro != "")
                        {
                            MensagemErro = "Erro ao tentar deferir o requerimento : " + MsgErro;
                        }
                        else
                        {
                            MensagemErro = "Informe ao suporte tecnico que o xml não foi gerado! " + MsgErro;
                        }
                        ok = false;
                    }
                    else
                    {
                        try
                        {
                            dHelperSQL.GravaRequerimentoJUCERJA(_ProtocoloRCPJ, _ProtocoloRequerimento, _ProtocoloViabilidade, _xmlRequerimento, _UsuarioRegin);
                        }
                        catch (Exception ex)
                        {
                            MensagemErro = ex.Message;
                            ok = false;
                        }

                    }

                    if (!ok)
                    {
                        p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        p.t005_nr_protocolo = _ProtocoloRequerimento;
                        p.DeleteStatus(4);
                        //Atualiza status do requerimento para Deferido (4)
                        UpdateStatus(ultimostatus);
                    }
                }
                return ok;
            }
            catch (Exception ex)
            {
                dT005_Protocolo p = new dT005_Protocolo();
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.DeleteStatus(4);
                //Atualiza status do requerimento para Deferido (4)
                UpdateStatus(ultimostatus);
                MensagemErro = " ao tentar deferir o requerimento.";
                string aa = ex.Message;
                return false;
            }


        }

        public void GravarExigencias()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.ExcluiExigencias(_ProtocoloRequerimento);
                foreach (bExigencias tmpExigencia in _Exigencias)
                {
                    p.GravarExigencias(_UsuarioRegin, tmpExigencia.CodExigencia, tmpExigencia.Descricao, tmpExigencia.ProtocoloRequerimento);
                }
            }

            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.ExcluiExigenciasOutrasTudo(_ProtocoloRequerimento);
                foreach (bExigencias tmpExigencia in _ExigenciasOutras)
                {
                    p.GravarExigenciasOutras(_CNPJ_Orgao_Registro, _UsuarioRegin, _ProtocoloRequerimento, tmpExigencia.Descricao, tmpExigencia.FundamentoLegal, tmpExigencia.Grupo);
                }
            }

            string stipoOR = ConfigurationManager.AppSettings["TipoOrgaoRegistro"].ToString();
            if (stipoOR == "JUCERJA")
            {

                using (dT005_Protocolo p = new dT005_Protocolo())
                {
                    string Texto = "";
                    string TextoOBS = "";
                    string TextoQSA = "";
                    string tit1 = "Dados da Empresa\n\r";
                    String tit2 = "QSA\n\r";
                    string tit3 = "Filial\n\r";
                    String tit4 = "Objeto Social\n\r";
                    //string tit5 = "Administradores\n\r";
                    String tit6 = "Foro\n\r";
                    string tit7 = "Clausulas Adicionais\n\r";
                    String titobs = "Outras Exigências\n\r";

                    p.ExcluiExigenciaOBSJucerja(_ProtocoloRequerimento);

                    foreach (bExigencias tmpAlertas in _ExigenciasOutras)
                    {
                        switch (tmpAlertas.Grupo)
                        {
                            case "1": // Dados da empresa
                                Texto += tit1;
                                Texto += tmpAlertas.Descricao;
                                Texto += "\n\r";
                                tit1 = "";
                                break;
                            case "2": // QSA
                                TextoQSA += tit2;
                                TextoQSA += tmpAlertas.Descricao;
                                TextoQSA += "\n\r";
                                tit2 = "";
                                break;
                            case "3": // Filial
                                Texto += tit3;
                                Texto += tmpAlertas.Descricao;
                                Texto += "\n\r";
                                tit3 = "";
                                break;
                            case "4": // Obj Social
                                Texto += tit4;
                                Texto += tmpAlertas.Descricao;
                                Texto += "\n\r";
                                tit4 = "";
                                break;
                            case "5": // Administradores
                                TextoQSA += tit2;
                                TextoQSA += tmpAlertas.Descricao;
                                TextoQSA += "\n\r";
                                tit2 = "";
                                break;
                            case "6": // Foro
                                Texto += tit6;
                                Texto += tmpAlertas.Descricao;
                                Texto += "\n\r";
                                tit6 = "";
                                break;
                            case "7": // Clausulas Adicionais
                                Texto += tit7;
                                Texto += tmpAlertas.Descricao;
                                Texto += "\n\r";
                                tit7 = "";
                                break;
                            default:
                                TextoOBS += titobs;
                                TextoOBS += tmpAlertas.Descricao;
                                TextoOBS += "\n\r";
                                titobs = "";
                                break;
                        }


                    }
                    Texto += TextoQSA;
                    Texto += TextoOBS;
                    // texto concatenado, gravar na base
                    //string a = "";
                    p.GravarExigencias(_UsuarioRegin, "9999", Texto, _ProtocoloRequerimento);
                }


            }

        }

        public void GravarConfirmacoes()
        {
            using (dT017_Protocolo_Confirmacao p = new dT017_Protocolo_Confirmacao())
            {

                //Verificar se o andamento atual é o mesmo da ultima confirmação
                //se for igual atualizo na mesma sequancia
                //se for diferente crio uma nova sequencia

                int _novaSequencia = 0;
                bool _criaNovaSequencia = false;

                bAndamento.getUltimaSequenciaConfirmacao();

                if (bAndamento.SecaoUltimaConfirmacao != "" && (bAndamento.SequenciaUltimaConfirmacao != bAndamento.SequenciaAndamento))
                {
                    _novaSequencia = p.BuscaSequencia(_ProtocoloRequerimento);
                    _criaNovaSequencia = true;
                }
                //if (bAndamento.SecaoUltimaConfirmacao != "" && (bAndamento.SecaoUltimaConfirmacao != bAndamento.SeccaoDestino
                //    && bAndamento.SequenciaUltimaConfirmacao != bAndamento.SequenciaAndamento))
                //{
                //    _novaSequencia = p.BuscaSequencia(_ProtocoloRequerimento);
                //    _criaNovaSequencia = true;
                //}



                foreach (bProtocoloConfirmacao pc in _Confirmacoes)
                {
                    if (_criaNovaSequencia)
                    {
                        pc.T017_sequencia = _novaSequencia;
                    }
                    pc.T017_usuario = _UsuarioRegin;
                    pc.Update();

                }
            }
        }

        public void GravarForo()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.T005_foro = _Foro;
                p.t005_Local_Assinatura = _Local_Assinatura;
                p.t005_Data_Assinatura = _Data_Assinatura;
                p.GravarForo();
            }
        }
        public void GravarCalsulaAdm()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.T005_in_clausila_adm = _t005_in_clausila_adm;
                p.T005_tx_clausula_adm = _t005_tx_clausula_adm;
                p.GravarClausulaAdm();
            }
        }
        public void GravarLocalEntrega()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.T005_local_entrega = _localEntregaprocesso;
                p.T005_co_unidade_entrega = _codUnidadeEntrega;
                p.GravarLocalEntrea();
            }
        }
        public void GravaOutrosEventos()
        {
            using (dT003_Pessoa_Juridica p = new dT003_Pessoa_Juridica())
            {
                p.GravaOutrosEventos(_CodigoEmpresa, _eventoConsolidacao, _eventoReativacao, _eventoReRatificacao);
            }
        }
        public void GravaValidacaoProtocolo(string pSituacao)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                p.GravaValidacaoProtocolo(pSituacao);

            }
        }

        public void GravardataAssinaturaAbervacaoContrato(DateTime pData)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                p.t005_dt_averbacao = pData;
                p.GravardataAssinaturaAbervacaoContrato();
            }
            _Data_Averbacao = pData;
        }

      
        public bool IsStatusFinalizadoDEFERIDO()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(4);
            }
        }

        public bool IsStatusFinalizadoINDEFERIDO()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(8);
            }
        }
        public bool IsStatusFinalizadoINDEFERIDOVIABILIDADE()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(5);
            }
        }

        public bool IsStatusFinalizadoCANCELADO()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(9);
            }
        }

        public bool IsStatusCancelado()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(9);
            }
        }

        public bool IsStatusExigencias()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(3);
            }
        }

        public bool IsStatusAposInicio()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatusAposInicio();
            }
        }

        public bool IsStatusRequerentePendente()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(0);
            }
        }

        public int GetStatusProtocolo()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.GetStatusProtocolo();
            }
        }

        public string GetNumProtocoloORbyReq(string tmpNumReq)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.GetNumProtocoloORbyReq(tmpNumReq);
            }
        }



        #endregion

        #region Validacao
        private Boolean VerificaIdade(DateTime wData)
        {
            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 18)
                return true;
            else
                return false;
        }

        public bool ValidaDadosGerais()
        {


            Boolean wChave = true;
            Boolean wChaveAdm = false;
            if (_TipoPessoaJuridicaCodigo == 1264)
            {
                bSocios socioAux = new bSocios();
                try
                {
                    if (!ValidaSocios())
                    {
                        return false;
                    }

                    //Validando os dados do objetos e solicitando o preenchimento no formulário.

                    Decimal TotalCotas = 0;
                    if (_TipoPessoaJuridicaCodigo == 1264)
                    {
                        foreach (bSocios socioAux1 in Socios)
                        {
                            TotalCotas = TotalCotas + socioAux1.QuotaCapitalSocial;
                        }
                    }
                    else
                    {
                        _QtdCotas = 0;
                    }

                    foreach (bSocios socioAux2 in Socios)
                    {
                        if (socioAux2.rep_legal != 0)
                            wChaveAdm = true;
                    }
                    if (TotalCotas != _QtdCotas)
                    {
                        _listErro.Add("A soma das Quotas do Sócio não batem com o Capital Social da Empresa.");
                        wChave = false;
                    }
                    //throw new Exception("A soma das Quotas do Sócio não batem com o Capital Social da Empresa.");
                    else
                    {
                        if (!wChaveAdm)
                        {
                            _listErro.Add("Escolha um ou mais Representantes da empresa");
                            wChave = false;
                        }
                        else
                        {
                            _TipoGravacao = "Socio";

                        }
                    }

                    if (wChave)
                        return wChave;
                    //}
                }
                catch (Exception exception)
                {
                    string _err = exception.Message;
                    return false;
                }

            }
            else
            {
                bSocios fdAux = new bSocios();
                try
                {
                    //Validando os dados do objetos e solicitando o preenchimento no formulário.

                    if (!ValidaDiretores())
                        throw new Exception("Para passa a proxima etapa é necessário completar o cadastro");

                    _TipoGravacao = "Diretor";

                    if (CarregaInformacoesAdicionais())
                    {
                    }

                }

                catch (Exception exception)
                {
                    string _err = exception.Message;
                    return false;
                }
            }

            return wChave;

        }

        public bool ValidaSocios()
        {


            bool socioOk = true;
            int indSocio = 0;
            foreach (bSocios socios in _Socios)
            {
                Boolean wMenor = false;
                if (socios.Qualificacao == String.Empty || socios.Qualificacao == null)
                {
                    _listErro.Add("Sócio " + socios.Nome + " sem qualificação");
                    //ErrorSummary.AddErrorMessage("Informe a Qualificação do sócio.");
                    socioOk = false;
                }
                int wAux1 = Convert.ToInt32(socios.Qualificacao);

                if (wAux1 == 27 || wAux1 == 29 || wAux1 == 30 || wAux1 == 37 || wAux1 == 38 || (wAux1 >= 55 && wAux1 <= 58))
                {
                    if (socios.Representantes.Count == 0)
                    {
                        socioOk = false;
                        _listErro.Add("Sócio " + socios.Nome + " deve ter um representante.");
                    }
                }
                if (socios.Nome == String.Empty || socios.Nome == null)
                {
                    _listErro.Add("Informe o Nome do sócio.");
                    socioOk = false;
                }
                if (socios.CPFCNPJ == String.Empty || socios.CPFCNPJ == null)
                {
                    _listErro.Add("Sócio " + socios.Nome + " sem CPF.");
                    socioOk = false;
                }
                if (socios.CPFCNPJ.Length != 14)
                {
                    if (socios.RG == String.Empty || socios.RG == null)
                    {
                        _listErro.Add("Informe o Número do Documento do sócio " + socios.Nome);
                        socioOk = false;
                    }
                    if (_TipoPessoaJuridicaCodigo == 1264)
                    {
                        if (socios.DataNascimento == DateTime.MinValue || socios.DataNascimento.ToString() == String.Empty
                            || socios.DataNascimento == null)
                        {
                            _listErro.Add("Informe a Data de Nascimento do sócio " + socios.Nome);
                            socioOk = false;
                        }
                        if (socios.DataNascimento != null && socios.DataNascimento != DateTime.MinValue)
                        {
                            wMenor = VerificaIdade(Convert.ToDateTime(socios.DataNascimento));
                            if (wMenor)
                            {
                                decimal wAux;
                                TimeSpan diff;
                                decimal DiasAno = 365;
                                diff = DateTime.Now.Subtract(Convert.ToDateTime(socios.DataNascimento));
                                //wAux = (diff.Days / 365);
                                wAux = diff.Days;
                                wAux = (wAux / DiasAno);
                                if (wAux > 16)
                                {
                                    if (socios.TipoIdentidade == String.Empty || socios.TipoIdentidade == null)
                                    {
                                        _listErro.Add("Informe o tipo de documento de identidade do sócio " + socios.Nome);
                                        socioOk = false;
                                    }
                                    if (socios.RG == String.Empty || socios.RG == null)
                                    {
                                        _listErro.Add("Informe o número de documento de identidade do sócio " + socios.Nome);
                                        socioOk = false;
                                    }
                                    if (socios.OrgaoExpedidor == String.Empty || socios.OrgaoExpedidor == null)
                                    {
                                        _listErro.Add("Informe o orgão expedidor do documento do sócio " + socios.Nome);
                                        socioOk = false;
                                    }
                                    //if (socios.OrgaoExpedidorUF == String.Empty || socios.OrgaoExpedidorUF == null)
                                    //{
                                    //    _listErro.Add("Informe a UF do orgão expedidor do documento do sócio " + socios.Nome);
                                    //    socioOk = false;
                                    //}
                                }
                            }
                        }
                        if (socios.NacionalidadeCodigo == 0)
                        {
                            _listErro.Add("Informe a Nacionalidade do sócio " + socios.Nome);
                            socioOk = false;
                        }
                        if ((socios.in_Sexo == string.Empty || socios.in_Sexo == null) && socios.CPFCNPJ.Length == 11)
                        {
                            _listErro.Add("Informe o sexo do sócio " + socios.Nome);
                            socioOk = false;
                        }
                        if (socios.EstadoCivil == "493" || socios.EstadoCivil == null)

                            if (socios.EstadoCivilRegime == String.Empty || socios.EstadoCivilRegime == null)
                            {
                                _listErro.Add("Informe o Regime de Bens do sócio " + socios.Nome);
                                socioOk = false;
                            }

                        if (wMenor)
                        {
                            if (socios.TipoEmancipado == "0" || socios.TipoEmancipado == null)
                            {
                                if (socios.Representantes.Count <= 0)
                                {
                                    _listErro.Add("Informe o(s) Representante(s) do sócio do sócio " + socios.Nome);
                                    socioOk = false;
                                    //throw new Exception("É necessário informar o representante legal do sócio");
                                }
                            }

                        }
                    }
                    if (socios.EstadoCivil == String.Empty || socios.EstadoCivil == null)
                    {
                        _listErro.Add("Informe o Estado Civil do sócio " + socios.Nome);
                        socioOk = false;
                    }

                    if (socios.Profissao_Descricao == String.Empty || socios.Profissao_Descricao == null)
                    {
                        _listErro.Add("Informe a Profissão do sócio " + socios.Nome);
                        socioOk = false;
                    }
                }

                #region Endereco
                if (socios.EndCEP == String.Empty || socios.EndCEP == null)
                {
                    _listErro.Add(socios.Nome + " - Informe o CEP da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if (socios.EndUF == String.Empty || socios.EndUF == null)
                {
                    _listErro.Add("Informe a UF da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if ((socios.EndMunicipio == String.Empty || socios.EndMunicipio == null) && socios.EndPais == "154")
                {
                    _listErro.Add("Informe o Município da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if (socios.EndBairro == String.Empty || socios.EndBairro == null)
                {
                    _listErro.Add("Informe o Bairro da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if ((socios.EndTipoLogradouro == String.Empty || socios.EndTipoLogradouro == null) && socios.EndPais == "154")
                {
                    _listErro.Add("Informe o Tipo de Logradouro da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if (socios.EndLogradouro == String.Empty || socios.EndLogradouro == null)
                {
                    _listErro.Add("Informe o Logradouro da residência do sócio " + socios.Nome);
                    socioOk = false;
                }
                if ((socios.EndNumero == String.Empty || socios.EndNumero == null) && socios.EndPais == "154")
                {
                    if (socios.EndComplemento == string.Empty)
                    {    //ErrorSummary.AddErrorMessage("Informe o Número da residência do sócio.");
                        socioOk = false;
                        _listErro.Add("Informe o Número ou o complemento da residência do sócio " + socios.Nome);
                    }
                }
                #endregion

                socios.Valido = socioOk;

                indSocio++;
            }

            return socioOk;

        }

        public bool ValidaRepresentante(bSocios s)
        {
            bool ret = true;

            if (String.IsNullOrEmpty(s.Nome))
                ret = false;

            if (String.IsNullOrEmpty(s.CPFCNPJ))
                ret = false;

            if (String.IsNullOrEmpty(s.TipoIdentidade))
                ret = false;

            if (String.IsNullOrEmpty(s.RG))
                ret = false;

            if (s.NacionalidadeCodigo == 0)
                ret = false;

            if (String.IsNullOrEmpty(s.TipoAssistido))
                ret = false;

            if (String.IsNullOrEmpty(s.EndCEP))
                ret = false;


            return ret;
        }

        private Boolean ValidaDiretores()
        {

            bool diretorOk = true;
            int wContaPresidente = 0;
            //Alteração 25012013
            foreach (bSocios d in _FundadorDiretor)
            {

                if (d.Qualificacao != String.Empty && d.Qualificacao != null)
                {
                    if (d.Qualificacao == "16")
                        wContaPresidente++;
                    if (d.Qualificacao_Descricao.ToUpper() != "FUNDADOR" &&
                        d.Qualificacao_Descricao.ToUpper() != "EMPRESÁRIO" &&
                        d.Qualificacao_Descricao.ToUpper() != "TITULAR" &&
                        d.Qualificacao_Descricao.ToUpper() != "ADMINISTRADOR")
                    {
                        if (d.DataInicioMandato.ToString() == String.Empty || d.DataInicioMandato == null)
                        {
                            diretorOk = false;
                        }
                        if (d.DataTerminoMandato.ToString() == String.Empty || d.DataTerminoMandato == null)
                        {
                            diretorOk = false;
                        }
                    }
                }

                if (d.Nome == String.Empty || d.Nome == null)
                {

                    diretorOk = false;
                }

                if (d.CPFCNPJ == String.Empty || d.CPFCNPJ == null)
                {
                    diretorOk = false;
                }
                if (d.CPFCNPJ.Length != 14)
                {
                    if (d.TipoIdentidade == String.Empty || d.TipoIdentidade == null)
                        diretorOk = false;

                    if (d.RG == String.Empty || d.RG == null)
                    {
                        diretorOk = false;
                    }

                    if (d.Nacionalidade == String.Empty || d.Nacionalidade == null)
                    {
                        diretorOk = false;
                    }

                    if (d.Profissao_Descricao == String.Empty || d.Profissao_Descricao == null)
                    {
                        diretorOk = false;
                    }
                }
                else
                    if (d.Representantes.Count == 0)
                    {
                        _listErro.Add(d.Nome + " deve ter um representante.");
                        diretorOk = false;
                    }
                if (d.EndCEP == String.Empty || d.EndCEP == null)
                {
                    diretorOk = false;
                }
                if (d.EndUF == String.Empty || d.EndUF == null)
                {
                    diretorOk = false;
                }
                if (d.EndMunicipio == String.Empty || d.EndMunicipio == null)
                {
                    diretorOk = false;
                }
                if (d.EndBairro == String.Empty || d.EndBairro == null)
                {
                    diretorOk = false;
                }
                if (d.EndTipoLogradouro == String.Empty || d.EndTipoLogradouro == null)
                {
                    diretorOk = false;
                }
                if (d.EndLogradouro == String.Empty || d.EndLogradouro == null)
                {
                    diretorOk = false;
                }
                if (d.EndNumero == String.Empty || d.EndNumero == null)
                {
                    diretorOk = false;
                }

                d.Valido = true;

                if (!diretorOk)
                {
                    _listErro.Add("Completar o cadastro do sócio " + d.Nome);
                    return false;
                }
            }

            if (wContaPresidente > 1)
            {
                _listErro.Add("Não é possível registrar mais de um Presidente.");
                diretorOk = false;
            }

            return diretorOk;


        }

        public void AdicionaFilial(bFilial fi)
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                int SqFilial = 0;
                using (dT001_Pessoa fp = new dT001_Pessoa())
                {
                    fp.MainConnectionProvider = cp;
                    //using (dCorrelativo c = new dCorrelativo())
                    //{
                    //    c.Tipo = Int32.Parse(CONST_PESSOA_CORRELATIVO);
                    //    SqEmpresa = c.GetCorrelativo();
                    //}
                    //p.t001_sq_pessoa = decimal.Parse(SqEmpresa);
                    fp.t001_in_tipo_pessoa = CONST_PESSOA_JURIDICA;
                    fp.t001_ds_pessoa = "Filial " + SedeNome;
                    fp.t001_in_dados_atualizados = "S";
                    fp.t001_dt_ult_atualizacao = DateTime.Now;
                    fp.t001_email = "";
                    fp.t001_tel_1 = "";
                    fp.t001_tel_2 = "";
                    fp.t001_sq_pessoa = fi.SqFilial;
                    SqFilial = fp.Update();
                    //_CodigoEmpresa = SqEmpresa;
                }
                using (dT003_Pessoa_Juridica fpj = new dT003_Pessoa_Juridica())
                {
                    fpj.MainConnectionProvider = cp;
                    fpj.t001_sq_pessoa = SqFilial;
                    fpj.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
                    fpj.t003_DBE = fi.FilialDBE;
                    fpj.t003_prot_viab = fi.FilialViabilidade;

                    fpj.Update();
                }

                //3.Gravando Vinculo Endereço
                //(dados do endereço da Empresa)
                using (dR002_Vinculo_Endereco fve = new dR002_Vinculo_Endereco())
                {
                    int SqVinculoEnderecoFilial;
                    fve.MainConnectionProvider = cp;

                    fve.t001_sq_pessoa = SqFilial;
                    fve.t001_sq_pessoa_pai = _CodigoEmpresa;//SqEmpresa;
                    //_t001_sq_pessoa_pai = ;
                    fve.a015_co_tipo_logradouro = decimal.Parse(AchaTipoLogradouro(fi.FilialTipoLogradouro));
                    fve.a015_ds_tipo_logradouro = fi.FilialTipoLogradouro;
                    fve.r002_ds_logradouro = fi.FilialLogradouro;
                    fve.r002_nr_logradouro = fi.FilialNumero;
                    fve.r002_ds_complemento = fi.FilialComplemento;
                    fve.r002_ds_bairro = fi.FilialBairro;
                    fve.a004_co_pais = 154;
                    fve.a005_co_municipio = fi.FilialCodMunicipio;
                    //_a004_co_pais = ;
                    fve.r002_nr_cep = fi.FilialCEP;
                    //_r002_ds_referencia = _se;
                    SqVinculoEnderecoFilial = fve.Update();
                }
                //Gravando Vinculo com a PJ (Vinculo Requerente)
                using (dR001_Vinculo fv = new dR001_Vinculo())
                {
                    fv.MainConnectionProvider = cp;
                    fv.t001_sq_pessoa = SqFilial;
                    fv.t001_sq_pessoa_pai = _CodigoEmpresa;
                    fv.a009_co_condicao = 501; // 2066 = Requerente
                    fv.r001_dt_entrada_vinculo = DateTime.Now;

                    fv.r001_ds_cargo_direcao = "FILIAL";
                    fv.r001_in_situacao = "A";

                    fv.Update();
                }

                //8.Gravando CNAE
                //(Codigo de Atividade Economica do Requerimento)

                using (dR004_Atuacao fa = new dR004_Atuacao())
                {
                    fa.MainConnectionProvider = cp;
                    foreach (bCNAE fc in _CNAEs)
                    {
                        fa.t001_sq_pessoa = SqFilial;
                        fa.a001_co_atividade = fc.CodigoCNAE;
                        fa.r004_in_principal_secundario = fc.TipoAtividade.ToString();
                        fa.Update();
                    }
                }
                cp.CommitTransaction();
                cp.Dispose();
            }

        }

        public void GravaRepresentante(bSocios r, string SqSocio)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();

                string SqRepresentante = r.SQPessoa;

                if (r.SQPessoa == null)
                {
                    r.SQPessoa = "0";
                }

                #region Gravando em Pessoa
                using (dT001_Pessoa pr = new dT001_Pessoa())
                {
                    pr.MainConnectionProvider = cv;

                    pr.t001_sq_pessoa = Convert.ToInt32(r.SQPessoa);
                    pr.t001_in_tipo_pessoa = "F";//r.TipoPessoa;
                    pr.t001_ds_pessoa = r.Nome;
                    pr.t001_in_dados_atualizados = "S";
                    pr.t001_dt_ult_atualizacao = DateTime.Now;
                    pr.t001_ddd = r.DDD;
                    pr.t001_tel_1 = r.Telefone;
                    pr.t001_email = r.Email;

                    SqRepresentante = pr.Update().ToString();
                    r.SQPessoa = SqRepresentante;
                }
                #endregion

                #region Gravando Pessoa Fisica
                if (r.TipoPessoa == "F")
                {
                    //14.Gravando Representantes de Socio - pessoa fisica
                    //(Dados dos Representantes de Socio do requerimento)
                    using (dT002_Pessoa_Fisica pfr = new dT002_Pessoa_Fisica())
                    {
                        pfr.MainConnectionProvider = cv;

                        pfr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                        pfr.t002_nr_cpf = r.CPFCNPJ;
                        if (r.CPFCNPJ.Length != 14)
                        {
                            if (!string.IsNullOrEmpty(r.TipoIdentidade))
                            {
                                pfr.a010_co_tipo_documento = decimal.Parse(r.TipoIdentidade);
                            }

                            pfr.t002_nr_documento = r.RG;
                            pfr.t002_ds_emissor_documento = r.OrgaoExpedidor;
                            pfr.a004_uf_org_exped = r.OrgaoExpedidorUF;
                            pfr.t002_ds_orgao_expedidor = r.OrgaoExpedidorNome;
                            pfr.a004_co_pais = r.NacionalidadeCodigo;

                            // pfr.t002_ds_nacionalidade = r.Nacionalidade;

                            if (!string.IsNullOrEmpty(r.NacionalidadeCodigo.ToString()))
                            {
                                pfr.a004_co_pais = r.NacionalidadeCodigo;
                                pfr.t002_ds_nacionalidade = r.Nacionalidade;
                                if (pfr.a004_co_pais != 154)
                                {
                                    pfr.t002_tipo_visto = r.tipo_visto;
                                    pfr.t002_emissao_visto = r.emissao_visto;
                                    pfr.t002_dt_validade_visto = r.validade_visto;
                                }
                                else
                                {
                                    pfr.t002_tipo_visto = "";
                                    pfr.t002_emissao_visto = null;
                                    pfr.t002_dt_validade_visto = null;
                                }
                            }

                            pfr.a004_co_uf_naturalidade = r.NaturalidadeCodigo;
                            if (!string.IsNullOrEmpty(r.EstadoCivil))
                                pfr.a012_co_estado_civil = decimal.Parse(r.EstadoCivil);
                            if (r.EstadoCivilRegime != null && r.EstadoCivilRegime != string.Empty)
                            {
                                pfr.a013_co_regime_bens = decimal.Parse(r.EstadoCivilRegime);
                            }
                            pfr.t002_dt_nascimento = r.DataNascimento;
                            pfr.a014_co_emancipacao = r.TipoEmancipado == "" ? 0 : Convert.ToDecimal(r.TipoEmancipado);

                            if (r.Profissao_Descricao != null && r.Profissao_Descricao != string.Empty)
                                pfr.t002_ds_profissao = r.Profissao_Descricao;
                            pfr.t002_in_sexo = r.in_Sexo;
                            pfr.t002_analfabeto = r.Analfabeto;

                        }
                        pfr.Update();
                    }
                }
                #endregion

                #region Gravando Representante de Socio - Endereço
                using (dR002_Vinculo_Endereco esr = new dR002_Vinculo_Endereco())
                {
                    int SqVinculoEnderecoRepresentante;
                    esr.MainConnectionProvider = cv;
                    esr.t001_sq_pessoa = int.Parse(SqRepresentante);
                    esr.t001_sq_pessoa_pai = int.Parse(SqSocio);
                    if (r.EndTipoLogradouro != null)
                    {
                        esr.a015_co_tipo_logradouro = decimal.Parse(r.EndTipoLogradouro);
                        esr.a015_ds_tipo_logradouro = r.EndDsTipoLogradouro;
                    }
                    esr.r002_ds_logradouro = r.EndLogradouro;
                    esr.r002_nr_logradouro = r.EndNumero;
                    esr.r002_ds_complemento = r.EndComplemento;
                    esr.r002_ds_bairro = r.EndBairro;
                    if (!string.IsNullOrEmpty(r.EndMunicipio))
                        esr.a005_co_municipio = decimal.Parse(r.EndMunicipio);//;

                    esr.a004_co_pais = 154;
                    esr.R002_uf = r.EndUF;
                    esr.r002_nr_cep = r.EndCEP;
                    SqVinculoEnderecoRepresentante = esr.Update();
                }
                #endregion

                #region Gravando Vinculo do Representante do Sócio com a PJ (Vinculo Sócio)
                using (dR001_Vinculo vr = new dR001_Vinculo())
                {
                    vr.MainConnectionProvider = cv;
                    vr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                    vr.t001_sq_pessoa_pai = decimal.Parse(SqSocio);
                    vr.a009_co_condicao = 504; // decimal.Parse(r.Qualificacao);
                    if (r.DataInicioMandato == null)
                    {
                        vr.r001_dt_inicio_mandato = DateTime.Now;
                    }
                    else
                    {
                        vr.r001_dt_inicio_mandato = r.DataInicioMandato;
                    }
                    vr.r001_dt_entrada_vinculo = DateTime.Now;
                    //vr.r001_dt_saida_vinculo = DateTime.MinValue;

                    vr.r001_ds_cargo_direcao = "REPRESENTANTE";
                    vr.r001_in_situacao = "A";
                    if (r.TipoAssistido == null)
                    {
                        r.TipoAssistido = "309";
                    }
                    vr.a030_co_tipo_assistido = decimal.Parse(r.TipoAssistido);
                    vr.r001_acao = r.tipoacao;
                    vr.t001_sq_pessoa_rep_legal = 0; // r.rep_legal;
                    vr.T001_cpf_cnpj_pessoa = r.CPFCNPJ;
                    vr.Update();

                    //Gravando Administrador
                    //Verficar se o representante não existe como adminstrador da empresa
                    //se não existrir incluir um vinculo de adminstrador
                    //bool _existeAdmin = false;
                    //foreach (bSocios admin in _Socios)
                    //{
                    //    if (r.CPFCNPJ == admin.CPFCNPJ && admin.Qualificacao == "5")
                    //    {
                    //        _existeAdmin = true;
                    //    }
                    //}

                    //if (r.rep_legal != 0 && _existeAdmin == false)
                    //{
                    //    vr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                    //    vr.t001_sq_pessoa_pai = decimal.Parse(_CodigoEmpresa.ToString());
                    //    if (r.Qualificacao == "22")
                    //    {
                    //        vr.a009_co_condicao = 22; // decimal.Parse(r.Qualificacao);
                    //        vr.r001_ds_cargo_direcao = "SÓCIO"; // r.Qualificacao_Descricao;
                    //    }
                    //    else
                    //    {
                    //        vr.a009_co_condicao = 5; // decimal.Parse(r.Qualificacao);
                    //        vr.r001_ds_cargo_direcao = "ADMINISTRADOR"; // r.Qualificacao_Descricao;
                    //    }

                    //    vr.r001_dt_entrada_vinculo = DateTime.Now;
                    //    vr.r001_in_situacao = "A";
                    //    vr.a030_co_tipo_assistido = decimal.Parse(r.TipoAssistido);
                    //    vr.t001_sq_pessoa_rep_legal = 1; // r.rep_legal;
                    //    vr.Update();
                    //}
                }
                #endregion

                cv.CommitTransaction();
            }
        }



        public void GravaRepresentante(bSocios r, string SqSocio, ConnectionProvider Cv)
        {
            string SqRepresentante = r.SQPessoa;
            //Gravando Representantes de Sócios em Pessoa
            if (r.SQPessoa == null)
            {
                r.SQPessoa = "0";
            }
            using (dT001_Pessoa pr = new dT001_Pessoa())
            {
                pr.MainConnectionProvider = Cv;
                pr.t001_sq_pessoa = Convert.ToInt32(r.SQPessoa);
                pr.t001_in_tipo_pessoa = "F";//r.TipoPessoa;
                pr.t001_ds_pessoa = r.Nome;
                pr.t001_in_dados_atualizados = "S";
                pr.t001_dt_ult_atualizacao = DateTime.Now;
                pr.t001_ddd = r.DDD;
                pr.t001_tel_1 = r.Telefone;
                pr.t001_email = r.Email;
                SqRepresentante = pr.Update().ToString();
                r.SQPessoa = SqRepresentante;

                if (r.TipoPessoa == "F")
                {
                    //14.Gravando Representantes de Socio - pessoa fisica
                    //(Dados dos Representantes de Socio do requerimento)
                    using (dT002_Pessoa_Fisica pfr = new dT002_Pessoa_Fisica())
                    {
                        pfr.MainConnectionProvider = Cv;

                        pfr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                        pfr.t002_nr_cpf = r.CPFCNPJ;
                        if (r.CPFCNPJ.Length != 14)
                        {
                            if (!string.IsNullOrEmpty(r.TipoIdentidade))
                                pfr.a010_co_tipo_documento = decimal.Parse(r.TipoIdentidade);
                            pfr.t002_nr_documento = r.RG;
                            pfr.t002_ds_emissor_documento = r.OrgaoExpedidor;
                            pfr.a004_uf_org_exped = r.OrgaoExpedidorUF;
                            pfr.t002_ds_orgao_expedidor = r.OrgaoExpedidorNome;
                            pfr.a004_co_pais = r.NacionalidadeCodigo;
                            // pfr.t002_ds_nacionalidade = r.Nacionalidade;

                            if (!string.IsNullOrEmpty(r.NacionalidadeCodigo.ToString()))
                            {
                                pfr.a004_co_pais = r.NacionalidadeCodigo;
                                pfr.t002_ds_nacionalidade = r.Nacionalidade;
                                if (pfr.a004_co_pais != 154)
                                {

                                    pfr.t002_tipo_visto = r.tipo_visto;
                                    pfr.t002_emissao_visto = r.emissao_visto;
                                    pfr.t002_dt_validade_visto = r.validade_visto;
                                }
                                else
                                {
                                    pfr.t002_tipo_visto = "";
                                    pfr.t002_emissao_visto = null;
                                    pfr.t002_dt_validade_visto = null;
                                }
                            }

                            pfr.a004_co_uf_naturalidade = r.NaturalidadeCodigo;
                            if (!string.IsNullOrEmpty(r.EstadoCivil))
                                pfr.a012_co_estado_civil = decimal.Parse(r.EstadoCivil);
                            if (r.EstadoCivilRegime != null && r.EstadoCivilRegime != string.Empty)
                            {
                                pfr.a013_co_regime_bens = decimal.Parse(r.EstadoCivilRegime);
                            }
                            pfr.t002_dt_nascimento = r.DataNascimento;
                            pfr.a014_co_emancipacao = Convert.ToDecimal(r.TipoEmancipado);
                            //if (r.Profissao != null)
                            //    pfr.a020_co_profissao = decimal.Parse(r.Profissao);
                            if (r.Profissao_Descricao != null && r.Profissao_Descricao != string.Empty)
                                pfr.t002_ds_profissao = r.Profissao_Descricao;
                            pfr.t002_in_sexo = r.in_Sexo;
                            pfr.t002_analfabeto = r.Analfabeto;

                        }

                        pfr.Update();
                    }
                }

                //15. Gravando Representante de Socio - Endereço
                //(dados dos endereços dos representantes do sócio)

                using (dR002_Vinculo_Endereco esr = new dR002_Vinculo_Endereco())
                {
                    int SqVinculoEnderecoRepresentante;
                    esr.MainConnectionProvider = Cv;
                    esr.t001_sq_pessoa = int.Parse(SqRepresentante);
                    esr.t001_sq_pessoa_pai = int.Parse(SqSocio);
                    if (r.EndTipoLogradouro == "")
                    {
                        r.EndTipoLogradouro = "0";
                    }
                    if (r.EndTipoLogradouro != null)
                    {
                        esr.a015_co_tipo_logradouro = decimal.Parse(r.EndTipoLogradouro);
                        esr.a015_ds_tipo_logradouro = r.EndDsTipoLogradouro;
                    }
                    esr.r002_ds_logradouro = r.EndLogradouro;
                    esr.r002_nr_logradouro = r.EndNumero;
                    esr.r002_ds_complemento = r.EndComplemento;
                    esr.r002_ds_bairro = r.EndBairro;
                    if (!string.IsNullOrEmpty(r.EndMunicipio))
                        esr.a005_co_municipio = decimal.Parse(r.EndMunicipio);//;

                    esr.a004_co_pais = 154;
                    esr.r002_nr_cep = r.EndCEP;
                    esr.R002_uf = r.EndUF;
                    SqVinculoEnderecoRepresentante = esr.Update();
                }


                //16. Gravando Vinculo do Representante do Sócio com a PJ (Vinculo Sócio)
                using (dR001_Vinculo vr = new dR001_Vinculo())
                {
                    vr.MainConnectionProvider = Cv;
                    vr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                    vr.t001_sq_pessoa_pai = decimal.Parse(SqSocio);
                    vr.a009_co_condicao = 504; // decimal.Parse(r.Qualificacao);
                    vr.r001_dt_entrada_vinculo = DateTime.Now;
                    //vr.r001_dt_saida_vinculo = DateTime.MinValue;
                    vr.r001_ds_cargo_direcao = "REPRESENTANTE"; // r.Qualificacao_Descricao;
                    vr.r001_in_situacao = "A";
                    if (r.TipoAssistido == null)
                    {
                        r.TipoAssistido = "309";
                    }
                    vr.a030_co_tipo_assistido = decimal.Parse(r.TipoAssistido);
                    vr.r001_acao = r.tipoacao;
                    vr.T001_cpf_cnpj_pessoa = r.CPFCNPJ;
                    vr.t001_sq_pessoa_rep_legal = 0;
                    vr.Update();
                }
            }

        }


        public void GravaRepresentanteDiretor(bSocios r, string SqSocio)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();
                //if (string.IsNullOrEmpty(SqRepresentante))
                string SqRepresentante = r.SQPessoa;
                //Gravando Representantes de Sócios em Pessoa
                //if (s.Representantes.Count > 0)
                //{
                using (dT001_Pessoa pr = new dT001_Pessoa())
                {
                    //int SqRepresentante;
                    //foreach (bSocios s in _Socios)
                    //foreach (bSocios r in s.Representantes)
                    //{
                    pr.MainConnectionProvider = cv;
                    pr.t001_sq_pessoa = Convert.ToInt32(r.SQPessoa);
                    pr.t001_in_tipo_pessoa = "F";//r.TipoPessoa;
                    pr.t001_ds_pessoa = r.Nome;
                    pr.t001_in_dados_atualizados = "S";
                    pr.t001_dt_ult_atualizacao = DateTime.Now;
                    //p.t001_email = s.;
                    //p.t001_tel_1 = "";
                    //p.t001_tel_2 = "";
                    SqRepresentante = pr.Update().ToString();
                    r.SQPessoa = SqRepresentante;
                    if (r.TipoPessoa == "F")
                    {
                        //14.Gravando Representantes de Socio - pessoa fisica
                        //(Dados dos Representantes de Socio do requerimento)
                        using (dT002_Pessoa_Fisica pfr = new dT002_Pessoa_Fisica())
                        {
                            pfr.MainConnectionProvider = cv;

                            pfr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                            pfr.t002_nr_cpf = r.CPFCNPJ;
                            if (r.CPFCNPJ.Length != 14)
                            {
                                if (r.TipoIdentidade != null)
                                    pfr.a010_co_tipo_documento = decimal.Parse(r.TipoIdentidade);
                                pfr.t002_nr_documento = r.RG;
                                pfr.t002_ds_emissor_documento = r.OrgaoExpedidor;
                                pfr.a004_uf_org_exped = r.OrgaoExpedidorUF;
                                pfr.a004_co_pais = r.NacionalidadeCodigo;
                                pfr.t002_ds_nacionalidade = r.Nacionalidade;
                                pfr.a004_co_uf_naturalidade = r.NaturalidadeCodigo;
                                if (r.EstadoCivil != null)
                                    pfr.a012_co_estado_civil = decimal.Parse(r.EstadoCivil);
                                if (r.EstadoCivilRegime != null && r.EstadoCivilRegime != string.Empty)
                                {
                                    pfr.a013_co_regime_bens = decimal.Parse(r.EstadoCivilRegime);
                                }
                                pfr.t002_dt_nascimento = r.DataNascimento;
                                //if (r.Profissao != null)
                                //    pfr.a020_co_profissao = decimal.Parse(r.Profissao);
                                if (r.Profissao_Descricao != null && r.Profissao_Descricao != string.Empty)
                                    pfr.t002_ds_profissao = r.Profissao_Descricao;
                                pfr.t002_in_sexo = r.in_Sexo;

                            }
                            //if (s.TipoEmancipado != null)
                            //{
                            //    pf.a014_co_emancipacao = decimal.Parse(s.TipoEmancipado);
                            //}
                            //pf.t002_nr_qtd_cotas = s.QuotaCapitalSocial;

                            pfr.Update();
                        }
                    }

                    //15. Gravando Representante de Socio - Endereço
                    //(dados dos endereços dos representantes do sócio)

                    using (dR002_Vinculo_Endereco esr = new dR002_Vinculo_Endereco())
                    {
                        int SqVinculoEnderecoRepresentante;
                        esr.MainConnectionProvider = cv;
                        esr.t001_sq_pessoa = int.Parse(SqRepresentante);
                        esr.t001_sq_pessoa_pai = int.Parse(SqSocio);
                        if (r.EndTipoLogradouro != null)
                        {
                            esr.a015_co_tipo_logradouro = decimal.Parse(r.EndTipoLogradouro);
                            esr.a015_ds_tipo_logradouro = r.EndDsTipoLogradouro;
                        }
                        esr.r002_ds_logradouro = r.EndLogradouro;
                        esr.r002_nr_logradouro = r.EndNumero;
                        esr.r002_ds_complemento = r.EndComplemento;
                        esr.r002_ds_bairro = r.EndBairro;
                        if (r.EndMunicipio != null)
                            esr.a005_co_municipio = decimal.Parse(r.EndMunicipio);//;

                        esr.a004_co_pais = 154;
                        //_a004_co_pais = s.;
                        esr.r002_nr_cep = r.EndCEP;
                        //_r002_ds_referencia = _se;
                        SqVinculoEnderecoRepresentante = esr.Update();
                    }


                    //16. Gravando Vinculo do Representante do Sócio com a PJ (Vinculo Sócio)
                    using (dR001_Vinculo vr = new dR001_Vinculo())
                    {
                        vr.MainConnectionProvider = cv;
                        vr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                        vr.t001_sq_pessoa_pai = decimal.Parse(SqSocio);
                        vr.a009_co_condicao = 504; // decimal.Parse(r.Qualificacao);
                        vr.r001_dt_entrada_vinculo = DateTime.Now;
                        //vr.r001_dt_saida_vinculo = DateTime.MinValue;
                        vr.r001_ds_cargo_direcao = "REPRESENTANTE"; // r.Qualificacao_Descricao;
                        vr.r001_in_situacao = "A";
                        vr.a030_co_tipo_assistido = decimal.Parse(r.TipoAssistido);
                        //v.r001_in_gerente_uso_firma = "";
                        //v.r001_vl_participacao = 0;
                        //v.r001_vl_participacao = s.QuotaCapitalSocial;
                        //v.r001_dt_inicio_mandato = DateTime.MinValue;
                        //v.r001_dt_termino_mandato = DateTime.MinValue;
                        //v.t001_sq_pessoa_rep_legal = "";
                        vr.t001_sq_pessoa_rep_legal = r.rep_legal;
                        vr.Update();

                        //Grava Representante Administrador
                        vr.MainConnectionProvider = cv;
                        vr.t001_sq_pessoa = decimal.Parse(SqRepresentante);
                        vr.t001_sq_pessoa_pai = decimal.Parse(CodigoEmpresa.ToString());
                        vr.a009_co_condicao = 5; // decimal.Parse(r.Qualificacao);
                        vr.r001_dt_entrada_vinculo = DateTime.Now;
                        //vr.r001_dt_saida_vinculo = DateTime.MinValue;
                        vr.r001_ds_cargo_direcao = "ADMINISTRADOR"; // r.Qualificacao_Descricao;
                        vr.r001_in_situacao = "A";
                        vr.a030_co_tipo_assistido = decimal.Parse(r.TipoAssistido);
                        //v.r001_in_gerente_uso_firma = "";
                        //v.r001_vl_participacao = 0;
                        //v.r001_vl_participacao = s.QuotaCapitalSocial;
                        //v.r001_dt_inicio_mandato = DateTime.MinValue;
                        //v.r001_dt_termino_mandato = DateTime.MinValue;
                        //v.t001_sq_pessoa_rep_legal = "";
                        vr.t001_sq_pessoa_rep_legal = 1; //r.rep_legal;
                        vr.Update();

                    }
                    //}

                }
                //}
                cv.CommitTransaction();
            }
        }
        public void CarregaRepresentantesSocio(bSocios s)
        {
            s.Representantes.Clear();
            DataTable dtRepr = dHelperQuery.CarregaRepresentantes(Convert.ToInt32(s.SQPessoa));
            foreach (DataRow rr in dtRepr.Rows)
            {
                bSocios re = new bSocios();
                re.Nome = rr["nome_responsavel"].ToString();
                re.SQPessoa = rr["SQ_REPRESENTANTE"].ToString();

                if (!string.IsNullOrEmpty(rr["R001_DT_INICIO_MANDATO"].ToString()))
                    re.DataInicioMandato = Convert.ToDateTime(rr["R001_DT_INICIO_MANDATO"].ToString());


                re.TipoPessoa = rr["TIPO_PESSOA"].ToString();
                //s.Qualificacao = r[""].ToString();
                re.CPFCNPJ = rr["CPF_RESPONSAVEL"].ToString();
                re.rep_legal = Convert.ToInt32(rr["REP_LEGAL"]);
                re.TipoIdentidade = rr["CO_TIPO_DOC_IDENT"].ToString();
                re.RG = rr["NO_DOC_IDENT"].ToString();
                re.OrgaoExpedidor = rr["ORGAO_EXPED"].ToString();
                re.OrgaoExpedidorUF = rr["UF_ORGAO_EXPED"].ToString();
                re.NacionalidadeCodigo = Convert.ToInt32(rr["CO_NACIONALIDADE"]);
                re.Nacionalidade = rr["NACIONALIDADE"].ToString();
                re.tipo_visto = rr["tipo_visto"].ToString();
                if (!string.IsNullOrEmpty(re.tipo_visto))
                {
                    re.emissao_visto = Convert.ToDateTime(rr["emissao_visto"].ToString());
                    if (!string.IsNullOrEmpty(rr["validade_visto"].ToString()))
                        re.validade_visto = Convert.ToDateTime(rr["validade_visto"].ToString());
                }

                re.NaturalidadeCodigo = rr["NATURALIDADE"].ToString();

                if (rr["SEXO"].ToString() == "M")
                    re.in_Sexo = "M";

                if (rr["SEXO"].ToString() == "F")
                    re.in_Sexo = "F";

                if (rr["REP_LEGAL"].ToString() != "0")
                    re.rep_legal = Convert.ToInt32(rr["REP_LEGAL"].ToString());

                re.EstadoCivil = rr["ESTADO_CIVIL"].ToString();
                re.EstadoCivilRegime = rr["REGIME_BENS"].ToString();
                if (!string.IsNullOrEmpty(rr["DATA_NASC"].ToString()))
                    re.DataNascimento = Convert.ToDateTime(rr["DATA_NASC"].ToString());

                re.TipoEmancipado = rr["CO_EMANCIPACAO"].ToString();
                re.TipoAssistido = rr["TIPO_REPRESENTANTE"].ToString();
                re.Analfabeto = rr["ANALFABETO"].ToString();
                re.Profissao = rr["PROFISSAO"].ToString();
                re.Profissao_Descricao = rr["DS_PROFISSAO"].ToString();
                re.DDD = rr["DDD"].ToString();
                re.Telefone = rr["TELEFONE"].ToString();
                re.Email = rr["EMAIL"].ToString();
                re.EndPais = rr["PAIS"].ToString();
                re.EndCEP = rr["CEP"].ToString();
                re.EndUF = rr["UF"].ToString();
                re.EndMunicipio = rr["MUNICIPIO"].ToString();
                re.EndBairro = rr["BAIRRO"].ToString();
                re.EndTipoLogradouro = rr["TIPO_LOGRADOURO"].ToString();
                if (!string.IsNullOrEmpty(rr["DS_TIPO_LOGRADOURO"].ToString()))
                    re.EndDsTipoLogradouro = rr["DS_TIPO_LOGRADOURO"].ToString();
                re.EndLogradouro = rr["LOGRADOURO"].ToString();
                re.EndNumero = rr["NO_LOGRADOURO"].ToString();
                //if (!VerificaSeNumerico(re.EndNumero))
                //{

                //}
                re.EndComplemento = rr["COMP_LOGRADOURO"].ToString();
                s.Representantes.Add(re);
            }

        }


        public int ContaNaoAdm()
        {
            int wConta = 0;
            for (int i = 0; i < _FundadorDiretor.Count; i++)
            {
                if (_FundadorDiretor[i].Qualificacao != "5")
                {
                    wConta++;
                }
            }
            return wConta;
        }
        public decimal BuscaSalarioMinimoVigente()
        {
            decimal wDec = 0;
            string DataValidade;
            if (!string.IsNullOrEmpty(_ProtocoloRCPJ))
            {

                if (_CNPJ_Orgao_Registro == "27079821000111") //RCPJ
                {
                    DataValidade = _ProtocoloRCPJ.Substring(1, 8).Substring(6, 2) + "/" + _ProtocoloRCPJ.Substring(1, 8).Substring(4, 2) + "/" + _ProtocoloRCPJ.Substring(1, 8).Substring(0, 4);
                }
                else if (_CNPJ_Orgao_Registro == "09280442000103") //JUCERJA
                {
                    DataValidade = dHelperSQL.GetDataProcessoJUCERJA(ProtocoloRCPJ);
                }
                else
                {
                    DataValidade = dHelperORACLE.GetDataEntradaByProtocoloSIARCO(ProtocoloRCPJ);
                }

                if (DataValidade == "")
                {
                    DataValidade = System.DateTime.Today.ToString("d");
                }
            }
            else
            {
                DataValidade = System.DateTime.Today.ToString("d");
            }
            using (dT014_EIRELI_Salario_Base sb = new dT014_EIRELI_Salario_Base())
            {
                DataTable wSB = sb.Query(DataValidade);
                if (wSB != null)
                    wDec = Convert.ToDecimal(wSB.Rows[0]["t014_salario_base"].ToString());
            }

            return wDec;
        }

        public decimal BuscaSalarioMinimoProtocolo(int wAno)
        {
            decimal wDec = 0;
            using (dT014_EIRELI_Salario_Base sb = new dT014_EIRELI_Salario_Base())
            {
                DataTable wSB = sb.QueryExisteNaJunta(wAno);
                if (wSB != null)
                    wDec = Convert.ToDecimal(wSB.Rows[0]["t014_salario_base"].ToString());
            }

            return wDec;
        }
        #endregion


        #region Ordenação de Filiais
        public void SetaSequenciaFilial()
        {
            if (_Filiais.Count == 0)
                return;

            int sequencia = 1;
            try
            {
                foreach (bFilial f in _Filiais)
                {
                    if (f.FilialUF == _orgaoRegistro.uf && f.Acao == 1)
                    {
                        f.Ordem = sequencia;
                        sequencia += 1;
                    }
                }
                foreach (bFilial f in _Filiais)
                {
                    if (f.FilialUF == _orgaoRegistro.uf && f.Acao == 3)
                    {
                        f.Ordem = sequencia;
                        sequencia += 1;
                    }
                }
                foreach (bFilial f in _Filiais)
                {
                    if (f.FilialUF != _orgaoRegistro.uf && f.Acao == 1)
                    {
                        f.Ordem = sequencia;
                        sequencia += 1;
                    }
                }
                foreach (bFilial f in _Filiais)
                {
                    if (f.FilialUF != _orgaoRegistro.uf && f.Acao == 3)
                    {
                        f.Ordem = sequencia;
                        sequencia += 1;
                    }
                }
                foreach (bFilial f in _Filiais)
                {
                    if (f.Acao == 5)
                    {
                        f.Ordem = sequencia;
                        sequencia += 1;
                    }
                }

                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    foreach (bFilial fi in _Filiais)
                    {
                        using (dR001_Vinculo fv = new dR001_Vinculo())
                        {
                            fv.MainConnectionProvider = cp;
                            fv.t001_sq_pessoa = fi.SqFilial;
                            fv.t001_sq_pessoa_pai = _CodigoEmpresa;
                            fv.R001_ordem_filial_contrato = fi.Ordem;
                            fv.UpdateSeqFilial();
                        }
                    }
                    cp.CommitTransaction();

                }
                AtualizaFiliais();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// utilizado em SolicitacaoInscricao.aspx
        /// </summary>
        /// <param name="fd"></param>
        public void CarregaRepresentantesDiretor(bSocios fd)
        {
            fd.Representantes.Clear();
            DataTable dtRepr = dHelperQuery.CarregaRepresentantes(Convert.ToInt32(fd.SQPessoa));
            foreach (DataRow rr in dtRepr.Rows)
            {
                bSocios re = new bSocios();
                re.Nome = rr["nome_responsavel"].ToString();
                re.SQPessoa = rr["SQ_REPRESENTANTE"].ToString();
                re.TipoPessoa = rr["TIPO_PESSOA"].ToString();
                //s.Qualificacao = r[""].ToString();
                re.CPFCNPJ = rr["CPF_RESPONSAVEL"].ToString();
                re.rep_legal = Convert.ToInt32(rr["ADM"]);
                re.TipoIdentidade = rr["CO_TIPO_DOC_IDENT"].ToString();
                re.RG = rr["NO_DOC_IDENT"].ToString();
                re.OrgaoExpedidor = rr["ORGAO_EXPED"].ToString();
                re.OrgaoExpedidorUF = rr["UF_ORGAO_EXPED"].ToString();
                re.NacionalidadeCodigo = Convert.ToInt32(rr["CO_NACIONALIDADE"]);
                re.Nacionalidade = rr["NACIONALIDADE"].ToString();
                re.NaturalidadeCodigo = rr["NATURALIDADE"].ToString();
                if (rr["SEXO"].ToString() == "M")
                    re.in_Sexo = "M";
                if (rr["SEXO"].ToString() == "F")
                    re.in_Sexo = "F";
                if (rr["REP_LEGAL"].ToString() != "0")
                    re.rep_legal = Convert.ToInt32(rr["REP_LEGAL"].ToString());
                re.EstadoCivil = rr["ESTADO_CIVIL"].ToString();
                re.EstadoCivilRegime = rr["REGIME_BENS"].ToString();
                if (rr["DATA_NASC"].ToString().Trim() != "")
                {
                    re.DataNascimento = Convert.ToDateTime(rr["DATA_NASC"].ToString());
                }
                re.TipoAssistido = rr["TIPO_REPRESENTANTE"].ToString();
                re.Profissao = rr["PROFISSAO"].ToString();
                re.Profissao_Descricao = rr["DS_PROFISSAO"].ToString();
                re.EndCEP = rr["CEP"].ToString();
                re.EndUF = rr["UF"].ToString();
                re.EndMunicipio = rr["MUNICIPIO"].ToString();
                re.EndBairro = rr["BAIRRO"].ToString();
                re.EndTipoLogradouro = rr["TIPO_LOGRADOURO"].ToString();
                if (!string.IsNullOrEmpty(rr["DS_TIPO_LOGRADOURO"].ToString()))
                    re.EndDsTipoLogradouro = rr["DS_TIPO_LOGRADOURO"].ToString();
                re.EndLogradouro = rr["LOGRADOURO"].ToString();
                re.EndNumero = rr["NO_LOGRADOURO"].ToString();
                //if (!VerificaSeNumerico(re.EndNumero))
                //{

                //}
                re.EndComplemento = rr["COMP_LOGRADOURO"].ToString();
                fd.Representantes.Add(re);
            }

        }


        #region Req de Alteração
        /// <summary>
        /// Retorna o idSeq do socio que saiu
        /// </summary>
        /// <returns></returns>

        public bool VerificaEntradaSaidaSocio()
        {
            bool ret = false;
            foreach (bSocios obj in _Socios)
            {
                if (obj.Qualificacao != "5" && (obj.tipoacao == 1 || obj.tipoacao == 5))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        public List<bSocios> VerificaSaidadeSocio()
        {
            List<bSocios> listSocios = new List<bSocios>();

            foreach (bSocios obj in _Socios)
            {
                if (obj.Qualificacao != "5" && obj.tipoacao == 5 && obj.Situacao == "A")
                {
                    listSocios.Add(obj);
                }
            }
            return listSocios;
        }
        public List<bSocios> VerificaEntradaSocio()
        {
            List<bSocios> listSocios = new List<bSocios>();

            foreach (bSocios obj in _Socios)
            {
                if (obj.Qualificacao != "5" && obj.tipoacao == 1)
                {
                    listSocios.Add(obj);
                }
            }
            return listSocios;
        }
        /// <summary>
        /// Valida se a quantida de quotas transferidas é maior que a qtde de quota do socio
        /// </summary>
        public void ValidaTRansferenciaQuotas()
        {
            // Para cada socio que recebeu quotas, verificar se a soma de transferencia é maior que a quantidade de quotas
            // 
            List<bSociosQuotas> listSocios = new List<bSociosQuotas>();
            foreach (bSociosQuotas obj in _transferenciaQuotas)
            {

                if (obj.QtdQuota > 0)
                {
                    listSocios.Add(obj);

                    //alterado pois qaundo o mesmo socio transfere para mais de um não coloca na lista
                    //if (!VerficaSocioListSocios(obj.CpfCedente, listSocios))
                    //{
                    //    listSocios.Add(obj);
                    //}

                }
            }

        }

        /// <summary>
        /// Retorna uma lista das quotas cedidas por um socio
        /// </summary>
        /// <returns></returns>
        public List<bSociosQuotas> getQuotasCedidas(int _SeqCedente)
        {
            List<bSociosQuotas> listSocios = new List<bSociosQuotas>();
            foreach (bSociosQuotas obj in _transferenciaQuotas)
            {
                if (obj.SQPessoa == _SeqCedente)
                {
                    listSocios.Add(obj);
                }
            }
            return listSocios;
        }
        public decimal GetValorQuotasCedidas(int idSeqpessoa)
        {
            decimal valor = 0;
            foreach (bSociosQuotas q in _transferenciaQuotas)
            {
                if (q.SQPessoa == idSeqpessoa)
                {
                    valor += q.QtdQuota;
                }
            }
            valor = valor * _ValorCota;
            return valor;
        }
        public decimal GetQtdQuotasCedidas(int idSeqpessoa)
        {
            decimal valor = 0;
            foreach (bSociosQuotas q in _transferenciaQuotas)
            {
                if (q.SQPessoa == idSeqpessoa)
                {
                    valor += q.QtdQuota;
                }
            }
            return valor;
        }
        public List<bSociosQuotas> VerificaCessaodeQuotas()
        {
            List<bSociosQuotas> listSocios = new List<bSociosQuotas>();
            foreach (bSociosQuotas obj in _transferenciaQuotas)
            {
                if (obj.QtdQuota > 0)
                {
                    listSocios.Add(obj);

                    //alterado pois qaundo o mesmo socio transfere para mais de um não coloca na lista
                    //if (!VerficaSocioListSocios(obj.CpfCedente, listSocios))
                    //{
                    //    listSocios.Add(obj);
                    //}

                }
            }
            return listSocios;
        }
        private bool VerficaSocioListSocios(string pCPFCedente, List<bSociosQuotas> listSocios)
        {
            foreach (bSociosQuotas lo in listSocios)
            {
                if (pCPFCedente == lo.CpfCedente)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        public bool IsStatusExigenciasSIARCO()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.t005_nr_protocolo = _ProtocoloRequerimento;
                return p.IsStatus(6);
            }
        }

        public DataTable CarregaTabelaAuxiliar(string wAux)
        {
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();
                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cv;
                    return v.PreencheTabelaAuxiliar(wAux);
                }
            }
        }

        public void ValidaTudo()
        {
            _Erros.Clear();
            //bErros Erro = new bErros();
            #region Validacoes Empresa
            if (string.IsNullOrEmpty(_RequerenteNome))
            {
                Erro.NomeCampo = "Nome do Requerente";
                Erro.Campo = "txtRequerenteNome";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_RequerenteCPF))
            {
                Erro = new bErros();
                Erro.NomeCampo = "CPF do Requerente";
                Erro.Campo = "txtRequerenteCPF";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_RequerenteEmail))
            {
                Erro = new bErros();
                Erro.NomeCampo = "E-mail do Requerente";
                Erro.Campo = "txtRequerenteEmail";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_RequerenteDDD))
            {
                Erro = new bErros();
                Erro.NomeCampo = "DDD do telefone do Requerente";
                Erro.Campo = "txtDDDRequerente";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            else
            {
                if (_RequerenteDDD.Trim().Length < 2)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "DDD do telefone do Requerente";
                    Erro.Campo = "txtDDDRequerente";
                    Erro.DescricaoErro = "DDD inválido";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            }
            if (string.IsNullOrEmpty(_RequerenteTelefone))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Telefone do Requerente";
                Erro.Campo = "txtRequerenteTelefone";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            else
            {
                if (_RequerenteTelefone.Trim().Length < 8)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Telefone do Requerente";
                    Erro.Campo = "txtRequerenteTelefone";
                    Erro.DescricaoErro = "Número inválido";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            }
            if (string.IsNullOrEmpty(_SedeNome))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Nome da sede";
                Erro.Campo = "txtSOCNomeEmpresa";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeDDD))
            {
                Erro = new bErros();
                Erro.NomeCampo = "DDD do telefone da sede";
                Erro.Campo = "txtSedeDDD";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            else
            {
                if (_SedeDDD.Trim().Length < 2)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "DDD do telefone da sede";
                    Erro.Campo = "txtSedeDDD";
                    Erro.DescricaoErro = "DDD inválido";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            }
            if (string.IsNullOrEmpty(_SedeTelefone))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Telefone da sede";
                Erro.Campo = "txtSedeTelefone";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            else
            {
                if (_SedeTelefone.Trim().Length < 8)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Telefone da sede";
                    Erro.Campo = "txtSedeTelefone";
                    Erro.DescricaoErro = "Número inválido";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            }
            if (string.IsNullOrEmpty(_SedeEmail))
            {
                Erro = new bErros();
                Erro.NomeCampo = "E-mail da sede";
                Erro.Campo = "txtSedeEmail";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (_Enquadramento == 0)
            {
                Erro = new bErros();
                Erro.NomeCampo = "O tipo de enquadramento da sede";
                Erro.Campo = "cbEnquadramento";
                Erro.DescricaoErro = "Em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (_CapitalSocial == 0 && _TipoPessoaJuridicaCodigo != 1265 && _TipoPessoaJuridicaCodigo != 1266)
            {
                Erro = new bErros();
                Erro.NomeCampo = "O Capital Social da sede";
                Erro.Campo = "txtCapitalSocial";
                Erro.DescricaoErro = "Em branco ou igual a 0(zero)";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }

            #region Sociedades e EIRELI
            if (_TipoPessoaJuridicaCodigo == 1264 || _NaturezaJuridicaCodigo == 2305 || _NaturezaJuridicaCodigo == 2313 || _NaturezaJuridicaCodigo == 2135)
            {
                if (_TipoPessoaJuridicaCodigo == 1264)
                {
                    if (_QtdCotas == 0)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "A quantidade de Quotas da sede";
                        Erro.Campo = "txtNoQuotas";
                        Erro.DescricaoErro = "Em branco ou igual a 0(zero)";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                    if (_ValorCota == 0)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "O valor da quota do capital social da sede";
                        Erro.DescricaoErro = "Em branco ou igual a 0(zero)";
                        Erro.Campo = "txtValorNominal";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                    if ((_ValorCota * _QtdCotas) != _CapitalSocial)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Valores relativos ao Capital Social da Sede";
                        Erro.Campo = "txtValorNominal";
                        Erro.DescricaoErro = "Valores incorretos";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                        Erro = new bErros();
                        Erro.NomeCampo = "Valores relativos ao Capital Social da Sede";
                        Erro.Campo = "txtNoQuotas";
                        Erro.DescricaoErro = "Valores incorretos";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                        Erro = new bErros();
                        Erro.NomeCampo = "Valores relativos ao Capital Social da Sede";
                        Erro.Campo = "txtCapitalSocial";
                        Erro.DescricaoErro = "Valores incorretos";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                    if (_moeda_corrente == "1")
                    {
                        if (string.IsNullOrEmpty(_ds_capital_nao_integralizado))
                        {
                            Erro = new bErros();
                            Erro.NomeCampo = "Descrição do capital não totalmente em moeda corrente.";
                            Erro.Campo = "txtDsCapitalEmpresaIntegralizado";
                            Erro.DescricaoErro = "Em branco.";
                            Erro.Localizacao = "1";
                            _Erros.Add(Erro);
                        }
                    }
                }
                if (_ehInicioDataRegistro == "N")
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Data de início da sociedade";
                    Erro.Campo = "txtInicioAtv";
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                _DuracaoSociedade = _AssociacaoTempoDuracao;
                if (_DuracaoSociedade != null) //Sociedade e EIRELI
                {
                    if (_DuracaoSociedade <= DateTime.Today)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Data de duração da sociedade";
                        Erro.Campo = "txtSOCTempoDuracao";
                        Erro.DescricaoErro = "Menor ou igual a data atual";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                        if (_DataInicioSociedade == null)
                        {
                            if (_DuracaoSociedade <= _DataInicioSociedade)
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Data de duração da sociedade";
                                Erro.Campo = "txtSOCTempoDuracao";
                                Erro.DescricaoErro = "Data de duração menor ou igual a data de início da atividade";
                                Erro.Localizacao = "1";
                                _Erros.Add(Erro);
                                Erro = new bErros();
                                Erro.NomeCampo = "Data de duração da sociedade";
                                Erro.Campo = "txtInicioAtv";
                                Erro.DescricaoErro = "Data de duração menor ou igual a data de início da atividade";
                                Erro.Localizacao = "1";
                                _Erros.Add(Erro);
                            }
                        }
                    }
                }
                if (_NaturezaJuridicaCodigo == 2305 || _NaturezaJuridicaCodigo == 2313)
                {
                    if (_CapitalSocial < _Salario_Minimo_Vigente * 100)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Capital social";
                        Erro.Campo = "txtCapitalSocial";
                        Erro.DescricaoErro = "O Capital Social deve ser igual ou maior que R$" + string.Format("{0:0,0.00}", Convert.ToString(_Salario_Minimo_Vigente * 100));
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                }

                //else
                //{
                //    if (_CapitalSocial == 0)
                //    {
                //        Erro = new bErros();
                //        Erro.NomeCampo = "Capital social";
                //        Erro.DescricaoErro = "Em branco ou com valor 0(zero)";
                //        Erro.Localizacao = "1";
                //        _Erros.Add(Erro);
                //    }
                //}
                if (_ehIntegralizado == "N")
                {

                    decimal d2 = 0;
                    decimal d3 = 0;
                    if (_data_limite_integralizacao == null)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Data limite para integralização do capital social";
                        Erro.Campo = "txtDataLimiteEmpresaIntegralizar";
                        Erro.DescricaoErro = "Data limite para integralização em branco";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                    else
                    {
                        if (ValidaData(Convert.ToDateTime(_data_limite_integralizacao)))
                        {
                            if (Convert.ToDateTime(_data_limite_integralizacao) <= DateTime.Today)
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Data limite para integralização do capital social";
                                Erro.Campo = "txtDataLimiteEmpresaIntegralizar";
                                Erro.DescricaoErro = "Menor ou igual a data atual";
                                Erro.Localizacao = "1";
                                _Erros.Add(Erro);
                            }
                        }
                    }
                    d2 = Convert.ToDecimal(_capital_nao_integralizado);
                    d3 = Convert.ToDecimal(_CapitalSocial);
                    if (string.IsNullOrEmpty(_ds_capital_nao_integralizado))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Descrição do capital não integralizado";
                        Erro.Campo = "txtDsCapitalEmpresaIntegralizado";
                        Erro.DescricaoErro = "Em branco";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                    if (d2 + d3 > 0 && d2 != d3)
                    {
                        if (_capital_integralizado == 0)
                        {
                            Erro = new bErros();
                            Erro.NomeCampo = "Capital Integralizado";
                            Erro.Campo = "txtCapitalEmpresaIntegralizado";
                            Erro.DescricaoErro = "Em branco ou iqual a 0(zero)";
                            Erro.Localizacao = "1";
                            _Erros.Add(Erro);
                        }
                    }
                    if (_CapitalSocial > 0)
                    {
                        if (_CapitalSocial != _capital_integralizado + _capital_nao_integralizado)
                        {
                            Erro = new bErros();
                            Erro.NomeCampo = "Capital Social,Capital Integralizado e Capital Não Integralizado";
                            Erro.Campo = "txtCapitalEmpresaIntegralizado";
                            Erro.DescricaoErro = "A Soma do capital integralizado e do capital a integralizar é diferente do capital social";
                            Erro.Localizacao = "1";
                            _Erros.Add(Erro);
                            Erro = new bErros();
                            Erro.NomeCampo = "Capital Social,Capital Integralizado e Capital Não Integralizado";
                            Erro.Campo = "txtCapitalEmpresaNaoIntegralizar";
                            Erro.DescricaoErro = "A Soma do capital integralizado e do capital a integralizar é diferente do capital social";
                            Erro.Localizacao = "1";
                            _Erros.Add(Erro);
                        }
                    }
                }
            }
            #endregion
            #region Endereço Sede
            if (string.IsNullOrEmpty(_SedeCEP))
            {
                Erro = new bErros();
                Erro.NomeCampo = "CEP da sede";
                Erro.Campo = "txtSedeCEP";
                Erro.DescricaoErro = "CEP da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeUF))
            {
                Erro = new bErros();
                Erro.NomeCampo = "UF da sede";
                Erro.Campo = "cbSedeUf";
                Erro.DescricaoErro = "UF da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeMunicipio))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Município da sede";
                Erro.Campo = "cbSedeMunicipio";
                Erro.DescricaoErro = "O município da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeBairro))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Bairro da sede";
                Erro.Campo = "cbSedeBairro";
                Erro.DescricaoErro = "O bairro da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeDsTipoLogradouro))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Tipo logradouro da sede";
                Erro.Campo = "cbSedeTipoLogradouro";
                Erro.DescricaoErro = "O tipo de logradouro da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeLogradouro))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Logradouro da sede";
                Erro.Campo = "cbSedeLogradouro";
                Erro.DescricaoErro = "O logradouro da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (string.IsNullOrEmpty(_SedeNumero) && string.IsNullOrEmpty(_SedeComplemento))
            {
                if (string.IsNullOrEmpty(_SedeNumero))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Número do logradouro da sede";
                    Erro.Campo = "txtSedeNumero";
                    Erro.DescricaoErro = "O número do logradouro da sede em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(_SedeComplemento))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Complemento do logradouro da sede";
                    Erro.Campo = "txtSedeComplemento";
                    Erro.DescricaoErro = "O complemento do logradouro da sede em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            }
            #endregion
            if (string.IsNullOrEmpty(_ObjetoSocial))
            {
                Erro = new bErros();
                Erro.NomeCampo = "Objeto Social da sede";
                Erro.Campo = "txtObjetoSocial";
                Erro.DescricaoErro = "O Objeto Social da sede em branco";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            if (_CNAEs.Count == 0)
            {
                Erro = new bErros();
                Erro.NomeCampo = "CNAE da sede";
                Erro.Campo = "ListCNAE";
                Erro.DescricaoErro = "Não existe(m) CNAE(s) para a sede";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            ValidaFilial();
            //if (Req.Filiais.Count > 0)
            //{
            //    decimal SomaCapitalFiliais = 0;
            //    for (int i = 0; i < Req.Filiais.Count; i++)
            //    {
            //        if (Req.Filiais[i].FilialCapitalDestacado != null && Req.Filiais[i].FilialCapitalDestacado > 0)
            //        {
            //            SomaCapitalFiliais += Req.Filiais[i].FilialCapitalDestacado;
            //        }
            //    }
            //    if (SomaCapitalFiliais > 0)
            //    {

            /// <summary>
            /// Validações de Alertas para o evento de alteração
            /// Verificações:
            ///     1. Verificar se o CNPJ no Siarco está diferente do CNPJ do requerimento
            ///     2. Se for alteração ou exclusão de sócio:
            ///         2.1 Verificar se o sócio existe no Siarco
            ///     3. Se for inclusão de sócio:
            ///         3.1 Verificar se o sócio já existe no Siarco
            /// </summary>
            //        if (SomaCapitalFiliais > Convert.ToDecimal(txtCapitalSocial.Text))
            //        {
            //            wChave = false;
            //            Alert("A soma do capital destacado da(s) filial(ais) é maior que o valor do capital social da matriz!");
            //        }
            //    }
            //}

            #endregion
            #region QSA
            try
            {
                int wQual = 0;
                Boolean wChaveNacionalidade = true;
                int wIndex = -1;
                decimal wCapSoc = 0;
                decimal wCapNaoIntegralizado = 0;
                _TodoQSA.Clear();
                foreach (bSocios s1 in _Socios)
                {
                    _TodoQSA.Add(s1);
                    if (s1.Representantes.Count > 0)
                    {
                        foreach (bSocios r1 in s1.Representantes)
                        {
                            r1.Qualificacao_Descricao = "REPRESENTANTE";
                            r1.Qualificacao = "504";
                            _TodoQSA.Add(r1);
                        }
                    }
                }
                foreach (bSocios s in _TodoQSA)
                {
                    string wTipoPessoa = "";
                    if (s.CPFCNPJ.Length > 11)
                        wTipoPessoa = "j";
                    else
                        wTipoPessoa = "F";
                    wIndex++;
                    if (s.Situacao == "A")
                    {
                        if (string.IsNullOrEmpty(s.Qualificacao))
                        {
                            Erro = new bErros();
                            Erro.NomeCampo = "Qualificação do Sócio " + s.Nome;
                            Erro.Campo = "ddlSocioQualificacao";
                            Erro.DescricaoErro = "É obrigatória.";
                            Erro.Index = wIndex;
                            Erro.Localizacao = "2";
                            _Erros.Add(Erro);
                        }
                        else
                        {
                            wQual = Convert.ToInt32(s.Qualificacao);
                            if (s.Qualificacao_Descricao.Contains("EXTERIOR"))
                            {
                                if (s.Representantes.Count == 0)
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "ddlSocioQualificacao";
                                    Erro.DescricaoErro = "Exige um representante.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                            }
                            if (string.IsNullOrEmpty(s.Nome))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Nome do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioNome";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.CPFCNPJ))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "CPF/CNPJ do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioCPFCNPJ";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (s.Qualificacao == "54" && (_TipoPessoaJuridicaCodigo == 1265 && _TipoPessoaJuridicaCodigo == 1266))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "A data de início do mandato";
                                Erro.Campo = "txtFundDiretorDataIni";
                                Erro.DescricaoErro = "É obrigatória.";
                                Erro.Localizacao = "3";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.Nacionalidade))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Nacionalidade do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "ddlSocioNacionalidade";
                                Erro.DescricaoErro = "É obrigatória.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            else
                            {
                                if (s.NacionalidadeCodigo != 154 && wTipoPessoa == "F")
                                {
                                    wChaveNacionalidade = false;
                                    if (string.IsNullOrEmpty(s.tipo_visto) && s.rep_legal > 0)
                                    {
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Tipo de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = "cbSocioTipoVisto";
                                        Erro.DescricaoErro = "É obrigatório.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                    }
                                    else
                                    {
                                        if (s.tipo_visto == "TEMPORÁRIO")
                                        {
                                            if (s.rep_legal > 0)
                                            {
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Tipo de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = "cbSocioTipoVisto";
                                                Erro.DescricaoErro = "Incompatível com o poder de administração.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Poder de administração do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = ""; // checkbox
                                                Erro.DescricaoErro = "Incompatível com o tipo de visto.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                            }
                                        }
                                        if (s.rep_legal > 0)
                                        {
                                            if (string.IsNullOrEmpty(s.emissao_visto.ToString()))
                                            {
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Data de emissão de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = "txtDtSocioEmissaoVisto";
                                                Erro.DescricaoErro = "É obrigatória.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                            }
                                            if (string.IsNullOrEmpty(s.validade_visto.ToString()) && !s.tipo_visto.Contains("INDETERM."))
                                            {
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Prazo de validade de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = "txtSocioValidadeVisto";
                                                Erro.DescricaoErro = "É obrigatória.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(s.validade_visto.ToString()) && !string.IsNullOrEmpty(s.emissao_visto.ToString()))
                                    {
                                        if (s.validade_visto <= s.emissao_visto)
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Prazo de validade de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "txtSocioValidadeVisto";
                                            Erro.DescricaoErro = "É menor que a data de emissao.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Prazo de emissão de visto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "txtDtSocioEmissaoVisto";
                                            Erro.DescricaoErro = "É maior que a data de validade.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                    }
                                }
                            }
                            if (wTipoPessoa == "F")
                            {
                                if (string.IsNullOrEmpty(s.TipoIdentidade))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Tipo de documento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "ddlTipoIdentidade";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                else
                                {
                                    if (s.TipoIdentidade == "6573" && s.NacionalidadeCodigo == 154)
                                    {
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Tipo de documento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = "ddlTipoIdentidade";
                                        Erro.DescricaoErro = "É incompatível com a nacionalidade.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Nacionalidade do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = "ddlSocioNacionalidade";
                                        Erro.DescricaoErro = "É incompatível com o tipo de documento.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                    }
                                }
                                if (string.IsNullOrEmpty(s.RG))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Nº do documento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtSocioRG";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }

                                if (string.IsNullOrEmpty(s.OrgaoExpedidor))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Orgão expedidor do documento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "cbSocioOrgaoExpedidor";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }

                                if (string.IsNullOrEmpty(s.OrgaoExpedidorUF) && wChaveNacionalidade)
                                {

                                    Erro = new bErros();
                                    Erro.NomeCampo = "UF do orgão emissor do documento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "ddlSocioOrgaoExpedidorUF";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (string.IsNullOrEmpty(s.in_Sexo))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Sexo do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = ""; // checkbox
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (string.IsNullOrEmpty(s.Analfabeto))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Analfabeto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = ""; // checkbox
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                else
                                {
                                    if (s.rep_legal > 0 && s.Analfabeto != "N")
                                    {
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Poder de administração do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = ""; //checkbox
                                        Erro.DescricaoErro = "Incompátivel com analfabeto.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Analfabeto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = ""; //checkbox
                                        Erro.DescricaoErro = "Incompátivel com poder de administração.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                    }
                                    if (s.Representantes.Count == 0 && s.Analfabeto != "N")
                                    {
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Analfabeto do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = ""; //checkbox
                                        Erro.DescricaoErro = "Necessita ser representado.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                    }
                                }
                                if (string.IsNullOrEmpty(s.EstadoCivil))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Estado Civil do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "ddlSocioEstadoCivil";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                else
                                {
                                    if (s.EstadoCivil == "493")
                                    {
                                        if (string.IsNullOrEmpty(s.EstadoCivilRegime))
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Regime do estado civil do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "ddlSocioRegime";
                                            Erro.DescricaoErro = "É obrigatório.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(s.DataNascimento.ToString()) || s.DataNascimento == DateTime.MinValue)
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Data de nascimento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtSocioDataNascimento";
                                    Erro.DescricaoErro = "É obrigatória";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                else
                                {
                                    if (VerificaIdade(Convert.ToDateTime(s.DataNascimento)))
                                    {
                                        if (Menor16(Convert.ToDateTime(s.DataNascimento)))
                                        {
                                            if (s.Representantes.Count == 0)
                                            {
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Data de nascimento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = "txtSocioDataNascimento";
                                                if (_NaturezaJuridicaCodigo != 2135 && _NaturezaJuridicaCodigo != 2305 && _NaturezaJuridicaCodigo != 2313)
                                                    Erro.DescricaoErro = "Menor de 16 anos necessita de um representante.";
                                                else
                                                    Erro.DescricaoErro = "Empresário não pode ser menor de 16 anos.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                            }
                                        }
                                        else
                                        {
                                            if (s.Representantes.Count == 0 && s.TipoEmancipado == "0")
                                            {
                                                Erro = new bErros();
                                                Erro.NomeCampo = "Data de nascimento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                                Erro.Campo = "txtSocioDataNascimento";
                                                if (_NaturezaJuridicaCodigo != 2135 && _NaturezaJuridicaCodigo != 2305 && _NaturezaJuridicaCodigo != 2313)
                                                    Erro.DescricaoErro = "Menor necessita de um representante ou ser emancipado.";
                                                else
                                                    Erro.DescricaoErro = "Menor necessita ser emancipado.";
                                                Erro.Index = wIndex;
                                                Erro.Localizacao = "2";
                                                _Erros.Add(Erro);
                                            }
                                        }
                                        if (s.TipoEmancipado == "0" && s.rep_legal > 0 && _NaturezaJuridicaCodigo != 2135)
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Poder de administração do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "chkAdm"; //checkbox
                                            Erro.DescricaoErro = "Menor representado não pode ter poder de administração.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                    }
                                    else
                                    {
                                        if (s.Qualificacao == "30") // menores
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "ddlSocioQualificacao";
                                            Erro.DescricaoErro = "A qualificação incompátivel com a data de nascimento.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Data de nascimento do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "txtSocioDataNascimento";
                                            Erro.DescricaoErro = "A data de nascimento incompátivel com a qualificação.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                        if (s.Qualificacao == "29" && s.rep_legal > 0) // incapazes
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "ddlSocioQualificacao";
                                            Erro.DescricaoErro = "A qualificação incompátivel com o poder de representação.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Poder de representação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "chkAdm"; // checkbox
                                            Erro.DescricaoErro = "Poder de representação incompátivel com a qualificação.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                        if (s.Qualificacao == "29" && s.Representantes.Count == 0) // incapazes
                                        {
                                            Erro = new bErros();
                                            Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                            Erro.Campo = "ddlSocioQualificacao";
                                            Erro.DescricaoErro = "A qualificação incompátivel precisa de um representante.";
                                            Erro.Index = wIndex;
                                            Erro.Localizacao = "2";
                                            _Erros.Add(Erro);
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(s.Profissao_Descricao))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Profissão do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtSocioProfissao";
                                    Erro.DescricaoErro = "É obrigatória.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (string.IsNullOrEmpty(s.Nome_Pai) && _NaturezaJuridicaCodigo == 2135)
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Nome do pai do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtNomePai";
                                    Erro.DescricaoErro = "É obrigatória.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (string.IsNullOrEmpty(s.Nome_Mae) && s.Qualificacao != "504")
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Nome da mãe do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtNomeMae";
                                    Erro.DescricaoErro = "É obrigatória.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                            }
                            if (string.IsNullOrEmpty(s.DDD))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "DDD do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioDDD";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.Telefone))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Telefone do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioTelefone";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.Email))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "E-mail do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioEmail";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (s.Qualificacao != "5" && s.Qualificacao != "504" && _TipoPessoaJuridicaCodigo == 1264)//_NaturezaJuridicaCodigo != 2135 && _NaturezaJuridicaCodigo != 2305 && _NaturezaJuridicaCodigo != 2313)
                            {
                                if (s.QuotaCapitalSocial == 0)
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Quotas do capital social do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtSocioQuota";
                                    Erro.DescricaoErro = "É obrigatória.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (s.QuotaCapitalSocial * _ValorCota >= _CapitalSocial)
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Quotas do capital social do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "txtSocioQuota";
                                    Erro.DescricaoErro = "É maior ou igual ao capital social da sede.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                //Capital totalmente integralizado - Falta
                                if (s.Capital_a_Integralizar != 0)
                                {
                                    wCapNaoIntegralizado += s.Capital_a_Integralizar;
                                    if (s.Capital_a_Integralizar + s.CapitalIntegralizado != s.QuotaCapitalSocial * _ValorCota)
                                    {
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Capital Integralizado do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = "txtValorIntegralizado";
                                        Erro.DescricaoErro = "Valor integralizado somado ao valor não integralizado não são iguais ao valor da quantidade de quotas.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                        Erro = new bErros();
                                        Erro.NomeCampo = "Capital não integralizado do " + s.Qualificacao_Descricao + " " + s.Nome;
                                        Erro.Campo = "txtValoraIntegralizar";
                                        Erro.DescricaoErro = "Valor integralizado somado ao valor não integralizado não são iguais ao valor da quantidade de quotas.";
                                        Erro.Index = wIndex;
                                        Erro.Localizacao = "2";
                                        _Erros.Add(Erro);
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(s.EndPais))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "País de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "cbPaisSocio";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (s.EndPais == "154" || string.IsNullOrEmpty(s.EndPais))
                            {
                                if (string.IsNullOrEmpty(s.EndUF))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "UF de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "cbSocioUf";
                                    Erro.DescricaoErro = "É obrigatória.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                                if (string.IsNullOrEmpty(s.EndMunicipio))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Município de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.Campo = "cbSocioMunicipio";
                                    Erro.DescricaoErro = "É obrigatório.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                            }
                            if (string.IsNullOrEmpty(s.EndBairro))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Bairro de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "cbSocioBairro";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.EndDsTipoLogradouro))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Tipo de logradouro de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "cbSocioTipoLogradouro";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.EndLogradouro))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Logradouro de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "cbSocioLogradouro";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                            if (string.IsNullOrEmpty(s.EndNumero) && string.IsNullOrEmpty(s.EndComplemento))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Número ou complemento de residência do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.Campo = "txtSocioNumero";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                        }
                        if (_TipoPessoaJuridicaCodigo == 1264 && s.Qualificacao != "5" && s.Qualificacao != "504")
                        {
                            if (s.QuotaCapitalSocial > 0)
                                wCapSoc += s.QuotaCapitalSocial * _ValorCota;
                            else
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Quantidade de quotas do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.DescricaoErro = "É obrigatório e deve ser maior que 1.";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                        }
                        if (s.Qualificacao == "5")
                        {
                            for (int i = 0; i < _Socios.Count; i++)
                            {
                                if (_Socios[i].CPFCNPJ.ToUpper() == s.CPFCNPJ.ToUpper() && _Socios[i].Qualificacao != "5")
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.DescricaoErro = "Já existe no QSA. Exclua-o e habilite o sócio.";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                            }
                        }
                        if ((s.Qualificacao == "22" && s.rep_legal > 0) || s.Qualificacao == "5")
                        {
                            if (s.NacionalidadeCodigo != 154 && s.tipo_visto == "TEMPORÁRIO") // ver como fazer com Portugues e outros ATENÇÃO
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                                Erro.DescricaoErro = "Estrangeiros com poder de administração devem ter vistos permanentes";
                                Erro.Index = wIndex;
                                Erro.Localizacao = "2";
                                _Erros.Add(Erro);
                            }
                        }
                        if (string.IsNullOrEmpty(s.Qualificacao))
                        {
                            Erro = new bErros();
                            Erro.NomeCampo = "Qualificação do " + s.Qualificacao_Descricao + " " + s.Nome;
                            Erro.DescricaoErro = "Estrangeiros com poder de administração devem ter vistos permanentes";
                            Erro.Index = wIndex;
                            Erro.Localizacao = "2";
                            _Erros.Add(Erro);
                        }
                        if (_TipoPessoaJuridicaCodigo == 1265 || _TipoPessoaJuridicaCodigo == 1266)
                        {
                            if (s.Qualificacao != "54")
                            {
                                if (string.IsNullOrEmpty(s.DataInicioMandato.ToString()))
                                {
                                    Erro = new bErros();
                                    Erro.NomeCampo = "Data de início de mandato do " + s.Qualificacao_Descricao + " " + s.Nome;
                                    Erro.DescricaoErro = "É obrigatoria para não fundadores.";
                                    Erro.Campo = "txtFundDiretorDataIni";
                                    Erro.Index = wIndex;
                                    Erro.Localizacao = "2";
                                    _Erros.Add(Erro);
                                }
                            }
                        }
                    }
                }
                if (wCapSoc != _CapitalSocial && _NaturezaJuridicaCodigo != 2135 && _NaturezaJuridicaCodigo != 2305 && _NaturezaJuridicaCodigo != 2313)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Capital Social";
                    Erro.DescricaoErro = "O capital social da empresa não é igual a soma das quotas dos sócios.";
                    Erro.Index = wIndex;
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (wCapNaoIntegralizado != _capital_nao_integralizado && _NaturezaJuridicaCodigo != 2135 && _NaturezaJuridicaCodigo != 2305 && _NaturezaJuridicaCodigo != 2313)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Capital Social";
                    Erro.DescricaoErro = "O capital social não integralizado da empresa não é igual a soma do capital não integralizado dos sócios.";
                    Erro.Index = wIndex;
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
            #endregion

                #region Adicionais
                if (_TipoPessoaJuridicaCodigo == 1265 || _TipoPessoaJuridicaCodigo == 1266)
                {
                    if (string.IsNullOrEmpty(_ArtigoEstatuto))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº do artigo que cite a diretoria";
                        Erro.DescricaoErro = "É obrigatória.";
                        Erro.Campo = "txtFundArtEstatuto1";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (_Edital_Fixado_Sede == "N" && _Edital_Publicado_Jornal == "N" && _Edital_Outros == "N")
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Forma de convocação";
                        Erro.DescricaoErro = "É obrigatória.";
                        Erro.Campo = "chkNaSede";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                        //Erro = new bErros();
                        //Erro.NomeCampo = "Forma de convocação";
                        //Erro.DescricaoErro = "É obrigatória.";
                        //Erro.Campo = "chkNoJornal";
                        //Erro.Index = wIndex;
                        //Erro.Localizacao = "3";
                        //_Erros.Add(Erro);
                        //Erro = new bErros();
                        //Erro.NomeCampo = "Forma de convocação";
                        //Erro.DescricaoErro = "É obrigatória.";
                        //Erro.Campo = "chkOutros";
                        //Erro.Index = wIndex;
                        //Erro.Localizacao = "3";
                        //_Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Convocacao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "O nº do artigo que cite a forma de convocação";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNumeroArtigoEstatutoConvocacao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Quorum_Deliberacao) && string.IsNullOrEmpty(_Outro_Quorum_Deliberacao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "O quorum para deliberação sobre destituição de administradores";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "ddlQuorumDeliberacao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                        //Erro = new bErros();
                        //Erro.NomeCampo = "O quorum para deliberação sobre destituição de administradores";
                        //Erro.DescricaoErro = "É obrigatório.";
                        //Erro.Campo = "txtQuorumDeliberacao";
                        ////Erro.Index = wIndex;
                        //Erro.Localizacao = "3";
                        //_Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Deliberacao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "O artigo do quorum para deliberação sobre destituição de administradores";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtArtQuorumDeliberacao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Quorum_Alteracao) && string.IsNullOrEmpty(_Outro_Quorum_Alteracao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Quorum para alteração do Estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "ddlQuorumAlteracao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                        //Erro = new bErros();
                        //Erro.NomeCampo = "Quorum para alteração do Estatuto";
                        //Erro.DescricaoErro = "É obrigatório.";
                        //Erro.Campo = "txtQuorumAlteracao";
                        ////Erro.Index = wIndex;
                        //Erro.Localizacao = "3";
                        //_Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(Num_Artigo_Estatuto_Alteracao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº do artigo para alteração do Estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtArtQuorumAlteracao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Quorum_Dissolucao) && string.IsNullOrEmpty(_Outro_Quorum_Dissolucao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Quorum para dissolução da associação";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "ddlQuorumDissolucao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                        //Erro = new bErros();
                        //Erro.NomeCampo = "Quorum para dissolucao da associação";
                        //Erro.DescricaoErro = "É obrigatório.";
                        //Erro.Campo = "txtQuorumDissolucao";
                        ////Erro.Index = wIndex;
                        //Erro.Localizacao = "3";
                        //_Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Dissolucao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº do artigo para dissolução da associação";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtArtQuorumDissolucao";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Destino_Patrimonio))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº do artigo que define o destino do patrimônio em caso de dissolução";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtDestinoPatrimonio";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Obrigacoes_Sociais))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Os membros respondem, subsidiariamente, pelas obrigações sociais?";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "rbtInformacoesAdicionaisSocioObrigacoesSociaisS";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    else
                    {
                        if (_Obrigacoes_Sociais == "S")
                        {
                            if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Obrigacoes_Sociais))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Nº do artigo refernte as obrigações sociais?";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Campo = "txtArtObrigacoesSociais";
                                //Erro.Index = wIndex;
                                Erro.Localizacao = "3";
                                _Erros.Add(Erro);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(_Possui_Fundo_Social))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "A Associa&ccedil;&atilde;o possui Fundo Social";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "rbtInformacoesAdicionaisFundoSocialS";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    else
                    {
                        if (_Possui_Fundo_Social == "S")
                        {
                            if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Fundo_Social))
                            {
                                Erro = new bErros();
                                Erro.NomeCampo = "Nº do artigo refernte ao fundo social";
                                Erro.DescricaoErro = "É obrigatório.";
                                Erro.Campo = "Num_Artigo_Estatuto_Obrigacoes_Sociais";
                                //Erro.Index = wIndex;
                                Erro.Localizacao = "3";
                                _Erros.Add(Erro);
                            }
                        }
                    }
                    if (_Fonte_Contribuicoes_Doacao == "N" && _Fonte_Contribuicoes_Mensais == "N" && _Fonte_Recursos_Governamentais == "N")
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Fontes de Recursos para manutenção da Associação";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "ckbFonteRecursoContribuicoesMensais";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Num_Artigo_Estatuto_Associacao))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº do artigo do estatuto que cite as fontes de recursos da Associação";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNrArtEstatutoAssociado";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Nome_Advogado))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Advogado que visou o estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNomeAdvogado";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_CPF_Advogado))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "CPF do advogado que visou o estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtAdvogadoCPFCNPJ";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Inscricao_OAB_Advogado))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nº da inscrição na OAB do advogado que visou o estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNrInscricaoOAB";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_UF_OAB_Advogado))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "UF da OAB do advogado que visou o estatuto";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "cbUFOrgaoExpedAdvogado";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_Nome_Contador))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Nome do contador";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNomeContador";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_CPF_Contador))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "CPF do contador";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtContadorCPFCNPJ";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_CRC_Contador))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "CRC do contador";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "txtNumeroContadorCRC";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                    if (string.IsNullOrEmpty(_UF_CRC))
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "UF do CRC do contador";
                        Erro.DescricaoErro = "É obrigatório.";
                        Erro.Campo = "SelectedItem";
                        //Erro.Index = wIndex;
                        Erro.Localizacao = "3";
                        _Erros.Add(Erro);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao validar o requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
                #endregion

        }
        public Boolean ValidaData(DateTime wData)
        {
            Boolean wChave = true;
            int wAno = 0;

            wAno = Convert.ToInt32(wData.ToShortDateString().Substring(6, 4));
            if (wAno < (DateTime.Today.Date.Year - 100))
            {
                wChave = false;
            }
            return wChave;
        }
        private void ValidaFilial()
        {

            decimal wCapitalDestacado = 0;

            if (_Filiais.Count == 0)
                return;
            int wContaFilial = 1;
            foreach (bFilial f in _Filiais)
            {
                wCapitalDestacado += f.FilialCapitalDestacado;
                if (f.FilialCapitalDestacado >= _CapitalSocial && f.FilialCapitalDestacado > 0)
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Capital destacado da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Maior que o capital social da sede";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialUF))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "UF da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialMunicipio))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Município da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialBairro))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Bairro da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialTipoLogradouro))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Tipo de logradouro da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialLogradouro))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Logradouro da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialNumero) && string.IsNullOrEmpty(f.FilialComplemento))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "Número do endereço da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                    Erro = new bErros();
                    Erro.NomeCampo = "Complemento do endereço da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                if (string.IsNullOrEmpty(f.FilialCEP))
                {
                    Erro = new bErros();
                    Erro.NomeCampo = "CEP da Filial " + wContaFilial.ToString();
                    Erro.DescricaoErro = "Em branco";
                    Erro.Localizacao = "1";
                    _Erros.Add(Erro);
                }
                else
                {
                    if (f.FilialCEP.Length != 8)
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "CEP da Filial " + wContaFilial.ToString();
                        Erro.DescricaoErro = "CEP inválido";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                }
                wContaFilial++;
            }
            if (wCapitalDestacado >= _CapitalSocial && wCapitalDestacado > 0)
            {
                Erro = new bErros();
                Erro.NomeCampo = "Capital destacado das filiais";
                Erro.DescricaoErro = "A soma dos capitais destacados é maior que o capital social";
                Erro.Localizacao = "1";
                _Erros.Add(Erro);
            }
            for (int i = 0; i < _Filiais.Count - 1; i++)
            {
                for (int j = i + 1; j < _Filiais.Count; j++)
                {
                    if (_Filiais[i] == _Filiais[j])
                    {
                        Erro = new bErros();
                        Erro.NomeCampo = "Filiais " + i.ToString() + " e " + j.ToString();
                        Erro.DescricaoErro = "São iquais";
                        Erro.Localizacao = "1";
                        _Erros.Add(Erro);
                    }
                }
            }

        }




        public void ValidaAlertas()
        {

            _Alertas.Clear();

            if (_nrMatricula == "")
            {
                return;
            }
            cSiarco dadosSiarco = new cSiarco(_nrMatricula);

            if (dadosSiarco.Socios == null)
            {
                return;
            }


            #region Verificar CNPJ da EMPRESA
            if (dadosSiarco.CNPJ != _nrEmpresaCNPJ)
            {
                AddAlerta(3, _nrEmpresaCNPJ);
            }
            #endregion



            #region Verificação do QSA
            // Caso seja alteracao ou exclusao de socio


            bool socioExiste = false;

            foreach (bSocios SocioReq in SociosAtivos)
            {
                if (SocioReq.tipoacao != 1) // 1 = Inclusão
                {//caso seja alteracao ou exclusao
                    socioExiste = false;
                    foreach (cSiarcoSocio SocioSiarco in dadosSiarco.Socios)
                    {
                        if (SocioSiarco.CNPJ == SocioReq.CPFCNPJ && String.IsNullOrEmpty(SocioSiarco.DataSaida))
                        {
                            socioExiste = true;
                            break;
                        }

                    }
                    if (!socioExiste)
                    {
                        AddAlerta(2, SocioReq.CPFCNPJ);
                        socioExiste = false;
                    }
                }
                else // Caso seja inclusao verificar existencia do socio
                {
                    foreach (cSiarcoSocio SocioSiarco in dadosSiarco.Socios)
                    {
                        if (SocioSiarco.CNPJ == SocioReq.CPFCNPJ && String.IsNullOrEmpty(SocioSiarco.DataSaida))
                        {
                            AddAlerta(5, SocioReq.CPFCNPJ);
                        }
                    }
                }


            }

            #endregion

            _Alertas.Clear();

            _Alertas = bAlertaRequerimento.GetAlertas(_ProtocoloRequerimento, _CNPJ_Orgao_Registro);


        }

        /// <summary>
        /// Adiciona alertas na classe requerimento
        /// </summary>
        private void AddAlerta(int Tipo, string valor)
        {
            bAlertaRequerimento ar = new bAlertaRequerimento();


            using (dt018_alerta_requerimento c = new dt018_alerta_requerimento())
            {
                c.T018_T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                c.T018_T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                c.T018_t097_id_alerta = Tipo;
                c.T018_valor = valor;

                //c.T018_T004_NR_CNPJ_ORG_REG = _CNPJ_Orgao_Registro;
                //c.T018_T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                //c.T018_t097_id_alerta = Tipo;
                //c.T018_valor = valor;
                c.Update();

            }
            //_Alertas.Add(ar);
        }

        #region Capa do processo

        public List<bAtoEventoCapa> getAtosEventosCapa()
        {
            return getAtosEventosCapa(3);
        }
        /// <summary>
        /// Retorna os eventos do contrato 1- só da matriz; 2 - só da Filial , 3 - Todos
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<bAtoEventoCapa> getAtosEventosCapa(int tipo)
        {
            /*Eventos Atos de Alteração
                 * 2- Alteração
                 *      020 - ALTERAÇÃO  DE NOME EMPRESARIAL
                 *      021 - ALTERAÇÃO  DE DADOS (EXCETO NOME EMPRESARIAL) 
                 *      022 - ALTERAÇÃO  DE DADOS E DE NOME EMPRESARIA
                 *      023	- ABERTURA   DE FILIAL NA UF DA SEDE
                 *	    024	- ALTERAÇÃO  DE FILIAL NA UF DA SEDE
                 *	    025	- EXTINÇÃO   DE FILIAL NA UF DA SEDE
                 *	    026	- ABERTURA   DE FILIAL EM OUTRA UF
                 *	    027	- ALTERAÇÃO  DE FILIAL EM OUTRA UF
                 *	    028	- EXTINÇÃO   DE FILIAL EM OUTRA UF
                 *	    029	- ABERTURA   DE FILIAL COM SEDE EM OUTRA UF
                 *	    030	- ALTERAÇÃO  DE FILIAL COM SEDE EM OUTRA 
                 *	    031	- EXTINÇÃO   DE FILIAL COM SEDE EM OUTRA UF
                *   ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 029 - ABERTURA DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 030 – ALTERAÇÃO DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 031 - EXTINÇÃO DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 036 - TRANSFERÊNCIA DE FILIAL PARA OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 037 - INSCRIÇÃO DE TRANSFERÊNCIA DE FILIAL DE OUTRA UF

                */
            List<bAtoEventoCapa> listAtoEventoCapa = new List<bAtoEventoCapa>();

            bAtoEventoCapa capa = new bAtoEventoCapa();

            //Verifica se é evento de constituição ou de alteração da matriz
            if (tipo == 1 || tipo == 3)
            {
                if (getEventoAvalia("101"))
                {
                    switch (_TipoPessoaJuridicaCodigo)
                    {
                        case 6568:
                            capa.CodigoAto = "080";
                            capa.CodigoEvento = "080";
                            capa.DescricaoEvento = "Inscrição";
                            break;
                        case 6569:
                            capa.CodigoAto = "091";
                            capa.CodigoEvento = "091";
                            capa.DescricaoEvento = "Ato Constitutivo";
                            break;
                        case 1264:
                            capa.CodigoAto = "090";
                            capa.CodigoEvento = "090";
                            capa.DescricaoEvento = "Contrato";
                            break;

                    }
                    listAtoEventoCapa.Add(capa);
                }

                if (!getEventoAvalia("101"))
                {
                    capa.CodigoAto = "002";
                    capa.CodigoEvento = "";
                    capa.DescricaoEvento = "Alteração";
                    listAtoEventoCapa.Add(capa);

                    capa = new bAtoEventoCapa();
                    if (_t005_uf_origem == ConfigurationManager.AppSettings["UFPrincipal"].ToString())
                    {
                        if (_bProtocoloEvento.Count == 1 && getEventoAvalia("220"))
                        {
                            capa.CodigoEvento = "020";
                            capa.DescricaoEvento = "Alteração de Nome Empresarial";
                        }
                        if (_bProtocoloEvento.Count >= 1 && !getEventoAvalia("220"))
                        {
                            capa.CodigoEvento = "021";
                            capa.DescricaoEvento = "Alteração de Dados (Exceto Nome Empresarial)";
                        }
                        if (_bProtocoloEvento.Count > 1 && getEventoAvalia("220"))
                        {
                            capa.CodigoEvento = "022";
                            capa.DescricaoEvento = "Alteração de Dados e de Nome Empresarial";
                        }
                    }
                    else
                    {
                        if (getEventoAvalia("210"))
                        {
                            capa.CodigoEvento = "039";
                            capa.DescricaoEvento = "Inscrição de Transferência de outra UF";
                        }
                    }

                    listAtoEventoCapa.Add(capa);
                }
            }
            //Eventos de Filial
            if (tipo == 2 || tipo == 3)
            {
                DataTable dt = dHelperQuery.getEventosFilial(_ProtocoloRequerimento);
                bOrgaoRegistro orgaoRegistro = new bOrgaoRegistro(_CNPJ_Orgao_Registro);

                foreach (DataRow row in dt.Rows)
                {
                    capa = new bAtoEventoCapa();

                    switch (row["evento"].ToString())
                    {
                        case "102":
                            if (_t005_uf_origem == orgaoRegistro.uf) //Matriz está na UF da JUNTA
                            {
                                if (row["UF"].ToString() == orgaoRegistro.uf)
                                {
                                    capa.CodigoEvento = "023";
                                    capa.DescricaoEvento = "Abertura de Filial na UF da Sede";
                                }
                                else
                                {
                                    capa.CodigoEvento = "026";
                                    capa.DescricaoEvento = "Abertura de Filial em outra UF";
                                }
                            }
                            else
                            {
                                capa.CodigoEvento = "029";
                                capa.DescricaoEvento = "Abertura de Filial com Sede em outra UF";
                            }

                            break;
                        case "210":

                            //Transferencia de dilal de outra UF
                            if (_t005_uf_origem != orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "037";
                                capa.DescricaoEvento = "Inscrição de Transferência de Filial de outra UF";
                            }
                            else
                            {
                                if (row["UF"].ToString() == orgaoRegistro.uf)
                                {
                                    capa.CodigoEvento = "024";
                                    capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                                }
                                else
                                {
                                    capa.CodigoEvento = "027";
                                    capa.DescricaoEvento = "Alteração de Filial em outra UF";
                                }
                            }
                            break;
                        case "211":

                            //UF da sede <> Uf da JUNTA
                            if (_t005_uf_origem != orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "030";
                                capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                            }
                            else
                            {
                                if (row["UF"].ToString() == orgaoRegistro.uf)
                                {
                                    capa.CodigoEvento = "024";
                                    capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                                }
                                else
                                {
                                    capa.CodigoEvento = "027";
                                    capa.DescricaoEvento = "Alteração de Filial em outra UF";
                                }
                            }
                            break;
                        case "209":
                            //UF da sede <> Uf da JUNTA
                            if (_t005_uf_origem != orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "030";
                                capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                            }
                            else
                            {
                                if (row["UF"].ToString() == orgaoRegistro.uf)
                                {
                                    capa.CodigoEvento = "024";
                                    capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                                }
                                else
                                {
                                    capa.CodigoEvento = "027";
                                    capa.DescricaoEvento = "Alteração de Filial em outra UF";
                                }
                            }
                            break;
                        case "244":
                            //024	- ALTERAÇÃO DE FILIAL NA UF DA SEDE
                            if (row["UF"].ToString() == _t005_uf_origem)
                            {
                                capa.CodigoEvento = "024";
                                capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                            }
                            //027	- ALTERAÇÃO DE FILIAL EM OUTRA UF
                            if (row["UF"].ToString() != orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "030";
                                capa.DescricaoEvento = "Alteração de Filial em outra UF";
                            }
                            //030	- ALTERAÇÃO DE FILIAL COM SEDE EM OUTRA UF
                            if (row["UF"].ToString() != _t005_uf_origem)
                            {
                                capa.CodigoEvento = "030";
                                capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                            }
                            break;
                        case "517":
                            if (row["UF"].ToString() == _t005_uf_origem)
                            {
                                capa.CodigoEvento = "025";
                                capa.DescricaoEvento = "Extinção de Filial na UF da Sede";
                            }
                            else
                            {
                                capa.CodigoEvento = "028";
                                capa.DescricaoEvento = "Extinção de Filial em outra UF";

                            }
                            break;
                    }
                    listAtoEventoCapa.Add(capa);
                }
            }

            if (_eventoConsolidacao == 1)
            {
                capa = new bAtoEventoCapa();
                capa.CodigoAto = "002";
                capa.CodigoEvento = "051";
                capa.DescricaoEvento = "Consolidação";
                listAtoEventoCapa.Add(capa);
            }
            if (_eventoReativacao == 1)
            {
                capa = new bAtoEventoCapa();
                capa.CodigoAto = "002";
                capa.CodigoEvento = "052";
                capa.DescricaoEvento = "Reativação";
                listAtoEventoCapa.Add(capa);
            }
            if (_eventoReRatificacao == 1)
            {
                capa = new bAtoEventoCapa();
                capa.CodigoAto = "002";
                capa.CodigoEvento = "048";
                capa.DescricaoEvento = "Rerratificação";
                listAtoEventoCapa.Add(capa);
            }
            return listAtoEventoCapa;
        }


        public List<bAtoEventoCapa> getAtosEventosCapaFilial(int sqPessoa)
        {



            /*Eventos Atos de Alteração
                 * 2- Alteração
                 *      020 - ALTERAÇÃO  DE NOME EMPRESARIAL
                 *      021 - ALTERAÇÃO  DE DADOS (EXCETO NOME EMPRESARIAL) 
                 *      022 - ALTERAÇÃO  DE DADOS E DE NOME EMPRESARIA
                 *      023	- ABERTURA   DE FILIAL NA UF DA SEDE
                 *	    024	- ALTERAÇÃO  DE FILIAL NA UF DA SEDE
                 *	    025	- EXTINÇÃO   DE FILIAL NA UF DA SEDE
                 *	    026	- ABERTURA   DE FILIAL EM OUTRA UF
                 *	    027	- ALTERAÇÃO  DE FILIAL EM OUTRA UF
                 *	    028	- EXTINÇÃO   DE FILIAL EM OUTRA UF
                 *	    029	- ABERTURA   DE FILIAL COM SEDE EM OUTRA UF
                 *	    030	- ALTERAÇÃO  DE FILIAL COM SEDE EM OUTRA 
                 *	    031	- EXTINÇÃO   DE FILIAL COM SEDE EM OUTRA UF
                *   ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 029 - ABERTURA DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 030 – ALTERAÇÃO DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 031 - EXTINÇÃO DE FILIAL COM SEDE EM OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 036 - TRANSFERÊNCIA DE FILIAL PARA OUTRA UF
                    ATO 310 - OUTROS DOCUMENTOS DE INTERESSE DA EMPRESA e EVENTO 037 - INSCRIÇÃO DE TRANSFERÊNCIA DE FILIAL DE OUTRA UF

                */
            List<bAtoEventoCapa> listAtoEventoCapa = new List<bAtoEventoCapa>();

            bAtoEventoCapa capa = new bAtoEventoCapa();

            DataTable dt = dHelperQuery.getEventosFilialBySqPessoa(sqPessoa);
            bOrgaoRegistro orgaoRegistro = new bOrgaoRegistro(_CNPJ_Orgao_Registro);

            foreach (DataRow row in dt.Rows)
            {
                capa = new bAtoEventoCapa();

                switch (row["evento"].ToString())
                {
                    case "102":
                        if (_t005_uf_origem == orgaoRegistro.uf) //Matriz está na UF da JUNTA
                        {
                            if (row["UF"].ToString() == orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "023";
                                capa.DescricaoEvento = "Abertura de Filial na UF da Sede";
                            }
                            else
                            {
                                capa.CodigoEvento = "026";
                                capa.DescricaoEvento = "Abertura de Filial em outra UF";
                            }
                        }
                        else
                        {
                            capa.CodigoEvento = "029";
                            capa.DescricaoEvento = "Abertura de Filial com Sede em outra UF";
                        }

                        break;
                    case "210":

                        //Transferencia de filial de outra UF
                        if (row["uf_origem"].ToString() != orgaoRegistro.uf)
                        {
                            capa.CodigoEvento = "037";
                            capa.DescricaoEvento = "Inscrição de Transferência de Filial de outra UF";
                        }
                        else
                        {
                            if (row["UF"].ToString() == orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "024";
                                capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                            }
                            else
                            {
                                capa.CodigoEvento = "027";
                                capa.DescricaoEvento = "Alteração de Filial em outra UF";
                            }
                        }
                        break;
                    case "211":

                        //UF da sede <> Uf da JUNTA
                        if (_t005_uf_origem != orgaoRegistro.uf)
                        {
                            capa.CodigoEvento = "030";
                            capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                        }
                        else
                        {
                            if (row["UF"].ToString() == orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "024";
                                capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                            }
                            else
                            {
                                capa.CodigoEvento = "027";
                                capa.DescricaoEvento = "Alteração de Filial em outra UF";
                            }
                        }
                        break;
                    case "209":
                        //UF da sede <> Uf da JUNTA
                        if (_t005_uf_origem != orgaoRegistro.uf)
                        {
                            capa.CodigoEvento = "030";
                            capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                        }
                        else
                        {
                            if (row["UF"].ToString() == orgaoRegistro.uf)
                            {
                                capa.CodigoEvento = "024";
                                capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                            }
                            else
                            {
                                capa.CodigoEvento = "027";
                                capa.DescricaoEvento = "Alteração de Filial em outra UF";
                            }
                        }
                        break;
                    case "244":
                        //024	- ALTERAÇÃO DE FILIAL NA UF DA SEDE
                        if (row["UF"].ToString() == _t005_uf_origem)
                        {
                            capa.CodigoEvento = "024";
                            capa.DescricaoEvento = "Alteração de Filial na UF da Sede";
                        }
                        //027	- ALTERAÇÃO DE FILIAL EM OUTRA UF
                        if (row["UF"].ToString() != orgaoRegistro.uf)
                        {
                            capa.CodigoEvento = "030";
                            capa.DescricaoEvento = "Alteração de Filial em outra UF";
                        }
                        //030	- ALTERAÇÃO DE FILIAL COM SEDE EM OUTRA UF
                        if (row["UF"].ToString() != _t005_uf_origem)
                        {
                            capa.CodigoEvento = "030";
                            capa.DescricaoEvento = "Alteração de Filial com Sede em outra UF";
                        }
                        break;
                    case "517":
                        if (row["UF"].ToString() == _t005_uf_origem)
                        {
                            capa.CodigoEvento = "025";
                            capa.DescricaoEvento = "Extinção de Filial na UF da Sede";
                        }
                        if (row["UF"].ToString() != orgaoRegistro.uf)
                        {
                            capa.CodigoEvento = "028";
                            capa.DescricaoEvento = "Extinção de Filial em outra UF";
                        }
                        if ((_t005_uf_origem != orgaoRegistro.uf) && (row["UF"].ToString() != _t005_uf_origem))
                        {
                            capa.CodigoEvento = "031";
                            capa.DescricaoEvento = "Extinção de Filial com Sede em outra UF";
                        }
                        break;
                }
                listAtoEventoCapa.Add(capa);
            }

            return listAtoEventoCapa;
        }


        public List<bAtoEventoCapa> getAtosEventosEnquadramento()
        {
            /*Eventos Atos de Alteração

                * Enquadramento ME	            315	Enquadramento MICROEMPRESA
                * Enquadramento EPP	            316	Enquadramento EMPRESA DE PEQUENO PORTE
                * Reenquadramneto ME para EPP	307	REENQUADRAMENTO DE MICROEMPRESA COMO EMPRESA DE PEQUENO PORTE 
                * Reenquadramneto EPP para ME	309	REENQUADRAMENTO DE EMPRESA DE PEQUENO PORTE COMO MICROEMPRESA 
                * Desenquadramento de ME	    317	DESENQUADRAMENTO DE MICROEMPRESA
                * Desenquadramento de EPP	    318	DESENQUADRAMENTO DE EMPRESA DE PEQUENO PORTE

                */
            List<bAtoEventoCapa> listAtoEventoCapa = new List<bAtoEventoCapa>();

            bAtoEventoCapa capa = new bAtoEventoCapa();
            //Verifica se é evento de constituição ou de alteração
            if (!IsProtocoloIncorporacao())
            {
                bReqGenProtocolo obj = new bReqGenProtocolo(_ProtocoloRequerimento);
                if (obj.T099_IN_TIPO_ENQUADRAMENTO != 0)
                {
                    switch (obj.T099_IN_TIPO_ENQUADRAMENTO)
                    {
                        case 2:
                            capa.CodigoAto = "307";
                            capa.DescricaoEvento = "Reenquadramento de microempresa como empresa de pequeno porte";
                            break;
                        case 3:
                            capa.CodigoAto = "309";
                            capa.DescricaoEvento = "Reenquadramento de empresa de pequeno porte como microempresa";
                            break;
                        case 4:
                            capa.CodigoAto = "317";
                            capa.DescricaoEvento = "Desenquadramento de microempresa";
                            break;
                        case 5:
                            capa.CodigoAto = "318";
                            capa.DescricaoEvento = "Desenquadramento de empresa de pequeno porte";
                            break;
                    }
                }

            }
            else
            {
                if (_Enquadramento != 0 && _Enquadramento != 1290)
                {

                    if (_Enquadramento == 1118)
                    {
                        capa.CodigoAto = "316";
                        capa.DescricaoEvento = "Enquadramento empresa de pequeno porte";

                    }
                    if (_Enquadramento == 1119)
                    {
                        capa.CodigoAto = "315";
                        capa.DescricaoEvento = "Enquadramento microempresa";
                    }
                }
            }

            listAtoEventoCapa.Add(capa);

            return listAtoEventoCapa;
        }

        private bool getEventoAvalia(string TipoDeEvento)
        {
            for (int i = 0; i < _bProtocoloEvento.Count; i++)
            {
                if (_bProtocoloEvento[i].CodigoEvento == decimal.Parse(TipoDeEvento))
                {
                    return true;
                }
            }
            return false;
        }

        private bool getEventoAvaliaByRequerimento(bRequerimento requerimento, string TipoDeEvento)
        {
            for (int i = 0; i < requerimento.ProtocoloEvento.Count; i++)
            {
                if (requerimento.ProtocoloEvento[i].CodigoEvento == decimal.Parse(TipoDeEvento))
                {
                    return true;
                }
            }
            return false;
        }

        private bool VerificaSeTemEventodeEndereco(bRequerimento requerimento)
        {
            for (int i = 0; i < requerimento.ProtocoloEvento.Count; i++)
            {
                if (requerimento.ProtocoloEvento[i].CodigoEvento.ToString() == "209"
                    || requerimento.ProtocoloEvento[i].CodigoEvento.ToString() == "210"
                    || requerimento.ProtocoloEvento[i].CodigoEvento.ToString() == "211")
                {

                    return true;
                }
            }
            return false;

        }

        private bool VerificaSeTemEventodeEndereco()
        {
            for (int i = 0; i < _bProtocoloEvento.Count; i++)
            {
                if (_bProtocoloEvento[i].CodigoEvento.ToString() == "209"
                    || _bProtocoloEvento[i].CodigoEvento.ToString() == "210"
                    || _bProtocoloEvento[i].CodigoEvento.ToString() == "211")
                {

                    return true;
                }
            }
            return false;

        }

        public void ExcluirTodasExigencias()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.ExcluiExigencias(_ProtocoloRequerimento);
                p.ExcluiExigenciasOutras(_ProtocoloRequerimento);
            }
        }

        #endregion

        public bool RequerimentoFilial()
        {
            if (!VerficaEventoMatriz() && VerficaEventoFilial())
            {
                return true;
            }

            return false;
        }
        public bool VerficaEventoFilial()
        {
            bool ret = false;
            foreach (bProtocoloEvento c in _bProtocoloEvento)
            {
                if (c.SqPessoa == _CodigoEmpresa)
                {
                    if (c.CodigoEvento.ToString() == "102")
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }
        public bool VerficaEventoMatriz()
        {
            bool ret = false;
            foreach (bProtocoloEvento c in _bProtocoloEvento)
            {
                if (c.SqPessoa == _CodigoEmpresa)
                {
                    if (c.CodigoEvento.ToString() != "102" && c.CodigoEvento.ToString() != "222")
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }
        public bool VerficaEventoEnquadramento()
        {
            bool ret = false;
            foreach (bProtocoloEvento c in _bProtocoloEvento)
            {
                if (c.CodigoEvento.ToString() == "222")
                {
                    ret = true;
                }
            }
            return ret;
        }

        public void TrocaViabilidadeRequerimento(string pNumRequerimentoAtual, string pNumRequerimentoNovo, string pUsuario, int pAtualizaSocio)
        {
            using (dHelperQuery dh = new dHelperQuery())
            {
                dh.Sp_processoAlterViabilidade(pNumRequerimentoAtual, pNumRequerimentoNovo, pUsuario, pAtualizaSocio);
            }

        }

        public string ValidaNaturezaViabilidade(string grupo)
        {
            return dHelperQuery.VerficaNaturezaViabilidade(_NaturezaJuridicaCodigo.ToString(), grupo);
        }

        public bSocios FindSocio(string cpf)
        {
            foreach (bSocios c in _Socios)
            {
                if (c.CPFCNPJ == cpf)
                {
                    return c;
                }
            }

            return new bSocios();
        }

        public void UpdateEventoCapa(string numProtocoloOR)
        {
            dr013_Requerimento_Evento evento = new dr013_Requerimento_Evento();
            evento.t004_nr_cnpj_org_reg = _CNPJ_Orgao_Registro;
            evento.T005_nr_protocolo = _ProtocoloRequerimento;

            evento.Delete(numProtocoloOR);

            foreach (bAtoEventoCapa c in _eventoCapa)
            {
                if (c.ProtocoloOR == numProtocoloOR)
                {
                    c.Update();
                }
            }
        }

        public void ClearEventoByProtocolo(string protocolo)
        {

            List<bAtoEventoCapa> _copia = new List<bAtoEventoCapa>();
            foreach (bAtoEventoCapa cc in _eventoCapa)
            {
                _copia.Add(cc);
            }

            foreach (bAtoEventoCapa c in _copia)
            {
                if (c.ProtocoloOR == protocolo)
                {
                    _eventoCapa.Remove(c);
                }
                if (_copia.Count == 0)
                {
                    break;
                }
            }

        }
        public List<bAtoEventoCapa> GetEventoByProtocolo(string protocolo)
        {
            List<bAtoEventoCapa> evento = new List<bAtoEventoCapa>();
            foreach (bAtoEventoCapa c in _eventoCapa)
            {
                if (c.ProtocoloOR == protocolo)
                {
                    evento.Add(c);
                }
            }

            return evento;
        }

        #region Comparação DBE Viabilidade
        public void CarregaDadosComparacaoViabilidade(DataSet result)
        {
            CarregaDadosComparacaoViabilidade(result, true);
        }

        public void CarregaDadosComparacaoViabilidade_old(DataSet result, bool pAtualizoEventos)
        {
            //if (_viabilidade == null)
            //    _viabilidade = new bRequerimento();
            if (result.Tables.Count == 0)
                return;

            DataTable ViaProtocolo = result.Tables["VIA_PROTOCOLO_VIAB"];
            if (ViaProtocolo != null)
            {
                #region Requerente
                _RequerenteNome = ViaProtocolo.Rows[0]["VPV_NOM_SOLIC"].ToString();
                _RequerenteCPF = ViaProtocolo.Rows[0]["VPV_VSV_CPF_CNPJ_SOLIC"].ToString().Trim();
                _RequerenteEmail = ViaProtocolo.Rows[0]["VPV_EMAIL_SOLIC"].ToString();
                #endregion

                #region Nome empresarial
                DataTable ViaRazao = result.Tables["VIA_PROT_RAZAO_SOCIAL"];
                if (ViaRazao != null)
                {
                    _SedeNome = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();
                    _SedeNome = RetiraTipoEnquadramento(_SedeNome);

                }

                #endregion

                #region Endereço da Empresa
                _SedeCEP = ViaProtocolo.Rows[0]["VPV_CEP"].ToString();
                _SedeUF = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                _SedeMunicipio = ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"].ToString();
                _nomeMunicipioSede = dHelperQuery.BuscarDescricaoMunicipio(_SedeMunicipio);
                _SedeBairro = ViaProtocolo.Rows[0]["VPV_BAIRRO"].ToString();
                _SedeTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                _SedeDsTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                _SedeLogradouro = ViaProtocolo.Rows[0]["VPV_LOGRADORO"].ToString();
                _SedeNumero = ViaProtocolo.Rows[0]["VPV_NUM_LOGRADOURO"].ToString();
                _SedeComplemento = ViaProtocolo.Rows[0]["VPV_COMP_LOGRADOURO"].ToString();
                #endregion

                #region Objeto Social/CANE
                _ObjetoSocial = ViaProtocolo.Rows[0]["VPV_OBJETIVO"].ToString();

                DataTable ViaCnaeAux = result.Tables["VIA_PROT_CNAE"];
                _CNAEs.Clear();

                //Inserir na lista primeiro a CNAE principal
                foreach (DataRow r in ViaCnaeAux.Rows)
                {
                    if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 1)
                    {
                        bCNAE c = new bCNAE();
                        c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                        c.Descricao = r["VPC_TAE_DESC"].ToString();
                        c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                        c.Versao = "V02";
                        //c.Update();
                        c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                        _CNAEs.Add(c);

                        break;
                    }


                }

                //Inserir na lista as CNAEs secundarias
                foreach (DataRow r in ViaCnaeAux.Rows)
                {
                    if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 2)
                    {
                        bCNAE c = new bCNAE();
                        c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                        c.Descricao = r["VPC_TAE_DESC"].ToString();
                        c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                        c.Versao = "V02";
                        //c.Update();
                        c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                        _CNAEs.Add(c);
                    }
                }

                #endregion


                #region Eventos
                if (pAtualizoEventos)
                {
                    DataTable ViaProtEvento = result.Tables["VIA_PROT_EVENTO"];
                    _bProtocoloEvento.Clear();

                    if (ViaProtEvento != null)
                    {
                        foreach (DataRow r in ViaProtEvento.Rows)
                        {
                            bProtocoloEvento ev = new bProtocoloEvento();
                            ev.CodigoEvento = Decimal.Parse(r["PEV_COD_EVENTO"].ToString());

                            _bProtocoloEvento.Add(ev);
                        }

                    }
                }
                #endregion


                #region Socios
                DataTable temp = result.Tables["VIA_PROT_SOCIOS"];
                DataView dv = temp.DefaultView;
                dv.Sort = "VPS_CPF_CNPJ_SOCIO asc";
                DataTable socios = dv.ToTable();
                _Socios.Clear();
                foreach (DataRow s in socios.Rows)
                {
                    bSocios ns = new bSocios();
                    ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                    ns.Nome = s["VPS_NOME_SOCIO"].ToString();
                    ns.Nome_Mae = s["VPS_NOME_MAE"].ToString();

                    if (_TipoPessoaJuridicaCodigo == 1264)
                        ns.Qualificacao = "22";

                    ns.Qualificacao_Descricao = "Sócio";
                    ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                    if (ns.CPFCNPJ.Trim().Length == 11)
                        ns.TipoPessoa = "F";
                    else
                        ns.TipoPessoa = "J";
                    ns.ObrigacoesSociais = "S";
                    ns.tipoacao = 3;
                    _Socios.Add(ns);
                }
                #endregion
            }
        }

        public void CarregaDadosComparacaoViabilidade(DataSet result, bool pAtualizoEventos)
        {
            if (_viabilidade == null)
                _viabilidade = new bRequerimento();
            if (result.Tables.Count == 0)
                return;

            DataTable ViaProtocolo = result.Tables["VIA_PROTOCOLO_VIAB"];
            if (ViaProtocolo != null)
            {
                #region Requerente
                //Viabilidade.RequerenteNome = ViaProtocolo.Rows[0]["VPV_NOM_SOLIC"].ToString();
                //Viabilidade.RequerenteCPF = ViaProtocolo.Rows[0]["VPV_VSV_CPF_CNPJ_SOLIC"].ToString().Trim();
                //Viabilidade.RequerenteEmail = ViaProtocolo.Rows[0]["VPV_EMAIL_SOLIC"].ToString();
                #endregion

                #region Eventos
                if (pAtualizoEventos)
                {
                    DataTable ViaProtEvento = result.Tables["VIA_PROT_EVENTO"];
                    Viabilidade.ProtocoloEvento.Clear();

                    if (ViaProtEvento != null)
                    {
                        foreach (DataRow r in ViaProtEvento.Rows)
                        {
                            bProtocoloEvento ev = new bProtocoloEvento();
                            ev.CodigoEvento = Decimal.Parse(r["PEV_COD_EVENTO"].ToString());

                            Viabilidade.ProtocoloEvento.Add(ev);
                        }

                    }
                }
                #endregion

                #region Nome empresarial
                DataTable ViaRazao = result.Tables["VIA_PROT_RAZAO_SOCIAL"];
                if (ViaRazao != null)
                {
                    Viabilidade.SedeNome = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();
                    //retirar o enquadramento quando for constituição
                    if (getEventoAvalia("101"))
                    {
                        Viabilidade.SedeNome = RetiraTipoEnquadramento(Viabilidade.SedeNome);
                    }
                }

                #endregion

                #region Endereço da Empresa
                Viabilidade.SedeCEP = ViaProtocolo.Rows[0]["VPV_CEP"].ToString();
                Viabilidade.SedeUF = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                Viabilidade.SedeMunicipio = ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"].ToString();
                Viabilidade.SedeNomeMunicipio = dHelperQuery.BuscarDescricaoMunicipio(Viabilidade.SedeMunicipio);
                Viabilidade.SedeBairro = ViaProtocolo.Rows[0]["VPV_BAIRRO"].ToString();
                Viabilidade.SedeTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                Viabilidade.SedeDsTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                Viabilidade.SedeLogradouro = ViaProtocolo.Rows[0]["VPV_LOGRADORO"].ToString();
                Viabilidade.SedeNumero = ViaProtocolo.Rows[0]["VPV_NUM_LOGRADOURO"].ToString();
                Viabilidade.SedeComplemento = ViaProtocolo.Rows[0]["VPV_COMP_LOGRADOURO"].ToString();
                #endregion

                #region Objeto Social/CANE
                Viabilidade.ObjetoSocial = ViaProtocolo.Rows[0]["VPV_OBJETIVO"].ToString();

                #endregion

                #region CNAE
                DataTable ViaCnaeAux = result.Tables["VIA_PROT_CNAE"];
                Viabilidade.CNAEs.Clear();

                //Inserir na lista primeiro a CNAE principal
                foreach (DataRow r in ViaCnaeAux.Rows)
                {
                    if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 1)
                    {
                        bCNAE c = new bCNAE();
                        c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                        c.Descricao = r["VPC_TAE_DESC"].ToString();
                        c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                        c.Versao = "V02";
                        //c.Update();
                        c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                        Viabilidade.CNAEs.Add(c);

                        break;
                    }


                }


                //Inserir na lista as CNAEs secundarias
                foreach (DataRow r in ViaCnaeAux.Rows)
                {
                    if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 2)
                    {
                        bCNAE c = new bCNAE();
                        c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                        c.Descricao = r["VPC_TAE_DESC"].ToString();
                        c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                        c.Versao = "V02";
                        //c.Update();
                        c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                        Viabilidade.CNAEs.Add(c);
                    }
                }


                #endregion

                #region Socios
                DataTable temp = result.Tables["VIA_PROT_SOCIOS"];
                if (temp != null)
                {
                    DataView dv = temp.DefaultView;
                    dv.Sort = "VPS_CPF_CNPJ_SOCIO asc";
                    DataTable socios = dv.ToTable();
                    Viabilidade.Socios.Clear();
                    foreach (DataRow s in socios.Rows)
                    {
                        bSocios ns = new bSocios();
                        ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                        ns.Nome = s["VPS_NOME_SOCIO"].ToString();
                        ns.Nome_Mae = s["VPS_NOME_MAE"].ToString();

                        if (Viabilidade.TipoPessoaJuridicaCodigo == 1264)
                            ns.Qualificacao = "22";

                        ns.Qualificacao_Descricao = "Sócio";
                        ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                        if (ns.CPFCNPJ.Trim().Length == 11)
                            ns.TipoPessoa = "F";
                        else
                            ns.TipoPessoa = "J";
                        ns.ObrigacoesSociais = "S";
                        ns.tipoacao = 3;
                        Viabilidade.Socios.Add(ns);
                    }
                }
                #endregion
            }
        }

        public void GetDBEaaa()
        {
            DataSet dsDBE = new DataSet();
            dsDBE = GetDataSetDBE(_CodigoDBE);
            CarregaDadosComparacaoDBE(dsDBE);

        }

        public void GetDBE(DataSet dsDBE)
        {
            CarregaDadosComparacaoDBE(dsDBE);
        }

        public void CarregaDadosComparacaoDBE(DataSet Ds)
        {
            DataTable result = new DataTable();
            DataTable DadosDBEControl = new DataTable();

            DadosDBEControl = Ds.Tables["DadosDBEControl"];

            if (_dbe == null)
                _dbe = new bRequerimento();


            #region Nome empresarial, CNAE Principal e Objeto Social

            result = Ds.Tables["DadosFCPJ"];
            if (result != null && result.Rows.Count > 0)
            {
                #region Dados da Empresa


                DBE.ObjetoSocial = result.Rows[0]["t73302_objeto_social"].ToString();
                DBE.SedeNome = result.Rows[0]["t73302_nom_empresarial"].ToString();
                DBE.NaturezaJuridicaCodigo = int.Parse(result.Rows[0]["t73302_nat_juridica"].ToString());

                //DBE.SedeNome = RetiraTipoEnquadramento(DBE.SedeNome);
                DBE.Nome_Fantasia = result.Rows[0]["t73302_nom_fantasia"].ToString();

                using (dR006_Natureza_Juridica_Tipo njt = new dR006_Natureza_Juridica_Tipo())
                {
                    DataTable dtBusca = njt.EncontraTipoPessoaJuridica(DBE.NaturezaJuridicaCodigo);
                    if (dtBusca.Rows.Count > 0)
                    {
                        DBE.TipoPessoaJuridicaCodigo = int.Parse(dtBusca.Rows[0]["a018_co_tipo_pessoa_juridica"].ToString());
                        DBE.TipoPessoaJuridicaDescricao = dtBusca.Rows[0]["a018_ds_tipo_pessoa_Juridica"].ToString();
                        DBE.NaturezaJuridicaDescricao = "";
                    }
                }

                if (result.Rows[0]["t73302_capital_social"].ToString() != "")
                {
                    DBE.CapitalSocial = decimal.Parse(result.Rows[0]["t73302_capital_social"].ToString());
                }
                DBE.nrMatricula = result.Rows[0]["t73302_nire"].ToString();


                DBE.nrEmpresaCNPJ = DadosDBEControl.Rows[0]["t73300_cnpj_empresa"].ToString();

                DBE.SedeDDD = result.Rows[0]["t73302_ddd_telefone_1"].ToString();
                DBE.SedeTelefone = result.Rows[0]["t73302_telefone_1"].ToString();
                DBE.SedeEmail = result.Rows[0]["t73302_correio_eletronico"].ToString();
                //PORTE DA EMPRESA (DE-PARA)
                //DBE ==>   01 – MICRO-EMPRESA, 03 – EMPRESA PEQUENO PORTE, 05 – DEMAIS
                //REQ ==>   1119 - ME                   1118 - EPP          1120 - NORMAL  
                string porte = result.Rows[0]["t73302_porte_empresa"].ToString();
                if (porte != "")
                {
                    switch (porte)
                    {
                        case "01":
                            DBE.Enquadramento = 1119;
                            break;
                        case "03":
                            DBE.Enquadramento = 1118;
                            break;
                        case "05":
                            DBE.Enquadramento = 1120;
                            break;
                        default:
                            DBE.Enquadramento = 1120;
                            break;
                    }

                }

                DBE.ObjetoSocial = result.Rows[0]["t73302_objeto_social"].ToString();

                #endregion





                //CNAE PRINCIPAL
                #region CNAE Principal

                DBE.CNAEs.Clear();
                bCNAE c = new bCNAE();
                if (result.Rows[0]["t73302_cnae_principal"].ToString() != "")
                {
                    c.CodigoCNAE = result.Rows[0]["t73302_cnae_principal"].ToString();
                    //c.Descricao = r["VPC_TAE_DESC"].ToString();
                    c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                    c.Versao = "V02";
                    c.TipoAtividade = 36;
                    DBE.CNAEs.Add(c);
                }
                #endregion
            }

            #endregion

            #region Endereço da Empresa

            if (result != null && result.Rows.Count > 0)
            {
                DBE.SedeCEP = result.Rows[0]["t73302_cep"].ToString();
                DBE.SedeUF = result.Rows[0]["t73302_uf"].ToString();
                DBE.SedeMunicipio = result.Rows[0]["t73302_cod_munic"].ToString();
                if (DBE.SedeMunicipio != "")
                {
                    DBE.SedeNomeMunicipio = dHelperQuery.BuscarDescricaoMunicipio(DBE.SedeMunicipio);
                }
                DBE.SedeBairro = result.Rows[0]["t73302_bairro"].ToString();
                DBE.SedeTipoLogradouro = dHelperQuery.GetTipoLogradouroDBE(result.Rows[0]["t73302_tip_logradouro"].ToString());
                DBE.SedeDsTipoLogradouro = dHelperQuery.GetTipoLogradouroDBE(result.Rows[0]["t73302_tip_logradouro"].ToString());
                DBE.SedeLogradouro = result.Rows[0]["t73302_nom_logradouro"].ToString();
                DBE.SedeNumero = result.Rows[0]["t73302_num_logradouro"].ToString();
                DBE.SedeComplemento = result.Rows[0]["t73302_complemento_logradouro"].ToString();
            }
            #endregion


            #region Objeto Social/CANE

            result = Ds.Tables["DadosCNAE"];

            if (result != null && result.Rows.Count > 0)
            {


                DataTable ViaCnaeAux = result;
                //DBE.CNAEs.Clear();
                foreach (DataRow r in ViaCnaeAux.Rows)
                {
                    bCNAE c = new bCNAE();
                    c.CodigoCNAE = r["t73304_cnae_secundaria"].ToString();
                    //c.Descricao = r["VPC_TAE_DESC"].ToString();
                    c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                    c.Versao = "V02";
                    c.TipoAtividade = 37;//decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                    DBE.CNAEs.Add(c);
                }
            }

            #endregion


            #region Eventos

            result = Ds.Tables["DadosEventos"];
            DataTable ViaProtEvento = result;
            DBE.ProtocoloEvento.Clear();

            if (ViaProtEvento != null && ViaProtEvento.Rows.Count > 0)
            {
                foreach (DataRow r in ViaProtEvento.Rows)
                {
                    bProtocoloEvento ev = new bProtocoloEvento();
                    ev.CodigoEvento = Decimal.Parse(r["t73301_cod_evento"].ToString());
                    bAto ato = new bAto();
                    string descevento = ato.getAtoDescricao(ev.CodigoEvento.ToString());
                    ev.DescricaoEvento = descevento;

                    ev.CnpjOrgaoRegistro = _CNPJ_Orgao_Registro;
                    DBE.ProtocoloEvento.Add(ev);
                }

            }

            #endregion

            #region Requerente
            result = Ds.Tables["DadosContador"];
            if (result != null && result.Rows.Count > 0)
            {
                //_RequerenteNome = string.Empty;
                //_RequerenteCodigo = 0;
                //_RequerenteCPF = string.Empty;
                //_RequerenteDDD = string.Empty;
                //_RequerenteTelefone = string.Empty;
                //_RequerenteEmail = string.Empty;

                DBE.RequerenteNome = result.Rows[0]["t73305_nom_contador"].ToString();
                DBE.RequerenteCPF = result.Rows[0]["t73305_cpf_cnpj_contador"].ToString();
                DBE.RequerenteDDD = result.Rows[0]["t73305_ddd_telefone"].ToString();
                DBE.RequerenteTelefone = result.Rows[0]["t73305_telefone"].ToString();
                DBE.RequerenteEmail = result.Rows[0]["t73305_correio_eletronico"].ToString();

                DBE.Contabilista.cpfCnpj = result.Rows[0]["t73305_cpf_cnpj_contador"].ToString();
                DBE.Contabilista.ds_Pessoa = result.Rows[0]["t73305_nom_contador"].ToString();

                DBE.Contabilista.co_CRC_Empresa = result.Rows[0]["t73305_seq_crc"].ToString();
                DBE.Contabilista.tip_CRC_Empresa = result.Rows[0]["t73305_tip_crc"].ToString();
                DBE.Contabilista.uf_CRC_Empresa = result.Rows[0]["t73305_uf_crc"].ToString();
                if (result.Rows[0]["t73305_cod_classific_empresa"].ToString() != "")
                    DBE.Contabilista.tip_Class_Empresa = Int32.Parse(result.Rows[0]["t73305_cod_classific_empresa"].ToString());
                else
                {
                    DBE.Contabilista.tip_Class_Empresa = 1;
                }
                DBE.Contabilista.co_CRC_Resp = result.Rows[0]["t73305_seq_crc_responsavel"].ToString();
                DBE.Contabilista.cpf_resp = result.Rows[0]["t73305_cpf_responsavel"].ToString();
                if (result.Rows[0]["t73305_dat_registro_crc"].ToString() != "")
                    DBE.Contabilista.DataInscricao = DateTime.Parse(result.Rows[0]["t73305_dat_registro_crc"].ToString());


                if (result.Rows[0]["t73305_cod_classific_contabilista"].ToString() != "")
                    DBE.Contabilista.tip_Class_Resp = Int32.Parse(result.Rows[0]["t73305_cod_classific_contabilista"].ToString());
                else
                {
                    DBE.Contabilista.tip_Class_Resp = 1;
                }


                DBE.Contabilista.tip_CRC_Resp = result.Rows[0]["t73305_tip_crc_responsavel"].ToString();
                DBE.Contabilista.uf_CRC_Resp = result.Rows[0]["t73305_uf_crc_responsavel"].ToString();



            }

            #endregion

            #region Socios


            result = Ds.Tables["DadosQSA"];
            DataTable socios = result;
            DBE.Socios.Clear();
            string _wtipoacao = "";
            if (socios != null && socios.Rows.Count > 0)
            {
                foreach (DataRow s in socios.Rows)
                {
                    bSocios ns = new bSocios();
                    ns.CPFCNPJ = s["t73303_cpf_cnpj_qsa"].ToString();
                    ns.Nome = s["t73303_nom_qsa"].ToString();
                    if (s["t73303_qualificacao_qsa"].ToString() == "49")
                    {
                        ns.Qualificacao = "22";
                        ns.Qualificacao_Descricao = "Sócio";
                        ns.rep_legal = 1;
                    }
                    else if (s["t73303_qualificacao_qsa"].ToString() == "65" || DBE.NaturezaJuridicaCodigo == 2305)
                    {
                        ns.Qualificacao = s["t73303_qualificacao_qsa"].ToString() == "" ? "65" : s["t73303_qualificacao_qsa"].ToString();
                        ns.Qualificacao_Descricao = "TITULAR";
                        ns.rep_legal = 0;
                    }
                    else
                    {
                        if (s["t73303_qualificacao_qsa"].ToString() != "")
                        {
                            if (s["t73303_qualificacao_qsa"].ToString() == "22")
                            {
                                if (ns.CPFCNPJ.Length > 11)
                                {
                                    ns.Qualificacao = "48";
                                }
                                else
                                {
                                    ns.Qualificacao = Int32.Parse(s["t73303_qualificacao_qsa"].ToString()).ToString();
                                    if (ns.Qualificacao == "5")
                                    {
                                        ns.rep_legal = 1;
                                    }
                                }

                            }
                            else
                            {
                                ns.Qualificacao = Int32.Parse(s["t73303_qualificacao_qsa"].ToString()).ToString();
                            }
                        }
                        else
                        {
                            ns.Qualificacao = "22";
                        }
                        ns.Qualificacao_Descricao = "Sócio";
                    }

                    if (ns.Qualificacao == "")
                    {
                        ns.Qualificacao = "22";
                    }



                    _wtipoacao = s["t73303_cod_evento"].ToString();
                    if (_wtipoacao != "" && _wtipoacao != "999")
                    {
                        ns.tipoacao = Int32.Parse(s["t73303_cod_evento"].ToString());
                        if (ns.tipoacao == 5)
                        {
                            ns.DataSaidaSocio = DateTime.Today;
                        }
                        if (ns.tipoacao == 1)
                        {
                            ns.EventoDBE = "Inclusão";
                        }
                        if (ns.tipoacao == 3)
                        {
                            ns.EventoDBE = "Alteração";
                        }
                        if (ns.tipoacao == 5)
                        {
                            ns.EventoDBE = "Baixa";
                        }
                    }

                    if (s["t73303_ind_cpf_cnpj_qsa"] == null || s["t73303_ind_cpf_cnpj_qsa"].ToString().Trim() == "")
                    {
                        if (ns.CPFCNPJ.Length == 14)
                        {
                            ns.TipoPessoa = "J";
                        }
                        else
                        {
                            ns.TipoPessoa = "F";
                        }
                    }
                    else
                    {
                        if (s["t73303_ind_cpf_cnpj_qsa"].ToString() == "2")
                            ns.TipoPessoa = "F";
                        else
                            ns.TipoPessoa = "J";
                    }

                    ns.ObrigacoesSociais = "S";

                    ns.DDD = s["t73303_ddd_telefone_qsa"].ToString();
                    ns.Telefone = s["t73303_telefone_qsa"].ToString();
                    ns.Email = s["t73303_correio_eletronico_qsa"].ToString();
                    //ns.CapitalIntegralizado = decimal.Parse("0" + s["t73303_capital_social_qsa"].ToString());
                    ns.PercentualCapital = decimal.Parse(s["t73303_perc_partic_qsa"].ToString());
                    if (ns.TipoPessoa == "F")
                    {
                        if (s["t73303_dt_nascimento_socio_pf"] != null && s["t73303_dt_nascimento_socio_pf"].ToString() != "")
                        {
                            ns.DataNascimento = DateTime.Parse(s["t73303_dt_nascimento_socio_pf"].ToString());
                        }
                    }

                    #region Endereço Sócio

                    ns.EndCEP = s["t73303_cep_qsa"].ToString();
                    ns.EndUF = s["t73303_uf_qsa"].ToString();
                    ns.EndMunicipio = s["t73303_cod_munic_qsa"].ToString();
                    ns.EndBairro = s["t73303_bairro_qsa"].ToString();
                    ns.EndTipoLogradouro = "0";// s["t73303_tip_lograd_qsa"].ToString();
                    ns.EndDsTipoLogradouro = dHelperQuery.GetTipoLogradouroDBE(s["t73303_tip_lograd_qsa"].ToString());
                    ns.EndLogradouro = s["t73303_lograd_qsa"].ToString();
                    ns.EndNumero = s["t73303_num_lograd_qsa"].ToString();
                    ns.EndComplemento = s["t73303_complemento_lograd_qsa"].ToString();

                    #endregion

                    #region Representante
                    if (s["t73303_cpf_rep_legal"] != null
                        && s["t73303_cpf_rep_legal"].ToString().Trim() != "")
                    {
                        bSocios rep = new bSocios();
                        rep.CPFCNPJ = s["t73303_cpf_rep_legal"].ToString();
                        rep.Nome = s["t73303_nom_rep_legal"].ToString();
                        rep.EndTipoLogradouro = "0";
                        rep.EndDsTipoLogradouro = dHelperQuery.GetTipoLogradouroDBE(s["t73303_tip_lograd_rep_legal"].ToString());
                        rep.EndLogradouro = s["t73303_lograd_rep_legal"].ToString();
                        rep.EndNumero = s["t73303_num_lograd_rep_legal"].ToString();
                        rep.EndComplemento = s["t73303_complemento_lograd_rep_"].ToString();
                        rep.EndBairro = s["t73303_bairro_rep_legal"].ToString();
                        rep.EndMunicipio = s["t73303_cod_munic_rep_legal"].ToString();
                        rep.EndCEP = s["t73303_cep_rep_legal"].ToString();
                        rep.EndUF = s["t73303_uf_rep_legal"].ToString();
                        rep.DDD = s["t73303_ddd_telefone_rep_legal"].ToString();
                        rep.Telefone = s["t73303_telefone_rep_legal"].ToString();
                        rep.Email = s["t73303_correio_eletronico_rep_"].ToString();
                        if (rep.CPFCNPJ.Length == 14)
                        {
                            rep.TipoPessoa = "J";
                        }
                        else
                        {
                            rep.TipoPessoa = "F";
                        }


                        ns.Representantes.Add(rep);
                    }

                    #endregion

                    DBE.Socios.Add(ns);

                    //Alteração 05/12/2014
                    //Se a qualificação for 49 criar o socio com a qualifiacação Admininstrador

                }
            }
            #endregion

        }

        private DataSet GetDataSetDBE(string _CodigoDBE)
        {
            DataSet Ds = new DataSet();

            DataTable result = new DataTable();
            int IdControlDBE = 0;

            result = dHelperQuery.getDadosDBEControl(_CodigoDBE);

            if (result != null && result.Rows.Count > 0)
            {
                IdControlDBE = int.Parse(result.Rows[0]["t73300_id_control"].ToString());

                //DadosDBEControl
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEFCPJ(IdControlDBE);

                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBECNAESecundaria(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEEventos(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEContador(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEQSA(IdControlDBE);
                Ds.Tables.Add(result);


            }

            return Ds;




        }
        public static DataSet CarregaDadosComparacaoDBE(string _CodigoDBE)
        {
            DataSet Ds = new DataSet();

            DataTable result = new DataTable();
            int IdControlDBE = 0;

            result = dHelperQuery.getDadosDBEControl(_CodigoDBE);

            if (result != null && result.Rows.Count > 0)
            {
                IdControlDBE = int.Parse(result.Rows[0]["t73300_id_control"].ToString());

                //DadosDBEControl
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEFCPJ(IdControlDBE);

                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBECNAESecundaria(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEEventos(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEContador(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEQSA(IdControlDBE);
                Ds.Tables.Add(result);


            }

            return Ds;




        }

        public void AcertaPorteNomeEmpresa()
        {
            string _nome = "";
            string _porte = "";

            _nome = RetiraTipoEnquadramento(_SedeNome);
            if (_reqGenprotocolo.T009_CO_PORTE == 1119)
            {
                _porte = "ME";
            }
            if (_reqGenprotocolo.T009_CO_PORTE == 1118)
            {
                _porte = "EPP";
            }
            _SedeNome = _nome + " " + _porte;
        }

        public string RetiraTipoEnquadramento(string wTexto)
        {
            wTexto = wTexto.Trim();

            if (!wTexto.Contains(" "))
            {
                return wTexto;
            }

            string wAux = wTexto + " ";
            string wEnquadramento = string.Empty;

            for (int wii = 0; wii <= tbEnquadramento.Length - 1; wii++)
            {
                wEnquadramento = tbEnquadramento[wii];
                int ii = wAux.IndexOf(tbEnquadramento[wii], wAux.Length - tbEnquadramento[wii].Length);
                if (ii != -1)
                {
                    wTexto = wTexto.Substring(0, wTexto.Length - tbEnquadramento[wii].Length + 1);
                    break;
                }
            }

            return wTexto;

        }

        public bool ComparaDBExViabilidade()
        {
            _DBEViab_Erro_Evento = new List<bool>();
            _DBEViab_Erro_NomeEmpresa = false;
            _DBEViab_Erro_TipoLogradouro = false;
            _DBEViab_Erro_Logradouro = false;
            _DBEViab_Erro_Numero = false;
            _DBEViab_Erro_Complemento = false;
            _DBEViab_Erro_Municipio = false;
            _DBEViab_Erro_Bairro = false;
            _DBEViab_Erro_CEP = false;
            _DBEViab_Erro_UF = false;
            _DBEViab_Erro_Natureza = false;

            _DBEViab_Erro_CNAE = new List<bool>();
            _DBEViab_Erro_Socios = new List<bool>();
            bool NenhumErro = true;

            bool _comparaTodoEndereco = this.Parametros.getValor(bParametro.Valores.VALIDA_COMPLEMENTO_DBE) == "1";

            #region Dados da Empresa
            //Eventos
            if (_dbe.ProtocoloEvento.Count != _viabilidade.ProtocoloEvento.Count)
            {
                if (_viabilidade.ProtocoloEvento.Count < _dbe.ProtocoloEvento.Count)
                {
                    bool encontrei = false;
                    for (int i = 0; i < _viabilidade.ProtocoloEvento.Count; i++)
                    {
                        for (int d = 0; d < _dbe.ProtocoloEvento.Count; d++)
                        {
                            if (_viabilidade.ProtocoloEvento[i].CodigoEvento == _dbe.ProtocoloEvento[d].CodigoEvento)
                            {
                                encontrei = true;
                                break;
                            }
                        }
                        if (!encontrei)
                        {
                            if (_viabilidade.ProtocoloEvento[i].CodigoEvento == 101
                                && _dbe.ProtocoloEvento.Count > 1
                                && (_dbe.ProtocoloEvento[1].CodigoEvento == 237 ||
                                    _dbe.ProtocoloEvento[1].CodigoEvento == 241))
                            {
                                _DBEViab_Erro_Evento.Add(false);
                            }
                            else
                            {
                                if (_tipoEmpresa == 2 && _viabilidade.ProtocoloEvento[i].CodigoEvento == 220)
                                {
                                    _DBEViab_Erro_Evento.Add(false);
                                }
                                else
                                {
                                    _DBEViab_Erro_Evento.Add(true);
                                    NenhumErro = false;
                                }
                                break;
                            }

                        }
                        else
                        {
                            _DBEViab_Erro_Evento.Add(false);
                            encontrei = false;
                        }
                    }

                }
                else
                {
                    _DBEViab_Erro_Evento.Add(true);
                    NenhumErro = false;
                }

            }
            else
            {

                for (int i = 0; i < _dbe.ProtocoloEvento.Count; i++)
                {
                    if (_dbe.ProtocoloEvento[i].CodigoEvento == _viabilidade.ProtocoloEvento[i].CodigoEvento)
                    {
                        _DBEViab_Erro_Evento.Add(false);
                    }
                    else
                    {
                        if (_tipoEmpresa == 2 && _viabilidade.ProtocoloEvento[i].CodigoEvento == 220)
                        {
                            _DBEViab_Erro_Evento.Add(false);
                        }
                        else
                        {
                            NenhumErro = false;
                            _DBEViab_Erro_Evento.Add(true);
                        }
                    }
                }
            }



            _DBEViab_Erro_TipoLogradouro = false;
            #region lixo
            //Comentado a comparacao 09/12/2013 Bruno Barreto
            //////if (!dHelperQuery.ComparaTextoAcento(_dbe.SedeTipoLogradouro, _viabilidade.SedeTipoLogradouro))
            //////{
            //////    string tipodesc = dHelperORACLE.GetTipoLogradouro(_dbe.SedeTipoLogradouro);

            //////    if (!dHelperQuery.ComparaTextoAcento(tipodesc, _viabilidade.SedeDsTipoLogradouro))
            //////    {
            //////        _DBEViab_Erro_TipoLogradouro = true;
            //////        NenhumErro = false;
            //////    }
            //////    else
            //////    {
            //////        _DBEViab_Erro_TipoLogradouro = false;
            //////    }
            //////}
            //////else
            //////{
            //////    _DBEViab_Erro_TipoLogradouro = false;
            //////}
            #endregion


            #endregion

            #region Natureza Juridica
            if (_dbe.NaturezaJuridicaCodigo != _NaturezaJuridicaCodigo)
            {
                _DBEViab_Erro_Natureza = true;
                NenhumErro = false;
            }
            else
            {
                _DBEViab_Erro_Natureza = false;
            }
            #endregion

            //Valida nome se a viabilidade for de constituicao ou alteração de nome
            //Retirado a condição para validar o ome se fosse constituição de Filial , pois no DBE não vem essa informação.
            //Caso reportado pela JUCEB
            //Alteração de filial de sede fora com alteração de nome
            //nesse caso não considerar esse evento
            #region Nome Empresarial
            if (_tipoEmpresa == 1)
            {
                if ((getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA)
                    || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_ALTERACAO_NOME_EMPRESARIAL)
                    || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_ALTERACAO_ENDERECO_ENTRE_ESTADOS))
                    && _dbe.SedeNome != "")
                {
                    if (RetiraTipoEnquadramento(TiraAcento(_dbe.SedeNome)) != RetiraTipoEnquadramento(TiraAcento(_viabilidade.SedeNome)))
                    {
                        _DBEViab_Erro_NomeEmpresa = true;
                        NenhumErro = false;
                    }
                    else
                    {
                        _DBEViab_Erro_NomeEmpresa = false;
                    }
                }
            }
            #endregion

            #region Endereço
            //Valida nome se a viabilidade for de constituicao ou alteração de nome
            if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA)
                || VerificaSeTemEventodeEndereco(_viabilidade)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_FILIAL))
            {
                if (TiraAcento(_dbe.SedeLogradouro.ToUpper()) != TiraAcento(_viabilidade.SedeLogradouro.ToUpper()))
                {
                    _DBEViab_Erro_Logradouro = true;
                    NenhumErro = false;
                }
                else
                {
                    _DBEViab_Erro_Logradouro = false;
                }

                if (TiraZerosEsquerda(TiraAcento(_dbe.SedeNumero.ToUpper())) != TiraZerosEsquerda(TiraAcento(_viabilidade.SedeNumero.ToUpper())))
                {
                    _DBEViab_Erro_Numero = true;
                    NenhumErro = false;
                }
                else
                {
                    _DBEViab_Erro_Numero = false;
                }

                if (TiraAcento(TiraCaracteres(_dbe.SedeComplemento.ToUpper())) != TiraAcento(TiraCaracteres(_viabilidade.SedeComplemento.ToUpper())))
                {
                    string CompDBE = TiraAcento(TiraCaracteres(_dbe.SedeComplemento.Trim().ToUpper()));
                    string CompViab = TiraAcento(TiraCaracteres(_viabilidade.SedeComplemento.Trim().ToUpper()));
                    List<string> t = CriaArray(TiraTodosEspacos(CompDBE));
                    List<string> tViab = CriaArray(TiraTodosEspacos(CompViab));

                    bool compara = ComparaComplementoTodos(t, tViab);


                    if (compara)
                    {
                        _DBEViab_Erro_Complemento = false;
                    }
                    else
                    {
                        _DBEViab_Erro_Complemento = true;
                        NenhumErro = false;
                    }

                }
                else
                {
                    _DBEViab_Erro_Complemento = false;
                }

                if (_dbe.SedeMunicipio.ToUpper() != _viabilidade.SedeMunicipio.ToUpper())
                {
                    _DBEViab_Erro_Municipio = true;
                    NenhumErro = false;
                }
                else
                {
                    _DBEViab_Erro_Municipio = false;
                }

                //if (_dbe.SedeBairro != _viabilidade.SedeBairro)
                if (TiraCaracteres(TiraAcento(_dbe.SedeBairro.ToUpper())) != TiraCaracteres(TiraAcento(_viabilidade.SedeBairro.ToUpper())))
                {
                    _DBEViab_Erro_Bairro = true;
                    NenhumErro = false;
                }
                else
                {
                    _DBEViab_Erro_Bairro = false;
                }

                //alteração solicitada pelo Xico
                //Verificar se o cep é generico, se for comparar as4 primeiras posições
                //se for igual ao do DBE passa
                //
                if (_dbe.SedeCEP != _viabilidade.SedeCEP)
                {
                    if (_viabilidade.SedeCEP.Length != 8)
                    {
                        _DBEViab_Erro_CEP = true;
                        NenhumErro = false;
                    }
                    else if (_viabilidade.SedeCEP.Substring(4, 4) == "0000")
                    {
                        if (_dbe.SedeCEP != "" && _viabilidade.SedeCEP.Substring(0, 4) != _dbe.SedeCEP.Substring(0, 4))
                        {
                            _DBEViab_Erro_CEP = true;
                            NenhumErro = false;
                        }
                    }
                    else
                    {
                        _DBEViab_Erro_CEP = true;
                        NenhumErro = false;
                    }
                }
                else
                {
                    _DBEViab_Erro_CEP = false;
                }

                if (_dbe.SedeUF.ToUpper() != _viabilidade.SedeUF.ToUpper())
                {
                    _DBEViab_Erro_UF = true;
                    NenhumErro = false;
                }
                else
                {
                    _DBEViab_Erro_UF = false;
                }

            }
            #endregion

            #region Dados CNAE

            //Valida CNAE se a viabilidade for de constituicao ou alteração de CNAE

            if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_FILIAL))
            {
                bool fExisteCane = false;
                if (_dbe.CNAEs.Count != _viabilidade.CNAEs.Count)
                {
                    //Caso a qtd seja diferente, inserir erro em todos os CNAEs
                    int qtd = 0;
                    if (_dbe.CNAEs.Count > _viabilidade.CNAEs.Count)
                        qtd = _dbe.CNAEs.Count;
                    else
                        qtd = _viabilidade.CNAEs.Count;
                    for (int k = 0; k < qtd; k++)
                    {
                        _DBEViab_Erro_CNAE.Add(true);
                    }
                    NenhumErro = false;
                }
                else
                {
                    foreach (bCNAE cnaeDbe in _dbe.CNAEs)
                    {
                        fExisteCane = false;
                        foreach (bCNAE cnaeViab in _viabilidade.CNAEs)
                        {
                            if (cnaeViab.CodigoCNAE == cnaeDbe.CodigoCNAE)
                            {
                                fExisteCane = true;
                                _DBEViab_Erro_CNAE.Add(false);
                                break;
                            }
                        }
                        if (!fExisteCane)
                        {
                            NenhumErro = false;
                            _DBEViab_Erro_CNAE.Add(true);
                        }

                    }

                }

            }
            #endregion

            #region Dados Socios
            //Valida socios se a viabilidade for de constituicao
            if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA))
            {
                ComparacaoDbeViabilidadeValida(_viabilidade.Socios, _dbe.SociosAtivos, _DBEViab_Erro_Socios, ref NenhumErro);
                ////se dbe.count != viabilidade.count
                //if (_dbe.SociosAtivos.Count != _viabilidade.Socios.Count)
                //{
                //    //se viabilidade.count < dbe.count
                //    if (_viabilidade.Socios.Count < _dbe.SociosAtivos.Count)
                //    {
                //        _DBEViab_Erro_Socios.Add(true);
                //        NenhumErro = false;
                //    }
                //    else //se viabilidade.count >= dbe.count
                //    {
                //        bool encontreiSocio;
                //        for (int d = 0; d < _dbe.SociosAtivos.Count; d++)
                //        {
                //            encontreiSocio = false;
                //            for (int v = 0; v < _viabilidade.Socios.Count; v++)
                //            {
                //                if (_dbe.SociosAtivos[d].CPFCNPJ == _viabilidade.Socios[v].CPFCNPJ)
                //                {
                //                    encontreiSocio = true;
                //                    break;
                //                }
                //            }
                //            if (!encontreiSocio)
                //            {
                //                NenhumErro = false;
                //                _DBEViab_Erro_Socios.Add(true);
                //            }
                //            else
                //            {
                //                _DBEViab_Erro_Socios.Add(false);
                //            }
                //        }
                //    }
                //}
                //else //se dbe.count == viabilidade.count
                //{
                //    for (int d = 0; d < _dbe.SociosAtivos.Count; d++)
                //    {
                //        if (_dbe.SociosAtivos[d].CPFCNPJ != _viabilidade.Socios[d].CPFCNPJ)
                //        {
                //            NenhumErro = false;
                //            _DBEViab_Erro_Socios.Add(true);
                //        }
                //        else
                //        {
                //            _DBEViab_Erro_Socios.Add(false);
                //        }
                //    }
                //}
            }

            #endregion


            return NenhumErro;

        }

        private void ComparacaoDbeViabilidadeValida(List<bSocios> sociosViabilida, List<bSocios> sociosDBE, List<bool> _DBEViab_Erro_Socios, ref bool NenhumErro)
        {
            bool contemSociosDbeNaViabilidade = false;
            foreach (bSocios socioDBE in sociosDBE)
            {
                contemSociosDbeNaViabilidade = false;
                foreach (bSocios socioViabilidade in sociosViabilida)
                {
                    if (socioDBE.CPFCNPJ == socioViabilidade.CPFCNPJ)
                    {
                        contemSociosDbeNaViabilidade = true;
                        break;
                    }
                }
                if (!contemSociosDbeNaViabilidade)
                    break;
            }


            if (!contemSociosDbeNaViabilidade) //não encontrou algum sócio do dbe
            {
                _DBEViab_Erro_Socios.Add(true);
                NenhumErro = false;
                _DBEViab_Mensagens_Erro.Add("Não foram encontrados todos os sócios do DBE na viabilidade");
            }
            else //encontrou todos os sócios do dbe no 
                _DBEViab_Erro_Socios.Add(false);

        }

        public void GetAlertaDBEViabilidade()
        {
            _listAlertaDbeViab.Clear();

            #region Eventos
            if (_dbe.ProtocoloEvento.Count != _viabilidade.ProtocoloEvento.Count)
            {
                if (_viabilidade.ProtocoloEvento.Count < _dbe.ProtocoloEvento.Count)
                {
                    bool encontrei = false;
                    for (int i = 0; i < _viabilidade.ProtocoloEvento.Count; i++)
                    {
                        for (int d = 0; d < _dbe.ProtocoloEvento.Count; d++)
                        {
                            if (_viabilidade.ProtocoloEvento[i].CodigoEvento == _dbe.ProtocoloEvento[d].CodigoEvento)
                            {
                                encontrei = true;
                                break;
                            }
                        }
                        if (!encontrei)
                        {
                            if (_viabilidade.ProtocoloEvento[i].CodigoEvento == 101
                                && _dbe.ProtocoloEvento.Count > 1
                                && (_dbe.ProtocoloEvento[1].CodigoEvento == 237 ||
                                    _dbe.ProtocoloEvento[1].CodigoEvento == 241))
                            {
                                _listAlertaDbeViab.Add("Evento da Viabilidade " + _viabilidade.ProtocoloEvento[i].CodigoEvento.ToString() + " não informado no DBE.");
                            }
                            else
                            {
                                if (_tipoEmpresa == 2 && _viabilidade.ProtocoloEvento[i].CodigoEvento == 220)
                                {
                                    _DBEViab_Erro_Evento.Add(false);
                                }
                                else
                                {
                                    _listAlertaDbeViab.Add("Evento da Viabilidade " + _viabilidade.ProtocoloEvento[i].CodigoEvento.ToString() + " não informado no DBE.");
                                }
                                break;
                            }

                        }
                    }

                }
            }

            #endregion

            #region Endereço
            //Valida nome se a viabilidade for de constituicao ou alteração de nome
            bool _erroEndereco = false;
            if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA)
                || VerificaSeTemEventodeEndereco(_viabilidade)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_FILIAL))
            {
                if (TiraAcento(_dbe.SedeLogradouro.ToUpper()) != TiraAcento(_viabilidade.SedeLogradouro.ToUpper()))
                {
                    _erroEndereco = true;
                }

                if (TiraZerosEsquerda(TiraAcento(_dbe.SedeNumero.ToUpper())) != TiraZerosEsquerda(TiraAcento(_viabilidade.SedeNumero.ToUpper())))
                {
                    _erroEndereco = true;
                }

                if (TiraAcento(TiraCaracteres(_dbe.SedeComplemento.ToUpper())) != TiraAcento(TiraCaracteres(_viabilidade.SedeComplemento.ToUpper())))
                {
                    string CompDBE = TiraAcento(TiraCaracteres(_dbe.SedeComplemento.Trim().ToUpper()));
                    string CompViab = TiraAcento(TiraCaracteres(_viabilidade.SedeComplemento.Trim().ToUpper()));
                    List<string> t = CriaArray(TiraTodosEspacos(CompDBE));
                    List<string> tViab = CriaArray(TiraTodosEspacos(CompViab));

                    bool compara = ComparaComplementoTodos(t, tViab);


                    if (!compara)
                    {
                        _erroEndereco = true;
                    }
                }

                if (_dbe.SedeMunicipio.ToUpper() != _viabilidade.SedeMunicipio.ToUpper())
                {
                    _erroEndereco = true;
                }

                if (TiraCaracteres(TiraAcento(_dbe.SedeBairro.ToUpper())) != TiraCaracteres(TiraAcento(_viabilidade.SedeBairro.ToUpper())))
                {
                    _erroEndereco = true;
                }

                //alteração solicitada pelo Xico
                //Verificar se o cep é generico, se for comparar as4 primeiras posições
                //se for igual ao do DBE passa
                //
                if (_dbe.SedeCEP != _viabilidade.SedeCEP)
                {
                    if (_viabilidade.SedeCEP.Length != 8)
                    {
                        _erroEndereco = true;
                    }
                    else if (_viabilidade.SedeCEP.Substring(4, 4) == "0000")
                    {
                        if (_dbe.SedeCEP != "" && _viabilidade.SedeCEP.Substring(0, 4) != _dbe.SedeCEP.Substring(0, 4))
                        {
                            _erroEndereco = true;
                        }
                    }
                }

                if (_dbe.SedeUF.ToUpper() != _viabilidade.SedeUF.ToUpper())
                {
                    _erroEndereco = true;
                }
            }
            if (_erroEndereco)
            {
                _listAlertaDbeViab.Add("Endereço informado na Viabilidade diferente do informado no DBE");
            }
            #endregion

            #region Dados CNAE

            //Valida CNAE se a viabilidade for de constituicao ou alteração de CNAE

            if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS)
                || getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_FILIAL))
            {
                bool fExisteCane = false;
                if (_dbe.CNAEs.Count != _viabilidade.CNAEs.Count)
                {
                    _listAlertaDbeViab.Add("CNAE informado na Viabilidade diferente do informado no DBE");
                }
                else
                {
                    foreach (bCNAE cnaeDbe in _dbe.CNAEs)
                    {
                        fExisteCane = false;
                        foreach (bCNAE cnaeViab in _viabilidade.CNAEs)
                        {
                            if (cnaeViab.CodigoCNAE == cnaeDbe.CodigoCNAE)
                            {
                                fExisteCane = true;
                                _DBEViab_Erro_CNAE.Add(false);
                                break;
                            }
                        }
                        if (!fExisteCane)
                        {
                            _listAlertaDbeViab.Add("CNAE informado na Viabilidade diferente do informado no DBE");
                            break;
                        }
                    }
                }

            }
            #endregion

            #region Dados Socios
            //if (getEventoAvaliaByRequerimento(_viabilidade, CODEVENTO_CONSTITUICAO_EMPRESA))
            //{
            //    ComparacaoDbeViabilidadeValida(_viabilidade.Socios, _dbe.SociosAtivos, _DBEViab_Erro_Socios, ref NenhumErro);

            //}

            #endregion

        }


        public bool DbeExisteEvento(string codigo)
        {
            bool ret = false;
            foreach (bProtocoloEvento obj in _dbe.ProtocoloEvento)
            {
                if (obj.CodigoEvento.ToString() == codigo)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        public List<string> CriaArray(string texto)
        {
            List<string> aux = new List<string>();
            int tam = 0;
            for (int i = 0; i < texto.Length; i += 26)
            {
                if (i + 26 > texto.Length)
                {
                    tam = texto.Length - i;
                }
                else
                {
                    tam = 26;
                }
                aux.Add(texto.Substring(i, tam).Trim());
            }

            return aux;
        }

        public bool ComparaComplemento(List<string> CompDBE, string CompViab)
        {
            bool retorno = true;
            string auxDBE = "";
            if (CompViab.Length == 0)
                return false;
            if (CompDBE.Count == 0)
                return false;

            for (int i = 0; i < CompDBE.Count; i++)
            {
                auxDBE += CompDBE[i].Replace(" ", "");
                if (CompViab.IndexOf(CompDBE[i]) == -1)
                {
                    retorno = false;
                    break;
                }
            }
            string auxViab = CompViab.Replace(" ", "").Trim();
            if (auxViab.Length != auxDBE.Length)
                return false;



            return retorno;
        }

        public bool ComparaComplementoTodos(List<string> CompDBE, List<string> CompViab)
        {
            bool retorno = true;
            if (CompViab.Count == 0)
                return false;
            if (CompDBE.Count == 0)
                return false;

            if (CompViab.Count != CompDBE.Count)
                return false;

            for (int i = 0; i < CompDBE.Count; i++)
            {
                if (CompDBE[i] != CompViab[i])
                {
                    retorno = false;
                    break;
                }
            }

            return retorno;
        }

        public bSocios FindSocio(string cpf, string _Qualificacao)
        {
            foreach (bSocios c in _Socios)
            {
                if (c.CPFCNPJ == cpf && c.Qualificacao == _Qualificacao)
                {
                    return c;
                }
            }

            return null;
        }

        public bSocios FindRepresentanteSocio(string cpfSocio, string cpfRepresentante, string Qualificacao)
        {
            foreach (bSocios c in _Socios)
            {
                if (c.CPFCNPJ == cpfSocio && c.Qualificacao == Qualificacao)
                {
                    foreach (bSocios r in c.Representantes)
                    {
                        if (r.CPFCNPJ == cpfRepresentante)
                        {
                            return r;
                        }
                    }
                }
            }

            return null;
        }

        public bSocios FindSocioDBE(string cpf, string _Qualificacao)
        {
            foreach (bSocios c in _dbe.Socios)
            {
                if (c.CPFCNPJ == cpf && c.Qualificacao == _Qualificacao)
                {
                    return c;
                }
                if (_Qualificacao == "5" && c.CPFCNPJ == cpf && c.rep_legal > 0)
                {
                    return c;
                }
            }

            return null;
        }
        public void AtualizasocioComDBE()
        {

            foreach (bSocios _scDBE in _dbe.Socios)
            {
                bSocios _scReq = FindSocio(_scDBE.CPFCNPJ, _scDBE.Qualificacao);
                if (_scReq != null)
                {

                    _scReq.Nome = _scDBE.Nome;

                    if (_scDBE.tipoacao == 1)
                    {
                        _scReq.tipoacao = 1;


                        if ((_scDBE.Qualificacao != "5" && _scDBE.rep_legal > 0))
                        {
                            bSocios _scReq1 = FindSocio(_scDBE.CPFCNPJ, "5");
                            if (_scReq1 != null)
                            {
                                if (_scReq1.tipoacao == 1)
                                {
                                    _scReq1.tipoacao = 1;
                                    _scReq1.Nome = _scDBE.Nome;
                                }
                            }
                            else
                            {
                                bSocios sR = new bSocios();
                                sR.CPFCNPJ = _scDBE.CPFCNPJ;
                                sR.Nome = _scDBE.Nome;
                                sR.Qualificacao = "5";
                                sR.Qualificacao_Descricao = "ADMINISTRADOR";
                                sR.rep_legal = 1;

                                sR.tipoacao = _scDBE.tipoacao;
                                sR.TipoPessoa = _scDBE.TipoPessoa;

                                sR.ObrigacoesSociais = _scDBE.ObrigacoesSociais;

                                sR.DDD = _scDBE.DDD;
                                sR.Telefone = _scDBE.Telefone;
                                sR.Email = _scDBE.Email;
                                sR.DataNascimento = _scDBE.DataNascimento;
                                _Socios.Add(sR);
                            }
                        }

                    }
                    //se no DBe for evento de alteração
                    //inibido pois o usuario colocava como inclusão do socio acao 1
                    //e no dbe  vem com aqualificação 49 e acao 3, que pode ser inclusão de qualificação de socio ou administrador
                    if (_scDBE.tipoacao == 3)
                    {

                        if (_scReq.tipoacao == 0)
                        {
                            _scReq.tipoacao = 3;

                            if ((_scDBE.Qualificacao != "5" && _scDBE.rep_legal > 0))
                            {
                                bSocios _scReq1 = FindSocio(_scDBE.CPFCNPJ, "5");
                                if (_scReq1 != null)
                                {
                                    if (_scReq1.tipoacao == 0)
                                    {
                                        _scReq1.tipoacao = 3;
                                        _scReq1.Nome = _scDBE.Nome;
                                    }
                                }
                            }
                        }
                    }

                    if (_scDBE.tipoacao == 5)
                    {
                        _scReq.tipoacao = 5;

                        if (_scDBE.Qualificacao != "5" && _scDBE.rep_legal > 0)
                        {
                            bSocios _scReq1 = FindSocio(_scDBE.CPFCNPJ, "5");
                            if (_scReq1 != null)
                            {
                                _scReq1.tipoacao = 5;
                            }
                        }
                    }
                    if (_scReq.Qualificacao != "5")
                        _scReq.PercentualCapital = _scDBE.PercentualCapital;

                    //Verifica se tem representante no DBE
                    //Valida se é o mesmo no requerimento
                    if (_scDBE.Representantes.Count > 0)
                    {
                        foreach (bSocios sRepre in _scDBE.Representantes)
                        {
                            bSocios _scRepre = FindRepresentanteSocio(_scReq.CPFCNPJ, sRepre.CPFCNPJ, _scReq.Qualificacao);
                            if (_scRepre == null)
                            {
                                //Não achou o representante informado no DBE
                                //Adicionar o Representante
                                bSocios rep = new bSocios();
                                rep.CPFCNPJ = sRepre.CPFCNPJ;
                                rep.Nome = sRepre.Nome;
                                rep.EndTipoLogradouro = sRepre.EndTipoLogradouro;
                                rep.EndDsTipoLogradouro = sRepre.EndDsTipoLogradouro;
                                rep.EndLogradouro = sRepre.EndLogradouro;
                                rep.EndNumero = sRepre.EndNumero;
                                rep.EndComplemento = sRepre.EndComplemento;
                                rep.EndBairro = sRepre.EndBairro;
                                rep.EndMunicipio = sRepre.EndMunicipio;
                                rep.EndCEP = sRepre.EndCEP;
                                rep.EndUF = sRepre.EndUF;
                                rep.DDD = sRepre.DDD;
                                rep.Telefone = sRepre.Telefone;
                                rep.Email = sRepre.Email;
                                rep.TipoPessoa = sRepre.TipoPessoa;
                                _scReq.Representantes.Add(rep);
                            }
                        }
                    }
                }
                else
                {
                    if (_scDBE.tipoacao == 1)
                    {
                        IncluiSociosDBEAlteracao(_scDBE);
                    }
                }
            }
        }

        private void IncluiSociosDBEAlteracao(bSocios sB)
        {

            bSocios sR = new bSocios();

            sR.CPFCNPJ = sB.CPFCNPJ;
            sR.Nome = sB.Nome;
            sR.Qualificacao = sB.Qualificacao;
            sR.Qualificacao_Descricao = sB.Qualificacao_Descricao;
            // sR.rep_legal = 1;

            sR.PercentualCapital = sB.PercentualCapital;
            sR.tipoacao = sB.tipoacao;
            sR.TipoPessoa = sB.TipoPessoa;

            sR.ObrigacoesSociais = sB.ObrigacoesSociais;

            sR.DDD = sB.DDD;
            sR.Telefone = sB.Telefone;
            sR.Email = sB.Email;
            //sR.CapitalIntegralizado = sB.CapitalIntegralizado;
            sR.DataNascimento = sB.DataNascimento;

            #region Endereço Sócio

            sR.EndCEP = sB.EndCEP;
            sR.EndUF = sB.EndUF;
            sR.EndMunicipio = sB.EndMunicipio;
            sR.EndBairro = sB.EndBairro;
            sR.EndTipoLogradouro = sB.EndTipoLogradouro;
            sR.EndDsTipoLogradouro = sB.EndDsTipoLogradouro;
            sR.EndLogradouro = sB.EndLogradouro;
            sR.EndNumero = sB.EndNumero;
            sR.EndComplemento = sB.EndComplemento;

            #endregion

            #region Representante
            foreach (bSocios rB in sB.Representantes)
            {
                bSocios rep = new bSocios();
                rep.CPFCNPJ = rB.CPFCNPJ;
                rep.Nome = rB.Nome;
                rep.EndTipoLogradouro = rB.EndTipoLogradouro;
                rep.EndDsTipoLogradouro = rB.EndDsTipoLogradouro;
                rep.EndLogradouro = rB.EndLogradouro;
                rep.EndNumero = rB.EndNumero;
                rep.EndComplemento = rB.EndComplemento;
                rep.EndBairro = rB.EndBairro;
                rep.EndMunicipio = rB.EndMunicipio;
                rep.EndCEP = rB.EndCEP;
                rep.EndUF = rB.EndUF;
                rep.DDD = rB.DDD;
                rep.Telefone = rB.Telefone;
                rep.Email = rB.Email;
                rep.TipoPessoa = rB.TipoPessoa;
                sR.Representantes.Add(rep);
            }

            #endregion

            _Socios.Add(sR);
            // Se for Eireli e nao tiver um outro adminstrador incluir o mesmo como administrador

            if ((sB.Qualificacao != "5" && sB.rep_legal > 0))
            {
                bSocios _scReq = FindSocio(sB.CPFCNPJ, "5");
                if (_scReq == null)
                {

                    sR = new bSocios();
                    sR.CPFCNPJ = sB.CPFCNPJ;
                    sR.Nome = sB.Nome;
                    sR.Qualificacao = "5";
                    sR.Qualificacao_Descricao = "ADMINISTRADOR";
                    sR.rep_legal = 1;

                    sR.tipoacao = sB.tipoacao;
                    sR.TipoPessoa = sB.TipoPessoa;

                    sR.ObrigacoesSociais = sB.ObrigacoesSociais;

                    sR.DDD = sB.DDD;
                    sR.Telefone = sB.Telefone;
                    sR.Email = sB.Email;
                    sR.DataNascimento = sB.DataNascimento;

                    #region Endereço Sócio

                    sR.EndCEP = sB.EndCEP;
                    sR.EndUF = sB.EndUF;
                    sR.EndMunicipio = sB.EndMunicipio;
                    sR.EndBairro = sB.EndBairro;
                    sR.EndTipoLogradouro = sB.EndTipoLogradouro;
                    sR.EndDsTipoLogradouro = sB.EndDsTipoLogradouro;
                    sR.EndLogradouro = sB.EndLogradouro;
                    sR.EndNumero = sB.EndNumero;
                    sR.EndComplemento = sB.EndComplemento;

                    #endregion
                    _Socios.Add(sR);
                }
            }


        }

        public void IncluiSociosDBEConstituicao()
        {
            foreach (bSocios sB in _dbe.Socios)
            {

                bSocios sR = new bSocios();

                sR.CPFCNPJ = sB.CPFCNPJ;
                sR.Nome = sB.Nome;
                sR.Qualificacao = sB.Qualificacao;
                sR.Qualificacao_Descricao = sB.Qualificacao_Descricao;
                // sR.rep_legal = 1;

                sR.PercentualCapital = sB.PercentualCapital;
                sR.tipoacao = sB.tipoacao;
                sR.TipoPessoa = sB.TipoPessoa;

                sR.ObrigacoesSociais = sB.ObrigacoesSociais;

                sR.DDD = sB.DDD;
                sR.Telefone = sB.Telefone;
                sR.Email = sB.Email;
                //sR.CapitalIntegralizado = sB.CapitalIntegralizado;
                sR.DataNascimento = sB.DataNascimento;

                #region Endereço Sócio

                sR.EndCEP = sB.EndCEP;
                sR.EndUF = sB.EndUF;
                sR.EndMunicipio = sB.EndMunicipio;
                sR.EndBairro = sB.EndBairro;
                sR.EndTipoLogradouro = sB.EndTipoLogradouro;
                sR.EndDsTipoLogradouro = sB.EndDsTipoLogradouro;
                sR.EndLogradouro = sB.EndLogradouro;
                sR.EndNumero = sB.EndNumero;
                sR.EndComplemento = sB.EndComplemento;

                #endregion

                #region Representante
                foreach (bSocios rB in sB.Representantes)
                {
                    bSocios rep = new bSocios();
                    rep.CPFCNPJ = rB.CPFCNPJ;
                    rep.Nome = rB.Nome;
                    rep.EndTipoLogradouro = rB.EndTipoLogradouro;
                    rep.EndDsTipoLogradouro = rB.EndDsTipoLogradouro;
                    rep.EndLogradouro = rB.EndLogradouro;
                    rep.EndNumero = rB.EndNumero;
                    rep.EndComplemento = rB.EndComplemento;
                    rep.EndBairro = rB.EndBairro;
                    rep.EndMunicipio = rB.EndMunicipio;
                    rep.EndCEP = rB.EndCEP;
                    rep.EndUF = rB.EndUF;
                    rep.DDD = rB.DDD;
                    rep.Telefone = rB.Telefone;
                    rep.Email = rB.Email;
                    rep.TipoPessoa = rB.TipoPessoa;
                    sR.Representantes.Add(rep);
                }

                #endregion

                _Socios.Add(sR);

                if ((sB.Qualificacao != "5" && sB.rep_legal > 0) || sB.Qualificacao == "65")
                {
                    sR = new bSocios();
                    sR.CPFCNPJ = sB.CPFCNPJ;
                    sR.Nome = sB.Nome;
                    sR.Qualificacao = "5";
                    sR.Qualificacao_Descricao = "ADMINISTRADOR";
                    sR.rep_legal = 1;

                    sR.tipoacao = sB.tipoacao;
                    sR.TipoPessoa = sB.TipoPessoa;

                    sR.ObrigacoesSociais = sB.ObrigacoesSociais;

                    sR.DDD = sB.DDD;
                    sR.Telefone = sB.Telefone;
                    sR.Email = sB.Email;
                    sR.DataNascimento = sB.DataNascimento;

                    #region Endereço Sócio

                    sR.EndCEP = sB.EndCEP;
                    sR.EndUF = sB.EndUF;
                    sR.EndMunicipio = sB.EndMunicipio;
                    sR.EndBairro = sB.EndBairro;
                    sR.EndTipoLogradouro = sB.EndTipoLogradouro;
                    sR.EndDsTipoLogradouro = sB.EndDsTipoLogradouro;
                    sR.EndLogradouro = sB.EndLogradouro;
                    sR.EndNumero = sB.EndNumero;
                    sR.EndComplemento = sB.EndComplemento;

                    #endregion
                    _Socios.Add(sR);
                }


            }

        }

        /// <summary>
        /// Retira os socios quando for DBe de Constituição e alterou o QSA
        /// </summary>
        public void VerificaSocioNovoDBE()
        {

            if (_dbeAtual == _CodigoDBE)
                return;

            List<string> _sqPessoas = new List<string>();

            int cont = 0;
            while (cont <= _Socios.Count - 1)
            {
                bSocios _scDBE = FindSocioDBE(_Socios[cont].CPFCNPJ, _Socios[cont].Qualificacao);
                if (_scDBE == null)
                {
                    //Se não achar o socio no DBE Exclui o socio

                    ApagaPessoa(Int32.Parse(_Socios[cont].SQPessoa), _Socios[cont].SQPessoaPai, Int32.Parse(_Socios[cont].Qualificacao), true);
                    _sqPessoas.Add(_Socios[cont].CPFCNPJ);

                    //Exclui da lista de socios
                    _Socios.Remove(_Socios[cont]);
                }
                else
                {
                    cont++;
                }
            }

            //foreach (bSocios _scReq in _Socios)
            //{
            //    bSocios _scDBE = FindSocioDBE(_scReq.CPFCNPJ, _scReq.Qualificacao);
            //    if (_scDBE == null)
            //    {
            //        //Se não achar o socio no DBE Exclui o socio

            //        ApagaPessoa(Int32.Parse(_scReq.SQPessoa), _scReq.SQPessoaPai, Int32.Parse(_scReq.Qualificacao),  true);
            //        _sqPessoas.Add(_scReq.CPFCNPJ);

            //        //Exclui da lista de socios
            //        _Socios.Remove(_scReq);
            //    }
            //}




        }

        /// <summary>
        /// Verifica se os sócios do DBE são iguais aos que estao no requerimento
        /// </summary>
        /// <returns></returns>
        public bool ValidaQtdSocioRequerimentoDBE()
        {
            bool _acheiSocio = true;

            if (_Socios.Count != _dbe.Socios.Count)
            {
                return false;
            }

            foreach (bSocios ss in _Socios)
            {
                foreach (bSocios ssDBE in _dbe.Socios)
                {
                    _acheiSocio = false;
                    if (ss.CPFCNPJ == ssDBE.CPFCNPJ)
                    {
                        ss.Nome = ssDBE.Nome;
                        _acheiSocio = true;
                        break;
                    }
                }
                if (!_acheiSocio)
                {
                    break;
                }
            }

            return _acheiSocio;
        }

        #endregion

        #region Divergencia DBE
        public bDivergenciaDBE getDivergenciaDBEByTipo(int tipo)
        {
            foreach (bDivergenciaDBE obj in _listDivergenciaDBE)
            {
                if (obj.Item == tipo)
                {
                    return obj;
                }
            }

            return null;
        }

        #endregion

        #region Util

        public string retProt(string valor)
        {
            string temp = valor;
            temp = temp.Replace("-", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("_", "");
            temp = temp.Replace(".", "");
            return temp;

        }
        public string TiraCaracteres(string valor)
        {
            string temp = valor;
            temp = temp.Replace(":", "");
            temp = temp.Replace(";", "");
            temp = temp.Replace(".", "");
            temp = temp.Replace(",", "");
            return temp;

        }
        public string TiraZerosEsquerda(string valor)
        {
            string temp1 = valor;
            for (int i = 0; i < valor.Length; i++)
            {
                if (valor.Substring(i, 1) != "0")
                    break;

                if (valor.Substring(i, 1) == "0")
                    temp1 = valor.Substring(i + 1, temp1.Length - 1);


            }

            return temp1;

        }

        private string ConverteUTF8(string texto)
        {
            Encoding enc = new UTF8Encoding(true, true);
            byte[] bytes = enc.GetBytes(texto);

            return enc.GetString(bytes);
        }

        public string TiraAcento(string pValue)
        {

            string pResult = pValue;

            //para tirar dois espaços emp branco
            pResult = TrimAll(pResult);

            pResult = pResult.Replace('À', 'A');
            pResult = pResult.Replace('Á', 'A');
            pResult = pResult.Replace('Â', 'A');
            pResult = pResult.Replace('Ã', 'A');
            pResult = pResult.Replace('Ä', 'A');

            pResult = pResult.Replace('à', 'a');
            pResult = pResult.Replace('á', 'a');
            pResult = pResult.Replace('â', 'a');
            pResult = pResult.Replace('ã', 'a');
            pResult = pResult.Replace('ä', 'a');

            pResult = pResult.Replace('È', 'E');
            pResult = pResult.Replace('É', 'E');
            pResult = pResult.Replace('Ê', 'E');
            pResult = pResult.Replace('Ë', 'E');

            pResult = pResult.Replace('è', 'e');
            pResult = pResult.Replace('é', 'e');
            pResult = pResult.Replace('ê', 'e');
            pResult = pResult.Replace('ë', 'e');

            pResult = pResult.Replace('Ì', 'I');
            pResult = pResult.Replace('Í', 'I');
            pResult = pResult.Replace('Î', 'I');
            pResult = pResult.Replace('Ï', 'I');

            pResult = pResult.Replace('ì', 'i');
            pResult = pResult.Replace('í', 'i');
            pResult = pResult.Replace('î', 'i');
            pResult = pResult.Replace('ï', 'i');

            pResult = pResult.Replace('Ò', 'O');
            pResult = pResult.Replace('Ó', 'O');
            pResult = pResult.Replace('Ô', 'O');
            pResult = pResult.Replace('Õ', 'O');
            pResult = pResult.Replace('Ö', 'O');

            pResult = pResult.Replace('ò', 'o');
            pResult = pResult.Replace('ó', 'o');
            pResult = pResult.Replace('ô', 'o');
            pResult = pResult.Replace('õ', 'o');
            pResult = pResult.Replace('ö', 'o');

            pResult = pResult.Replace('Ù', 'U');
            pResult = pResult.Replace('Ú', 'U');
            pResult = pResult.Replace('Û', 'U');
            pResult = pResult.Replace('Ü', 'U');

            pResult = pResult.Replace('ù', 'u');
            pResult = pResult.Replace('ú', 'u');
            pResult = pResult.Replace('û', 'u');
            pResult = pResult.Replace('ü', 'u');

            pResult = pResult.Replace('Ç', 'C');
            pResult = pResult.Replace('ç', 'c');

            pResult = pResult.Replace('`', ' ');
            pResult = pResult.Replace('´', ' ');

            pResult = retProt(pResult);

            pResult = TrimAll(pResult);
            return pResult;



        }

        public static string TiraTodosEspacos(string s)
        {
            s = s.Trim();
            while (s.IndexOf(" ") != -1)
                s = s.Replace(" ", "");
            return s;
        }

        public static string TrimAll(string s)
        {
            s = s.Trim();
            while (s.IndexOf("  ") != -1)
                s = s.Replace("  ", " ");
            return s;
        }
        public string RetornaOrgaoRegistro(string wCodigo)
        {
            string wOrgao = wCodigo;
            bTabelasAuxiliares cOrgaoExpedidor = new bTabelasAuxiliares();
            DataTable dt = cOrgaoExpedidor.getOrgaoExpedidor();
            string opcao = "Codigo = '" + wCodigo + "'";
            DataRow[] Registro;
            Registro = dt.Select(opcao);
            if (Registro.Length > 0)
            {
                wOrgao = Registro[0][0].ToString();
            }
            if (wCodigo.Trim().ToUpper() != wOrgao.Trim().ToUpper())
                wOrgao = wOrgao.Substring(Registro[0][1].ToString().Length + 3);
            return wOrgao;
        }

        #endregion

        #region JUCERJA
        public DataTable VerificaStatusExistente(string wDBE, string wReq)
        {
            DataTable dt = new DataTable();
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                dt = p.VerificaStatusDBE(wDBE, wReq);
            }
            return dt;
        }
        public void UpdateDBEProtocolo(string wProtocolo, string wDBE)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                p.UpdateDBEProtocolo(wProtocolo, wDBE);
            }
        }

        public string VerificaDBEExistente(string wDBE, out string Protocolo, out int qtdlinhas)
        {
            string wAux = "";
            Protocolo = "";
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                DataTable dt = new DataTable();
                dt = p.VerificaExistenciaDBE(wDBE);
                if (dt.Rows.Count > 0)
                {
                    wAux = dt.Rows[0]["t005_nr_protocolo_viabilidade"].ToString();
                    Protocolo = dt.Rows[0]["t005_nr_protocolo"].ToString();
                }
                qtdlinhas = dt.Rows.Count;
            }
            return wAux;
        }
        #endregion


        #region Gera XML para Siarco
        public string GeraXmlRequerimentoSiarco1(string _requerimento)
        {
            string _retorno = "";
            string _msg = "";

            using (dT005_Protocolo obj = new dT005_Protocolo())
            {
                _retorno = obj.geraXmlDoRequerimentoSiarco(_requerimento, ref _msg);
            }
            return _retorno;
        }

        public string GeraXmlRequerimentoSiarco(string _requerimento, ref string msgErro)
        {
            msgErro = "";
            string _msg = "";

            // Gera XML Requerimento
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                _xmlRequerimento = p.geraXmlDoRequerimentoSiarco(_requerimento, ref _msg); ;

                if (_xmlRequerimento == "" || _xmlRequerimento == null)
                {
                    msgErro = "Informe ao suporte tecnico que o xml não foi gerado! " + _ProtocoloRequerimento + " " + msgErro;
                }
                else
                {
                    return _xmlRequerimento;
                }
            }
            return "";

        }


        #endregion

        #region Cria qualificações sócio
        private bool VerificaExisteOutraQualificacao(string _cpf, int _sqPessoa)
        {
            bool _existe = false;
            foreach (bSocios s in _Socios)
            {
                if (s.CPFCNPJ == _cpf && Int32.Parse(s.SQPessoa) != _sqPessoa)
                {
                    _existe = true;
                }
            }
            return _existe;
        }
        public void AtualizaNovaCondicaoSocio()
        {
            foreach (bSocios soc in _Socios)
            {
                VerificaAtualizaQualificacaoSocioNovo(soc);
            }

            AtualizaSocios();

        }

        public void VerificaAtualizaQualificacaoSocioNovo(bSocios soc)
        {
            //se for socio com flag de adm
            //atualizo a flag para zero e crio outra qualificação para adm
            //isso para os casos antigos
            if ((soc.Qualificacao != "5" && soc.rep_legal > 0))
            {
                soc.rep_legal = 0;
                soc.Update(_CodigoEmpresa);

                //Verifica se existe outra qualificação
                if (!VerificaExisteOutraQualificacao(soc.CPFCNPJ, Int32.Parse(soc.SQPessoa)))
                {
                    bSocios wbSocio = new bSocios();
                    wbSocio.SQPessoa = "0";
                    wbSocio.CPFCNPJ = soc.CPFCNPJ;
                    wbSocio.Nome = soc.Nome;
                    wbSocio.Qualificacao = "5";
                    wbSocio.rep_legal = 0;
                    wbSocio.tipoacao = soc.tipoacao;
                    wbSocio.DataSaidaSocio = soc.DataSaidaSocio;
                    wbSocio.CapitalIntegralizado = 0;
                    wbSocio.Capital_a_Integralizar = 0;
                    wbSocio.QuotaCapitalSocial = 0;

                    wbSocio.TipoIdentidade = soc.TipoIdentidade;
                    wbSocio.RG = soc.RG;
                    wbSocio.TipoPessoa = soc.TipoPessoa;
                    wbSocio.OrgaoExpedidor = soc.OrgaoExpedidor;
                    wbSocio.OrgaoExpedidorUF = soc.OrgaoExpedidorUF;
                    wbSocio.NacionalidadeCodigo = soc.NacionalidadeCodigo;
                    wbSocio.Nacionalidade = soc.Nacionalidade;
                    wbSocio.Analfabeto = soc.Analfabeto;

                    wbSocio.tipo_visto = soc.tipo_visto;
                    wbSocio.emissao_visto = soc.emissao_visto;
                    wbSocio.validade_visto = soc.validade_visto;
                    wbSocio.Justificativa_Visto = soc.Justificativa_Visto;
                    wbSocio.in_Sexo = soc.in_Sexo;

                    wbSocio.EstadoCivil = soc.EstadoCivil;
                    wbSocio.EstadoCivilRegime = soc.EstadoCivilRegime;
                    wbSocio.DataNascimento = soc.DataNascimento;
                    wbSocio.TipoEmancipado = soc.TipoEmancipado;
                    wbSocio.Profissao = "0";
                    wbSocio.Profissao_Descricao = soc.Profissao_Descricao;

                    wbSocio.Nome_Mae = soc.Nome_Mae;
                    wbSocio.Nome_Pai = soc.Nome_Pai;

                    #region Endereço
                    wbSocio.DDD = soc.DDD;
                    wbSocio.Telefone = soc.Telefone;
                    wbSocio.Email = soc.Email;
                    wbSocio.EndCEP = soc.EndCEP;
                    wbSocio.EndUF = soc.EndUF;
                    wbSocio.EndMunicipio = soc.EndMunicipio;
                    wbSocio.EndBairro = soc.EndBairro;
                    wbSocio.EndTipoLogradouro = soc.EndTipoLogradouro;
                    wbSocio.EndDsTipoLogradouro = soc.EndDsTipoLogradouro;
                    wbSocio.EndLogradouro = soc.EndLogradouro;
                    wbSocio.EndNumero = soc.EndNumero;
                    wbSocio.EndComplemento = soc.EndComplemento;
                    #endregion

                    wbSocio.Update(_CodigoEmpresa);


                }

            }
        }

        public void VerificaAtualizaQualificacaoSocioNovo_old(bSocios soc)
        {

            if ((soc.Qualificacao != "5" && soc.rep_legal > 0) || soc.Qualificacao == "65")
            {

                //Verifica se existe outra qualificação
                if (!VerificaExisteOutraQualificacao(soc.CPFCNPJ, Int32.Parse(soc.SQPessoa)))
                {
                    bSocios wbSocio = new bSocios();
                    wbSocio.SQPessoa = "0";
                    wbSocio.CPFCNPJ = soc.CPFCNPJ;
                    wbSocio.Nome = soc.Nome;
                    wbSocio.Qualificacao = soc.Qualificacao;
                    wbSocio.rep_legal = 0;
                    wbSocio.tipoacao = soc.tipoacao;
                    wbSocio.DataSaidaSocio = soc.DataSaidaSocio;
                    wbSocio.CapitalIntegralizado = soc.CapitalIntegralizado;
                    wbSocio.Capital_a_Integralizar = soc.Capital_a_Integralizar;
                    wbSocio.QuotaCapitalSocial = soc.QuotaCapitalSocial;

                    wbSocio.TipoIdentidade = soc.TipoIdentidade;
                    wbSocio.RG = soc.RG;
                    wbSocio.TipoPessoa = soc.TipoPessoa;
                    wbSocio.OrgaoExpedidor = soc.OrgaoExpedidor;
                    wbSocio.OrgaoExpedidorUF = soc.OrgaoExpedidorUF;
                    wbSocio.NacionalidadeCodigo = soc.NacionalidadeCodigo;
                    wbSocio.Nacionalidade = soc.Nacionalidade;
                    wbSocio.Analfabeto = soc.Analfabeto;

                    wbSocio.tipo_visto = soc.tipo_visto;
                    wbSocio.emissao_visto = soc.emissao_visto;
                    wbSocio.validade_visto = soc.validade_visto;
                    wbSocio.Justificativa_Visto = soc.Justificativa_Visto;
                    wbSocio.in_Sexo = soc.in_Sexo;

                    wbSocio.EstadoCivil = soc.EstadoCivil;
                    wbSocio.EstadoCivilRegime = soc.EstadoCivilRegime;
                    wbSocio.DataNascimento = soc.DataNascimento;
                    wbSocio.TipoEmancipado = soc.TipoEmancipado;
                    wbSocio.Profissao = "0";
                    wbSocio.Profissao_Descricao = soc.Profissao_Descricao;

                    wbSocio.Nome_Mae = soc.Nome_Mae;
                    wbSocio.Nome_Pai = soc.Nome_Pai;

                    #region Endereço
                    wbSocio.DDD = soc.DDD;
                    wbSocio.Telefone = soc.Telefone;
                    wbSocio.Email = soc.Email;
                    wbSocio.EndCEP = soc.EndCEP;
                    wbSocio.EndUF = soc.EndUF;
                    wbSocio.EndMunicipio = soc.EndMunicipio;
                    wbSocio.EndBairro = soc.EndBairro;
                    wbSocio.EndTipoLogradouro = soc.EndTipoLogradouro;
                    wbSocio.EndDsTipoLogradouro = soc.EndDsTipoLogradouro;
                    wbSocio.EndLogradouro = soc.EndLogradouro;
                    wbSocio.EndNumero = soc.EndNumero;
                    wbSocio.EndComplemento = soc.EndComplemento;
                    #endregion

                    wbSocio.Update(_CodigoEmpresa);

                    soc.Qualificacao = "5";
                    soc.CapitalIntegralizado = 0;
                    soc.Capital_a_Integralizar = 0;
                    soc.QuotaCapitalSocial = 0;
                    soc.Update(_CodigoEmpresa);
                }

            }
        }

        /// <summary>
        /// Cria qualificação da pessoa quando está alterando a qualificação
        /// de Socio para Socio/Adm ou Adm para Socio/Adm
        /// tipo =1 Sococio
        /// tipo = 2 Adm
        /// </summary>
        /// <param name="soc"></param>
        /// <param name="pTipo"></param>
        public void CriaQualificacaoAdm(bSocios soc, int pTipo)
        {
            string _qualificacao = "22";
            string _qualificacaoDescricao = "Sócio";
            int _repLegal = 0;
            if (pTipo == 2)
            {
                _qualificacao = "5";
                _repLegal = 1;
                _qualificacaoDescricao = "ADMINSTRADOR";
            }

            bSocios wbSocio = new bSocios();
            wbSocio.SQPessoa = "0";
            wbSocio.CPFCNPJ = soc.CPFCNPJ;
            wbSocio.Nome = soc.Nome;
            wbSocio.Qualificacao = _qualificacao;
            wbSocio.Qualificacao_Descricao = _qualificacaoDescricao;
            wbSocio.rep_legal = _repLegal;
            wbSocio.tipoacao = 1;
            //wbSocio.DataSaidaSocio = soc.DataSaidaSocio;
            //wbSocio.CapitalIntegralizado = soc.CapitalIntegralizado;
            //wbSocio.Capital_a_Integralizar = soc.Capital_a_Integralizar;
            //wbSocio.QuotaCapitalSocial = soc.QuotaCapitalSocial;

            wbSocio.TipoIdentidade = soc.TipoIdentidade;
            wbSocio.RG = soc.RG;
            wbSocio.TipoPessoa = soc.TipoPessoa;
            wbSocio.OrgaoExpedidor = soc.OrgaoExpedidor;
            wbSocio.OrgaoExpedidorUF = soc.OrgaoExpedidorUF;
            wbSocio.NacionalidadeCodigo = soc.NacionalidadeCodigo;
            wbSocio.Nacionalidade = soc.Nacionalidade;
            wbSocio.Analfabeto = soc.Analfabeto;

            wbSocio.tipo_visto = soc.tipo_visto;
            wbSocio.emissao_visto = soc.emissao_visto;
            wbSocio.validade_visto = soc.validade_visto;
            wbSocio.Justificativa_Visto = soc.Justificativa_Visto;
            wbSocio.in_Sexo = soc.in_Sexo;

            wbSocio.EstadoCivil = soc.EstadoCivil;
            wbSocio.EstadoCivilRegime = soc.EstadoCivilRegime;
            wbSocio.DataNascimento = soc.DataNascimento;
            wbSocio.TipoEmancipado = soc.TipoEmancipado;
            wbSocio.Profissao = "0";
            wbSocio.Profissao_Descricao = soc.Profissao_Descricao;

            wbSocio.Nome_Mae = soc.Nome_Mae;
            wbSocio.Nome_Pai = soc.Nome_Pai;

            #region Endereço
            wbSocio.DDD = soc.DDD;
            wbSocio.Telefone = soc.Telefone;
            wbSocio.Email = soc.Email;
            wbSocio.EndCEP = soc.EndCEP;
            wbSocio.EndUF = soc.EndUF;
            wbSocio.EndMunicipio = soc.EndMunicipio;
            wbSocio.EndBairro = soc.EndBairro;
            wbSocio.EndTipoLogradouro = soc.EndTipoLogradouro;
            wbSocio.EndDsTipoLogradouro = soc.EndDsTipoLogradouro;
            wbSocio.EndLogradouro = soc.EndLogradouro;
            wbSocio.EndNumero = soc.EndNumero;
            wbSocio.EndComplemento = soc.EndComplemento;
            #endregion

            wbSocio.Update(_CodigoEmpresa);
            _Socios.Add(wbSocio);

        }

        public static int VerificaLegalizacaoEmpresa(string pNire)
        {
            return dHelperORACLE.VerificaLegalizacaoEmpresa(pNire);
        }
        #endregion


        #region Exigencia
        public XmlDocument CriaXMLExigencias()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement mainNode;
            XmlCDataSection mainNodecData;
            XmlText mainText;
            XmlElement NoExigencia;
            // Write down the XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            // Create the root element
            XmlElement rootNode = xmlDoc.CreateElement("ROWSET");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);

            //OUTRASEXIGENCIAS

            XmlElement parentNode = xmlDoc.CreateElement("OUTRASEXIGENCIAS");
            xmlDoc.DocumentElement.PrependChild(parentNode);



            foreach (bExigencias outras in _ExigenciasOutras)
            {
                NoExigencia = xmlDoc.CreateElement("EXIGENCIA");
                xmlDoc.DocumentElement.PrependChild(NoExigencia);
                parentNode.AppendChild(NoExigencia);

                mainNode = xmlDoc.CreateElement("OUTRAEXIGENCIA");
                mainNodecData = xmlDoc.CreateCDataSection(outras.Descricao);
                //mainText = xmlDoc.CreateTextNode(outras.Descricao);

                NoExigencia.AppendChild(mainNode);
                mainNode.AppendChild(mainNodecData);


                mainNode = xmlDoc.CreateElement("FUNDAMENTOLEGAL");

                mainNodecData = xmlDoc.CreateCDataSection(outras.FundamentoLegal);
                //mainText = xmlDoc.CreateTextNode(outras.FundamentoLegal);

                NoExigencia.AppendChild(mainNode);
                mainNode.AppendChild(mainNodecData);

            }



            //FIM OUTRAS EXIGENCIAS


            parentNode = xmlDoc.CreateElement("EXIGENCIAS");

            //parentNode.SetAttribute("ID", "01");
            xmlDoc.DocumentElement.PrependChild(parentNode);

            foreach (bExigencias exigencias in _Exigencias)
            {
                mainNode = xmlDoc.CreateElement("CODEXIGENCIA");
                //XmlCDataSection mainNode2 = xmlDoc.CreateCDataSection("CODEXIGENCIA");
                mainText = xmlDoc.CreateTextNode(exigencias.CodExigencia);
                parentNode.AppendChild(mainNode);
                mainNode.AppendChild(mainText);

            }


            XmlElement SITEXIGENCIANode = xmlDoc.CreateElement("SITEXIGENCIA");
            XmlText SITEXIGENCIAText = xmlDoc.CreateTextNode("6");
            SITEXIGENCIANode.AppendChild(SITEXIGENCIAText);
            xmlDoc.DocumentElement.PrependChild(SITEXIGENCIANode);

            XmlElement UsuarioNode = xmlDoc.CreateElement("Usuario");
            XmlText UsuarioText = xmlDoc.CreateTextNode(_UsuarioRegin);
            UsuarioNode.AppendChild(UsuarioText);
            xmlDoc.DocumentElement.PrependChild(UsuarioNode);

            XmlElement DataNode = xmlDoc.CreateElement("Data");
            XmlText DataText = xmlDoc.CreateTextNode("20120716155800");
            DataNode.AppendChild(DataText);
            xmlDoc.DocumentElement.PrependChild(DataNode);

            XmlElement NireNode = xmlDoc.CreateElement("Nire");
            XmlText NireText = xmlDoc.CreateTextNode("");
            NireNode.AppendChild(NireText);
            xmlDoc.DocumentElement.PrependChild(NireNode);

            XmlElement RequerimentoNode = xmlDoc.CreateElement("Requerimento");
            XmlText RequerimentoText = xmlDoc.CreateTextNode(_ProtocoloRequerimento);
            RequerimentoNode.AppendChild(RequerimentoText);
            xmlDoc.DocumentElement.PrependChild(RequerimentoNode);


            XmlElement protocoloNode = xmlDoc.CreateElement("Protocolo");
            XmlText protocoloText = xmlDoc.CreateTextNode(_ProtocoloRCPJ);
            protocoloNode.AppendChild(protocoloText);
            xmlDoc.DocumentElement.PrependChild(protocoloNode);

            return xmlDoc;
        }

        #endregion

        #region Contrato Padrao

        public bool ImprimeContratoPadrao()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT count(*) qtd   ");
            sql.AppendLine(" FROM t030_template_contrato ");
            sql.AppendLine(" where t030_co_ato = " + _CodigoAto);
            sql.AppendLine("  and t030_co_nat_juridica = "+ _NaturezaJuridicaCodigo.ToString() );
            sql.AppendLine("  and t030_status = 1");

            DataTable dt = dHelperQuery.ExecuteQuery(sql.ToString());

            return Int32.Parse(dt.Rows[0][0].ToString()) > 0;
        }


        public void PreencheContratoPerguntas()
        {
            foreach (bContratoConteudo clausula in _contratoPadrao.ListClausula)
            {
                string textoclausula = clausula.T031_texto_original;
                foreach (bContratoCampo objeto in _contratoPadrao.ListCampo)
                {
                    if (objeto.T032_origem_campo == 2)
                    {
                        if (clausula.T031_sq_clausula == objeto.T031_sq_clausula)
                        {
                            textoclausula = textoclausula.Replace(objeto.T032_nome_campo, objeto.Resposta_campo);
                            clausula.T031_texto_clausula = textoclausula;
                            //clausula.T031_texto_clausula = clausula.T031_texto_clausula.Replace(objeto.T032_nome_campo, objeto.Resposta_campo);
                        }
                    }
                }
            }
            
        }

        public void PreencheContrato(string pTipo)
        {
            //pTipo 1 - simplificado LTDA 2 - Completo LTDA
            switch (pTipo)
            {
                case "1":
                    MontaContratoTipo1();
                    break;
                case "3":
                    MontaContratoTipo1();
                    break;
                case "2":
                case "4":
                    MontaContratoTipo2();
                    break;
                case "5":
                    MontaContratoTipo5();
                    break;
                case "6":
                    MontaContratoTipo5();
                    break;
            }

            //Atualiza o texto das clausulas editaveis
            dContratoClausula dc = new dContratoClausula();
            dc.T005_nr_requerimento = _ProtocoloRequerimento;
            dc.T030_id_contrato = Int32.Parse(pTipo);
            DataTable dtClausulaEditada = dc.QueryCalusulasEditadas();
            foreach (DataRow dr in dtClausulaEditada.Rows)
            {
                foreach (bContratoConteudo co in _contratoPadrao.ListClausula)
                {
                    if (co.T031_sq_clausula == Int32.Parse(dr["t031_sq_clausula_mae"].ToString()))
                    {
                        co.T031_texto_clausula = dr["t035_texto"].ToString();
                        break;
                    }
                }
            }

            MontaAssinatura();

        }

        private void MontaContratoTipo1()
        {
            foreach (bContratoConteudo clausula in _contratoPadrao.ListClausula)
            {
                foreach (bContratoCampo campo in clausula.Campos)
                {
                    switch (clausula.T031_sq_clausula)
                    {
                        case 0:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoPadraoQSA());
                            break;
                        case 1:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _SedeNome);
                            break;
                        case 2:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoEndereco());
                            break;
                        case 4:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _ObjetoSocial);
                            break;
                        case 5:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoInicioAtividade());
                            break;
                        case 6:
                            clausula.T031_texto_clausula = ContratoCapitalSocial(clausula.T031_texto_clausula, campo.T032_nome_campo);
                            break;
                        case 7:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoIntegralizacaoCapital());
                            break;
                        case 9:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaAdminstracao(1));
                            break;
                        case 17:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaForo());
                            break;
                        case 18:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, (3 + _Num_Vias).ToString());
                            break;
                    }
                }
                clausula.T031_texto_original = clausula.T031_texto_clausula;
            }
        }

        private void MontaContratoTipo2()
        {
            foreach (bContratoConteudo clausula in _contratoPadrao.ListClausula)
            {
                foreach (bContratoCampo campo in clausula.Campos)
                {
                    switch (clausula.T031_sq_clausula)
                    {
                        case 0:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoPadraoQSA());
                            break;
                        case 1:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _SedeNome);
                            break;
                        case 2:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoEndereco());
                            break;
                        case 4:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _ObjetoSocial);
                            break;
                        case 5:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoInicioAtividade());
                            break;
                        case 6:
                            clausula.T031_texto_clausula = ContratoCapitalSocial(clausula.T031_texto_clausula, campo.T032_nome_campo);
                            break;
                        case 7:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoIntegralizacaoCapital());
                            break;
                        case 10:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaAdminstracao(1));
                            break;
                        case 37:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaForo());
                            break;
                        case 39:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, (3 + _Num_Vias).ToString());
                            break;
                    }
                }
                clausula.T031_texto_original = clausula.T031_texto_clausula;
            }
        }

        private void MontaContratoTipo5()
        {
            foreach (bContratoConteudo clausula in _contratoPadrao.ListClausula)
            {
                foreach (bContratoCampo campo in clausula.Campos)
                {
                    switch (clausula.T031_sq_clausula)
                    {
                        case 0:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoPadraoQSA());
                            break;
                        case 1:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _SedeNome);
                            break;
                        case 2:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoEndereco());
                            break;
                        case 4:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, _ObjetoSocial);
                            break;
                        case 5:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ContratoInicioAtividadeEireli());
                            break;
                        case 66666:
                            clausula.T031_texto_clausula = ContratoCapitalSocial(clausula.T031_texto_original, campo.T032_nome_campo);
                            break;
                        case 6:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, CapitalSocialEireli());
                            break;
                        case 7:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaAdminstracao(2));
                            break;
                        case 12:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, ClausulaForo());
                            break;
                        case 18:
                            clausula.T031_texto_clausula = clausula.T031_texto_original.Replace(campo.T032_nome_campo, (3 + _Num_Vias).ToString());
                            break;
                    }
                }
                clausula.T031_texto_original = clausula.T031_texto_clausula;
            }
        }

        public void MontaAssinatura()
        {
            bContratoAssinatura ass = new bContratoAssinatura();
            //bContratoAssinatura representante = new bContratoAssinatura();

            List<bSocios> pSocioAtivos = GetSociosAtivos();

            foreach (bSocios s in pSocioAtivos)
            {
                ass = new bContratoAssinatura();

                if (s.CPFCNPJ.Length == 11)
                {
                    DateTime wIdade = new DateTime();

                    wIdade = Convert.ToDateTime(s.DataNascimento.ToString());


                    if (ValidaMenorIdade(wIdade))
                    {
                        if (Menor16(wIdade))
                        {

                            foreach (bSocios rep in s.Representantes)
                            {

                                ass = new bContratoAssinatura();
                                ass.Nome = s.Nome.ToUpper();
                                ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                                ass.NomeRepresentante = rep.Nome.ToUpper() + " (" + rep.TipoRepresentante.ToUpper() + ")";
                                ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ);

                                _contratoPadrao.ListAssinatura.Add(ass);
                            }

                        }
                        if (!Menor16(wIdade))
                        {

                            ass = new bContratoAssinatura();
                            ass.Nome = s.Nome.ToUpper();
                            ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                            _contratoPadrao.ListAssinatura.Add(ass);

                            foreach (bSocios rep in s.Representantes)
                            {

                                ass = new bContratoAssinatura();
                                ass.Nome = s.Nome.ToUpper();
                                ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                                ass.NomeRepresentante = rep.Nome.ToUpper() + " (" + rep.TipoRepresentante.ToUpper() + ")";
                                ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ);

                                _contratoPadrao.ListAssinatura.Add(ass);

                            }

                        }
                    }
                    else
                    {

                        if (s.Representantes.Count == 0)
                        {
                            ass = new bContratoAssinatura();
                            ass.Nome = s.Nome.ToUpper();
                            ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                            _contratoPadrao.ListAssinatura.Add(ass);

                        }
                        else
                        {
                            foreach (bSocios rep in s.Representantes)
                            {
                                ass = new bContratoAssinatura();
                                ass.Nome = s.Nome.ToUpper();
                                ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                                ass.NomeRepresentante = "P/P: " + rep.Nome.ToUpper();
                                ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ.ToString());

                                _contratoPadrao.ListAssinatura.Add(ass);
                            }
                        }

                    }
                }
                else
                {
                    foreach (bSocios rep in s.Representantes)
                    {
                        ass = new bContratoAssinatura();
                        ass.Nome = s.Nome.ToUpper();
                        ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                        ass.NomeRepresentante = "REPRESENTANTE " + rep.Nome.ToUpper();
                        ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ.ToString());

                        _contratoPadrao.ListAssinatura.Add(ass);
                    }
                }

                if (!string.IsNullOrEmpty(s.CpfOutorgante))
                {
                    ass = new bContratoAssinatura();

                    ass.Nome = s.NomeOutorgante + " ( OUTORGANTE )";
                    ass.Cpf =  FormataCPFCNPJ(s.CpfOutorgante);
                    _contratoPadrao.ListAssinatura.Add(ass);
                }


            }

            List<bSocios> pAdmAtivos = GetAdministradoresSomente();
            foreach (bSocios cAdm in pAdmAtivos)
            {
                ass = new bContratoAssinatura();

                ass.Nome = cAdm.Nome.ToUpper() + " (ADMINISTRADOR)";
                ass.Cpf = FormataCPFCNPJ(cAdm.CPFCNPJ);

                _contratoPadrao.ListAssinatura.Add(ass);

            }

        }

        public void MontaAssinatura_old()
        {
            bContratoAssinatura ass = new bContratoAssinatura();
            //bContratoAssinatura representante = new bContratoAssinatura();

            List<bSocios> pSocioAtivos = GetSociosAtivos();

            foreach (bSocios s in pSocioAtivos)
            {
                ass = new bContratoAssinatura();

                DateTime wIdade = new DateTime();
                if (s.CPFCNPJ.Length == 11)
                {
                    wIdade = Convert.ToDateTime(s.DataNascimento.ToString());
                }

                if (s.Representantes.Count == 0)
                {
                    ass.Nome = s.Nome.ToUpper();
                    ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);
                    _contratoPadrao.ListAssinatura.Add(ass);
                }
                else
                {
                    if (Menor18(wIdade))
                    {
                        foreach (bSocios rep in s.Representantes)
                        {
                            ass = new bContratoAssinatura();
                            ass.Nome = s.Nome.ToUpper();
                            ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                            ass.NomeRepresentante = rep.Nome.ToUpper() + " (" + rep.TipoRepresentante.ToUpper() + ")";
                            ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ);

                            _contratoPadrao.ListAssinatura.Add(ass);
                        }
                    }
                    else
                    {
                        foreach (bSocios rep in s.Representantes)
                        {
                            ass = new bContratoAssinatura();
                            ass.Nome = s.Nome.ToUpper();
                            ass.Cpf = FormataCPFCNPJ(s.CPFCNPJ);

                            ass.NomeRepresentante = "P/P: " + rep.Nome.ToUpper();
                            ass.CpfRepresentante = FormataCPFCNPJ(rep.CPFCNPJ.ToString());

                            _contratoPadrao.ListAssinatura.Add(ass);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(s.CpfOutorgante))
                {
                    ass = new bContratoAssinatura();

                    ass.Nome = s.NomeOutorgante + " ( OUTORGANTE )";
                    ass.Cpf = "CPF: " + FormataCPFCNPJ(s.CpfOutorgante);
                    _contratoPadrao.ListAssinatura.Add(ass);
                }


            }

            List<bSocios> pAdmAtivos = GetAdministradoresSomente();
            foreach (bSocios cAdm in pAdmAtivos)
            {
                ass = new bContratoAssinatura();

                ass.Nome = cAdm.Nome.ToUpper() + " (ADMINISTRADOR)";
                ass.Cpf = "CPF: " + FormataCPFCNPJ(cAdm.CPFCNPJ);

                _contratoPadrao.ListAssinatura.Add(ass);

            }

        }

        private string ClausulaForo()
        {
            string _ret = "";
            string _txtCalusula = "";

            dcnt_clausulas_adicionais clausula = new dcnt_clausulas_adicionais();
            clausula.reque_protocolo = _ProtocoloRequerimento;
            DataTable dt1 = clausula.Query();
            clausula.num_clausula = "99";
            dt1 = clausula.Query();

            if (dt1.Rows.Count > 0)
            {
                _txtCalusula = dt1.Rows[0]["clausula"].ToString();
            }

            if (!string.IsNullOrEmpty(_Foro))
            {
                _ret = "Fica eleito o foro da comarca de " + _Foro + ", para dirimir as questões";
                _ret += " oriundas do presente contrato.";
            }
            else
            {
                _ret = _txtCalusula;
            }
            return _ret;
        }

        private string ClausulaAdminstracao(int pTipo)
        {
            //pTipo 1- LTDA 2 - EIRELI

            string wClausula9 = string.Empty;
            string _clausulaE = "";

            ArrayList _arrqualificacao = new ArrayList();

            bSocios soctemp = new bSocios();
            bool _btexto = false;

            List<bSocios> pAdministradoresIsoladamente = GetAdministradoresIsoladamente();
            foreach (bSocios ss in pAdministradoresIsoladamente)
            {
                wClausula9 += _clausulaE;

                if (pTipo == 1)
                {
                    if (VerificaSocioAdm(ss.SQPessoa))
                        wClausula9 += " ISOLADAMENTE a(o) Sócio(a) " + ss.Nome + " ";
                    else
                        wClausula9 += " ISOLADAMENTE a(o) não Sócio " + ss.Nome + " ";
                }
                else
                {
                    if (VerificaSocioAdm(ss.SQPessoa))
                        wClausula9 += " ISOLADAMENTE a " + ss.Nome + " ";
                    else
                        wClausula9 += " ISOLADAMENTE a " + ss.Nome + " ";
                }

                if (ss.Qualificacao == "5" && !VerificaSocioAdm(ss.SQPessoa))
                {
                    wClausula9 += GetDadosQSA(ss, false, false, false, false) + ", ";
                    _arrqualificacao.Add(ss.SQPessoa);
                }
                _clausulaE = ", ";
                _btexto = true;
            }

            if (_clausulaE != "")
                _clausulaE = " e a ";

            List<bSocios> pAdministradoresAtivos = GetAdministradoresAtivos();
            foreach (bSocios ss in pAdministradoresAtivos)
            {

                if (ss.AdminstracaoConjuntamente == 1)
                {
                    wClausula9 += _clausulaE + ss.Nome;

                    if (ss.Qualificacao == "5" && !VerificaSocioAdm(ss.SQPessoa))
                    {
                        wClausula9 += ", " + GetDadosQSA(ss, false, false, false, false) + ", ";
                        _arrqualificacao.Add(ss.SQPessoa);
                    }
                    wClausula9 += " CONJUNTAMENTE com ";
                    _btexto = true;

                    foreach (bAdminstracao badm in ss.ListAdmnistracaoConjunto)
                    {
                        soctemp = BuscaSocioLista(badm.SQPessoaConjunto);
                        if (soctemp != null)
                        {
                            if (pTipo == 1)
                            {
                                if (VerificaSocioAdm(soctemp.SQPessoa))
                                    wClausula9 += " a(o) Sócio " + soctemp.Nome + " ";
                                else
                                    wClausula9 += " a(o) não Sócio " + soctemp.Nome + " ";
                            }
                            else
                            {
                                if (VerificaSocioAdm(soctemp.SQPessoa))
                                    wClausula9 += " a " + soctemp.Nome + " ";
                                else
                                    wClausula9 += " a " + soctemp.Nome + " ";
                            }


                            if (soctemp.Qualificacao == "5" && !VerificaSocioAdm(ss.SQPessoa))
                            {
                                wClausula9 += GetDadosQSA(soctemp, false, false, false, false) + ", ";
                                _arrqualificacao.Add(soctemp.SQPessoa);
                            }
                        }
                    }

                }
            }
            //List<bSocios> pAdministradoresAtivos = GetAdministradoresAtivos();
            foreach (bSocios ss in pAdministradoresAtivos)
            {
                if (ss.AdminstracaoTodos == 1)
                {
                    if (_btexto)
                        _clausulaE = " e a ";
                    if (pTipo == 1)
                    {
                        if (VerificaSocioAdm(ss.SQPessoa))
                            wClausula9 += _clausulaE + "  a(o) Sócio " + ss.Nome.ToString().ToUpper();
                        else
                            wClausula9 += _clausulaE + "  a(o) não Sócio " + ss.Nome.ToString().ToUpper();
                    }
                    else
                    {
                        if (VerificaSocioAdm(ss.SQPessoa))
                            wClausula9 += _clausulaE + ss.Nome.ToString().ToUpper();
                        else
                            wClausula9 += _clausulaE + ss.Nome.ToString().ToUpper();
                    }

                    // wClausula9 += _clausulaE + ss.Nome.ToString().ToUpper();
                    if (ss.Qualificacao == "5" && !VerificaSocioAdm(ss.SQPessoa))
                    {
                        wClausula9 += GetDadosQSA(ss, false, false, false, false);
                        _arrqualificacao.Add(ss.SQPessoa);
                    }

                    wClausula9 += " CONJUNTAMENTE com todos os demais administradores ";
                }
            }
            return wClausula9;
        }

        private string ContratoPreambulo(string _texto, string campo)
        {
            if (campo == "@PREAMBULO@")
            {
                _texto = _texto.Replace(campo, ContratoPadraoQSA());
            }
            if (campo == "@LEI6404@")
            {
                _texto = _texto.Replace(campo, FormataValor(_QtdCotas) + " (" + GetNumeroporExtenso(_QtdCotas, false).ToLower() + ")");
            }

            return _texto;
        }

        private string ContratoCapitalSocial(string _texto, string campo)
        {
            if (campo == "@CAPITALSOCIAL@")
            {
                _texto = _texto.Replace(campo, FormataReal(_CapitalSocial) + " (" + GetNumeroporExtenso(_CapitalSocial, true) + ")");
            }
            if (campo == "@QUOTAS@")
            {
                _texto = _texto.Replace(campo, FormataValor(_QtdCotas) + " (" + GetNumeroporExtenso(_QtdCotas, false).ToLower() + ")");
            }
            if (campo == "@VALORQUOTAS@")
            {
                _texto = _texto.Replace(campo, FormataReal(_ValorCota) + " (" + GetNumeroporExtenso(_ValorCota, true) + ")");
            }
            if (campo == "@TABELASOCIOSCAPITAL@")
            {
                _texto = _texto.Replace(campo, MontaTabelaSocioContrato());
            }
            return _texto;
        }

        private string ContratoIntegralizacaoCapital()
        {
            string wClausula6 = "";

            /* _moeda_corrente = 0 -> integralizado em moeda corrente
             * _moeda_corrente = 1 -> integralizado em moeda corrente ou bens
             * _capital_nao_integralizado -> valor do capital não integralizado
             * 
            */
            //O capital social está totalmente integralizado nesta data, conforme segue:
            if (_capital_nao_integralizado == 0)
            {
                if (_moeda_corrente == "0")
                {
                    wClausula6 += " O capital social está totalmente integralizado nesta data, em moeda corrente nacional.";
                }
                else
                {
                    wClausula6 += " O capital social está totalmente integralizado nesta data, conforme segue: ";
                    if (!String.IsNullOrEmpty(_ds_capital_nao_integralizado))
                    {
                        wClausula6 += TrimAll(_ds_capital_nao_integralizado);
                    }
                }
            }
            else
            {
                if (_moeda_corrente == "0")
                {
                    if (_capital_integralizado > 0)
                    {
                        wClausula6 += "O capital social integralizado neste ato é de  R$ " + FormataReal(_capital_integralizado) + " (" + GetNumeroporExtenso(_capital_integralizado, true) + ") em moeda corrente nacional";
                        wClausula6 += " e o valor de R$ " + FormataReal(_capital_nao_integralizado) + " (" + GetNumeroporExtenso(_capital_nao_integralizado, true) + ") à integralizar até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao) + ", sendo que:";
                        int ordem = 1;
                        foreach (bSocios soc in _SociosAtivos)
                        {
                            //o  sócio  FULANO DE TAL  integraliza  XX  (quantidade de cotas por extenso)
                            //quotas, no valor de R$ XX,XX (valor das cotas por extenso), até tal data....
                            //wClausula6 += soc.Nome + " integraliza " + FormataValor(soc.QuotaCapitalSocial) + " (" + GetNumeroporExtenso(soc.QuotaCapitalSocial, false).ToLower() + ") ";
                            wClausula6 += "<br>" + ordem.ToString() + " - " + soc.Nome + " integraliza R$ " + FormataReal(soc.CapitalIntegralizado) + " (" + GetNumeroporExtenso(soc.CapitalIntegralizado, true).ToLower() + ") neste ato";
                            if (soc.Capital_a_Integralizar > 0)
                            {
                                wClausula6 += " e integralizará R$ " + FormataReal(soc.Capital_a_Integralizar) + " (" + GetNumeroporExtenso(soc.Capital_a_Integralizar, true).ToLower() + ") até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao)+", em moeda corrente nacional";
                            }
                            wClausula6 += ".";
                            ordem++;
                        }
                    }
                    else
                    {
                        wClausula6 += "O capital social subscrito será integralizado pelos sócios quotistas acima qualificados, conforme segue:";
                        int ordem = 1;
                        foreach (bSocios soc in _SociosAtivos)
                        {
                            //o  sócio  FULANO DE TAL  integraliza  XX  (quantidade de cotas por extenso)
                            //quotas, no valor de R$ XX,XX (valor das cotas por extenso), até tal data....
                            //wClausula6 += soc.Nome + " integraliza " + FormataValor(soc.QuotaCapitalSocial) + " (" + GetNumeroporExtenso(soc.QuotaCapitalSocial, false).ToLower() + ") ";
                            wClausula6 += "<br>" + ordem.ToString() + " - " + soc.Nome + " integralizará R$ " + FormataReal(soc.Capital_a_Integralizar) + " (" + GetNumeroporExtenso(soc.Capital_a_Integralizar, true).ToLower() + ") até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao) + ", em moeda corrente nacional";
                            wClausula6 += ".";
                            ordem++;
                        }

                    }
                }
                else
                {
                    if (_capital_integralizado > 0)
                    {
                        wClausula6 += "O capital social integralizado neste ato é de  R$ " + FormataReal(_capital_integralizado) + " (" + GetNumeroporExtenso(_capital_integralizado, true) + ")";
                        wClausula6 += " e o valor de R$ " + FormataReal(_capital_nao_integralizado) + " (" + GetNumeroporExtenso(_capital_nao_integralizado, true) + ") à integralizar até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao) + ", conforme segue: ";
                        if (!String.IsNullOrEmpty(_ds_capital_nao_integralizado))
                        {
                            wClausula6 += "<br>";       
                            wClausula6 += TrimAll(_ds_capital_nao_integralizado);
                        }
                    }
                    else
                    {
                        wClausula6 += "O capital social subscrito será integralizado pelos sócios quotistas acima qualificados, conforme segue: ";
                        if (!String.IsNullOrEmpty(_ds_capital_nao_integralizado))
                        {
                            wClausula6 += "<br>";
                            wClausula6 += TrimAll(_ds_capital_nao_integralizado);
                        }
                    }
                    //int ordem = 1;
                    //foreach (bSocios soc in _SociosAtivos)
                    //{
                    //    //o  sócio  FULANO DE TAL  integraliza  XX  (quantidade de cotas por extenso)
                    //    //quotas, no valor de R$ XX,XX (valor das cotas por extenso), até tal data....
                    //    //wClausula6 += soc.Nome + " integraliza " + FormataValor(soc.QuotaCapitalSocial) + " (" + GetNumeroporExtenso(soc.QuotaCapitalSocial, false).ToLower() + ") ";
                    //    wClausula6 += "<br>" + ordem.ToString() + " - " + soc.Nome + " integraliza R$ " + FormataReal(soc.CapitalIntegralizado) + " (" + GetNumeroporExtenso(soc.CapitalIntegralizado, true).ToLower() + ") neste ato";
                    //    if (soc.Capital_a_Integralizar > 0)
                    //    {
                    //        wClausula6 += " e a integralizar R$ " + FormataReal(soc.Capital_a_Integralizar) + " (" + GetNumeroporExtenso(soc.Capital_a_Integralizar, true).ToLower() + ") até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao);
                    //    }
                    //    ordem++;
                    //}
                }
            }
            return wClausula6;
        }

        private string ContratoCapitalaIntegralizar()
        {
            string wClausula6 = "";
            if (ds_capital_nao_integralizado != "")
            {
                wClausula6 = " O capital social subscrito, será integralizado pelos sócios quotistas acima qualificadas, conforme segue:";
                wClausula6 += ds_capital_nao_integralizado;
            }
            return wClausula6;
        }

        private string CapitalSocialEireli()
        {
            string wClausula6 = " A empresa tem o capital de R$ " + FormataReal(_CapitalSocial) + " (" + GetNumeroporExtenso(_CapitalSocial, true) + ")";

            if (_capital_integralizado == _CapitalSocial)
            {
                if (_moeda_corrente == "0") //em moeda
                {
                    wClausula6 += ", totalmente subscrito e integralizado, neste ato, em moeda corrente nacional, de responsabilidade do titular.";
                }
                else
                {
                    wClausula6 += ", subscrito e integralizado, neste ato, da seguinte forma: ";
                    wClausula6 += TrimAll(_ds_capital_nao_integralizado);
                    wClausula6 += ", de responsabilidade do titular.";
                }
            }
            else
            {
                if (_moeda_corrente == "0") //em moeda
                {
                    wClausula6 += ", integralizado neste ato R$ " + FormataReal(_capital_integralizado) + " (" + GetNumeroporExtenso(_capital_integralizado, true) + ") em moeda corrente nacional";
                    if (_capital_nao_integralizado != 0)
                    {
                        wClausula6 += ", sendo que os R$ " + FormataReal(_capital_nao_integralizado) + " (" + GetNumeroporExtenso(_capital_nao_integralizado, true) + ") restantes serão integralizados até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao) + ".";
                    }
                }
                else
                {
                    //outras formas
                    wClausula6 += ", integralizado neste ato R$ " + FormataReal(_capital_integralizado) + " (" + GetNumeroporExtenso(_capital_integralizado, true) + ")";
                    wClausula6 += " da seguinte forma: ";
                    wClausula6 += TrimAll(_ds_capital_nao_integralizado);
                    if (_capital_nao_integralizado != 0)
                    {
                        wClausula6 += ", sendo que os R$ " + FormataReal(_capital_nao_integralizado) + " (" + GetNumeroporExtenso(_capital_nao_integralizado, true) + ") restantes serão integralizados até " + String.Format("{0:dd/MM/yyyy}", _data_limite_integralizacao) + ", de responsabilidade do titular.";
                    }
                }
            }
            return wClausula6;
        }

        private string MontaTabelaSocioContrato()
        {
            string _resultado = "";
            decimal totalQuota = 0;
            decimal totalPerc = 0;
            decimal totalValores = 0;
            try
            {
                if (SociosAtivos.Count > 0)
                {
                    _resultado += "<table cellspacing=\"0\" cellpadding=\"3\" border=\"1\" width=\"100%\">";
                    _resultado += "<tr>";
                    _resultado += "<td align=\"center\" width=\"10%\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">N. ORDEM</font></b></td>";
                    _resultado += "<td align=\"center\"   width=\"40%\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">SÓCIOS</font></b></td>";
                    _resultado += "<td align=\"center\"  width=\"10%\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">QUOTAS</font></b></td>";
                    _resultado += "<td align=\"center\"  width=\"10%\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">%</font></b></td>";
                    _resultado += "<td align=\"center\" width=\"10%\"></td>";
                    _resultado += "<td align=\"center\"  width=\"20%\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">VALORES</font></b></td>";
                    _resultado += "</tr>";
                    int ordem = 1;
                    foreach (bSocios socio in SociosAtivos)
                    {
                        _resultado += "<tr>";

                        _resultado += "<td align=\"center\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + ordem.ToString() + "</font></td>";
                        _resultado += "<td align=\"left\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + socio.Nome + "</font></td>";
                        _resultado += "<td align=\"right\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataValor(socio.QuotaCapitalSocial).ToString() + "</font></td>";
                        _resultado += "<td align=\"right\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataReal(socio.PercentualCapital).ToString() + "</font></td>";
                        _resultado += "<td align=\"center\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">R$</font></td>";
                        _resultado += "<td align=\"right\"><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataReal(socio.CapitalIntegralizado + socio.Capital_a_Integralizar).ToString() + "</font></td>";

                        _resultado += "</tr>";

                        totalQuota += socio.QuotaCapitalSocial;
                        totalPerc += socio.PercentualCapital;
                        totalValores += (socio.CapitalIntegralizado + socio.Capital_a_Integralizar);

                        ordem++;
                    }

                    _resultado += "<tr>";

                    _resultado += "<td align=\"center\" colspan=\"2\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">TOTAL</font></b></td>";
                    _resultado += "<td align=\"right\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataValor(totalQuota).ToString() + "</font></b></td>";
                    _resultado += "<td align=\"right\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataReal(totalPerc).ToString() + "</font></b></td>";
                    _resultado += "<td align=\"center\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">R$</font></b></td>";
                    _resultado += "<td align=\"right\"><b><font face=\"Times New Roman\" size=\"3\" color=\"black\">" + FormataReal(totalValores).ToString() + "</font></b></td>";

                    _resultado += "</tr>";

                    _resultado += "</table>";
                }
            }
            catch (Exception e)
            {
                _resultado = "";
            }
            return _resultado;
        }

        private string ContratoPadraoQSA()
        {
            string IdentificaSocios = "";
            int _qtdSocio = SociosAtivos.Count;
            int _contSocio = 0;
            foreach (bSocios s in SociosAtivos)
            {
                if (s.tipoacao != 5)
                {
                    _contSocio++;
                    IdentificaSocios += GetDadosQSA(s, false, false);
                    if (_contSocio != _qtdSocio)
                        IdentificaSocios += "<br/><br/>";
                }

            }

            return IdentificaSocios;
        }
        private string ContratoEndereco()
        {
            string ret = "";
            ret = _SedeDsTipoLogradouro.ToUpper() + " " + _SedeLogradouro.ToUpper() + ", " + _SedeNumero.ToUpper();
            ret += ((_SedeComplemento.ToUpper() != String.Empty) ? (", " + _SedeComplemento.ToUpper()) : "");
            ret += ", " + _SedeBairro.ToUpper() + ", " + _nomeMunicipioSede.ToUpper() + ", " + _SedeUF.ToUpper() + ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(_SedeCEP));

            return ret;
        }
        private string ContratoInicioAtividade()
        {
            string _nomeJunta = "";
            //bOrgaoRegistro corgao = new bOrgaoRegistro(_CNPJ_Orgao_Registro);
            //_nomeJunta = corgao.descricao;

            _nomeJunta = _orgaoRegistro.descricao;

            string ret = "";
            if (!string.IsNullOrEmpty(_DataInicioSociedade.ToString()))
            {
                ret = " A sociedade iniciará suas atividades a partir de " + String.Format("{0:dd/MM/yyyy}", _DataInicioSociedade);

                if (_indicadorSPE == 1)
                {
                    ret += " e seu prazo de duração será determinado e vinculado a consecução do objeto social.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(_AssociacaoTempoDuracao.ToString()))
                    {
                        DateTime wData1 = Convert.ToDateTime(_AssociacaoTempoDuracao.ToString());
                        ret += " e seu prazo de duração será até " + wData1.ToString("dd/MM/yyyy") + ".";
                    }
                    else
                        ret += " e seu prazo de duração será indeterminado.";
                }
            }
            else
            {

                //ret = " A empresa iniciará suas atividades a partir da data do arquivamento ";
                ret = " A sociedade iniciará suas atividades a partir do registro deste ato perante a Junta Comercial do Estado de Santa Catarina ";
                if (_indicadorSPE == 1)
                {
                    ret += "e seu prazo de duração será determinado e vinculado a consecução do objeto social.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(_AssociacaoTempoDuracao.ToString()))
                    {
                        DateTime wData1 = Convert.ToDateTime(_AssociacaoTempoDuracao.ToString());
                        ret += " e seu prazo de duração será até " + wData1.ToString("dd/MM/yyyy") + ". \n\n ";
                    }
                    else
                        ret += " e seu prazo de duração será indeterminado.\n\n";
                }
            }


            return ret;
        }

        private string ContratoInicioAtividadeEireli()
        {
            string _nomeJunta = "";

            _nomeJunta = _orgaoRegistro.descricao;

            string ret = "";
            if (!string.IsNullOrEmpty(_DataInicioSociedade.ToString()))
            {
                ret = " A empresa iniciará suas atividades a partir de " + String.Format("{0:dd/MM/yyyy}", _DataInicioSociedade);

                if (_indicadorSPE == 1)
                {
                    ret += " e seu prazo de duração será determinado e vinculado a consecução do objeto social.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(_AssociacaoTempoDuracao.ToString()))
                    {
                        DateTime wData1 = Convert.ToDateTime(_AssociacaoTempoDuracao.ToString());
                        ret += " e seu prazo de duração será até " + wData1.ToString("dd/MM/yyyy") + ".";
                    }
                    else
                        ret += " e seu prazo de duração será indeterminado.";
                }
            }
            else
            {

                //ret = " A empresa iniciará suas atividades a partir da data do arquivamento ";
                ret = " A empresa iniciará suas atividades a partir do registro deste ato perante a Junta Comercial do Estado de Santa Catarina ";
                if (_indicadorSPE == 1)
                {
                    ret += "e seu prazo de duração será determinado e vinculado a consecução do objeto social.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(_AssociacaoTempoDuracao.ToString()))
                    {
                        DateTime wData1 = Convert.ToDateTime(_AssociacaoTempoDuracao.ToString());
                        ret += " e seu prazo de duração será até " + wData1.ToString("dd/MM/yyyy") + ". \n\n ";
                    }
                    else
                        ret += " e seu prazo de duração será indeterminado.\n\n";
                }
            }


            return ret;
        }

        private bSocios BuscaSocioLista(int idSqpessoa)
        {
            foreach (bSocios adm in _Socios)
            {
                if (adm.SQPessoa == idSqpessoa.ToString())
                {
                    return adm;
                }
            }
            return null;
        }

        private string ProcuraTexto(string _texto, string _search)
        {
            string wTexto = "";
            int _pos = 0;
            int _posFinal = 0;

            if (_texto.Trim() != "")
            {
                _pos = _texto.IndexOf("@", 0);

                if (_pos >= 0)
                {
                    _posFinal = _texto.IndexOf("@", _pos + 1);
                    wTexto = _texto.Substring(_pos, _posFinal - _pos + 1);
                }
            }
            return wTexto;
        }

        private string GetNumeroporExtenso(decimal wNumero, bool comReal)
        {
            string ret = "";
            if (comReal)
            {
                NumeroExtenso.Extenso NE = new NumeroExtenso.Extenso();
                ret = NE.NumeroToExtenso(wNumero).ToString().Trim();
            }
            else
            {
                ret = toExtenso(wNumero);
                //ret = new NumeroMyExtensoSemReal.MyExtensoSemReal().MyExtenso_Valor(wNumero);
            }

            return ret;
        }
        private string FormataValor(decimal wValor)
        {
            return String.Format("{0:#,0}", wValor);
        }
        private string FormataReal(decimal wValor)
        {
            return String.Format("{0:#,0.00}", wValor);
        }

        private string toExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " Trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " Trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " Bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " Bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " Milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " Milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " Mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);

                    if (i == 12)
                    {
                        //if (valor_por_extenso.Length > 8)
                        //    if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                        //        valor_por_extenso += " de";
                        //    else
                        //        if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                        //            valor_por_extenso += " de";
                        //        else
                        //            if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                        //                valor_por_extenso += " de";

                        //if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                        //    valor_por_extenso += " REAL";
                        //else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                        //    valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }

                    //if (i == 15)
                    //    if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                    //        valor_por_extenso += " CENTAVO";
                    //    else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                    //        valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }

        private string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "Cem" : "Cento";
                else if (a == 2) montagem += "Duzentos";
                else if (a == 3) montagem += "Trezentos";
                else if (a == 4) montagem += "Quatrocentos";
                else if (a == 5) montagem += "Quinhentos";
                else if (a == 6) montagem += "Seiscentos";
                else if (a == 7) montagem += "Setecentos";
                else if (a == 8) montagem += "Oitocentos";
                else if (a == 9) montagem += "Novecentos";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "Dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "Onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "Um";
                    else if (c == 2) montagem += "Dois";
                    else if (c == 3) montagem += "Três";
                    else if (c == 4) montagem += "Quatro";
                    else if (c == 5) montagem += "Cinco";
                    else if (c == 6) montagem += "Seis";
                    else if (c == 7) montagem += "Sete";
                    else if (c == 8) montagem += "Oito";
                    else if (c == 9) montagem += "Nove";

                return montagem;
            }
        }
        public string FormataCPFCNPJ(string cpf)
        {
            cpf = cpf.Trim();
            string _cpfLabel = "CPF: ";
            System.Text.StringBuilder str = new System.Text.StringBuilder();

            if (cpf.Length == 11)
            {
                str.Append(cpf.Substring(0, 3));
                str.Append(".");
                str.Append(cpf.Substring(3, 3));
                str.Append(".");
                str.Append(cpf.Substring(6, 3));
                str.Append("-");
                str.Append(cpf.Substring(9, 2));
            }
            else
            {
                _cpfLabel = "CNPJ: ";

                str.Append(cpf.Substring(0, 2));
                str.Append(".");
                str.Append(cpf.Substring(2, 3));
                str.Append(".");
                str.Append(cpf.Substring(5, 3));
                str.Append("/");
                str.Append(cpf.Substring(8, 4));
                str.Append("-");
                str.Append(cpf.Substring(12, 2));
            }

            return _cpfLabel + str.ToString();
        }

        private string getNomeEstado(string _UF)
        {

            return "";

        }
        #endregion

        #region Autonomo
        public void EnviaRequerimentoAutonomo(List<string> pEventos)
        {

            try
            {
                /*
                 * Autonomo tem qu passar 1 ou 2
                 * Demais passar 4
                 * pro_tge_vgacao, 1=constituição 2=alteração 4 =inscrição municipal
                 * 
                 */
                string _xml = "";
                string _tgeAcao = "4";
                //vem da viabilidade
                string _vTipRegistro = "12";
                if (_tipoRegistroViab != "0")
                    _vTipRegistro = _tipoRegistroViab;

                if (_NaturezaJuridicaCodigo == 9999)
                {
                    if (_CodigoAto == "001")
                        _tgeAcao = "1";
                    else if (_CodigoAto == "002")
                        _tgeAcao = "2";
                }

                //using (dInscricaoEstadual c = new dInscricaoEstadual())
                //{
                //    c.Protocolo = _ProtocoloRequerimento;
                //    _xml = c.GetXML();
                //}
                //1-Chmar rotina para gerar o XML
                //2-gravar nas tabelas PSC_PROTOCOLO, PSC_PROTOCOLO_IDENT, PSC_PROT_EVENTO_RFB, WBS_CONTROL DE ENVIO

                using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    #region PSC_PROTOCOLO
                    using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                    {
                        p.MainConnectionProvider = cp;
                        p.Pro_protocolo = _ProtocoloRequerimento;
                        p.Pro_status = 1;
                        p.Pro_fec_inc = dHelperORACLE.SysdateOracle();
                        p.Pro_tmu_tuf_uf = _orgaoRegistro.uf;
                        p.Pro_tmu_cod_mun = _codMunicipioInscMunicipal;
                        p.Pro_tip_operacao = 53;
                        p.Pro_env_sef = 2;
                        p.Pro_flag_vigilancia = 2;
                        p.Pro_fec_atualizacao = dHelperORACLE.SysdateOracle();
                        p.Pro_tge_tgacao = 700;
                        p.Pro_tge_vgacao = Int32.Parse(_tgeAcao);
                        p.Pro_cnpj_org_reg = _CNPJ_Orgao_Registro;
                        p.PRO_NR_REQUERIMENTO = _ProtocoloRequerimento;
                        p.PRO_VPV_COD_PROTOCOLO = _ProtocoloViabilidade;

                        p.Update();

                    }

                    #endregion

                    #region PSC_IDENT_PROTOCOLO
                    StringBuilder sqlIdent = new StringBuilder();
                    sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                    sqlIdent.Append("'" + _ProtocoloRequerimento + "'");
                    sqlIdent.Append(", '" + _Socios[0].CPFCNPJ + "'");
                    sqlIdent.Append(", '" + _Socios[0].Nire + "'");
                    sqlIdent.Append(", '" + _Socios[0].Nome + "'");
                    sqlIdent.Append(" )");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlIdent);
                    }

                    #endregion

                    #region PSC_RECEITA_ARQUIVO
                    StringBuilder sqlWbs = new StringBuilder();
                    sqlWbs.AppendLine(@"INSERT INTO PSC_RECEITA_ARQUIVO
                                           (PRA_PRO_PROTOCOLO
                                           ,PRA_ARQUIVO
                                           ,PRA_NOME_ARQUIVO)
                                     VALUES ( ");
                    sqlWbs.Append("'" + _ProtocoloRequerimento + "'");
                    sqlWbs.Append(" ,'' ");
                    sqlWbs.Append(" ,'" + _ProtocoloRequerimento + ".xml'");
                    sqlWbs.Append(" )");
                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlWbs);
                    }

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.GravaReceitaArquivo(_ProtocoloRequerimento, _xml);
                    }

                    #endregion

                    #region MAC_LOG_CARGA_JUNTA_HOMOLOG
                    using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                    {
                        m.MainConnectionProvider = cp;

                        m.MLC_PROTOCOLO = _ProtocoloRequerimento;
                        m.MLC_CPF_HOMOLOGADOR = "11111111111";
                        m.MLC_DTA_HOMOLOGACAO = dHelperORACLE.SysdateOracle();
                        m.MLC_DATA_CARREGA_WS11 = dHelperORACLE.SysdateOracle();
                        m.Update();

                    }
//                    StringBuilder sqlHomolog = new StringBuilder();
//                    sqlHomolog.AppendLine(@" INSERT INTO MAC_LOG_CARGA_JUNTA_HOMOLOG
//                                                                       (MLC_PROTOCOLO
//                                                                       ,MLC_DTA_HOMOLOGACAO
//                                                                       ,MLC_CPF_HOMOLOGADOR
//                                                                       ,MLC_DATA_CARREGA_WS11)
//                                                                 VALUES           ( ");
//                    sqlHomolog.AppendLine("'" + _ProtocoloRequerimento + "'");
//                    sqlHomolog.AppendLine(" ,sysdate " );
//                    sqlHomolog.AppendLine(" , '11111111111'");
//                    sqlHomolog.AppendLine(" ,sysdate ");
//                    sqlHomolog.AppendLine(" )");

                    //sqlHomolog.AppendLine("'" + _ProtocoloRequerimento + "'");
                    //sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    //sqlHomolog.AppendLine(" , '11111111111'");
                    //sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    //sqlHomolog.AppendLine(" )");

                    //using (dHelperORACLE c = new dHelperORACLE())
                    //{
                    //    c.MainConnectionProvider = cp;
                    //    c.ExecuteNonQuery(sqlHomolog);
                    //}
                    #endregion

                    #region RUC_GENERAL
                    using (Ruc_General rg = new Ruc_General())
                    {
                        rg.MainConnectionProvider = cp;
                        rg.rge_pra_protocolo = _ProtocoloRequerimento;
                        rg.rge_ruc = "";
                        rg.rge_tge_ttip_reg = 257;
                        rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                        rg.rge_tge_ttip_ctrib = 153;
                        rg.rge_tge_vtip_ctrib = 9999;
                        rg.rge_tge_ttip_pers = 233;
                        rg.rge_tge_vtip_pers = 1;
                        rg.rge_cgc_cpf = _Socios[0].CPFCNPJ;
                        rg.rge_tge_ttamanho = 21;
                        rg.rge_tge_vtamanho = 3;
                        rg.rge_nomb = _Socios[0].Nome;
                        if (_SedeMunicipio == "")
                            rg.rge_codg_mun = Decimal.Parse(_orgaoRegistro.codigo_municipio.ToString());
                        else
                            rg.rge_codg_mun = Decimal.Parse(_SedeMunicipio);
                        rg.rge_tae_cod_actvd = Decimal.Parse(_CNAEs[0].CodigoCNAE);
                        rg.rge_tuf_cod_uf = _orgaoRegistro.uf;

                        rg.Update();

                    }
                    #endregion

                    #region RUC_PROF
                    List<string> _listSocioGravado = new List<string>();
                    bool _socioGravado = false;
                    using (Ruc_Prof rp = new Ruc_Prof())
                    {
                        foreach (bSocios s in _Socios)
                        {
                            _socioGravado = false;
                            foreach (string _busca in _listSocioGravado)
                            {
                                if (_busca == s.CPFCNPJ)
                                {
                                    _socioGravado = true;
                                    break;
                                }
                            }
                            if (!_socioGravado)
                            {
                                rp.MainConnectionProvider = cp;
                                rp.rpr_rge_pra_protocolo = _ProtocoloRequerimento;
                                rp.rpr_fec_const_nasc = DateTime.Parse(s.DataNascimento.ToString());
                                //rp.rpr_num_reg_merc;
                                //rp.rpr_fec_reg_merc;
                                rp.rpr_tge_ttip_doc = 151;
                                rp.rpr_tge_vtip_doc = decimal.Parse(s.TipoIdentidade);
                                rp.rpr_num_doc_ident = s.RG;
                                //rp.rpr_fec_emi_doc_id;
                                rp.rpr_sexo = s.in_Sexo == "M" ? 1 : 2;
                                rp.rpr_nume = s.EndNumero;
                                //rp.rpr_caja_po;
                                //rp.rpr_zona_caja_po;
                                rp.rpr_tge_tpais = 22;
                                rp.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                                rp.rpr_emis_doc_ident = s.OrgaoExpedidor;
                                rp.rpr_ident_comp = s.EndComplemento;
                                rp.rpr_refer = "";
                                rp.rpr_ttl_tip_logradoro = s.EndDsTipoLogradouro;
                                rp.rpr_direccion = s.EndLogradouro;
                                rp.rpr_urbanizacion = s.EndBairro;
                                rp.rpr_tes_cod_estado = s.EndUF;
                                rp.rpr_zona_postal = s.EndCEP;
                                rp.rpr_tmu_cod_mun = Decimal.Parse(s.EndMunicipio);
                                rp.rpr_cgc_cpf_secd = s.CPFCNPJ;
                                rp.rpr_tge_ttip_pers = 233;
                                rp.rpr_tge_vtip_pers = s.TipoPessoa == "F" ? 1 : 2;
                                rp.rpr_nomb = s.Nome;
                                //rp.rpr_cnpj_vacio;
                                //rp.rpr_uf_emis;
                                rp.rpr_nome_pai = s.Nome_Pai;
                                rp.rpr_nome_mae = s.Nome_Mae;
                                rp.rpr_email = s.Email;
                                rp.rpr_escolaridade = s.EscolaridadeCodigo.ToString();

                                _listSocioGravado.Add(s.CPFCNPJ);

                                rp.Update();
                            }
                        }
                    }

                    #endregion

                    #region RUC_RELAT_PROF
                    using (Ruc_Relat_Prof rp = new Ruc_Relat_Prof())
                    {
                        foreach (bSocios s in _Socios)
                        {
                            rp.MainConnectionProvider = cp;
                            rp.rrp_rge_pra_protocolo = _ProtocoloRequerimento;
                            rp.rrp_cgc_cpf_secd = s.CPFCNPJ;
                            rp.rrp_tge_ttip_relac = 24;
                            rp.rrp_tge_vtip_relac = 2;
                            //rp.rrp_fec_inic_part;
                            //rp.rrp_fec_fin_part;
                            //rp.rrp_crc_ctdr;
                            //rp.rrp_uf_crc_ctdr;
                            //rp.rrp_ubic_libr_fisc;
                            //rp.rrp_tge_tsit_empl;
                            //rp.rrp_tge_vsit_empl;
                            //rp.rrp_fec_actl;
                            //rp.rrp_porc_part;
                            //rp.rrp_porc_cap_vota;
                            //rp.rrp_tip_doc;
                            rp.rrp_tge_tcod_qual = 23;
                            rp.rrp_tge_vcod_qual = Decimal.Parse(s.Qualificacao.ToString());
                            rp.rrp_cedu_prof = "";
                            rp.rrp_desc_doc = "";
                            rp.rrp_tus_cod_usr = "JUCESC";
                            rp.rrp_cnpj_vacio = 0;
                            rp.Update();
                        }

                    }
                    #endregion

                    #region RUC_ESTAB
                    using (Ruc_Estab rb = new Ruc_Estab())
                    {
                        rb.MainConnectionProvider = cp;
                        rb.res_rge_pra_protocolo = _ProtocoloRequerimento;
                        rb.res_ide_estab = 0;
                        rb.res_tip_estab = 1;
                        rb.res_tge_ttip_reg = 155;
                        rb.res_tge_vtip_reg = 9999;
                        rb.res_tge_tcond_uso = 152;
                        rb.res_tge_vcond_uso = 9999;
                        rb.res_sigl = "";
                        rb.res_area = 0;
                        rb.res_tge_tuni_medid = 156;
                        rb.res_tge_vuni_medid = 9999;
                        //rb.res_fec_inic;
                        //rb.res_fec_fin;
                        rb.res_nume = _SedeNumero;
                        //rb.res_caja_po = ""; ;
                        //rb.res_zona_caja_po = "";
                        rb.res_tus_cod_usr = "REGIN";
                        rb.res_nom_estab = "";
                        //rb.res_num_reg_prop;
                        //rb.res_quad_lote;
                        rb.res_ident_comp = _SedeComplemento;
                        rb.res_refer = ""; ;
                        rb.res_ttl_tip_logradoro = _SedeDsTipoLogradouro;
                        rb.res_direccion = _SedeLogradouro; ;
                        rb.res_urbanizacion = _SedeBairro;
                        rb.res_tes_cod_estado = _SedeUF;
                        rb.res_zona_postal = _SedeCEP;
                        rb.res_tmu_cod_mun = _SedeMunicipio == "" ? 0 : Int32.Parse(_SedeMunicipio);
                        rb.res_nire_sede = "";
                        rb.res_cnpj_sede = ""; ;

                        rb.Update();
                    }
                    #endregion

                    #region RUC_COMP

                    using (Ruc_Comp rc = new Ruc_Comp())
                    {
                        rc.MainConnectionProvider = cp;
                        //rc.rco_fec_const_nasc = null;
                        rc.rco_num_reg_merc = _nrMatricula;
                        //rc.rco_fec_reg_merc = null;
                        rc.rco_tge_ttip_doc = 151;
                        rc.rco_tge_vtip_doc = 9999;
                        //rc.rco_num_doc_ident
                        //rc.rco_fec_emi_doc_id
                        rc.rco_tnc_cod_natur = _NaturezaJuridicaCodigo;
                        rc.rco_domic_pais = 1;
                        rc.rco_fec_incorp = dHelperORACLE.SysdateOracle();
                        rc.rco_val_cap_soc = _CapitalSocial;
                        //rc.rco_fec_rg_cap_soc
                        //rc.rco_sexo 
                        rc.rco_nume = _SedeNumero;
                        //rc.rco_caja_po
                        //rc.rco_zona_caja_po
                        rc.rco_tge_tpais = 22;
                        rc.rco_tge_vpais = 105;
                        //rc.rco_ruc_ext_uf
                        rc.rco_tus_cod_usr = "REGIN";
                        //rc.rco_emis_doc_ident
                        //rc.rco_quad_lote
                        rc.rco_ident_comp = _SedeComplemento;
                        rc.rco_refer = "";
                        rc.rco_lic_mun = "";
                        rc.rco_ttl_tip_logradoro = _SedeDsTipoLogradouro;
                        rc.rco_direccion = _SedeLogradouro;
                        rc.rco_urbanizacion = _SedeBairro;
                        rc.rco_tes_cod_estado = _SedeUF;
                        rc.rco_zona_postal = _SedeCEP;
                        //rc.rco_tge_tcier_bal
                        //rc.rco_tge_vcier_bal
                        //rc.rco_tge_treg_trib
                        //rc.rco_tge_vreg_trib
                        rc.rco_tmu_cod_mun = _SedeMunicipio == "" ? 0 : Decimal.Parse(_SedeMunicipio);
                        rc.rco_rge_pra_protocolo = _ProtocoloRequerimento;
                        //rc.rco_num_reg_merc_sede
                        rc.Update();
                    }

                    #endregion

                    #region RUC_CBO_ECON
                    if (_tgeAcao == "4")
                    {
                        using (Ruc_Actv_Econ c = new Ruc_Actv_Econ())
                        {
                            c.MainConnectionProvider = cp;

                            foreach (bCNAE cc in _CNAEs)
                            {
                                c.rae_rge_pra_protocolo = _ProtocoloRequerimento;
                                c.rae_tae_cod_actvd = cc.CodigoCNAE;
                                c.rae_calif_actv = cc.TipoAtividade == 36 ? "1" : "2";
                                c.rae_porcent = 100;
                                c.rae_tus_cod_usr = "REGIN";
                                c.rae_fec_actl = dHelperORACLE.SysdateOracle();
                                c.Update();
                            }
                        }
                    }
                    else
                    {
                        using (Ruc_Cbo_Econ c = new Ruc_Cbo_Econ())
                        {
                            c.MainConnectionProvider = cp;

                            foreach (bCNAE cc in _CNAEs)
                            {
                                c.rae_rge_pra_protocolo = _ProtocoloRequerimento;
                                c.rae_tae_cod_actvd = cc.CodigoCNAE;
                                c.rae_calif_actv = cc.TipoAtividade == 36 ? "1" : "2";
                                c.rae_porcent = 100;
                                c.rae_tus_cod_usr = "REGIN";
                                c.rae_fec_actl = dHelperORACLE.SysdateOracle();
                                c.Update();
                            }
                        }
                    }
                    #endregion

                    #region RUC_GEN_PROTOCOLO
                    using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                    {
                        gc.MainConnectionProvider = cp;
                        gc.rgp_rge_pra_protocolo = _ProtocoloRequerimento;
                        gc.rgp_tge_tip_tab = 902;
                        gc.rgp_tge_cod_tip_tab = 1;
                        gc.rgp_valor = _ObjetoSocial;
                        gc.rgp_tus_cod_usr = "REGIN";
                        gc.rgp_fec_actl = dHelperORACLE.SysdateOracle();
                        gc.Update();

                        gc.rgp_rge_pra_protocolo = _ProtocoloRequerimento;
                        gc.rgp_tge_tip_tab = 902;
                        gc.rgp_tge_cod_tip_tab = 6;
                        gc.rgp_valor = _ProtocoloViabilidade;
                        gc.rgp_tus_cod_usr = "REGIN";
                        gc.rgp_fec_actl = dHelperORACLE.SysdateOracle();
                        gc.Update();


                    }

                    #endregion

                    #region PSC_PROT_EVENTO_RFB
                    StringBuilder sqlEvento = new StringBuilder();
                    for (int i = 0; i < pEventos.Count; i++)
                    {

                        sqlEvento = new StringBuilder();
                        sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                        sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                        sqlEvento.AppendLine(" Values ( ");
                        sqlEvento.AppendLine("'" + _ProtocoloRequerimento + "'");
                        sqlEvento.AppendLine(" ," + pEventos[i].ToString());
                        sqlEvento.AppendLine(" ) ");
                        using (dHelperORACLE c = new dHelperORACLE())
                        {
                            c.MainConnectionProvider = cp;
                            c.ExecuteNonQuery(sqlEvento);
                        }
                    }


                    #endregion

                    #region TAB_INFORM_EXTRA_JUNTA
                    _listSocioGravado = new List<string>();
                    using (Tab_Inform_Extra_juntacs ex = new Tab_Inform_Extra_juntacs())
                    {
                        foreach (bSocios s in _Socios)
                        {
                            _socioGravado = false;
                            foreach (string _busca in _listSocioGravado)
                            {
                                if (_busca == s.CPFCNPJ)
                                {
                                    _socioGravado = true;
                                    break;
                                }
                            }
                            if (!_socioGravado)
                            {
                                ex.MainConnectionProvider = cp;
                                ex.tie_protocolo = _ProtocoloRequerimento;
                                ex.tie_cpf_cnpj = s.CPFCNPJ;
                                ex.tie_tipo_relacao = 2;
                                ex.tie_ddd_fone1 = s.DDD;
                                ex.tie_fone1 = s.Telefone;
                                ex.tie_ddd_fone2 = "";
                                ex.tie_fone2 = "";
                                ex.tie_ddd_fax = "";
                                ex.tie_fax = "";
                                ex.tie_tipo_unidade = "0";
                                ex.tie_orgao_registro = "";
                                ex.tie_cnpj_registro = "";
                                ex.tie_forma_atuacao = "";
                                ex.tie_email = s.Email;
                                ex.tie_distrito = "";
                                ex.tie_in_centro_distribuicao = 0;
                                ex.tie_in_franqueado = 0;
                                ex.tie_cnpj_franqueador = "";
                                ex.tie_nr_ato_legal = "";
                                ex.tie_tipo_propriedade = 0;

                                ex.Update();

                                _listSocioGravado.Add(s.CPFCNPJ);
                            }
                        }
                    }

                    #endregion

                    #region Outros Estabelecimentos
                    int _ideEstab = 1;
                    if (_Filiais.Count > 0)
                    {
                        foreach (bFilial f in _Filiais)
                        {
                            using (Ruc_Outro_Estab rb = new Ruc_Outro_Estab())
                            {
                                rb.MainConnectionProvider = cp;
                                rb.res_rge_pra_protocolo = _ProtocoloRequerimento;
                                rb.res_ide_estab = _ideEstab;
                                rb.res_tip_estab = 1;
                                rb.res_tge_ttip_reg = 155;
                                rb.res_tge_vtip_reg = 9999;
                                rb.res_tge_tcond_uso = 152;
                                rb.res_tge_vcond_uso = 9999;
                                rb.res_sigl = "";
                                rb.res_area = f.AreaUtilizada;
                                rb.res_tge_tuni_medid = 156;
                                rb.res_tge_vuni_medid = 9999;
                                rb.res_nume = f.FilialNumero;
                                //rb.res_caja_po = ""; ;
                                //rb.res_zona_caja_po = "";
                                rb.res_tus_cod_usr = "REGIN";
                                rb.res_nom_estab = "";
                                rb.res_num_reg_prop = f.IPTU;
                                //rb.res_quad_lote;
                                rb.res_ident_comp = f.FilialComplemento;
                                rb.res_refer = ""; ;
                                rb.res_ttl_tip_logradoro = f.FilialTipoLogradouro;
                                rb.res_direccion = f.FilialLogradouro;
                                rb.res_urbanizacion = f.FilialBairro;
                                rb.res_tes_cod_estado = f.FilialUF;
                                rb.res_zona_postal = f.FilialCEP;
                                rb.res_tmu_cod_mun = f.FilialCodMunicipio;
                                rb.res_nire_sede = "";
                                rb.res_cnpj_sede = ""; ;
                                rb.Res_viabilidade = f.FilialViabilidade;
                                rb.Update();
                            }

                            using (Ruc_actv_outro_estab c = new Ruc_actv_outro_estab())
                            {
                                c.MainConnectionProvider = cp;

                                foreach (bCNAE cc in f.CNAEs)
                                {
                                    c.Rae_ide_estab = _ideEstab;
                                    c.rae_rge_pra_protocolo = _ProtocoloRequerimento;
                                    c.rae_tae_cod_actvd = cc.CodigoCNAE;
                                    c.rae_calif_actv = cc.TipoAtividade == 36 ? "1" : "2";
                                    c.Update();
                                }
                            }
                            _ideEstab++;
                        }
                    }
                    #endregion

                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SEFAZ PA
        public void InsertVeiculos()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    using (dVeiculos v = new dVeiculos())
                    {
                        v.MainConnectionProvider = cp;

                        v.T005_protocolo = _ProtocoloRequerimento;
                        v.Delete();

                        foreach (bTransporte t in _veiculos)
                        {
                            v.Uf = t.Uf;
                            v.Placa = t.Placa;
                            v.Municipio = t.Municipio;
                            v.Propietario = t.Propietario;
                            v.Acao = t.Acao;

                            v.Insert();
                        }
                    }
                    cp.CommitTransaction();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }

}



