using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dA002_Ato : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _a002_co_ato;
        protected string _a002_ds_ato;
        protected string _a002_in_viabilidade;
        protected string _a002_in_listar;
        protected string _a002_in_socios;
        #endregion

        #region Class Member Declarations
        public decimal a002_co_ato
        {
            get { return _a002_co_ato; }

            set { _a002_co_ato = value; }
        }

        public string a002_ds_ato
        {
            get { return _a002_ds_ato; }

            set { _a002_ds_ato = value; }
        }

        public string a002_in_viabilidade
        {
            get { return _a002_in_viabilidade; }

            set { _a002_in_viabilidade = value; }
        }

        public string a002_in_listar
        {
            get { return _a002_in_listar; }

            set { _a002_in_listar = value; }
        }

        public string a002_in_socios
        {
            get { return _a002_in_socios; }
            set { _a002_in_socios = value; }
        }
        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into a002_ato");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	a002_co_ato, ");
            SqlI.AppendLine("	a002_ds_ato, ");
            SqlI.AppendLine("	a002_in_viabilidade, ");
            SqlI.AppendLine("	a002_in_listar, ");
            SqlI.AppendLine("   a002_in_socios");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_a002_co_ato, ");
            SqlI.AppendLine("	@v_a002_ds_ato, ");
            SqlI.AppendLine("	@v_a002_in_viabilidade, ");
            SqlI.AppendLine("	@v_a002_in_listar, ");
            SqlI.AppendLine("   @v_a002_in_socios");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     A002_Ato Set ");
            SqlU.AppendLine("		a002_co_ato = @v_a002_co_ato, ");
            SqlU.AppendLine("		a002_ds_ato = @v_a002_ds_ato, ");
            SqlU.AppendLine("		a002_in_viabilidade = @v_a002_in_viabilidade, ");
            SqlU.AppendLine("		a002_in_listar = @v_a002_in_listar, ");
            SqlU.AppendLine("       a002_in_socios = @v_a002_in_socios");
            SqlU.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!!


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a002_in_socios", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_socios));

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

        public DataTable getAtos()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a002_co_ato, ");
            Sql.AppendLine("		a002_ds_ato, ");
            Sql.AppendLine("		a002_in_viabilidade, ");
            Sql.AppendLine("		a002_in_listar, ");
            Sql.AppendLine("        a002_in_socios");
            Sql.AppendLine(" From	A002_Ato");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A002_Ato");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_socios", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_socios));

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

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a002_co_ato, ");
            Sql.AppendLine("		a002_ds_ato, ");
            Sql.AppendLine("		a002_in_viabilidade, ");
            Sql.AppendLine("		a002_in_listar, ");
            Sql.AppendLine("        a002_in_socios");
            Sql.AppendLine(" From	A002_Ato");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	a002_co_ato = @a002_co_ato");
            if (_a002_ds_ato != null && _a002_ds_ato!="")
                Sql.AppendLine(" And	a002_ds_ato = @a002_ds_ato");
            if (_a002_in_viabilidade != null && _a002_in_viabilidade != "") 
                Sql.AppendLine(" And	a002_in_viabilidade = @a002_in_viabilidade");
            if (_a002_in_listar != null && _a002_in_listar != "") 
                Sql.AppendLine(" And	a002_in_listar = @a002_in_listar");
            if (_a002_in_socios != null && _a002_in_socios != "") 
                Sql.AppendLine(" And	a002_in_socios = @a002_in_socios");

            if (_a002_co_ato == 0)
                Sql.AppendLine(" And a002_co_ato = -1");
            else
                Sql.AppendLine(" And a002_co_ato = @a002_co_ato");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A002_Ato");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_socios", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_socios));

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
        public DataTable QueryAtoEventoRegistro()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select  ");
            Sql.AppendLine("		rEOR.R007_VL_EVENTO as valor, ");
            Sql.AppendLine("		rAE.R008_IN_FATOR_MULTIPLICADOR as multiplica, ");
            Sql.AppendLine("        rAOR.R006_DS_ATO_BOLETO as descricao ");
            Sql.AppendLine(" from   r006_ato_orgao_registro rAOR,  ");
            Sql.AppendLine("        r007_evento_orgao_registro rEOR, ");
            Sql.AppendLine("        r008_ato_evento rAE ");
            Sql.AppendLine(" where  ");
            Sql.AppendLine("        rAOR.A002_CO_ATO = 101  ");
            Sql.AppendLine("        and rAOR.A002_CO_ATO = rAE.A002_CO_ATO  ");
            Sql.AppendLine("        and rAE.A003_CO_EVENTO = rEOR.A003_CO_EVENTO  ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("AtoEventoMultiplicaValor");
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

        public DataTable QueryAtosAlteracao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a002_co_ato as codigo, ");
            Sql.AppendLine("		a002_ds_ato as descricao, ");
            Sql.AppendLine("		a002_in_viabilidade, ");
            Sql.AppendLine("		a002_in_listar");
            Sql.AppendLine(" From	A002_Ato");
            Sql.AppendLine(" Where	");
            Sql.AppendLine(" a002_in_listar != 'N'	");
            Sql.AppendLine(" order by a002_ds_ato ");
            //Sql.AppendLine(" and a002_in_Socios = 'S' ");
            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	a002_co_ato = @a002_co_ato");
            //Sql.AppendLine(" And	a002_ds_ato = @a002_ds_ato");
            //Sql.AppendLine(" And	a002_in_viabilidade = @a002_in_viabilidade");
            //Sql.AppendLine(" And	a002_in_listar = @a002_in_listar");

            //if (_a002_co_ato == 0)
            //    Sql.AppendLine(" And a002_co_ato = -1");
            //else
            //    Sql.AppendLine(" a002_co_ato = @a002_co_ato");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A002_Ato");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));


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

        public DataTable QueryAtosSocios()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a002_co_ato as codigo, ");
            Sql.AppendLine("		a002_ds_ato as descricao, ");
            Sql.AppendLine("		a002_in_viabilidade, ");
            Sql.AppendLine("		a002_in_socios");
            Sql.AppendLine(" From	A002_Ato");
            Sql.AppendLine(" Where	");
            Sql.AppendLine(" a002_in_listar != 'N' ");
            Sql.AppendLine(" and a002_in_socios = 'S'	");
            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	a002_co_ato = @a002_co_ato");
            //Sql.AppendLine(" And	a002_ds_ato = @a002_ds_ato");
            //Sql.AppendLine(" And	a002_in_viabilidade = @a002_in_viabilidade");
            //Sql.AppendLine(" And	a002_in_listar = @a002_in_listar");
           
            //if (_a002_co_ato == 0)
            //    Sql.AppendLine(" And a002_co_ato = -1");
            //else
            //    Sql.AppendLine(" a002_co_ato = @a002_co_ato");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A002_Ato");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));


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

        public DataTable VerSeNecessitaViabilidade(int tipo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a002_co_ato as codigo, ");
            Sql.AppendLine("		a002_ds_ato as descricao, ");
            Sql.AppendLine("		a002_in_viabilidade, ");
            Sql.AppendLine("		a002_in_socios");
            Sql.AppendLine(" From	A002_Ato");
            Sql.AppendLine(" Where	");
            Sql.AppendLine(" a002_co_ato = " + tipo);
            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	a002_co_ato = @a002_co_ato");
            //Sql.AppendLine(" And	a002_ds_ato = @a002_ds_ato");
            //Sql.AppendLine(" And	a002_in_viabilidade = @a002_in_viabilidade");
            //Sql.AppendLine(" And	a002_in_listar = @a002_in_listar");

            //if (_a002_co_ato == 0)
            //    Sql.AppendLine(" And a002_co_ato = -1");
            //else
            //    Sql.AppendLine(" a002_co_ato = @a002_co_ato");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A002_Ato");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_co_ato", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_co_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_ds_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_ds_ato));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_viabilidade));
                //cmdToExecute.Parameters.Add(new MySqlParameter("@a002_in_listar", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a002_in_listar));


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


