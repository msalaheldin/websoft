using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class MoneyTransfer
    {

        public int Sender { get; set; }
        public int Receiver { get; set; }
        public int amount { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize<MoneyTransfer>(this);
    }
}
