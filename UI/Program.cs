using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;


namespace UI
{
     class Program
    {
        public static void StartPrint()
        {
            Console.WriteLine(@"
chose option:
        1. add tester
        2. add trainee
        3. add test
        4. print tester
        5. delete tester
        6. print trainee
        7. delete trainee
        8. update tester
        9. update trainee
        10. DistanceTesters
        11. available tester
        12. get tester list
        13. get trainee list
        14. get test list
        15. print test
        16. GroupByCarTypeTester
        17. GroupBySchoolTrainee
        18. GroupByTesterOfTrainee
        19. GroupByNumTestTrainee
        20. get all success tests
        21. get all tests in November
        22. update test
        23. check if trainee succuss
        24. trainee tests number
        25.  tests by datetime (day and month)

    
        0. exit
");
        }

        public static void Inputlist(IBL bl)
        {
            Tester tester1 = new Tester();
            tester1.ID = 311525836;
            tester1.Phone = "050";
            tester1.Seniority = 15;
            tester1.MaxTest = 14;
            tester1.FName = "a";
            tester1.LName = "aa";
            tester1.CarType = CarType.PrivateCar;
            tester1.Gender = Gender.Male;
            tester1.MyAddress= new Address("city", "street", 1);
            tester1.MaxD = 1;
            tester1.BDate = new DateTime(1934, 08, 17);
            bl.AddTester(tester1);

            Trainee trainee1 = new Trainee();
            trainee1.ID = 123456782;
            trainee1.Phone = "050";
            trainee1.LessonNum = 40;
            trainee1.School = "c";
            trainee1.FName = "a";
            trainee1.LName = "aa";
            trainee1.CarType = CarType.PrivateCar;
            trainee1.Gender = Gender.Male;
            trainee1.MyAddress = new Address("city", "street", 1);
            trainee1.Gear = Gearbox.Automatic;
            trainee1.BDate = new DateTime(1995, 06, 17);
            //  trainee1.LastTest = new DateTime(1934, 08, 17);
            bl.AddTrainee(trainee1);

            Test test1 = new Test();
            test1.IdTrainee = 123456782;
            test1.IdTester = 311525836;
            test1.DateTest = new DateTime(2018, 12, 26);
            test1.AddressTest = new Address("jerusalem", "golomb", 38);
            test1.DayAndHour.Set(DayOfWeek.Sunday, 11);
          //  bl.AddTest(test1);

            Tester tester2 = new Tester();
            tester2.ID = 314699554;
            tester2.Phone = "050";
            tester2.Seniority = 25;
            tester2.MaxTest = 24;
            tester2.FName = "b";
            tester2.LName = "bb";
            tester2.CarType = CarType.PrivateCar;
            tester2.Gender = Gender.Male;
            tester2.MyAddress = new Address("city", "street", 2);
            tester2.MaxD = 2;
            tester2.BDate = new DateTime(1934, 08, 27);
            bl.AddTester(tester2);

            Trainee trainee2 = new Trainee();
            trainee2.ID = 322675562;
            trainee2.Phone = "050";
            trainee2.LessonNum = 40;
            trainee2.School = "b";
            trainee2.FName = "b";
            trainee2.LName = "bb";
            trainee2.CarType = CarType.PrivateCar;
            trainee2.Gender = Gender.Male;
            trainee2.MyAddress = new Address("city", "street", 2);
            trainee2.Gear = Gearbox.Automatic;
            trainee2.BDate = new DateTime(1995, 06, 27);
            //  trainee2.LastTest = new DateTime(1934, 08, 27);
            bl.AddTrainee(trainee2);

            Test test2 = new Test();
            test2.IdTrainee = 322675562;
            test2.IdTester = 314699554;
            test2.DateTest = new DateTime(2018, 11, 26);
            test2.AddressTest = new Address("jerusalem", "golomb", 38);
            test2.DayAndHour.Set(DayOfWeek.Monday, 14);
       //     bl.AddTest(test2);

            
           /* Trainee trainee3 = new Trainee();
            trainee3.ID = 33;
            trainee3.Phone = "050";
            trainee3.LessonNum = 40;
            trainee3.School = "a";
            trainee3.FName = "c";
            trainee3.LName = "cc";
            trainee3.CarType = CarType.PrivateCar;
            trainee3.Gender = Gender.Male;
            trainee3.MyAddress = new Address();
            trainee3.MyAddress.SetAddress("city", "street", 2);
            trainee3.Gear = Gearbox.Automatic;
            trainee3.BDate = new DateTime(1995, 06, 27);
            //  trainee2.LastTest = new DateTime(1934, 08, 27);
            bl.AddTrainee(trainee3);*/
        }


        public static void a()
        {
            IBL bl = FactoryBL.Instance();
bl.DeleteAllTrainee();
            
           

            List<Trainee> list = bl.GetTraineesList();


            for (int i = 0; i < 3; i++)
            {
                Trainee s = new Trainee { ID = i, FName = "first", LName = "last" };
                bl.AddTrainee(s);
            }
            list = bl.GetTraineesList();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("sdsssssssssssssssssssssssssssss");


            bl.DeleteTrainee(1);


            bl.UpdateTrainee(new Trainee { ID = 2, FName = "oshri", LName = "cohen" });
            list = bl.GetTraineesList();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }



        }

