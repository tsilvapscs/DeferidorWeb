using System;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dcnt_clausulas_adicionais : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _cod_protocolo;
        protected string _num_clausula;
        protected string _num_clausula_novo;
        protected string _clausula;
        protected string _titulo_clausula;
        protected string _reque_protocolo;
        protected int _sequencia;
        #endregion

        #region Class Member Declarations
        public string cod_protocolo
        {
            get { return _cod_protocolo; }

            set { _cod_protocolo = value; }
        }

        public string num_clausula
        {
            get { return _num_clausula; }

            set { _num_clausula = value; }
        }
        public string num_clausula_novo
        {
            get { return _num_clausula_novo; }

            set { _num_clausula_novo = value; }
        }
        public string clausula
        {
            get { return _clausula; }

            set { _clausula = value; }
        }

        public string titulo_clausula
        {
            get { return _titulo_clausula; }
            set { _titulo_clausula = value; }
        }
        public string reque_protocolo
        {
            get { return _reque_protocolo; }
            set { _reque_protocolo = value; }
        }
        public int sequencia
        {
            get { return _sequencia; }
            set { _sequencia = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t007_clausulas_adicionais");
            SqlI.AppendLine("  (");
            //SqlI.AppendLine("	T005_nr_protocolo_viabilidade ");
            SqlI.AppendLine("	t007_num_clausula ");
            SqlI.AppendLine("	,t007_clausula ");
            SqlI.AppendLine("   ,t007_titulo_clausula");
            SqlI.AppendLine("   ,t005_nr_protocolo");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            //SqlI.AppendLine("	@v_cod_protocolo ");
            SqlI.AppendLine("	@v_num_clausula ");
            SqlI.AppendLine("	,@v_clausula ");
            SqlI.AppendLine("	,@v_titulo_clausula");
            SqlI.AppendLine("	,@v_reque_protocolo");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     t007_clausulas_adicionais Set ");
            //SqlU.AppendLine("		T005_nr_protocolo_viabilidade = @v_cod_protocolo, ");
            SqlU.AppendLine("		t007_num_clausula = @v_num_clausula, ");
            SqlU.AppendLine("		t007_clausula = @v_clausula, ");
            SqlU.AppendLine("		t007_titulo_clausula = @v_titulo_clausula, ");
            SqlU.AppendLine("		t005_nr_protocolo = @v_reque_protocolo ");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!
            //SqlU.AppendLine(" And	T005_nr_protocolo_viabilidade = '" + _cod_protocolo + "'");
            SqlU.AppendLine(" And	t005_nr_protocolo = '" + _reque_protocolo + "'");
            SqlU.AppendLine(" And	t007_num_clausula = '" + _num_clausula + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                //cmdToExecute.Parameters.Add(new MySqlParameter("@v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_num_clausula", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_clausula", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_titulo_clausula", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _titulo_clausula));
                cmdToExecute.Parameters.Add(new MySqlParameter("@v_reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reque_protocolo));

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

            Sql.AppendLine(@"SELECT ca.t007_num_clausula                AS NUM_CLAUSULA
                                   , ca.t007_clausula                   AS CLAUSULA
                                   , ca.t007_titulo_clausula            AS TITULO_CLAUSULA
                                   , ca.t005_nr_protocolo               AS REQUE_PROTOCOLO
                                   , t.T005_NR_PROTOCOLO_VIABILIDADE    AS COD_PROTOCOLO
                                   , ca.t007_sq_clausula sequencia
                           FROM     t007_clausulas_adicionais ca
                                    JOIN t005_protocolo t
                                        ON t.T005_NR_PROTOCOLO = ca.t005_nr_protocolo
                            Where	1 = 1 ");

            Sql.AppendLine(" And	ca.t005_nr_protocolo = '" + _reque_protocolo + "'");

            if (!string.IsNullOrEmpty(num_clausula) && num_clausula == "99")
                Sql.AppendLine(" And	ca.t007_num_clausula = '" + _num_clausula + "'");
            else
                Sql.AppendLine(" And	ca.t007_num_clausula != '99'");

            Sql.AppendLine(" Order By ca.t007_num_clausula");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t007_clausulas_adicionais");
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

        public int RecuperaUltimaNumeracaoClausula()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@" Select 
		                            T005_nr_protocolo_viabilidade as cod_protocolo, 
		                            t007_num_clausula             as num_clausula, 
		                            t007_clausula                 as clausula, 
		                            t007_titulo_clausula          as titulo_clausula,
		                            t005_nr_protocolo             as reque_protocolo
                             From	t007_clausulas_adicionais
                             Where	1 = 1 
                               And	 t005_nr_protocolo = '" + _reque_protocolo + "'");
            Sql.AppendLine("Order By  t007_num_clausula DESC");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t007_clausulas_adicionais");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count == 0)
                    return 0;
                else
                    return Convert.ToInt32(toReturn.Rows[0]["num_clausula"].ToString());
               
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


        public void Delete()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" DELETE");
            Sql.AppendLine(" From	t007_clausulas_adicionais");
            Sql.AppendLine(" Where	");
            Sql.AppendLine(" t005_nr_protocolo = '" + _reque_protocolo + "'");
            Sql.AppendLine(" And	t007_num_clausula = '" + _num_clausula + "'" );



            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t007_clausulas_adicionais");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

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
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void AtualizaNumeroCalusula()
        {
            StringBuilder SqlU = new StringBuilder();

           
            SqlU.AppendLine(" Update  t007_clausulas_adicionais Set ");
            SqlU.AppendLine("		  t007_num_clausula = @v_num_clausula_novo ");
            SqlU.AppendLine(" Where	");
            SqlU.AppendLine(" 	    t005_nr_protocolo = '" + _reque_protocolo + "'");
            SqlU.AppendLine(" And	t007_sq_clausula= " + _sequencia.ToString()  );

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("@v_num_clausula_novo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_clausula_novo));

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
        #endregion
    }
} 


