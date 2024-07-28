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
    class bPessoa : DBInteractionBase
    {
        #region Class Member Declarations
        private int t001_sq_pessoa = 0;
        private string t001_in_tipo_pessoa;
        private string t001_ds_pessoa;
        private string t001_nome_fantasia;
        private string t001_in_dados_atualizados;
        private Nullable<DateTime> t001_dt_ult_atualizacao;
        private string t001_email;
        private string t001_ddd;
        private string t001_tel_2;
        private string t001_tel_1;
        private string t001_ds_pessoa_fonetica;
        #endregion

        #region Class Property Declarations
        public int T001_sq_pessoa
        {
            get { return t001_sq_pessoa; }
            set { t001_sq_pessoa = value; }
        }

        public string T001_in_tipo_pessoa
        {
            get { return t001_in_tipo_pessoa; }
            set { t001_in_tipo_pessoa = value; }
        }

        public string T001_ds_pessoa
        {
            get { return t001_ds_pessoa; }
            set { t001_ds_pessoa = value; }
        }

        public string T001_nome_fantasia
        {
            get { return t001_nome_fantasia; }
            set { t001_nome_fantasia = value; }
        }

        public string T001_in_dados_atualizados
        {
            get { return t001_in_dados_atualizados; }
            set { t001_in_dados_atualizados = value; }
        }

        public Nullable<DateTime> T001_dt_ult_atualizacao
        {
            get { return t001_dt_ult_atualizacao; }
            set { t001_dt_ult_atualizacao = value; }
        }

        public string T001_email
        {
            get { return t001_email; }
            set { t001_email = value; }
        }

        public string T001_ddd
        {
            get { return t001_ddd; }
            set { t001_ddd = value; }
        }

        public string T001_tel_1
        {
            get { return t001_tel_1; }
            set { t001_tel_1 = value; }
        }

        public string T001_tel_2
        {
            get { return t001_tel_2; }
            set { t001_tel_2 = value; }
        }

        public string T001_ds_pessoa_fonetica
        {
            get { return t001_ds_pessoa_fonetica; }
            set { t001_ds_pessoa_fonetica = value; }
        }
        #endregion

    }
}
