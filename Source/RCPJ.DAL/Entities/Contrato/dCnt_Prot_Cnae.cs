using System;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_Prot_Cnae : DBInteractionBase
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _cpc_cod_protocolo;
        private string _reque_protocolo;
        protected string _cpc_tae_cod_actd;
        protected decimal _cpc_tip_cnae;
        protected decimal _cpc_tad_sequencia;
        protected string _cpc_tad_tin_cnpj;
        protected string _sq_filial;
        #endregion

        #region Class Member Declarations

        public string sq_filial
        {
            get { return _sq_filial; }
            set { _sq_filial = value; }
        }

        public string cpc_cod_protocolo
        {
            get { return _cpc_cod_protocolo; }

            set { _cpc_cod_protocolo = value; }
        }

        public string cpc_tae_cod_actd
        {
            get { return _cpc_tae_cod_actd; }

            set { _cpc_tae_cod_actd = value; }
        }

        public decimal cpc_tip_cnae
        {
            get { return _cpc_tip_cnae; }

            set { _cpc_tip_cnae = value; }
        }
        public decimal cpc_tad_sequencia
        {
            get { return _cpc_tad_sequencia; }

            set { _cpc_tad_sequencia = value; }
        }

        public string cpc_tad_tin_cnpj
        {
            get { return _cpc_tad_tin_cnpj; }

            set { _cpc_tad_tin_cnpj = value; }
        }

        public string reque_protocolo
        {
            get { return _reque_protocolo; }

            set { _reque_protocolo = value; }
        }
        

        #endregion

        public DataTable QueryFilial()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"SELECT  pj.T001_SQ_PESSOA                  AS T001_SQ_PESSOA
                                     , a.A001_CO_ATIVIDADE              AS A001_CO_ATIVIDADE
                                     , a.R004_IN_PRINCIPAL_SECUNDARIO   AS R004_IN_PRINCIPAL_SECUNDARIO
                                     , ae.TAE_DESC                      AS TAD_DESC_ATIVIDADE
                                     , pj.T003_DS_OBJETO_SOCIAL         AS T003_DS_OBJETO_SOCIAL 
                            FROM    r004_atuacao a
                                    INNER JOIN tab_actv_econ ae
                                    ON ae.TAE_COD_ACTVD = a.A001_CO_ATIVIDADE
                                    INNER JOIN t003_pessoa_juridica pj
                                    ON a.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                            Where	1 = 1 
                                    And	pj.t001_sq_pessoa = '" + _sq_filial + "'");
            Sql.AppendLine(" order by a.R004_IN_PRINCIPAL_SECUNDARIO ");

           
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("CNAEFILIAL");

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();
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


        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            
            Sql.AppendLine(" SELECT ");
            Sql.AppendLine("        c.cpc_cod_protocolo");
            Sql.AppendLine("        ,c.cpc_tae_cod_actd");
            Sql.AppendLine("        ,c.principal_secundario");
            Sql.AppendLine("        ,t.TAE_DESC");
            Sql.AppendLine("        ,c.reque_protocolo");
            Sql.AppendLine(" From	vw_cnt_prot_cnae c");
            Sql.AppendLine("        inner join tab_actv_econ t on t.TAE_COD_ACTVD = c.cpc_tae_cod_actd");
            Sql.AppendLine(" Where	1 = 1 ");

            if(_cpc_cod_protocolo != "")
                Sql.AppendLine(" And	c.cpc_cod_protocolo = '" + _cpc_cod_protocolo + "'");
            if (_reque_protocolo != "")
                Sql.AppendLine(" And	c.reque_protocolo = '" + _reque_protocolo + "'");
            
            if (_cpc_tip_cnae > 0)
            {
                Sql.AppendLine(" And	c.principal_secundario = '" + _cpc_tip_cnae + "'");
            }
            Sql.AppendLine(" order by c.principal_secundario,c.cpc_tae_cod_actd ");



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A001_Atividade");

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

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_prot_cnae");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("  cpc_cod_protocolo, ");
            SqlI.AppendLine("  cpc_tae_cod_actd, ");
            SqlI.AppendLine("  cpc_tip_cnae, ");
            SqlI.AppendLine("  cpc_tad_sequencia, ");
            SqlI.AppendLine("  cpc_tad_tin_cnpj, ");
            SqlI.AppendLine("  reque_protocolo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("  @cpc_cod_protocolo, ");
            SqlI.AppendLine("  @cpc_tae_cod_actd, ");
            SqlI.AppendLine("  @cpc_tip_cnae, ");
            SqlI.AppendLine("  @cpc_tad_sequencia, ");
            SqlI.AppendLine("  @cpc_tad_tin_cnpj, ");
            SqlI.AppendLine("  @reque_protocolo ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_prot_cnae Set ");
            SqlU.AppendLine("  cpc_tae_cod_actd = @cpc_tae_cod_actd, ");
            SqlU.AppendLine("  cpc_tip_cnae = @cpc_tip_cnae, ");
            SqlU.AppendLine("  cpc_tad_sequencia = @cpc_tad_sequencia, ");
            SqlU.AppendLine("  cpc_tad_tin_cnpj =  @cpc_tad_tin_cnpj");
            //SqlU.AppendLine("  reque_protocolo =  @reque_protocolo");
            SqlU.AppendLine(" Where	1 = 1 ");


            SqlU.AppendLine(" And	cpc_cod_protocolo = '" + _cpc_cod_protocolo + "'"); ;
            SqlU.AppendLine(" And   reque_protocolo = '" + _reque_protocolo + "'");
            if (!string.IsNullOrEmpty(_cpc_tae_cod_actd))
            {
                SqlU.AppendLine(" And	cpc_tae_cod_actd = '" + _cpc_tae_cod_actd + "'");
            }
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("@cpc_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpc_cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@cpc_tae_cod_actd", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpc_tae_cod_actd));
                cmdToExecute.Parameters.Add(new MySqlParameter("@cpc_tip_cnae", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpc_tip_cnae));
                cmdToExecute.Parameters.Add(new MySqlParameter("@cpc_tad_sequencia", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpc_tad_sequencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("@cpc_tad_tin_cnpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cpc_tad_tin_cnpj));
                cmdToExecute.Parameters.Add(new MySqlParameter("@reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reque_protocolo));


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
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();
                }
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
