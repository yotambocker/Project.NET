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
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    
        public class ConvertToBool : IValueConverter
        {
            public object Convert(  object value,  Type targetType, object parameter,  CultureInfo culture)
            {
                if ((Answer)value == Answer.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public object ConvertBack( object value,   Type targetType,  object parameter,   CultureInfo culture)
            {
            if ((bool)value ==true)
            {
                return Answer.Success;
            }
            else
            {
                return Answer.Fail;
            }
        }
        }
    

    

    /// <summary>
    /// Interaction logic for UpTestWindow.xaml
    /// </summary>
    public partial class UpTestWindow : Window
    {
        Test test;
        IBL bl;
        TesterInfo TesterInfoCheck;

        public UpTestWindow(int num)
        {
            InitializeComponent();
            bl = BL.FactoryBL.Instance();
            test = bl.FindTestBuyNum(num);
            TesterInfoCheck = new TesterInfo(test.Criterion, test.Answer, test.TesterNote);
            upgrid.DataContext = test;
            infogrid.DataContext = TesterInfoCheck;
        }



        private void Update_Click(object sender, RoutedEventArgs e)
        {
               try
               {
                   bl.UpdateTest(Convert.ToInt32(test.NumTest), TesterInfoCheck.Criterion, TesterInfoCheck.Testeranswer, TesterInfoCheck.Testernote);
                this.Hide();
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
        }


    }
}
