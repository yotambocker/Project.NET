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
    /// Interaction logic for UpTrainee.xaml
    /// </summary>
    public partial class UpTrainee : Window
    {
        IBL bl;
        public UpTrainee(Trainee trainee)
        {
            InitializeComponent();
            Upgrid.DataContext = trainee;
            bl = BL.FactoryBL.Instance();
            this.carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.gearComboBox.ItemsSource = Enum.GetValues(typeof(Gearbox));
            this.myTestDataGrid.ItemsSource = bl.ListTestIdTrainee(trainee.ID);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
           bl.UpdateTrainee(new Trainee {
                ID = Convert.ToInt32(iDTextBox.Text),
                Phone = phoneTextBox.Text,
                LessonNum = Convert.ToInt32(lessonNumTextBox.Text),
                School = schoolTextBox.Text,
                Teacher =teacherTextBox.Text,
                FName = fNameTextBox.Text,
                LName =lNameTextBox.Text,
                Gender = (Gender)Enum.Parse(typeof(Gender), genderComboBox.Text),
                 MyAddress =new Address(cityTextBox.Text, streetTextBox.Text, Convert.ToInt32(nBuildingTextBox.Text)),
                BDate = DateTime.Parse(bDateDatePicker.Text),
                CarType = (CarType)Enum.Parse(typeof(CarType), carTypeComboBox.Text),
                Gear = (Gearbox)Enum.Parse(typeof(Gearbox), gearComboBox.Text),
                LastTest=DateTime.Parse(lastTestDatePicker.Text),
                HaveGlasses=Convert.ToBoolean(haveGlassesCheckBox.IsChecked)
                /*, MyTest*/
            });
            MessageBox.Show("Updated!");
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"Are you sure you want to delete?", "Delete",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTrainee(Convert.ToInt32(iDTextBox.Text));
                MessageBox.Show("Deleted!");
                this.Close();
            }
        }

      
    }
}