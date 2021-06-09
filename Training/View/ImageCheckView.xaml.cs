using System.Windows.Controls;

namespace Training.View
{
    using Pages;

    /// <summary>
    /// Логика взаимодействия для ImageCheckView.xaml
    /// </summary>
    public partial class ImageCheckView : UserControl
    {
        public ImageCheckView()
        {
            InitializeComponent();
            ImageCheckFrame.Navigate(new ImageCheckPage());
            Navigation.SubFrame = ImageCheckFrame;
        }
    }
}
