namespace sd_fight
{
    static class Utils
    {
        public static System.Windows.Controls.Image LoadImage(string uri)
        {
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Source = LoadImageSource(uri);
            return img;
        }

        public static System.Windows.Media.Imaging.BitmapImage LoadImageSource(string uri)
        {
            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
            bi.BeginInit();
            bi.StreamSource = GetStreamFromFile(uri);
            bi.EndInit();
            return bi;
        }

        public static System.IO.Stream GetStreamFromFile(string uri)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(uri, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] buf = new byte[fs.Length];
                fs.Read(buf, 0, (int)fs.Length);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                return ms;
            }
            catch (System.Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                System.Environment.Exit(0);
            }
            return null;
        }

        public static void WriteLog(string log)
        {
            try
            {
                // TODO: write log file
            }
            catch
            {
                // pass
            }
        }
    }
}
