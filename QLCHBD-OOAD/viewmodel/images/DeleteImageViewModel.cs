using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.images
{
    class DeleteImageViewModel: BaseViewModel
    {
        private RentalBillRepository rentalBillRepository = RentalBillRepository.getIntance();
        private ImagesRepository imagesRepository = ImagesRepository.getInstance();

        public ICommand deleteImageCommand { get; set; }

        public static event DeleteImageHandler deleteImage;

        public DeleteImageViewModel(long needDeleteId)
        {
            deleteImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { deleteImage(needDeleteId); });
            deleteImage += onDeleteImage;
        }

        private bool checkValidToDelete(long id)
        {
            if (rentalBillRepository.getNumberOfBillById(id) == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Can't delete the disk current in rentaling !");
                return false;
            }
        }
        
        public void onDeleteImage(long id)
        {
            
            if (checkValidToDelete(id))
            {
                imagesRepository.deleteDisk(id);
                ImageDetailViewModel.getIntance().backToImages();
                ImagesViewModel.getIntance().onChange();
            }
  
        }

    }
}
