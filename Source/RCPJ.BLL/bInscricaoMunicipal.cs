using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RCPJ.DAL;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bInscricaoMunicipal
    {
        #region Variaveis
        private string _protocolo;
        private string _cnpj;
        private string _nire;
        private string _razaoSocial;
        private string _uf;
        private int _codmunicipio;
        private int _evento;
        private string _cnpjSEFAZ;
        #endregion

        #region  Property Declarations
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        public string Nire
        {
            get { return _nire; }
            set { _nire = value; }
        }
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }
        public string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public int Codmunicipio
        {
            get { return _codmunicipio; }
            set { _codmunicipio = value; }
        }
        public int Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }

        public string CnpjSEFAZ
        {
            get { return _cnpjSEFAZ; }
            set { _cnpjSEFAZ = value; }
        }

        #endregion

        public void EnviaRequerimento(List<string> pEventos)
        {

            try
            {
               
                using (dInscricaoEstadual d = new dInscricaoEstadual())
                {
                    d.Protocolo = _protocolo;

                    d.Codmunicipio = _codmunicipio.ToString();
                    d.CnpjSefaz = _cnpjSEFAZ;
                    d.UF = _uf;
                    d.Nire = _nire;
                    d.Cnpj = _cnpj;
                    d.RazaoSocial = _razaoSocial;
                    d.Evento = _evento;

                    d.EnviaRequerimentoAutonomo(pEventos);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReenviaRequerimento(string pProtocolo)
        {
            using (dInscricaoEstadual d = new dInscricaoEstadual())
            {
                d.Protocolo = pProtocolo;
                d.ReennviaRequerimento();

            }
        }

        public String GetXML()
        {
            string pXml = "";
            try
            {

                using (dInscricaoEstadual d = new dInscricaoEstadual())
                {
                    d.Protocolo = _protocolo;

                    pXml = d.GetXML();

                }
                return pXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}