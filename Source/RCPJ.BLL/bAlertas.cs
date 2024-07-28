using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bAlertas
    {
        #region Declaração dos atributos
        private string _NomeCampo = string.Empty;
        private string _Campo = string.Empty;

        #endregion

        #region Definição dos atributos
        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }
        public string NomeCampo
        {
            get { return _NomeCampo; }
            set { _NomeCampo = value; }
        }
        #endregion
    }
}

