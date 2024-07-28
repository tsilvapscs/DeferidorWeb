using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    class bA001_Atividade
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _a001_co_atividade;
        protected string _a001_ds_atividade;
        protected string _a001_in_tipo;
        #endregion

        #region Class Member Declarations
        public string a001_co_atividade
        {
            get { return _a001_co_atividade; }

            set { _a001_co_atividade = value; }
        }

        public string a001_ds_atividade
        {
            get { return _a001_ds_atividade; }

            set { _a001_ds_atividade = value; }
        }

        public string a001_in_tipo
        {
            get { return _a001_in_tipo; }

            set { _a001_in_tipo = value; }
        }

        #endregion

        #region Implements
        public DataTable Query()
        {
            using (dA001_Atividade a = new dA001_Atividade())
            {
                a.a001_co_atividade = _a001_co_atividade;
                a.a001_ds_atividade = _a001_ds_atividade;
                a.a001_in_tipo = _a001_in_tipo;
                return (a.Query());
            }
        }

        #endregion
    }

}
