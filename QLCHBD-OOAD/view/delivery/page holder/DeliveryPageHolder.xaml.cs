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
using QLCHBD_OOAD.viewmodel.delivery.detail_order;

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
            DeliveryDetailPageViewModel.turnToDeliveryPage += turnBackToMainPage;
            DeliveryDetailPageViewModel.turnToDeliveryCheckOutPage += turnDeliveryCheckOutPage;
            ProviderListWindowViewModel.turnToProviderDetailPage += turnToProviderDetailPage;
            DeliveryProviderDetailViewModel.turnToDeliveryDetailPage += turnBackToMainPage;
            DeliveryPageViewModel.turnToImportFormDetailPage += turnDeliveryDetailPage;
            DeliveryPageViewModel.turnToPaymentPage += turnDeliveryCheckOutPage;
            DeliveryCheckOutViewModel.turnToDeliveryMainPage += turnBackToDeliveryMainPage;
        }

        private void turnBackToMainPage(string page)
        {
            try
            {
                deliveryHolder.NavigationService.GoBack();
            }
            catch(InvalidOperationException e)
            {
                deliveryHolder.ClearValue(UidProperty);
                deliveryHolder.Content = new DeliveryMainPage();
            }
            
        }

        private void turnDeliveryDetailPage(string id)
        {
            deliveryHolder.ClearValue(UidProperty);
            deliveryHolder.Content = new DeliveryDetailPage(id);
        }
        private void turnDeliveryCheckOutPage(string id)
        {
            deliveryHolder.ClearValue(UidProperty);
            deliveryHolder.Content = new DeliveryCheckOutPage(id);
        }

        private void turnBackToDeliveryMainPage(string page)
        {
            deliveryHolder.ClearValue(UidProperty);
            deliveryHolder.Content = new DeliveryMainPage();
        }
        private void turnToProviderDetailPage(string id)
        {
            deliveryHolder.ClearValue(UidProperty);
            deliveryHolder.Content = new DeliveryProviderDetailPage(id);
        }
    }
}
