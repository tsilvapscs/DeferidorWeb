using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
namespace RCPJ.BLL
{
    [Serializable]
    class bVinculo : DBInteractionBase
    {
        #region  Property Declarations
        private int _t001_sq_pessoa;
        private int _t001_sq_pessoa_pai;
        private int _a030_co_tipo_assistido;
        private int _a009_co_condicao;
        private Nullable<DateTime> _r001_dt_entrada_vinculo;
        private Nullable<DateTime> _r001_dt_saida_vinculo;
        private string _r001_ds_cargo_direcao;
        private string _r001_in_situacao;
        private string _r001_in_gerente_uso_firma;
        private decimal _r001_vl_participacao;
        private Nullable<DateTime> _r001_dt_inicio_mandato;
        private Nullable<DateTime> r001_dt_termino_mandato;
        private int _t001_sq_pessoa_rep_legal;
        private string _r001_in_fundador;
        private int _r001_acao;
        private int _r001_ordem_filial_contrato;

        #endregion

        #region Class Member Declarations
        public int T001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }
            set { _t001_sq_pessoa = value; }
        }

        public int T001_sq_pessoa_pai
        {
            get { return _t001_sq_pessoa_pai; }
            set { _t001_sq_pessoa_pai = value; }
        }

        public int A030_co_tipo_assistido
        {
            get { return _a030_co_tipo_assistido; }
            set { _a030_co_tipo_assistido = value; }
        }
        public int A009_co_condicao
        {
            get { return _a009_co_condicao; }
            set { _a009_co_condicao = value; }
        }
        public Nullable<DateTime> R001_dt_entrada_vinculo
        {
            get { return _r001_dt_entrada_vinculo; }
            set { _r001_dt_entrada_vinculo = value; }
        }
        public Nullable<DateTime> R001_dt_saida_vinculo
        {
            get { return _r001_dt_saida_vinculo; }
            set { _r001_dt_saida_vinculo = value; }
        }
        public string R001_ds_cargo_direcao
        {
            get { return _r001_ds_cargo_direcao; }
            set { _r001_ds_cargo_direcao = value; }
        }
        public string R001_in_situacao
        {
            get { return _r001_in_situacao; }
            set { _r001_in_situacao = value; }
        }
        public string R001_in_gerente_uso_firma
        {
            get { return _r001_in_gerente_uso_firma; }
            set { _r001_in_gerente_uso_firma = value; }
        }
        public decimal R001_vl_participacao
        {
            get { return _r001_vl_participacao; }
            set { _r001_vl_participacao = value; }
        }
        public Nullable<DateTime> R001_dt_inicio_mandato
        {
            get { return _r001_dt_inicio_mandato; }
            set { _r001_dt_inicio_mandato = value; }
        }
        public Nullable<DateTime> R001_dt_termino_mandato
        {
            get { return r001_dt_termino_mandato; }
            set { r001_dt_termino_mandato = value; }
        }
        public int T001_sq_pessoa_rep_legal
        {
            get { return _t001_sq_pessoa_rep_legal; }
            set { _t001_sq_pessoa_rep_legal = value; }
        }
        public string R001_in_fundador
        {
            get { return _r001_in_fundador; }
            set { _r001_in_fundador = value; }
        }
        public int R001_acao
        {
            get { return _r001_acao; }
            set { _r001_acao = value; }
        }
        public int R001_ordem_filial_contrato
        {
            get { return _r001_ordem_filial_contrato; }
            set { _r001_ordem_filial_contrato = value; }
        }

        #region Data Access
        public void Update()
        {
            using (dR001_Vinculo v = new dR001_Vinculo())
            {
              //  v.MainConnectionProvider = cp;
                v.t001_sq_pessoa = _t001_sq_pessoa;
                v.t001_sq_pessoa_pai = _t001_sq_pessoa_pai;
                v.a009_co_condicao = _a009_co_condicao; 
                v.r001_dt_entrada_vinculo = DateTime.Now;
                v.r001_ds_cargo_direcao = _r001_ds_cargo_direcao;
                v.r001_in_situacao = _r001_in_situacao;
                v.r001_vl_participacao = _r001_vl_participacao;
                v.Update();
            }
        }

        #endregion

        #endregion
    }
}
