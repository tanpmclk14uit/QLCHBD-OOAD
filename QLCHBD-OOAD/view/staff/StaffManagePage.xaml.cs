using QLCHBD_OOAD.viewmodel.staff;
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

namespace QLCHBD_OOAD.view.staff
{
    /// <summary>
    /// Interaction logic for StaffManagePage.xaml
    /// </summary>
    public partial class StaffManagePage : Page
    {
        public StaffManagePage()
        {
            InitializeComponent();
            this.DataContext = StaffManageViewModel.getIntance();

        }

       
    }
}
