using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.AAA_BazaPosgresql
{
    public static class BookCRUDclass
    {
        public static string ReadFunction()
        {
            #region 
            int n = 0;
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
                string sql = $"select book.book_id,book.book_name,book.authet_fullname,bookcategory.category_name,book.price from book\r\ninner join bookcategory using(category_id)";
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
                                if(n==0)
                                {
                                    r1 += "id : ";
                                }
                                else if(n==1)
                                {
                                    r1 += " , name : ";
                                }
                                else if (n == 2)
                                {
                                    r1 += " , auther : ";
                                }
                                else if (n == 3)
                                {
                                    r1 += "category : ";
                                }
                                else if (n==4)
                                {
                                    r1 += " , price : ";
                                }
                                r1 += $"{value}\t";
                                //Console.Write($"{value}\t");
                                n++;
                            }
                            r += r1 + "\n\n\n\n";
                            r1 = "";
                            n = 0;
                            //Console.WriteLine();
                        }
                    }
                    return r;
                }
            }
            #endregion
        }

        public static void CreateFunction(string bname, string aname,int id,float price)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"insert into book(book_name,authet_fullname,category_id,price)\r\nvalues\r\n('{bname}','{aname}',{id},{price})", connection);

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

            using NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM book WHERE book_id={id}", connection);


            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }
        public static void UpdateFunction(int id, string bname, string aname, int id_c,float price)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE book SET book_name = '{bname}',authet_fullname='{aname}',category_id={id_c},price={price} WHERE book_id={id}", connection);


            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }


        public static List<object[]> ReadwithSendfileFunction()
        {
            #region 
            int n = 0;
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
                string sql = $"select book.book_id,book.book_name,book.authet_fullname,bookcategory.category_name,price from book\r\ninner join bookcategory using(category_id)";
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

                        return resultList;
                    }
                    
                }
            }
            #endregion
        }
    }
}