        public static Tester ToAddTester()
        {
            Tester tester = new Tester();
            Console.WriteLine("please enter the id of the tester\n");
            tester.ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter the phone number of the tester\n");
            tester.Phone = Console.ReadLine();
            Console.WriteLine("please enter the seniority of the tester\n");
            tester.Seniority = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter how much tests the tester can do in one week\n");
            tester.MaxTest = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter the tester's first name\n");
            tester.FName = Console.ReadLine();
            Console.WriteLine("please enter the tester's last name\n");
            tester.LName = Console.ReadLine();
            Console.WriteLine("Which type of car the tester test about: \n" +
                " if private car write 1\n" +
                " if two wheels car-write 2\n" +
                " if medium truck-write 3\n" +
                " if heavy truck-write 4\n");
            int help = Convert.ToInt32(Console.ReadLine());
            switch (help)
            {
                case 1: tester.CarType = CarType.PrivateCar; break;
                case 2: tester.CarType = CarType.TwoWheels; break;
                case 3: tester.CarType = CarType.MTruck; break;
                case 4: tester.CarType = CarType.HTruck; break;
            }
            Console.WriteLine("if the tester is male press 1.  if female press 2\n");
            help = Convert.ToInt32(Console.ReadLine());
            if (help == 1) tester.Gender = Gender.Male;
            else tester.Gender = Gender.Female;
        //    tester.MyAddress = new Address();
            Console.WriteLine("what the name of the city of {0}'s house?", tester.FName);
            string c = Console.ReadLine();
            Console.WriteLine("what the name of the street of {0}'s house?", tester.FName);
            string s = Console.ReadLine();
            Console.WriteLine("what the number of the bilding of {0}'s house?", tester.FName);
            int nb = Convert.ToInt32(Console.ReadLine());
            tester.MyAddress = new Address(c, s, nb);
            Console.WriteLine(" What the maximum destenation in kilometers from {0} street that {1} can test", tester.MyAddress.Street, tester.FName);
            tester.MaxD = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("witch date {0} born?", tester.FName);
            tester.BDate = DateTime.Parse(Console.ReadLine());
            return tester;
        }

        public static Trainee ToAddTrainee()
        {
            Trainee trainee = new Trainee();
            Console.WriteLine("please enter the id of the trainee\n");
            trainee.ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter the phone number of the trainee\n");
            trainee.Phone = Console.ReadLine();
            Console.WriteLine("please enter the trainee's first name\n");
            trainee.FName = Console.ReadLine();
            Console.WriteLine("please enter the trainee's last name\n");
            trainee.LName = Console.ReadLine();
            Console.WriteLine("Which type of car the trainee will examine about: \n" +
               " if private car write 1.\n" +
               " if two wheels car-write 2.\n" +
               " if medium truck-write 3.\n" +
               " if heavy truck-write 4.\n");
            int help = Convert.ToInt32(Console.ReadLine());
            switch (help)
            {
                case 1: trainee.CarType = CarType.PrivateCar; break;
                case 2: trainee.CarType = CarType.TwoWheels; break;
                case 3: trainee.CarType = CarType.MTruck; break;
                case 4: trainee.CarType = CarType.HTruck; break;
            }
            Console.WriteLine("if the trainee is male press 1.  if female press 2\n");
            help = Convert.ToInt32(Console.ReadLine());
            if (help == 1) trainee.Gender = Gender.Male;
            else trainee.Gender = Gender.Female;
            Console.WriteLine("what the name of the city of {0}'s house?", trainee.FName);
            string c = Console.ReadLine();
            Console.WriteLine("what the name of the street of {0}'s house?", trainee.FName);
            string s = Console.ReadLine();
            Console.WriteLine("what the number of the bilding of {0}'s house?", trainee.FName);
            int nb = Convert.ToInt32(Console.ReadLine());
           // trainee.MyAddress = new Address();
            trainee.MyAddress = new Address(c, s, nb);
            Console.WriteLine("In witch school {0} {1} is learning? ", trainee.FName, trainee.LName);
            trainee.School = Console.ReadLine();
            Console.WriteLine("How many lessons did {0} already make?", trainee.FName);
            trainee.LessonNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What the name of {0} teacher?", trainee.FName);
            trainee.Teacher = Console.ReadLine();
            Console.WriteLine("witch date {0} born?", trainee.FName);
            trainee.BDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Whitch gear did {0} learn to drive about? if manual press 1.  if automatic press 2\n", trainee.FName);
            help = Convert.ToInt32(Console.ReadLine());
            if (help == 1) trainee.Gear = Gearbox.Manual;
            else trainee.Gear = Gearbox.Automatic;

            return trainee;
        }

