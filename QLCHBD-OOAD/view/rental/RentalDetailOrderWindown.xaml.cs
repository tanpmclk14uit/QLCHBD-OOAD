using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.viewmodel.rental;
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

namespace QLCHBD_OOAD.view.rental
{
    /// <summary>
    /// Interaction logic for RentalDetailOrderWindown.xaml
    /// </summary>
    public partial class RentalDetailOrderWindown : Window
    {
        DetailRentalPageViewModel rentalPageViewModel;
        public RentalDetailOrderWindown(long retalOrderId, long guestID)
        {
            InitializeComponent();
            rentalPageViewModel = new DetailRentalPageViewModel(retalOrderId, guestID);
            DataContext = rentalPageViewModel;
            MyDialog.toggleForm += ToggleForm;
            
        }
        private void ToggleForm()
        {
            if (this.Opacity == 1)
            {
                this.Opacity = 0.3;
                this.IsEnabled = false;
            }
            else
            {
                this.Opacity = 1;
                this.IsEnabled = true;
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (rentalPageViewModel.currentRentalBill.returnedALl)
            {
                MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.CONFIRM, "Do you want to delete this rental, the action can't be undone!");
                myDialog.ShowDialog();
                if (myDialog.action == true)
                {
                    rentalPageViewModel.deleteOrder();
                    this.Close();
                }
               
            }
            else
            {
                MyDialog myDialog = new MyDialog(appUtil.MyDialogStyle.ERROR, "All item must be returned first!!");
                myDialog.ShowDialog();
            }
            
        }
    }
}
