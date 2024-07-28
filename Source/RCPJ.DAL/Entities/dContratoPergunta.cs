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
    public class dContratoPergunta : DBInteractionBase
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

        public string GetValorCampo(string requerimento, int idCampo)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT  t034_texto_pergunta as valor
                         FROM t034_contrato_pergunta
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t032_sq_campo = @v_t032_sq_campo");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t034_contrato_pergunta");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t032_sq_campo", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, idCampo));



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
                    return toReturn.Rows[0]["valor"].ToString();
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

        public int VerificaExisteCampo(string requerimento, int idCampo)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT  t034_texto_pergunta as valor
                         FROM t034_contrato_pergunta
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t032_sq_campo = @v_t032_sq_campo");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t034_contrato_pergunta");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t032_sq_campo", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, idCampo));



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

                return toReturn.Rows.Count;
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
                                 , t032_sq_campo
                                 , t034_texto_pergunta 
                         FROM t034_contrato_pergunta
                          Where t005_nr_requerimento = @v_t005_nr_requerimento");
                                // AND t032_sq_campo = @v_t032_sq_campo");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t034_contrato_pergunta");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t032_sq_campo", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t032_sq_campo));



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
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(@"INSERT INTO t034_contrato_pergunta 
                                  (t005_nr_requerimento
                                  , t030_id_contrato
                                  , t032_sq_campo
                                  , t034_texto_pergunta
                                  ) VALUES (
                                    @v_t005_nr_requerimento 
                                  , @v_t030_id_contrato
                                  , @v_t032_sq_campo
                                  , @v_t034_texto_pergunta
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t032_sq_campo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t032_sq_campo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t034_texto_pergunta", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t034_texto_pergunta));

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

        public void Update()
        {

            StringBuilder Sql = new StringBuilder();
            
            Sql.AppendLine(@"UPDATE t034_contrato_pergunta 
                                SET t034_texto_pergunta = @v_t034_texto_pergunta
                             WHERE t005_nr_requerimento = @v_t005_nr_requerimento
                                    and t032_sq_campo = @v_t032_sq_campo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t032_sq_campo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t032_sq_campo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t034_texto_pergunta", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t034_texto_pergunta));

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
