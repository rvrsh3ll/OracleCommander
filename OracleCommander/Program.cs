using System;
using CommandLine;
using Oracle.ManagedDataAccess.Client;

namespace OracleCommander
{
    internal class Program
    {
        class Options
        {
            [Option(Default = false, HelpText = "User to connect as.")]
            public string User { get; set; }

            [Option(Default = false, HelpText = "Password.")]
            public string Password { get; set; }

            [Option(Default = false, HelpText = "Hostname.")]
            public string Hostname { get; set; }

            [Option(Default = false, HelpText = "Port.")]
            public string Port { get; set; }

            [Option(Default = false, HelpText = "Instance name.")]
            public string Instance { get; set; }

        }



        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args);
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
                

            var options = new Options();
            
            if (options == null)
            {
                Console.WriteLine("Hostname Required.");
                return;
            }
            try
            {
                string conString = "\"User Id=\" + options.User + \";\" + \"password=\" + options.Password + \";\" + \"Data Source=\" + options.Hostname + \";\" + options.port + \"/\" + options.instance + \";\" + \"Pooling = false;\"";

                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "dummy command";

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