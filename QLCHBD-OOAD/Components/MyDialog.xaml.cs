using QLCHBD_OOAD.appUtil;
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
    /// Interaction logic for MyDialog.xaml
    /// </summary>
    public partial class MyDialog : Window
    {

        public static event ToggleFormDialogNotifyHandler toggleForm;
        public bool action;
        public MyDialog(MyDialogStyle myDialogStyle, string content)
        {
            InitializeComponent();
            setStyle(myDialogStyle, content);
            toggleForm();
        }
        private void setStyle(MyDialogStyle myDialogStyle, string content)
        {
            var bc = new BrushConverter();
            this.content.Content = content;
            switch (myDialogStyle)
            {
                case MyDialogStyle.ALERT:
                {
                        errorIcon.Visibility = Visibility.Hidden;
                        alertIcon.Visibility = Visibility.Visible;
                        confirmIcon.Visibility = Visibility.Hidden;
                        cancel.Visibility = Visibility.Visible;
                        confirm.Background =(Brush) bc.ConvertFrom("#A72828");
                        break;
                }
                case MyDialogStyle.CONFIRM:
                {
                        errorIcon.Visibility = Visibility.Hidden;
                        alertIcon.Visibility = Visibility.Hidden;
                        confirmIcon.Visibility = Visibility.Visible;
                        cancel.Visibility = Visibility.Visible;
                        confirm.Background =(Brush) bc.ConvertFrom("#28A745");
                        break;
                }
                case MyDialogStyle.ERROR:
                {
                        errorIcon.Visibility = Visibility.Visible;
                        alertIcon.Visibility = Visibility.Visible;
                        confirmIcon.Visibility = Visibility.Hidden;
                        cancel.Visibility = Visibility.Hidden;
                        confirm.Background = (Brush)bc.ConvertFrom("#A72828");
                        break;
                }
                case MyDialogStyle.INFORMATION:
                {
                        errorIcon.Visibility = Visibility.Hidden;
                        alertIcon.Visibility = Visibility.Hidden;
                        confirmIcon.Visibility = Visibility.Visible;
                        cancel.Visibility = Visibility.Hidden;
                        confirm.Background = (Brush)bc.ConvertFrom("#28A745");
                        break;
                }
                    
            }
        }
        
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            action = false;
            this.Close();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            action = true;
            this.Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            toggleForm();
            base.OnClosed(e);
        }
    }
}
