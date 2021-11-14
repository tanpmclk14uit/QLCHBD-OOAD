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
using System.Windows.Shapes;
using QLCHBD_OOAD.viewmodel.delivery.Component;


namespace QLCHBD_OOAD.view.delivery.Add_Order
{
    /// <summary>
    /// Interaction logic for AddNewDiskDeliveryWindow.xaml
    /// </summary>
    public partial class AddNewDiskDeliveryWindow : Window
    {
        public AddNewDiskDeliveryWindow(int count)
        {
            InitializeComponent();
            DataContext = new DeliveryAddNewViewModel(count);
            DeliveryAddNewViewModel.closeForm += closeWindow;
        }

        public void closeWindow()
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
