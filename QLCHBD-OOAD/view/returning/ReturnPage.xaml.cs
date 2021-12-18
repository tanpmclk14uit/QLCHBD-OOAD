using QLCHBD_OOAD.Components;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHBD_OOAD.view.returning
{
    /// <summary>
    /// Interaction logic for ReturnPage.xaml
    /// </summary>
    public partial class ReturnPage : Page
{
     private ReturningViewModel returningViewModel;
    public ReturnPage(long retalOrderId, long guestID)
    {
        InitializeComponent();
        returningViewModel = new ReturningViewModel(retalOrderId, guestID);
        DataContext = returningViewModel;
    }

        private void selectAll_Checked(object sender, RoutedEventArgs e)
        {
            returningViewModel.selectAll();
        }

        private void selectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            returningViewModel.unSelectAll();
        }

        private void returnAll_Checked(object sender, RoutedEventArgs e)
        {
            returningViewModel.returnAll();
        }

        private void returnAll_Unchecked(object sender, RoutedEventArgs e)
        {
            returningViewModel.unCheckReturnAll();
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            if (returningViewModel.isReturnListEmpty())
            {
                MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ERROR, "Nothing returned! Please check return column and check box!");
                myDialog.ShowDialog();
            }
            else
            {
                MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "You definitely want to return all disks?");
                myDialog.ShowDialog();
                if (myDialog.action == true)
                {
                    returningViewModel.makeNewReceipt();
                }
            }
                      
        }
    }
}
