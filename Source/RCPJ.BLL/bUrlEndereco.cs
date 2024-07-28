using System;
using System.Collections.Generic;
using System.Text;

namespace RCPJ.BLL
{
    [Serializable]
    public class bUrlEndereco
    {

        private string _uf = "";
        private string _url = "";

        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }
        

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
