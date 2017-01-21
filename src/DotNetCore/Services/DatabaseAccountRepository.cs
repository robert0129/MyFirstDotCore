using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Models;
using DotNetCore.Data;

namespace DotNetCore.Services
{
    public class DatabaseAccountRepository : IAccountRepository
    {
        private readonly DotnetCoreDbContext _dbContext;
        public DatabaseAccountRepository(DotnetCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAccount(Account account)
        {
            try
            {
                account.uuid = Guid.NewGuid();
                account.RegisterDate = DateTime.UtcNow;
                if (_dbContext.Accounts.Max(acct => acct.Id) > 10)
                    return;
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts;
        }
    }
}
