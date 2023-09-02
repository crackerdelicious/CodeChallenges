using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace ToDoList
{
    public class Task
    {
        public int Id { get; private set; }
        public string? Description { get; private set; }
        public string? DueDate { get; private set; }
        public bool MarkAsCompleted { get; private set; }

        public Task(int id, string? description, string dueDate, bool markAsCompleted)
        {
            Id = id;
            Description = description;
            DueDate = dueDate;
            MarkAsCompleted = markAsCompleted;
        }

        public override string ToString()
        {
            string isComplete = MarkAsCompleted ? "Completed" : "Incomplete";
            return $"{Id}. [{isComplete}] {Description} (Due: {DueDate})";
        }
    }

    public class TaskRepository
    {
        private static int _nextId = 1;
        private static List<Task> _tasks = new List<Task>();
        
        public static void AddTask(string description, string dueDate, bool markAsCompleted) 
        {
            int Id = _nextId++;
            Task task = new Task(Id, description, dueDate, markAsCompleted);
            _tasks.Add(task);
            Console.WriteLine($"Task \"{description}\" added successfully!");
        }

        public static List<Task> GetAllTasks()
        {
            return _tasks;
        }
    }

    public class TaskDisplay
    {
        public static void DisplayTasks(List<Task> tasks)
        {
            if (tasks.Count != 0)
            {
                Console.WriteLine("List of Tasks:");
                foreach (Task task in tasks)
                {
                    Console.WriteLine(task.ToString());
                }
            }
            else
            {
                Console.WriteLine("No task in a task list");
            }
        }
    }

    public class TaskManager
    {
        public static string CreateTask(string input)
        {
            string? value;
            do
            {
                Console.Write($"Enter {input.ToLower()}: ");
                value = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine($"{input} cannot be empty or whitespace");
                    continue;
                }
                if (input.Equals("due date", StringComparison.OrdinalIgnoreCase) && !IsValidDate(input))
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date.");
                    continue;
                }
                break;
            } while (true);
            Console.WriteLine($"\n{input} added");
            return value;
        }

        public static bool IsValidDate(string date)
        {
            return Regex.IsMatch(date, @"^(\d{4})-(\d{2})-(\d{2})$");
        }
    }

    public class UserInputChoices
    {
        public static void PerformTaskByChoices(int choice) 
        {
            switch (choice)
            {
                case 1:
                    TaskRepository.AddTask(TaskManager.CreateTask("task description"), TaskManager.CreateTask("due date"), false);
                    break;
                case 2:
                    TaskDisplay.DisplayTasks(TaskRepository.GetAllTasks());
                    break;
                case 3:
                    Console.Write("Enter the ID of the task to mark as complete: ");
                    break;
                case 4:
                    Console.Write("Enter the ID of the task to delete: ");
                    break;
            }
        }
    }

    public class Application
    {
        public static void InitTasks()
        {
            TaskRepository.AddTask("Buy Grocery", "2023-09-02", true);
            TaskRepository.AddTask("Walking Dogs", "2023-09-02", true);
            TaskRepository.AddTask("Launch Project", "2023-09-02", false);
            TaskRepository.AddTask("Trade Forex", "2023-09-02", false);
            TaskRepository.AddTask("Learn Blockchain", "2023-09-02", false);
            TaskRepository.AddTask("Coding Challenge", "2023-09-02", true);
            TaskRepository.AddTask("Programing C#", "2023-09-02", true);
            TaskRepository.AddTask("Create new product Shopee", "2023-09-02", false);
            TaskRepository.AddTask("Check Stock", "2023-09-02", false);
        }

        public static void PrintMenus()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. View all tasks");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. Exit\n");
            Console.Write("Enter your choice: ");
        }

        public static void Run()
        {
            InitTasks();

            Console.WriteLine("Welcome to the ToDo List Application!");
            int choice;
            do
            {
                PrintMenus();

                if (int.TryParse(Console.ReadLine()?.Trim(), out choice))
                {
                    switch (choice)
                    {
                        case 1: 
                        case 2: 
                        case 3: 
                        case 4:
                            UserInputChoices.PerformTaskByChoices(choice);
                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid choice");
                            break;
                    }
                }
            } while (choice != 5);
        }
    }
}
