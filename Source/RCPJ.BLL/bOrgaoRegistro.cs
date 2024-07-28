using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;
using System.Configuration.Assemblies;

namespace RCPJ.BLL
{
    [Serializable]
    public class bOrgaoRegistro
    { //ff
        #region Variables
        private string _cnpj = string.Empty;
        private string _tipo = string.Empty;
        private string _descricao = string.Empty;
        private string _descricaoCombo = string.Empty;
        private string _sigla = string.Empty;
        private string _uf = string.Empty;
        private int _codigo_municipio = -1;
        private string _ds_municipio = string.Empty;
        private string _ds_bairro = string.Empty;
        private int _co_tipo_logradouro = 1;
        private string _ds_tipo_logradouro = string.Empty;
        private string _ds_logradouro = string.Empty;
        private string _numero = string.Empty;
        private string _complemento = string.Empty;
        private string _cep = string.Empty;
        private string _codNire = string.Empty;
        private int _imprimeCapa = 2;
        private int _geraProtocolo = 2;
        private int _imprimeObjSocial = 2;
        private int _gravaTabelaPreparo = 2;
        private int _tipoDocumentoGerado = 1;
        private int _T004_IN_CARREGA_DBE=1;
        private string _wsViabilidade = "";
        private string _urlConsultaEmpresa = "";
        private string _codigoDBE = "";
        private string _WsRegin = "";
        private string _ds_pais;
        private string _ds_referencia;
        private string _nr_logradouro;
        private int _imprime_qsa;
        private List<bUrlEndereco> _listWsEndereco = new List<bUrlEndereco>();

        public int Imprime_qsa
        {
            get { return _imprime_qsa; }
            set { _imprime_qsa = value; }
        }

        public string Nr_logradouro
        {
            get { return _nr_logradouro; }
            set { _nr_logradouro = value; }
        }
        public string Ds_referencia
        {
            get { return _ds_referencia; }
            set { _ds_referencia = value; }
        }
        public string Ds_pais
        {
            get { return _ds_pais; }
            set { _ds_pais = value; }
        }
        
        #endregion

        #region Properties
        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public int TipoDocumentoGerado
        {
            get { return _tipoDocumentoGerado; }
            set { _tipoDocumentoGerado = value; }
        }

        public string cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string DescricaoCombo
        {
            get { return _descricaoCombo; }
            set { _descricaoCombo = value; }
        }
        

        public string sigla
        {
            get { return _sigla; }
            set { _sigla = value; }
        }
        public string uf
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public int codigo_municipio
        {
            get { return _codigo_municipio; }
            set { _codigo_municipio = value; }
        }
        public string ds_municipio
        {
            get { return _ds_municipio; }
            set{_ds_municipio = value;}
        }
        public string ds_bairro
        {
            get { return _ds_bairro; }
            set { _ds_bairro = value; }
        }
        public int co_tipo_logradouro
        {
            get { return _co_tipo_logradouro; }
            set { _co_tipo_logradouro = value; }
        }


        public string ds_tipo_logradouro
        {
            get { return _ds_tipo_logradouro; }
            set { _ds_tipo_logradouro = value; }
        }
        public string ds_logradouro
        {
            get { return _ds_logradouro; }
            set { _ds_logradouro = value; }
        }
        public string numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public string cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public string CodigoNire
        {
            get { return _codNire; }
            set { _codNire = value; }
        }
        public int ImprimeCapa
        {
            get { return _imprimeCapa; }
            set { _imprimeCapa = value; }
        }

        public int GeraProtocolo
        {
            get { return _geraProtocolo; }
            set { _geraProtocolo = value; }
        }

        public int ImprimeObjSocial
        {
            get { return _imprimeObjSocial; }
            set { _imprimeObjSocial = value; }
        }
        public int GravaTabelaPreparo
        {
            get { return _gravaTabelaPreparo; }
            set { _gravaTabelaPreparo = value; }
        }
        public int T004_IN_CARREGA_DBE
        {
            get { return _T004_IN_CARREGA_DBE; }
            set { _T004_IN_CARREGA_DBE = value; }
        }

