using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR003_Rel_Org_Munic : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected string _t004_nr_cnpj_org_reg;
        protected decimal _a005_co_municipio;
        #endregion

        #region Class Member Declarations
        public string t004_nr_cnpj_org_reg
        {
            get { return _t004_nr_cnpj_org_reg; }

            set { _t004_nr_cnpj_org_reg = value; }
        }

        public decimal a005_co_municipio
        {
            get { return _a005_co_municipio; }

            set { _a005_co_municipio = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into r003_rel_org_munic");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	a005_co_municipio");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_t004_nr_cnpj_org_reg, ");
            SqlI.AppendLine("	@v_a005_co_municipio");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     R003_Rel_Org_Munic Set ");
            SqlU.AppendLine("		t004_nr_cnpj_org_reg = @v_t004_nr_cnpj_org_reg, ");
            SqlU.AppendLine("		a005_co_municipio = @v_a005_co_municipio");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t004_nr_cnpj_org_reg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t004_nr_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_a005_co_municipio", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _a005_co_municipio));
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

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		t004_nr_cnpj_org_reg, ");
            Sql.AppendLine("		a005_co_municipio");
            Sql.AppendLine(" From	R003_Rel_Org_Munic");
            Sql.AppendLine(" Where	1 = 1 ");
            
            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	t004_nr_cnpj_org_reg = _t004_nr_cnpj_org_reg");
            //Sql.AppendLine(" And	a005_co_municipio = _a005_co_municipio");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("R003_Rel_Org_Munic");
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

        public DataTable QueryMunicipioOrgaoUF(string wCodigoUF)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine("SELECT  uf.TUF_UF       as A004_CO_UF , ");
            Sql.AppendLine("        uf.TUF_NOME     as A004_DS_UF , ");
            Sql.AppendLine("        m.TMU_NOM_MUN   as descricao, ");
            Sql.AppendLine("        m.TMU_COD_MUN   as codigo, ");
            Sql.AppendLine("        rom.T004_NR_CNPJ_ORG_REG ");
            Sql.AppendLine("FROM    tab_cep_uf as uf ");
            Sql.AppendLine("        inner join tab_munic as m ");
            Sql.AppendLine("            on uf.TUF_UF = m.TMU_TUF_UF ");
            Sql.AppendLine("        inner join r003_rel_org_munic as rom ");
            Sql.AppendLine("            on rom.A005_CO_MUNICIPIO = m.TMU_COD_MUN ");
            Sql.AppendLine("WHERE uf.TUF_UF = '" + wCodigoUF + "'");
            Sql.AppendLine(" ORDER by m.TUF_NOME");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open conn ection.
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

        public DataTable QueryNomeMunicipio(string wCodigoMunicipio)
        {
            


            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		rom. T004_NR_CNPJ_ORG_REG AS codigo, ");
            Sql.AppendLine("		o.T004_DS_ORG_REG AS descricao, ");
            Sql.AppendLine("		rom.A005_CO_MUNICIPIO as municipio ");
            Sql.AppendLine(" From	r003_rel_org_munic AS rom ");
            Sql.AppendLine(" inner join t004_orgao_registro as o ");
            Sql.AppendLine(" on o.T004_NR_CNPJ_ORG_REG = rom.T004_NR_CNPJ_ORG_REG ");

            Sql.AppendLine(" Where	1 = 1 ");
            Sql.AppendLine(" And rom.A005_CO_MUNICIPIO = '" + wCodigoMunicipio + "'"); 
            Sql.AppendLine(" order by o.T004_DS_ORG_REG");
            //TODO: Implements Where Clause Here!!!! 
            //Sql.AppendLine(" And	t004_nr_cnpj_org_reg = _t004_nr_cnpj_org_reg");
            //Sql.AppendLine(" And	a005_co_municipio = _a005_co_municipio");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable();
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


