using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoPergunta
    {
        #region Variables
        private string _t005_nr_requerimento;
        private int _t030_id_contrato;
        private int _t032_sq_campo;
        private string _t034_texto_pergunta;
      
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
        public int T032_sq_campo
        {
            get { return _t032_sq_campo; }
            set { _t032_sq_campo = value; }
        }
        public string T034_texto_pergunta
        {
            get { return _t034_texto_pergunta; }
            set { _t034_texto_pergunta = value; }
        }
        #endregion

        #region Data Access
        public static string GetValorCampo(string requerimento, int idCampo)
        {
            using (dContratoPergunta dal = new dContratoPergunta())
            {
                return dal.GetValorCampo(requerimento, idCampo);
            }
        }

        public static int VerificaExisteCampo(string requerimento, int idCampo)
        {
            using (dContratoPergunta dal = new dContratoPergunta())
            {
                return dal.VerificaExisteCampo(requerimento, idCampo);
            }
        }

        public DataTable Query()
        {
            using (dContratoPergunta dal = new dContratoPergunta())
            {
                dal.T005_nr_requerimento = _t005_nr_requerimento;
                return dal.Query();
            }
        }

        private void Insert()
        {
            using (dContratoPergunta dal = new dContratoPergunta())
            {
                dal.T005_nr_requerimento = _t005_nr_requerimento;
                dal.T030_id_contrato = _t030_id_contrato;
                dal.T032_sq_campo = _t032_sq_campo;
                dal.T034_texto_pergunta = _t034_texto_pergunta;

                dal.Insert();
            }
        }
        public void Update()
        {
            int ret = 0;
            using (dContratoPergunta dal = new dContratoPergunta())
            {
                ret = dal.VerificaExisteCampo(_t005_nr_requerimento, _t032_sq_campo);
            }

            if (ret == 0)
            {
                using (dContratoPergunta dal = new dContratoPergunta())
                {
                    dal.T005_nr_requerimento = _t005_nr_requerimento;
                    dal.T030_id_contrato = _t030_id_contrato;
                    dal.T032_sq_campo = _t032_sq_campo;
                    dal.T034_texto_pergunta = _t034_texto_pergunta;

                    dal.Insert();
                }
            }
            else
            {
                using (dContratoPergunta dal = new dContratoPergunta())
                {
                    dal.T005_nr_requerimento = _t005_nr_requerimento;
                    dal.T032_sq_campo = _t032_sq_campo;
                    dal.T034_texto_pergunta = _t034_texto_pergunta;

                    dal.Update();
                }
            }
        }
        //public void Delete()
        //{
        //    using (dContratoParagrafo dal = new dContratoParagrafo())
        //    {
        //        dal.T033_id_paragrafo = _t033_id_paragrafo;
        //        dal.Delete();
        //    }
        //}
        #endregion
    }
}
