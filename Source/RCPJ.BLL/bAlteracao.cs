using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using System.Text.RegularExpressions;


namespace RCPJ.BLL
{
    [Serializable]
    public class bAlteracao111 :DBInteractionBase
    {
        private string _NIRE = string.Empty;
        private string _CNPJ = "68196305000154"; //string.Empty;
        private string _Nome = "Camila Pitanga"; //string.Empty;
        private string _NaturezaJuridica = "2038";
        private string _CEP = "22640100";//string.Empty;
        private string _UF = "RJ"; //string.Empty;
        private string _Municipio = "60011";//string.Empty;
        private string _Bairro = "BARRA DA TIJUCA"; // string.Empty;
        private string _TipoLogradouro = "AVENIDA";//string.Empty;
        private string _Logradouro = "DAS AMERICAS (ATE 1600 LADO PAR)"; // string.Empty;
        private string _Numero = "2264"; //string.Empty;
        private string _Complemento = "Casa 2"; //string.Empty;
        private decimal _CapitalSocial = 100000; //0;
        private string _Porte = "EPP"; //string.Empty;
        private Nullable<DateTime> _DataInicioAtividade = System.DateTime.Now;// null;
        private string _ObjetoSocial = "COMÉRCIO DE ALIMENTOS.";
      //CNAE
        private List<bCNAE> _CNAEs = new List<bCNAE>();
      //QSA
        public class newSocios : bSocios
        {
            new private string _protocoloNovo = string.Empty;
            new private Nullable<DateTime> _DataEntrada = null;
            new private Nullable<DateTime> _DataSaida = null;
            new private string _Situacao = string.Empty;
            public string Situacao
            {
                get { return _Situacao; }
                set { _Situacao = value; }
            }
            public Nullable<DateTime> DataSaida
            {
                get { return _DataSaida; }
                set { _DataSaida = value; }
            }
            public Nullable<DateTime> DataEntrada
            {
                get { return _DataEntrada; }
                set { _DataEntrada = value; }
            }
            public string ProtocoloNovo
            {
                get { return _protocoloNovo; }
                set { _protocoloNovo = value; }
            }
        }
        private List<newSocios> _Socios = new List<newSocios>();
        //private List<bSocios> _Socios = new List<bSocios>();
        
        private int _QtdFiliaisNaUF = 5;

        
        public string NIRE
        {
            get { return _NIRE; }
            set { _NIRE = value; }
        }
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string NaturezaJuridica
        {
            get { return _NaturezaJuridica; }
            set { _NaturezaJuridica = value; }
        }
        public string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public string TipoLogradouro
        {
            get { return _TipoLogradouro; }
            set { _TipoLogradouro = value; }
        }
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        public decimal CapitalSocial
        {
            get { return _CapitalSocial; }
            set { _CapitalSocial = value; }
        }
        public string Porte
        {
            get { return _Porte; }
            set { _Porte = value; }
        }
        public Nullable<DateTime> DataInicioAtividade
        {
            get { return _DataInicioAtividade; }
            set { _DataInicioAtividade = value; }
        }
        public string ObjetoSocial
        {
            get { return _ObjetoSocial; }
            set { _ObjetoSocial = value; }
        }
        public List<bCNAE> CNAEs
        {
            get { return (List<bCNAE>)_CNAEs; }
            set { _CNAEs = value; }
        }
        //public List<bSocios> Socios
        //{
        //    get { return (List<bSocios>)_Socios; }
        //    set { _Socios = value; }
        //}
        public List<newSocios> Socios
        {
            get { return (List<newSocios>)_Socios; }
            set { _Socios = value; }
        }
        public int QtdFilialNaUF
        {
            get { return _QtdFiliaisNaUF; }
            set { _QtdFiliaisNaUF =value; }
        }
    }
}
