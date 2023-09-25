using System;
using System.Runtime.CompilerServices;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class OpenAccount : Window
    {
        public OpenAccount()
        {
            InitializeComponent();
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public int GetMoney()
        {
            try
            {
                if (Int32.TryParse(this.money.Text, out int money))
                {
                    return money;
                }
                else
                {
                    throw new InputDataException("Введено не корректное значение");
                }
            }
            catch (InputDataException ex)
            {
                MessageBox.Show($"Ошибка! {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0;
            }
        }
    }
}
