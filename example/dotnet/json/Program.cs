/**
 * About working with JSON data in files
 * https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview
 */
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic ;

namespace jsonBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var accounts = ReadAccounts();

            foreach (var account in accounts) {
                Console.WriteLine(account);
                Console.WriteLine(account.Balance);
                //account.Balance = 1;
            }

            SaveAccounts(accounts);
        }

        static IEnumerable<Account> ReadAccounts()
        {
            String file = "../data/account.json";

            using (StreamReader r = new StreamReader(file))
            {
                string data = r.ReadToEnd();
                // Console.WriteLine(data);

                var json = JsonSerializer.Deserialize<Account[]>(
                    data,
                    new JsonSerializerOptions {
                        PropertyNameCaseInsensitive = true
                    }
                );

                //Console.WriteLine(json[0]);
                return json;
            }
        }

        static void SaveAccounts(IEnumerable<Account> accounts)
        {
            String file = "../data/account.json";

            using (var outputStream = File.OpenWrite(file))
            {
                JsonSerializer.Serialize<IEnumerable<Account>>(
                    new Utf8JsonWriter (
                        outputStream,
                        new JsonWriterOptions {
                            SkipValidation = true,
                            Indented = true
                        }
                    ),
                    accounts
                );
            }
        }
    }

    public class Account
    {
        public int Number { get; set; }
        public int Balance { get; set; }
        public string Label { get; set; }
        public int Owner { get; set; }
        
        public override string ToString() {
            return JsonSerializer.Serialize<Account>(this);
        }
    }
}
