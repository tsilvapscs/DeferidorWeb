using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
using RCPJ.DAL.Helper;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using psc.Framework.Data;
using dal = psc.ApplicationBlocks.DAL;
using System.Data.OracleClient;
using RCPJ.DAL.Ruc;

namespace RCPJ.DAL
{
    public class dInscricaoEstadual : DBInteractionBase
    {
        #region  Variaveis
        private string _protocolo;
        private string _cnpj;
        private string _nire;
        private string _razaoSocial;
        private string _uf;
        private string _codmunicipio;
        private string _cnpjSefaz;
        private int _evento;
        private string _cnpjOrgaoRegistro;
        #endregion

        #region  Property Declarations
        public string CnpjSefaz
        {
            get { return _cnpjSefaz; }
            set { _cnpjSefaz = value; }
        }
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        public string Nire
        {
            get { return _nire; }
            set { _nire = value; }
        }
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }
        public string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public string Codmunicipio
        {
            get { return _codmunicipio; }
            set { _codmunicipio = value; }
        }
        public int Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }
        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        #endregion

        public String GetXML()
        {

            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = "GeraXML";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new MySqlParameter("pProtocolo", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, _protocolo));
                    cmd.Parameters.Add(new MySqlParameter("pXML", MySqlDbType.Text, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, ""));


                    cmd.Connection = _conn;
                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                    return (string)cmd.Parameters["pXML"].Value;
                }
            }
        }

        public void EnviaRequerimento()
        {
            EnviaRequerimentoOracle();
        }
        public void EnviaRequerimentoSQL()
        {

            try
            {

                string _xml = "";
                using (dInscricaoEstadual c = new dInscricaoEstadual())
                {
                    c.Protocolo = _protocolo;
                    _xml = c.GetXML();
                }
                //1-Chmar rotina para gerar o XML
                //2-gravar nas tabelas PSC_PROTOCOLO, PSC_PROTOCOLO_IDENT, PSC_PROT_EVENTO_RFB, WBS_CONTROL DE ENVIO

                using (ConnectionProviderSQL cp = new ConnectionProviderSQL())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(@"insert into psc_protocolo
                              (pro_protocolo, 
                              pro_status, 
                              pro_fec_inc, 
                              pro_tmu_tuf_uf, 
                              pro_tmu_cod_mun, 
                              pro_tip_operacao, 
                              pro_env_sef, 
                              pro_flag_vigilancia, 
                              pro_fec_atualizacao, 
                              pro_tge_tgacao, 
                              pro_tge_vgacao)
                            values (");
                    sql.AppendLine("'" + _protocolo + "'");
                    sql.AppendLine("," + "1");      //v_pro_status, 
                    sql.AppendLine(",'" + DateTime.Today.ToString("yyyyMMdd") + "'"); //v_pro_fec_inc, 
                    sql.AppendLine(",'" + _uf + "'");//v_pro_tmu_tuf_uf, 
                    sql.AppendLine("," + _codmunicipio); //v_pro_tmu_cod_mun, 
                    sql.AppendLine(", 52");//v_pro_tip_operacao, 
                    sql.AppendLine(", 1 ");//v_pro_env_sef, 
                    sql.AppendLine(", 2");//v_pro_flag_vigilancia, 
                    sql.AppendLine(",'" + DateTime.Today.ToString("yyyyMMdd") + "'");//v_pro_fec_atualizacao, 
                    sql.AppendLine(", 700");//v_pro_tge_tgacao, 
                    sql.AppendLine(", 2");//v_pro_tge_vgacao, 
                    sql.AppendLine("  ) ");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sql);
                    }

                    StringBuilder sqlIdent = new StringBuilder();
                    sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                    sqlIdent.Append("'" + _protocolo + "'");
                    sqlIdent.Append(", '" + _cnpj + "'");
                    sqlIdent.Append(", '" + _nire + "'");
                    sqlIdent.Append(", '" + _razaoSocial + "'");
                    sqlIdent.Append(" )");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlIdent);
                    }

                    StringBuilder sqlWbs = new StringBuilder();

                    sqlWbs.AppendLine(@"INSERT INTO PSC_RECEITA_ARQUIVO
                                           (PRA_PRO_PROTOCOLO
                                           ,PRA_ARQUIVO
                                           ,PRA_NOME_ARQUIVO
                                           ,pra_status_import)
                                     VALUES ( ");
                    sqlWbs.Append("'" + _protocolo + "'");
                    sqlWbs.Append(" ,'" + _xml + "'");
                    sqlWbs.Append(" ,'" + _protocolo + ".xml'");
                    sqlWbs.Append(" ,1");
                    sqlWbs.Append(" )");
                   

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlWbs);
                    }

                    StringBuilder sqlEvento = new StringBuilder();
                    sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                    sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                    sqlEvento.AppendLine(" Values ( ");
                    sqlEvento.AppendLine("'" + _protocolo + "'");
                    sqlEvento.AppendLine(" ," + _evento.ToString());
                    sqlEvento.AppendLine(" ) ");
                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlEvento);
                    }

                    StringBuilder sqlHomolog = new StringBuilder();
                    sqlHomolog.AppendLine(@" INSERT INTO MAC_LOG_CARGA_JUNTA_HOMOLOG
                                                                       (MLC_PROTOCOLO
                                                                       ,MLC_DTA_HOMOLOGACAO
                                                                       ,MLC_CPF_HOMOLOGADOR)
                                                                 VALUES           ( ");
                    sqlHomolog.AppendLine("'" + _protocolo + "'");
                    sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("yyyyMMdd") + "'");
                    sqlHomolog.AppendLine(" , '11111111111'");
                    //sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("yyyyMMdd") + "'");
                    sqlHomolog.AppendLine(" )");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlHomolog);
                    }

                    //Atualiza status do requerimento
                    UpdateStatus();
                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviaRequerimentoOracle()
        {

            try
            {

                string _xml = "";
                using (dInscricaoEstadual c = new dInscricaoEstadual())
                {
                    c.Protocolo = _protocolo;
                    _xml = c.GetXML();
                }
                //1-Chmar rotina para gerar o XML
                //2-gravar nas tabelas PSC_PROTOCOLO, PSC_PROTOCOLO_IDENT, PSC_PROT_EVENTO_RFB, WBS_CONTROL DE ENVIO

                using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();


                    using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                    {
                        p.MainConnectionProvider = cp;
                        p.Pro_protocolo = _protocolo;
                        p.Pro_status = 1;
                        p.Pro_fec_inc = dHelperORACLE.SysdateOracle();
                        p.Pro_tmu_tuf_uf = _uf;
                        p.Pro_tmu_cod_mun = Int32.Parse(_codmunicipio);
                        p.Pro_tip_operacao = 52;
                        p.Pro_env_sef = 1;
                        p.Pro_flag_vigilancia = 2;
                        p.Pro_fec_atualizacao = dHelperORACLE.SysdateOracle();
                        p.Pro_tge_tgacao = 700;
                        p.Pro_tge_vgacao = 1;
                        p.Pro_cnpj_org_reg = _cnpjOrgaoRegistro;
                        p.PRO_NR_REQUERIMENTO = _protocolo;
                        p.PRO_VPV_COD_PROTOCOLO = "";

                        p.Update();

                    }

//                    StringBuilder sql = new StringBuilder();
//                    sql.AppendLine(@"insert into psc_protocolo
//                              (pro_protocolo, 
//                              pro_status, 
//                              pro_fec_inc, 
//                              pro_tmu_tuf_uf, 
//                              pro_tmu_cod_mun, 
//                              pro_tip_operacao, 
//                              pro_env_sef, 
//                              pro_flag_vigilancia, 
//                              pro_fec_atualizacao, 
//                              pro_tge_tgacao, 
//                              pro_tge_vgacao)
//                            values (");
//                    sql.AppendLine("'" + _protocolo + "'");
//                    sql.AppendLine("," + "1");      
//                    sql.AppendLine(",'" + DateTime.Today.ToString("dd-MM-yyyy") + "'"); //v_pro_fec_inc, 
//                    sql.AppendLine(",'" + _uf + "'");
//                    sql.AppendLine("," + _codmunicipio);
//                    sql.AppendLine(", 52");
//                    sql.AppendLine(", 1 ");
//                    sql.AppendLine(", 2");
//                    sql.AppendLine(",'" + DateTime.Today.ToString("dd-MM-yyyy") + "'");//v_pro_fec_atualizacao, 
//                    sql.AppendLine(", 700");
//                    sql.AppendLine(", 5");
//                    sql.AppendLine("  ) ");

//                    using (dHelperORACLE c = new dHelperORACLE())
//                    {
//                        c.MainConnectionProvider = cp;
//                        c.ExecuteNonQuery(sql);
//                    }

                    StringBuilder sqlIdent = new StringBuilder();
                    sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                    sqlIdent.Append("'" + _protocolo + "'");
                    sqlIdent.Append(", '" + _cnpj + "'");
                    sqlIdent.Append(", '" + _nire + "'");
                    sqlIdent.Append(", '" + _razaoSocial + "'");
                    sqlIdent.Append(" )");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlIdent);
                    }

                    StringBuilder sqlWbs = new StringBuilder();
                    sqlWbs.AppendLine(@"INSERT INTO PSC_RECEITA_ARQUIVO
                                           (PRA_PRO_PROTOCOLO
                                           ,PRA_ARQUIVO
                                           ,PRA_NOME_ARQUIVO)
                                     VALUES ( ");
                    sqlWbs.Append("'" + _protocolo + "'");
                    sqlWbs.Append(" ,'' ");
                    sqlWbs.Append(" ,'" + _protocolo + ".xml'");
                    sqlWbs.Append(" )");
                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlWbs);
                    }


                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.GravaReceitaArquivo(_protocolo, _xml);
                    }


                    StringBuilder sqlEvento = new StringBuilder();
                    sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                    sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                    sqlEvento.AppendLine(" Values ( ");
                    sqlEvento.AppendLine("'" + _protocolo + "'");
                    sqlEvento.AppendLine(" ," + _evento.ToString());
                    sqlEvento.AppendLine(" ) ");
                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlEvento);
                    }


                    using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                    {
                        m.MainConnectionProvider = cp;

                        m.MLC_PROTOCOLO = _protocolo;
                        m.MLC_CPF_HOMOLOGADOR = "11111111111";
                        m.MLC_DTA_HOMOLOGACAO = dHelperORACLE.SysdateOracle();
                        m.MLC_DATA_CARREGA_WS11 = dHelperORACLE.SysdateOracle();
                        m.Update();

                    }
                    

                    //Atualiza status do requerimento
                    UpdateStatus();
                    
                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviaRequerimentoAutonomo(List<string> eventos)
        {

            try
            {

                string _xml = "";
                using (dInscricaoEstadual c = new dInscricaoEstadual())
                {
                    c.Protocolo = _protocolo;
                    _xml = c.GetXML();
                }
                //1-Chmar rotina para gerar o XML
                //2-gravar nas tabelas PSC_PROTOCOLO, PSC_PROTOCOLO_IDENT, PSC_PROT_EVENTO_RFB, WBS_CONTROL DE ENVIO

                using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(@"insert into psc_protocolo
                              (pro_protocolo, 
                              pro_status, 
                              pro_fec_inc, 
                              pro_tmu_tuf_uf, 
                              pro_tmu_cod_mun, 
                              pro_tip_operacao, 
                              pro_env_sef, 
                              pro_flag_vigilancia, 
                              pro_fec_atualizacao, 
                              pro_tge_tgacao, 
                              pro_tge_vgacao,
                              pro_cnpj_org_reg)
                            values (");
                    sql.AppendLine("'" + _protocolo + "'");
                    sql.AppendLine("," + "1");
                    sql.AppendLine(",'" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    sql.AppendLine(",'" + _uf + "'");
                    sql.AppendLine("," + _codmunicipio);
                    sql.AppendLine(", 53"); // Codigo 53
                    sql.AppendLine(", 2 ");
                    sql.AppendLine(", 2");
                    sql.AppendLine(",'" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    sql.AppendLine(", 700");
                    sql.AppendLine(", 2");
                    sql.AppendLine(",'" + _cnpjSefaz + "' ");
                    sql.AppendLine("  ) ");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sql);
                    }

                    StringBuilder sqlIdent = new StringBuilder();
                    sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                    sqlIdent.Append("'" + _protocolo + "'");
                    sqlIdent.Append(", '" + _cnpj + "'");
                    sqlIdent.Append(", '" + _nire + "'");
                    sqlIdent.Append(", '" + _razaoSocial + "'");
                    sqlIdent.Append(" )");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlIdent);
                    }

                    StringBuilder sqlWbs = new StringBuilder();
                    sqlWbs.AppendLine(@"INSERT INTO PSC_RECEITA_ARQUIVO
                                           (PRA_PRO_PROTOCOLO
                                           ,PRA_ARQUIVO
                                           ,PRA_NOME_ARQUIVO)
                                     VALUES ( ");
                    sqlWbs.Append("'" + _protocolo + "'");
                    sqlWbs.Append(" ,'' ");
                    sqlWbs.Append(" ,'" + _protocolo + ".xml'");
                    sqlWbs.Append(" )");
                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlWbs);
                    }


                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.GravaReceitaArquivo(_protocolo, _xml);
                    }

                    StringBuilder sqlEvento = new StringBuilder();
                    for (int i = 0; i < eventos.Count; i++)
                    {

                        sqlEvento = new StringBuilder();
                        sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                        sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                        sqlEvento.AppendLine(" Values ( ");
                        sqlEvento.AppendLine("'" + _protocolo + "'");
                        sqlEvento.AppendLine(" ," + eventos[i].ToString());
                        sqlEvento.AppendLine(" ) ");
                        using (dHelperORACLE c = new dHelperORACLE())
                        {
                            c.MainConnectionProvider = cp;
                            c.ExecuteNonQuery(sqlEvento);
                        }
                    }


                    StringBuilder sqlHomolog = new StringBuilder();
                    sqlHomolog.AppendLine(@" INSERT INTO MAC_LOG_CARGA_JUNTA_HOMOLOG
                                                                       (MLC_PROTOCOLO
                                                                       ,MLC_DTA_HOMOLOGACAO
                                                                       ,MLC_CPF_HOMOLOGADOR)
                                                                 VALUES           ( ");
                    sqlHomolog.AppendLine("'" + _protocolo + "'");
                    sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    sqlHomolog.AppendLine(" , '11111111111'");
                    //sqlHomolog.AppendLine(" , '" + DateTime.Today.ToString("dd-MM-yyyy") + "'");
                    sqlHomolog.AppendLine(" )");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlHomolog);
                    }

                    //Atualiza status do requerimento
                    UpdateStatus();
                   
                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateStatus()
        {
            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     t011_protocolo_status Set ");
            SqlU.AppendLine("		T011_IN_SITUACAO = @v_T011_IN_SITUACAO ");
            SqlU.AppendLine(" Where	t005_nr_protocolo = @v_t005_nr_protocolo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T011_IN_SITUACAO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, '3'));

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

        public void ReennviaRequerimento()
        {

            try
            {

                string _xml = "";
                using (dInscricaoEstadual c = new dInscricaoEstadual())
                {
                    c.Protocolo = _protocolo;
                    _xml = c.GetXML();
                }
                //1-Chmar rotina para gerar o XML
                //2-gravar nas tabelas PSC_PROTOCOLO, PSC_PROTOCOLO_IDENT, PSC_PROT_EVENTO_RFB, WBS_CONTROL DE ENVIO

                using (ConnectionProviderSQL cp = new ConnectionProviderSQL())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" DELETE PSC_RECEITA_ARQUIVO ");
                    sql.AppendLine(" WHERE PRA_PRO_PROTOCOLO = '" + _protocolo + "'");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sql);
                    }

                    StringBuilder sql1 = new StringBuilder();
                    sql1.AppendLine(" UPDATE PSC_PROTOCOLO ");
                    sql1.AppendLine(" SET PRO_STATUS = 1 ");
                    sql1.AppendLine(" WHERE PRO_PROTOCOLO = '" + _protocolo + "'");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sql1);
                    }

                    StringBuilder sqlWbs = new StringBuilder();

                    sqlWbs.AppendLine(@"INSERT INTO PSC_RECEITA_ARQUIVO
                                           (PRA_PRO_PROTOCOLO
                                           ,PRA_ARQUIVO
                                           ,PRA_NOME_ARQUIVO
                                           ,pra_status_import)
                                     VALUES ( ");
                    sqlWbs.Append("'" + _protocolo + "'");
                    sqlWbs.Append(" ,'" + _xml + "'");
                    sqlWbs.Append(" ,'" + _protocolo + ".xml'");
                    sqlWbs.Append(" ,1");
                    sqlWbs.Append(" )");

                    using (dHelperSQL c = new dHelperSQL())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlWbs);
                    }

                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
