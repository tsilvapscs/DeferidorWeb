using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.Data;


namespace RCPJ.BLL
{
    [Serializable]
    public class bContratoPadrao
    {
        private int _codigo;
        private int _editavel = 0;

        private string _protocoloRequerimento;
        private List<bContratoConteudo> _listClausula = new List<bContratoConteudo>();
        private List<bContratoCampo> _listCampo = new List<bContratoCampo>();
        private List<bContratoAssinatura> _listAssinatura = new List<bContratoAssinatura>();
        private List<bContratoAssinatura> _listTestemunhas = new List<bContratoAssinatura>();

        public List<bContratoAssinatura> ListTestemunhas
        {
            get { return _listTestemunhas; }
            set { _listTestemunhas = value; }
        }

        public List<bContratoAssinatura> ListAssinatura
        {
            get { return _listAssinatura; }
            set { _listAssinatura = value; }
        }

        public string ProtocoloRequerimento
        {
            get { return _protocoloRequerimento; }
            set { _protocoloRequerimento = value; }
        }
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public int Editavel
        {
            get { return _editavel; }
            set { _editavel = value; }
        }
        public List<bContratoConteudo> ListClausula
        {
            get { return _listClausula; }
            set { _listClausula = value; }
        }

        public List<bContratoCampo> ListCampo
        {
            get { return _listCampo; }
            set { _listCampo = value; }
        }

        #region Constructors
        public bContratoPadrao()
        {
            
        }

