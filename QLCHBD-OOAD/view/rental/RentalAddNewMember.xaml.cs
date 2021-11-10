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
        public RentalAddNewMember()
        {
            InitializeComponent();
            this.DataContext = new GuestViewModel(null);
            GuestViewModel.closeForm += this.Close;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
