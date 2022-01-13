using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.viewmodel.dashboard;
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

namespace QLCHBD_OOAD.view.dashboard
{
    /// <summary>
    /// Interaction logic for FirstLandingPage.xaml
    /// </summary>
    public partial class FirstLandingPage : Page
    {
        public static event OnChangePage onChangePageDelivering;
        public static event OnChangePage onChangePageTotal;
        public static event OnChangePage onChangePageInStock;
        public static event OnChangePage onChangePageBorrowed;
        public FirstLandingPage()
        {
            InitializeComponent();
            this.DataContext = FirstLandingViewModel.getIntance();
            FirstLandingViewModel.getIntance().onRefresh();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            onChangePageDelivering();
        }

        private void total_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentStaff.getInstance().currentStaff.isManager)
            {
                onChangePageTotal();
            }
            else
            {
                MyDialog myDialog = new MyDialog(MyDialogStyle.ERROR, "Admin only!");
                myDialog.ShowDialog();
            }
           
        }

        private void inStock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            onChangePageInStock();
        }

        private void borrowed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            onChangePageBorrowed();
        }
    }
}
