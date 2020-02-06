using System;
using System.IO;
using System.Text.Json.Serialization;

namespace sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            String file = "../data/bank.json";
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                Console.WriteLine(json);
                
                // List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                dynamic items = JsonConvert.DeserializeObject(json);
                foreach(var item in items)
                {
                    Console.WriteLine("{0} {1}", item.temp, item.vcc);
                }
            }
        }
    }

//
// How to read and write a JSON file
//
// make menu driven console program
//
// Read input from user
//
// Read and use command line arguments
//

    // public class Item
    // {
    //     public int millis;
    //     public string stamp;
    //     public DateTime datetime;
    //     public string light;
    //     public float temp;
    //     public float vcc;
    // }
}
