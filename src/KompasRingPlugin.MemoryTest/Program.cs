using System.Diagnostics;
using Core;
using Model;

var ringsCount = 0;
var _ring = new Ring
{
	Width = 30,
	Height = 30,
	Radius = 40,
	JewelryAngle = 45,
	RoundScale = 5
};

long peakMemoryUsage = 0;
var isCrushed = false;

var buildTimeFilePath = "../../../buildTime.txt";
var itemIndexFilePath = "../../../itemIndex.txt";
var memoryUsageFilePath = "../../../memoryUsage.txt";

if (!File.Exists(buildTimeFilePath))
{
    File.Create(buildTimeFilePath);
}
if (!File.Exists(itemIndexFilePath))
{
    File.Create(itemIndexFilePath);
}
if (!File.Exists(memoryUsageFilePath))
{
    File.Create(memoryUsageFilePath);
}

var buildTimeWriter = new StreamWriter(buildTimeFilePath, false);
var itemIndexWriter = new StreamWriter(itemIndexFilePath, false);
var memoryUsageWriter = new StreamWriter(memoryUsageFilePath, false);

try
{
    Console.WriteLine("Подготовка к тестированию... \nПодключение к компасу и проверочное построение детали.");
    var ringBuilder = new RingBuilder();
    await ringBuilder.Build(_ring);
}
catch (Exception e)
{
    Console.WriteLine("Не удается построить деталь");
    Console.WriteLine(e.Message);
    Console.ReadLine();
    return;
}

itemIndexWriter.Write("[");
memoryUsageWriter.Write("[");
using (Process myProcess = Process.GetProcessesByName("kStudy").FirstOrDefault())
{
    do
    {
        if (!myProcess.HasExited)
        {
            try
            {
                var ringBuilder = new RingBuilder();
                ringBuilder.Build(_ring);
                ++ringsCount;
                myProcess.Refresh();
                Console.Clear();
                Console.WriteLine($"  Количество деталей        : {ringsCount}");
                itemIndexWriter.Write($" {ringsCount},");
                //indexes.Add(ringsCount);
                Console.WriteLine($"  Physical memory usage     : {myProcess.WorkingSet64}");
                memoryUsageWriter.Write($" {myProcess.WorkingSet64},");
                //memoryUsages.Add(myProcess.WorkingSet64);
                Console.WriteLine($"  User processor time       : {myProcess.UserProcessorTime}");
                Console.WriteLine($"  Privileged processor time : {myProcess.PrivilegedProcessorTime}");
                Console.WriteLine($"  Total processor time      : {myProcess.TotalProcessorTime}");
                Console.WriteLine($"  Paged system memory size  : {myProcess.PagedSystemMemorySize64}");
                Console.WriteLine($"  Paged memory size         : {myProcess.PagedMemorySize64}");
            }
            catch
            {
                KompasConnector.Instance.Disconnect();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("  Тестирование завершено.");
                Console.WriteLine($"  Построено деталей				: {ringsCount}");
                Console.WriteLine(
                    $"  Затрачено всего времени		: {myProcess.TotalProcessorTime.TotalMinutes} мин : {myProcess.TotalProcessorTime.TotalSeconds} сек");
                var forOne = myProcess.TotalProcessorTime.Milliseconds / ringsCount;
                Console.WriteLine($"  Затрачено на одну деталь		: {forOne} мс");
                Console.ReadLine();
                itemIndexWriter.Write("]");
                memoryUsageWriter.Write("]");
                itemIndexWriter.Close();
                memoryUsageWriter.Close();
                return;
            }
        }
    } while (ringsCount < 5);

    KompasConnector.Instance.Disconnect();
    itemIndexWriter.Write("]");
    memoryUsageWriter.Write("]");
    itemIndexWriter.Close();
    memoryUsageWriter.Close();

    if (isCrushed)
    {
        Console.ReadLine();
        return;
    }
	Console.WriteLine("Выполнено");
}