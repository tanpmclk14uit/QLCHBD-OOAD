using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.viewmodel.rental
{
    class RentalDiskDetailFormViewModel: BaseViewModel
    {
        private Images _selectedImage;

        public Images selectedImage
        {
            get => _selectedImage;
        }

        public RentalDiskDetailFormViewModel(Images images)
        {
            _selectedImage = images;
        }
    }
}
