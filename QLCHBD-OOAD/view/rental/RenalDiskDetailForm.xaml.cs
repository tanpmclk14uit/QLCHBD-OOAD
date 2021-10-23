using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.images;
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
    /// Interaction logic for RenalDiskDetailForm.xaml
    /// </summary>
    public partial class RenalDiskDetailForm : Window
    {
        public static event ToggleFormDialogNotifyHandler ToggleForm;
        public static event ClearListViewSelected clearListViewSelected;
        public RenalDiskDetailForm(Images selectedImage)
        {
            InitializeComponent();
            DataContext = new RentalDiskDetailFormViewModel(selectedImage);
            ToggleForm();
            RentalDiskDetailFormViewModel.closeForm += RentalDiskDetailFormViewModel_closeForm;
        }

        private void RentalDiskDetailFormViewModel_closeForm()
        {
            clearListViewSelected();                      
            this.Close();            
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ToggleForm();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            clearListViewSelected();
            this.Close();        
            
        }
    }
}
