using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public class BankAccount : Client
    {
        public BankAccount(string SurName, string Name, string MiddleName, string PhoneNumber, string Passport, int DepositAccount, int NonDepositAccount)
            : base(SurName, Name, MiddleName, PhoneNumber, Passport, DepositAccount, NonDepositAccount)
        {
        }
    }
}
