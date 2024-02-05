using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ConsoleApp1.AAA_BazaPosgresql
{
    public static class ShopClassDataBaza
    {
        public static void ChatIdWriting_Function(long id,string name,string book_name)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"insert into orderuser (user_chat_id,user_fullname,book_id)" +
                $"values ({id},'{name}',(select book_id from book where book_name='{book_name}'  limit 1))", connection);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }
        public static void PytypeIdWriting_Function(long id,string p)
        {
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";


            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand($"\r\nUPDATE ORDERUSER\r\nSET paytype = '{p}'\r\nWHERE user_chat_id={id} and paytype is null" 
                , connection);

            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            #endregion
        }






    }
}
