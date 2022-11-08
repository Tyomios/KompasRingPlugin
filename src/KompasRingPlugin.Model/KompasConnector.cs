using Kompas6API5;
using System;
using System.Threading.Tasks;

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
        get => _instance ??= new KompasConnector();
    }

    public async void Connect()
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


    //public IKompasDocument3D GetDocument()
    //{

    //}
}