        public string WsViabilidade
        {
            get { return _wsViabilidade; }
            set { _wsViabilidade = value; }
        }

        public string URLConsultaEmpresa
        {
            get { return _urlConsultaEmpresa; }
            set { _urlConsultaEmpresa = value; }
        }
        
        public string CodigoDBE
        {
            get { return _codigoDBE; }
            set { _codigoDBE = value; }
        }
        public string WsRegin
        {
            get { return _WsRegin; }
            set { _WsRegin = value; }
        }
        #endregion

        #region Implementação
        /// <summary>
        /// PREENCHE A CLASSE ORGAO DE REGISTRO
        /// </summary>
        public void Populate()
        {
            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                org.t004_nr_cnpj_org_reg = _cnpj;
                org.t004_uf = _uf;
                org.t004_in_org_reg = _tipo;
                
                DataTable dtOrgaoReg = org.BuscarOrgaoRegistro();
                if (dtOrgaoReg.Rows.Count > 0)
                {
                    _cnpj               = dtOrgaoReg.Rows[0]["cnpj"].ToString();
                    _descricao          = dtOrgaoReg.Rows[0]["descricao"].ToString();
                    _sigla              = dtOrgaoReg.Rows[0]["sigla"].ToString();
                    _uf                 = dtOrgaoReg.Rows[0]["uf"].ToString();
                    
                    if(dtOrgaoReg.Rows[0]["cod_municipio"].ToString() != "")
                        _codigo_municipio = Convert.ToInt32(dtOrgaoReg.Rows[0]["cod_municipio"].ToString());

                    _ds_municipio       = dtOrgaoReg.Rows[0]["ds_municipio"].ToString();
                    _ds_bairro          = dtOrgaoReg.Rows[0]["ds_bairro"].ToString();
                    _ds_tipo_logradouro = dtOrgaoReg.Rows[0]["tipo_logradouro"].ToString();
                    _ds_logradouro      = dtOrgaoReg.Rows[0]["ds_logradouro"].ToString();
                    _numero             = dtOrgaoReg.Rows[0]["numero"].ToString();
                    _ds_referencia      = dtOrgaoReg.Rows[0]["ds_referencia"].ToString();
                    _complemento        = dtOrgaoReg.Rows[0]["complemento"].ToString();
                    _cep                = dtOrgaoReg.Rows[0]["cep"].ToString();
                    _codNire            = dtOrgaoReg.Rows[0]["T004_COD_NIRE"].ToString();
                    _uf                 = dtOrgaoReg.Rows[0]["T004_UF"].ToString();
                    _wsViabilidade      = dtOrgaoReg.Rows[0]["T004_WS_VIABILIDADE"].ToString();
                    _WsRegin            = dtOrgaoReg.Rows[0]["T004_WS_REGIN"].ToString();
                    //Mudança para tabela de parâmetros
                    if (dtOrgaoReg.Rows[0]["T004_IN_GERA_PROTOCOLO"].ToString() != "")
                        _geraProtocolo = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_GERA_PROTOCOLO"].ToString());

                    if (dtOrgaoReg.Rows[0]["T004_IN_GERA_CAPA"].ToString() != "")
                        _imprimeCapa        = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_GERA_CAPA"].ToString());
                    
                    if (dtOrgaoReg.Rows[0]["T004_IN_IMP_OBJ"].ToString() != "")
                        _imprimeObjSocial   = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_IMP_OBJ"].ToString());

                    _gravaTabelaPreparo     = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_GRAVA_PREPARO"].ToString());
                    _tipoDocumentoGerado    = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_TIP_DOC"].ToString());
                    _T004_IN_CARREGA_DBE    = String.IsNullOrEmpty(dtOrgaoReg.Rows[0]["T004_IN_CARREGA_DBE"].ToString()) == true? 1: Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_CARREGA_DBE"].ToString());


                    _imprime_qsa = _T004_IN_CARREGA_DBE = String.IsNullOrEmpty(dtOrgaoReg.Rows[0]["T004_IN_IMP_QSA"].ToString()) == true ? 1 : Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_IMP_QSA"].ToString());
                    //_T004_IN_CARREGA_DBE    = Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_CARREGA_DBE"].ToString());
                    _urlConsultaEmpresa     = dtOrgaoReg.Rows[0]["T004_URL_CONSULTA_SIARCO"].ToString();
                    _tipo                   = dtOrgaoReg.Rows[0]["T004_IN_ORG_REG"].ToString();

                    _wsViabilidade          = dtOrgaoReg.Rows[0]["T004_WS_VIABILIDADE"].ToString();

                    bParametro p = new bParametro(_cnpj);
                    if(p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR)!="")
                    {
                        _geraProtocolo = int.Parse(p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR));
                    }

