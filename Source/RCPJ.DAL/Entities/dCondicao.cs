using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL.Entities
{
    public class dCondicao : DBInteractionBase
    {
        #region  Property Declarations
        private decimal _a009_co_condicao;
        private string _a009_ds_condicao;
        private string _a009_in_sociedade;
        private string _a009_in_associacao;
        private string _a009_in_fundacao;
        private decimal _a009_tipo_pessoa;
        private decimal _a009_poder_adminstrar;
        private decimal _a009_obrig_repres;
        private decimal _a009_capital_social;
        private decimal _a009_permite_integralizar;
        private decimal _a009_residencia;
        private decimal _a009_doc_identif;
        private string _a009_in_representante;
        #endregion

        #region Class Member Declarations
        public decimal a009_co_condicao
        {
            get { return _a009_co_condicao; }

            set { _a009_co_condicao = value; }
        }

        public string a009_ds_condicao
        {
            get { return _a009_ds_condicao; }

            set { _a009_ds_condicao = value; }
        }

        public string a009_in_sociedade
        {
            get { return _a009_in_sociedade; }

            set { _a009_in_sociedade = value; }
        }

        public string a009_in_associacao
        {
            get { return _a009_in_associacao; }

            set { _a009_in_associacao = value; }
        }

        public string a009_in_fundacao
        {
            get { return _a009_in_fundacao; }

            set { _a009_in_fundacao = value; }
        }

        public decimal a009_tipo_pessoa
        {
            get { return _a009_tipo_pessoa; }

            set { _a009_tipo_pessoa = value; }
        }

        public decimal a009_poder_adminstrar
        {
            get { return _a009_poder_adminstrar; }

            set { _a009_poder_adminstrar = value; }
        }

        public decimal a009_obrig_repres
        {
            get { return _a009_obrig_repres; }

            set { _a009_obrig_repres = value; }
        }

        public decimal a009_capital_social
        {
            get { return _a009_capital_social; }

            set { _a009_capital_social = value; }
        }

        public decimal a009_permite_integralizar
        {
            get { return _a009_permite_integralizar; }

            set { _a009_permite_integralizar = value; }
        }

        public decimal a009_residencia
        {
            get { return _a009_residencia; }

            set { _a009_residencia = value; }
        }

        public decimal a009_doc_identif
        {
            get { return _a009_doc_identif; }

            set { _a009_doc_identif = value; }
        }

        public string a009_in_representante
        {
            get { return _a009_in_representante; }

            set { _a009_in_representante = value; }
        }

        #endregion 

        
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Select ");
            Sql.AppendLine("		a009_co_condicao, ");
            Sql.AppendLine("		a009_ds_condicao, ");
            Sql.AppendLine("		a009_in_sociedade, ");
            Sql.AppendLine("		a009_in_associacao, ");
            Sql.AppendLine("		a009_in_fundacao, ");
            Sql.AppendLine("		a009_tipo_pessoa, ");
            Sql.AppendLine("		a009_poder_adminstrar, ");
            Sql.AppendLine("		a009_obrig_repres, ");
            Sql.AppendLine("		a009_capital_social, ");
            Sql.AppendLine("		a009_permite_integralizar, ");
            Sql.AppendLine("		a009_residencia, ");
            Sql.AppendLine("		a009_doc_identif, ");
            Sql.AppendLine("		a009_in_representante");
            Sql.AppendLine(" From	a009_condicao");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("a009_condicao");
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
    }
}
