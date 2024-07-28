using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bTabelasAuxiliares
    {
        #region MIGRADO JUCERJA
        public DataTable getEquiparados()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryPaisEquiparado());
            }
        }
        #endregion

        public static string GetConfigRegin(int pCod)
        {

            DataTable dtConfig = dHelperORACLE.getConfigRegin();
            string pRetorno = "";
            if (pCod == 28)
            {
                pRetorno = dtConfig.Rows[0]["PCR_SECAO_DESTINO_FCN"].ToString();
            }
            

            return pRetorno;
        }

        public static DataTable QueryTabGenericaByCodigo(int Codigo)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryByTipo(Codigo.ToString()));

            }
        }

        public static DataTable QueryCNAEContribExt()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryCNAEContribExt());

            }
        }

        public static DataTable QueryFormaAtuacao()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryFormaAtuacao());

            }
        }

        public static DataTable QueryComercioExterior()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryComercioExterior());

            }
        }

        public static DataTable QueryEscolaridade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryByTipo("99"));

            }
        }
        public static DataTable QueryTipoUnidade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTipoUnidade());

            }
        }

        public static int getTipoPessoaJuridicaId(Int32 codigoNJ)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.getTipoPessoaJuridicaId(codigoNJ));

            }
        }

        public DataTable QueryNaturezaJuridicaTipoGrupo(string codGrupo)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaTipoGrupo(codGrupo));

            }
        }

        public DataTable QueryNaturezaJuridicaOrgaoPublico()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaOrgaoPublico());

            }
        }

        public DataTable QueryNaturezaJuridicaRCPJ()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaRcpj());

            }
        }

        public DataTable QueryNaturezaJuridicaAll()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaAll());

            }
        }
        public DataTable QueryNaturezaJuridicaSefaz()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaSefaz());

            }

        }

        public DataTable QueryNaturezaJuridicaTodas()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridicaTodas());

            }
        }

       
        public DataTable getNaturezaJuridica_old()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridica_old());

            }
        }
        public DataTable getNaturezaJuridica(Int32 codigoNJ)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryNaturezaJuridica(codigoNJ));

            }
        }

        public string GetNaturezaJuridica(Int32 codigoNJ)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.getNaturezaJuridica(codigoNJ));

            }
        }
 
        public DataTable getPorteSociedade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryPorteSociedade());

            }

        }

        public DataTable getTipoPessoaJuridica()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTipoPessoaJuridica());

            }

        }

        public DataTable getOrgaoExpedidor()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryOrgaoExpedidor());

            }

        }

        public DataTable getEstadoCivil()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryEstadoCivil());

            }

        }

        public DataTable getTipoAtividade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTipoAtividade());

            }

        }

        public DataTable getQualificacaoSociedade(Int32 wAux)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryQualificacaoSociedade(wAux));

            }

        }
        public DataTable getQualificacaoAssociacao()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryQualificacaoAssociacao());

            }

        }

        public DataTable getProfissao()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryProfissao());

            }

        }
        public DataTable getEnquadramento()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryEnquadramento());

            }

        }

        public string getEnquadramentoPorCodigo(string Codigo)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryEnquadramentoPorCodigo(Codigo));

            }

        }

        

        public DataTable getTipoVisto()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTipoVisto());

            }

        }

        public DataTable getCargo()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryCargo());

            }

        }

        public DataTable getFormaConvocacao()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryFormaConvocacao());

            }

        }

        public DataTable getTabelaQuorum()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTabelaQuorum());

            }
        }

        public DataTable getUF()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryUF());

            }

        }

        public DataTable getOrgaoUF_aa()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryOrgaoUF_aa());

            }

        }

        public DataTable getPais()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryPais());

            }

        }

        public DataTable getPaisNacionalidade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryPaisNacionalidade());

            }

        }

        public DataTable getTipoLogradouro()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {

                return (c.QueryTipoLogradouro());

            }

        }

        public String getCorrelativo(string Tipo)
        {
            using (dCorrelativo c = new dCorrelativo())
            {

                return (c.GetCorrelativo());

            }

        }

        public DataTable getTipoIdentidade()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryTipoDocumento());
            }
        }

        public DataTable getRegime()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryRegime());
            }
        }

        /// <summary>
        /// Retorna o Regime de bnes pelo tipo do Estado civil
        /// </summary>
        /// <param name="pTipo" values="1-Casado 2-Solteiro"></param>
        /// <returns></returns>
        public static DataTable getRegimeporTipo(int pTipo)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryRegimePorTipo(pTipo));
            }
        }


        public DataTable getTipoAssistidoRepresentado(Boolean wChave)
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryTipoAssistidoRepresentado(wChave));
            }
        }
        public DataTable getTipoAssistidoRepresentado()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryTipoAssistidoRepresentado());
            }
        }
        public DataTable getEmancipacao()
        {
            using (TAB_GENERICA c = new TAB_GENERICA())
            {
                return (c.QueryEmancipacao());
            }
        }

        public DataTable getAtosAlteracao()
        {
            using (dA002_Ato a = new dA002_Ato())
            {
                return (a.QueryAtosAlteracao());
            }

        }

        public DataTable getAtosSocios()
        {
            using (dA002_Ato a = new dA002_Ato())
            {
                return (a.QueryAtosSocios());
            }

        }
        public DataTable getMunicipioOrgao(string WCodigoRegistro)
        {
            using (dR003_Rel_Org_Munic m = new dR003_Rel_Org_Munic())
                return (m.QueryNomeMunicipio(WCodigoRegistro ));
        }
        public DataTable getMunicipioOrgaoUF(string WCodigoRegistro)
        {
            using (dR003_Rel_Org_Munic m = new dR003_Rel_Org_Munic())
                return (m.QueryMunicipioOrgaoUF(WCodigoRegistro));
        }

        public DataTable getOrgaoRegistro(string nome)
        {
            using (dT004_Orgao_Registro c = new dT004_Orgao_Registro())
            {
                c.t004_ds_org_reg = nome;
                return c.Query();
            }
        }

        public static void UpdateOrgaoRegistro(string _sigla, string _descricao)
        {
            //VErifica se existe codigo cadastrado
            using (TAB_GENERICA o = new TAB_GENERICA())
            {
                o.TGE_TIP_TAB = 1565;
                o.TGE_NOMB_DESC = _sigla + " - " + _descricao;
                o.Update();

                //DataTable dt = o.Query(_sigla, _descricao);
                //if (dt.Rows.Count == 0)
                //{
                //    o.TGE_TIP_TAB = 1565;
                //    o.TGE_NOMB_DESC = _sigla + " - " + _descricao;
                //    o.Update();
                //}
            }
        }
    }
}