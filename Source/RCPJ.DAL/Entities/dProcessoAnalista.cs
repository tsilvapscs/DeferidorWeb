using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dProcessoAnalista : DBInteractionBase
    {


        #region Variables
        private string _PROTOCOLO = "";
        private string _cod_usuario = "";
        private int _SEQ_ANDAMENTO = 0;
        private int _COD_STATUS = 0;

        
        #endregion
        
        #region Class Member Declarations
        public string PROTOCOLO
        {
            get { return _PROTOCOLO; }
            set { _PROTOCOLO = value; }
        }
        public string COD_USUARIO
        {
            get { return _cod_usuario; }
            set { _cod_usuario = value; }
        }
        public int SEQ_ANDAMENTO
        {
            get { return _SEQ_ANDAMENTO; }
            set { _SEQ_ANDAMENTO = value; }
        }
        public int COD_STATUS
        {
            get { return _COD_STATUS; }
            set { _COD_STATUS = value; }
        }
        #endregion

        public void Update()
        {

            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine("insert into  controleimagem.processo_analista(PROTOCOLO, SEQ_ANDAMENTO,  COD_STATUS , COD_USUARIO ) ");
            SqlU.AppendLine(" Values (@v_PROTOCOLO , @v_SEQ_ANDAMENTO , @v_COD_STATUS, @v_COD_USUARIO)");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("v_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_SEQ_ANDAMENTO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _SEQ_ANDAMENTO));
                //cmdToExecute.Parameters.Add(new MySqlParameter("v_t001_sq_pessoa_rep_legal", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _d));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_COD_STATUS", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _COD_STATUS));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_COD_USUARIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_usuario));

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

        public void UpdateProtocoloImagem(string _protocolo, int _status)
        {

            StringBuilder SqlU = new StringBuilder();

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update   controleimagem.central_carga ");
            SqlU.AppendLine(" set SITUACAO = " + _status.ToString());
            SqlU.AppendLine(" where PROTOCOLO = @v_protocolo ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _protocolo));

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

        public DataTable GetProcessosFuncionario()
        {

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("processo_analista");

            Sql.AppendLine(@"SELECT cc.protocolo , cc.nome_empresa , cc.nome_unidade, cc.ATO, cc.NAT_JURIDICA , concat(cc.NAT_JURIDICA,' - ',nj.t009_ds_natureza_juridica) DESC_NAT_JURIDICA, concat(cc.ATO,' - ',a.NO_ATO) DESC_ATO
                            FROM
                                 controleimagem.processo_analista pa
                                 INNER JOIN controleimagem.central_carga cc
                                   on pa.PROTOCOLO = cc.PROTOCOLO 
                                    inner join t009_natureza_juridica nj on nj.t009_co_natureza_juridica = cc.NAT_JURIDICA
                                    LEFT JOIN shared.ato a ON cc.ato = a.CO_ATO
                            Where pa.COD_USUARIO = @v_COD_USUARIO AND cc.SITUACAO = 2");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("v_COD_USUARIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_usuario));

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

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();

            }
        }

        public bool IsProcessoDigital(string pProtocolo)
        {

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("centralprocesso");

            Sql.AppendLine(@"SELECT count(*) as qtd
                            FROM
                                 controleimagem.central_carga cc
                            Where cc.PROTOCOLO  = @v_PROTOCOLO");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {

                cmdToExecute.Parameters.Add(new MySqlParameter("v_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                _mainConnection.Open();

                adapter.Fill(toReturn);

                return (toReturn.Rows.Count > 0);
                

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();

            }
        }
    }
}
