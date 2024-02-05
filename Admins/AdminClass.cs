using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using ConsoleApp1.JsonDatabazas;
using Telegram.Bot.Types.ReplyMarkups;
using ConsoleApp1.Buttonsss;
using Telegram.Bot.Types.Enums;
using ConsoleApp1.AAA_BazaPosgresql;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Kernel.XMP.Impl.XPath;

using IronPdf;
using Google.Protobuf;
using ConsoleApp1.SEndFileTelegramChat;




namespace ConsoleApp1.Admins
{
    public static class AdminClass
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            
            if(update.Message.Type==MessageType.Text)
            {
                TextFunction(botClient,update,cancellationToken);
            }
            else
            {
                await ControlBottonClass.AdminFirstButton(botClient, update, cancellationToken);
            }
        }
        public static async Task TextFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await ControlBottonClass.AdminFirstButton(botClient, update, cancellationToken);
            string MessageText = update.Message.Text;
            if (MessageText == "/start")
            {

                await ControlBottonClass.AdminFirstButton(botClient, update, cancellationToken);
            }
            #region BUTTONS PASTGI
            else if (MessageText == "CategoryCRUD")
            {
                await ControlBottonClass.CategoryCRUDButton(botClient, update, cancellationToken);
            }
            else if (MessageText == "BookCRUD")
            {
                await ControlBottonClass.BookCRUDButton(botClient, update, cancellationToken);
            }
            else if (MessageText == "PayTypeCRUD")
            {
                await ControlBottonClass.PayTypeCRUDButton(botClient, update, cancellationToken);
            }
            #endregion

            #region STARTWITH CATEGORY
            else if (MessageText.StartsWith("createcategory"))
            {
                string[] createcategory = MessageText.Split('#');
                CategoryCRUDClass.CreateFunction(createcategory[1]);

                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"{createcategory[1]} categorylar safiga qoshildi",
               cancellationToken: cancellationToken);

            }
            else if (MessageText.StartsWith("updatecategory"))
            {
                string[] updatecategory = MessageText.Split('#');
                CategoryCRUDClass.UpdateFunction(int.Parse(updatecategory[1]), updatecategory[2]);
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"id={updatecategory[1]} {updatecategory[2]} ga o'zgartirildi",
               cancellationToken: cancellationToken);
            }
            else if (MessageText.StartsWith("deletecategory"))
            {
                string[] deletecategory = MessageText.Split('#');
                CategoryCRUDClass.DeleteFunction(int.Parse(deletecategory[1]));
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"id={deletecategory[1]} o'chirildi",
               cancellationToken: cancellationToken);
            }
            #endregion

            #region STARTWITH PAYTYPE
            else if (MessageText.StartsWith("createpaytype"))
            {
                string[] createpaytype = MessageText.Split('#');
                PaytypeCRUDClass.CreateFunction(createpaytype[1]);

                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"{createpaytype[1]} pytypelar safiga qoshildi",
               cancellationToken: cancellationToken);

            }
            else if (MessageText.StartsWith("updatepaytype"))
            {
                string[] updatepaytype = MessageText.Split('#');
                PaytypeCRUDClass.UpdateFunction(int.Parse(updatepaytype[1]), updatepaytype[2]);
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"id={updatepaytype[1]} {updatepaytype[2]} ga o'zgartirildi",
               cancellationToken: cancellationToken);
            }
            else if (MessageText.StartsWith("deletepaytype"))
            {
                string[] deletepaytype = MessageText.Split('#');
                PaytypeCRUDClass.DeleteFunction(int.Parse(deletepaytype[1]));
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"paytypedan id={deletepaytype[1]} o'chirildi",
               cancellationToken: cancellationToken);
            }
            #endregion

            #region STARTWITH BOOK
            else if (MessageText.StartsWith("createbook"))
            {
                string[] createbook = MessageText.Split('#');
                BookCRUDclass.CreateFunction(createbook[1], createbook[2], int.Parse(createbook[3]), float.Parse(createbook[4]));

                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"{createbook[1]} booklar safiga qoshildi",
               cancellationToken: cancellationToken);

            }
            else if (MessageText.StartsWith("updatebook"))
            {
                string[] updatebook = MessageText.Split('#');
                BookCRUDclass.UpdateFunction(int.Parse(updatebook[1]), updatebook[2], updatebook[3], int.Parse(updatebook[4]), float.Parse(updatebook[5]));
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"id={updatebook[1]} update qilindi",
               cancellationToken: cancellationToken);
            }
            else if (MessageText.StartsWith("deletebook"))
            {
                string[] deletebook = MessageText.Split('#');
                BookCRUDclass.DeleteFunction(int.Parse(deletebook[1]));
                await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: $"bookdan id={deletebook[1]} o'chirildi",
               cancellationToken: cancellationToken);
            }
            #endregion

            else if (MessageText == "Orders")
            {
                await SendPdftoTelegramClass.SendOrdersExcel(botClient, update, cancellationToken);



            }
            else if (MessageText == "Customers")
            {
                await SendPdftoTelegramClass.SendAllUsers1(botClient,update,cancellationToken);
                await SendPdftoTelegramClass.SendAllUsers2(botClient,update,cancellationToken);
            }
            else if (MessageText == "Products")
            {
                await SendPdftoTelegramClass.SendAllProducts1(botClient,update,cancellationToken);
                await SendPdftoTelegramClass.SendAllProducts2(botClient,update,cancellationToken);
                //"Orders"
            }
           
        }
    }
}
