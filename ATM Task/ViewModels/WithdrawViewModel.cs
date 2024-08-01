using ATM_Task.Models;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ATM_Task.ViewModels
{
    public class WithdrawViewModel : ViewModelBase
    {
        private ATM ATM => ATMShared.Instance.ATM;
        private int withdrawAmount;
        private bool prioritizeLarge;
        private string status;
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

        public int WithdrawAmount
        {
            get => withdrawAmount;
            set
            {
                withdrawAmount = value;
                OnPropertyChanged(nameof(withdrawAmount));
            }
        }

        public bool PrioritizeLarge
        {
            get => prioritizeLarge;
            set
            {
                prioritizeLarge = value;
                OnPropertyChanged(nameof(prioritizeLarge));
            }
        }

        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(status));
            }
        }

        public ICommand WithdrawCommand { get; }

        public WithdrawViewModel()
        {
            status = "";
            WithdrawCommand = new RelayCommand(Withdraw);
            UserBalance = ATM.GetUserBalance();
        }

        private void Withdraw()
        {
            Debug.WriteLine("Withdrawing");
            try
            {
                var bills = ATM.Withdraw(WithdrawAmount, PrioritizeLarge);
                string billText = billsToText(bills);
                Status = "Снятие средств прошло успешно.\nВыданы купюры: " + billText;
            }
            catch (Exception ex)
            {
                Status = "Возникла ошибка во время снятия средств.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            WithdrawAmount = 0;
            UserBalance = ATM.GetUserBalance();
        }

        private string billsToText(Dictionary<BillDenomination, int> bills)
        {
            StringBuilder result = new StringBuilder();
            foreach (var billType in bills)
            {
                if (billType.Value != 0)
                {
                    result.Append(billType.Key + " x " + billType.Value + " ");
                }
            }
            return result.ToString();
        }
    }
}
