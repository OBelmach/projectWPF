using System;
using System.Collections.ObjectModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Client> listClints;
        public static int Id { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            listClints = Client.GetClients();
            clients.ItemsSource = listClints;
        }
        private void Changed_Click(object sender, RoutedEventArgs e)
        {
            Id = clients.SelectedIndex;
            if (clients.SelectedItem != null)
            {
                ClientProfile profile = new ClientProfile();
                profile.DataContext = clients.SelectedItem;
                profile.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите клиента!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Id = clients.SelectedIndex;
            if (clients.SelectedItem != null)
            {
                Client.ChangedClient += Client.ChangEventHandler;
                Client.InvokeEvent($"{DateTime.Now} удалил клиента {listClints[Id].SurName}");
                listClints.RemoveAt(Id);
                Client.SaveClients(listClints);
                Client.ChangedClient -= Client.ChangEventHandler;
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Client.SaveClients(listClints);
            MessageBox.Show("Сохранение прошло успешно", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.Show();
            this.Close();
        }

        private void ActionLog_Click(object sender, RoutedEventArgs e)
        {
            ActionLog actionLog = new ActionLog();
            actionLog.Show();
            this.Close();
        }
    }
}

