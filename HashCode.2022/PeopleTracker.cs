using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode._2022
{
    public class PeopleTracker
    {
        private List<Person> Idle { get; set; }
        private Dictionary<int, List<Person>> Lookup { get; set; }

        public PeopleTracker(List<Person> people)
        {
            this.Idle = people;
            this.Lookup = new Dictionary<int, List<Person>>();
        }

        public void RegisterPersonBusy(Person person, int dayBackAvailable)
        {
            if (!this.Lookup.ContainsKey(dayBackAvailable))
                this.Lookup.Add(dayBackAvailable, new List<Person>());
            this.Lookup[dayBackAvailable].Add(person);
        }

        //public List<Person> GetAvailablePeople(int day)
        //{
        //    var list = new List<Person>();
        //    list.AddRange(this.Idle);
        //    list.AddRange()
        //}
    }
}
