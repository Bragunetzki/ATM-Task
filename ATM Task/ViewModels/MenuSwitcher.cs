using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Task.ViewModels
{
    public static class MenuSwitcher
    {
        public static MainWindow ActiveMainWindow;

        public static void SwitchTo(ViewModelBase newMenu)
        {
            ActiveMainWindow.SwitchMenu(newMenu);
        }
    }
}
