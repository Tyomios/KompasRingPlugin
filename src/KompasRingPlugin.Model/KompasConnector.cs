using System;
using System.Threading;
using System.Threading.Tasks;
using Kompas6Constants;
using KompasAPI7;
using Thread = System.Threading.Thread;

namespace Model;

public class KompasConnector
{

    private KompasConnector()
    {

    }

    private static IKompasAPIObject s_kompasObject;

    private static IApplication s_kompasApplication;

    private static KompasConnector _instance;

    public static KompasConnector Instance
    {
        get => _instance ??= new ();
    }

    /// <summary>
    /// Выполняет подключение к приложению КОМПАС-3D.
    /// </summary>
    private async void Connect()
    {
        if (s_kompasObject is not null)
        {
            //todo окно сообщения о том, что подключение выполнено ранее.
            return;
        }

        var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.7");

        await Task.Run(() =>
        {
            s_kompasObject = (IKompasAPIObject)Activator.CreateInstance(kompasType);
            s_kompasApplication = s_kompasObject.Application;
            s_kompasApplication.Visible = true;
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
    public IKompasDocument3D GetDocument()
    {
        if(s_kompasObject is null) Connect();

        Thread.Sleep(4000); //todo подумать о замене.
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