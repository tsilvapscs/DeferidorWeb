using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.Data;
namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoConteudo
    {
        #region Variables
        private int _t030_id_contrato;
        private int _t031_sq_clausula;
        private string _t031_texto_clausula;
        private string _t031_texto_original;
        private int _t031_sq_clausula_mae;
        private int _t031_texto_editavel;
        private int _t031_adiciona_paragrafo;
        private int _t031_fixa_final;
        private int _t031_in_numeracao;
        private int _t031_in_adicional;


        private string numclausula;
        private string numclausulaMae;
        private List<bContratoCampo> _campos = new List<bContratoCampo>();
        private int _adicional = 2;
       

       
        #endregion

        #region Declarations
        public int T030_id_contrato
        {
            get { return _t030_id_contrato; }
            set { _t030_id_contrato = value; }
        }
        
        public int T031_sq_clausula
        {
            get { return _t031_sq_clausula; }
            set { _t031_sq_clausula = value; }
        }
        

        public string T031_texto_clausula
        {
            get { return _t031_texto_clausula; }
            set { _t031_texto_clausula = value; }
        }
        

        public int T031_sq_clausula_mae
        {
            get { return _t031_sq_clausula_mae; }
            set { _t031_sq_clausula_mae = value; }
        }
        

        public int T031_texto_editavel
        {
            get { return _t031_texto_editavel; }
            set { _t031_texto_editavel = value; }
        }
        

        public int T031_adiciona_paragrafo
        {
            get { return _t031_adiciona_paragrafo; }
            set { _t031_adiciona_paragrafo = value; }
        }
        
        public int T031_fixa_final
        {
            get { return _t031_fixa_final; }
            set { _t031_fixa_final = value; }
        }

        public List<bContratoCampo> Campos
        {
            get { return _campos; }
            set { _campos = value; }
        }
        public string T031_texto_original
        {
            get { return _t031_texto_original; }
            set { _t031_texto_original = value; }
        }
        public int Adicional
        {
            get { return _adicional; }
            set { _adicional = value; }
        }
        public string NumclausulaMae
        {
            get { return numclausulaMae; }
            set { numclausulaMae = value; }
        }

        public string Numclausula
        {
            get { return numclausula; }
            set { numclausula = value; }
        }
        public int T031_in_numeracao
        {
            get { return _t031_in_numeracao; }
            set { _t031_in_numeracao = value; }
        }
        public int T031_in_adicional
        {
            get { return _t031_in_adicional; }
            set { _t031_in_adicional = value; }
        }
        #endregion

        public void Insert(string _requerimento)
        {
           
            using (dContratoClausula c = new dContratoClausula())
            {
                c.T005_nr_requerimento = _requerimento;
                c.T030_id_contrato = _t030_id_contrato;
                c.T035_sq_clausula = _t031_sq_clausula;
                c.T035_texto = _t031_texto_clausula;
                c.T031_sq_clausula_mae = _t031_sq_clausula_mae;

                c.Insert();
                _t031_sq_clausula = c.T035_sq_clausula;

            }
        }

        public void Update(string _requerimento)
        {
            bool _existe = false;
            bool _clausulaAdicional = true;

            using (dContratoClausula c = new dContratoClausula())
            {
                c.T005_nr_requerimento = _requerimento;
                c.T030_id_contrato = _t030_id_contrato;
                c.T035_sq_clausula = _t031_sq_clausula;
                c.T035_texto = _t031_texto_clausula;
                //Se _t031_sq_clausula < 1000 é porque estou editando uma clausula padrao
                // gravo na tabela t035_contrato_clausula com  _t031_sq_clausula_mae =  _t031_sq_clausula
                
                if (_t031_sq_clausula < 1000)
                {
                    c.T031_sq_clausula_mae = _t031_sq_clausula;
                    string ret = c.ConsultaExisteClausula();
                    if (ret != "")
                    {
                        c.T035_sq_clausula = Int32.Parse(ret);
                        _existe = true;
                    }
                }
                else
                {
                    _clausulaAdicional = false;
                    c.T031_sq_clausula_mae = _t031_sq_clausula_mae;
                    if (c.GetClausula() != "")
                        _existe = true;
                }
                
                if (!_existe)
                {
                    
                    c.Insert();
                    if (_clausulaAdicional)
                        _t031_sq_clausula = c.T035_sq_clausula;
                }
                else
                {
                    c.Update();
                }
                
            }
        }
        public void Update_old(string _requerimento)
        {
            bool _existe = false;
            bool _clausulaAdicional = true;

            using (dContratoClausula c = new dContratoClausula())
            {
                c.T005_nr_requerimento = _requerimento;
                c.T030_id_contrato = _t030_id_contrato;
                c.T035_sq_clausula = _t031_sq_clausula;
                c.T035_texto = _t031_texto_clausula;
                //Se _t031_sq_clausula < 1000 é porque estou editando uma clausula padrao
                // gravo na tabela t035_contrato_clausula com  _t031_sq_clausula_mae =  _t031_sq_clausula

                if (_t031_sq_clausula < 1000)
                {
                    c.T031_sq_clausula_mae = _t031_sq_clausula_mae;
                    string ret = c.ConsultaExisteClausula();
                    if (ret != "")
                    {
                        c.T035_sq_clausula = Int32.Parse(ret);
                        _existe = true;
                    }
                }
                else
                {
                    _clausulaAdicional = false;
                    c.T031_sq_clausula_mae = _t031_sq_clausula_mae;
                    if (c.GetClausula() != "")
                        _existe = true;
                }

                if (!_existe)
                {
                    c.Insert();
                    if (_clausulaAdicional)
                        _t031_sq_clausula = c.T035_sq_clausula;
                }
                else
                {
                    c.Update();
                }

            }
        }
        public void Delete()
        {
            using (dContratoClausula c = new dContratoClausula())
            {
                c.T035_sq_clausula = _t031_sq_clausula;
                c.Delete();
            }
        }
    }
}
