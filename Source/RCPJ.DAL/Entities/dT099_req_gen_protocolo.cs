using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL
{
    public class dT099_req_gen_protocolo : DBInteractionBase
    {
        #region  Property Declarations
        private string _T005_NR_PROTOCOLO;
        private int _T099_IN_TIPO_ENQUADRAMENTO;
        private int _T009_CO_PORTE;
        private string _T001_DS_PESSOA_ANT = "";
        private int _T009_TIP_ESTAB_RFB = 0;
        private string _T009_NR_ULT_REGISTRO = "";
        private Nullable<DateTime> _T009_DT_ULT_REGISTRO;
        private string _T009_NR_CNPJ_MATRIZ = "";
        private string _T009_UF_MATRIZ = "";
        private int _T009_TIPO_UNIDADE = 0;
        private string _T009_NR_CNPJ_ORG_REG;
        private string _T009_NR_NIRE_MATRIZ = "";
        private string _T009_TIPO_FORMA_OUTROS;

      
        private int _T009_IN_FORMA_ATUACAO = 0;
        private int _T009_IN_CENTRO_DISTRIBUICAO = 0;
        private int _T009_IN_FRANQUEADO = 0;
        private string _T009_NR_CNPJ_FRANQUEADOR = "";
        private string _T009_NR_ATO_LEGAL = "";

        private int _T009_IN_ATIV_AGROINDUSTRIA = 2;
        private int _T009_TIP_PROPRIEDADE = 0;
        private int _T009_TIP_INSCRICAO_ST = 0;
        private string _T009_NR_CONVENIO = "";
        private int _T009_TP_ATO_LEGAL = 0;
        private string _T009_COORDENADAS = "";
        private decimal _T009_VL_PATRIMONIO = 0;
        private int _T009_IN_CANTEIRO_OBRAS = 2;
        private int _T009_IN_TRANSPORTE = 2;
        private int _T009_IN_COMERCIO_EXTERIOR = 0;
        private int _T009_IN_SUBSTITUTO_TRIB = 2;
        #endregion

        #region Class Member Declarations
        public string T005_NR_PROTOCOLO
        {
            get { return _T005_NR_PROTOCOLO; }
            set { _T005_NR_PROTOCOLO = value; }
        }
        public int T099_IN_TIPO_ENQUADRAMENTO
        {
            get { return _T099_IN_TIPO_ENQUADRAMENTO; }
            set { _T099_IN_TIPO_ENQUADRAMENTO = value; }
        }
        public string T009_TIPO_FORMA_OUTROS
        {
            get { return _T009_TIPO_FORMA_OUTROS; }
            set { _T009_TIPO_FORMA_OUTROS = value; }
        }



        public int T009_CO_PORTE
        {
            get { return _T009_CO_PORTE; }
            set { _T009_CO_PORTE = value; }
        }

        public string T001_DS_PESSOA_ANT
        {
            get { return _T001_DS_PESSOA_ANT; }
            set { _T001_DS_PESSOA_ANT = value; }
        }
        public int T009_TIP_ESTAB_RFB
        {
            get { return _T009_TIP_ESTAB_RFB; }
            set { _T009_TIP_ESTAB_RFB = value; }
        }
        public string T009_NR_ULT_REGISTRO
        {
            get { return _T009_NR_ULT_REGISTRO; }
            set { _T009_NR_ULT_REGISTRO = value; }
        }
        public Nullable<DateTime> T009_DT_ULT_REGISTRO
        {
            get { return _T009_DT_ULT_REGISTRO; }
            set { _T009_DT_ULT_REGISTRO = value; }
        }
        public string T009_NR_CNPJ_MATRIZ
        {
            get { return _T009_NR_CNPJ_MATRIZ; }
            set { _T009_NR_CNPJ_MATRIZ = value; }
        }
        public string T009_UF_MATRIZ
        {
            get { return _T009_UF_MATRIZ; }
            set { _T009_UF_MATRIZ = value; }
        }
        public int T009_TIPO_UNIDADE
        {
            get { return _T009_TIPO_UNIDADE; }
            set { _T009_TIPO_UNIDADE = value; }
        }
        public string T009_NR_CNPJ_ORG_REG
        {
            get { return _T009_NR_CNPJ_ORG_REG; }
            set { _T009_NR_CNPJ_ORG_REG = value; }
        }
        public string T009_NR_NIRE_MATRIZ
        {
            get { return _T009_NR_NIRE_MATRIZ; }
            set { _T009_NR_NIRE_MATRIZ = value; }
        }
        public int T009_IN_FORMA_ATUACAO
        {
            get { return _T009_IN_FORMA_ATUACAO; }
            set { _T009_IN_FORMA_ATUACAO = value; }
        }

        public int T009_IN_CENTRO_DISTRIBUICAO
        {
            get { return _T009_IN_CENTRO_DISTRIBUICAO; }
            set { _T009_IN_CENTRO_DISTRIBUICAO = value; }
        }

        public int T009_IN_FRANQUEADO
        {
            get { return _T009_IN_FRANQUEADO; }
            set { _T009_IN_FRANQUEADO = value; }
        }

        public string T009_NR_CNPJ_FRANQUEADOR
        {
            get { return _T009_NR_CNPJ_FRANQUEADOR; }
            set { _T009_NR_CNPJ_FRANQUEADOR = value; }
        }
        public string T009_NR_ATO_LEGAL
        {
            get { return _T009_NR_ATO_LEGAL; }
            set { _T009_NR_ATO_LEGAL = value; }
        }
        public int T009_IN_ATIV_AGROINDUSTRIA
        {
            get { return _T009_IN_ATIV_AGROINDUSTRIA; }
            set { _T009_IN_ATIV_AGROINDUSTRIA = value; }
        }
        public int T009_TIPO_PROPRIEDADE
        {
            get { return _T009_TIP_PROPRIEDADE; }
            set { _T009_TIP_PROPRIEDADE = value; }
        }
        public int T009_TIP_INSCRICAO_ST
        {
            get { return _T009_TIP_INSCRICAO_ST; }
            set { _T009_TIP_INSCRICAO_ST = value; }
        }
        public string T009_NR_CONVENIO
        {
            get { return _T009_NR_CONVENIO; }
            set { _T009_NR_CONVENIO = value; }
        }
        public int T009_TP_ATO_LEGAL
        {
            get { return _T009_TP_ATO_LEGAL; }
            set { _T009_TP_ATO_LEGAL = value; }
        }
        public string T009_COORDENADAS
        {
            get { return _T009_COORDENADAS; }
            set { _T009_COORDENADAS = value; }
        }
        public decimal T009_VL_PATRIMONIO
        {
            get { return _T009_VL_PATRIMONIO; }
            set { _T009_VL_PATRIMONIO = value; }
        }
        public int T009_IN_CANTEIRO_OBRAS
        {
            get { return _T009_IN_CANTEIRO_OBRAS; }
            set { _T009_IN_CANTEIRO_OBRAS = value; }
        }
        public int T009_IN_TRANSPORTE
        {
            get { return _T009_IN_TRANSPORTE; }
            set { _T009_IN_TRANSPORTE = value; }
        }
        public int T009_IN_COMERCIO_EXTERIOR
        {
            get { return _T009_IN_COMERCIO_EXTERIOR; }
            set { _T009_IN_COMERCIO_EXTERIOR = value; }
        }
        public int T009_IN_SUBSTITUTO_TRIB
        {
            get { return _T009_IN_SUBSTITUTO_TRIB; }
            set { _T009_IN_SUBSTITUTO_TRIB = value; }
        }
        #endregion

        #region Implements
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select *   ");
            Sql.AppendLine(" from t099_req_gen_protocolo ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  T005_NR_PROTOCOLO = '" + _T005_NR_PROTOCOLO + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("t099_req_gen_protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
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

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(@" Insert into t099_req_gen_protocolo
                      (
                    T005_NR_PROTOCOLO 
                    ,T099_IN_TIPO_ENQUADRAMENTO 
                    ,T009_CO_PORTE 
                    ,T001_DS_PESSOA_ANT 
                    ,T009_TIP_ESTAB_RFB 
                    ,T009_NR_ULT_REGISTRO
                    ,T009_DT_ULT_REGISTRO 
                    ,T009_NR_CNPJ_MATRIZ 
                    ,T009_UF_MATRIZ
                    ,T009_TIPO_UNIDADE  
                    ,T009_NR_CNPJ_ORG_REG  
                    ,T009_NR_NIRE_MATRIZ 
                    ,T009_IN_FORMA_ATUACAO
                    ,T009_IN_CENTRO_DISTRIBUICAO
                    ,T009_IN_FRANQUEADO
                    ,T009_NR_CNPJ_FRANQUEADOR
                    ,T009_NR_ATO_LEGAL
                    ,T009_IN_ATIV_AGROINDUSTRIA
                    ,T009_TIP_PROPRIEDADE
                    ,T009_TIP_INSCRICAO_ST
                    ,T009_NR_CONVENIO
                    ,T009_TP_ATO_LEGAL
                    ,T009_TIPO_UNIDADE_OUTROS
                    ,T009_COORDENADAS
                    ,T009_VL_PATRIMONIO
                    ,T009_IN_CANTEIRO_OBRAS
                    ,T009_IN_TRANSPORTE
                    ,T009_IN_COMERCIO_EXTERIOR
                    ,T009_IN_SUBSTITUTO_TRIB
                    )
                    Values 
                    (
                    @v_T005_NR_PROTOCOLO 
                    ,@v_T099_IN_TIPO_ENQUADRAMENTO 
                    ,@v_T009_CO_PORTE 
                    ,@v_T001_DS_PESSOA_ANT
                    ,@v_T009_TIP_ESTAB_RFB 
                    ,@v_T009_NR_ULT_REGISTRO
                    ,@v_T009_DT_ULT_REGISTRO
                    ,@v_T009_NR_CNPJ_MATRIZ
                    ,@v_T009_UF_MATRIZ
                    ,@v_T009_TIPO_UNIDADE
                    ,@v_T009_NR_CNPJ_ORG_REG
                    ,@v_T009_NR_NIRE_MATRIZ
                    ,@v_T009_IN_FORMA_ATUACAO
                    ,@v_T009_IN_CENTRO_DISTRIBUICAO
                    ,@v_T009_IN_FRANQUEADO
                    ,@v_T009_NR_CNPJ_FRANQUEADOR
                    ,@v_T009_NR_ATO_LEGAL
                    ,@v_T009_IN_ATIV_AGROINDUSTRIA
                    ,@v_T009_TIP_PROPRIEDADE
                    ,@v_T009_TIP_INSCRICAO_ST
                    ,@v_T009_NR_CONVENIO
                    ,@v_T009_TP_ATO_LEGAL
                    ,@v_T009_TIPO_UNIDADE_OUTROS
                    ,@v_T009_COORDENADAS
                    ,@v_T009_VL_PATRIMONIO
                    ,@v_T009_IN_CANTEIRO_OBRAS
                    ,@v_T009_IN_TRANSPORTE 
                    ,@v_T009_IN_COMERCIO_EXTERIOR
                    ,@v_T009_IN_SUBSTITUTO_TRIB
                      )");

 

            // Codigo Update ******************* 
            SqlU.AppendLine(@" Update     t099_req_gen_protocolo Set 
            		        T099_IN_TIPO_ENQUADRAMENTO = @v_T099_IN_TIPO_ENQUADRAMENTO 
            		        ,T009_CO_PORTE = @v_T009_CO_PORTE 
            		        ,T001_DS_PESSOA_ANT = @v_T001_DS_PESSOA_ANT 
            		        ,T009_TIP_ESTAB_RFB = @v_T009_TIP_ESTAB_RFB 
            		        ,T009_NR_ULT_REGISTRO = @v_T009_NR_ULT_REGISTRO 
            		        ,T009_DT_ULT_REGISTRO = @v_T009_DT_ULT_REGISTRO 
            		        ,T009_NR_CNPJ_MATRIZ = @v_T009_NR_CNPJ_MATRIZ 
            		        ,T009_UF_MATRIZ = @v_T009_UF_MATRIZ 
            		        ,T009_TIPO_UNIDADE = @v_T009_TIPO_UNIDADE 
            		        ,T009_NR_CNPJ_ORG_REG = @v_T009_NR_CNPJ_ORG_REG 
            		        ,T009_NR_NIRE_MATRIZ = @v_T009_NR_NIRE_MATRIZ 
            		        ,T009_IN_FORMA_ATUACAO = @v_T009_IN_FORMA_ATUACAO 
            		        ,T009_IN_CENTRO_DISTRIBUICAO = @v_T009_IN_CENTRO_DISTRIBUICAO 
            		        ,T009_IN_FRANQUEADO = @v_T009_IN_FRANQUEADO 
            		        ,T009_NR_CNPJ_FRANQUEADOR = @v_T009_NR_CNPJ_FRANQUEADOR 
                            ,T009_NR_ATO_LEGAL = @v_T009_NR_ATO_LEGAL 
                            ,T009_IN_ATIV_AGROINDUSTRIA = @v_T009_IN_ATIV_AGROINDUSTRIA 
                            ,T009_TIP_PROPRIEDADE = @v_T009_TIP_PROPRIEDADE 
                            ,T009_TIP_INSCRICAO_ST = @v_T009_TIP_INSCRICAO_ST
                            ,T009_NR_CONVENIO = @v_T009_NR_CONVENIO
                            ,T009_TP_ATO_LEGAL = @v_T009_TP_ATO_LEGAL
                            ,T009_TIPO_UNIDADE_OUTROS = @v_T009_TIPO_UNIDADE_OUTROS
                            ,T009_COORDENADAS = @v_T009_COORDENADAS
                            ,T009_VL_PATRIMONIO = @v_T009_VL_PATRIMONIO
                            ,T009_IN_CANTEIRO_OBRAS = @v_T009_IN_CANTEIRO_OBRAS
                            ,T009_IN_TRANSPORTE = @v_T009_IN_TRANSPORTE 
                            ,T009_IN_COMERCIO_EXTERIOR = @v_T009_IN_COMERCIO_EXTERIOR
                            ,T009_IN_SUBSTITUTO_TRIB = @v_T009_IN_SUBSTITUTO_TRIB
                     Where	 
                     T005_NR_PROTOCOLO = '" + _T005_NR_PROTOCOLO + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T099_IN_TIPO_ENQUADRAMENTO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T099_IN_TIPO_ENQUADRAMENTO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_CO_PORTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_CO_PORTE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T001_DS_PESSOA_ANT", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T001_DS_PESSOA_ANT));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TIP_ESTAB_RFB", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TIP_ESTAB_RFB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_ULT_REGISTRO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_ULT_REGISTRO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_DT_ULT_REGISTRO", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_DT_ULT_REGISTRO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_CNPJ_MATRIZ", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_CNPJ_MATRIZ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_UF_MATRIZ", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_UF_MATRIZ));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TIPO_UNIDADE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TIPO_UNIDADE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_CNPJ_ORG_REG", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_CNPJ_ORG_REG));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_NIRE_MATRIZ", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_NIRE_MATRIZ));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_FORMA_ATUACAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_FORMA_ATUACAO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_CENTRO_DISTRIBUICAO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_CENTRO_DISTRIBUICAO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_FRANQUEADO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_FRANQUEADO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_CNPJ_FRANQUEADOR", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_CNPJ_FRANQUEADOR));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_ATO_LEGAL", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_ATO_LEGAL));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_ATIV_AGROINDUSTRIA", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_ATIV_AGROINDUSTRIA));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TIP_PROPRIEDADE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TIP_PROPRIEDADE));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TIP_INSCRICAO_ST", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TIP_INSCRICAO_ST));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_NR_CONVENIO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_NR_CONVENIO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TP_ATO_LEGAL", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TP_ATO_LEGAL));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_TIPO_UNIDADE_OUTROS", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_TIPO_FORMA_OUTROS));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_COORDENADAS", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_COORDENADAS));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_VL_PATRIMONIO", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_VL_PATRIMONIO));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_CANTEIRO_OBRAS", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_CANTEIRO_OBRAS));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_TRANSPORTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_TRANSPORTE));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_COMERCIO_EXTERIOR", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_COMERCIO_EXTERIOR));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_IN_SUBSTITUTO_TRIB", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_IN_SUBSTITUTO_TRIB));

                
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

        #endregion
    }
}
