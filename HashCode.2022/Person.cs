using System.Collections.Generic;

namespace HashCode._2022
{
    public class Person
    {
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }

        public override string ToString()
        {
            return $"{this.Name} [{this.Skills.Count}] skills";
        }
    }
}
