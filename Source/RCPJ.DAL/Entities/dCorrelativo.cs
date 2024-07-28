using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    [Serializable]
    public class dCorrelativo : DBInteractionBase
    {
        // Variables ******************* 
        #region  Property Declarations
        private Int32 _tipo;

        public Int32 Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        #endregion

        public String GetCorrelativo()
        {
            MySqlCommand cmdToExecute = new MySqlCommand(); 
            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new MySqlParameter("pTipo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,_tipo));
            cmdToExecute.Parameters.Add(new MySqlParameter("v_vpv_cod_protocolo", MySqlDbType.VarChar,20  , ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, ""));
            
            
            cmdToExecute.CommandText = "GetNumeroCorrelativo";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            
            
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Open connection. 
                _mainConnection.Open();

                cmdToExecute.ExecuteNonQuery();

                return cmdToExecute.Parameters["@v_vpv_cod_protocolo"].Value.ToString();
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
 
    }
}
