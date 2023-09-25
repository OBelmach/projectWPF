using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ClientLibrary
{
    public class Client : INotifyPropertyChanged
    {
        public static event Action<string> ChangedClient;
        private string name;
        private string surName;
        private string middleName;
        private string phoneNumber;
        private string passport;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    ChangedClient += ChangEventHandler;
                    InvokeEvent($"{DateTime.Now} изменил {name} на {value}");
                    name = value;
                    OnPropertyChanged("Name");
                    ChangedClient -= ChangEventHandler;
                }
            }
        }
        public string SurName
        {
            get
            {
                return surName;
            }
            set
            {
                if (surName != value)
                {
                    ChangedClient += ChangEventHandler;
                    InvokeEvent($"{DateTime.Now} изменил {surName} на {value}");
                    surName = value;
                    OnPropertyChanged("SurName");
                    ChangedClient -= ChangEventHandler;
                }
            }
        }
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (middleName != value)
                {
                    ChangedClient += ChangEventHandler;
                    InvokeEvent($"{DateTime.Now} изменил {middleName} на {value}");
                    middleName = value;
                    OnPropertyChanged("MiddleName");
                    ChangedClient -= ChangEventHandler;
                }
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (phoneNumber != value)
                {
                    ChangedClient += ChangEventHandler;
                    InvokeEvent($"{DateTime.Now} изменил {phoneNumber} на {value}");
                    phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                    ChangedClient -= ChangEventHandler;
                }
            }
        }
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                if (passport != value)
                {
                    ChangedClient += ChangEventHandler;
                    InvokeEvent($"{DateTime.Now} изменил {passport} на {value}");
                    passport = value;
                    OnPropertyChanged("Passport");
                    ChangedClient -= ChangEventHandler;
                }
            }
        }
        private int depositAccount;
        private int nonDepositAccount;
        public int DepositAccount { get { return depositAccount; } set { depositAccount = value; OnPropertyChanged("DepositAccount"); } }
        public int NonDepositAccount { get { return nonDepositAccount; } set { nonDepositAccount = value; OnPropertyChanged("NonDepositAccount"); } }
        public Client(string SurName, string Name, string MiddleName, string PhoneNumber, string Passport, int DepositAccount, int NonDepositAccount)
        {
            surName = SurName;
            name = Name;
            middleName = MiddleName;
            phoneNumber = PhoneNumber;
            passport = Passport;
            depositAccount = DepositAccount;
            nonDepositAccount = NonDepositAccount;
        }
        public int OpenAccount(int money, string msg)
        {
            InvokeEvent($"{DateTime.Now} открыл {msg} счет клиенту {surName} на сумму: {money}");
            return money;
        }
        public int CloseAccount(string msg)
        {
            InvokeEvent($"{DateTime.Now} закрыл {msg} счет клиенту {surName}");
            return default(int);
        }
        public static void InvokeEvent(string msg)
        {
            ChangedClient?.Invoke(msg);
        }
        public static ObservableCollection<Client> GetClients()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            string json = File.ReadAllText("clients.json");
            clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(json);
            return clients;
        }
        public static void SaveClients(ObservableCollection<Client> clients)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText("clients.json", json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public static void ChangEventHandler(string msg)
        {
            string actionlog = $"# Manager {msg}";           
            File.AppendAllText("actionLog.json", actionlog);
        }
    }
}

