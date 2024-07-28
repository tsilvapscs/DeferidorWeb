using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;
using System.Data;
using System.Data.OracleClient;
//using Oracle.DataAccess.Types;
using System.Xml;
using System.IO;
using System.Data.OleDb;

namespace RCPJ.DAL.Helper
{
    public class dHelperORACLE : DBInteractionBaseORACLE
    {
        public dHelperORACLE()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCod">
        ///  Lista de pCampo
        ///  1 - PCR_FLAG_CARTORIO
        ///  2 - PCR_FLAG_JUNTA
        ///  3 - PCR_UF
        ///  4 - PCR_INICIAL_NIRE
        ///  5 - PCR_EMAIL_INSTITUICAO
        /// </param>
        /// <returns></returns>
        /// 

        public void GravaReceitaArquivo(string protocolo, string xml)
        {
            OracleCommand cmdToExecute = new OracleCommand();
            cmdToExecute.CommandType = CommandType.Text;
            OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnectionORACLE;

            StringBuilder sqlReceita = new StringBuilder();
            sqlReceita.AppendLine(@"UPDATE PSC_RECEITA_ARQUIVO
                                        SET PRA_ARQUIVO = :pArquivo
                                        WHERE PRA_PRO_PROTOCOLO = '" + protocolo + "'");

            cmdToExecute.CommandText = sqlReceita.ToString();

            cmdToExecute.Parameters.Add(new OracleParameter("pArquivo", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, xml));

            if (_mainConnectionIsCreatedLocal)
            {
                // Open connection.
                _mainConnectionORACLE.Open();
            }
            else
            {
                if (_mainConnectionProviderORACLE.IsTransactionPending)
                {
                    cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
                }
            }

            cmdToExecute.ExecuteNonQuery();


        }

        public void ExecuteNonQuery(StringBuilder sql)
        {
            OracleCommand cmdToExecute = new OracleCommand();
            cmdToExecute.CommandType = CommandType.Text;
            OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnectionORACLE;

            try
            {

                cmdToExecute.CommandText = sql.ToString();

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnectionORACLE.Open();
                }
                else
                {
                    if (_mainConnectionProviderORACLE.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
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
                    _mainConnectionProviderORACLE.Dispose();
                }
                cmdToExecute.Dispose();
            }
        }

        public static DataTable ExecuteQuery(string Sql)
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                DataTable toReturn = new DataTable("Query");

                
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);
                    return toReturn;
                }
                catch (Exception ex)
                {
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

        public static string ExecuteScalar(string Sql)
        {
            try
            {
                string ret = "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = Sql;
                        cmd.CommandType = CommandType.Text;

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        return cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static DataTable getImagensContrato(string wProtocolo)
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Andamento");

                Sql.AppendLine(@"Select * from (
                            Select pi.*
                            from  PSC_PROTOCOLO_IMAGEM pi
                            Where   pi.ppi_id_sequencia in 
                                   (Select PPI_ID_SEQUENCIA from PSC_IMG_PROCESSO
                                    Where pip_nr_processo = '" + wProtocolo + "'");
                Sql.AppendLine(@"    and PPI_ID_SEQUENCIA = pi.ppi_id_sequencia)
                                  And pi.ppi_tip_arquivo = 1
                                  order by pip_sq_imagem desc
                                   )
                             Where        rownum = 1
                            union all
                            Select * from (
                            Select pi.*
                            from  PSC_PROTOCOLO_IMAGEM pi
                            Where   pi.ppi_id_sequencia in 
                                   (Select PPI_ID_SEQUENCIA from PSC_IMG_PROCESSO
                                    Where pip_nr_processo = '" + wProtocolo + "'");
                Sql.AppendLine(@"                      and PPI_ID_SEQUENCIA = pi.ppi_id_sequencia)
                                  And pi.ppi_tip_arquivo = 2
                                  order by pip_sq_imagem desc
                                   )
                             Where        rownum = 1");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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



        /// <summary>
        /// Retorna lista de processos numa seção
        /// </summary>
        /// <returns></returns>
        public static DataTable getProcessosSecao()
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("psc_config_regin");

//                Sql.AppendLine(@"Select p.nr_protocolo protocolo, p.co_sequencial sequencialProcesso
//                                       , p.co_natureza_juridica natureza , p.no_empresarial Nome
//                                       , pp.sq_andamento , pp.sq_funcionario_analista , pp.co_sequencial , pp.dt_andamento
//                                       , s.co_ato Ato
//                                From   andamento pp, processo p, solicitacao s
//                                Where  (pp.sq_andamento, pp.nr_protocolo) = (
//                                                                            Select max(a.sq_andamento), a.nr_protocolo
//                                                                            From   vwAndamento a
//                                                                            Where  a.nr_protocolo = pp.nr_protocolo
//                                                                            Group By a.nr_protocolo
//                                                                           )
//                                And    pp.nr_protocolo = p.nr_protocolo
//                                And    pp.si_secao_destino In ('JF')
//                                And p.nr_protocolo = s.nr_protocolo
//                                 And (   p.in_status_processo = 1 
//                                 )
//                                 And pp.dt_andamento > sysdate -60
//                                 And rownum = 1
//                                 order by pp.dt_andamento ");

                Sql.AppendLine(@"SELECT PROTOCOLO
                                 , COD_USUARIO
                                 , SEQ_ANDAMENTO
                                 , DT_ENTRADA
                                 , DT_SELECAO
                                 , SITUACAO
                                 , UNIDADE
                                 , NAT_JURIDICA natureza
                                 , ATO
                                 , NOME_EMPRESA nome
                            FROM
                              controleimagem.central_carga");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        public static DataTable getConfigRegin()
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("psc_config_regin");

                Sql.AppendLine(" select * from psc_config_regin ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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
        /// <summary>
        /// Carga dos requerimentos da base Mysql => Oracle
        /// </summary>
        /// <param name="NumRequerimento">Número do Requerimento</param>
        /// <param name="NumProtocoloRCPJ">Número do Processo do Órgão de Registro</param>
        /// <returns>(Verdadeiro/False) (Resposta se o procedimento foi com sucesso ou não</returns>
        public static bool CarregaRequerimentoMySQL(string NumRequerimento, String NumProtocoloRCPJ)
        {
            try
            {

                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "CarregaRequerimentoMySQL";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("@NumRequerimento", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumRequerimento));
                        cmd.Parameters.Add(new OracleParameter("@NumProtocoloRCPJ", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));

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

        public static string VerificaExigenciaSIARCO(string NumProtocolo) //,string Usuario)
        {
            try
            {
                //string oRetorno = "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "select PKG_EXIGENCIA.ConsultaExigencia('" + NumProtocolo + "') from dual";
                        cmd.CommandType = CommandType.Text; //CommandType.Text;

                        //cmd.Parameters.Add(new OracleParameter("pprotocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocolo));
                        //cmd.Parameters.Add(new OracleParameter("pUsuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, Usuario));
                        //cmd.Parameters.Add(new OracleParameter("retval", OracleType.VarChar, 0, ParameterDirection.ReturnValue, true, 0, 0, "retval", DataRowVersion.Proposed, oRetorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        //cmd.ExecuteNonQuery();
                        return cmd.ExecuteOracleScalar().ToString();

                        //return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static string CumpreExigenciaSIARCO(string NumProtocolo, string Usuario)
        {
            try
            {
                string oRetorno = "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "PKG_EXIGENCIA.CumpreExigencia";
                        cmd.CommandType = CommandType.StoredProcedure; //CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocolo));
                        cmd.Parameters.Add(new OracleParameter("pUsuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, Usuario));
                        cmd.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 2, ParameterDirection.InputOutput, true, 2, 0, "", DataRowVersion.Proposed, oRetorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();
                        return oRetorno = cmd.Parameters["pRetorno"].Value.ToString();
                        ;
                        //cmd.ExecuteOracleScalar().ToString();

                        // return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        //public static void CarregaXmlBaseOrgaoRegistro(string NumNire, XmlDocument wXml)
        //{
        //    try
        //    {
        //        string oRetorno = "";
        //        using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
        //        {
        //            using (OracleCommand cmd = new OracleCommand())
        //            {
        //                cmd.CommandText = "PKG_EXIGENCIA.CumpreExigencia";
        //                cmd.CommandType = CommandType.StoredProcedure; //CommandType.Text;

        //                cmd.Parameters.Add(new OracleParameter("pNrNire", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumNire));
        //                //cmd.Parameters.Add(new OracleParameter("pXmlSaida", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, Usuario));
        //                cmd.Parameters.Add(new OracleParameter("pXmlSaida", OracleType.Clob, 2, ParameterDirection.InputOutput, true, 2, 0, "", DataRowVersion.Proposed, oRetorno));



        //                cmd.Connection = _conn;
        //                cmd.Connection.Open();

        //                cmd.ExecuteNonQuery();
        //                wXml = cmd.Parameters["pRetorno"].Value.ToString();
        //                ;
        //                //cmd.ExecuteOracleScalar().ToString();

        //                // return cmd.Parameters["return_value"].Value.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw ex;
        //    }

        //}


        /// <summary>
        /// Busca o nome da empresa solicitada pelo número do processo do órgão de registro
        /// </summary>
        /// <param name="NumProtocoloJucerja">Número do processo do Òrgão de Registro</param>
        /// <returns>Nome da empresa encontrado na base</returns>
        public static string GetNomeEmpresaByProtocoloSIARCO(string NumProtocoloJunta)
        {
            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "select pkg_exigencia.consultanomeempresa('" + NumProtocoloJunta + "') from dual";
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.Add(new OracleParameter("pprotocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloJunta));
                        //cmd.Parameters.Add(new OracleParameter("@NumProtocoloRCPJ", OracleDbType.Varchar2, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                        //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        //cmd.ExecuteNonQuery();
                        return cmd.ExecuteScalar().ToString();

                        //return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static string GravaRequerimentoSIARCO(string nr_protocolo
                                                    , string nr_requerimento
                                                    , string nr_protocolo_viabilidade
                                                    , string arquivo_xml
                                                    , string cpfusuario
                                                    )
        {

            string _arq = "1";
            try
            {
                 
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "PKG_GERA_SIARCO.REQUERIMENTO_update";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo));
                        cmd.Parameters.Add(new OracleParameter("p_nr_requerimento", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_requerimento));
                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo_viabilidade", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo_viabilidade));
                        cmd.Parameters.Add(new OracleParameter("p_usuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, cpfusuario));
                        cmd.Parameters.Add(new OracleParameter("p_arquivo_xml", OracleType.Clob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _arq));
                        cmd.Parameters.Add(new OracleParameter("p_status_atualizacao", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ""));
                        cmd.Parameters.Add(new OracleParameter("retorno", OracleType.VarChar, 500, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, retorno));
                        //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection = _conn;
                        //cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        string Resp = cmd.Parameters["retorno"].Value.ToString();

                        if (Resp == "1") //E porque gravou certo ai atualizo o XML na tabela
                        {
                            StringBuilder Sql = new StringBuilder();
                            Sql.Append(" Update requerimento ");
                            Sql.Append(" Set	arquivo_xml = :pArquivo ");
                            Sql.Append(" Where  nr_protocolo = '" + nr_protocolo + "'");

                            using (OracleCommand cmdToExecute2 = new OracleCommand())
                            {
                                cmdToExecute2.CommandText = Sql.ToString();
                                cmdToExecute2.CommandType = CommandType.Text;

                                cmdToExecute2.Parameters.Add(new OracleParameter("pArquivo", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, arquivo_xml));
                                cmdToExecute2.Connection = _conn;
                                //cmdToExecute2.Connection.Open();
                                cmdToExecute2.ExecuteNonQuery();
                            }
                        }
                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static string GravaRequerimentoRCPJ(string nr_protocolo
                                                  , string nr_requerimento
                                                  , string nr_protocolo_viabilidade
                                                  , string arquivo_xml
                                                  )
        {

            string _arq = "1";
            try
            {

                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "PKG_GERA_SIARCO.REQUERIMENTO_update";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo));
                        cmd.Parameters.Add(new OracleParameter("p_nr_requerimento", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_requerimento));
                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo_viabilidade", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo_viabilidade));
                        cmd.Parameters.Add(new OracleParameter("p_arquivo_xml", OracleType.Clob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _arq));
                        cmd.Parameters.Add(new OracleParameter("p_status_atualizacao", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ""));
                        cmd.Parameters.Add(new OracleParameter("retorno", OracleType.VarChar, 500, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, retorno));
                        //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection = _conn;
                        //cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        string Resp = cmd.Parameters["retorno"].Value.ToString();

                        if (Resp == "1") //E porque gravou certo ai atualizo o XML na tabela
                        {
                            StringBuilder Sql = new StringBuilder();
                            Sql.Append(" Update requerimento ");
                            Sql.Append(" Set	arquivo_xml = :pArquivo ");
                            Sql.Append(" Where  nr_protocolo = '" + nr_protocolo + "'");

                            using (OracleCommand cmdToExecute2 = new OracleCommand())
                            {
                                cmdToExecute2.CommandText = Sql.ToString();
                                cmdToExecute2.CommandType = CommandType.Text;

                                cmdToExecute2.Parameters.Add(new OracleParameter("pArquivo", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, arquivo_xml));
                                cmdToExecute2.Connection = _conn;
                                //cmdToExecute2.Connection.Open();
                                cmdToExecute2.ExecuteNonQuery();
                            }
                        }
                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static string AtualizaXML(string nr_protocolo, string arquivo_xml)
        {
            string _arq = "";
            try
            {

                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    _conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {


                        StringBuilder Sql = new StringBuilder();
                        Sql.Append(" Update requerimento ");
                        Sql.Append(" Set	arquivo_xml = :pArquivo ");
                        Sql.Append(" Where  nr_protocolo = '" + nr_protocolo + "'");

                        using (OracleCommand cmdToExecute2 = new OracleCommand())
                        {
                            cmdToExecute2.CommandText = Sql.ToString(); //"PKG_SERVI_CONTROL.WBS_CONTROL_RECEPCION_Update" ;
                            cmdToExecute2.CommandType = CommandType.Text;

                            cmdToExecute2.Parameters.Add(new OracleParameter("pArquivo", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, arquivo_xml));
                            cmdToExecute2.Connection = _conn;
                            //cmdToExecute2.Connection.Open();

                            if (cmdToExecute2.ExecuteNonQuery() == 0)
                            {
                                throw new Exception("Ocorreu um problema ao atualizar o arquivo.");
                            }

                        }
                        return "";

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }


        public static string GravaAndamentoIndeferido(string pProtocolo, string pProtocoloVinculado, string pUsuario)
        {
            //Procedure GravaAndamentoIndeferido(p_nr_protocolo           varchar2,
            //                             p_nr_protocolo_vinculado varchar2,
            //                             p_usuario                varchar2,
            //                             retorno                  out varchar2)

            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "PKG_ANDAMENTO.GravaAndamentoIndeferido";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo_vinculado", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocoloVinculado));
                        cmd.Parameters.Add(new OracleParameter("p_usuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pUsuario));
                        cmd.Parameters.Add(new OracleParameter("retorno", OracleType.VarChar, 800, ParameterDirection.Output, true, 2, 0, "", DataRowVersion.Proposed, retorno));

                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["retorno"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }
        /// <summary>
        /// Faz o andamneto de DE
        /// </summary>
        /// <param name="nr_protocolo"></param>
        /// <param name="nr_requerimento"></param>
        /// <param name="nr_protocolo_viabilidade"></param>
        /// <param name="arquivo_xml"></param>
        /// <param name="cpfusuario"></param>
        /// <returns></returns>
        public static string GravaAndamento(string nr_protocolo
                                                   , string cpfusuario
                                                   )
        {
            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "PKG_ANDAMENTO.gravaandamento";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, nr_protocolo));
                        cmd.Parameters.Add(new OracleParameter("p_usuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, cpfusuario));
                        cmd.Parameters.Add(new OracleParameter("retorno", OracleType.VarChar, 2, ParameterDirection.Output, true, 2, 0, "", DataRowVersion.Proposed, retorno));

                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["retorno"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        #region Andamento

        public static string GravaAndamento(string pProtocolo, string pRrequerimento, string pViabilidade, string pCpf,
                                          string pCpfAnalista, string pSequencia, string pAndamento, string pDespacho,
                                          string pSecaoOrigem, string pSecaoDestino, string pXML)
        {
            //Procedure GravaAndamentoDoRequerimento (p_nr_protocolo in varchar2,
            //                                  p_nr_requerimento in varchar2,
            //                                  p_nr_protocolo_viabilidade in varchar2,
            //                                  p_cpf_funcionario in varchar2,
            //                                  p_cpf_analista in varchar2,
            //                                  p_co_sequencial in char,
            //                                  p_sq_andamento in varchar2,
            //                                  p_despacho    in varchar2,
            //                                  p_secao_origem in varchar2,
            //                                  p_secao_destino in varchar2,
            //                                  p_xml           in clob,
            //                                  p_retorno       out varchar2) is

            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "PKG_ANDAMENTO.GravaAndamentoDoRequerimento";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmd.Parameters.Add(new OracleParameter("p_nr_requerimento", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pRrequerimento));
                        cmd.Parameters.Add(new OracleParameter("p_nr_protocolo_viabilidade", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pViabilidade));
                        cmd.Parameters.Add(new OracleParameter("p_cpf_funcionario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pCpf));
                        cmd.Parameters.Add(new OracleParameter("p_cpf_analista", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pCpfAnalista));
                        cmd.Parameters.Add(new OracleParameter("p_co_sequencial", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pSequencia));
                        cmd.Parameters.Add(new OracleParameter("p_sq_andamento", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pAndamento));
                        cmd.Parameters.Add(new OracleParameter("p_despacho", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pDespacho));
                        cmd.Parameters.Add(new OracleParameter("p_secao_origem", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pSecaoOrigem));
                        cmd.Parameters.Add(new OracleParameter("p_secao_destino", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pSecaoDestino));
                        cmd.Parameters.Add(new OracleParameter("p_xml", OracleType.Clob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pXML));


                        cmd.Parameters.Add(new OracleParameter("p_retorno", OracleType.VarChar, 4000, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, retorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["p_retorno"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
        }


        public static DataTable GetUltimoAndamento(string pProtocolo)
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");

                cmdToExecute.CommandText = "pkg_andamento.consultaregultimoandamentoprc";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("p_Cursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumProtocoloRCPJ"></param>
        /// <returns></returns>
        public static bool IsProtocoloUtilizadoSIARCO(string NumProtocoloRCPJ)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
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

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        public static int VerificaLegalizacaoEmpresa(string pNire)
        {

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");

                Sql.AppendLine(@" select count(*) as qtd 
                              from psc_protocolo p
                                    inner join psc_ident_protocolo i on i.pip_pro_protocolo = p.pro_protocolo
                              where 1 =1
                                    and i.pip_nire = '" + pNire + "'");
                Sql.AppendLine("         and pro_nr_requerimento is not null");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;

                
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                try
                {
                    adapter.Fill(toReturn);
                    return int.Parse(toReturn.Rows[0][0].ToString());
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


        public static string BacaProximoAndamParaExaminador(string NumProtocoloRCPJ)
        {
            try
            {
                string oRetorno = "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "BacaProximoAndamParaExaminador";
                        cmd.CommandType = CommandType.StoredProcedure; //CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                        //cmd.Parameters.Add(new OracleParameter("pUsuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, Usuario));
                        cmd.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 2, ParameterDirection.InputOutput, true, 2, 0, "", DataRowVersion.Proposed, oRetorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();
                        return cmd.Parameters["pRetorno"].Value.ToString();
                        ;
                        //cmd.ExecuteOracleScalar().ToString();

                        // return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
        }

        public static string BacaProximoAndamParaCliente(string NumProtocoloRCPJ)
        {
            try
            {
                string oRetorno = "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "BacaProximoAndamentoAposCE";
                        cmd.CommandType = CommandType.StoredProcedure; //CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                        //cmd.Parameters.Add(new OracleParameter("pUsuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, Usuario));
                        cmd.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 2, ParameterDirection.InputOutput, true, 2, 0, "", DataRowVersion.Proposed, oRetorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();
                        return cmd.Parameters["pRetorno"].Value.ToString();
                        ;
                        //cmd.ExecuteOracleScalar().ToString();

                        // return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExigenciasSIARCO()
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Exigencias");

                cmdToExecute.CommandText = "pkg_exigencia.wsconsultaexigencias";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new OracleParameter("p_Cursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));
                //cmd.Parameters.Add(new OracleParameter("@NumProtocoloRCPJ", OracleDbType.Varchar2, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        public static string CriaAndamento(string p_protocolo, string p_si_secao_destino, string p_usuario)
        {

            //Procedure GravaAndaPreliminar (
            //  p_protocolo             varchar2,
            //  p_si_secao_destino       varchar2,
            //  p_usuario                varchar2,
            //  retorno              out varchar2)
            //vai retornar 1 ou uma mensagem de erro
            try
            {

                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";

                        cmd.CommandText = "PKG_ANDAMENTO.GravaAndaPreliminar";
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add(new OracleParameter("p_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, p_protocolo));
                        cmd.Parameters.Add(new OracleParameter("p_si_secao_destino", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, p_si_secao_destino));
                        cmd.Parameters.Add(new OracleParameter("p_usuario", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, p_usuario));

                        cmd.Parameters.Add(new OracleParameter("retorno", OracleType.VarChar, 2000, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, retorno));



                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["retorno"].Value.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static void CriaAndamentoProtocolo(string pprotocolo, string psistema, string pcosequencial, int psqfuncionario, int panalista, int pvinculado)
        {
            

            //Procedure gravarAndamentoProtocolo(
            //                         pProtocolo in Varchar,
            //                         pSistema   In Varchar,
            //                         pCoSequencial in char,
            //                         pSqFuncionario In Number,
            //                         pAnalista In Number,
            //                         pVinculado In Number Default 1)
            try
            {

                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";

                        cmd.CommandText = "PKG_ANDAMENTO.gravarandamentoprotocolo";
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pprotocolo));
                        cmd.Parameters.Add(new OracleParameter("pSistema", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, psistema));
                        cmd.Parameters.Add(new OracleParameter("pCoSequencial", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pcosequencial));
                        cmd.Parameters.Add(new OracleParameter("pSqFuncionario", OracleType.Int32, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, psqfuncionario));
                        cmd.Parameters.Add(new OracleParameter("pAnalista", OracleType.Int32, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, DBNull.Value));
                        cmd.Parameters.Add(new OracleParameter("pVinculado", OracleType.Int32, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pvinculado));

                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static string AddExigenciasSiarco(XmlDocument XmlExigencias)
        {
            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        try
                        {
                            string pRetorno = "";
                            cmd.CommandText = "PKG_EXIGENCIA.WSProcessoExigencia";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new OracleParameter("pxml", OracleType.Clob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, XmlExigencias.InnerXml));
                            cmd.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 4000, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, pRetorno));
                            //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;
                            cmd.Connection = _conn;
                            cmd.Connection.Open();

                            cmd.ExecuteNonQuery();
                            //return cmd.ExecuteScalar().ToString();

                            return cmd.Parameters["pRetorno"].Value.ToString();
                        }
                        catch (Exception ex)
                        {
                            return ex.Message + " " + cmd.Parameters["pRetorno"].Value.ToString();
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

        #region Validacao Login Outros Sistemas
        public DataTable getUsuarioJuntaPE(string pChave)
        {

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	prt_usuariocg");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	chavelogin = '" + pChave + "'");

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                DataTable toReturn = new DataTable("prt_usuariocg");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
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
        #endregion

        public static DateTime SysdateOracle()
        {

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Select Sysdate pFecha From dual ");

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                DataTable toReturn = new DataTable("SYSDATEORACLE");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;
                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                try
                {
                    adapter.Fill(toReturn);
                    return DateTime.Parse(toReturn.Rows[0]["pFecha"].ToString());
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

        public static string GetDataEntradaByProtocoloSIARCO(string NumProtocoloSiarco)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(@" select dt_solicitacao as data_entrada 
                                    from solicitacao s
                                    where s.nr_protocolo = '" + NumProtocoloSiarco + @"'
                                     and s.co_ato in ('080','090','002', '091')
                                     order by dt_solicitacao");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        #region ChamaXml de Empresa


        /// <summary>
        /// Verificas the processa web service.
        /// </summary>
        /// <returns></returns>
        public static DataSet getXmlEmpresaDs(string pNire)
        {
            DataSet result = new DataSet();
            XmlTextReader reader = new XmlTextReader(new StringReader(getXmlEmpresa(pNire, 2)));
            result.ReadXml(reader);

            return result;
        }
        /// <summary>
        /// Verificas the processa web service.
        /// </summary>
        /// <returns></returns>
        public static DataSet getXmlEmpresaDs(string pNire, int pUnionCnae)
        {
            DataSet result = new DataSet();
            XmlTextReader reader = new XmlTextReader(new StringReader(getXmlEmpresa(pNire, pUnionCnae)));
            result.ReadXml(reader);

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNire"></param>
        /// <returns></returns>
        public static string getXmlEmpresa(string pNire, int pUnionCnae)
        {
            using (OracleConnection _Connection = new OracleConnection(General.ConnectionString()))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    cmdToExecute.CommandText = "pkg_util.getXmlEmpresa";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    // Use base class' connection object
                    cmdToExecute.Connection = _Connection;

                    cmdToExecute.Parameters.Add(new OracleParameter("pNire", OracleType.Char, 11, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pNire));
                    cmdToExecute.Parameters.Add(new OracleParameter("pUnion", OracleType.Int32, 11, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pUnionCnae));
                    cmdToExecute.Parameters.Add(new OracleParameter("pXmlOut", OracleType.Clob, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));

                    // Open connection.
                    _Connection.Open();

                    cmdToExecute.ExecuteNonQuery();
                    // Execute query.

                    OracleLob CLOB = (OracleLob)cmdToExecute.Parameters["pXmlOut"].Value;

                    return (string)CLOB.Value;

                }
            }
        }
        #endregion

        public static int getNumeroAlteracaoSiarco(string pNire)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"select Count(*) QtdAtos from 
                            (
                              select    haa.dt_arquiv_desarquiv
                              from      historico_ato_arquivado haa, historico_evento_pessoa hep, pessoa_juridica PJ 
                              where     pj.sq_pessoa             = hep.sq_pessoa
                                        and       hep.sq_arquivamento = haa.sq_arquivamento ");
            sql.AppendLine("     and pj.nr_nire = '" + pNire + "'");
            sql.AppendLine(@"  Union All
                              select    haa.dt_arquiv_desarquiv
                              from      historico_ato_arquivado haa, historico_evento_pessoa hep, pessoa_juridica PJ 
                              where     hep.sq_arquivamento = haa.sq_arquivamento
                              and       pj.sq_pessoa             = hep.sq_pessoa ");
            sql.AppendLine("   and       pj.nr_nire_sede = '" + pNire + "'");
            sql.AppendLine(@" )
                              Where       1 = 1 
                              --And         ato In ('002', 'B09'");
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("NumeroAtos");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);

                    return Convert.ToInt32(toReturn.Rows[0]["QtdAtos"].ToString());

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
        public static bool IsUsuarioSiarco(string pCPF)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "Select Count(*) From funcionario t where t.nr_cpf = '" + pCPF + "'";
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Connection.Open();
                    // Execute query.
                    object pCount = cmdToExecute.ExecuteScalar();

                    if (decimal.Parse(pCount.ToString()) > 0)
                        return true;
                    else
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
                }
            }
        }

        public static bool IsUsuarioSiarcoAnalista(string pCPF)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine(@"Select Count(*) 
                                From   funcionario t 
                                where  t.nr_cpf = '" + pCPF + "'");
                sql.AppendLine(@"       And t.co_funcao in (select tfa_codigo 
                                                           from TAB_FUNCAO_ANALISTA Where TFA_SISTEMA = 1)");

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Connection.Open();
                    // Execute query.
                    object pCount = cmdToExecute.ExecuteScalar();

                    if (decimal.Parse(pCount.ToString()) > 0)
                        return true;
                    else
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
                }
            }
        }

        public static string DevolveUsuarioSiarco(string pCPF)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "Select no_funcionario,nr_cpf From funcionario where nr_cpf = '" + pCPF + "'";
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;



                try
                {
                    cmdToExecute.Connection.Open();
                    // Execute query.
                    DataTable Result = new DataTable();
                    //cmd.ExecuteNonQuery();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                    adapter.Fill(Result);

                    //ret = cmd.ExecuteScalar().ToString();

                    if (Result.Rows.Count > 0)
                    {

                        return Result.Rows[0]["no_funcionario"].ToString();
                    }
                    else
                    {
                        return "";
                    }
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
                }
            }
        }

        public static DataTable getFuncionarioSiarco(string pCPF)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = @"select f.nr_cpf cpf, f.sq_funcionario , f.co_sequencial Unidade , f.co_funcao, f.no_funcionario
                                            from funcionario f
                                            where f.nr_cpf = '" + pCPF + "'";
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;



                try
                {
                    cmdToExecute.Connection.Open();

                    DataTable Result = new DataTable();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                    adapter.Fill(Result);

                    return Result;
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
                }
            }
        }

         public static DataTable getFuncionarioFiltro(string pCPF)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = @"Select tir_cpf_resp, tff_tipo_filtro , 
                                                    tff_valor 
                                            From  TAB_FUNCIONARIO_FILTRO Where tir_cpf_resp = '" + pCPF + "'";
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;



                try
                {
                    cmdToExecute.Connection.Open();

                    DataTable Result = new DataTable();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                    adapter.Fill(Result);

                    return Result;
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
                }
            }
        }

        

        public static DataTable getFuncionarioAndamentoDBE(string pProtocolo, string pAndamento)
        {


            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("AndamentoDBE");

                Sql.AppendLine(@" Select   f.no_funcionario as Nome , f. nr_cpf as CPF
                                  From funcionario f
                                        inner join andamento a
                                        on a.sq_funcionario_analista = f.sq_funcionario
                                  Where a.nr_protocolo = '" + pProtocolo + "'");
                Sql.AppendLine(@"        and a.si_secao_origem = 'CK' 
                                 and rownum = 1
                                 order by a.sq_andamento desc");  

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        public static void ConsultaAndamentoDBE(string pProtocolo, ref string _cpf, ref string _retorno)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                string pRetorno = "";
                string cpfFunc = "";

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "pkg_exigencia.ConsultaAndamentoDBE";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("cpfFunc", OracleType.VarChar, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, cpfFunc));
                cmdToExecute.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, pRetorno));

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Connection.Open();
                    cmdToExecute.ExecuteNonQuery();


                    _retorno = cmdToExecute.Parameters["pRetorno"].Value.ToString();
                    _cpf = cmdToExecute.Parameters["cpfFunc"].Value.ToString();

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
                }
            }
        }

        public static bool IsProtocoloSiarcoComCliente(string pProtocolo)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                String pRetorno = "";
                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "pkg_exigencia.ConsultaExigenciaComCliente";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                //cmd.Parameters.Add(new OracleParameter("@NumProtocoloRCPJ", OracleType.VarChar2, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                cmdToExecute.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, pRetorno));

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Connection.Open();
                    // Execute query.
                    cmdToExecute.ExecuteNonQuery();

                    if (cmdToExecute.Parameters["pRetorno"].Value.ToString() == "1")
                        return true;
                    else
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
                }
            }
        }

        public static bool IsProtocoloSiarcoComExaminador(string pProtocolo)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                String pRetorno = "";
                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = "pkg_exigencia.ConsultaExigenciaComExaminador";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("pRetorno", OracleType.VarChar, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, pRetorno));

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Connection.Open();
                    // Execute query.
                    cmdToExecute.ExecuteNonQuery();

                    if (cmdToExecute.Parameters["pRetorno"].Value.ToString() == "1")
                        return true;
                    else
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
                }
            }
        }

        public static string GetTipoLogradouro(string TipoLogradouro)
        {
            try
            {
                if (TipoLogradouro == "")
                    return "";
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        StringBuilder Sql = new StringBuilder();
                        Sql.AppendLine(" select tpologr from tab_cep_tipo_abrev ");
                        Sql.AppendLine(" where tpolograbrev= '" + TipoLogradouro + "'");
                        cmd.CommandText = Sql.ToString();
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.Add(new OracleParameter("pprotocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloJunta));
                        //cmd.Parameters.Add(new OracleParameter("@NumProtocoloRCPJ", OracleDbType.Varchar2, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, NumProtocoloRCPJ));
                        //cmd.Parameters.Add("return_value", OracleType.VarChar,).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection = _conn;
                        //cmd.Connection.Open();
                        DataTable Result = new DataTable();
                        //cmd.ExecuteNonQuery();
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        adapter.Fill(Result);

                        //ret = cmd.ExecuteScalar().ToString();

                        if (Result.Rows.Count > 0)
                        {

                            return Result.Rows[0][0].ToString();
                        }
                        else
                        {
                            return "";
                        }

                        //return cmd.Parameters["return_value"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }

        }

        public static DataTable getAndamento(string pProtocolo)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Andamento");

                Sql.AppendLine(@" select a.nr_protocolo 
                                        , a.sq_andamento 
                                        , a.si_secao_origem
                                        , e.no_secao as no_secao_origem
                                        , a.si_secao_destino 
                                        , dest.no_secao as no_secao_destino
                                        , a.dt_andamento
                                from andamento a
                                     inner join estrutura_organizacional e
                                     on a.si_secao_origem = e.si_secao
                                     and e.co_sequencial = '000'
                                          inner join estrutura_organizacional dest
                                     on a.si_secao_destino = dest.si_secao
                                     and dest.co_sequencial = '000'
                                where nr_protocolo = '" + pProtocolo + "'");
                Sql.AppendLine("      order by  a.sq_andamento");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

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

        public static string GetStatusBloqueio(string pNire)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@"Select     case p.in_bloqueio_desbloqueio
                                        When '1' Then 'NENHUM BLOQUEIO'
                                        When '2' Then 'DESBLOQUEADA'
                                        When '3' Then 'BLOQUEADA'
                                        End             BloqueioDescricao
                                From   pessoa p
                                       inner join  pessoa_juridica pj 
                                         on p.sq_pessoa = pj.sq_pessoa
                                Where   1 = 1 
                                and     pj.nr_nire = '" + pNire + "'");

            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                DataTable toReturn = new DataTable("Bloqueio");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                    {
                        return toReturn.Rows[0]["BloqueioDescricao"].ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ex)
                {
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


        public static string VerificaCargaProcesso(string pProtocolo)
        {
            //pkg_central_arquivo.apagacentralcarga(pprotocolo => :pprotocolo, psaida => :psaida);
            try
            {
                using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        string retorno = "";
                        cmd.CommandText = "pkg_central_arquivo.apagacentralcarga";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("pprotocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmd.Parameters.Add(new OracleParameter("psaida", OracleType.VarChar, 4000, ParameterDirection.Output, true, 2, 0, "", DataRowVersion.Proposed, retorno));

                        cmd.Connection = _conn;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return cmd.Parameters["psaida"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }


        }

        public static DataTable getUnidadeDNRC(string CodJunta, string Sequencia, string TipoSessao)
        {
            using (OracleConnection _conn = new OracleConnection(General.ConnectionString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("unidade");

                Sql.AppendLine(@" Select u.co_junta_comercial, u.co_sequencial, u.co_municipio, u.no_unidade, u.nr_cgc 
                              From unidade u Where 1 = 1 
                                    And u.in_geracao_nire = 'S'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.Connection = _conn;


                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
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

