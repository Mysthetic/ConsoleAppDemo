using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAppDemo;

class Program
{
    static void Main(string[] args)
    {
        List<TodoEntry> todoList = new List<TodoEntry>()
        {
            new TodoEntry("Sample Todo"),
            new TodoEntry("Due Todo", dueDate: DateTime.Now.AddDays(3))
        };

        while (true)
        {
            Console.WriteLine("Enter command (type \"exit\" to quit): ");
            var command = Console.ReadLine();

            if (command == "exit")
            {
                break;
            }
            if (string.IsNullOrEmpty(command))
            {
                continue;
            }

            if (command.StartsWith("create"))
            {
                string[] todoParams = command.Split(" ");
                if (todoParams.Length <= 1)
                {
                    Console.WriteLine($"USAGE: create <todo-name> [<todo-description>] [<todo-due-date>]");
                    continue;
                }

                DateTime dueDate = default;
                bool hasDueDate = todoParams.Length == 4 && DateTime.TryParse(todoParams[3], out dueDate);
                DateTime? dueDateParam = hasDueDate ? dueDate : null;

                var newEntry = new TodoEntry(todoParams![1], (todoParams.Length >= 3 ? todoParams[2] : null), dueDateParam);
                todoList.Add(newEntry);

                string dueDateMessage = hasDueDate ? $"(Due date: {dueDateParam})" : "";
                Console.WriteLine($"Added '{newEntry.Title}' to Todo List {dueDateMessage}");
            }

            // command "list"
            else if (command.StartsWith("list"))
            {
                Console.WriteLine("{0,-40} {1,-20} {2,-20} {3,-10}", "Id", "Title", "Due Date", "Create Date");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                todoList.ForEach(i => Console.WriteLine($"{i.Id,-20} {i.Title,-20} {i.DueDate,-20} {i.CreateDate,-10}"));
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            }

            // command "remove" by Guid
            else if (command.StartsWith("remove"))
            {
                string[] removeParams = command.Split(" ");
                if (removeParams.Length <= 1)
                {
                    Console.WriteLine($"USAGE: remove <todo Id>");
                    continue;
                }

                Guid todoId;
                if (!Guid.TryParse(removeParams[1], out todoId))
                {
                    Console.WriteLine("Invalid todo Id format.");
                    continue;
                }

                TodoEntry removeTodo = todoList.Find(i => i.Id.Equals(todoId));

                if (removeTodo == null)
                {
                    Console.WriteLine($"Todo with Id '{removeParams[1]}' not found.");
                    continue;
                }
                else
                {
                    todoList.Remove(removeTodo);
                    Console.WriteLine($"Removed '{removeTodo.Id}' '{removeTodo.Title}'");
                }
            }

            // command "filter" by Title
            else if (command.StartsWith("filter"))
            {
                string[] filterParams = command.Split(" ");
                if (filterParams.Length <= 1)
                {
                    Console.WriteLine($"USAGE: filter <keyword>");
                    continue;
                }

                string keyword = filterParams[1];
                var filteredTodos = todoList.Where(todo => todo.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

                if (filteredTodos.Count == 0)
                {
                    Console.WriteLine($"No todos found containing '{keyword}'.");
                    continue;
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Todos containing '{keyword}':");
                foreach (var todo in filteredTodos)
                {
                    Console.WriteLine($"- {todo.Title}" +
                                      $"{(todo.Description != null ? $" ({todo.Description})" : "")}" +
                                      $"{(todo.DueDate != null ? $" [Due: {todo.DueDate}]" : "")}");
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            }


            Console.WriteLine("Your command: {0}", command);
        }
    }
}
