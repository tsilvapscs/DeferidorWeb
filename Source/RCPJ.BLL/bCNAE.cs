using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    public class bCNAE
    {
        #region Class Member Declarations

        private String _CodigoCNAE;
        private decimal _TipoAtividade;
        private string _FormAtiv;
        private string _Versao;
        private String _TipoAtividadeDescricao;
        private String _Descricao;
        private String _ProtocoloRequerimento;
        private string _Exercida;
        private int _sqPessoa;
       
        #endregion

        #region Class Property Declarations
        public string Versao
        {
            get { return _Versao; }
            set { _Versao = value; }
        }
        public string FormAtiv
        {
            get { return _FormAtiv; }
            set { _FormAtiv = value; }
        }
        public String CodigoCNAE
        {
            get
            {
                return (String)_CodigoCNAE;
            }
            set
            {
                _CodigoCNAE = value;
                //if (_CodigoCNAE != "" && _Descricao == null)
                //{
                //    using (dA001_Atividade a = new dA001_Atividade())
                //    {
                //        a.a001_co_atividade = _CodigoCNAE;
                //        _Descricao = a.getDescricaoByCodigo();
                        

                //    }
                //}

            }
        }

        public decimal TipoAtividade
        {
            get
            {
                return (decimal)_TipoAtividade;
            }
            set
            {
                _TipoAtividade = value;
                using (dA022_Tipo_Atividade ta = new dA022_Tipo_Atividade())
                {
                    ta.a022_co_tipo_atividade = _TipoAtividade;
                    _TipoAtividadeDescricao = ta.getDescricaoByCodigo();

                }
            }
        }
        public String TipoAtividadeDescricao
        {
            set {_TipoAtividadeDescricao = value;}
            get
            {
                return (String)_TipoAtividadeDescricao;
            }

        }
        public String Descricao
        {
            get
            {
                return _Descricao; 
            }
            set { _Descricao = value; }
        }
        public string Exercida
        {
            get { return _Exercida; }
            set { _Exercida = value; }
        }
        
        public string ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }
        public int SqPessoa
        {
            get { return _sqPessoa; }
            set { _sqPessoa = value; }
        }
       
        #endregion

        public DataTable Query()
        {
            using (dA001_Atividade a = new dA001_Atividade())
            {
                a.a001_co_atividade = _CodigoCNAE;
                return a.Query();
            }
        }

        public void Insert()
        {
            using (dR004_Atuacao a = new dR004_Atuacao())
            {
                a.t001_sq_pessoa = _sqPessoa;
                a.a001_co_atividade = _CodigoCNAE;
                a.r004_in_principal_secundario = _TipoAtividade.ToString();
                a.Insert();

            }
        }
        public void Update111()
        {
            using (dR004_Atuacao a = new dR004_Atuacao())
            {
                a.t001_sq_pessoa = _sqPessoa;
                a.a001_co_atividade = _CodigoCNAE;
                a.r004_in_principal_secundario = _FormAtiv;
                a.Insert();

            }
        }

        public DataTable QueryByDescricao()
        {
            using (dA001_Atividade a = new dA001_Atividade())
            {
                a.a001_ds_atividade = _Descricao;
                return a.QueryByDescricao();
            }
        }

        public static string GetDescricao(string Codigo)
        {
            using (dA001_Atividade a = new dA001_Atividade())
            {
                a.a001_co_atividade = Codigo;
                return a.getDescricaoByCodigo();
            }
        }
        public static string GetDescricaocbo(string Codigo)
        {
            using (dA001_Atividade a = new dA001_Atividade())
            {
                a.a001_co_atividade = Codigo;
                return a.getDescricaoByCodigo();
            }
        }
        public void Delete()
        {
            using (dCNAE a = new dCNAE())
            {
                a.SqPessoa = _sqPessoa;
                a.CodigoCNAE = _CodigoCNAE;
                a.Delete();

            }
        }

    }
}
