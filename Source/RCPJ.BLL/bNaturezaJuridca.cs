using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bNaturezaJuridca : DBInteractionBase
    {
        #region  Property Declarations
        private string _codigo;
        private string _descricao;
        private string _grupoViabilidade;
        private int _QuantidadeMininoQSA = 2;
        private int _exigeAdminstrador = 1;

        #endregion

        #region Class Member Declarations
        public string Codigo
        {
            get { return _codigo; }

            set { _codigo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }

            set { _descricao = value; }
        }

        public int QuantidadeMininoQSA
        {
            get { return _QuantidadeMininoQSA; }

            set { _QuantidadeMininoQSA = value; }
        }

        public int ExigeAdminstrador
        {
            get { return _exigeAdminstrador; }

            set { _exigeAdminstrador = value; }
        }
        public string GrupoViabilidade
        {
            get { return _grupoViabilidade; }
            set { _grupoViabilidade = value; }
        }
        #endregion 

        #region Constructors
        public bNaturezaJuridca(string Codigo)
            : this()
        {
            Populate(Codigo);
        }
        public bNaturezaJuridca()
            
        {
           
        }

        private void Populate(string Codigo)
        {

            DataTable dt = dHelperQuery.getNaturezaJuridicaRequerimento(decimal.Parse(Codigo));
            foreach (DataRow row in dt.Rows)
            {
                _codigo = row["t009_co_natureza_juridica"].ToString();
                _descricao = row["t009_ds_natureza_juridica"].ToString();
                _grupoViabilidade = row["t009_co_grupo"].ToString();
                _QuantidadeMininoQSA = Int32.Parse(row["t009_qtd_min_qsa"].ToString());
                _exigeAdminstrador = Int32.Parse(row["t009_in_adminstrador"].ToString());
            }
        }


        public static bool getNaturezaJuridicaCartorio(string _codNatureza)
        {
            using (dHelperQuery dal = new dHelperQuery())
            {
                DataTable dt = dal.getNaturezaJuridicaCartorio(_codNatureza);
                return dt.Rows.Count == 1;
            }
        }

        #endregion
    }
}
