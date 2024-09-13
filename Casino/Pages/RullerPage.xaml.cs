using Casino.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Windows.Threading;

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для RullerPage.xaml
    /// </summary>
    public partial class RullerPage : Page
    {

        double totalSpin;
        double spined;
        Random random;
        WinColor Color;
        WinColor Sel = WinColor.Red;
        DispatcherTimer SpinTimer;
        public RullerPage()
        {
            InitializeComponent();
            SpinTimer = new DispatcherTimer();
            SpinTimer.Interval = TimeSpan.FromMilliseconds(1);
            SpinTimer.Tick += SpinTimer_Tick;
            random = new Random();

        }

        private void BetTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = BetTB.Text;
            text = new string(text.Where(c => int.TryParse(c.ToString(), out int temp)).ToArray());
            BetTB.Text = text;
            BetsButtons.IsEnabled = BetTB.Text != "" && int.Parse(BetTB.Text) < Utils.LoggedUser.CurrentBalance;
        }

        private void MakeBet(object sender, RoutedEventArgs e)
        {

            var selected = (sender as Button).DataContext;
            if (selected == "red")
                Sel = WinColor.Red;
            else if (selected == "green")
                Sel = WinColor.Green;
            else
                Sel = WinColor.Black;

            totalSpin = (random.NextDouble() + 0.5) * 360d;

            var sector = ((int)(totalSpin / (360 / 38)) % 38);
            if(sector == 1 || sector == 20)
            {
                Color = WinColor.Green;
            }else if( sector > 1 && sector < 20)
            {
                if (sector % 2 == 1)
                    Color = WinColor.Red;
                else
                    Color = WinColor.Black;
            }
            else if(sector > 21 && sector < 38)
            {
                if (sector % 2 == 0)
                    Color = WinColor.Red;
                else
                    Color = WinColor.Black;
            }
            var bet = int.Parse(BetTB.Text);

            Utils.LoggedUser.CurrentBalance -= bet;
            var transaction = new Transaction();
            transaction.User = Utils.LoggedUser;
            transaction.MoneyMoving = -bet;
            transaction.Date = DateTime.Now;
            transaction.TypeId = 2;

            Utils.DB.Transaction.Add(transaction);
            Utils.DB.SaveChanges();

            SpinTimer.IsEnabled = true;
            SpinTimer.Start();
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            spined += 1;
            if(spined > totalSpin)
            {
                SpinTimer.Stop();
                spined = 0;
                if(Color == Sel)
                {
                    var winRatio = 2;
                    if (Color == WinColor.Green)
                        winRatio = 15;

                    var bet = int.Parse(BetTB.Text) * winRatio;
                    MessageBox.Show($"Вы выиграли {bet}");

                    Utils.LoggedUser.CurrentBalance += bet;
                    var transaction = new Transaction();
                    transaction.User = Utils.LoggedUser;
                    transaction.MoneyMoving = bet;
                    transaction.Date = DateTime.Now;
                        transaction.TypeId = 1;

                    Utils.DB.Transaction.Add(transaction);
                    Utils.DB.SaveChanges();
                }
                else
                {
                    MessageBox.Show($"Вы проиграли");

                }
                return;
            }
            RullerImage.RenderTransform = new RotateTransform(spined, 250, 250);
        }
    }

    enum WinColor
    {
        Red, Black, Green
    }
}
