using ConsoleApp1.AAA_BazaPosgresql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ConsoleApp1.JsonDatabazas;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Argon;
using OfficeOpenXml;
using OfficeOpenXml;
using Npgsql;
using Google.Protobuf.WellKnownTypes;

namespace ConsoleApp1.SEndFileTelegramChat
{
    public static class SendPdftoTelegramClass
    {
        #region SEND FUNCTION 1
        public static async Task SendAllUsers1(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string path = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\JsonDatabazas\DataBaza.json";
            var adress_users_pdf = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\pdfs\AllUsers1.pdf";


            IronPdf.License.IsValidLicense("IRONSUITE.ROBIYAHAKIMOVA2003.GMAIL.COM.31506 - F6F04CEDBB - EKOG6 - BUYVC6YURM5J - U4ZHWGHH6AGZ - A5NRKSSXQTKW - HL4K6TMCTQE6 - 6X4PD2X4524N - VDVMYW7H4JN6 - FWGRNC - TUQA5GNOV2GLUA - DEPLOYMENT.TRIAL - K6ZMGU.TRIAL.EXPIRES.04.MAR.2024");
            string text = System.IO.File.ReadAllText(path);
            ChromePdfRenderer renderer = new ChromePdfRenderer();
            IronPdf.PdfDocument pdf = renderer.RenderHtmlAsPdf(text);
            pdf.SaveAs(adress_users_pdf);
            await using Stream stream = System.IO.File.OpenRead(adress_users_pdf);
            await botClient.SendDocumentAsync(
                chatId: update.Message.Chat.Id,
                document: InputFile.FromStream(stream: stream, fileName: $"All_users2.pdf"),
                caption: "Hamma foydalanuvchi haqida malumot"
                );
            stream.Dispose();
        }
        public static async Task SendAllProducts1(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //string path = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\JsonDatabazas\DataBaza.json";
            var adress_users_pdf = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\pdfs\AllProduct1.pdf";


            IronPdf.License.IsValidLicense("IRONSUITE.ROBIYAHAKIMOVA2003.GMAIL.COM.31506 - F6F04CEDBB - EKOG6 - BUYVC6YURM5J - U4ZHWGHH6AGZ - A5NRKSSXQTKW - HL4K6TMCTQE6 - 6X4PD2X4524N - VDVMYW7H4JN6 - FWGRNC - TUQA5GNOV2GLUA - DEPLOYMENT.TRIAL - K6ZMGU.TRIAL.EXPIRES.04.MAR.2024");
            //string text = System.IO.File.ReadAllText(path);
            ChromePdfRenderer renderer = new ChromePdfRenderer();
            IronPdf.PdfDocument pdf = renderer.RenderHtmlAsPdf(BookCRUDclass.ReadFunction());
            pdf.SaveAs(adress_users_pdf);
            await using Stream stream = System.IO.File.OpenRead(adress_users_pdf);
            await botClient.SendDocumentAsync(
                chatId: update.Message.Chat.Id,
                document: InputFile.FromStream(stream: stream, fileName: $"All_products1.pdf"),
                caption: "Hamma mahsulot haqida malumot"
                );
            stream.Dispose();
        }
        #endregion


        #region SEND FUNCTION 2
        public static async Task SendAllUsers2(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string path = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\JsonDatabazas\DataBaza.json";
            string pathsend = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\pdfs\AllUsers2.pdf";
            string StringJson = System.IO.File.ReadAllText(path);
            var JsonList = JsonConvert.DeserializeObject<List<Root>>(StringJson);
            int n = 1;


            DirectoryInfo projectDirectoryInfo =
                  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            Console.WriteLine(projectDirectoryInfo.FullName);

            string pdfsFolder = Directory.CreateDirectory(
                 Path.Combine(projectDirectoryInfo.FullName, "pdfs")).FullName;

            QuestPDF.Settings.License = LicenseType.Community;
            // code in your main method
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                      .Text("Users!\n")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);
                          n = 1;
                          foreach (var el in JsonList)
                          {
                              x.Item().Text($"{n}-customer");
                              x.Item().Text("chat id : " + el.chatid);
                              x.Item().Text("phone number : " + el.phonenumber);
                              x.Item().Text("name : " + el.firstname);
                              x.Item().Text("\n");
                              n++;
                          }

                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
                .GeneratePdf(Path.Combine(pdfsFolder, "AllUsers2.pdf"));


            await using Stream stream = System.IO.File.OpenRead(pathsend);
            await botClient.SendDocumentAsync(
                chatId: update.Message.Chat.Id,
                document: InputFile.FromStream(stream: stream, fileName: $"All_users2.pdf"),
                caption: "Hamma foydalanuvchi haqida malumot"
                );
            stream.Dispose();
        }




        public static async Task SendAllProducts2(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string pathsend = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\pdfs\AllProducts2.pdf";
            int n = 0;

            DirectoryInfo projectDirectoryInfo =
                  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            Console.WriteLine(projectDirectoryInfo.FullName);

            string pdfsFolder = Directory.CreateDirectory(
                 Path.Combine(projectDirectoryInfo.FullName, "pdfs")).FullName;

            QuestPDF.Settings.License = LicenseType.Community;
            // code in your main method
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                      .Text("Products!\n")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);
                          var resultlist = BookCRUDclass.ReadwithSendfileFunction();
                          foreach (var row in resultlist)
                          {

                              foreach (var value in row)
                              {
                                  if (n == 0)
                                  {
                                      x.Item().Text($"id : " + $"{value}");
                                  }
                                  else if (n == 1)
                                  {
                                      x.Item().Text("name : " + $"{value}");
                                  }
                                  else if (n == 2)
                                  {
                                      x.Item().Text("auther : " + $"{value}");
                                  }
                                  else if (n == 3)
                                  {
                                      x.Item().Text("category : " + $"{value}");
                                  }
                                  else
                                  {
                                      x.Item().Text("price : " + $"{value}");
                                  }

                                  n++;

                              }
                              x.Item().Text("\n");
                              n = 0;



                          }
                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
                .GeneratePdf(Path.Combine(pdfsFolder, "AllProducts2.pdf"));






