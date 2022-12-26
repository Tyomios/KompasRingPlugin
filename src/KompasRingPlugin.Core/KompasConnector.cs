namespace Core;

/// <summary>
/// Отвечает за подключение к КОМПАС-3D.
/// </summary>
public class KompasConnector
{
    /// <summary>
    /// Создает экземпляр класса <see cref="KompasConnector"/>.
    /// </summary>
    private KompasConnector()
    {

    }

    //TODO: RSDN+
    /// <summary>
    /// КОМПАС-3D.
    /// </summary>
    private static KompasObject? _kompasObject;

    /// <summary>
    /// Инстанция подключения.
    /// </summary>
    private static KompasConnector? _instance;

    /// <summary>
    /// Возвращает инстанцию подключения.
    /// </summary>
    public static KompasConnector Instance
    {
        get => _instance ??= new ();
    }

    /// <summary>
    /// Выполняет подключение к приложению КОМПАС-3D.
    /// </summary>
    private async Task Connect()
    {
        if (_kompasObject is not null)
        {
            return;
        }

        var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
        if (kompasType is null)
        {
            throw new Exception("Не удалось обнаружить приложение КОМПАС-3D");
        }

        await Task.Run(() =>
        {
            _kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
            if (_kompasObject is not null)
            {
                _kompasObject.ActivateControllerAPI();
                _kompasObject.Visible = true;
                return;
            }
            throw new Exception("Не удалось подключиться к КОМПАС-3D");
        });
    }

    /// <summary>
    /// Производит закрытие приложения КОМПАС-3D, если оно было открыто.
    /// </summary>
    public void Disconnect()
    {
        try
        {
            if (_kompasObject is null) return;

            _kompasObject.Quit();
        }
        //TODO: RSDN +
        catch (Exception e)
        {
            
        }
    }

    /// <summary>
    /// Возвращает новый документ для создания детали.
    /// </summary>
    /// <returns> Документ для создания трехмерной детали. </returns>
    public async Task<Document3D> GetDocument()
    {
        if(_kompasObject is null)
        {
            await Connect();
        }

        Document3D doc3D = (Document3D)_kompasObject.Document3D();
        doc3D.Create(false, true);

        return doc3D;
    }
}