using System;
using System.Collections.Generic;
class TaskSchedulerSystem
{
    // All tasks (history)
    private List<string> allTasks = new List<string>();
    // Execution order (FIFO)
    private Queue<string> taskQueue = new Queue<string>();
    // Undo last executed task (LIFO)
    private Stack<string> executedStack = new Stack<string>();
    // Priority-based tasks (lower number = higher priority)
    private SortedDictionary<int, string> priorityTasks = new
    SortedDictionary<int, string>();
    // Prevent duplicate tasks
    private HashSet<string> taskSet = new HashSet<string>();
    // Add Task
    public void AddTask(string task, int priority)
    {
        if (!taskSet.Add(task))
        {
            Console.WriteLine($"Duplicate task ignored: {task}");
            return;
        }
        allTasks.Add(task);
        taskQueue.Enqueue(task);
        priorityTasks[priority] = task;
        Console.WriteLine($"Task added: {task} (Priority {priority})");
    }

    // Execute Next Task (FIFO)
    public void ExecuteTask()
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("No tasks to execute.");
            return;
        }
        string task = taskQueue.Dequeue();
        executedStack.Push(task);
        Console.WriteLine($"Executed: {task}");
    }

    // Undo Last Executed Task
    public void UndoLastTask()
    {
        if (executedStack.Count == 0)
        {
            Console.WriteLine("No executed task to undo.");
            return;
        }
        string task = executedStack.Pop();
        taskQueue.Enqueue(task);
        Console.WriteLine($"Undo executed task: {task}");
    }
    // Show All Tasks
    public void ShowAllTasks()
    {
        Console.WriteLine("\nAll Tasks:");
        foreach (var task in allTasks)
        {
            Console.WriteLine(task);
        }
    }

    // Show Priority Tasks
    public void ShowPriorityTasks()
    {
        Console.WriteLine("\nPriority-Based Tasks:");
        foreach (var item in priorityTasks)
        {
            Console.WriteLine($"Priority {item.Key} → {item.Value}");
        }
    }
    
    // Show Pending Queue
    public void ShowQueue()
    {
        Console.WriteLine("\nPending Task Queue:");
        foreach (var task in taskQueue)
        {
            Console.WriteLine(task);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskSchedulerSystem scheduler = new TaskSchedulerSystem();
        // Add tasks
        scheduler.AddTask("Backup Database", 2);
        scheduler.AddTask("Run Security Scan", 1);
        scheduler.AddTask("Update System", 3);
        scheduler.AddTask("Backup Database", 2); 
        scheduler.ExecuteTask();
        scheduler.ExecuteTask();
        scheduler.UndoLastTask();
        scheduler.ShowAllTasks();
        scheduler.ShowPriorityTasks();
        scheduler.ShowQueue();
        Console.WriteLine("\nExecution Completed.");
    }
}