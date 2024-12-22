using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilioMVVM.Models
{
    internal class About
    {
        public string Title => AppInfo.Name;
        //public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://www.instagram.com/emi_lio_gue/";
        public string Message => "Emilio Guerrero";
    }
}
