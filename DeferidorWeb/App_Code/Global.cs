using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;



/// <summary>
/// Summary description for Global
/// </summary>
/// 

public class Global
{

    public static Object CreateObject(string XMLString, Object YourClassObject)
    {
        XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
        //The StringReader will be the stream holder for the existing XML file 
        YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
        //initially deserialized, the data is represented by an object without a defined type 
        return YourClassObject;
    }

    public static string CreateXML(Object YourClassObject)
    {
        XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
        // Initializes a new instance of the XmlDocument class.          
        XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
        // Creates a stream whose backing store is memory. 
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xmlSerializer.Serialize(xmlStream, YourClassObject);
            xmlStream.Position = 0;
            //Loads the XML document from the specified string.
            xmlDoc.Load(xmlStream);
            return xmlDoc.InnerXml;
        }
    }
    public static bool valNulo(object pValue)
    {
        if (pValue == null || pValue.ToString() == "")
        {
            return true;
        }

        return false;
    }

    public static string valNuloBranco(object pValue)
    {
        try
        {
            if (pValue == null || pValue.ToString() == "")
            {
                return "";
            }

            return pValue.ToString();
        }
        catch
        {
            return "";
        }
    }


    public static void salvaDados(string xmlMEIInput, string nomeArquivo)
    {
        Random random = new Random();
        //int i = random.Next(200);
        //string nomeArquivo = codServico + "1 " + versao + "2 " + numeroProtocolo + "3 " + numeroOcorrencia.ToString() + "4 " + numeroServicoRecusado.ToString();
        FileInfo f = new FileInfo(@ConfigurationManager.AppSettings["caminhoFisicoXml"].ToString() + nomeArquivo + ".xml");
        if (f.Exists)
        {
            f.Delete();
        }
        StreamWriter w = f.CreateText();
        w.WriteLine(xmlMEIInput);
        w.Close();
    }
}
