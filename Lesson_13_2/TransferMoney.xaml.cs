using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ClientLibrary;

namespace Lesson_13_2
{
    public partial class TransferMoney : Window
    {
        private ObservableCollection<Client> listClients;
        Client client;
        public TransferMoney()
        {
            InitializeComponent();
            listClients = Client.GetClients();
            client = listClients[MainWindow.Id];
        }
        /// <summary>
        /// Выбор первого счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOne_Checked(object sender, RoutedEventArgs e)
        {
            if (checkOne.IsChecked == true)
            {
                checkTwo.IsChecked = false;
                textOne.IsReadOnly = false;
                textTwo.IsReadOnly = true;
                textTwo.Clear();
            }
        }
        /// <summary>
        /// Выбор второго счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTwo_Checked(object sender, RoutedEventArgs e)
        {
            if (checkTwo.IsChecked == true)
            {
                checkOne.IsChecked = false;
                textTwo.IsReadOnly = false;
                textOne.IsReadOnly = true;
                textOne.Clear();
            }
        }
        /// <summary>
        /// Перевод средств с одного счета на другой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_TransferMoney(object sender, RoutedEventArgs e)
        {
            if ((bool)checkOne.IsChecked && client.DepositAccount != 0 || (bool)checkTwo.IsChecked && client.NonDepositAccount != 0)
            {
                if (client.DepositAccount != 0 && client.NonDepositAccount != 0)
                {
                    if ((bool)checkOne.IsChecked && Int32.TryParse(textOne.Text, out int num1) && num1 <= client.DepositAccount)
                    {
                        Client.ChangedClient += Client.ChangEventHandler;
                        Client.InvokeEvent($"{DateTime.Now} перевел с депозитного счета на недепозитный {num1} клиенту {client.SurName}");
                        client.DepositAccount -= num1;
                        showAccountOne.Text = client.DepositAccount.ToString();
                        client.NonDepositAccount += num1;
                        showAccountTwo.Text = client.NonDepositAccount.ToString();
                        Client.ChangedClient -= Client.ChangEventHandler;
                    }
                    else if ((bool)checkTwo.IsChecked && Int32.TryParse(textTwo.Text, out int num2) && num2 <= client.NonDepositAccount)
                    {
                        Client.ChangedClient += Client.ChangEventHandler;
                        Client.InvokeEvent($"{DateTime.Now} перевел с недепозитного счета на депозитный {num2} клиенту {client.SurName}");
                        client.NonDepositAccount -= num2;
                        showAccountTwo.Text = client.NonDepositAccount.ToString();
                        client.DepositAccount += num2;
                        showAccountOne.Text = client.DepositAccount.ToString();
                        Client.ChangedClient -= Client.ChangEventHandler;
                    }
                    else
                    {
                        MessageBox.Show("Хотите больше чем есть или не верный ввод");
                    }
                }
                else
                {
                    MessageBox.Show("Оба счета должны быть открыты");
                }
            }
            else
            {
                MessageBox.Show("Счет пуст или вы не выбрали счет");
            }
        }
        /// <summary>
        /// Подтверждение перевода средств
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
