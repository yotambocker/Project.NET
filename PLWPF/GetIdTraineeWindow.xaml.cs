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
    /// Interaction logic for GetIdTraineeWindow.xaml
    /// </summary>
    public partial class GetIdTraineeWindow : Window
    {
        Trainee trainee;
        IBL bl;
        public GetIdTraineeWindow()
        {
            InitializeComponent();
            trainee = new Trainee();
            grid.DataContext = trainee;
            bl = BL.FactoryBL.Instance();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            Window addNewTrainee = new AddTraineeWindow();
            addNewTrainee.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iDTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                if (bl.FindTraineeByID(trainee.ID) == null)
                {
                    throw new Exception("not exist");
                }
                else
                {
                    Window UpTrainee = new UpTrainee(bl.FindTraineeByID(trainee.ID));
                    UpTrainee.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
