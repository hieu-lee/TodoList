using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class NotificationService
    {
        private readonly TodoDbContext dbContext;
        private SessionsService sessionsService;
        public HashSet<ToDoItem> MyTodoItems = new();
        private Timer NotificationTracker = new(30 * 1000);
        private readonly string icon = $"{System.IO.Directory.GetCurrentDirectory()}/wwwroot/checkbox.png";
        public NotificationService(TodoDbContext context, SessionsService session)
        {
            sessionsService = session;
            dbContext = context;
            if (session.session.logged)
            {
                StartNotification();
            }
        }

        public void StartNotification()
        {
            var time = DateTime.Now.ToLocalTime();
            MyTodoItems = dbContext.Items.Where(s => !s.Completed && s.Owner == sessionsService.session.username && (s.TimeRemind == null || s.TimeRemind.Value >= time)).ToHashSet();
            NotificationTracker.Elapsed += (s, e) =>
            {
                var date = DateTime.Now.ToLocalTime();
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
                List<ToDoItem> OutdatedItems = new();
                List<Task> Tasks = new();
                foreach (var item in MyTodoItems)
                {
                    if (MyTodoItems.Contains(item))
                    {
                        if (item.TimeRemind is not null)
                        {
                            if (item.TimeRemind.Value == date)
                            {
                                string time = item.TimeRemind.Value.ToString();
                                var option = new NotificationOptions(item.Title, $"{time}\n{item.Content}")
                                {
                                    Icon = icon,
                                    OnClick = async () =>
                                    {
                                        var mainWindow = Electron.WindowManager.BrowserWindows.First();
                                        var uri = await mainWindow.WebContents.GetUrl();
                                        var c = 0;
                                        var base_uri = "";
                                        for (int i = 0; i < uri.Length; i++)
                                        {
                                            base_uri += uri[i];
                                            if (uri[i] == '/')
                                            {
                                                c++;
                                            }
                                            if (c == 3)
                                            {
                                                break;
                                            }
                                        }
                                        var todayUri = base_uri + "today";
                                        if (uri != todayUri)
                                        {
                                            mainWindow.LoadURL(uri);
                                        }
                                    }
                                };
                                Electron.Notification.Show(option);
                                OutdatedItems.Add(item);
                            }
                            if (item.TimeRemind.Value < date)
                            {
                                OutdatedItems.Add(item);
                            }
                        }
                        else
                        {
                            if (item.LastNotified is null || item.LastNotified.Value < DateTime.Now.Date)
                            {
                                var task = Task.Factory.StartNew(() =>
                                {
                                    var todoItem = dbContext.Items.Find(item.ItemId);
                                    todoItem.LastNotified = DateTime.Now.Date;
                                    dbContext.Items.Update(todoItem);
                                    dbContext.SaveChanges();
                                });
                                string time = "Daily";
                                var option = new NotificationOptions(item.Title, $"{time}\n{item.Content}")
                                {
                                    Icon = icon,
                                    OnClick = async () =>
                                    {
                                        var mainWindow = Electron.WindowManager.BrowserWindows.First();
                                        var uri = await mainWindow.WebContents.GetUrl();
                                        var c = 0;
                                        var base_uri = "";
                                        for (int i = 0; i < uri.Length; i++)
                                        {
                                            base_uri += uri[i];
                                            if (uri[i] == '/')
                                            {
                                                c++;
                                            }
                                            if (c == 3)
                                            {
                                                break;
                                            }
                                        }
                                        var todayUri = base_uri + "today";
                                        if (uri != todayUri)
                                        {
                                            mainWindow.LoadURL(uri);
                                        }
                                    }
                                };
                                Electron.Notification.Show(option);
                                Tasks.Add(task);
                            }
                        }
                    }
                };
                Parallel.ForEach(OutdatedItems, item =>
                {
                    MyTodoItems.Remove(item);
                });
                Parallel.ForEach(Tasks, async task =>
                {
                    await task;
                });
            };
            NotificationTracker.Start();
        }

        public void AddNewItems(HashSet<ToDoItem> NewItems)
        {
            foreach (var item in NewItems)
            {
                if (item.TimeRemind == null || item.TimeRemind.Value >= DateTime.Now.ToLocalTime())
                {
                    if (!MyTodoItems.Contains(item))
                    {
                        if (!item.Completed)
                        {
                            MyTodoItems.Add(item);
                        }
                    }
                    else
                    {
                        if (item.Completed)
                        {
                            MyTodoItems.Remove(item);
                        }
                    }
                }
            };
        }

        public void DeleteItem(ToDoItem Item)
        {
            if (MyTodoItems.Contains(Item))
            {
                MyTodoItems.Remove(Item);
            }
        }

        public void DeleteManyItem(IEnumerable<ToDoItem> Items)
        {
            Parallel.ForEach(Items, item =>
            {
                if (MyTodoItems.Contains(item))
                {
                    MyTodoItems.Remove(item);
                }
            });
        }

        public void StopNotification()
        {
            NotificationTracker.Stop();
            NotificationTracker = new(30 * 1000);
            MyTodoItems = new();
        }
    }
}
