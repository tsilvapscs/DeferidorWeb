using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;
namespace RCPJ.BLL
{
    [Serializable]
    public class bDivergenciaDBE
    {
        #region  Property Declarations
        protected string _t094_NR_CNPJ_ORG_REG;
        protected string _t094_T005_NR_PROTOCOLO;
        protected int _t094_cod_divergencia;
        protected string _t094_ds_divergencia;
        #endregion

        #region Class Member Declarations
        public string NumeroOrgaoRegistro
        {
            get { return _t094_NR_CNPJ_ORG_REG; }
            set { _t094_NR_CNPJ_ORG_REG = value; }
        }
        public string NumeroProtocolo
        {
            get { return _t094_T005_NR_PROTOCOLO; }

            set { _t094_T005_NR_PROTOCOLO = value; }
        }

        public int Item
        {
            get { return _t094_cod_divergencia; }

            set { _t094_cod_divergencia = value; }
        }
        public string Texto
        {
            get { return _t094_ds_divergencia; }

            set { _t094_ds_divergencia = value; }
        }

        #endregion

        public DataTable Query()
        {
            using (dt094_div_dbe o = new dt094_div_dbe())
            {
                o.NumeroOrgaoRegistro = _t094_NR_CNPJ_ORG_REG;
                o.NumeroProtocolo = _t094_T005_NR_PROTOCOLO;

                return o.Query();
            }
        }

        public void Update()
        {
            using (dt094_div_dbe o = new dt094_div_dbe())
            {
                o.NumeroOrgaoRegistro = _t094_NR_CNPJ_ORG_REG;
                o.NumeroProtocolo = _t094_T005_NR_PROTOCOLO;
                o.Item = _t094_cod_divergencia;
                o.Texto = _t094_ds_divergencia;

                o.Update();
            }
        }
    }
}
