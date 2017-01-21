using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Models;

namespace DotNetCore.Services
{
    public class AccountRepository : IAccountRepository
    {
        private List<Account> _accounts = new List<Account> {};
        
        public IEnumerable<Account> GetAllAccounts()
        {
            return _accounts;
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }
    }
}
