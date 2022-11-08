using Kompas6API5;
using System;
using System.Threading.Tasks;
using KompasAPI7;

namespace Model;

public class KompasConnector
{

    private KompasConnector()
    {

    }

    private static KompasObject _kompasObject;

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
        if (_kompasObject is not null)
        {
            //todo окно сообщения о том, что подключение выполнено ранее.
            return;
        }

        var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");

        await Task.Run(() =>
        {
            _kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
            _kompasObject.ActivateControllerAPI();
            _kompasObject.Visible = true;
        });
    }

    /// <summary>
    /// Возвращает новый документ для создания детали.
    /// </summary>
    /// <returns> Документ для создания трехмерной детали. </returns>
    public IKompasDocument3D GetDocument()
    {
        if(_kompasObject is null) Connect();

        var document = (ksDocument3D)_kompasObject.Document3D();
        document.Create();

        return (IKompasDocument3D)document;
    }
}