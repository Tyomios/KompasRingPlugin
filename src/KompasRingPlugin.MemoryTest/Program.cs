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

using (Process myProcess = Process.GetProcessesByName("kStudy").FirstOrDefault())
{
    do
	{
		if (!myProcess.HasExited)
		{
            try
            {
                var ringBuilder = new RingBuilder();
                await ringBuilder.Build(_ring);
            }
            catch
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("  Тестирование завершено.");
                Console.WriteLine($"  Построено деталей				: {ringsCount}");
                Console.WriteLine($"  Затрачено всего времени		: {myProcess.TotalProcessorTime.TotalMinutes} мин : {myProcess.TotalProcessorTime.TotalSeconds} сек");
                var forOne = myProcess.TotalProcessorTime.Milliseconds / ringsCount;
                Console.WriteLine($"  Затрачено на одну деталь		: {forOne} мс");
				Console.ReadLine();
                return;
            }
            
            ++ringsCount;
            myProcess.Refresh();
			Console.Clear();
            Console.WriteLine($"  Количество деталей        : {ringsCount}");
			Console.WriteLine($"  Physical memory usage     : {myProcess.WorkingSet64}");
            Console.WriteLine($"  User processor time       : {myProcess.UserProcessorTime}");
			Console.WriteLine($"  Privileged processor time : {myProcess.PrivilegedProcessorTime}");
			Console.WriteLine($"  Total processor time      : {myProcess.TotalProcessorTime}");
			Console.WriteLine($"  Paged system memory size  : {myProcess.PagedSystemMemorySize64}");
			Console.WriteLine($"  Paged memory size         : {myProcess.PagedMemorySize64}");
			
			//peakPagedMem = myProcess.PeakPagedMemorySize64;
			//peakVirtualMem = myProcess.PeakVirtualMemorySize64;
			//peakWorkingSet = myProcess.PeakWorkingSet64;

			//if (myProcess.Responding)
			//{
			//	Console.WriteLine("Status = Running");
			//}
			//else
			//{
			//	Console.WriteLine("Status = Not Responding");
			//}
		}
	}
	while (ringsCount < 200);

    if (isCrushed)
    {
        Console.ReadLine();
        return;
    }
	Console.WriteLine();
	Console.WriteLine($"  Process exit code          : {myProcess.ExitCode}");

	//Console.WriteLine($"  Peak physical memory usage : {peakWorkingSet}");
	//Console.WriteLine($"  Peak paged memory usage    : {peakPagedMem}");
	//Console.WriteLine($"  Peak virtual memory usage  : {peakVirtualMem}");
}