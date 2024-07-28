using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class ProcessoAnalista
    {
        #region Variables
        private string _PROTOCOLO = "";
        private string _cod_usuario = "";
        private int _SEQ_ANDAMENTO = 0;
        private int _COD_STATUS = 0;

        #endregion

        #region Class Member Declarations
        public string PROTOCOLO
        {
            get { return _PROTOCOLO; }
            set { _PROTOCOLO = value; }
        }
        public string COD_USUARIO
        {
            get { return _cod_usuario; }
            set { _cod_usuario = value; }
        }
        public int SEQ_ANDAMENTO
        {
            get { return _SEQ_ANDAMENTO; }
            set { _SEQ_ANDAMENTO = value; }
        }
        public int COD_STATUS
        {
            get { return _COD_STATUS; }
            set { _COD_STATUS = value; }
        }
        #endregion

        public void Update()
        {
            using (dProcessoAnalista obj = new dProcessoAnalista())
            {
                obj.PROTOCOLO = _PROTOCOLO;
                obj.COD_STATUS = _COD_STATUS;
                obj.SEQ_ANDAMENTO = _SEQ_ANDAMENTO;
                obj.COD_USUARIO = _cod_usuario;
                obj.Update();
            }

        }
        public void UpdateProtocoloImagem(string _protocolo, int _status)
        {
            using (dProcessoAnalista obj = new dProcessoAnalista())
            {
                obj.UpdateProtocoloImagem(_protocolo, _status);
            }
        }

        public DataTable GetProcessosFuncionario()
        {
            using (dProcessoAnalista obj = new dProcessoAnalista())
            {
                obj.COD_USUARIO = _cod_usuario;
                return obj.GetProcessosFuncionario();
            }
        }
        public static bool IsProcessoDigital(string pProtocolo)
        {
            using (dProcessoAnalista obj = new dProcessoAnalista())
            {
                return obj.IsProcessoDigital(pProtocolo);
            }
        }
    }
}
