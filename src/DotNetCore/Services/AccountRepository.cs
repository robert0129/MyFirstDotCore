using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Models;

namespace DotNetCore.Services
{
    public class AccountRepository : IAccountRepository
    {
        private List<Account> _accounts = new List<Account>
        {
            new Account { id = 0, username = "Robert", email = "robert.chen@robert.com", phone="0800-000-123", password = "1234567890"},
            new Account { id = 1, username = "Robert2", email = "robert2.chen@robert.com", phone="0800-000-123", password = "1234567890"},
            new Account { id = 2, username = "Robert3", email = "robert3.chen@robert.com", phone="0800-000-123", password = "1234567890"},
        };

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accounts;
        }

        public void AddAccount(Account account)
        {
            account.id = _accounts.Max(a => a.id) + 1;
            _accounts.Add(account);
        }
    }
}
