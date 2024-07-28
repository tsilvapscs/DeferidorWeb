using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using System.Text.RegularExpressions;

namespace RCPJ.BLL
{
    [Serializable]
    public class bDocumento : DBInteractionBase
    {
        #region Propriedades da classe

        protected int _t010_id = 0;
        protected string _t005_nr_protocolo = string.Empty;
        protected int _t010_tipo_documento = 0;
        protected byte[] _t010_documento;
        protected string _t010_descricao_doc = string.Empty;
        protected string _t010_tipo_arquivo = string.Empty;
        protected int _num_paginas;
        protected int _num_vias;
        protected string _t004_nr_cnpj_org_reg = string.Empty;
        protected string _t010_nome_arquivo = string.Empty;
        protected string _t010_capital_nao_integralizado = string.Empty;

        #endregion

        #region Membros da classe

        public int t010_id
        {
            get { return _t010_id; }
            set { _t010_id = value; }
        }

        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }
            set { _t005_nr_protocolo = value; }
        }
        public int t010_tipo_documento
        {
            get { return _t010_tipo_documento; }
            set { _t010_tipo_documento = value; }
        }
        public byte[] t010_documento
        {
            get { return _t010_documento; }
            set { _t010_documento = value; }
        }
        public string t010_descricao_doc
        {
            get { return _t010_descricao_doc; }
            set { _t010_descricao_doc = value; }
        }
        public string t010_tipo_arquivo
        {
            get { return _t010_tipo_arquivo; }
            set { _t010_tipo_arquivo = value; }
        }
        public int num_paginas
        {
            get { return _num_paginas; }
            set { _num_paginas = value; }
        }
        public int num_vias
        {
            get { return _num_vias; }
            set { _num_vias = value; }
        }
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }
            set { _t004_nr_cnpj_org_reg = value; }
        }
        public string t010_nome_arquivo
        {
            get { return _t010_nome_arquivo; }
            set { _t010_nome_arquivo = value; }
        }
        public string t010_capital_nao_integralizado
        {
            get { return _t010_capital_nao_integralizado; }
            set { _t010_capital_nao_integralizado = value; }
        }

        #endregion

        #region Constructors
        public bDocumento()
        {
            //InitClass();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numero"></param>
        public bDocumento(int id)
            : this()
        {
            _t010_id = id;
            Populate();
        }
        #endregion

        #region Implementação

        private void Populate()
        {
            DataTable dt = QueryAById();
            if (dt.Rows.Count == 1)
            {
                _t010_id = Int32.Parse(dt.Rows[0]["t010_1d"].ToString());
                _t005_nr_protocolo = dt.Rows[0]["t005_nr_protocolo"].ToString();
                _t010_tipo_documento = Int32.Parse(dt.Rows[0]["t010_tipo_documento"].ToString());
                _t010_documento = (byte[])dt.Rows[0]["t010_documento"];
                _t010_descricao_doc = dt.Rows[0]["t010_descricao_doc"].ToString();
                _t010_tipo_arquivo = dt.Rows[0]["t010_tipo_arquivo"].ToString();
                _t004_nr_cnpj_org_reg = dt.Rows[0]["t004_nr_cnpj_org_reg"].ToString();
                _t010_nome_arquivo = dt.Rows[0]["t010_nome_arquivo"].ToString();

            }
        }

        public void Update()
        {
            using (dT010_Contratos_docs c = new dT010_Contratos_docs())
            {
                c.t005_nr_protocolo = _t005_nr_protocolo;
                c.t010_tipo_documento = _t010_tipo_documento;
                c.t010_documento = _t010_documento;
                c.t010_descricao_doc = _t010_descricao_doc;
                c.t010_tipo_arquivo = _t010_tipo_arquivo;
                c.t004_nr_cnpj_org_reg = _t004_nr_cnpj_org_reg;
                c.t010_nome_arquivo = _t010_nome_arquivo;
                c.t010_capital_nao_integralizado = _t010_capital_nao_integralizado;

                c.Update();
            }

        }

        public void updatePag_vias()
        {
            using (dT010_Contratos_docs c = new dT010_Contratos_docs())
            {
                c.t005_nr_protocolo = _t005_nr_protocolo;
                c.updatePag_vias();
            }
        }

        public DataTable QueryAById()
        {

            using (dT010_Contratos_docs c = new dT010_Contratos_docs())
            {
                c.t010_id = _t010_id;

                return c.Query();
            }
        }

        public DataTable Query()
        {

            using (dT010_Contratos_docs c = new dT010_Contratos_docs())
            {
                c.t005_nr_protocolo = _t005_nr_protocolo;
                c.t010_nome_arquivo = _t010_nome_arquivo;

                return c.Query();
            }
        }

        public void Delete()
        {

            using (dT010_Contratos_docs c = new dT010_Contratos_docs())
            {
                c.t010_id = _t010_id;

              //  c.DeleteById();
            }

        }

        #endregion
    }
}
