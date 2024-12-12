using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmSimulationLibrary
{
    public class AutomatedTellerMachine
    {
        public string AtmId { get; set; }
        public string Location { get; set; }
        public decimal CashAvailable { get; private set; }

        public event TransactionHandler OnOperationPerformed;

        public AutomatedTellerMachine(string atmId, string location, decimal initialCash)
        {
            AtmId = atmId;
            Location = location;
            CashAvailable = initialCash;
        }

        public bool Authenticate(Account account, string cardNumber, string pinCode)
        {
            if (account == null)
            {
                OnOperationPerformed?.Invoke("ATM Authentication failed: Account is null.");
                return false;
            }

            bool isAuthenticated = account.Authenticate(cardNumber, pinCode);
            string message = isAuthenticated
                ? "ATM Authentication successful."
                : "ATM Authentication failed.";

            OnOperationPerformed?.Invoke(message);
            return isAuthenticated;
        }


        public void Withdraw(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                OnOperationPerformed?.Invoke("Withdrawal amount must be greater than zero.");
                return;
            }

            if (amount > CashAvailable)
            {
                OnOperationPerformed?.Invoke("ATM does not have sufficient cash.");
                return;
            }

            if (amount > account.Balance)
            {
                OnOperationPerformed?.Invoke("Insufficient account balance.");
                return;
            }

            account.Withdraw(amount);
            CashAvailable -= amount;
            OnOperationPerformed?.Invoke($"Withdrawal of {amount:C} successful. Remaining ATM cash: {CashAvailable:C}");
        }

        public void Deposit(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                OnOperationPerformed?.Invoke("Deposit amount must be greater than zero.");
                return;
            }

            account.Deposit(amount);
            CashAvailable += amount;
            OnOperationPerformed?.Invoke($"Deposit of {amount:C} successful. Total ATM cash: {CashAvailable:C}");
        }
    }

}
