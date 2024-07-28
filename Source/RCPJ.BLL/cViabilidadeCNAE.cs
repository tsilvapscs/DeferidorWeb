using System;
using System.Collections;
using System.Data;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    /// <summary>
    /// Summary description for cViabilidadeCNAE.
    /// </summary>
    /// 
    [Serializable]
    public class cViabilidadeCNAE1
    {
        #region Class Member Declarations
        protected decimal   _pTipoCNAE, 
                            _pPercent;
        protected string    _pCodActiEcon, 
                            _pProtocolo, 
                            _pDescricaoCNAE, 
                            _pTipoCNAEDescripcion, 
                            _pSeqCNAE = "", 
                            _pCnpj = "";
        #endregion

        #region Class Property Declarations
        public string pTipoCNAEDescripcion
        {
            get
            {
                return (string)_pTipoCNAEDescripcion;
            }
            set
            {
                _pTipoCNAEDescripcion = value;
            }
        }
        public string pDescricaoCNAE
        {
            get
            {
                return (string)_pDescricaoCNAE;
            }
            set
            {
                _pDescricaoCNAE = value;
            }
        }
        public string pProtocolo
        {
            get
            {
                return (string)_pProtocolo;
            }
            set
            {
                _pProtocolo = value;
            }
        }


        public string pCodActiEcon
        {
            get
            {
                return (string)_pCodActiEcon;
            }
            set
            {
                _pCodActiEcon = value;
            }
        }


        public decimal pTipoCNAE
        {
            get
            {
                return (decimal)_pTipoCNAE;
            }
            set
            {
                _pTipoCNAE = value;
            }
        }
        public string pSeqCNAE
        {
            get
            {
                return _pSeqCNAE;
            }
            set
            {
                _pSeqCNAE = value;
            }
        }
        public string pCnpj
        {
            get
            {
                return _pCnpj;
            }
            set
            {
                _pCnpj = value;
            }
        }
        #endregion

        #region Implemens
        public static ArrayList Query(string pProtocolo)
        {
            DataTable toReturn = new DataTable();
            ArrayList Al = new ArrayList();
            cViabilidadeCNAE1 c;

            try
            {
                using (VIA_PROT_CNAE d = new VIA_PROT_CNAE())
                {
                    toReturn = d.Query(pProtocolo);//
                }

                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                {
                    c = new cViabilidadeCNAE1();
                    c._pProtocolo = (string)toReturn.Rows[a]["VPC_COD_PROTOCOLO"];
                    c._pCodActiEcon = (string)toReturn.Rows[a]["VPC_TAE_COD_ACTVD"];
                    c._pTipoCNAE = toReturn.Rows[a]["VPV_TIP_CNAE"] == System.DBNull.Value ? int.MinValue : decimal.Parse(toReturn.Rows[a]["VPV_TIP_CNAE"].ToString());
                    c._pSeqCNAE = (toReturn.Rows[a]["VPV_TAD_SEQUENCIA"].ToString() != "") ? toReturn.Rows[a]["VPV_TAD_SEQUENCIA"].ToString() : "";
                    c.pCnpj = (toReturn.Rows[a]["VPV_TAD_TIN_CNPJ"].ToString() != "") ? toReturn.Rows[a]["VPV_TAD_TIN_CNPJ"].ToString() : "";
                    using (dHelperActEcon ae = new dHelperActEcon())
                    {

                        c._pDescricaoCNAE = ae.QueryActividadEconomica(c._pCodActiEcon, "").Rows[0]["Tae_Desc"].ToString();
                        if (c._pSeqCNAE != null && c._pSeqCNAE != "")
                        {
                            c._pDescricaoCNAE = ae.QueryActividadEconomicaDecricaoSeq(c._pCodActiEcon, c._pSeqCNAE, "").Rows[0]["Descr"].ToString();
                        }
                    }
                   // c._pTipoCNAEDescripcion = dHelperQuery.getRegistroGeneria(7, c._pTipoCNAE.ToString()).Rows[0]["Descricao"].ToString();

                    Al.Add(c);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Al;
        }

        public static ArrayList QueryRucActvEcon(string pProtocolo)
        {
            DataTable toReturn = new DataTable();
            ArrayList Al = new ArrayList();
            cViabilidadeCNAE1 c;

            try
            {
                using (RUC_ACTV_ECON11 d = new RUC_ACTV_ECON11())
                {
                    d.rae_rge_pra_protocolo = pProtocolo;
                    toReturn = d.Query();
                }
                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                {
                    c = new cViabilidadeCNAE1();
                    c._pProtocolo = (string)toReturn.Rows[a]["RAE_RGE_PRA_PROTOCOLO"];
                    c._pCodActiEcon = (string)toReturn.Rows[a]["RAE_TAE_COD_ACTVD"];
                    c._pTipoCNAE = toReturn.Rows[a]["RAE_CALIF_ACTV"] == System.DBNull.Value ? int.MinValue : decimal.Parse(toReturn.Rows[a]["RAE_CALIF_ACTV"].ToString());
                    c._pPercent = toReturn.Rows[a]["RAE_PORCENT"] == System.DBNull.Value ? int.MinValue : decimal.Parse(toReturn.Rows[a]["RAE_PORCENT"].ToString());

                    using (dHelperActEcon ae = new dHelperActEcon())
                    {
                        c._pDescricaoCNAE = ae.QueryActividadEconomica(c._pCodActiEcon, "").Rows[0]["Tae_Desc"].ToString();
                    }
                   // c._pTipoCNAEDescripcion = dHelperQuery.getRegistroGeneria(7, c._pTipoCNAE.ToString()).Rows[0]["Descricao"].ToString();

                    Al.Add(c);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Al;
        }

        public void Update(ConnectionProvider cp)
        {
            using (VIA_PROT_CNAE c = new VIA_PROT_CNAE())
            {
                c.MainConnectionProvider = cp;
                c.VPC_COD_PROTOCOLO = pProtocolo;
                c.VPC_TAE_COD_ACTVD = pCodActiEcon;
                c.VPV_TIP_CNAE = pTipoCNAE;
                c.VPV_TAD_SEQUENCIA = pSeqCNAE;
                c.VPV_TAD_TIN_CNPJ = pCnpj;
                c.Update();
            }
        }

        public void DeleteCnaes(ConnectionProvider cp, string pProtocolo)
        {
            using (VIA_PROT_CNAE c = new VIA_PROT_CNAE())
            {
                c.MainConnectionProvider = cp;

                c.DeleteCnaes(pProtocolo);
            }
        }
        #endregion
    }
}
