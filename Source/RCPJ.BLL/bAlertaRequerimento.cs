using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using RCPJ.DAL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;

namespace RCPJ.BLL
{
     [Serializable]
    public class bAlertaRequerimento : DBInteractionBase
    {
        #region  Property Declarations
        private string _t018_T004_NR_CNPJ_ORG_REG;
        private string _t018_T005_NR_PROTOCOLO;
        private int _t018_t097_id_alerta;
        private string _t018_valor;
        private int _t018_status;
        private string _t018_usr_desativo = string.Empty;
        private DateTime _t018_dt_desativo;
        private string _descricao;
        private string _grupo;

        

        #endregion

        #region Class Member Declarations


        public string Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public int ID
        {
            get { return _t018_t097_id_alerta; }
            set { _t018_t097_id_alerta = value; }
        }

        public string Valor
        {
            get { return _t018_valor; }
            set { _t018_valor = value; }
        }

        public string T018_T004_NR_CNPJ_ORG_REG
        {
            get { return _t018_T004_NR_CNPJ_ORG_REG; }
            set { _t018_T004_NR_CNPJ_ORG_REG = value; }
        }
        public string T018_T005_NR_PROTOCOLO
        {
            get { return _t018_T005_NR_PROTOCOLO; }
            set { _t018_T005_NR_PROTOCOLO = value; }
        }
        public int T018_t097_id_alerta
        {
            get { return _t018_t097_id_alerta; }
            set { _t018_t097_id_alerta = value; }
        }
        public string T018_valor
        {
            get { return _t018_valor; }
            set { _t018_valor = value; }
        }
        public int T018_status
        {
            get { return _t018_status; }
            set { _t018_status = value; }
        }
        public string T018_usr_desativo
        {
            get { return _t018_usr_desativo; }
            set { _t018_usr_desativo = value; }
        }
        public DateTime T018_dt_desativo
        {
            get { return _t018_dt_desativo; }
            set { _t018_dt_desativo = value; }
        }
        #endregion

        #region Constructors
        public bAlertaRequerimento()
        {
            //InitClass();
        }

        public bAlertaRequerimento(string pProtocolo )
            : this()
        {
            _t018_T005_NR_PROTOCOLO = pProtocolo;
            Populate();
        }
        #endregion

        private void Populate()
        {

            if (_t018_T005_NR_PROTOCOLO == string.Empty)
                return;

            dt018_alerta_requerimento alerta = new dt018_alerta_requerimento();
            alerta.T018_T005_NR_PROTOCOLO = _t018_T005_NR_PROTOCOLO;

            DataTable dt = alerta.Query();

            foreach (DataRow dr in dt.Rows)
            {
                bAlertaRequerimento c = new bAlertaRequerimento();
                c.T018_T004_NR_CNPJ_ORG_REG      = dr["t018_T004_NR_CNPJ_ORG_REG"].ToString();
                c.T018_T005_NR_PROTOCOLO = dr["t018_T005_NR_PROTOCOLO"].ToString();
                c.T018_t097_id_alerta = Int32.Parse(dr["t018_t097_id_alerta"].ToString());
                c.T018_valor = dr["t018_valor"].ToString();
                c.T018_status = Int32.Parse(dr["t018_status"].ToString());
                c.T018_usr_desativo = dr["t018_usr_desativo"].ToString();
                c.T018_dt_desativo = DateTime.Parse(dr["t018_dt_desativo"].ToString());
            }
           
        }

        public static List<bAlertaRequerimento> GetAlertas(string numprotocolo, string orgaoregistro)
        {

           
            dt018_alerta_requerimento alerta = new dt018_alerta_requerimento();
            alerta.T018_T004_NR_CNPJ_ORG_REG = orgaoregistro;
            alerta.T018_T005_NR_PROTOCOLO = numprotocolo;

            DataTable dt = alerta.QueryAlertas();

            List<bAlertaRequerimento> _Alertas = new List<bAlertaRequerimento>();

            foreach (DataRow dr in dt.Rows)
            {
                bAlertaRequerimento c = new bAlertaRequerimento();
                c._t018_T004_NR_CNPJ_ORG_REG = orgaoregistro;
                c._t018_T005_NR_PROTOCOLO = numprotocolo;
                c.T018_t097_id_alerta = Int32.Parse(dr["id"].ToString());
                c.T018_valor = dr["valor"].ToString();
                c.Descricao = dr["Descricao"].ToString();
                //c.T018_status = Int32.Parse(dr["t018_status"].ToString());
                //c.T018_usr_desativo = dr["t018_usr_desativo"].ToString();
                //c.T018_dt_desativo = DateTime.Parse(dr["t018_dt_desativo"].ToString());

                _Alertas.Add(c);
            }

            return _Alertas;

        }

        public DataTable Query()
        {
            if (_t018_T005_NR_PROTOCOLO == string.Empty)
                return new DataTable();

            dt018_alerta_requerimento alerta = new dt018_alerta_requerimento();
            alerta.T018_T005_NR_PROTOCOLO = _t018_T005_NR_PROTOCOLO;

            return alerta.Query();
 
        }

        public void Update()
        {
            using (dt018_alerta_requerimento dal = new dt018_alerta_requerimento())
            {
                dal.T018_T004_NR_CNPJ_ORG_REG   = _t018_T004_NR_CNPJ_ORG_REG;
                dal.T018_T005_NR_PROTOCOLO      = _t018_T005_NR_PROTOCOLO;
                dal.T018_t097_id_alerta         = _t018_t097_id_alerta;
                dal.T018_valor                  = _t018_valor;
                dal.T018_status                 = _t018_status;
                dal.T018_usr_desativo           = _t018_usr_desativo;
                dal.T018_dt_desativo            = _t018_dt_desativo;
                
                dal.Update();
            }
        }
     
     }
}
