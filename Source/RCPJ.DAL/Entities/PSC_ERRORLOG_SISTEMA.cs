using System;
using System.Data;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Text;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;


namespace RCPJ.DAL.Entities
{
	/// <summary>
	/// Summary description for PSC_ERRORLOG_SISTEMA.
	/// </summary>
	public class PSC_ERRORLOG_SISTEMA : DBInteractionBase
	{
		#region Class Member Declarations
        protected decimal _pes_id=0;
		protected string			 _pes_source="", _pes_targetsite="", _pes_stacktrace="", _pes_message="", _pes_usuario="";
        protected DateTime _pes_fec_inc = DateTime.Now; //General.DataMinValue();

        protected string _tIR_NOM_RESP = "";

		#endregion

		#region Class Property Declarations

      
        public decimal pes_id
        {
            get
            {
                return (decimal)_pes_id;
            }
            set
            {
                _pes_id = value;
            }
        }

        public string pes_usuario
		{
			get
			{
				return (string)_pes_usuario;
			}
			set
			{
				_pes_usuario =  value;
			}
		}
		public string pes_source
		{
			get
			{
				return (string)_pes_source;
			}
			set
			{
				_pes_source =  value;
			}
		}
		public string pes_targetsite
		{
			get
			{
				return (string)_pes_targetsite;
			}
			set
			{
				_pes_targetsite =  value;
			}
		}
		public string pes_stacktrace
		{
			get
			{
				return (string)_pes_stacktrace;
			}
			set
			{
				_pes_stacktrace =  value;
			}
		}
		public string pes_message
		{
			get
			{
				return (string)_pes_message;
			}
			set
			{
				_pes_message =  value;
			}
		}
        public DateTime pes_fec_inc
        {
            
            get
            {
                return (DateTime)_pes_fec_inc;
            }
            
    
            set
            {
                
                _pes_fec_inc = value;
            }
        }

        public string tIR_NOM_RESP
        {
            get
            {
                return (string)_tIR_NOM_RESP;
            }
            set
            {
                _tIR_NOM_RESP = value;
            }
        }



		#endregion 

		public void Insert()
		{
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = "PSC_ERRORLOG_SISTEMA_insert";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {


                cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_source", MySqlDbType.VarChar, 300, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_source));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_targetsite", MySqlDbType.VarChar, 300, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_targetsite));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_stacktrace", MySqlDbType.VarChar, 2000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_stacktrace));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_message", MySqlDbType.VarChar, 2000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_message));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_usuario", MySqlDbType.VarChar, 2000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_usuario));





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
       
        public DataTable Query()
        {
            return Query(false);

        }
        public DataTable Query(bool Count)
        {
            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            
            if (Count)
            {
                //Sql.AppendLine("Set dateformat dmy ");
                Sql.AppendLine(" Select count(0) Quantidade");
            }
            else
            {
                Sql.AppendLine(" Select top 100");
                Sql.AppendLine("		* ");
                

            }
            
            Sql.AppendLine(" From	PSC_ERRORLOG_SISTEMA");
            //Sql.AppendLine(" From	TAB_INST_RESPONSAVEL INNER JOIN  PSC_ERRORLOG_SISTEMA ON TAB_INST_RESPONSAVEL.TIR_CPF_RESP = PSC_ERRORLOG_SISTEMA.PES_USUARIO");
            
            Sql.AppendLine(" Where	1 = 1 ");



            if (_pes_id != int.MinValue )
            {
                Sql.AppendLine(" And 	PSC_ERRORLOG_SISTEMA.pes_id = '" + _pes_id + "' ");


            }
            
                if (_pes_source != "")
                {
                    Sql.AppendLine(" And	PSC_ERRORLOG_SISTEMA.pes_source = '" + _pes_source + "'");
                }
            
                if (_pes_message != "")
                {

                    Sql.AppendLine(" And	PSC_ERRORLOG_SISTEMA.pes_message = @v_pes_message ");
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_pes_message", MySqlDbType.VarChar, 2000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pes_message));
                }

                if (_pes_targetsite != "")
                {
                    Sql.AppendLine(" And	PSC_ERRORLOG_SISTEMA.pes_targetsite = '" + pes_targetsite + "'");
                }
                if (_pes_stacktrace != "")
                {
                    Sql.AppendLine(" And	PSC_ERRORLOG_SISTEMA.pes_stacktrace = '" + pes_stacktrace + "'");
                }

                if (_pes_usuario != "")
                {
                    Sql.AppendLine(" And	PSC_ERRORLOG_SISTEMA.pes_usuario = '" + pes_usuario + "'");
                }

                if (_pes_fec_inc != General.DataMinValue())
                {

                    //Sql.AppendLine(" And CAST(FLOOR(CAST( pes_fec_inc AS float)) AS datetime) = CONVERT(VARCHAR,'02/09/2009',106 )");


                    Sql.AppendLine(" And CAST(FLOOR(CAST( PSC_ERRORLOG_SISTEMA.pes_fec_inc AS float)) AS DATETIME) = CONVERT(VARCHAR,'" + pes_fec_inc.ToString("yyyyMMdd") + "',106 )");
                    
                }
            /*
                if (_tIR_NOM_RESP != "")
                {
                    Sql.AppendLine(" And 	TAB_INST_RESPONSAVEL.tIR_NOM_RESP = '" + _tIR_NOM_RESP + "' ");
                    //Sql.AppendLine(" And 	TAB_INST_RESPONSAVEL.tIR_NOM_RESP = @v_TIR_NOM_RESP ");
                    //cmdToExecute.Parameters.Add(new MySqlParameter("v_TIR_NOM_RESP", MySqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tIR_NOM_RESP));
                    //Sql.AppendLine(" Any (SELECT * FROM tAB_INST_RESPONSAVEL WHERE 1=1 AND  tAB_INST_RESPONSAVEL.tIR_NOM_RESP = PSC_ERRORLOG_SISTEMA.pes_usuario )");

                }
           */
            
            //SqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("PSC_ERRORLOG_SISTEMA");
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
	}
}
