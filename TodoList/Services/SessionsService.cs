using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class SessionsService
    {
        private readonly TodoDbContext dbContext;
        public Session session;
        public SessionsService(TodoDbContext context)
        {
            dbContext = context;
            session = dbContext.Sessions.Find(1);
            if (session is null)
            {
                session = new();
                dbContext.Sessions.Add(session);
                dbContext.SaveChanges();
            }
        }
        
        public void SignIn(Account account)
        {
            session.logged = true;
            session.username = account.username;
            dbContext.Sessions.Update(session);
            dbContext.SaveChanges();
        }

        public void SignUp(Account account)
        {
            session.logged = true;
            session.username = account.username;
            dbContext.Sessions.Update(session);
            dbContext.SaveChanges();
        }

        public void SignOut()
        {
            session.logged = false;
            session.username = string.Empty;
            dbContext.Sessions.Update(session);
            dbContext.SaveChanges();
        }
    }
}
