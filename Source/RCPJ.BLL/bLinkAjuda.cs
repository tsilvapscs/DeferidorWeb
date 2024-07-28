using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bLinkAjuda
    {
        #region Class Member Declarations
        private string _codNatureza;
        private string _codEvento;
        private string _descricao;


        private string _link;
        private List<bLinkAjuda> _list = new List<bLinkAjuda>();
        #endregion

        #region Class Property Declarations
        public string CodNatureza
        {
            get { return _codNatureza; }
            set { _codNatureza = value; }
        }
        public string CodEvento
        {
            get { return _codEvento; }
            set { _codEvento = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        #endregion

        public static DataTable getLista(string pRequerimento)
        {
            DataTable dt = dHelperQuery.getListaAjuda(pRequerimento);
            return dt;
        }

        //public static List<bLinkAjuda> getLista(string codNatureza)
        //{
        //    DataTable dt = dHelperQuery.getListaAjuda(codNatureza);

        //    List<bLinkAjuda> _list = new List<bLinkAjuda>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        bLinkAjuda obj = new bLinkAjuda();
        //        //obj._codNatureza = row["a099_cod_nat_juridica"].ToString();
        //        //obj._codEvento = row["a099_cod_evento"].ToString();
        //        obj._link = row["link"].ToString();
        //        obj._descricao = row["descricao"].ToString();
        //        _list.Add(obj);
        //    }
        //    return _list;
        //}
    }
}
