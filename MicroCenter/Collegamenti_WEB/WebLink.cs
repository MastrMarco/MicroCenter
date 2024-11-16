using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCenter.Collegamenti_WEB
{
   internal class WebLink
    {
        private string _GitHub = "https://github.com/MastrMarco";

        private string _YouTube = "https://www.youtube.com/@mastrmarco";

        private string _Telegram = "https://t.me/MastrMarco_YT";

        private string _Co_Fi = "https://ko-fi.com/mastrmarco";

        private string _Info_Software = "https://github.com/MastrMarco/MicroCenter";



        public string GitHub
        {
            get { return _GitHub; }

        }

        public string Youtube
        {
            get { return _YouTube; }

        }
        public string Telegram
        {
            get { return _Telegram; }

        }
        public string Donazioni
        {
            get { return _Co_Fi; }

        }
        public string Informazioni
        {
            get { return _Info_Software; }

        }
    }
}
