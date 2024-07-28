using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bTipoRepresentante : DBInteractionBase
    {
        #region Class Member Declarations
        private int _codigo = 0;
        private String _descricao = string.Empty;
        #endregion

        #region Class Property Declarations
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public String Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        #endregion

         #region Constructors
        public bTipoRepresentante()
        {
            //InitClass();
        }

        public bTipoRepresentante(int pCodigo)
            : this()
        {
            _codigo = pCodigo;
            Populate();
        }
        #endregion

        private void Populate()
        {

            if (_codigo == 0)
                return;

            DataTable dt = dHelperQuery.getTipoRepresentante(_codigo); ;

            foreach (DataRow dr in dt.Rows)
            {
                _codigo = Int32.Parse(dr["t015_co_tipo_assistido_representado"].ToString());
                _descricao = dr["t015_ds_tipo_assistido_representado"].ToString();
            }
           
        }

    }
}
