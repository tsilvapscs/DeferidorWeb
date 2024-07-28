using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using psc.Application.Siarco;


namespace RCPJ.BLL
{
    [Serializable]
    public class bContabilista:DBInteractionBase
    {
        #region Menbros
        private string _CNPJ_Orgao_Registro = string.Empty;
        private string _nr_Protocolo = string.Empty;
        private string _ds_Pessoa = string.Empty;
        private string _cpfCnpj = string.Empty;
        private int _tip_Class_Empressa = int.MinValue;
        private string _uf_CRC_Empresa = string.Empty;
        private string _co_CRC_empresa = string.Empty;
        private string _tip_CRC_Empresa = string.Empty;
        private string _cpf_Resp = string.Empty;
        private int _tip_Class_Resp = int.MinValue;
        private string _uf_CRC_Resp = string.Empty;
        private string _co_CRC_Resp = string.Empty;
        private string _tip_CRC_Resp = string.Empty;
        private Nullable<DateTime> _dataInscricao;
        #endregion
        #region Propriedades
        public string CNPJ_Orgao_Registro
        {
            get { return _CNPJ_Orgao_Registro; }
            set { _CNPJ_Orgao_Registro = value; }
        }
        public string nr_Protocolo
        {
            get { return _nr_Protocolo; }
            set { _nr_Protocolo = value; }
        }
        public string ds_Pessoa
        {
            get { return _ds_Pessoa; }
            set { _ds_Pessoa = value; }
        }
        public string cpfCnpj
        {
            get { return _cpfCnpj; }
            set { _cpfCnpj = value; }
        }
        public int tip_Class_Empresa
        {
            get { return _tip_Class_Empressa; }
            set { _tip_Class_Empressa = value; }
        }
        public string uf_CRC_Empresa
        {
            get { return _uf_CRC_Empresa; }
            set { _uf_CRC_Empresa = value; }
        }
        public string co_CRC_Empresa
        {
            get { return _co_CRC_empresa; }
            set { _co_CRC_empresa = value; }
        }
        public string tip_CRC_Empresa
        {
            get { return _tip_CRC_Empresa; }
            set { _tip_CRC_Empresa = value; }
        }
        public string cpf_resp
        {
            get { return _cpf_Resp; }
            set { _cpf_Resp = value; }
        }
        public int tip_Class_Resp
        {
            get { return _tip_Class_Resp; }
            set { _tip_Class_Resp = value; }
        }
        public string uf_CRC_Resp
        {
            get { return _uf_CRC_Resp; }
            set { _uf_CRC_Resp = value; }
        }
        public string co_CRC_Resp
        {
            get { return _co_CRC_Resp; }
            set { _co_CRC_Resp = value; }
        }
        public string tip_CRC_Resp
        {
            get { return _tip_CRC_Resp; }
            set { _tip_CRC_Resp = value; }
        }
        public Nullable<DateTime> DataInscricao
        {
            get { return _dataInscricao; }
            set { _dataInscricao = value; }
        }
        #endregion
        #region Implementação
        public DataTable Query()
        {
            DataTable oRetorno = new DataTable();
            using (dt093_Contador c = new dt093_Contador())
            {
                c.CNPJ_Orgao_Registro = _CNPJ_Orgao_Registro;
                c.nr_Protocolo = _nr_Protocolo;
                oRetorno = c.Query();
            }
            return oRetorno;
        }
        public void Update()
        {

            using (dt093_Contador c = new dt093_Contador())
            {
                c.CNPJ_Orgao_Registro = _CNPJ_Orgao_Registro;
                c.nr_Protocolo = _nr_Protocolo;
                c.cpfCnpj =_cpfCnpj;
                c.ds_Pessoa =_ds_Pessoa;
                c.tip_Class_Empresa = _tip_Class_Empressa;
                c.uf_CRC_Empresa =_uf_CRC_Empresa;
                c.co_CRC_Empresa = _co_CRC_empresa;
                c.tip_CRC_Empresa =_tip_CRC_Empresa;
                if (_cpfCnpj.Length == 14)
                {
                    c.cpf_resp = _cpf_Resp;
                    c.tip_Class_Resp = _tip_Class_Resp;
                    c.uf_CRC_Resp = _uf_CRC_Resp;
                    c.co_CRC_Resp = _co_CRC_Resp;
                    c.tip_CRC_Resp = _tip_CRC_Resp;
                    c.DataInscricao = _dataInscricao;
                }
                else
                {
                    c.cpf_resp = "";
                    c.tip_Class_Resp = 0;
                    c.uf_CRC_Resp = "";
                    c.co_CRC_Resp = "";
                    c.tip_CRC_Resp = "";
                    c.DataInscricao = null;
                }
                c.Update();
            }
        }

        
        #endregion
    }
}
