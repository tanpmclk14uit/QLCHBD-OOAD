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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryReportViewModel.getInstance().getDeliveryInRange();
        }
    }
}
