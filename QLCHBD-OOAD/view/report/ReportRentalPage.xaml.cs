using QLCHBD_OOAD.viewmodel.rental;
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
    /// Interaction logic for ReportRentalPage.xaml
    /// </summary>
    public partial class ReportRentalPage : Page
    {
        public ReportRentalPage()
        {
            InitializeComponent();
            DataContext = new RentalReportViewModel();
        }

        private void rental_Click(object sender, RoutedEventArgs e)
        {
            rental.Background = Brushes.AliceBlue;
            receipt.Background = Brushes.White;
            rentalItem.Visibility = Visibility.Visible;
            receiptItems.Visibility = Visibility.Hidden;
        }

        private void receipt_Click(object sender, RoutedEventArgs e)
        {
            rental.Background = Brushes.White;
            receipt.Background = Brushes.AliceBlue;
            rentalItem.Visibility = Visibility.Hidden;
            receiptItems.Visibility = Visibility.Visible;
        }
    }
}
