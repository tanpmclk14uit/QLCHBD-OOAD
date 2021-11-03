using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.model.images;
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
    /// Interaction logic for ImagesPage.xaml
    /// </summary>
    public partial class ImagesPage : Page
    {
        public ImagesPage()
        {
            InitializeComponent();
            DataContext = ImagesViewModel.getIntance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            diskView newDiskView = new diskView();
            newDiskView.ShowDialog();
        }

        private void ListImages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Images;
            if (item != null)
            {
                ImagesViewModel.getIntance().selectedImage = item;
            }
        }

        private void ListImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImagesViewModel.getIntance().selectedImageChange();
        }
    }
}
