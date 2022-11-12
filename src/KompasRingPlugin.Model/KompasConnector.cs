using System;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;

namespace Model;

public class KompasConnector
{
    private KompasConnector()
    {

    }

    private static KompasObject s_kompasObject;

    private static IApplication s_kompasApplication;

    private static KompasConnector _instance;

    public static KompasConnector Instance
    {
        get => _instance ??= new ();
    }

    /// <summary>
    /// Выполняет подключение к приложению КОМПАС-3D.
    /// </summary>
    private async Task Connect()
    {
        if (s_kompasObject is not null)
        {
            //todo окно сообщения о том, что подключение выполнено ранее.
            return;
        }

        var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");

        await Task.Run(() =>
        {
            s_kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
            s_kompasObject.Visible = true;
        });
    }

    /// <summary>
    /// Производит закрытие приложения КОМПАС-3D, если оно было открыто.
    /// </summary>
    public void Disconnect()
    {
        if (s_kompasApplication is null) return;
        
        s_kompasApplication.Quit();
    }

    /// <summary>
    /// Возвращает новый документ для создания детали.
    /// </summary>
    /// <returns> Документ для создания трехмерной детали. </returns>
    public async Task<IKompasDocument3D> GetDocument()
    {
        if(s_kompasObject is null)
        {
            Connect().Wait(10000);
        }


        var activeDocument = s_kompasApplication.ActiveDocument;
        if (activeDocument is not null 
            && activeDocument.Type.Equals(DocumentTypeEnum.ksDocumentPart))
        {
            return (IKompasDocument3D)s_kompasApplication.ActiveDocument;
        }

        var newDocument = s_kompasApplication.Documents.Add(DocumentTypeEnum.ksDocumentPart);
        s_kompasApplication.ActiveDocument = newDocument;

        return (IKompasDocument3D)s_kompasApplication.ActiveDocument;
    }
}