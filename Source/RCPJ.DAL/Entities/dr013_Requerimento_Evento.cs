using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dr013_Requerimento_Evento : DBInteractionBase
    {
        #region  Property Declarations
        private string _t004_nr_cnpj_org_reg;
        private string _r013_nr_protocolo_or;
        private string _t005_nr_protocolo;
        private string _r013_cod_ato;
        private string _r013_cod_evento;
        private int _qtdEvento = 0;
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }
        public string T005_nr_protocolo
        {
            get { return _t005_nr_protocolo; }
            set { _t005_nr_protocolo = value; }
        }
        public string R013_nr_protocolo_or
        {
            get { return _r013_nr_protocolo_or; }
            set { _r013_nr_protocolo_or = value; }
        }
        public string R013_cod_ato
        {
            get { return _r013_cod_ato; }
            set { _r013_cod_ato = value; }
        }
        public string R013_cod_evento
        {
            get { return _r013_cod_evento; }
            set { _r013_cod_evento = value; }
        }
        public int QtdEvento
        {
            get { return _qtdEvento; }
            set { _qtdEvento = value; }
        }
        #endregion


        #region Implements
        public void Delete(string numProtocolo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" DELETE");
            Sql.AppendLine(" From	r013_requerimento_evento");
            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");
            Sql.AppendLine(" And	R013_NR_PROTOCOLO_OR = '" + numProtocolo + "'");
            Sql.AppendLine(" And	t004_nr_cnpj_org_reg = '" + _t004_nr_cnpj_org_reg + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

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

            }
        }

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r013_requerimento_evento");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	t005_nr_protocolo,");
            SqlI.AppendLine("	r013_nr_protocolo_or,");
            SqlI.AppendLine("	r013_cod_ato,");
            SqlI.AppendLine("	r013_cod_evento,");
            SqlI.AppendLine("	r013_qtd_evento");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_t005_nr_protocolo,");
            SqlI.AppendLine("	@v_r013_nr_protocolo_or,");
            SqlI.AppendLine("	@v_r013_cod_ato,");
            SqlI.AppendLine("	@v_r013_cod_evento,");
            SqlI.AppendLine("	@v_r013_qtd_evento");
            SqlI.AppendLine("  )");

         
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlI.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t005_nr_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t005_nr_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r013_nr_protocolo_or", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r013_nr_protocolo_or));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r013_cod_ato", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r013_cod_ato));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r013_cod_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _r013_cod_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_r013_qtd_evento", MySqlDbType.Int16, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _qtdEvento));

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
        // Codigo Query ******************* 

        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            Sql.AppendLine(" From	r013_cod_evento");
            Sql.AppendLine(" Where	t004_nr_cnpj_org_reg = '" + _t004_nr_cnpj_org_reg + "'");
            Sql.AppendLine(" And	t005_nr_protocolo = '" + _t005_nr_protocolo + "'");




            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("r013_cod_evento");
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

        public DataTable QueryAtos()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT r.T005_NR_PROTOCOLO , r.R013_COD_ATO , r.R013_COD_EVENTO , a.NO_ATO , e.NO_EVENTO
                                FROM r013_requerimento_evento r
                                  inner join ato a on a .CO_ATO = r.R013_COD_ATO
                                  left join evento e on r.r013_cod_evento = e.CO_EVENTO
                                WHERE 1 = 1
                                  AND r.t005_nr_protocolo = " + _t005_nr_protocolo);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("r013_cod_evento");
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
