using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Process_Guard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            bool? flag = openFileDialog.ShowDialog();
            if (flag != null && (bool)flag)
            {
                filePath.Text = openFileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = filePath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("你输入的路径不存在");
                return;
            }

            Task.Run(() => 
            {
                START:
                Process process = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        CreateNoWindow = false,
                    }
                };
                process.Start();

                process.WaitForExit();

                while (!process.HasExited){ }
                goto START;
            });

            Visibility = Visibility.Hidden;
            Processing processing = new();
            processing.ShowDialog();
        }
    }
}
