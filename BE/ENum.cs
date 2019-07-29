using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BE
{
    public enum Gender { Male, Female }
    public enum CarType { PrivateCar, TwoWheels, MTruck, HTruck }
    public enum Gearbox { Manual, Automatic }
    public enum Answer {  Fail, Success }

    public class TesterInfo
    {
        public Criterion Criterion { get; set; }
        public Answer Testeranswer { get; set; }
        public string Testernote { get; set; }
        public TesterInfo() { }
        public TesterInfo(Criterion criterion, Answer testeranswer, string testernote)
        {
            Criterion = new Criterion();
            Criterion.SavingDistance = criterion.SavingDistance;
            Criterion.ReverseParking = criterion.ReverseParking;
            Criterion.CheckMirrors = criterion.CheckMirrors;
            Criterion.Signal = criterion.Signal;
            Criterion.Speed = criterion.Speed;
            Criterion.ObedienceSigns = criterion.ObedienceSigns;

           Testeranswer = testeranswer;
            Testernote= testernote;
        }
    }





    public class Address
    {
        public Address() { }
        public string Street { get; set; }
        public string City { get; set; }
        public int NBuilding { get; set; }
        public Address(string c, string s, int nb)
        {
            City = c;
            Street = s;
            NBuilding = nb;
        }
        public string GetString() { return string.Format("{0}, {1}, {2}",City, Street,  NBuilding); }
    }

    public class Criterion
    {
        public Answer SavingDistance { get; set; }
        public Answer ReverseParking { get; set; }
        public Answer CheckMirrors { get; set; }
        public Answer Signal { get; set; }
        public Answer Speed { get; set; }
        public Answer ObedienceSigns { get; set; }

        public Criterion() { }
        public Criterion(Answer savingDistance, Answer reverseParking, Answer checkMirrors, Answer signal, Answer speed, Answer obedienceSigns)
        {
            SavingDistance = savingDistance;
            ReverseParking = reverseParking;
            CheckMirrors = checkMirrors;
            Signal = signal;
            Speed = speed;
            ObedienceSigns = obedienceSigns;
        }

        public string GetString()
        {
            return string.Format(@"
SavingDistance: {0}
ReverseParking: {1}
CheckMirrors  : {2}
Signal        : {3}
Speed         : {4}
ObedienceSigns: {5}", SavingDistance, ReverseParking, CheckMirrors, Signal, Speed, ObedienceSigns);
        }
    }

    public class Day
    {
        public DayOfWeek DAY { get; set; }
        public int HOUR { get; set; }

        public Day() { }
        public Day(DayOfWeek D, int H)
        {
            DAY = D;
            HOUR = H;
        }

        public void Set(DayOfWeek d, int h)
        {
            DAY = d;
            HOUR = h;
        }
        public Day get()
        {
            Day tmp = new Day();
            tmp.DAY = DAY;
            tmp.HOUR = HOUR;
            return tmp;
        }
        public string GetString() { return string.Format("{0} at {1}:00",DAY , HOUR); }
    }
}