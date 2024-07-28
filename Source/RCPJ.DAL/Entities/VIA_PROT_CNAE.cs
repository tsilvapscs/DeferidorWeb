using System;
using System.Data;
using System.Text;
using psc.Framework;
using psc.Framework.Data;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.ConnectionBase;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    /// <summary>
    /// Purpose: Data Access class for the table 'VIA_PROT_CNAE'.
    /// </summary>
    public class VIA_PROT_CNAE : DBInteractionBase
    {
        #region Class Member Declarations
        protected decimal _vPV_TIP_CNAE;
        protected string _vPC_TAE_COD_ACTVD, _vPC_COD_PROTOCOLO;
        protected string _VPV_TAD_SEQUENCIA = "";
        protected string _VPV_TAD_TIN_CNPJ = "";

        #endregion

        #region Class Property Declarations
        public string VPC_COD_PROTOCOLO
        {
            get
            {
                return (string)_vPC_COD_PROTOCOLO;
            }
            set
            {
                _vPC_COD_PROTOCOLO = value;
            }
        }


        public string VPC_TAE_COD_ACTVD
        {
            get
            {
                return (string)_vPC_TAE_COD_ACTVD;
            }
            set
            {
                _vPC_TAE_COD_ACTVD = value;
            }
        }


        public decimal VPV_TIP_CNAE
        {
            get
            {
                return (decimal)_vPV_TIP_CNAE;
            }
            set
            {
                _vPV_TIP_CNAE = value;
            }
        }
        public string VPV_TAD_SEQUENCIA
        {
            get
            {
                return (string)_VPV_TAD_SEQUENCIA;
            }
            set
            {
                _VPV_TAD_SEQUENCIA = value;
            }
        }
        public string VPV_TAD_TIN_CNPJ
        {
            get
            {
                return (string)_VPV_TAD_TIN_CNPJ;
            }
            set
            {
                _VPV_TAD_TIN_CNPJ = value;
            }
        }


        #endregion

        #region Implements
        public DataTable Query(string pProtocolo)
        {
            string sql = "";
            sql = "Select * From via_prot_cnae a Where  a.vpc_cod_protocolo = '" + pProtocolo + "'";
            sql += "Order by VPV_TIP_CNAE, VPC_TAE_COD_ACTVD ";

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql));
        }

        public void Update()
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "VIA_PROT_CNAE_update";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_VPC_COD_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _vPC_COD_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_VPC_TAE_COD_ACTVD", MySqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _vPC_TAE_COD_ACTVD));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_VPV_TIP_CNAE", MySqlDbType.Int32, 22, ParameterDirection.Input, true, 1, 0, "", DataRowVersion.Proposed, _vPV_TIP_CNAE));

                if (_VPV_TAD_SEQUENCIA != null && _VPV_TAD_SEQUENCIA == "")
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_VPV_TAD_SEQUENCIA", MySqlDbType.Int32, 3, ParameterDirection.Input, true, 1, 0, "", DataRowVersion.Proposed, DBNull.Value));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_VPV_TAD_SEQUENCIA", MySqlDbType.Int32, 3, ParameterDirection.Input, true, 1, 0, "", DataRowVersion.Proposed, decimal.Parse(_VPV_TAD_SEQUENCIA)));
                }

                if (_VPV_TAD_TIN_CNPJ != null && _VPV_TAD_TIN_CNPJ == "")
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("V_VPV_TAD_TIN_CNPJ", MySqlDbType.VarChar, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("V_VPV_TAD_TIN_CNPJ", MySqlDbType.VarChar, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _VPV_TAD_TIN_CNPJ));
                }
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        public void DeleteCnaes(string pProtocolo)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" Delete via_prot_cnae ");
            sql.Append(" Where  vpc_cod_protocolo = '" + pProtocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
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
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        #endregion
    }
}
