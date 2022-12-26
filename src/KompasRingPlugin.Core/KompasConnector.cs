namespace Core{

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

    /// <summary>
    /// КОМПАС-3D.
    /// </summary>
    private static KompasObject s_kompasObject;

    /// <summary>
    /// Инстанция подключения.
    /// </summary>
    private static KompasConnector _instance;

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
    private async Task Connect() //todo добавить попытки подключения или поиска включенного или перенести подключение при старте приложения.
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
            s_kompasObject.ActivateControllerAPI();
            s_kompasObject.Visible = true;
        });
    }

    /// <summary>
    /// Производит закрытие приложения КОМПАС-3D, если оно было открыто.
    /// </summary>
    public void Disconnect()
    {
        try
        {
            if (s_kompasObject is null) return;

            s_kompasObject.Quit();
        }
        catch 
        {
            
        }
    }

    /// <summary>
    /// Возвращает новый документ для создания детали.
    /// </summary>
    /// <returns> Документ для создания трехмерной детали. </returns>
    public async Task<Document3D> GetDocument()
    {
        if(s_kompasObject is null)
        {
            await Connect();
        }

        Document3D doc3D = (Document3D)s_kompasObject.Document3D();
        doc3D.Create(false, true);

        return doc3D;
    }
}
}