using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using db_test;

namespace db_test
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase Itarill = new DataBase("itarill.ddns.net", "peet", "Test", "3306", "szoftech");

            MySqlConnection conn = new MySqlConnection(Itarill.GetConnectionString());

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            string price = Console.ReadLine();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                // Perform database operations

                string sql_insert = string.Format("INSERT Into Test Values('NULL', '{0}', '{1}');", name, price);
                MySqlCommand cmd_insert = new MySqlCommand(sql_insert, conn);
                cmd_insert.ExecuteNonQuery();

                string sql = "SELECT * FROM Test;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
                }
                rdr.Close();

            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Number);
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Press enter to exit...");

            Console.ReadLine();

        }
    }
}
