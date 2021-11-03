using QLCHBD_OOAD.view.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QLCHBD_OOAD.viewmodel.images
{
    class ImageFunctionViewModel: BaseViewModel
    {

        public Page _slideFrame;

    // Property
        public Page SlideFrame
        {
            get { return _slideFrame; }
            set
            {
                _slideFrame.NavigationService.Refresh();
                _slideFrame = value;
                OnPropertyChanged("SlideFrame");
            }
        }

        private ImageFunctionViewModel()
        {
            _slideFrame = new ImagesPage();
        }

        private static ImageFunctionViewModel _instance;

        public static ImageFunctionViewModel getIntance()
        {
            if (_instance == null)
            {
                _instance = new ImageFunctionViewModel();
            }
            return _instance;
        }

    }
}
