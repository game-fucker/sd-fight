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

namespace sd_fight
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Controls.Canvas cv;
        private UIElement[] imgs;
        private UIElement bg;
        private double bgLeft;
        private bool pressLeft;
        private bool pressRight;

        public MainWindow()
        {
            InitializeComponent();
            InitSize();

            UIElement img1 = LoadImage("image/1.png");
            Canvas.SetLeft(img1, 180);
            Canvas.SetTop(img1, 180);

            UIElement img2 = LoadImage("image/2.png");
            Canvas.SetLeft(img2, 180 + 20);
            Canvas.SetTop(img2, 180);

            UIElement img3 = LoadImage("image/3.png");
            Canvas.SetLeft(img3, 180);
            Canvas.SetTop(img3, 180);

            UIElement img4 = LoadImage("image/4.png");
            Canvas.SetLeft(img4, 180 - 20);
            Canvas.SetTop(img4, 180);

            imgs = new UIElement[4];
            imgs[0] = img1;
            imgs[1] = img2;
            imgs[2] = img3;
            imgs[3] = img4;

            bg = LoadImage("image/m1.jpg");
            bgLeft = 0;

            cv = new System.Windows.Controls.Canvas();
            cv.Children.Add(bg);
            this.Content = cv;

            this.KeyDown += MoveBG;
            this.KeyUp += StopMoveBG;
            this.Closing += Window_Closing;

            new System.Threading.Thread(PlaySkill).Start();
            new System.Threading.Thread(KeepOnMoveBG).Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        public void PlaySkill()
        {
            for (int i = 0; i < 100000; i++)
            {
                try
                {
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        cv.Children.Add(imgs[i % imgs.Length]);
                    }));
                    System.Threading.Thread.Sleep(30);
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        cv.Children.Remove(imgs[i % imgs.Length]);
                    }));
                }
                catch (Exception)
                {
                    //MessageBox.Show(e.Message);
                    break;
                }
            }
        }

        private void MoveBG(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key.ToString());
            if (e.Key == Key.D)
            {
                pressRight = true;
            }
            else if (e.Key == Key.A)
            {
                pressLeft = true;
            }
        }

        private void StopMoveBG(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                pressRight = false;
            }
            else if (e.Key == Key.A)
            {
                pressLeft = false;
            }
        }

        private void KeepOnMoveBG()
        {
            for (; ; )
            {
                int dir = 0;
                if (pressLeft && pressRight)
                {
                    dir = 0;
                }
                else if (pressLeft)
                {
                    dir = -1;
                }
                else if (pressRight)
                {
                    dir = 1;
                }
                if (dir != 0)
                {
                    bgLeft -= 1 * dir;
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        Canvas.SetLeft(bg, bgLeft);
                    }));
                }
                System.Threading.Thread.Sleep(5);
            }
        }

        private void InitSize()
        {
            this.Title = "SD Fight";
            this.Icon = LoadImageSource("image/icon.png");
            this.Width = 410;
            this.Height = 335;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private Image LoadImage(string uri)
        {
            System.Windows.Controls.Image img = new Image();
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Source = LoadImageSource(uri);

            return img;
        }

        private BitmapImage LoadImageSource(string uri)
        {
            System.Windows.Media.Imaging.BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = GetStreamFromFile(uri);
            bi.EndInit();
            return bi;
        }

        private System.IO.Stream GetStreamFromFile(string uri)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(uri, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] buf = new byte[fs.Length];
                fs.Read(buf, 0, (int)fs.Length);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                return ms;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                System.Environment.Exit(0);
            }
            return null;
        }
    }
}
