namespace sd_fight
{
    class Guy : Player
    {
        private const int Speed = 5;
        
        private System.Windows.UIElement stand;
        private System.Windows.UIElement[] walkLeft;
        private System.Windows.UIElement[] walkRight;
        private int[] walkRate;

        private MainWindow window;
        private KeyListener KeyListener;
        private int WindowWidth;
        private int WindowHeight;

        private bool faceRight;
        private System.Windows.UIElement prev;
        private int posX;
        private int posY;

        public Guy(MainWindow window, KeyListener kl, int width, int height)
        {
            Init();
            this.window = window;
            KeyListener = kl;
            WindowWidth = width;
            WindowHeight = height;

            faceRight = true;
        }

        public void Run()
        {
            new System.Threading.Thread(Loop).Start();
        }

        private void Loop()
        {
            InitStandThere();
            long walkIdx = 0;
            int rate = this.walkRate[0];
            while (true)
            {
                if (KeyListener.IsPressingRight)
                {
                    rate++;
                    if (rate >= this.walkRate[walkIdx])
                    {
                        this.posX++;
                        this.window.DoAction(new System.Action(() =>
                        {
                            System.Windows.Controls.Canvas.SetLeft(this.walkRight[walkIdx], this.posX);
                            System.Windows.Controls.Canvas.SetTop(this.walkRight[walkIdx], this.posY);
                        }));
                        this.window.RemoveUIElement(prev);
                        this.window.AddUIElement(this.walkRight[walkIdx]);
                        this.prev = this.walkRight[walkIdx];
                        walkIdx = (walkIdx + 1) % this.walkRight.Length;
                        rate = 0;
                    }
                }
                else if (this.prev != this.stand)
                {
                    this.window.DoAction(new System.Action(() => {
                        System.Windows.Controls.Canvas.SetLeft(this.stand, this.posX);
                        System.Windows.Controls.Canvas.SetTop(this.stand, this.posY);
                    }));
                    this.window.RemoveUIElement(prev);
                    this.window.AddUIElement(this.stand);
                    this.prev = this.stand;
                    walkIdx = 0;
                    rate = this.walkRate[0];
                }
                System.Threading.Thread.Sleep(Speed);
            }
        }

        private void InitStandThere()
        {
            this.posX = 20;
            this.posY = this.WindowHeight / 2 + 30;
            this.window.DoAction(new System.Action(() =>
            {
                System.Windows.Controls.Canvas.SetLeft(stand, this.posX);
                System.Windows.Controls.Canvas.SetTop(stand, this.posY);
            }));
            this.window.AddUIElement(stand);
            this.prev = stand;
        }

        private void Init()
        {
            stand = Utils.LoadImage("image/player/guy/stand.png");

            walkLeft = new System.Windows.UIElement[4];
            walkLeft[0] = Utils.LoadImage("image/player/guy/walk-left-0.png");
            walkLeft[1] = Utils.LoadImage("image/player/guy/walk-left-1.png");
            walkLeft[2] = Utils.LoadImage("image/player/guy/walk-left-2.png");
            walkLeft[3] = Utils.LoadImage("image/player/guy/walk-left-3.png");

            walkRight = new System.Windows.UIElement[4];
            walkRight[0] = Utils.LoadImage("image/player/guy/walk-right-0.png");
            walkRight[1] = Utils.LoadImage("image/player/guy/walk-right-1.png");
            walkRight[2] = Utils.LoadImage("image/player/guy/walk-right-2.png");
            walkRight[3] = Utils.LoadImage("image/player/guy/walk-right-3.png");

            this.walkRate = new int[4] { 40, 40, 40, 40 };
        }
    }
}