        public static Test ToAddTest()
        {
            Test test = new Test();
            Console.WriteLine("please enter the id of the trainee\n");
            test.IdTrainee = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter the id of the tester\n");
            test.IdTester = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What date will the test take place?\n");
            test.DateTest = new DateTime();
            test.DateTest = DateTime.Parse(Console.ReadLine());
            while (test.DateTest.DayOfWeek == DayOfWeek.Friday || test.DateTest.DayOfWeek == DayOfWeek.Saturday)
            {
                Console.WriteLine("there are no tests in that day\n pleach chose another date");
                test.DateTest = DateTime.Parse(Console.ReadLine());
            }
            Console.WriteLine("what the name of the city where the test start?");
            string c = Console.ReadLine();
            Console.WriteLine("what the name of the street  where the test start?");
            string s = Console.ReadLine();
            Console.WriteLine("what the number of the bilding  where the test start?");
            int nb = Convert.ToInt32(Console.ReadLine());
            test.AddressTest = new Address(c, s, nb);
            Console.WriteLine("in whitch hour the test will be?");
            int hour = Convert.ToInt32(Console.ReadLine());
            while (hour > 14 || hour < 9)
            {
                Console.WriteLine("there are no tests in that hour. please chose hour between 9 to 14");
            }
            test.DayAndHour.HOUR = hour;
            test.DayAndHour.DAY = test.DateTest.DayOfWeek;
            /* List<Tester> gg = new List<Tester>();
             gg = bl.GetTestersList();
             foreach (Tester yy in gg)
             {
                 if (yy.Schedule.Dict[test.DayAndHour.d][11] == false)
                     Console.WriteLine("well done everything work");
             }
             break;
             */

            //     bl.PrintTestList(bl.GetTestsList());
            return test;
        }
        
