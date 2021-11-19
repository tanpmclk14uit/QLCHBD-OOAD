using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLCHBD_OOAD.viewmodel.delivery.detail_order
{

    class DeliveryAddNewAlbumViewModel : BaseViewModel
    {
        public static event CloseFormHandler closeForm;
        private AlbumRepository albumRepository;
        public static event GetImportItems GetItemsFromAddWindow;


        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public DeliveryAddNewAlbumViewModel()
        {
            albumRepository = AlbumRepository.getInstance();
            ConfirmCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onConfirm(); });
            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) => { onCancel(); });
        }

        private void onCancel()
        {
            closeForm();
        }

        private void onConfirm()
        {
            if ((string.IsNullOrEmpty(tbAlbumName) || string.IsNullOrWhiteSpace(tbAlbumName)))
            {
                MessageBox.Show("Please fill name", "Null");
            }
            else
            {
                albumRepository.addAlbum(tbAlbumName);
                closeForm();
            }
        }

        public string tbAlbumName { get; set;}

    }
}
