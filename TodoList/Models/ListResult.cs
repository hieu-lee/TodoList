using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class ListResult
    {
        public string name { get; set; }
        public bool success { get; set; }
        public string err { get; set; }
        public SortedSet<ToDoList> lists { get; set; }
        public HashSet<ToDoItem> items { get; set; }
    }
}
