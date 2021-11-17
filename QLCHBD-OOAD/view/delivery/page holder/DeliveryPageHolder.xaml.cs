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
using QLCHBD_OOAD.view.delivery.DeliveryPage;
using QLCHBD_OOAD.view.delivery.Add_Order;
using QLCHBD_OOAD.viewmodel.delivery;
using QLCHBD_OOAD.viewmodel.delivery.Component;
using QLCHBD_OOAD.viewmodel.delivery.provider;

namespace QLCHBD_OOAD.view.delivery
{
    /// <summary>
    /// Interaction logic for DeliveryPageHolder.xaml
    /// </summary>
    public partial class DeliveryPageHolder : Page
    {
        public DeliveryPageHolder()
        {
            InitializeComponent();
            DataContext = DeliveryPageViewModel.getInstance();
            deliveryHolder.Content = new DeliveryMainPage();
            DeliveryDetailPageViewModel.turnToDeliveryPage += turnBackToDeliveryMainPage;
            ProviderListWindowViewModel.turnToProviderDetailPage += turnToProviderDetailPage;
            DeliveryProviderDetailViewModel.turnToDeliveryDetailPage += turnBackToDeliveryMainPage;
            DeliveryPageViewModel.turnToImportFormDetailPage += turnDeliveryDetailPage;
        }

        private void turnDeliveryDetailPage(string id)
        {
            deliveryHolder.Content = new DeliveryDetailPage(id);
        }


        private void turnBackToDeliveryMainPage(string page)
        {
            deliveryHolder.ClearValue(UidProperty);
            deliveryHolder.Content = new DeliveryMainPage();
        }
        private void turnToProviderDetailPage(string id)
        {
            deliveryHolder.Content = new DeliveryProviderDetailPage(id);
        }
    }
}
