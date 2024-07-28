using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContrato : DBInteractionBase
    {
        #region Class Member Declarations

        private decimal _SQ_Pessoa = 0;
        
        private String _ProtocoloViabilidade = string.Empty;
        private String _ProtocoloRequerimento = string.Empty;
       
        private String _Nome_Testemunha = string.Empty;// ok
        private String _CPF_Testemunha = string.Empty;//ok
        private String _Nr_Documento_Testemunha = string.Empty;
        private String _Emissor_Doc_Testemunha = string.Empty;
        private String _UF_Emissor = String.Empty;
    
        private String _Nome_Advogado = string.Empty;
        private String _Inscricao_OAB_Advogado = string.Empty;
        private String _UF_OAB = string.Empty;
        private int _Num_Paginas = 0;
        private int _Num_Vias = 0;
        private string _Contrato_Padrao;
        private String CONST_PESSOA_FISICA = "F";
        private decimal _capital_integralizado;
        private decimal _capital_nao_integralizado;
        private Nullable<DateTime> _data_limite_integralizacao;
       

        #endregion

        #region Class Property Declarations

        public decimal SQ_Pessoa
        {
            get { return _SQ_Pessoa; }
            set { _SQ_Pessoa = value; }
        }

        public String ProtocoloViabilidade
        {
            get { return _ProtocoloViabilidade; }
            set { _ProtocoloViabilidade = value; }
        }
        public String ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }
       
        public String Nome_Testemunha
        {
            get { return _Nome_Testemunha; }
            set { _Nome_Testemunha = value; }
        }
        public String CPF_Testemunha
        {
            get { return _CPF_Testemunha; }
            set { _CPF_Testemunha = value; }
        }
        public String Nr_Documento_Testemunha
        {
            get { return _Nr_Documento_Testemunha; }
            set { _Nr_Documento_Testemunha = value; }
        }
        public String  Emissor_Doc_Testemunha
        {
            get { return _Emissor_Doc_Testemunha; }
            set { _Emissor_Doc_Testemunha = value; }
        }
        public String UF_Emissor
        {
            get { return _UF_Emissor; }
            set { _UF_Emissor = value; }
        }
        
        //public String CNPJ_Orgao_Registro
        //{
        //    get { return _CNPJ_Orgao_Registro; }
        //    set { _CNPJ_Orgao_Registro = value; }
        //}
        public String Nome_Advogado
        {
            get { return _Nome_Advogado; }
            set { _Nome_Advogado = value; }
        }

        public String UF_OAB
        {
            get { return _UF_OAB; }
            set { _UF_OAB = value; }
        }

        public String Inscricao_OAB_Advogado
        {
            get { return _Inscricao_OAB_Advogado; }
            set { _Inscricao_OAB_Advogado = value; }
        }

      

        public int Num_Paginas
        {
            get { return (int)_Num_Paginas; }
            set { _Num_Paginas = value; }
        }

        public int Num_Vias
        {
            get { return (int)_Num_Vias; }
            set { _Num_Vias = value; }
        }
        public string Contrato_Padrao
        {
            get { return _Contrato_Padrao; }
            set { _Contrato_Padrao = value; }
        }
        public decimal Capital_Integralizado
        {
            get { return _capital_integralizado; }
            set { _capital_integralizado = value; }
        }
        public decimal Capital_Nao_Integralizado
        {
            get { return _capital_nao_integralizado; }
            set { _capital_nao_integralizado = value; }
        }
        public Nullable<DateTime> Data_limite_Integralizacao
        {
            get { return _data_limite_integralizacao; }
            set { _data_limite_integralizacao = value; }
        }

        #endregion

        #region Implements


        public void ApagaTestemunha_old()
        {
            int Sq_testemunha = 0;
            DataTable dt = new DataTable();
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();

                dCnt_prot_testemunha testemunha = new dCnt_prot_testemunha();
                testemunha.cod_protocolo = _ProtocoloViabilidade; //Req.ProtocoloViabilidade;
                testemunha.reque_protocolo = _ProtocoloRequerimento; //Req.ProtocoloRequerimento;
                dt = testemunha.Query();

                if (dt.Rows.Count > 0)
                {
                    cp.BeginTransaction();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //cp.BeginTransaction();
                        Sq_testemunha = Convert.ToInt32(dt.Rows[i]["filho"].ToString());


                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {

                            v.MainConnectionProvider = cp;
                            v.Deleta(Sq_testemunha, 0, 0);

                        }
                        using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                        {

                            pf.MainConnectionProvider = cp;
                            pf.Deleta(Sq_testemunha);

                        }
                        using (dT001_Pessoa p = new dT001_Pessoa())
                        {

                            p.MainConnectionProvider = cp;
                            p.Deleta(Sq_testemunha);

                        }
                        //cp.CommitTransaction();
                    }
                    cp.CommitTransaction();
                }

            }

        }

        public void ApagaTestemunha()
        {
            DataTable dt = new DataTable();

            dCnt_prot_testemunha testemunha = new dCnt_prot_testemunha();
            testemunha.cod_protocolo = _ProtocoloViabilidade; 
            testemunha.reque_protocolo = _ProtocoloRequerimento; 
            dt = testemunha.Query();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    using (dR001_Vinculo v = new dR001_Vinculo())
                    {
                        v.ExcluiVinculo(dt.Rows[i]["filho"].ToString());
                    }

                }
            }
        }

        public void UpdateTestemunha()
        {
            int SqPessoa;
            
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();

                //6. Gravando Pessoa (Pessoa)
                //(Dados do Pessoa)
                using (dT001_Pessoa p = new dT001_Pessoa())
                {

                    p.MainConnectionProvider = cp;
                    p.t001_in_tipo_pessoa = CONST_PESSOA_FISICA;
                    p.t001_ds_pessoa = _Nome_Testemunha;
                    p.t001_in_dados_atualizados = "S";
                    p.t001_dt_ult_atualizacao = DateTime.Now;
                    p.t001_sq_pessoa = SQ_Pessoa;
                    SqPessoa = p.Update();

                }

                //7. Gravando Pessoa (Pessoa Fisica)
                //(Dados do Pessoa)

                using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                {
                    pf.MainConnectionProvider = cp;

                    pf.t001_sq_pessoa = SqPessoa;
                    pf.t002_nr_cpf = _CPF_Testemunha;
                    pf.t002_nr_documento = _Nr_Documento_Testemunha;
                    pf.t002_ds_emissor_documento = _Emissor_Doc_Testemunha;
                    pf.a004_uf_org_exped = _UF_Emissor;

                    pf.Update();
                }

                //Gravando Vinculo com a PJ (Vinculo Pessoa)
                using (dR001_Vinculo v = new dR001_Vinculo())
                {

                    v.MainConnectionProvider = cp;
                    v.t001_sq_pessoa_pai = Convert.ToDecimal(RecuperaPai(ProtocoloRequerimento.ToString()));
                    v.t001_sq_pessoa = SqPessoa;
                    v.a009_co_condicao = 503;
                    v.r001_dt_entrada_vinculo = DateTime.Now;
                    v.r001_in_situacao = "A";
                    v.T001_cpf_cnpj_pessoa = _CPF_Testemunha;
                    v.Update();
                }
                cp.CommitTransaction();
            }
        }
    

        public void UpdateAdvogado()
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();


                using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                {

                    pr.MainConnectionProvider = cp;
                    //pr.t006_ds_nome_advogado = _Nome_Advogado;
                    //pr.t006_nr_inscr_oab = _Inscricao_OAB_Advogado;
                    //pr.t006_ds_uf_oab_advogado = _UF_OAB;

                    pr.t006_nr_num_paginas = _Num_Paginas;
                    pr.t006_nr_num_vias = _Num_Vias;
                    pr.t006_contrato_padrao = _Contrato_Padrao;
                    pr.t005_nr_protocolo = _ProtocoloRequerimento;
                    pr.UpdateAdvogado();
                   
                }

              
                cp.CommitTransaction();
    }
}
        
        

        public string RecuperaPai(string wProtocolo)
        {
            string SqEmpresa;
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                //6. Gravando Pessoa (Pessoa)
                //(Dados do Pessoa)
                using (dT005_Protocolo p = new dT005_Protocolo())
                {

                    p.MainConnectionProvider = cp;
                    p.t005_nr_protocolo = wProtocolo;
                    SqEmpresa = p.RecuperaPai();
                    return SqEmpresa.ToString();
                }
            }
        }
        public void GravaCapitalIntegralizado()
        {
            //int SqEmpresa;
            //int SqPessoa;
            //int TipoPessoa = 0;
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();

                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cp;
                    pj.t003_vl_capital_integralizado = _capital_integralizado;
                    pj.t003_vl_capital_nao_integralizado = _capital_nao_integralizado;
                    pj.t003_data_limite_integralizacao = _data_limite_integralizacao;
                    pj.t001_sq_pessoa = _SQ_Pessoa;
                    pj.UpdateCapitalIntegralizado();
                }
                cp.CommitTransaction();
            }
        }

        #endregion

}
        //}
}

