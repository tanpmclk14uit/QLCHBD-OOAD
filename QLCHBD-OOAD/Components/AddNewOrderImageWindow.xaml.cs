using QLCHBD_OOAD.model.images;
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
    /// Interaction logic for AddNewOrderImageWindow.xaml
    /// </summary>
    public partial class AddNewOrderImageWindow : Window
    {
        AddNewOrderImageViewModel addNewOrderImageViewModel;

        public AddNewOrderImageWindow(List<Images> lstOnOrder)
        {
            InitializeComponent();
            addNewOrderImageViewModel = new AddNewOrderImageViewModel(lstOnOrder);
            this.DataContext = addNewOrderImageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            addNewOrderImageViewModel.updateList();
        }
    }
}
