using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dR006_Natureza_Juridica_Tipo : DBInteractionBase
    {
        #region  Property Declarations
        protected decimal _co_natureza_juridica;
        protected decimal _co_tipo_pessoa_juridica;
        #endregion

        #region Class Member Declarations
        public decimal co_natureza_juridica
        {
            get { return _co_natureza_juridica; }
            set { _co_natureza_juridica = value; }
        }
        public decimal co_tipo_pessoa_juridica
        {
            get { return _co_tipo_pessoa_juridica; }
            set { _co_tipo_pessoa_juridica = value; }
        }
        #endregion

        #region Implements
        public DataTable  EncontraTipoPessoaJuridica(int CodNaturezaJuridica)
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		nj.a006_co_natureza_juridica, ");
            Sql.AppendLine("		nj.a018_co_tipo_pessoa_juridica, ");
            Sql.AppendLine("        pj.a018_ds_tipo_pessoa_Juridica, ");
            Sql.AppendLine("        n.t009_ds_natureza_juridica ");
            
            Sql.AppendLine(" From	R006_Natureza_Juridica_Tipo as nj");
            Sql.AppendLine(" inner join a018_tipo_pessoa_juridica as pj");
            Sql.AppendLine(" on nj.a018_co_tipo_pessoa_juridica = pj.a018_co_tipo_pessoa_juridica");
            Sql.AppendLine(" join t009_natureza_juridica as n");
            Sql.AppendLine(" on n.t009_co_natureza_juridica = nj.a006_co_natureza_juridica ");
            Sql.AppendLine(" Where	1 = 1 ");

            //TODO: Implements Where Clause Here!!!! 
            Sql.AppendLine(" And	a006_co_natureza_juridica = " + CodNaturezaJuridica  );
            


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
