using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bAtoJunta
    {
        #region  Property Declarations
        private string _t101_co_ato;
        private string _t101_ds_ato;
        private string _t101_tip_ato;
        #endregion

        #region Class Member Declarations

        public string Codigo
        {
            get { return _t101_co_ato; }
            set { _t101_co_ato = value; }
        }
        public string Descricao
        {
            get { return _t101_ds_ato; }
            set { _t101_ds_ato = value; }
        }
        public string Tipo
        {
            get { return _t101_tip_ato; }
            set { _t101_tip_ato = value; }
        }
        #endregion

        public List<bAtoJunta> Query()
        {
            List<bAtoJunta> lista = new List<bAtoJunta>();

            using (dT101_Ato_Junta o = new dT101_Ato_Junta())
            {
                o.T101_co_ato = _t101_co_ato;
                DataTable dt = o.Query();
                foreach (DataRow row in dt.Rows)
                {
                    bAtoJunta ato = new bAtoJunta();
                    ato.Codigo = row["t101_co_ato"].ToString();
                    ato.Descricao = row["t101_ds_ato"].ToString();
                    ato.Tipo = row["t101_tip_ato"].ToString();

                    lista.Add(ato);
                }

            }

            return lista;

        }

    }
}