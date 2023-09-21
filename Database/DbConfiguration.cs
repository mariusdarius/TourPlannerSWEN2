using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbConfiguration
    {
        public DbConfiguration()
        {
            
        }

        public static bool TestConnection()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected to the database.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Unable to connect to the database.");
                    return false;
                }
            }
        }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5434;User Id=postgres;Password=dario;Database=tourplannerdb;");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool isConnected = DbConfiguration.TestConnection();

            if (isConnected)
            {
                Console.WriteLine("Connected to the database successfully.");
            }
            else
            {
                Console.WriteLine("Failed to connect to the database.");
            }

            Console.ReadKey();
        }
    }
}