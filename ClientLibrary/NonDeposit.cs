using System;

namespace ClientLibrary
{
    public class NonDeposit : IAccount<BankAccount>, ITransfer<Client>
    {
        private BankAccount nondep;
        public NonDeposit()
        {

        }
        public NonDeposit(BankAccount client)
        {
            nondep = client;
        }
        public BankAccount TopUpAccounts(int money, Client client)
        {
            Client.InvokeEvent($"{DateTime.Now} пополнил недепозитный счет клиенту {client.SurName} на {money}");
            nondep.NonDepositAccount += money;
            return nondep;
        }

        public void TransferMoney(Client currentClient, Client topUpClient, int money)
        {
            {
                Client.InvokeEvent($"{DateTime.Now} перевел {money} с недепозитного счета {currentClient.SurName} на счет {topUpClient.SurName}");
                currentClient.NonDepositAccount -= money;
                topUpClient.NonDepositAccount += money;
            }
        }
    }
}
