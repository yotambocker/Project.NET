using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private void inputlist(IBL bl)
        {
            Tester tester1 = new Tester();
            tester1.ID = 311525836;
            tester1.Phone = "050";
            tester1.Seniority = 15;
            tester1.MaxTest = 9999;
            tester1.FName = "yotam";
            tester1.LName = "bocker";
            tester1.CarType = CarType.PrivateCar;
            tester1.Gender = Gender.Male;
            tester1.MyAddress = new Address("city", "street", 1);
            tester1.MaxD = 1;
            tester1.BDate = new DateTime(1934, 08, 17);
            bl.AddTester(tester1);

            Trainee trainee1 = new Trainee();
            trainee1.ID = 123456782;
            trainee1.Phone = "050";
            trainee1.LessonNum = 40;
            trainee1.School = "dasds";
            trainee1.FName = "sadasd";
            trainee1.LName = "asdsads";
            trainee1.CarType = CarType.PrivateCar;
            trainee1.Gender = Gender.Male;
            trainee1.MyAddress = new Address("ss", "ddss", 44);
            trainee1.Gear = Gearbox.Automatic;
            trainee1.BDate = new DateTime(1995, 06, 17);
            trainee1.HaveGlasses = true;
            //  trainee1.LastTest = new DateTime(1934, 08, 17);
            bl.AddTrainee(trainee1);
            Test test1 = new Test();
            test1.IdTrainee = 123456782;
            test1.IdTester = 311525836;
            test1.DateTest = new DateTime(2018, 12, 26);
            test1.AddressTest = new Address("jerusalem", "golomb", 38);
            test1.DayAndHour.Set(DayOfWeek.Sunday, 11);
            bl.AddTest(test1);

            Tester tester2 = new Tester();
            tester2.ID = 314699554;
            tester2.Phone = "050";
            tester2.Seniority = 25;
            tester2.MaxTest = 24;
            tester2.FName = "bsasas";
            tester2.LName = "basasb";
            tester2.CarType = CarType.PrivateCar;
            tester2.Gender = Gender.Male;
            tester2.MyAddress = new Address("ciasaty", "stasreet", 2);
            tester2.MaxD = 2;
            tester2.BDate = new DateTime(1934, 08, 27);
            bl.AddTester(tester2);

            Trainee trainee2 = new Trainee();
            trainee2.ID = 322675562;
            trainee2.Phone = "050";
            trainee2.LessonNum = 40;
            trainee2.School = "sasab";
            trainee2.FName = "bdsds";
            trainee2.LName = "bsdsb";
            trainee2.CarType = CarType.PrivateCar;
            trainee2.Gender = Gender.Male;
            trainee2.MyAddress = new Address("city", "street", 22);
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
            bl.AddTest(test2);

         /*   Trainee trainee3 = new Trainee();
            trainee3.ID = 11;
            trainee3.Phone = "050";
            trainee3.LessonNum = 40;
            trainee3.School = "c";
            trainee3.FName = "c";
            trainee3.LName = "cc";
            trainee3.CarType = CarType.PrivateCar;
            trainee3.Gender = Gender.Male;
            trainee3.MyAddress = new Address("city", "street", 2);
            trainee3.Gear = Gearbox.Automatic;
            trainee3.BDate = new DateTime(1995, 06, 27);
            //  trainee2.LastTest = new DateTime(1934, 08, 27);
            bl.AddTrainee(trainee3);

    */

        }

        public MainWindow()
        {
            InitializeComponent();
            IBL bl = BL.FactoryBL.Instance();
        
            bl.GetTestsList();

        }
        
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Window GetPasswordAdminWindow = new GetPasswordAdminWindow();
            GetPasswordAdminWindow.Show();
        }
        private void GetIDbutton_Click(object sender, RoutedEventArgs e)
        {
            Window getid = new GetIdTraineeWindow();
            getid.Show();
        }

        private void aTester_Click(object sender, RoutedEventArgs e)
        {
            Window getTesterId = new GetIdTesterWindow();
            getTesterId.Show();
        }
    }
}

