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

namespace QLCHBD_OOAD.Components
{
    /// <summary>
    /// Interaction logic for DeliveryAskForm.xaml
    /// </summary>
    public partial class DeliveryAskForm : Window
    {
        public DeliveryAskForm()
        {
            InitializeComponent();
            DataContext = new DeliveryAskFormViewModel();
            DeliveryAskFormViewModel.closeForm += DeliveryAskFormViewModel_CloseForm;
        }

        private void DeliveryAskFormViewModel_CloseForm()
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
