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
    }
}
