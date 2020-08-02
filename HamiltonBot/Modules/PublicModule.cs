using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Discord;
using Discord.Commands;
using HamiltonBot.Models;
using HamiltonBot.Services;

namespace HamiltonBot.Modules
{
    [Name("Public ")]
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public CommandService CommandService { get; set; }

        public HelperService HelperService { get; set; }
        public PictureService PictureService { get; set; }

        // Ban a user
        [Command("ban")]
        [RequireContext(ContextType.Guild)]
        // make sure the user invoking the command can ban
        [RequireUserPermission(GuildPermission.BanMembers)]
        // make sure the bot itself can ban
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync("ok!");
        }

        [Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);

        // Setting a custom ErrorMessage property will help clarify the precondition error
        [Command("guild_only")]
        [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
        public Task GuildOnlyCommand()
            => ReplyAsync("Nothing to see here!");

        [Command("help")]
        public async Task Help(String cmd = "")
        {
            try
            {
                List<ModuleInfo> modules = CommandService.Modules.ToList();
                List<CommandInfo> commands = CommandService.Commands.ToList();

                var builder = HelperService.StandardGuildEmbed(Context, HelperService.StandardColor.Success);

                if (string.IsNullOrWhiteSpace(cmd))
                {
                    string embedFieldText = string.Empty;
                    foreach (var module in modules)
                    {
                        if (module.Commands.Count == 0) { continue; }

                        foreach (var command in module.Commands)
                        {
                            embedFieldText += $"{command.Name}, ";
                        }
                        builder.AddField(module.Name, embedFieldText.Trim().TrimEnd(','), true);
                        embedFieldText = string.Empty;
                    }

                    await ReplyAsync(embed: builder.WithCurrentTimestamp().WithDescription("For more detailed information on a command, use `!help <command>`.").WithTitle("Help").Build());
                }
                else
                {
                    try
                    {
                        cmd = cmd.Trim().ToLower();
                        var module = modules.SingleOrDefault(m => m.Name.Remove(m.Name.LastIndexOf(' ')).ToLower() == cmd);
                        if (module == null)
                        {
                            module = commands.SingleOrDefault(c => c.Name.ToLower() == cmd)?.Module;
                            if (module == null)
                            {
                                foreach (var command in commands)
                                {
                                    foreach (var alias in command.Aliases)
                                    {
                                        if (alias == cmd)
                                        {
                                            module = command.Module;
                                        }
                                    }
                                }
                            }
                        }

                        if (module == null)
                        {
                            throw new KeyNotFoundException($"Format: `!help <command?>`.\n`<command?>`: Command `{cmd}` does not exist.");
                        }

                        foreach (var command in module.Commands)
                        {
                            builder.AddField($"{command.Name}", $"{command.Summary ?? "No summary available."}");
                        }

                        await ReplyAsync(embed: builder.WithCurrentTimestamp().WithTitle($"{module.Name}").Build());
                    }
                    catch (KeyNotFoundException ex)
                    {
                        await ReplyAsync(embed: HelperService.GuildFailureEmbed(Context, ex));
                    }
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync(embed: HelperService.GuildFailureEmbed(Context, ex));
            }
        }

        // 'params' will parse space-separated elements into a list
        [Command("list")]
        public Task ListAsync(params string[] objects)
            => ReplyAsync("You listed: " + string.Join("; ", objects));

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }
    }
}