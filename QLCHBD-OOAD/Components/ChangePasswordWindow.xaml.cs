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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public static event ToggleFormDialogNotifyHandler ToggleForm;
        public ChangePasswordWindow()
        {
            InitializeComponent();
            ToggleForm();
            this.DataContext = new ChangePasswordViewModel();
            ChangePasswordViewModel.cancel += cancel;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ToggleForm();
        }

        private void cancel()
        {
            this.Close();
            ToggleForm();
            ChangePasswordViewModel.cancel -= cancel;
        }
    }
}
