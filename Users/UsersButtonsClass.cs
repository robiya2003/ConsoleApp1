using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using Npgsql;
using Google.Protobuf.WellKnownTypes;
using iText.Forms.Form.Element;

namespace ConsoleApp1.Users
{
    public static class UsersButtonsClass
    {
        public static async Task UsersFirstButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            var buttons = new List<List<KeyboardButton>>();

            var buttonsgorizontal1 = new List<KeyboardButton>();
            buttonsgorizontal1.Add(new KeyboardButton("Category📚"));
            buttonsgorizontal1.Add(new KeyboardButton("Auther👨🏻‍💼"));

            var buttonsgorizontal2 = new List<KeyboardButton>();
            buttonsgorizontal2.Add(new KeyboardButton("Shop🛍"));
            buttonsgorizontal2.Add(new KeyboardButton("My purchases📝"));
            buttons.Add(buttonsgorizontal1);
            buttons.Add(buttonsgorizontal2);


            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Choose a response",
                replyMarkup: new ReplyKeyboardMarkup(buttons),
                cancellationToken: cancellationToken);
            #endregion
        }


        public static async Task CategoryUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            int n = 0;
            string strId = "";
            string strcategory = "";

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
                            button1 = new List<InlineKeyboardButton>();
                            strId = "";
                            strcategory = "";
                            n = 0;
                            foreach (var value in row)
                            {
                                if (n == 0)
                                {
                                    strId = value.ToString();
                                }
                                else
                                {
                                    strcategory = value.ToString();
                                }
                                n++;
                            }
                            button1.Add(InlineKeyboardButton.WithCallbackData(text: strcategory, callbackData: "category#" + strId));
                            buttons.Add(button1);

                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Categorylardan birini tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }



        public static async Task BookUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            

            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT authet_fullname FROM book group by authet_fullname";
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
                            button1 = new List<InlineKeyboardButton>();
                            
                            foreach (var value in row)
                            {
                                button1.Add(InlineKeyboardButton.WithCallbackData(text: value.ToString(), callbackData: "auther#" + value.ToString()));
                                buttons.Add(button1);
                            }
                            

                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Kitoblar royhati",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }


        public static async Task CategoryBookUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken,string c)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            string a = "";
            string b = "";
            string p = "";
            int n = 0;

            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT book_name,authet_fullname,price FROM book where " +
                    $"\r\ncategory_id={int.Parse(c)}";
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
                            n = 0;
                            button1 = new List<InlineKeyboardButton>();

                            foreach (var value in row)
                            {
                                if(n== 0)
                                {
                                    b=value.ToString();
                                }
                                else if(n== 1) 
                                {
                                    a= value.ToString();
                                }
                                else
                                {
                                    p = value.ToString();
                                }
                                n++;
                            }

                            button1.Add(InlineKeyboardButton.WithCallbackData(text: a+" "+b+"->"+p, callbackData: a+"#"+b));
                            buttons.Add(button1);
                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "Kitoblar ro'yhati narxlar so'mda ",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }
        public static async Task AutherBookUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string c)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            string a = "";
            string b = "";
            int n = 0;

            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT book_name,price FROM book where authet_fullname='{c}' ";
             
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
                            n = 0;
                            button1 = new List<InlineKeyboardButton>();

                            foreach (var value in row)
                            {
                                if (n == 0)
                                {
                                    b = value.ToString();
                                }
                                else
                                {
                                    a = value.ToString();
                                }
                                n++;
                            }

                            button1.Add(InlineKeyboardButton.WithCallbackData(text: b + "-> " + a, callbackData: b + "#" + a));
                            buttons.Add(button1);
                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "Amallardan birini tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }
        //////////////////////////////////////////////////////////////////////////////
        public static async Task shopCategoryUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            int n = 0;
            string strId = "";
            string strcategory = "";

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
                            button1 = new List<InlineKeyboardButton>();
                            strId = "";
                            strcategory = "";
                            n = 0;
                            foreach (var value in row)
                            {
                                if (n == 0)
                                {
                                    strId = value.ToString();
                                }
                                else
                                {
                                    strcategory = value.ToString();
                                }
                                n++;
                            }
                            button1.Add(InlineKeyboardButton.WithCallbackData(text: strcategory, callbackData: "categoryshop#" + strId));
                            buttons.Add(button1);

                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Qaysi categorydan tanlamoqchisiz",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }
        public static async Task ShopCategoryBookUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string c)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            string a = "";
            string b = "";
            string p = "";
            int n = 0;

            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT book_name,authet_fullname,price FROM book where " +
                    $"\r\ncategory_id={int.Parse(c)}";
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
                            n = 0;
                            button1 = new List<InlineKeyboardButton>();

                            foreach (var value in row)
                            {
                                if (n == 0)
                                {
                                    b = value.ToString();
                                }
                                else if (n == 1)
                                {
                                    a = value.ToString();
                                }
                                else
                                {
                                    p = value.ToString();
                                }
                                n++;
                            }

                            button1.Add(InlineKeyboardButton.WithCallbackData(text: a + " " + b+"->"+p, callbackData: "shopbook#"+a + "#" + b+"#"+p));
                            buttons.Add(button1);
                        }
                    }
                }
            }



            await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "sotib olmoqchi bolgan kitobingizni tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
            #endregion
        }
        public static async Task Tasdiqlash(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            var button2 = new List<InlineKeyboardButton>();

            button1.Add(InlineKeyboardButton.WithCallbackData(text:"tasdiqlash", callbackData: "tasdiqlash"));
            
            buttons.Add(button1);
            buttons.Add(button2);
            await botClient.SendTextMessageAsync(
                chatId: update.CallbackQuery.From.Id,
                text: "Kitob sotib olishni tasdiqlang",
                replyMarkup: new InlineKeyboardMarkup(buttons),
                cancellationToken: cancellationToken);
        }
        public static async Task PytypeBookUserButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            int n = 0;
            string strId = "";
            string strcategory = "";
            string connectionString = "Host=localhost;" +
                    "Port=5432;" +
                    "Database=onlinemarketexam;" +
                    "User Id=postgres;" +
                    "Password=dfrt43i0;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM paytypecategory";
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
                            button1 = new List<InlineKeyboardButton>();
                            strId = "";
                            strcategory = "";
                            n = 0;
                            foreach (var value in row)
                            {
                                if (n == 0)
                                {
                                    strId = value.ToString();
                                }
                                else
                                {
                                    strcategory = value.ToString();
                                }
                                n++;
                            }
                            button1.Add(InlineKeyboardButton.WithCallbackData(text: strcategory, callbackData: "shoppaytype#" + strcategory));
                            buttons.Add(button1);

                        }
                    }

                }
            }
                await botClient.SendTextMessageAsync(
           chatId: update.CallbackQuery.From.Id,
           text: "Amallardan birini tanlang",
           replyMarkup: new InlineKeyboardMarkup(buttons),
           cancellationToken: cancellationToken);
                #endregion
            
        }
    }
}