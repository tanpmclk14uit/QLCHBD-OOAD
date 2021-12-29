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
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.viewmodel.delivery;

namespace QLCHBD_OOAD.view.delivery
{
    /// <summary>
    /// Interaction logic for DeliveryMainPage.xaml
    /// </summary>
    public partial class DeliveryMainPage : Page
    {
        public DeliveryMainPage()
        {
            InitializeComponent();
            DataContext = DeliveryPageViewModel.getInstance();
            DeliveryPageViewModel.getInstance().resetUI();
        }

        private void setupUI()
        {
            if (!CurrentStaff.getInstance().currentStaff.isManager)
            {
                bttAddProvider.Visibility = Visibility.Hidden;
                bttModifyProvider.Visibility = Visibility.Hidden;
                bttNewOrder.Visibility = Visibility.Hidden;
            }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DeliveryPageViewModel.getInstance().onItemClick();
        }

    }
}
