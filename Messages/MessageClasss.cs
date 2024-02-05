using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using ConsoleApp1.JsonDatabazas;
using ConsoleApp1.Admins;
using ConsoleApp1.Users;

namespace ConsoleApp1.Messages
{
    internal class MessageClasss
    {
        public static async Task MessageAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            if (BazaClass.Checking(update.Message.Chat.Id) && update.Message.Type != MessageType.Contact)
            {
                ReplyKeyboardMarkup markup =
                    new ReplyKeyboardMarkup
                        (KeyboardButton.WithRequestContact("Contact yuborish"));
                markup.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: "Contact",
                        replyMarkup: markup
                );
                return;
            }
            else if (update.Message.Type == MessageType.Contact)
            {
                if (BazaClass.Checking(update.Message.Chat.Id))
                {
                    BazaClass.Apppend(update.Message.Chat.Id, update.Message.Contact.FirstName, update.Message.Contact.PhoneNumber);
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    replyToMessageId: message.MessageId,
                    text: "Hush kelibsiz : \n" + BazaClass.GetMe(message.Chat.Id),
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                }
            }
            else if (update.Message.Chat.Id == 12107296951)
            {
                AdminClass.EssentialFunction(botClient, update, cancellationToken);
            }
            else
            {
                UsersClassController.EssentialFunction(botClient, update, cancellationToken);

            }
        }
    }
}
