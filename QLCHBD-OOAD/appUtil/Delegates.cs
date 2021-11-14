using QLCHBD_OOAD.model.images;
using QLCHBD_OOAD.model.retal;
using QLCHBD_OOAD.model.delivery;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCHBD_OOAD.model.Guest;

//this is use for define delegate
namespace QLCHBD_OOAD.appUtil
{
    public delegate void BackHandler();
    public delegate void AddImageHandler(Images newImages);
    public delegate void ToggleFormDialogNotifyHandler();
    public delegate void DeleteImageHandler(long id);
    public delegate void SaveImagesHandler(Images newInfo);
    public delegate void ChangePageHandler(string page ="");
    public delegate void TurnToDetailPageHandler(long rentalId, long guestId);
    public delegate void TurnBackPageHandler();
    public delegate void ClearListViewSelected();
    public delegate void CloseFormHandler();
    public delegate void AddNewRentalBillItem(RentalBillItem rentalBillItem);
    public delegate void AddNewDeliveryOrderItem(string delivery);
    public delegate void GuestTranferInformation(Guest guest);
    public delegate void SelectedImageChangeHandler();
    public delegate void AddOrderHandler();
    public delegate void DeleteOrderDiskItemHandler();
    public delegate void ConfirmAddOrderImage();
    public delegate void ConfirmAddOrderItemImage(Images images);
    public delegate void GetImportItems(DeliOrderItems items);
    public delegate void onChangeListImages();
}
