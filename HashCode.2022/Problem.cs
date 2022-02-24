using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode._2022
{

    public static class Problem
    {
        public static StringBuilder Solve(string[] lines)
        {
            var builder = new StringBuilder();

            // parse line of int's separated by space
            var header = lines[0].Split(' ').Select(x => int.Parse(x)).ToArray();

            int nofContributors = header[0];
            int nofProjects = header[1];

            var content = new Queue<string>(lines.Skip(1));

            var people = new List<Person>();
            for (int i = 0; i < nofContributors; i++)
            {
                var personInfo = content.Dequeue().Split(' ');
                string name = personInfo[0];
                var skills = new List<Skill>();
                int nofSkills = int.Parse(personInfo[1]);
                for (int j = 0; j < nofSkills; j++)
                {
                    var skillInfo = content.Dequeue().Split(' ');

                    var skill = new Skill
                    {
                        Name = skillInfo[0],
                        Level = int.Parse(skillInfo[1])
                    };
                    skills.Add(skill);
                }
                var person = new Person
                {
                    Name = name,
                    Skills = skills
                };
                people.Add(person);
            }

            var projects = new List<Project>();
            for (int i = 0; i < nofProjects; i++)
            {
                var projectInfo = content.Dequeue().Split(' ');
                var project = new Project
                {
                    Name = projectInfo[0],
                    DurationDays = int.Parse(projectInfo[1]),
                    Score = int.Parse(projectInfo[2]),
                    BestBeforeDay = int.Parse(projectInfo[3]),
                    NofRoles = int.Parse(projectInfo[4])
                };

                for (int j = 0; j < project.NofRoles; j++)
                {
                    var skillInfo = content.Dequeue().Split(' ');
                    var skill = new Skill
                    {
                        Name = skillInfo[0],
                        Level = int.Parse(skillInfo[1])
                    };
                    project.RequiredSkills.Add(skill);
                }

                projects.Add(project);
            }

            var consumer = new Consumer(people, projects);
            consumer.Run();
            consumer.GenerateOutput(builder);

            return builder;
        }
    }
}
