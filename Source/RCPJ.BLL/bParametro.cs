using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using System.Data;
using System.Configuration.Assemblies;

namespace RCPJ.BLL
{
    [Serializable]
    public class bParametro
    {

        #region Propriedades
        private string _cnpj = string.Empty;
        private string _codigo = string.Empty;
        private string _descricao = string.Empty;
        private string _valor = string.Empty;
        private List<bParametro> _listaParam = new List<bParametro>();

        public List<bParametro> ListaParam
        {
            get { return _listaParam; }
            set { _listaParam = value; }
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
        public string codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public string valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
       
        #endregion
    
     
        #region Constructors
        public bParametro()
        {
           
            //InitClass();
        }
        public bParametro(string pCNPJ)
            : this()
        {
            _cnpj = pCNPJ;
            Populate();
        }
        #endregion

        public enum Valores:int
        {
            IMPRIME_DECLARACAO_PORTE = 1,
            AMBIENTE = 2,
            UF_PRINCIPAL = 3,
            WS_VIABILIDADE = 4,
            WS_ENDERECO = 5,
            TIPO_ORGAO_REGISTRO = 6,
            CNPJ_INSTITUICAO_DEFAULT = 7,
            TELA_CONFIRMACAO_DBE_VIABILIDADE = 8,
            SERVICOS_PORTAL = 9,
            VIABILIDADE_CONCLUIDA = 10,
            LOGOTIPO_CABECALHO = 11,
            LOGOTIPO_RODAPE = 12,
            FORMATO_NUM_PROCESSO_OR = 13,
            FORMATO_NUM_REQUERIMENTO = 14,
            PAGINA_PRINCIPAL = 15,
            PRIMERA_CLAVE = 16,
            WS_SERVICO_REQUERIMENTO = 17,
            START_PAGE_PRINCIPAL = 18,
            CODIGO_OUTRAS_EXIGENCIAS_PADRAO = 19,
            GERA_PROTOCOLO_OR = 20,
            EXIGE_REPRESENTANTE_ANALFABETO = 21,
            EXIBE_CAMPO_UNIDADE_DE_ENTREGA = 22,
            EXIBE_INFORMACOES_ADICIONAIS_ASSOCIACAOeFUNDACAO = 23,
            IMPRIME_BOLETO_DAE = 24,
            EXIBE_SERVICOS_ALTERACAO = 25,
            EXIBE_DBE_DOCUMENTOS_GERADOS = 26,
            IMPRIME_CONTRATO_PADRAO = 27,
            IMPRIME_CONTRACAPA = 28,
            VALIDA_CNAE_FILIAL_MATRIZ = 29,
            IMPRIME_NOME_ORG_EXPED = 30,
            PERMITE_EVENTO_FILIAL_EMPRESARIO = 31,
            IMPRIME_SOCIO_NOVO_PREAMBULO = 32,
            IMPRIME_CPF_NO_CONTRATO = 33,
            IMPRIME_CNPJ_FILIAL_EMPRESARIO = 34,
            PERMITE_EVENTO_MATRIZ_FILIAL_EMPRESARIO = 35,
            ANALISE_PREVIA_FCN = 36,
            VALIDA_COMPLEMENTO_DBE = 37,
            EXIGE_NOME_PAI_SOCIO = 38,
            EDITA_NOME_FANTASIA = 39,
            CONFIRMA_SIM_DEFAULT = 40,
            COMPARA_ENDERECO_DBE = 41,
            POSSUI_EVENTO_BAIXA = 42,
            CRITICA_USU_ANALISTA = 43,
            VALIDA_PROTOCOLO_OR = 44,
            POSSUI_PARECER_JURIDICO = 45,
            QUANTIDADE_PROCESSO_ANALISTA = 46,
            EDITA_OBJETO_FILIAL = 47,
            EXIBE_BOTAO_PORTE = 48,
            IMPRIME_CONTRATO_PARAMETRO = 49,
            EXIGE_NOME_MAE_SOCIO = 50,
            VALIDA_DATA_VIABILIDADE = 51,
            HABILITA_PROCESSO_DIGITAL = 52,
            POSSUI_VIA_UNICA = 53,
            UTILIZA_NOVO_CONTRATO_BAIXA = 54,
            MOSTRA_CAMPOS_BAIXA_EMPRESARIO = 55,
            PERMITE_SOCIOS_GUARDA_LIVRO = 56
        }

        #region Implementação

        public string getValor(Valores pCodigo)
        {
            string ret = "";
            foreach (bParametro obj in _listaParam)
            {
                if (obj.codigo == ((int)pCodigo).ToString())
                {
                    ret = obj.valor;
                    break;
                }
            }

            return ret;
        }

        public void Populate()
        {
            using (dT004_Orgao_Registro org = new dT004_Orgao_Registro())
            {
                _listaParam = new List<bParametro>();

                DataTable dtOrgaoReg = org.GetParametros(_cnpj);
                foreach(DataRow row in dtOrgaoReg.Rows)
                {
                    bParametro temp = new bParametro();

                    temp.cnpj = row["cnpj"].ToString();
                    temp.descricao = row["descricao"].ToString();
                    temp.codigo = row["codigo"].ToString();
                    temp.valor = row["valor"].ToString();

                    _listaParam.Add(temp);
                }

            }
        }
        #endregion
        
       
    }
}
