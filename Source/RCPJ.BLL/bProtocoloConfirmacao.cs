using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bProtocoloConfirmacao : System.IDisposable
    {

        #region  Property Declarations
        private string _t005_nr_protocolo;
        private decimal _t017_sequencia;
        private decimal _t017_grupo;
        private decimal _t017_item;
        private decimal _t017_status;
        private decimal _t017_confirma;
        private string _t017_usuario;
        private DateTime _t017_dt_confirmacao;
        private string _t017_motivo;
        private string _t017_andamento_secao;
        private int _t017_andamento_seq;
        #endregion

        #region Class Member Declarations

        public string T005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }
            set { _t005_nr_protocolo = value; }
        }

        public decimal T017_sequencia
        {
            get { return _t017_sequencia; }
            set { _t017_sequencia = value; }
        }

        public decimal T017_grupo
        {
            get { return _t017_grupo; }
            set { _t017_grupo = value; }
        }

        public decimal T017_item
        {
            get { return _t017_item; }
            set { _t017_item = value; }
        }

        public decimal T017_status
        {
            get { return _t017_status; }
            set { _t017_status = value; }
        }

        public decimal T017_confirma
        {
            get { return _t017_confirma; }
            set { _t017_confirma = value; }
        }

        public string T017_usuario
        {
            get { return _t017_usuario; }
            set { _t017_usuario = value; }
        }

        public DateTime T017_dt_confirmacao
        {
            get { return _t017_dt_confirmacao; }
            set { _t017_dt_confirmacao = value; }
        }

        public string t017_motivo
        {
            get { return _t017_motivo; }
            set { _t017_motivo = value; }
        }
        public string T017_andamento_secao
        {
            get { return _t017_andamento_secao; }
            set { _t017_andamento_secao = value; }
        }
        public int T017_andamento_seq
        {
            get { return _t017_andamento_seq; }
            set { _t017_andamento_seq = value; }
        }
        #endregion


        public static bool predicateFindSocios(bProtocoloConfirmacao pc)
        {

            if (pc.T017_grupo == 1)
            {
                return true;
            }
            {
                return false;
            }

        }

        public static bool predicateFindFiliais(bProtocoloConfirmacao pc)
        {

            if (pc.T017_grupo == 4)
            {
                return true;
            }
            {
                return false;
            }

        }

        

        public void Update()
        {
            //

            using (dT017_Protocolo_Confirmacao pc = new dT017_Protocolo_Confirmacao())
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    pc.MainConnectionProvider = cp;
                    pc.t005_nr_protocolo = _t005_nr_protocolo;
                    pc.t017_sequencia = _t017_sequencia;
                    pc.t017_grupo = _t017_grupo;
                    pc.t017_item = _t017_item;
                    pc.t017_status = _t017_status;
                    pc.t017_confirma = _t017_confirma;
                    pc.t017_usuario = _t017_usuario;
                    pc.t017_dt_confirmacao = _t017_dt_confirmacao;
                    pc.t017_motivo = _t017_motivo;
                    pc.T017_andamento_secao = _t017_andamento_secao;
                    pc.T017_andamento_seq = _t017_andamento_seq;
                    pc.Update();
                    cp.CommitTransaction();
                }
                
            }
        }

        //public void apagaConfirmacao(string wSequencia, string wItem, string wProtocolo)
        //{
        //    using (dT017_Protocolo_Confirmacao pc = new dT017_Protocolo_Confirmacao())
        //    {
        //        using (ConnectionProvider cp = new ConnectionProvider())
        //        {
        //            cp.OpenConnection();
        //            cp.BeginTransaction();
        //            pc.MainConnectionProvider = cp;
        //            pc.ApagaConfirmacao(wSequencia, wItem, wProtocolo);
        //            cp.CommitTransaction();
        //        }
        //    }
        //}

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
        
    }
}
