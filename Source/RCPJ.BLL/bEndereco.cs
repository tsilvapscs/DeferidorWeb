using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;

namespace RCPJ.BLL
{
    [Serializable]
    public class bEndereco
    {
        #region Class Member Declarations
        private String _cep = string.Empty;
        private String _uf = string.Empty;
        private string _DsTipoLogradouro = string.Empty;
        private String _CodMunicipio = string.Empty;
        private string _nomeMunicipio = string.Empty;
        private String _Bairro = string.Empty;
        private String _TipoLogradouro = string.Empty;
        private String _Logradouro = string.Empty;
        private String _Numero = string.Empty;
        private String _Complemento = string.Empty;
        #endregion

        #region Class Property Declarations
        public String CEP
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public String UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public String CodMunicipio
        {
            get { return _CodMunicipio; }
            set { _CodMunicipio = value; }
        }
        public String NomeMunicipio
        {
            get { return _nomeMunicipio; }
            set { _nomeMunicipio = value; }
        }
        public String Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public String TipoLogradouro
        {
            get { return _TipoLogradouro; }
            set { _TipoLogradouro = value; }
        }
        public String Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }
        public String Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public String Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        #endregion
    }
}
