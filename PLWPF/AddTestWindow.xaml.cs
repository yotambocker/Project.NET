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
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        Test test;
        IBL bl;
        public AddTestWindow()
        {
            InitializeComponent();


            test = new Test();
            grid1.DataContext = test;
            bl = BL.FactoryBL.Instance();

            this.dAYComboBox.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            //  hOURComboBox.Items.

            // this.hOURComboBox.ItemsSource = Enum.GetValues(typeof(DateTime));
        }



        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               if (test.DayAndHour.HOUR<6) test.DayAndHour.HOUR += 9;

                bl.AddTest(test);
                test = new Test();
                grid1.DataContext = test;
                MessageBox.Show("added");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Window UpTestWindow = new UpTestWindow(Convert.ToInt32(test.NumTest));
            UpTestWindow.Show();
        }
    }
}
