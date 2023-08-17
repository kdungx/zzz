using zzz.ff;

namespace zzz.ff
{
    class BankAccount
    {
        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            private set
            {
                if (balance != value)
                {
                    balance = value;
                    OnBalanceChanged(value);
                }
            }
        }

        private Action<decimal> balanceChanged;

        public void SubscribeBalanceChanged(Action<decimal> handler)
        {
            balanceChanged += handler;
        }

        public void UnsubscribeBalanceChanged(Action<decimal> handler)
        {
            balanceChanged -= handler;
        }

        protected virtual void OnBalanceChanged(decimal newBalance)
        {
            balanceChanged?.Invoke(newBalance);
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount();

        Action<decimal> balanceChangedHandler = newBalance =>
        {
            Console.WriteLine($"Balance changed. New balance: {newBalance}");
        };

        account.SubscribeBalanceChanged(balanceChangedHandler);

        account.Deposit(100);
        account.Withdraw(50);

        account.UnsubscribeBalanceChanged(balanceChangedHandler);
    }
}
