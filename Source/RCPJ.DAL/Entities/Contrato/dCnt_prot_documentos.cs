using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_prot_documentos : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        //private string _protocolo;
        //private string _reque_protocolo;
        //private string _cnpj_org_reg;
        //private string _nome_arquivo;
        //private byte[] _binaries_arquivo;
        //private string _tipo_documento;
        private int _num_paginas;
        private int _num_vias;
        private string _t005_nr_protocolo;
        private int _t010_tipo_documento;
        private byte[] _t010_documento;
        private string _t010_descricao_doc;
        private string _t010_nome_arquivo;
        private string _t010_tipo_arquivo;
        private string _t004_nr_cnpj_org_reg;
        #endregion

        #region Class Member Declarations

        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }
            set { _t005_nr_protocolo = value; }
        }
        public int t010_tipo_documento
        {
            get {return _t010_tipo_documento;}
            set { _t010_tipo_documento = value; }
        }
        public byte[] t010_documento
        {
            get { return _t010_documento; }
            set { _t010_documento = value; }
        }
        public string t010_descricao_doc
        {
            get { return _t010_descricao_doc; }
            set { _t010_descricao_doc = value; }
        }
        public string t010_nome_arquivo
        {
            get { return _t010_nome_arquivo; }
            set { _t010_nome_arquivo = value; }
        }
        public string t010_tipo_arquivo
        {
            get { return _t010_tipo_arquivo; }
            set { _t010_tipo_arquivo = value; }
        }
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }
            set { _t004_nr_cnpj_org_reg = value; }
        }
        //public string Protocolo
        //{
        //    get { return _protocolo; }
        //    set { _protocolo = value; }
        //}

        //public string nome_arquivo
        //{
        //    get { return _nome_arquivo; }
        //    set { _nome_arquivo = value; }
        //}

        //public byte[] binaries_arquivo
        //{
        //    get { return _binaries_arquivo; }
        //    set { _binaries_arquivo = value; }
        //}


        //public string tipo_documento
        //{
        //    get { return _tipo_documento; }
        //    set { _tipo_documento = value; }
        //}

        //public string reque_protocolo
        //{
        //    get { return _reque_protocolo; }
        //    set { _reque_protocolo = value; }
        //}
        //public string cnpj_org_reg
        //{
        //    get { return _cnpj_org_reg; }
        //    set { _cnpj_org_reg = value; }
        //}
        public int num_paginas
        {
            get { return _num_paginas; }
            set { _num_paginas = value; }
        }
        public int num_vias
        {
            get { return _num_vias; }
            set { _num_vias = value; }
        }
  
        #endregion


        #region Implements
        /// <summary>
        /// Inseri ou  Atualiza o socio na base
        /// </summary>
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into t010_contratos_docs");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" t005_nr_protocolo, ");
            SqlI.AppendLine(" t010_tipo_documento, ");
            SqlI.AppendLine(" t010_documento, ");
            SqlI.AppendLine(" t010_descricao_doc, ");
            SqlI.AppendLine(" t010_nome_arquivo, ");
            SqlI.AppendLine(" t010_tipo_arquivo, ");
            SqlI.AppendLine(" t004_nr_cnpj_org_reg");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" @v_t005_nr_protocolo, ");
            SqlI.AppendLine(" @v_t010_tipo_documento, ");
            SqlI.AppendLine(" @v_t010_documento, ");
            SqlI.AppendLine(" @v_t010_descricao_doc, ");
            SqlI.AppendLine(" @v_t010_nome_arquivo, ");
            SqlI.AppendLine(" @v_t010_tipo_arquivo, ");
            SqlI.AppendLine(" @v_t004_nr_cnpj_org_reg ");
            SqlI.AppendLine("  )");
                
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_documento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_documento", MySqlDbType.LongBlob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_descricao_doc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_descricao_doc));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_nome_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_nome_arquivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_arquivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg)); 
                
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
                updatePag_vias();
            }
        }

        public void updatePag_vias()
        {
            StringBuilder SqlU = new StringBuilder();
           
            SqlU.AppendLine(" UPDATE t006_protocolo_requerimento SET ");
            SqlU.AppendLine(" T006_NR_NUM_PAGINAS = @v_num_paginas, ");
            SqlU.AppendLine(" T006_NR_NUM_VIAS = @v_num_vias ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And   t005_nr_protocolo = '" + _t005_nr_protocolo + "'");
            
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_num_paginas", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_paginas));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_num_vias", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_vias));
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
                //if (cmdToExecute.ExecuteNonQuery() == 0)
                //{
                //    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();
                //}
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

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine(" t005_nr_protocolo, ");
            Sql.AppendLine(" t010_tipo_documento, ");
            Sql.AppendLine(" t010_documento, ");
            Sql.AppendLine(" t010_descricao_doc, ");
            Sql.AppendLine(" t010_nome_arquivo, ");
            Sql.AppendLine(" t010_tipo_arquivo, "); 
            Sql.AppendLine(" t004_nr_cnpj_org_reg ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");

           
            if (!string.IsNullOrEmpty(_t005_nr_protocolo))
            {
                Sql.AppendLine(" And	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");
            }
            if (!string.IsNullOrEmpty(t010_nome_arquivo))
            {
                Sql.AppendLine(" And	t010_nome_arquivo = '" + _t010_nome_arquivo + "'");
            }
            
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t010_contratos_docs");
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

        public DataTable QuerySemBinaries()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine(" t005_nr_protocolo, ");
            Sql.AppendLine(" t010_nome_arquivo, ");

            Sql.AppendLine(" t010_tipo_arquivo ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            if (!string.IsNullOrEmpty(_t005_nr_protocolo))
            {
                Sql.AppendLine(" And	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");
            }
            if (!string.IsNullOrEmpty(_t010_tipo_arquivo))
            {
                Sql.AppendLine(" And	t010_tipo_arquivo = '" + _t010_tipo_arquivo + "'");
            }
            /*  Sql.AppendLine(" And	nome_socio = _nome_socio");
              Sql.AppendLine(" And	nome_mae = _nome_mae");
              Sql.AppendLine(" And	tipo_socio = _tipo_socio");
              Sql.AppendLine(" And	nacionalidade = _nacionalidade");
              Sql.AppendLine(" And	naturalidade = _naturalidade");
              Sql.AppendLine(" And	estado_civil = _estado_civil");
              Sql.AppendLine(" And	regime_bens = _regime_bens");
              Sql.AppendLine(" And	data_nasc = _data_nasc");
              Sql.AppendLine(" And	tipo_doc_ident = _tipo_doc_ident");
              Sql.AppendLine(" And	no_doc_ident = _no_doc_ident");
              Sql.AppendLine(" And	orgao_exped = _orgao_exped");
              Sql.AppendLine(" And	tipo_logradouro = _tipo_logradouro");
              Sql.AppendLine(" And	logradouro = _logradouro");
              Sql.AppendLine(" And	no_logradouro = _no_logradouro");
              Sql.AppendLine(" And	bairro = _bairro");
              Sql.AppendLine(" And	municipio = _municipio");
              Sql.AppendLine(" And	cep = _cep");
              Sql.AppendLine(" And	uf = _uf");
              Sql.AppendLine(" And	profissao = _profissao");
              Sql.AppendLine(" And	uf_orgao_exped = _uf_orgao_exped");
              Sql.AppendLine(" And	qtd_cotas = _qtd_cotas");
              Sql.AppendLine(" And	adm = _adm"); */


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t010_contratos_docs");
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

        #endregion

    }
}
