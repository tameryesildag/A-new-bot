using System;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Discord.Commands;
using Discord.API;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

namespace SmileBotCore
{

    public class MainModule : ModuleBase
    {
        static int sıra;
        string[] kills;
        string[] deaths;
        string[] assists;
        string[] result;
        int l = 0;
        static int üstlimitsaniye;
        static int saniye;
        public static string[] secenekler;
        public static int[] oylar;
        static List<ulong> idlist = new List<ulong>();
        int idlersira = 0;
        public static string sorust;
        public int s;
        Timer t2 = new Timer(100);
        IUser typekullanici;
        double typezaman = 0;
        string aranankelime;
        string typemetin = "hey type me to set a record";
        IMessageChannel ch;
        IMessageChannel ch2;
        string dudutext = "Yok";
        SmileBotCore.Program pr = new SmileBotCore.Program();
        SmileBotCore.Riot riot = new SmileBotCore.Riot();
        Ping p;
        static denemeservice ds = new denemeservice();
        [Command("Ping")]
        public async Task Ping()
        {
            await Turkcemi(Context);
            p = new Ping();
            await Context.Channel.SendMessageAsync("**" + p.Send("www.discordapp.com").RoundtripTime.ToString() + "ms" + "**");

        }
        [Command("emoji")]
        [Alias("emote")]
        public async Task service([Remainder] string metin)
        {
            makeitfancy mif = new makeitfancy();
            await Context.Channel.SendMessageAsync(mif._makeitfancy(metin));
        }
        [Command("jsondeneme")]
        public async Task jsondeneme()
        {
            Jsoneditor je = new Jsoneditor();
            je.oylarbul(Context.Guild.Id.ToString());
        }
        [Command("Topla")]
        [Alias("Collect!")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task Topla()
        {
           await Turkcemi(Context);
            var users = Context.Guild.GetUsersAsync();
            
            foreach(IGuildUser u in (Context.Guild as SocketGuild).Users)
            {
                await u.ModifyAsync(x => x.ChannelId = (Context.User as IGuildUser).VoiceChannel.Id);
            }
            if (l == 1) await Context.Channel.SendMessageAsync("Ses kanallarındaki herkes " + (Context.User as IGuildUser).VoiceChannel.Name + " adlı kanala taşınıyor.");
            else await Context.Channel.SendMessageAsync("Everyone in the voice channels is moving to: " + (Context.User as IGuildUser).VoiceChannel.Name);
        }
        [Command("texttobinary")]
        [Alias("binary","ttb")]
        public async Task texttobinary([Remainder] string text)
        {
            string output = "";
            foreach (char c in text)
            {
                output += (Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            await Context.Channel.SendMessageAsync("**" + output + "**");

        }
        [Command("binarytotext")]
        [Alias("text","btt")]
        public async Task binarytotext([Remainder] string binary)
        {
            string asilbinary = binary.Replace(" ", string.Empty);
            var data = GetBytesFromBinaryString(asilbinary);
            var text = Encoding.ASCII.GetString(data);
            await Context.Channel.SendMessageAsync("**" + text + "**");
        }
        [Command("typing-game")]
        public async Task typegame()
        {
            await Turkcemi(Context);
            (Context.Client as DiscordSocketClient).MessageReceived += mesajgelince;
            ch2 = Context.Channel as IMessageChannel;
            typekullanici = Context.User;
            t2.Elapsed += async (sender, e) => await typetimerislem();
            t2.Start();
            if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Bu metni en kısa sürede yaz: \n\n**" + typemetin + "**");
            else await Context.Channel.SendMessageAsync(Context.User.Mention + " Type this text as fast as you can: \n\n**" + typemetin + "**");
            await Task.Delay(3000);
        }
        private Task mesajgelince(SocketMessage msgParam)
        {
            Turkcemi(Context);
            SocketMessage mesaj = msgParam;
            if(mesaj.Content == typemetin)
            {
                double asilsure = typezaman / 10;
                if (l == 1) ch2.SendMessageAsync(typekullanici.Mention + " **" + asilsure.ToString() + "** Saniyede yazdın.");
                else ch2.SendMessageAsync(typekullanici.Mention + " You wrote the text in **" + asilsure.ToString() + "** seconds.");
                t2.Stop();
                typezaman = 0;
                (Context.Client as DiscordSocketClient).MessageReceived -= mesajgelince;
            }
            return Task.CompletedTask;
        }
        [Command("Dil")]
        [Alias("Language")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Dil(string d)
        {
            await Turkcemi(Context);
            if (d == "en" || d == "english" || d == "English" || d == "İngilizce" || d == "ingilizce")
            {
               // var currentuser = Context.Guild.GetCurrentUserAsync();
                IGuildUser Client = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);
               
                await Client.ModifyAsync(x => x.Nickname = "EN Smile Bot");
                await Context.Channel.SendMessageAsync("Language is set to **English**.");
            }
            if (d == "tr" || d == "Türkçe" || d == "türkçe" || d == "Turkish" || d == "turkish")
            {
                IGuildUser Client = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);

                await Client.ModifyAsync(x => x.Nickname = "TR Smile Bot");
                await Context.Channel.SendMessageAsync("Dil **Türkçeye** ayarlandı.");
            }
            if (d != "tr" & d != "en" & d != "Türkçe" & d != "türkçe" & d != "Turkish" & d != "turkish" & d != "english" & d != "English" & d != "İngilizce" & d != "ingilizce")
            {
                await Context.Channel.SendMessageAsync("!language <tr / en>");
            }
            await Task.Delay(10000);
        }
        [Command("sunucular")]
        [Alias("Servers")]
        public async Task sunucular()
        {
            await Turkcemi(Context);
            if (Context.User.Id == 244451433812852736)
            {
                string metin = "";
                foreach (IGuild gui in (Context.Client as DiscordSocketClient).Guilds)
                {
                    metin += "\n- " + gui.Name + " ";
                }
                await Context.Channel.SendMessageAsync("```" + metin + "```");
            }
            else
            {
                if (l == 1) await Context.Channel.SendMessageAsync("Sunucuları görüntülemeye yetkiniz yok.");
                else await Context.Channel.SendMessageAsync("You do not have permission to view servers.");
            }
        }
        [Command("Kapa")]
        [Alias("Close")]
        public async Task Kapa()
        {
            await Turkcemi(Context);
            if (Context.User.Id == 244451433812852736)
            {
                if (l == 1) await Context.Channel.SendMessageAsync("Bot çevrimdışı.");
                else await Context.Channel.SendMessageAsync("Bot is offline.");
                Environment.Exit(0);
               
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.User.Mention + " Yetkiniz yok.");
            }
        }
        [Command("Hava")]
        [Alias("Havadurumu")]
        public async Task Hava(string şehir)
        {
            await Turkcemi(Context);
            SmileBotCore.havadurumu hd = new SmileBotCore.havadurumu();
            hd.sicaklikbul(şehir);
            if (l == 1) await Context.Channel.SendMessageAsync(hd.mesaj + "**" + hd.derece.ToString() + "** derece." + "\nYağış oranı **" + hd.yagisorani + "**" + "\nNem **" + hd.nem + "**");
            else await Context.Channel.SendMessageAsync("The Weather in " + şehir + " **" + hd.derece.ToString() + "** degree" + "\nPrecipitation rate **" + hd.yagisorani + "**" + "\nHumidty **" + hd.nem + "**");
        }
        [Command("Havaradar")]
        [Alias("radar", "weatherradar")]
        public async Task Havaradar()
        {
            await Turkcemi(Context);
            if (l == 1)
            {
                string localFilename = "radar.jpg";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://www.mgm.gov.tr/FTPDATA/uzal/radar/comp/compppi15.jpg", localFilename);
                }
                await Context.Channel.SendFileAsync("radar.jpg", DateTime.Now.ToString());
            }
            else
            {
                await Context.Channel.SendMessageAsync("This feature is disabled in English Version.");
            }
            await Task.Delay(3000);
        }
        [Command("mcskin")]
        [Alias("skin","minecraftskin")]
        public async Task mcskin(string isim)
        {
                string localFilename = @"D:\Visual studio projects\SmileBot\Kodumunbot\bin\Debug\skin.jpg";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://minecraftskinstealer.com/skin.php?u=" + isim + "&s=700", localFilename);
                }
                await Context.Channel.SendFileAsync("skin.jpg");
                await Task.Delay(3000);
        }
        [Command("Özürlü")]
        [Alias("Disabled")]
        public async Task Özürlü([Remainder] string metin)
        {
            await Turkcemi(Context);
            await Context.Message.DeleteAsync();
            char[] charlar;
            charlar = metin.ToCharArray();
            char[] charlar2 = new char[charlar.Length];
            foreach (char c in charlar)
            {
               // Console.WriteLine(c);
            }
            for (int i = 0; i < charlar.Length; i++)
            {
                if (sıra == 0)
                {
                    charlar2[i] = Char.ToUpper(charlar[i]);
                    sıra = 1;
                    continue;
                }
                if (sıra == 1)
                {
                    charlar2[i] = Char.ToLower(charlar[i]);
                    sıra = 0;
                }
            }
            string s = new string(charlar2);
            await Context.Channel.SendFileAsync("sponge.jpg", "**" + s + "**");
        }
        [Command("Steamdurum")]
        [Alias("steam", "steamserver", "steamonline", "steamstatus")]
        public async Task Steamserver()
        {
            await Turkcemi(Context);
            p = new Ping();
            string temp = "";
            try
            {
                temp = ("**" + p.Send("store.steampowered.com").RoundtripTime.ToString() + "ms" + "**");
                if (l == 1) await Context.Channel.SendMessageAsync("**Steam market sunucuları çevrimiçi** ✅");
                else await Context.Channel.SendMessageAsync("**Steam market servers are online** ✅");
            }
            catch
            {
                if (l == 1) await Context.Channel.SendMessageAsync("**Steam market sunucuları çevrimdışı** ❌");
                else await Context.Channel.SendMessageAsync("**Steam market servers are offline** ❌");
            }
        }
        [Command("Anket")]
        [Alias("Oylama", "Poll")]
        public async Task Anket([Remainder] string soru = "")
        {
            Console.WriteLine("Anket komutu tetiklendi. (" + Context.Guild.Name + ")");
                await Turkcemi(Context);
                sorust = soru;
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Soru ayarlandı. Seçenekleri ayarlayın !secenekler <secenek> <secenek> ...");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " Question has been set. Set the options with !Options <option> <option> ...");
        }
        [Command("Secenekler")]
        [Alias("Options", "Choices", "Seçenekler")]
        public async Task secenek(string secenek1 = "", string secenek2 = "", string secenek3 = "", string secenek4 = "", string secenek5 = "")
        {
            await Turkcemi(Context);
            if (sorust == "") {
                if (l == 1) await Context.Channel.SendMessageAsync("İlk önce soruyu ayarlayın. !anket <soru>");
                else await Context.Channel.SendMessageAsync("Set the question first. !poll <question>");
                return;
            }
            if (secenek2 == "")
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Bir anket oluşturabilmeniz için en az 2 seçenek olmalı.");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " There must be at least 2 options to create a poll.");
                return;
            }
            secenekler = new string[5];
            secenekler[0] = secenek1;
            secenekler[1] = secenek2;
            secenekler[2] = secenek3;
            secenekler[3] = secenek4;
            secenekler[4] = secenek5;
            secenekler = secenekler.Except(new string[] { "" }).ToArray();
            int sayi = 1;
            string metin =  Context.User.Mention + " Bir oylama oluşturdu.  \n\n" +"**"+ sorust +"**"+ "\n";
            if (l == 0) metin =  Context.User.Mention + " Created a poll.  \n\n" +"**"+ sorust +"**"+ "\n";
            string title = "Anket";
            if (l == 0) title = "Poll";
            foreach (var vr in secenekler)
            {
                if (sayi == 1)
                {
                    metin += "\n\n**" + ":one:" + "- " + vr + "**";
                }
                if(sayi == 2)
                {
                    metin += "\n\n**" + ":two:" + "- " + vr + "**";
                }
                if (sayi == 3)
                {
                    metin += "\n\n**" + ":three:" + "- " + vr + "**";
                }
                if (sayi == 4)
                {
                    metin += "\n\n**" + ":four:" + "- " + vr + "**";
                }
                sayi += 1;
            }
            if (l == 1) metin += "\n\n Aşşağıdan oylayabilirsiniz :arrow_down:";
            else metin += "\n\n You can vote below :arrow_down:";
            var eb = new EmbedBuilder() { Title = ":clipboard: " + title, Description = metin, Color = Color.LightOrange };
            //  var msg = ReplyAsync("", false, eb);
            var msg = await ReplyAsync("",false,eb);
            IEmote emote1 = new Emoji("1⃣");
            IEmote emote2 = new Emoji("2⃣");
            IEmote emote3 = new Emoji("3⃣");
            IEmote emote4 = new Emoji("4⃣");
            sayi -= 1;
            Console.WriteLine(sayi);
            if(sayi == 2)
            {
                await msg.AddReactionAsync(emote1);
                await msg.AddReactionAsync(emote2);
            }
            if(sayi == 3)
            {
                await msg.AddReactionAsync(emote1);
                await msg.AddReactionAsync(emote2);
                await msg.AddReactionAsync(emote3);
            }
            if(sayi == 4)
            {
                await msg.AddReactionAsync(emote1);
                await msg.AddReactionAsync(emote2);
                await msg.AddReactionAsync(emote3);
                await msg.AddReactionAsync(emote4);
            }
        }
        [Command("Anketbitir")]
        [Alias("Anketbit", "Anketkapa", "finishpoll", "pollresults")]
        public async Task Anketbitir()
        {
            await Turkcemi(Context);
            foreach (var Item in await Context.Channel.GetMessagesAsync(20).Flatten())
            {
                if (Item.Author.Id == Context.Client.CurrentUser.Id && Item.Embeds != null)
                {
                    try
                    {
                        if(Item.Embeds.FirstOrDefault().Title == ":clipboard: Poll" || Item.Embeds.FirstOrDefault().Title == ":clipboard: Anket")
                        {
                            int foreachsira = 0;
                            int r1 = 1;
                            int r2 = 1;
                            int r3 = 1;
                            int r4 = 1;
                          Console.WriteLine("Bir anket mesajı bulundu. (" + Context.Guild.Name + ")");
                          
                          foreach(var vr in (Item as IUserMessage).Reactions)
                            {
                               foreachsira += 1;
                               Console.WriteLine(vr.ToString() + " " + vr.Value.ReactionCount);
                                if (foreachsira == 1) r1 = vr.Value.ReactionCount;
                                if (foreachsira == 2) r2 = vr.Value.ReactionCount;
                                if (foreachsira == 3) r3 = vr.Value.ReactionCount;
                                if (foreachsira == 4) r4 = vr.Value.ReactionCount;
                            }
                            Console.WriteLine("r1" + r1);
                            Console.WriteLine("r2" +r2);
                            Console.WriteLine("r3" +r3);
                            Console.WriteLine("r4" +r4);
                            int[] rarray = new int[] { r1, r2, r3, r4 };
                            int kazananvalue = rarray.Max();
                            int kazananindex = rarray.ToList().IndexOf(kazananvalue);
                            kazananindex += 1;
                            if (l == 1) await Context.Channel.SendMessageAsync(kazananindex.ToString() + ". seçenek  " + kazananvalue.ToString() + " oy ile seçildi.");
                            else await Context.Channel.SendMessageAsync("Option " + kazananindex.ToString() + " is chosen with " + kazananvalue.ToString() + " vote.");

                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Anket bitirilirken bir hata meydana geldi. " + ex);
                    }
                }
            }
        }
        [Command("say")]
        [Alias("gerisay", "geriyesay", "count", "countback", "countdown")]
        public async Task Gerisay(int zaman, string birim = "sn")
        {
            await Turkcemi(Context);
            if (birim == "dk" || birim == "dakika" || birim == "m" || birim == "minute")
            {
                üstlimitsaniye = zaman * 60;
                if (l == 1) await Context.Channel.SendMessageAsync("Geri sayım başladı. Biteceği zaman: **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
                else await Context.Channel.SendMessageAsync("Countdown has started. Countdown will finish at **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
            }
            /*  if (birim == "dakika")
              {
                  üstlimitsaniye = zaman * 60;
                  if (l == 1) await Context.Channel.SendMessageAsync("Geri sayım başladı. Biteceği zaman: **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
                  else await Context.Channel.SendMessageAsync("Countdown has started. Countdown will finish at **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
              } */
            if (birim == "sn" || birim == "saniye" || birim == "s" || birim == "second")
            {
                üstlimitsaniye = zaman;
                if (l == 1) await Context.Channel.SendMessageAsync("Geri sayım başladı. Biteceği zaman: **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
                else await Context.Channel.SendMessageAsync("Countdown has started. Countdown will finish at **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
            }
            /* if (birim == "saniye")
             {
                 üstlimitsaniye = zaman;
                 if (l == 1) await Context.Channel.SendMessageAsync("Geri sayım başladı. Biteceği zaman: **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
                 else await Context.Channel.SendMessageAsync("Countdown has started. Countdown will finish at **" + DateTime.Now.AddSeconds(üstlimitsaniye).ToString() + "**");
             } */
            ch = Context.Channel;

            Timer t = new Timer(1000);
            t.Elapsed += async (sender, e) => await timerislem();
            t.Start();
        }

        [Command("Rastgele")]
        [Alias("Rasgele", "Random")]
        public async Task Rastgele(int altlimit, int üstlimit)
        {
            await Turkcemi(Context);
            int sonuc;
            Random rastgele = new Random();
            sonuc = rastgele.Next(altlimit, üstlimit);
            await Context.Channel.SendMessageAsync("**" + sonuc.ToString() + "**");
            
        }

        [Command("apikey")]
        [Alias("api", "key")]
        public async Task apikey(string key)
        {
            if (Context.User.Id == 244451433812852736)
            {
                await Turkcemi(Context);
                riot.apikey = key;
                await Context.Channel.SendMessageAsync("API anahtarı güncellendi.");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.User.Mention + " Yetkiniz yok.");
            }
           await Task.Delay(1000);
        }
        [Command("Davet")]
        [Alias("Invite", "Createinvite", "davetoluştur", "davetolustur")]
        public async Task Davet()
        {
            await Turkcemi(Context);
            IInvite davet = await (Context.Channel as IGuildChannel).CreateInviteAsync();
            var eb = new EmbedBuilder() { Title = "Davet", Description = davet.Url, Color = Color.Blue };
            await Context.Channel.SendMessageAsync("", false, eb);
        }
        [Command("Çağır")]
        [Alias("Cagir", "Gel","Summon")]
        public async Task deneme()
        {
            await Turkcemi(Context);
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            IAudioClient client = await channel.ConnectAsync();
            var stream = client.CreatePCMStream(AudioApplication.Music);
        }
        [Command("isim")]
        [Alias("ad", "name")]
        public async Task isim(IUser kullanici, [Remainder] string isim)
        {
            await Turkcemi(Context);
            string eski = kullanici.Username;
            await (kullanici as IGuildUser).ModifyAsync(x => x.Nickname = isim);
            await Context.Channel.SendMessageAsync("**" + eski + "**" + " isimli kullanıcının takma adı " + kullanici.Mention + " olarak ayarlandı");
        }
        [Command("Oyun")]
        [Alias("Oyunayarla")]
        public async Task Oyun([Remainder] string oyun)
        {
            await Turkcemi(Context);
            await (Context.Client as DiscordSocketClient).SetGameAsync(oyun);
            if (l == 1) await Context.Channel.SendMessageAsync("Oyun " + "**" + oyun + "**" + " Olarak ayarlandı.");
            else await Context.Channel.SendMessageAsync("Game is set as " + "**" + oyun + "**" + ".");
            await Task.Delay(10000);
        }
        [Command("Saat")]
        [Alias("Zaman", "Tarih", "Gün", "Ay", "Time", "hour", "clock", "Month", "Day")]
        public async Task Saat()
        {
            await Turkcemi(Context);
            if (l == 1) await Context.Channel.SendMessageAsync("Sistem zamanı: " + "**" + DateTime.Now.ToString() + "**");
            else await Context.Channel.SendMessageAsync("System time " + "**" + DateTime.Now.ToString() + "**");
            await Task.Delay(1000);
        }
        [Command("Asal")]
        [Alias("prime", "primenumber", "isprime", "isprimenumber")]
        public async Task Asal(int num1)
        {
            await Turkcemi(Context);
            if (num1 == 0 || num1 == 1)
            {
                //  Console.WriteLine(num1 + " is not prime number");
                if (l == 1) await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " Bir asal sayı değil.");
                else await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " is not a prime number.");

            }
            else
            {
                for (int a = 2; a <= num1 / 2; a++)
                {
                    if (num1 % a == 0)
                    {
                        if (l == 1) await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " Bir asal sayı değil.");
                        else await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " is not a prime number.");
                        return;
                    }

                }
                if (l == 1) await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " Bir asal sayı.");
                else await Context.Channel.SendMessageAsync("**" + num1.ToString() + "**" + " is a prime number.");
            }
        }
        [Command("Hesapla")]
        [Alias("İşlem", "Hesap", "calculate")]
        public async Task Hesapla(double sayi1 = 0, string isaret = "", double sayi2 = 0)
        {
            await Turkcemi(Context);
            if (sayi1 == 0)
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Neyi?");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " Calculate what?");
            }
            if (isaret == "+")
            {
                double sonuc;
                sonuc = sayi1 + sayi2;
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());

            }
            if (isaret == "-")
            {
                double sonuc;
                sonuc = sayi1 - sayi2;
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());
            }
            if (isaret == "*")
            {
                double sonuc;
                sonuc = sayi1 * sayi2;
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());
            }
            if (isaret == "%")
            {
                double sonuc;
                sonuc = (sayi1 * sayi2) / 100;
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());
            }
            if (isaret == "^")
            {
                double sonuc = sayi1;
                for (double i = sayi2; i > 1; i--)
                {
                    sonuc = sonuc * sayi1;
                }
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());
            }
            if (isaret == "!")
            {
                double sonuc = sayi1;
                for (double i = sayi1 - 1; i > 0; i--)
                {
                    sonuc = sonuc * i;
                }
                await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sonuc.ToString());
            }
            await Task.Delay(2000);
        }
        [Command("Embed")]
        public async Task Embed(string başlık, [Remainder] string açıklama)
        {
            await Turkcemi(Context);
            await Context.Message.DeleteAsync();
            var eb = new EmbedBuilder() { Title = "**" + başlık + "**", Description = açıklama, Color = Color.Blue };
            await Context.Channel.SendMessageAsync("", false, eb); 
        }
        [Command("Sağırlaştır")]
        [Alias("Sağır","deaf","deafen","Sagir","Sagirlastir")]
        public async Task Sağırlaştır(IUser kullanici)
        {
            await Turkcemi(Context);
            if ((Context.User as IGuildUser).GetPermissions(Context.Channel as ITextChannel).ManageChannel)
            {
                if ((kullanici as IGuildUser).IsDeafened == false)
                {
                    await (kullanici as IGuildUser).ModifyAsync(x => x.Deaf = true);
                    if (l == 1) await Context.Channel.SendMessageAsync(kullanici.Mention + " sağırlaştırıldı.");
                    else await Context.Channel.SendMessageAsync(kullanici.Mention + " is deafened.");
                }
                else
                {
                    await (kullanici as IGuildUser).ModifyAsync(x => x.Deaf = false);
                    if (l == 1) await Context.Channel.SendMessageAsync(kullanici.Mention + " sağırlaştırılması kaldırıldı.");
                    else await Context.Channel.SendMessageAsync(kullanici.Mention + " is undeafened");
                }
            }
            else
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Bir kullanıcıyı sağırlaştırmak için sunucuda kanalları yönetme yetkisine sahip olmalısınız");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " You need to have 'Manage Channels' permission in server to deafen a user.");
            }

        }
        [Command("Sustur")]
        [Alias("Mute")]
        public async Task Sustur(IUser kullanici)
        {
            await Turkcemi(Context);
            if ((Context.User as IGuildUser).GetPermissions(Context.Channel as ITextChannel).ManageChannel)
            {
                if ((kullanici as IGuildUser).IsMuted == false)
                {
                    await (kullanici as IGuildUser).ModifyAsync(x => x.Mute = true);
                    if (l == 1) await Context.Channel.SendMessageAsync(kullanici.Mention + " susturuldu.");
                    else await Context.Channel.SendMessageAsync(kullanici.Mention + " is muted.");
                }
                else
                {
                    await (kullanici as IGuildUser).ModifyAsync(x => x.Mute = false);
                    if (l == 1) await Context.Channel.SendMessageAsync(kullanici.Mention + " susturulması kaldırıldı.");
                    else await Context.Channel.SendMessageAsync(kullanici.Mention + " is unmuted.");
                }
            }
            else
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Bir kullanıcıyı susturmak için sunucuda kanalları yönetme yetkisine sahip olmanız gerek.");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " You need to have 'Manage Channels' permission in server to mute a user.");
            }
        }
        [Command("Taşı")]
        [Alias("Tasi","Move")]
        public async Task taşı(IUser kullanici,IChannel kanal)
        {
            await Turkcemi(Context);
            if ((Context.User as IGuildUser).GetPermissions(Context.Channel as ITextChannel).ManageChannel)
            {
                await (kullanici as IGuildUser).ModifyAsync(x => x.ChannelId = kanal.Id);
            }
            else
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Mention + " Bir kullanıcının ses kanalını değiştirmek için sunucuda kanalları yönetme yetkisine sahip olmanız gerek.");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " You need to have 'Manage Channels' permission in server to move a user to a different voice channel.");
            }
        }
        

        [Command("Yardım")]
        [Alias("Komutlar","Help","Yardim")]
        public async Task Yardim()
        {
           await Turkcemi(Context);
            string description = "\n!yardım\n!sil <mesaj sayısı> {kullanici}\n!dil <Türkçe / İngilizce>\n!sihirdar <sihirdar ismi>\n!taşı <kullanıcı> <hedef kanal>\n!sustur <kullanıcı>\n!sağırlaştır <kullanıcı>\n!embed <başlık> <açıklama>\n!hesapla <sayı> <işlem> <sayı>\n!saat" +
                "\n!oyun <isim>\n!isim <kullanıcı> <yeni isim>\n!davet\n!̶a̶p̶i̶k̶e̶y̶ ̶<̶R̶i̶o̶t̶ ̶a̶p̶i̶ ̶a̶n̶a̶h̶t̶a̶r̶ı̶>̶\n!rastgele <alt limit> <üst limit>\n!ping\n!asal <sayı>\n!say <süre> {Sn / Dk}\n!anket <soru>\n!secenekler <secenek> <secenek> ..\n!anketbitir"
                + "\n!hava <şehir>\n!havaradar\n!özürlü <başlık>\n!binary <yazı>\n!text <binary>\n!mcskin <username>\n!emoji <metin>\n!topla";
            description += "\n\n Bot davet linki: http://goo.gl/EkUUb7";

            string description2 = "Kullanılabilir komutlar: \nSüslü parantez içerisinde olan parametreler isteğe bağlıdır.\n**";
            string title = "Github için tıklayın";
            if (l == 0)
            {
                description = "\n!help\n!delete <number of messages> {user}\n!language <English / Turkish>\n!summoner <summoner name>\n!move <user> <target channel>\n!mute <user>\n!deaf <user>\n!embed <title> <description>\n!calculate <number> <operation> <number>\n!time" +
                "\n!game <game name>\n!name <user> <new name>\n!invite\n!random <lower limit> <upper limit>\n!ping\n!isprime <number>\n!count <time> {s / m}\n!poll <question>\n!options <option> <option> ..\n!finishpoll"
                + "\n!weather <city>\n!binary <text>\n!text <binary>\n!mcskin <username>\n!emoji <text>\n!collect";
                description += "\n\n Bot invite link: http://goo.gl/EkUUb7";
                title = "Click for Github";
                description2 = "Available commands: \nParameters in fancy brackets are optional.\n**";
            }  
            var eb = new EmbedBuilder() { Title = title, Description = description2 + description + "**", Color = Color.Blue };
            var fb = new EmbedFieldBuilder();
            eb.WithUrl("https://www.github.com/tmr0222/SmileBot");
            await Context.Channel.SendMessageAsync("",false,eb);
        }
        [Command("Sihirdar")]
        [Alias("Summoner")]
        public async Task Sihirdar([Remainder] string isim)
        {
            await Turkcemi(Context);
            try
            {
                Console.WriteLine(isim + "aranıyor");
                if (l == 1) await Context.Channel.SendMessageAsync(isim + " Aranıyor...");
                else await Context.Channel.SendMessageAsync("Searching for " + isim + "...");
                kills = new string[5];
                deaths = new string[5];
                assists = new string[5];
                result = new string[5];
                isim.Replace(" ", "%20");


                // 1
                riot.summonerbul(isim);
                riot.macbilgi(riot.summonerid, riot.mac1id);
                kills[0] = riot.kill;
                deaths[0] = riot.death;
                assists[0] = riot.assist;
                result[0] = riot.macsonuc;
                // 2
                riot.summonerbul(isim);
                riot.macbilgi(riot.summonerid, riot.mac2id);
                kills[1] = riot.kill;
                deaths[1] = riot.death;
                assists[1] = riot.assist;
                result[1] = riot.macsonuc;
                // 3
                riot.summonerbul(isim);
                riot.macbilgi(riot.summonerid, riot.mac3id);
                kills[2] = riot.kill;
                deaths[2] = riot.death;
                assists[2] = riot.assist;
                result[2] = riot.macsonuc;
                // 4
                riot.summonerbul(isim);
                riot.macbilgi(riot.summonerid, riot.mac4id);
                kills[3] = riot.kill;
                deaths[3] = riot.death;
                assists[3] = riot.assist;
                result[3] = riot.macsonuc;
                // 5
                riot.summonerbul(isim);
                riot.macbilgi(riot.summonerid, riot.mac5id);
                kills[4] = riot.kill;
                deaths[4] = riot.death;
                assists[4] = riot.assist;
                result[4] = riot.macsonuc;

                var eb = new EmbedBuilder() { Title = "", Description = "**Maçlar**", Color = Color.Blue };
                if (l == 0) eb = new EmbedBuilder() { Title = "", Description = "**Matches**", Color = Color.Blue };
                string newname = riot.summonername;
                newname.First().ToString().ToUpper();
                EmbedAuthorBuilder MyAuthorBuilder = new EmbedAuthorBuilder();
                if (l == 1) MyAuthorBuilder.WithName(newname + " - Tüm profil için tıkla");
                else MyAuthorBuilder.WithName(newname + " - Click for complete profile");
                MyAuthorBuilder.WithIconUrl("https://i.hizliresim.com/JlvA4B.jpg");
                eb.WithAuthor(MyAuthorBuilder);
                for (int i = 0; i < 5; i++)
                {
                    if (result[i] == "Zafer")
                    {
                      if(l == 1)  result[i] = ":white_check_mark: Zafer";
                      else result[i] = ":white_check_mark: Win";
                    }
                    if (result[i] == "Bozgun")
                    {
                       if(l == 1) result[i] = ":no_entry_sign: Bozgun";
                       else result[i] = ":no_entry_sign: Loss";
                    }
                }
                EmbedFieldBuilder MyEmbedField = new EmbedFieldBuilder();
                MyEmbedField.WithIsInline(true);
                MyEmbedField.WithName(result[0]);
                MyEmbedField.WithValue(kills[0] + "/" + deaths[0] + "/" + assists[0]);
                eb.AddField(MyEmbedField);

                EmbedFieldBuilder MyEmbedField2 = new EmbedFieldBuilder();
                MyEmbedField2.WithIsInline(true);
                MyEmbedField2.WithName(result[1]);
                MyEmbedField2.WithValue(kills[1] + "/" + deaths[1] + "/" + assists[1]);
                eb.AddField(MyEmbedField2);

                EmbedFieldBuilder MyEmbedField3 = new EmbedFieldBuilder();
                MyEmbedField3.WithIsInline(true);
                MyEmbedField3.WithName(result[2]);
                MyEmbedField3.WithValue(kills[2] + "/" + deaths[2] + "/" + assists[2]);
                eb.AddField(MyEmbedField3);

                EmbedFieldBuilder MyEmbedField4 = new EmbedFieldBuilder();
                MyEmbedField4.WithIsInline(true);
                MyEmbedField4.WithName(result[3]);
                MyEmbedField4.WithValue(kills[3] + "/" + deaths[3] + "/" + assists[3]);
                eb.AddField(MyEmbedField4);

                EmbedFieldBuilder MyEmbedField5 = new EmbedFieldBuilder();
                MyEmbedField5.WithIsInline(true);
                MyEmbedField5.WithName(result[4]);
                MyEmbedField5.WithValue(kills[4] + "/" + deaths[4] + "/" + assists[4]);
                eb.AddField(MyEmbedField5);

                EmbedFieldBuilder MyEmbedField6 = new EmbedFieldBuilder();
                MyEmbedField6.WithIsInline(true);
                MyEmbedField6.WithName("...");
                MyEmbedField6.WithValue("...");
                eb.AddField(MyEmbedField6);

                eb.WithUrl("http://www.lolking.net/summoner/tr/" + riot.summonerid2);

                await Context.Channel.SendMessageAsync("", false, eb);
               // Task.Delay(5000);
            }
            catch(Exception e)
            {
                if (l == 1) await Context.Channel.SendMessageAsync("```İşlemi gerçekleştirirken bir hata meydana geldi.\n" + e.Message + "\nAPI Anahtarını güncelleyin. \ndeveloper.riotgames.com```");
                else await Context.Channel.SendMessageAsync("```Error: \n" + e.Message + "" + "\nUpdate the API key. \ndeveloper.riotgames.com```");
            }
        }

        [Command("Sil")]
        [Alias("Delete")]
        public async Task Sil(int sayi, IUser kullanici = null)
        {
            await Turkcemi(Context);
            await Context.Message.DeleteAsync();
            if ((Context.User as IGuildUser).GetPermissions(Context.Channel as ITextChannel).ManageMessages)
            {
                int Amount = 0;
                foreach (var Item in await Context.Channel.GetMessagesAsync(sayi).Flatten())
                {
                    if (kullanici != null)
                    {
                        if (Item.Author == kullanici)
                        {
                            await Item.DeleteAsync();
                        }
                    }
                    else
                    {
                        Amount++;
                        await Item.DeleteAsync();
                    }

                   await Task.Delay(5000);
                }
            }
            else
            {
                if (l == 1) await Context.Channel.SendMessageAsync(Context.User.Username + " Mesaj silebilmeniz için sunucuda mesajları yönetme yetkisine sahip olmanız gerek.");
                else await Context.Channel.SendMessageAsync(Context.User.Mention + " You need to have 'Manage Messages' permission in server to delete messages.");
            }
 
        }
        private async Task timerislem()
        {
            
            saniye += 1;
            if(saniye == üstlimitsaniye)
            {
                await Turkcemi(Context);
                if (l == 1) await ch.SendMessageAsync("Geri sayım bitti. (**" + üstlimitsaniye + "** saniye)");
                else await ch.SendMessageAsync("Countdown is over. (**" + üstlimitsaniye + "** seconds)");
            }
        }
        public Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }
        private async Task typetimerislem()
        {
            typezaman += 1;
        }
        private async Task Turkcemi(ICommandContext cx)
        {
            IGuildUser gu = await cx.Guild.GetUserAsync(cx.Client.CurrentUser.Id);
            if (gu.Nickname.Contains("TR"))
            {
                l = 1;
            }
            else
            {
                l = 0;
            }
        }
    }
}            
