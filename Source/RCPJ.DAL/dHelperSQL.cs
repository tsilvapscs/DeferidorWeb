using System;
using System.Collections.Generic;
using System.Text;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace RCPJ.DAL.Helper
{
    public class dHelperSQL : DBInteractionBaseSQL
    {
        public dHelperSQL()
        {
        }

        #region atualiza requerimento jucerja
        public static void GravaRequerimentoJUCERJA(string nr_protocolo
                                                   , string nr_requerimento
                                                   , string nr_protocolo_viabilidade
                                                   , string arquivo_xml
                                                   , string cpfusuario
                                                   )
        {

            
            try
            {
                using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
                {
                    _conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "REQUERIMENTO_XML_update";

                        string _arq = "";

                        cmd.Parameters.Add(new SqlParameter("v_nr_protocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo));
                        cmd.Parameters.Add(new SqlParameter("v_nr_requerimento", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_requerimento));
                        cmd.Parameters.Add(new SqlParameter("v_nr_protocolo_viabilidade", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo_viabilidade));
                        cmd.Parameters.Add(new SqlParameter("v_arquivo_xml", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _arq));
                        cmd.Parameters.Add(new SqlParameter("v_cpf_responsavel", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, cpfusuario));
                        cmd.Parameters.Add(new SqlParameter("v_status_atualizacao", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ""));

                        cmd.Connection = _conn;
                        //cmd.Connection.Open();
                        cmd.ExecuteNonQuery();


                        StringBuilder Sql = new StringBuilder();
                        Sql.Append(" Update requerimento_xml ");
                        Sql.Append(" Set	arquivo_xml = @v_Arquivo ");
                        Sql.Append(" Where  nr_protocolo = '" + nr_protocolo + "'");

                        using (SqlCommand cmdToExecute2 = new SqlCommand())
                        {
                            cmdToExecute2.CommandText = Sql.ToString();
                            cmdToExecute2.CommandType = CommandType.Text;

                            cmdToExecute2.Parameters.Add(new SqlParameter("v_Arquivo", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, arquivo_xml));
                            cmdToExecute2.Connection = _conn;
                            //cmdToExecute2.Connection.Open();
                            cmdToExecute2.ExecuteNonQuery();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }
        #endregion

        #region StoredProcedure Carrega RequerimentoSQL

        public static bool CarregaRequerimentoMySQL(string NumRequerimento, String NumProtocoloRCPJ)
        {
            try
            {

                using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "CarregaRequerimentoMySQL";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@NumRequerimento", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumRequerimento));
                        cmd.Parameters.Add(new SqlParameter("@NumProtocoloRCPJ", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        public static string GetDataEntradaByProtocoloJUCERJA(string NumProtocoloJucerja)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select p.DTA_ENTRADA as 'data_entrada' from PROCESSOS p  ");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.NRO_PROTOCOLO = '" + NumProtocoloJucerja + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["data_entrada"].ToString() != "")
                            return toReturn.Rows[0]["data_entrada"].ToString();
                    return "";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static string GetDataProcessoJUCERJA(string wProtocoloJucerja)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");
                Sql.Append(@" 
                SELECT * FROM dbo.PROCESSOS
                WHERE 
                NRO_PROTOCOLO ='" + wProtocoloJucerja + "'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
                try
                {
                    // Execute query.

                    adapter.Fill(toReturn);
                    return toReturn.Rows[0]["DTA_ENTRADA"].ToString();
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }



        public static string GetNomeEmpresaByProtocoloRCPJ(string NumProtocoloRCPJ)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select p.sociedade as 'nome_busca' ");
                Sql.AppendLine(" from rcpjprod.caixa.dbo.tProtocolo p");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.codprotocolo = '" + NumProtocoloRCPJ + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["nome_busca"].ToString() != "")
                            return toReturn.Rows[0]["nome_busca"].ToString();
                    return "";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static string GetNomeEmpresaByProtocoloJUCERJA(string NumProtocoloJucerja)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select p.NOME_EMPRESARIAL as 'nome_busca' from PROCESSOS p  ");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.NRO_PROTOCOLO = '" + NumProtocoloJucerja + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["nome_busca"].ToString() != "")
                            return toReturn.Rows[0]["nome_busca"].ToString();
                    return "";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static bool IsProtocoloUtilizadoRCPJ(string NumProtocoloRCPJ )
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select count(*) ");
                Sql.AppendLine(" from t005_protocolo p");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.T005_NR_PROTOCOLO = '" + NumProtocoloRCPJ + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (int.Parse(toReturn.Rows[0][0].ToString()) > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static DataTable GetExigenciasRCPJ()
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionStringSQL()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");
                Sql.Append(@" 
                SELECT  cod_exigencia as 'Codigo'
                        ,desc_exigencia as 'Descricao'
                        

                FROM rcpjprod.balcao.dbo.tabexigencias
                ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static DataTable GetExigenciasJUCERJA(int NatJur)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable(); // ("Exigencias");
                //                Sql.Append(@" 
                //                SELECT  COD_EXIGENCIA as 'Codigo'
                //                        ,COD_EXIGENCIA + ' - ' +DESC_EXIGENCIA as 'Descricao'
                //                FROM EXIGENCIAS
                //                order by DESC_EXIGENCIA
                //                ");
                Sql.Append(@" 
                SELECT  j.COD_EXIGENCIA as 'Codigo'
                        ,j.COD_EXIGENCIA + ' - ' + e.DESC_EXIGENCIA as 'Descricao'
                FROM TAB_EXIGENCIA_NAT_JURIDICA j
                INNER JOIN EXIGENCIAS e
                ON j.COD_EXIGENCIA = e.COD_EXIGENCIA
                WHERE e.DTA_EXCLUSAO IS NULL and j.COD_NAT_JURIDICA = '" + NatJur
                + "' " );
                //+ "'  AND j.COD_EXIGENCIA != '063' ");

                Sql.AppendLine(" order by DESC_EXIGENCIA");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
                try
                {
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static DataTable GetExigenciasJUCERJA()
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");
                Sql.Append(@" 
                SELECT  COD_EXIGENCIA as 'Codigo'
                        ,DESC_EXIGENCIA as 'Descricao'
                        

                FROM EXIGENCIAS
                ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static DataTable GetEmTramiteNaJUCERJA(string wProtocoloJucerja)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");
                Sql.Append(@" 
                SELECT * FROM dbo.ULTIMO_ANDAMENTO_PROCESSO
                WHERE COD_SECAO_DESTINO in ('AC', 'DS','DC' , 'SG', 'PL' ,'PE') 
                AND NRO_PROTOCOLO ='" + wProtocoloJucerja + "'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
                try
                {
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }


        public static DataTable ValidaProcessoRequerimento(string processo, string nreq)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable(); // ("Exigencias");


                cmdToExecute.CommandText = "CONSULTA_REQUERIMENTO_PROTOCOLO";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new SqlParameter("@NRO_PROTOCOLO", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, processo));
                cmdToExecute.Parameters.Add(new SqlParameter("@NRO_REQUERIMENTO", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nreq));

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
                try
                {
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }



        public static string BuscaPorProcessoAssociadoJunta(string NumProtocolo)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(@"   SELECT NRO_REQUERIMENTO
	                                FROM PROCESSOS
	                                WHERE NRO_PROTOCOLO = '" + NumProtocolo + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["NRO_REQUERIMENTO"].ToString() != "")
                            return toReturn.Rows[0]["NRO_REQUERIMENTO"].ToString();
                    return "";
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        

        public void ExecuteNonQuery(StringBuilder sql)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnectionSQL;

            try
            {

                cmdToExecute.CommandText = sql.ToString();

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnectionSQL.Open();
                }
                else
                {
                    if (_mainConnectionProviderSQL.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProviderSQL.CurrentTransaction;
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
                    _mainConnectionProviderSQL.Dispose();
                }
                cmdToExecute.Dispose();
            }
        }

        public static DataTable ExecuteQuery(string Sql)
        {

            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                
                DataTable toReturn = new DataTable("result");
                
                cmdToExecute.CommandText = Sql;
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {

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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }

            
        }

        public DataTable ExecuteNonQuery(string Sql)
        {
            using (SqlConnection _conn = new SqlConnection(General.ConnectionString()))
            {
                SqlCommand cmdToExecute = new SqlCommand();
                
                DataTable toReturn = new DataTable();

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                try
                {
                   
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
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }
    }
}

