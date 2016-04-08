using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using db_test;

namespace db_test
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 0;

            DataBase db = new DataBase("hubasky.ddns.net", "peet", "HP_TEST", "3306", "szoftech");

            using (MySqlConnection conn = new MySqlConnection(db.GetConnectionString()))
            {
                conn.Open();

                using (var context = new TestDb(conn, false))
                {
                    var persons = context.Persons.ToList();
                    foreach (Person person in persons)
                    {
                        Console.WriteLine("{0}, {1}, {2}", person.Id, person.Name, person.Phone);
                        id = person.Id + 1;
                    }

                    Person newPerson = new Person();
                    newPerson.Id = id;
                    Console.Write("Név: ");
                    newPerson.Name = Console.ReadLine();
                    Console.Write("Tel: ");
                    newPerson.Phone = Console.ReadLine();

                    context.Persons.Add(newPerson);
                    context.SaveChanges();


                }

                conn.Close();
            }

            Console.WriteLine("Nyomj egy billentyűt a kilépéshez.");
            Console.ReadKey();

        }

        public void noEntity()
        {

            DataBase Itarill = new DataBase("hubasky.ddns.net", "peet", "Test", "3306", "szoftech");

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
        }
    }
}
