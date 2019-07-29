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
using System.Data.SqlClient;
using System.Data;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow1.xaml
    /// </summary>
    public partial class AdminWindow1
    {
        IBL bl;

        public AdminWindow1()
        {
            InitializeComponent();
            bl = BL.FactoryBL.Instance();
            // List<Trainee> trainees = FactoryBL.Instance().GetTraineesList();
            List<Test> tests = FactoryBL.Instance().GetTestsList();
            //  this.TestDataGrid.Text = tests.ToString();
            //  foreach (var item in trainees) MessageBox.Show(item.ToString());

            this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GetTraineesList();
            this.TesterDataGrid.ItemsSource = FactoryBL.Instance().GetTestersList();
            this.TestDataGrid.ItemsSource = FactoryBL.Instance().GetTestsList();

        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            Window AddTestWindow = new AddTestWindow();
            AddTestWindow.Show();
        }
        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {
            Window AddTraineeWindow = new AddTraineeWindow();
            AddTraineeWindow.Show();
        }
        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            Window AddTesterWindow = new AddTesterWindow();
            AddTesterWindow.Show();
        }

        private void UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            int id = (((Button)sender).DataContext as BE.Trainee).ID;
            Window UpTrainee = new UpTrainee(bl.FindTraineeByID(id));
            UpTrainee.Show();
            this.Hide();
        }
        private void UpdateTest_Click(object sender, RoutedEventArgs e)
        {
            int numtest = Convert.ToInt32((((Button)sender).DataContext as BE.Test).NumTest);
            Window UpTestWindow = new UpTestWindow(numtest);
            UpTestWindow.Show();
            this.Hide();
        }
        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
            int id = (((Button)sender).DataContext as BE.Tester).ID;
            Window UpTester = new UpTester(bl.FindTesterByID(id));
            UpTester.Show();
            this.Hide();
        }


        private void DeleteTrainee_Click(object sender, RoutedEventArgs e)
        {
            int id = (((Button)sender).DataContext as BE.Trainee).ID;

            //  MessageBox.Show((((Button) sender).DataContext as BE.Trainee).ID.ToString());
            MessageBoxResult result = MessageBox.Show
          (
          "Are you sure you want to delete?", "Delete",
          MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
          MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTrainee(id);
                MessageBox.Show("Deleted!");
                this.Close();
            }
        }
        private void DeleteTester_Click(object sender, RoutedEventArgs e)
        {
            int id = (((Button)sender).DataContext as BE.Tester).ID;

            //  MessageBox.Show((((Button) sender).DataContext as BE.Trainee).ID.ToString());
            MessageBoxResult result = MessageBox.Show
          (
          "Are you sure you want to delete?", "Delete",
          MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
          MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTester(id);
                MessageBox.Show("Deleted!");
                this.Close();
            }
        }

        private void GroupBySchoolTrainee_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"To sort?", "sort",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupBySchoolTrainee(true);
            }
            else
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupBySchoolTrainee(false);
            }
        }

        private void GroupByTeacherOfTrainee_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"To sort?", "sort",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupByTeacherOfTrainee(true);
            }
            else
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupByTeacherOfTrainee(false);
            }
        }

        private void GroupByNumTestTrainee_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"To sort?", "sort",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupByNumTestTrainee(true);
            }
            else
            {
                this.TraineeDataGrid.ItemsSource = FactoryBL.Instance().GroupByNumTestTrainee(false);
            }
        }

        private void GroupByCarType_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"To sort?", "sort",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                this.TesterDataGrid.ItemsSource = FactoryBL.Instance().GroupByCarTypeTester(true);
            }
            else
            {
                this.TesterDataGrid.ItemsSource = FactoryBL.Instance().GroupByCarTypeTester(false);
            }
        }

        private void Delete_all_trainee_Click(object sender, RoutedEventArgs e)
        {     
                MessageBoxResult result = MessageBox.Show
    (
    "Are you sure you want to delete all trainees?", "Delete",
    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                if (result == MessageBoxResult.Yes)
                {
                bl.DeleteAllTrainee();
                    MessageBox.Show("Deleted!");
                    this.Close();
                }
        }

        private void Delete_all_testers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show
(
"Are you sure you want to delete all testers?", "Delete",
MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes,
MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteAllTester();
                MessageBox.Show("Deleted!");
                this.Close();
            }
        }

        private void Succsess_Click(object sender, RoutedEventArgs e)
        {
           MessageBox.Show(bl.PrintTestList(bl.BoolTests(bl.SuccessTest)));
        }

        private void NotPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(bl.PrintTestList(bl.BoolTests(bl.FailTest)));
        }
    }
}
