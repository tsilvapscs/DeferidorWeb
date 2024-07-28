using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
namespace RCPJ.BLL
{
    [Serializable]
    public class bLeiloeiro : DBInteractionBase
    {
        #region Class Member Declarations

        private int _matricula;
        private String _Nome;
        private String _cpf;
        private String _numArquivamento = string.Empty;
        #endregion

        #region Class Property Declarations
        public int Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }
        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public String Cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        public String NumArquivamento
        {
            get { return _numArquivamento; }
            set { _numArquivamento = value; }
        }
        #endregion

          #region Constructors
        public bLeiloeiro()
        {
            InitClass();
        }

        public bLeiloeiro(int pMatricula)
            : this()
        {
            _matricula = pMatricula;
            Populate();
        }

        private void InitClass()
        {
            _matricula = 0;
            _Nome = string.Empty;
            _cpf = string.Empty;
            _numArquivamento = string.Empty;
        }

        private void Populate()
        {

            if (_matricula == 0)
                return;

            DataTable dt = Query();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    _matricula = Int32.Parse(dr["NRO_MATR_LEILOEIRO"].ToString());
                    _Nome = dr["NOME_LEILOEIRO"].ToString();
                    _cpf = dr["CPF"].ToString();
                    _numArquivamento = dr["NRO_ARQUIVAMENTO_LEILOEIRO"].ToString();

                }
            }
            else
            {
                _matricula = 0;
            }
        }

        #endregion

        #region Data Acces
        public DataTable Query()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"Select NRO_MATR_LEILOEIRO, 
                                    NOME_LEILOEIRO,  
                                    CPF, 
                                    NRO_ARQUIVAMENTO_LEILOEIRO
                            From jucerja..leiloeiros
                            where COD_TPEXERCICIO = 1 and dta_baixa is null and NRO_MATR_LEILOEIRO is not null
                                  And NRO_MATR_LEILOEIRO = " + _matricula.ToString());
                            

            return dHelperSQL.ExecuteQuery(sql.ToString());   

        }

        #endregion
    }
}
