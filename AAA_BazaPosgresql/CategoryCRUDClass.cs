using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.AAA_BazaPosgresql
{
    public static class CategoryCRUDClass
    {
        public static string ReadFunction()
        {
            #region
            string r = "";
            string r1 = "";
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM bookcategory order by category_id";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<object[]> resultList = new List<object[]>();
                        while (reader.Read())
                        {
                            object[] values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            resultList.Add(values);
                        }
                        foreach (var row in resultList)
                        {
                            foreach (var value in row)
                            {
                                r1 += $"{value}\t";
                                //Console.Write($"{value}\t");
                            }
                            r += r1 + "\n";
                            r1 = "";
                            //Console.WriteLine();
                        }
                    }
                    return r;
                }
            }
            #endregion
        }
        public static void CreateFunction(string name)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO bookcategory (category_name) VALUES ('{name}')", connection);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }
        public static void DeleteFunction(int id)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM bookcategory WHERE category_id={id}", connection);


            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }
        public static void UpdateFunction(int id,string name) 
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE bookcategory SET category_name = '{name}' WHERE category_id={id}", connection);


            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }
    }
}
