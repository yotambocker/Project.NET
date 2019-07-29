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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GetPasswordAdminWindow.xaml
    /// </summary>
    public partial class GetPasswordAdminWindow : Window
    {
        public GetPasswordAdminWindow()
        {
            InitializeComponent();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (checkPasswordText.Password == "")
            {
                Window Adminwindow = new AdminWindow1();
                Adminwindow.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Password incorrect");
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {

                if (e.Key == Key.Return)
                {
                Check_Click(sender, e);
                }
            }
        
    }
}