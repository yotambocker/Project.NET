using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;
using BE;
using DAL;


namespace BL
{
    public class MyBL : IBL
    {

        Idal dal = FactoryDAL.Instance();
        public void DeleteAllTrainee()
        {
            dal.DeleteAllTrainee();
        }
        public void DeleteAllTester()
        {
            dal.DeleteAllTester();
        }
        public void DeleteAllTest()
        {
            dal.DeleteAllTest();
        }


        public void AddTester(Tester tester)
        {
            try
            {
                if (!checkID(tester.ID)) throw new Exception("Exception: tester id error");
                DateTime now = new DateTime();
                now = DateTime.Now;
                now = now.AddYears(-Configuration.MinAgeTester);
                int result = DateTime.Compare(tester.BDate, now);
                if (result > 0) throw new Exception("Exception: The tester is under the age of 40");
                else dal.AddTester(tester);
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }

        public void AddTest(Test test)
        {
            try
            {
                if (test.DayAndHour.DAY != test.DateTest.DayOfWeek) throw new Exception("Exception: DateTest and day dont match");
                if (test.DayAndHour.DAY == DayOfWeek.Saturday || test.DayAndHour.DAY == DayOfWeek.Friday) throw new Exception("Exception: test cannot be in Saturday or Friday");
                if (!checkID(test.IdTrainee)) throw new Exception("Exception: trainee id error");
                Trainee trainee = dal.FindTraineeByID(test.IdTrainee);
                if (trainee == null) throw new ArgumentException("Exception: trainee don't found");
                if (trainee.LessonNum < Configuration.MinTestLesson) throw new ArgumentException("Exception: less than minimum lessons");
                if ((DateTime.Now - trainee.LastTest).Days <= Configuration.MinRangeTest) throw new ArgumentException("Exception: lass than 7 days from the last test of trainee");
                List<Test> toCheck = FactoryBL.Instance().GetTestsList();
                bool check = true;
                foreach (Test Ttest in toCheck)
                {
                    if (Ttest.IdTrainee == test.IdTrainee && Ttest.DateTest == test.DateTest)
                        check = false;
                }
                if (check)
                {
                    var findtester = dal.GetTestersList().Where(tester => tester.Sched[test.DayAndHour.DAY.GetHashCode(), test.DayAndHour.HOUR - 9]
                                                    && tester.CarType == trainee.CarType
                                                    && tester.Tests < tester.MaxTest);
                    ArrayList array = new ArrayList();
                    test.GoodDistance = false;
                    test.IdTester = 0;
                    foreach (Tester theTester in findtester)
                    {
                        array.Add(test);
                        array.Add(theTester);
                        Thread thread = new Thread(CalculateDistance);
                        thread.Start(array);
                        thread.Join();
                        if (test.GoodDistance == true)
                        {
                            test.IdTester = theTester.ID;
                            break;
                        }
                        else
                            throw new Exception("There is no suitable tester");
                    }
                }
                else
                    //Console.WriteLine("this trainee already have a test in the {0}", test.DateTest);
                    throw new Exception("this trainee already have a test in " + test.DateTest.ToShortDateString());
            }
            catch (System.Exception e)
            {
                throw e;
            }
            dal.AddTest(test);
        }

        public void AddTrainee(Trainee trainee)
        {
            try
            {
                if (!checkID(trainee.ID)) throw new Exception("Exception: trainee id error");
                DateTime now = new DateTime();
                now = DateTime.Now;
                now = now.AddYears(-Configuration.MinAgeTrainee);
                int result = DateTime.Compare(trainee.BDate, now);
                if (result > 0) throw new Exception("Exception: The trainee is under the age of 18");
                else dal.AddTrainee(trainee);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void DeleteTester(int id)
        {
            dal.DeleteTester(id);
        }
        public void DeleteTrainee(int id)
        {
            dal.DeleteTrainee(id);
        }

        public void UpdateTester(Tester t)
        {
            dal.UpdateTester(t);
        }
        public void UpdateTrainee(Trainee t)
        {
            dal.UpdateTrainee(t);
        }
        public void UpdateTest(int numtest, Criterion c, Answer a, string tnote)
        {

            Test t = FindTestBuyNum(numtest);

            int count = 0;
            if (c.SavingDistance == Answer.Success) count++;
            if (c.ReverseParking == Answer.Success) count++;
            if (c.CheckMirrors == Answer.Success) count++;
            if (c.Signal == Answer.Success) count++;
            if (c.Speed == Answer.Success) count++;
            if (c.ObedienceSigns == Answer.Success) count++;
            try
            {
                if (count <= 3 && a == Answer.Success) throw new Exception("Success answer error: less than 4 success criterions ");
                if (count > 3 && a == Answer.Fail) throw new Exception("Fail answer error: more than 3 success criterions");
                dal.UpdateTest(numtest, c, a, tnote);
            }
            catch (Exception e) { throw e; }
        }

        public List<Tester> GetTestersList()
        {
            return dal.GetTestersList();
        }
        public List<Test> GetTestsList()
        {
            return dal.GetTestsList();
        }
        public List<Trainee> GetTraineesList()
        {
            return dal.GetTraineesList();
        }


        public IEnumerable<Tester> AvailableTesters(Day DayAndHour)
        {
            var v = from item in dal.GetTestersList()
                    where item.Sched[DayAndHour.DAY.GetHashCode(), DayAndHour.HOUR - 9]
                    select item;
            return v;
        }
        public void CalculateDistance(object obj)
        {
            Test test1 = (Test)((ArrayList)obj)[0];
            string source = test1.AddressTest.Street + " " + test1.AddressTest.NBuilding + " st. " + test1.AddressTest.City;
            Tester tester1 = (Tester)((ArrayList)obj)[1];
            string destination = tester1.MyAddress.Street + " " + tester1.MyAddress.NBuilding + " st. " + tester1.MyAddress.City;
            int count = 0;
            test1.GoodDistance = false;
            bool found = false;
            while (count < 10 && found == false)
            {
                string KEY = "2LKfTyiWG4NeQcGOlH9aPxXZejLUU07m";
                string url = @"https://www.mapquestapi.com/directions/v2/route" +
     @"?key=" + KEY +
     @"&from=" + source +
     @"&to=" + destination +
     @"&outFormat=xml" +
     @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
     @"&enhancedNarrative=false&avoidTimedConditions=false";
                //request from MapQuest service the distance between the 2 addresses
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();
                //the response is given in an XML format
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(responsereader);
                if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                //we have the expected answer
                {
                    found = true;
                    //display the returned distance
                    XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                    double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                    double DistanceInKM = distInMiles * 1.609344;
                    test1.DistanceFromTester = DistanceInKM;
                    if (DistanceInKM < tester1.MaxD)
                    {
                        test1.GoodDistance = true;
                    }
                    else
                        test1.GoodDistance = false;
                }
                else
                {
                    Thread.Sleep(200);
                    count++;
                }
            }
            if (found == false && test1.DistanceFromTester < 50.0)
                test1.GoodDistance = true;
        }


        public IEnumerable<Test> BoolTests(Predicate<Test> Del)
        {
            var v = from item in dal.GetTestsList()
                    where Del(item)
                    select item;
            return v;
        }
        public int TraineeTestNum(Trainee trainee)
        {
            var v = from item in dal.GetTestsList()
                    where item.IdTrainee == trainee.ID
                    select item;
            return v.Count();

        }
        public bool SuccessTrainee(Trainee trainee)
        {
            Predicate<Test> Del = new Predicate<Test>(SuccessTest);
            var v = from item in dal.GetTestsList()
                    where Del(item) && item.IdTrainee == trainee.ID
                    select item;

            foreach (var item in v) if (item != null) return true;
            return false;
        }
        public IEnumerable<Test> TestOnDate(DateTime time)
        {
            var v = from item in dal.GetTestsList()
                    where item.DateTest.Day == time.Day && item.DateTest.Month == time.Month
                    select item;
            return v;
        }

        public void PrintTesterList(IEnumerable<Tester> list)
        {
            foreach (Tester l in list) { Console.WriteLine(l); }
        }
        public void PrintTraineeList(IEnumerable<Trainee> list)
        {
            foreach (Trainee l in list) { Console.WriteLine(l); }
        }
        public string PrintTestList(IEnumerable<Test> list)
        {
            string tmp = "numtest:\n";
            foreach (Test l in list) { tmp += l.NumTest.ToString() + '\n'; }
            return tmp;
        }


        public List<Tester> GroupByCarTypeTester(bool ToSort)
        {
            List<Tester> l = new List<Tester>();
            if (ToSort == true)
            {
                var testerList = from t in dal.GetTestersList()
                                 orderby t.CarType, t.FName
                                 group t by t.CarType into traineeInGroup
                                 select new
                                 {
                                     key = traineeInGroup.Key,
                                     t = traineeInGroup
                                 };
                foreach (var group in testerList)
                {
                    foreach (var t in group.t)
                        l.Add(t);
                }
                return l;
            }
            else
            {
                var testerList = from t in dal.GetTestersList()
                                 group t by t.CarType into traineeInGroup
                                 select new
                                 {
                                     key = traineeInGroup.Key,
                                     t = traineeInGroup
                                 };
                foreach (var group in testerList)
                {
                    foreach (var t in group.t)
                        l.Add(t);
                }
                return l;
            }
        }
        public List<Trainee> GroupBySchoolTrainee(bool ToSort)
        {
            List<Trainee> l = new List<Trainee>();
            if (ToSort == true)
            {
                var traineeList = from t in dal.GetTraineesList()
                                  orderby t.School, t.FName
                                  group t by t.School into traineeInGroup
                                  select new
                                  {

                                      key = traineeInGroup.Key,
                                      t = traineeInGroup
                                  };
                foreach (var group in traineeList)
                {
                    Trainee nullTraine = new Trainee();
                    foreach (var t in group.t)
                        l.Add(t);
                }
                return l;
            }
            else
            {
                var traineeList = from t in dal.GetTraineesList()
                                  group t by t.School into traineeInGroup
                                  select new
                                  {
                                      key = traineeInGroup.Key,
                                      t = traineeInGroup
                                  };
                foreach (var group in traineeList)
                {
                    Trainee nullTraine = new Trainee();
                    foreach (var t in group.t)
                        l.Add(t);
                }
                return l;
            }
        }
        public List<Trainee> GroupByTeacherOfTrainee(bool ToSort)
        {
            List<Trainee> l = new List<Trainee>();
            if (ToSort == true)
            {
                var traineeList = from t in dal.GetTraineesList()
                                  orderby t.Teacher, t.FName
                                  group t by t.Teacher into traineeInGroup
                                  select new
                                  {
                                      key = traineeInGroup.Key,
                                      t = traineeInGroup
                                  };
                foreach (var group in traineeList)
                    foreach (var t in group.t)
                        l.Add(t);
                return l;
            }
            else
            {
                var traineeList = from t in dal.GetTraineesList()
                                  group t by t.Teacher into traineeInGroup
                                  select new
                                  {
                                      key = traineeInGroup.Key,
                                      t = traineeInGroup
                                  };
                foreach (var group in traineeList)
                    foreach (var t in group.t)
                        l.Add(t);
                return l;
            }
        }
        public List<Trainee> GroupByNumTestTrainee(bool ToSort)
        {
            List<Trainee> l = new List<Trainee>();
            if (ToSort == true)
            {
                var traineeList = from t in dal.GetTraineesList()
                                  orderby TraineeTestNum(t), t.FName
                                  group t by TraineeTestNum(t) into numTestInGroup
                                  select new
                                  {
                                      key = numTestInGroup.Key,
                                      t = numTestInGroup
                                  };
                foreach (var group in traineeList)
                    foreach (var num in group.t)
                        l.Add(num);
                return l;
            }
            else
            {
                var traineeList = from t in dal.GetTraineesList()
                                  group t by TraineeTestNum(t) into numTestInGroup
                                  select new
                                  {
                                      key = numTestInGroup.Key,
                                      t = numTestInGroup
                                  };
                foreach (var group in traineeList)
                    foreach (var num in group.t)
                        l.Add(num);
                return l;
            }
        }

        public IEnumerable<Test> ListTestIdTrainee(int mID)
        {
            IEnumerable<Test> tests = from item in dal.GetTestsList()
                                      where item.IdTrainee == mID
                                      select item;
            return tests;
        }
        public IEnumerable<Test> ListTestIdTester(int mID)
        {
            IEnumerable<Test> tests = from item in dal.GetTestsList()
                                      where item.IdTester == mID
                                      select item;
            return tests;
        }


        public Trainee FindTraineeByID(int mID)
        {
            return dal.FindTraineeByID(mID);
        }
        public Tester FindTesterByID(int mID)
        {
            return dal.FindTesterByID(mID);
        }
        public Test FindTestBuyNum(int num)
        {
            return dal.FindTestByNumtest(num);
        }

        /// <summary>
        /// check if the id is correct
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        bool checkID(int num)
        {
            int lastdigit = num % 10;
            num = num / 10;
            int digit = new int();
            int[] dig = new int[9];
            for (int i = 8; i > -1; i--)
            {
                dig[i] = num % 10;
                if (i % 2 == 0)
                {
                    dig[i] = dig[i] * 2;//כאשר הספרה במיקום זוגי
                    if (dig[i] > 9) dig[i] = (dig[i] / 10) + (dig[i] % 10);//כאשר יש 2 ספרות למספר
                }
                digit += dig[i];
                num = num / 10;
            }
            digit = 10 - digit % 10;


            if (digit == lastdigit) return true;
            else return false;
        }

        public bool FailTest(Test t) { return (t.Answer == Answer.Fail); }
        public bool SuccessTest(Test t) { return (t.Answer == Answer.Success); }
        public bool JanuaryTest(Test t) { return (t.DateTest.Month == 1); }
        public bool NovemberTest(Test t) { return (t.DateTest.Month == 11); }
        public bool DecemberTest(Test t) { return (t.DateTest.Month == 12); }
    }
}