using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
namespace WorkingWihImages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                if (CheckIfUrlIsImage(openFileDialog.FileName))
                {
                    ImageToWork.Source = null;
                    _bitmaps.Clear();
                    var bitmap = new Bitmap(openFileDialog.FileName);
                    MessageBox.Show(bitmap.Height.ToString());
                    MessageBox.Show(bitmap.Width.ToString());
                    RunProcesing(bitmap);
                }
                else
                    MessageBox.Show("Please open image");
            }
        }
        private void RunProcesing(Bitmap bitmap)
        {

        }
        private List<Pixel> GetPixels(Bitmap bitmap)
        {
            var pixels = new List<Pixel>((bitmap.Height * bitmap.Width));
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixels.Add(new Pixel()
                    {
                        Color = bitmap.GetPixel(x, y),
                        Point = new System.Drawing.Point() { X = x, Y = y }
                    });
                }
            }



            return pixels;
        }    
        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            int value = (int)ScrollBarForPercintage.Value;
            Percentage.Text = $"{value.ToString()} %";
            if (_bitmaps == null || _bitmaps.Count == 0)
                return;

            ImageToWork.Source = _bitmaps[value - 1];
        }
        private bool CheckIfUrlIsImage(string fileName)
        {
            int indexOfDot = fileName.LastIndexOf(".");
            string ext = (fileName.Substring(indexOfDot, fileName.Length - indexOfDot)).ToLower();
            switch (ext)
            {
                case ".png":
                case ".bmp":
                case ".jpg":
                    return true;
            }
            return false;
        }
    }
}
