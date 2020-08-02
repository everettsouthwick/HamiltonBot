using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Text;

namespace HamiltonBot.Models
{
    public class ApprovalPoll
    {
        public int QuestionId { get; set; }
        public int PollId { get; set; }
        public string State { get; set; }
        public int PoliticianId { get; set; }
        public string Politician { get; set; }
        public int PollsterId { get; set; }
        public string Pollster { get; set; }
        public string DisplayName { get; set; }
        public int PollsterRatingId { get; set; }
        public string PollsterRatingName { get; set; }
        public string FteGrade { get; set; }
        public int SampleSize { get; set; }
        public Population Population { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Tracking { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public decimal Yes { get; set; }
        public decimal No { get; set; }
        public decimal Spread { get; set; }
    }

    public sealed class ApprovalPollMap : ClassMap<ApprovalPoll>
    {
        public ApprovalPollMap()
        {
            Map(m => m.QuestionId).Name("question_id").Default(0);
            Map(m => m.PollId).Name("poll_id").Default(0);
            Map(m => m.State).Name("state");
            Map(m => m.PoliticianId).Name("politician_id").Default(0);
            Map(m => m.Politician).Name("politician");
            Map(m => m.PollsterId).Name("pollster_id").Default(0);
            Map(m => m.Pollster).Name("pollster");
            Map(m => m.DisplayName).Name("display_name");
            Map(m => m.PollsterRatingId).Name("pollster_rating_id").Default(0);
            Map(m => m.PollsterRatingName).Name("pollster_rating_name");
            Map(m => m.FteGrade).Name("fte_grade");
            Map(m => m.SampleSize).Name("sample_size").Default(0);
            Map(m => m.Population).Name("population");
            Map(m => m.StartDate).Name("start_date");
            Map(m => m.EndDate).Name("end_date");
            Map(m => m.Tracking).Name("tracking").Default(false);
            Map(m => m.CreatedAt).Name("created_at");
            Map(m => m.Notes).Name("notes");
            Map(m => m.Url).Name("url");
            Map(m => m.Source).Name("source");
            Map(m => m.Yes).Name("yes").Default(0.0m);
            Map(m => m.No).Name("no").Default(0.0m);
        }
    }

    public enum Population
    {
        [Display(Name = "Adults")]
        A,

        [Display(Name = "Registered Voters")]
        RV,

        [Display(Name = "Likely Voters")]
        LV,

        V
    }
}