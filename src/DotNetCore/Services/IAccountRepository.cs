using DotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Services
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
    }
}
