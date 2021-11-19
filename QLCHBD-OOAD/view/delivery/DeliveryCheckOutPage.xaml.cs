using QLCHBD_OOAD.viewmodel.delivery.detail_order;
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

namespace QLCHBD_OOAD.view.delivery
{
    /// <summary>
    /// Interaction logic for DeliveryCheckOutPage.xaml
    /// </summary>
    public partial class DeliveryCheckOutPage : Page
    {
        public DeliveryCheckOutPage(string id)
        {
            InitializeComponent();
            DataContext = new DeliveryCheckOutViewModel(id);
        }
    }
}
