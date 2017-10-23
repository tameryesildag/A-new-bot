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
        string token = ":)";

        
        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection()
            .AddSingleton(client)
            .AddSingleton(commands)
            .AddSingleton(new denemeservice())
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
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private Task Baslangic()
        {
            client.SetGameAsync("!language <en/tr> !help");
            Console.WriteLine("Hazır");
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

