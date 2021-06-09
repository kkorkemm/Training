using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Training
{
    using Base;
    using Pages;

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /// Проверка на то, запомнен ли пользователь
            string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Training\login.txt";

            if (File.Exists(file))
            {
                string login;

                using (StreamReader sr = new StreamReader(file))
                {
                    login = sr.ReadLine();
                    sr.Close();
                }

                var user = TrainingDBEntities.GetContext().User.Where(p => p.Login == login).FirstOrDefault();
                
                if (user != null)
                {
                    TrainingDBEntities.currentUser = user;
                    MainFrame.Navigate(new MainPage());
                }
                else
                {
                    MainFrame.Navigate(new LoginPage());
                }
            }
            else
            {
                MainFrame.Navigate(new LoginPage());
            }

            Navigation.MainFrame = MainFrame;
        }
    }
}