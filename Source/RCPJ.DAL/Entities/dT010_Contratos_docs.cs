using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    //Teste SVN
    public class dT010_Contratos_docs : DBInteractionBase
    {
        #region Propriedades da classe
            protected int _t010_id = 0;
            protected string _t005_nr_protocolo = string.Empty;
            protected int _t010_tipo_documento = 0;
            protected byte[] _t010_documento;
            protected string _t010_descricao_doc = string.Empty;
            protected string _t010_tipo_arquivo = string.Empty;
            protected int _num_paginas;
            protected int _num_vias;
            protected string _t004_nr_cnpj_org_reg = string.Empty;
            protected string _t010_nome_arquivo = string.Empty;
            protected string _t010_capital_nao_integralizado = string.Empty;
        #endregion
        #region Membros da classe
            public int t010_id
            {
                get { return _t010_id; }
                set { _t010_id = value; }
            }

            public string t005_nr_protocolo
            {
                get { return _t005_nr_protocolo; }
                set { _t005_nr_protocolo = value; }
            }
            public int t010_tipo_documento
            {
                get { return _t010_tipo_documento; }
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
            public string t010_tipo_arquivo
            {
                get { return _t010_tipo_arquivo; }
                set { _t010_tipo_arquivo = value; }
            }
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
            public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }
            set { _t004_nr_cnpj_org_reg = value; }
        }
            public string t010_nome_arquivo
        {
            get { return _t010_nome_arquivo; }
            set { _t010_nome_arquivo = value; }
        }
        public string t010_capital_nao_integralizado
        {
            get { return _t010_capital_nao_integralizado; }
            set { _t010_capital_nao_integralizado = value; }
        }

        #endregion
        #region Implementação
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t010_contratos_docs");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" t005_nr_protocolo, ");
            SqlI.AppendLine(" t010_tipo_documento, ");
            SqlI.AppendLine(" t010_descricao_doc, ");
            SqlI.AppendLine(" t010_tipo_arquivo, ");
            SqlI.AppendLine(" t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine(" t010_nome_arquivo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" @v_t005_nr_protocolo, ");
            SqlI.AppendLine(" @v_t010_tipo_documento, ");
            SqlI.AppendLine(" @v_t010_descricao_doc, ");
            SqlI.AppendLine(" @v_t010_tipo_arquivo, ");
            SqlI.AppendLine(" @v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine(" @v_t010_nome_arquivo ");
            SqlI.AppendLine("  )");
            //
            SqlU.AppendLine(" UPDATE t010_contratos_docs SET ");
            SqlU.AppendLine(" t005_nr_protocolo = @v_t005_nr_protocolo, ");
            SqlU.AppendLine(" t010_tipo_documento = @v_t010_tipo_documento, ");
            SqlU.AppendLine(" t010_descricao_doc = @v_t010_descricao_doc, ");
            SqlU.AppendLine(" t010_tipo_arquivo = @v_t010_tipo_arquivo, ");
            SqlU.AppendLine(" t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            SqlU.AppendLine(" t010_nome_arquivo = @v_t010_nome_arquivo ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And   t005_nr_protocolo = '" + _t005_nr_protocolo + "'");
            SqlU.AppendLine(" And t010_nome_arquivo = '" + _t010_nome_arquivo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.CommandTimeout = 50;
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_documento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_documento));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_documento", MySqlDbType.LongBlob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_descricao_doc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_descricao_doc));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_arquivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_nome_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_nome_arquivo));

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
                updatePag_vias();
            }
        }

        public void Insert()
        {
            StringBuilder SqlI = new StringBuilder();

            SqlI.AppendLine(" Insert into t010_contratos_docs");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" t005_nr_protocolo, ");
            SqlI.AppendLine(" t010_tipo_documento, ");
            SqlI.AppendLine(" t010_documento, ");
            SqlI.AppendLine(" t010_descricao_doc, ");
            SqlI.AppendLine(" t010_tipo_arquivo, ");
            SqlI.AppendLine(" t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine(" t010_nome_arquivo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine(" @v_t005_nr_protocolo, ");
            SqlI.AppendLine(" @v_t010_tipo_documento, ");
            SqlI.AppendLine(" @v_t010_documento, ");
            SqlI.AppendLine(" @v_t010_descricao_doc, ");
            SqlI.AppendLine(" @v_t010_tipo_arquivo, ");
            SqlI.AppendLine(" @v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine(" @v_t010_nome_arquivo ");
            SqlI.AppendLine("  )");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.CommandTimeout = 50;
            
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_documento", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_documento", MySqlDbType.LongBlob, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_documento));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_descricao_doc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_descricao_doc));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_tipo_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_tipo_arquivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_t010_nome_arquivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t010_nome_arquivo));

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
            SqlU.AppendLine(" And   t005_nr_protocolo = @v_t005_nr_protocolo");
          
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

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

                cmdToExecute.ExecuteNonQuery();

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
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" AND t005_nr_protocolo ='" + _t005_nr_protocolo + "'");
            if (_t010_nome_arquivo != string.Empty && _t010_nome_arquivo != null)
                Sql.AppendLine(" AND t010_nome_arquivo = '" + _t010_nome_arquivo + "'");
            Sql.AppendLine(" order by t010_nome_arquivo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Cnt_prot_documentos");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                _t010_nome_arquivo = string.Empty;
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
        public DataTable QueryById()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" AND t005_nr_protocolo ='" + _t005_nr_protocolo + "'");
            Sql.AppendLine(" AND T010_1D = " + _t010_id );
            

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Cnt_prot_documentos");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);
                _t010_nome_arquivo = string.Empty;
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
        public DataTable Delete()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" AND t005_nr_protocolo ='" + _t005_nr_protocolo + "'");
            if (_t010_nome_arquivo != string.Empty && _t010_nome_arquivo != null)
                Sql.AppendLine(" AND t010_nome_arquivo = '" + _t010_nome_arquivo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Cnt_prot_documentos");
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
        public DataTable DeleteById()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Delete ");
            Sql.AppendLine(" From	t010_contratos_docs");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" AND T010_1D = '" + _t010_id + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Cnt_prot_documentos");
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

