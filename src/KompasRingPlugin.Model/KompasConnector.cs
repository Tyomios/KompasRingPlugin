using System;
using System.Threading.Tasks;
using KompasAPI7;

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
    public async void Connect()
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
    //public IKompasDocument3D GetDocument()
    //{
    //    //if(s_kompasObject is null) Connect();

    //    var document = (KompasDocument3D)s_kompasObject.Document3D(); //todo доработать время ожидания подключения или вынести в 2 метода.
    //    document.Crea;

    //    return (IKompasDocument3D)document;
    //}
}