using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Task.Models
{
    public enum BillDenomination
    {
        Five = 5,
        Ten = 10,
        Fifty = 50,
        Hundred = 100,
        TwoHundred = 200,
        FiveHundred = 500,
        Thousand = 1000,
        FiveThousand = 5000
    }

    public class ATM
    {
        private decimal userBalance;
        private readonly Dictionary<BillDenomination, int> bills;
        private readonly int cassetteCapacity;

        public ATM(Dictionary<BillDenomination, int> initialBills, int cassetteCapacity = 1000, decimal initialBalance = 0)
        {
            userBalance = initialBalance;
            bills = new Dictionary<BillDenomination, int>(initialBills); ;
            this.cassetteCapacity = cassetteCapacity;
        }

        public decimal GetUserBalance()
        {
            return userBalance;
        }

        public Dictionary<BillDenomination, int> GetBills()
        {
            return bills;
        }

        public void Deposit(Dictionary<BillDenomination, int> bills)
        {
            foreach (KeyValuePair<BillDenomination, int> billCount in bills)
            {
                if (this.bills.TryGetValue(billCount.Key, out int value))
                {
                    if (billCount.Value < 0)
                        throw new InvalidOperationException("Enter positive bill values.");
                    if (value + billCount.Value > cassetteCapacity)
                    {
                        throw new InvalidOperationException("Exceeded bill capacity");
                    }
                }
            }

            decimal sum = 0;
            foreach (KeyValuePair<BillDenomination, int> billCount in bills)
            {
                if (!this.bills.ContainsKey(billCount.Key))
                {
                    this.bills[billCount.Key] = 0;
                }
                this.bills[billCount.Key] += billCount.Value;
                sum += billCount.Value * (int) billCount.Key;
            }

            userBalance += sum;
        }

        public Dictionary<BillDenomination, int> Withdraw(int amount, bool preferLargeDenominations)
        {
            if (userBalance < amount)
            {
                throw new InvalidOperationException("Insufficient user balance");
            }
            if (amount < 0)
            {
                throw new InvalidOperationException("Negative value");
            }

            var selectedBills = SelectBills(amount, preferLargeDenominations);

            if (selectedBills.Count == 0)
            {
                throw new InvalidOperationException("Insufficient bills in ATM");
            }

            foreach (var billCount in selectedBills)
            {
                bills[billCount.Key] -= billCount.Value;
            }

            userBalance -= amount;
            return selectedBills;
        }

        private Dictionary<BillDenomination, int> SelectBills(int targetSum, bool preferLargeDenominations)
        {
            var result = new Dictionary<BillDenomination, int>();
            var sortedBills = new List<BillDenomination>(bills.Keys);

            if (preferLargeDenominations)
                sortedBills.Sort((a, b) => ((int)b).CompareTo((int)a));
            else
                sortedBills.Sort((a, b) => ((int)a).CompareTo((int)b));

            foreach (var denomination in sortedBills)
            {
                int billValue = (int)denomination;
                int count = Math.Min(targetSum / billValue, bills[denomination]);
                result.Add(denomination, count);
                targetSum -= billValue * count;
                if (targetSum == 0)
                {
                    return result;
                }
            }

            if (targetSum != 0)
            {
                return [];
            }
            return result;
        }
    }
}
