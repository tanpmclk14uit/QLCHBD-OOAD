using QLCHBD_OOAD.model.delivery;
using QLCHBD_OOAD.viewmodel.delivery;
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
using System.Windows.Shapes;

namespace QLCHBD_OOAD.view.delivery.component
{
    /// <summary>
    /// Interaction logic for AddNewProvider.xaml
    /// </summary>
    public partial class AddNewProvider : Window
    {

        public AddNewProvider()
        {
            InitializeComponent();
            DataContext = new AddNewProviderViewModel();
            AddNewProviderViewModel.closeForm += closeWindow;
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
