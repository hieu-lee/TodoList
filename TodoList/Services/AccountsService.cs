using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class AccountsService
    {
        private TodoDbContext dbContext;
        private EncryptionAndCompressService encryptionService;
        private SessionsService sessionsService;
        public AccountsService(TodoDbContext context, EncryptionAndCompressService encrypt, SessionsService sessionsService)
        {
            this.sessionsService = sessionsService;
            dbContext = context;
            encryptionService = encrypt;
        }

        public SignResult SignUp(Account account)
        {
            try
            {
                dbContext.Accounts.Add(account);
                sessionsService.SignUp(account);
                return new() { success = true };
            }
            catch (Exception)
            {
                return new() { success = false, err = "Your username has been taken" };
            }
        }

        public SignResult SignIn(Account account)
        {
            var acc = dbContext.Accounts.Find(account.username);
            if (acc is null)
            {
                return new() { success = false, err = "Your username does not exist" };
            }
            if (acc.password != encryptionService.Encrypt(account.password))
            {
                return new() { success = false, err = "Your password is incorrect" };
            }
            sessionsService.SignIn(account);
            return new() { success = true };
        }

        public void SignOut()
        {
            sessionsService.SignOut();
        }
    }
}
