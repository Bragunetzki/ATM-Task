using ATM_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Task.ViewModels
{
    public class ATMShared
    {
        private static readonly Lazy<ATMShared> instance = new Lazy<ATMShared>(() => new ATMShared());
        public static ATMShared Instance => instance.Value;

        public ATM ATM { get; set; }
        private ATMShared() {
            var bills = new Dictionary<BillDenomination, int>()
            {
                { BillDenomination.Five, 100 },
                { BillDenomination.Ten,100 },
                { BillDenomination.Fifty,100 },
                { BillDenomination.Hundred,100 },
                { BillDenomination.TwoHundred,100 },
                { BillDenomination.FiveHundred,100 },
                { BillDenomination.Thousand,100 },
                { BillDenomination.FiveThousand,100 }
            };
            ATM = new ATM(bills);
        }
    }
}
