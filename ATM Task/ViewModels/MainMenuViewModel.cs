using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATM_Task.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand GoToWithdrawMenuCommand { get; private set; }
        public ICommand GoToDepositMenuCommand { get; private set; }
        public ICommand GoToStatusMenuCommand { get; private set; }

        public MainMenuViewModel()
        {
            GoToWithdrawMenuCommand = new RelayCommand(GoToWithdrawMenu);
            GoToDepositMenuCommand = new RelayCommand(GoToDepositMenu);
            GoToStatusMenuCommand = new RelayCommand(GoToStatusMenu);
        }

        private void GoToWithdrawMenu()
        {
            MenuSwitcher.SwitchTo(new WithdrawViewModel());
        }

        private void GoToDepositMenu()
        {
            MenuSwitcher.SwitchTo(new DepositViewModel());
        }

        private void GoToStatusMenu()
        {
            MenuSwitcher.SwitchTo(new StatusViewModel());
        }
    }
}
