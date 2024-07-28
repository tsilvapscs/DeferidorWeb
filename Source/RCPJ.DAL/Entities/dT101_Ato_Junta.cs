using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT101_Ato_Junta : DBInteractionBase
    {
  
        #region  Property Declarations
        private string _t101_co_ato;
        private string _t101_ds_ato;
        private string _t101_tip_ato;
        #endregion

        #region Class Member Declarations

        public string T101_co_ato
        {
            get { return _t101_co_ato; }
            set { _t101_co_ato = value; }
        }
        public string T101_ds_ato
        {
            get { return _t101_ds_ato; }
            set { _t101_ds_ato = value; }
        }
        public string T101_tip_ato
        {
            get { return _t101_tip_ato; }
            set { _t101_tip_ato = value; }
        }
        #endregion

        #region Implements
       
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	t101_ato_or");
            Sql.AppendLine(" Where	1 = 1 ");

            if (!string.IsNullOrEmpty(_t101_tip_ato))
                Sql.AppendLine(" And	t101_tip_ato = @t101_tip_ato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t101_ato_or");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();

                cmdToExecute.Parameters.Add(new MySqlParameter("@t101_tip_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t101_tip_ato));

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }
       

        #endregion
    }
} 
