using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode._2022
{
    public class PersonSkill
    {
        public Person Person { get; }
        public int SkillLevel { get; }

        public PersonSkill(Person person, int skillLevel)
        {
            this.Person = person;
            this.SkillLevel = skillLevel;
        }

        public override string ToString()
        {
            return $"{this.Person.Name} lvl:{this.SkillLevel}";
        }
    }

    public class PeopleSkillComparer : IComparer<PersonSkill>
    {
        public int Compare(PersonSkill x, PersonSkill y)
        {
            return x.SkillLevel.CompareTo(y.SkillLevel);
        }
    }

    public class Consumer
    {
        public List<Person> People { get; set; }
        public List<Project> Projects { get; set; }

        public List<PlannedProject> Planned { get; set; }
        //public List<PlannedProject> Completed { get; set; }

        public Consumer(List<Person> people, List<Project> projects)
        {
            this.People = people;
            this.Projects = projects.OrderBy(x => x.LastStartDayMaxPoints).ToList();
            this.Planned = new List<PlannedProject>();
            //this.Completed = new List<PlannedProject>();
        }

        public void Run()
        {
            int lastValueableDay = this.Projects.Max(x => x.LastStartDayOnePoint);

            for (int i = 1; i < lastValueableDay; i++)
            {
                // remove projects that no longer give points
                for (int j = 0; j < this.Projects.Count; j++)
                {
                    var proj = this.Projects[j];
                    int threshold = proj.BestBeforeDay - proj.DurationDays + proj.Score - 1;
                    if (i > threshold)
                    {
                        this.Projects.Remove(proj);
                    }
                }

                // create collection of available people
                var availablePeople = this.People.Where(x => x.DayAvailable <= i);
                var dict = new Dictionary<string, List<PersonSkill>>(); // by skillName
                foreach (var p in availablePeople)
                {
                    foreach (var s in p.Skills)
                    {
                        if (!dict.ContainsKey(s.Name))
                            dict.Add(s.Name, new List<PersonSkill>());
                        dict[s.Name].Add(new PersonSkill(p, s.Level));
                    }
                }
                var comparer = new PeopleSkillComparer();
                foreach (var kvp in dict)
                {
                    kvp.Value.Sort(comparer);
                }

                // schedule projects
                var candidateProjects = this.Projects.Take(10).ToList();
                foreach (var project in candidateProjects)
                {
                    var usedPeople = new HashSet<Person>();
                    foreach (var skill in project.RequiredSkills)
                    {
                        if (!dict.ContainsKey(skill.Name))
                        {
                            // no people available with this skill
                            break;
                        }
                        var target = dict[skill.Name].FirstOrDefault(x => x.SkillLevel >= skill.Level);
                        if (target == null)
                            break;
                        usedPeople.Add(target.Person);
                    }

                    if (usedPeople.Count != project.NofRoles)
                    {
                        // we did not find enough people
                        continue;
                    }
                    else
                    {
                        // schedule the project
                        this.ScheduleProject(project, usedPeople.ToList(), i);
                    }

                    // remove used people from dict
                    foreach (var usedPerson in usedPeople)
                    {
                        foreach (var skill in usedPerson.Skills)
                        {
                            dict[skill.Name].RemoveAll(x => x.Person == usedPerson);
                        }
                    }
                }
            }
        }

        private void ScheduleProject(Project project, List<Person> people, int currentDay)
        {
            var proj = new PlannedProject
            {
                Project = project,
                People = people
            };
            this.Projects.Remove(project);
            this.Planned.Add(proj);

            foreach (var person in people)
            {
                // update return day
                person.DayAvailable = currentDay + project.DurationDays;
                // update skills
                foreach (var skill in person.Skills)
                {
                    var projSkill = project.RequiredSkills.FirstOrDefault(x => x.Name == skill.Name);
                    if (projSkill == null)
                        continue;
                    if (skill.Level == projSkill.Level)
                        skill.Level++;
                }
            }
        }

        public void GenerateOutput(StringBuilder builder)
        {
            builder.AppendLine(this.Planned.Count.ToString());
            foreach (var plannedProject in this.Planned)
            {
                builder.AppendLine(plannedProject.Project.Name);
                builder.AppendLine(string.Join(' ', plannedProject.People.Select(p => p.Name)));
            }
        }
    }
}
