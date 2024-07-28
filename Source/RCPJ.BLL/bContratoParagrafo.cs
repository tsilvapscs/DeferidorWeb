using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoParagrafo
    {
        #region Variables
        private string _t005_nr_requerimento;
        private int _t030_id_contrato;
        private int _t031_sq_clausula;
        private int _t033_id_paragrafo;
        private string _t033_texto_paragrafo;
      
        #endregion

        #region Declarations
        public string T005_nr_requerimento
        {
            get { return _t005_nr_requerimento; }
            set { _t005_nr_requerimento = value; }
        }
        public int T030_id_contrato
        {
            get { return _t030_id_contrato; }
            set { _t030_id_contrato = value; }
        }
        public int T031_sq_clausula
        {
            get { return _t031_sq_clausula; }
            set { _t031_sq_clausula = value; }
        }
        public int T033_id_paragrafo
        {
            get { return _t033_id_paragrafo; }
            set { _t033_id_paragrafo = value; }
        }
        public string T033_texto_paragrafo
        {
            get { return _t033_texto_paragrafo; }
            set { _t033_texto_paragrafo = value; }
        }
        #endregion

        #region Data Access
        public DataTable Query()
        {
            using (dContratoParagrafo dal = new dContratoParagrafo())
            {
                dal.T005_nr_requerimento = _t005_nr_requerimento;
                dal.T031_sq_clausula = _t031_sq_clausula;
                dal.T030_id_contrato = _t030_id_contrato;
                return dal.Query();
            }
        }

        public void Insert()
        {
            using (dContratoParagrafo dal = new dContratoParagrafo())
            {
                dal.T005_nr_requerimento = _t005_nr_requerimento;
                dal.T030_id_contrato = _t030_id_contrato;
                dal.T031_sq_clausula = _t031_sq_clausula;
                dal.T033_texto_paragrafo = _t033_texto_paragrafo;

                dal.Insert();

                _t033_id_paragrafo = dal.T033_id_paragrafo;
            }
        }
        public void Update()
        {
            using (dContratoParagrafo dal = new dContratoParagrafo())
            {
                dal.T033_id_paragrafo = _t033_id_paragrafo;
                dal.T033_texto_paragrafo = _t033_texto_paragrafo;

                dal.Update();
            }
        }
        public void Delete()
        {
            using (dContratoParagrafo dal = new dContratoParagrafo())
            {
                dal.T033_id_paragrafo = _t033_id_paragrafo;
                dal.Delete();
            }
        }
        #endregion
    }
}
