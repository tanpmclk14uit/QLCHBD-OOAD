﻿using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.model.Guest;
using QLCHBD_OOAD.viewmodel.guest;
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

namespace QLCHBD_OOAD.view.guest
{
    /// <summary>
    /// Interaction logic for GuestDetailInformation.xaml
    /// </summary>
    public partial class GuestDetailInformation : Window
    {

        public static event ToggleFormDialogNotifyHandler toggleForm;
        private long guestId= -1;
        public GuestDetailInformation(Guest guest)
        {
            InitializeComponent();
            this.DataContext = GuestDetailViewModel.getInstance();
            if(guest != null)
            {
                GuestDetailViewModel.getInstance().setGuest(guest);
                guestId = guest.id;
            }
            toggleForm();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            toggleForm();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyDialog myDialog = new MyDialog(MyDialogStyle.ALERT, "Do you want to delete this member");
            myDialog.ShowDialog();
            if (myDialog.action)
            {
                if (guestId != -1)
                {
                    GuestDetailViewModel.getInstance().deleteGuest(guestId);
                }
                this.Close();
                toggleForm();
            }            
        }
    }
}
