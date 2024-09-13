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

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoggedFrame.Navigate(new ProfilePage());
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var path = (sender as Image).DataContext;
            switch (path) {
                case "profile":
                    LoggedFrame.Navigate(new ProfilePage());
                    break;
                case "casino": 
                    LoggedFrame.Navigate(new RullerPage());
                    break;
            }
        }
    }
}
