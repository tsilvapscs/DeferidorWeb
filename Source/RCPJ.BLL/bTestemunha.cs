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
    public class bTestemunha : DBInteractionBase
    {
        #region Class Member Declarations

        private int _SQ_Pessoa = 0;

        private String _ProtocoloViabilidade = string.Empty;
        private String _ProtocoloRequerimento = string.Empty;

        private String _Nome_Testemunha = string.Empty;// ok
        private String _CPF_Testemunha = string.Empty;//ok
        private String _Nr_Documento_Testemunha = string.Empty;
        private String _Emissor_Doc_Testemunha = string.Empty;
        private String _UF_Emissor = String.Empty;

       
        private List<bTestemunha> _listTestemunha = new List<bTestemunha>();

        #endregion

        #region Class Property Declarations
        public List<bTestemunha> ListTestemunhas
        {
            get { return _listTestemunha; }
            set { _listTestemunha = value; }
        }
        public int SQ_Pessoa
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
        public String Emissor_Doc_Testemunha
        {
            get { return _Emissor_Doc_Testemunha; }
            set { _Emissor_Doc_Testemunha = value; }
        }
        public String UF_Emissor
        {
            get { return _UF_Emissor; }
            set { _UF_Emissor = value; }
        }

       

        #endregion

        #region Implements

        public List<bTestemunha> getTestemunhas()
        {

            dCnt_prot_testemunha testemunha = new dCnt_prot_testemunha();
            testemunha.reque_protocolo = _ProtocoloRequerimento;
            DataTable dt = testemunha.Query();
            _listTestemunha = new List<bTestemunha>();

            foreach(DataRow row in dt.Rows)
            {
                bTestemunha obj = new bTestemunha();

                obj.SQ_Pessoa = Convert.ToInt32(row["filho"].ToString());
                obj.CPF_Testemunha = row["cpf_testemunha"].ToString();
                obj.Nome_Testemunha = row["nome_testemunha"].ToString();
                obj.Nr_Documento_Testemunha = row["num_identidade"].ToString();
                obj.Emissor_Doc_Testemunha = row["orgao_exp"].ToString();
                obj.UF_Emissor = row["uf"].ToString();

                _listTestemunha.Add(obj);

            }
            return _listTestemunha;
        }

        public void ApagaTestemunha()
        {
            int Sq_testemunha = 0;
            DataTable dt = new DataTable();
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();

                dCnt_prot_testemunha testemunha = new dCnt_prot_testemunha();
                testemunha.cod_protocolo = _ProtocoloViabilidade; 
                testemunha.reque_protocolo = _ProtocoloRequerimento; 
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
                    }
                    cp.CommitTransaction();
                    getTestemunhas();
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
                    //p.t001_sq_pessoa = SqPessoa;
                    p.t001_in_tipo_pessoa = "F";
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
                    v.T001_cpf_cnpj_pessoa = _CPF_Testemunha;                    
                    v.r001_dt_entrada_vinculo = DateTime.Now;
                    v.r001_in_situacao = "A";
                    v.Update();
                }
                cp.CommitTransaction();
                getTestemunhas();
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

       #endregion

    }
}

