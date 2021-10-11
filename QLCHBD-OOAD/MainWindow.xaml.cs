using QLCHBD_OOAD.dao;
using QLCHBD_OOAD.view.rental;
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

namespace QLCHBD_OOAD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HomeScreen content = new HomeScreen();
            //Holder.Content = content;
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
        }

        private void bttDelivering_Click(object sender, RoutedEventArgs e)
        {
            //Screen content = new Screen();
            //Holder.Content = content;
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





        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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
        private void bttMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
                WindowState = WindowState.Maximized;

        }

    }
}
