using System;
using System.Threading;
using System.Threading.Tasks;
 
namespace ConsoleApplication1
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("--- CheckPoint A ---");
	
            Task<int> task1 = Task.Run(() => {
		Console.WriteLine("task1 started.");
                int result1 = SomeMethod(10000);
		Console.WriteLine("task1 finished.");
                return result1;
            });

            Console.WriteLine("--- CheckPoint B ---");

            Task<int> task2 = Task.Run(async () => {
		Console.WriteLine("task2 started.");
		int result2 = await task1;
                result2 = result2 + SomeMethod(2000);
		Console.WriteLine("task2 finished.");
                return result2;
            });

            Console.WriteLine("--- CheckPoint C ---");
 
            Task<int> task3 = Task.Run(async () => {
		Console.WriteLine("task3 started.");
		int result3 = await task2;
                result3 = result3 + SomeMethod(300);
		Console.WriteLine("task3 finished.");
                return result3;
            });

            Console.WriteLine("--- CheckPoint D ---");

	    int result = await task3;
            Console.WriteLine(result);

            Console.WriteLine("--- CheckPoint E ---");
        }
        
        static int SomeMethod(int x)
        {
		Thread.Sleep(x);
		return x;
        }
    }
}
