using System;
using System.Collections.ObjectModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class TransferBetweenClients : Window
    {
        private ObservableCollection<Client> listClients;        
        public TransferBetweenClients()
        {
            InitializeComponent();
            listClients = Client.GetClients();           
        }
        private void CheckBox_Checked_Deposit(object sender, RoutedEventArgs e)
        {
            if (checkBoxDeposit.IsChecked == true)
            {
                checkBoxNonDeposit.IsChecked = false;
            }
        }
        private void CheckBox_Checked_NonDeposit(object sender, RoutedEventArgs e)
        {
            if (checkBoxNonDeposit.IsChecked == true)
            {
                checkBoxDeposit.IsChecked = false;
            }
        }
        private void Button_Click_Transfer(object sender, RoutedEventArgs e)
        {
            Client client = listClients[MainWindow.Id];
            Client topUpClient = comboBoxList.SelectedItem as Client;
            ITransfer<BankAccount> transfer;
            if (topUpClient != null && MainWindow.Id != comboBoxList.SelectedIndex && ((bool)checkBoxDeposit.IsChecked || (bool)checkBoxNonDeposit.IsChecked) && Int32.TryParse(money.Text, out int moneyTransfer))
            {
                Client.ChangedClient += Client.ChangEventHandler;
                BankAccount currentClient = new BankAccount
                    (client.SurName, client.Name, client.MiddleName,
                    client.PhoneNumber, client.Passport, client.DepositAccount,
                    client.NonDepositAccount);
                BankAccount topUpBankClient = new BankAccount
                    (topUpClient.SurName, topUpClient.Name, topUpClient.MiddleName,
                    topUpClient.PhoneNumber, topUpClient.Passport, topUpClient.DepositAccount,
                    topUpClient.NonDepositAccount);
                if (checkBoxDeposit.IsChecked == true && currentClient.DepositAccount != 0 && topUpBankClient.DepositAccount != 0)
                {
                    transfer = new Deposit();
                    transfer.TransferMoney(currentClient, topUpBankClient, moneyTransfer);
                    listClients[MainWindow.Id] = currentClient;
                    listClients[comboBoxList.SelectedIndex] = topUpBankClient;
                    Client.SaveClients(listClients);
                    MessageBox.Show("Операция проведена успешна", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    Client.ChangedClient -= Client.ChangEventHandler;
                    ClientProfile profile = new ClientProfile();
                    profile.DataContext = currentClient;
                    profile.Show();
                    this.Close();
                }
                else if (checkBoxNonDeposit.IsChecked == true && currentClient.NonDepositAccount != 0 && topUpBankClient.NonDepositAccount != 0)
                {
                    transfer = new NonDeposit();
                    transfer.TransferMoney(currentClient, topUpBankClient, moneyTransfer);
                    listClients[MainWindow.Id] = currentClient;
                    listClients[comboBoxList.SelectedIndex] = topUpBankClient;
                    Client.SaveClients(listClients);
                    MessageBox.Show("Операция проведена успешна", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                    Client.ChangedClient -= Client.ChangEventHandler;
                    ClientProfile profile = new ClientProfile();
                    profile.DataContext = currentClient;
                    profile.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Счета должны быть открыты");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть выбраны и заполнены корректно");
            }
        }
        private void comboBoxList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Client client = comboBoxList.SelectedItem as Client;
            nameSelectedClient.Text = client.SurName;
            depositSelectedClient.Text = client.DepositAccount.ToString();
            nonDepositSelectedClient.Text = client.NonDepositAccount.ToString();
        }
    }
}
