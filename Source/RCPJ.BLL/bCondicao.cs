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
    public class bCondicao : DBInteractionBase
    {
        #region  Property Declarations 
        private decimal 			_a009_co_condicao;
        private string 			_a009_ds_condicao;
        private string 			_a009_in_sociedade;
        private string 			_a009_in_associacao;
        private string 			_a009_in_fundacao;
        private decimal 			_a009_tipo_pessoa;
        private decimal 			_a009_poder_adminstrar;
        private decimal 			_a009_obrig_repres;
        private decimal 			_a009_capital_social;
        private decimal 			_a009_permite_integralizar;
        private decimal 			_a009_residencia;
        private decimal 			_a009_doc_identif;
        private string 			_a009_in_representante;

        private List<bCondicao> _listCondicao;

       
        #endregion 

        #region Class Member Declarations
        public decimal a009_co_condicao
        {
            get { return _a009_co_condicao; }

            set { _a009_co_condicao = value; }
        }

        public string a009_ds_condicao
        {
            get { return _a009_ds_condicao; }

            set { _a009_ds_condicao = value; }
        }

        public string a009_in_sociedade
        {
            get { return _a009_in_sociedade; }

            set { _a009_in_sociedade = value; }
        }

        public string a009_in_associacao
        {
            get { return _a009_in_associacao; }

            set { _a009_in_associacao = value; }
        }

        public string a009_in_fundacao
        {
            get { return _a009_in_fundacao; }

            set { _a009_in_fundacao = value; }
        }

        public decimal a009_tipo_pessoa
        {
            get { return _a009_tipo_pessoa; }

            set { _a009_tipo_pessoa = value; }
        }

        public decimal a009_poder_adminstrar
        {
            get { return _a009_poder_adminstrar; }

            set { _a009_poder_adminstrar = value; }
        }

        public decimal a009_obrig_repres
        {
            get { return _a009_obrig_repres; }

            set { _a009_obrig_repres = value; }
        }

        public decimal a009_capital_social
        {
            get { return _a009_capital_social; }

            set { _a009_capital_social = value; }
        }

        public decimal a009_permite_integralizar
        {
            get { return _a009_permite_integralizar; }

            set { _a009_permite_integralizar = value; }
        }

        public decimal a009_residencia
        {
            get { return _a009_residencia; }

            set { _a009_residencia = value; }
        }

        public decimal a009_doc_identif
        {
            get { return _a009_doc_identif; }

            set { _a009_doc_identif = value; }
        }

        public string a009_in_representante
        {
            get { return _a009_in_representante; }

            set { _a009_in_representante = value; }
        }

        public List<bCondicao> ListCondicao
        {
            get { return _listCondicao; }
            set { _listCondicao = value; }
        }
        #endregion 

        #region Constructors
        public bCondicao(decimal cond)
            : this()
        {
            
        }
        public bCondicao()
            
        {
           
        }
        #endregion

        public DataTable QueryAll()
        {
            using (dCondicao c = new dCondicao())
            {
                return c.Query();
            }
        }

        public bCondicao condicaobyCod(decimal id)
        {
            foreach (bCondicao cond in _listCondicao)
            {
                if (id == cond.a009_co_condicao)
                {
                    return cond;
                }
            }
            return null;
        }
        
        public void CarregaCondicao()
        {

            _listCondicao = new List<bCondicao>();

            DataTable dt = this.QueryAll();
            foreach (DataRow row in dt.Rows)
            {
                bCondicao c = new bCondicao();
                c.a009_co_condicao = Decimal.Parse(row["a009_co_condicao"].ToString());
                c.a009_ds_condicao = row["a009_ds_condicao"].ToString();
                c.a009_in_sociedade = row["a009_in_sociedade"].ToString();
                c.a009_in_associacao = row["a009_in_associacao"].ToString();
                c.a009_in_fundacao = row["a009_in_fundacao"].ToString();
                c.a009_tipo_pessoa = Decimal.Parse(row["a009_tipo_pessoa"].ToString());
                c.a009_poder_adminstrar = Decimal.Parse(row["a009_poder_adminstrar"].ToString());
                c.a009_obrig_repres = Decimal.Parse(row["a009_obrig_repres"].ToString());
                c.a009_capital_social = Decimal.Parse(row["a009_capital_social"].ToString() == "" ? "0" : row["a009_capital_social"].ToString());
                c.a009_permite_integralizar = Decimal.Parse(row["a009_permite_integralizar"].ToString() == "" ? "0" : row["a009_permite_integralizar"].ToString());
                c.a009_residencia = Decimal.Parse(row["a009_residencia"].ToString() == "" ? "0" : row["a009_residencia"].ToString());
                c.a009_doc_identif = Decimal.Parse(row["a009_doc_identif"].ToString() == "" ? "0" : row["a009_doc_identif"].ToString());
                c.a009_in_representante = row["a009_in_representante"].ToString();

                _listCondicao.Add(c);
            }


            
        }
    }
}
