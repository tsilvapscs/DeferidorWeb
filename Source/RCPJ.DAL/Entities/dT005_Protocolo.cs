using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    /// <summary>
    /// Summary description for t005_protocolo.
    /// </summary>
    public class dT005_Protocolo : DBInteractionBase
    {
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected string _t005_nr_protocolo;
        protected decimal _t001_sq_pessoa;
        protected Nullable<DateTime> _t005_dt_entrada;
        protected Nullable<DateTime> _t005_dt_averbacao;
        protected string _t005_nr_protocolo_viabilidade;
        protected string _t005_nr_docad;
        protected string _t005_nr_dbe;
        protected decimal _t005_xl_docad;
        protected decimal _t005_xl_dbe;
        protected string _t005_capa;
        protected string _t005_decl_idoneidade;
        protected string _t005_nr_protocolo_prefeitura;
        protected string _t005_nr_protocolo_RCPJ;
        private string _t005_foro;
        private int _t005_nr_alteracao;
        private string _t005_protocolo_orgao_origem;
        private string _t005_co_ato = string.Empty;
        private string _t005_uf_origem = string.Empty;
        private int _localEntregaPorcesso = 1;
        private int _t005_tipo_doc_reque = 1;
        private int _T005_IN_TRANSF_UNIPESSOAL;

        private string _Local_Assinatura;
        private Nullable<DateTime> _Data_Assinatura;
        private int _t005_in_dbe_carregado;
        private string _t005_co_unidade_entrega = string.Empty;
        protected Boolean _atualizaStatus = true;
        private int _t005_in_clausila_adm = 2;
        private int _t005_in_alt_admin = 2;
        private string _t005_tx_clausula_adm = "";
        private int _t005_ind_end_estab = 1;
        private int _t005_codMunicipioInscMunicipal = 0;
       
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string t005_nr_protocolo_prefeitura
        {
            get { return _t005_nr_protocolo_prefeitura; }
            set { _t005_nr_protocolo_prefeitura = value; }
        }
        public string t005_capa
        {
            get { return _t005_capa; }
            set { _t005_capa = value; }
        }
        public string t005_decl_idoneidade
        {
            get { return _t005_decl_idoneidade; }
            set { _t005_decl_idoneidade = value; }
        }
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }
            set { _t004_nr_cnpj_org_reg = value; }
        }
        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }
            set { _t005_nr_protocolo = value; }
        }
        public decimal t001_sq_pessoa
        {
            get { return _t001_sq_pessoa; }
            set { _t001_sq_pessoa = value; }
        }
        public Nullable<DateTime> t005_dt_entrada
        {
            get { return _t005_dt_entrada; }
            set { _t005_dt_entrada = value; }
        }
        public Nullable<DateTime> t005_dt_averbacao
        {
            get { return _t005_dt_averbacao; }
            set { _t005_dt_averbacao = value; }
        }
        public string t005_nr_protocolo_viabilidade
        {
            get { return _t005_nr_protocolo_viabilidade; }
            set { _t005_nr_protocolo_viabilidade = value; }
        }
        public string t005_nr_docad
        {
            get { return _t005_nr_docad; }
            set { _t005_nr_docad = value; }
        }
        public string t005_nr_dbe
        {
            get { return _t005_nr_dbe; }
            set { _t005_nr_dbe = value; }
        }
        public decimal t005_xl_docad
        {
            get { return _t005_xl_docad; }
            set { _t005_xl_docad = value; }
        }
        public decimal t005_xl_dbe
        {
            get {return _t005_xl_dbe;}
            set { _t005_xl_dbe = value;}
        }
        public string T005_nr_protocolo_RCPJ
        {
            get { return _t005_nr_protocolo_RCPJ; }
            set { _t005_nr_protocolo_RCPJ = value; }
        }

        public string T005_foro
        {
            get { return _t005_foro; }
            set { _t005_foro = value; }
        }
        public int T005_nr_alteracao
        {
            get { return _t005_nr_alteracao; }
            set { _t005_nr_alteracao = value; }
        }
        public string T005_protocolo_orgao_origem
        {
            get { return _t005_protocolo_orgao_origem; }
            set { _t005_protocolo_orgao_origem = value; }
        }

        public string T005_co_ato
        {
            get { return _t005_co_ato; }
            set { _t005_co_ato = value; }
        }
        public string T005_uf_origem
        {
            get { return _t005_uf_origem; }
            set { _t005_uf_origem = value; }
        }
        public int T005_local_entrega
        {
            get { return _localEntregaPorcesso; }
            set { _localEntregaPorcesso = value; }
        }
        public int T005_tipo_doc_reque
        {
            get { return _t005_tipo_doc_reque; }
            set { _t005_tipo_doc_reque = value; }
        }
        public int T005_IN_TRANSF_UNIPESSOAL
        {
            get { return _T005_IN_TRANSF_UNIPESSOAL; }
            set { _T005_IN_TRANSF_UNIPESSOAL = value; }
        }
        public string t005_Local_Assinatura
        {
            get { return _Local_Assinatura; }
            set { _Local_Assinatura = value; }
        }
        public Nullable<DateTime> t005_Data_Assinatura
        {
            get { return _Data_Assinatura; }
            set { _Data_Assinatura = value; }
        }
        public int T005_in_dbe_carregado
        {
            get { return _t005_in_dbe_carregado; }
            set { _t005_in_dbe_carregado = value; }
        }
        public string T005_co_unidade_entrega
        {
            get { return _t005_co_unidade_entrega; }
            set { _t005_co_unidade_entrega = value; }
        }
        public Boolean AtualizaStatus
        {
            get { return _atualizaStatus; }
            set { _atualizaStatus = value; }
        }
        public int T005_in_alt_admin
        {
            get { return _t005_in_alt_admin; }
            set { _t005_in_alt_admin = value; }
        }

        public int T005_in_clausila_adm
        {
            get { return _t005_in_clausila_adm; }
            set { _t005_in_clausila_adm = value; }
        }


        public string T005_tx_clausula_adm
        {
            get { return _t005_tx_clausula_adm; }
            set { _t005_tx_clausula_adm = value; }
        }


        public int T005_codMunicipioInscMunicipal
        {
            get { return _t005_codMunicipioInscMunicipal; }
            set { _t005_codMunicipioInscMunicipal = value; }
        }
        #endregion 


        #region Implements

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(@" Insert into t005_protocolo
                              (
            	                t004_nr_cnpj_org_reg, 
            	                t005_nr_protocolo, 
            	                t001_sq_pessoa, 
            	                t005_dt_entrada, 
            	                t005_dt_averbacao, 
            	                t005_nr_protocolo_viabilidade, 
            	                t005_nr_docad, 
            	                t005_nr_dbe, 
                                t005_nr_protocolo_prefeitura,
            	                t005_nr_protocolo_rcpj, 
            	                t005_nr_alteracao,
                                t005_protocolo_orgao_origem,
                                t005_co_ato,
                                t005_uf_origem,
                                t005_tipo_doc_reque,
                                t005_in_dbe_carregado,
                                t005_codMunicipioInscMunicipal
                              )
                             Values 
                              (
            	                @v_t004_nr_cnpj_org_reg, 
            	                @v_t005_nr_protocolo, 
            	                @v_t001_sq_pessoa, 
            	                evaldate(@v_t005_dt_entrada), 
            	                evaldate(@v_t005_dt_averbacao), 
            	                @v_t005_nr_protocolo_viabilidade, 
            	                @v_t005_nr_docad, 
            	                @v_t005_nr_dbe, 
                                @v_t005_nr_protocolo_prefeitura,
            	                @v_t005_nr_protocolo_rcpj, 
            	                @v_t005_nr_alteracao,
                                @v_t005_protocolo_orgao_origem,
                                @v_t005_co_ato,
                                @v_t005_uf_origem,
                                @v_t005_tipo_doc_reque,
                                @v_t005_in_dbe_carregado,
                                @v_t005_codMunicipioInscMunicipal
                              )");

            // Codigo Update ******************* 
            SqlU.AppendLine(@" Update   t005_protocolo Set 
                                        t001_sq_pessoa = @v_t001_sq_pessoa, 
                                        t005_dt_averbacao = evaldate(@v_t005_dt_averbacao), 
                                        t005_nr_protocolo_viabilidade = @v_t005_nr_protocolo_viabilidade, 
                                        t005_nr_docad = @v_t005_nr_docad, 
                                        t005_nr_dbe = @v_t005_nr_dbe, 
                                        t005_nr_protocolo_prefeitura = @v_t005_nr_protocolo_prefeitura ,
                                        t005_nr_protocolo_rcpj = @v_t005_nr_protocolo_rcpj,
                                        t005_co_ato = @v_t005_co_ato,
                                        t005_uf_origem = @v_t005_uf_origem,
                                        t005_tipo_doc_reque = @v_t005_tipo_doc_reque,
                                        t005_in_dbe_carregado = @v_t005_in_dbe_carregado,
                                        t005_nr_alteracao = @v_t005_nr_alteracao,
                                        t005_codMunicipioInscMunicipal = @v_t005_codMunicipioInscMunicipal
                             Where	1 = 1 
                             And t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg
                             And t005_nr_protocolo = @v_t005_nr_protocolo
                             And t001_sq_pessoa = " + _t001_sq_pessoa);

            string w1 = _t004_nr_cnpj_org_reg;
            string w2 = _t005_nr_protocolo;
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_dt_entrada", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_dt_entrada));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_dt_averbacao", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_dt_averbacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_docad", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_docad));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_dbe", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_prefeitura", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_prefeitura));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_rcpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_RCPJ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_alteracao", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_alteracao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_protocolo_orgao_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_protocolo_orgao_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_co_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_co_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_uf_origem", MySqlDbType.VarChar, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_uf_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_tipo_doc_reque", MySqlDbType.Int32, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_tipo_doc_reque));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_in_dbe_carregado", MySqlDbType.Int32, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_in_dbe_carregado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_codMunicipioInscMunicipal", MySqlDbType.Int32, 2, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_codMunicipioInscMunicipal));
                
                
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
                    cmdToExecute.CommandText = "SELECT LAST_INSERT_ID()";
                    _t005_nr_protocolo = cmdToExecute.ExecuteScalar().ToString();
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
        
        public void UpdateViabili()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t005_protocolo");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t005_nr_protocolo, ");
            SqlI.AppendLine("	t001_sq_pessoa, ");
            SqlI.AppendLine("	t005_dt_entrada, ");
            SqlI.AppendLine("	t005_dt_averbacao, ");
            SqlI.AppendLine("	t005_nr_protocolo_viabilidade, ");
            SqlI.AppendLine("	t005_nr_docad, ");
            SqlI.AppendLine("	t005_nr_dbe, ");
            SqlI.AppendLine("   t005_nr_protocolo_prefeitura,");
            SqlI.AppendLine("	t005_nr_protocolo_rcpj ");
            //SqlI.AppendLine("	t005_xl_docad, ");
            //SqlI.AppendLine("	t005_xl_dbe");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t005_nr_protocolo, ");
            SqlI.AppendLine("	@v_t001_sq_pessoa, ");
            SqlI.AppendLine("	evaldate(@v_t005_dt_entrada), ");
            SqlI.AppendLine("	evaldate(@v_t005_dt_averbacao), ");
            SqlI.AppendLine("	@v_t005_nr_protocolo_viabilidade, ");
            SqlI.AppendLine("	@v_t005_nr_docad, ");
            SqlI.AppendLine("	@v_t005_nr_dbe, ");
            SqlI.AppendLine("   @v_t005_nr_protocolo_prefeitura,");
            SqlI.AppendLine("	@v_t005_nr_protocolo_rcpj ");

            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     t005_protocolo Set ");

            //SqlU.AppendLine("		t001_sq_pessoa = @v_t001_sq_pessoa, ");

            SqlU.AppendLine("		t005_dt_averbacao = evaldate(@v_t005_dt_averbacao), ");
            SqlU.AppendLine("		t005_nr_protocolo_viabilidade = @v_t005_nr_protocolo_viabilidade, ");
            SqlU.AppendLine("		t005_nr_docad = @v_t005_nr_docad, ");
            SqlU.AppendLine("		t005_nr_dbe = @v_t005_nr_dbe, ");
            SqlU.AppendLine("       t005_nr_protocolo_prefeitura = @v_t005_nr_protocolo_prefeitura ,");
            SqlU.AppendLine("		t005_nr_protocolo_rcpj = @v_t005_nr_protocolo_rcpj ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg");
            SqlU.AppendLine(" And t005_nr_protocolo = @v_t005_nr_protocolo");
            SqlU.AppendLine(" And t001_sq_pessoa = " + _t001_sq_pessoa);
            string w1 = _t004_nr_cnpj_org_reg;
            string w2 = _t005_nr_protocolo;
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t001_sq_pessoa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_dt_entrada", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_dt_entrada));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_dt_averbacao", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_dt_averbacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_docad", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_docad));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_dbe", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_prefeitura", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_prefeitura));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_rcpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_RCPJ));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_xl_docad", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_xl_docad));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_xl_dbe", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_xl_dbe));

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

        public void UpdateCodigoAto(string pAto, string pRequerimento)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_CO_ATO= '" + pAto + "'");
            Sql.AppendLine(" WHERE  T005_NR_PROTOCOLO = '" + pRequerimento + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateCapa(string pProtocolo, string Caminho)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            
            System.IO.FileStream st = new System.IO.FileStream(Caminho + pProtocolo + ".pdf", System.IO.FileMode.Open);
            byte[] buffer = new byte[st.Length];
            st.Read(buffer, 0, (int)st.Length);
            st.Close();
            Sql.AppendLine("UPDATE t005_protocolo set t005_capa = @image WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            cmdToExecute.Parameters.AddWithValue("@image", buffer);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateDeclaracao(string pProtocolo, string Caminho)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            //Sql.AppendLine(" Update dt005_protocolo set t005_capa = " +  wPdf);
            //Sql.AppendLine(" Where PRO_PROTOCOLO = '" + pProtocolo + "'");

            //Sql.AppendLine(" UPDATE ");
            //Sql.AppendLine("t005_protocolo SET T005_CAPA = " + wPdf);
            //Sql.AppendLine(" WHERE ");
            //Sql.AppendLine("T005_NR_PROTOCOLO = '" + pProtocolo + "'");

            System.IO.FileStream st = new System.IO.FileStream(Caminho + pProtocolo + ".pdf", System.IO.FileMode.Open);
            byte[] buffer = new byte[st.Length];
            st.Read(buffer, 0, (int)st.Length);
            st.Close();
            Sql.AppendLine("UPDATE t005_protocolo set t005_decl_idoneidade = @image WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            cmdToExecute.Parameters.AddWithValue("@image", buffer);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void RecuperaCapa(string pProtocolo, string Caminho)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            //string NoPai;
            Sql.AppendLine("SELECT t005_capa from t005_protocolo WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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
                byte[] buffer = (byte[])cmdToExecute.ExecuteScalar();
                using (System.IO.FileStream fs = new System.IO.FileStream(Caminho + pProtocolo + "a.pdf", System.IO.FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public string RecuperaPai()
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wPai;
            Sql.AppendLine("SELECT t001_sq_pessoa from t005_protocolo WHERE T005_NR_PROTOCOLO = '" + t005_nr_protocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                wPai = toReturn.Rows[0]["t001_sq_pessoa"].ToString();
                return wPai;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
               
            }
        }

        #region Validação Viabilidade/DBE utilizado em outro requerimento

        public DataTable VerificaExistenciaDBE(string wDBE )
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            //string wPai;
            
            Sql.AppendLine(@"SELECT p.t005_nr_protocolo_viabilidade
                                    , p.t005_nr_protocolo 
                             FROM t005_protocolo p WHERE p.T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine("  AND 0 = (SELECT count(*) FROM t011_protocolo_status s WHERE s.T005_NR_PROTOCOLO = p.T005_NR_PROTOCOLO ");
            Sql.AppendLine("  AND s.T011_IN_SITUACAO = 9)");


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public int VerificaExistenciaDBE(string wDBE, string wProtocolo)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            //string wPai;

            Sql.AppendLine(@"SELECT count(*) as qtd
                            FROM
                              t005_protocolo p
                            WHERE
                              p.T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine("   AND p.T005_NR_PROTOCOLO <> '" + wProtocolo + "'");
            Sql.AppendLine("  AND fnGetStatusProcesso(p.t005_nr_protocolo) <> 9");


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                return Int32.Parse(toReturn.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
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
        /// Retorna vazio se o DBE nao está sendo usado, retorna o numero do requerimento
        /// </summary>
        /// <param name="wDBE"></param>
        /// <param name="wProtocolo"></param>
        /// <returns></returns>
        public string ValidaDbeUtilizado(string wDBE, string wProtocolo)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            
            //Se nao tem protocolo, é porque é um requerimento novo
            //ai verifico se o DBe está associado a algum outro Requerimento
            Sql.AppendLine(@"SELECT p.T005_NR_PROTOCOLO as requerimento
                            FROM
                              t005_protocolo p
                            WHERE
                              p.T005_NR_DBE = '" + wDBE + "'");
            if(wProtocolo != "")
                Sql.AppendLine("   AND p.T005_NR_PROTOCOLO <> '" + wProtocolo + "'");
            
            Sql.AppendLine("  AND fnGetStatusProcesso(p.t005_nr_protocolo) not in (8,9)");


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    return toReturn.Rows[0]["requerimento"].ToString();
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable VerificaDbeUtilizado(string wDBE , string pRequerimento)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            //string wPai;

            Sql.AppendLine(@"SELECT p.t005_nr_protocolo_viabilidade
                                    , p.t005_nr_protocolo 
                             FROM t005_protocolo p WHERE p.T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine("  AND 0 = (SELECT count(*) FROM t011_protocolo_status s WHERE s.T005_NR_PROTOCOLO = p.T005_NR_PROTOCOLO ");
            Sql.AppendLine("  AND s.T011_IN_SITUACAO = 9)");


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable VerificaDbeUtilizadoFilial(string wDBE , string pRequerimento)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            //string wPai;

            Sql.AppendLine(@" SELECT requerimento
                            FROM
                              (
                              SELECT (SELECT t005_nr_protocolo
                                      FROM
                                        t005_protocolo
                                      WHERE
                                        t001_sq_pessoa IN
                                        (SELECT t001_sq_pessoa_pai
                                         FROM
                                           r001_vinculo r
                                         WHERE
                                           r.T001_SQ_PESSOA = pj.t001_sq_pessoa)) Requerimento
                                   , pj.*
                              FROM
                                t003_pessoa_juridica pj
                              WHERE 1 = 1 "); 
             Sql.AppendLine("       AND t003_DBE = '" + wDBE + "') A");
             Sql.AppendLine("  WHERE ");
             Sql.AppendLine("  fnGetStatusProcesso(Requerimento) <> 9");
             Sql.AppendLine("  AND requerimento != '" + pRequerimento + "'");


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public string ValidaViabilidadeUtilizada(String pViabilidade, string pRequerimento)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            
            Sql.AppendLine(@"SELECT p.t005_nr_protocolo as requerimento
                            From t005_protocolo p 
                            WHERE p.T005_NR_PROTOCOLO_VIABILIDADE = '" + pViabilidade + "'");
            if(pRequerimento != "")
                Sql.AppendLine("   AND p.T005_NR_PROTOCOLO <> '" + pRequerimento + "'");

            Sql.AppendLine("  AND fnGetStatusProcesso(p.t005_nr_protocolo) not in (8,9)");
            
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    return "Viabilidade está sendo utilizada na Requerimento " + toReturn.Rows[0]["requerimento"].ToString();
                else 
                    return "";
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

        public string AchaProtocoloSeHouver(String wProtocoloViabilidade)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty;
            Sql.AppendLine("SELECT p.t005_nr_protocolo from t005_protocolo p WHERE p.T005_NR_PROTOCOLO_VIABILIDADE = '" + wProtocoloViabilidade + "'");
            Sql.AppendLine("   and  0 = (select count(*) from t011_protocolo_status s ");
            Sql.AppendLine("  where s.T005_NR_PROTOCOLO = p.T005_NR_PROTOCOLO ");
            Sql.AppendLine("  and s.T011_IN_SITUACAO = 9) ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    wProtocoloRequerimento = toReturn.Rows[0]["t005_nr_protocolo"].ToString();
                return wProtocoloRequerimento;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }
        #endregion


        


        public string BuscaPorProcessoJunta(String wProcessoJunta)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty;
            string wProtocoloEnquadramento = String.Empty;

            Sql.AppendLine("SELECT t005_nr_protocolo , t005_nr_protocolo_enquadramento from t005_protocolo WHERE T005_NR_PROTOCOLO_RCPJ = '" + wProcessoJunta + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                {
                    wProtocoloRequerimento = toReturn.Rows[0]["t005_nr_protocolo"].ToString();
                    wProtocoloEnquadramento = toReturn.Rows[0]["t005_nr_protocolo_enquadramento"].ToString();
                }   
                return wProtocoloRequerimento;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public DataTable BuscaPorProtocolo(String wProcessoJunta)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");

            Sql.AppendLine(@" SELECT T005_NR_PROTOCOLO  requerimento
                                    , T005_NR_PROTOCOLO_RCPJ protocolo
                                    , t005_nr_protocolo_enquadramento protocoloEnquadramento
                                    from t005_protocolo 
                                    WHERE   T005_NR_PROTOCOLO_RCPJ = '" + wProcessoJunta + "'");
            Sql.AppendLine("         or t005_nr_protocolo_enquadramento = '" + wProcessoJunta + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
               
            }
        }

        public void RecuperaDecl(string pProtocolo, string Caminho)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine("SELECT t005_decl_idoneidade from t005_protocolo WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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
                byte[] buffer = (byte[])cmdToExecute.ExecuteScalar();
                using (System.IO.FileStream fs = new System.IO.FileStream(Caminho + pProtocolo + "a.pdf", System.IO.FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }


        public string GeraProtocoloJunta()
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "geraNumProtocolo";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //IN par_que_numero CHAR(2), out numero_completo int (9)
                cmdToExecute.Parameters.Add(new MySqlParameter("Par_que_numero", MySqlDbType.VarChar, 2, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, "6"));
                cmdToExecute.Parameters.Add(new MySqlParameter("Numero_completo", MySqlDbType.VarChar, 20, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, ""));

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

                return cmdToExecute.Parameters["@Numero_completo"].Value.ToString();


            }
            catch (Exception ex)
            {
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

        public void UpdateNumeroDBE(string pRequerimento, string numeroDBE)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_NR_DBE = '" + numeroDBE + "'");
            Sql.AppendLine(" WHERE  T005_NR_PROTOCOLO = '" + pRequerimento + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateNumeroDBEPJ(int sqPessoa, string numeroDBE)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T003_Pessoa_juridica ");
            Sql.AppendLine(" SET    T003_DBE = '" + numeroDBE + "'");
            Sql.AppendLine(" WHERE  T001_SQ_PESSOA = " + sqPessoa.ToString());

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateNumeroDARE(string pRequerimento, string numeroDARE)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_NR_DAE = '" + numeroDARE + "'");
            Sql.AppendLine(" WHERE  T005_NR_PROTOCOLO = '" + pRequerimento + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateProtocoloOrgaoRegistro(string CnpjOrgaoRegistro, string pRequerimento, string pProtocoloOrgaoRegistro)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_NR_PROTOCOLO_RCPJ = '" + pProtocoloOrgaoRegistro + "' ");
            Sql.AppendLine(" WHERE  T004_NR_CNPJ_ORG_REG = '" + CnpjOrgaoRegistro + "'");
            Sql.AppendLine(" AND    T005_NR_PROTOCOLO = '" + pRequerimento + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateProtocoloEnquadramento(string CnpjOrgaoRegistro, string pRequerimento, string pProtocoloEnquadramento)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_NR_PROTOCOLO_ENQUADRAMENTO = '" + pProtocoloEnquadramento + "'");
            Sql.AppendLine(" WHERE  T004_NR_CNPJ_ORG_REG = '" + CnpjOrgaoRegistro + "'");
            Sql.AppendLine(" AND    T005_NR_PROTOCOLO = '" + pRequerimento + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void UpdateStatusProtocolo(string pProtocolo, decimal pStatus, decimal MotivoCancela)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Update PSC_PROTOCOLO set PRO_STATUS = " + pStatus);
            if (pStatus == 99)
            {
                Sql.AppendLine(" , PRO_DATA_CANCELAMENTO = getdate()");
                Sql.AppendLine(" , PRO_CMC_id_MOTIVO = " + MotivoCancela);
            }
            Sql.AppendLine(" Where PRO_PROTOCOLO = '" + pProtocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public void EnviaRegistroManutencao(decimal pTipoRegistro, string pCNPJEnviar, string pCNPJRegist, ref string pCodProtocolo)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "CrearRegistroDeXmlManutencao";
            cmdToExecute.CommandType = CommandType.StoredProcedure;


            _mainConnection.Open();

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("pTipoRegistro", MySqlDbType.Int32, 22, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, pTipoRegistro));
                cmdToExecute.Parameters.Add(new MySqlParameter("pCNPJEnviar", MySqlDbType.VarChar, 14, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pCNPJEnviar));
                cmdToExecute.Parameters.Add(new MySqlParameter("pCNPJRegist", MySqlDbType.VarChar, 14, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pCNPJRegist));
                cmdToExecute.Parameters.Add(new MySqlParameter("pCodProtocolo", MySqlDbType.VarChar, 20, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, pCodProtocolo));


                cmdToExecute.ExecuteNonQuery();

                pCodProtocolo = cmdToExecute.Parameters["pCodProtocolo"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdToExecute.Dispose();
            }
        }

        public string geraXmlDoRequerimento(string nrProtocoloOR, string nrRequerimento, ref string MsgErro)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "requerimento_geraXmlDoRequerimento";
            cmdToExecute.CommandType = CommandType.StoredProcedure;


            _mainConnection.Open();

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("nrProtocoloOR", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 22, 0, "", DataRowVersion.Proposed, nrProtocoloOR));
                cmdToExecute.Parameters.Add(new MySqlParameter("nrRequerimento", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, nrRequerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("saida", MySqlDbType.Text, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("MsgErro", MySqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));

                cmdToExecute.ExecuteNonQuery();
                if (cmdToExecute.Parameters["MsgErro"].Value == null)
                {
                    MsgErro = "";
                }
                else
                {
                    MsgErro = cmdToExecute.Parameters["MsgErro"].Value.ToString();
                }
                return cmdToExecute.Parameters["saida"].Value.ToString();


            }
            catch (Exception ex)
            {
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

        public string geraXmlDoRequerimentoSiarco(string nrRequerimento, ref string MsgErro)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "geraXmlRequerimentoProtocolo";
            cmdToExecute.CommandType = CommandType.StoredProcedure;


            _mainConnection.Open();

            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("nrRequerimento", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, nrRequerimento));
                cmdToExecute.Parameters.Add(new MySqlParameter("saida", MySqlDbType.Text, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));
                cmdToExecute.Parameters.Add(new MySqlParameter("MsgErro", MySqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, null));

                cmdToExecute.ExecuteNonQuery();
                if (cmdToExecute.Parameters["MsgErro"].Value == null)
                {
                    MsgErro = "";
                }
                else
                {
                    MsgErro = cmdToExecute.Parameters["MsgErro"].Value.ToString();
                }
                return cmdToExecute.Parameters["saida"].Value.ToString();


            }
            catch (Exception ex)
            {
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

        public void ApagarProtocolo(string pProtocolo)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "PKG_JUCESCSEF.DeleteRuc";
            cmdToExecute.CommandType = CommandType.StoredProcedure;


            _mainConnection.Open();

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("pProtocolo", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("pMunicipio", MySqlDbType.Int32, 6, ParameterDirection.Input, true, 6, 0, "", DataRowVersion.Proposed, System.DBNull.Value));

                cmdToExecute.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }
        public string PegaValidaVariableDaAplicacao(string pXml, string Protocolo, string ValidaProtocolo)
        {
            using (MySqlConnection conn = _mainConnection)
            using (MySqlCommand cmd = new MySqlCommand())
            {
                string pOutXml = "";
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "PegaValidaVariableDaAplicacao";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("pXml", MySqlDbType.VarChar, pXml.Length + 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pXml));
                cmd.Parameters.Add(new MySqlParameter("pProtocolo", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Protocolo));
                cmd.Parameters.Add(new MySqlParameter("ValidaProtocolo", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ValidaProtocolo));
                cmd.Parameters.Add(new MySqlParameter("pOutXml", MySqlDbType.VarChar, 4000, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, pOutXml));

                cmd.ExecuteNonQuery();
                //OracleLob CLOB = (OracleLob)cmd.Parameters["pOutXml"].Value;

                return cmd.Parameters["pOutXml"].Value.ToString();
            }
        }


        public DataTable QueryXmlWs(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select    ");
            Sql.AppendLine(" protocolo.T004_NR_CNPJ_ORG_REG as CNPJ_RCPJ,   ");
            Sql.AppendLine(" protocolo.T005_NR_PROTOCOLO as ProtocoloRequerimento,   ");
            Sql.AppendLine(" protocolo.T001_SQ_PESSOA as SequencialPessoa,   ");
            Sql.AppendLine(" protocolo.T005_DT_ENTRADA as DataEntrada,   ");
            Sql.AppendLine(" protocolo.T005_DT_AVERBACAO as DataAverbacao,   ");
            Sql.AppendLine(" protocolo.T005_NR_PROTOCOLO_VIABILIDADE as NumeroProtocoloViabilidade,   ");
            Sql.AppendLine(" protocolo.T005_NR_DOCAD as NumeroDocad,   ");
            Sql.AppendLine(" protocolo.T005_NR_DBE as NumeroDbe,   ");
            Sql.AppendLine(" protocolo.T005_VALOR_PROTOCOLO as ValorProtocolo   ");
            Sql.AppendLine(" from t005_protocolo   protocolo   ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  protocolo.T005_NR_PROTOCOLO = '" + pProtocolo + "'");

            //Sql.AppendLine(" and  protocolo.T005_NR_PROTOCOLO =  '50100000000395'

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
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

        /// <summary>
        /// Atualiza situação do protocolo.
        /// </summary>
        /// <param name="Situacao">Parâmetro Situação</param>
        public void GravaValidacaoProtocolo(string Situacao)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" UPDATE T005_Protocolo ");
            Sql.AppendLine(" SET    T005_IN_SITUACAO = '" + Situacao + "'");
            Sql.AppendLine(" WHERE  T004_NR_CNPJ_ORG_REG = '" + _t004_nr_cnpj_org_reg + "'");
            Sql.AppendLine(" AND    T005_NR_PROTOCOLO = '" + _t005_nr_protocolo + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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
        #endregion

        #region Examinador
        public void UpdateExaminador()
        {
            
            StringBuilder SqlU = new StringBuilder();


            SqlU.AppendLine(@" Update   t005_protocolo Set 
                                        t005_nr_protocolo_rcpj = @v_t005_nr_protocolo_rcpj
                             Where	1 = 1 
                             And t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg
                             And t005_nr_protocolo = @v_t005_nr_protocolo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_viabilidade));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_dbe", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_dbe));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo_rcpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo_RCPJ));


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

        public bool IsStatus(int Status)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty;
            Sql.AppendLine(" SELECT T011_in_Situacao ");
            Sql.AppendLine(" FROM t011_protocolo_status ps ");
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine(" ps.T005_NR_PROTOCOLO = '" + _t005_nr_protocolo + "'");
            Sql.AppendLine(" order by ps.T011_DT_SITUACAO desc  ");
            Sql.AppendLine(" Limit 1 ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                {
                    if (int.Parse(toReturn.Rows[0]["T011_in_Situacao"].ToString()) == Status)
                        return true;
                    else
                        return false;
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public void UpdateStatusLog(int Status, string Usuario , string CodigoDBE, string ProtocoloViabilidade)
        {

            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();
            SqlI.AppendLine(" Insert into t011_protocolo_status_log");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,T011_IN_SITUACAO ");
            SqlI.AppendLine("	,T011_DT_SITUACAO ");
            SqlI.AppendLine("	,T011_USUARIO ");
            SqlI.AppendLine("	,T005_NR_PROTOCOLO_VIABILIDADE ");
            SqlI.AppendLine("	,T005_NR_DBE ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,@T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,@T011_IN_SITUACAO ");
            SqlI.AppendLine("	,@T011_DT_SITUACAO ");
            SqlI.AppendLine("	,@T011_USUARIO ");
            SqlI.AppendLine("	,@T005_NR_PROTOCOLO_VIABILIDADE ");
            SqlI.AppendLine("	,@T005_NR_DBE ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();

            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_IN_SITUACAO", MySqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Status.ToString()));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_DT_SITUACAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO_VIABILIDADE", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ProtocoloViabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_DBE", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, CodigoDBE));

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

        public void UpdateStatus(int Status, string Usuario)
        {

            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();
            SqlI.AppendLine(" Insert into t011_protocolo_status");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T004_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	T011_IN_SITUACAO, ");
            SqlI.AppendLine("	T011_DT_SITUACAO, ");
            SqlI.AppendLine("	T011_USUARIO ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T004_NR_CNPJ_ORG_REG, ");
            SqlI.AppendLine("	@T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	@T011_IN_SITUACAO, ");
            SqlI.AppendLine("	@T011_DT_SITUACAO, ");
            SqlI.AppendLine("	@T011_USUARIO ");
            SqlI.AppendLine("  )");


            SqlU.AppendLine(" Update t011_protocolo_status Set ");
            SqlU.AppendLine("	T004_NR_CNPJ_ORG_REG = @T004_NR_CNPJ_ORG_REG, ");
            SqlU.AppendLine("	T005_NR_PROTOCOLO = @T005_NR_PROTOCOLO, ");
            SqlU.AppendLine("	T011_IN_SITUACAO = @T011_IN_SITUACAO , ");
            SqlU.AppendLine("	T011_DT_SITUACAO = evaldate(@T011_DT_SITUACAO), ");
            SqlU.AppendLine("	T011_USUARIO = @T011_USUARIO ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And T004_NR_CNPJ_ORG_REG = @T004_NR_CNPJ_ORG_REG");
            SqlU.AppendLine(" And T005_NR_PROTOCOLO = @T005_NR_PROTOCOLO");
            SqlU.AppendLine(" And T011_IN_SITUACAO = @T011_IN_SITUACAO");



            MySqlCommand cmdToExecute = new MySqlCommand();
            if (!_atualizaStatus)
                cmdToExecute.CommandText = SqlI.ToString();
            else
                cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_IN_SITUACAO", MySqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Status.ToString()));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_DT_SITUACAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));

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

        public void UpdateStatus(int Status, string Usuario, string NumReqSubstituto)
        {

            StringBuilder SqlI = new StringBuilder();
            SqlI.AppendLine(" Insert into t011_protocolo_status");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,T011_IN_SITUACAO ");
            SqlI.AppendLine("	,T011_DT_SITUACAO ");
            SqlI.AppendLine("	,T011_USUARIO ");
            SqlI.AppendLine("	,T011_NR_PROTOCOLO_SUBSTITUTO ");

            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T004_NR_CNPJ_ORG_REG ");
            SqlI.AppendLine("	,@T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,@T011_IN_SITUACAO ");
            SqlI.AppendLine("	,@T011_DT_SITUACAO ");
            SqlI.AppendLine("	,@T011_USUARIO ");
            SqlI.AppendLine("	,@T011_NR_PROTOCOLO_SUBSTITUTO ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_IN_SITUACAO", MySqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Status.ToString()));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_DT_SITUACAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_NR_PROTOCOLO_SUBSTITUTO", MySqlDbType.VarChar, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, NumReqSubstituto));

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

        public bool DeleteStatus(int Status)
        {

            StringBuilder SqlD = new StringBuilder();
            SqlD.AppendLine(" Delete from t011_protocolo_status ");
            SqlD.AppendLine(" Where	 ");
            SqlD.AppendLine(" T005_NR_PROTOCOLO = @T005_NR_PROTOCOLO ");
            SqlD.AppendLine(" And T011_IN_SITUACAO = @T011_IN_SITUACAO ");
            SqlD.AppendLine(" And T004_NR_CNPJ_ORG_REG = @T004_NR_CNPJ_ORG_REG ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T011_IN_SITUACAO", MySqlDbType.VarChar, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Status.ToString()));

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
                    return false;
                }
                else
                {
                    return true;
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

        public void GravarExigencias(string Usuario, string codigo, string descricao, string protocolo)
        {
            StringBuilder SqlI = new StringBuilder();
            SqlI.AppendLine(" Insert into t012_protocolo_exigencias");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T012_CO_EXIGENCIA, ");
            SqlI.AppendLine("	T012_DS_EXIGENCIA, ");
            SqlI.AppendLine("	T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	T012_USUARIO, ");
            SqlI.AppendLine("	T012_DT_INCLUSAO ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@T012_CO_EXIGENCIA, ");
            SqlI.AppendLine("	@T012_DS_EXIGENCIA, ");
            SqlI.AppendLine("	@T005_NR_PROTOCOLO, ");
            SqlI.AppendLine("	@T012_USUARIO, ");
            SqlI.AppendLine("	@T012_DT_INCLUSAO ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_CO_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, codigo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DS_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, descricao));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DT_INCLUSAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T012_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));

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
       
        public void GravarExigenciasOutras(string cnpjOrgaoRegistro, string Usuario, string protocolo, string descricao, string fundamentoLegal, string grupo)
        {
            StringBuilder SqlI = new StringBuilder();
            SqlI.AppendLine(@" Insert into t016_protocolo_exigencias_outras
                              (
                                T004_NR_CNPJ_ORG_REG, 
                                T005_NR_PROTOCOLO, 
                                T016_DS_EXIGENCIA, 
                                T016_DS_FUNDAMENTO_LEGAL, 
                                T016_USUARIO, 
                                T016_DT_INCLUSAO,
                                T016_DS_GRUPO
                              )
                             Values 
                              (
                                @T004_NR_CNPJ_ORG_REG, 
                                @T005_NR_PROTOCOLO, 
                                @T016_DS_EXIGENCIA, 
                                @T016_DS_FUNDAMENTO_LEGAL, 
                                @T016_USUARIO, 
                                @T016_DT_INCLUSAO,
                                @T016_DS_GRUPO
                              )
                              ");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T004_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cnpjOrgaoRegistro));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T016_DS_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, descricao));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T016_DS_FUNDAMENTO_LEGAL", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, fundamentoLegal));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T016_USUARIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T016_DT_INCLUSAO", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new MySqlParameter("@T016_DS_GRUPO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, grupo));
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

        public void ExcluiExigencias(string protocolo)
        {
            StringBuilder SqlD = new StringBuilder();
            SqlD.AppendLine(@" 
            DELETE FROM t012_protocolo_exigencias
            WHERE 
            T005_NR_PROTOCOLO = '" + protocolo + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_CO_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, codigo));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DS_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, descricao));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, protocolo));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DT_INCLUSAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));

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

        public void ExcluiExigenciasOutras(string protocolo)
        {
            StringBuilder SqlD = new StringBuilder();
            SqlD.AppendLine(@" 
            DELETE FROM t016_protocolo_exigencias_outras
            WHERE 
            T005_NR_PROTOCOLO = '" + protocolo + "'"
            + " And T016_DS_FUNDAMENTO_LEGAL != 'Gerado Automaticamente' ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

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

        public void ExcluiExigenciasOutrasTudo(string protocolo)
        {
            StringBuilder SqlD = new StringBuilder();
            SqlD.AppendLine(@" 
            DELETE FROM t016_protocolo_exigencias_outras
            WHERE 
            T005_NR_PROTOCOLO = '" + protocolo + "' ");
            //+ " And T016_DS_FUNDAMENTO_LEGAL != 'Gerado Automaticamente' ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

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

        public void ExcluiExigenciaOBSJucerja(string protocolo)
        {
            StringBuilder SqlD = new StringBuilder();
            SqlD.AppendLine(@" 
            DELETE FROM t012_protocolo_exigencias
            WHERE 
            T005_NR_PROTOCOLO = '" + protocolo 
            + "' And T012_CO_EXIGENCIA = '9999'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_CO_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, codigo));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DS_EXIGENCIA", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, descricao));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, protocolo));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_DT_INCLUSAO", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@T012_USUARIO", MySqlDbType.VarChar, 15, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Usuario));

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

        public DataTable ListaExaminador()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select distinct T011_usuario from t011_protocolo_status  ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Examinador");
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

        public DataTable ListaSituacao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(@"SELECT a.TGE_COD_TIP_TAB as id_situacao,
                         a.TGE_NOMB_DESC as descricao
                         FROM
                            tab_generica a
                        WHERE
                            a.TGE_TIP_TAB = 1532 and a.TGE_COD_TIP_TAB >1 
                            ORDER BY a.TGE_TIP_TAB ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Situacao");
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

        public DataTable ListaProtocolo(params object[] parameters)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT PT.T005_NR_PROTOCOLO AS REQUERIMENTO
                             , PS.T001_DS_PESSOA AS NOMEREQUERENTE
                              , fnGetStatusProcesso(pt.T005_NR_PROTOCOLO) T011_IN_SITUACAO
                               , (SELECT DEscricao
                                  FROM
                                    tab_situacao
                                  WHERE
                                    id_situacao = fnGetStatusProcesso(pt.T005_NR_PROTOCOLO)) AS Descricao
                             , T005_DT_ENTRADA AS DATAPROTOCOLO
                              , '' AS EXAMINADOR
                               -- , T011_IN_SITUACAO
                               , ps1.T001_DS_PESSOA AS NOMEEMPRESA
                             , pt.T005_NR_PROTOCOLO_RCPJ ProtocoloJunta
                             , PJ.T003_NR_MATRICULA AS NIRE
                             , pj.T003_NR_CNPJ AS CNPJ
                             , PT.T005_NR_PROTOCOLO_RCPJ PROTOCOLO
                        FROM
                          t005_protocolo PT
                        INNER JOIN r001_vinculo VI
                        ON PT.T001_SQ_PESSOA = VI.T001_SQ_PESSOA_PAI
                        AND VI.A009_CO_CONDICAO = 500
                        INNER JOIN t001_pessoa PS
                        ON VI.T001_SQ_PESSOA = PS.T001_SQ_PESSOA
                        INNER JOIN t001_pessoa ps1
                        ON PT.T001_SQ_PESSOA = ps1.T001_SQ_PESSOA
                        INNER JOIN t003_pessoa_juridica pj
                        ON ps1.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                        
                Where 1=1 ");

            if (parameters[1].ToString() != "")
            {
                Sql.Append(" and PT.T005_NR_PROTOCOLO = '" + parameters[1].ToString() + "'");
            }
            if (parameters[2].ToString() != "")
            {
                Sql.Append(" and ps1.T001_DS_PESSOA like '" + parameters[2].ToString() + "%'");
            }
            if (parameters[3].ToString() != "")
            {
                Sql.Append(" and PS.T001_DS_PESSOA like '" + parameters[3].ToString() + "%'");
            }
            if (parameters[4].ToString() != "")
            {
                // Sql.Append(" and T011_USUARIO = '" + parameters[4].ToString() + "'");
            }
            if (parameters[5].ToString() != "")
            {
                Sql.Append(" AND fnGetStatusProcesso(pt.T005_NR_PROTOCOLO) = " + parameters[5].ToString());
                //Sql.Append(" and T011_IN_SITUACAO = " + parameters[5].ToString());
            }
            if (parameters[6].ToString() != "")
            {
                Sql.Append(" and DATE_FORMAT(T005_DT_ENTRADA, '%Y%m%d')>= " + DateTime.Parse(parameters[6].ToString()).ToString("yyyyMMdd"));
            }
            if (parameters[7].ToString() != "")
            {
                Sql.Append(" and DATE_FORMAT(T005_DT_ENTRADA, '%Y%m%d')<= " + DateTime.Parse(parameters[7].ToString()).ToString("yyyyMMdd"));
            }
            if (!String.IsNullOrEmpty(parameters[8].ToString()))
            {
                Sql.Append(" and pj.T003_NR_CNPJ = '" + parameters[8].ToString() + "'");
            }
            if (!String.IsNullOrEmpty(parameters[9].ToString()))
            {
                Sql.Append(" and pt.T005_NR_PROTOCOLO_RCPJ = '" + parameters[9].ToString() + "'");
            }
            if (!String.IsNullOrEmpty(parameters[10].ToString()))
            {
                Sql.Append(" and pj.T003_NR_MATRICULA = '" + parameters[10].ToString() + "'");
            }
            if (parameters.Length > 10)
            {
                if (!String.IsNullOrEmpty(parameters[11].ToString()))
                {
                    Sql.Append(" and pj.T003_DBE = '" + parameters[11].ToString() + "'");
                }
            }

            Sql.Append(" ORDER BY   PT.T005_NR_PROTOCOLO ");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Consulta");
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

        public DataTable GetStatusExame(string pRequerimento)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT PT.T005_NR_PROTOCOLO AS protocolo
                               , tab_GENERICA.Descricao
                               , T011_USUARIO AS cpf_examinador
                               , T011_IN_SITUACAO status_situacao
                               , SITUACAO.T011_DT_SITUACAO dataAnalise

                          FROM
                              t005_protocolo PT
                          INNER JOIN
                            (SELECT st1.T004_NR_CNPJ_ORG_REG
                                  , st1.T005_NR_PROTOCOLO
                                  , st1.T011_DT_SITUACAO
                                  , st1.T011_IN_SITUACAO
                                  , st1.T011_USUARIO
                             FROM
                               (
                               SELECT
                                     ST.T005_NR_PROTOCOLO
                                    , max(ST.T011_DT_SITUACAO) T011_DT_SITUACAO
                               FROM
                                 t011_protocolo_status ST 
                               GROUP BY st.T005_NR_PROTOCOLO
                               ) t1
                             INNER JOIN t011_protocolo_status st1
                             ON st1.T005_NR_PROTOCOLO = t1.T005_NR_PROTOCOLO
                             AND st1.T011_DT_SITUACAO = t1.T011_DT_SITUACAO
                            ) SITUACAO
                          ON SITUACAO.T005_NR_PROTOCOLO = PT.T005_NR_PROTOCOLO
                          INNER JOIN (SELECT a.TGE_COD_TIP_TAB AS id_situacao
                                           , a.TGE_NOMB_DESC AS descricao
                                      FROM
                                        tab_generica a
                                      WHERE
                                        a.TGE_TIP_TAB = 1532
                                        AND a.TGE_COD_TIP_TAB > 0) tab_GENERICA
                          ON tab_GENERICA.id_situacao = SITUACAO.T011_IN_SITUACAO 
                        
                Where 1=1 ");

            if (pRequerimento != "")
            {
                Sql.Append(" and PT.T005_NR_PROTOCOLO = '" + pRequerimento + "'");
            }

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Consulta");
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

        #endregion

        public void UpdateTextoRestituicaoBaixa(string _protocolo, string _texto)
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update    T005_Protocolo ");
            SqlU.AppendLine(" Set       T005_TX_CLAUSULA_RESTITUICAO = @v_T005_TX_CLAUSULA_RESTITUICAO ");
            SqlU.AppendLine(" Where	    1 = 1 ");
            SqlU.AppendLine(" And	    t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_T005_TX_CLAUSULA_RESTITUICAO", MySqlDbType.Text, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _texto));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _protocolo));

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

        public void UpdateClausulaArbitral(string _t005_nr_protocolo, string _clausualArbitral)
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update    T005_Protocolo ");
            SqlU.AppendLine(" Set       T005_TX_CLAUSULA_ARBITRAL = @T005_TX_CLAUSULA_ARBITRAL ");
            SqlU.AppendLine(" Where	    1 = 1 ");
            SqlU.AppendLine(" And	    t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_TX_CLAUSULA_ARBITRAL", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _clausualArbitral));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));

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
        public int GetStatusProtocolo()
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty;
            Sql.AppendLine(" SELECT T011_in_Situacao ");
            Sql.AppendLine(" FROM t011_protocolo_status ps ");
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine(" ps.T005_NR_PROTOCOLO = '" + _t005_nr_protocolo + "'");
            //Sql.AppendLine(" and ps.t011_in_situacao > 1");
            Sql.AppendLine(" order by ps.T011_DT_SITUACAO desc  ");
            Sql.AppendLine(" Limit 1 ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                {
                    return int.Parse(toReturn.Rows[0]["T011_in_Situacao"].ToString());
                }
                return 1;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public bool IsStatusAposInicio()
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            string wProtocoloRequerimento = String.Empty;
            Sql.AppendLine(" SELECT T011_in_Situacao ");
            Sql.AppendLine(" FROM t011_protocolo_status ps ");
            Sql.AppendLine(" WHERE ");
            Sql.AppendLine(" ps.T005_NR_PROTOCOLO = '" + _t005_nr_protocolo + "'");
            Sql.AppendLine(" and ps.t011_in_situacao > 1");
            Sql.AppendLine(" order by ps.T011_DT_SITUACAO desc  ");
            Sql.AppendLine(" Limit 1 ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                {
                    return true;
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public void GravardataAssinaturaAbervacaoContrato()
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update    T005_Protocolo ");
            SqlU.AppendLine(" Set       T005_DT_AVERBACAO = @T005_DT_AVERBACAO ");
            SqlU.AppendLine(" Where	    1 = 1 ");
            SqlU.AppendLine(" And	    t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@T005_DT_AVERBACAO", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_dt_averbacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));

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

        public void GravarClausulaAdm()
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update     T005_Protocolo Set ");
            SqlU.AppendLine("		t005_in_clausila_adm = @v_t005_in_clausila_adm, ");
            SqlU.AppendLine("		t005_tx_clausula_adm = @v_t005_tx_clausula_adm ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And	t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_in_clausila_adm", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_in_clausila_adm));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_tx_clausula_adm", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_tx_clausula_adm));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));

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

        public void GravarForo()
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update     T005_Protocolo Set ");
            SqlU.AppendLine("		t005_foro = @v_t005_foro, ");
            SqlU.AppendLine("		T005_LOCAL_ASSINATURA = @v_Local_Assinatura, ");
            SqlU.AppendLine("		T005_DATA_ASSINATURA = evaldate(@v_Data_Assinatura) ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And	t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_foro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_foro));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_Local_Assinatura", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Local_Assinatura));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_Data_Assinatura", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _Data_Assinatura));
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

        public void GravarLocalEntrea()
        {
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update     T005_Protocolo Set ");
            SqlU.AppendLine("		t005_local_entrega = @v_t005_local_entrega, ");
            SqlU.AppendLine("		t005_co_unidade_entrega = @v_t005_co_unidade_entrega ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And	t005_nr_protocolo = @v_t005_nr_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_local_entrega", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _localEntregaPorcesso));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_co_unidade_entrega", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_co_unidade_entrega));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));

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

        public void UpdateTransfUnipessoal()
        {
            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T005_Protocolo Set ");
            SqlU.AppendLine("		T005_IN_TRANSF_UNIPESSOAL = @v_T005_IN_TRANSF_UNIPESSOAL ");
            SqlU.AppendLine(" Where	");
            SqlU.AppendLine(" T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO ");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_IN_TRANSF_UNIPESSOAL", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_IN_TRANSF_UNIPESSOAL));
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
        
        public string GetNumProtocoloORbyReq(string tmpNumReq)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");

            Sql.AppendLine("SELECT T005_NR_PROTOCOLO_RCPJ from t005_protocolo WHERE T005_NR_PROTOCOLO = '" + tmpNumReq + "'");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0 && toReturn.Rows[0][0].ToString()!="")
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        public void ApagaViabilidadeCancelada(string pProtocolo)
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(@" Update   t005_protocolo Set 
                                        t005_nr_protocolo_viabilidade = '', 
                                        t005_nr_dbe = '' 
                             Where t005_nr_protocolo = @v_t005_nr_protocolo ");

            string w1 = _t004_nr_cnpj_org_reg;
            string w2 = _t005_nr_protocolo;
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

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

        public void GravaTipoRegistroViab(string _tipoRegistro, string _protocolo)
        {
            
            StringBuilder SqlU = new StringBuilder();
            SqlU.AppendLine(" Update    T005_Protocolo ");
            SqlU.AppendLine(" Set       T005_TIP_REGISTRO_VIAB = @v_T005_TIP_REGISTRO_VIAB ");
            SqlU.AppendLine(" Where	    t005_nr_protocolo = @v_t005_nr_protocolo  ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_T005_TIP_REGISTRO_VIAB", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipoRegistro));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _protocolo));

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

        #region JUCERJA
        /// <summary>
        /// Consulta número de protocolo da viabilidade passando como parâmetro de busca o número do DBE da RFB. 
        /// </summary>
        /// <param name="wDBE">Parâmetro número do DBE da RFB.</param>
        /// <param name="wreq">Parâmetro número do requerimento</param>
        /// <returns>Retorna uma DataTable com o número de protocolo da viabilidade 
        /// que tem como filtro de busca o número do requerimento.</returns>
        public DataTable VerificaStatusDBE(string wDBE, string wreq)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("t005_protocolo");
            //string wPai;
            //Sql.AppendLine("SELECT t005_nr_protocolo_viabilidade from t005_protocolo WHERE T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine("SELECT p.t005_nr_protocolo_viabilidade ");
            Sql.AppendLine(", p.T005_NR_PROTOCOLO ");
            Sql.AppendLine(", ps.T011_IN_SITUACAO ");
            Sql.AppendLine(", ps.T011_DT_SITUACAO ");
            Sql.AppendLine("FROM t005_protocolo p ");
            Sql.AppendLine("inner join t011_protocolo_status ps ");
            Sql.AppendLine("on ps.T005_NR_PROTOCOLO = p.T005_NR_PROTOCOLO ");
            Sql.AppendLine("WHERE T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine("AND p.T005_NR_PROTOCOLO = '" + wreq + "'");
            Sql.AppendLine(" order by ps.T011_DT_SITUACAO DESC");
            //Sql.AppendLine("AND ps.T011_IN_SITUACAO != 9 order by ps.T011_DT_SITUACAO DESC"); 02/05/2013
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                //               cmdToExecute.ExecuteNonQuery();
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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
                //if (_mainConnectionIsCreatedLocal)
                //{
                //    // Close connection.
                //    _mainConnection.Close();
                //}
                //cmdToExecute.Dispose();
            }
        }

        /// <summary>
        /// Atualiza o número do DBE no requerimento.
        /// </summary>
        /// <param name="wProtocolo">Parâmetro número do requerimento.</param>
        /// <param name="wDBE">Número do DBE da RFB.</param>
        public void UpdateDBEProtocolo(string wProtocolo, string wDBE)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Update t005_protocolo set T005_NR_DBE = '" + wDBE + "'");
            Sql.AppendLine(" Where T005_NR_PROTOCOLO = '" + wProtocolo + "'");
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        #endregion
    }

    
}