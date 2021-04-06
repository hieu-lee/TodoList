using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Account
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public HashSet<ToDoList> lists { get; set; } = new();

        public override bool Equals(object obj)
        {
            var other = (Account)obj;
            return username.Equals(other.username);
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }
    }
}
