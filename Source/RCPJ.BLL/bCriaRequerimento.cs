using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using RCPJ.DAL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Ruc;
using System.Xml;


namespace RCPJ.BLL
{
    [Serializable]
    public class bCriaRequerimento
    {
        #region Class Member Declarations
        //private bRequerimento _req;
        private DataSet result;
        #endregion

        public void Criar(string _xml)
        {

            //coleta LoadedObj = (coleta)SerializerObj.Deserialize(_xml);

            //Transforma o xml num dataSet
            result = new DataSet();
            XmlTextReader reader = new XmlTextReader(new StringReader(_xml));
            result.ReadXml(reader);

            bRequerimento req = new bRequerimento();
            DataTable empresa = new DataTable();
            empresa = result.Tables["dadosgerais"];

            DataTable IdentificacaoEmpresa = result.Tables["IdentificacaoEmpresa"];

            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["NireMatricula"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["nomeEmpresarial"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["nomeFantasia"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["naturezaJuridica"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["dataInicioAtividade"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["dataAssinatura"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["capitalSocial"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["indicaIntegralizaCapital"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["capitalIntegralizado"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["porte"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["ddd"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["telefone1"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["telefone2"].ToString();
            req.nrMatricula = IdentificacaoEmpresa.Rows[0]["email"].ToString();

        }

    }
}
