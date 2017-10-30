using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;


namespace SmileBotCore
{
    public class Program : ModuleBase
    {
        private CommandService commands;
        public DiscordSocketClient client;
        private IServiceProvider services;
        string token = "MzQzODk4ODY4MzU1OTU2NzM3.DM0Crw.bxOQvLl4JjdwpLw7VAd6dlNTBrA";



        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection()
            .AddSingleton(client)
            .AddSingleton(commands)
            .BuildServiceProvider(); 

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            client.Log += Log;
            client.Ready += Baslangic;
            client.GuildMemberUpdated += (olduser, newuser) => updated(olduser, newuser);
            
            Console.ReadKey();
        }
        public async Task InstallCommands()
        {
            client.MessageReceived += HandleCommands;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task HandleCommands(SocketMessage msgParam)
        {
            var msg = msgParam as SocketUserMessage;
            char prefix = '!';
            if (msg == null) return;
            if (msg.Author.IsBot) return;
            //  Console.WriteLine(msg.Content);
            int argPos = 0;
            if (!(msg.HasCharPrefix(prefix, ref argPos) || msg.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            
            var context = new CommandContext(client, msg);
            var result = await commands.ExecuteAsync(context, argPos, services);
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt") + "Komut tetiklendi " + (msgParam.Channel as IGuildChannel).Guild.Name);
            if (!result.IsSuccess)
            {
                if (result.ErrorReason != "The input text has too few parameters.")
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            if(result.ErrorReason == "The input text has too few parameters.")
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt") + "Eksik parametre / " + msg.Content.Substring(1, msg.Content.Length - 1));
                foreach(var vr in commands.Commands)
                {
                    if (msg.Content.Substring(1, msg.Content.Length - 1) == vr.Name.ToLower())
                    {
                        await msg.Channel.SendMessageAsync("**" + vr.Summary + "**");
                    }
                    else
                    {
                        foreach (var vre in vr.Aliases)
                        {
                            if (msg.Content.Substring(1, msg.Content.Length - 1) == vre.ToLower())
                            {
                                await msg.Channel.SendMessageAsync("**" + vr.Summary + "**");
                            }

                        }
                    }
                }
            }
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private Task Baslangic()
        {
            client.SetGameAsync("!language <en/tr> !help");
            client.CurrentUser.ModifyAsync(x =>
            {
                x.Username = "BIPBOPBIP";
            });
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt") + "Hazır");
            return Task.CompletedTask;
        }
        private Task updated(SocketGuildUser arg1, SocketGuildUser arg2)
        {
            if (arg2.Id == 343898868355956737)
            {
                
                arg2.Guild.DefaultChannel.SendMessageAsync("**❗Do not change my nickname by yourself. I may stop working.❗**");
            }
            return Task.CompletedTask;
        }
    }
}

