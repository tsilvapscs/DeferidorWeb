//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for cDarePE
/// </summary>
public class cDarePE
{
    //
    // TODO: Add constructor logic here
    //
    private string _erro;  //  - Código de erro
    private string _msgErro;  //     - Mensagem detalhada de erro
    private string _act;  //         - atividade (replicação do parâmetro act de chamada)
    private string _protocolo;  //   - protocolo gerado
    private string _arquivo;  //     

    /*
    'CÓDIGO DE ERRO DE RETORNO
    '    Códigos de erro
    '         0 - Ok
    '         1 - Nenhuma requisição realizada
    '         2 - Um ou mais parâmetros faltando (segue lista)
    '         3 - Ato solicitado não é permitido
    '         4 - Combinação ATO-EVENTO não é permitida
    '         5 - Documento inválido (deve ser um CPF ou CNPJ)
    '         6 - Documento não encontrado no cadastro de usuários (o CPF ou CNPJ)
    '         7 - Tipo de empresa inválido (da tabela GrupoSolicitacao)
    '         8 - Falha ao solicitar PDF
    '         9 - Data de vencimento inválida
    '        10 - Erro ao gerar/carregar DAE
     */




    public string erro
    {
        get { return _erro; }
        set { _erro = value; }
    }

    public string msgErro
    {
        get { return _msgErro; }
        set { _msgErro = value; }
    }

    public string act
    {
        get { return _act; }
        set { _act = value; }
    }

    public string protocolo
    {
        get { return _protocolo; }
        set { _protocolo = value; }
    }

    public string arquivo
    {
        get { return _arquivo; }
        set { _arquivo = value; }

    }
}
