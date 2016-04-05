using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;

namespace SLXUpdateIDs
{
    public class Main
    {
        
        Worker worker;
        public Main()
        {
            Work work = new Work();
        }

        public string GetSettings(string key)
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
                Console.WriteLine("Error reading app settings");
            }
            return ret;
        }

        public string GetConnectionStr()
        {
            string username = GetSettings("Username");
            string password = GetSettings("Password");
            string server = GetSettings("Server");
            string database = GetSettings("Database");

            string connectionstr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", password, username, database, server);
            return connectionstr;
        }

    }
    public class Work
    {
        static ConsoleFunctions console = new ConsoleFunctions();



       

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

        /*
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
        */
    }

    /*
    public class Sql
    {
        Work work = new Work();

        System system = new System();
        
        public string GetConnection()
        {
            
            string username = system.GetSetting("Username");
            string password = system.GetSetting("Password");
            string server = system.GetSetting("Server");
            string database = system.GetSetting("Database");

            string con = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", password, username, database, server);

            return "";
        }
 
        /*static int GetSqlInt(string sql)
        {

            OleDbConnection objConn = new OleDbConnection(GetConnection());
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);

            try
            {
                int reader = int.Parse(objCmd.ExecuteScalar().ToString());
                return reader;
            }
            catch (Exception e)
            {
                return 0;
            }
        }*/

        /*static string GetSqlValue(string sql)
        {
            //returns single item from sql
            OleDbConnection objConn = new OleDbConnection(GetConnection());
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);

            try
            {
                string reader = objCmd.ExecuteScalar().ToString();
                return reader;
            }
            catch (FXSystem.Exception e)
            {
                return e.Message;
            }
        }*/

        /*static List<string> GetSqlValues(string sql)
        {
            //returns a generic list containing single column returned from sql 
            List<string> ret = new List<string>();

            OleDbConnection objConn = new OleDbConnection(GetConnection());
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);

            try
            {
                OleDbDataReader reader = objCmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(reader[0].ToString());
                }
            }
            catch (FXSystem.Exception e)
            {
                ret.Add(e.Message);
            }
            return ret;
        }*/

        /*static int ExecuteSql(string sql)
        {
            //Execute SQL statement - Returns number of records effected
            OleDbConnection objConn = new OleDbConnection(GetConnection());
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);
            Console.WriteLine(sql);
            int x = 0;
            try
            {
                x = objCmd.ExecuteNonQuery();
            }
            catch (FXSystem.Exception)
            {
                return 0;
            }
            return x;
        }
    }*/
}
