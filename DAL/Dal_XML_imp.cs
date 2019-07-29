using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace DAL
{


    public class Dal_XML_imp : Idal
    {

        XElement traineeRoot, testerRoot, testRoot, configRoot;
        string traineePath = @"TraineeXml.xml";
        string testerPath = @"TesterXml.xml";
        string testPath = @"TestXml.xml";
        string configPath = @"Config.xml";

        public Dal_XML_imp()
        {

            if (!File.Exists(traineePath))
                CreateFilesTrainee();
            else
                LoadDataTrainee();

            if (!File.Exists(testerPath))
                CreateFilesTester();
            else
                LoadDataTester();

            if (!File.Exists(testPath))
                CreateFilesTest();
            else
                LoadDataTest();

            if (!File.Exists(configPath))
                CreateFilesConfig();
            else
                LoadDataConfig();


        }

        private void CreateFilesTrainee()
        {
            traineeRoot = new XElement("trainees");
            traineeRoot.Save(traineePath);
        }
        private void CreateFilesTester()
        {
            testerRoot = new XElement("testers");
            testerRoot.Save(testerPath);
        }
        private void CreateFilesTest()
        {
            testRoot = new XElement("tests");
            testRoot.Save(testPath);
        }



        private void CreateFilesConfig()
        {
            int n = 0;
            XElement NumTest = new XElement("NumTest", n);
            configRoot = new XElement("config", NumTest);
            configRoot.Add(new XElement("password", null));
            configRoot.Save(configPath);
        }

        private void LoadDataConfig()
        {
            try
            {
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        public int GetNumTest()
        {
            LoadDataConfig();
            int x = Convert.ToInt32(configRoot.Value);
            configRoot.Value = (x + 1).ToString();
            configRoot.Save(configPath);
            return x;
        }




        private void LoadDataTrainee()
        {
            try
            {
                traineeRoot = XElement.Load(traineePath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataTester()
        {
            try
            {
                testerRoot = XElement.Load(testerPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataTest()
        {
            try
            {
                testRoot = XElement.Load(testPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public void DeleteAllTrainee()
        {
            traineeRoot = null;
            traineeRoot = new XElement("trainees");
            traineeRoot.Save(traineePath);
        }
        public void DeleteAllTester()
        {
            testerRoot = null;
            testerRoot = new XElement("testers");
            testerRoot.Save(testerPath);
        }
        public void DeleteAllTest()
        {
            testRoot = null;
            testRoot = new XElement("tests");
            testRoot.Save(testPath);
        }

        public List<Trainee> GetTraineesList()
        {
            LoadDataTrainee();
            List<Trainee> trainees;
            List<Trainee> tmp = new List<Trainee>();
            try
            {
                trainees = (from t in traineeRoot.Elements()
                            select new Trainee()
                            {
                                ID = int.Parse(t.Element("ID").Value),
                                Phone = t.Element("Phone").Value,
                                LessonNum = int.Parse(t.Element("LessonNum").Value),
                                School = t.Element("School").Value,
                                Teacher = t.Element("Teacher").Value,
                                LName = t.Element("LName").Value,
                                FName = t.Element("FName").Value,
                                Gender = (Gender)Enum.Parse(typeof(Gender), t.Element("Gender").Value),
                                MyAddress = new Address(t.Element("MyAddress").Element("City").Value, t.Element("MyAddress").Element("Street").Value, int.Parse(t.Element("MyAddress").Element("NBuilding").Value)),
                                BDate = DateTime.Parse(t.Element("BDate").Value),
                                CarType = (CarType)Enum.Parse(typeof(CarType), t.Element("CarType").Value),
                                Gear = (Gearbox)Enum.Parse(typeof(Gearbox), t.Element("Gear").Value),
                                LastTest = DateTime.Parse(t.Element("LastTest").Value),
                                HaveGlasses = bool.Parse(t.Element("HaveGlasses").Value)//,
                            }).ToList();
                foreach (var trainee in trainees)
                {
                    tmp.Add(copyTrainee(trainee));
                }
            }
            catch
            {
                tmp = null;
            }
            return tmp;
        }
        public List<Tester> GetTestersList()
        {
            LoadDataTester();
            List<Tester> testers;
            List<Tester> tmp = new List<Tester>();
            try
            {
                testers = (from t in testerRoot.Elements()
                           select new Tester()
                           {
                               ID = int.Parse(t.Element("ID").Value),
                               Phone = t.Element("Phone").Value,
                               Seniority = int.Parse(t.Element("Seniority").Value),
                               MaxTest = int.Parse(t.Element("MaxTest").Value),
                               //Distancefromaddres = int.Parse(t.Element("Distancefromaddres").Value),
                               Tests = int.Parse(t.Element("Tests").Value),
                               MaxD = double.Parse(t.Element("MaxD").Value),
                               //Password = t.Element("Password").Value,
                               LName = t.Element("LName").Value,
                               FName = t.Element("FName").Value,
                               Gender = (Gender)Enum.Parse(typeof(Gender), t.Element("Gender").Value),
                               MyAddress = new Address(t.Element("MyAddress").Element("City").Value, t.Element("MyAddress").Element("Street").Value, int.Parse(t.Element("MyAddress").Element("NBuilding").Value)),
                               BDate = DateTime.Parse(t.Element("BDate").Value),
                               CarType = (CarType)Enum.Parse(typeof(CarType), t.Element("CarType").Value)
                               //Sched[,] = bool.Parse(t.Element("Sched").Value),
                           }).ToList();
                foreach (var tester in testers)
                {
                    tmp.Add(copyTester(tester));
                }
            }
            catch
            {
                tmp = null;
            }
            return tmp;
        }
        public List<Test> GetTestsList()
        {
            LoadDataTest();
            List<Test> tests;
            List<Test> tmp = new List<Test>();
            try
            {
                tests = (from t in testRoot.Elements()
                         select new Test()
                         {
                             NumTest = t.Element("NumTest").Value,
                             TesterNote = t.Element("TesterNote").Value,
                             IdTrainee = int.Parse(t.Element("IdTrainee").Value),
                             IdTester = int.Parse(t.Element("IdTester").Value),
                             Answer = (Answer)Enum.Parse(typeof(Answer), t.Element("Answer").Value),

                             DayAndHour = new Day(
                                (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.Element("DayAndHour").Element("DAY").Value),
                                 int.Parse(t.Element("DayAndHour").Element("HOUR").Value)
                                 ),

                             AddressTest = new Address(
                                 t.Element("AddressTest").Element("City").Value,
                                 t.Element("AddressTest").Element("Street").Value,
                                 int.Parse(t.Element("AddressTest").Element("NBuilding").Value)
                                 ),

                             Criterion = new Criterion(
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("SavingDistance").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("ReverseParking").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("CheckMirrors").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("Signal").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("Speed").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("ObedienceSigns").Value)
                                 ),


                             DateTest = DateTime.Parse(t.Element("DateTest").Value)
                         }).ToList();
                foreach (var test in tests)
                {
                    tmp.Add(copyTest(test));
                }
            }
            catch
            {
                tmp = null;
            }
            return tmp;
        }

        public Trainee FindTraineeByID(int ID)
        {
            LoadDataTrainee();
            Trainee trainee;
            try
            {
                trainee = (from t in traineeRoot.Elements()
                           where int.Parse(t.Element("ID").Value) == ID
                           select new Trainee()
                           {
                               ID = int.Parse(t.Element("ID").Value),
                               Phone = t.Element("Phone").Value,
                               LessonNum = int.Parse(t.Element("LessonNum").Value),
                               School = t.Element("School").Value,
                               Teacher = t.Element("Teacher").Value,
                               LName = t.Element("LName").Value,
                               FName = t.Element("FName").Value,
                               Gender = (Gender)Enum.Parse(typeof(Gender), t.Element("Gender").Value),
                               MyAddress = new Address(t.Element("MyAddress").Element("City").Value, t.Element("MyAddress").Element("Street").Value, int.Parse(t.Element("MyAddress").Element("NBuilding").Value)),
                               BDate = DateTime.Parse(t.Element("BDate").Value),
                               CarType = (CarType)Enum.Parse(typeof(CarType), t.Element("CarType").Value),
                               Gear = (Gearbox)Enum.Parse(typeof(Gearbox), t.Element("Gear").Value),
                               LastTest = DateTime.Parse(t.Element("LastTest").Value),
                               HaveGlasses = bool.Parse(t.Element("HaveGlasses").Value)
                           }).FirstOrDefault();
            }
            catch
            {
                trainee = null;
            }
            return trainee;
        }
        public Tester FindTesterByID(int ID)
        {
            LoadDataTester();
            Tester tester;
            try
            {
                tester = (from t in testerRoot.Elements()
                          where int.Parse(t.Element("ID").Value) == ID
                          select new Tester()
                          {
                              ID = int.Parse(t.Element("ID").Value),
                              Phone = t.Element("Phone").Value,
                              Seniority = int.Parse(t.Element("Seniority").Value),
                              MaxTest = int.Parse(t.Element("MaxTest").Value),
                              //Distancefromaddres = int.Parse(t.Element("Distancefromaddres").Value),
                              Tests = int.Parse(t.Element("Tests").Value),
                              MaxD = double.Parse(t.Element("MaxD").Value),
                              //Password = t.Element("Password").Value,
                              LName = t.Element("LName").Value,
                              FName = t.Element("FName").Value,
                              Gender = (Gender)Enum.Parse(typeof(Gender), t.Element("Gender").Value),
                              MyAddress = new Address(t.Element("MyAddress").Element("City").Value, t.Element("MyAddress").Element("Street").Value, int.Parse(t.Element("MyAddress").Element("NBuilding").Value)),
                              BDate = DateTime.Parse(t.Element("BDate").Value),
                              CarType = (CarType)Enum.Parse(typeof(CarType), t.Element("CarType").Value)
                              //         Sched[,] = bool.Parse(t.Element("Sched").Value),
                          }).FirstOrDefault();
            }
            catch
            {
                tester = null;
            }
            return tester;
        }
        public Test FindTestByNumtest(int num)
        {
            LoadDataTest();
            Test test;
            try
            {
                test = (from t in testRoot.Elements()
                        where t.Element("NumTest").Value == String.Format("{0:D8}", num)
                        select new Test()
                        {
                            IdTrainee = int.Parse(t.Element("IdTrainee").Value),
                            IdTester = int.Parse(t.Element("IdTester").Value),
                            NumTest = t.Element("NumTest").Value,
                            TesterNote = t.Element("TesterNote").Value,
                            DateTest = DateTime.Parse(t.Element("DateTest").Value),
                            Answer = (Answer)Enum.Parse(typeof(Answer), t.Element("Answer").Value),
                            AddressTest = new Address(t.Element("AddressTest").Element("City").Value, t.Element("AddressTest").Element("Street").Value, int.Parse(t.Element("AddressTest").Element("NBuilding").Value)),

                            DayAndHour = new Day(
                                (DayOfWeek)Enum.Parse(typeof(DayOfWeek), t.Element("DayAndHour").Element("DAY").Value),
                                 int.Parse(t.Element("DayAndHour").Element("HOUR").Value)
                                 ),

                            Criterion = new Criterion(
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("SavingDistance").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("ReverseParking").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("CheckMirrors").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("Signal").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("Speed").Value),
                                 (Answer)Enum.Parse(typeof(Answer), t.Element("Criterion").Element("ObedienceSigns").Value)
                                 )

                        }).FirstOrDefault();
            }
            catch
            {
                test = null;
            }
            return test;
        }

        public void AddTrainee(Trainee tmp)
        {
            Trainee trainee = copyTrainee(tmp);
            if (FindTraineeByID(trainee.ID) != null) throw new Exception("exception: A trainee with the same id already exists in the system");

            XElement ID = new XElement("ID", trainee.ID);
            XElement Phone = new XElement("Phone", trainee.Phone);
            XElement LessonNum = new XElement("LessonNum", trainee.LessonNum);
            XElement School = new XElement("School", trainee.School);
            XElement Teacher = new XElement("Teacher", trainee.Teacher);
            XElement LName = new XElement("LName", trainee.LName);
            XElement FName = new XElement("FName", trainee.FName);
            XElement Gender = new XElement("Gender", trainee.Gender);
            XElement City = new XElement("City", trainee.MyAddress.City);
            XElement Street = new XElement("Street", trainee.MyAddress.Street);
            XElement NBuilding = new XElement("NBuilding", trainee.MyAddress.NBuilding);
            XElement MyAddress = new XElement("MyAddress", City, Street, NBuilding);
            XElement BDate = new XElement("BDate", trainee.BDate);
            XElement CarType = new XElement("CarType", trainee.CarType);
            XElement Gear = new XElement("Gear", trainee.Gear);
            XElement LastTest = new XElement("LastTest", trainee.LastTest);
            XElement HaveGlasses = new XElement("HaveGlasses", trainee.HaveGlasses);

            traineeRoot.Add(new XElement("trainee", ID, Phone, LessonNum, School, Teacher, LName, FName, Gender, MyAddress, BDate, CarType, Gear, LastTest, HaveGlasses));
            traineeRoot.Save(traineePath);
        }
        public void AddTester(Tester tmp)
        {
            Tester tester = copyTester(tmp);
            if (FindTesterByID(tester.ID) != null) throw new Exception("exception: A tester with the same id already exists in the system");

            XElement ID = new XElement("ID", tester.ID);
            XElement Phone = new XElement("Phone", tester.Phone);
            XElement Seniority = new XElement("Seniority", tester.Seniority);
            XElement MaxTest = new XElement("MaxTest", tester.MaxTest);
            // XElement Distancefromaddres = new XElement("Distancefromaddres", tester.Distancefromaddres);
            XElement Tests = new XElement("Tests", tester.Tests);
            XElement MaxD = new XElement("MaxD", tester.MaxD);
            // XElement Password = new XElement("Password", tester.Password);
            XElement LName = new XElement("LName", tester.LName);
            XElement FName = new XElement("FName", tester.FName);
            XElement BDate = new XElement("BDate", tester.BDate);
            XElement Gender = new XElement("Gender", tester.Gender);
            XElement CarType = new XElement("CarType", tester.CarType);
            XElement City = new XElement("City", tester.MyAddress.City);
            XElement Street = new XElement("Street", tester.MyAddress.Street);
            XElement NBuilding = new XElement("NBuilding", tester.MyAddress.NBuilding);
            XElement MyAddress = new XElement("MyAddress", City, Street, NBuilding);

            XElement Sched = new XElement("Sched", new XElement("suday", new XElement("h9", tester.Sched[0, 0]), new XElement("h10", tester.Sched[0, 1]), new XElement("h11", tester.Sched[0, 2]), new XElement("h12", tester.Sched[0, 3]),
                     new XElement("h13", tester.Sched[0, 4]), new XElement("h14", tester.Sched[0, 5])), new XElement("monday", new XElement("h9", tester.Sched[1, 0]), new XElement("h10", tester.Sched[1, 1]), new XElement("h11", tester.Sched[1, 2]), new XElement("h12", tester.Sched[1, 3]),
                     new XElement("h13", tester.Sched[1, 4]), new XElement("h14", tester.Sched[1, 5])), new XElement("Tuesday", new XElement("h9", tester.Sched[2, 0]), new XElement("h10", tester.Sched[2, 1]), new XElement("h11", tester.Sched[2, 2]), new XElement("h12", tester.Sched[2, 3]),
                     new XElement("h13", tester.Sched[2, 4]), new XElement("h14", tester.Sched[2, 5])), new XElement("Wednesday", new XElement("h9", tester.Sched[3, 0]), new XElement("h10", tester.Sched[3, 1]), new XElement("h11", tester.Sched[3, 2]), new XElement("h12", tester.Sched[3, 3]),
                     new XElement("h13", tester.Sched[3, 4]), new XElement("h14", tester.Sched[3, 5])), new XElement("Thursday", new XElement("h9", tester.Sched[4, 0]), new XElement("h10", tester.Sched[4, 1]), new XElement("h11", tester.Sched[4, 2]), new XElement("h12", tester.Sched[4, 3]),
                     new XElement("h13", tester.Sched[4, 4]), new XElement("h14", tester.Sched[4, 5])));


            testerRoot.Add(new XElement("tester", ID, Phone, Seniority, MaxTest, Tests, LName, FName, Gender, MyAddress, BDate, CarType, MaxD));
            testerRoot.Save(testerPath);
        }
        public void AddTest(Test t)
        {

            t.NumTest = String.Format("{0:D8}", GetNumTest());

            Test test = copyTest(t);
            XElement NumTest = new XElement("NumTest", test.NumTest);
            XElement IdTrainee = new XElement("IdTrainee", test.IdTrainee);
            XElement IdTester = new XElement("IdTester", test.IdTester);
            XElement TesterNote = new XElement("TesterNote", test.TesterNote);
            XElement Answer = new XElement("Answer", test.Answer);


            XElement HOUR = new XElement("HOUR", test.DayAndHour.HOUR);
            XElement DAY = new XElement("DAY", test.DayAndHour.DAY);
            XElement DayAndHour = new XElement("DayAndHour", DAY, HOUR);

            XElement SavingDistance = new XElement("SavingDistance", test.Criterion.SavingDistance);
            XElement ReverseParking = new XElement("ReverseParking", test.Criterion.ReverseParking);
            XElement CheckMirrors = new XElement("CheckMirrors", test.Criterion.CheckMirrors);
            XElement Signal = new XElement("Signal", test.Criterion.Signal);
            XElement Speed = new XElement("Speed", test.Criterion.Speed);
            XElement ObedienceSigns = new XElement("ObedienceSigns", test.Criterion.ObedienceSigns);
            XElement Criterion = new XElement("Criterion", SavingDistance, ReverseParking, CheckMirrors, Signal, Speed, ObedienceSigns);

            XElement City = new XElement("City", test.AddressTest.City);
            XElement Street = new XElement("Street", test.AddressTest.Street);
            XElement NBuilding = new XElement("NBuilding", test.AddressTest.NBuilding);
            XElement AddressTest = new XElement("AddressTest", City, Street, NBuilding);


            XElement DateTest = new XElement("DateTest", test.DateTest);

            testRoot.Add(new XElement("test", IdTrainee, IdTester, TesterNote, NumTest, Answer, DayAndHour, Criterion, AddressTest, DateTest));
            testRoot.Save(testPath);
            //configRoot.Element("config").Element("NumTest").Value += 1;

            Tester TesterTmp = FindTesterByID(t.IdTester);
            TesterTmp.Sched[t.DayAndHour.DAY.GetHashCode(), t.DayAndHour.HOUR - 9] = false;
            TesterTmp.Tests++;
        }

        public void DeleteTrainee(int ID)
        {
            XElement traineeElement = traineeRoot.Elements().Where(tr => int.Parse(tr.Element("ID").Value) == ID).FirstOrDefault();
            if (traineeElement != null) traineeElement.Remove();
            traineeRoot.Save(traineePath);
        }
        public void DeleteTester(int ID)
        {
            XElement testerElement = testerRoot.Elements().Where(tr => int.Parse(tr.Element("ID").Value) == ID).FirstOrDefault();
            if (testerElement != null) testerElement.Remove();
            testerRoot.Save(testerPath);
        }

        public void UpdateTrainee(Trainee tmp)
        {
            Trainee trainee = copyTrainee(tmp);
            XElement traineeElement = (from tr in traineeRoot.Elements()
                                       where int.Parse(tr.Element("ID").Value) == trainee.ID
                                       select tr).FirstOrDefault();
            if (trainee.Phone != null) traineeElement.Element("Phone").Value = trainee.Phone;
            traineeElement.Element("LessonNum").Value = trainee.LessonNum.ToString();
            if (trainee.School != null) traineeElement.Element("School").Value = trainee.School;
            if (trainee.Teacher != null) traineeElement.Element("Teacher").Value = trainee.Teacher;
            if (trainee.LName != null) traineeElement.Element("LName").Value = trainee.LName;
            if (trainee.FName != null) traineeElement.Element("FName").Value = trainee.FName;
            traineeElement.Element("Gender").Value = trainee.Gender.ToString();

            if (trainee.MyAddress.City != null) traineeElement.Element("MyAddress").Element("City").Value = trainee.MyAddress.City;
            if (trainee.MyAddress.Street != null) traineeElement.Element("MyAddress").Element("Street").Value = trainee.MyAddress.Street;
            traineeElement.Element("MyAddress").Element("NBuilding").Value = trainee.MyAddress.NBuilding.ToString();

            traineeElement.Element("BDate").Value = trainee.BDate.ToString();
            traineeElement.Element("CarType").Value = trainee.CarType.ToString();
            traineeElement.Element("Gear").Value = trainee.Gear.ToString();
            traineeElement.Element("LastTest").Value = trainee.LastTest.ToString();
            traineeElement.Element("HaveGlasses").Value = trainee.HaveGlasses.ToString();
            traineeRoot.Save(traineePath);
        }
        public void UpdateTester(Tester tmp)
        {
            Tester tester = copyTester(tmp);
            XElement testerElement = (from tr in testerRoot.Elements()
                                      where int.Parse(tr.Element("ID").Value) == tester.ID
                                      select tr).FirstOrDefault();
            if (tester.Phone != null) testerElement.Element("Phone").Value = tester.Phone;
            testerElement.Element("Seniority").Value = tester.Seniority.ToString();
            if (tester.LName != null) testerElement.Element("LName").Value = tester.LName;
            if (tester.FName != null) testerElement.Element("FName").Value = tester.FName;
            testerElement.Element("Gender").Value = tester.Gender.ToString();

            if (tester.MyAddress.City != null) testerElement.Element("MyAddress").Element("City").Value = tester.MyAddress.City;
            if (tester.MyAddress.Street != null) testerElement.Element("MyAddress").Element("Street").Value = tester.MyAddress.Street;
            testerElement.Element("MyAddress").Element("NBuilding").Value = tester.MyAddress.NBuilding.ToString();
            testerElement.Element("BDate").Value = tester.BDate.ToString();
            testerElement.Element("CarType").Value = tester.CarType.ToString();
            testerElement.Element("MaxTest").Value = tester.MaxTest.ToString();
            testerElement.Element("MaxD").Value = tester.MaxD.ToString();
            testerElement.Element("Tests").Value = tester.Tests.ToString();
            // testerElement.Element("Sched[,]").Value = tester.Sched[,].ToString();
            testerRoot.Save(testerPath);
        }
        public void UpdateTest(int numtest, Criterion c, Answer a, string tnote)
        {
            Test test = copyTest(FindTestByNumtest(numtest));
            test.Criterion = c;
            test.Answer = a;
            test.TesterNote = tnote;

            XElement testElement = (from tr in testRoot.Elements()
                                    where tr.Element("NumTest").Value == test.NumTest
                                    select tr).FirstOrDefault();
            if (test.NumTest != null) testElement.Element("NumTest").Value = test.NumTest;
            if (test.TesterNote != null) testElement.Element("TesterNote").Value = test.TesterNote;
            testElement.Element("IdTrainee").Value = test.IdTrainee.ToString();
            testElement.Element("IdTester").Value = test.IdTester.ToString();
            testElement.Element("Answer").Value = test.Answer.ToString();
            testElement.Element("DayAndHour").Element("HOUR").Value = test.DayAndHour.HOUR.ToString();
            testElement.Element("DayAndHour").Element("DAY").Value = test.DayAndHour.DAY.ToString();

            testElement.Element("Criterion").Element("SavingDistance").Value = test.Criterion.SavingDistance.ToString();
            testElement.Element("Criterion").Element("ReverseParking").Value = test.Criterion.ReverseParking.ToString();
            testElement.Element("Criterion").Element("CheckMirrors").Value = test.Criterion.CheckMirrors.ToString();
            testElement.Element("Criterion").Element("Signal").Value = test.Criterion.Signal.ToString();
            testElement.Element("Criterion").Element("Speed").Value = test.Criterion.Speed.ToString();
            testElement.Element("Criterion").Element("ObedienceSigns").Value = test.Criterion.ObedienceSigns.ToString();

            if (test.AddressTest.City != null) testElement.Element("AddressTest").Element("City").Value = test.AddressTest.City;
            if (test.AddressTest.Street != null) testElement.Element("AddressTest").Element("Street").Value = test.AddressTest.Street;
            testElement.Element("AddressTest").Element("NBuilding").Value = test.AddressTest.NBuilding.ToString();

            testElement.Element("DateTest").Value = test.DateTest.ToString();

            testRoot.Save(testPath);
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
            tmp.MyAddress = new Address();
            if (trainee.MyAddress != null) tmp.MyAddress = new Address(trainee.MyAddress.City, trainee.MyAddress.Street, trainee.MyAddress.NBuilding);
            tmp.Phone = trainee.Phone;
            tmp.School = trainee.School;
            tmp.Teacher = trainee.Teacher;

            return tmp;
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
    }
}