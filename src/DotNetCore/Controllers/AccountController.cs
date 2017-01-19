using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Services;
using DotNetCore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = _accountRepository.GetAllAccounts();
            //return View(model);
            //return View("IndexPartivalView", model);
            return View("IndexViewComponent", model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.AddAccount(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }
    }
}
