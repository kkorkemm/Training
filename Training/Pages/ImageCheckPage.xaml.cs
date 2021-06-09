using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MessageBoxes;
using System.Windows.Media;

namespace Training.Pages
{
    /// <summary>
    /// Логика взаимодействия для ImageCheckPage.xaml
    /// </summary>
    public partial class ImageCheckPage : Page
    {
        public ImageCheckPage()
        {
            InitializeComponent();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            /// Фильтр: только изображения
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.png) | *.jpg; *.jpeg; *.png"
            };

            if (fileDialog.ShowDialog() == true)
            {
                /// чтение изображения
                byte[] imageData;
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                }
                BitmapImage image = new BitmapImage(new Uri(fileDialog.FileName));

                /// Проверка на выполнение условий
                StringBuilder errors = new StringBuilder();

                /// размер изображения не превышает 2 гб
                int byteLength = 1024 * 1024 * 1024;
                if (imageData.Length > byteLength)
                    errors.AppendLine("Размер изображения не должно превышать 2ГБ");

                /// изображение вертикальное
                if (image.Height < image.Width)
                    errors.AppendLine("Изображение должно быть вертикальным");

                /// соотношение сторон - 3 на 4
                double size = Math.Round(image.Width / image.Height, 2);
                double neededSize = 3.0 / 4.0;
                if (size != neededSize)
                    errors.AppendLine("Соотношение сторон должно быть 3 на 4");

                if (errors.Length > 0)
                {
                    MyMessageBox.Error(errors.ToString());
                    return;
                }
                else
                {
                    /// отображение изображения на форме
                    Img.Source = image;
                }
            }

        }

        private void BtnRotate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var img = new TransformedBitmap((BitmapSource)Img.Source, new RotateTransform(180));

                Img.Source = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
