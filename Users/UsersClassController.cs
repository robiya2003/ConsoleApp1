using ConsoleApp1.Buttonsss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using ConsoleApp1.AAA_BazaPosgresql;
using Npgsql;
using ConsoleApp1.JsonDatabazas;
using Google.Protobuf;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp1.Users
{
    public static class UsersClassController
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await UsersButtonsClass.UsersFirstButton(botClient, update, cancellationToken);
            if (update.Message.Type == MessageType.Text)
            {
                string m=update.Message.Text;
                if (m == "Category📚")
                {
                    await UsersButtonsClass.CategoryUserButton(botClient, update, cancellationToken);
                }
                else if (m == "Auther👨🏻‍💼")
                {
                    await UsersButtonsClass.BookUserButton(botClient, update, cancellationToken);
                }
                else if(m == "Shop🛍")
                {
                    await UsersButtonsClass.shopCategoryUserButton(botClient, update, cancellationToken);
                    
                }
                else if (m == "My purchases📝")
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
                        string sql = $"select book.book_name,book.price,orderuser.paytype from orderuser\r\ninner join book using(book_id)\r\nwhere orderuser.user_chat_id={update.Message.Chat.Id} and tasdiqlash is not null";
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
                                        if (n == 0)
                                        {
                                            r1 += "book : ";
                                        }
                                        else if (n == 1)
                                        {
                                            r1 += " , price : ";
                                        }
                                        else if (n == 2)
                                        {
                                            r1 += " , tolov turi : ";
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
                        }
                    }
                    #endregion
                    await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text:r,
                    cancellationToken: cancellationToken);
                }
                
            }
            else
            {
                return;
            }
        }

    }
}
