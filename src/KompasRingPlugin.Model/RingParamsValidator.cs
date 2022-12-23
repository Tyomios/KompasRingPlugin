using System;

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
        if (ring.RoundScale.Equals(ring.Width / 2) && ring.JewelryAngle > 0)
        {
            throw new Exception("Невозможно построить корректную деталь " +
                                "при указанных значениях скругления и ювелирного выреза");
        }
        if (ring.Engraving.TextSize > ring.Width / 2 
            && !ring.Engraving.Text.Equals(String.Empty))
        {
            throw new Exception("Значение размера текста превышает толщину кольца");
        }

        var engravingLength = ring.Engraving.TextSize * ring.Engraving.Text.Length;
        if (engravingLength >= ring.Radius * 2)
        {
            throw new Exception("Длина текста превышает длину кольца. " +
                                "\n Измените размер текста или увеличьте размер кольца");
        }

    }

}