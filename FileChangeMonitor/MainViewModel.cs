using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FileChangeMonitor.Views;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace FileChangeMonitor {
    public class MainViewModel : BaseViewModel {
        public ICommand OpenCompileViewCommand { get; }

        public MainViewModel() {
            this.OpenCompileViewCommand = new RelayCommand(this.OpenCompileView);
        }

        public void OpenCompileView() {
            // this isn't MVVM friendly but i cba atm :-)
            new WriteStateWindow().Show();
        }
    }
}
