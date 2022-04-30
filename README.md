# AsyncProgram

# 1. First of all, Run the programe
```
# dotnet run
--- CheckPoint A ---
task1 started.
--- CheckPoint B ---
task2 started.
task3 started.
task1 finished.
task2 finished.
task3 finished.
--- CheckPoint C ---
12300

```

# 2. What is an Async program
It doesn't matter who (or which thread) executes which task, as long as the tasks are executed in order.<br>
The order is <br>
- task3 must wait for task2
- task2 must wait for task1

The "async" is a symbol of start to run asynchronous program. You can find some "await" symbols coresponding to the "async".<br>
It is very simillar to **"Barrier Synchronization"** in MPI which are used in High performance computing.<br>

```
        static **async** Task Main()
        {
            Console.WriteLine("--- CheckPoint A ---");
            
          ...
          
            Console.WriteLine("--- CheckPoint B ---");
            
            int result = **await** task3;
            
            Console.WriteLine("--- CheckPoint C ---");
            
            Console.WriteLine(result);
        }
```





```
        static async Task Main()
        {
            Console.WriteLine("--- CheckPoint A ---");

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

            Console.WriteLine("--- CheckPoint B ---");

            int result = await task3;

            Console.WriteLine("--- CheckPoint C ---");

            Console.WriteLine(result);
        }
```
