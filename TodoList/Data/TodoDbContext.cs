using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ToDoList> Lists { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }
    }
}
