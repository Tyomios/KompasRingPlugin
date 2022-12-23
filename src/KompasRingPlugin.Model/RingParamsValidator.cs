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
            throw new Exception("Длина текста превышает длину кольца." +
                                "\n Измените размер текста или увеличьте размер кольца");
        }
        if (ring.JewelryAngle.Equals(360))
        {
            throw new Exception("При выбранном угле выреза нарушена целостность кольца.");
        }
        if (!ring.Engraving.Text.Equals(String.Empty)
            && ring.JewelryAngle > 270)
        {
            throw new Exception("При выбранном угле выреза нарушена целостность гравировки кольца.");
        }

        var safetyEngravingJewrlyCutRelation = 1;
        var actualRelation = engravingLength / 2 * (ring.Radius + ring.Height);
        if (ring.JewelryAngle.Equals(270) 
            && actualRelation > safetyEngravingJewrlyCutRelation)
        {
            throw new Exception("При выбранном угле выреза нарушена целостность гравировки кольца.");
        }
    }
}