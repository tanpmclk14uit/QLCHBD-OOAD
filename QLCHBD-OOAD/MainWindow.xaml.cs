using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.view.images;
using QLCHBD_OOAD.view.rental;
using QLCHBD_OOAD.view.delivery;
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
using QLCHBD_OOAD.viewmodel;
using QLCHBD_OOAD.viewmodel.images;
using QLCHBD_OOAD.Components;
using QLCHBD_OOAD.view.report;
using QLCHBD_OOAD.view.staff;
using QLCHBD_OOAD.view.guest;
using QLCHBD_OOAD.view.dashboard;
using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.view.login;
using QLCHBD_OOAD.model.staff;

namespace QLCHBD_OOAD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mRestoreIfMove = false;
        public MainWindow()
        {
            InitializeComponent();
            FirstLandingPage content = new FirstLandingPage();
            Holder.Content = content;
            setUpForAuthorize();
            diskView.ToggleForm += ToggleForm;
            DeleteImageForm.ToggleForm += ToggleForm;
            ChangeImageInformationForm.ToggleForm += ToggleForm;
            RenalDiskDetailForm.ToggleForm += ToggleForm;
            RentalAddMember.ToggleForm += ToggleForm;
            AddNewOrderImageWindow.ToggleForm += ToggleForm;
            AddNewStaffWindow.Toggle += ToggleForm;
            ChangePasswordWindow.ToggleForm += ToggleForm;
            GuestDetailInformation.toggleForm += ToggleForm;
            RentalAddNewMember.toggle += ToggleSecondaryForm;
            FirstLandingPage.onChangePageDelivering += onChangePageDelivering;
            FirstLandingPage.onChangePageTotal += onChangePageTotal;
            FirstLandingPage.onChangePageBorrowed += onChangeBorrowed;
            FirstLandingPage.onChangePageInStock += onChangeInStock;
        }

        private void setUpForAuthorize()
        {
            if (!CurrentStaff.getInstance().currentStaff.isManager)
            {
                btnManageStaff.Visibility = Visibility.Collapsed;
                spAccountManage.Margin = new Thickness(0, 20, 0 ,0);
            }
            else
            {
                btnManageStaff.Visibility = Visibility.Visible;
            }
        }

        private void onChangeInStock()
        {
            ImageFunctionPage content = new ImageFunctionPage();
            Holder.Content = content;
        }

        private void onChangeBorrowed()
        {
            RentalMainPage rentalMainPage = new RentalMainPage();
            Holder.Content = rentalMainPage;
        }

        private void onChangePageTotal()
        {
            ReportMainPage content = new ReportMainPage();
            Holder.Content = content;
        }

        private void onChangePageDelivering()
        {
            DeliveryMainPage content = new DeliveryMainPage();
            Holder.Content = content;
        }

        public bool isOpen = false;
        private void openCloseManageAccount()
        {
            if (!isOpen)
            {
                spAccountManage.Visibility = Visibility.Visible;
                isOpen = true;
            }
            else
            {
                spAccountManage.Visibility = Visibility.Hidden;
                isOpen = false;
            }
        }
        private void bttDashBoard_Click(object sender, RoutedEventArgs e)
        {
            FirstLandingPage content = new FirstLandingPage();
            Holder.Content = content;
        }

        private void bttImages_Click(object sender, RoutedEventArgs e)
        {

            ImageFunctionPage imagesPage = new ImageFunctionPage();
            Holder.Content = imagesPage;
        }

        private void bttDelivering_Click(object sender, RoutedEventArgs e)
        {
            DeliveryPageHolder deliveryHoler = new DeliveryPageHolder();
            Holder.Content = deliveryHoler;
        }

        private void bttBorrowed_Click(object sender, RoutedEventArgs e)
        {
            RentalMainPage rentalMainPage = new RentalMainPage();
            Holder.Content = rentalMainPage;
        }

        private void bttReport_Click(object sender, RoutedEventArgs e)
        {

            ReportMainPage content = new ReportMainPage();
            Holder.Content = content;
        }
        private void bttLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow content = new LoginWindow();
            content.Show();
            CurrentStaff.getInstance().CopyPropertiesTo(new Staff());
            this.Close();
        }


        private void bttClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            this.Close();
        }
        private void bttMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void SwitchWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                bttMaximize.Content = "⬜";
            }
            else
            {
                WindowState = WindowState.Maximized;
                bttMaximize.Content = "❐";
            }
        }
        private void bttMaximize_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindowState();
        }
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if ((ResizeMode == ResizeMode.CanResize) || (ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    SwitchWindowState();
                }

                return;
            }

            else if (WindowState == WindowState.Maximized)
            {
                mRestoreIfMove = true;
                return;
            }

            DragMove();
        }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (mRestoreIfMove)
            {
                mRestoreIfMove = false;

                var point = PointToScreen(e.MouseDevice.GetPosition(this));

                Left = point.X - (RestoreBounds.Width * 0.5);
                Top = point.Y;

                WindowState = WindowState.Normal;
                bttMaximize.Content = "⬜";

                DragMove();
            }
        }

        private void Header_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mRestoreIfMove = false;
        }

        private void ToggleSecondaryForm()
        {
            if(this.Opacity == 1)
            {
                this.Opacity = 0.29;
                this.IsEnabled = false;
            }
            else
            {
                if (this.Opacity == 0.3)
                {
                    return;
                }
                else
                {
                    this.Opacity = 1;
                    this.IsEnabled = true;
                }                
            }
        }
        private void ToggleForm()
        {
            if (this.Opacity == 1)
            {
                this.Opacity = 0.3;
                this.IsEnabled = false;
            }
            else
            {
                this.Opacity = 1;
                this.IsEnabled = true;
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            openCloseManageAccount();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
            openCloseManageAccount();
        }

        private void btnManageStaff_Click(object sender, RoutedEventArgs e)
        {
            StaffManagePage content = new StaffManagePage();
            Holder.Content = content;
            openCloseManageAccount();
        }
    }
}
