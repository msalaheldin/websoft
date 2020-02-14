using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            var accounts = new List<Account>(ReadAccounts());
            
          
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1) View accounts");
            Console.WriteLine("2) View account by number");
            Console.WriteLine("3) Search");
            Console.WriteLine("4) Move");
            Console.WriteLine("5) New Account");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    var t = new TablePrinter("Number", "Balance", "Label", "Owner");
                    foreach (var account in accounts)
                    {

                        t.AddRow(Convert.ToString(account.Number), Convert.ToString(account.Balance),
                            Convert.ToString(account.Label), Convert.ToString(account.Owner));
                    }
                    t.Print();
                    return true;
                case "2":
                    Console.Write("Please provide the account number: ");
                    int accountNumber = Convert.ToInt32(Console.ReadLine());
                    foreach(var account in accounts)
                    {
                        if (account.Number == accountNumber)
                        {
                             t = new TablePrinter("Number", "Balance", "Label", "Owner");
                            t.AddRow(Convert.ToString(account.Number), Convert.ToString(account.Balance),
                            Convert.ToString(account.Label), Convert.ToString(account.Owner));
                            t.Print();
                        }
                        
                    }
                    return true;

                case "3":
                    Console.Write(" Search: ");
                    String value = Console.ReadLine();
                    t = new TablePrinter("Number", "Balance", "Label", "Owner");
                    foreach (var account in accounts) {
                        if (account.ToString().Contains(value,StringComparison.OrdinalIgnoreCase)) {
                            t.AddRow(Convert.ToString(account.Number), Convert.ToString(account.Balance),
                            Convert.ToString(account.Label), Convert.ToString(account.Owner));
                            
                        }
                    }
                    t.Print();
                    return true;
                case "4":
                    Console.Write("Please specify the sender's account number: ");
                    int senNum = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Please specify the receiver's account number: ");
                    int recevNum = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Please specify the amount to be moved: ");
                    int amount = Convert.ToInt32(Console.ReadLine());

                    if (ValidNumber(accounts, senNum) && ValidNumber(accounts, recevNum)&&(senNum!=recevNum))
                    {
                        foreach(var account in accounts)
                        {
                            if (account.Number == senNum) {
                                      account.Balance = account.Balance - amount;
                                Console.WriteLine("The new balance after moving: " + account.Balance
                                    );

                            }
                            if(account.Number == recevNum) {
                                account.Balance = account.Balance + amount;
                                Console.WriteLine("The new balance after adding: " + account.Balance
                                    );
                            }
                        }
                        SaveAccounts(accounts);

                    }
                    else { Console.WriteLine("Invalid! Make sure that the sender and the receiver exist and it is not the same number"); 
                    }

                    return true;
                case "5":
                    Account newAcc = new Account();
                    Console.Write("Specify account number: ");
                    newAcc.Number = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Specify account balance: ");
                    newAcc.Balance = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Specify account label: ");
                    newAcc.Label = Console.ReadLine();
                    Console.Write("Specify account owner: ");
                    newAcc.Owner = Convert.ToInt32(Console.ReadLine());

                    accounts.Add(newAcc);

                    
                    SaveAccounts(accounts);
                    Console.WriteLine("A new account has been added successfully");


                   
                    return true;
                case "6":
                    Console.WriteLine("Bye!");
                    return false;
                default:
                    return true;
            }
        }
        static bool ValidNumber(IEnumerable<Account> accounts, int num) {
            bool isValid = false;
            foreach (var account in accounts) {
                if (account.Number == num)
                {
                    isValid = true;
                }
            }
            
            return isValid;
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
                    new JsonSerializerOptions
                    {
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

    class Account
    {
        public int Number { get; set; }
        public int Balance { get; set; }
        public string Label { get; set; }
        public int Owner { get; set; }



        public override string ToString()
        {
            return JsonSerializer.Serialize<Account>(this);
        }
    }

    public class TablePrinter
    {
        private readonly string[] titles;
        private readonly List<int> lengths;
        private readonly List<string[]> rows = new List<string[]>();

        public TablePrinter(params string[] titles)
        {
            this.titles = titles;
            lengths = titles.Select(t => t.Length).ToList();
        }

        public void AddRow(params object[] row)
        {
            if (row.Length != titles.Length)
            {
                throw new System.Exception($"Added row length [{row.Length}] is not equal to title row length [{titles.Length}]");
            }
            rows.Add(row.Select(o => o.ToString()).ToArray());
            for (int i = 0; i < titles.Length; i++)
            {
                if (rows.Last()[i].Length > lengths[i])
                {
                    lengths[i] = rows.Last()[i].Length;
                }
            }
        }

        public void Print()
        {
            lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            System.Console.WriteLine("+");

            string line = "";
            for (int i = 0; i < titles.Length; i++)
            {
                line += "| " + titles[i].PadRight(lengths[i]) + ' ';
            }
            System.Console.WriteLine(line + "|");

            lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            System.Console.WriteLine("+");

            foreach (var row in rows)
            {
                line = "";
                for (int i = 0; i < row.Length; i++)
                {
                    if (int.TryParse(row[i], out int n))
                    {
                        line += "| " + row[i].PadLeft(lengths[i]) + ' ';  // numbers are padded to the left
                    }
                    else
                    {
                        line += "| " + row[i].PadRight(lengths[i]) + ' ';
                    }
                }
                System.Console.WriteLine(line + "|");
            }

            lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            System.Console.WriteLine("+");
        }
    }


}
