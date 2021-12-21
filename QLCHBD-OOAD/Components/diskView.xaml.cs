using QLCHBD_OOAD.appUtil;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHBD_OOAD.Components
{
    /// <summary>
    /// Interaction logic for diskDetail.xaml
    /// </summary>
    public partial class diskView : Window
    {
        public static event ToggleFormDialogNotifyHandler ToggleForm;
        private DiskViewViewModel diskViewViewModel;
        public diskView()
        {
            InitializeComponent();
            ToggleForm();
            diskViewViewModel = new DiskViewViewModel();
            DataContext = diskViewViewModel;
        }

        private void bttDivorce_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(tb_quantity.Text) > 0)
            {
                tb_quantity.Text = (Convert.ToInt32(tb_quantity.Text) - 1).ToString();
            }
        }

        private void bttPlus_Click(object sender, RoutedEventArgs e)
        {
            tb_quantity.Text = (Convert.ToInt32(tb_quantity.Text) + 1).ToString();
        }

        private void bttSave_Click(object sender, RoutedEventArgs e)
        {
            if (diskViewViewModel.validateForUI())
            {
                this.Close();
                ToggleForm();
            }
        }

        private void bttCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ToggleForm();
        }


    }
}
