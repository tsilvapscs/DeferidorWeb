using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bTipoSecaoAndamento
    {
        #region Class Member Declarations
        private string _codigo;
        private int _tipo;
      
        #endregion

        #region Class Property Declarations
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        #endregion


        public static DataTable Query()
        {

            return dHelperQuery.ExecuteQuery("SELECT * FROM a027_tipo_secao");
                 
        }
    }
}
