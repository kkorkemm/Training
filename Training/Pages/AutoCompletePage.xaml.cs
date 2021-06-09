using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Training.Pages
{
    using Base;

    /// <summary>
    /// Логика взаимодействия для AutoCompletePage.xaml
    /// </summary>
    public partial class AutoCompletePage : Page
    {
        public AutoCompletePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При изменении значения TextBox - поиск пользователей по имени
        /// </summary>
        private void TextName_TextChanged(object sender, TextChangedEventArgs e)
        {
            /// список пользователей
            var allUsers = TrainingDBEntities.GetContext().User.ToList();

           /// проверка на заполнение
            if (!string.IsNullOrWhiteSpace(TextName.Text))
            {
                /// вывод в listview совпадений
                var users = allUsers.Where(p => p.Name.ToLower().Contains(TextName.Text.ToLower())).ToList();
                ListUsers.ItemsSource = users;
            }
            else
            {
                /// очищение listview при пустом значении поля ввода
                ListUsers.ItemsSource = null;
            }
        }

        /// <summary>
        /// При нажатии на пользователя из listview
        /// </summary>
        private void ListUsers_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(ListUsers, e.OriginalSource as DependencyObject) as ListViewItem;

            /// Автодополнение и очищение listbox
            if (item != null)
            {
                var user = item.DataContext as User;
                TextName.Text = user.Name;
                ListUsers.ItemsSource = null;
            }
        }
    }
}
