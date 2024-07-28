using System;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

using psc.Framework;
using psc.Framework.Data;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.ConnectionBase;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    
    public class dContratoClausula : DBInteractionBase
    {
        #region Variables
        private string _t005_nr_requerimento;
        private int _t030_id_contrato;
        private int _t035_sq_clausula;
        private string _t035_texto;
        private int _t031_sq_clausula_mae;

       
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
        public int T035_sq_clausula
        {
            get { return _t035_sq_clausula; }
            set { _t035_sq_clausula = value; }
        }
        public string T035_texto
        {
            get { return _t035_texto; }
            set { _t035_texto = value; }
        }
        public int T031_sq_clausula_mae
        {
            get { return _t031_sq_clausula_mae; }
            set { _t031_sq_clausula_mae = value; }
        }
        #endregion

        public string GetClausula()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t035_sq_clausula
                                 , t035_texto
                          FROM  t035_contrato_clausula
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t035_sq_clausula = @v_t035_sq_clausula
                                AND t030_id_contrato = @v_t030_id_contrato ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t035_contrato_clausula");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_sq_clausula", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t035_sq_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));



                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                _mainConnection.Open();

                // Execute query.
                adaptador.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["t035_sq_clausula"].ToString();
                else 
                    return "";

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
            }

        }

        public string ConsultaExisteClausula()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t035_sq_clausula
                                 , t035_texto
                          FROM  t035_contrato_clausula
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t030_id_contrato = @v_t030_id_contrato
                                AND T031_sq_clausula_mae = @v_t031_sq_clausula_mae");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t035_contrato_clausula");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t031_sq_clausula_mae", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t031_sq_clausula_mae));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));



                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                _mainConnection.Open();

                // Execute query.
                adaptador.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["t035_sq_clausula"].ToString();
                else
                    return "";

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
            }

        }


        public string VerificaExisteClausula()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t035_sq_clausula
                                 , t035_texto
                          FROM  t035_contrato_clausula
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t030_id_contrato = @v_t030_id_contrato
                                AND t035_sq_clausula = @t035_sq_clausula");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t035_contrato_clausula");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_sq_clausula", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t035_sq_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));



                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                _mainConnection.Open();

                // Execute query.
                adaptador.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["t035_sq_clausula"].ToString();
                else
                    return "";

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
            }

        }

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t035_sq_clausula
                                 , t035_texto
                                 , t031_sq_clausula_mae
                          FROM  t035_contrato_clausula
                          Where t005_nr_requerimento = @v_t005_nr_requerimento AND t031_sq_clausula_mae = -1");

            if(_t030_id_contrato != 0)
                Sql.Append(" AND t030_id_contrato = @v_t030_id_contrato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t035_contrato_clausula");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                if (_t030_id_contrato != 0)
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));



                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                _mainConnection.Open();

                // Execute query.
                adaptador.Fill(toReturn);

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
            }

        }

        public DataTable QueryCalusulasEditadas()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t035_sq_clausula
                                 , t035_texto
                                 , t031_sq_clausula_mae
                          FROM  t035_contrato_clausula
                          Where t005_nr_requerimento = @v_t005_nr_requerimento 
                                 AND t031_sq_clausula_mae <> -1
                                 AND t030_id_contrato = @v_t030_id_contrato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t035_contrato_clausula");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));



                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                _mainConnection.Open();

                // Execute query.
                adaptador.Fill(toReturn);

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
            }

        }

        public void Insert()
        {

            StringBuilder SqlI = new StringBuilder();
            SqlI.AppendLine(@"INSERT INTO t035_contrato_clausula 
                                  (t005_nr_requerimento
                                  , t030_id_contrato
                                  , t035_texto
                                  , t031_sq_clausula_mae
                                  ) VALUES (
                                    @v_t005_nr_requerimento 
                                  , @v_t030_id_contrato
                                  , @v_t035_texto
                                  , @v_t031_sq_clausula_mae
                                  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t030_id_contrato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_sq_clausula", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t035_sq_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_texto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t035_texto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t031_sq_clausula_mae", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t031_sq_clausula_mae));

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

                cmdToExecute.ExecuteNonQuery();
                cmdToExecute.CommandText = "SELECT LAST_INSERT_ID()";
                _t035_sq_clausula = int.Parse(cmdToExecute.ExecuteScalar().ToString());

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

        public void Update()
        {

            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"UPDATE t035_contrato_clausula 
                                SET t035_texto = @v_t035_texto
                             WHERE t035_sq_clausula = @v_t035_sq_clausula");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_sq_clausula", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t035_sq_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_texto", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t035_texto));

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

            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"DELETE FROM t035_contrato_clausula 
                             WHERE t035_sq_clausula = @v_t035_sq_clausula");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t035_sq_clausula", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t035_sq_clausula));

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
