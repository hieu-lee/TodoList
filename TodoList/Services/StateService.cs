using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services
{
    public class StateService
    {
        public HashSet<Username> Owners { get; set; } = new();
    }
}
