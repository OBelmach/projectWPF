using System;

namespace ClientLibrary
{
    public class Deposit : IAccount<BankAccount>, ITransfer<Client>
    {
        public BankAccount dep;
        public Deposit()
        {

        }
        public Deposit(BankAccount client)
        {
            dep = client;
        }
        public BankAccount TopUpAccounts(int money, Client client)
        {
            Client.InvokeEvent($"{DateTime.Now} пополнил депозитный счет клиенту {client.SurName} на {money}");
            dep.DepositAccount += money;
            return dep;
        }

        public void TransferMoney(Client currentClient, Client topUpClient, int money)
        {
            {
                Client.InvokeEvent($"{DateTime.Now} перевел {money} с депозитного счета {currentClient.SurName} на счет {topUpClient.SurName}");
                currentClient.DepositAccount -= money;
                topUpClient.DepositAccount += money;
            }
        }
    }
}
