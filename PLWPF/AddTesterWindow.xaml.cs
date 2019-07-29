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
using System.Windows.Shapes;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTesterWindow.xaml
    /// </summary>
    public partial class AddTesterWindow : Window
    {
        Tester tester;
        IBL bl;
        public AddTesterWindow()
        {
            InitializeComponent();
            tester = new Tester();
            Addgrid.DataContext = tester;
            bl = BL.FactoryBL.Instance();
            this.carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            /* bool[][] tmp = new bool[5][];
             for(int i = 0; i < 5; i++)
             {
                 tmp[i] = new bool[6];
             }
             this.Sun9.IsChecked = tmp[0][0];
             tester.Sched = myconvert(tmp);
              this.Mon9.IsChecked = tester.Sched[1, 0];
              this.Thurs9.IsChecked = tester.Sched[2, 0];
              this.Wednes9.IsChecked = tester.Sched[3, 0];
              this.Tues9.IsChecked = tester.Sched[4, 0];
              this.Sun10.IsChecked = tester.Sched[0, 1];
              this.Mon10.IsChecked = tester.Sched[1, 1];
              this.Thurs10.IsChecked = tester.Sched[2, 1];
              this.Wednes10.IsChecked = tester.Sched[3, 1];
              this.Tues10.IsChecked = tester.Sched[4, 1];

              this.Sun11.IsChecked = tester.Sched[0, 2];
              this.Mon11.IsChecked = tester.Sched[1, 2];
              this.Thurs11.IsChecked = tester.Sched[2, 2];
              this.Wednes11.IsChecked = tester.Sched[3, 2];
              this.Tues11.IsChecked = tester.Sched[4, 2];

              this.Sun12.IsChecked = tester.Sched[0,3];
              this.Mon12.IsChecked = tester.Sched[1,3];
              this.Thurs12.IsChecked = tester.Sched[2,3];
              this.Wednes12.IsChecked = tester.Sched[3,3];
              this.Tues12.IsChecked = tester.Sched[4,3];

              this.Sun13.IsChecked = tester.Sched[0,4];
              this.Mon13.IsChecked = tester.Sched[1,4];
              this.Thurs13.IsChecked = tester.Sched[2,4];
              this.Wednes13.IsChecked = tester.Sched[3,4];
              this.Tues13.IsChecked = tester.Sched[4,4];

              this.Sun14.IsChecked = tester.Sched[0,5];
              this.Mon14.IsChecked = tester.Sched[1,5];
              this.Thurs14.IsChecked = tester.Sched[2,5];
              this.Wednes14.IsChecked = tester.Sched[3,5];
              this.Tues14.IsChecked = tester.Sched[4,5];*/
        }

        public bool[,] myconvert(bool[][] x)
        {
            bool[,] tmp = new bool[5, 6];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x[i].Length; j++)
                    tmp[i, j] = x[i][j];
            }
            return tmp;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTester(tester);
                tester = new Tester();
                tester.MyAddress = new Address();
                Addgrid.DataContext = tester;

                MessageBox.Show("Added!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
