using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class ClientProfile : Window
    {
        ObservableCollection<Client> listClients;
        private OpenAccount account { get; set; }
        public ClientProfile()
        {
            InitializeComponent();
            listClients = Client.GetClients();
        }
        /// <summary>
        /// Открытие счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_OpenAccount(object sender, RoutedEventArgs e)
        {
            if (listClients[MainWindow.Id].DepositAccount == default || listClients[MainWindow.Id].NonDepositAccount == default)
            {
                account = new OpenAccount();
                account.Show();
                account.Closed += WindowAddAccount_Closed;
            }
            else
            {
                MessageBox.Show("Все счета открыты", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void WindowAddAccount_Closed(object sender, EventArgs e)
        {
            Client.ChangedClient += Client.ChangEventHandler;
            int money = account.GetMoney();
            Client selectedclient = listClients[MainWindow.Id];
            if (selectedclient.DepositAccount == default)
            {
                selectedclient.DepositAccount = selectedclient.OpenAccount(money, "депозитный");
            }
            else if (selectedclient.NonDepositAccount == default)
            {
                selectedclient.NonDepositAccount = selectedclient.OpenAccount(money, "недепозитный");
            }
            else
            {
                MessageBox.Show("Все счета открыты");
            }
            Client.ChangedClient -= Client.ChangEventHandler;
            depositAccount.Text = selectedclient.DepositAccount.ToString();
            nonDepositAccount.Text = selectedclient.NonDepositAccount.ToString();
            Client.SaveClients(listClients);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }


        /// <summary>
        /// Сохранение измененного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            string newSurName = surName.Text;
            string newName = name.Text;
            string newMiddleName = middleName.Text;
            string newPhoneNumber = phoneNumber.Text;
            string newPassport = passport.Text;
            string tempOne = depositAccount.Text;
            int newAccountOne = Convert.ToInt32(tempOne);
            string tempTwo = nonDepositAccount.Text;
            int newAccountTwo = Convert.ToInt32(tempTwo);
            Client client = new Client(newSurName, newName, newMiddleName, newPhoneNumber, newPassport, newAccountOne, newAccountTwo);
            listClients[MainWindow.Id] = client;
            Client.SaveClients(listClients);
            MessageBox.Show("Сохранение прошло успешно", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Открытие окназакрытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_CloseAccount(object sender, RoutedEventArgs e)
        {
            CloseAccount close = new CloseAccount();
            close.DataContext = listClients[MainWindow.Id];
            close.Show();
            this.Hide();
        }

        /// <summary>
        /// Открытие окна перевода средств
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Transfer(object sender, RoutedEventArgs e)
        {
            TransferMoney transfer = new TransferMoney();
            transfer.showAccountOne.Text = listClients[MainWindow.Id].DepositAccount.ToString();
            transfer.showAccountTwo.Text = listClients[MainWindow.Id].NonDepositAccount.ToString();
            transfer.Show();
            this.Hide();
        }
        /// <summary>
        /// Открытие окна пополнения счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_TopUpAccount(object sender, RoutedEventArgs e)
        {
            TopUpAccount topUpAccount = new TopUpAccount();
            topUpAccount.showDepositAccount.Text = listClients[MainWindow.Id].DepositAccount.ToString();
            topUpAccount.showNonDepositAccount.Text = listClients[MainWindow.Id].NonDepositAccount.ToString();
            topUpAccount.Show();
            this.Hide();
        }
        /// <summary>
        /// Открытие окна перевода средств другому клиенту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_TransferMoney(object sender, RoutedEventArgs e)
        {
            TransferBetweenClients transferBetweenClients = new TransferBetweenClients();
            transferBetweenClients.comboBoxList.ItemsSource = listClients;
            transferBetweenClients.comboBoxList.DisplayMemberPath = "SurName";
            transferBetweenClients.DataContext = listClients[MainWindow.Id];
            transferBetweenClients.Show();
            this.Hide();
        }
    }
}
