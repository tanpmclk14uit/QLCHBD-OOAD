using QLCHBD_OOAD.appUtil;
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
        public static event ToggleFormDialogNotifyHandler ToggleForm;

        public AddNewOrderImageWindow(List<Images> lstOnOrder)
        {
            InitializeComponent();
            ToggleForm();
            addNewOrderImageViewModel = new AddNewOrderImageViewModel(lstOnOrder);
            this.DataContext = addNewOrderImageViewModel;
            AddNewOrderImageViewModel.deleteOrderItem += updateList;
            AddNewOrderImageViewModel.confirmOrderImage += CloseForm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ToggleForm();
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            
           
        }

        private void orderTable_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            addNewOrderImageViewModel.updateList();
            updateList();
        }

        private void updateList()
        {
            this.orderTable.CommitEdit();
            this.orderTable.CommitEdit();
            this.orderTable.CancelEdit();
            this.orderTable.CancelEdit();
            orderTable.Items.Refresh();
        }

        private void CloseForm()
        {
            this.Close();
            ToggleForm();
        }
    }
}
