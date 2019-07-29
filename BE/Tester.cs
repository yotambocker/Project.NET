using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester
    {
        //properties:
        public int ID { get; set; }
        public string Phone { get; set; }
        public int Seniority { get; set; }
        public int MaxTest { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public double MaxD { get; set; }
        public DateTime BDate { get; set; } = new DateTime(1900, 1, 1);
        public Gender Gender { get; set; }
        public Address MyAddress { get; set; } = new Address();
        public CarType CarType { get; set; }
        public bool[,] Sched { get; set; }
        public int Tests { get; set; }

        public Tester()
        {
            BDate = new DateTime(1 / 1 / 1960);
            Tests = 0;
            MyAddress = new Address();
            Sched = new bool[5, 6]
            {
                { true,true,true,true,true,true},
                { true,true,true,true,true,true},
                { true,true,true,true,true,true},
                { true,true,true,true,true,true},
                { true,true,true,true,true,true}
            };
        }





        public string GetTimeTableString()
        {
            string tmp = "";
            for (DayOfWeek day = 0; day.GetHashCode() < 5; day++)
            {
                for (int hour = 0; hour < 6; hour++)
                {
                    if (Sched[day.GetHashCode(), hour]==false)
                    {
                        tmp += "taken in " + day + " at " + (hour+9) + ":00\n";    
                    }
                }
            }
            return tmp;
        }

        public override string ToString()
        {
            return /*"name: " + FName + " " + LName + '\n' +
                "Id: " + ID + "" + '\n' +
                "phone number: " + Phone + '\n' +
                "address: " + MyAddress.GetString() + '\n' +
                "Birthday: " + BDate.ToShortDateString() + '\n' +
                "gender: " + Gender + '\n' +
                "experience years': " + Seniority + '\n' +
                "Maximum tests in week: " + MaxTest + '\n' +
                "Maximum distance from address: " + MaxD + '\n' +
                "Type of car: " + CarType + '\n' +
                "available time:" + '\n' +*/
                GetTimeTableString();

        }
    }
}