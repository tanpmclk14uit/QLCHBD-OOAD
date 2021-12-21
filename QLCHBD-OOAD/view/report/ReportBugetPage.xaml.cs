using QLCHBD_OOAD.viewmodel.budget;
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
    /// Interaction logic for ReportBugetPage.xaml
    /// </summary>
    public partial class ReportBugetPage : Page
    {
        public ReportBugetPage()
        {
            InitializeComponent();
            DataContext = BugetViewModel.getInstance();
        }

        private void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            BugetViewModel.getInstance().getBugetInRange();
        }
    }
}
