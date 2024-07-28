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
    public class dContratoPadrao : DBInteractionBase
    {
        public DataTable Query(int id_contrato)
        {

            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" SELECT *
                           FROM
                             t030_template_contrato
                           WHERE t030_id_contrato = @v_t030_id_contrato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T030_TEMPLATE_CONTRATO");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, id_contrato));

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
            }
        }


        public DataTable QueryTipoContrato(string _codAto, string _codNatureza)
        {

            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" SELECT t030_id_contrato as codigo , t030_nome as descricao
                           FROM
                             t030_template_contrato
                           WHERE t030_status = 1 AND t030_co_ato = @v_t030_co_ato 
                                 AND  t030_co_nat_juridica = @v_t030_co_nat_juridica");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T030_TEMPLATE_CONTRATO");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_co_ato", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _codAto));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t030_co_nat_juridica", MySqlDbType.VarChar, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _codNatureza));


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
            }
        }

        public DataTable QueryClausulaConteudo(int id_contrato)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT *
                         FROM t031_template_contrato_clausulas where t030_id_contrato = @v_id_contrato");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T031_TEMPLATE_CONTRATO_CLAUSULAS_CONTEUDO");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, id_contrato));



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

        public DataTable QueryClausulaCampos(int id_contrato)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t030_id_contrato            
                                 , t031_sq_clausula 
                                 , t032_sq_campo         
                                 , t032_nome_campo      
                                 , t032_origem_campo     
                                 , t032_tipo_campo      
                                 , t032_descricao_campo  
                                 , t032_titulo_campo 
                                 , t032_sq_campo_depende 
                                 FROM
                                 t032_template_contrato_clausula_campo where t030_id_contrato = @v_id_contrato");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T031_TEMPLATE_CONTRATO_CLAUSULAS_CAMPO");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, id_contrato));



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

        public DataTable QueryClausulaCamposByClausula(int id_contrato , int id_clausula)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT t030_id_contrato            
                                 , t031_sq_clausula 
                                 , t032_sq_campo         
                                 , t032_nome_campo      
                                 , t032_origem_campo     
                                 , t032_tipo_campo      
                                 , t032_descricao_campo  
                                 , t032_titulo_campo          
                                 FROM t032_template_contrato_clausula_campo 
                         WHERE t032_origem_campo = 1 And t030_id_contrato = @v_id_contrato
                                AND t031_sq_clausula = @v_id_clausula");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("T031_TEMPLATE_CONTRATO_CLAUSULAS_CAMPO");
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_id_contrato", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, id_contrato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_id_clausula", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, id_clausula));



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

    }
}
