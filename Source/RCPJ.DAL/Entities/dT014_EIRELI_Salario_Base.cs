using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT014_EIRELI_Salario_Base : DBInteractionBase
    {
        #region  Property Declarations
        private Nullable<DateTime> _t014_dt_inclusao;
        private Nullable<DateTime> _t014_dt_validade;
        private decimal _t014_salario_base;
        private string _t014_usuario;
        #endregion

        #region Class Member Declarations
        public Nullable<DateTime> t014_dt_inclusao
        {
            get { return _t014_dt_inclusao; }
            set { _t014_dt_inclusao = value; }
        }
        public Nullable<DateTime> t014_dt_validade
        {
            get { return _t014_dt_validade; }
            set { _t014_dt_validade = value; }
        }
        public decimal t014_salario_base
        {
            get { return _t014_salario_base; }
            set { _t014_salario_base = value; }
        }
        public string t014_usuario
        {
            get { return _t014_usuario; }
            set { _t014_usuario = value; }
        }
        #endregion

        #region Implements
        public DataTable Query(string DataValidade)
        {
            //string Sql = "Select * from t014_EIRELI_Salario_Base"; // DESC";
            string Sql = @"
                    SELECT *
                        FROM
                            t014_eireli_salario_base
                        WHERE
                            t014_dt_validade <= str_to_date('" + DataValidade + @"', '%d/%m/%Y')
                        ORDER BY
                            t014_dt_validade desc
                       limit 1";
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T014_EIRELI_SALARIO_BASE");
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

        public DataTable QueryExisteNaJunta(int wAnoEntrada)
        {
            string Sql = "Select t014_salario_base from t014_EIRELI_Salario_Base where year(t014_dt_validade) =" + wAnoEntrada; // DESC";
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t014_EIRELI_Salario_Base");
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
        #endregion
    }
}
