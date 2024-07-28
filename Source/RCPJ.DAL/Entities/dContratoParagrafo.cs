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
    public class dContratoParagrafo : DBInteractionBase
    {
        #region Variables
        private string _t005_nr_requerimento;
        private int _t030_id_contrato;
        private int _t031_sq_clausula;
        private int _t033_id_paragrafo;
        private string _t033_texto_paragrafo;

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
        public int T031_sq_clausula
        {
            get { return _t031_sq_clausula; }
            set { _t031_sq_clausula = value; }
        }
        public int T033_id_paragrafo
        {
            get { return _t033_id_paragrafo; }
            set { _t033_id_paragrafo = value; }
        }
        public string T033_texto_paragrafo
        {
            get { return _t033_texto_paragrafo; }
            set { _t033_texto_paragrafo = value; }
        }
        #endregion

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t005_nr_requerimento
                                 , t030_id_contrato
                                 , t031_sq_clausula
                                 , t033_id_paragrafo
                                 , t033_texto_paragrafo 
                          FROM  t033_contrato_paragrafo
                          Where t005_nr_requerimento = @v_t005_nr_requerimento
                                AND t031_sq_clausula = @v_t031_sq_clausula");
            if(_t030_id_contrato != 0)
                Sql.AppendLine(" AND t030_id_contrato = @v_t030_id_contrato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T031_TEMPLATE_CONTRATO_CLAUSULAS_CONTEUDO");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_requerimento", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t005_nr_requerimento));
                if (_t030_id_contrato != 0)
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t030_id_contrato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t031_sq_clausula", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _t031_sq_clausula));



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

            SqlI.AppendLine(@"INSERT INTO t033_contrato_paragrafo 
                                  (t005_nr_requerimento
                                  , t030_id_contrato
                                  , t031_sq_clausula
                                  , t033_texto_paragrafo
                                  ) VALUES (
                                    @v_t005_nr_requerimento 
                                  , @v_t030_id_contrato
                                  , @v_t031_sq_clausula
                                  , @v_t033_texto_paragrafo
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t031_sq_clausula", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t031_sq_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t033_texto_paragrafo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t033_texto_paragrafo));

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
                _t033_id_paragrafo = int.Parse(cmdToExecute.ExecuteScalar().ToString());

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

            Sql.AppendLine(@"UPDATE t033_contrato_paragrafo 
                                SET t033_texto_paragrafo = @v_t033_texto_paragrafo
                             WHERE t033_id_paragrafo = @V_t033_id_paragrafo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("V_t033_id_paragrafo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t033_id_paragrafo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t033_texto_paragrafo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t033_texto_paragrafo));

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

            Sql.AppendLine(@"DELETE FROM t033_contrato_paragrafo 
                             WHERE t033_id_paragrafo = @V_t033_id_paragrafo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("V_t033_id_paragrafo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t033_id_paragrafo));

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
