using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo
{
    public class TodoEntry
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DueDate { get; set; }

        public TodoEntry(string title, string? description = null, DateTime? dueDate = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            DueDate = dueDate;
        }

        public TodoEntry(TodoEntry entry)
        {
            Id = entry.Id;
            Title = entry.Title;
            Description = entry.Description;
            CreateDate = entry.CreateDate;
            UpdateDate = entry.UpdateDate;
            DueDate = entry.DueDate;
        }
    }
}
