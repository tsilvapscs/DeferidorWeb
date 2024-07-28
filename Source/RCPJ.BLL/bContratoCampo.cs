using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoCampo
    {
        #region Variables
        private int _t030_id_contrato;
        private int _t031_sq_clausula;
        private int _t032_sq_campo;
        private string _t032_nome_campo;
        private int _t032_origem_campo;
        private int _t032_tipo_campo;
        private string _t032_descricao_campo;
        private string _t032_titulo_campo;
        private int _t032_sq_campo_depende;

        
        private string _resposta_campo;

        #endregion

        #region Declarations

        public string Resposta_campo
        {
            get { return _resposta_campo; }
            set { _resposta_campo = value; }
        }
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
        public int T032_sq_campo
        {
            get { return _t032_sq_campo; }
            set { _t032_sq_campo = value; }
        }
        public string T032_nome_campo
        {
            get { return _t032_nome_campo; }
            set { _t032_nome_campo = value; }
        }
        public int T032_origem_campo
        {
            get { return _t032_origem_campo; }
            set { _t032_origem_campo = value; }
        }
        public int T032_tipo_campo
        {
            get { return _t032_tipo_campo; }
            set { _t032_tipo_campo = value; }
        }
        public string T032_descricao_campo
        {
            get { return _t032_descricao_campo; }
            set { _t032_descricao_campo = value; }
        }
        public string T032_titulo_campo
        {
            get { return _t032_titulo_campo; }
            set { _t032_titulo_campo = value; }
        }
        public int T032_sq_campo_depende
        {
            get { return _t032_sq_campo_depende; }
            set { _t032_sq_campo_depende = value; }
        }
        #endregion
    }
}
