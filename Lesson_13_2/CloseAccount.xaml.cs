using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class CloseAccount : Window
    {
        private ObservableCollection<Client> listClients;
        Client client;
        public CloseAccount()
        {
            InitializeComponent();
            listClients = Client.GetClients();
            client = listClients[MainWindow.Id];
        }

        private void checkOne_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkOne.IsChecked)
            {
                checkTwo.IsChecked = false;
            }
        }

        private void checkTwo_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkTwo.IsChecked)
            {
                checkOne.IsChecked = false;
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            bool closing = false;
            Client.ChangedClient += Client.ChangEventHandler;
            if ((bool)checkOne.IsChecked && listClients[MainWindow.Id].DepositAccount != default)
            {
                client.DepositAccount = client.CloseAccount("депозитный");
                closing = true;
            }
            else if ((bool)checkTwo.IsChecked && listClients[MainWindow.Id].NonDepositAccount != default)
            {
                client.NonDepositAccount = client.CloseAccount("недепозитный");
                closing = true;
            }
            else
            {
                MessageBox.Show("На счету и так не было денег\nИли вы ничего не выбрали", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (closing)
            {
                Client.ChangedClient -= Client.ChangEventHandler;
                Client.SaveClients(listClients);           
                this.Close();
                MessageBox.Show("Закрытие счета произведено успешно!\nНе забудьте сохранить", "Congratilation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Client.ChangedClient -= Client.ChangEventHandler;
            ClientProfile clientProfile = new ClientProfile
            {
                DataContext = client
            };
            clientProfile.Show();
        }
    }
}
