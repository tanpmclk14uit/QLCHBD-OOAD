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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHBD_OOAD.view.report
{
    /// <summary>
    /// Interaction logic for ReportMainPage.xaml
    /// </summary>
    public partial class ReportMainPage : Page
    {
        public ReportMainPage()
        {
            InitializeComponent();
            holder.Content = new ReportBugetPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            holder.Content = new ReportGuestPage();
        }

        private void Click_Buget(object sender, RoutedEventArgs e)
        {
            holder.Content = new ReportBugetPage();
        }

        private void Click_Delivery(object sender, RoutedEventArgs e)
        {
            holder.Content = new ReportDeliveryPage();
        }
    }
}
