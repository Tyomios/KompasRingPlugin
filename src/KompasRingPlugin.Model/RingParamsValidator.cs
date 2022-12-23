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
        var errorList = string.Empty;
        var errorNumber = 1;
        if (ring.RoundScale > ring.Width)
        {
            errorList += $"{errorNumber++}. Значение скругления превышает толщину кольца.\n";
        }
        if (ring.RoundScale.Equals(ring.Width / 2) && ring.JewelryAngle > 0)
        {
            errorList += $"{errorNumber++}. Невозможно построить корректную деталь " +
                         "при указанных значениях скругления и ювелирного выреза.\n";
        }
        if (ring.Engraving.TextSize > ring.Width / 2 
            && !ring.Engraving.Text.Equals(String.Empty))
        {
            errorList += $"{errorNumber++}. Значение размера текста превышает толщину кольца.\n";
        }

        var engravingLength = ring.Engraving.TextSize * ring.Engraving.Text.Length;
        if (engravingLength >= ring.Radius * 2)
        {
            errorList += $"{errorNumber++}. Длина текста превышает длину кольца." +
                         "\n\t Измените размер текста или увеличьте размер кольца.\n";
        }
        if (ring.JewelryAngle > 180)
        {
            var safetyEngravingJewrlyCutRelation270 = 1;
            var actualRelation = engravingLength / (2 * (ring.Radius + ring.Height));
            var safetyEngravingJewrlyCutRelation225 = 1.3;

            if (ring.JewelryAngle.Equals(360))
            {
                errorList += $"{errorNumber++}. При выбранном угле выреза нарушена целостность кольца.\n";
            }
            else if (!ring.Engraving.Text.Equals(String.Empty)
                && ring.JewelryAngle > 270)
            {
                errorList += $"{errorNumber++}. При выбранном угле выреза нарушена целостность гравировки кольца.\n";
            }
            else if (ring.JewelryAngle.Equals(270)
                     && actualRelation > safetyEngravingJewrlyCutRelation270)
            {
                errorList += $"{errorNumber++}. При выбранном угле выреза нарушена целостность гравировки кольца.\n";
            }
            else if (ring.JewelryAngle.Equals(225)
                     && actualRelation > safetyEngravingJewrlyCutRelation225)
            {
                errorList += $"{errorNumber++}. При выбранном угле выреза нарушена целостность гравировки кольца.\n";
            }
        }

        if (!errorList.Equals(String.Empty))
        {
            throw new Exception(errorList);
        }
    }
}