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
    /// Interaction logic for UpTester.xaml
    /// </summary>
    public partial class UpTester : Window
    {
        IBL bl;
        public UpTester(Tester tester)
        {

            InitializeComponent();
            Upgrid.DataContext = tester;
            bl = BL.FactoryBL.Instance();
            this.carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.myTestDataGrid.ItemsSource = bl.ListTestIdTester(tester.ID); ;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateTester(new Tester
            {

                ID = Convert.ToInt32(iDTextBox.Text),
                Phone = phoneTextBox.Text,
                Seniority = Convert.ToInt32(seniorityTextBox.Text),
                MaxTest = Convert.ToInt32(maxDTextBox.Text),
                Tests = Convert.ToInt32(testsTextBox.Text),
                MaxD = Convert.ToDouble(maxDTextBox.Text),
                LName = lNameTextBox.Text,
                FName = fNameTextBox.Text,
                MyAddress = new Address(cityTextBox.Text, streetTextBox.Text, Convert.ToInt32(nBuildingTextBox.Text)),
                BDate = DateTime.Parse(bDateDatePicker.Text),
                Gender = (Gender)Enum.Parse(typeof(Gender), genderComboBox.Text),
                CarType = (CarType)Enum.Parse(typeof(CarType), carTypeComboBox.Text)

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
                bl.DeleteTester(Convert.ToInt32(iDTextBox.Text));
                MessageBox.Show("Deleted!");
                this.Close();
            }
        }

        private void UpdateTest_Click(object sender, RoutedEventArgs e)
        {
            int numtest = Convert.ToInt32((((Button)sender).DataContext as BE.Test).NumTest);
            Window UpTestWindow = new UpTestWindow(numtest);
            UpTestWindow.Show();
            this.Hide();
        }


    }
}