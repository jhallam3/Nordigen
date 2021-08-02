using System.Collections.Generic;

namespace Nordigen.DataTypes.Accounts
{
    
    public class BalanceAmount
    {
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class Balance
    {
        public BalanceAmount balanceAmount { get; set; }
        public string balanceType { get; set; }
        public string referenceDate { get; set; }
    }

    public class AccountBalances
    {
        public List<Balance> balances { get; set; }
    }



}