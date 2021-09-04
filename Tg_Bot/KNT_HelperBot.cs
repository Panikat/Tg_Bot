using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Tg_Bot.ServiceClass;
using Tg_Bot.Interfaces;
using Tg_Bot.Enumerate;

namespace Tg_Bot
{
    class KNT_HelperBot : IManegeKNTBot
    {
        private string Token { get; set; }
        private TelegramBotClient client;

        public delegate void PauseForWork();
        public event PauseForWork PauseForWorking;

        Server.Server server;

        private SelectedLang selectedLang;

        public KNT_HelperBot()
        {

            Token = File.ReadLines(FileName.Token).First();

            client = new TelegramBotClient(Token);

            server = new Server.Server();

            selectedLang = new SelectedLang();
            selectedLang.SetLang(FileName.ENfile);

        }

        [Obsolete]
        public void StartReciving()
        {
            Console.WriteLine(client.GetMeAsync().Result);
            try
            {
                client.StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                client.StopReceiving();
            }
            server.TurnOnAsync();

            //var sMsg = new ScheduledMsg();
            //sMsg.SenderAllNewUsers(new DateTime(2021, 08, 31, 06, 10, 00), client, "Извините за неудобства в алгоритме был баг, который сейчас устранен, пожалуйста перезапустите бота и пользуйтесь");
        }

        [Obsolete]
        public void TurnOn_OfEvent()
        {
            try
            {
                client.OnMessage += StartMessege;
                client.OnCallbackQuery += CallBackInlineQuaryMain;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                client.StopReceiving();
            }
        }

        [Obsolete]
        public void StopReciving()
        {
            if (PauseForWorking == null)
            {
                client.StopReceiving();

                throw new KNTHelperBotException("------>\nThe object has no pause event\n<------\n", "add to Event of =>PauseForWorking<= function for pause:\n" +
                    "for Example:\n" +
                    "public void PauseIvent()\n" +
                    "{\n" +
                    "Console.ReadKey();\n" +
                    "}\n");
            }
            PauseForWorking?.Invoke();
            client.StopReceiving();

        }

        //[Obsolete]
        private async void StartMessege(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg != null)
            {
                bool userInBlackList = false;

                try
                {
                    userInBlackList = TelegramClientCheck.InBlackList(msg.From);
                }
                catch (KNTHelperBotException ex)
                {
                    Console.WriteLine(ex.Message + "\n=======\n" + ex.GetWhatToDo());
                }

                TelegramClientCheck.AddToNewUsers(msg.From);

                if (!userInBlackList)
                {
                    //if (!TelegramClientCheck.IsAdmins(msg.From)){
                    //DateTime release = new DateTime(2021, 08, 31, 05, 30, 00);
                    //release = release.ToUniversalTime();

                    //if (DateTime.Now.ToUniversalTime() < release)
                    //{
                    //    TimeSpan date = release.Subtract(DateTime.Now.ToUniversalTime());

                    //    TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, msg.Text);

                    //    await client.SendTextMessageAsync(msg.Chat.Id, $"До релиза бота осталось: {date.Days} д. {date.Hours} ч. {date.Minutes} м.");
                    //    return;
                    //}
                    //}

                    if (msg.Text == "/start")
                    {
                        if (!TelegramClientCheck.IsFriend(msg.From))
                        {
                            await client.SendTextMessageAsync(msg.From.Id, $"Слушай, {msg.From.FirstName}🤨 ты не отсюдого, тебе низя 😋");
                            await client.SendTextMessageAsync(msg.From.Id, "😏");

                            TelegramBotLogger.PrintBanInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, msg.Text);
                            goto EndOfListenOfMsg;
                        }
                        //выбор языка
                        /*await client.SendTextMessageAsync(msg.Chat.Id, "Выберите язык: | Оберіть мову: | Choose the language:", replyMarkup: new ButtonGenerator().GetLangKeyBoardButtons());
                        switch (msg.Text)
                        {
                            case "🇺🇦Украинский!🇺🇦":
                                selectedLang.SetLang(FileName.UAfile);
                                break;
                            case "🇷🇺Русский!🇷🇺":
                                selectedLang.SetLang(FileName.RUfile);
                                break;
                            case "🇬🇧Английский!🇬🇧":
                                selectedLang.SetLang(FileName.ENfile);
                                break;
                            default:
                                break;
                        }*/
                        await client.SendTextMessageAsync(msg.Chat.Id, File.ReadAllText(FileName.MainDir + selectedLang.WelcomeFile), replyMarkup: new ButtonGenerator().GetKeyBoardButtons(ref selectedLang));


                    }

                    /*

                     Расписание:
                          - Числитель:
                                * Выбор дня недели:
                                     - Пн - Вт - Ср - Чт - Пт

                          - Знаменатель
                                * Выбор дня недели:
                                     - Пн - Вт - Ср - Чт - Пт

                          - Звонки      ->      Текст расписания звонков

                     */

                    else if (msg.Text == selectedLang.TimetableBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.TimeTable.ToString());

                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: selectedLang.TypeOfWeek[(int)Enumerate.TypeOfWeek.None], // selectedLang.TypeOfWeek[0]
                            replyMarkup: new ButtonGenerator().GetInlineButtons_TimeTable(ref selectedLang));
                    }

