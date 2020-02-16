using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp.Services;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("api/accounts/{number}")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        public NumberController(JsonFileAccountService accountService) {

            this.AccountService = accountService;

        }
        public JsonFileAccountService AccountService { get; }

        [HttpGet]
        public String GetAccount( int number) {
            Account account = AccountService.GetSpecAccount(number);
            if (account != null) {
                return account.ToString();
            }
            return "Account doesn't exist!";
        }


    }
}