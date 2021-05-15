using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace MyFirstBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bonjour");

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
                if (e.Message.Content.ToLower().StartsWith("piing"))
                {
                    var newMessage = await e.Message.RespondAsync("pong!");

                    var emoji = DiscordEmoji.FromName(s, ":ok_hand:");

                    await newMessage.CreateReactionAsync(emoji);
                }

            };

            discord.MessageReactionAdded += async (s, e) =>
            {
                await ReactionChanged(s, e.Emoji, e.Message, e.User, true);
            };

            discord.MessageReactionRemoved += async (s, e) =>
            {
                await ReactionChanged(s, e.Emoji, e.Message, e.User, false);
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        static async Task ReactionChanged(DiscordClient discordClient, DiscordEmoji emoji, DiscordMessage message, DiscordUser user, Boolean added)
        {
            if (discordClient.CurrentUser.Equals(user)) return;
            if (!discordClient.CurrentUser.Equals(message.Author)) return;

            await message.ModifyAsync(message.Content.Substring(0, message.Content.Length - 1) + (added ? " ping!" : " pong!"));
        }
    }
};