                    _codigoDBE = dtOrgaoReg.Rows[0]["T004_DBE_ORG"].ToString();
                }
                

            }
            GetListWSEndereco();
        }
        /// <summary>
        /// RECUPERA A URL DA CONSULTA DA VIABILIDADE
        /// </summary>
        /// <param name="pUF"></param>
        /// <returns></returns>
        public string getWsViabilidade(string pUF)
        {
            string ret = "";
             using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryWsViabilidade(pUF);
                if (dtOrgaoReg.Rows.Count > 0)
                {
                    ret = dtOrgaoReg.Rows[0]["T004_WS_VIABILIDADE"].ToString(); ;
                }
             }
             return ret;
        }

        private void GetListWSEndereco()
        {
            using (dT004_Orgao_Registro dal = new dT004_Orgao_Registro())
            {
                DataTable dt = dal.GetListaEndereco();
                foreach (DataRow row in dt.Rows)
                {
                    bUrlEndereco e = new bUrlEndereco();
                    e.Uf = row["uf"].ToString();
                    e.Url = row["url"].ToString();
                    _listWsEndereco.Add(e);
                }

            }
        }

        public string GetWsEndereco(string uf)
        {
            foreach (bUrlEndereco e in _listWsEndereco)
            {
                if (e.Uf == uf)
                    return e.Url;
            }

            return "";
        }

        /// <summary>
        /// RECUPERA A URL DA CONSULTA DOS DADOS DA EMPRESA
        /// </summary>
        /// <param name="pUF"></param>
        /// <returns></returns>
        public static string getUrlConsultaEmpresa(string pUf)
        {
            string ret = "";
            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryUrlConsultaEmpresa(pUf);
                if (dtOrgaoReg.Rows.Count > 0)
                {
                    ret = dtOrgaoReg.Rows[0]["T004_URL_CONSULTA_SIARCO"].ToString(); 
                }
            }
            return ret;
        }


        public string getUrlConsultaCnpj(string cnpj)
        {
            string dcnpj = "";

          
            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryDados(cnpj);
                if (dtOrgaoReg.Rows.Count > 0)
                {
                   dcnpj = dtOrgaoReg.Rows[0]["T004_NR_CNPJ_ORG_REG"].ToString();
                   
                }
            }
            return dcnpj;
            /*if ((dcnpj != null)|| (dcnpj==cnpj))
            {
                return true;
            }
            else {
                return false;
            }*/
        }

        public string getMunicipios(string Cidades)
        {
            string cod_cidades = "";


            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryMunicipios(Cidades);
                if (dtOrgaoReg.Rows.Count > 0)
                {
                    cod_cidades = dtOrgaoReg.Rows[0]["codigo_municipio"].ToString();

                }
            }
            return cod_cidades;
            /*if ((dcnpj != null)|| (dcnpj==cnpj))
            {
                return true;
            }
            else {
                return false;
            }*/
        }




        public string getEnderecoCep(string cep)
        {
            string enderecoCep = "";


            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryCepEndereco(cep); ;
                if (dtOrgaoReg.Rows.Count > 0)
                {
                    enderecoCep = dtOrgaoReg.Rows[0]["TLG_NOME"].ToString();

                }
            }
            return enderecoCep ;
            /*if ((dcnpj != null)|| (dcnpj==cnpj))
            {
                return true;
            }
            else {
                return false;
            }*/
        }


        public int getTipLog(string TipoLog)
        {
            int cod_municipio = 0;


            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryTipoLogradouro(TipoLog);
                if (dtOrgaoReg.Rows.Count > 0)
                {
                     cod_municipio = Convert.ToInt32(dtOrgaoReg.Rows[0]["TTI_CLAV"].ToString());

                }
            }
            return cod_municipio;
            /*if ((dcnpj != null)|| (dcnpj==cnpj))
            {
                return true;
            }
            else {
                return false;
            }*/
        }

        public string getTipLogNome(int TipLogNum)
        {
            string Nom_Via = "";


            //using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            //{
            //    DataTable dtOrgaoReg = org.QueryTipoNumLogradouro(TipLogNum);
            //    if (dtOrgaoReg.Rows.Count > 0)
            //    {
            //        Nom_Via = dtOrgaoReg.Rows[0]["TTI_NOME"].ToString();

            //    }
            //}
            return Nom_Via;
            /*if ((dcnpj != null)|| (dcnpj==cnpj))
            {
                return true;
            }
            else {
                return false;
            }*/
        }


        public List<bOrgaoRegistro> GetListWsRegin(string pUF)
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();
           
            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryWsViabilidade(pUF);
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                    cOrgao.WsRegin = dtOrgaoReg.Rows[0]["T004_WS_REGIN"].ToString();
                    list.Add(cOrgao);
                }

               
            }
            return list;
        }

        public List<bOrgaoRegistro> GetCepEndereco(string Cep)
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryCepEndereco(Cep);
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                    cOrgao.ds_logradouro = dtOrgaoReg.Rows[0]["TLG_NOME"].ToString();
                    cOrgao.uf = dtOrgaoReg.Rows[0]["TLG_TBA_TMU_TUF_UF"].ToString();
                    cOrgao.codigo_municipio =  Convert.ToInt32(dtOrgaoReg.Rows[0]["TLG_TBA_TMU_COD_MUN"].ToString());
                    list.Add(cOrgao);
                }


            }
            return list;
        }



        public static List<bOrgaoRegistro> GetUF()
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryUF();
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                   
                        cOrgao._uf = row["UF"].ToString();


                   
                    list.Add(cOrgao);
                }


            }
            return list;
        }




        public static List<bOrgaoRegistro> GetCidades(string Cidade)
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryMunicipios(Cidade);
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                    if (row["codigo_municipio"].ToString() != "")
                        cOrgao.codigo_municipio = Convert.ToInt32(row["codigo_municipio"].ToString());


                    cOrgao._ds_municipio = row["ds_municipio"].ToString();
                    list.Add(cOrgao);
                }


            }
            return list;
        }



        public static List<bOrgaoRegistro> GetTipoLogradouro()
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryTipoLogradouro();
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                    if (row["TTI_CLAV"].ToString() != "")
                        cOrgao._co_tipo_logradouro = Convert.ToInt32(row["TTI_CLAV"].ToString());
                    cOrgao._ds_tipo_logradouro = row["TTI_NOME"].ToString();

                    
                    list.Add(cOrgao);
                }


            }
            return list;
        }

