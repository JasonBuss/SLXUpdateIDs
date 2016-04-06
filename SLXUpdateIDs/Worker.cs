﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace SLXUpdateIDs
{
    class Worker
    {
        //Private Properties
        private bool isInitialized { get; set; }
        private System system;
        private Data data;
        private ProgramFunctions prg;
        private ConsoleFunctions console;
        static List<string> code_list;
        public Dictionary<string, string> keys;

        //Instance Constructor
        public Worker()
        {
            isInitialized = true;
            system = new System();
            prg = new ProgramFunctions();
            data = new Data();
            console = new ConsoleFunctions();
            code_list = prg.SetCodeList();
            keys = prg.SetNewSiteKeys(code_list);
        }

        public void RunMain()
        {
            foreach (var item in keys)
            {
                Console.WriteLine(string.Format("Key={0}, Value={1}", item.Key, item.Value));
            }
        }

        public void RunWithArgs(string[] arguments)
        {
            int x = 0;
            while (x < arguments.Count())
            {
                string cleanarg = arguments[x].Substring(1, (arguments[x].Length - 1));
                switch (cleanarg.ToUpper())
                {
                    case "TEST":
                        break;
                    case "HELP":
                        break;
                    default:
                        Console.WriteLine("Unknown parameter");
                        break;
                }
                x++;
            }
            console.Pause();
        }
    }

    class ProgramFunctions
    {
        //Private Properties
        private bool isInitialized { get; set; }
        private System system;
        private Data data;

        //Constructor
        public ProgramFunctions()
        {
            isInitialized = true;
            system = new System();
            data = new Data();
        }

        public List<string> SetCodeList()
        {
            List<string> sitecodes = new List<string>();
            string codesql = "select distinct sitecode from sysdba.SiteKeys";
            sitecodes = data.GetSqlValues(codesql);
            return sitecodes;
        }

        public Dictionary<string, string> SetNewSiteKeys(List<string> code_list)
        {
            Dictionary<string, string> sitekeys = new Dictionary<string, string>();
          
            foreach (string item in code_list)
            {
                Console.WriteLine(String.Format("Replacement Sitekey for Sitekey: {0} ", item));
                string value = Console.ReadLine();
                if (checkcode(value))
                {
                    sitekeys.Add(item, Console.ReadLine());
                }
            }

            return sitekeys;

        }

        public bool checkcode(string code)
        {
            bool ret = false;
            if (code.Length != 4)
            {
                Console.WriteLine("New Code values must be 4 characters.");
                checkcode(code);
            }
            else
            {
                ret = true;
            }
            return ret;
        }
    }

    class Data
    {
        private bool isInitialized { get; set; }
        private System system;
        private string strConnection;

        public Data()
        {
            system = new System();
            strConnection = GetConnectionStr();
        }

        public string GetConnectionStr()
        {
            string username = system.GetSettings("Username");
            string password = system.GetSettings("Password");
            string server = system.GetSettings("Server");
            string database = system.GetSettings("Database");

            string connectionstr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", password, username, database, server);
            return connectionstr;
        }

        public OleDbConnection GetConnection(string ConnectString)
        {
            OleDbConnection objConn = new OleDbConnection(ConnectString);
            return objConn;
        }

        public List<string> GetSqlValues(string sql)
        {
            //returns a generic list containing single column returned from sql 
            List<string> ret = new List<string>();

            string strprovider = strConnection;
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
            catch (Exception e)
            {
                ret.Add(e.Message);
            }
            return ret;
        }

        public void SetFields()
        {
            
            string codesql = "select TABLENAME, FIELDNAME from sysdba.SECTABLEDEFS where fieldname like '%ID'";
            OleDbConnection objConn = new OleDbConnection(strConnection);
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

        public int GetSqlInt(string sql)
        {

            OleDbConnection objConn = new OleDbConnection(strConnection);
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
        }

        
    }
}
