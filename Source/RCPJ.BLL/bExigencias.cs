using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace RCPJ.BLL
{
    [Serializable]
    public class bExigencias
    {
        #region Class Member Declarations

        private String _CodExigencia;

        private String _Descricao;

        private String _ProtocoloRequerimento;

        private String _FundamentoLegal;

        //private string _codigoOutrasExigenciasPadrao;

        private string _grupo;
        
        private int _origem;
        
        private string _andamento_secao;
        
        private int _andamento_seq;

        private Nullable<DateTime> _dt_Inclusão = null;
        #endregion

        #region Class Property Declarations

        public int Origem
        {
            get { return _origem;}
            set { _origem = value; }
        }


        public String Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        public string CodigoOutrasExigenciasPadrao
        {
            get 
            {
                return ConfigurationManager.AppSettings["CodigoOutrasExigenciasPadrao"] != null ? ConfigurationManager.AppSettings["CodigoOutrasExigenciasPadrao"].ToString() : "";
            }
            
        }

        public String CodExigencia
        {
            get { return _CodExigencia; }
            set { _CodExigencia = value; }
        }
        public String Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public String ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }

        public String FundamentoLegal
        {
            get { return _FundamentoLegal; }
            set { _FundamentoLegal = value; }
        }
        public string AndamentoSecao
        {
            get { return _andamento_secao; }
            set { _andamento_secao = value; }
        }
        public int AndamentoSeq
        {
            get { return _andamento_seq; }
            set { _andamento_seq = value; }
        }
        public Nullable<DateTime> dtInclusao
        {
            get { return _dt_Inclusão; }
            set { _dt_Inclusão = value; }
        }
        #endregion

        #region Implements


        #endregion
    }
}
