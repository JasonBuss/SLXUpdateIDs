﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLXUpdateIDs
{
    class System
    {

        // Public Properties
        private bool isInitialized { get; set; }
        // Private Properties
        private ConsoleFunctions console;

        // Instance Constructor
        public System()
        {
            isInitialized = true;
            console = new ConsoleFunctions();
        }

        
        public string GetSettings(string key)
        {
            string ret = "";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                string result = appSettings[key] ?? "Setting Not found";
                ret = result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return ret;
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
    /*
    class HelpAbout
    {
        bool isInitialized;
        static System system;
        ConsoleFunctions console;

        public HelpAbout()
        {
            isInitialized = true;
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
            string.Format("Program Name:    Home Application"),
            string.Format("Program Author:  {0}", system.GetSetting("Author")),
            ""
        };

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

    */
    #endregion
}