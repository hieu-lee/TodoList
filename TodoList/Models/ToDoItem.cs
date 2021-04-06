using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ToDoItem
    {
        [Key]
        public string ItemId { get; set; } = Guid.NewGuid().ToString();
        public string ListId { get; set; }
        public DateTime TimeCreate { get; set; }
        public bool Important { get; set; } = false;
        public bool Completed { get; set; } = false;
        public string Content { get; set; }
        public string Title { get; set; }

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
