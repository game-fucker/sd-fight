namespace sd_fight
{
    class KeyListener
    {
        private const int KeySpace = 1024; // 浪费点空间

        private const System.Windows.Input.Key Up = System.Windows.Input.Key.W;
        private const System.Windows.Input.Key Down = System.Windows.Input.Key.S;
        private const System.Windows.Input.Key Left = System.Windows.Input.Key.A;
        private const System.Windows.Input.Key Right = System.Windows.Input.Key.D;
        private const System.Windows.Input.Key Attack = System.Windows.Input.Key.J;
        private const System.Windows.Input.Key Jump = System.Windows.Input.Key.K;
        private const System.Windows.Input.Key RepeatAttack = System.Windows.Input.Key.U;
        private const System.Windows.Input.Key RepeatJump = System.Windows.Input.Key.I;

        public bool IsPressingUp { get; set; }
        public bool IsPressingDown { get; set; }
        public bool IsPressingLeft { get; set; }
        public bool IsPressingRight { get; set; }
        public bool IsPressingAttack { get; set; }
        public bool IsPressingRepeatAttack { get; set; }
        public bool IsPressingJump { get; set; }
        public bool IsPressingRepeatJump { get; set; }

        private System.Func<bool>[] onKeyDown;
        private System.Func<bool>[] onKeyUp;

        public KeyListener()
        {
            onKeyDown = new System.Func<bool>[KeySpace];
            onKeyDown[(int)Up] = () => { IsPressingUp = true; return true; };
            onKeyDown[(int)Down] = () => { IsPressingDown = true; return true; };
            onKeyDown[(int)Left] = () => { IsPressingLeft = true; return true; };
            onKeyDown[(int)Right] = () => { IsPressingRight = true; return true; };
            onKeyDown[(int)Attack] = () => { IsPressingAttack = true; return true; };
            onKeyDown[(int)RepeatAttack] = () => { IsPressingRepeatAttack = true; return true; };
            onKeyDown[(int)Jump] = () => { IsPressingJump = true; return true; };
            onKeyDown[(int)RepeatJump] = () => { IsPressingRepeatJump = true; return true; };
            
            onKeyUp = new System.Func<bool>[KeySpace];
            onKeyUp[(int)Up] = () => { IsPressingUp = false; return false; };
            onKeyUp[(int)Down] = () => { IsPressingDown = false; return false; };
            onKeyUp[(int)Left] = () => { IsPressingLeft = false; return false; };
            onKeyUp[(int)Right] = () => { IsPressingRight = false; return false; };
            onKeyUp[(int)Attack] = () => { IsPressingAttack = false; return false; };
            onKeyUp[(int)RepeatAttack] = () => { IsPressingRepeatAttack = false; return false; };
            onKeyUp[(int)Jump] = () => { IsPressingJump = false; return false; };
            onKeyUp[(int)RepeatJump] = () => { IsPressingRepeatJump = false; return false; };
        }

        private void OnKeyEvent(System.Windows.Input.Key key, System.Func<bool>[] binding)
        {
            if (System.Windows.Input.Key.A <= key && key <= System.Windows.Input.Key.Z)
            {
                binding[(int)key]?.Invoke();
            }
        }

        public void OnKeyDown(System.Windows.Input.Key key)
        {
            OnKeyEvent(key, onKeyDown);
        }

        public void OnKeyUp(System.Windows.Input.Key key)
        {
            OnKeyEvent(key, onKeyUp);
        }
    }
}
