using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dT017_Protocolo_Confirmacao : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations

        protected string _t005_nr_protocolo="";
        protected decimal _t017_sequencia=-1;
        protected decimal _t017_grupo=-1;
        protected decimal _t017_item=-1;
        protected decimal _t017_status=-1;
        protected decimal _t017_confirma=-1;
        protected string _t017_usuario="";
        protected DateTime _t017_dt_confirmacao=DateTime.MinValue;
        protected string _t017_motivo = "";
        protected string _t017_andamento_secao = "";
        protected int _t017_andamento_seq = 0;
        #endregion

        #region Class Member Declarations
        public string t005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }

            set { _t005_nr_protocolo = value; }
        }

        public decimal t017_sequencia
        {
            get { return _t017_sequencia; }

            set { _t017_sequencia = value; }
        }

        public decimal t017_grupo
        {
            get { return _t017_grupo; }

            set { _t017_grupo = value; }
        }

        public decimal t017_item
        {
            get { return _t017_item; }

            set { _t017_item = value; }
        }

        public decimal t017_status
        {
            get { return _t017_status; }

            set { _t017_status = value; }
        }

        public decimal t017_confirma
        {
            get { return _t017_confirma; }

            set { _t017_confirma = value; }
        }

        public string t017_usuario
        {
            get { return _t017_usuario; }

            set { _t017_usuario = value; }
        }

        public DateTime t017_dt_confirmacao
        {
            get { return _t017_dt_confirmacao; }

            set { _t017_dt_confirmacao = value; }
        }
        public string t017_motivo
        {
            get { return _t017_motivo; }
            set { _t017_motivo = value; }
        }
        public string T017_andamento_secao
        {
            get { return _t017_andamento_secao; }
            set { _t017_andamento_secao = value; }
        }
        public int T017_andamento_seq
        {
            get { return _t017_andamento_seq; }
            set { _t017_andamento_seq = value; }
        }
        #endregion


        #region Implements


 


        public DataTable GetConfirmacaoFCN(string pProtocolo, string pSecao)
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(@"SELECT * from 
                              t017_protocolo_confirmacao c
                            WHERE ");
            Sql.AppendLine("  T005_NR_PROTOCOLO = '" + pProtocolo + "' ");
            Sql.AppendLine("    AND c.T017_SEQUENCIA = ");
            Sql.AppendLine("      (SELECT max(c.T017_SEQUENCIA) seq ");
            Sql.AppendLine("       FROM t017_protocolo_confirmacao c ");
            Sql.AppendLine("       WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "' ");
            Sql.AppendLine("             and T017_ANDAMENTO_SECAO = '" + pSecao + "')");
                               
            try
            {

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao_FCN");

                cmdToExecute.Connection = _mainConnection;

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


        public DataTable GetUltimaConfirmacao(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(@"SELECT c.T017_SEQUENCIA
                                 , c.T017_ANDAMENTO_SECAO
                                 , c.T017_ANDAMENTO_SEQ
                            FROM
                              t017_protocolo_confirmacao c
                            WHERE
                              T005_NR_PROTOCOLO = '" + pProtocolo + "' "); 
            Sql.AppendLine("     AND c.T017_SEQUENCIA = ");
            Sql.AppendLine("      (SELECT max(c.T017_SEQUENCIA) seq ");
            Sql.AppendLine("       FROM t017_protocolo_confirmacao c ");
            Sql.AppendLine("       WHERE T005_NR_PROTOCOLO = '" + pProtocolo + "')");
            Sql.AppendLine("            limit 1");

            try
            {

                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao");

                cmdToExecute.Connection = _mainConnection;

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

        public DataTable GetUltimaConfirmacao(string pProtocolo, string pSecao)
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(@"SELECT distinct t005_nr_protocolo
                                 , t017_usuario
                                 , t017_dt_confirmacao
                                 , t017_andamento_seq
                                 , t017_andamento_secao
                            FROM T017_Protocolo_Confirmacao ");
            Sql.AppendLine(" WHERE t005_nr_protocolo = '" + pProtocolo + "'");
            Sql.AppendLine("       and t017_andamento_secao = '" + pSecao + "'");
            Sql.AppendLine("       order by t017_andamento_seq desc limit 1");

            try
            {

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao");

                cmdToExecute.Connection = _mainConnection;

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

        public int BuscaSequencia(string wProtocolo)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            sql.AppendLine("SELECT * FROM  t017_protocolo_confirmacao c");
            sql.AppendLine("WHERE c.T005_NR_PROTOCOLO = '" + wProtocolo + "'");
            sql.AppendLine("ORDER BY T017_SEQUENCIA DESC");
            try
            {
              cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable dt = new DataTable("T017_Protocolo_Confirmacao");

                // Use base class' connection object 
                cmdToExecute.Connection = _mainConnection;

                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(dt);
                int ii = 0;
                if (dt.Rows.Count > 0)
                    ii = int.Parse(dt.Rows[0]["t017_sequencia"].ToString());
                ii++;
                return ii;
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

            SqlI.AppendLine(" Insert into t017_protocolo_confirmacao");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t005_nr_protocolo, ");
            SqlI.AppendLine("	t017_sequencia, ");
            SqlI.AppendLine("	t017_grupo, ");
            SqlI.AppendLine("	t017_item, ");
            SqlI.AppendLine("	t017_status, ");
            SqlI.AppendLine("	t017_confirma, ");
            SqlI.AppendLine("	t017_usuario, ");
            SqlI.AppendLine("	t017_dt_confirmacao, ");
            SqlI.AppendLine("   t017_motivo,");
            SqlI.AppendLine("   t017_andamento_secao,");
            SqlI.AppendLine("   t017_andamento_seq");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t005_nr_protocolo, ");
            SqlI.AppendLine("	@v_t017_sequencia, ");
            SqlI.AppendLine("	@v_t017_grupo, ");
            SqlI.AppendLine("	@v_t017_item, ");
            SqlI.AppendLine("	@v_t017_status, ");
            SqlI.AppendLine("	@v_t017_confirma, ");
            SqlI.AppendLine("	@v_t017_usuario, ");
            SqlI.AppendLine("	@v_t017_dt_confirmacao, ");
            SqlI.AppendLine("   @v_t017_motivo, ");
            SqlI.AppendLine("   @v_t017_andamento_secao, ");
            SqlI.AppendLine("   @v_t017_andamento_seq");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T017_Protocolo_Confirmacao Set ");
            SqlU.AppendLine("		t017_status = @v_t017_status, ");
            SqlU.AppendLine("		t017_confirma = @v_t017_confirma, ");
            SqlU.AppendLine("		t017_usuario = @v_t017_usuario, ");
            SqlU.AppendLine("		t017_dt_confirmacao = @v_t017_dt_confirmacao, ");
            SqlU.AppendLine("       t017_motivo = @v_t017_motivo,");
            SqlU.AppendLine("       t017_andamento_secao = @v_t017_andamento_secao, ");
            SqlU.AppendLine("       t017_andamento_seq = @v_t017_andamento_seq ");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And t005_nr_protocolo = @v_t005_nr_protocolo ");
            SqlU.AppendLine(" And t017_sequencia = @v_t017_sequencia ");
            SqlU.AppendLine(" And t017_grupo = @v_t017_grupo ");
            SqlU.AppendLine(" And t017_item = @v_t017_item ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_sequencia", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_sequencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_grupo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_grupo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_item", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_item));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_status", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_status));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_confirma", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_confirma));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_usuario", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_dt_confirmacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_dt_confirmacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_motivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_motivo));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_andamento_secao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_andamento_secao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_andamento_seq", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_andamento_seq));
                
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
        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t005_nr_protocolo, ");
            Sql.AppendLine("		t017_sequencia, ");
            Sql.AppendLine("		t017_grupo, ");
            Sql.AppendLine("		t017_item, ");
            Sql.AppendLine("		t017_status, ");
            Sql.AppendLine("		t017_confirma, ");
            Sql.AppendLine("		t017_usuario, ");
            Sql.AppendLine("		t017_dt_confirmacao, ");
            Sql.AppendLine("        t017_motivo, ");
            Sql.AppendLine("        T017_ANDAMENTO_SEQ, ");
            Sql.AppendLine("        T017_ANDAMENTO_SECAO");
            Sql.AppendLine(" From	T017_Protocolo_Confirmacao");
            Sql.AppendLine(" Where	1 = 1 ");

            if (_t005_nr_protocolo != "")
                Sql.AppendLine(" And	t005_nr_protocolo = @v_t005_nr_protocolo");
            
            if (_t017_grupo != -1)
                Sql.AppendLine(" And	t017_grupo = @v_t017_grupo");
            if (_t017_item != -1)
                Sql.AppendLine(" And	t017_item = @v_t017_item");
            if (_t017_status != -1)
                Sql.AppendLine(" And	t017_status = @v_t017_status");
            if (_t017_confirma != -1)
                Sql.AppendLine(" And	t017_confirma = @v_t017_confirma");
            if (_t017_usuario != "")
                Sql.AppendLine(" And	t017_usuario = @v_t017_usuario");
            if (_t017_dt_confirmacao != DateTime.MinValue)
                Sql.AppendLine(" And t017_dt_confirmacao = @v_t017_dt_confirmacao");

            if (_t017_sequencia != -1)
            {
                Sql.AppendLine(" And t017_sequencia = @v_t017_sequencia");
            }
            else
            {

                Sql.AppendLine(" AND T017_SEQUENCIA = ( ");
                Sql.AppendLine(" SELECT max(t017_sequencia) AS 'T017_SEQUENCIA' ");
                Sql.AppendLine(" FROM "); 
                Sql.AppendLine(" t017_protocolo_confirmacao ");
                Sql.AppendLine(" WHERE ");
                Sql.AppendLine(" T005_NR_PROTOCOLO = @v_t005_nr_protocolo");
                if (_t017_andamento_secao != "")
                {
                    Sql.AppendLine(" AND T017_andamento_secao = @v_t017_andamento_secao");
                }
                Sql.AppendLine(") ");
                if (_t017_andamento_secao != "")
                {
                    Sql.AppendLine(" And T017_andamento_secao = @v_t017_andamento_secao");
                }
            }


            try
            {
                // Codigo dbParameter ******************* 

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_sequencia", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_sequencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_grupo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_grupo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_item", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_item));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_status", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_status));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_confirma", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_confirma));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_usuario", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_dt_confirmacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_dt_confirmacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_motivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_motivo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_andamento_secao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_andamento_secao));


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao");

                // Use base class' connection object 
                cmdToExecute.Connection = _mainConnection;

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


        public void AddNewSequence()
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(@" INSERT INTO t017_protocolo_confirmacao 
                                (T005_NR_PROTOCOLO
                                ,T017_SEQUENCIA
                                ,T017_GRUPO
                                ,T017_ITEM
                                ,T017_STATUS
                                ,T017_CONFIRMA
                                ,T017_USUARIO
                                ,T017_DT_CONFIRMACAO
                                ,T017_MOTIVO)

            ");
            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t005_nr_protocolo, ");
            Sql.AppendLine("		(t017_sequencia+1) as 't017_sequencia', ");
            Sql.AppendLine("		t017_grupo, ");
            Sql.AppendLine("		t017_item, ");
            Sql.AppendLine("		t017_status, ");
            Sql.AppendLine("		t017_confirma, ");
            Sql.AppendLine("		t017_usuario, ");
            Sql.AppendLine("		t017_dt_confirmacao, ");
            Sql.AppendLine("        t017_motivo");
            Sql.AppendLine(" From	T017_Protocolo_Confirmacao");
            Sql.AppendLine(" Where	1 = 1 ");

            if (_t005_nr_protocolo != "")
                Sql.AppendLine(" And	t005_nr_protocolo = @v_t005_nr_protocolo");

            //if (_t017_grupo != -1)
            //    Sql.AppendLine(" And	t017_grupo = @v_t017_grupo");
            //if (_t017_item != -1)
            //    Sql.AppendLine(" And	t017_item = @v_t017_item");
            //if (_t017_status != -1)
            //    Sql.AppendLine(" And	t017_status = @v_t017_status");
            //if (_t017_confirma != -1)
            //    Sql.AppendLine(" And	t017_confirma = @v_t017_confirma");
            //if (_t017_usuario != "")
            //    Sql.AppendLine(" And	t017_usuario = @v_t017_usuario");
            //if (_t017_dt_confirmacao != DateTime.MinValue)
            //    Sql.AppendLine(" And t017_dt_confirmacao = @v_t017_dt_confirmacao");

            if (_t017_sequencia != -1)
            {
                Sql.AppendLine(" And t017_sequencia = @v_t017_sequencia");
            }
            else
            {

                Sql.AppendLine(" AND T017_SEQUENCIA = ( ");
                Sql.AppendLine(" SELECT max(t017_sequencia) AS 'T017_SEQUENCIA' ");
                Sql.AppendLine(" FROM ");
                Sql.AppendLine(" t017_protocolo_confirmacao ");
                Sql.AppendLine(" WHERE ");
                Sql.AppendLine(" T005_NR_PROTOCOLO = @v_t005_nr_protocolo) ");
            }


            try
            {
                // Codigo dbParameter ******************* 

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_sequencia", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_sequencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_grupo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_grupo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_item", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_item));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_status", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_status));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_confirma", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_confirma));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_usuario", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_usuario));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_dt_confirmacao", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_dt_confirmacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t017_motivo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t017_motivo));


                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao");

                // Use base class' connection object 
                cmdToExecute.Connection = _mainConnection;

                // Open connection. 
                _mainConnection.Open();

                // Execute query. 
                cmdToExecute.ExecuteNonQuery();
                //adapter.Fill(toReturn);
                //return toReturn;
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

        public void ApagaConfirmacao(decimal wSequencia, decimal wItem, string wProtocolo)
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            Sql.AppendLine(" Delete ");
                 Sql.AppendLine(" From	T017_Protocolo_Confirmacao");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And t017_sequencia = " + wSequencia);
            Sql.AppendLine(" And t017_item = " + wItem);
            Sql.AppendLine(" And t005_nr_protocolo = '" + wProtocolo + "'");

            try
            {
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("T017_Protocolo_Confirmacao");

                // Use base class' connection object 
                cmdToExecute.Connection = _mainConnection;

                // Open connection. 
              //  _mainConnection.Open();

                // Execute query. 
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

        public string GetUsuarioAndamento(string protocolo, string pSecao)
        {
            StringBuilder sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            string retorno = "";


            sql.AppendLine(" SELECT t017_usuario ");
            sql.AppendLine(" FROM t017_protocolo_confirmacao ");
            sql.AppendLine(" where t017_andamento_secao = '" + pSecao + "'" );
            sql.AppendLine("       and t005_nr_protocolo = '" + protocolo + "'");
            sql.AppendLine("       limit 1 ");
            try
            {
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("GetUsuarioAndamento");

                // Use base class' connection object 
                cmdToExecute.Connection = _mainConnection;

                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                adapter.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                    retorno = toReturn.Rows[0]["t017_usuario"].ToString();

                return retorno;
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
