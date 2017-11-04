namespace sd_fight
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private const int WindowWidth = 410;
        private const int WindowHeight = 335;
        private const int RefreshFrameDuration = 16;

        private System.Windows.Controls.Canvas canvas;
        private System.Windows.UIElement[] imgs;
        private System.Windows.UIElement bg;
        private double bgLeft;
        private KeyListener KeyListener;
        private Guy guy;

        public MainWindow()
        {
            InitializeComponent();
            InitWindow();
            KeyListener = new KeyListener();
            guy = new Guy(this, KeyListener, WindowWidth, WindowHeight);

            /*
            System.Windows.UIElement img1 = Utils.LoadImage("image/1.png");
            System.Windows.Controls.Canvas.SetLeft(img1, 180);
            System.Windows.Controls.Canvas.SetTop(img1, 180);

            System.Windows.UIElement img2 = Utils.LoadImage("image/2.png");
            System.Windows.Controls.Canvas.SetLeft(img2, 180 + 20);
            System.Windows.Controls.Canvas.SetTop(img2, 180);

            System.Windows.UIElement img3 = Utils.LoadImage("image/3.png");
            System.Windows.Controls.Canvas.SetLeft(img3, 180);
            System.Windows.Controls.Canvas.SetTop(img3, 180);

            System.Windows.UIElement img4 = Utils.LoadImage("image/4.png");
            System.Windows.Controls.Canvas.SetLeft(img4, 180 - 20);
            System.Windows.Controls.Canvas.SetTop(img4, 180);

            imgs = new System.Windows.UIElement[4];
            imgs[0] = img1;
            imgs[1] = img2;
            imgs[2] = img3;
            imgs[3] = img4;
            */
            bg = Utils.LoadImage("image/chapter/2.jpg");
            bgLeft = 0;

            canvas = new System.Windows.Controls.Canvas();
            canvas.Children.Add(bg);
            this.Content = canvas;

            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.Closing += WindowClosing;

            guy.Run();
            //new System.Threading.Thread(PlaySkill).Start();
            new System.Threading.Thread(KeepOnMoveBG).Start();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 原谅我吧，懒得一个个地Dispose，就把这项艰巨的任务交给操作系统了
            System.Environment.Exit(0);
        }

        public void DoAction(System.Action action)
        {
            try
            {
                this.Dispatcher.Invoke(action);
            }
            catch (System.Exception e)
            {
                Utils.WriteLog(e.Message);
            }
        }

        public void AddUIElement(System.Windows.UIElement ui)
        {
            try
            {
                this.Dispatcher.Invoke(new System.Action(() =>
                {
                    this.canvas.Children.Add(ui);
                }));
            }
            catch (System.Exception e)
            {
                Utils.WriteLog(e.Message);
            }
        }

        public void RemoveUIElement(System.Windows.UIElement ui)
        {
            try
            {
                this.Dispatcher.Invoke(new System.Action(() =>
                {
                    this.canvas.Children.Remove(ui);
                }));
            }
            catch (System.Exception e)
            {
                Utils.WriteLog(e.Message);
            }
        }

        public void PlaySkill()
        {
            for (int i = 0; i < 100000; i++)
            {
                try
                {
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        canvas.Children.Add(imgs[i % imgs.Length]);
                    }));
                    System.Threading.Thread.Sleep(30);
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        canvas.Children.Remove(imgs[i % imgs.Length]);
                    }));
                }
                catch (System.Exception e)
                {
                    Utils.WriteLog(e.Message);
                    break;
                }
            }
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyListener.OnKeyDown(e.Key);
        }

        private void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyListener.OnKeyUp(e.Key);
        }
        
        private void KeepOnMoveBG()
        {
            for (; ; )
            {
                int dir = 0;
                if (KeyListener.IsPressingLeft && KeyListener.IsPressingRight)
                {
                    dir = 0;
                }
                else if (KeyListener.IsPressingLeft)
                {
                    dir = -1;
                }
                else if (KeyListener.IsPressingRight)
                {
                    dir = 1;
                }
                if (dir != 0)
                {
                    bgLeft -= 1 * dir;
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        System.Windows.Controls.Canvas.SetLeft(bg, bgLeft);
                    }));
                }
                System.Threading.Thread.Sleep(RefreshFrameDuration);
            }
        }

        private void InitWindow()
        {
            this.Title = "SD Fight";
            this.Icon = Utils.LoadImageSource("image/icon.png");
            this.Width = WindowWidth;
            this.Height = WindowHeight;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }
    }
}
