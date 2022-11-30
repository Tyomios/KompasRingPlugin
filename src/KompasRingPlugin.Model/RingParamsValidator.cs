namespace Model;

/// <summary>
/// Сервисный класс проверки значений для построения кольца с гравировкой
/// </summary>
public static class RingParamsValidator
{
    /// <summary>
    /// Проверка параметров кольца.
    /// </summary>
    /// <param name="ring">Проверяемое кольцо</param>
    /// <returns> false при ошибке в параметрах </returns>
    public static void CheckCorrectValues(Ring ring)
    {
        if (ring.RoundScale > ring.Width)
        {
            throw new Exception("Значение скругления превышает толщину кольца");
        }
        if (ring.Engraving.TextSize > ring.Width / 2 
            && !ring.Engraving.Text.Equals(String.Empty))
        {
            throw new Exception("Значение размера текста превышает толщину кольца");
        }
    }
}