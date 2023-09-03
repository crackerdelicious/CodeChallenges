using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace ToDoList
{
    public class Task
    {
        public int TaskId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsCompleted { get; set; }

        public Task(int taskId, string description, DateTime dueDate, bool isCompleted)
        {
            TaskId = taskId;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }

        public override string ToString()
        {
            string markTask = IsCompleted ? "Completed" : "Incomplete";
            return $"{TaskId}. [{markTask}] {Description} Due: ({DueDate})";
        }
    }

    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private int maxTaskId = 0;

        public bool AddTask(string description, string dueDateString)
        {
            // Validate Date Format
            if(!DateTime.TryParseExact(dueDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate))
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                return false;
            }

            // Validate Future Date
            if (dueDate <= DateTime.Now)
            {
                Console.WriteLine("Due date must be in the future.");
                return false;
            }

            // Add a task
            int Id = ++maxTaskId;
            Task newTask = new Task(Id, description, dueDate, false);
            tasks.Add(newTask);
            Console.WriteLine("Task added successfully!");
            return true;
        }

        public bool GetAllTasks()
        {
            if (tasks.Any())
            {
                Console.WriteLine("List of Tasks:");
                foreach (Task task in tasks)
                {
                    Console.WriteLine(task.ToString());
                }
                return true;
            }
            else
            {
                Console.WriteLine("No tasks in a list. Create a task.");
                return false;
            }
        }

        public void MarkTaskAsComplete(int taskId)
        {
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks in a list. Create a task.");
            }
            else
            {
                IEnumerable<Task> matchingTasks = tasks.Where(task => taskId == task.TaskId);
                
                foreach (Task task in matchingTasks)
                {
                    task.IsCompleted = true;
                }
                Console.WriteLine("Task marked as complete!");
            }
        }

        public void DeleteTask(int taskId)
        {
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks in a list. Create a task.");
            }
            else
            {
                tasks.RemoveAt(taskId - 1);
                Console.WriteLine("Task deleted successfully!");
            }
        }

        public void SaveTasksToFile(string filePath)
        {
            using (StreamWriter writer = new(filePath))
            {
                foreach (Task task in tasks)
                {
                    // Serialize and write task properties as a line in the file.
                    string taskLine = $"{task.TaskId},{task.Description},{task.DueDate},{task.IsCompleted}";
                    writer.WriteLine(taskLine);
                }
            }
            Console.WriteLine($"Tasks saved to `{filePath}` successfully!");
        }

        public void LoadTasksFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                tasks.Clear(); // Clear exiting tasks

                using (StreamReader reader = new(filePath))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line into task properties
                        string[] taskData = line.Split(',');
                        if (taskData.Length == 4)
                        {
                            int taskId = int.Parse(taskData[0]);
                            string description = taskData[1];
                            DateTime dueDate = DateTime.Parse(taskData[2]);
                            bool isCompleted = bool.Parse(taskData[3]);

                            // Create a new Task object and add it to the list
                            Task task = new Task(taskId, description, dueDate, isCompleted);
                            tasks.Add(task);

                            // Update TaskId
                            if (taskId > maxTaskId)
                            {
                                maxTaskId = taskId;
                            }
                        }
                    }
                }
                Console.WriteLine("Tasks loaded from file successfully!");
            }
            else
            {
                Console.WriteLine("No task file found.");
            }
        }
    }

    internal class Program
    {
        static void PrintMenus()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. View all tasks");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. Save tasks to file");
            Console.WriteLine("7. Load tasks from file\n");

            Console.Write("Enter your choice: ");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ToDo List Application!");

            TaskManager taskManager = new TaskManager();
            
            int choice;
            
            do
            {
                PrintMenus();

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            do
                            {
                                Console.Write("Enter task description: ");
                                string? description = Console.ReadLine()?.Trim();
                                if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
                                {
                                    Console.WriteLine("This field cannot be empty or whitespace.");
                                    continue;
                                }

                                Console.Write("Enter due date (yyyy-mm-dd): ");
                                string? dueDate = Console.ReadLine()?.Trim();
                                if (string.IsNullOrEmpty(dueDate) || string.IsNullOrWhiteSpace(dueDate))
                                {
                                    Console.WriteLine("This field cannot be empty or whitespace.");
                                    continue;
                                }

                                bool taskAdded = taskManager.AddTask(description, dueDate);

                                if (taskAdded)
                                {
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            } while (true);
                            break;
                        case 2:
                            taskManager.GetAllTasks();
                            break;
                        case 3:
                            do
                            {
                                if (!taskManager.GetAllTasks()) break;

                                Console.Write("Enter the ID of the task to mark as complete: ");
                                if (int.TryParse(Console.ReadLine(), out int taskId))
                                {
                                    taskManager.MarkTaskAsComplete(taskId); 
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invald input. Please enter the ID");
                                    continue;
                                }
                            } while (true);
                            break;
                        case 4:
                            do
                            {
                                if (!taskManager.GetAllTasks()) break;

                                Console.Write("Enter the ID of the task to delete: ");
                                if (int.TryParse(Console.ReadLine(), out int taskId))
                                {
                                    taskManager.DeleteTask(taskId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invald input. Please enter the ID");
                                    continue;
                                }
                            } while (true);
                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
                            break;
                        case 6:
                            taskManager.SaveTasksToFile(@"D:\C#\DummyProjects\CodeChallenges\ToDoList\tasks.txt");
                            break;
                        case 7:
                            taskManager.LoadTasksFromFile(@"D:\C#\DummyProjects\CodeChallenges\ToDoList\tasks.txt");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please choose between 1 - 7");
                }
            } while (choice != 5);
        }
    }
}