using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace MyFirstBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test 5");

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "ODQzMTA4MjYxOTk3OTY5NDE5.YJ_DwQ.sQpXS2nfc_PzNSJrgIeJG9ZtCOY",
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong! 5");

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}