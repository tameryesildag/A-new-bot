using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
// using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;

namespace SmileBotCore
{
    class havadurumu
    {
        public int derece;
        public string yagisorani = "Yok";
        public string mesaj;
        public string nem;
        public void sicaklikbul(string şehir)
        {
            if (şehir == "Ankara")
            {
                // https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/39.93,32.86
                string url = "https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/39.93,32.86";
               // var jss = new JavaScriptSerializer();
                var jsunn = new WebClient().DownloadString(url);
                JsonConvert.DeserializeObject(jsunn);
                dynamic dynObj = JsonConvert.DeserializeObject(jsunn);
                string derecefh = dynObj.currently.temperature;
                Console.WriteLine(dynObj.currently.temperature);
                string input = derecefh;
                int index = input.IndexOf(",");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }
                int index2 = input.IndexOf(".");
                if (index2 > 0)
                {
                    input = input.Substring(0, index2);
                }
                double inthali = Convert.ToDouble(input);
                Console.WriteLine(input);
                inthali = 5.0 / 9.0 * (inthali - 32);
                Console.WriteLine(inthali);
                derece = Convert.ToInt32(inthali);
                mesaj = "Ankara'da hava şuan ";
                yagisorani = dynObj.currently.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok";
                }
                nem = dynObj.currently.humidity;
                Console.WriteLine(nem);
            }
            if (şehir == "Kahramanmaraş")
            {
                string url = "https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/37.57,36.92";
               // var jss = new JavaScriptSerializer();
                var jsunn = new WebClient().DownloadString(url);
                JsonConvert.DeserializeObject(jsunn);
                dynamic dynObj = JsonConvert.DeserializeObject(jsunn);
                string derecefh = dynObj.currently.temperature;
                Console.WriteLine(dynObj.currently.temperature);
                string input = derecefh;
                int index = input.IndexOf(",");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }
                int index2 = input.IndexOf(".");
                if (index2 > 0)
                {
                    input = input.Substring(0, index2);
                }
                double inthali = Convert.ToDouble(input);
                Console.WriteLine(input);
                inthali = 5.0 / 9.0 * (inthali - 32);
                Console.WriteLine(inthali);
                derece = Convert.ToInt32(inthali);
                mesaj = "Kahramanmaraş'da hava şuan ";
                yagisorani = dynObj.currently.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok";
                }
                nem = dynObj.currently.humidity;
                Console.WriteLine(nem);
            }
            if (şehir == "İstanbul")
            {
                string url = "https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/41.01,28.97";
              //  var jss = new JavaScriptSerializer();
                var jsunn = new WebClient().DownloadString(url);
                JsonConvert.DeserializeObject(jsunn);
                dynamic dynObj = JsonConvert.DeserializeObject(jsunn);
                string derecefh = dynObj.currently.temperature;
                Console.WriteLine(dynObj.currently.temperature);
                string input = derecefh;
                int index = input.IndexOf(",");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }
                int index2 = input.IndexOf(".");
                if (index2 > 0)
                {
                    input = input.Substring(0, index2);
                }
                double inthali = Convert.ToDouble(input);
                Console.WriteLine(input);
                inthali = 5.0 / 9.0 * (inthali - 32);
                Console.WriteLine(inthali);
                derece = Convert.ToInt32(inthali);
                mesaj = "İstanbul'da hava şuan ";
                yagisorani = dynObj.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok.";
                }
                yagisorani = dynObj.currently.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok";
                }
                nem = dynObj.currently.humidity;
                Console.WriteLine(nem);
            }
            if(şehir == "Antalya")
            {
                string url = "https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/36.89,30.71";
              //  var jss = new JavaScriptSerializer();
                var jsunn = new WebClient().DownloadString(url);
                JsonConvert.DeserializeObject(jsunn);
                dynamic dynObj = JsonConvert.DeserializeObject(jsunn);
                string derecefh = dynObj.currently.temperature;
                Console.WriteLine(dynObj.currently.temperature);
                string input = derecefh;
                int index = input.IndexOf(",");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }
                int index2 = input.IndexOf(".");
                if (index2 > 0)
                {
                    input = input.Substring(0, index2);
                }
                double inthali = Convert.ToDouble(input);
                Console.WriteLine(input);
                inthali = 5.0 / 9.0 * (inthali - 32);
                Console.WriteLine(inthali);
                derece = Convert.ToInt32(inthali);
                mesaj = "Antalya'da hava şuan ";
                yagisorani = dynObj.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok.";
                }
                yagisorani = dynObj.currently.precipIntensity;
                if (yagisorani == null)
                {
                    yagisorani = "yok";
                }
                nem = dynObj.currently.humidity;
                Console.WriteLine(nem);
            }
        }
    }
}
