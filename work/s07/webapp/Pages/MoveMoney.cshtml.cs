using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using webapp.Models;
using webapp.Services;

namespace webapp.Pages
{
    public class MoveMoneyModel : PageModel
    {
        private readonly ILogger<MoveMoneyModel> _logger;
        public JsonFileAccountService AccountService;

        public MoveMoneyModel(ILogger<MoveMoneyModel> logger, 
            JsonFileAccountService accountService
            )
        {
            _logger = logger;
            AccountService = accountService;
        }

        public IActionResult OnPost(IFormCollection collection) {
            int sender = Convert.ToInt32(collection["sender"]);
            int receiver = Convert.ToInt32(collection["receiver"]);
            int amount = Convert.ToInt32(collection["amount"]);

            if (AccountService.Move(sender, receiver, amount))
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("Error");
            }
        }

    }
}
