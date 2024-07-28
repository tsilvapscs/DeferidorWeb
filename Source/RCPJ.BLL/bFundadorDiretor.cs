using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;

namespace RCPJ.BLL
{

    [Serializable]
    public class bFundadorDiretor_old : DBInteractionBase
    {
        private String _SQPessoa;
        private String _ObrigacoesSociais;
        private String _TipoPessoa;
        private String _Qualificacao;
        private String _Qualificacao_Descricao;
        private string _Fundador;

        private String _Nome;
        protected string _nome_pai;
        protected string _nome_mae;
        private String _CPFCNPJ;
        private String _TipoIdentidade;
        private String _OrgaoExpedidor;
        private String _OrgaoExpedidorUF;
        private String _RG;
        private String _Nacionalidade;
        private String _Profissao;
        private String _Profissao_Descricao;
        //private decimal _QuotaCapitalSocial = int.MinValue;
        private String _EndPais;
        private String _EndCEP;
        private String _EndUF;
        private String _EndMunicipio;
        private String _EndBairro;
        private String _EndTipoLogradouro;
        private String _EndDsTipoLogradouro;
        private String _EndLogradouro;
        private String _EndNumero;
        private String _EndComplemento;
        private String _Sexo;
        private String _EstadoCivil;
        private String _EstadoCivilRegime;
        private String _TipoAssistido;
        private string _TipoEmancipado;
        private String _NaturalidadeCodigo;
        private String _in_Sexo;
        private DateTime _DataNascimento;
        private int _NacionalidadeCodigo;
        private bool _Valido;
        private Nullable <DateTime> _DataInicioMandato;
        private Nullable <DateTime> _DataTerminoMandato;
        private int _rep_legal;
        private string _tipo_visto;
        private Nullable<DateTime> _dt_emissao_visto;
        private Nullable<DateTime> _dt_validade_visto;
        private string _representante;
        private List<bFundadorDiretor_old> _Representantes = new List<bFundadorDiretor_old>();
        //CASO EIRELI E EMPRESARIO
        private decimal _CapitalIntegralizado = 0;

        public decimal CapitalIntegralizado
        {
            get { return _CapitalIntegralizado; }
            set { _CapitalIntegralizado = value; }
        }
        private decimal _Capital_a_Integralizar = 0;

        public decimal Capital_a_Integralizar
        {
            get { return _Capital_a_Integralizar; }
            set { _Capital_a_Integralizar = value; }
        }

        //FIM CASO EIRELI E EMPRESARIO

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
        public string EndDsTipoLogradouro
        {
            get { return _EndDsTipoLogradouro; }
            set { _EndDsTipoLogradouro = value; }
        }
        public int rep_legal
        {
            get { return _rep_legal; }
            set { _rep_legal = value; }
        }
        public string representante
        {
            get { return _representante; }
            set { _representante = value; }
        }

        public List<bFundadorDiretor_old> Representantes
        {
            get { return (List<bFundadorDiretor_old>)_Representantes; }
            set { _Representantes = value; }
        }

        public string in_sexo
        {
            get { return _in_Sexo; }
            set { _in_Sexo = value; }
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

        public DateTime DataNascimento
        {
            get { return (DateTime)_DataNascimento; }
            set { _DataNascimento = value; }
        }

        public String SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }

        public String TipoPessoa
        {
            get { return _TipoPessoa; }
            set { _TipoPessoa = value; }
        }

        public String TipoEmancipado
        {
            get { return _TipoEmancipado; }
            set { _TipoEmancipado = value; }
        }

        public String CPFCNPJ
        {
            get { return _CPFCNPJ; }
            set { _CPFCNPJ = value; }
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

        public String Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
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


        //public decimal QuotaCapitalSocial
        //{
        //    get { return _QuotaCapitalSocial; }
        //    set { _QuotaCapitalSocial = value; }
        //}

        public string EndPais
        {
            get { return _EndPais; }
            set { _EndPais = value; }
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
        public string Fundador
        {
            get { return _Fundador; }
            set { _Fundador = value; }
        }
        public Nullable <DateTime> DataInicioMandato
        {
            get { return _DataInicioMandato; }
            set { _DataInicioMandato = value; }
        }
        public Nullable <DateTime> DataTerminoMandato
        {
            get { return _DataTerminoMandato; }
            set { _DataTerminoMandato = value; }
        }
    }

}
