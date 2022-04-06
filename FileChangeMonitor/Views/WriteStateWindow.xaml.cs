using System.Windows;

namespace FileChangeMonitor.Views {
    public partial class WriteStateWindow : Window {
        public WriteStateWindow() {
            InitializeComponent();
            this.DataContext = new WriteStateViewModel();
        }
    }
}