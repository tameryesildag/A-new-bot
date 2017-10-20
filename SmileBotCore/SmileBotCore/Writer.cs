using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SmileBotCore
{
   public class Writer
    {
       public StreamWriter yazici;
       public string okunanyazi1;
       public string okunanyazi2;
        string satir1;
        string satir2;

  
        public void Write(string isim, string yazilacak, int para)
        {
            string dosya_yolu = @"D:\Visual studio projects\SmileBot\Kodumunbot\bin\Debug\userData\" + isim + ".txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            
            sw.WriteLine(yazilacak);
            
            sw.Flush();
            sw.Close();
            fs.Close();
            dosyayaekle(dosya_yolu, para.ToString());
        }
        public void Read(string isim)
        {
            string dosya_yolu = @"D:\Visual studio projects\SmileBot\Kodumunbot\bin\Debug\userData\" + isim + ".txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            okunanyazi1 = sw.ReadLine();
            okunanyazi2 = sw.ReadLine();
            /* while (yazi != null)
             {
                 okunanyazi = yazi;
                 Console.WriteLine(okunanyazi);
                 yazi = sw.ReadLine();

             } */
            sw.Close();
            fs.Close(); 
        } 
        public void dosyayaekle(string dosyayolu, string yazilacak)
        {
            File.AppendAllText(dosyayolu, yazilacak);
        }
        public void paraekle(string isim, int miktar)
        {
            string dosya_yolu = @"D:\Visual studio projects\SmileBot\Kodumunbot\bin\Debug\userData\" + isim + ".txt";
            Read(isim);
            satir1 = okunanyazi1;
            satir2 = okunanyazi2;
            int yenipara = Int32.Parse(satir2) + miktar;
            satir2 = yenipara.ToString();
            File.Delete(dosya_yolu);
            Write(isim, satir1, yenipara);

        }
        public void parasil(string isim, int miktar)
        {
            string dosya_yolu = @"D:\Visual studio projects\SmileBot\Kodumunbot\bin\Debug\userData\" + isim + ".txt";
            Read(isim);
            satir1 = okunanyazi1;
            satir2 = okunanyazi2;
            int yenipara = Int32.Parse(satir2) - miktar;
            satir2 = yenipara.ToString();
            File.Delete(dosya_yolu);
            Write(isim, satir1, yenipara);

        }

    }
}
