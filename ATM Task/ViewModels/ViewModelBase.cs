using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATM_Task.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _goToMainMenuCommand;
        public ICommand GoToMainMenuCommand => _goToMainMenuCommand ?? (_goToMainMenuCommand = new RelayCommand(GoToMainMenu));

        protected void GoToMainMenu()
        {
            MenuSwitcher.SwitchTo(new MainMenuViewModel());
        }
    }
}
