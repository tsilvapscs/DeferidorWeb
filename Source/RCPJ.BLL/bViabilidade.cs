using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bViabilidade
    {
        #region Variaveis
        private string _protocolo = "";
        private string _uf;
        private string _razaoSocial = "";
        private DateTime _dataSolicitacao;
        private string _municipioCodigo = "";
        private string _iptu = "";
        private int _tipoRegistro;
        private string _nire = "";
        private string _cnpj = "";
        private string _nireMatriz = "";
        private string _cnpjMatriz = "";
        private string _inscricaoMunicipal = "";
        private string _inscricaoEstadual = "";
        private string _protocoloConsultaPrevia = "";
        private string _protocoloViabilidadeVinculada = "";
        private string _coordenadaGeografica;
        private string _coordenadaAjustada;
        private string _status = "";
        private string _objetoSocial = "";
        private bEndereco _endereco = new bEndereco();
        private bRequerente _requerente = new bRequerente();
        private List<bCNAE> _cnaes = new List<bCNAE>();
        private List<bSocios> _socios = new List<bSocios>();
        private List<bProtocoloEvento> _eventos = new List<bProtocoloEvento>();
        private string _cnpjOrgaoRegistro = "";

      
        #endregion

        #region Properties
        public string Protocolo
        {
            get { return _protocolo; }
            set { _protocolo = value; }
        }

        public string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }

        public string ObjetoSocial
        {
            get { return _objetoSocial; }
            set { _objetoSocial = value; }
        }

        public DateTime DataSolicitacao
        {
            get { return _dataSolicitacao; }
            set { _dataSolicitacao = value; }
        }

        public string MunicipioCodigo
        {
            get { return _municipioCodigo; }
            set { _municipioCodigo = value; }
        }

        public string Iptu
        {
            get { return _iptu; }
            set { _iptu = value; }
        }
        
        public int TipoRegistro
        {
            get { return _tipoRegistro; }
            set { _tipoRegistro = value; }
        }

        public string Nire
        {
            get { return _nire; }
            set { _nire = value; }
        }

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        public string NireMatriz
        {
            get { return _nireMatriz; }
            set { _nireMatriz = value; }
        }

        public string CnpjMatriz
        {
            get { return _cnpjMatriz; }
            set { _cnpjMatriz = value; }
        }

        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set { _inscricaoMunicipal = value; }
        }

        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = value; }
        }
        
        public string ProtocoloConsultaPrevia
        {
            get { return _protocoloConsultaPrevia; }
            set { _protocoloConsultaPrevia = value; }
        }

        public string ProtocoloViabilidadeVinculada
        {
            get { return _protocoloViabilidadeVinculada; }
            set { _protocoloViabilidadeVinculada = value; }
        }

        public string CoordenadaGeografica
        {
            get { return _coordenadaGeografica; }
            set { _coordenadaGeografica = value; }
        }

        public string CoordenadaAjustada
        {
            get { return _coordenadaAjustada; }
            set { _coordenadaAjustada = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public bEndereco Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public bRequerente Requerente
        {
            get { return _requerente; }
            set { _requerente = value; }
        }

        public List<bCNAE> Cnaes
        {
            get { return _cnaes; }
            set { _cnaes = value; }
        }

        public List<bSocios> Socios
        {
            get { return _socios; }
            set { _socios = value; }
        }
        
        public List<bProtocoloEvento> Eventos
        {
            get { return _eventos; }
            set { _eventos = value; }
        }
        
        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        #endregion

        #region Constructors
        public bViabilidade()
        {
           
            //InitClass();
        }
        public bViabilidade(DataSet dsViab)
            : this()
        {
            Populate(dsViab);
        }
        #endregion

        private void Populate(DataSet result)
        {

            DataTable ViaProtocolo = result.Tables["VIA_PROTOCOLO_VIAB"];
            if (ViaProtocolo != null)
            {

                #region Dados da viabilidade
                _protocolo = ViaProtocolo.Rows[0]["VPV_COD_PROTOCOLO"].ToString();
                _uf = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                _dataSolicitacao = DateTime.Parse(ViaProtocolo.Rows[0]["VPV_FEC_SOLICITUD"].ToString());
                _municipioCodigo = ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"].ToString();
                _iptu = ViaProtocolo.Rows[0]["VPV_IPTU"].ToString();
                _tipoRegistro = Int32.Parse(ViaProtocolo.Rows[0]["VPV_TIP_REGISTRO"].ToString());
                _nire = ViaProtocolo.Rows[0]["VPV_NIRE"].ToString();
                _cnpj = ViaProtocolo.Rows[0]["VPV_CNPJ"].ToString();
                _nireMatriz = ViaProtocolo.Rows[0]["VPV_NIRE_MATRIZ"].ToString();
                _cnpjMatriz = ViaProtocolo.Rows[0]["VPV_CNPJ_MATRIZ"].ToString();
                _inscricaoMunicipal = ViaProtocolo.Rows[0]["VPV_INSCRMUNICIPAL"].ToString();
                _inscricaoEstadual = ViaProtocolo.Rows[0]["VPV_INSCRESTADUAL"].ToString();
                _protocoloConsultaPrevia = ViaProtocolo.Rows[0]["VPV_VIABILIDADE"].ToString();
                _protocoloViabilidadeVinculada = ViaProtocolo.Rows[0]["VPV_VIABILIDADE_VINCULADA"].ToString();
                _coordenadaGeografica = ViaProtocolo.Rows[0]["VPV_COD_GEOGRAFICO"].ToString();
                _coordenadaAjustada = ViaProtocolo.Rows[0]["VPV_COD_GEOGRAFICA_AJUSTADA"].ToString();
                _status = ViaProtocolo.Rows[0]["STATUS_VIABILIDADE"].ToString();
                _cnpjOrgaoRegistro = ViaProtocolo.Rows[0]["PRO_CNPJ_ORG_REG"].ToString();
                #endregion

                #region Requerente
                _requerente.Nome = ViaProtocolo.Rows[0]["VPV_NOM_SOLIC"].ToString();
                _requerente.Cpf = ViaProtocolo.Rows[0]["VPV_VSV_CPF_CNPJ_SOLIC"].ToString().Trim();
                _requerente.Email = ViaProtocolo.Rows[0]["VPV_EMAIL_SOLIC"].ToString();
                #endregion

                #region Eventos
                DataTable ViaProtEvento = result.Tables["VIA_PROT_EVENTO"];
                _eventos.Clear();

                if (ViaProtEvento != null)
                {
                    foreach (DataRow r in ViaProtEvento.Rows)
                    {
                        bProtocoloEvento ev = new bProtocoloEvento();
                        ev.CodigoEvento = Decimal.Parse(r["PEV_COD_EVENTO"].ToString());
                        ev.DescricaoEvento = dHelperQuery.BuscarDescricaoEvento(r["PEV_COD_EVENTO"].ToString());
                        _eventos.Add(ev);
                    }

                }
                #endregion

                #region Nome empresarial
                DataTable ViaRazao = result.Tables["VIA_PROT_RAZAO_SOCIAL"];
                if (ViaRazao != null)
                {
                    _razaoSocial = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();
                }

                #endregion

                #region Endereço da Empresa
                _endereco.CEP= ViaProtocolo.Rows[0]["VPV_CEP"].ToString();
                _endereco.UF = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                _endereco.CodMunicipio = ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"].ToString();
                _endereco.NomeMunicipio = dHelperQuery.BuscarDescricaoMunicipio(_endereco.CodMunicipio);
                _endereco.Bairro= ViaProtocolo.Rows[0]["VPV_BAIRRO"].ToString();
                _endereco.TipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                _endereco.Logradouro = ViaProtocolo.Rows[0]["VPV_LOGRADORO"].ToString();
                _endereco.Numero = ViaProtocolo.Rows[0]["VPV_NUM_LOGRADOURO"].ToString();
                _endereco.Complemento = ViaProtocolo.Rows[0]["VPV_COMP_LOGRADOURO"].ToString();
                #endregion

                #region Objeto Social/CANE
                _objetoSocial = ViaProtocolo.Rows[0]["VPV_OBJETIVO"].ToString();

                #endregion

                #region CNAE
                if (_tipoRegistro != 12) //Autonomo
                {
                    DataTable ViaCnaeAux = result.Tables["VIA_PROT_CNAE"];
                    _cnaes.Clear();

                    foreach (DataRow r in ViaCnaeAux.Rows)
                    {
                        if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 1)
                        {
                            bCNAE c = new bCNAE();
                            c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                            c.Descricao = r["VPC_TAE_DESC"].ToString();
                            c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                            c.Versao = "V02";
                            c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;

                            _cnaes.Add(c);

                            break;
                        }
                    }
                    //Inserir na lista as CNAEs secundarias
                    foreach (DataRow r in ViaCnaeAux.Rows)
                    {
                        if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 2)
                        {
                            bCNAE c = new bCNAE();
                            c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                            c.Descricao = r["VPC_TAE_DESC"].ToString();
                            c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                            c.Versao = "V02";
                            c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                            _cnaes.Add(c);
                        }
                    }

                }
                else
                {
                    DataTable ViaCnaeAux = result.Tables["VIA_PROT_CBO"];
                    _cnaes.Clear();

                    foreach (DataRow r in ViaCnaeAux.Rows)
                    {
                        if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 1)
                        {
                            bCNAE c = new bCNAE();
                            c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                            c.Descricao = r["VPC_TAE_DESC"].ToString();
                            c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                            c.Versao = "V02";
                            c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;

                            _cnaes.Add(c);

                            break;
                        }
                    }
                    //Inserir na lista as CNAEs secundarias
                    foreach (DataRow r in ViaCnaeAux.Rows)
                    {
                        if (decimal.Parse(r["VPV_TIP_CNAE"].ToString()) == 2)
                        {
                            bCNAE c = new bCNAE();
                            c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                            c.Descricao = r["VPC_TAE_DESC"].ToString();
                            c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                            c.Versao = "V02";
                            c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                            _cnaes.Add(c);
                        }
                    }
                }
                #endregion

                #region Socios
                DataTable temp = result.Tables["VIA_PROT_SOCIOS"];
                if (temp != null)
                {
                    DataView dv = temp.DefaultView;
                    dv.Sort = "VPS_CPF_CNPJ_SOCIO asc";
                    DataTable socios = dv.ToTable();
                    _socios.Clear();
                    foreach (DataRow s in socios.Rows)
                    {
                        bSocios ns = new bSocios();
                        ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                        ns.Nome = s["VPS_NOME_SOCIO"].ToString();
                        ns.Nome_Mae = s["VPS_NOME_MAE"].ToString();

                        ns.Qualificacao = "22";

                        ns.Qualificacao_Descricao = "Sócio";
                        ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                        if (ns.CPFCNPJ.Trim().Length == 11)
                            ns.TipoPessoa = "F";
                        else
                            ns.TipoPessoa = "J";
                        ns.ObrigacoesSociais = "S";
                        ns.tipoacao = 3;
                        _socios.Add(ns);
                    }
                }
                #endregion
            }


         
        }
    }
}
