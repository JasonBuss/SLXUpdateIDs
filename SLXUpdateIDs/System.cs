using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SLXUpdateIDs
{
    class System
    {

        // Private Properties
        private ConsoleFunctions console;

        // Instance Constructor
        public System()
        {
            console = new ConsoleFunctions();
        }
    }

    #region Helper Classes

    class ConsoleFunctions
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
        public void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("press ENTER to continue....");
            Console.ReadLine();
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
    
    class HelpAbout
    {
        static System system;
        ConsoleFunctions console;

        public HelpAbout()
        {
            system = new System();
            console = new ConsoleFunctions();
        }

        public string[] hlp = new string[]
        {
            //string array to contain the help file contents
            "",
            string.Format("/HELP - Displays this help file"),
            string.Format("/ABOUT - Displays About information"),
            ""
            
        };

        public string[] abt = new string[]
        {
            //string array to contain 'about' information 
            "",
            ""
        };

        public string[] header = new string[]
        {
            "",
            string.Format("********************************************************"),
            string.Format("*SLXUpdateID utility - (c) 2016 Customer FX Corporation*"),
            string.Format("********************************************************"),
            "",
            string.Format("This utility is used to update Saleslogix ID values"),
            string.Format("with new IDs based on user-defined site code values"),

            ""
        };

        public string[] instr = new string[]
        {
            "",
            string.Format("***Update Sitecode values***"),
            "",
            string.Format("Enter a new site code for each code presented.  Note - Sitecodes"),
            string.Format("must be 4 characters.  Short values will be padded with '1234' and long"),
            string.Format("values stripped after 4 characters"),
            ""
        };

        public string ShowInstr()
        {
            return ReturnString(instr);
        }

        public void ShowInstr(bool printconsole)
        {
            if (printconsole)
            {
                PrintToConsole(instr);
            }
        }

        public string ShowHeader()
        {
            return ReturnString(header);
        }

        public void ShowHeader(bool printconsole)
        {
            if (printconsole)
            {
                PrintToConsole(header);
            }
        }

        public string ShowHelp()
        {
            return ReturnString(hlp);
        }

        public void ShowHelp(bool printconsole)
        {
            if (printconsole)
            {
                PrintToConsole(hlp);
            }
        }

        public string ShowAbout()
        {
            return ReturnString(abt);
        }

        public void ShowAbout(bool printconsole)
        {
            if (printconsole)
            {
                PrintToConsole(abt);
            }
        }

        public void PrintToConsole(string[] list)
        {
            foreach (string elem in list)
            {
                console.WriteLine(elem);
            }
        }

        public string ReturnString(string[] list)
        {
            string ret = "";

            foreach (string elem in list)
            {
                ret = ret + elem + Environment.NewLine;
            }
            return ret;
        }
    }

    
    #endregion
}
