using QLCHBD_OOAD.Components;
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

namespace QLCHBD_OOAD.view.images
{
    /// <summary>
    /// Interaction logic for ImageDetailPage.xaml
    /// </summary>
    public partial class ImageDetailPage : Page
    {
        public ImageDetailPage()
        {
            InitializeComponent();
            DataContext = ImageDetailViewModel.getIntance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteImageForm deleteImageForm = new DeleteImageForm(ImageDetailViewModel.selectedDisk.id);
            deleteImageForm.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChangeImageInformationForm changeImageInformationForm = new ChangeImageInformationForm(ImageDetailViewModel.selectedDisk);
            changeImageInformationForm.Show();
        }
    }
}