        public bContratoPadrao(int codigo)
            : this()
        {
            _codigo = codigo;
            Populate();
        }
        public bContratoPadrao(int codigo, string requerimento)
            : this()
        {
            _codigo = codigo;
            _protocoloRequerimento = requerimento;
            Populate();
        }
        private void Populate()
        {
            //clausulas e paragrafos
            DataTable dtContrato = Query();
            _editavel = Int32.Parse(dtContrato.Rows[0]["t030_in_editavel"].ToString());
            int _seqClausulaMae = -10;

            DataTable dt = QueryClausulaConteudo(_codigo);

            foreach (DataRow dr in dt.Rows)
            {
                
                bContratoConteudo c = new bContratoConteudo();
                
                

                if (Int32.Parse(dr["t031_fixa_final"].ToString()) == 2)
                {
                    if (_seqClausulaMae == -10)
                        _seqClausulaMae = Int32.Parse(dr["t031_sq_clausula"].ToString());

                    if(_seqClausulaMae != Int32.Parse(dr["t031_sq_clausula_mae"].ToString()))
                    {
                        //Se a clausula mae é difeente é porque mudou de clausula e acabou os paragrafos fixos
                        //Adiciono os paragrafos gravados

                        bContratoParagrafo o = new bContratoParagrafo();

                        o.T005_nr_requerimento = _protocoloRequerimento;
                        o.T031_sq_clausula = _seqClausulaMae; // c.T031_sq_clausula;
                        o.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                        DataTable dtP = o.Query();
                        foreach (DataRow row in dtP.Rows)
                        {
                            bContratoConteudo cp = new bContratoConteudo();

                            cp.T030_id_contrato = Int32.Parse(row["t030_id_contrato"].ToString());
                            cp.T031_sq_clausula = Int32.Parse(row["t033_id_paragrafo"].ToString());
                            cp.T031_texto_clausula = row["t033_texto_paragrafo"].ToString();
                            cp.T031_texto_original = row["t033_texto_paragrafo"].ToString();
                            cp.T031_sq_clausula_mae = Int32.Parse(row["t031_sq_clausula"].ToString());
                            cp.T031_texto_editavel = 1;
                            _listClausula.Add(cp);
                        }

                        _seqClausulaMae = Int32.Parse(dr["t031_sq_clausula"].ToString());
                    }

                    c.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                    c.T031_sq_clausula = Int32.Parse(dr["t031_sq_clausula"].ToString());
                    c.T031_texto_clausula = dr["t031_texto_clausula"].ToString();
                    c.T031_texto_original = dr["t031_texto_clausula"].ToString();
                    c.T031_sq_clausula_mae = Int32.Parse(dr["t031_sq_clausula_mae"].ToString());
                    c.T031_texto_editavel = Int32.Parse(dr["t031_texto_editavel"].ToString());
                    c.T031_adiciona_paragrafo = Int32.Parse(dr["t031_adiciona_paragrafo"].ToString());
                    c.T031_fixa_final = Int32.Parse(dr["t031_fixa_final"].ToString());
                    c.T031_in_numeracao = Int32.Parse(dr["T031_in_numeracao"].ToString());
                    c.Campos = GetCamposClausula(c.T030_id_contrato, c.T031_sq_clausula);


                    _listClausula.Add(c);

                    

                    //Adicionando os paragrafos gravados
                    // so posso adicionar depois que verificar se a clausula possui paragrafos padrao
                    //bContratoParagrafo o = new bContratoParagrafo();

                    //o.T005_nr_requerimento = _protocoloRequerimento;
                    //o.T031_sq_clausula = c.T031_sq_clausula;
                    //o.T030_id_contrato = c.T030_id_contrato;
                    //DataTable dtP = o.Query();
                    //foreach (DataRow row in dtP.Rows)
                    //{
                    //    bContratoConteudo cp = new bContratoConteudo();

                    //    cp.T030_id_contrato = Int32.Parse(row["t030_id_contrato"].ToString());
                    //    cp.T031_sq_clausula = Int32.Parse(row["t033_id_paragrafo"].ToString());
                    //    cp.T031_texto_clausula = row["t033_texto_paragrafo"].ToString();
                    //    cp.T031_texto_original = row["t033_texto_paragrafo"].ToString();
                    //    cp.T031_sq_clausula_mae = Int32.Parse(row["t031_sq_clausula"].ToString());
                    //    cp.T031_texto_editavel = 1;
                    //    _listClausula.Add(cp);
                    //}

                    
                }
            }

            //campos Perguntas
            DataTable dtCampos = QueryClausulaQuestoes(_codigo);
            foreach (DataRow dr in dtCampos.Rows)
            {
                bContratoCampo c = new bContratoCampo();

                c.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                c.T031_sq_clausula = Int32.Parse(dr["t031_sq_clausula"].ToString());
                c.T032_sq_campo = Int32.Parse(dr["t032_sq_campo"].ToString());
                c.T032_nome_campo = dr["t032_nome_campo"].ToString();
                c.T032_origem_campo = Int32.Parse(dr["t032_origem_campo"].ToString());
                c.T032_tipo_campo = Int32.Parse(dr["t032_tipo_campo"].ToString());
                c.T032_descricao_campo = dr["t032_descricao_campo"].ToString();
                c.T032_titulo_campo = dr["T032_titulo_campo"].ToString();
                c.Resposta_campo = bContratoPergunta.GetValorCampo(_protocoloRequerimento, c.T032_sq_campo);
                c.T032_sq_campo_depende = Int32.Parse(dr["T032_sq_campo_depende"].ToString());
                _listCampo.Add(c);
            }

            //Caluslas Adicionais
            DataTable dtCA = QueryClausulaAdicional();
            foreach (DataRow dr in dtCA.Rows)
            {
                bContratoConteudo ca = new bContratoConteudo();

                ca.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                ca.T031_sq_clausula = Int32.Parse(dr["t035_sq_clausula"].ToString());
                ca.T031_texto_clausula = dr["t035_texto"].ToString();
                ca.T031_texto_original = dr["t035_texto"].ToString();
                ca.T031_sq_clausula_mae = -1;
                ca.T031_texto_editavel = 1;
                ca.T031_adiciona_paragrafo = 1;
                ca.T031_fixa_final = 2;
                ca.T031_in_numeracao = 1;
                ca.T031_in_adicional = 1;
                _listClausula.Add(ca);


                //Adicionando os paragrafos gravados
                bContratoParagrafo o = new bContratoParagrafo();

                o.T005_nr_requerimento = _protocoloRequerimento;
                o.T031_sq_clausula = ca.T031_sq_clausula;
                o.T030_id_contrato = 0;
                DataTable dtP = o.Query();
                foreach (DataRow row in dtP.Rows)
                {
                    bContratoConteudo cp = new bContratoConteudo();

                    cp.T030_id_contrato = Int32.Parse(row["t030_id_contrato"].ToString());
                    cp.T031_sq_clausula = Int32.Parse(row["t033_id_paragrafo"].ToString());
                    cp.T031_texto_clausula = row["t033_texto_paragrafo"].ToString();
                    cp.T031_texto_original = row["t033_texto_paragrafo"].ToString();
                    cp.T031_sq_clausula_mae = Int32.Parse(row["t031_sq_clausula"].ToString());
                    cp.T031_texto_editavel = 1;
                    _listClausula.Add(cp);
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                bContratoConteudo c = new bContratoConteudo();

                if (Int32.Parse(dr["t031_fixa_final"].ToString()) == 1)
                {
                    c.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                    c.T031_sq_clausula = Int32.Parse(dr["t031_sq_clausula"].ToString());
                    c.T031_texto_clausula = dr["t031_texto_clausula"].ToString();
                    c.T031_texto_original = dr["t031_texto_clausula"].ToString();
                    c.T031_sq_clausula_mae = Int32.Parse(dr["t031_sq_clausula_mae"].ToString());
                    c.T031_texto_editavel = Int32.Parse(dr["t031_texto_editavel"].ToString());
                    c.T031_adiciona_paragrafo = Int32.Parse(dr["t031_adiciona_paragrafo"].ToString());
                    c.T031_fixa_final = Int32.Parse(dr["t031_fixa_final"].ToString());
                    c.T031_in_numeracao = Int32.Parse(dr["T031_in_numeracao"].ToString());
                    c.Campos = GetCamposClausula(c.T030_id_contrato, c.T031_sq_clausula);


                    _listClausula.Add(c);

                    //Adicionando os paragrafos gravados
                    bContratoParagrafo o = new bContratoParagrafo();

                    o.T005_nr_requerimento = _protocoloRequerimento;
                    o.T031_sq_clausula = c.T031_sq_clausula;
                    o.T030_id_contrato = _codigo;
                    DataTable dtP = o.Query();
                    foreach (DataRow row in dtP.Rows)
                    {
                        bContratoConteudo cp = new bContratoConteudo();

                        cp.T030_id_contrato = Int32.Parse(row["t030_id_contrato"].ToString());
                        cp.T031_sq_clausula = Int32.Parse(row["t033_id_paragrafo"].ToString());
                        cp.T031_texto_clausula = row["t033_texto_paragrafo"].ToString();
                        cp.T031_texto_original = row["t033_texto_paragrafo"].ToString();
                        cp.T031_sq_clausula_mae = Int32.Parse(row["t031_sq_clausula"].ToString());
                        cp.T031_texto_editavel = 1;
                        _listClausula.Add(cp);
                    }
                }
            }
        }

        #endregion

        public bool ContratoEditado()
        {
            foreach (bContratoConteudo cont in _listClausula)
            {
                if (cont.T031_sq_clausula >= 1000)
                {
                    return true;
                }
            }

            return false;
        }
        private List<bContratoCampo> GetCamposClausula(int id_contrato, int id_clausula)
        {
            List<bContratoCampo> _campos = new List<bContratoCampo>();

            DataTable dt = QueryClausulaCamposByClausula(id_contrato, id_clausula);

            foreach (DataRow dr in dt.Rows)
            {
                bContratoCampo c = new bContratoCampo();

                c.T030_id_contrato = Int32.Parse(dr["t030_id_contrato"].ToString());
                c.T031_sq_clausula = Int32.Parse(dr["t031_sq_clausula"].ToString());
                c.T032_sq_campo = Int32.Parse(dr["t032_sq_campo"].ToString());
                c.T032_nome_campo = dr["t032_nome_campo"].ToString();
                c.T032_origem_campo = Int32.Parse(dr["t032_origem_campo"].ToString());
                c.T032_tipo_campo = Int32.Parse(dr["t032_tipo_campo"].ToString());
                c.T032_descricao_campo = dr["t032_descricao_campo"].ToString();
                c.T032_titulo_campo = dr["T032_titulo_campo"].ToString();

                _campos.Add(c);
            }
            return _campos;
        }

        public DataTable QueryClausulaConteudo(int id_contrato)
        {
            using (dContratoPadrao dal = new dContratoPadrao())
            {
                return dal.QueryClausulaConteudo(id_contrato);
            }
        }

        public DataTable QueryClausulaQuestoes(int id_contrato)
        {
            using (dContratoPadrao dal = new dContratoPadrao())
            {
                return dal.QueryClausulaCampos(id_contrato);
            }
        }

        public static DataTable GetTipoContrato(string _ato, string _codNatureza)
        {
            using (dContratoPadrao dal = new dContratoPadrao())
            {
                return dal.QueryTipoContrato(_ato, _codNatureza);
            }
        }

        private DataTable Query()
        {
            using (dContratoPadrao dal = new dContratoPadrao())
            {
                return dal.Query(_codigo);
            }
        }

        public DataTable QueryClausulaCamposByClausula(int id_contrato, int id_clausula)
        {
            using (dContratoPadrao dal = new dContratoPadrao())
            {
                return dal.QueryClausulaCamposByClausula(id_contrato, id_clausula);
            }
        }

        public DataTable QueryClausulaAdicional()
        {
            using (dContratoClausula c = new dContratoClausula())
            {
                c.T005_nr_requerimento = _protocoloRequerimento;

                if (_codigo.Equals(1) || _codigo.Equals(2) || _codigo.Equals(5))
                    c.T030_id_contrato = _codigo;
                return c.Query();
            }
        }
    }
}
