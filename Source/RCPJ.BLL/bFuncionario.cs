using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bFuncionario
    {
        #region Class Member Declarations

        private int _sqFuncionario = 0;
        private string _nome = string.Empty;
        private string _cpf = string.Empty;
        private string _CnpjOrgao = string.Empty;
        private string _unidade = string.Empty;
        private string _funcao = string.Empty;
        private List<bFuncinarioPerfil> _perfil = new List<bFuncinarioPerfil>();
        private int _qtdProcessosAtual = 0;

        private bool _podeDistribuirProcesso = false;


        #endregion

        #region Class Property Declarations

        public int SqFuncionario
        {
            get { return _sqFuncionario; }
            set { _sqFuncionario = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public String CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        public String CnpjOrgao
        {
            get { return _CnpjOrgao; }
            set { _CnpjOrgao = value; }
        }

        public string Unidade
        {
            get { return _unidade; }
            set { _unidade = value; }
        }
        public string Funcao
        {
            get { return _funcao; }
            set { _funcao = value; }
        }
        public List<bFuncinarioPerfil> Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }

        public int QtdProcessosAtual
        {
            get { return _qtdProcessosAtual; }
            set { _qtdProcessosAtual = value; }
        }

        public bool PodeDistribuirProcesso
        {
            get { return _podeDistribuirProcesso; }
            set { _podeDistribuirProcesso = value; }
        }

        #endregion

        public bFuncionario()
        {
        }

        public bFuncionario(string pCPF)
            : this()
        {
            _cpf = pCPF;
            Populate();
        }
        private void Populate()
        {

            if (_cpf == string.Empty)
                return;

            DataTable dt = dHelperORACLE.getFuncionarioSiarco(_cpf);

            foreach (DataRow dr in dt.Rows)
            {
                //"select f.sq_funcionario , f.co_sequencial Unidade , f.co_funcao, f.no_funcionario
                _sqFuncionario = Int32.Parse(dr["sq_funcionario"].ToString());
                _nome = dr["no_funcionario"].ToString();
                _cpf = dr["cpf"].ToString();
                _unidade = dr["unidade"].ToString();
                _funcao = dr["co_funcao"].ToString();
                getFiltroFuncionario();
            }

        }

        private void getFiltroFuncionario()
        {
            DataTable dt = dHelperORACLE.getFuncionarioFiltro(_cpf);

            foreach (DataRow dr in dt.Rows)
            {
                bFuncinarioPerfil obj = new bFuncinarioPerfil();
                obj.Tipo = Int32.Parse(dr["tff_tipo_filtro"].ToString());
                obj.Valor = dr["tff_valor"].ToString();
                _perfil.Add(obj);
            }
        }

        public DataTable getProcessos()
        {
            StringBuilder sqlWhere = new StringBuilder();
            bool podeDistribuir = false;
            foreach (bFuncinarioPerfil perf in _perfil)
            {
                if (perf.Tipo == 1) //Naturezas juridicas permitidas
                {
                    sqlWhere.AppendLine(" And NAT_JURIDICA in (" + perf.Valor + ")");
                }
                if (perf.Tipo == 2) //Atos permitidos
                {
                    sqlWhere.AppendLine(" And ATO IN (" + perf.Valor + ")");
                }
                if (perf.Tipo == 3) //Eventos Excludentes
                {
                    sqlWhere.AppendLine(" And EVENTO NOT IN (" + perf.Valor + ")");
                }

                if (perf.Tipo == 5)
                {
                    if (perf.Valor == "S")
                    {
                        _podeDistribuirProcesso = true;
                    }
                }

            }

            return dHelperQuery.getProcessosSecao(sqlWhere.ToString(), _podeDistribuirProcesso);
        }
        public int getQuantidadeProcessosPermitidos()
        {
            int resultado = 1;
            foreach (bFuncinarioPerfil perf in _perfil)
            {
                if (perf.Tipo == 4)//Quantidade de Processos
                {
                    try
                    {
                        resultado = Int32.Parse(perf.Valor);
                    }
                    catch
                    {
                        resultado = 1;
                    }
                }
            }
            return resultado;
        }

        public List<bProcessoCentral> ListProcessos()
        {
            StringBuilder sqlWhere = new StringBuilder();
            bool podeDistribuir = false;
            foreach (bFuncinarioPerfil perf in _perfil)
            {
                if (perf.Tipo == 1) //Naturezas juridicas permitidas
                {
                    sqlWhere.AppendLine(" And NAT_JURIDICA in (" + perf.Valor + ")");
                }
                if (perf.Tipo == 2) //Atos permitidos
                {
                    sqlWhere.AppendLine(" And ATO IN (" + perf.Valor + ")");
                }
                if (perf.Tipo == 3) //Eventos Excludentes
                {
                    sqlWhere.AppendLine(" And EVENTO NOT IN (" + perf.Valor + ")");
                }

                if (perf.Tipo == 5)
                {
                    if (perf.Valor == "S")
                    {
                        _podeDistribuirProcesso = true;
                    }
                }

            }

            DataTable Dt =  dHelperQuery.getProcessosSecao(sqlWhere.ToString(), false);

            //Pegar Filtro tipo 6 - Andamento permitidido
            string _secaoFuncionario = "";
            foreach(bFuncinarioPerfil obj in _perfil)
            {
                if(obj.Tipo == 6)
                {
                    _secaoFuncionario = obj.Valor;
                }
            }
            List<bProcessoCentral> _list = new List<bProcessoCentral>();

            DataTable dtAndamento = new DataTable();
            string _secaoProcesso = "";
            foreach (DataRow row in Dt.Rows)
            {
                if (_secaoFuncionario != "")
                {
                    dtAndamento = dHelperORACLE.GetUltimoAndamento(row["PROTOCOLO"].ToString());
                    if (dtAndamento.Rows.Count > 0)
                    {
                        _secaoProcesso = dtAndamento.Rows[0]["si_secao_destino"].ToString();
                    }

                }
                if (_secaoFuncionario == _secaoProcesso)
                {
                    bProcessoCentral o = new bProcessoCentral();
                    o.Protocolo = row["PROTOCOLO"].ToString();
                    o.Cod_Usuario = row["COD_USUARIO"].ToString();
                    o.Seq_Andamento = Int32.Parse(row["SEQ_ANDAMENTO"].ToString());
                    o.Dt_Entrada = DateTime.Parse(row["DT_ENTRADA"].ToString());
                    o.Dt_Entrada = DateTime.Parse(row["DT_SELECAO"].ToString());
                    o.Situacao = Int32.Parse(row["SITUACAO"].ToString());
                    o.Unidade = row["UNIDADE"].ToString();
                    o.Natureza = row["natureza"].ToString();
                    o.Ato = row["ATO"].ToString();
                    o.Nome = row["nome"].ToString();
                    o.Nome_unidade = row["NOME_UNIDADE"].ToString();
                    o.Desc_nat_juridica = row["DESC_NAT_JURIDICA"].ToString();
                    o.Desc_ato = row["DESC_ATO"].ToString();
                    o.Secao = _secaoProcesso;

                    _list.Add(o);

                    break;
                }
            }

            return _list;
        }
    }
}
