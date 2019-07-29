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
    /// Interaction logic for GetIdTesterWindow.xaml
    /// </summary>
    /// 
    public partial class GetIdTesterWindow : Window
    {
        Tester tester;
        IBL bl;
        public GetIdTesterWindow()
        {
            InitializeComponent();
            tester = new Tester();
            grid.DataContext = tester;
            bl = BL.FactoryBL.Instance();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            Window AddTester = new AddTesterWindow();
            AddTester.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iDTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                if (bl.FindTesterByID(tester.ID) == null)
                {
                    throw new Exception("not exist");
                }
                else
                {
                    Window UpTester = new UpTester(bl.FindTesterByID(tester.ID));
                    UpTester.Show();
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
