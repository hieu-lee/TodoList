using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class ListsService
    {
        private TodoDbContext dbContext;
        public ListsService(TodoDbContext context)
        {
            dbContext = context;
        }

        public ListResult GetLists(string username)
        {
            var myacc = dbContext.Accounts.Find(username);
            if (myacc is null)
            {
                return new() { success = false, err = "Username not found" };
            }
            return new() { success = true, lists = myacc.lists };
        }

        public async Task<ListResult> GetItemsAsync(string username, string listid)
        {
            var res = await dbContext.Lists.FindAsync(listid);
            if (res is null)
            {
                return new() { success = false, err = "List not found" };
            }
            if (res.Owners.Contains(new(username))) 
            {
                return new() { success = true, items = res.Items };
            }
            return new() { success = false, err = "You cannot access or modify this list" };
        }

        public bool FindAccount(string username)
        {
            var acc = dbContext.Accounts.Find(username);
            return (acc is not null);
        }

        public async Task CreateNewListAsync(string username, ToDoList list)
        {
            var task = dbContext.Lists.AddAsync(list);
            var myacc = dbContext.Accounts.Find(username);
            myacc.lists.Add(list);
            await task;
            dbContext.Accounts.Update(myacc);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ListResult> UpdateListAsync(string username, ToDoList list)
        {
            var oldlist = dbContext.Lists.Find(list.ListId);
            if (oldlist is null)
            {
                return new() { success = false, err = "List not found" };
            }
            if (oldlist.Owners.Contains(new(username))) 
            {
                oldlist.Items = list.Items;
                oldlist.TimeCreate = list.TimeCreate;
                oldlist.Owners = list.Owners;
                dbContext.Lists.Update(oldlist);
                await dbContext.SaveChangesAsync();
                return new() { success = true };
            }
            return new() { success = false, err = "You cannot access or modify this list" };
        }

        public async Task<ListResult> DeleteListAsync(string username, string listid)
        {
            var list = dbContext.Lists.Find(listid);
            if (list is null)
            {
                return new() { success = false, err = "List not found" };
            }
            if (list.Owners.Contains(new(username)))
            {
                var task = dbContext.Accounts.FindAsync(username);
                dbContext.Lists.Remove(list);
                var acc = await task;
                acc.lists.Remove(list);
                dbContext.Accounts.Update(acc);
                await dbContext.SaveChangesAsync();
                return new() { success = true };
            }
            return new() { success = false, err = "You cannot access or modify this list" };
        }
    }
}
