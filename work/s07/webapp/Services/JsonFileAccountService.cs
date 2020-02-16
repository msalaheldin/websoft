using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using webapp.Models;

namespace webapp.Services
{
    public class JsonFileAccountService
    {
        public JsonFileAccountService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "..", "data", "account.json"); }
        }

        public IEnumerable<Account> GetAccounts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Account[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        public Account GetSpecAccount(int accountNumber) {
            var accounts = GetAccounts();
            foreach (var account in accounts) {
                if (account.Number == accountNumber) {
                    return account;
                        }
            }
            return null;
        }

        public bool Move(int senNum, int recevNum, int amount) {
            var accounts = GetAccounts();

            if (ValidNumber(accounts, senNum) && ValidNumber(accounts, recevNum) && (senNum != recevNum))
            {
                foreach (var account in accounts)
                {
                    if (account.Number == senNum)
                    {
                        account.Balance = account.Balance - amount;
                        Console.WriteLine("The new balance after moving: " + account.Balance
                            );
                    }
                    if (account.Number == recevNum)
                    {
                        account.Balance = account.Balance + amount;
                        Console.WriteLine("The new balance after adding: " + account.Balance
                            );
                    }
                }
                SaveAccounts(accounts);
                return true;
            }
            return false;
        }

        public bool ValidNumber(IEnumerable<Account> accounts, int num)
        {
            bool isValid = false;
            foreach (var account in accounts)
            {
                if (account.Number == num)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public void SaveAccounts(IEnumerable<Account> accounts)
        {
            String file = "../data/account.json";

            using (var outputStream = File.OpenWrite(file))
            {
                JsonSerializer.Serialize<IEnumerable<Account>>(
                    new Utf8JsonWriter(
                        outputStream,
                        new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }
                    ),
                    accounts
                );
            }
        }


    }

}

