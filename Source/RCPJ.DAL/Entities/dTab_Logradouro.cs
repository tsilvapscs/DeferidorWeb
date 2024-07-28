using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dTab_Logradouro : DBInteractionBase
    {
        #region  Property Declarations
        protected decimal _tlg_clav_seq; //chave
        protected string _tlg_clav; //chave?      
        protected string _tlg_nome; // nome logradouro
        protected string _tlg_tba_clav; // código bairro
        protected string _tlg_cep8; // CEP
        protected string _tlg_tba_tmu_tuf_uf; //UF
        protected decimal _tlg_tti_clav; // tipo de logradouro
        //protected string _tlg_compl; // complemento do logradouro
        protected string _tlg_origem; // origem sempre 1 <Sistema>
        protected string _tlg_tba_tmu_cod_mun; // código município
        protected string _tlg_tus_cod_usr; // sempre <sistema>
        protected DateTime _tlg_fec_actl; // data da inclusão
        #endregion

        #region Class Member Declarations
        public decimal tlg_clav_seq
        {
            get { return _tlg_clav_seq; }
            set { _tlg_clav_seq = value; }
        }
        public string tlg_clav
        {
            get { return _tlg_clav; }
            set { _tlg_clav = value; }
        }
        public string tlg_nome
        {
            get { return _tlg_nome; }
            set { _tlg_nome = value; }
        }
        public string tlg_tba_clav
        {
            get { return _tlg_tba_clav; }
            set { _tlg_tba_clav = value; }
        }
        public string tlg_cep8
        {
            get { return _tlg_cep8; }
            set { _tlg_cep8 = value; }
        }
        public string tlg_tba_tmu_tuf_uf
        {
            get { return _tlg_tba_tmu_tuf_uf; }
            set { _tlg_tba_tmu_tuf_uf = value; }
        }
        public decimal tlg_tti_clav
        {
            get { return _tlg_tti_clav; }
            set { _tlg_tti_clav = value; }
        }
        //public string _tlg_compl; // complemento do logradouro
        public string tlg_origem
        {
            get { return _tlg_origem; }
            set { _tlg_origem = value; }
        }
        public string tlg_tba_tmu_cod_mun
        {
            get { return _tlg_tba_tmu_cod_mun; }
            set { _tlg_tba_tmu_cod_mun = value; }
        }
        public string tlg_tus_cod_usr
        {
            get { return _tlg_tus_cod_usr; }
            set { _tlg_tus_cod_usr = value; }
        }
        public DateTime tlg_fec_actl
        {
            get { return _tlg_fec_actl; }
            set { _tlg_fec_actl = value; }
        }

        #endregion

        #region Implementação
        public int ProximoCodigoLogradouro()
        {
            string wProximoCodigo = string.Empty;
            DataTable toReturn = new DataTable();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select max(TLG_CLAV_SEQ) from tab_cep_logr");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                // Open connection.
                // _mainConnection.Open();

                // Execute query.
                //cmdToExecute.ExecuteNonQuery();
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                    wProximoCodigo = toReturn.Rows[0]["max(tlg_clav_seq)"].ToString();
                return Convert.ToInt32(wProximoCodigo) + 1;
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
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into tab_cep_logr");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	tlg_clav_seq , ");
            SqlI.AppendLine("	tlg_clav , ");
            SqlI.AppendLine("	tlg_nome , ");
            SqlI.AppendLine("	tlg_tba_clav , ");
            SqlI.AppendLine("	tlg_cep8 , ");
            SqlI.AppendLine("	tlg_tba_tmu_tuf_uf , ");
            SqlI.AppendLine("	tlg_tti_clav , ");
            SqlI.AppendLine("	tlg_origem  , ");
            SqlI.AppendLine("	tlg_tba_tmu_cod_mun , ");
            SqlI.AppendLine("	tlg_tus_cod_usr , ");
            SqlI.AppendLine("	tlg_fec_actl  ");

            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_tlg_clav_seq, ");
            SqlI.AppendLine("	@v_tlg_clav, ");
            SqlI.AppendLine("	@v_tlg_nome, ");
            SqlI.AppendLine("	@v_tlg_tba_clav, ");
            SqlI.AppendLine("	@v_tlg_cep8, ");
            SqlI.AppendLine("	@v_tlg_tba_tmu_tuf_uf, ");
            SqlI.AppendLine("	@v_tlg_tti_clav, ");
            SqlI.AppendLine("	@v_tlg_origem, ");
            SqlI.AppendLine("	@v_tlg_tba_tmu_cod_mun, ");
            SqlI.AppendLine("	@v_tlg_tus_cod_usr, ");
            SqlI.AppendLine("	@v_tlg_fec_actl");
            SqlI.AppendLine("  )");

            
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandType = CommandType.Text;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_clav_seq", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_clav_seq));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_clav", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_clav));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_nome", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_nome));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_tba_clav", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_tba_clav));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_cep8", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_cep8));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_tba_tmu_tuf_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_tba_tmu_tuf_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_tti_clav", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_tti_clav));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_tba_tmu_cod_mun", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_tba_tmu_cod_mun));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_tus_cod_usr", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_tus_cod_usr));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tlg_fec_actl", MySqlDbType.DateTime, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, tlg_fec_actl));

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
                
                    cmdToExecute.CommandText = SqlI.ToString();
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
