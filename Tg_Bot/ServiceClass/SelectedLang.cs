using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tg_Bot.ServiceClass
{
    class SelectedLang
    {
        public string TimetableBtn { get; set; }
        public string LessonsBtn { get; set; }
        public string ConferencesBtn { get; set; }
        public string FAQBtn { get; set; }
        public string ConnectBtn { get; set; }
        public string ServerBtn { get; set; }
        private string[] typeOfWeek = new string[4];
        public string[] TypeOfWeek
        {
            get { return typeOfWeek; }
            set
            {
                if (value.Length == typeOfWeek.Length)
                {
                    for (int index = 0; index < value.Length; index++)
                    {
                        string element = value[index];
                        typeOfWeek[index] = element;
                    }
                }
            }
        }
        private string[] typeOfDay = new string[6];
        public string[] TypeOfDay
        {
            get { return typeOfDay; }
            set
            {
                if (value.Length == typeOfDay.Length)
                {
                    for (int index = 0; index < value.Length; index++)
                    {
                        string element = value[index];
                        typeOfDay[index] = element;
                    }
                }
            }
        }
        private string[] typeOfLesson = new string[6];
        public string[] TypeOfLesson
        {
            get { return typeOfLesson; }
            set
            {
                if (value.Length == typeOfLesson.Length)
                {
                    for (int index = 0; index < value.Length; index++)
                    {
                        string element = value[index];
                        typeOfLesson[index] = element;
                    }
                }
            }
        }
        public string FAQMsg { get; set; }
        public string FAQurl { get; set; }
        public string QRMsg { get; set; }
        public string LackOfInfo { get; set; }
        public string WelcomeFile { get; set; }
        public string ComunicationAnswerFile { get; set; }
        public void SetLang(string langFile)
        {
            string json = File.ReadAllText(langFile);
            SelectedLang obj = JsonConvert.DeserializeObject<SelectedLang>(json);
            TimetableBtn = obj.TimetableBtn;
            LessonsBtn = obj.LessonsBtn;
            ConferencesBtn = obj.ConferencesBtn;
            FAQBtn = obj.FAQBtn;
            ConnectBtn = obj.ConnectBtn;
            ServerBtn = obj.ServerBtn;
            TypeOfWeek = obj.TypeOfWeek;
            TypeOfDay = obj.TypeOfDay;
            TypeOfLesson = obj.TypeOfLesson;
            FAQMsg = obj.FAQMsg;
            FAQurl = obj.FAQurl;
            QRMsg = obj.QRMsg;
            LackOfInfo = obj.LackOfInfo;
            WelcomeFile = obj.WelcomeFile;
            ComunicationAnswerFile = obj.ComunicationAnswerFile;
        }
    }
}