                    /*
                     х6
                     Предметы:
                          - Предмет:
                                *Преподаватели -> Имена - Связь - 

                     */

                    else if (msg.Text == selectedLang.LessonsBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.Lessons.ToString());

                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.None], // selectedLang.TypeOfLesson[0]
                            replyMarkup: new ButtonGenerator().GetInlineButtons_Lessons(ref selectedLang));
                    }

                    /*

                    Ссылка на чат - "Вопрос-ответ" 

                     */

                    else if (msg.Text == selectedLang.FAQBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.QuesAnsw.ToString());

                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: selectedLang.FAQMsg,
                            replyMarkup: (InlineKeyboardMarkup)new ButtonGenerator().GetInlineButton_QuestionAnswe(ref selectedLang));
                    }

                    /*
                    х6
                     Предметы:
                          - Предмет:
                                *вид урока и ссылка в кнопке

                     */

                    else if (msg.Text == selectedLang.ConferencesBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.Conferences.ToString());

                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.None], // selectedLang.TypeOfLesson[0]
                            replyMarkup: new ButtonGenerator().GetinlineKeyboard_Conf(ref selectedLang));
                    }

                    /*

                    Мой контакт 

                     */

                    else if (msg.Text == selectedLang.ConnectBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.Сommunication.ToString());

                        await client.SendTextMessageAsync(msg.Chat.Id, File.ReadAllText(FileName.MainDir + selectedLang.ComunicationAnswerFile));
                    }

                    else if (msg.Text == selectedLang.ServerBtn)
                    {
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, TypeOfButton.ToServer.ToString());

                        await client.SendTextMessageAsync(msg.Chat.Id, FileName.DonateLink);
                        await client.SendTextMessageAsync(msg.Chat.Id, selectedLang.QRMsg);
                        await client.SendPhotoAsync(msg.Chat.Id, FileName.DonateQrCode);
                    }
                    else
                        TelegramBotLogger.PrintInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, msg.Text);
                }
                else
                    TelegramBotLogger.PrintBanInfo(e.Message.From.FirstName, e.Message.From.Id.ToString(), e.Message.From.Username, msg.Text);

                EndOfListenOfMsg:;
            }
        }
        [Obsolete]
        private void CallBackInlineQuaryMain(object sender, CallbackQueryEventArgs callBack)
        {
            InlineData inlineData = InlineData.Parse(callBack.CallbackQuery.Data);

            switch (inlineData.Button)
            {
                case TypeOfButton.None:
                    throw new KNTHelperBotException("Unredefined call", "It is not clear who called the function");

                case TypeOfButton.TimeTable:
                    TimeTable(inlineData, callBack);
                    break;
                case TypeOfButton.DayOfWeek:
                    DayOfWeek(inlineData, callBack);
                    break;
                case TypeOfButton.Lessons:
                    LessonsInfo(inlineData, callBack);
                    break;
                case TypeOfButton.Conferences:
                    break;
                default:
                    break;
            }
        }

        [Obsolete]
        private async void TimeTable(InlineData data, CallbackQueryEventArgs callBack)
        {
            await client.AnswerCallbackQueryAsync(callBack.CallbackQuery.Id, $"ля, кого я вижу, {callBack.CallbackQuery.From.FirstName} 🤨");

            TelegramBotLogger.PrintInfo(callBack.CallbackQuery.From.FirstName, callBack.CallbackQuery.From.Id.ToString(),
               callBack.CallbackQuery.From.Username, data.Week.ToString());

            switch (data.Week)
            {
                case Enumerate.TypeOfWeek.None:
                    throw new KNTHelperBotException("Unredefined call", $"It is not clear who called the function *Timetable*");

                case Enumerate.TypeOfWeek.Numerator:
                    await client.SendTextMessageAsync(
                chatId: callBack.CallbackQuery.From.Id,
                text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.None], // selectedLang.TypeOfDay[0]
                replyMarkup: new ButtonGenerator().GetinlineKeyboard_DayOfWeek(data, ref selectedLang));
                    break;

                case Enumerate.TypeOfWeek.Denominator:
                    await client.SendTextMessageAsync(
                chatId: callBack.CallbackQuery.From.Id,
                text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.None], // selectedLang.TypeOfDay[0]
                replyMarkup: new ButtonGenerator().GetinlineKeyboard_DayOfWeek(data, ref selectedLang));
                    break;

                case Enumerate.TypeOfWeek.Call_:
                    await client.AnswerCallbackQueryAsync(callBack.CallbackQuery.Id);
                    await client.SendPhotoAsync(callBack.CallbackQuery.From.Id, FileName.TimeTable);
                    break;
                default:
                    break;
            }
        }

        [Obsolete]
        private async void DayOfWeek(InlineData data, CallbackQueryEventArgs callBack)
        {
            await client.AnswerCallbackQueryAsync(callBack.CallbackQuery.Id);
            //await client.SendTextMessageAsync(callBack.CallbackQuery.From.Id, $"Ты выбрал {data.TypeOfDay}, тип недели {data.TypeOfWeek}:\n");

            TelegramBotLogger.PrintInfo(callBack.CallbackQuery.From.FirstName, callBack.CallbackQuery.From.Id.ToString(),
               callBack.CallbackQuery.From.Username, $"{data.Week}-{data.Day}");

            string file = FileName.MainDir + $@"{data.Week}/{data.Day}.txt";

            ReadOrCreateFiles(file, 153, callBack.CallbackQuery.From.Id.ToString());
        }

        [Obsolete]
        private async void LessonsInfo(InlineData data, CallbackQueryEventArgs callBack)
        {
            await client.AnswerCallbackQueryAsync(callBack.CallbackQuery.Id);

            TelegramBotLogger.PrintInfo(callBack.CallbackQuery.From.FirstName, callBack.CallbackQuery.From.Id.ToString(),
               callBack.CallbackQuery.From.Username, data.Lesson.ToString());

            string file = FileName.LissonInfoDir + $"{data.Lesson}-info.txt";

            ReadOrCreateFiles(file, 151, callBack.CallbackQuery.From.Id.ToString());
        }


        private async void ReadOrCreateFiles(string fileName, long size, string id)
        {

            //string fileName = FileName.MainDir + $"{data.TypeOfWeek}_{data.TypeOfDay}.txt";
            if (!Directory.Exists(FileName.LissonInfoDir)) Directory.CreateDirectory(FileName.LissonInfoDir);
            if (!Directory.Exists(FileName.DenominatorDir)) Directory.CreateDirectory(FileName.DenominatorDir);
            if (!Directory.Exists(FileName.NumeratorDir)) Directory.CreateDirectory(FileName.NumeratorDir);


            if (!File.Exists(fileName))
            {
                using (File.Create(fileName)) { }
            }
            if (File.Exists(fileName))
            {
                long size_;
                using (FileStream fileStream = File.Open(fileName, FileMode.Open))
                    size_ = fileStream.Length;

                if (size_ == 0 || size_ == size)
                {
                    await client.SendTextMessageAsync(id, selectedLang.LackOfInfo);
                }
                else
                {
                    await client.SendTextMessageAsync(id, File.ReadAllText(fileName));
                }
            }
        }
    }
}
