using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RCPJ.BLL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Ruc;
using RCPJ.DAL.Helper;
using System.Collections.Generic;
using System.Text;
using psc.Framework.Data;
using System.Data.OracleClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (WsRFB.ServiceReginRFB c = new WsRFB.ServiceReginRFB())
        {



            //WsRFB.Retorno retorno;
            //retorno = c.ServiceXMLDBE("00057525897204", "PA30965360");


            //ProcessaDadosDbe(retorno.oWs35Response);



        }
    }
    public void ProcessaDadosDbe(WsRFB.serviceResponse pReponse)
    {

        bRequerimento c = new bRequerimento();
        bool eventoConstituicao = false;


        #region Empresa
        WsRFB.redesim DadosDbe = pReponse.dadosRedesim;
        //pj.t73302_caixa_postal = Global.valNuloBranco(DadosDbe.fcpj.caixaPostal);
        if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
        {
            c.CapitalSocial = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
        }


        if (DadosDbe.fcpj.endereco != null)
        {
            c.SedeBairro = Global.valNuloBranco(DadosDbe.fcpj.endereco.bairro);
            c.SedeCEP = Global.valNuloBranco(DadosDbe.fcpj.endereco.cep);
            c.SedeMunicipio = Global.valNuloBranco(DadosDbe.fcpj.endereco.codMunicipio);
            if (c.SedeMunicipio != "")
            {
                c.SedeMunicipio += psc.Framework.General.CalculateVerificationDigit(c.SedeMunicipio, 11).ToString();
                c.SedeMunicipio = int.Parse(c.SedeMunicipio).ToString();
            }
            //pj.t73302_cod_pais = Global.valNuloBranco(DadosDbe.fcpj.endereco.codPais);
            c.SedeComplemento = Global.valNuloBranco(DadosDbe.fcpj.endereco.complementoLogradouro);
            //c.seded = Global.valNuloBranco(DadosDbe.fcpj.endereco.distrito);
            c.SedeLogradouro = Global.valNuloBranco(DadosDbe.fcpj.endereco.logradouro);
            c.SedeNumero = Global.valNuloBranco(DadosDbe.fcpj.endereco.numLogradouro);
            //c.Sede = Global.valNuloBranco(DadosDbe.fcpj.endereco.referencia);
            c.SedeTipoLogradouro = Global.valNuloBranco(DadosDbe.fcpj.endereco.codTipoLogradouro);
            //pj.t73302_cidade_exterior = Global.valNuloBranco(DadosDbe.fcpj.endereco.cidadeExterior);
            c.SedeUF = Global.valNuloBranco(DadosDbe.fcpj.endereco.uf);
            //pj.t73302_cod_tipo_unidade = Global.valNuloBranco(DadosDbe.atividadeEconomica.codTipoUnidade);
        }

        //c.Sedeca = Global.valNuloBranco(DadosDbe.fcpj.cepCaixaPostal);
        c.nrEmpresaCNPJ = Global.valNuloBranco(DadosDbe.cnpj);
        if (Global.valNuloBranco(DadosDbe.fcpj.nire) != "")
        {
            c.nrMatricula = Global.valNuloBranco(decimal.Parse(DadosDbe.fcpj.nire));
        }
        //pj.t73302_tip_org_registro = Global.valNuloBranco(DadosDbe.fcpj.codTipoOrgaoRegistro);
        c.Porte = Global.valNuloBranco(DadosDbe.fcpj.codPorteEmpresa);

        c.Nome_Fantasia = Global.valNuloBranco(DadosDbe.fcpj.nomeFantasia);
        c.NaturezaJuridicaCodigo = int.Parse(Global.valNuloBranco(DadosDbe.fcpj.codNaturezaJuridica));
        c.SedeNome = Global.valNuloBranco(DadosDbe.fcpj.nomeEmpresarial);


        if (DadosDbe.atividadeEconomica != null)
        {
            c.ObjetoSocial = Global.valNuloBranco(DadosDbe.atividadeEconomica.objetoSocial);
            //c.CNAEs = Global.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal);
        }

        if (DadosDbe.fcpj.contato != null)
        {
            c.SedeEmail = Global.valNuloBranco(DadosDbe.fcpj.contato.correioEletronico);
            c.SedeTelefone = Global.valNuloBranco(DadosDbe.fcpj.contato.telefone1);
            //pj.t73302_telefone_2 = Global.valNuloBranco(DadosDbe.fcpj.contato.telefone2);
            //c.sedef = Global.valNuloBranco(DadosDbe.fcpj.contato.dddFax);
            c.SedeDDD = Global.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone1);
            //pj.t73302_ddd_telefone_2 = Global.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone2);
            //pj.t73302_fax = Global.valNuloBranco(DadosDbe.fcpj.contato.fax);
        }

        if (DadosDbe.fcpj.codNaturezaJuridica == "2135")
        {
            bSocios cSocio = new bSocios();

            #region Preenche Socio caso seja naturea Empresaio
            if (DadosDbe.fcpj.cpfResponsavel != "")
            {
                WsRFB.endereco enderecoResponsavel = new WsRFB.endereco();
                WsRFB.contato contatoResponsavel = new WsRFB.contato();

                /*
                    Comença com o dado da emporesa mesmo, caso venha o endereço do responsavel pego de la
                 * comentado para nao levar o endereço da empresa para o socio, solicitado por xico 28/10/2014
                 * alegando que os estados falaram que a pessoa quasi sempre chega com o memso endereço, por isso
                 * sera forçado a digitar ele novamente se for o caso.
                 */
                //enderecoResponsavel = DadosDbe.fcpj.endereco;
                //contatoResponsavel = DadosDbe.fcpj.contato;

                if (DadosDbe.fcpj.endResponsavel != null)
                {
                    if (!Global.valNulo(DadosDbe.fcpj.endResponsavel.bairro) && !Global.valNulo(DadosDbe.fcpj.endResponsavel.cep))
                    {
                        enderecoResponsavel = DadosDbe.fcpj.endResponsavel;
                        contatoResponsavel = DadosDbe.fcpj.contatoResponsavel;
                    }
                }

                cSocio.EndBairro = enderecoResponsavel.bairro;
                if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
                {
                    //qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                    //qsa.t73303_capital_social_qsa = qsa.t73303_capital_social_empresa;

                }

                cSocio.PercentualCapital = 100;

                cSocio.Qualificacao = "50";
                cSocio.EndCEP = Global.valNulo(enderecoResponsavel.cep) ? "" : enderecoResponsavel.cep;
                cSocio.tipoacao = 1;
                //qsa.t73303_cod_evento = "001";
                if (!eventoConstituicao)
                {
                    cSocio.tipoacao = 3;
                }

                cSocio.EndMunicipio = Global.valNulo(enderecoResponsavel.codMunicipio) ? "" : enderecoResponsavel.codMunicipio;
                cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                //qsa.t73303_cod_munic_qsa = int.Parse(qsa.t73303_cod_munic_qsa).ToString();
                //qsa.t73303_cod_pais = Global.valNulo(enderecoResponsavel.codPais) ? "" : enderecoResponsavel.codPais;
                cSocio.EndComplemento = Global.valNulo(enderecoResponsavel.complementoLogradouro) ? "" : enderecoResponsavel.complementoLogradouro;
                cSocio.Email = Global.valNulo(contatoResponsavel.correioEletronico) ? "" : contatoResponsavel.correioEletronico;
                //qsa.t73303_ind_cpf_cnpj_qsa = "2"; //Sempre cpf
                int De = 11;
                int Ate = DadosDbe.fcpj.cpfResponsavel.Length - De;
                cSocio.CPFCNPJ = DadosDbe.fcpj.cpfResponsavel.Substring(Ate);

                //qsa.t73303_dat_evento = DateTime.Now;
                //qsa.t73303_dat_inicio_mandato = DateTime.Now;
                //qsa.t73303_ddd_fax_qsa = contatoResponsavel.dddFax;
                cSocio.DDD = contatoResponsavel.dddTelefone1;
                //qsa.t73303_distrito_qsa = enderecoResponsavel.distrito;
                //qsa.t73303_fax_qsa = contatoResponsavel.fax;
                cSocio.EndLogradouro = enderecoResponsavel.logradouro;
                //qsa.t73303_nacionalidade_qsa = Socio.nacionalidadeSocioPf;
                //qsa.t73303_nire_qsa = Socio.;
                cSocio.Nome = DadosDbe.fcpj.nomeResponsavel;
                cSocio.EndNumero = enderecoResponsavel.numLogradouro;

                cSocio.Qualificacao = "50";
                cSocio.Telefone = contatoResponsavel.telefone1;
                cSocio.EndTipoLogradouro = enderecoResponsavel.codTipoLogradouro;
                cSocio.EndUF = enderecoResponsavel.uf;

            }
            #endregion
        }

        #endregion

        #region Eventos
        if (DadosDbe.fcpj.codEvento != null)
        {
            bProtocoloEvento cEvento = new bProtocoloEvento();
            int i = 0;
            foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
            {
                if (pCodEvento == "")
                {
                    break;
                }
                if (pCodEvento.Trim() == "101" || pCodEvento.Trim() == "102")
                {
                    eventoConstituicao = true;
                }
                cEvento.CodigoEvento = decimal.Parse(pCodEvento);
                //ev.t73301_tip_evento = DadosDbe.fcpj.tipoEvento[i].ToString();
                i += i;
                c.ProtocoloEvento.Add(cEvento);
            }
        }
        #endregion

        #region Cnae

        if (DadosDbe.atividadeEconomica != null)
        {
            bCNAE cCnae = new bCNAE();

            if (DadosDbe.atividadeEconomica != null)
            {
                cCnae.CodigoCNAE = Global.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal);
                cCnae.TipoAtividade = 36;
                c.CNAEs.Add(cCnae);
            }

            foreach (string pCodEvento in DadosDbe.atividadeEconomica.codCnaeSecundaria)
            {
                if (pCodEvento.Length < 5)
                {
                    break;
                }

                cCnae.CodigoCNAE = pCodEvento;
                cCnae.TipoAtividade = 37;
                c.CNAEs.Add(cCnae);

            }
        }

        #endregion

        #region Socios
        if (DadosDbe.socios != null)
        {
            foreach (WsRFB.socio Socio in DadosDbe.socios)
            {
                if (Socio.cnpjCpfSocio != "")
                {
                    bSocios cSocio = new bSocios();
                    bSocios cRepre = new bSocios();
                    if (Socio.endSocio != null)
                    {
                        cSocio.EndBairro = Global.valNulo(Socio.endSocio.bairro) ? "" : Socio.endSocio.bairro;
                        cSocio.EndCEP = Global.valNulo(Socio.endSocio.cep) ? "" : Socio.endSocio.cep;
                        cSocio.EndMunicipio = Global.valNulo(Socio.endSocio.codMunicipio) ? "" : Socio.endSocio.codMunicipio;
                        cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                        cSocio.EndMunicipio = int.Parse(cSocio.EndMunicipio).ToString();

                        cSocio.EndPais = Global.valNulo(Socio.endSocio.codPais) ? "" : Socio.endSocio.codPais;
                        cSocio.EndComplemento = Global.valNulo(Socio.endSocio.complementoLogradouro) ? "" : Socio.endSocio.complementoLogradouro;
                        cSocio.EndLogradouro = Socio.endSocio.logradouro;
                        //qsa.t73303_distrito_qsa = Socio.endSocio.distrito;
                        cSocio.EndNumero = Socio.endSocio.numLogradouro;
                        cSocio.EndTipoLogradouro = Socio.endSocio.codTipoLogradouro;
                        cSocio.EndUF = Socio.endSocio.uf;

                    }

                    if (!Global.valNulo(Socio.cpfRepresentanteLegal) && Socio.cpfRepresentanteLegal != "")
                    {
                        cRepre.CPFCNPJ = Global.valNulo(Socio.cpfRepresentanteLegal) ? "" : Socio.cpfRepresentanteLegal;
                        cRepre.Qualificacao = Global.valNulo(Socio.codQualificacaoRepresentanteLegal) ? "" : Socio.codQualificacaoRepresentanteLegal;

                        if (Socio.endRepLegal != null)
                        {
                            cRepre.EndBairro = Global.valNulo(Socio.endRepLegal.bairro) ? "" : Socio.endRepLegal.bairro;
                            cRepre.EndCEP = Global.valNulo(Socio.endRepLegal.cep) ? "" : Socio.endRepLegal.cep;
                            cRepre.EndMunicipio = Global.valNulo(Socio.endRepLegal.codMunicipio) ? "" : Socio.endRepLegal.codMunicipio;
                            cRepre.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cRepre.EndMunicipio, 11).ToString();
                            cRepre.EndMunicipio = int.Parse(cRepre.EndMunicipio).ToString();
                            cRepre.EndComplemento = Global.valNulo(Socio.endRepLegal.complementoLogradouro) ? "" : Socio.endRepLegal.complementoLogradouro;
                            //qsa.t73303_distrito_rep_legal = Socio.endRepLegal.distrito;
                            cRepre.EndLogradouro = Socio.endRepLegal.logradouro;
                            cRepre.EndNumero = Socio.endRepLegal.numLogradouro;
                            cRepre.EndUF = Socio.endRepLegal.uf;
                            cRepre.EndTipoLogradouro = Socio.endRepLegal.codTipoLogradouro;
                        }

                        if (Socio.contatoRepLegal != null)
                        {
                            cRepre.Email = Global.valNulo(Socio.contatoRepLegal.correioEletronico) ? "" : Socio.contatoRepLegal.correioEletronico;
                            //cRepre. = Global.valNulo(Socio.contatoRepLegal.dddFax) ? "" : Socio.contatoRepLegal.dddFax;
                            cRepre.DDD = Global.valNulo(Socio.contatoRepLegal.dddTelefone1) ? "" : Socio.contatoRepLegal.dddTelefone1;
                            //cRepre. = Global.valNulo(Socio.contatoRepLegal.fax) ? "" : Socio.contatoRepLegal.fax;
                            cRepre.Telefone = Global.valNulo(Socio.contatoRepLegal.telefone1) ? "" : Socio.contatoRepLegal.telefone1;
                        }
                    }



                    if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
                    {
                        c.CapitalSocial = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                        //qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                    }

                    if (!Global.valNulo(Socio.capitalSocialSocio))
                    {
                        //qsa.t73303_perc_partic_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                        cSocio.CapitalIntegralizado = decimal.Parse(Socio.capitalSocialSocio) / 100;
                    }

                    /*
                        Isto aqui e porque antes o percentual vinha no campo capitalSocialSocio
                     * mas foi enviado uma ou iam colocar uma atualização nova para mudar o percentual para
                     * percentualCapitalSocialSocio, entao se esse campo vier considero este mesmo para o calculo
                     */
                    if (!Global.valNulo(Socio.percentualCapitalSocialSocio))
                    {
                        cSocio.PercentualCapital = decimal.Parse(Socio.percentualCapitalSocialSocio) / 100;
                        //qsa.t73303_capital_social_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                    }

                    //
                    //Alterado em 03/11/2014 para pegar o capital social do socio caso o campo Socio.capitalSocialSocio > 0
                    //
                    if ((!Global.valNulo(c.CapitalSocial) && cSocio.CapitalIntegralizado > 0)
                        && cSocio.PercentualCapital != 0)
                    {
                        cSocio.CapitalIntegralizado = (cSocio.CapitalIntegralizado * cSocio.PercentualCapital) / 100;
                        //qsa.t73303_perc_partic_qsa = (qsa.t73303_capital_social_empresa * qsa.t73303_perc_partic_qsa) / 100;
                    }
                    else
                    {
                        if (!Global.valNulo(Socio.capitalSocialSocio) && decimal.Parse(Socio.capitalSocialSocio) > 0)
                        {
                            cSocio.CapitalIntegralizado = decimal.Parse(Socio.capitalSocialSocio) / 100;
                        }
                    }


                    cSocio.Qualificacao = Global.valNulo(Socio.codQualificacaoSocio) ? "" : Socio.codQualificacaoSocio;
                    cSocio.EventoDBE = Global.valNulo(Socio.codEvento) ? "" : Socio.codEvento;



                    if (Socio.contatoSocio != null)
                    {
                        cSocio.Email = Global.valNulo(Socio.contatoSocio.correioEletronico) ? "" : Socio.contatoSocio.correioEletronico;
                        //qsa.t73303_ddd_fax_qsa = Socio.contatoSocio.dddFax;
                        cSocio.DDD = Socio.contatoSocio.dddTelefone1;
                        //qsa.t73303_fax_qsa = Socio.contatoSocio.fax;
                        cSocio.Telefone = Socio.contatoSocio.telefone1;

                    }


                    //qsa.t73303_ind_cpf_cnpj_qsa = Global.valNulo(Socio.indCnpjCpfSocio) ? "" : Socio.indCnpjCpfSocio;
                    int De = 14;
                    if (Socio.indCnpjCpfSocio == "2")
                    {
                        De = 11;
                    }
                    int Ate = (Socio.cnpjCpfSocio.Length - De);
                    cSocio.CPFCNPJ = Socio.cnpjCpfSocio.Substring(Ate);


                    //qsa.t73303_dat_emis_ident_rep_lega = Socio.representanteLegal.;
                    //qsa.t73303_dat_emissao_ident = Socio.d;
                    //qsa.t73303_dat_evento = dHelperQuery.convertStringDateYYYMMDD(Global.valNulo(Socio.dataEvento) ? "" : Socio.dataEvento);
                    //qsa.t73303_dat_inicio_mandato = dHelperQuery.convertStringDateYYYMMDD(Global.valNulo(Socio.dataInclusaoCorreta) ? "" : Socio.dataInclusaoCorreta);
                    //qsa.t73303_dat_termino_mandato;
                    //qsa.t73303_dt_nascimento_socio_pf = Socio.d;
                    //qsa.t73303_ident_passap_qsa = Socio.i;
                    //qsa.t73303_ident_rep_legal;

                    //qsa.t73303_matricula_rcpj = Socio.contatoSocio.ma
                    cSocio.NacionalidadeCodigo = Global.valNulo(Socio.nacionalidadeSocioPf) ? 0 : int.Parse(Socio.nacionalidadeSocioPf);
                    //qsa.t73303_nire_qsa = Socio.;
                    cSocio.Nome = Global.valNulo(Socio.socio1) ? "" : Socio.socio1;
                    cRepre.Nome = Global.valNulo(Socio.representanteLegal) ? "" : Socio.representanteLegal;
                    //qsa.t73303_or_emis_ident_rep_legal = Socio.;
                    //qsa.t73303_orgao_emissor_ident;
                    //qsa.t73303_orig_inf_lograd;
                    //qsa.t73303_origem_endereco_rep_leg;

                    //qsa.t73303_uf_or_emissor_rep_legal = Socio.;
                    //qsa.t73303_uf_orgao_emissor_ident;
                    //qsa.t73303_uso_firma_administrador = "";

                    if (cRepre != null)
                    {
                        cSocio.Representantes.Add(cRepre);
                    }

                    c.Socios.Add(cSocio);

                }
            }
        }
        #endregion
    }
    protected void btnGravaws11_Click(object sender, EventArgs e)
    {
        List<string> pEventos = new List<string>();
        pEventos.Add("101");

        WsRFB.ServiceReginRFB ws = new WsRFB.ServiceReginRFB();
        WsRFB.Retorno Retorno = new WsRFB.Retorno();
        WsRFB.redesim dados35 = new WsRFB.redesim();

        ws.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

        Retorno = ws.ServiceWs35Soarquivo("00057525897204", "PA30965361");

        if (Retorno.oWs35Response != null && Retorno.oWs35Response.dadosRedesim != null)
        {
            dados35 = Retorno.oWs35Response.dadosRedesim;
        }

        FormataXMLRequerimento(pEventos, "123456789", "07273558000190", "12345678901", "", "", "27079821000111", dados35, "PA3096536100057525897204");

        //GravaWsRFB11Ruc11(pEventos, "123456789", "07273558000190", "12345678901", "", "", "27079821000111", dados35, "PA3096536100057525897204");
    }

    public void FormataXMLRequerimento(List<string> pEventos, string pProtocoloJunta, string pCNPJ, string nire, string pNroViabilidade, string NroRequerimento, string pCNPJOrgaoRegistro, WsRFB.redesim pDados35, string pNroDBE)
    {
        try
        {
            WsRFB.ServiceReginRFB ws = new WsRFB.ServiceReginRFB();
            WsRFB.dadosCNPJ dados = new WsRFB.dadosCNPJ();
            WsRFB.Retorno resp = new WsRFB.Retorno();
            ws.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
            resp = ws.ServiceWs11(pCNPJ);

            dados = resp.oCNPJResponse.dadosCNPJ[0];


            DateTime DtHoje = dHelperORACLE.SysdateOracle();

            //List<string> pEventos = new List<string>();

            using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                string _vTipRegistro = GetTipoEmpresaPorNaturezaJuridica(dados.naturezaJuridica); //Verificar

                decimal CodMuniEmpresa = decimal.Parse(CalDvMunicipio(dados.endereco.codMunicipio));

                #region PSC_PROTOCOLO
                using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                {
                    p.MainConnectionProvider = cp;
                    p.Pro_protocolo = pProtocoloJunta;
                    p.Pro_status = 1;
                    p.Pro_fec_inc = DtHoje;
                    p.Pro_tmu_tuf_uf = dados.endereco.uf;
                    p.Pro_tmu_cod_mun = int.Parse(CodMuniEmpresa.ToString());
                    p.Pro_tip_operacao = 1; //Verificar
                    p.Pro_env_sef = 2;
                    p.Pro_flag_vigilancia = 2;
                    p.Pro_fec_atualizacao = DtHoje;
                    p.Pro_tge_tgacao = 700;
                    p.Pro_tge_vgacao = Int32.Parse("2");
                    p.Pro_cnpj_org_reg = pCNPJOrgaoRegistro;
                    p.PRO_NR_REQUERIMENTO = NroRequerimento;
                    p.PRO_VPV_COD_PROTOCOLO = pNroViabilidade;
                    p.PRO_NR_DBE = pNroDBE;

                    p.Update();

                }
                #endregion

                #region PSC_IDENT_PROTOCOLO
                StringBuilder sqlIdent = new StringBuilder();
                sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                sqlIdent.Append("'" + pProtocoloJunta + "'");
                sqlIdent.Append(", '" + dados.cnpj + "'");
                sqlIdent.Append(", '" + nire + "'");
                sqlIdent.Append(", '" + dados.nomeEmpresarial + "'");
                sqlIdent.Append(" )");

                using (dHelperORACLE c = new dHelperORACLE())
                {
                    c.MainConnectionProvider = cp;
                    c.ExecuteNonQuery(sqlIdent);
                }

                #endregion

                #region MAC_LOG_CARGA_JUNTA_HOMOLOG
                using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                {
                    m.MainConnectionProvider = cp;

                    m.MLC_PROTOCOLO = pProtocoloJunta;
                    m.MLC_CPF_HOMOLOGADOR = "11111111111";
                    m.MLC_DTA_HOMOLOGACAO = DtHoje;
                    m.MLC_DATA_CARREGA_WS11 = DtHoje;
                    //m.Update();

                }
                #endregion

                #region RUC_GENERAL
                using (Ruc_General rg = new Ruc_General())
                {
                    rg.MainConnectionProvider = cp;
                    rg.rge_pra_protocolo = pProtocoloJunta;
                    rg.rge_ruc = "";
                    rg.rge_tge_ttip_reg = 257;
                    rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                    rg.rge_tge_ttip_ctrib = 153;
                    rg.rge_tge_vtip_ctrib = 9999;
                    rg.rge_tge_ttip_pers = 233;
                    rg.rge_tge_vtip_pers = 1;
                    rg.rge_cgc_cpf = dados.cnpj;
                    rg.rge_tge_ttamanho = 21;
                    rg.rge_tge_vtamanho = 3;
                    rg.rge_nomb = dados.nomeEmpresarial;
                    rg.rge_codg_mun = CodMuniEmpresa;
                    //rg.rge_tae_cod_actvd = dados.cnaePrincipal;
                    rg.rge_tuf_cod_uf = dados.endereco.uf;

                    rg.Update();

                }
                #endregion

                #region RUC_ESTAB
                using (Ruc_Estab rb = new Ruc_Estab())
                {
                    rb.MainConnectionProvider = cp;
                    rb.res_rge_pra_protocolo = pProtocoloJunta;
                    rb.res_ide_estab = 0;
                    rb.res_tip_estab = 1;
                    rb.res_tge_ttip_reg = 155;
                    rb.res_tge_vtip_reg = 9999;
                    rb.res_tge_tcond_uso = 152;
                    rb.res_tge_vcond_uso = 9999;
                    rb.res_sigl = "";
                    rb.res_area = 0;
                    rb.res_tge_tuni_medid = 156;
                    rb.res_tge_vuni_medid = 9999;
                    //rb.res_fec_inic;
                    //rb.res_fec_fin;
                    rb.res_nume = dados.endereco.numLogradouro;
                    //rb.res_caja_po = ""; ;
                    //rb.res_zona_caja_po = "";
                    rb.res_tus_cod_usr = "REGIN";
                    rb.res_nom_estab = dados.nomeEmpresarial;
                    //rb.res_num_reg_prop;
                    //rb.res_quad_lote;
                    rb.res_ident_comp = dados.endereco.complementoLogradouro;
                    rb.res_refer = validaNulo(dados.endereco.referencia);
                    rb.res_ttl_tip_logradoro = dados.endereco.codTipoLogradouro;
                    rb.res_direccion = dados.endereco.logradouro;
                    rb.res_urbanizacion = dados.endereco.bairro;
                    rb.res_tes_cod_estado = dados.endereco.uf;
                    rb.res_zona_postal = validaNulo(dados.endereco.cep);
                    rb.res_tmu_cod_mun = CodMuniEmpresa;
                    rb.res_nire_sede = "";
                    rb.res_cnpj_sede = validaNulo(dados.cnpjMatriz);

                    rb.Update();
                }
                #endregion

                #region RUC_COMP

                using (Ruc_Comp rc = new Ruc_Comp())
                {
                    rc.MainConnectionProvider = cp;
                    //rc.rco_fec_const_nasc = null;
                    rc.rco_num_reg_merc = nire;
                    //rc.rco_fec_reg_merc = null;
                    rc.rco_tge_ttip_doc = 151;
                    rc.rco_tge_vtip_doc = 9999;
                    //rc.rco_num_doc_ident
                    //rc.rco_fec_emi_doc_id
                    rc.rco_tnc_cod_natur = decimal.Parse(dados.naturezaJuridica);
                    rc.rco_domic_pais = 1;
                    rc.rco_fec_incorp = DtHoje;
                    rc.rco_val_cap_soc = decimal.Parse(dados.capitalSocial) / 100;
                    //rc.rco_fec_rg_cap_soc
                    //rc.rco_sexo 
                    rc.rco_nume = validaNulo(dados.endereco.numLogradouro);
                    //rc.rco_caja_po
                    //rc.rco_zona_caja_po
                    rc.rco_tge_tpais = 22;
                    rc.rco_tge_vpais = 105;
                    //rc.rco_ruc_ext_uf
                    rc.rco_tus_cod_usr = "REGIN";
                    //rc.rco_emis_doc_ident
                    //rc.rco_quad_lote
                    rc.rco_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                    rc.rco_refer = validaNulo(dados.endereco.referencia);
                    rc.rco_lic_mun = "";
                    rc.rco_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                    rc.rco_direccion = validaNulo(dados.endereco.logradouro);
                    rc.rco_urbanizacion = validaNulo(dados.endereco.bairro);
                    rc.rco_tes_cod_estado = validaNulo(dados.endereco.uf);
                    rc.rco_zona_postal = validaNulo(dados.endereco.cep);
                    //rc.rco_tge_tcier_bal
                    //rc.rco_tge_vcier_bal
                    //rc.rco_tge_treg_trib
                    //rc.rco_tge_vreg_trib
                    rc.rco_tmu_cod_mun = CodMuniEmpresa;
                    rc.rco_rge_pra_protocolo = pProtocoloJunta;
                    //rc.rco_num_reg_merc_sede
                    rc.Update();
                }

                #endregion

                #region RUC_ACTV_ECON

                Ruc_Actv_Econ cav = new Ruc_Actv_Econ();
                cav.MainConnectionProvider = cp;
                cav.rae_rge_pra_protocolo = pProtocoloJunta;
                cav.rae_tae_cod_actvd = dados.cnaePrincipal;
                cav.rae_calif_actv = "1";
                cav.rae_porcent = 100;
                cav.rae_tus_cod_usr = "REGIN";
                cav.rae_fec_actl = DtHoje;
                //cav.Update();
                if (dados.cnaeSecundaria != null)
                {
                    foreach (string pCNAE in dados.cnaeSecundaria)
                    {
                        if (pCNAE != null && pCNAE != "0000000")
                        {
                            using (cav = new Ruc_Actv_Econ())
                            {
                                cav.MainConnectionProvider = cp;

                                cav.rae_rge_pra_protocolo = pProtocoloJunta;
                                cav.rae_tae_cod_actvd = pCNAE;
                                cav.rae_calif_actv = "2";
                                cav.rae_porcent = 0;
                                cav.rae_tus_cod_usr = "REGIN";
                                cav.rae_fec_actl = DtHoje;
                                cav.Update();
                            }
                        }
                    }
                }
                #endregion

                #region RUC_GEN_PROTOCOLO
                using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                {
                    gc.MainConnectionProvider = cp;
                    gc.rgp_rge_pra_protocolo = pProtocoloJunta;
                    gc.rgp_tge_tip_tab = 902;
                    gc.rgp_tge_cod_tip_tab = 1;
                    gc.rgp_valor = dados.objetoSocial;
                    gc.rgp_tus_cod_usr = "REGIN";
                    gc.rgp_fec_actl = DtHoje;
                    if (dados.objetoSocial != "")
                    {
                        gc.Update();
                    }
                }

                #endregion

                #region PSC_PROT_EVENTO_RFB
                StringBuilder sqlEvento = new StringBuilder();
                for (int i = 0; i < pEventos.Count; i++)
                {
                    sqlEvento = new StringBuilder();
                    sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                    sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                    sqlEvento.AppendLine(" Values ( ");
                    sqlEvento.AppendLine("'" + pProtocoloJunta + "'");
                    sqlEvento.AppendLine(" ," + pEventos[i].ToString());
                    sqlEvento.AppendLine(" ) ");
                    using (dHelperORACLE cpe = new dHelperORACLE())
                    {
                        cpe.MainConnectionProvider = cp;
                        cpe.ExecuteNonQuery(sqlEvento);
                    }
                }


                #endregion

                #region RUC_RELAT_PROF e RUC_PROF

                foreach (WsRFB.dadosSocio socio in dados.dadosSocio)
                {
                    WsRFB.dadosCPF dados09 = new WsRFB.dadosCPF();

                    if (socio.cpfCnpj.Length < 12)
                    {
                        WsRFB.ServiceReginRFB ws09 = new WsRFB.ServiceReginRFB();
                        WsRFB.Retorno resp09 = new WsRFB.Retorno();
                        ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                        resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                        if (resp09.status == "OK")
                        {
                            dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                        }
                    }

                    using (Ruc_Relat_Prof rp = new Ruc_Relat_Prof())
                    {
                        rp.MainConnectionProvider = cp;
                        rp.rrp_rge_pra_protocolo = pProtocoloJunta;
                        rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rrp_tge_ttip_relac = 24;
                        rp.rrp_tge_vtip_relac = 2;
                        rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                        rp.rrp_tge_tcod_qual = 23;
                        rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                        rp.rrp_desc_doc = "";
                        rp.rrp_tus_cod_usr = "JUCESC";
                        rp.rrp_cnpj_vacio = 0;
                        rp.Update();
                    }

                    using (Ruc_Prof rp = new Ruc_Prof())
                    {
                        rp.MainConnectionProvider = cp;
                        rp.rpr_rge_pra_protocolo = pProtocoloJunta;
                        rp.rpr_tge_tpais = 22;
                        rp.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                        rp.rpr_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rpr_tge_ttip_pers = 233;
                        rp.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                        rp.rpr_nomb = socio.nome;

                        if (dados09 != null && dados09.numCPF != "")
                        {
                            rp.rpr_nome_mae = dados09.nomeMae;

                            rp.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                            rp.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                            rp.rpr_nume = validaNulo(dados09.endereco.numLogradouro);
                            rp.rpr_ident_comp = validaNulo(dados09.endereco.complementoLogradouro);
                            rp.rpr_refer = validaNulo(dados09.endereco.referencia);
                            rp.rpr_ttl_tip_logradoro = validaNulo(dados09.endereco.codTipoLogradouro);
                            rp.rpr_direccion = validaNulo(dados09.endereco.logradouro);
                            rp.rpr_urbanizacion = validaNulo(dados09.endereco.bairro);
                            rp.rpr_tes_cod_estado = validaNulo(dados09.endereco.uf);
                            rp.rpr_zona_postal = validaNulo(dados09.endereco.cep);

                            rp.rpr_tmu_cod_mun = Decimal.Parse(CalDvMunicipio(dados09.endereco.codMunicipio));

                        }
                        rp.Update();
                    }

                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            string a = "";
        }
    }


    public void GravaWsRFB11Ruc11(List<string> pEventos, string pProtocoloJunta, string pCNPJ, string nire, string pNroViabilidade, string NroRequerimento, string pCNPJOrgaoRegistro, WsRFB.redesim pDados35, string pNroDBE)
    {
        try
        {
            WsRFB.ServiceReginRFB ws = new WsRFB.ServiceReginRFB();
            WsRFB.dadosCNPJ dados = new WsRFB.dadosCNPJ();
            WsRFB.Retorno resp = new WsRFB.Retorno();
            ws.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
            resp = ws.ServiceWs11(pCNPJ);

            dados = resp.oCNPJResponse.dadosCNPJ[0];


            DateTime DtHoje = dHelperORACLE.SysdateOracle();

            //List<string> pEventos = new List<string>();

            using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
            {
                cp.OpenConnection();
                cp.BeginTransaction();
                string _vTipRegistro = GetTipoEmpresaPorNaturezaJuridica(dados.naturezaJuridica); //Verificar

                decimal CodMuniEmpresa = decimal.Parse(CalDvMunicipio(dados.endereco.codMunicipio)); 

                #region PSC_PROTOCOLO
                using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                {
                    p.MainConnectionProvider = cp;
                    p.Pro_protocolo = pProtocoloJunta;
                    p.Pro_status = 1;
                    p.Pro_fec_inc = DtHoje;
                    p.Pro_tmu_tuf_uf = dados.endereco.uf;
                    p.Pro_tmu_cod_mun = int.Parse(CodMuniEmpresa.ToString()); 
                    p.Pro_tip_operacao = 1; //Verificar
                    p.Pro_env_sef = 2;
                    p.Pro_flag_vigilancia = 2;
                    p.Pro_fec_atualizacao = DtHoje;
                    p.Pro_tge_tgacao = 700;
                    p.Pro_tge_vgacao = Int32.Parse("2");
                    p.Pro_cnpj_org_reg = pCNPJOrgaoRegistro;
                    p.PRO_NR_REQUERIMENTO = NroRequerimento;
                    p.PRO_VPV_COD_PROTOCOLO = pNroViabilidade;
                    p.PRO_NR_DBE = pNroDBE;

                    p.Update();

                }
                #endregion

                #region PSC_IDENT_PROTOCOLO
                StringBuilder sqlIdent = new StringBuilder();
                sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL ) 
                                    Values ( ");
                sqlIdent.Append("'" + pProtocoloJunta + "'");
                sqlIdent.Append(", '" + dados.cnpj + "'");
                sqlIdent.Append(", '" + nire + "'");
                sqlIdent.Append(", '" + dados.nomeEmpresarial + "'");
                sqlIdent.Append(" )");

                using (dHelperORACLE c = new dHelperORACLE())
                {
                    c.MainConnectionProvider = cp;
                    c.ExecuteNonQuery(sqlIdent);
                }

                #endregion

                #region MAC_LOG_CARGA_JUNTA_HOMOLOG
                using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                {
                    m.MainConnectionProvider = cp;

                    m.MLC_PROTOCOLO = pProtocoloJunta;
                    m.MLC_CPF_HOMOLOGADOR = "11111111111";
                    m.MLC_DTA_HOMOLOGACAO = DtHoje;
                    m.MLC_DATA_CARREGA_WS11 = DtHoje;
                    //m.Update();

                }
                #endregion

                #region RUC_GENERAL
                using (Ruc_General rg = new Ruc_General())
                {
                    rg.MainConnectionProvider = cp;
                    rg.rge_pra_protocolo = pProtocoloJunta;
                    rg.rge_ruc = "";
                    rg.rge_tge_ttip_reg = 257;
                    rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                    rg.rge_tge_ttip_ctrib = 153;
                    rg.rge_tge_vtip_ctrib = 9999;
                    rg.rge_tge_ttip_pers = 233;
                    rg.rge_tge_vtip_pers = 1;
                    rg.rge_cgc_cpf = dados.cnpj;
                    rg.rge_tge_ttamanho = 21;
                    rg.rge_tge_vtamanho = 3;
                    rg.rge_nomb = dados.nomeEmpresarial;
                    rg.rge_codg_mun = CodMuniEmpresa; 
                    //rg.rge_tae_cod_actvd = dados.cnaePrincipal;
                    rg.rge_tuf_cod_uf = dados.endereco.uf;

                    rg.Update();

                }
                #endregion

                #region RUC_ESTAB
                using (Ruc_Estab rb = new Ruc_Estab())
                {
                    rb.MainConnectionProvider = cp;
                    rb.res_rge_pra_protocolo = pProtocoloJunta;
                    rb.res_ide_estab = 0;
                    rb.res_tip_estab = 1;
                    rb.res_tge_ttip_reg = 155;
                    rb.res_tge_vtip_reg = 9999;
                    rb.res_tge_tcond_uso = 152;
                    rb.res_tge_vcond_uso = 9999;
                    rb.res_sigl = "";
                    rb.res_area = 0;
                    rb.res_tge_tuni_medid = 156;
                    rb.res_tge_vuni_medid = 9999;
                    //rb.res_fec_inic;
                    //rb.res_fec_fin;
                    rb.res_nume = dados.endereco.numLogradouro;
                    //rb.res_caja_po = ""; ;
                    //rb.res_zona_caja_po = "";
                    rb.res_tus_cod_usr = "REGIN";
                    rb.res_nom_estab = dados.nomeEmpresarial;
                    //rb.res_num_reg_prop;
                    //rb.res_quad_lote;
                    rb.res_ident_comp = dados.endereco.complementoLogradouro;
                    rb.res_refer = validaNulo(dados.endereco.referencia);
                    rb.res_ttl_tip_logradoro = dados.endereco.codTipoLogradouro;
                    rb.res_direccion = dados.endereco.logradouro;
                    rb.res_urbanizacion = dados.endereco.bairro;
                    rb.res_tes_cod_estado = dados.endereco.uf;
                    rb.res_zona_postal = validaNulo(dados.endereco.cep);
                    rb.res_tmu_cod_mun = CodMuniEmpresa; 
                    rb.res_nire_sede = "";
                    rb.res_cnpj_sede = validaNulo(dados.cnpjMatriz);

                    rb.Update();
                }
                #endregion

                #region RUC_COMP

                using (Ruc_Comp rc = new Ruc_Comp())
                {
                    rc.MainConnectionProvider = cp;
                    //rc.rco_fec_const_nasc = null;
                    rc.rco_num_reg_merc = nire;
                    //rc.rco_fec_reg_merc = null;
                    rc.rco_tge_ttip_doc = 151;
                    rc.rco_tge_vtip_doc = 9999;
                    //rc.rco_num_doc_ident
                    //rc.rco_fec_emi_doc_id
                    rc.rco_tnc_cod_natur = decimal.Parse(dados.naturezaJuridica);
                    rc.rco_domic_pais = 1;
                    rc.rco_fec_incorp = DtHoje;
                    rc.rco_val_cap_soc = decimal.Parse(dados.capitalSocial) / 100;
                    //rc.rco_fec_rg_cap_soc
                    //rc.rco_sexo 
                    rc.rco_nume = validaNulo(dados.endereco.numLogradouro);
                    //rc.rco_caja_po
                    //rc.rco_zona_caja_po
                    rc.rco_tge_tpais = 22;
                    rc.rco_tge_vpais = 105;
                    //rc.rco_ruc_ext_uf
                    rc.rco_tus_cod_usr = "REGIN";
                    //rc.rco_emis_doc_ident
                    //rc.rco_quad_lote
                    rc.rco_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                    rc.rco_refer = validaNulo(dados.endereco.referencia);
                    rc.rco_lic_mun = "";
                    rc.rco_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                    rc.rco_direccion = validaNulo(dados.endereco.logradouro);
                    rc.rco_urbanizacion = validaNulo(dados.endereco.bairro);
                    rc.rco_tes_cod_estado = validaNulo(dados.endereco.uf);
                    rc.rco_zona_postal = validaNulo(dados.endereco.cep);
                    //rc.rco_tge_tcier_bal
                    //rc.rco_tge_vcier_bal
                    //rc.rco_tge_treg_trib
                    //rc.rco_tge_vreg_trib
                    rc.rco_tmu_cod_mun = CodMuniEmpresa;
                    rc.rco_rge_pra_protocolo = pProtocoloJunta;
                    //rc.rco_num_reg_merc_sede
                    rc.Update();
                }

                #endregion

                #region RUC_ACTV_ECON

                Ruc_Actv_Econ cav = new Ruc_Actv_Econ();
                cav.MainConnectionProvider = cp;
                cav.rae_rge_pra_protocolo = pProtocoloJunta;
                cav.rae_tae_cod_actvd = dados.cnaePrincipal;
                cav.rae_calif_actv = "1";
                cav.rae_porcent = 100;
                cav.rae_tus_cod_usr = "REGIN";
                cav.rae_fec_actl = DtHoje;
                //cav.Update();
                if (dados.cnaeSecundaria != null)
                {
                    foreach (string pCNAE in dados.cnaeSecundaria)
                    {
                        if (pCNAE != null && pCNAE != "0000000")
                        {
                            using (cav = new Ruc_Actv_Econ())
                            {
                                cav.MainConnectionProvider = cp;

                                cav.rae_rge_pra_protocolo = pProtocoloJunta;
                                cav.rae_tae_cod_actvd = pCNAE;
                                cav.rae_calif_actv = "2";
                                cav.rae_porcent = 0;
                                cav.rae_tus_cod_usr = "REGIN";
                                cav.rae_fec_actl = DtHoje;
                                cav.Update();
                            }
                        }
                    }
                }
                #endregion

                #region RUC_GEN_PROTOCOLO
                using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                {
                    gc.MainConnectionProvider = cp;
                    gc.rgp_rge_pra_protocolo = pProtocoloJunta;
                    gc.rgp_tge_tip_tab = 902;
                    gc.rgp_tge_cod_tip_tab = 1;
                    gc.rgp_valor = dados.objetoSocial;
                    gc.rgp_tus_cod_usr = "REGIN";
                    gc.rgp_fec_actl = DtHoje;
                    if (dados.objetoSocial != "")
                    {
                        gc.Update();
                    }
                }

                #endregion

                #region PSC_PROT_EVENTO_RFB
                StringBuilder sqlEvento = new StringBuilder();
                for (int i = 0; i < pEventos.Count; i++)
                {
                    sqlEvento = new StringBuilder();
                    sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                    sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                    sqlEvento.AppendLine(" Values ( ");
                    sqlEvento.AppendLine("'" + pProtocoloJunta + "'");
                    sqlEvento.AppendLine(" ," + pEventos[i].ToString());
                    sqlEvento.AppendLine(" ) ");
                    using (dHelperORACLE cpe = new dHelperORACLE())
                    {
                        cpe.MainConnectionProvider = cp;
                        cpe.ExecuteNonQuery(sqlEvento);
                    }
                }


                #endregion

                #region RUC_RELAT_PROF e RUC_PROF

                foreach (WsRFB.dadosSocio socio in dados.dadosSocio)
                {
                    WsRFB.dadosCPF dados09 = new WsRFB.dadosCPF();

                    if (socio.cpfCnpj.Length < 12)
                    {
                        WsRFB.ServiceReginRFB ws09 = new WsRFB.ServiceReginRFB();
                        WsRFB.Retorno resp09 = new WsRFB.Retorno();
                        ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                        resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                        if (resp09.status == "OK")
                        {
                            dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                        }
                    }

                    using (Ruc_Relat_Prof rp = new Ruc_Relat_Prof())
                    {
                        rp.MainConnectionProvider = cp;
                        rp.rrp_rge_pra_protocolo = pProtocoloJunta;
                        rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rrp_tge_ttip_relac = 24;
                        rp.rrp_tge_vtip_relac = 2;
                        rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                        rp.rrp_tge_tcod_qual = 23;
                        rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                        rp.rrp_desc_doc = "";
                        rp.rrp_tus_cod_usr = "JUCESC";
                        rp.rrp_cnpj_vacio = 0;
                        rp.Update();
                    }

                    using (Ruc_Prof rp = new Ruc_Prof())
                    {
                        rp.MainConnectionProvider = cp;
                        rp.rpr_rge_pra_protocolo = pProtocoloJunta;
                        rp.rpr_tge_tpais = 22;
                        rp.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                        rp.rpr_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rpr_tge_ttip_pers = 233;
                        rp.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                        rp.rpr_nomb = socio.nome;
                        
                        if (dados09 != null && dados09.numCPF != "")
                        {
                            rp.rpr_nome_mae = dados09.nomeMae;

                            rp.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                            rp.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                            rp.rpr_nume = validaNulo(dados09.endereco.numLogradouro);
                            rp.rpr_ident_comp = validaNulo(dados09.endereco.complementoLogradouro);
                            rp.rpr_refer = validaNulo(dados09.endereco.referencia);
                            rp.rpr_ttl_tip_logradoro = validaNulo(dados09.endereco.codTipoLogradouro);
                            rp.rpr_direccion = validaNulo(dados09.endereco.logradouro);
                            rp.rpr_urbanizacion = validaNulo(dados09.endereco.bairro);
                            rp.rpr_tes_cod_estado = validaNulo(dados09.endereco.uf);
                            rp.rpr_zona_postal = validaNulo(dados09.endereco.cep);

                            rp.rpr_tmu_cod_mun = Decimal.Parse(CalDvMunicipio(dados09.endereco.codMunicipio));

                        }
                        rp.Update();
                    }

                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            string a = "";
        }
    }

    private string validaNulo(object pValue)
    {
        if (pValue == null) 
        {
            return "";
        }
        return pValue.ToString();
    }

    public static string GetTipoEmpresaPorNaturezaJuridica(String pNatureza)
    {


        using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT TNJ_CO_GRUPO
                         FROM TAB_NATUREZA_JURIDCA
                         WHERE TNJ_CO_NATUREZA_JURIDICA = :pNatureza");


            OracleCommand cmdToExecute = new OracleCommand();
            cmdToExecute.CommandText = sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Pessoa_Juridica");
            cmdToExecute.Connection = _conn;

            OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

            try
            {
                cmdToExecute.Parameters.Add(new OracleParameter("pNatureza", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pNatureza));

                cmdToExecute.Connection.Open();
                // Execute query.

                adapter.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                {
                    return toReturn.Rows[0]["TNJ_CO_GRUPO"].ToString();
                }
                else
                {
                    return "11";
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                _conn.Close();
                cmdToExecute.Dispose();
            }
        }
    }

    
    public DateTime ConvertStringDateTime(string _data)
    {
        if (_data.Trim() != "")
        {
            int dia = int.Parse(_data.Substring(6, 2));
            int mes = int.Parse(_data.Substring(4, 2));
            int ano = int.Parse(_data.Substring(0, 4));

            return new DateTime(ano, mes, dia);
        }
        return new DateTime(1, 1, 1);
    }

    private string CalDvMunicipio(string CodMUnicipio)
    {
        if (CodMUnicipio != "")
        {
            string codMuni = psc.Framework.General.CalculateVerificationDigit(CodMUnicipio, 11).ToString();

            return CodMUnicipio + codMuni;
        }

        return "";

    }

    
}

