using System;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using psc.Framework;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class RUC_ACTV_ECON11 : DBInteractionBase
    {
        #region  Property Declarations
        protected string _rae_calif_actv;
        protected decimal _rae_porcent;
        protected DateTime _rae_fec_actl;
        protected string _rae_tus_cod_usr;
        protected string _rae_tae_cod_actvd;
        protected string _rae_rge_pra_protocolo;
        #endregion

        #region Class Member Declarations
        public string rae_calif_actv
        {
            get
            {
                return _rae_calif_actv;
            }
            set
            {
                _rae_calif_actv = value;
            }
        }
        public decimal rae_porcent
        {
            get
            {
                return _rae_porcent;
            }
            set
            {
                _rae_porcent = value;
            }
        }
        public DateTime rae_fec_actl
        {
            get
            {
                return _rae_fec_actl;
            }
            set
            {
                _rae_fec_actl = value;
            }
        }
        public string rae_tus_cod_usr
        {
            get
            {
                return _rae_tus_cod_usr;
            }
            set
            {
                _rae_tus_cod_usr = value;
            }
        }
        public string rae_tae_cod_actvd
        {
            get
            {
                return _rae_tae_cod_actvd;
            }
            set
            {
                _rae_tae_cod_actvd = value;
            }
        }
        public string rae_rge_pra_protocolo
        {
            get
            {
                return _rae_rge_pra_protocolo;
            }
            set
            {
                _rae_rge_pra_protocolo = value;
            }
        }
        #endregion

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		rae_calif_actv, ");
            Sql.Append("		rae_porcent, ");
            Sql.Append("		rae_fec_actl, ");
            Sql.Append("		rae_tus_cod_usr, ");
            Sql.Append("		rae_tae_cod_actvd, ");
            Sql.Append("		rae_rge_pra_protocolo");
            Sql.Append(" From	RUC_ACTV_ECON");
            Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And	rae_calif_actv = _rae_calif_actv");
            //Sql.Append(" And	rae_porcent = _rae_porcent");
            //Sql.Append(" And	rae_fec_actl = _rae_fec_actl");
            //Sql.Append(" And	rae_tus_cod_usr = _rae_tus_cod_usr");
            //Sql.Append(" And	rae_tae_cod_actvd = _rae_tae_cod_actvd");


            if (_rae_rge_pra_protocolo != "")
            {
                Sql.Append(" And	rae_rge_pra_protocolo = '" + _rae_rge_pra_protocolo + "'");
            }

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("RUC_ACTV_ECON");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }


        internal DataTable QueryRucActvEcon(string pProtocolo)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
