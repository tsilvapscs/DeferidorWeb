using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Munic : DBInteractionBase
    {

        // Variables ******************* 
        #region  Property Declarations
        protected decimal _tmu_cod_mun;
        protected string _tmu_nom_mun;
        protected decimal _tmu_cod_catg;
        protected string _tmu_num_cep;
        protected decimal _tmu_cod_correios;
        protected decimal _tmu_cod_celesc;
        protected string _tmu_tuf_uf;
        #endregion

        #region Class Member Declarations
        public decimal tmu_cod_mun
        {
            get { return _tmu_cod_mun; }

            set { _tmu_cod_mun = value; }
        }

        public string tmu_nom_mun
        {
            get { return _tmu_nom_mun; }

            set { _tmu_nom_mun = value; }
        }

        public decimal tmu_cod_catg
        {
            get { return _tmu_cod_catg; }

            set { _tmu_cod_catg = value; }
        }

        public string tmu_num_cep
        {
            get { return _tmu_num_cep; }

            set { _tmu_num_cep = value; }
        }

        public decimal tmu_cod_correios
        {
            get { return _tmu_cod_correios; }

            set { _tmu_cod_correios = value; }
        }

        public decimal tmu_cod_celesc
        {
            get { return _tmu_cod_celesc; }

            set { _tmu_cod_celesc = value; }
        }

        public string tmu_tuf_uf
        {
            get { return _tmu_tuf_uf; }

            set { _tmu_tuf_uf = value; }
        }

        #endregion


        #region Implements
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_munic");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tmu_cod_mun, ");
            SqlI.AppendLine("	tmu_nom_mun, ");
            SqlI.AppendLine("	tmu_cod_catg, ");
            SqlI.AppendLine("	tmu_num_cep, ");
            SqlI.AppendLine("	tmu_cod_correios, ");
            SqlI.AppendLine("	tmu_cod_celesc, ");
            SqlI.AppendLine("	tmu_tuf_uf");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tmu_cod_mun, ");
            SqlI.AppendLine("	@v_tmu_nom_mun, ");
            SqlI.AppendLine("	@v_tmu_cod_catg, ");
            SqlI.AppendLine("	@v_tmu_num_cep, ");
            SqlI.AppendLine("	@v_tmu_cod_correios, ");
            SqlI.AppendLine("	@v_tmu_cod_celesc, ");
            SqlI.AppendLine("	@v_tmu_tuf_uf");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     Tab_Munic Set ");
            SqlU.AppendLine("		tmu_cod_mun = @v_tmu_cod_mun, ");
            SqlU.AppendLine("		tmu_nom_mun = @v_tmu_nom_mun, ");
            SqlU.AppendLine("		tmu_cod_catg = @v_tmu_cod_catg, ");
            SqlU.AppendLine("		tmu_num_cep = @v_tmu_num_cep, ");
            SqlU.AppendLine("		tmu_cod_correios = @v_tmu_cod_correios, ");
            SqlU.AppendLine("		tmu_cod_celesc = @v_tmu_cod_celesc, ");
            SqlU.AppendLine("		tmu_tuf_uf = @v_tmu_tuf_uf");
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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_mun", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_mun));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_nom_mun", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_nom_mun));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_catg", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_catg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_num_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_num_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_correios", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_correios));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_celesc", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_celesc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_tuf_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_tuf_uf));
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
            Sql.AppendLine("		tmu_cod_mun codigo, ");
            Sql.AppendLine("		tmu_nom_mun descricao, ");
            Sql.AppendLine("		tmu_tuf_uf");
            Sql.AppendLine(" From	Tab_Munic");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            if (!string.IsNullOrEmpty(_tmu_tuf_uf))
                Sql.AppendLine(" And	tmu_tuf_uf = '" + _tmu_tuf_uf + "'");
            else
                Sql.AppendLine(" And	tmu_cod_mun = " + _tmu_cod_mun);
            

            _tmu_tuf_uf = string.Empty;
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Tab_Munic");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
               // _mainConnection.Open();

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