            await using Stream stream = System.IO.File.OpenRead(pathsend);
            await botClient.SendDocumentAsync(
                chatId: update.Message.Chat.Id,
                document: InputFile.FromStream(stream: stream, fileName: $"All_products2.pdf"),
                caption: "Hamma mahsulot haqida malumot"
                );
            stream.Dispose();
        }
        #endregion


        public static async Task SendOrdersExcel(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region
            string excelFilePath = "C:\\Users\\LENOVO\\Desktop\\OnlineMarketBookExcam\\ConsoleApp1\\pdfs\\ordermalumoti.xlsx";
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";
            List<object[]> resultList = new List<object[]>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"\r\nselect * from OrderUser where tasdiqlash is not null";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        //List<object[]> resultList = new List<object[]>();
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
                                Console.Write($"{value}\t");

                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            #endregion
            //////////////////////////////
            try
            {

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells["A1"].Value = "orderuser_id";
                    worksheet.Cells["B1"].Value = "user_chat_id";
                    worksheet.Cells["C1"].Value = "user_fullname";
                    worksheet.Cells["D1"].Value = "book_id";
                    worksheet.Cells["E1"].Value = "paytype";
                    worksheet.Cells["F1"].Value = "tasdiqlash";

                    int n = 2;
                    int i = 1;
                    foreach (var row in resultList)
                    {
                        i = 1;
                        foreach (var value in row)
                        {

                            if (i == 1)
                            {
                                worksheet.Cells[$"A{n}"].Value = value;
                            }
                            else if (i == 2)
                            {
                                worksheet.Cells[$"B{n}"].Value = value;
                            }
                            else if (i == 3)
                            {
                                worksheet.Cells[$"C{n}"].Value = value;
                            }
                            else if (i == 4)
                            {
                                worksheet.Cells[$"D{n}"].Value = value;
                            }
                            else if (i == 5)
                            {
                                worksheet.Cells[$"E{n}"].Value = value;
                            }
                            else if (i == 6)
                            {
                                worksheet.Cells[$"F{n}"].Value = value;
                            }

                            i++;
                        }
                        n++;
                    }
                    var fileInfo = new System.IO.FileInfo(excelFilePath);
                    package.SaveAs(fileInfo);
                }

                Console.WriteLine("Fayl muvaffaqiyatli yaratildi: " + excelFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xatolik yuz berdi: " + ex.Message);
            }
            #endregion

            await using Stream stream = System.IO.File.OpenRead(excelFilePath);
            await botClient.SendDocumentAsync(
                chatId: update.Message.Chat.Id,
                document: InputFile.FromStream(stream: stream, fileName: $"ordermalumoti.xlsx"),
                caption: "Hamma foydalanuvchi haqida malumot"
                );
            stream.Dispose();
        }
        public static void WriteToExcel()
        {
            #region
            string excelFilePath = "C:\\Users\\LENOVO\\Desktop\\OnlineMarketBookExcam\\ConsoleApp1\\pdfs\\ordermalumoti.xlsx";
            #region
            string connectionString = "Host=localhost;" +
                "Port=5432;" +
                "Database=onlinemarketexam;" +
                "User Id=postgres;" +
                "Password=dfrt43i0;";
            List<object[]> resultList = new List<object[]>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"\r\nselect * from OrderUser";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        //List<object[]> resultList = new List<object[]>();
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
                                Console.Write($"{value}\t");

                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            #endregion
            //////////////////////////////
            try
            {
               
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells["A1"].Value = "orderuser_id";
                    worksheet.Cells["B1"].Value = "user_chat_id";
                    worksheet.Cells["C1"].Value = "user_fullname";
                    worksheet.Cells["D1"].Value = "book_id";
                    worksheet.Cells["E1"].Value = "paytype";
                    worksheet.Cells["F1"].Value = "tasdiqlash";
                    
                    int n = 2;
                    int i = 1;
                    foreach (var row in resultList)
                    {
                        i = 1;
                        foreach (var value in row)
                        {
                            
                            if (i==1)
                            {
                                worksheet.Cells[$"A{n}"].Value = value;
                            }
                            else if(i==2)
                            {
                                worksheet.Cells[$"B{n}"].Value = value;
                            }
                            else if(i==3)
                            {
                                worksheet.Cells[$"C{n}"].Value = value;
                            }
                            else if(i==4)
                            {
                                worksheet.Cells[$"D{n}"].Value = value;
                            }
                            else if(i==5)
                            {
                                worksheet.Cells[$"E{n}"].Value = value;
                            }
                            else if(i==6)
                            {
                                worksheet.Cells[$"F{n}"].Value = value;
                            }
              
                            i++;
                        }
                        n++;
                    }
                    var fileInfo = new System.IO.FileInfo(excelFilePath);
                    package.SaveAs(fileInfo);
                }

                Console.WriteLine("Fayl muvaffaqiyatli yaratildi: " + excelFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xatolik yuz berdi: " + ex.Message);
            }
            #endregion

        }
    }
}
