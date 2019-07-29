using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;

namespace DAL
{
    public class Dal_imp : Idal
    {
        public void AddTester(Tester t)
        {
            bool found = false;
            foreach (Tester a in DataSource.allTesters) if (a.ID == t.ID) found = true;
            if (!found)
                DataSource.allTesters.Add(copyTester(t));
            else
                throw new Exception("exception: A tester with the same id already exists in the system");
        }
        public void AddTrainee(Trainee t)
        {
            bool found = false;
            foreach (Trainee a in DataSource.allTrainee) if (a.ID == t.ID) found = true;
            if (!found) DataSource.allTrainee.Add(copyTrainee(t));
            else
                throw new Exception("exception: A trainee with the same id already exists in the system");
        }
        public void AddTest(Test t)
        {
          //  t.NumTest = String.Format("{0:D8}", Configuration.Numtest);
            DataSource.allTests.Add(copyTest(t));
            Tester TesterTmp = FindTesterByID(t.IdTester);
            TesterTmp.Sched[t.DayAndHour.DAY.GetHashCode(), t.DayAndHour.HOUR - 9] = false;
            TesterTmp.Tests++;
        }

        public void UpdateTrainee(Trainee t)
        {
            var s = DataSource.allTrainee.Where(x => x.ID == t.ID).FirstOrDefault();
            if (s != null)
            {
                DataSource.allTrainee.Remove(s);
                DataSource.allTrainee.Add(copyTrainee(t));
            }
        }
        public void UpdateTester(Tester t)
        {
            var s = DataSource.allTesters.Where(x => x.ID == t.ID).FirstOrDefault();
            if (s != null)
            {
                DataSource.allTesters.Remove(s);
                DataSource.allTesters.Add(copyTester(t));
            }
        }
        public void UpdateTest(int numtest, Criterion c, Answer a, string tnote)
        {
            Test test = FindTestByNumtest(numtest);
            test.Criterion = c;
            test.Answer = a;
            test.TesterNote = tnote;
        }

        public void DeleteTester(int id)
        {
            var s = DataSource.allTesters.Where(x => x.ID == id).FirstOrDefault();
            if (s != null) DataSource.allTesters.Remove(s);
        }
        public void DeleteTrainee(int id)
        {
            var s = DataSource.allTrainee.Where(x => x.ID == id).FirstOrDefault();
            if (s != null) DataSource.allTrainee.Remove(s);
        }

        /// <summary>
        /// the function copy an object of  the class 'Tester' by value
        /// (and no by reference like the operator '=' do)
        /// </summary>
        /// <param name="t">the object od the class that we want to copy</param>
        /// <returns>the new object that we just created</returns>
        Tester copyTester(Tester t)
        {
            Tester temp = new Tester();
            temp.BDate = t.BDate;
            temp.CarType = t.CarType;
            temp.FName = t.FName;
            temp.Gender = t.Gender;
            temp.ID = t.ID;
            temp.LName = t.LName;
            temp.MaxD = t.MaxD;
            temp.MaxTest = t.MaxTest;
            if (t.MyAddress != null) temp.MyAddress = new Address(t.MyAddress.City, t.MyAddress.Street, t.MyAddress.NBuilding);
            temp.Phone = t.Phone;
            temp.Seniority = t.Seniority;
            temp.Tests = t.Tests;
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (t.Sched[i, j] == false)
                        temp.Sched[i, j] = false;
                }
            }
            return temp;
        }
        /// <summary>
        /// the function copy an object of the class Tests by value
        /// (and no by reference like the operator '=' do)
        /// </summary>
        /// <param name="test"> the test we want to copy</param>
        /// <returns>the new object that we just created</returns>
        Test copyTest(Test test)
        {
            Test tmp = new Test();
            if (test.AddressTest != null) tmp.AddressTest = new Address(test.AddressTest.City, test.AddressTest.Street, test.AddressTest.NBuilding);
            tmp.Answer = test.Answer;
            tmp.Criterion = new Criterion(test.Criterion.SavingDistance, test.Criterion.ReverseParking, test.Criterion.CheckMirrors, test.Criterion.Signal, test.Criterion.Speed, test.Criterion.ObedienceSigns);
            tmp.DateTest = test.DateTest;
            tmp.DayAndHour.Set(test.DayAndHour.DAY, test.DayAndHour.HOUR);
            tmp.IdTester = test.IdTester;
            tmp.IdTrainee = test.IdTrainee;
            tmp.NumTest = test.NumTest;
            tmp.TesterNote = test.TesterNote;
            return tmp;
        }
        /// <summary>
        /// the function copy an object of  the class 'Trainee' by value
        /// (and no by reference like the operator '=' do)
        /// </summary>
        /// <param name="trainee">the object od the class that we want to copy</param>
        /// <returns>the new object that we just created</returns>
        Trainee copyTrainee(Trainee trainee)
        {
            Trainee tmp = new Trainee();
            tmp.BDate = trainee.BDate;
            tmp.CarType = trainee.CarType;
            tmp.FName = trainee.FName;
            tmp.Gear = trainee.Gear;
            tmp.Gender = trainee.Gender;
            tmp.HaveGlasses = trainee.HaveGlasses;
            tmp.ID = trainee.ID;
            tmp.LastTest = trainee.LastTest;
            tmp.LessonNum = trainee.LessonNum;
            tmp.LName = trainee.LName;
            //tmp.MyAddress = new Address();
            if (trainee.MyAddress != null) tmp.MyAddress = new Address(trainee.MyAddress.City, trainee.MyAddress.Street, trainee.MyAddress.NBuilding);
            tmp.Phone = trainee.Phone;
            tmp.School = trainee.School;
            tmp.Teacher = trainee.Teacher;
            
            return tmp;
        }
        public List<Tester> GetTestersList()
        {
            List<Tester> tmp = new List<Tester>();
            foreach (Tester t in DataSource.allTesters)
            {
                tmp.Add(copyTester(t));
            }
            return tmp;
        }
        public List<Test> GetTestsList()
        {
            List<Test> tmp = new List<Test>();
            foreach (Test test in DataSource.allTests)
            {
                tmp.Add(copyTest(test));
            }
            return tmp;
        }
        public List<Trainee> GetTraineesList()
        {
            List<Trainee> tmp = new List<Trainee>();
            foreach (Trainee trainee in DataSource.allTrainee)
            {
                tmp.Add(copyTrainee(trainee));
            }
            return tmp;
        }

        public Trainee FindTraineeByID(int mID)
        {
            Trainee trainee = DataSource.allTrainee.Where(train => train.ID == mID).FirstOrDefault();
            return trainee;
        }
        public Tester FindTesterByID(int mID)
        {
            Tester tester = DataSource.allTesters.Where(theTester => theTester.ID == mID).FirstOrDefault();
            return tester;
        }
        public Test FindTestByNumtest(int num)
        {
            foreach (Test t in DataSource.allTests)
                if (t.NumTest == String.Format("{0:D8}", num)) return t;
            return null;
        }

        public void DeleteAllTrainee() { }

        public void DeleteAllTester() { }

        public void DeleteAllTest() { }
    }
}
