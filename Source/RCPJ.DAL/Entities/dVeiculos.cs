using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dVeiculos : DBInteractionBase
    {
        #region Declarations
        string _t005_protocolo;
     
        string _placa = "";
        string _uf = "";
        string _municipio = "";
        string _propietario = "1";
        int _acao = 0;

        public string T005_protocolo
        {
            get { return _t005_protocolo; }
            set { _t005_protocolo = value; }
        } 
        public int Acao
        {
            get { return _acao; }
            set { _acao = value; }
        }

        public string Propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
        }

        public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        #endregion

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select *   ");
            Sql.AppendLine(" from t102_veiculos   ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  T005_PROTOCOLO = '" + _t005_protocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Veiculos");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
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

        public void Insert()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into t102_veiculos");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T005_PROTOCOLO ");
            SqlI.AppendLine("	,T102_PLACA ");
            SqlI.AppendLine("	,T102_UF ");
            SqlI.AppendLine("	,T012_MUNICIPIO ");
            SqlI.AppendLine("	,T012_PROPRIETARIO ");
            SqlI.AppendLine("   ,T102_ACAO ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T005_PROTOCOLO ");
            SqlI.AppendLine("	,@T102_PLACA ");
            SqlI.AppendLine("	,@T102_UF ");
            SqlI.AppendLine("	,@T012_MUNICIPIO ");
            SqlI.AppendLine("	,@T012_PROPRIETARIO ");
            SqlI.AppendLine("   ,@T102_ACAO ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T102_PLACA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _placa));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T102_UF", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_MUNICIPIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _municipio));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_PROPRIETARIO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _propietario));

                cmdToExecute.Parameters.Add(new MySqlParameter("@T102_ACAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _acao));
                

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

        public void Delete()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Delete from t102_veiculos where T005_PROTOCOLO = @T005_PROTOCOLO");
           

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo));
              

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
    }
}
