using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public bool logged { get; set; } = false;
        public string username { get; set; } = string.Empty;
    }
}
