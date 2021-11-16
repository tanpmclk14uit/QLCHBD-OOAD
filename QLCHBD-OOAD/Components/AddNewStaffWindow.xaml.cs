using QLCHBD_OOAD.appUtil;
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
using System.Windows.Shapes;

namespace QLCHBD_OOAD.Components
{
    /// <summary>
    /// Interaction logic for AddNewStaffWindow.xaml
    /// </summary>
    public partial class AddNewStaffWindow : Window
    {
        public static event ToggleFormDialogNotifyHandler Toggle; 
        public AddNewStaffWindow()
        {
            InitializeComponent();
            Toggle();
            this.DataContext = new AddStaffViewModel();
            AddStaffViewModel.cancel += cancel;
        }

        private void cancel()
        {
            this.Close();
            Toggle();
            AddStaffViewModel.cancel -= cancel;
        }    
    }
}
