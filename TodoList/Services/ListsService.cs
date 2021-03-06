using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class ListsService
    {
        private readonly TodoDbContext dbContext;
        public ListsService(TodoDbContext context)
        {
            dbContext = context;
        }

        public async Task<ListResult> GetListsAsync(string username)
        {
            var myacc = await dbContext.Accounts.FindAsync(username);
            if (myacc is null)
            {
                return new() { success = false, err = "Username not found" };
            }
            var listsArray = dbContext.Lists.Where(s => s.Owner == myacc).ToArray();
            SortedSet<ToDoList> lists = new();
            foreach (var list in listsArray)
            {
                lists.Add(list);
            }
            return new() { success = true, lists = lists };
        }

        public async Task<ListResult> GetTodayItemsAsync(string username)
        {
            var acc = await dbContext.Accounts.FindAsync(username);
            if (acc is null)
            {
                return new() { success = false, err = "username does not exist" };
            }
            var items = dbContext.Items.Where(s => (s.TimeRemind == null || s.TimeRemind.Value.Date == DateTime.Now.Date) && s.Owner == username).ToHashSet();
            return new() { success = true, items = items };
        }

        public async Task<ListResult> GetItemsFromDateAsync(string username, DateTime date)
        {
            date = date.Date;
            var acc = await dbContext.Accounts.FindAsync(username);
            if (acc is null)
            {
                return new() { success = false, err = "username does not exist" };
            }
            var items = dbContext.Items.Where(s => (s.TimeRemind == null || s.TimeRemind.Value.Date == date) && s.Owner == username).ToHashSet();
            return new() { success = true, items = items };
        }

        public async Task<ListResult> GetItemsAsync(string username, string listid)
        {
            var res = await dbContext.Lists.FindAsync(listid);
            if (res is null)
            {
                return new() { success = false, err = "List not found" };
            }
            if (res.Owner.username == username) 
            {
                var items = dbContext.Items.Where(s => s.ParentListId == listid).ToHashSet();
                return new() { success = true, items = items, name = res.Name };
            }
            return new() { success = false, err = "You cannot access or modify this list" };
        }

        public async Task CreateNewListAsync(string username, ToDoList list)
        {
            var myacc = dbContext.Accounts.Find(username);
            list.Owner = myacc;
            myacc.lists.Add(list);
            dbContext.Lists.Add(list);
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
            if (oldlist.Owner.username == username) 
            {
                oldlist.Items = list.Items;
                oldlist.TimeCreate = list.TimeCreate;
                dbContext.Lists.Update(oldlist);
                await dbContext.SaveChangesAsync();
                return new() { success = true };
            }
            return new() { success = false, err = "You cannot access or modify this list" };
        }

        public async Task UpdateTodayItemsAsync(HashSet<ToDoItem> items)
        {
            dbContext.Items.UpdateRange(items);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ListResult> DeleteListAsync(string username, string listid)
        {
            var list = dbContext.Lists.Find(listid);
            if (list is null)
            {
                return new() { success = false, err = "List not found" };
            }
            if (list.Owner.username == username)
            {
                dbContext.Items.RemoveRange(list.Items);
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

        public async Task DeleteItemAsync(ToDoItem item)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
