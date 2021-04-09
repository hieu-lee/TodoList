using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Username
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public string ListId { get; set; }

        public Username(string username)
        {
            this.username = username;
        }
        public override bool Equals(object obj)
        {
            var other = (Username)obj;
            return username.Equals(other.username);
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }
    }
}
