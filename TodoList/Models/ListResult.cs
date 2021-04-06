using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class ListResult
    {
        public bool success { get; set; }
        public string err { get; set; }
        public HashSet<ToDoList> lists { get; set; }
        public HashSet<ToDoItem> items { get; set; }
    }
}
