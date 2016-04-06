using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

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
                console.Pause();
                
                
            }
            else
            {
                //if (worker.ValidateArgs(args)) 
                //{
                worker.RunWithArgs(args);
                //}
            }
        }




       /* #region Program function to me moved
        static void SetFields()
        {

            string codesql = "select TABLENAME, FIELDNAME from sysdba.SECTABLEDEFS where fieldname like '%ID'";
            OleDbConnection objConn = new OleDbConnection(strconnection);
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(codesql, objConn);
            OleDbDataReader reader = objCmd.ExecuteReader();

            while (reader.Read())
            {
                string tablename = reader[0].ToString();
                string fieldname = reader[1].ToString();

                int x = 0;
                if (GetSqlInt(string.Format("select isnull(max(len({0})), 0) as idlen from sysdba.{1} where {0} is not null", fieldname, tablename)) == 12)
                {
                    int z = 0;
                    foreach (var item in keys)
                    {
                        z = z + UpdateIds(tablename, fieldname, item.Key, item.Value);
                        x++;
                    }
                    Console.WriteLine(string.Format("Updating {1}.{2}: Records Updated: {0}", z.ToString(), tablename, fieldname));
                }
            }
        }

        static Dictionary<string, string> SetNewSiteKeys()
        {
            Dictionary<string, string> sitekeys = new Dictionary<string,string>();

            foreach (string item in code_list)
            {
                Console.WriteLine(String.Format("Replacement Sitekey for Sitekey: {0} ", item));
                sitekeys.Add(item, Console.ReadLine());
            }
            return sitekeys;
        }

        static int UpdateIds(string tablename, string fieldname, string oldsitecode, string newsitecode)
        {
            
            //string strsql = string.Format("update sysdba.{0} set {1} = replace({1}, '{2}', '{3}') where {1} like '%{2}%')", tablename, fieldname, oldsitecode.Trim(), newsitecode.Trim());
            //int x = ExecuteSql(strsql);

            //Test Code (Counts only)
            string strsql = string.Format("select count (*) from sysdba.{0} where {1} like '%{2}%'", tablename, fieldname, oldsitecode.Trim());
            //Console.WriteLine(strsql);
            int x = GetSqlInt(strsql);

            return x;
        }

        static int GetSqlInt(string sql)
        {

            OleDbConnection objConn = new OleDbConnection(strconnection);
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);

            try
            {
                int reader = int.Parse(objCmd.ExecuteScalar().ToString());
                return reader;
            }
            catch (FXSystem.Exception)
            {
                return 0;
            }
        }

        static string GetSqlValue(string sql)
        {
            //returns single item from sql
            OleDbConnection objConn = new OleDbConnection(strconnection);
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
        }

        static List<string> GetSqlValues(string sql)
        {
            //returns a generic list containing single column returned from sql 
            List<string> ret = new List<string>();

            string strprovider = strconnection;
            OleDbConnection objConn = new OleDbConnection(strprovider);
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
        }

        static int ExecuteSql(string sql)
        {
            //Execute SQL statement - Returns number of records effected
            OleDbConnection objConn = new OleDbConnection(strconnection);
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

        static bool ValidateArgs(string[] arguments)
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

        static List<string> SetCodeList()
        {
            //returns list of possible sitekeys from sitekeys table
            List<string> sitecodes = new List<string>();
            string codesql = "select distinct sitecode from sysdba.sitekeys";
            sitecodes = GetSqlValues(codesql);
            return sitecodes;
        }



        static void Test()
        {

            Console.WriteLine(" ID Reset utility");
            Console.WriteLine(" 1 - reset");
            Console.WriteLine(" 2 - update");
            Console.WriteLine(" Any other value to exit");


            string sel = Console.ReadLine();

            switch (sel)
            {
                case "1":
                    testreset();
                    break;
                case "2":
                    testupdate("ABCD", "TEST");
                    break;
                default:
                    break;
            }

        }

        static void testreset()
        {
            string strsql = "update sysdba.test set strval2 = strval";
            int result = ExecuteSql(strsql);
            Console.WriteLine(result);
        }

        static void testupdate(string searchVal, string replaceVal)
        {
            string strsql = string.Format("update sysdba.test set strval2 = replace(strval2, '{0}', '{1}') where strval2 like '%{0}%'", searchVal, replaceVal);
            int result = ExecuteSql(strsql);
            Console.WriteLine(result);
        }
         
        static void Pause()
        {
            //method to pause output
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue....");
            Console.ReadLine();
        }
        #endregion*/

    }

}
