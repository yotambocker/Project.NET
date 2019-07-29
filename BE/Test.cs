using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        public Test()
        {
            DayAndHour = new Day();
            Criterion = new Criterion();
        }
        //properties:
        public string NumTest { get; set; } = "";
        public int IdTrainee { get; set; }
        public int IdTester { get; set; }
        public string TesterNote { get; set; }
        public Address AddressTest { get; set; } = new Address();
        public Answer Answer { get; set; } 
        public DateTime DateTest { get; set; } = new DateTime(2018, 1, 1);
        public Day DayAndHour { get; set; }
        public Criterion Criterion { get; set; } = null;
        public bool GoodDistance { get; set; }
        public double DistanceFromTester { get; set; }

        public override string ToString()
        {
            return "Test's number: " + NumTest + '\n' +
                "Date of test: " + DateTest.ToShortDateString() + '\n' +
                "Day and Hour of test: " + DayAndHour.GetString() + '\n' +
                "Address test: " + AddressTest.GetString() + '\n' +
                "Trainee's id: " + IdTrainee + '\n' +
                "Tester's id: " + IdTester + '\n' +
                "Criterion: " + Criterion.GetString() + '\n' +
                "Tester's note: " + TesterNote + '\n' +
                "Answer: " + Answer + '\n';

        }

    }
}