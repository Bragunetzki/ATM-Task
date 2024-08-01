using ATM_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ATM_Task.ViewModels
{
    class DepositViewModel : ViewModelBase
    {
        private ATM ATM => ATMShared.Instance.ATM;
        private int fiveAmount;
        private int tenAmount;
        private int fiftyAmount;
        private int hundredAmount;
        private int twoHundredAmount;
        private int fiveHundredAmount;
        private int thousandAmount;
        private int fiveThousandAmount;
        private string status;

        public int FiveAmount
        {
            get => fiveAmount;
            set
            {
                fiveAmount = value;
                OnPropertyChanged(nameof(FiveAmount));
            }
        }

        public int TenAmount
        {
            get => tenAmount;
            set
            {
                tenAmount = value;
                OnPropertyChanged(nameof(TenAmount));
            }
        }

        public int FiftyAmount
        {
            get => fiftyAmount;
            set
            {
                fiftyAmount = value;
                OnPropertyChanged(nameof(FiftyAmount));
            }
        }

        public int HundredAmount
        {
            get => hundredAmount;
            set
            {
                hundredAmount = value;
                OnPropertyChanged(nameof(HundredAmount));
            }
        }

        public int TwoHundredAmount
        {
            get => twoHundredAmount;
            set
            {
                twoHundredAmount = value;
                OnPropertyChanged(nameof(TwoHundredAmount));
            }
        }

        public int FiveHundredAmount
        {
            get => fiveHundredAmount;
            set
            {
                fiveHundredAmount = value;
                OnPropertyChanged(nameof(FiveHundredAmount));
            }
        }

        public int ThousandAmount
        {
            get => thousandAmount;
            set
            {
                thousandAmount = value;
                OnPropertyChanged(nameof(ThousandAmount));
            }
        }

        public int FiveThousandAmount
        {
            get => fiveThousandAmount;
            set
            {
                fiveThousandAmount = value;
                OnPropertyChanged(nameof(FiveThousandAmount));
            }
        }

        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ICommand DepositCommand { get; }

        public DepositViewModel()
        {
            DepositCommand = new RelayCommand(Deposit);
            status = "";
        }

        private void Deposit()
        {
            Dictionary<BillDenomination, int> bills = new()
            {
                {BillDenomination.Five, FiveAmount},
                {BillDenomination.Ten, TenAmount},
                {BillDenomination.Fifty, FiftyAmount},
                {BillDenomination.Hundred, HundredAmount},
                {BillDenomination.TwoHundred, TwoHundredAmount},
                {BillDenomination.FiveHundred, FiveHundredAmount},
                {BillDenomination.Thousand, ThousandAmount},
                {BillDenomination.FiveThousand, FiveThousandAmount}
            };
            try
            {
                ATM.Deposit(bills);
                Status = "Внесение средств прошло успешно. Ваши средства: " + ATM.GetUserBalance();
            }
            catch (Exception e)
            {
                Status = "Возникла ошибка во время внесения средств.";
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            FiveAmount = 0;
            TenAmount = 0;
            FiftyAmount = 0;
            HundredAmount = 0;
            TwoHundredAmount = 0;
            FiveHundredAmount = 0;
            ThousandAmount = 0;
            FiveThousandAmount = 0;
        }
    }
}
