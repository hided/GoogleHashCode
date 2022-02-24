using System.Collections.Generic;
using System.Linq;

namespace HashCode._2022
{
    public class Person
    {
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }
        public int DayAvailable { get; set; } = -1;

        public bool HasSkill(Skill skill)
        {
            if (skill.Level == 0)
                return true;

            var personSkill = this.Skills.FirstOrDefault(x => x.Name == skill.Name);
            return personSkill != null && personSkill.Level >= skill.Level;
        }

        public override string ToString()
        {
            return $"{this.Name} [{this.Skills.Count}] skills";
        }
    }
}
