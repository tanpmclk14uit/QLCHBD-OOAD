using QLCHBD_OOAD.viewmodel.images;
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

namespace QLCHBD_OOAD.Components
{
    /// <summary>
    /// Interaction logic for AddNewOrderItemImageWindow.xaml
    /// </summary>
    public partial class AddNewOrderItemImageWindow : Window
    {
        public AddNewOrderItemImageWindow()
        {
            InitializeComponent();
            DataContext = new AddNewOrderItemImageViewModel();
            AddNewOrderItemImageViewModel.closeForm += closeWindow;
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
