﻿using Discord;
using Discord.WebSocket;

namespace HTWDiscordBot.Services
{
    public class DiscordService
    {
        private readonly DiscordSocketClient client;
        private readonly ConfigService configService;

        public DiscordService(ConfigService configService, DiscordSocketClient client)
        {
            this.configService = configService;
            this.client = client;
        }

        public async Task InitializeAsync()
        {
            client.Ready += Client_ReadyAsync;
            await client.LoginAsync(TokenType.Bot, configService.Config.Token);
            await client.StartAsync();
        }

        //Wird ausgeführt, wenn der Bot bereit ist
        private async Task Client_ReadyAsync()
        {
            await client.SetGameAsync("Hack The Web", type: ActivityType.Playing);
        }

        //Konfiguriert die DiscordSocketConfig
        public static DiscordSocketConfig CreateDiscordSockteConfig()
        {
            DiscordSocketConfig discordSocketConfig = new()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences,
                LogGatewayIntentWarnings = false
            };

            return discordSocketConfig;
        }
    }
}
