using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Services;
using DotNetCore.Models;

namespace DotNetCore.ViewComponents
{
    public class AccountViewComponent : ViewComponent
    {
        private readonly IAccountRepository _accountRepository;
        public AccountViewComponent(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IViewComponentResult Invoke(Account account)
        {
            return View(account);
        }
    }
}
