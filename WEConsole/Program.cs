using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEConsole.Services;

namespace WEConsole
{
    class Program
        
    {
        static void Main(string[] args)
        {
            StorageService Storage = new StorageService();
            Boolean programend = false;
            string TableName = "UNASSIGNED";
            do
            {
                Console.WriteLine("Enter 1 to open table, 2 to get the timestamp, 3 to use a query and q to quit.");
                string resp = Console.ReadLine();
                try
                {
                    switch (resp)
                    {
                        case "1":
                            Console.WriteLine("Enter a Table name");
                            TableName = Console.ReadLine();
                            Storage.GetTable(TableName);
                            break;
                        case "2":
                            Console.WriteLine("Enter the Partition key");
                            string pkeyinput = Console.ReadLine();
                            Console.WriteLine("Enter the Row key");
                            string rkeyinput = Console.ReadLine();
                            Storage.gettimestamp(TableName, pkeyinput, rkeyinput);
                            break;
                        case "3":
                            Console.WriteLine("Enter the Partition key");
                            string pkeyqueryinput = Console.ReadLine();
                            Storage.getquery(TableName,pkeyqueryinput );
                            break;
                        case "q":
                            programend = true;
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error - {e}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            } while (programend == false);
            
            //Storage.gettimestamp();
            //Storage.getquery();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
