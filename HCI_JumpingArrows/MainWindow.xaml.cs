using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

using HCI_JumpingArrows.user_controls;

namespace HCI_JumpingArrows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private GameControl gameControl;
        private Menu menu;
        public MainWindow()
        {
            InitializeComponent();
            menu = new Menu();
            menu.FontSize = 14;
            Grid.SetRow(menu, 0);
            MenuItem menuItem1 = new MenuItem();
            menuItem1.BorderThickness = new System.Windows.Thickness(2);
            menuItem1.BorderBrush = System.Windows.Media.Brushes.Gray;
            menuItem1.Click += Start;
            menuItem1.Header = "New game";

            MenuItem menuItem2 = new MenuItem();
            menuItem2.BorderThickness = new System.Windows.Thickness(2);
            menuItem2.BorderBrush = System.Windows.Media.Brushes.Gray;
            menuItem2.Click += Back;
            menuItem2.Header = "Go back";

            menu.Items.Add(menuItem1);
            menu.Items.Add(menuItem2);
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();

            ContentGrid.Children.Add(menu);

            if (gameControl != null)
                gameControl.StopGame();

            gameControl = new GameControl();
            Grid.SetRow(gameControl, 1);
            ContentGrid.Children.Add(gameControl);
            gameControl.StartGame();
        }
        //povrataka na pocetak
        private void Back(object sender, RoutedEventArgs e)
        {
            if (gameControl != null)
                gameControl.StopGame();
            ContentGrid.Children.Clear();
            Leaderboard leaderboard = new Leaderboard();
            Grid.SetRow(leaderboard, 1);
            ContentGrid.Children.Add(leaderboard);
        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                Start(sender,e);
            if(gameControl!=null)
            gameControl.OnKeyPress(sender, e);
        }
    }
}
