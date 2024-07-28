using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dContador : DBInteractionBase
    {
        #region Class Member Declarations
        private int _SQPessoa;
        private string _requerimento;
        private string _cnpjOrgaoRegistro;


        private String _crc = string.Empty;
        private String _ufCrc = string.Empty;
        //private String _TipoPessoa;

        private String _Email;
        private String _Nome;
        private String _CPFCNPJ;


        private String _EndCEP;
        private String _EndPais;
        private String _EndUF;
        private String _EndMunicipio;
        private String _EndCodMunicipio;


        private String _EndBairro;
        private String _EndTipoLogradouro;
        private string _EndDsTipoLogradouro;
        private String _EndLogradouro;
        private String _EndNumero;
        private String _EndComplemento;
        private String _DDD;
        private String _Telefone;


        #endregion

        #region Class Property Declarations

        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }

        public int SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }

        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public String CRC
        {
            get { return _crc; }
            set { _crc = value; }
        }
        public String UfCrc
        {
            get { return _ufCrc; }
            set { _ufCrc = value; }
        }
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
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
            set { _Telefone = value; }
        }

        public String CPFCNPJ
        {
            get { return _CPFCNPJ.Trim(); }
            set { _CPFCNPJ = value; }
        }

        public String EndCEP
        {
            get { return _EndCEP; }
            set { _EndCEP = value; }
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

        public String EndCodMunicipio
        {
            get { return _EndCodMunicipio; }
            set { _EndCodMunicipio = value; }
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

        #endregion

        #region Implements
        
        #endregion
    }
}
