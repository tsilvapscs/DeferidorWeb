using System;
using System.Collections.Generic;
using System.Text;
//using System.Threading.Tasks;

namespace RCPJ.BLL
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class coleta
    {

        private coletaDadosGerais dadosGeraisField;

        private coletaGrupoEvento grupoEventoField;

        private coletaIdentificacaoEmpresa identificacaoEmpresaField;

        private coletaEnderecoEmpresa enderecoEmpresaField;

        private coletaAtuacaoEmpresa atuacaoEmpresaField;

        private coletaSocioFundadorDiretor[] grupoQSAField;

        private object grupoFilialField;

        private coletaContador contadorField;

        /// <remarks/>
        public coletaDadosGerais dadosGerais
        {
            get
            {
                return this.dadosGeraisField;
            }
            set
            {
                this.dadosGeraisField = value;
            }
        }

        /// <remarks/>
        public coletaGrupoEvento GrupoEvento
        {
            get
            {
                return this.grupoEventoField;
            }
            set
            {
                this.grupoEventoField = value;
            }
        }

        /// <remarks/>
        public coletaIdentificacaoEmpresa IdentificacaoEmpresa
        {
            get
            {
                return this.identificacaoEmpresaField;
            }
            set
            {
                this.identificacaoEmpresaField = value;
            }
        }

        /// <remarks/>
        public coletaEnderecoEmpresa EnderecoEmpresa
        {
            get
            {
                return this.enderecoEmpresaField;
            }
            set
            {
                this.enderecoEmpresaField = value;
            }
        }

        /// <remarks/>
        public coletaAtuacaoEmpresa AtuacaoEmpresa
        {
            get
            {
                return this.atuacaoEmpresaField;
            }
            set
            {
                this.atuacaoEmpresaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("socioFundadorDiretor", IsNullable = false)]
        public coletaSocioFundadorDiretor[] GrupoQSA
        {
            get
            {
                return this.grupoQSAField;
            }
            set
            {
                this.grupoQSAField = value;
            }
        }

        /// <remarks/>
        public object GrupoFilial
        {
            get
            {
                return this.grupoFilialField;
            }
            set
            {
                this.grupoFilialField = value;
            }
        }

        /// <remarks/>
        public coletaContador Contador
        {
            get
            {
                return this.contadorField;
            }
            set
            {
                this.contadorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaDadosGerais
    {

        private ulong cnpjOrgaoRegistroField;

        private string dataProtocoloField;

        private string ufField;

        private ulong protocoloORField;

        private ulong protocoloViabilidadeField;

        private string nrDbeField;

        private object cnpjEmpresaField;

        private byte versaoField;

        private byte qtdEmpresasField;

        private byte qtdFiliaisNoEstadoField;

        private byte qtdFiliaisForaDoEstadoField;

        /// <remarks/>
        public ulong cnpjOrgaoRegistro
        {
            get
            {
                return this.cnpjOrgaoRegistroField;
            }
            set
            {
                this.cnpjOrgaoRegistroField = value;
            }
        }

        /// <remarks/>
        public string dataProtocolo
        {
            get
            {
                return this.dataProtocoloField;
            }
            set
            {
                this.dataProtocoloField = value;
            }
        }

        /// <remarks/>
        public string uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public ulong ProtocoloOR
        {
            get
            {
                return this.protocoloORField;
            }
            set
            {
                this.protocoloORField = value;
            }
        }

        /// <remarks/>
        public ulong ProtocoloViabilidade
        {
            get
            {
                return this.protocoloViabilidadeField;
            }
            set
            {
                this.protocoloViabilidadeField = value;
            }
        }

        /// <remarks/>
        public string NrDbe
        {
            get
            {
                return this.nrDbeField;
            }
            set
            {
                this.nrDbeField = value;
            }
        }

        /// <remarks/>
        public object CnpjEmpresa
        {
            get
            {
                return this.cnpjEmpresaField;
            }
            set
            {
                this.cnpjEmpresaField = value;
            }
        }

        /// <remarks/>
        public byte Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }

        /// <remarks/>
        public byte qtdEmpresas
        {
            get
            {
                return this.qtdEmpresasField;
            }
            set
            {
                this.qtdEmpresasField = value;
            }
        }

        /// <remarks/>
        public byte qtdFiliaisNoEstado
        {
            get
            {
                return this.qtdFiliaisNoEstadoField;
            }
            set
            {
                this.qtdFiliaisNoEstadoField = value;
            }
        }

        /// <remarks/>
        public byte qtdFiliaisForaDoEstado
        {
            get
            {
                return this.qtdFiliaisForaDoEstadoField;
            }
            set
            {
                this.qtdFiliaisForaDoEstadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaGrupoEvento
    {

        private byte eventoField;

        /// <remarks/>
        public byte evento
        {
            get
            {
                return this.eventoField;
            }
            set
            {
                this.eventoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaIdentificacaoEmpresa
    {

        private object nireMatriculaField;

        private string nomeEmpresarialField;

        private string nomeFantasiaField;

        private string naturezaJuridicaField;

        private object dataInicioAtividadeField;

        private uint dataAssinaturaField;

        private uint capitalSocialField;

        private byte indicaIntegralizaCapitalField;

        private uint capitalIntegralizadoField;

        private string porteField;

        private byte dddField;

        private uint telefone1Field;

        private object telefone2Field;

        private string emailField;

        /// <remarks/>
        public object NireMatricula
        {
            get
            {
                return this.nireMatriculaField;
            }
            set
            {
                this.nireMatriculaField = value;
            }
        }

        /// <remarks/>
        public string nomeEmpresarial
        {
            get
            {
                return this.nomeEmpresarialField;
            }
            set
            {
                this.nomeEmpresarialField = value;
            }
        }

        /// <remarks/>
        public string nomeFantasia
        {
            get
            {
                return this.nomeFantasiaField;
            }
            set
            {
                this.nomeFantasiaField = value;
            }
        }

        /// <remarks/>
        public string naturezaJuridica
        {
            get
            {
                return this.naturezaJuridicaField;
            }
            set
            {
                this.naturezaJuridicaField = value;
            }
        }

        /// <remarks/>
        public object dataInicioAtividade
        {
            get
            {
                return this.dataInicioAtividadeField;
            }
            set
            {
                this.dataInicioAtividadeField = value;
            }
        }

        /// <remarks/>
        public uint dataAssinatura
        {
            get
            {
                return this.dataAssinaturaField;
            }
            set
            {
                this.dataAssinaturaField = value;
            }
        }

        /// <remarks/>
        public uint capitalSocial
        {
            get
            {
                return this.capitalSocialField;
            }
            set
            {
                this.capitalSocialField = value;
            }
        }

        /// <remarks/>
        public byte indicaIntegralizaCapital
        {
            get
            {
                return this.indicaIntegralizaCapitalField;
            }
            set
            {
                this.indicaIntegralizaCapitalField = value;
            }
        }

        /// <remarks/>
        public uint capitalIntegralizado
        {
            get
            {
                return this.capitalIntegralizadoField;
            }
            set
            {
                this.capitalIntegralizadoField = value;
            }
        }

        /// <remarks/>
        public string porte
        {
            get
            {
                return this.porteField;
            }
            set
            {
                this.porteField = value;
            }
        }

        /// <remarks/>
        public byte ddd
        {
            get
            {
                return this.dddField;
            }
            set
            {
                this.dddField = value;
            }
        }

        /// <remarks/>
        public uint telefone1
        {
            get
            {
                return this.telefone1Field;
            }
            set
            {
                this.telefone1Field = value;
            }
        }

        /// <remarks/>
        public object telefone2
        {
            get
            {
                return this.telefone2Field;
            }
            set
            {
                this.telefone2Field = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaEnderecoEmpresa
    {

        private string tipoLogradouroField;

        private string logradouroField;

        private string numeroField;

        private string complementoField;

        private string bairroField;

        private uint cepField;

        private ushort codMunicipioField;

        private string ufField;

        private byte codPaisField;

        /// <remarks/>
        public string tipoLogradouro
        {
            get
            {
                return this.tipoLogradouroField;
            }
            set
            {
                this.tipoLogradouroField = value;
            }
        }

        /// <remarks/>
        public string logradouro
        {
            get
            {
                return this.logradouroField;
            }
            set
            {
                this.logradouroField = value;
            }
        }

        /// <remarks/>
        public string numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <remarks/>
        public string bairro
        {
            get
            {
                return this.bairroField;
            }
            set
            {
                this.bairroField = value;
            }
        }

        /// <remarks/>
        public uint cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        public ushort CodMunicipio
        {
            get
            {
                return this.codMunicipioField;
            }
            set
            {
                this.codMunicipioField = value;
            }
        }

        /// <remarks/>
        public string uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public byte codPais
        {
            get
            {
                return this.codPaisField;
            }
            set
            {
                this.codPaisField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaAtuacaoEmpresa
    {

        private string objetoSocialField;

        private uint cnaePrincipalField;

        private object cnaeSecundariaField;

        /// <remarks/>
        public string objetoSocial
        {
            get
            {
                return this.objetoSocialField;
            }
            set
            {
                this.objetoSocialField = value;
            }
        }

        /// <remarks/>
        public uint cnaePrincipal
        {
            get
            {
                return this.cnaePrincipalField;
            }
            set
            {
                this.cnaePrincipalField = value;
            }
        }

        /// <remarks/>
        public object cnaeSecundaria
        {
            get
            {
                return this.cnaeSecundariaField;
            }
            set
            {
                this.cnaeSecundariaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaSocioFundadorDiretor
    {

        private byte eventoField;

        private uint dataEventoField;

        private string nomeField;

        private ulong cpfCnpjField;

        private object nireField;

        private byte qualificacaoField;

        private uint vlParticipacaoField;

        private string tipoLogradouroField;

        private string logradouroField;

        private string numeroField;

        private string complementoField;

        private string bairroField;

        private uint cepField;

        private ushort codMunicipioField;

        private string ufField;

        private byte codPaisField;

        private byte dddField;

        private uint telefone1Field;

        private object telefone2Field;

        private string emailField;

        private byte flagAdministradorField;

        private string gerenteUsoFirmaField;

        private coletaSocioFundadorDiretorFirmantes firmantesField;

        private coletaSocioFundadorDiretorDadosPessoaFisica dadosPessoaFisicaField;

        private object grupoRepresentanteField;

        /// <remarks/>
        public byte evento
        {
            get
            {
                return this.eventoField;
            }
            set
            {
                this.eventoField = value;
            }
        }

        /// <remarks/>
        public uint dataEvento
        {
            get
            {
                return this.dataEventoField;
            }
            set
            {
                this.dataEventoField = value;
            }
        }

        /// <remarks/>
        public string nome
        {
            get
            {
                return this.nomeField;
            }
            set
            {
                this.nomeField = value;
            }
        }

        /// <remarks/>
        public ulong cpfCnpj
        {
            get
            {
                return this.cpfCnpjField;
            }
            set
            {
                this.cpfCnpjField = value;
            }
        }

        /// <remarks/>
        public object nire
        {
            get
            {
                return this.nireField;
            }
            set
            {
                this.nireField = value;
            }
        }

        /// <remarks/>
        public byte qualificacao
        {
            get
            {
                return this.qualificacaoField;
            }
            set
            {
                this.qualificacaoField = value;
            }
        }

        /// <remarks/>
        public uint vlParticipacao
        {
            get
            {
                return this.vlParticipacaoField;
            }
            set
            {
                this.vlParticipacaoField = value;
            }
        }

        /// <remarks/>
        public string tipoLogradouro
        {
            get
            {
                return this.tipoLogradouroField;
            }
            set
            {
                this.tipoLogradouroField = value;
            }
        }

        /// <remarks/>
        public string logradouro
        {
            get
            {
                return this.logradouroField;
            }
            set
            {
                this.logradouroField = value;
            }
        }

        /// <remarks/>
        public string numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <remarks/>
        public string bairro
        {
            get
            {
                return this.bairroField;
            }
            set
            {
                this.bairroField = value;
            }
        }

        /// <remarks/>
        public uint cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        public ushort CodMunicipio
        {
            get
            {
                return this.codMunicipioField;
            }
            set
            {
                this.codMunicipioField = value;
            }
        }

        /// <remarks/>
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        public byte codPais
        {
            get
            {
                return this.codPaisField;
            }
            set
            {
                this.codPaisField = value;
            }
        }

        /// <remarks/>
        public byte ddd
        {
            get
            {
                return this.dddField;
            }
            set
            {
                this.dddField = value;
            }
        }

        /// <remarks/>
        public uint telefone1
        {
            get
            {
                return this.telefone1Field;
            }
            set
            {
                this.telefone1Field = value;
            }
        }

        /// <remarks/>
        public object telefone2
        {
            get
            {
                return this.telefone2Field;
            }
            set
            {
                this.telefone2Field = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        public byte flagAdministrador
        {
            get
            {
                return this.flagAdministradorField;
            }
            set
            {
                this.flagAdministradorField = value;
            }
        }

        /// <remarks/>
        public string gerenteUsoFirma
        {
            get
            {
                return this.gerenteUsoFirmaField;
            }
            set
            {
                this.gerenteUsoFirmaField = value;
            }
        }

        /// <remarks/>
        public coletaSocioFundadorDiretorFirmantes Firmantes
        {
            get
            {
                return this.firmantesField;
            }
            set
            {
                this.firmantesField = value;
            }
        }

        /// <remarks/>
        public coletaSocioFundadorDiretorDadosPessoaFisica dadosPessoaFisica
        {
            get
            {
                return this.dadosPessoaFisicaField;
            }
            set
            {
                this.dadosPessoaFisicaField = value;
            }
        }

        /// <remarks/>
        public object GrupoRepresentante
        {
            get
            {
                return this.grupoRepresentanteField;
            }
            set
            {
                this.grupoRepresentanteField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaSocioFundadorDiretorFirmantes
    {

        private ulong cpfField;

        /// <remarks/>
        public ulong cpf
        {
            get
            {
                return this.cpfField;
            }
            set
            {
                this.cpfField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaSocioFundadorDiretorDadosPessoaFisica
    {

        private string nomeMaeField;

        private string nomePaiField;

        private uint dtNascimentoField;

        private string nrDocumentoField;

        private string descricaoOrgaoEmissorField;

        private string ufOrgaoEmissorField;

        private object dtEmissaoDocumentoField;

        private object dtVencimentoDocumentoField;

        private byte codigoPaisField;

        private string sexoField;

        private object ufNaturalidadeField;

        private string estadoCivilField;

        private string regimeBensField;

        private string emancipacaoField;

        private string descricaoProfissaoField;

        /// <remarks/>
        public string nomeMae
        {
            get
            {
                return this.nomeMaeField;
            }
            set
            {
                this.nomeMaeField = value;
            }
        }

        /// <remarks/>
        public string nomePai
        {
            get
            {
                return this.nomePaiField;
            }
            set
            {
                this.nomePaiField = value;
            }
        }

        /// <remarks/>
        public uint dtNascimento
        {
            get
            {
                return this.dtNascimentoField;
            }
            set
            {
                this.dtNascimentoField = value;
            }
        }

        /// <remarks/>
        public string nrDocumento
        {
            get
            {
                return this.nrDocumentoField;
            }
            set
            {
                this.nrDocumentoField = value;
            }
        }

        /// <remarks/>
        public string descricaoOrgaoEmissor
        {
            get
            {
                return this.descricaoOrgaoEmissorField;
            }
            set
            {
                this.descricaoOrgaoEmissorField = value;
            }
        }

        /// <remarks/>
        public string ufOrgaoEmissor
        {
            get
            {
                return this.ufOrgaoEmissorField;
            }
            set
            {
                this.ufOrgaoEmissorField = value;
            }
        }

        /// <remarks/>
        public object dtEmissaoDocumento
        {
            get
            {
                return this.dtEmissaoDocumentoField;
            }
            set
            {
                this.dtEmissaoDocumentoField = value;
            }
        }

        /// <remarks/>
        public object dtVencimentoDocumento
        {
            get
            {
                return this.dtVencimentoDocumentoField;
            }
            set
            {
                this.dtVencimentoDocumentoField = value;
            }
        }

        /// <remarks/>
        public byte codigoPais
        {
            get
            {
                return this.codigoPaisField;
            }
            set
            {
                this.codigoPaisField = value;
            }
        }

        /// <remarks/>
        public string sexo
        {
            get
            {
                return this.sexoField;
            }
            set
            {
                this.sexoField = value;
            }
        }

        /// <remarks/>
        public object UfNaturalidade
        {
            get
            {
                return this.ufNaturalidadeField;
            }
            set
            {
                this.ufNaturalidadeField = value;
            }
        }

        /// <remarks/>
        public string estadoCivil
        {
            get
            {
                return this.estadoCivilField;
            }
            set
            {
                this.estadoCivilField = value;
            }
        }

        /// <remarks/>
        public string regimeBens
        {
            get
            {
                return this.regimeBensField;
            }
            set
            {
                this.regimeBensField = value;
            }
        }

        /// <remarks/>
        public string emancipacao
        {
            get
            {
                return this.emancipacaoField;
            }
            set
            {
                this.emancipacaoField = value;
            }
        }

        /// <remarks/>
        public string descricaoProfissao
        {
            get
            {
                return this.descricaoProfissaoField;
            }
            set
            {
                this.descricaoProfissaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class coletaContador
    {

        private ulong cpfCnpjContabField;

        private string nomeEmpresarialContabField;

        private ushort sequencialCrcContabField;

        private string ufCrcContabField;

        private byte classifCrcContabField;

        private string tipoCrcContabField;

        /// <remarks/>
        public ulong cpfCnpjContab
        {
            get
            {
                return this.cpfCnpjContabField;
            }
            set
            {
                this.cpfCnpjContabField = value;
            }
        }

        /// <remarks/>
        public string nomeEmpresarialContab
        {
            get
            {
                return this.nomeEmpresarialContabField;
            }
            set
            {
                this.nomeEmpresarialContabField = value;
            }
        }

        /// <remarks/>
        public ushort sequencialCrcContab
        {
            get
            {
                return this.sequencialCrcContabField;
            }
            set
            {
                this.sequencialCrcContabField = value;
            }
        }

        /// <remarks/>
        public string ufCrcContab
        {
            get
            {
                return this.ufCrcContabField;
            }
            set
            {
                this.ufCrcContabField = value;
            }
        }

        /// <remarks/>
        public byte classifCrcContab
        {
            get
            {
                return this.classifCrcContabField;
            }
            set
            {
                this.classifCrcContabField = value;
            }
        }

        /// <remarks/>
        public string tipoCrcContab
        {
            get
            {
                return this.tipoCrcContabField;
            }
            set
            {
                this.tipoCrcContabField = value;
            }
        }
    }


}
