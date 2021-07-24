using System.Collections.Generic;

namespace Nordigen.DataTypes.Accounts
{
   
    public class DebtorAccount
    {
        public string iban { get; set; }
    }

    public class TransactionAmount
    {
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class Booked
    {
        public string transactionId { get; set; }
        public string debtorName { get; set; }
        public DebtorAccount debtorAccount { get; set; }
        public TransactionAmount transactionAmount { get; set; }
        public string bankTransactionCode { get; set; }
        public string bookingDate { get; set; }
        public string valueDate { get; set; }
        public string remittanceInformationUnstructured { get; set; }
    }

    public class Pending
    {
        public TransactionAmount transactionAmount { get; set; }
        public string valueDate { get; set; }
        public string remittanceInformationUnstructured { get; set; }
    }

    public class Transactions
    {
        public List<Booked> booked { get; set; }
        public List<Pending> pending { get; set; }
    }

    public class AccountsTransactions
    {
        public Transactions transactions { get; set; }
    }
}