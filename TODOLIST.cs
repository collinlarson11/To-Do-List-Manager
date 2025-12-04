// Author: Collin Lapointe
// Assignment: TODO List Manager — IOT1001 Programming & Logic Applied Activity 4
// Description: Console-based task manager using a 1D string array with a max of 10 tasks

using System;

class TodoListManager
{
    // Array to store tasks
    static string[] todoList = new string[10];

    // Parallel array to track whether each task is complete
    static bool[] isDone = new bool[10];

    // Tracks how many tasks are currently stored
    static int taskCount = 0;

    static void Main()
    {
        int choice;

        // Main loop: runs until user selects Exit
        while (true)
        {
            Console.Clear(); // Keep interface clean
            DisplayMenu();  // Show menu

            // Validate menu input
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            // Handle menu selection
            switch (choice)
            {
                case 1: AddTask(); break;
                case 2: ViewAllTasks(); break;
                case 3: MarkTaskComplete(); break;
                case 4: DeleteTask(); break;
                case 5: ViewIncompleteTasks(); break;
                case 6: return; // Exit program
            }
        }
    }

    // a) DisplayMenu(): Shows main menu
    static void DisplayMenu()
    {
        Console.WriteLine("=== TODO List Manager ===");
        Console.WriteLine("1. Add a task");
        Console.WriteLine("2. View all tasks");
        Console.WriteLine("3. Mark task as complete");
        Console.WriteLine("4. Delete a task");
        Console.WriteLine("5. View incomplete tasks only");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    // b) AddTask(): Adds a new task if space is available
    static void AddTask()
    {
        if (taskCount >= 10)
        {
            Console.WriteLine("Task list is full!");
            
            return;
        }

        string task;
        do
        {
            Console.Write("Enter task description: ");
            task = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(task))
                Console.WriteLine("Task cannot be empty.");
        } while (string.IsNullOrWhiteSpace(task));

        todoList[taskCount] = task;
        isDone[taskCount] = false; // Mark as incomplete
        taskCount++;

        Console.WriteLine("Task added successfully!");
        Console.ReadLine();
    }

    // c) ViewAllTasks(): Displays all tasks with numbering and [DONE] prefix
    static void ViewAllTasks()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks yet!");
        }
        else
        {
            for (int i = 0; i < taskCount; i++)
            {
                string status = isDone[i] ? "[DONE]" : "[INCOMPLETE]";
                Console.WriteLine($"{i + 1}. {status} {todoList[i]}");
            }
        }
        Console.ReadLine();
    }

    // d) MarkTaskComplete(): Marks a task as complete
    static void MarkTaskComplete()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks to mark.");
            Console.ReadLine();
            return;
        }

        ViewAllTasks(); // Show current tasks
        int index;
        Console.Write("Enter task number to mark as complete: ");

        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > taskCount)
        {
            Console.Write("Invalid number. Try again: ");
        }

        index -= 1; // Convert to array index

        if (isDone[index])
        {
            Console.WriteLine("Task is already marked as complete.");
        }
        else
        {
            isDone[index] = true;
            Console.WriteLine("Task marked as complete.");
        }
        Console.ReadLine();
    }

    // e) DeleteTask(): Deletes a task and shifts remaining tasks
    static void DeleteTask()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks to delete.");
            Console.ReadLine();
            return;
        }

        ViewAllTasks(); // Show current tasks
        int index;
        Console.Write("Enter task number to delete: ");

        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > taskCount)
        {
            Console.Write("Invalid number. Try again: ");
        }

        index -= 1; // Convert to array index

        // Shift tasks left to fill the gap
        for (int i = index; i < taskCount - 1; i++)
        {
            todoList[i] = todoList[i + 1];
            isDone[i] = isDone[i + 1];
        }
        
        // Clear last slot
        todoList[taskCount - 1] = null;
        isDone[taskCount - 1] = false;
        taskCount--;
        Console.WriteLine("Task deleted successfully.");
        Console.ReadLine();
    }

    // f) ViewIncompleteTasks(): Shows only tasks not marked as complete
    static void ViewIncompleteTasks()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks yet!");
            Console.ReadLine();
            return;
        }

        int count = 0;
        for (int i = 0; i < taskCount; i++)
        {
            if (!isDone[i])
            {
                Console.WriteLine($"{i + 1}. [INCOMPLETE] {todoList[i]}");
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("All tasks completed! Great job!");
        }

        Console.ReadLine();
    }
}
