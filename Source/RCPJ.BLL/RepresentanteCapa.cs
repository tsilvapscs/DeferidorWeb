using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;

namespace RCPJ.BLL
{
    public class RepresentanteCapa : DBInteractionBase
    {
        #region Variables
        private string _nome;
        private string _ddd;
        private string _telefone;
        private string _local;
        private string _data;
        #endregion

        #region properties
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string Ddd
        {
            get { return _ddd; }
            set { _ddd = value; }
        }
        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }
        public string Local
        {
            get { return _local; }
            set { _local = value; }
        }
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
        #endregion
    }
}
