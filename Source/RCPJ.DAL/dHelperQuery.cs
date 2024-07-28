using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using psc.ApplicationBlocks.DAL;

using psc.Framework;
using psc.Framework.Data;

namespace RCPJ.DAL.Helper
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    /// 


    public class dHelperQuery : DBInteractionBase
    {

        public dHelperQuery()
        {
        }


        public static DataTable ExecuteQuery(string sql)
        {

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql));

            return Dt;
        }


        /// <summary>
        /// Retorna lista de processos numa seção
        /// </summary>
        /// <returns></returns>
        public static DataTable getProcessosSecao(string sqlWhere, bool podeDistribuir)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("PROCESSOS");

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
                             , NOME_UNIDADE
                             , concat(cc.NAT_JURIDICA,' - ',nj.t009_ds_natureza_juridica) DESC_NAT_JURIDICA
                             , concat(cc.ATO,' - ',a.NO_ATO) DESC_ATO
                        FROM
                          controleimagem.central_carga cc
                        inner join t009_natureza_juridica nj on nj.t009_co_natureza_juridica = cc.NAT_JURIDICA
                        LEFT JOIN shared.ato a ON cc.ato = a.CO_ATO
                        WHERE 1 =1 
                            AND SITUACAO in (10)");
                        //alterado em 15/03/2016 pois mudaram o status
                        //AND SITUACAO in (0,1)");

            if (sqlWhere != "")
            {
                Sql.AppendLine(sqlWhere);
            }

            Sql.AppendLine(" ORDER BY DT_ENTRADA DESC  ");

            if (!podeDistribuir)
            {
                Sql.AppendLine(" LIMIT 1");
            }


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public void Sp_processoAlterViabilidade(string pNumRequerimentoAtual, string pNumRequerimentoNovo, string pUsuario, int pAtualizaSocio)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "Sp_processoAlterViabilidade";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("pNumRequerimentoAtual", MySqlDbType.VarChar, 60, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, pNumRequerimentoAtual));
                cmdToExecute.Parameters.Add(new MySqlParameter("pNumRequerimentoNovo", MySqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNumRequerimentoNovo));
                cmdToExecute.Parameters.Add(new MySqlParameter("pUsuario", MySqlDbType.VarChar, 8, ParameterDirection.Input, true, 2, 0, "", DataRowVersion.Proposed, pUsuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("pAtualizaSocio", MySqlDbType.Int32, 11, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pAtualizaSocio));

                if (_mainConnectionIsCreatedLocal)
                {

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
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }



        public static DataTable getEventosFilialBySqPessoa(int sqPessoa)
        {
            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("EventosFilial");

            Sql.AppendLine(@"SELECT 
                                   p.T005_NR_PROTOCOLO protocolo
                                 , A003_CO_EVENTO evento 
                                 , ve.r002_uf UF
                                 , v.T001_SQ_PESSOA sqPpessoa
                                 , v.T001_SQ_PESSOA_PAI sqPpessoaPai 
                                 , pj.T003_UF_ORIGEM UF_Origem
                            FROM
                              t003_pessoa_juridica pj
                            INNER JOIN t001_pessoa ps
                            ON pj.T001_SQ_PESSOA = ps.T001_SQ_PESSOA
                            INNER JOIN r005_protocolo_evento pe
                            ON pe.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            INNER JOIN r001_vinculo V
                            ON V.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            INNER JOIN t005_protocolo p
                            ON p.T001_SQ_PESSOA = V.T001_SQ_PESSOA_PAI
                            INNER JOIN r002_vinculo_endereco ve
                            ON ve.t001_sq_pessoa = pj.T001_SQ_PESSOA
                            WHERE
                              v.A009_CO_CONDICAO = 501 AND v.T001_SQ_PESSOA = " + sqPessoa.ToString());

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static DataTable getEventosFilial(String pProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("EventosFilial");

            Sql.AppendLine(@"SELECT 
                                   p.T005_NR_PROTOCOLO protocolo
                                 , A003_CO_EVENTO evento 
                                 , ve.r002_uf UF
                                 , v.T001_SQ_PESSOA sqPpessoa
                                 , v.T001_SQ_PESSOA_PAI sqPpessoaPai 

                            FROM
                              t003_pessoa_juridica pj
                            INNER JOIN t001_pessoa ps
                            ON pj.T001_SQ_PESSOA = ps.T001_SQ_PESSOA
                            INNER JOIN r005_protocolo_evento pe
                            ON pe.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            INNER JOIN r001_vinculo V
                            ON V.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            INNER JOIN t005_protocolo p
                            ON p.T001_SQ_PESSOA = V.T001_SQ_PESSOA_PAI
                            INNER JOIN r002_vinculo_endereco ve
                            ON ve.t001_sq_pessoa = pj.T001_SQ_PESSOA
                            WHERE
                              v.A009_CO_CONDICAO = 501 AND p.T005_NR_PROTOCOLO = '" + pProtocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        /// <summary>
        /// Obter a data do Sistema buscando no banco de dados
        /// </summary>
        /// <returns></returns>
        public static DateTime DataSistema()
        {

            StringBuilder sql = new StringBuilder();

            sql.Append(" select NOW() as pFecha");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            return DateTime.Parse(Dt.Rows[0]["pFecha"].ToString());
        }

        public static string VerficaNaturezaViabilidade(string natureza, string grupo)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@" select count(*) qtd 
                        from t009_natureza_juridica a
                        where ");
            sql.AppendLine(" a.t009_co_natureza_juridica = '" + natureza + "'");
            sql.AppendLine(" and a.t009_co_grupo = " + grupo);

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            return Dt.Rows[0]["qtd"].ToString();
        }


        /// <summary>
        /// Buscar a descricao do nome logradouro.
        /// </summary>
        /// <param name="codLogradouro">The cod logradouro.</param>
        /// <returns></returns>
        //public static DataTable BuscarDescricaoNomeLogradouro(string codLogradouro)
        //{
        //   // to do
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append(" select t.tlg_clav cod, t.tlg_nome Descricao from tab_cep_logr t ");


        //    sql.Append(" Where  1 = 1 ");
        //    if (codLogradouro != String.Empty)
        //    {
        //        // sql.Append(" and t.tlg_clav = '" + codLogradouro + "' ");
        //        sql.Append(" and TLG_CLAV_SEQ = '" + codLogradouro + "' ");
        //    }

        //    sql.Append(" Order by tlg_nome ");
        //    return DataHelper.GetTable(dalOracle.ExecuteReader(GeneralOracle.ConnectionString(), CommandType.Text, sql.ToString()));
        //}

        public static bool VerificaExisteRequerimento(string pRequerimento)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT Count(*) qtd FROM t005_protocolo Where t005_nr_protocolo = '" + pRequerimento + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            return Int32.Parse(Dt.Rows[0]["qtd"].ToString()) == 1;

        }
        /// <summary>
        /// Encontra o numero do requerimento pelo protocolo Junta
        /// </summary>
        /// <param name="pProtocolo"></param>
        /// <returns></returns>
        public static string GetProtocoloByprotocoloJunta(string pProtocolo)
        {
            StringBuilder sql = new StringBuilder();
            string _retorno = string.Empty;

            sql.AppendLine("SELECT t005_nr_protocolo FROM t005_protocolo Where t005_nr_protocolo_rcpj = '" + pProtocolo + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            if (Dt.Rows.Count > 0)
            {
                _retorno = Dt.Rows[0]["t005_nr_protocolo"].ToString();
            }
            return _retorno;

        }

        public static DataTable GetRequerimento(string pRequerimento, string pViabilidade, string pDBE, string pProtocolo)
        {
            StringBuilder sql = new StringBuilder();
            string _retorno = string.Empty;

            sql.AppendLine(@"SELECT t005_nr_protocolo Requerimento, 
                              t005_nr_protocolo_viabilidade Viabilidade, 
                              t005_nr_dbe DBE, 
                              t005_nr_protocolo_rcpj Protocolo,
                              pp.T001_DS_PESSOA Nome
                            FROM
                              t005_protocolo p 
                              inner join t001_pessoa pp on p.T001_SQ_PESSOA = pp.t001_sq_pessoa 
                            Where fnGetStatusProcesso(p.T005_NR_PROTOCOLO) <>  '9' ");

            if (pRequerimento.Trim() != "")
                sql.AppendLine(" and t005_nr_protocolo = '" + pRequerimento + "'");

            if (pViabilidade.Trim() != "")
                sql.AppendLine(" and t005_nr_protocolo_Viabilidade = '" + pViabilidade + "'");

            if (pDBE.Trim() != "")
                sql.AppendLine(" and t005_nr_DBE = '" + pDBE + "'");

            if (pProtocolo.Trim() != "")
                sql.AppendLine(" and t005_nr_protocolo_rcpj = '" + pProtocolo + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            return Dt;

        }

        public static DataTable CarregaRequerenteComProtocolo(string wProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT pr.T005_NR_PROTOCOLO AS T005_NR_PROTOCOLO
                                 , pf.T002_NR_CPF AS T002_NR_CPF
                                 , v.T001_SQ_PESSOA_PAI AS T001_SQ_PESSOA_PAI
                                 , p.T001_SQ_PESSOA AS T001_SQ_PESSOA
                                 , p.T001_DS_PESSOA AS T001_DS_PESSOA
                                 , p.T001_DDD AS T001_DDD
                                 , p.T001_TEL_1 AS T001_TEL_1
                                 , p.T001_EMAIL AS T001_EMAIL
                                 , pr.T004_NR_CNPJ_ORG_REG AS T004_NR_CNPJ_ORG_REG
                                 , pr.T005_IN_SITUACAO AS T005_IN_SITUACAO 
                            FROM t005_protocolo pr
                            INNER JOIN r001_vinculo v ON pr.T001_SQ_PESSOA = v.T001_SQ_PESSOA_PAI AND v.A009_CO_CONDICAO = 500
                            INNER JOIN t001_pessoa p ON v.T001_SQ_PESSOA = p.T001_SQ_PESSOA
                            INNER JOIN t002_pessoa_fisica pf ON p.T001_SQ_PESSOA = pf.T001_SQ_PESSOA WHERE pr.T005_NR_PROTOCOLO = '" + wProtocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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
        public static DataTable CarregaEmpresaComProtocolo(string wProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("Select * from vw_dados_empresa where t005_nr_protocolo = '" + wProtocolo + "'");

            sql.AppendLine(@"SELECT p.T005_NR_PROTOCOLO                 AS T005_NR_PROTOCOLO
                               , p.T005_NR_PROTOCOLO_VIABILIDADE        AS T005_NR_PROTOCOLO_VIABILIDADE
                               , p.T001_SQ_PESSOA                       AS T001_SQ_PESSOA
                               , ps.T001_NOME_FANTASIA                  AS T001_NOME_FANTASIA
                               , ps.T001_DS_PESSOA                      AS T001_DS_PESSOA
                               , ps.T001_DDD                            AS T001_DDD
                               , ps.T001_TEL_1                          AS T001_TEL_1
                               , ps.T001_EMAIL                          AS T001_EMAIL
                               , e.r002_nr_cep                          AS R002_NR_CEP
                               , e.a005_co_municipio                    AS A005_CO_MUNICIPIO
                               , e.a015_ds_tipo_logradouro              AS A015_DS_TIPO_LOGRADOURO
                               , e.a015_co_tipo_logradouro              AS A015_CO_TIPO_LOGRADOURO
                               , e.r002_ds_logradouro                   AS R002_DS_LOGRADOURO
                               , e.r002_nr_logradouro                   AS R002_NR_LOGRADOURO
                               , e.r002_ds_complemento                  AS R002_DS_COMPLEMENTO
                               , e.r002_ds_bairro                       AS R002_DS_BAIRRO
                               , 
                                 CASE
                                 WHEN isnull (e.r002_uf) THEN
                                   tm.TMU_TUF_UF
                                 ELSE
                                     e.r002_uf
                                 END                                    AS r002_UF
                               , pj.T003_SOCIOS_OBRIGACOES_SOCIAIS      AS T003_SOCIOS_OBRIGACOES_SOCIAIS
                               , pj.T003_VL_CAPITAL_SOCIAL              AS T003_VL_CAPITAL_SOCIAL
                               , pj.T003_VL_CAPITAL_INTEGRALIZADO       AS T003_VL_CAPITAL_INTEGRALIZADO
                               , pj.T003_VL_CAPITAL_NAO_INTEGRALIZADO   AS T003_VL_CAPITAL_NAO_INTEGRALIZADO
                               , evaldate(pj.T003_DATA_LIMITE_INTEGRALIZACAO) AS T003_DATA_LIMITE_INTEGRALIZACAO
                               , pj.T003_DS_CAPITAL_NAO_INTEGRALIZADO   AS T003_DS_CAPITAL_NAO_INTEGRALIZADO
                               , pj.T003_VL_QTD_COTAS                   AS T003_VL_QTD_COTAS
                               , pj.T003_VL_VALOR_COTA                  AS T003_VL_VALOR_COTA
                               , pj.T003_DT_INICIO_ATIVIDADE            AS T003_DT_INICIO_ATIVIDADE
                               , pj.T003_DT_PRAZO_DETERMINADO           AS T003_DT_PRAZO_DETERMINADO
                               , pj.T003_DS_OBJETO_SOCIAL               AS T003_DS_OBJETO_SOCIAL
                               , pj.T003_NR_MATRICULA                   AS T003_NR_MATRICULA
                               , pj.T003_DT_CONSTITUICAO                AS T003_DT_CONSTITUICAO
                               , pj.A006_CO_NATUREZA_JURIDICA           AS A006_CO_NATUREZA_JURIDICA
                               , nj.t009_ds_natureza_juridica           AS A009_DS_NATUREZA_JURIDICA
                               , pj.T003_CO_TIPO_PES_JUR                AS T003_CO_TIPO_PES_JUR
                               , pj.T003_TIPO_ENQUADRAMENTO             AS T003_TIPO_ENQUADRAMENTO
                               , pj.T003_NR_CNPJ                        AS T003_NR_CNPJ
                               , tpj.A018_DS_TIPO_PESSOA_JURIDICA       AS A018_DS_TIPO_PESSOA_JURIDICA
                               , tm.TMU_TUF_UF                          AS TMU_TUF_UF
                               , p.T005_NR_DBE                          AS T005_NR_DBE
                               , p.T005_NR_PROTOCOLO_RCPJ               AS T005_NR_PROTOCOLO_RCPJ
                               , p.T005_FORO                            AS T005_FORO
                               , tm.TMU_NOM_MUN                         AS NOMEMUNICIPIO
                               , p.T005_NR_ALTERACAO                    AS T005_NR_ALTERACAO
                               , pj.T003_IND_FILIAL_SEDE_FORA           AS T003_IND_FILIAL_SEDE_FORA
                               , p.T004_NR_CNPJ_ORG_REG                 AS T004_NR_CNPJ_ORG_REG
                               , p.T005_CO_ATO                          AS T005_CO_ATO
                               , p.T005_NR_PROTOCOLO_ENQUADRAMENTO      AS T005_NR_PROTOCOLO_ENQUADRAMENTO
                               , p.T005_UF_ORIGEM                       AS T005_UF_ORIGEM
                               , pj.T003_IND_UNIPESSOAL                 AS T003_IND_UNIPESSOAL
                               , p.T005_DATA_ASSINATURA                 AS T005_DATA_ASSINATURA
                               , p.T005_LOCAL_ASSINATURA                AS T005_LOCAL_ASSINATURA
                               , p.T005_DT_AVERBACAO                    AS T005_DT_AVERBACAO
                               , pj.A007_CO_SITUACAO                    AS A007_CO_SITUACAO
                               , p.T005_TIPO_DOC_REQUE                  AS T005_TIPO_DOC_REQUE
                               , pj.T003_MOEDA_CORRENTE                 AS T003_MOEDA_CORRENTE
                               , p.T005_IN_TRANSF_UNIPESSOAL            AS T005_IN_TRANSF_UNIPESSOAL
                               , p.T005_IN_DBE_CARREGADO                AS T005_IN_DBE_CARREGADO
                               , pj.T003_IN_REDUCAO_CAPITAL             AS T003_IN_REDUCAO_CAPITAL
                               , pj.T003_DS_REDUCAO_CAPITAL             AS T003_DS_REDUCAO_CAPITAL
                               , p.T005_CO_UNIDADE_ENTREGA              AS T005_CO_UNIDADE_ENTREGA
                               , pj.T003_DS_SITUACAO                    AS T003_DS_SITUACAO
                               , p.T005_IN_CLAUSILA_ADM
                               , p.T005_TX_CLAUSULA_ADM
                               , p.T005_NR_DAE
                               , p.T005_IN_ALT_ADMIN
                               , p.T005_PROTOCOLO_ORGAO_ORIGEM
                               , pj.T003_IND_INTEGRALIZANDO_CAP
                               , PJ.T003_DT_TERMINO_ATIV
                               , pj.T003_IND_SPE
                               , pj.T003_IND_MATRIZ  
                               , p.T005_TX_CLAUSULA_ARBITRAL
                               , pj.T003_IN_END_ESTAB
                               , pj.T003_IN_CONSOLIDACAO
                               , pj.T003_IN_REATIVACAO
                               , pj.T003_IN_RERATIFICACAO
                               , p.T005_TX_CLAUSULA_RESTITUICAO
                               , pj.T003_IPTU
                               , pj.T003_AREA_UTILIZADA
                               , p.T005_TIP_REGISTRO_VIAB
                               , p.T005_CODMUNICIPIOINSCMUNICIPAL
                          FROM  t005_protocolo p
                            INNER JOIN r002_vinculo_endereco e
                              ON p.T001_SQ_PESSOA = e.t001_sq_pessoa
                            INNER JOIN t001_pessoa ps
                              ON p.T001_SQ_PESSOA = ps.T001_SQ_PESSOA
                            INNER JOIN t003_pessoa_juridica pj
                              ON ps.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            LEFT JOIN tab_munic tm
                              ON e.a005_co_municipio = tm.TMU_COD_MUN
                            LEFT JOIN t009_natureza_juridica nj
                              ON nj.t009_co_natureza_juridica = pj.A006_CO_NATUREZA_JURIDICA
                            LEFT JOIN a018_tipo_pessoa_juridica tpj
                            ON tpj.A018_CO_TIPO_PESSOA_JURIDICA = pj.T003_CO_TIPO_PES_JUR

                            Where t005_nr_protocolo = '" + wProtocolo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static DataTable getTipoRepresentante(int _codigo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select * from t015_tipo_assistido_representado where t015_co_tipo_assistido_representado = " + _codigo);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
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

        public static DataTable CarregaStatusDoProtocolo(string wProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Select * from t011_protocolo_status where t005_nr_protocolo = '" + wProtocolo + "' order by t011_dt_situacao DESC");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static DataTable CarregaCnaeComProtocolo(int wPessoa)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"  SELECT pj.T001_SQ_PESSOA AS T001_SQ_PESSOA
                                      , a.A001_CO_ATIVIDADE AS A001_CO_ATIVIDADE
                                      , a.R004_IN_PRINCIPAL_SECUNDARIO AS R004_IN_PRINCIPAL_SECUNDARIO
                                      , a.R004_EXERCIDA AS R004_EXERCIDA
                                      , ae.TAE_DESC AS TAD_DESC_ATIVIDADE
                                      , pj.T003_DS_OBJETO_SOCIAL AS T003_DS_OBJETO_SOCIAL 
                                FROM r004_atuacao a
                                     INNER JOIN tab_actv_econ ae
                                       ON ae.TAE_COD_ACTVD = a.A001_CO_ATIVIDADE
                                     INNER JOIN t003_pessoa_juridica pj
                                       ON a.T001_SQ_PESSOA = pj.T001_SQ_PESSOA WHERE PJ.t001_sq_pessoa = " + wPessoa);
            sql.AppendLine(@"   ORDER BY
                                  a.R004_IN_PRINCIPAL_SECUNDARIO ASC ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static string BuscaDescricaoCBO(string _codigo)
        {
            
             MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"  SELECT tae_desc FROM tab_cbo_econ ae WHERE ae.tae_cod_actvd = " + _codigo);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["tae_desc"].ToString();
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
                adapter.Dispose();
            }
        }
        public static DataTable CarregaCBOComProtocolo(int wPessoa)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"  SELECT a.T001_SQ_PESSOA                 AS T001_SQ_PESSOA
                                      , a.A001_CO_ATIVIDADE             AS A001_CO_ATIVIDADE
                                      , a.R004_IN_PRINCIPAL_SECUNDARIO  AS R004_IN_PRINCIPAL_SECUNDARIO
                                      , a.R004_EXERCIDA                 AS R004_EXERCIDA
                                      , ae.TAE_DESC                     AS TAD_DESC_ATIVIDADE
                                FROM r004_atuacao a
                                     INNER JOIN tab_cbo_econ ae
                                       ON ae.TAE_COD_ACTVD = a.A001_CO_ATIVIDADE
                                WHERE a.T001_SQ_PESSOA = " + wPessoa);
            sql.AppendLine(@"   ORDER BY
                                  a.R004_IN_PRINCIPAL_SECUNDARIO ASC ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static DataTable QueryAdministradores(int wPessoa)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"SELECT p.T001_DS_PESSOA
                                    , pf.T002_NR_CPF
                            FROM    r001_vinculo v
                                    INNER JOIN t001_pessoa p
                                        ON v.T001_SQ_PESSOA = p.T001_SQ_PESSOA
                                    INNER JOIN t002_pessoa_fisica pf
                                        ON pf.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                             WHERE ");
            sql.AppendLine(" v.T001_SQ_PESSOA_PAI = " + wPessoa);
            sql.AppendLine(" AND (v.A009_CO_CONDICAO < 100 ");
            sql.AppendLine(" AND v.T001_SQ_PESSOA_REP_LEGAL > 0) ");
            sql.AppendLine(" OR (v.T001_SQ_PESSOA_PAI = " + wPessoa);
            sql.AppendLine(" AND v.A009_CO_CONDICAO = 5)");
            sql.AppendLine(" OR (v.T001_SQ_PESSOA_PAI = " + wPessoa);
            sql.AppendLine(" AND v.A009_CO_CONDICAO = 50) ");



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static DataTable CarregaRepresentantes(int wPessoa)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();



            //sql.AppendLine("Select * from vw_dcnt_repr_socios where sq_pai = " + wPessoa);

            sql.AppendLine(@"SELECT t2.T002_NR_CPF AS CPF_RESPONSAVEL
                                     , pr.T005_NR_PROTOCOLO_VIABILIDADE         AS cod_protocolo
                                     , pr.T005_NR_PROTOCOLO                     AS reque_protocolo
                                     , t1.T001_DS_PESSOA                        AS NOME_RESPONSAVEL
                                     , t1.T001_SQ_PESSOA                        AS SQ_REPRESENTANTE
                                     , r1.T001_SQ_PESSOA_PAI                    AS SQ_PAI
                                     , t1.T001_DDD                              AS DDD
                                     , t1.T001_TEL_1                            AS TELEFONE
                                     , t1.T001_EMAIL                            AS EMAIL
                                     , ta.T015_co_tipo_assistido_representado   AS tipo_representante
                                     , ta.T015_ds_tipo_assistido_representado   AS ds_tipo_representante
                                     , t1.T001_IN_TIPO_PESSOA                   AS TIPO_PESSOA
                                     , t2.A004_CO_PAIS                          AS CO_NACIONALIDADE
                                     , t2.T002_ANALFABETO                       AS ANALFABETO
                                     , t2.T002_DS_NACIONALIDADE                 AS NACIONALIDADE
                                     , t2.T002_TIPO_VISTO                       AS TIPO_VISTO
                                     , t2.T002_DT_EMISSAO_VISTO                 AS EMISSAO_VISTO
                                     , t2.T002_DT_VALIDADE_VISTO                AS VALIDADE_VISTO
                                     , t2.A004_CO_UF_NATURALIDADE               AS NATURALIDADE
                                     , a12.A012_CO_ESTADO_CIVIL                 AS ESTADO_CIVIL
                                     , a12.A012_DS_ESTADO_CIVIL                 AS DS_ESTADO_CIVIL
                                     , a13.A013_CO_REGIME_BENS                  AS REGIME_BENS
                                     , a13.A013_DS_REGIME_BENS                  AS DS_REGIME_BENS
                                     , t2.A014_CO_EMANCIPACAO                   AS CO_EMANCIPACAO
                                     , e.A014_DS_EMANCIPACAO                    AS DS_EMANCIPACAO
                                     , t2.T002_DT_NASCIMENTO                    AS DATA_NASC
                                     , a10.A010_DS_TIPO_DOCUMENTO               AS TIPO_DOC_IDENT
                                     , a10.A010_CO_TIPO_DOCUMENTO               AS CO_TIPO_DOC_IDENT
                                     , t2.T002_NR_DOCUMENTO                     AS NO_DOC_IDENT
                                     , t2.T002_DS_EMISSOR_DOCUMENTO             AS ORGAO_EXPED
                                     , a15.A015_CO_TIPO_LOGRADOURO              AS TIPO_LOGRADOURO
                                     , r2.a015_ds_tipo_logradouro               AS DS_TIPO_LOGRADOURO
                                     , r2.r002_ds_logradouro                    AS LOGRADOURO
                                     , r2.r002_nr_logradouro                    AS NO_LOGRADOURO
                                     , r2.r002_ds_bairro                        AS BAIRRO
                                     , a5.TMU_COD_MUN                           AS MUNICIPIO
                                     , a5.TMU_NOM_MUN                           AS DS_MUNICIPIO
                                     , r2.r002_nr_cep                           AS CEP
                                     , a5.TMU_TUF_UF                            AS UF
                                     , r2.a004_co_pais                          AS PAIS
                                     , a20.A020_CO_PROFISSAO                    AS PROFISSAO
                                     , t2.T002_DS_PROFISSAO                     AS DS_PROFISSAO
                                     , t2.A004_UF_ORG_EXPED                     AS UF_ORGAO_EXPED
                                     , t2.T002_DS_ORGAO_EXPEDIDOR               AS T002_DS_ORGAO_EXPEDIDOR
                                     , t2.T002_IN_SEXO                          AS SEXO
                                     , t2.T002_NR_QTD_COTAS                     AS QTD_COTAS
                                     , a9.A009_CO_CONDICAO                      AS ADM
                                     , a9.A009_DS_CONDICAO                      AS DS_CONDICAO
                                     , r2.r002_ds_complemento                   AS COMP_LOGRADOURO
                                     , r1.A009_CO_CONDICAO                      AS QUALIFICACAO
                                     , r1.R001_IN_SITUACAO                      AS SITUACAO
                                     , r1.R001_DS_CARGO_DIRECAO                 AS DS_QUALIFICACAO
                                     , r1.T001_SQ_PESSOA_REP_LEGAL              AS REP_LEGAL
                                     , r1.R001_ACAO                             AS R001_ACAO
                                     , r1.R001_DT_ENTRADA_VINCULO               AS R001_DT_ENTRADA_VINCULO
                                     , r1.R001_DT_INICIO_MANDATO                AS R001_DT_INICIO_MANDATO 
                                FROM    t002_pessoa_fisica t2
                                        JOIN r002_vinculo_endereco r2
                                            ON t2.T001_SQ_PESSOA = r2.t001_sq_pessoa
                                        LEFT JOIN r001_vinculo r1
                                            ON t2.T001_SQ_PESSOA = r1.T001_SQ_PESSOA
                                        LEFT JOIN r001_vinculo r3
                                            ON r1.T001_SQ_PESSOA_PAI = r3.T001_SQ_PESSOA
                                        LEFT JOIN t005_protocolo pr
                                            ON r3.T001_SQ_PESSOA_PAI = pr.T001_SQ_PESSOA
                                        LEFT JOIN t001_pessoa t1
                                            ON t1.T001_SQ_PESSOA = r1.T001_SQ_PESSOA
                                        LEFT JOIN a009_condicao a9
                                            ON a9.A009_CO_CONDICAO = r1.A009_CO_CONDICAO
                                        LEFT JOIN TAB_MUNIC a5
                                            ON r2.a005_co_municipio = a5.TMU_COD_MUN
                                        LEFT JOIN a015_tipo_logradouro a15
                                            ON r2.a015_co_tipo_logradouro = a15.A015_CO_TIPO_LOGRADOURO
                                        LEFT JOIN a020_profissao a20
                                            ON t2.A020_CO_PROFISSAO = a20.A020_CO_PROFISSAO
                                        LEFT JOIN a017_pais a17
                                            ON t2.A004_CO_PAIS = a17.A017_CO_PAIS
                                         LEFT JOIN a010_tipo_documento a10
                                            ON t2.A010_CO_TIPO_DOCUMENTO = a10.A010_CO_TIPO_DOCUMENTO
                                        LEFT JOIN a013_regime_bens a13
                                            ON t2.A013_CO_REGIME_BENS = a13.A013_CO_REGIME_BENS
                                        LEFT JOIN a012_estado_civil a12
                                            ON t2.A012_CO_ESTADO_CIVIL = a12.A012_CO_ESTADO_CIVIL
                                        LEFT JOIN T015_tipo_assistido_representado ta
                                            ON ta.T015_co_tipo_assistido_representado = r1.A030_CO_TIPO_ASSISTIDO
                                        LEFT JOIN a014_emancipacao e
                                            ON e.A014_CO_EMANCIPACAO = t2.A014_CO_EMANCIPACAO 
                                WHERE   r1.A009_CO_CONDICAO = 504
                                        AND pr.T005_NR_PROTOCOLO IS NOT NULL");
            sql.AppendLine("            AND r1.T001_SQ_PESSOA_PAI = " + wPessoa);




            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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
        public static DataTable CarregaFiliaisComProtocolo(int wPessoa)
        {

            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"SELECT pj.T001_SQ_PESSOA           AS t001_sq_pessoa
                                 , v.T001_SQ_PESSOA_PAI         AS T001_SQ_PESSOA_PAI
                                 , p.T005_NR_PROTOCOLO          AS COD_PROTOCOLO
                                 , p.T005_NR_PROTOCOLO          AS REQUE_PROTOCOLO
                                 , r2.a015_ds_tipo_logradouro   AS tipo_logradouro
                                 , r2.r002_ds_logradouro        AS logradouro
                                 , r2.r002_ds_bairro            AS bairro
                                 , r2.r002_nr_cep               AS cep
                                 , r2.r002_nr_logradouro        AS num_logradouro
                                 , r2.r002_ds_complemento       AS comp_logradouro
                                 , m.TMU_TUF_UF                 AS uf
                                 , r2.r002_uf                   AS r002_UF
                                 , r2.a005_co_municipio         AS tmu_cod_mun
                                 , m.TMU_NOM_MUN                AS tmu_des_mun
                                 , pj.t003_PROT_VIAB            AS NUM_VIABILIDADE
                                 , pj.t003_DBE                  AS t003_DBE
                                 , v.R001_ACAO                  AS R001_ACAO
                                 , pj.T003_VL_CAPITAL_SOCIAL    AS T003_VL_CAPITAL_SOCIAL
                                 , pj.T003_NR_MATRICULA         AS T003_NR_MATRICULA
                                 , pj.T003_NR_CNPJ              AS T003_NR_CNPJ
                                 , v.R001_ORDEM_FILIAL_CONTRATO AS R001_ORDEM_FILIAL_CONTRATO
                                 , pj.T003_DS_OBJETO_SOCIAL     AS T003_DS_OBJETO_SOCIAL
                                 , pj.T003_DT_INICIO_ATIVIDADE  AS T003_DT_INICIO_ATIVIDADE
                                 , pj.T003_IND_CNAE_DESTACADA   AS T003_IND_CNAE_DESTACADA
                                 , pj.T003_UF_ORIGEM            AS T003_UF_ORIGEM
                                 , pj.T003_IPTU
                                 , pj.T003_AREA_UTILIZADA   
                                 , pp.T001_DDD
                                 , pp.T001_TEL_1
                            FROM r001_vinculo v
                                  INNER JOIN T001_PESSOA pp
                                    ON pp.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                  INNER JOIN t003_pessoa_juridica pj
                                    ON v.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                                   INNER JOIN t005_protocolo p
                                    ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA_PAI
                                   LEFT JOIN r002_vinculo_endereco r2
                                    ON pj.T001_SQ_PESSOA = r2.t001_sq_pessoa
                                   LEFT JOIN TAB_MUNIC m
                                    ON m.TMU_COD_MUN = r2.a005_co_municipio
                                   LEFT JOIN a015_tipo_logradouro tl
                                    ON r2.a015_co_tipo_logradouro = tl.A015_CO_TIPO_LOGRADOURO
                            WHERE v.A009_CO_CONDICAO = 501");

            sql.AppendLine(" And v.T001_SQ_PESSOA_PAI = " + wPessoa + " order by v.R001_ORDEM_FILIAL_CONTRATO");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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
        public static DataTable CarregaSocioProtocolo(string wProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();

            //sql.AppendLine("Select * from vw_dcnt_prot_socios where reque_protocolo = '" + wProtocolo + "'");

            sql.AppendLine(@"SELECT t2.T002_NR_CPF                      AS CPF_CNPJ_SOCIO
                                     , t5.T005_NR_PROTOCOLO_VIABILIDADE AS COD_PROTOCOLO
                                     , t1.T001_DS_PESSOA                AS NOME_SOCIO
                                     , t1.T001_SQ_PESSOA                AS SQ_SOCIO
                                     , t1.T001_IN_TIPO_PESSOA           AS TIPO_PESSOA
                                     , t1.T001_DDD                      AS T001_DDD
                                     , t1.T001_TEL_1                    AS T001_TEL_1
                                     , t1.T001_EMAIL                    AS T001_EMAIL
                                     , t2.T002_NOME_PAI                 AS NOME_PAI
                                     , t2.A014_CO_EMANCIPACAO           AS TIPO_EMANCIPACAO
                                     , t2.T002_NOME_MAE                 AS NOME_MAE
                                     , t2.A004_CO_PAIS                  AS CO_NACIONALIDADE
                                     , t2.T002_DS_NACIONALIDADE         AS NACIONALIDADE
                                     , t2.A004_CO_UF_NATURALIDADE       AS NATURALIDADE
                                     , t2.T002_JUSTIFICATIVA_VISTO      AS JUSTIFICATIVA_VISTO
                                     , a12.A012_CO_ESTADO_CIVIL         AS ESTADO_CIVIL
                                     , a12.A012_DS_ESTADO_CIVIL         AS DS_ESTADO_CIVIL
                                     , a13.A013_CO_REGIME_BENS          AS REGIME_BENS
                                     , a13.A013_DS_REGIME_BENS          AS DS_REGIME_BENS
                                     , t2.A014_CO_EMANCIPACAO           AS CO_EMANCIPACAO
                                     , e.A014_DS_EMANCIPACAO            AS DS_EMANCIPACAO
                                     , t2.T002_DT_NASCIMENTO            AS DATA_NASC
                                     , a10.A010_DS_TIPO_DOCUMENTO       AS TIPO_DOC_IDENT
                                     , a10.A010_CO_TIPO_DOCUMENTO       AS CO_TIPO_DOC_IDENT
                                     , t2.T002_NR_DOCUMENTO             AS NO_DOC_IDENT
                                     , t2.T002_DS_EMISSOR_DOCUMENTO     AS ORGAO_EXPED
                                     , t2.T002_IN_SEXO                  AS SEXO
                                     , t2.T002_ANALFABETO               AS ANALFABETO
                                     , t2.T002_CAPITAL_INTEGRALIZADO    AS CAPITAL_INTEGRALIZADO
                                     , t2.T002_CAPITAL_A_INTEGRALIZAR   AS CAPITAL_A_INTEGRALIZAR
                                     , t2.T002_DATA_FINAL_INTEGRALIZACAO AS DATA_FINAL_INTEGRALIZACAO
                                     , a17a.A017_CO_PAIS                AS pais_residencia
                                     , a17a.A017_DS_PAIS                AS ds_pais_residencia
                                     , a15.A015_CO_TIPO_LOGRADOURO      AS TIPO_LOGRADOURO
                                     , r2.a015_ds_tipo_logradouro       AS DS_TIPO_LOGRADOURO
                                     , r2.r002_ds_logradouro            AS LOGRADOURO
                                     , r2.r002_nr_logradouro            AS NO_LOGRADOURO
                                     , r2.r002_ds_bairro                AS BAIRRO
                                     , a5.TMU_COD_MUN                   AS MUNICIPIO
                                     , a5.TMU_NOM_MUN                   AS DS_MUNICIPIO
                                     , r2.r002_nr_cep                   AS CEP
                                     , a5.TMU_TUF_UF                    AS UF
                                     , a20.A020_CO_PROFISSAO            AS PROFISSAO
                                     , t2.T002_DS_PROFISSAO             AS DS_PROFISSAO
                                     , t2.A004_UF_ORG_EXPED             AS UF_ORGAO_EXPED
                                     , t2.T002_NR_QTD_COTAS             AS QTD_COTAS
                                     , t2.T002_APORTE_SOCIO             AS APORTE_SOCIO
                                     , t2.T002_TIPO_VISTO               AS TIPO_VISTO
                                     , t2.T002_DT_EMISSAO_VISTO         AS EMISSAO_VISTO
                                     , t2.T002_DT_VALIDADE_VISTO        AS VALIDADE_VISTO
                                     , r1.T001_SQ_PESSOA_REP_LEGAL      AS ADM
                                     , r2.r002_ds_complemento           AS COMPL_LOGRADOURO
                                     , r1.A009_CO_CONDICAO              AS QUALIFICACAO
                                     , a9.A009_DS_CONDICAO              AS DS_QUALIFICACAO
                                     , r1.R001_IN_FUNDADOR              AS FUNDADOR
                                     , r1.R001_DT_INICIO_MANDATO        AS DT_INICIO_MANDATO
                                     , r1.R001_DT_TERMINO_MANDATO       AS DT_TERMINO_MANDATO
                                     , r1.R001_DS_CARGO_DIRECAO         AS DS_CARGO_DIRECAO
                                     , r1.R001_IN_SITUACAO              AS R001_IN_SITUACAO
                                     , t5.T005_NR_PROTOCOLO             AS REQUE_PROTOCOLO
                                     , pj.A006_CO_NATUREZA_JURIDICA     AS A006_CO_NATUREZA_JURIDICA
                                     , r1.R001_ACAO                     AS R001_ACAO
                                     , t2.T002_DT_OBITO                 AS T002_DT_OBITO
                                     , r1.R001_DT_SAIDA_VINCULO         AS R001_DT_SAIDA_VINCULO
                                     , t2.T002_NIRE                     AS T002_NIRE
                                     , t2.T002_DS_ORGAO_EXPEDIDOR       AS T002_DS_ORGAO_EXPEDIDOR
                                     , t2.T002_CPF_OUTORGANTE           AS T002_CPF_OUTORGANTE
                                     , t2.T002_DS_OUTORGANTE            AS T002_DS_OUTORGANTE
                                     , t2.T002_IN_ADM_ISOLADAMENTE      AS T002_IN_ADM_ISOLADAMENTE
                                     , t2.T002_IN_ADM_CONJUNTAMENTE     AS T002_IN_ADM_CONJUNTAMENTE
                                     , t2.T002_IN_ADM_TODOS             AS T002_IN_ADM_TODOS
                                     , t2.T002_TIP_ORGAO_REGISTRO       AS T002_TIP_ORGAO_REGISTRO
                                     , t2.T002_IN_SIARCO                AS T002_IN_SIARCO
                                     , t2.T002_IN_DIV_DBE               AS T002_IN_DIV_DBE
                                     , t2.T002_TIPO_ACAO                AS T002_TIPO_ACAO
                                     , t2.T002_PERC_CAPITAL             AS T002_PERC_CAPITAL
                                     , t2.T002_DT_SAIDA_ADM             AS T002_DT_SAIDA_ADM 
                                     , t2.T002_IN_RESP_LIVRO            AS T002_IN_RESP_LIVRO
                                     , t2.T002_IN_RESP_ATIVO_PASSIVO    AS T002_IN_RESP_ATIVO_PASSIVO
                                     , t2.T002_CO_ESCOLARIDADE          AS T002_CO_ESCOLARIDADE
                                FROM  t002_pessoa_fisica t2
                                      JOIN r002_vinculo_endereco r2
                                        ON t2.T001_SQ_PESSOA = r2.t001_sq_pessoa
                                      JOIN r001_vinculo r1
                                        ON t2.T001_SQ_PESSOA = r1.T001_SQ_PESSOA
                                      JOIN t001_pessoa t1
                                        ON t1.T001_SQ_PESSOA = r1.T001_SQ_PESSOA
                                      JOIN a009_condicao a9
                                        ON a9.A009_CO_CONDICAO = r1.A009_CO_CONDICAO
                                      JOIN t005_protocolo t5
                                        ON r1.T001_SQ_PESSOA_PAI = t5.T001_SQ_PESSOA
                                      LEFT JOIN tab_munic a5
                                        ON r2.a005_co_municipio = a5.TMU_COD_MUN
                                      LEFT JOIN a015_tipo_logradouro a15
                                        ON r2.a015_co_tipo_logradouro = a15.A015_CO_TIPO_LOGRADOURO
                                      LEFT JOIN a020_profissao a20
                                        ON t2.A020_CO_PROFISSAO = a20.A020_CO_PROFISSAO
                                      LEFT JOIN a017_pais a17
                                        ON t2.A004_CO_PAIS = a17.A017_CO_PAIS
                                      LEFT JOIN a017_pais a17a
                                        ON r2.a004_co_pais = a17a.A017_CO_PAIS
                                      LEFT JOIN a010_tipo_documento a10
                                        ON t2.A010_CO_TIPO_DOCUMENTO = a10.A010_CO_TIPO_DOCUMENTO
                                      LEFT JOIN a013_regime_bens a13
                                        ON t2.A013_CO_REGIME_BENS = a13.A013_CO_REGIME_BENS
                                      LEFT JOIN a012_estado_civil a12
                                        ON t2.A012_CO_ESTADO_CIVIL = a12.A012_CO_ESTADO_CIVIL
                                      LEFT JOIN a014_emancipacao e
                                        ON e.A014_CO_EMANCIPACAO = t2.A014_CO_EMANCIPACAO
                                      LEFT JOIN t003_pessoa_juridica pj
                                        ON pj.T001_SQ_PESSOA = t5.T001_SQ_PESSOA 
                                WHERE r1.A009_CO_CONDICAO <> 500
                                      AND r1.A009_CO_CONDICAO <> 503 ");

            sql.AppendLine(" AND t5.T005_NR_PROTOCOLO = '" + wProtocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

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

        public static DataTable CarregaSocioProtocoloPuro(string wProtocolo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sqlAux = new StringBuilder();
            sqlAux.AppendLine("Select ");
            sqlAux.AppendLine("p.T001_SQ_PESSOA , ");
            sqlAux.AppendLine(" p.T001_DS_PESSOA as NOME, ");
            sqlAux.AppendLine(" pf.T002_NR_CPF as CPFCNPJ, ");
            sqlAux.AppendLine(" pr.T005_NR_PROTOCOLO ");
            sqlAux.AppendLine(" from ");
            sqlAux.AppendLine(" t001_pessoa as p ");
            sqlAux.AppendLine(" inner join r001_vinculo as v ");
            sqlAux.AppendLine(" on p.T001_SQ_PESSOA = v.T001_SQ_PESSOA ");
            sqlAux.AppendLine(" inner join t002_pessoa_fisica as pf ");
            sqlAux.AppendLine(" on v.T001_SQ_PESSOA = pf.t001_sq_pessoa ");
            sqlAux.AppendLine(" inner join t005_protocolo as pr ");
            sqlAux.AppendLine(" on v.T001_SQ_PESSOA_PAI = pr.T001_SQ_PESSOA ");
            sqlAux.AppendLine(" where v.A009_CO_CONDICAO <> 500 and pr.t005_nr_protocolo = '" + wProtocolo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sqlAux.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public static string GetNomeEmpresaByRequerimento(string NumRequerimento)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("T005_Protocolo");
                Sql.AppendLine(" SELECT e.T001_DS_PESSOA Nome_Empresa");
                Sql.AppendLine(" FROM t005_protocolo p ");
                Sql.AppendLine(" INNER JOIN t001_pessoa e ");
                Sql.AppendLine(" ON p.T001_SQ_PESSOA = e.T001_SQ_PESSOA ");
                Sql.AppendLine(" Where ");
                Sql.AppendLine(" p.T005_NR_PROTOCOLO = '" + NumRequerimento + "' ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0]["nome_Empresa"].ToString() != "")
                            return toReturn.Rows[0]["nome_Empresa"].ToString();
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

        public static string getNomeOrgaoRegistro(string CNPJOrgao)
        {
            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("OrgaoRegistro");
                Sql.AppendLine(@" SELECT o.T004_DS_ORG_REG
                                    FROM
                                        t004_orgao_registro o
                                    WHERE
                                        o.T004_NR_CNPJ_ORG_REG = '" + CNPJOrgao + @"' ");


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        if (toReturn.Rows[0][0].ToString() != "")
                            return toReturn.Rows[0][0].ToString();
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
        public static DataTable CarregaExigencias(string NumRequerimento)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"
            SELECT  e.T012_CO_EXIGENCIA AS 'codexigencia'
                    , e.T012_DS_EXIGENCIA AS 'descricao'
                    , e.T005_NR_PROTOCOLO AS 'protocolorequerimento'
                    
            FROM    t012_protocolo_exigencias e
            WHERE
            
            e.T005_NR_PROTOCOLO = '" + NumRequerimento
            + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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
        public static DataTable CarregaExigenciasOutras(string NumRequerimento)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"
            SELECT      p.T016_SQ_EXIGENCIA as 'CodExigencia'
                        , p.T004_NR_CNPJ_ORG_REG
                        , p.T005_NR_PROTOCOLO as 'ProtocoloRequerimento'
                        , p.T016_DS_EXIGENCIA as 'Descricao'
                        , p.T016_DS_FUNDAMENTO_LEGAL as 'FundamentoLegal'
                        , p.T016_USUARIO
                        , p.T016_DT_INCLUSAO
                        , p.T016_DS_GRUPO as 'GRUPO'
                FROM
                  t016_protocolo_exigencias_outras p
                WHERE
            p.T005_NR_PROTOCOLO = '" + NumRequerimento + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
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

        public DataTable CarregarCombo(string Tabela, string Selecionador, object Parametro, string Ordem)
        {
            string wSql;
            DataTable dt = new DataTable();
            if (Selecionador.Length != 0)
            {
                if (Parametro.ToString() == string.Empty) { return dt; }
                //if (int.parse = int)
                // {wSql = "Select * from " + Tabela + " where " + Selecionador + " = " + Parametro + " order by " + Ordem + "";}
                //else 
                wSql = "Select * from " + Tabela + " where " + Selecionador + " = '" + Parametro + "' order by " + Ordem + "";
            }
            // }
            else
            {
                wSql = "Select * from " + Tabela + " order by " + Ordem + "";
            }

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = wSql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable();
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
            //try
            //{
            //    //SqlConnection conn = new SqlConnection("Server=10.11.0.110;User ID=regin;Password=pscs2009rj;Database=rj_portal;");
            //    conn.Open();
            //    SqlCommand cmdSql = new SqlCommand();
            //    cmdSql.Connection = conn;
            //    cmdSql.CommandText = wSql.ToString();
            //    cmdSql.CommandType = CommandType.Text;
            //    SqlDataAdapter adp = new SqlDataAdapter(cmdSql);
            //    adp.Fill(dt);
            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //}
            //return toReturn;
        }
        public DataTable CarregaTabelaGenerica(int TipoTab)
        {

            string wSql = "Select tge_cod_tip_tab, tge_nomb_desc, tge_id_tabela from TAB_GENERICA where tge_tip_tab = " + TipoTab + " and TGE_COD_TIP_TAB > 0 order by TGE_COD_TIP_TAB";
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = wSql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable();
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

        #region Busca endereco por CEP
        public static StringBuilder EnderecoQueryStringSelect(string _tLG_CEP8)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select ");
            sql.Append(" tlg_clav_seq CodigoLogradouro");
            sql.Append("      ,tlg_tti_clav TipoLogradouro");
            sql.Append("      ,tlg_nome NomeLogradouro");
            sql.Append("      ,ct.tti_nome TipoLogradouroDesc ");
            sql.Append("      ,tlg_compl Complemento");
            sql.Append("      ,tlg_tba_clav CodigoBairro");
            sql.Append("      ,tba_exts NomeBairro");
            sql.Append("      ,tlg_tba_tmu_cod_mun CodigoMunicipio");
            sql.Append("      ,tmu_nom_mun NomeMunicipio");
            sql.Append("      ,tlg_tba_tmu_tuf_uf UF");
            sql.Append("      ,tlg_cep8 CEP");
            sql.Append("      ,tlg_origem CodigoOrigem");
            sql.Append("      ,case when tlg_origem = 1 then 'CORREIOS' else 'JUNTA' end Origem");
            sql.Append("      ,tlg_clav CodigoLogradouroOrigem");
            sql.Append("  from tab_cep_logr, tab_cep_bair, tab_munic, tab_cep_tipo ct ");
            sql.Append("  where tlg_tba_clav = tba_clav  ");
            sql.Append("  And   ct.tti_clav = tlg_tti_clav ");
            sql.Append("  and tlg_tba_tmu_tuf_uf = tba_tmu_tuf_uf ");
            sql.Append("  and tlg_tba_tmu_cod_mun = tba_tmu_cod_mun  ");
            sql.Append("  and tba_tmu_cod_mun = tmu_cod_mun ");
            sql.Append("  and tba_tmu_tuf_uf = tmu_tuf_uf ");

            if (_tLG_CEP8 != "")
            {
                sql.Append("  and tlg_cep8 like '%" + _tLG_CEP8 + "%'");
            }
            return sql;
        }
        public static DataTable EnderecoQuery(string _tLG_CEP8)
        {
            StringBuilder sql = EnderecoQueryStringSelect(_tLG_CEP8);
            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
        }
        private DataTable EnderecoQuery2(string _tLG_NOME, string _tLG_TBA_CLAV, string _tLG_CEP8, string _tLG_UF, string _tLG_TTI_CLAV, decimal _tLG_TMU_COD_MUN)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "TAB_CEP_LOGR_Query";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("TAB_CEP_LOGR");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //cmdToExecute.Parameters.Add(new MySqlParameter("P_Cursor", MySqlDbType.Udt, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_NOME", MySqlDbType.VarChar, 60, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _tLG_NOME));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_TBA_CLAV", MySqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tLG_TBA_CLAV));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_CEP8", MySqlDbType.VarChar, 8, ParameterDirection.Input, true, 2, 0, "", DataRowVersion.Proposed, _tLG_CEP8));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_UF", MySqlDbType.VarChar, 2, ParameterDirection.Input, true, 2, 0, "", DataRowVersion.Proposed, _tLG_UF));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_TTI_CLAV", MySqlDbType.VarChar, 30, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tLG_TTI_CLAV));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_TMU_COD_MUN", MySqlDbType.Int32, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tLG_TMU_COD_MUN));
                cmdToExecute.Parameters.Add(new MySqlParameter("V_TLG_CLAV_SEQ", MySqlDbType.Int32, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Int32.MinValue));

                if (_mainConnectionIsCreatedLocal)
                {

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
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }
        #endregion

        #region Busca Descrição
        public static string BuscarDescricaoLogradouroString(string Type)
        {
            DataTable Dt = BuscarDescricaoLogradouro(Type);
            return Dt.Rows[0]["Descricao"].ToString();
        }


        public static string BuscarDescricaoPais(string CodPais)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT p.A017_DS_PAIS as 'Descricao' FROM a017_pais p");

            sql.Append(" Where  1 = 1 ");
            if (CodPais != "")
            {
                sql.Append(" And p.A017_CO_PAIS= " + CodPais);

            }

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString())).Rows[0]["Descricao"].ToString();

        }

        public static string BuscarDescricaoTipoDocumento(string CodDoc)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT t.A010_DS_TIPO_DOCUMENTO as 'Descricao' FROM a010_tipo_documento t ");

            sql.Append(" Where  1 = 1 ");
            if (CodDoc != "")
            {
                sql.Append(" And t.A010_CO_TIPO_DOCUMENTO = " + CodDoc);

            }

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString())).Rows[0]["Descricao"].ToString();

        }

        public static string BuscarDescricaoEstadoCivil(string Codigo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT t.A012_DS_ESTADO_CIVIL as 'Descricao' FROM a012_estado_civil t ");

            sql.Append(" Where  1 = 1 ");
            if (Codigo != "")
            {
                sql.Append(" And t.A012_CO_ESTADO_CIVIL = " + Codigo);

            }

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString())).Rows[0]["Descricao"].ToString();

        }

        public static string BuscarDescricaoRegimeBens(string Codigo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT t.a013_ds_regime_bens as 'Descricao' FROM a013_regime_bens t ");

            sql.Append(" Where  1 = 1 ");
            if (Codigo != "")
            {
                sql.Append(" And t.a013_co_regime_bens = " + Codigo);

            }
            DataTable dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            if (dt.Rows.Count == 0)
                return "";
            else
                return dt.Rows[0]["Descricao"].ToString();

        }

        public static string BuscarDescricaoEvento(string Codigo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT t.A002_DS_ATO as 'Descricao' FROM  a002_ato t ");

            sql.Append(" Where  1 = 1 ");
            if (Codigo != "")
            {
                sql.Append(" and t.A002_CO_ATO =  " + Codigo);
            }

            DataTable dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Descricao"].ToString();
            }
            return "Evento não emcontrado na Tabela";
        }

        public static string BuscarDescricaoTipoEmancipado(string Codigo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT t.A014_DS_EMANCIPACAO as 'Descricao' FROM a014_emancipacao t ");

            sql.Append(" Where  1 = 1 ");
            if (Codigo != "")
            {
                sql.Append(" and t.A014_CO_EMANCIPACAO =  " + Codigo);
            }

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString())).Rows[0]["Descricao"].ToString();
        }
        public static string BuscarDescricaoMunicipio(string codigoMunicipio)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT tmu_nom_mun FROM   tab_munic WHERE   tmu_cod_mun = " + codigoMunicipio);

            DataTable dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["tmu_nom_mun"].ToString().Trim();
            else
                return "";

        }

        public static bool ComparaTextoAcento(string texto1, string texto2)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT '");
            sql.AppendLine(texto1 + "' = '");
            sql.AppendLine(texto2 + "' ");


            DataTable dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString() == "1";
            else
                return false; ;

        }

        public static DataTable BuscarDescricaoLogradouro(string Type)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" Select tti_clav Cod, tti_nome Descricao From tab_cep_tipo ");

            sql.Append(" Where  1 = 1 ");
            if (Type != "")
            {
                sql.Append(" and tti_clav = " + Type);
            }

            sql.Append(" Order by tti_nome ");
            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
        }
        #endregion

        #region Requerimento Alteração
        public static DataTable getNaturezaJuridicaAlteracao()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                                      nj.t009_co_natureza_juridica as codigo
                                     , nj.t009_ds_natureza_juridica as descricao
                                FROM
                                  r006_natureza_juridica_tipo njt
                                INNER JOIN t009_natureza_juridica nj
                                ON nj.t009_co_natureza_juridica = njt.A006_CO_NATUREZA_JURIDICA
                                WHERE
                                  nj.t009_co_grupo IS NOT NULL
                                ORDER BY
                                  1");

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
        }

        public static DataTable getNaturezaJuridicaRequerimento(decimal pCodNatureza)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT nj.* ");
            sql.AppendLine("FROM   t009_natureza_juridica nj ");
            sql.AppendLine("WHERE  1 = 1 ");
            if (pCodNatureza != int.MinValue)
            {
                sql.AppendLine("and nj.t009_co_natureza_juridica = " + pCodNatureza);
            }

            sql.AppendLine("ORDER BY t009_ds_natureza_juridica");

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
        }

        public DataTable getNaturezaJuridicaCartorio(string _codNatureza)
        {

            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT nj.t009_co_grupo
                             , nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        INNER JOIN r006_natureza_juridica_tipo r
                        ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica
                        WHERE
                          1 = 1
                          AND t009_tipo_natureza = 2");
            if (!string.IsNullOrEmpty(_codNatureza))
                Sql.Append(" and nj.t009_co_natureza_juridica = " + _codNatureza);

            Sql.Append(" ORDER BY nj.t009_ds_natureza_juridica ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        public static DataTable getCondicaoDNRC(decimal pCodCondicao)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT nj.* ");
            sql.AppendLine("FROM   a009_condicao nj ");
            sql.AppendLine("WHERE  1 = 1 ");
            if (pCodCondicao != int.MinValue)
            {
                sql.AppendLine("and nj.A009_CO_CONDICAO = " + pCodCondicao);
            }

            sql.AppendLine("ORDER BY A009_DS_CONDICAO");

            return DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));
        }

        #endregion

        #region Dados WebServices Requerimento Protocolo Web

        public DataTable getDadosEventosRequerimento(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("SELECT RR.R013_COD_EVENTO EVENTO ");
            Sql.AppendLine("FROM ");
            Sql.AppendLine("  r013_requerimento_evento rr ");
            Sql.AppendLine("WHERE  ");
            Sql.AppendLine("  rr.R013_NR_PROTOCOLO_OR = '" + pProtocolo + "' ");
            Sql.AppendLine("  and RR.R013_COD_EVENTO is not null ");

            using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("DADOSEVENTOS"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }

        public DataTable getSeqPessoa_AA(string pProtocoloJunta)
        {


            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT a.T005_NR_PROTOCOLO SEQ, ");
            Sql.AppendLine(" pj.T001_SQ_PESSOA ");
            Sql.AppendLine(" FROM ");
            Sql.AppendLine("   t005_protocolo a, t003_pessoa_juridica pj ");
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine("   1 = 1 ");
            Sql.AppendLine("   AND a.T001_SQ_PESSOA = pj.T001_SQ_PESSOA ");
            Sql.AppendLine("   AND (a.T005_NR_PROTOCOLO_RCPJ = '" + pProtocoloJunta + "' ");
            Sql.AppendLine("        OR a.T005_NR_PROTOCOLO_ENQUADRAMENTO = '" + pProtocoloJunta + "') ");

            using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("DADOSPROCESSO"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }


        public DataTable getOutrosDados_AA(string Protocolo)
        {

            DataTable dtEmpresa = getSeqPessoa_AA(Protocolo);
            if (dtEmpresa.Rows.Count == 0)
            {
                return new DataTable();
            }


            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" SELECT p.T002_NOME_MAE MAE, ");
            Sql.AppendLine("        p.T002_NR_CPF CPF, ");
            Sql.AppendLine("        pe.T001_DS_PESSOA NOMEEMPRESARIO ");
            Sql.AppendLine(" FROM   r001_vinculo v, t002_pessoa_fisica p, t001_pessoa pe ");
            Sql.AppendLine(" WHERE  1 = 1 ");
            Sql.AppendLine(" AND    p.T001_SQ_PESSOA = pe.T001_SQ_PESSOA ");
            Sql.AppendLine(" AND    v.T001_SQ_PESSOA = p.T001_SQ_PESSOA ");
            Sql.AppendLine(" AND    v.T001_SQ_PESSOA_PAI = " + dtEmpresa.Rows[0]["T001_SQ_PESSOA"].ToString());
            Sql.AppendLine(" AND    v.A009_CO_CONDICAO in (22, 50, 65) ");
            Sql.AppendLine(" LIMIT 1 ");

            using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("DADOSPROCESSO"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }

        public DataTable getDadosProcessoRequerimento_AA(string pProtocoloJunta)
        {

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT a.T005_NR_PROTOCOLO protocolo ");
            Sql.AppendLine("      , a.T005_CO_ATO ato ");
            Sql.AppendLine("      , a.T005_NR_DBE ");
            Sql.AppendLine("      , a.T005_NR_PROTOCOLO_VIABILIDADE ");
            Sql.AppendLine("      , ( ");
            Sql.AppendLine("        SELECT pf.T001_DS_PESSOA FROM requerimento.r001_vinculo v, t001_pessoa pf WHERE v.T001_SQ_PESSOA_PAI = a.t001_sq_pessoa ");
            Sql.AppendLine("          AND pf.T001_SQ_PESSOA = v.T001_SQ_PESSOA ");
            Sql.AppendLine("          AND v.A009_CO_CONDICAO = 500) AS nomerequerente ");
            Sql.AppendLine("      , ( ");
            Sql.AppendLine("        SELECT pf.T001_EMAIL FROM requerimento.r001_vinculo v, t001_pessoa pf WHERE v.T001_SQ_PESSOA_PAI = a.t001_sq_pessoa ");
            Sql.AppendLine("          AND pf.T001_SQ_PESSOA = v.T001_SQ_PESSOA ");
            Sql.AppendLine("          AND v.A009_CO_CONDICAO = 500) AS emailrequerente ");
            Sql.AppendLine("      , ( ");
            Sql.AppendLine("        SELECT pf.T002_NR_CPF FROM requerimento.r001_vinculo v, t002_pessoa_fisica pf WHERE v.T001_SQ_PESSOA_PAI = a.t001_sq_pessoa ");
            Sql.AppendLine("          AND pf.T001_SQ_PESSOA = v.T001_SQ_PESSOA ");
            Sql.AppendLine("          AND v.A009_CO_CONDICAO = 500) AS cpfrequerente  ");
            Sql.AppendLine("   FROM t005_protocolo a  ");
            Sql.AppendLine("   WHERE (a.T005_NR_PROTOCOLO_RCPJ = '" + pProtocoloJunta + "' OR a.T005_NR_PROTOCOLO_ENQUADRAMENTO = '" + pProtocoloJunta + "') ");

            using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("DADOSPROCESSO"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }


        // antigo leandro erro via unica 03/08/2015
        //public DataTable getDadosProcessoRequerimento_AA(string pProtocoloJunta)
        //{


        //    DataTable dtOutos = getOutrosDados_AA(pProtocoloJunta);

        //    string MAE = "";
        //    string NOMERESPONSAVEL = "";
        //    string CPFRESPONSAVEL = "";

        //    if (dtOutos.Rows.Count > 0)
        //    {
        //        MAE = dtOutos.Rows[0]["MAE"].ToString();
        //        NOMERESPONSAVEL = dtOutos.Rows[0]["NOMEEMPRESARIO"].ToString();
        //        CPFRESPONSAVEL = dtOutos.Rows[0]["CPF"].ToString();
        //    }


        //    StringBuilder Sql = new StringBuilder();
        //    Sql.AppendLine(" SELECT a.T005_NR_PROTOCOLO SEQ, ");
        //    Sql.AppendLine(" 1 TIPOREGISTRO, ");
        //    Sql.AppendLine(" pj.T001_SQ_PESSOA, ");
        //    //Sql.AppendLine(" a.T005_CO_ATO ATO  ");
        //    Sql.AppendLine("    (SELECT v.T001_CPF_CNPJ_PESSOA CPFREQUERENTE ");
        //    Sql.AppendLine("    FROM    r001_vinculo v ");
        //    Sql.AppendLine("    WHERE   v.T001_SQ_PESSOA_PAI = pj.T001_SQ_PESSOA");
        //    Sql.AppendLine("    AND     v.A009_CO_CONDICAO = 500) CPFREQUERENTE,");

        //    Sql.AppendLine(" (SELECT DISTINCT ee.R013_COD_ATO FROM r013_requerimento_evento ee WHERE 1 = 1 and R013_COD_ATO is not null AND ee.R013_NR_PROTOCOLO_OR = '" + pProtocoloJunta + "' LIMIT 1) ATO");
        //    Sql.AppendLine(" , pj.A006_CO_NATUREZA_JURIDICA NATUREZA  ");
        //    Sql.AppendLine(" , a.T005_NR_PROTOCOLO PROTOCOLO ");
        //    Sql.AppendLine(" , pe.T001_DS_PESSOA NOMEEMPRESA ");
        //    Sql.AppendLine(" , pj.T003_NR_MATRICULA NIRE ");
        //    Sql.AppendLine(" , pj.T003_NR_CNPJ CNPJ ");
        //    Sql.AppendLine(" , a.T005_NR_PROTOCOLO_VIABILIDADE VIABILIDADE ");
        //    Sql.AppendLine(" , a.T005_NR_PROTOCOLO_RCPJ PROTOCOLO_OR ");
        //    Sql.AppendLine(" , 0 QTVIAS ");
        //    Sql.AppendLine(" , 000 VALORPROCESSO ");
        //    Sql.AppendLine(" ,'' OBSERVACAOESPECIFICA ");
        //    Sql.AppendLine(" ,'" + MAE + "' NOMEMAE ");
        //    Sql.AppendLine(" ,'" + NOMERESPONSAVEL + "' NOMERESPONSAVEL ");
        //    Sql.AppendLine(" ,'" + CPFRESPONSAVEL + "' CPFRESPONSAVEL ");
        //    Sql.AppendLine(" FROM ");
        //    Sql.AppendLine("   t005_protocolo a, t003_pessoa_juridica pj, t001_pessoa pe ");
        //    Sql.AppendLine(" WHERE ");
        //    Sql.AppendLine("   1 = 1 ");
        //    Sql.AppendLine("   AND pj.T001_SQ_PESSOA = pe.T001_SQ_PESSOA ");
        //    Sql.AppendLine("   AND a.T001_SQ_PESSOA = pj.T001_SQ_PESSOA ");
        //    Sql.AppendLine("   AND (a.T005_NR_PROTOCOLO_RCPJ = '" + pProtocoloJunta + "' ");
        //    Sql.AppendLine("        OR a.T005_NR_PROTOCOLO_ENQUADRAMENTO = '" + pProtocoloJunta + "') ");

        //    using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            using (DataTable toReturn = new DataTable("DADOSPROCESSO"))
        //            {
        //                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
        //                {
        //                    conn.Open();
        //                    cmd.Connection = conn;
        //                    cmd.CommandText = Sql.ToString();
        //                    cmd.CommandType = CommandType.Text;

        //                    adapter.Fill(toReturn);

        //                    return toReturn;

        //                }
        //            }

        //        }
        //    }
        //}


        public DataTable getDadosProcessoRequerimentoComRequerente(string T001_SQ_PESSOA)
        {


            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" SELECT v.T001_CPF_CNPJ_PESSOA CPFREQUERENTE ");
            Sql.AppendLine(" FROM r001_vinculo v ");
            Sql.AppendLine(" WHERE v.T001_SQ_PESSOA_PAI = " + T001_SQ_PESSOA);
            Sql.AppendLine(" AND v.A009_CO_CONDICAO = 500 ");

            using (MySqlConnection conn = new MySqlConnection(psc.Framework.General.ConnectionStringMYSQL()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("DADOSREQUERENTE"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }


        #endregion

        public static bool ValidaMunicipioConvenio(decimal pCodMunicipio)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" Select * ");
            sql.Append(" From   wbs_inst_serv s, tab_instituicao i");
            sql.Append(" Where  i.tin_cnpj = s.wis_tin_cnpj");
            sql.Append(" And    i.tin_tip_instituicao = 2 "); //Prefeitura
            sql.Append(" And    i.tin_tmu_cod_mun = " + pCodMunicipio);
            sql.Append(" And    s.wis_dat_ativacao Is Not Null ");

            MySqlDataReader reader = dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString());

            bool resp = reader.HasRows;

            reader.Close();

            return resp;


            //return dal.ExecuteReader(General.ConnectionString(), CommandType.Text, sql.ToString()).HasRows;
        }

        #region JUCERJA
        public static bool IsProtocoloUtilizadoJUCERJA(string NumProtocoloRCPJ, string numRequerimento)
        {

            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select count(*) ");
                Sql.AppendLine(" from t005_protocolo p");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.T005_NR_PROTOCOLO_RCPJ = '" + NumProtocoloRCPJ + "' ");
                Sql.AppendLine(" and p.t005_nr_protocolo != '" + numRequerimento + "' ");

                Sql.AppendLine(" and p.T005_NR_PROTOCOLO not in ");
                Sql.AppendLine("                    (select ps.t005_nr_protocolo  from t011_protocolo_status ps ");
                Sql.AppendLine(" where ps.T011_IN_SITUACAO IN(5,9)) ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

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

        public static bool IsProtocoloUtilizadoJUCERJA(string NumProtocoloRCPJ, string numRequerimento, out string ProtocoloAnteriorEncontrato)
        {

            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("t005_protocolo");
                Sql.AppendLine(" select p.t005_nr_protocolo as 'NumRequerimentoAnterior' ");
                Sql.AppendLine(" from t005_protocolo p");
                Sql.AppendLine(" where ");
                Sql.AppendLine(" p.T005_NR_PROTOCOLO_RCPJ = '" + NumProtocoloRCPJ + "' ");
                Sql.AppendLine(" and p.t005_nr_protocolo != '" + numRequerimento + "' ");

                Sql.AppendLine(" and p.T005_NR_PROTOCOLO not in ");
                Sql.AppendLine("                    (select ps.t005_nr_protocolo  from t011_protocolo_status ps ");
                Sql.AppendLine(" where ps.T011_IN_SITUACAO IN(9,5)) ");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

                try
                {
                    ProtocoloAnteriorEncontrato = "";
                    // Open connection.
                    // _mainConnection.Open();

                    // Execute query.
                    //cmdToExecute.ExecuteNonQuery();
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                    {
                        if (toReturn.Rows[0][0].ToString().Trim() != "")
                        {
                            ProtocoloAnteriorEncontrato = toReturn.Rows[0][0].ToString().Trim();
                            return true;
                        }
                    }

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
        #endregion

        #region Comparacao DBE x Viabilidade
        public static DataTable getDadosDBEControl(String pNumeroDBE)
        {

            string recibo = pNumeroDBE.Substring(0, 10);
            string identificador = pNumeroDBE.Substring(pNumeroDBE.Length - 14);



            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosDBEControl");

            Sql.AppendLine(@"SELECT * FROM T73300_DBE_CONTROL
                            WHERE t73300_rec_solicitacao = '" + recibo + @"' 
                                  AND t73300_ide_solicitacao = '" + identificador + "' ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static DataTable getDadosDBEEventos(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosEventos");

            Sql.AppendLine(@"SELECT * FROM t73301_dbe_evento
                            WHERE t73300_id_control = " + Chave);


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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


        public static DataTable getDadosDBEFCPJ(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosFCPJ");

            Sql.AppendLine(@"SELECT * FROM t73302_dbe_fcpj
                            WHERE t73300_id_control = " + Chave);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static DataTable getDadosDBEQSA(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosQSA");

            Sql.AppendLine(@"SELECT * FROM t73303_dbe_qsa
                            WHERE t73300_id_control = " + Chave);
            Sql.AppendLine(" order by t73303_cpf_cnpj_qsa");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static DataTable getDadosDBECNAESecundaria(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosCNAE");

            Sql.AppendLine(@"SELECT * FROM t73304_dbe_cnae_secundaria
                            WHERE t73300_id_control = " + Chave);
            Sql.AppendLine(" order by t73304_cnae_secundaria ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static DataTable getDadosDBEContador(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosContador");

            Sql.AppendLine(@"SELECT * FROM t73305_dbe_contador
                            WHERE t73300_id_control = " + Chave);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

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

        public static string GetTipoLogradouroDBE(string _tipoLogradouro)
        {

            StringBuilder sql = new StringBuilder();
            string _retTipoLogradouro = "";

            sql.AppendLine(@" select TPOLOGR
                        from tab_cep_tipo_abrev 
                        where ");
            sql.AppendLine(" TPOLOGRABREV = '" + _tipoLogradouro + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            if (Dt.Rows.Count != 0)
            {
                _retTipoLogradouro = Dt.Rows[0]["TPOLOGR"].ToString();
            }
            else
            {
                _retTipoLogradouro = _tipoLogradouro;
            }

            return _retTipoLogradouro;
        }
        public static string GetDescricaoEventoRFB(string _evento)
        {

            StringBuilder sql = new StringBuilder();
            string _ret = "";

            sql.AppendLine(@" SELECT RTRIM(A002_DS_ATO) DESCRICAO
                              FROM a002_ato
                              WHERE A002_CO_ATO =" + _evento);

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            if (Dt.Rows.Count != 0)
            {
                _ret = Dt.Rows[0]["DESCRICAO"].ToString();
            }

            return _ret;
        }
        #endregion

        #region Link Ajuda
        public static DataTable getListaAjuda(string _codigo)
        {
            MySqlConnection _mainConnection = new MySqlConnection();

            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT distinct 
                                     al.a099_ds_documento descricao
                                    , al.a099_ds_link link
                             FROM t005_protocolo p
                                INNER JOIN t003_pessoa_juridica pj
                                ON p.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                                INNER JOIN r005_protocolo_evento pe
                                ON p.T004_NR_CNPJ_ORG_REG = pe.T004_NR_CNPJ_ORG_REG
                                AND p.T005_NR_PROTOCOLO = pe.T007_NR_PROTOCOLO
                                INNER JOIN a099_link_documentos al
                                ON al.a099_cod_evento = pe.A003_CO_EVENTO
                                AND al.a099_cod_nat_juridica = pj.t003_co_tipo_pes_jur 
                             WHERE p.T005_NR_PROTOCOLO = '" + _codigo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
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

        #region Digitalização
        public static DataTable getImagensContrato(string wProtocolo)
        {


            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("Andamento");

                Sql.AppendLine(@"Select * from (
                            Select 1 tipoImagem, imagem
                            from  controleimagem.imagem_trabalhada 
                            Where   protocolo = '" + wProtocolo + "'");

                Sql.AppendLine(@"  
                            union all
                            Select 2 tipoImagem , imagem
                            from controleimagem.imagem_origem
                            Where   protocolo = '" + wProtocolo + "'");

                Sql.AppendLine(" ) A");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

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

        public static DataTable getProcessosVinculados(string wProtocolo)
        {


            using (MySqlConnection _conn = new MySqlConnection(General.ConnectionStringMYSQL()))
            {
                MySqlCommand cmdToExecute = new MySqlCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("ProcessoVinculado");

                Sql.AppendLine(@"SELECT pv.protocolo
                                         , pv.protocolo_vinculado
                                         , pv.ato
                                         , a.NO_ATO
                                         , it.PROTOCOLO img_trabalhada
                                         , imo.PROTOCOLO img_original FROM controleimagem.processo_vinculado pv
                                    LEFT JOIN shared.ato a ON pv.ato = a.CO_ATO
                                    LEFT JOIN controleimagem.imagem_trabalhada it ON pv.protocolo_vinculado = it.PROTOCOLO
                                    LEFT JOIN controleimagem.imagem_origem imo ON pv.protocolo_vinculado = imo.PROTOCOLO
                            Where   pv.protocolo = '" + wProtocolo + "'");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

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

    }

}

