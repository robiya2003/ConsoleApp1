using ConsoleApp1.AAA_BazaPosgresql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Npgsql;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ConsoleApp1.Users
{
    internal class UsersCallBackQueryCheck
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var t = update.CallbackQuery.Data.ToString();
            if (t.StartsWith("category#"))
            {
                string[] category = t.Split('#');
                await UsersButtonsClass.CategoryBookUserButton(botClient, update, cancellationToken, category[1]);
            }
            else if (t.StartsWith("auther#"))
            {
                string[] auther = t.Split('#');
                await UsersButtonsClass.AutherBookUserButton(botClient, update, cancellationToken, auther[1]);
            }
            else if (t.StartsWith("categoryshop#"))
            {
                string[] categoryshop = t.Split('#');
                await UsersButtonsClass.ShopCategoryBookUserButton(botClient, update, cancellationToken, categoryshop[1]);
            }
            else if (t.StartsWith("shopbook#"))
            {
                string[] bookshop = t.Split('#');
                await UsersButtonsClass.PytypeBookUserButton(botClient, update, cancellationToken);

                ShopClassDataBaza.ChatIdWriting_Function(update.CallbackQuery.From.Id, update.CallbackQuery.From.FirstName, bookshop[2]);


            }
            else if (t.StartsWith("shoppaytype#"))
            {
                string[] bookshop = t.Split('#');
                Console.WriteLine(bookshop[1]);
                ShopClassDataBaza.PytypeIdWriting_Function(update.CallbackQuery.From.Id, bookshop[1]);

                await UsersButtonsClass.Tasdiqlash(botClient, update, cancellationToken);
            }
            else if (t.StartsWith("tasdiqlash"))
            {
                #region
                string connectionString = "Host=localhost;" +
                    "Port=5432;" +
                    "Database=onlinemarketexam;" +
                    "User Id=postgres;" +
                    "Password=dfrt43i0;";


                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();

                using NpgsqlCommand cmd = new NpgsqlCommand($"\r\nUPDATE ORDERUSER\r\nSET tasdiqlash = true \r\nWHERE user_chat_id={update.CallbackQuery.From.Id} and tasdiqlash is null"
                    , connection);

                int rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();
                #endregion
            }
            else
            {
                return;
            }


        }
    }
}
