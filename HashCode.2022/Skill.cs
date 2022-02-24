namespace HashCode._2022
{
    public class Skill
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Level}";
        }
    }
}
