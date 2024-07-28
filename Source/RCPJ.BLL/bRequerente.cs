using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using RCPJ.DAL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bRequerente
    {
        #region  Property Declarations
        private string _nome = "";
        private string _cpf = "";
        private string _ddd = "";
        private string _telefone = "";
        private string _email= "";
        private int _sqPessoa = 0;
        private int _sqEmpresa = 0;
        
        #endregion

        #region Class Member Declarations

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        public string DDD
        {
            get { return _ddd; }
            set { _ddd = value; }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public int SqPessoa
        {
            get { return _sqPessoa; }
            set { _sqPessoa = value; }
        }
        public int SqEmpresa
        {
            get { return _sqEmpresa; }
            set { _sqEmpresa = value; }
        }
        #endregion 

        public void Update()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dT001_Pessoa p = new dT001_Pessoa())
                    {

                        #region Gravando Pessoa
                        p.MainConnectionProvider = cp;
                        p.t001_in_tipo_pessoa = "F";
                        p.t001_ds_pessoa = _nome;
                        p.t001_in_dados_atualizados = "S";
                        p.t001_dt_ult_atualizacao = DateTime.Now;
                        if (_ddd != string.Empty)
                            p.t001_ddd = _ddd;
                        if (_telefone != string.Empty)
                            p.t001_tel_1 = _telefone;
                        p.t001_email = _email == null ? "" : _email;

                        if (_sqPessoa != 0)
                            p.t001_sq_pessoa = Convert.ToDecimal(_sqPessoa);
                        else
                            p.t001_sq_pessoa = 0;

                        _sqPessoa = p.Update();
                        
                        #endregion


                        #region Gravando Pessoa Fisica
                        using (dT002_Pessoa_Fisica pf = new dT002_Pessoa_Fisica())
                        {
                            pf.MainConnectionProvider = cp;

                            pf.t001_sq_pessoa = _sqPessoa;
                            pf.t002_nr_cpf = _cpf;
                            pf.Update();
                        }
                        #endregion

                        #region Gravando Vinculo
                        using (dR001_Vinculo v = new dR001_Vinculo())
                        {
                            v.MainConnectionProvider = cp;
                            v.t001_sq_pessoa = _sqPessoa;
                            v.t001_sq_pessoa_pai = _sqEmpresa;
                            v.a009_co_condicao = 500; 
                            v.r001_dt_entrada_vinculo = DateTime.Now;
                            v.r001_ds_cargo_direcao = "REQUERENTE";
                            v.r001_in_situacao = "A";
                            v.r001_vl_participacao = 0;
                            v.T001_cpf_cnpj_pessoa = _cpf;
                            v.Update();
                        }
                        #endregion

                    }

                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

    }
}
