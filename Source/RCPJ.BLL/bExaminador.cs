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
    public class bExaminador : DBInteractionBase
    {
        #region Class Member Declarations
        private string _requerimento;
        private String _Nome;
        private String _cpf;

        private Nullable<DateTime> _DataAnalise;

        private String _descricaoStatus;
        private int _status;

       
        #endregion

        #region Class Property Declarations

       
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }

        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        
        public Nullable<DateTime> DataAnalise
        {
            get { return _DataAnalise; }
            set { _DataAnalise = value; }
        }

        public String CPF
        {
            get { return _cpf.Trim(); }
            set { _cpf = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }


        #endregion

        #region Constructors
        public bExaminador()
        {
            InitClass();
        }

        public bExaminador(string pProtocolo)
            : this()
        {
            _requerimento = pProtocolo;
            Populate();
        }

        private void InitClass()
        {
            
            _Nome = string.Empty;
            _cpf = string.Empty;
        }
        private void Populate()
        {

            if (_requerimento == string.Empty)
                return;

            DataTable dt = Query();

            foreach (DataRow dr in dt.Rows)
            {

                _Nome = "";
                _cpf = dr["cpf_examinador"].ToString();
                _descricaoStatus = dr["Descricao"].ToString();
                _status = Int32.Parse(dr["status_situacao"].ToString());
                _DataAnalise = DateTime.Parse(dr["dataAnalise"].ToString());
            }

        }

        #endregion

        #region Data Acces
        public DataTable Query()
        {

            using (dT005_Protocolo dal = new dT005_Protocolo())
            {
                return dal.GetStatusExame(_requerimento);
            }

        }

       
        #endregion
    }
}
