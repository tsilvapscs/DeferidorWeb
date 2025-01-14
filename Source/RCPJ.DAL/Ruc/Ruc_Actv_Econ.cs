using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;
using System.Data;
using System.Data.OracleClient;
//using Oracle.DataAccess.Types;
using System.Xml;
using System.IO;
using System.Data.OleDb;
namespace RCPJ.DAL.Ruc
{
    public class Ruc_Actv_Econ : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rae_calif_actv;
        protected decimal _rae_porcent;
        protected DateTime _rae_fec_actl;
        protected string _rae_tus_cod_usr;
        protected string _rae_tae_cod_actvd;
        protected string _rae_rge_pra_protocolo;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rae_calif_actv
        {
            get { return _rae_calif_actv; }
            set { _rae_calif_actv = value; }
        }
        public decimal rae_porcent
        {
            get { return _rae_porcent; }
            set { _rae_porcent = value; }
        }
        public DateTime rae_fec_actl
        {
            get { return _rae_fec_actl; }
            set { _rae_fec_actl = value; }
        }
        public string rae_tus_cod_usr
        {
            get { return _rae_tus_cod_usr; }
            set { _rae_tus_cod_usr = value; }
        }
        public string rae_tae_cod_actvd
        {
            get { return _rae_tae_cod_actvd; }
            set { _rae_tae_cod_actvd = value; }
        }
        public string rae_rge_pra_protocolo
        {
            get { return _rae_rge_pra_protocolo; }
            set { _rae_rge_pra_protocolo = value; }
        }
        #endregion

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into ruc_actv_econ");
                Sql.Append("  (");
                Sql.Append("	rae_calif_actv, ");
                Sql.Append("	rae_porcent, ");
                Sql.Append("	rae_fec_actl, ");
                Sql.Append("	rae_tus_cod_usr, ");
                Sql.Append("	rae_tae_cod_actvd, ");
                Sql.Append("	rae_rge_pra_protocolo");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_rae_calif_actv, ");
                Sql.Append("	:v_rae_porcent, ");
                Sql.Append("	:v_rae_fec_actl, ");
                Sql.Append("	:v_rae_tus_cod_usr, ");
                Sql.Append("	:v_rae_tae_cod_actvd, ");
                Sql.Append("	:v_rae_rge_pra_protocolo");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_calif_actv", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_calif_actv));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_porcent", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_porcent));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_fec_actl", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_fec_actl));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_tus_cod_usr", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_tus_cod_usr));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_tae_cod_actvd", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_tae_cod_actvd));
                cmdToExecute.Parameters.Add(new OracleParameter("v_rae_rge_pra_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rae_rge_pra_protocolo));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnectionORACLE.Open();
                }
                else
                {
                    if (_mainConnectionProviderORACLE.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnectionProviderORACLE.Dispose();
                }
            }

        }
    }
}
