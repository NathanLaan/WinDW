﻿using System;
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
            Task task = Task.Factory.StartNew(() =>
            {
                PasswordGenerationWorker();
            });
            //task.Wait();
        }

        private void PasswordGenerationWorkerCompleted()
        {
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
                    string[] lines = diceWareText.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
                    foreach (string line in lines)
                    {
                        string[] keyAndValue = line.Split(new string[]{"\t"}, StringSplitOptions.None);
                        try
                        {
                            this._diceWareWordList.Add(keyAndValue[0], keyAndValue[1]);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(this, "Error loading DiceWareWordList: " + exception.ToString());
                        }
                    }
                }
            }
        }

        private void UpdateOutput(string value)
        {
            this.txtOutput.AppendText(this._diceWareWordList[value]);
            this.txtOutput.AppendText(" ");
        }

        public void PasswordGenerationWorker()
        {
            for (int i = 0; i < 6; i++)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    UpdateOutput(GenerateFiveDigitNumber());
                }));

                Thread.Sleep(500);
            }

            //
            // Action when complete.
            //
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                PasswordGenerationWorkerCompleted();
            }));
        }


        /// <summary>
        /// Min = 11111
        /// Max = 66666
        /// </summary>
        private string GenerateFiveDigitNumber()
        {
            StringBuilder val = new StringBuilder();

            int numRandomLoops = this._random.Next(0, 5);
            int count = 0;
            do
            {
                int newValue = 0;
                int numRandomLoopsCount = 0;
                do
                {
                    newValue = this._random.Next(1, 7);
                } while (numRandomLoopsCount++ < numRandomLoops);
                val.Append(newValue);
            } while (count++ < 4);

            return val.ToString();
        }

    }
}
