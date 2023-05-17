using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Pages
{
    public class AvailableDay
    {
        public AvailableDay(int id, string startTime, string endTime) 
        { 
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class Skill
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }

        public string[] Tags { get; set; }
        public string ServiceType{ get; set; }
        public string LocationType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SkillTrade { get; set; }
        public string[] SkillExchange { get; set; } 
        public string Credit { get; set; }
        public string Active { get; set; }
        public AvailableDay[] AvailableDays { get; set; }
    }
}
