using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bSecaoParecer
    {
        #region Variables
        private string _secaoOrigem = "";
        
        private string _secaoDestino = "";
        private string _nomesecaoDestino = "";
        #endregion

        #region Class Member Declarations
        public string SecaoOrigem
        {
            get { return _secaoOrigem; }
            set { _secaoOrigem = value; }
        }
        public string SecaoDestino
        {
            get { return _secaoDestino; }
            set { _secaoDestino = value; }
        }

        public string NomeSecaoDestino
        {
            get { return _nomesecaoDestino; }
            set { _nomesecaoDestino = value; }
        }
        #endregion
    }
}
