namespace ClientLibrary
{
    public interface ITransfer<in T>
        where T : Client
    {
        void TransferMoney(T currentClient, T topUpClient, int money);
    }
}
