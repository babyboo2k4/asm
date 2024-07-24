namespace SpringHeroBank;

public class Transaction
{
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string ReceiverAccount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string accountNumber, string type, decimal amount, string receiverAccount = null)
        {
            AccountNumber = accountNumber;
            Type = type;
            Amount = amount;
            ReceiverAccount = receiverAccount;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Date}: {Type} - {Amount:C}" + (ReceiverAccount != null ? $" đến {ReceiverAccount}" : "");
        }
    }
