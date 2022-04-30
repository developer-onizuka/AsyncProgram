# AsyncProgram

# 1. First of all, Run the programe
```
# dotnet run
--- CheckPoint A ---
--- CheckPoint B ---
--- CheckPoint C ---
--- CheckPoint D ---
task1 started.
task2 started.
task3 started.
task1 finished.
task2 finished.
task3 finished.
--- CheckPoint E ---
12300
```
or
```
# dotnet run
--- CheckPoint A ---
--- CheckPoint B ---
task1 started.
--- CheckPoint C ---
--- CheckPoint D ---
task2 started.
task3 started.
task1 finished.
task2 finished.
task3 finished.
--- CheckPoint E ---
12300
```

# 2. What is an Async program
It doesn't matter who (or which thread) executes which task, as long as the tasks are executed in order.<br>
The order is <br>
- task3 must wait for task2
- task2 must wait for task1

The "async" is a symbol of start to run asynchronous program. You can find some "await" symbols coresponding to the "async".<br>
It is very simillar to **"Barrier Synchronization"** in MPI which are used in High performance computing.<br>
In this case, the "await" is waitng for the completion of task3.

```
        static async Task Main()
        {
            Console.WriteLine("--- CheckPoint A ---");
          ...
            Console.WriteLine("--- CheckPoint B ---");
          ...
            Console.WriteLine("--- CheckPoint C ---");
          ...
            Console.WriteLine("--- CheckPoint D ---");
          ...
            int result = await task3;
            Console.WriteLine(result);
            
            Console.WriteLine("--- CheckPoint E ---");
        }
```

Then, the order must be
```
--- CheckPoint A ---
--- CheckPoint B ---
--- CheckPoint C ---
--- CheckPoint D ---
12300
--- CheckPoint E ---
```

# 3. Async/Await in Lamda
In task1's Lamda, there is no Async/Await pair. But there is an Async/Await pair in task2 and task3 each. <br>

The order is <br>

(1) The "task1 started" in task1 is displayed after "CheckPoint A".<br>
(2) The "task2 started" in task2 is also displayed subsequently, because of the Task's feature.<br>
(3) The "task3 started" in task3 is also displayed subsequently, because of the Task's feature.<br>

Next,<br>
Which of followings would be finished earlier ? <br>
- SomeMethod(10000), SomeMethod(2000) or SomeMethod(300) <br>

Of cource SomeMethod(300) in task3 is the eariest to be finished. <br>
The second is SomeMethod(2000) in task2.<br>
Even though the above, task2 will not finish because of "await" in task2. It is waiting for the completion of task1. <br>
After finising of task1, task2 will finish.<br>

(4) The "task1 finished" in task1 is desplayed.<br>
(5) The "task2 finished" in task2 is desplayed.<br>
(6) The "task3 finished" in task3 is desplayed.<br>


Finally, the race of CheckPoint<A,B,C,D> and each Task can not be controlled.<br>
But CheckPoint E must be performed after "await" of task3.

All of above is a reason about the output of the Async program of mine.

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
```
