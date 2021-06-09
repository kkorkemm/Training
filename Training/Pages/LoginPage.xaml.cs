using System;
using System.IO;
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
using MessageBoxes;

namespace Training.Pages
{
    using Base;

    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            /// Если пользователь заблокирован, но заново запустил приложение
            if (Properties.Settings.Default.TryCount > 3)
            {
                if (DateTime.Now.Minute - Properties.Settings.Default.TryTime.Minute <= 1 && DateTime.Now.Minute - Properties.Settings.Default.TryTime.Minute >= 0)
                {
                    Block();
                }
                else
                {
                    Reset();
                }
            }
        }

        /// <summary>
        /// Сброс настроек приложения
        /// </summary>
        void Reset()
        {
            Properties.Settings.Default.TryCount = 0;
            Properties.Settings.Default.TryTime = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Блокировка (выход из приложения)
        /// </summary>
        void Block()
        {
            Properties.Settings.Default.Save();
            MyMessageBox.Error("Вход заблокирован на 1 минуту");
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            /// Проверка на заполнение полей
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TextLogin.Text))
                errors.AppendLine("Введите логин");

            if (string.IsNullOrWhiteSpace(TextPass.Password))
                errors.AppendLine("Введите пароль");

            if (errors.Length > 0)
            {
                /// Изменение настроек приложения при неправильной попытке
                Properties.Settings.Default.TryCount++;

                if (Properties.Settings.Default.TryCount > 3)
                {
                    Properties.Settings.Default.TryTime = DateTime.Now;
                    Block();
                }

                Properties.Settings.Default.Save();

                MyMessageBox.Error(errors.ToString());
                return;
            }
            else
            {
                /// поиск пользователя
                var user = TrainingDBEntities.GetContext().User.Where(p => p.Login == TextLogin.Text && p.Password == TextPass.Password).FirstOrDefault();

                /// пользователь не найден
                if (user == null)
                {
                    /// так же сохранение настроек при попытке зайти через несуществующего пользователя
                    Properties.Settings.Default.TryCount++;

                    if (Properties.Settings.Default.TryCount > 3)
                    {
                        Properties.Settings.Default.TryTime = DateTime.Now;

                        Block();
                    }

                    Properties.Settings.Default.Save();

                    MyMessageBox.Warning("Такого пользователя не существует в базе");
                    return;
                }
                else
                {
                    /// при успешной авторизации: сброс настроек и сохранение текущего пользователя

                    Reset();

                    TrainingDBEntities.currentUser = user;

                    /// при checked Запомнить меня
                    if (CheckRemember.IsChecked == true)
                    {
                        string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Training";

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        using (StreamWriter sw = new StreamWriter(folder + @"\login.txt", false, Encoding.Default))
                        {
                            sw.WriteLine(user.Login);
                            sw.Close();
                        }

                        Navigation.MainFrame.Navigate(new MainPage());
                    }
                }
            }
        }
    }
}
