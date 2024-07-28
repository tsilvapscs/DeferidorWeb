using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bTransporte
    {
        string _placa = "";
        string _uf = "";
        string _municipio = "";
        string _propietario = "1";
        int _acao = 0;

        public int Acao
        {
            get { return _acao; }
            set { _acao = value; }
        }

        public string Propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
        }

        public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public string DescAcao
        {
            get { return _acao == 1 ? "Inclusão" : "Exclusão"; }
        }
        public string DescPropietario
        {
            get { return _propietario == "1" ? "Próprio" : "Não Próprio"; }
        }

    }
}
