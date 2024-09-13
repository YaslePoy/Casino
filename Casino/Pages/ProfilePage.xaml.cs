using Casino.DB;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            DataContext = Utils.LoggedUser;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }


        private void MoneyMove(decimal money)
        {
            if (money == 0)
                return;
            Utils.LoggedUser.CurrentBalance += money;
            var transaction = new Transaction();
            transaction.User = Utils.LoggedUser;
            transaction.MoneyMoving = money;
            transaction.Date = DateTime.Now;
            if (money < 0)
                transaction.TypeId = 2;
            else
                transaction.TypeId = 1;

            Utils.DB.Transaction.Add(transaction);
            Utils.DB.SaveChanges();
            DataContext = null;
            DataContext = Utils.LoggedUser;

        }

        private void MoneyOut(object sender, RoutedEventArgs e)
        {
            var delta = DeltaTB.Text;
            if(decimal.TryParse(delta, out decimal money))
            {
                MoneyMove(-money);
            }
        }
        private void MoneyIn(object sender, RoutedEventArgs e)
        {
            var delta = DeltaTB.Text;
            if (decimal.TryParse(delta, out decimal money))
            {
                MoneyMove(money);
            }
        }

        private void DeltaTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = DeltaTB.Text;
            text =new string(text.Where(c => int.TryParse(c.ToString(), out int temp)).ToArray());
            DeltaTB.Text = text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TransactionsPage());
        }
    }
}
