using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;

namespace Tg_Bot.ServiceClass
{
    class ButtonGenerator
    {
        public IReplyMarkup GetKeyBoardButtons(ref SelectedLang selectedLang)
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = selectedLang.TimetableBtn } },
                    new List<KeyboardButton> { new KeyboardButton { Text = selectedLang.LessonsBtn } /*, new KeyboardButton { Text = "💻Конференции!💻" }*/ },
                    new List<KeyboardButton> { new KeyboardButton { Text = selectedLang.FAQBtn } },
                    new List<KeyboardButton> { new KeyboardButton { Text = selectedLang.ConnectBtn } },
                    new List<KeyboardButton> { new KeyboardButton { Text = selectedLang.ServerBtn } }
                }
            };
        }

        public IReplyMarkup GetLangKeyBoardButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "🇺🇦Украинский!🇺🇦" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "🇷🇺Русский!🇷🇺" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "🇬🇧Английский!🇬🇧" } }
                }
            };
        }

        public InlineKeyboardButton GetInlineButton_QuestionAnswe(ref SelectedLang selectedLang)
        {
            InlineKeyboardButton button = new InlineKeyboardButton();

            button.Text = selectedLang.FAQurl;
            using (FileStream fstream = new FileStream(FileName.QuesAnsw, FileMode.Open))
            using (StreamReader reader = new StreamReader(fstream))
                button.Url = reader.ReadLine();

            return button;
        }

        public InlineKeyboardMarkup GetInlineButtons_TimeTable(ref SelectedLang selectedLang)
        {
            return new InlineKeyboardMarkup(new[]
                            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(
                                text: selectedLang.TypeOfWeek[(int)Enumerate.TypeOfWeek.Numerator], //selectedLang.TypeOfWeek[1]
                                callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.TimeTable,
                            Week = Enumerate.TypeOfWeek.Numerator,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.None
                            }.Crypt())
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(
                                text: selectedLang.TypeOfWeek[(int)Enumerate.TypeOfWeek.Denominator], //selectedLang.TypeOfWeek[2]
                                callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.TimeTable,
                            Week = Enumerate.TypeOfWeek.Denominator,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.None
                            }.Crypt())
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(
                                text: selectedLang.TypeOfWeek[(int)Enumerate.TypeOfWeek.Call_], //selectedLang.TypeOfWeek[3]
                                callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.TimeTable,
                            Week = Enumerate.TypeOfWeek.Call_,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.None
                            }.Crypt())
                        }
                     });
        }

        public InlineKeyboardMarkup GetInlineButtons_Lessons(ref SelectedLang selectedLang)
        {
            return new InlineKeyboardMarkup(new[]
                            {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.ASD], //selectedLang.TypeOfLesson[1]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Lessons,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.ASD
                            }.Crypt()
                                    ),
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.VM], //selectedLang.TypeOfLesson[2]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Lessons,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.VM
                            }.Crypt())
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.LMV], //selectedLang.TypeOfLesson[3]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Lessons,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.LMV
                            }.Crypt()),
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.OPI], //selectedLang.TypeOfLesson[4]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Lessons,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.OPI
                            }.Crypt())
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.TV], //selectedLang.TypeOfLesson[5]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Lessons,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.TV
                                }.Crypt())
                                //InlineKeyboardButton.WithCallbackData("6!")
                            }
                        });
        }

        public InlineKeyboardMarkup GetinlineKeyboard_Conf(ref SelectedLang selectedLang)
        {
            return new InlineKeyboardMarkup(new[]
                            {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.ASD], //selectedLang.TypeOfLesson[1]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Conferences,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.ASD
                                }.Crypt()),
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.VM], //selectedLang.TypeOfLesson[2]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Conferences,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.VM
                                }.Crypt())
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.LMV], //selectedLang.TypeOfLesson[3]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Conferences,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.LMV
                                }.Crypt()),
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.OPI], //selectedLang.TypeOfLesson[4]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Conferences,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.OPI
                                }.Crypt())
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(
                                    text: selectedLang.TypeOfLesson[(int)Enumerate.TypeOfLesson.TV], //selectedLang.TypeOfLesson[5]
                                    callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.Conferences,
                            Week = Enumerate.TypeOfWeek.None,
                            Day = Enumerate.TypeOfDay.None,
                            Lesson = Enumerate.TypeOfLesson.TV
                                }.Crypt())
                            }
                            });
        }

        public InlineKeyboardMarkup GetinlineKeyboard_DayOfWeek(InlineData data, ref SelectedLang selectedLang)
        {
            return new InlineKeyboardMarkup(new[]
            {
            new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.Monday], //selectedLang.TypeOfDay[1]
                        callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.DayOfWeek,
                            Week = data.Week,
                            Day = Enumerate.TypeOfDay.Monday,
                            Lesson = Enumerate.TypeOfLesson.None
                                }.Crypt()), //for read from (for example NumeratorMonday.txt)
                    InlineKeyboardButton.WithCallbackData(
                        text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.Tuesday], //selectedLang.TypeOfDay[2]
                        callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.DayOfWeek,
                            Week = data.Week,
                            Day = Enumerate.TypeOfDay.Tuesday,
                            Lesson = Enumerate.TypeOfLesson.None
                                }.Crypt())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.Wednesday], //selectedLang.TypeOfDay[3]
                        callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.DayOfWeek,
                            Week = data.Week,
                            Day = Enumerate.TypeOfDay.Wednesday,
                            Lesson = Enumerate.TypeOfLesson.None
                                }.Crypt()),
                    InlineKeyboardButton.WithCallbackData(
                        text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.Thursday], //selectedLang.TypeOfDay[4]
                        callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.DayOfWeek,
                            Week = data.Week,
                            Day = Enumerate.TypeOfDay.Thursday,
                            Lesson = Enumerate.TypeOfLesson.None
                                }.Crypt())
                },
                  new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: selectedLang.TypeOfDay[(int)Enumerate.TypeOfDay.Friday], //selectedLang.TypeOfDay[5]
                        callbackData: new InlineData(){
                            Button = Enumerate.TypeOfButton.DayOfWeek,
                            Week = data.Week,
                            Day = Enumerate.TypeOfDay.Friday,
                            Lesson = Enumerate.TypeOfLesson.None
                                }.Crypt())
                }
            });
        }
    }
}
