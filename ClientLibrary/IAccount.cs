namespace ClientLibrary
{
    public interface IAccount<out T>
    {
        T TopUpAccounts(int money, Client client);
    }
}
