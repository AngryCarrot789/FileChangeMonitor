using System.IO;
using System.Windows;
using FileChangeMonitor.Files;
using FileChangeMonitor.Views;
using REghZy.Streams;

namespace FileChangeMonitor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();

            // DirectoryState state1 = new DirectoryState(@"F:\Pictures");
            // DirectoryState state2;
//
            // using (FileStream stream = File.OpenWrite(@"C:\Users\kettl\Desktop\dir_state.dat")) {
            //     DataOutputStream output = new DataOutputStream(stream, SeekOrigin.Begin, 0);
            //     state1.Write(output);
            //     output.Flush();
            // }
//
            // using (FileStream stream = File.OpenRead(@"C:\Users\kettl\Desktop\dir_state.dat")) {
            //     DataInputStream input = new DataInputStream(stream, SeekOrigin.Begin, 0);
            //     state2 = new DirectoryState(@"F:\", input);
            // }
//
            // MessageBox.Show("ee");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            new WriteStateWindow().Show();
        }
    }
}