using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileChangeMonitor.Files;
using Microsoft.Win32;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;
using REghZy.Streams;
using WK.Libraries.BetterFolderBrowserNS.Helpers;

namespace FileChangeMonitor.Views {
    public class WriteStateViewModel : BaseViewModel {
        private string directory;
        private string outputFile;
        private bool isCompiling;

        private Exception error;

        public string Directory {
            get => this.directory;
            set => RaisePropertyChanged(ref this.directory, value);
        }

        public string OutputFile {
            get => this.outputFile;
            set => RaisePropertyChanged(ref this.outputFile, value);
        }

        public bool IsCompiling {
            get => this.isCompiling;
            set => RaisePropertyChanged(ref this.isCompiling, value);
        }

        public ICommand CompileCommand { get; }
        public ICommand BrowseTargetCommand { get; }
        public ICommand BrowseOutputCommand { get; }

        public WriteStateViewModel() {
            this.CompileCommand = new RelayCommand(Compile);
            this.BrowseTargetCommand = new RelayCommand(BrowseTarget);
            this.BrowseOutputCommand = new RelayCommand(BrowseOutput);
        }

        private void BrowseOutput() {
            BetterFolderBrowserDialog browser = new BetterFolderBrowserDialog();
            browser.Title = "Select the output directory, which is where the compiled directory will be saved to";
            browser.AllowMultiselect = false;
            if (browser.ShowDialog()) {
                this.OutputFile = Path.Combine(browser.FileName, "CompiledDirectory.dat");
            }
        }

        private void BrowseTarget() {
            BetterFolderBrowserDialog browser = new BetterFolderBrowserDialog();
            browser.Title = "Browse for a target directory, which will be compiled";
            browser.AllowMultiselect = false;
            if (browser.ShowDialog()) {
                this.Directory = browser.FileName;
            }
        }

        private void Compile() {
            if (!File.Exists(this.directory)) {
                MessageBox.Show($"The target directory to compile ({this.directory}) does not exist");
                return;
            }

            if (File.Exists(this.outputFile)) {
                MessageBoxResult result = MessageBox.Show($"The output file already exists ({this.outputFile}). Do you want to overwrite it?", "Overwrite file?", MessageBoxButton.YesNoCancel);
                if (result != MessageBoxResult.Yes) {
                    return;
                }
            }

            Task.Run(() => {
                this.IsCompiling = true;
                DirectoryState state;
                try {
                    state = new DirectoryState(this.directory);
                }
                catch (Exception e) {
                    MessageBox.Show("Failed to capture a state of the target directory: " + e.Message + ". The app will now crash, for debug purposes :(");
                    this.IsCompiling = false;
                    this.error = e;
                    return;
                }

                using (FileStream stream = File.OpenWrite(this.outputFile)) {
                    DataOutputStream output = new DataOutputStream(stream, SeekOrigin.Begin);
                    state.Write(output);
                    output.Flush();
                }

                this.IsCompiling = false;
            }).ContinueWith((p) => {
                if (this.error != null) {
                    throw this.error;
                }
            });
        }
    }
}
