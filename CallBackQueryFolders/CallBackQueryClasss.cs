using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using ConsoleApp1.AAA_BazaPosgresql;


namespace ConsoleApp1.CallBackQueryFolders
{
    public static class CallBackQueryClasss
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var t = update.CallbackQuery.Data.ToString();
            #region CATEGORY
            if (t == "categoryc")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "CREATE amalini bajarishingiz uchun createcategory  song # belgini yozib  category  nomini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            else if (t == "categoryr")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text:"BOOK CATEGORIES\n"+ CategoryCRUDClass.ReadFunction(),
               cancellationToken: cancellationToken);
            }
            else if (t == "categoryu")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "UPDATE amalini bajarishingiz uchun updatecategory song # belgini yozib id yana bitta # belgini tashlab yangi  category  nomini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            else if (t == "categoryd")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "DELETE amalini bajarishingiz uchun deletecategory song # belgini yozib  category  idsini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            #endregion

            #region BOOK
            else if (t == "bookc")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "CREATE amalini bajarmoqchi bo'lsangiz createbook # belgi book name # belgi auther name # belgi category id va # belgi narxini yozamiz yozamiz",
               cancellationToken: cancellationToken);
            }
            else if (t == "bookr")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "BOOKS\n" + BookCRUDclass.ReadFunction(),
               cancellationToken: cancellationToken);
            }
            else if (t == "booku")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "UPDATE amalini bajarmoqchi bo'lsangiz updatebook # belgi o'zgaruvchi id # belgi book name # belgi auther name # belgi category id va # belgi narxini yozamiz yozamiz",
               cancellationToken: cancellationToken);
            }
            else if (t == "bookd")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "DELETE amalini bajarmoqchi bo'lsangiz deletebook # belgi song id ",
               cancellationToken: cancellationToken);
            }
            #endregion

            #region PAYTYPE
            else if (t == "paytypec")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "CREATE amalini bajarishingiz uchun createpaytype  song # belgini yozib  paytype  nomini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            else if (t == "paytyper")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: PaytypeCRUDClass.ReadFunction(),
               cancellationToken: cancellationToken);
            }
            else if (t == "paytypeu")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "UPDATE amalini bajarishingiz uchun updatepaytype song # belgini yozib id yana bitta # belgini tashlab yangi  paytype  nomini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            else if (t == "paytyped")
            {
                await botClient.SendTextMessageAsync(
               chatId: update.CallbackQuery.From.Id,
               text: "DELETE amalini bajarishingiz uchun deletepaytype song # belgini yozib  paytype  idsini yozib kiritishingiz kerak",
               cancellationToken: cancellationToken);
            }
            #endregion
        }
    }
}
