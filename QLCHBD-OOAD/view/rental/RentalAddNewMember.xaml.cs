using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.viewmodel.guest;
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
    /// Interaction logic for RentalAddNewMember.xaml
    /// </summary>
    public partial class RentalAddNewMember : Window
    {
        public static event ToggleFormDialogNotifyHandler toggle;
        public RentalAddNewMember(Guest guest = null)
        {
            InitializeComponent();
            this.DataContext = new GuestViewModel(guest);
            GuestViewModel.closeForm += this.Close;
            toggle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            toggle();
        }
    }
}
