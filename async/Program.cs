using System;
using System.Threading;
using System.Threading.Tasks;
 
namespace ConsoleApplication1
{
    class Program
    {
        static async Task Main()
        {
            Task<int> task1 = Task.Run(() => {
		Console.WriteLine("task1 started.");
                int result1 = SomeMethod(10000);
		Console.WriteLine("task1 finished.");
                return result1;
            });

            Task<int> task2 = Task.Run(async () => {
		Console.WriteLine("task2 started.");
		int result2 = await task1;
                result2 = result2 + SomeMethod(2000);
		Console.WriteLine("task2 finished.");
                return result2;
            });
 
            Task<int> task3 = Task.Run(async () => {
		Console.WriteLine("task3 started.");
		int result3 = await task2;
                result3 = result3 + SomeMethod(300);
		Console.WriteLine("task3 finished.");
                return result3;
            });

	    int result = await task3;
 
            Console.WriteLine(result);
        }
        
        static int SomeMethod(int x)
        {
		Thread.Sleep(x);
		return x;
        }
    }
}
