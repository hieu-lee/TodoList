using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ToDoList : IComparable<ToDoList>
    {
        [Key]
        public string ListId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public Account Owner { get; set; }
        public HashSet<ToDoItem> Items { get; set; } = new();
        public string DeleteHeight { get; set; } = "0";

        public int CompareTo(ToDoList other)
        {
            var a = -TimeCreate.CompareTo(other.TimeCreate);
            if (a != 0)
            {
                return a;
            }
            return ListId.CompareTo(other.ListId);
        }

        public override bool Equals(object obj)
        {
            var other = (ToDoList)obj;
            return ListId.Equals(other.ListId);
        }

        public override int GetHashCode()
        {
            return ListId.GetHashCode();
        }
    }
}
