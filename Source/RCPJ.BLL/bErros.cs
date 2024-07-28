using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bErros
    {
        #region Declaração dos atributos
        private string _NomeCampo = string.Empty;
        private string _Campo = string.Empty;
        private string _DescricaoErro = string.Empty;
        private int _Index = -1;
        private string _Localizacao = string.Empty;
        #endregion
        #region Definição dos atributos
        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        public string NomeCampo
        {
            get { return _NomeCampo; }
            set { _NomeCampo = value; }
        }
        public string DescricaoErro
        {
            get { return _DescricaoErro; }
            set { _DescricaoErro = value; }
        }
        public string Localizacao
        {
            get { return _Localizacao; }
            set { _Localizacao = value; }
        }
        #endregion
    }
}

