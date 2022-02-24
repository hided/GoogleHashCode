using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode._2022
{
    public class Consumer
    {
        public List<Person> People { get; set; }
        public List<Project> Projects { get; set; }

        public List<PlannedProject> Planned { get; set; }

        public Consumer(List<Person> people, List<Project> projects)
        {
            this.People = people;
            this.Projects = projects;
            this.Planned = new List<PlannedProject>();
        }

        public void Run()
        {
            int lastValueableDay = this.Projects.Max(x => x.LastStartDayOnePoint);

            var sortedProjects = this.Projects.OrderBy(x => x.LastStartDayMaxPoints);
            var projectQueue = new Queue<Project>(sortedProjects);


            for (int i = 0; i < lastValueableDay; i++)
            {
                var project = projectQueue.Peek();
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
