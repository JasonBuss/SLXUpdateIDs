using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLXUpdateIDs
{

    class Worker
    {
        static Main main = new Main();
        public bool ValidateArgs(string[] arguments)
        {
            bool ret = true;
            int x = 0;
            while (x < arguments.Count())
            {
                string value = arguments[x];
                if (!ValidateArg(value)) { ret = false; }
                x++;
            }

            if (ret == false) { Console.WriteLine(" Invalid Arguments - Exiting"); }
            return ret;
        }

        static bool ValidateArg(string argument)
        {
            //Individual argument checks
            bool ret = true;

            if (argument[0].ToString() != "/") { ret = false; } //arguments must start with forward slash
            return ret;
        }
    }

    class Messages
    {
        static void Help()
        {
            Console.WriteLine("");
            Console.WriteLine("Here is the help for this utility.");
            Console.WriteLine("Here is the help for this utility.");
            Console.WriteLine("Here is the help for this utility.");
            Console.WriteLine("Here is the help for this utility.");
            Console.WriteLine("Here is the help for this utility.");
            Console.WriteLine("Here is the help for this utility.");
            Console.Write("This is a");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Red");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Word.");
        }
    }


}
