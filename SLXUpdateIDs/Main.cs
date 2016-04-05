using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;

namespace SLXUpdateIDs
{
    public class Work
    {
        public ConsoleFunctions console = new ConsoleFunctions();

        public string GetSetting(string key)
        {
            string ret = "None";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                string result = appSettings[key] ?? "Setting Not found";
                ret = result;
            }
            catch (ConfigurationErrorsException)
            {
                console.WriteLine("Error reading app settings");
            }
            return ret;
        }

        public void testfunction()
        {
            Data home = new Data();

            OleDbConnection con = home.GetDatabaseConnection("Personal");

            string retvalue;

            console.WriteLine("Enter SQL Statement");
            try
            {
                retvalue = home.executeSql(con, console.ReadLine());
            }
            catch (Exception e)
            {
                retvalue = e.ToString();
            }
            console.WriteLine(String.Format("RETURNED = {0}", retvalue));
        }

        #region Arguments
        /* Functions to validate arguments passed to application */
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

            if (ret == false) { console.WriteLine(" Invalid Arguments - Exiting"); }
            return ret;
        }

        static bool ValidateArg(string argument)
        {
            //Individual argument checks
            bool ret = true;

            if (argument[0].ToString() != "/") { ret = false; } //arguments must start with forward slash

            return ret;
        }
        #endregion

        #region Help/About

        public string showHelp(bool printToConsole)
        {
            HelpAbout hlp = new HelpAbout();
            string ret = null;
            if (printToConsole)
            {
                hlp.ShowHelp(true);
            }
            else
            {
                ret = hlp.ShowHelp();
            }
            return ret;
        }

        public string showAbout(bool PrintToConsole)
        {
            HelpAbout hlp = new HelpAbout();
            string ret = null;
            if (PrintToConsole)
            {
                hlp.ShowAbout(true);
            }
            else
            {
                ret = hlp.ShowAbout();
            }
            return ret;
        }

        #endregion

    }

    public class Data
    {
        static Worker work = new Worker();
        static ConsoleFunctions console = new ConsoleFunctions();

        public OleDbConnection GetDatabaseConnection(string database)
        {
            string server = work.GetSetting("Server");
            string username = work.GetSetting("Username");
            string password = work.GetSetting("Password");

            string constr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", password, username, database, server);
            console.WriteLine(constr);
            OleDbConnection conn = new OleDbConnection(constr);
            return conn;
        }

        public string executeSql(OleDbConnection conn, string sql)
        {

            conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            string ret;

            try
            {
                ret = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception e)
            {
                ret = e.ToString();
            }
            return ret;
        }



    }

    #region Helper Classes
    public class ConsoleFunctions
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
    public class HelpAbout
    {
        static Work work = new Work();
        static ConsoleFunctions con = new ConsoleFunctions();

        static string[] hlp = new string[]
        {
            //string array to contain the help file contents
            "",
            string.Format("/HELP - Displays this help file"),
            string.Format("/ABOUT - Displays About information"),
            ""
        };

        static string[] abt = new string[]
        {
            //string array to contain 'about' information 
            "",
            string.Format("Program Name:    Home Application"),
            string.Format("Program Author:  {0}", work.GetSetting("Author")),
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
                con.WriteLine(elem);
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
