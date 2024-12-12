using System;

namespace AtmSimulationLibrary
{
    public delegate void TransactionHandler(string message);

    public class Account
    {
        public event TransactionHandler OnTransactionCompleted;

        public string CardNumber { get; set; }
        public string PinCode { get; set; }
        public string OwnerName { get; set; }
        public decimal Balance { get; private set; }

        public Account(string cardNumber, string pinCode, string ownerName, decimal initialBalance)
        {
            CardNumber = cardNumber;
            PinCode = pinCode;
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public bool Authenticate(string cardNumber, string pinCode)
        {
            if (CardNumber == cardNumber && PinCode == pinCode)
            {
                OnTransactionCompleted?.Invoke("Authentication successful.");
                return true;
            }
            else
            {
                OnTransactionCompleted?.Invoke("Authentication failed.");
                return false;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                OnTransactionCompleted?.Invoke("Deposit amount must be greater than zero.");
                return;
            }

            Balance += amount;
            OnTransactionCompleted?.Invoke($"Deposit successful. New balance: {Balance:C}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                OnTransactionCompleted?.Invoke("Withdrawal amount must be greater than zero.");
                return;
            }

            if (amount > Balance)
            {
                OnTransactionCompleted?.Invoke("Insufficient funds.");
                return;
            }

            Balance -= amount;
            OnTransactionCompleted?.Invoke($"Withdrawal successful. New balance: {Balance:C}");
        }

        public void Transfer(Account recipient, decimal amount)
        {
            if (amount <= 0)
            {
                OnTransactionCompleted?.Invoke("Transfer amount must be greater than zero.");
                return;
            }

            if (amount > Balance)
            {
                OnTransactionCompleted?.Invoke("Insufficient funds for transfer.");
                return;
            }

            Balance -= amount;
            recipient.Deposit(amount);
            OnTransactionCompleted?.Invoke($"Transfer successful. New balance: {Balance:C}");
        }
    }
}
