using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
namespace HCI_JumpingArrows.user_controls
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private Queue<Key> generatedArrows = new Queue<Key>();
        private Queue<int> generetedPositions = new Queue<int>();
        private Random random = new Random();

        private int ScoreLimit_SpeedChange = 2000;
        private bool stop = false;
        private bool pause = false;
        private string progress;
        private int hitCounter = 0;
        private int missCounter = 0;
        private int hitMax = 0;
        private int missMax = 0;
        private TextBox txt = new TextBox();
        private bool savedScore=false;

        private CustomObject arrow1;
        private CustomObject arrow2; 
        private CustomObject arrow3;
        private CustomObject arrow4;
        private CustomObject arrow5;


        private Rectangle avatar;

        private string endMsg;
        public string EndMsg
        {
            get { return endMsg; }
            set
            {
                endMsg = value;
                OnPropertyChanged("EndMsg");
            }
        }
        private string statistics;
        public string Statistics
        {
            get { return statistics; }
            set
            {
                statistics = value;
                OnPropertyChanged("Statistics");
            }
        }
        public string Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }
        private double offset;
        public double Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                OnPropertyChanged("Offset");
            }
        }

        private int progressBarValue = 0;
        public int ProgressBarValue
        {
            get { return progressBarValue; }
            set
            {

                progressBarValue = value;
                Progress = (progressBarValue / 10) + "%";
                OnPropertyChanged("ProgressBarValue");


                if (ProgressBarValue < 1000)
                    Offset = 1000.0 / ProgressBarValue;
                else
                {
                    Offset = 1;
                    StopGame();
                }
            }
        }

        private int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                ScoreValue = "Score: " + score;
            }
        }
        private void IncreaseScore(int x)
        {
            if (stop == false && pause == false)
            {
                int score = (int)(x * GameSpeed);
                Score += score;

                int decrement = (int)Math.Floor(10 + GameSpeed);
                if (decrement > ProgressBarValue)
                    ProgressBarValue = 0;
                else ProgressBarValue -= decrement;

                if (Score >= ScoreLimit_SpeedChange)
                {
                    GameSpeed += 0.25;
                    Console.WriteLine(GameSpeed);
                    ScoreLimit_SpeedChange += 2000;
                }
            }
        }
        //kazna 
        private void DecreaseScore(int x)
        {
            if (stop == false && pause == false)
            {
                int score = (int)(x * GameSpeed);
                Score -= score;
            }
        }
        private string scoreValue = "Score: 0";
        public string ScoreValue
        {
            get { return scoreValue; }
            set
            {
                scoreValue = value;
                OnPropertyChanged("ScoreValue");
            }
        }

        private double gameSpeed = 1;
        public double GameSpeed
        {
            get { return gameSpeed; }
            set {
                gameSpeed = value;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(500 / GameSpeed));
            }
        }

        private string msg;
        public string Msg
        {
            get { return msg; }
            set
            {
                msg = value;
                OnPropertyChanged("Msg");
            }
        }
        public GameControl()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler((object sender, EventArgs e) => {
                ProgressBarValue += 10;
            });
            DataContext = this;
            for (int i = 0; i < 1000; i++)
            {
                generetedPositions.Enqueue(random.Next() % 5 + 1);
                int x = random.Next(10000);
                switch (x % 4)
                {
                    case 0:
                        generatedArrows.Enqueue(Key.Up);
                        break;
                    case 1:
                        generatedArrows.Enqueue(Key.Left);
                        break;
                    case 2:
                        generatedArrows.Enqueue(Key.Down);
                        break;
                    default:
                        generatedArrows.Enqueue(Key.Right);
                        break;
                }
            }
        }
        public void StartGame()
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);

            arrow1 = new CustomObject(generatedArrows.Dequeue());
            arrow1.Left = 30 + ((generetedPositions.Dequeue()-1) * 60);
            arrow1.Top = 50;

            arrow2 = new CustomObject(generatedArrows.Dequeue());
            arrow2.Left = 30 + ((generetedPositions.Dequeue() - 1) * 60);
            arrow2.Top = 110;

            arrow3 = new CustomObject(generatedArrows.Dequeue());
            arrow3.Left = 30 + ((generetedPositions.Dequeue() - 1) * 60);
            arrow3.Top = 170;

            arrow4 = new CustomObject(generatedArrows.Dequeue());
            arrow4.Left = 30 + ((generetedPositions.Dequeue() - 1) * 60);
            arrow4.Top = 230;

            arrow5 = new CustomObject(generatedArrows.Dequeue());
            arrow5.Left = 30 + ((generetedPositions.Dequeue() - 1) * 60);
            arrow5.Top = 290;

            Random random = new Random();
            string icon = "";
            switch (random.Next() % 5)
            {
                case 0:
                    icon = "avatar1.png";
                    break;
                case 1:
                    icon = "avatar2.png";
                    break;
                case 2:
                    icon = "avatar3.png";
                    break;
                case 3:
                    icon = "avatar4.png";
                    break;
                case 4:
                    icon = "avatar5.png";
                    break;

            }
            avatar = new Rectangle();
            avatar.Height = 50;
            avatar.Width = 50;
            ImageBrush image = new ImageBrush();
            ImageSource source = new ImageSourceConverter().ConvertFrom(new Uri("pack://application:,,,/HCI_JumpingArrows;component/resources/" + icon)) as ImageSource;
            image.ImageSource = source;
            avatar.Fill = image;
            Canvas.SetLeft(avatar, 150);
            Canvas.SetTop(avatar, 350);

            EndMsg = "3";
            stop = true;
            Task.Delay(1000).ContinueWith(_ =>
            {
                EndMsg = "2";
            });
            Task.Delay(2000).ContinueWith(_ =>
            {
                EndMsg = "1";
            });
            Task.Delay(3000).ContinueWith(_ =>
            {
                EndMsg = "";
                dispatcherTimer.Start();
                stop = false;
            });

            
            

            Graphics.Children.Add(avatar);
            Graphics.Children.Add(arrow5.Rect);
            Graphics.Children.Add(arrow4.Rect);
            Graphics.Children.Add(arrow3.Rect);
            Graphics.Children.Add(arrow2.Rect);
            Graphics.Children.Add(arrow1.Rect);

        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }

        }
        private void PauseGame()
        {
            pause = true;
            dispatcherTimer.Stop();
            Msg = "";
            EndMsg = "Game paused (press P)!";
        }
        private void UnPauseGame()
        {
            Msg = "";

            EndMsg = "3";
            stop = true;
            Task.Delay(1000).ContinueWith(_ =>
            {
                EndMsg = "2";
            });
            Task.Delay(2000).ContinueWith(_ =>
            {
                EndMsg = "1";
            });
            Task.Delay(3000).ContinueWith(_ =>
            {
                EndMsg = "";
                dispatcherTimer.Start();
                pause = false;
                stop = false;
            });



        }
        public void StopGame()
        {
            if (stop == false) {   //dodato
                Console.WriteLine("Game stopped!");
                dispatcherTimer.Stop();
                stop = true;
                EndMsg = ScoreValue;
                ScoreValue = "";
                Msg = "";
                Statistics = "Most consecutive hits-" + hitMax + ",  misses-" + missMax;

                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.VerticalAlignment = VerticalAlignment.Bottom;
                txt.Margin = new System.Windows.Thickness(0, 0, 0, 200);
                txt.Text = "Username";
                txt.Foreground = System.Windows.Media.Brushes.Gray;
                txt.Width = 150;
                txt.Height = 24;
                txt.FontSize = 14;
                txt.FontWeight = FontWeights.Bold;
                txt.GotFocus += (object sender, RoutedEventArgs i) =>
                {
                    if (txt.Text.Trim().Equals("Username"))
                    {
                        txt.Text = ""; txt.Foreground = System.Windows.Media.Brushes.Black;
                    }
                };
                Grid.SetRow(txt, 0);
                ContentGrid.Children.Add(txt);
            }
        }
        private void Move()
        {
            Canvas.SetLeft(avatar, arrow5.Left);

            arrow1.Top= arrow2.Top;
            arrow2.Top= arrow3.Top;
            arrow3.Top= arrow4.Top;
            arrow4.Top= arrow5.Top;

            arrow5 = arrow4;
            arrow4 = arrow3;
            arrow3 = arrow2;
            arrow2 = arrow1;
            arrow1 = new CustomObject(generatedArrows.Dequeue());
            arrow1.Left = 30 + ((generetedPositions.Dequeue()-1) * 60);
            arrow1.Top = 50;

            Graphics.Children.Clear();
            Graphics.Children.Add(avatar);
            Graphics.Children.Add(arrow5.Rect);
            Graphics.Children.Add(arrow4.Rect);
            Graphics.Children.Add(arrow3.Rect);
            Graphics.Children.Add(arrow2.Rect);
            Graphics.Children.Add(arrow1.Rect);
        }
        public void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (stop == false)
            {
                if (e.Key == Key.S)
                    StopGame();
                else if (pause && e.Key == Key.P)
                    UnPauseGame();
                else if (!pause && e.Key == Key.P)
                    PauseGame();
                else
                {
                    if (e.Key == arrow5.Key)
                    {
                        hitCounter++;
                        missCounter = 0;
                        IncreaseScore(100);
                        if (hitCounter > hitMax)
                            hitMax = hitCounter;
                        Move();
                    }
                    else
                    {
                        hitCounter = 0;
                        missCounter++;
                        if (missCounter > missMax)
                            missMax = missCounter;
                    }
                    if (missCounter != 0 && missCounter % 3 == 0)
                    {
                        DecreaseScore(missCounter / 3 * 500);
                        Msg = missCounter + " misses in row -" + (missCounter / 3 * 500 );

                    }
                    else if (hitCounter != 0 && hitCounter % 5 == 0)
                    {
                        IncreaseScore(hitCounter / 5 * 100);
                        Msg = hitCounter + " hits in row +" + (hitCounter / 5 * 100);
                    }
                }
            }
            else if (txt.IsFocused && e.Key==Key.Enter && txt.Text.Equals("")==false && savedScore==false)
            {
                StreamWriter sw = new StreamWriter("results.txt", true);
                sw.WriteLine(txt.Text + "#" + Score);
                sw.Close();
                savedScore = true;
            }
        }
    }
    internal class CustomObject {
        public Rectangle Rect { get; set; }
        public Key Key { get; set; }

        private int left;
        public int Left
        {
            get { return left; }
            set
            {
                left = value;
                Canvas.SetLeft(Rect, value);
            }
        }

        private int top;
        public int Top
        {
            get { return top; }
            set
            {
                top = value;
                Canvas.SetTop(Rect,value);
            }
        }
        
        public CustomObject(Key key)
        {
            Key = key;
            Rect = new Rectangle();
            Rect.Height = 50;
            Rect.Width = 50;
            string icon = "";
            switch (key)
            {
                case Key.Up:
                    icon = "up.png";
                    break;
                case Key.Down:
                    icon = "down.png";
                    break;
                case Key.Left:
                    icon = "left.png";
                    break;
                case Key.Right:
                    icon= "right.png";
                    break;
            }
            ImageBrush image= new ImageBrush();
            ImageSource source= new ImageSourceConverter().ConvertFrom(new Uri("pack://application:,,,/HCI_JumpingArrows;component/resources/" + icon)) as ImageSource;
            image.ImageSource = source;
            Rect.Fill = image;
        }
    }
}
