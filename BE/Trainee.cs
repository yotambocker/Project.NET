using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        //properties:
        public int ID { get; set; }
        public string Phone { get; set; }
        public int LessonNum { get; set; }
        public string School { get; set; }
        public string Teacher { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public Gender Gender { get; set; }
        public Address MyAddress { get; set; } = new Address();
        public DateTime BDate { get; set; } = new DateTime(1990, 1, 1);
        public CarType CarType { get; set; }
        public Gearbox Gear { get; set; }
        public DateTime LastTest { get; set; } = new DateTime(2000, 1, 1);
        public bool HaveGlasses { set; get; }

        public override string ToString()
        {
            return "name: " + FName + " " + LName + '\n' +
                "Id: " + ID + "" + '\n' +
                "phone number: " + Phone + '\n' +
                "address: " + MyAddress.GetString() + '\n' +
                "Birthday: " + BDate.ToShortDateString() + '\n' +
                "gender: " + Gender + '\n' +
                "Number of lessons: " + LessonNum + '\n' +
                "School: " + School + '\n' +
                "Teacher: " + Teacher + '\n' +
                "Type of car: " + CarType + '\n' +
                "Have Glasses:" + HaveGlasses + '\n' +
                "Gear: " + Gear + '\n';
        }
    }
}
