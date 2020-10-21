using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace RunCPython
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool stop = false;
        string script = "";
        public MainWindow()
        {
            InitializeComponent();
            PythonInstallation py = new PythonInstallation();
            bool ready = py.SetupEnvironment();
            if (ready == false)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            RunPythonScript runPy = new RunPythonScript();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                script = openFileDialog.FileName;
                if (!script.EndsWith(".py"))
                {
                    MessageBox.Show("Only select python scripts, please!");
                }
            }
            if (script != "")
            {
                if (txtBox.Text != "")
                {
                    if (chkBox.IsChecked.GetValueOrDefault())
                    {
                        txtBoxOutput.Text = runPy.PythonScriptIO(script, txtBox.Text);
                    }
                    else
                    {
                        if (runPy.PythonScriptInput(script, txtBox.Text))
                        {
                            txtBoxOutput.Text = "running script was successful";
                        }
                    }
                }
                else
                {
                    if (chkBox.IsChecked.GetValueOrDefault())
                    {
                        txtBoxOutput.Text = runPy.PythonScriptOutput(script);
                    }
                    else
                    {
                        if (runPy.PythonScript(script))
                        {
                            txtBoxOutput.Text = "running script was successful";
                        }
                    }
                }
            }
        }
    }
}
