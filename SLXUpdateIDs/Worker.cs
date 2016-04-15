using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

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
        private HelpAbout helpabout;
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
            helpabout = new HelpAbout();
            code_list = prg.SetCodeList();
            keys = prg.SetNewSiteKeys(code_list);
        }

        public void RunMain()
        {
            foreach (var item in keys)
            {
                //Console.WriteLine(string.Format("Key={0}, Value={1}", item.Key, item.Value));
            }

            data.SetFields(keys);
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
        private HelpAbout help;

        //Constructor
        public ProgramFunctions()
        {
            isInitialized = true;
            system = new System();
            data = new Data();
            help = new HelpAbout();
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
            help.ShowHeader(true);
            help.ShowInstr(true);
          
            foreach (string item in code_list)
            {
                Console.WriteLine(String.Format("Replacement Sitekey for Sitekey: {0} ", item));
                string value = Console.ReadLine();
                value = value + "1234567890";
                value = value.Substring(0, 4).ToUpper();
                sitekeys.Add(item, value);
            }
            return sitekeys;
        }

    }

    class Data
    {
        private System system;
        public Data()
        {
            system = new System();
        }

        public OleDbConnection GetConnection(string ConnectString)
        {
            OleDbConnection objConn = UseUDL();
            return objConn;
        }

        public List<string> GetSqlValues(string sql)
        {
            List<string> ret = new List<string>();

            OleDbConnection objConn = UseUDL();
            try
            { 
                objConn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
                
            }
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

        public void SetFields(Dictionary<string, string> keys)
        {
            
            string codesql = @"select TABLENAME, FIELDNAME from sysdba.SECTABLEDEFS where fieldname like '%ID' or fieldname in 
('CREATEUSER','MODIFYUSER','LASTHISTORYBY','ADMINROLE','COMPLETEDUSER','BASEDON','ACTIVITYBASEDON',
'ACTIVITYTYPECODE','ORIGINAL_CREATEUSER', 'ORIGINAL_MODIFYUSER','RESOLVEDBY','SHIPPEDBY',
'STATUSCODE','SUBMITTEDBY','TYPECODE','ASSIGNEDTOID','CLOSEDBY','DEFSECCODE','FILLUSER','RECEIVEDBY',
'REQUSER','VIACODE','ASSIGNEDTO','CONTACT_EQUIVALENT','DEFAULTPUBLICACCESS','DEFECTPRODUCT','FIXEDINBUILD',
'LEAD_PROCESSED_BY','PRIMARYSITE','PRIORITYCODE','PROJECTCODE','PUBLICACCESSCODE','REQUESTEDBY',
'RESOLUTIONCODE','SERVICECODE','SEVERITYCODE','SHIPVIACODE','SITECODE'
)";

            
            OleDbConnection objConn = UseUDL();
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(codesql, objConn);
            OleDbDataReader reader = objCmd.ExecuteReader();

            foreach (var code in keys)
            {
                UpdateSitekeys(code.Key, code.Value);
            }

            while (reader.Read())
            {
                string tablename = reader[0].ToString();
                string fieldname = reader[1].ToString();

                int x = 0;
                int cnt = 0;
                if (GetSqlInt(string.Format("select isnull(max(len({0})), 0) as idlen from sysdba.{1} where {0} is not null", fieldname, tablename)) == 12)
                {
                    int z = 0;
                    foreach (var item in keys)
                    {
                        z = z + UpdateIds(tablename, fieldname, item.Key, item.Value);
                        x++;
                    }
                    //Console.WriteLine(string.Format("Updating {1}.{2}: Records Updated: {0}", z.ToString(), tablename, fieldname));
                    cnt = z;
                }
                Console.WriteLine(string.Format("Updating {0}.{1}: Records Updated: {2}", tablename, fieldname, cnt));
            }
        }

        public int UpdateIds(string tablename, string fieldname, string oldsitecode, string newsitecode)
        {

            string strsql = string.Format("update sysdba.{0} set {1} = replace({1}, '{2}', '{3}') where {1} like '%{2}%'", tablename, fieldname, oldsitecode.Trim(), newsitecode.Trim());
            int x = ExecuteSql(strsql);
            return x;
        }

        public int UpdateSitekeys(string oldsitecode, string newsitecode)
        {
            string strsql = string.Format("update sysdba.sitekeys set sitecode = '{0}' where sitecode = '{1}'", newsitecode.Trim(), oldsitecode.Trim());
            Console.WriteLine(strsql);
            int x = ExecuteSql(strsql);
            return x;
        }

        public int GetSqlInt(string sql)
        {
            OleDbConnection objConn = UseUDL();
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);

            try
            {
                int reader = int.Parse(objCmd.ExecuteScalar().ToString());
                return reader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }
        }

        public void fillExceptionsList(string Value)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            
            string file = "/ExceptionsList.txt";
            File.AppendAllText(dir + file, string.Format("TIMESTAMP: {0}", DateTime.Now.ToString())+Environment.NewLine);
            File.AppendAllText(dir+file, Value + Environment.NewLine);
        }

        public int ExecuteSql(string sql)
        {
            //Execute SQL statement - Returns number of records effected
            OleDbConnection objConn = UseUDL();
            objConn.Open();
            OleDbCommand objCmd = new OleDbCommand(sql, objConn);
            int x = 0;
            try
            {
                x = objCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                fillExceptionsList(e.ToString());
                return 0;
            }
            return x;
        }

        public OleDbConnection UseUDL()
        {
            string path = Directory.GetCurrentDirectory();
            string file = "SLXUpdateIDs.udl";
            string thedeal = string.Format("File Name = {0}\\{1}", path, file);
            OleDbConnection conn = new OleDbConnection(thedeal);
            return conn;
            
        }
    }
}
