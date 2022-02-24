using System;
using System.Collections.Generic;

namespace HashCode._2022
{
    public class Project
    {
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public int Score { get; set; }
        public int BestBeforeDay { get; set; }
        public int NofRoles { get; set; }
        public List<Skill> RequiredSkills { get; set; } = new List<Skill>();

        public int LastStartDayMaxPoints => this.BestBeforeDay - this.DurationDays;
        public int LastEndDayOnePoint => this.BestBeforeDay + this.Score - 1;
        public int LastStartDayOnePoint => this.LastEndDayOnePoint - this.DurationDays;

        public int GetPointIfStartAt(int day)
        {
            int something = this.BestBeforeDay - (day + this.DurationDays);
            return something >= 0 ? this.Score : Score + something;
        }

        public override string ToString()
        {
            return $"{this.Name}, {this.DurationDays} days, {this.Score} points, {this.NofRoles} roles";
        }
    }
}
