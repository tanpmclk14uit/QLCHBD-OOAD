using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.model.receipt;
using QLCHBD_OOAD.viewmodel.returning;
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

namespace QLCHBD_OOAD.view.returning
{
    /// <summary>
    /// Interaction logic for ReceiptDetailWindow.xaml
    /// </summary>
    public partial class ReceiptDetailWindow : Window
    {
        public ReceiptDetailWindow(Receipt receipt)
        {
            InitializeComponent();
            DataContext = new DetailReceiptViewModel(receipt);
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

   
    }
}
