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
            Console.WriteLine("Enter a Table name");
            string TableName = Console.ReadLine();
            Storage.GetTable(TableName);
            Storage.getquery("test");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
