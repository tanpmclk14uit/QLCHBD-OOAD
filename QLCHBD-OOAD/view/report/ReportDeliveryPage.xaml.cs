using QLCHBD_OOAD.viewmodel.delivery;
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
    /// Interaction logic for ReportDeliveryPage.xaml
    /// </summary>
    public partial class ReportDeliveryPage : Page
    {
        public ReportDeliveryPage()
        {
            InitializeComponent();
            DataContext = DeliveryReportViewModel.getInstance();
        }
        private void setupUI()
        {
            if (txCancel.Text.Equals("0")) gridCancel.Visibility = Visibility.Collapsed;
            if (txDelivered.Text.Equals("0")) gridDelivered.Visibility = Visibility.Collapsed;
            if (txDelivering.Text.Equals("0")) gridDelivering.Visibility = Visibility.Collapsed;
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryReportViewModel.getInstance().getDeliveryInRange();
            setupUI();
        }

        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DeliveryReportViewModel.getInstance().getDeliveryInRange();
            setupUI();
        }
    }
}
