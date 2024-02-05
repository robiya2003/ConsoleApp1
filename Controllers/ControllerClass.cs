using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Reflection.Metadata.Ecma335;
using ConsoleApp1.Messages;
using ConsoleApp1.CallBackQueryFolders;
using ConsoleApp1.Users;

namespace ConsoleApp1.Controllers
{
    public static class ControllerClass
    {
        public static async Task EssentialFunction()
        {
            #region
            var botClient = new TelegramBotClient("6406750559:AAFoJbQbMTSYAzMQzcAphTNA5F7Kf7CG-GY");
            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() 
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
            #endregion
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                var handler = update.Type switch
                {

                    UpdateType.Message => MessageClasss.MessageAsyncFunction(botClient, update, cancellationToken),
                    UpdateType.CallbackQuery => CheckFunction(botClient, update, cancellationToken),

                    _ => MessageClasss.MessageAsyncFunction(botClient, update, cancellationToken),
                };
                try
                {
                    await handler;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
            #region
            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
            #endregion
        }
        public static async Task CheckFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.CallbackQuery.From.Id== 12107296951)
            {
                await CallBackQueryClasss.EssentialFunction(botClient,update, cancellationToken);
            }
            else
            {
                await UsersCallBackQueryCheck.EssentialFunction(botClient, update, cancellationToken);
            }
        }
    }
}
