using QLCHBD_OOAD.appUtil;
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
    /// Interaction logic for RentalAddMember.xaml
    /// </summary>
    public partial class RentalAddMember : Window
    {
        public static event ToggleFormDialogNotifyHandler ToggleForm;
        public RentalAddMember(long id=-1)
        {
            InitializeComponent();
            if (id != -1)
            {
                RentalAddMemberViewModel.getIntance().setGuest(id.ToString());
            }
            DataContext = RentalAddMemberViewModel.getIntance();            
            ToggleForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ToggleForm();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            ToggleForm();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;                       
            RentalAddNewMember rental = new RentalAddNewMember();                    
            rental.ShowDialog();          

        }
        
    }
}
