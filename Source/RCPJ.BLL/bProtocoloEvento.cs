using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bProtocoloEvento
    {
        #region Class Member Declarations

        protected string _CnpjOrgaoRegistro;
        protected string _ProtocoloRequerimento;
        protected decimal _CodigoEvento;
        protected decimal _CodigoAto;
        protected string _ProtocoloViabilidade;
        private string _descricaoEvento;
        private string _descricaoResumida;
        private int _sqPessoa;

       

        #endregion

        #region Class Property Declarations

        public string CnpjOrgaoRegistro
        {
            get { return _CnpjOrgaoRegistro; }
            set { _CnpjOrgaoRegistro = value; }
        }
        public string ProtocoloRequerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }
        public decimal CodigoEvento
        {
            get { return _CodigoEvento; }
            set { _CodigoEvento = value; }
        }
        public decimal CodigoAto
        {
            get { return _CodigoAto; }
            set { _CodigoAto = value; }
        }
        public string ProtocoloViabilidade
        {
            get { return _ProtocoloViabilidade; }
            set { _ProtocoloViabilidade = value; }
        }
        public string DescricaoEvento
        {
            get { return _descricaoEvento; }
            set { _descricaoEvento = value; }
        }
        public string DescricaoResumida
        {
            get { return _descricaoResumida; }
            set { _descricaoResumida = value; }
        }
        #endregion

        public int SqPessoa
        {
            get { return _sqPessoa; }
            set { _sqPessoa = value; }
        }

        #region Implements
        public static List<bProtocoloEvento> getListAtos()
        {
            List<bProtocoloEvento> _listEventos = new List<bProtocoloEvento>(); 
            using(dA002_Ato obj = new dA002_Ato())
            {
                DataTable dt = obj.getAtos();
                foreach (DataRow row in dt.Rows)
                {
                    bProtocoloEvento ev = new bProtocoloEvento();
                    ev.CodigoEvento = Decimal.Parse(row["a002_co_ato"].ToString());
                    ev.DescricaoEvento = row["a002_ds_ato"].ToString();
                    _listEventos.Add(ev);

                }
            }

            return _listEventos;
        }
        #endregion
    }
}
