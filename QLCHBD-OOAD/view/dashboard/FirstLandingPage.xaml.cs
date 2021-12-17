using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.viewmodel.dashboard;
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

namespace QLCHBD_OOAD.view.dashboard
{
    /// <summary>
    /// Interaction logic for FirstLandingPage.xaml
    /// </summary>
    public partial class FirstLandingPage : Page
    {
        public static event OnChangePage onChangePageDelivering;
        public FirstLandingPage()
        {
            InitializeComponent();
            this.DataContext = FirstLandingViewModel.getIntance();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            onChangePageDelivering();
        }
    }
}
