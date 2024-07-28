<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RedirectV2Post.aspx.cs" Inherits="RCPJ.Application.RedirectV2Post" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<script>
    function openWindowWithPost(url, name, windowoption, params) {
        var newWindow = window.open("", name, windowoption);
        var form = newWindow.document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", url);
        form.setAttribute("target", name);
        for (var i in params) {
            if (params.hasOwnProperty(i)) {
                var input = newWindow.document.createElement('input');
                input.type = 'hidden';
                input.name = i;
                input.id = i;
             
                input.value = params[i];
                form.appendChild(input);
            }
        }
        newWindow.document.body.appendChild(form);
        form.submit();
        newWindow.document.body.removeChild(form);
    }
    function poptastic(url, nome, pdialogWidth, pdialogHeight) {

        if (url.substring(0, 7) == "http://") {
            var newWindow = window.open(url, '_self');
            //newWindow.focus();
            return;
        }

        var largura, altura;
        if (nome == null) {
            nome = "TelaPorDefectoJUCESC";
            nome = url.split('.')[0];
        }

        var pdialogWidth, pdialogHeight;

        if (pdialogHeight == null) {
            pdialogHeight = 580;
        }

        if (pdialogWidth == null) {
            pdialogWidth = 780;
        }

        largura = screen.width - 10;
        altura = screen.height - 80;
        newwindow = window.open(url, nome, 'width=' + largura + ',height=' + altura + ',top=0,left=0,screenX=0,screenY=0,status=yes,scrollbars=yes,toolbar=no,resizable=yes,maximized=yes,menubar=no,location=no');


        if (newwindow == null) {
            alert('SEU NAVEGADOR OU BARRA DE FERRAMENTAS BLOQUEOU A ABERTURA DE NOVAS JANELAS (POP-UP). POR FAVOR HABILITE');
        }

        //	newwindow = window.open(url,nome,'fullscreen=yes, scrollbars=auto,maximized=yes,resizable=yes,status=yes,dependent=yes,titlebar=no,menubar=no,location=no');

        //newwindow=window.open(url,nome,'height=' + pdialogHeight + ',width=' + pdialogWidth + ', scrollbars=yes,resizable=1,status=yes,dependent=yes,titlebar=no, menubar=no, location=no');
        //newwindow=window.open(url,nome, 'maximized=yes,scrollbars=yes,resizable=yes,status=yes,dependent=yes,titlebar=no, menubar=no, location=no');
        newwindow.moveTo(2, 2);


        if (nome == 'help' || nome == 'xslt' || nome == 'CONSULTA' || nome == 'CONSULTA2' || nome == 'CONSULTASINSTITUICAO' || nome == 'CONSULTA3') newwindow.moveTo(60, 10)

        //if (window.focus) { newwindow.focus() }

    }
</script>
<body>
    <form id="form1" runat="server" method="post">
    <div>
    </div>
    </form>
</body>
</html>
