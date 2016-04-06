using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SLXUpdateIDs
{
    class Program
    {

        static Worker worker;
        static ConsoleFunctions console;
        
        static void Main(string[] args)
        {
            worker = new Worker();
            console = new ConsoleFunctions();
            if (args.Length == 0) 
            {

                worker.RunMain();
                Console.WriteLine("");
                Console.WriteLine("Update complete!");
                console.Pause();
                
                
            }
            else
            {
                //if (worker.ValidateArgs(args)) 
                //{
                worker.RunWithArgs(args);
                console.Pause();
                //}
            }
        }
    }

}
