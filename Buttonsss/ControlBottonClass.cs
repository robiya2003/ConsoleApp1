using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConsoleApp1.Buttonsss
{
    internal class ControlBottonClass
    {
        public static async Task AdminFirstButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var buttons = new List<List<KeyboardButton>>();

            var buttonsgorizontal1 = new List<KeyboardButton>();
            buttonsgorizontal1.Add(new KeyboardButton("CategoryCRUD"));
            buttonsgorizontal1.Add(new KeyboardButton("BookCRUD"));
            buttonsgorizontal1.Add(new KeyboardButton("PayTypeCRUD"));

            var buttonsgorizontal2 = new List<KeyboardButton>();
            buttonsgorizontal2.Add(new KeyboardButton("Products"));
            buttonsgorizontal2.Add(new KeyboardButton("Customers"));
            buttonsgorizontal2.Add(new KeyboardButton("Orders"));
            buttons.Add(buttonsgorizontal1);
            buttons.Add(buttonsgorizontal2);


            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Choose a response",
                replyMarkup: new ReplyKeyboardMarkup(buttons),
                cancellationToken: cancellationToken);
        }
        public static async Task CategoryCRUDButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            var button2 = new List<InlineKeyboardButton>();
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "CREATE", callbackData: "categoryc"));
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "READ", callbackData: "categoryr"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "UPDATE", callbackData: "categoryu"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "DELETE", callbackData: "categoryd"));
            buttons.Add(button1);   
            buttons.Add(button2);
            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Amallardan birini tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
        }

        public static async Task BookCRUDButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            var button2 = new List<InlineKeyboardButton>();
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "CREATE", callbackData: "bookc"));
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "READ", callbackData: "bookr"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "UPDATE", callbackData: "booku"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "DELETE", callbackData: "bookd"));
            buttons.Add(button1);
            buttons.Add(button2);
            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Amallardan birini tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
        }
        public static async Task PayTypeCRUDButton(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var button1 = new List<InlineKeyboardButton>();
            var button2 = new List<InlineKeyboardButton>();
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "CREATE", callbackData: "paytypec"));
            button1.Add(InlineKeyboardButton.WithCallbackData(text: "READ", callbackData: "paytyper"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "UPDATE", callbackData: "paytypeu"));
            button2.Add(InlineKeyboardButton.WithCallbackData(text: "DELETE", callbackData: "paytyped"));
            buttons.Add(button1);
            buttons.Add(button2);
            await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "Amallardan birini tanlang",
               replyMarkup: new InlineKeyboardMarkup(buttons),
               cancellationToken: cancellationToken);
        }

    }
}
