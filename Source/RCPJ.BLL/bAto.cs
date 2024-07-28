using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bAto
    {
        // Variables ******************* 
        #region  Property Declarations
        protected decimal _a002_co_ato;
        protected string _a002_ds_ato;
        protected string _a002_in_viabilidade;
        protected string _a002_in_listar;
        protected string _ProtocoloViabilidade;
        #endregion

        #region Class Member Declarations
        public string ProtocoloViabilidade
        {
            get { return _ProtocoloViabilidade; }
            set { _ProtocoloViabilidade = value; }
        }
        public decimal Codigo
        {
            get { return _a002_co_ato; }

            set { _a002_co_ato = value; }
        }

        public string Descricao
        {
            get { return _a002_ds_ato; }

            set { _a002_ds_ato = value; }
        }

        public string a002_in_viabilidade
        {
            get { return _a002_in_viabilidade; }

            set { _a002_in_viabilidade = value; }
        }

        public string a002_in_listar
        {
            get { return _a002_in_listar; }

            set { _a002_in_listar = value; }
        }

        #endregion

        public DataTable getQueryAtoEventoRegistro(string Tipo)
        {
            using (dA002_Ato c = new dA002_Ato())
            {
                return (c.QueryAtoEventoRegistro());
            }
        }

        public string getAtoDescricao(string CodigoAto)
        {
            using (dA002_Ato c = new dA002_Ato())
            {
                c.a002_co_ato = decimal.Parse(CodigoAto);
                DataTable result = c.Query();
                if (result.Rows.Count > 0)
                {
                    return result.Rows[0][1].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
