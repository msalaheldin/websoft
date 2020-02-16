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
    [Route("api/move")]
    [ApiController]
    public class MoveController : ControllerBase
    {

        public MoveController(JsonFileAccountService accountService)
        {

            this.AccountService = accountService;

        }
        public JsonFileAccountService AccountService { get; }

        [HttpPost]
        public String MoveMoney([FromBody] MoneyTransfer moneyTransfer) {
            if (AccountService.Move(moneyTransfer.Sender, moneyTransfer.Receiver, moneyTransfer.amount)) {
                return "The amount has been successfuly moved";
            }
            return "Invalid!\nPlease make sure that the sender/receiver exist and are not the same!";
        }


    }
}