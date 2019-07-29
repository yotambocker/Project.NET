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
            grid1.DataContext = tester;
            bl = BL.FactoryBL.Instance();

    
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTester(tester);
                tester = new Tester();
                grid1.DataContext = tester;
                MessageBox.Show("added");
                //   Trainee tmp = bl.GetTraineesList()[0];
                //    MessageBox.Show("{0}", tmp.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
