using ConsoleApp1.Controllers;
using ConsoleApp1.SEndFileTelegramChat;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await ControllerClass.EssentialFunction();
            //SendPdftoTelegramClass.WriteToExcel();
        }
    }
}
