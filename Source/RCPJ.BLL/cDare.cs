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
    private string _erro;  //  - C�digo de erro
    private string _msgErro;  //     - Mensagem detalhada de erro
    private string _act;  //         - atividade (replica��o do par�metro act de chamada)
    private string _protocolo;  //   - protocolo gerado
    private string _arquivo;  //     

    /*
    'C�DIGO DE ERRO DE RETORNO
    '    C�digos de erro
    '         0 - Ok
    '         1 - Nenhuma requisi��o realizada
    '         2 - Um ou mais par�metros faltando (segue lista)
    '         3 - Ato solicitado n�o � permitido
    '         4 - Combina��o ATO-EVENTO n�o � permitida
    '         5 - Documento inv�lido (deve ser um CPF ou CNPJ)
    '         6 - Documento n�o encontrado no cadastro de usu�rios (o CPF ou CNPJ)
    '         7 - Tipo de empresa inv�lido (da tabela GrupoSolicitacao)
    '         8 - Falha ao solicitar PDF
    '         9 - Data de vencimento inv�lida
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
