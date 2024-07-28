using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using psc.Framework;
using MySql.Data.MySqlClient;
using RCPJ.DAL.ConnectionBase;

namespace RCPJ.DAL.Entities
{
    public class dCnt_Protocolo_Filial : DBInteractionBase
    {
        private string _cod_protocolo;
        private string _reque_protocolo;
        private decimal _num_filial = decimal.MinValue;

       
        private string _tipo_logradouro;
        private string _logradouro;
        private string _bairro;
        private string _cep;
        private string _num_logradouro;
        private string _comp_logradouro;
        private string _uf;
        private decimal _tmu_cod_mun;
        private string _num_viabilidade;
        

     

        public decimal tmu_cod_mun
        {
            get { return _tmu_cod_mun; }
            set { _tmu_cod_mun = value; }
        }

        public string uf
        {
            get { return _uf; }
            set { _uf = value; }
        }


        public string cod_protocolo
        {
            get { return _cod_protocolo; }
            set { _cod_protocolo = value; }
        }

        public string reque_protocolo
        {
            get { return _reque_protocolo; }
            set { _reque_protocolo = value; }
        }
        public string tipo_logradouro
        {
            get { return _tipo_logradouro; }
            set { _tipo_logradouro = value; }
        }
        public string logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }        
        public string bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public string cep
        {
            get { return _cep; }
            set { _cep = value; }
        }        
        public string num_logradouro
        {
            get { return _num_logradouro; }
            set { _num_logradouro = value; }
        }
        public string comp_logradouro
        {
            get { return _comp_logradouro; }
            set { _comp_logradouro = value; }
        }
        public decimal num_filial
        {
            get { return _num_filial; }
            set { _num_filial = value; }
        }

        public string num_viabilidade
        {
            get { return _num_viabilidade; }
            set { _num_viabilidade = value; }
        }

   
        

        #region Implements
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into cnt_protocolo_filial");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	cod_protocolo, ");
            SqlI.AppendLine("	tipo_logradouro, ");
            SqlI.AppendLine("	logradouro, ");
            SqlI.AppendLine("	bairro, ");
            SqlI.AppendLine("	cep, ");            
            SqlI.AppendLine("	num_logradouro, ");
            SqlI.AppendLine("	comp_logradouro, ");         
            SqlI.AppendLine("	uf, ");
            SqlI.AppendLine("	tmu_cod_mun,");
            SqlI.AppendLine("	num_filial, ");
            SqlI.AppendLine("	num_viabilidade, ");
            SqlI.AppendLine("	reque_protocolo ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_cod_protocolo, ");
            SqlI.AppendLine("	@v_tipo_logradouro, ");
            SqlI.AppendLine("	@v_logradouro, ");
            SqlI.AppendLine("	@v_bairro, ");
            SqlI.AppendLine("	@v_cep, ");            
            SqlI.AppendLine("	@v_num_logradouro, ");
            SqlI.AppendLine("	@v_comp_logradouro, ");
            SqlI.AppendLine("	@v_uf, ");
            SqlI.AppendLine("	@v_tmu_cod_mun, ");
            SqlI.AppendLine("	@v_num_filial, ");
            SqlI.AppendLine("	@v_num_viabilidade,");
            SqlI.AppendLine("	@v_reque_protocolo");
            
         
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     cnt_protocolo_filial Set ");
            //SqlU.AppendLine("		cod_protocolo = @v_cod_protocolo, ");
            SqlU.AppendLine("		tipo_logradouro = @v_tipo_logradouro, ");
            SqlU.AppendLine("		logradouro = @v_logradouro, ");
            SqlU.AppendLine("		bairro = @v_bairro, ");
            SqlU.AppendLine("		cep = @v_cep, ");            
            SqlU.AppendLine("		num_logradouro = @v_num_logradouro, ");
            SqlU.AppendLine("		comp_logradouro = @v_comp_logradouro, ");
            SqlU.AppendLine("		uf = @v_uf, ");
            SqlU.AppendLine("	    tmu_cod_mun = @v_tmu_cod_mun, ");
            SqlU.AppendLine("	    num_viabilidade = @v_num_viabilidade, ");
            SqlU.AppendLine("	    reque_protocolo = @v_reque_protocolo");
            SqlU.AppendLine(" Where	1 = 1 ");
            SqlU.AppendLine(" And	cod_protocolo =  @v_cod_protocolo");
            SqlU.AppendLine(" And	reque_protocolo = @v_reque_protocolo");
            SqlU.AppendLine(" And   num_filial = @v_num_filial ");   


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cod_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cod_protocolo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tmu_cod_mun", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tmu_cod_mun));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_tipo_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _cep));                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_comp_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _comp_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_viabilidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_viabilidade));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_num_filial", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _num_filial));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_reque_protocolo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _reque_protocolo));
                
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

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <returns></returns>
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select * ");
            //Sql.AppendLine("		cod_protocolo, ");
            //Sql.AppendLine("	    tipo_logradouro, ");
            //Sql.AppendLine("		logradouro, ");
            //Sql.AppendLine("		bairro, ");
            //Sql.AppendLine("		cep, ");
            //Sql.AppendLine("		num_logradouro, ");
            //Sql.AppendLine("		comp_logradouro, ");
            //Sql.AppendLine("		uf,");
            //Sql.AppendLine("		tmu_cod_mun, ");
            ////Sql.AppendLine("		num_filial, ");
            ////Sql.AppendLine("		num_viabilidade,  ");
            //Sql.AppendLine("		reque_protocolo ");
            Sql.AppendLine(" From	vw_dCnt_protocolo_filial");
            Sql.AppendLine(" Where	1 = 1 ");

           
            //Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");
            Sql.AppendLine(" And	reque_protocolo = '" + _reque_protocolo + "'");
            //if (_num_filial != null && _num_filial != decimal.MinValue)
            //{
            //    Sql.AppendLine(" And num_filial = '" + _num_filial + "'");
            //}
            Sql.AppendLine(" ORDER BY t001_sq_pessoa ");
           

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("vw_dCnt_protocolo_filial");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            
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

        public void DeleteTodos()
        {
            StringBuilder Sql = new StringBuilder();
            StringBuilder SqlUpdate = new StringBuilder();

            Sql.AppendLine(" DELETE From cnt_protocolo_filial");
            Sql.AppendLine(" Where	1 = 1 ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                if (cmdToExecute.ExecuteNonQuery() > 0)
                {
                    
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

            }
        }
        /// <summary>
        /// Delete a filial da base
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            StringBuilder Sql = new StringBuilder();
              StringBuilder SqlUpdate = new StringBuilder();
  
            Sql.AppendLine(" DELETE From cnt_protocolo_filial");
            Sql.AppendLine(" Where	1 = 1 ");


            Sql.AppendLine(" And	cod_protocolo = '" + _cod_protocolo + "'");           
            Sql.AppendLine(" And num_filial = '" + _num_filial + "'");
      

            SqlUpdate.AppendLine(" UPDATE cnt_protocolo_filial c");
            SqlUpdate.AppendLine(" SET NUM_FILIAL = NUM_FILIAL-1");
            SqlUpdate.AppendLine(" where COD_PROTOCOLO = '" + _cod_protocolo + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
        
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection. 
                _mainConnection.Open();

                // Execute query. 

                if (cmdToExecute.ExecuteNonQuery() > 0)
                {
                    cmdToExecute.CommandText = SqlUpdate.ToString();
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
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
              
            }
        }
    

        #endregion


    }
}
