using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmSimulationLibrary
{
    public class Bank
    {
        public string Name { get; set; }
        private List<Account> Accounts { get; set; }
        private List<AutomatedTellerMachine> Atms { get; set; }

        public Bank(string name)
        {
            Name = name;
            Accounts = new List<Account>();
            Atms = new List<AutomatedTellerMachine>();
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void AddAtm(AutomatedTellerMachine atm)
        {
            Atms.Add(atm);
        }

        public Account GetAccountByCardNumber(string cardNumber)
        {
            var account = Accounts.FirstOrDefault(acc => acc.CardNumber == cardNumber);
            if (account == null)
            {
                throw new ArgumentNullException(nameof(cardNumber), "Account with the provided card number does not exist.");
            }
            return account;
        }


        public AutomatedTellerMachine GetAtmById(string atmId)
        {
            var atm = Atms.FirstOrDefault(atm => atm.AtmId == atmId);
            if (atm == null)
            {
                throw new ArgumentNullException(nameof(atmId), "ATM with the provided ID does not exist.");
            }
            return atm;
        }

    }
}
