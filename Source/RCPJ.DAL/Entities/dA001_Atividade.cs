using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dA001_Atividade : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _a001_co_atividade;
        protected string _a001_ds_atividade;
        protected string _a001_in_tipo;
        #endregion

        #region Class Member Declarations
        public string a001_co_atividade
        {
            get { return _a001_co_atividade; }

            set { _a001_co_atividade = value; }
        }

        public string a001_ds_atividade
        {
            get { return _a001_ds_atividade; }

            set { _a001_ds_atividade = value; }
        }

        public string a001_in_tipo
        {
            get { return _a001_in_tipo; }

            set { _a001_in_tipo = value; }
        }

        #endregion


        #region Implements

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();

            Sql.AppendLine(@"SELECT 
                                   TAE_COD_ACTVD    AS A001_CO_ATIVIDADE
                                 , TAE_DESC         AS A001_DS_ATIVIDADE
                                 , 2                AS A001_IN_TIPO 
                            FROM TAB_ACTV_ECON 
                            WHERE	1 = 1 ");

            if (_a001_co_atividade != null)
            {
                Sql.AppendLine(" And	TAE_COD_ACTVD = @_a001_co_atividade");
                cmdToExecute.Parameters.Add(new MySqlParameter("@_a001_co_atividade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_co_atividade));
            }
            if (_a001_ds_atividade != null)
            {
                Sql.AppendLine(" And	TAE_DESC like @_a001_ds_atividade");
                cmdToExecute.Parameters.Add(new MySqlParameter("@_a001_ds_atividade", MySqlDbType.VarChar, 500 , ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_ds_atividade));
            }

            //if (_a001_in_tipo != null)
            //{
            //    Sql.AppendLine(" And	a001_in_tipo = @_a001_in_tipo");
            //    cmdToExecute.Parameters.Add(new MySqlParameter("@_a001_in_tipo", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_in_tipo));
            //}

            
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_ACTV_ECON");
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

        public String getDescricaoByCodigo()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"SELECT 
                               TAE_COD_ACTVD AS A001_CO_ATIVIDADE
                             , TAE_DESC AS A001_DS_ATIVIDADE
                             , 2 AS A001_IN_TIPO 
                            FROM TAB_ACTV_ECON 
                            WHERE TAE_COD_ACTVD = @a001_co_atividade");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            DataTable toReturn = new DataTable("TAB_ACTV_ECON");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();
                cmdToExecute.Parameters.Add(new MySqlParameter("a001_co_atividade", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a001_co_atividade));

                // Execute query. 
                adapter.Fill(toReturn);
                return toReturn.Rows.Count > 0 ? toReturn.Rows[0]["a001_ds_atividade"].ToString():"";
                
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

        public DataTable QueryXmlWs(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" SELECT   ");
            Sql.AppendLine(" a.A001_CO_ATIVIDADE as CODIGOATIVIDADE,     ");
            Sql.AppendLine(" a.R004_IN_PRINCIPAL_SECUNDARIO TipoCnae   ");
            Sql.AppendLine(" FROM   ");
            Sql.AppendLine(" R004_ATUACAO a,   ");
            Sql.AppendLine(" T003_PESSOA_JURIDICA pj,  ");
            Sql.AppendLine(" T005_PROTOCOLO protocolo   ");
            Sql.AppendLine(" where 1 = 1  ");
            Sql.AppendLine(" and  protocolo.T005_NR_PROTOCOLO = '" + pProtocolo + "'");
            Sql.AppendLine(" and  protocolo.T001_SQ_PESSOA = pj.T001_SQ_PESSOA  ");
            Sql.AppendLine(" and  pj.T001_SQ_PESSOA = a.T001_SQ_PESSOA   ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_ACTV_ECON");
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

        public DataTable QueryByDescricao()
        {
            StringBuilder Sql = new StringBuilder();
            MySqlCommand cmdToExecute = new MySqlCommand();

            Sql.AppendLine(@"SELECT 
                               TAE_COD_ACTVD AS CodigoCNAE
                             , TAE_DESC      AS Descricao
                             , 2             AS A001_IN_TIPO 
                            FROM TAB_ACTV_ECON 
                            WHERE	1 = 1");

            if (_a001_ds_atividade != null)
            {
                Sql.AppendLine(" And	TAE_DESC like '%" + _a001_ds_atividade + "%'");
                            }
            Sql.AppendLine(" Limit 50");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_ACTV_ECON");
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


        #endregion
    }
} 



