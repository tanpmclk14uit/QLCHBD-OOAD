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
            //HomeScreen content = new HomeScreen();
            //Holder.Content = content;
            diskView.ToggleForm += ToggleForm;
            DeleteImageForm.ToggleForm += ToggleForm;
            ChangeImageInformationForm.ToggleForm += ToggleForm;
            RenalDiskDetailForm.ToggleForm += ToggleForm;           
        }

        private void bttDashBoard_Click(object sender, RoutedEventArgs e)
        {
            //Screen content = new Screen();
            //Holder.Content = content;
        }

        private void bttImages_Click(object sender, RoutedEventArgs e)
        {
            //Screen content = new Screen();
            //Holder.Content = content;
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
            //Screen content = new Screen();
            //Holder.Content = content;
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
    }
}
