using System;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

using psc.Framework;
using psc.Framework.Data;
using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
using RCPJ.DAL.ConnectionBase;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    /// <summary>
    /// Summary description for TAB_GENERICA.
    /// </summary>
    public class TAB_GENERICA : DBInteractionBase
    {
        #region Class Member Declarations
        protected DateTime _tGE_FEC_ACTUAL;
        protected decimal _tGE_COD_TIP_TAB = int.MinValue, _tGE_TIP_TAB = int.MinValue;
        protected string _tGE_NOMB_DESC = "";
        protected decimal _tGE_ID_TABELA = int.MinValue;
        #endregion

        #region Class Property Declarations
        public decimal TGE_TIP_TAB
        {
            get
            {
                return (decimal)_tGE_TIP_TAB;
            }
            set
            {
                _tGE_TIP_TAB = value;
            }
        }


        public decimal TGE_COD_TIP_TAB
        {
            get
            {
                return (decimal)_tGE_COD_TIP_TAB;
            }
            set
            {
                _tGE_COD_TIP_TAB = value;
            }
        }

        public decimal TGE_ID_TABELA
        {
            get
            {
                return (decimal)_tGE_ID_TABELA;
            }
            set
            {
                _tGE_ID_TABELA = value;
            }
        }
        public string TGE_NOMB_DESC
        {
            get
            {
                return (string)_tGE_NOMB_DESC;
            }
            set
            {
                _tGE_NOMB_DESC = value;
            }
        }


        public DateTime TGE_FEC_ACTUAL
        {
            get
            {
                return (DateTime)_tGE_FEC_ACTUAL;
            }
        }
        #endregion

        #region Implements
        public void Update()
        {
            int _valor = 0;

            StringBuilder SqlS = new StringBuilder();
            SqlS.Append(" Select MAX( TGE_COD_TIP_TAB) valor ");
            SqlS.Append(" From tab_generica ");
            SqlS.Append(" Where  TGE_TIP_TAB = " + _tGE_TIP_TAB);

            MySqlCommand cmdToExecuteS = new MySqlCommand();
            cmdToExecuteS.CommandText = SqlS.ToString();
            cmdToExecuteS.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("");
            MySqlDataAdapter adapterS = new MySqlDataAdapter(cmdToExecuteS);
            cmdToExecuteS.Connection = _mainConnection;

            _mainConnection.Open();
            adapterS.Fill(toReturn);
            _valor = int.Parse(toReturn.Rows[0][0].ToString()) + 1;



            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO TAB_GENERICA (TGE_TIP_TAB , TGE_COD_TIP_TAB , TGE_NOMB_DESC) ");
            sql.AppendLine(" values ( @v_TGE_TIP_TAB, @v_TGE_COD_TIP_TAB , @v_TGE_NOMB_DESC)");
            
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new MySqlParameter("v_TGE_TIP_TAB", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _tGE_TIP_TAB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_TGE_COD_TIP_TAB", MySqlDbType.Int32, 22, ParameterDirection.Input, false, 5, 0, "", DataRowVersion.Proposed, _valor));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_TGE_NOMB_DESC", MySqlDbType.VarChar, 120, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tGE_NOMB_DESC));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    // _mainConnection.Open();
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
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public DataTable Query(string _tipo, string _codigo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_tip_tab tipo , ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao, ");
            Sql.Append("		tge_fec_actual");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	tge_tip_tab = " + _tipo);
            Sql.Append(" And	tge_cod_tip_tab = " + _codigo);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable QueryDescricao(string _tipo, string _descricao)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_tip_tab tipo , ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao, ");
            Sql.Append("		tge_fec_actual");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	tge_tip_tab = " + _tipo);
            Sql.Append(" And	tge_nomb_desc = " + _descricao);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable Query(decimal pTipoBusca)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_tip_tab, ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao, ");
            Sql.Append("		tge_fec_actual");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	tge_tip_tab = '" + _tGE_ID_TABELA + "'");
            //Sql.Append(" And	tge_cod_tip_tab = _tge_cod_tip_tab");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable QueryByIdTabela()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_tip_tab, ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao, ");
            Sql.Append("		tge_fec_actual");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	tge_id_tabela = '" + _tGE_ID_TABELA + "'");
            //Sql.Append(" And	tge_cod_tip_tab = _tge_cod_tip_tab");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable QueryFormaAtuacao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(@" Select tge_tip_tab, 
            		            '0'||tge_cod_tip_tab codigo, 
            		            tge_nomb_desc descricao, 
            		            tge_fec_actual
                         From	tab_generica
                         Where	1 = 1 
                         And	tge_tip_tab = 149
                         And	TGE_COD_TIP_TAB <> 0");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("FormaAtuacao");
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

        public DataTable QueryComercioExterior()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(@" Select tge_tip_tab, 
            		            tge_cod_tip_tab codigo, 
            		            tge_nomb_desc descricao, 
            		            tge_fec_actual
                         From	tab_generica
                         Where	1 = 1 
                         And	tge_tip_tab = 46
                         And	TGE_COD_TIP_TAB <> 0");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("FormaAtuacao");
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
        public DataTable QueryByTipo(string tipo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_tip_tab, ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao, ");
            Sql.Append("		tge_fec_actual");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	tge_tip_tab = " + tipo);
            Sql.Append(" And	TGE_COD_TIP_TAB <> 0");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable QueryNaturezaJuridica_old()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A006_CO_NATUREZA_JURIDICA Codigo, ");
            Sql.Append(" A006_DS_NATUREZA_JURIDICA Descricao ");
            Sql.Append(" From	a006_natureza_juridica");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");
            Sql.Append(" Order by 1 ");
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
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

        public DataTable QueryNaturezaJuridicaTodas()
        {
            //Raul Verificar comentario
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT nj.t009_co_grupo
                             , nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        INNER JOIN r006_natureza_juridica_tipo r
                        ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica
                        WHERE
                          1 = 1
                          AND t009_co_grupo IS NOT NULL
                          -- AND t009_co_grupo IN(10, 11, 14, 15, 20) lEANDRO COMENTADO ERRO JUCERJA 29/07/2015 - VERIFICAR
                        ORDER BY
                          nj.t009_co_grupo
                        , nj.t009_ds_natureza_juridica ");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        public DataTable QueryNaturezaJuridicaRcpj()
        {

            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"SELECT nj.t009_co_grupo
                             , nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        INNER JOIN r006_natureza_juridica_tipo r
                        ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica
                        WHERE
                          1 = 1
                          AND t009_co_grupo IS NOT NULL
                          AND t009_co_grupo IN(10, 11, 14, 15, 17, 20)
                        ORDER BY
                          nj.t009_co_grupo
                        , nj.t009_ds_natureza_juridica ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        public DataTable QueryNaturezaJuridica(Int32 codigoNJ)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(" Select ");
            Sql.Append(" nj.t009_co_natureza_juridica codigo, ");
            Sql.Append(" nj.t009_ds_natureza_juridica descricao ");
            Sql.Append(" from	t009_natureza_juridica nj ");
            Sql.Append(" inner join r006_natureza_juridica_tipo r ");
            Sql.Append(" on nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica");
            Sql.Append(" where	1 = 1 ");
            Sql.Append(" and not t009_co_grupo is NULL ");
            if (codigoNJ != -1) // 13/06/2012
                Sql.AppendLine("AND nj.t009_co_grupo = " + codigoNJ); //13/06/2012
            Sql.Append(" order by nj.t009_ds_natureza_juridica ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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


        public string getNaturezaJuridica(Int32 codigoNJ)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(" Select ");
            Sql.Append(" nj.t009_co_natureza_juridica codigo, ");
            Sql.Append(" nj.t009_ds_natureza_juridica descricao ");
            Sql.Append(" from	t009_natureza_juridica nj ");
            Sql.Append(" inner join r006_natureza_juridica_tipo r ");
            Sql.Append(" on nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica");
            Sql.Append(" where	1 = 1 ");
            Sql.Append(" and not t009_co_grupo is NULL ");
            if (codigoNJ != -1) // 13/06/2012
                Sql.AppendLine("AND nj.t009_co_natureza_juridica = " + codigoNJ); //13/06/2012
            Sql.Append(" order by nj.t009_ds_natureza_juridica ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                if (toReturn.Rows.Count > 0)
                {
                    return toReturn.Rows[0][1].ToString();
                }
                else
                {
                    return "";
                }
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


        public DataTable QueryPorteSociedade()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a011_co_porte Codigo, ");
            Sql.Append(" a011_ds_porte Descricao ");
            Sql.Append(" From	a011_porte");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryTipoPessoaJuridica()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a018_co_tipo_pessoa_juridica Codigo, ");
            Sql.Append(" a018_ds_tipo_pessoa_juridica Descricao ");
            Sql.Append(" From	a018_tipo_pessoa_juridica");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryOrgaoExpedidor()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select");
            Sql.Append(" tge_nomb_desc Descricao, ");
            Sql.Append(" mid(tge_nomb_desc, 1, instr(tge_nomb_desc, ' -') - 1) Codigo");
            Sql.Append(" From	tab_generica ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append(" And tge_tip_tab = 1565 ");
            Sql.Append(" And TGE_COD_TIP_TAB > 0 ");
            Sql.Append("order by TGE_NOMB_DESC");

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


        public DataTable QueryEstadoCivil()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a012_co_estado_civil Codigo, ");
            Sql.Append(" a012_ds_estado_civil Descricao ");
            Sql.Append(" From	a012_estado_civil order by a012_ds_estado_civil");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryTipoAtividade()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a022_co_tipo_atividade Codigo, ");
            Sql.Append(" a022_ds_tipo_atividade Descricao ");
            Sql.Append(" From	a022_tipo_atividade ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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
        public DataTable QueryPaisEquiparado()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A017_CO_PAIS Codigo, ");
            Sql.Append(" A017_NACIONALIDADE Descricao ");
            Sql.Append(" From	a017_pais ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append(" And T013_EQUIPARADO = '1' ");
            Sql.AppendLine(" order by a017_NACIONALIDADE");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable();
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
        public DataTable QueryQualificacaoAssociacao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a009_co_qualificacao_associacao Codigo, ");
            Sql.Append(" a019_ds_qualificacao_associacao Descricao ");
            Sql.Append(" From	a019_qualificacao_associacao ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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
        public DataTable QueryQualificacaoSociedade(Int32 wAux)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" select ");
            Sql.AppendLine(" c.A009_CO_CONDICAO Codigo, ");
            Sql.AppendLine(" c.A009_DS_CONDICAO Descricao, ");
            Sql.AppendLine(" c.A009_TIPO_PESSOA ");
            Sql.AppendLine(" from a009_condicao as c ");
            Sql.AppendLine(" inner join t008_tipo_natureza_juridica as t ");
            Sql.AppendLine(" on c.A009_CO_CONDICAO = t.A009_CO_CONDICAO ");
            Sql.AppendLine(" where t.A006_CO_NATUREZA_JURIDICA = " + wAux);
            Sql.AppendLine(" order by c.A009_DS_CONDICAO");

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

        public DataTable QueryProfissao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a020_co_profissao Codigo, ");
            Sql.Append(" a020_ds_profissao Descricao ");
            Sql.Append(" From	a020_profissao ");
            Sql.Append(" Where	1 = 1 ");
            if (_tGE_ID_TABELA > 0)
            {
                Sql.Append("and a020_co_profissao = " + _tGE_ID_TABELA);
            }
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

        public DataTable QueryEnquadramento()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" TGE_ID_TABELA Codigo, ");
            Sql.Append(" TGE_NOMB_DESC Descricao ");
            Sql.Append(" From	TAB_GENERICA ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append("and TGE_TIP_TAB = 1511 and tge_cod_tip_tab = 1");

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

        public string QueryEnquadramentoPorCodigo(string Codigo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" TGE_ID_TABELA Codigo, ");
            Sql.Append(" TGE_NOMB_DESC Descricao ");
            Sql.Append(" From	TAB_GENERICA ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append(" and TGE_TIP_TAB = 1511 and tge_cod_tip_tab = 1");
            Sql.Append(" and TGE_ID_TABELA = " + Codigo);

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
                if (toReturn.Rows.Count > 0)
                {
                    return toReturn.Rows[0][1].ToString();
                }
                else
                {
                    return "";
                }
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

        public DataTable QueryTipoVisto()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" TGE_ID_TABELA Codigo, ");
            Sql.Append(" TGE_NOMB_DESC Descricao ");
            Sql.Append(" From	TAB_GENERICA ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append("and TGE_TIP_TAB = 1533 and tge_cod_tip_tab != 0");

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


        public DataTable QueryCargo()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a023_co_cargo Codigo, ");
            Sql.Append(" a023_ds_cargo Descricao ");
            Sql.Append(" From	a023_cargo ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryFormaConvocacao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a026_co_forma_convocacao Codigo, ");
            Sql.Append(" a026_ds_forma_convocacao Descricao ");
            Sql.Append(" From	a026_forma_convocacao ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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


        public DataTable QueryTabelaQuorum()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a027_co_tabela_quorum Codigo, ");
            Sql.Append(" a027_ds_tabela_quorum Descricao ");
            Sql.Append(" From	a027_tabela_quorum ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryUF()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(@"SELECT   tab_cep_uf.TUF_UF     AS Codigo
                                    , tab_cep_uf.TUF_NOME   AS Descricao 
                            FROM TAB_CEP_UF");


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

        public DataTable QueryOrgaoUF_aa()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine("SELECT  uf.A004_CO_UF codigo, ");
            Sql.AppendLine("        uf.A004_DS_UF descricao, ");
            Sql.AppendLine( "       m.TMU_NOM_MUN, ");
            Sql.AppendLine("        m.TMU_COD_MUN, ");
            Sql.AppendLine("        rom.T004_NR_CNPJ_ORG_REG ");
            Sql.AppendLine("FROM    a004_uf as uf ");
            Sql.AppendLine("        inner join tab_munic as m ");
            Sql.AppendLine("            on uf.A004_CO_UF = m.TMU_TUF_UF ");
            Sql.AppendLine("        inner join r003_rel_org_munic as rom ");
            Sql.AppendLine("            on rom.A005_CO_MUNICIPIO = m.TMU_COD_MUN ");
            Sql.AppendLine(" order by uf.a004_ds_uf");

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


        public DataTable QueryPais()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A017_CO_PAIS Codigo, ");
            Sql.Append(" A017_DS_PAIS Descricao ");
            Sql.Append(" From	a017_pais order by a017_ds_pais");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryPaisNacionalidade()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A017_CO_PAIS Codigo, ");
            Sql.Append(" A017_NACIONALIDADE Descricao ");
            Sql.Append(" From	a017_pais order by a017_NACIONALIDADE");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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


        public DataTable QueryTipoLogradouro()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A015_CO_TIPO_LOGRADOURO Codigo, ");
            Sql.Append(" A015_DS_TIPO_LOGRADOURO Descricao ");
            Sql.Append(" From a015_tipo_logradouro order by A015_DS_TIPO_LOGRADOURO");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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


        public DataTable QueryTipoDocumento()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a010_co_tipo_documento Codigo, ");
            Sql.Append(" a010_ds_tipo_documento Descricao ");
            Sql.Append(" From	a010_tipo_documento ");
            Sql.Append(" order by a010_ds_tipo_documento");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        /// <summary>
        /// Retorna o Regime de bnes pelo tipo do Estado civil
        /// </summary>
        /// <param name="pTipo" values="1-Casado 2-Solteiro"></param>
        /// <returns></returns>        
        public DataTable QueryRegimePorTipo(int pTipo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" A031_CO_REGIME_BENS Codigo, ");
            Sql.Append(" A031_DS_REGIME_BENS Descricao ");
            Sql.Append(" From	a031_regime_bens ");
            Sql.Append(" Where	A031_TP_REGIME_BENS = " + pTipo.ToString());
            Sql.Append(" order by A031_DS_REGIME_BENS ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a031_regime_bens");
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

        public DataTable QueryRegime()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a013_co_regime_bens Codigo, ");
            Sql.Append(" a013_ds_regime_bens Descricao ");
            Sql.Append(" From	a013_regime_bens order by a013_ds_regime_bens ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public DataTable QueryTipoAssistidoRepresentado(Boolean wChave)
        {
            // Se wChave = true Mostra (Tutor, Pai ou Mãe) se wChave = false Mostra Representante
            StringBuilder Sql = new StringBuilder();
            

            Sql.Append(" Select ");
            Sql.Append(" t015_co_tipo_assistido_representado Codigo, ");
            Sql.Append(" t015_ds_tipo_assistido_representado Descricao ");
            Sql.Append(" From	t015_tipo_assistido_representado   ");
            Sql.Append(" Where	1 = 1 ");
            
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
        public DataTable QueryTipoAssistidoRepresentado()
        {
            // Se wChave = true Mostra (Tutor, Pai ou Mãe) se wChave = false Mostra Representante
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" t015_co_tipo_assistido_representado Codigo, ");
            Sql.Append(" t015_ds_tipo_assistido_representado Descricao ");
            Sql.Append(" From	t015_tipo_assistido_representado   ");
            Sql.Append(" Where	1 = 1 ");

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

        public DataTable QueryEmancipacao()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append(" a014_co_emancipacao Codigo, ");
            Sql.Append(" a014_ds_emancipacao Descricao ");
            Sql.Append(" From	a014_emancipacao   ");
            //Sql.Append(" Where	1 = 1 ");
            //Sql.Append(" And tge_tip_tab = '" + _tGE_TIP_TAB + "'");

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

        public string QueryEmancipacao(string Codigo)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            //Sql.Append(" a014_co_emancipacao Codigo, ");
            Sql.Append(" a014_ds_emancipacao Descricao ");
            Sql.Append(" From	a014_emancipacao   ");
            Sql.Append(" Where	1 = 1 ");
            Sql.Append(" And a014_co_emancipacao = '" + Codigo + "'");

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
                if (toReturn.Rows.Count > 0)
                {
                    return toReturn.Rows[0][0].ToString();
                }
                else
                    return "";
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
            StringBuilder sql = new StringBuilder();

            sql.Append(" Delete tab_generica ");
            sql.Append(" Where tge_tip_tab = " + _tGE_TIP_TAB);
            sql.Append(" And tge_cod_tip_tab = " + _tGE_COD_TIP_TAB);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

        public int getTipoPessoaJuridicaId(Int32 codigoNJ)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@" Select n.A006_CO_NATUREZA_JURIDICA,
                                     n.A018_CO_TIPO_PESSOA_JURIDICA
                              From   r006_natureza_juridica_tipo n
                              Where  n.A006_CO_NATUREZA_JURIDICA = " + codigoNJ);

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("r006_natureza_juridica_tipo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                return toReturn.Rows.Count > 0 ? Int32.Parse(toReturn.Rows[0]["A018_CO_TIPO_PESSOA_JURIDICA"].ToString()) : 0;

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

        public DataTable QueryNaturezaJuridicaAll()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" SELECT nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        INNER JOIN r006_natureza_juridica_tipo r
                        ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica
                        INNER JOIN a018_tipo_pessoa_juridica a
                        ON a.A018_CO_TIPO_PESSOA_JURIDICA = r.A018_CO_TIPO_PESSOA_JURIDICA");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        /// <summary>
        /// Retorna as naturezas juridaca pelo grupo
        /// </summary>
        /// <param name="codGrupo"></param>
        /// <returns></returns>
        public DataTable QueryNaturezaJuridicaTipoGrupo(string codGrupo)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@" SELECT nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        INNER JOIN r006_natureza_juridica_tipo r
                        ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica
                        WHERE r.A018_CO_TIPO_PESSOA_JURIDICA  = " + codGrupo);
            Sql.AppendLine(" ORDER BY nj.t009_ds_natureza_juridica");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        public DataTable QueryNaturezaJuridicaSefaz()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@" SELECT nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                             , r.A018_CO_TIPO_PESSOA_JURIDICA
                         FROM t009_natureza_juridica nj 
                                INNER JOIN r006_natureza_juridica_tipo r 
                                    ON nj.t009_co_natureza_juridica = r.a006_co_natureza_juridica 
                                INNER JOIN a018_tipo_pessoa_juridica a 
                                    ON a.A018_CO_TIPO_PESSOA_JURIDICA = r.A018_CO_TIPO_PESSOA_JURIDICA
                         WHERE nj.t009_tipo_natureza = 1
                         ORDER BY nj.t009_ds_natureza_juridica");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a006_natureza_juridica");
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

        public DataTable QueryNaturezaJuridicaRCPJByCodigo()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@" SELECT nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        WHERE nj.t009_co_natureza_juridica in(
                            '2232', '2240','2259','2267',
                            '2313','3069','3204','3220',
                            '3999')
                        ORDER BY nj.t009_ds_natureza_juridica");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("natureza_juridica");
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

        public static int getTipoNaturezaJuridicaOR(string natureza)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@" select t009_tipo_natureza as tipo
                        from t009_natureza_juridica a
                        where ");
            sql.AppendLine(" a.t009_co_natureza_juridica = '" + natureza + "'");

            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

            return Int32.Parse(Dt.Rows[0]["tipo"].ToString());
        }

        public DataTable QueryNaturezaJuridicaOrgaoPublico()
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(@" SELECT nj.t009_co_natureza_juridica codigo
                             , nj.t009_ds_natureza_juridica descricao
                        FROM
                          t009_natureza_juridica nj
                        WHERE nj.t009_co_natureza_juridica in(
                                   '1015','1023','1031','1040','1058','1066',
                                    '1074','1082','1104','1112','1120','1139','1147',
                                    '1155','1163','1171','1180','1201','1210'
                                    )
                        ORDER BY nj.t009_ds_natureza_juridica");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("natureza_juridica");
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

        public DataTable QueryTipoUnidade()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		 codigo, ");
            Sql.Append("		descricao ");
            Sql.Append(" From	tab_tipo_unidade order by codigo");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("tab_tipo_unidade");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);

                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

        }

       
        public DataTable QueryCNAEContribExt()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select ");
            Sql.Append("		tge_cod_tip_tab codigo, ");
            Sql.Append("		tge_nomb_desc descricao ");
            Sql.Append(" From	tab_generica");
            Sql.Append(" Where	1 = 1 ");

            Sql.Append(" And	TGE_TIP_TAB = 45");
            Sql.Append(" And	tge_cod_tip_tab <> 0");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("TAB_GENERICA");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);

                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }

        }

        public DataTable QueryPscErorrLogSistema(string datade, string dataAte)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.Append(" Select *");
            Sql.Append(" From	psc_errorlog_sistema");
            Sql.Append(" Where	1 = 1 ");

            if(datade != "")
                Sql.Append(" and DATE_FORMAT(PES_FEC_INC, '%Y%m%d') >= '" + datade + "'");

            if (dataAte != "")
                Sql.Append(" and DATE_FORMAT(PES_FEC_INC, '%Y%m%d') >= '" + dataAte + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("psc_errorlog_sistema");
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
