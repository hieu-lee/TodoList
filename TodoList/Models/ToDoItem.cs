using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ToDoItem
    {
        [Key]
        public string ItemId { get; set; } = Guid.NewGuid().ToString();
        public string ParentListId { get; set; }
        public string Owner { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime? TimeRemind { get; set; }
        public bool Important { get; set; } = false;
        public bool Completed { get; set; } = false;
        public string Content { get; set; } = string.Empty;
        public string Title { get; set; }
        public string ContentHeight { get; set; } = "0";
        public string DeleteHeight { get; set; } = "0";
        public DateTime? LastNotified { get; set; }

        public override bool Equals(object obj)
        {
            var other = (ToDoItem)obj;
            return ItemId.Equals(other.ItemId);
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
    }
}
