using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bFilial : IRequerimento
    {

        #region Class Member Declarations
        private string _ufSede = "";
        private string _nome;
        private string _cnpjOrgaoRegistro = "";
        private string _ProtocoloRequerimento = "";

        private string _codigoEvento = "";
        private int _SqFilial = 0;
        private int _sqEmpresa = 0;
        private string _FilialDBE;
        private string _FilialViabilidade = "";
        private string _FilialEvento;
        private String _FilialCEP;
        private String _FilialUF;
        private String _FilialUFOrigem;

        private int _FilialCodMunicipio;
        private String _FilialMunicipio;
        private String _FilialBairro;
        private String _FilialTipoLogradouro;
        private String _FilialLogradouro;
        private String _FilialNumero;
        private String _FilialComplemento;

        private List<bCNAE> _CNAEs = new List<bCNAE>();

        private decimal _FilialCapitalDestacado;
        private String _FilialOBJSocial;
        private string _Nire = String.Empty;
        private string _Cnpj = String.Empty;
        private int _Acao;
        private string _dsAcao = string.Empty;

        private int _FilialCnaeDestacado;
        private List<bProtocoloEvento> _bProtocoloEvento = new List<bProtocoloEvento>();
        private int _ordem = 0;
        private string _FilialNome;
        private Nullable<DateTime> _dataInicioAtividade;
        private string _t003_iptu = "";
        private decimal _t003_area_utilizada = 0;
        private string _ddd = "";
        private string _telefone = "";

        #endregion

        #region Enum
        public enum TipoEvento
        {
            Incorporacao = 1,
            AlteracaoEndereco = 2,
            AlteracaoCnae = 3,
            Exclusao = 5
        }

        #endregion

        #region Class Property Declarations
        public string UfSede
        {
            get { return _ufSede; }
            set { _ufSede = value; }
        }
        public string Requerimento
        {
            get { return _ProtocoloRequerimento; }
            set { _ProtocoloRequerimento = value; }
        }

        public int SqEmpresa
        {
            get { return _sqEmpresa; }
            set { _sqEmpresa = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        public int SqFilial
        {
            get { return _SqFilial; }
            set { _SqFilial = value; }
        }
        public string FilialNome
        {
            get { return _FilialNome; }
            set { _FilialNome = value; }
        }
        public string FilialDBE
        {
            get { return _FilialDBE; }
            set { _FilialDBE = value; }
        }
        public string FilialViabilidade
        {
            get { return _FilialViabilidade; }
            set { _FilialViabilidade = value; }
        }
        public string FilialEvento
        {
            get { return _FilialEvento; }
            set { _FilialEvento = value; }
        }
        public String FilialCEP
        {
            get { return _FilialCEP; }
            set { _FilialCEP = value; }
        }
        public String FilialUF
        {
            get { return _FilialUF; }
            set { _FilialUF = value; }
        }
        public String FilialUFOrigem
        {
            get { return _FilialUFOrigem; }
            set { _FilialUFOrigem = value; }
        }
        public int FilialCodMunicipio
        {
            get { return _FilialCodMunicipio; }
            set { _FilialCodMunicipio = value; }
        }
        public String FilialMunicipio
        {
            get { return _FilialMunicipio; }
            set { _FilialMunicipio = value; }
        }
        public String FilialBairro
        {
            get { return _FilialBairro; }
            set { _FilialBairro = value; }
        }
        public String FilialTipoLogradouro
        {
            get { return _FilialTipoLogradouro; }
            set { _FilialTipoLogradouro = value; }
        }
        public String FilialLogradouro
        {
            get { return _FilialLogradouro; }
            set { _FilialLogradouro = value; }
        }
        public String FilialNumero
        {
            get { return _FilialNumero; }
            set { _FilialNumero = value; }
        }
        public String FilialComplemento
        {
            get { return _FilialComplemento; }
            set { _FilialComplemento = value; }
        }
        public List<bCNAE> CNAEs
        {
            get { return (List<bCNAE>)_CNAEs; }
            set { _CNAEs = value; }
        }
        public String FilialOBJSocial
        {
            get { return _FilialOBJSocial; }
            set { _FilialOBJSocial = value; }
        }
        public decimal FilialCapitalDestacado
        {
            get { return _FilialCapitalDestacado; }
            set { _FilialCapitalDestacado = value; }
        }
        public string Nire
        {
            get { return _Nire; }
            set { _Nire = value; }
        }
        public string Cnpj
        {
            get { return _Cnpj; }
            set { _Cnpj = value; }
        }
        public int Acao
        {
            get { return _Acao; }
            set { _Acao = value; }
        }
        public string DescrAcao
        {
            get { return _dsAcao; }
            set { _dsAcao = value; }
        }
        public int FilialCnaeDestacado
        {
            get { return _FilialCnaeDestacado; }
            set { _FilialCnaeDestacado = value; }
        }
        public string TipoAcaoDescricao
        {
            get
            {
                if (_Acao == 1)
                {
                    return "Abertura de Filial";
                }
                if (_Acao == 3)
                {
                    return "Alteração de Filial";
                }
                if (_Acao == 4)
                {
                    return "Transferencia de Filial";
                }
                if (_Acao == 5)
                {
                    return "Extinção de Filial";
                }
                return "Ação não definida";
            }
        }
        public List<bProtocoloEvento> ProtocoloEvento
        {
            get { return (List<bProtocoloEvento>)_bProtocoloEvento; }
            set { _bProtocoloEvento = value; }
        }

        public int Ordem
        {
            get { return _ordem; }
            set { _ordem = value; }
        }
        public Nullable<DateTime> DataInicioAtividade
        {
            get { return _dataInicioAtividade; }
            set { _dataInicioAtividade = value; }
        }
        public string CodigoEvento
        {
            get { return _codigoEvento; }
            set { _codigoEvento = value; }
        }
        public string IPTU
        {
            get { return _t003_iptu; }
            set { _t003_iptu = value; }
        }

        public decimal AreaUtilizada
        {
            get { return _t003_area_utilizada; }
            set { _t003_area_utilizada = value; }
        }
        public string DDD
        {
            get { return _ddd; }
            set { _ddd = value; }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        #endregion

        
        #region Update metodos

        public void Update()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    using (dT001_Pessoa fp = new dT001_Pessoa())
                    {
                        fp.MainConnectionProvider = cp;
                        fp.t001_in_tipo_pessoa = "J";
                        fp.t001_ds_pessoa = _nome;
                        fp.t001_in_dados_atualizados = "S";
                        fp.t001_dt_ult_atualizacao = DateTime.Now;
                        fp.t001_email = "";
                        fp.t001_ddd = _ddd;
                        fp.t001_tel_1 = _telefone;
                        fp.t001_tel_2 = "";
                        fp.t001_sq_pessoa = _SqFilial;

                        _SqFilial = fp.Update();


                    }
                    using (dT003_Pessoa_Juridica fpj = new dT003_Pessoa_Juridica())
                    {
                        fpj.MainConnectionProvider = cp;
                        fpj.t001_sq_pessoa = _SqFilial;
                        fpj.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;
                        fpj.t003_nr_matricula = _Nire;
                        fpj.t003_nr_cnpj = _Cnpj;
                        fpj.t003_DBE = _FilialDBE;
                        fpj.t003_prot_viab = _FilialViabilidade;
                        fpj.t003_vl_capital_social = _FilialCapitalDestacado;
                        fpj.T003_IND_CNAE_DESTACADA = _FilialCnaeDestacado;
                        fpj.t003_ds_objeto_social = _FilialOBJSocial;
                        fpj.t003_dt_inicio_atividade = _dataInicioAtividade;
                        fpj.T003_uf_origem = _FilialUFOrigem;
                        fpj.T003_iptu = _t003_iptu;
                        fpj.T003_area_utilizada = _t003_area_utilizada;
                        
                        fpj.Update();
                    }

                    //3.Gravando Vinculo Endereço
                    //(dados do endereço da Empresa)
                    using (dR002_Vinculo_Endereco fve = new dR002_Vinculo_Endereco())
                    {
                        int SqVinculoEnderecoFilial;
                        fve.MainConnectionProvider = cp;

                        fve.t001_sq_pessoa = _SqFilial;
                        fve.t001_sq_pessoa_pai = SqEmpresa;

                        fve.a015_co_tipo_logradouro = 0;
                        fve.a015_ds_tipo_logradouro = _FilialTipoLogradouro;
                        fve.r002_ds_logradouro = _FilialLogradouro;
                        fve.r002_nr_logradouro = _FilialNumero;
                        fve.r002_ds_complemento = _FilialComplemento;
                        fve.r002_ds_bairro = _FilialBairro;
                        fve.a004_co_pais = 154;
                        fve.a005_co_municipio = _FilialCodMunicipio;
                        fve.r002_nr_cep = _FilialCEP;
                        fve.R002_uf = _FilialUF;

                        SqVinculoEnderecoFilial = fve.Update();
                    }
                    //Gravando Vinculo com a PJ (Vinculo Filial)
                    using (dR001_Vinculo fv = new dR001_Vinculo())
                    {
                        fv.MainConnectionProvider = cp;
                        fv.t001_sq_pessoa = _SqFilial;
                        fv.t001_sq_pessoa_pai = SqEmpresa;
                        fv.a009_co_condicao = 501;
                        fv.r001_dt_entrada_vinculo = DateTime.Now;
                        fv.r001_ds_cargo_direcao = "FILIAL";
                        fv.r001_in_situacao = "A";
                        fv.r001_acao = _Acao;
                        fv.T001_cpf_cnpj_pessoa = _Cnpj == "" ? _SqFilial.ToString() : _Cnpj;
                        fv.Update();
                    }

                    //8.Gravando CNAE
                    //(Codigo de Atividade Economica do Requerimento)

                    using (dR004_Atuacao fa = new dR004_Atuacao())
                    {
                        fa.MainConnectionProvider = cp;
                        foreach (bCNAE fc in _CNAEs)
                        {
                            fa.t001_sq_pessoa = _SqFilial;
                            fa.a001_co_atividade = fc.CodigoCNAE;
                            fa.r004_in_principal_secundario = fc.TipoAtividade.ToString();
                            fa.r004_exercida = fc.Exercida;
                            fa.Update();
                        }
                    }

                    #region Gravando Ato e Evento RFB

                    using (dR005_Protocolo_Evento pe = new dR005_Protocolo_Evento())
                    {
                        pe.MainConnectionProvider = cp;
                        foreach (bProtocoloEvento a in _bProtocoloEvento)
                        {
                            pe.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;
                            pe.t007_nr_protocolo = _ProtocoloRequerimento;
                            pe.a003_co_evento = a.CodigoEvento;
                            pe.t001_sq_pessoa = _SqFilial;
                            pe.Update();
                        }
                    }
                    #endregion

                    #region Gravando Evento Junta

                    using (dR015_Evento_Filial pe = new dR015_Evento_Filial())
                    {
                        pe.MainConnectionProvider = cp;

                        pe.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;
                        pe.t005_nr_protocolo = _ProtocoloRequerimento;
                        pe.a003_co_evento = GetEventoJunta();
                        pe.t001_sq_pessoa = _SqFilial;
                        pe.Update();

                    }
                    #endregion

                    cp.CommitTransaction();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public void Delete()
        {
            //_SqFilial
            //sqEmpresa
            using (ConnectionProvider cv = new ConnectionProvider())
            {
                cv.OpenConnection();
                cv.BeginTransaction();

                using (dR005_Protocolo_Evento ev = new dR005_Protocolo_Evento())
                {
                    ev.MainConnectionProvider = cv;
                    ev.Deleta(_SqFilial);
                }

                using (dR015_Evento_Filial ev = new dR015_Evento_Filial())
                {
                    ev.MainConnectionProvider = cv;
                    ev.Delete(_SqFilial);
                }

                using (dR001_Vinculo v = new dR001_Vinculo())
                {
                    v.MainConnectionProvider = cv;
                    v.Deleta(_SqFilial, _sqEmpresa, 0);
                }

                using (dR004_Atuacao a = new dR004_Atuacao())
                {
                    a.MainConnectionProvider = cv;
                    a.Deleta(_SqFilial);
                }
                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cv;
                    pj.Deleta(_SqFilial);
                }

                using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                {
                    ve.MainConnectionProvider = cv;
                    ve.Deleta(_SqFilial);
                }
                using (dT001_Pessoa p = new dT001_Pessoa())
                {
                    p.MainConnectionProvider = cv;
                    p.Deleta(_SqFilial);
                }

                

                cv.CommitTransaction();
            }

        }

        #endregion

        #region Public Methods
        public bool BuscaTipoEvento(TipoEvento tipo)
        {
            bool ret = false;
            foreach (bProtocoloEvento ev in _bProtocoloEvento)
            {
                if (tipo == TipoEvento.AlteracaoEndereco)
                {
                    switch (Int32.Parse(ev.CodigoEvento.ToString()))
                    {
                        case 101:
                            ret = true;
                            break;
                        case 102:
                            ret = true;
                            break;
                    }
                }

                if (tipo == TipoEvento.AlteracaoEndereco)
                {
                    switch (Int32.Parse(ev.CodigoEvento.ToString()))
                    {
                        case 209:
                            ret = true;
                            break;
                        case 210:
                            ret = true;
                            break;
                        case 211:
                            ret = true;
                            break;
                    }

                }
                if (tipo == TipoEvento.AlteracaoCnae)
                {
                    if (ev.CodigoEvento == 244)
                    {
                        ret = true;
                    }
                }
                if (tipo == TipoEvento.AlteracaoCnae)
                {
                    if (ev.CodigoEvento == 604)
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }

        public string getEnderecoEmpresa()
        {
            string _ende = "";
            _ende = _FilialTipoLogradouro.ToUpper() + " " + _FilialLogradouro.ToString().ToUpper() + ", ";
            _ende += _FilialNumero.ToUpper() + ((_FilialComplemento.ToUpper() != String.Empty) ? (", " + _FilialComplemento.ToUpper()) : " ");
            _ende += ", " + _FilialBairro.ToUpper() + ", " + _FilialMunicipio.ToUpper() + ", ";
            _ende += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(_FilialCEP)) + " " + _FilialUF.ToUpper();
            return _ende;
        }

        #endregion

        #region Functios
        private string NomeCaixaBaixa(string value)
        {
            string Retorno = string.Empty;
            if (string.IsNullOrEmpty(value))
                return "";
            string[] wValue = value.Split(' ');
            for (int i = 0; i < wValue.Length; i++)
            {
                string aux = wValue[i].ToLower();
                if (aux.Length <= 3)
                {
                    if (aux == "da" || aux == "de" || aux == "do" || aux == "das" || aux == "dos" || aux == "e")
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = aux;
                        else
                            Retorno += " " + aux;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = NomeProprio(aux);
                        else
                            Retorno += " " + NomeProprio(aux);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Retorno))
                        Retorno = NomeProprio(aux);
                    else
                        Retorno += " " + NomeProprio(aux);
                }
            }
            return Retorno;

        }
        private string NomeProprio(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Length == 0) return value;
            value = value.ToLower();
            System.Text.StringBuilder result = new System.Text.StringBuilder(value);
            result[0] = char.ToUpper(result[0]);

            for (int i = 1; i < result.Length; ++i)
            {

                if (char.IsWhiteSpace(result[i - 1]))
                {
                    result[i] = char.ToUpper(result[i]);
                }
            }
            return result.ToString();
        }

        #endregion

        private void AtualizaCane()
        {
            _CNAEs.Clear();

            DataTable dtCnae = dHelperQuery.CarregaCnaeComProtocolo(_SqFilial);
            foreach (DataRow rf in dtCnae.Rows)
            {
                bCNAE c = new bCNAE();
                c.CodigoCNAE = rf["A001_CO_ATIVIDADE"].ToString();
                c.Descricao = rf["TAD_DESC_ATIVIDADE"].ToString();
                c.TipoAtividade = decimal.Parse(rf["R004_IN_PRINCIPAL_SECUNDARIO"].ToString());
                c.Exercida = "1";
                _CNAEs.Add(c);
            }

        }

        private string GetEventoJunta()
        {

            //protocolo	      evento	UF	sq Ppessoa	sq Ppessoa Pai	UF_Origem
            //81300000003574	102	    MT	10743	        10740	    (null)
            //81300000003574	102	    MT	10744	        10740	    (null)

            string ret = "";
            bOrgaoRegistro orgaoRegistro = new bOrgaoRegistro(_cnpjOrgaoRegistro);

            foreach (bProtocoloEvento a in _bProtocoloEvento)
            {
                switch (a.CodigoEvento.ToString())
                {
                    case "102":
                        if (_ufSede == orgaoRegistro.uf) //Matriz está na UF da JUNTA
                        {
                            if (_FilialUF == orgaoRegistro.uf)
                            {
                                //"Abertura de Filial na UF da Sede";
                               ret = "023";
                            }
                            else
                            {
                                //"Abertura de Filial em outra UF";
                                ret = "026";
                            }
                        }
                        else
                        {
                            //"Abertura de Filial com Sede em outra UF";
                            ret = "029";
                        }

                        break;
                    case "210":

                        //Transferencia de Filial de outra UF
                        if (_ufSede != orgaoRegistro.uf)
                        {
                            //"Inscrição de Transferência de Filial de outra UF";
                            ret = "037";
                        }
                        else
                        {
                            if (_FilialUF == orgaoRegistro.uf)
                            {
                                //"Alteração de Filial na UF da Sede";
                                ret = "024";
                            }
                            else
                            {
                                //"Alteração de Filial em outra UF";
                                ret = "027";
                            }
                        }
                        break;
                    case "211":

                        //UF da sede <> Uf da JUNTA
                        if (_ufSede != orgaoRegistro.uf)
                        {
                            //"Alteração de Filial com Sede em outra UF";
                            ret = "030";
                        }
                        else
                        {
                            if (_FilialUF == orgaoRegistro.uf)
                            {
                                //"Alteração de Filial na UF da Sede";
                                ret = "024";
                            }
                            else
                            {
                                //"Alteração de Filial em outra UF";
                                ret = "027";
                            }
                        }
                        break;
                    case "209":
                        //UF da sede <> Uf da JUNTA
                        if (_ufSede != orgaoRegistro.uf)
                        {
                            ret = "030";
                            //"Alteração de Filial com Sede em outra UF";
                        }
                        else
                        {
                            if (_FilialUF == orgaoRegistro.uf)
                            {
                                ret = "024";
                                //"Alteração de Filial na UF da Sede";
                            }
                            else
                            {
                                ret = "027";
                                //"Alteração de Filial em outra UF";
                            }
                        }
                        break;
                    case "244":
                        //024	- ALTERAÇÃO DE FILIAL NA UF DA SEDE
                        if (_FilialUF == orgaoRegistro.uf)
                        {
                            ret = "024";
                            //"Alteração de Filial na UF da Sede";
                        }
                        //027	- ALTERAÇÃO DE FILIAL EM OUTRA UF
                        if (_FilialUF != orgaoRegistro.uf)
                        {
                            ret = "030";
                            //"Alteração de Filial em outra UF";
                        }
                        //030	- ALTERAÇÃO DE FILIAL COM SEDE EM OUTRA UF
                        if (_FilialUF != T005_uf_origem)
                        {
                            ret = "030";
                            //"Alteração de Filial com Sede em outra UF";
                        }
                        break;
                    case "517":
                        if (_FilialUF == orgaoRegistro.uf)
                        {
                            ret = "025";
                            //"Extinção de Filial na UF da Sede";
                        }
                        else
                        {
                            ret = "028";
                            //"Extinção de Filial em outra UF";

                        }
                        break;
                }

            }

            return ret;
        }
    }
}
