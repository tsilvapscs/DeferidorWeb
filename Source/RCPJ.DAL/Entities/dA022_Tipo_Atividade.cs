using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dA022_Tipo_Atividade : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _a022_co_tipo_atividade;
        protected string _a022_ds_tipo_atividade;
        #endregion

        #region Class Member Declarations
        public decimal a022_co_tipo_atividade
        {
            get { return _a022_co_tipo_atividade; }

            set { _a022_co_tipo_atividade = value; }
        }

        public string a022_ds_tipo_atividade
        {
            get { return _a022_ds_tipo_atividade; }

            set { _a022_ds_tipo_atividade = value; }
        }

        #endregion


        #region Implements
        //public void Update()
        //{
        //    StringBuilder SqlI = new StringBuilder();
        //    StringBuilder SqlU = new StringBuilder();

        //    SqlI.AppendLine(" Insert into a022_tipo_atividade");
        //    SqlI.AppendLine("  (");
        //    SqlI.AppendLine("	a022_co_tipo_atividade, ");
        //    SqlI.AppendLine("	a022_ds_tipo_atividade");
        //    SqlI.AppendLine("  )");
        //    SqlI.AppendLine(" Values ");
        //    SqlI.AppendLine("  (");
        //    SqlI.AppendLine("	@v_a022_co_tipo_atividade, ");
        //    SqlI.AppendLine("	@v_a022_ds_tipo_atividade");
        //    SqlI.AppendLine("  )");

        //    // Codigo Update ******************* 
        //    SqlU.AppendLine(" Update     A022_Tipo_Atividade Set ");
        //    SqlU.AppendLine("		a022_co_tipo_atividade = @v_a022_co_tipo_atividade, ");
        //    SqlU.AppendLine("		a022_ds_tipo_atividade = @v_a022_ds_tipo_atividade");
        //    SqlU.AppendLine(" Where	1 = 1 ");

        //    //TODO: Implements Where Clause Here!!!!


        //    MySqlCommand cmdToExecute = new MySqlCommand();
        //    cmdToExecute.CommandText = SqlU.ToString();
        //    cmdToExecute.CommandType = CommandType.Text;

        //    // Use base class' connection object 
        //    cmdToExecute.Connection = _mainConnection;

        //    try
        //    {

        //        // Codigo dbParameter ******************* 
        //        cmdToExecute.Parameters.Add(new MySqlParameter("v_a022_co_tipo_atividade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a022_co_tipo_atividade));
        //        cmdToExecute.Parameters.Add(new MySqlParameter("v_a022_ds_tipo_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a022_ds_tipo_atividade));
        //        if (_mainConnectionIsCreatedLocal)
        //        {
        //            // Open connection. 
        //            _mainConnection.Open();
        //        }
        //        else
        //        {
        //            if (_mainConnectionProvider.IsTransactionPending)
        //            {
        //                cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
        //            }
        //        }

        //        // Execute query. 
        //        if (cmdToExecute.ExecuteNonQuery() == 0)
        //        {
        //            cmdToExecute.CommandText = SqlI.ToString();
        //            cmdToExecute.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object 
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_mainConnectionIsCreatedLocal)
        //        {

        //            // Close connection. 
        //            _mainConnection.Close();
        //        }
        //        cmdToExecute.Dispose();
        //    }
        //}
        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a022_co_tipo_atividade, ");
            Sql.AppendLine("		a022_ds_tipo_atividade");
            Sql.AppendLine(" From	a022_tipo_atividade");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	a022_co_tipo_atividade = @_a022_co_tipo_atividade");
            Sql.AppendLine(" And	a022_ds_tipo_atividade = @_a022_ds_tipo_atividade");
            

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A022_Tipo_Atividade");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                cmdToExecute.Parameters.Add(new MySqlParameter("@a022_co_tipo_atividade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a022_co_tipo_atividade));
                cmdToExecute.Parameters.Add(new MySqlParameter("@a022_ds_tipo_atividade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a022_ds_tipo_atividade));
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

        public String getDescricaoByCodigo()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine(" a022_co_tipo_atividade, ");
            Sql.AppendLine(" a022_ds_tipo_atividade");
            Sql.AppendLine(" From	a022_tipo_atividade");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And  a022_co_tipo_atividade = @_a022_co_tipo_atividade");
            


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("A022_Tipo_Atividade");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                cmdToExecute.Parameters.Add(new MySqlParameter("@_a022_co_tipo_atividade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a022_co_tipo_atividade));
                
                // Execute query. 

                adapter.Fill(toReturn);
                return toReturn.Rows.Count > 0 ? toReturn.Rows[0]["a022_ds_tipo_atividade"].ToString() : "";
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