        static void Main(string[] args)
        {

        /*    IBL bl = FactoryBL.Instance();

            a();
          


            Console.WriteLine("wellcome to our program.");
            string x = "0";
            x = Console.ReadLine();
       
                       IBL bl = FactoryBL.Instance();
        
                        Inputlist(bl);
      int chose = 1;
                        Console.WriteLine("wellcome to our program.");
                        while (chose != 0)
                        {
                           StartPrint();

                            try
                            {
                                chose = Convert.ToInt32(Console.ReadLine());
                                switch (chose)
                                {
                                    case 1:
                                        bl.AddTester(ToAddTester());
                                        break;
                                    case 2:
                                        bl.AddTrainee(ToAddTrainee());
                                        break;
                                    case 3:
                                        bl.AddTest(ToAddTest());

                                        

                                        Test tmp = ToAddTest();
                                        var check = bl.GetTestsList().Where(test => test.IdTrainee == tmp.IdTrainee && test.DateTest == tmp.DateTest);
                                        if (check != null)
                                            bl.AddTest(tmp);
                                        else
                                            Console.WriteLine("its impossible to set 2 test to the same trainee in one day");                     
                                      List<Tester> gg = new List<Tester>();
                                    gg = bl.GetTestersList();
                                    foreach (Tester yy in gg)
                                    {
                                        if (yy.Sched[DayOfWeek.Wednesday.GetHashCode(),11-9] == false)
                                            Console.WriteLine("well done everything work");
                                    }


                                         



            
                                break;
                            case 4:
                                bl.PrintTesterList(bl.GetTestersList());
                                break;
                            case 5:
                                Console.WriteLine("to delete tester enter id:");
                                bl.DeleteTester(Convert.ToInt32(Console.ReadLine()));
                                break;
                            case 6:
                                bl.PrintTraineeList(bl.GetTraineesList()); break;
                            case 7:
                                Console.WriteLine("to delete trainee enter id:");
                                bl.DeleteTrainee(Convert.ToInt32(Console.ReadLine()));
                                break;
                            case 8:
                                Console.WriteLine("update tester:");
                                bl.UpdateTester(ToAddTester());
                                break;
                            case 9:
                                Console.WriteLine("update trainee:");
                                bl.UpdateTrainee(ToAddTrainee());
                                break;
                            case 10:
                                Console.WriteLine("DistanceTesters:");

                                Console.WriteLine("city:");
                                string cc = Console.ReadLine();
                                Console.WriteLine("street:");
                                string ss = Console.ReadLine();
                                Console.WriteLine("number of the bilding:");
                                int nnb = Convert.ToInt32(Console.ReadLine());

                            Address add = new Address(cc, ss, nnb);
                                bl.PrintTesterList(bl.DistanceTesters(add));
                                break;
                            case 11:
                                Console.WriteLine("whitch day in the week do you want to check? please enter a number\n");
                                int day = Convert.ToInt32(Console.ReadLine());
                                DayOfWeek d = new DayOfWeek();
                                switch (day)
                                {
                                    case 1:
                                        d = DayOfWeek.Sunday;
                                        break;
                                    case 2:
                                        d = DayOfWeek.Monday;
                                        break;
                                    case 3:
                                        d = DayOfWeek.Tuesday;
                                        break;
                                    case 4:
                                        d = DayOfWeek.Wednesday;
                                        break;
                                    case 5:
                                        d = DayOfWeek.Thursday;
                                        break;
                                    case 6:
                                        Console.WriteLine("There are not tests in this day.");
                                        break;
                                    case 7:
                                        Console.WriteLine("There are not tests in this day.");
                                        break;
                                    default:
                                        Console.WriteLine("There are not such day.");
                                        break;
                                }
                                Console.WriteLine("write whitch hour in {0} do you want to check", d);
                                int myHour = Convert.ToInt32(Console.ReadLine());
                                Day toCheck = new Day();
                                toCheck.Set(d, myHour);
                                bl.PrintTesterList(bl.AvailableTesters(toCheck));
                                break;
                            case 12:
                                bl.GetTestersList();
                                break;
                            case 13:
                                bl.GetTraineesList();
                                break;
                            case 14:
                                bl.GetTraineesList();
                                break;
                            case 15:
                                bl.PrintTestList(bl.GetTestsList());
                                break;
                            case 16:
                                Console.WriteLine("sort?");
                                bl.GroupByCarTypeTester(Convert.ToBoolean(Console.ReadLine()));
                                bl.PrintTesterList(bl.GetTestersList());
                                break;
                            case 17:
                                Console.WriteLine("sort?");
                                bl.GroupBySchoolTrainee(Convert.ToBoolean(Console.ReadLine()));
                                bl.PrintTraineeList(bl.GetTraineesList());
                                break;
                            case 18:
                                Console.WriteLine("sort?");
                                bl.GroupByTeacherOfTrainee(Convert.ToBoolean(Console.ReadLine()));
                                break;
                            case 19:
                                Console.WriteLine("sort?");
                                bl.GroupByNumTestTrainee(Convert.ToBoolean(Console.ReadLine()));
                                break;

                            case 20:
                                bl.PrintTestList(bl.BoolTests(bl.SuccessTest));
                                break;
                            case 21:
                                bl.PrintTestList(bl.BoolTests(bl.DecemberTest));
                                break;

                            case 22:
                                Criterion c = new Criterion();
                                string testernote;
                                Answer ans;

                                Console.WriteLine("numtest?");
                                int numtest = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("SavingDistance?");
                                c.SavingDistance = (Answer)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("ReverseParking?");
                                c.ReverseParking = (Answer)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("CheckMirrors?");
                                c.CheckMirrors = (Answer)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Signal?");
                                c.Signal = (Answer)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Speed?");
                                c.Speed = (Answer)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("ObedienceSigns?");
                                c.ObedienceSigns = (Answer)Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("anser?");
                                ans = (Answer)Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Tester Note?");
                                testernote = Console.ReadLine();

                                bl.UpdateTest(numtest, c, ans, testernote);

                                break;
                            case 23:
                                Console.WriteLine(bl.SuccessTrainee(ToAddTrainee()));
                                break;
                            case 24:
                                Console.WriteLine(bl.TraineeTestNum(ToAddTrainee()));
                                break;

                            case 25:
                                Console.WriteLine("date time?");
                                DateTime dt = Convert.ToDateTime(Console.ReadLine());
                                bl.PrintTestList(bl.TestOnDate(dt));
                                break;



                            case 0: break;
                            default: break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                }
                
             */
        }
    }
}
