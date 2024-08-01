using ATM_Task.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Task.ViewModels
{
    public class StatusViewModel : ViewModelBase
    {
        private ATM ATM => ATMShared.Instance.ATM;
        private ObservableCollection<KeyValuePair<BillDenomination, int>> billCounts;
        private decimal userBalance;

        public decimal UserBalance
        {
            get => userBalance;
            set
            {
                userBalance = value;
                OnPropertyChanged(nameof(userBalance));
            }
        }
        
        public ObservableCollection<KeyValuePair<BillDenomination, int>> BillCounts
        {
            get => billCounts ?? [];
            set
            {
                billCounts = value;
                OnPropertyChanged(nameof(billCounts));
            }
        }

        public StatusViewModel()
        {
            ObservableCollection<KeyValuePair<BillDenomination, int>> billCounts = new(ATM.GetBills());
            BillCounts = billCounts;
            UserBalance = ATM.GetUserBalance();
        }
    }
}
