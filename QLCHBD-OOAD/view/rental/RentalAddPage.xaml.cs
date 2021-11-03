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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHBD_OOAD.view.rental
{
    /// <summary>
    /// Interaction logic for RentalAddPage.xaml
    /// </summary>
    public partial class RentalAddPage : Page
    {
        public RentalAddPage()
        {
            InitializeComponent();
            DataContext = RentalAddPageViewModel.getIntance();
            RenalDiskDetailForm.clearListViewSelected += RenalDiskDetailForm_clearListViewSelected;
        }

        private void RenalDiskDetailForm_clearListViewSelected()
        {

            listImage.UnselectAll();
        }
    }
}
