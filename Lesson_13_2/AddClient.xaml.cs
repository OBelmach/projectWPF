using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class AddClient : Window
    {
        ObservableCollection<Client> listClients;
        public AddClient()
        {
            InitializeComponent();
            listClients = Client.GetClients();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surName = _surName.Text;
                string name = _name.Text;
                string middleName = _middleName.Text;
                string phone = _phone.Text;
                string passport = _passport.Text;
                if (string.IsNullOrEmpty(surName) || surName.Length < 3)
                {
                    _surName.ToolTip = "Все поля должны быть заполнены!";
                    _surName.Background = Brushes.LightGray;
                    throw new InputDataException("Входное значение меньше 3 символов");
                }
                else if (string.IsNullOrEmpty(name) || name.Length < 3)
                {
                    _name.ToolTip = "Все поля должны быть заполнены!";
                    _name.Background = Brushes.LightGray;
                    throw new InputDataException("Входное значение меньше 3 символов");
                }
                else if (string.IsNullOrEmpty(middleName) || middleName.Length < 3)
                {
                    _middleName.ToolTip = "Все поля должны быть заполнены!";
                    _middleName.Background = Brushes.LightGray;
                    throw new InputDataException("Входное значение меньше 3 символов");
                }
                else if (string.IsNullOrEmpty(phone) || phone.Length < 7)
                {
                    _phone.ToolTip = "Все поля должны быть заполнены!";
                    _phone.Background = Brushes.LightGray;
                    throw new InputDataException("Входное значение меньше 7 символов");
                }
                else if (string.IsNullOrEmpty(passport) || passport.Length < 9)
                {
                    _passport.ToolTip = "Все поля должны быть заполнены!";
                    _passport.Background = Brushes.LightGray;
                    throw new InputDataException("Паспортные данные меньше 9 символов");
                }
                else
                {
                    Client newClient = new Client(_surName.Text, _name.Text, _middleName.Text, _phone.Text, _passport.Text, 0, 0);
                    Client.ChangedClient += Client.ChangEventHandler;
                    Client.InvokeEvent($"{DateTime.Now} добавил клиента {newClient.SurName}");
                    listClients.Add(newClient);
                    string json = JsonConvert.SerializeObject(listClients);
                    File.WriteAllText("clients.json", json);
                    MessageBox.Show("Добавление прошло успешно", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    Client.ChangedClient -= Client.ChangEventHandler;
                    this.Close();
                }
            }
            catch (InputDataException ex)
            {
                MessageBox.Show($"Ошибка! {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
