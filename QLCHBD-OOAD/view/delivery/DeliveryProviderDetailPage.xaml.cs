using QLCHBD_OOAD.viewmodel.delivery.provider;
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
    /// Interaction logic for DeliveryProviderDetailPage.xaml
    /// </summary>
    public partial class DeliveryProviderDetailPage : Page
    {
        public DeliveryProviderDetailPage(string id)
        {
            InitializeComponent();
            DataContext = new DeliveryProviderDetailViewModel(id);
        }

       
    }
}
