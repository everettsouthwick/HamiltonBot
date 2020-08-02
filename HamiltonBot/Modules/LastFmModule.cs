using Discord.Addons.Interactive;
using Discord.Commands;
using HamiltonBot.Services;
using IF.Lastfm.Core;
using IF.Lastfm.Core.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamiltonBot.Modules
{
    [Name("LastFM 🎧")]
    public class LastFmModule : InteractiveBase
    {
        public HelperService HelperService { get; set; }

        //[Command("approvalpolls")]
        //[Alias("ap", "approval")]
        //[Summary("Displays a list of latest Presidential approval polls.\n**Usage:** `!approvalpolls`\n**Aliases:** `!ap`, `!approval`")]
        //public async Task ApprovalPollsAsync()
        //{
        //    using (TextReader file = new StreamReader(@"C:\Users\Everett\source\repos\HamiltonBot\HamiltonBot\Modules\import.csv"))
        //    {
        //        using (var csv = new CsvReader(file, CultureInfo.InvariantCulture))
        //        {
        //            csv.Configuration.RegisterClassMap<ApprovalPollMap>();
        //            try
        //            {
        //                var polls = await csv.GetRecordsAsync<ApprovalPoll>().ToListAsync();

        //                var message = HelperService.StandardPaginatedMessage(Context, HelperService.StandardColor.Success);

        //                List<string> pages = new List<string>();
        //                string description = string.Empty;
        //                for (int i = 0; i <= polls.Count; i++)
        //                {
        //                    if (i > 0 && i % 10 == 0)
        //                    {
        //                        pages.Add(description);
        //                        description = string.Empty;
        //                    }

        //                    if (i >= 100) { break; }

        //                    polls[i].Spread = polls[i].Yes - polls[i].No;
        //                    description += $"{polls[i].EndDate.ToShortDateString()} [{polls[i].DisplayName}]({polls[i].Url}): **Yes** {polls[i].Yes}% - **No** {polls[i].No}% ({(polls[i].Spread > 0 ? "+" : "")}{polls[i].Spread})\n";
        //                }

        //                message.Title = "Presidential Approval Polls";
        //                message.Pages = pages;

        //                await PagedReplyAsync(message);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //        }
        //    }
        //}
    }
}