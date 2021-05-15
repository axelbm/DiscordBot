using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace MyFirstBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

            // Console.WriteLine("test 3");


            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong pong!");

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}