/*        public static List<bOrgaoRegistro> GetTipoParametros()
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                DataTable dtOrgaoReg = org.QueryParametro();
                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();
                    if (row["T004_IN_GERA_PROTOCOLO"].ToString() != "")
                        //cOrgao._imprimeCapa = Convert.ToInt32(row["T004_IN_GERA_CAPA"].ToString());
                    //cOrgao._imprimeObjSocial = Convert.ToInt32(row["T004_IN_IMP_OBJ"].ToString());
                    cOrgao._imprimeCapa=Convert.ToInt32(dtOrgaoReg.Rows[0]["T004_IN_GERA_CAPA"].ToString());
                    cOrgao._imprimeObjSocial =Convert.ToInt32( dtOrgaoReg.Rows[0]["T004_IN_IMP_OBJ"].ToString());
                    list.Add(cOrgao);
                }


            }
            return list;
        }
        */

        public static DataTable QueryOrgao(string cnpj)
        {
            using (dT004_Orgao_Registro dal = new dT004_Orgao_Registro())
            {
                return dal.QueryDados(cnpj);
                
            }
        }

        






        
        public static List<bOrgaoRegistro> getListaOrgaoRegistro()
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                org.t004_in_org_reg = "2";

                DataTable dtOrgaoReg = org.BuscarOrgaoRegistro();

                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();

                    cOrgao.cnpj = row["cnpj"].ToString();
                    cOrgao.descricao = row["descricao"].ToString();
                    cOrgao.sigla = row["sigla"].ToString();
                    cOrgao.uf = row["uf"].ToString();

                    if (row["cod_municipio"].ToString() != "")
                        cOrgao.codigo_municipio = Convert.ToInt32(row["cod_municipio"].ToString());

                    cOrgao.ds_municipio= row["ds_municipio"].ToString();
                    cOrgao.ds_bairro = row["ds_bairro"].ToString();
                    //cOrgao.ds_tipo_logradouro = row["tipo_logradouro"].ToString();
                    cOrgao._co_tipo_logradouro = Convert.ToInt32(row["tipo_logradouro"].ToString());
                    cOrgao.ds_logradouro = row["ds_logradouro"].ToString();
                    cOrgao.numero = row["numero"].ToString();
                    cOrgao.complemento = row["complemento"].ToString();
                    cOrgao.cep = row["cep"].ToString();
                    cOrgao.CodigoNire = row["T004_COD_NIRE"].ToString();
                    cOrgao.uf = row["T004_UF"].ToString();
                    cOrgao.WsViabilidade = row["T004_WS_VIABILIDADE"].ToString();

                    cOrgao.DescricaoCombo = cOrgao.ds_municipio + " - " + cOrgao.descricao;

                    //Mudança para tabela de parâmetros
                    if (row["T004_IN_GERA_PROTOCOLO"].ToString() != "")
                        cOrgao.GeraProtocolo = Convert.ToInt32(row["T004_IN_GERA_PROTOCOLO"].ToString());

                    if (row["T004_IN_GERA_CAPA"].ToString() != "")
                        cOrgao.ImprimeCapa = Convert.ToInt32(row["T004_IN_GERA_CAPA"].ToString());

                    if (row["T004_IN_IMP_OBJ"].ToString() != "")
                        cOrgao.ImprimeObjSocial = Convert.ToInt32(row["T004_IN_IMP_OBJ"].ToString());

                    cOrgao.GravaTabelaPreparo = Convert.ToInt32(row["T004_IN_GRAVA_PREPARO"].ToString());
                    cOrgao.TipoDocumentoGerado = Convert.ToInt32(row["T004_TIP_DOC"].ToString());
                    cOrgao.T004_IN_CARREGA_DBE = Convert.ToInt32(row["T004_IN_CARREGA_DBE"].ToString());
                    cOrgao.URLConsultaEmpresa = row["T004_URL_CONSULTA_SIARCO"].ToString();
                    cOrgao.Tipo = row["T004_IN_ORG_REG"].ToString();
                    cOrgao.WsViabilidade = dtOrgaoReg.Rows[0]["T004_WS_VIABILIDADE"].ToString();
                    cOrgao.WsRegin = dtOrgaoReg.Rows[0]["T004_WS_REGIN"].ToString();

                    bParametro p = new bParametro(cOrgao.cnpj);
                    if (p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR) != "")
                    {
                        cOrgao.GeraProtocolo = int.Parse(p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR));
                    }

                    cOrgao.CodigoDBE = row["T004_DBE_ORG"].ToString();

                    list.Add(cOrgao);
                }

            }

            return list;
        }

       
        public static List<bOrgaoRegistro> getListaPrefeituras()
        {
            List<bOrgaoRegistro> list = new List<bOrgaoRegistro>();

            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                org.t004_in_org_reg = "3";

                DataTable dtOrgaoReg = org.BuscarOrgaoRegistro();

                foreach (DataRow row in dtOrgaoReg.Rows)
                {
                    bOrgaoRegistro cOrgao = new bOrgaoRegistro();

                    cOrgao.cnpj = row["cnpj"].ToString();
                    cOrgao.descricao = row["descricao"].ToString();
                    cOrgao.sigla = row["sigla"].ToString();
                    cOrgao.uf = row["uf"].ToString();

                    if (row["cod_municipio"].ToString() != "")
                        cOrgao.codigo_municipio = Convert.ToInt32(row["cod_municipio"].ToString());

                    cOrgao.ds_municipio = row["ds_municipio"].ToString();
                    cOrgao.ds_bairro = row["ds_bairro"].ToString();
                    cOrgao.ds_tipo_logradouro = row["tipo_logradouro"].ToString();
                    cOrgao.ds_logradouro = row["ds_logradouro"].ToString();
                    cOrgao.numero = row["numero"].ToString();
                    cOrgao.complemento = row["complemento"].ToString();
                    cOrgao.cep = row["cep"].ToString();
                    cOrgao.CodigoNire = row["T004_COD_NIRE"].ToString();
                    cOrgao.uf = row["T004_UF"].ToString();
                    cOrgao.WsViabilidade = row["T004_WS_VIABILIDADE"].ToString();


                    //Mudança para tabela de parâmetros
                    if (row["T004_IN_GERA_PROTOCOLO"].ToString() != "")
                        cOrgao.GeraProtocolo = Convert.ToInt32(row["T004_IN_GERA_PROTOCOLO"].ToString());

                    if (row["T004_IN_GERA_CAPA"].ToString() != "")
                        cOrgao.ImprimeCapa = Convert.ToInt32(row["T004_IN_GERA_CAPA"].ToString());

                    if (row["T004_IN_IMP_OBJ"].ToString() != "")
                        cOrgao.ImprimeObjSocial = Convert.ToInt32(row["T004_IN_IMP_OBJ"].ToString());

                    cOrgao.GravaTabelaPreparo = Convert.ToInt32(row["T004_IN_GRAVA_PREPARO"].ToString());
                    cOrgao.TipoDocumentoGerado = Convert.ToInt32(row["T004_TIP_DOC"].ToString());
                    cOrgao.T004_IN_CARREGA_DBE = Convert.ToInt32(row["T004_IN_CARREGA_DBE"].ToString());
                    cOrgao.URLConsultaEmpresa = row["T004_URL_CONSULTA_SIARCO"].ToString();
                    cOrgao.Tipo = row["T004_IN_ORG_REG"].ToString();
                    cOrgao.WsViabilidade = dtOrgaoReg.Rows[0]["T004_WS_VIABILIDADE"].ToString();
                    cOrgao.WsRegin = dtOrgaoReg.Rows[0]["T004_WS_REGIN"].ToString();

                    bParametro p = new bParametro(cOrgao.cnpj);
                    if (p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR) != "")
                    {
                        cOrgao.GeraProtocolo = int.Parse(p.getValor(bParametro.Valores.GERA_PROTOCOLO_OR));
                    }

                    cOrgao.CodigoDBE = row["T004_DBE_ORG"].ToString();

                    list.Add(cOrgao);
                }

            }

            return list;
        }

        #endregion

        #region CrudRegistro
        public  void InserirRegistro()
        {

            try
            {
                dT004_Orgao_Registro cadInserirOrgao = new dT004_Orgao_Registro();
                //bOrgaoRegistro cadInserir = new bOrgaoRegistro();
             

                cadInserirOrgao.a004_co_pais =Convert.ToDecimal(_ds_pais.ToString());
                cadInserirOrgao.a005_co_municipio = Convert.ToDecimal(_codigo_municipio.ToString());
                cadInserirOrgao.a015_co_tipo_logradouro = Convert.ToDecimal(_co_tipo_logradouro.ToString());
                cadInserirOrgao.t004_ds_bairro = _ds_bairro;
                cadInserirOrgao.t004_ds_complemento = _complemento;
                cadInserirOrgao.t004_nr_cep = _cep;
                cadInserirOrgao.t004_ds_logradouro = _ds_logradouro;
                cadInserirOrgao.t004_ds_org_reg = _descricao;
                cadInserirOrgao.t004_ds_referencia = _ds_referencia;
                cadInserirOrgao.t004_nr_cnpj_org_reg = _cnpj;
                cadInserirOrgao.t004_uf = _uf;
                cadInserirOrgao.T004_ws_regin = _WsRegin;
                cadInserirOrgao.t004_nr_logradouro = _numero;
                cadInserirOrgao.T004_DS_SIGLA_ORG_REG = _sigla;
                cadInserirOrgao.T004_gera_protocolo = _geraProtocolo;
                cadInserirOrgao.T004_gera_capa = _imprimeCapa;
                cadInserirOrgao.T004_imp_obj = _imprimeObjSocial;
                cadInserirOrgao.T004_grava_preparo = _gravaTabelaPreparo;
                cadInserirOrgao.T004_tip_doc = _tipoDocumentoGerado;
                cadInserirOrgao.T004_in_imp_qsa = _imprime_qsa;
                cadInserirOrgao.T004_ws_viabilidade = _wsViabilidade;
                
                cadInserirOrgao.T004_dbe_org = _codigoDBE;
                

                cadInserirOrgao.Insert();
            }catch(Exception ex){

                Console.WriteLine(ex.ToString());
            }
         
        }

        public void DeleteRegistro()
        {

            try
            {
                dT004_Orgao_Registro cadDeletarOrgao = new dT004_Orgao_Registro();
                //bOrgaoRegistro cadInserir = new bOrgaoRegistro();



                cadDeletarOrgao.t004_nr_cnpj_org_reg = _cnpj;
               


                cadDeletarOrgao.Delete();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }

        public void AlterarRegistro()
        {

            try
            {
                dT004_Orgao_Registro cadAlterarOrgao = new dT004_Orgao_Registro();
                //bOrgaoRegistro cadInserir = new bOrgaoRegistro();

                
                cadAlterarOrgao.a004_co_pais = Convert.ToDecimal(_ds_pais.ToString());
                cadAlterarOrgao.a005_co_municipio = Convert.ToDecimal(_codigo_municipio.ToString()); 
                cadAlterarOrgao.a015_co_tipo_logradouro = Convert.ToDecimal(_co_tipo_logradouro);
                cadAlterarOrgao.t004_ds_bairro = _ds_bairro;
                cadAlterarOrgao.t004_ds_complemento = _complemento;
                cadAlterarOrgao.t004_nr_cep = _cep;
                cadAlterarOrgao.t004_ds_logradouro = _ds_logradouro;
                cadAlterarOrgao.t004_ds_org_reg = _descricao;
               
                cadAlterarOrgao.t004_ds_referencia = _ds_referencia;
                cadAlterarOrgao.t004_nr_cnpj_org_reg = _cnpj;
                cadAlterarOrgao.t004_uf = _uf;
                cadAlterarOrgao.T004_ws_regin = _WsRegin;
                cadAlterarOrgao.t004_nr_logradouro = _numero;
                cadAlterarOrgao.T004_DS_SIGLA_ORG_REG = _sigla;
                cadAlterarOrgao.T004_in_carrega_dbe = _T004_IN_CARREGA_DBE;
                cadAlterarOrgao.T004_gera_protocolo = _geraProtocolo;
                cadAlterarOrgao.T004_gera_capa = _imprimeCapa;
                cadAlterarOrgao.T004_imp_obj = _imprimeObjSocial;
                cadAlterarOrgao.T004_grava_preparo = _gravaTabelaPreparo;
                cadAlterarOrgao.T004_tip_doc = _tipoDocumentoGerado;
                cadAlterarOrgao.T004_in_imp_qsa = _imprime_qsa;
                cadAlterarOrgao.T004_ws_viabilidade =  _wsViabilidade;
                cadAlterarOrgao.T004_dbe_org = _codigoDBE;

                cadAlterarOrgao.Update_registro();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }


        #endregion

#region Constructors
        public bOrgaoRegistro()
        {
           
            //InitClass();
        }
        public bOrgaoRegistro(string pCNPJ)
            : this()
        {
            _cnpj = pCNPJ;
            Populate();
        }

     
        public bOrgaoRegistro(string pCNPJ, string pUF)
            : this()
        {
            _uf = pUF;
            _cnpj = pCNPJ;
            Populate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCNPJ"></param>
        /// <param name="pUF"></param>
        /// <param name="pTipo"></param>
        public bOrgaoRegistro(string pCNPJ, string pUF, string pTipo)
            : this()
        {
            _uf = pUF;
            _tipo = pTipo;
            Populate();
        }






        #endregion

    }
}
