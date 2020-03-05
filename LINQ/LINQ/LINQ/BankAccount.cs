﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class BankAccountEventArgs : EventArgs
    {
        private double transactionAmount;
        private DateTime timestamp = DateTime.Now;

        public BankAccountEventArgs(double transactionAmount) => this.transactionAmount = transactionAmount;

        public override string ToString() => $"Transaction amount {transactionAmount}, timestamp {timestamp}";
    }

    public class BankAccount
    {
        // Fields.
        private List<double> transactions = new List<double>();

        // Properties.
        public string AccountHolder { get; set; }
        public double Balance { get; set; } = 0;

        // Events.
        public event EventHandler<BankAccountEventArgs> ProtectionLimitExceeded;
        public event EventHandler<BankAccountEventArgs> Overdrawn;

        // Constructor.
        public BankAccount(string accountHolder = "", double balance = 0)
        {
            this.AccountHolder = accountHolder;
            this.Balance = balance;
        }

        // Methods.
        public void Deposit(double amount)
        {

            // Deposit money, and store transaction amount.
            Balance += amount;
            transactions.Add(amount);

            // If balance has exceeded the government's protection limit, raise a ProtectionLimitExceeded event.
            if (Balance >= 50000 && ProtectionLimitExceeded != null)
            {
                ProtectionLimitExceeded(this, new BankAccountEventArgs(amount));
            }
        }

        public void Withdraw(double amount)
        {

            // Withdraw money, and store transaction amount as a negative amount (to denote a withdrawal). 
            Balance -= amount;
            transactions.Add(-amount);

            // If account is now negative, raise an Overdrawn event.
            if (Balance < 0 && Overdrawn != null)
            {
                Overdrawn(this, new BankAccountEventArgs(amount));
            }
        }

        // Return a read-only wrapper for the transaction list. Prevents client app from meddling...
        public ReadOnlyCollection<double> Transactions => transactions.AsReadOnly(); 

        public override string ToString() => $"Account {AccountHolder}, balance {Balance:C}";
    }
}
