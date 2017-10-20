using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace SmileBotCore
{
    class Jsoneditor
    {
        public int secenek1oylar;
        public int secenek2oylar;
        public int secenek3oylar;
        public int secenek4oylar;
        public int secenek5oylar;

        public void oylarbul(string id)
        {
            string JSON = File.ReadAllText("oylamalar.json");
            dynamic str = JsonConvert.DeserializeObject(JSON);
            foreach(var vr in str)
            {
                foreach(var vr2 in vr)
                {
                    foreach(var vr3 in vr2)
                    {
                        if(vr3.id == id)
                        {
                            Console.WriteLine("bulduk " + vr3.secenek1);
                        }
                    }
                }
            }
        }
        public void yaz()
        {
            string JSON = File.ReadAllText("oylamalar.json");
            dynamic str = JsonConvert.DeserializeObject(JSON);
            
        }
        public void oku()
        {

        }
    }
}
