using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HamiltonBot.Services
{
    public class HelperService
    {
        public enum StandardColor
        {
            Success,
            Failure
        }

        public EmbedBuilder CreateStandardUserEmbed(SocketCommandContext Context)
        {
            return new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl(),
                    Name = Context.User.Username
                },
                Color = GetColorFromStatus(Context.User.Status)
            };
        }

        public Color GetColorFromStatus(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.AFK:
                case UserStatus.Idle:
                    return new Color(250, 166, 26);

                case UserStatus.DoNotDisturb:
                    return new Color(240, 71, 71);

                case UserStatus.Invisible:
                case UserStatus.Offline:
                    return new Color(48, 49, 54);

                case UserStatus.Online:
                default:
                    return new Color(67, 181, 129);
            }
        }

        public Color GetStandardColor(StandardColor color)
        {
            switch (color)
            {
                case StandardColor.Success:
                    return new Color(67, 181, 129);

                case StandardColor.Failure:
                default:
                    return new Color(240, 71, 71);
            }
        }

        public Embed GuildFailureEmbed(SocketCommandContext Context, Exception ex)
        {
            var builder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.Guild.IconUrl,
                    Name = Context.Guild.Name
                },
                Color = GetStandardColor(StandardColor.Failure),
                Description = $"{ex.Message} {new Emoji("❌")}",
                Title = "Error!"
            };

            return builder.WithCurrentTimestamp().Build();
        }

        public EmbedBuilder StandardGuildEmbed(SocketCommandContext Context, StandardColor color)
        {
            return new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.Guild.IconUrl,
                    Name = Context.Guild.Name
                },
                Color = GetStandardColor(color)
            };
        }

        public PaginatedMessage StandardPaginatedMessage(SocketCommandContext Context, StandardColor color)
        {
            return new PaginatedMessage
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.Guild.IconUrl,
                    Name = Context.Guild.Name
                },
                Color = GetStandardColor(color),
                Options = new PaginatedAppearanceOptions
                {
                    DisplayInformationIcon = false,
                    Stop = new Emoji("❌")
                }
            };
        }

        public Embed UserFailureEmbed(SocketCommandContext Context, Exception ex)
        {
            var builder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl(),
                    Name = Context.User.Username
                },
                Color = GetStandardColor(StandardColor.Failure),
                Description = $"{ex.Message} {new Emoji("❌")}",
                Title = "Error!"
            };

            return builder.WithCurrentTimestamp().Build();
        }
    }
}