using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
namespace RCPJ.BLL
{
    [Serializable]
    public class bProcessoCentral
    {

        #region Variables
        string _protocolo = "";

       
        string _cod_Usuario = "";
        int _seq_Andamento = 0;
        DateTime _dt_Entrada;
        DateTime _dt_Selecao;
        int _Situacao = 0;
        string _unidade = "";
        string _natureza = "";
        string _ato = "";
        string _nome = "";
        string _nome_unidade = "";
        string _desc_nat_juridica = "";
        string _desc_ato = "";
        string _secao = "";

       
        #endregion

        #region Class Property Declarations
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }
        public string Cod_Usuario
        {
            get { return _cod_Usuario; }
            set { _cod_Usuario = value; }
        }
        public int Seq_Andamento
        {
            get { return _seq_Andamento; }
            set { _seq_Andamento = value; }
        }

        public DateTime Dt_Entrada
        {
            get { return _dt_Entrada; }
            set { _dt_Entrada = value; }
        }

        public DateTime Dt_Selecao
        {
            get { return _dt_Selecao; }
            set { _dt_Selecao = value; }
        }

        public int Situacao
        {
            get { return _Situacao; }
            set { _Situacao = value; }
        }

        public string Unidade
        {
            get { return _unidade; }
            set { _unidade = value; }
        }

        public string Natureza
        {
            get { return _natureza; }
            set { _natureza = value; }
        }

        public string Ato
        {
            get { return _ato; }
            set { _ato = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Nome_unidade
        {
            get { return _nome_unidade; }
            set { _nome_unidade = value; }
        }

        public string Desc_nat_juridica
        {
            get { return _desc_nat_juridica; }
            set { _desc_nat_juridica = value; }
        }


        public string Desc_ato
        {
            get { return _desc_ato; }
            set { _desc_ato = value; }
        }
        public string Secao
        {
            get { return _secao; }
            set { _secao = value; }
        }
        #endregion
        
    }
}
