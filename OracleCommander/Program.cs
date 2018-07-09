using System;
using NDesk.Options;
using Oracle.ManagedDataAccess.Client;

namespace OracleCommander
{
    public class Program
    {
        private static string User = null;
        private static string Password = null;
        private static string Hostname = null;
        private static int Port = 1521;
        private static string Instance = null;
        private static string Command = null;

        public static void Main(string[] args)
        {



            OptionSet opts = new OptionSet()
            .Add("u|user=", u => User = u)
            .Add("password=", v => Password = v)
            .Add("h|hostname=", h => Hostname = h)
            .Add("p|port=", (int p) => Port = p)
            .Add("i|instance=", i => Instance = i)
            .Add("c|command=", c => Command = c);

            try
            {
                opts.Parse(args);
             }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                
                string conString = "User Id={0};Password={1};Data Source={2}:{3}/{4}";
                Console.WriteLine(string.Format("Trying connection: " + conString, User, Password, Hostname, Port, Instance));
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = Command;

                //Execute the command 
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }
                Console.ReadLine();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

                   }

    }
}