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
    public class bAdvogado : DBInteractionBase
    {
        #region Class Member Declarations

        private String _ProtocoloRequerimento = string.Empty;

        private String _Nome = string.Empty;
        private String _inscricao_OAB = string.Empty;
        private String _uf_OAB = string.Empty;
        private String _cpf = string.Empty;

        #endregion

        #region Class Property Declarations

        public String ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }

        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public String UF_OAB
        {
            get { return _uf_OAB; }
            set { _uf_OAB = value; }
        }

        public String Inscricao_OAB
        {
            get { return _inscricao_OAB; }
            set { _inscricao_OAB = value; }
        }
        public String CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        #endregion

        #region Implements

        public void Update()
        {
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();


                using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                {

                    pr.MainConnectionProvider = cp;

                    pr.t006_ds_nome_advogado = _Nome;
                    pr.t006_nr_inscr_oab = _inscricao_OAB;
                    pr.t006_ds_uf_oab_advogado = _uf_OAB;
                    pr.t006_nr_cpf_advogado = _cpf;
                    pr.t005_nr_protocolo = _ProtocoloRequerimento;
                    pr.UpdateAdvogado_new();

                }


                cp.CommitTransaction();
            }
        }

        #endregion

    }
}

