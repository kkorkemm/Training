using System.Windows.Controls;

namespace Training.View
{
    using Pages;

    /// <summary>
    /// Логика взаимодействия для AutoCompleteView.xaml
    /// </summary>
    public partial class AutoCompleteView : UserControl
    {
        public AutoCompleteView()
        {
            InitializeComponent();
            AutoCompleteFrame.Navigate(new AutoCompletePage());
            Navigation.SubFrame = AutoCompleteFrame;
        }
    }
}
