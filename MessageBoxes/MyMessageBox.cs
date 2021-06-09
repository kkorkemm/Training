using System.Windows;

namespace MessageBoxes
{
    /// <summary>
    /// Класс для вызова MessageBox
    /// </summary>
    public class MyMessageBox
    {
        /// <summary>
        /// Вызов MessageBox при ошибке
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public static void Error(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Вызов MessageBox с вопросом пользователю
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public static void Question(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        /// <summary>
        /// Вызов MessageBox с информацией
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public static void Information(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Вызов MessageBox с предупреждением
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public static void Warning(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
