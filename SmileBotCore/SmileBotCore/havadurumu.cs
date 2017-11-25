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
        string ankaracor = "39.93,32.86";
        string kahramanmarascor = "37.57,36.92";
        string istanbulcor = "41.01,28.97";
        string antalyacor = "36.89,30.71";
        string newyorkcor = "40.71,-73.99";
        string izmircor = "38.42,27.14";
        string berlincor = "52.52,13.40";
        string kualalumpurcor = "3.13,101.68";
        string tokyocor = "35.68,139.69";
        string moskovacor = "55.75,37.61";
        string pariscor = "48.85,2.35";
        string dubaicor = "25.20,55.27";
        string riodejaneirocor = "-22.90,-43.17";
        string londracor = "51.50,-0.12";
        string antartikacor = "-83.30,-17.22";
        public void sicaklikbul(string şehir)
        {
            şehir = şehir.ToLower();
            if (şehir == "ankara")
            {
                apicalistir(ankaracor);
                mesaj = "Ankara'da hava şuan ";
            }
            if(şehir == "kahramanmaraş")
            {
                apicalistir(kahramanmarascor);
                mesaj = "Kahramanmaraş'ta hava şuan ";
            }
            if(şehir == "istanbul")
            {
                apicalistir(istanbulcor);
                mesaj = "İstanbul'da hava şuan ";
            }
            if(şehir == "antalya")
            {
                apicalistir(antalyacor);
                mesaj = "Antalya'da hava şuan ";
            }
            if(şehir == "new york" || şehir == "newyork")
            {
                apicalistir(newyorkcor);
                mesaj = "New york'ta hava şuan ";
            }
            if(şehir == "izmir")
            {
                apicalistir(izmircor);
                mesaj = "İzmir'de hava şuan ";
            }
            if(şehir == "berlin")
            {
                apicalistir(berlincor);
                mesaj = "Berlin'de hava şuan ";
            }
            if(şehir == "kuala lumpur")
            {
                apicalistir(kualalumpurcor);
                mesaj = "Kuala Lumpur'da hava şuan ";
            }
            if(şehir == "tokyo")
            {
                apicalistir(tokyocor);
                mesaj = "Tokyo'da hava şuan ";
            }
            if(şehir == "moskova")
            {
                apicalistir(moskovacor);
                mesaj = "Moskova'da hava şuan ";
            }
            if(şehir == "paris")
            {
                apicalistir(pariscor);
                mesaj = "Paris'te hava şuan ";
            }
            if(şehir == "dubai")
            {
                apicalistir(dubaicor);
                mesaj = "Dubai'de hava şuan ";
            }
            if(şehir == "rio" || şehir == "rio de janeiro")
            {
                apicalistir(riodejaneirocor);
                mesaj = "Rio de Janeiro'da hava şuan ";
            }
            if(şehir == "londra")
            {
                apicalistir(londracor);
                mesaj = "Londra'da hava şuan ";
            }
            if(şehir == "antartika")
            {
                apicalistir(antartikacor);
                mesaj = "Antartika'da hava şuan ";
            }
        }
        public void apicalistir(string cor)
        {
            // https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/39.93,32.86
            string url = "https://api.darksky.net/forecast/942e70eb107870d818feed0a51c5b0d5/" + cor;
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
