using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using System.Windows.Threading;

namespace WinDW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<string, string> _diceWareWordList;
        private Random _random;

        public MainWindow()
        {
            this._diceWareWordList = new Dictionary<string, string>();
            this._random = new Random();
            this.LoadFile();
            InitializeComponent();
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            this.btnGeneratePassword.IsEnabled = false;
            Task tk = Task.Factory.StartNew(() =>
            {
                PasswordGenerationWorker();
            });
            tk.Wait();
            this.btnGeneratePassword.IsEnabled = true;
        }

        private void LoadFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var dwFileName = "WinDW.DiceWareWordList.txt";

            using (Stream stream = assembly.GetManifestResourceStream(dwFileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string diceWareText = reader.ReadToEnd();
                    string[] lines = diceWareText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                }
            }
        }



        public void PasswordGenerationWorker()
        {
            Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                //UpdateOutput(value);
            }));

            Thread.Sleep(1000);
        }

    }
}
