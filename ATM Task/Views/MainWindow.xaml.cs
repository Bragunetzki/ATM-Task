using ATM_Task.ViewModels;
using ATM_Task.Views;
using System.Windows;

namespace ATM_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var atmInstance = ATMShared.Instance.ATM;
            InitializeComponent();
            MenuSwitcher.ActiveMainWindow = this;
            SwitchMenu(new MainMenuViewModel());
        }

        public void SwitchMenu(ViewModelBase newMenu)
        {
            DataContext = newMenu;

            switch (newMenu)
            {
                case MainMenuViewModel:
                    MainContentControl.Content = new MainMenu() { DataContext = newMenu };
                    break;
                case DepositViewModel:
                    MainContentControl.Content = new DepositView() { DataContext = newMenu };
                    break;
                case WithdrawViewModel:
                    MainContentControl.Content = new WithdrawView() { DataContext = newMenu };
                    break;
                case StatusViewModel:
                    MainContentControl.Content = new StatusView() { DataContext = newMenu };
                    break;
            }
        }
    }
}