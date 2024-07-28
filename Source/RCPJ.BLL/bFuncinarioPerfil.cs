using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bFuncinarioPerfil
    {
        #region Class Member Declarations

        private int _tipo = 0;
        private string _valor = string.Empty;

        #endregion

        #region Class Property Declarations

        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        
        #endregion

    }
}
