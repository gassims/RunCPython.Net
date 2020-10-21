using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunCPython
{
    class RunPythonScript
    {
        //Process is public so that we can exit it when closing
        public Process process;
        //To be able to close all processes related to software if main window closes
        public bool processOn;
        //These are the Standard Output and Standard Error strings for the Python process
        string outputText = string.Empty, standardError = string.Empty;

        ///<summary>
        ///run the Python script
        ///receives the path to the Python script
        /// </summary>
        public bool PythonScript(string filePythonScript)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(basePath + @"CPython\python.exe") || !File.Exists(filePythonScript))
            {
                // To see if there's a path but no python.exe
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //We're going to have to stop the application and try to install again
                    Directory.Delete(basePath + @"CPython\", true);
                    //this boolean is for future changes
                    MainWindow.stop = true;
                    return false;
                }
                return false;
            }
            else
            {
                string filePythonExePath = basePath + @"CPython\python.exe";
                /// <summary>
                /// Execute Python script file
                /// </summary>
                /// <param name="filePythonScript">The CPython script file</param>
                /// <returns>returns boolean true if success</returns>
                try
                {
                    //
                    using (process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo(filePythonExePath)
                        {
                            Arguments = filePythonScript,
                            UseShellExecute = false,
                            RedirectStandardOutput = false,
                            RedirectStandardInput = false,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };
                        process.Start();
                        processOn = true;

                        //We should know if the python faces errors
                        standardError = process.StandardError.ReadToEnd();

                        //Process will wait until Python finished running its script
                        process.WaitForExit();

                        if (standardError != string.Empty)
                        {
                            processOn = false;
                            return false;
                        }
                        processOn = false;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        ///<summary>
        ///run the Python script
        ///receives the path to the Python script and the input
        /// </summary>
        public bool PythonScriptInput(string filePythonScript, string input)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(basePath + @"CPython\python.exe") || !File.Exists(filePythonScript))
            {
                // To see if there's a path but no python.exe
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //We're going to have to stop the application and try to install again
                    Directory.Delete(basePath + @"CPython\", true);

                    MainWindow.stop = true;
                    return false;
                }
                return false;
            }
            else
            {
                string filePythonExePath = basePath + @"CPython\python.exe";
                /// <summary>
                /// Execute Python script file
                /// </summary>
                // <param name="filePythonScript">The CPython script file</param>
                /// <returns>returns boolean true if success</returns>
                
                try
                {
                    using (process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo(filePythonExePath)
                        {
                            Arguments = filePythonScript,
                            UseShellExecute = false,
                            RedirectStandardOutput = false,
                            RedirectStandardInput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };
                        process.Start();
                        processOn = true;
                        
                        process.StandardInput.WriteLine(input);
                        
                        standardError = process.StandardError.ReadToEnd();

                        
                        process.WaitForExit();

                        if (standardError != string.Empty)
                        {
                            processOn = false;
                            return false;
                        }
                        processOn = false;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        ///<summary>
        ///run the Python script
        ///receives the path to the Python script
        ///returns output
        /// </summary>
        public string PythonScriptOutput(string filePythonScript)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(basePath + @"CPython\python.exe") || !File.Exists(filePythonScript))
            {
                
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    
                    Directory.Delete(basePath + @"CPython\", true);

                    MainWindow.stop = true;
                    return "error";
                }
                return "error";
            }
            else
            {
                string filePythonExePath = basePath + @"CPython\python.exe";

                try
                {
                    using (process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo(filePythonExePath)
                        {
                            Arguments = filePythonScript,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardInput = false,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };
                        process.Start();
                        processOn = true;

                        
                        outputText = process.StandardOutput.ReadToEnd();
                        process.StandardOutput.Close();

                        
                        standardError = process.StandardError.ReadToEnd();

                        
                        process.WaitForExit();

                        if (standardError != string.Empty)
                        {
                            processOn = false;
                            return outputText + "There was an error: " + standardError;
                        }
                        processOn = false;
                        return outputText;
                    }
                }
                catch
                {
                    return "false";
                }
            }
        }

        ///<summary>
        ///run the Python script
        ///receives the path to the Python script and the input
        ///returns output
        /// </summary>
        public string PythonScriptIO(string filePythonScript, string input)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(basePath + @"CPython\python.exe") || !File.Exists(filePythonScript))
            {
                
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                
                    Directory.Delete(basePath + @"CPython\", true);

                    MainWindow.stop = true;
                    return "error";
                }
                return "error";
            }
            else
            {
                string filePythonExePath = basePath + @"CPython\python.exe";
                /// <summary>
                /// Execute Python script file
                /// </summary>

                try
                {
                    using (process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo(filePythonExePath)
                        {
                            Arguments = filePythonScript,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardInput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };
                        process.Start();
                        processOn = true;
                        

                        process.StandardInput.WriteLine(input);
                        process.StandardInput.Close();
                        

                        outputText = process.StandardOutput.ReadToEnd();
                        process.StandardOutput.Close();
                        

                        standardError = process.StandardError.ReadToEnd();

                        

                        process.WaitForExit();

                        if (standardError != string.Empty)
                        {
                            processOn = false;
                            return outputText + "There was an error: " + standardError;
                        }
                        processOn = false;
                        return outputText;
                    }
                }
                catch
                {
                    return "false";
                }
            }
        }

    }
}
