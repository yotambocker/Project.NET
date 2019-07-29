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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        Trainee trainee;
        IBL bl;
        public AddTraineeWindow()
        {
            InitializeComponent();

            trainee = new Trainee();
            grid1.DataContext = trainee;
            bl = BL.FactoryBL.Instance();

            this.carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.gearComboBox.ItemsSource = Enum.GetValues(typeof(Gearbox));
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTrainee(trainee); 
                trainee = new Trainee();
                grid1.DataContext = trainee;
                MessageBox.Show("added");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
