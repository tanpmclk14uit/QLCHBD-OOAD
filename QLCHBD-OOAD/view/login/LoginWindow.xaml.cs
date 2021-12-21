using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.viewmodel.login;
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

namespace QLCHBD_OOAD.view.login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel loginViewModel = new LoginViewModel();
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = loginViewModel;
            LoginViewModel.loginHanlder += login;
        }

        private void login()
        {
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            loginViewModel.password = pbPassword.Password;
        }
    }
}
