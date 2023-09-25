using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class TopUpAccount : Window
    {
        private ObservableCollection<Client> listClients;
        Client client;
        public TopUpAccount()
        {
            InitializeComponent();
            listClients = Client.GetClients();
            client = listClients[MainWindow.Id];
        }
        /// <summary>
        /// Выбор депозитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDeposit_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)(checkDeposit.IsChecked = true))
            {
                checkNonDeposit.IsChecked = false;
                textDeposit.IsReadOnly = false;
                textNonDeposit.IsReadOnly = true;
                textNonDeposit.Clear();
            }
        }
        /// <summary>
        /// Выбор недепозитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkNonDeposit_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)(checkNonDeposit.IsChecked = true))
            {
                checkDeposit.IsChecked = false;
                textNonDeposit.IsReadOnly = false;
                textDeposit.IsReadOnly = true;
                textDeposit.Clear();
            }
        }
        /// <summary>
        /// Пополнение счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_TopUpAccount(object sender, RoutedEventArgs e)
        {            
            BankAccount bankAccount = new BankAccount(client.SurName, client.Name, client.MiddleName, client.PhoneNumber, client.Passport, client.DepositAccount, client.NonDepositAccount);
            IAccount<Client> Account;
            if ((bool)checkDeposit.IsChecked && client.DepositAccount != 0 || (bool)checkNonDeposit.IsChecked && client.NonDepositAccount != 0)
            {                
                Client.ChangedClient += Client.ChangEventHandler;
                if ((bool)checkDeposit.IsChecked && Int32.TryParse(textDeposit.Text, out int moneyDeposit))
                {
                    Account = new Deposit(bankAccount);
                    client = Account.TopUpAccounts(moneyDeposit, client);
                    showDepositAccount.Text = client.DepositAccount.ToString();
                    Client.ChangedClient -= Client.ChangEventHandler;                    
                }
                else if ((bool)checkNonDeposit.IsChecked && Int32.TryParse(textNonDeposit.Text, out int moneyNonDeposit))
                {
                    Account = new NonDeposit(bankAccount);                    
                    client = Account.TopUpAccounts(moneyNonDeposit, client);
                    showNonDepositAccount.Text = client.NonDepositAccount.ToString();
                    Client.ChangedClient -= Client.ChangEventHandler;                  
                }
                else
                {
                    MessageBox.Show("Не верный ввод");
                }
            }
            else
            {
                MessageBox.Show("Счет пуст или вы не выбрали счет");
            }
        }
        /// <summary>
        /// Подтверждение пополнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Client.SaveClients(listClients);
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            ClientProfile clientProfile = new ClientProfile
            {
                DataContext = client
            };
            clientProfile.Show();
        }
    }
}
