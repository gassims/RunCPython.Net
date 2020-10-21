using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;

namespace RunCPython
{
    class PythonInstallation
    {
        
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        ///<summary>
        ///returns true if PythonEnvironment is ready
        ///<para>if PythonEnvironment is not ready, it calls
        ///setpyEnvironment()</para>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool SetupEnvironment()
        {
            if (!File.Exists(basePath + @"CPython\python.exe"))
            {
                return setpyEnvironment();
            }
            else if (File.Exists(basePath + @"CPython\python.exe"))
            {
                return true;
            }
            else
                return false;
        }

        ///<summary>
        ///Unzips the Python installation.
        ///Stops the application from running until
        ///the Python installation is ready.
        ///Uses (Tasks) and returns boolean
        /// </summary>
        private bool setpyEnvironment()
        {
            string path = basePath + @"CPython\";
            if (Directory.Exists(path))
            {
                try
                {
                    
                    //Asks from the user to wait for Python packages to be copied
                    //before the application starts
                    MessageBox.Show("Please wait for the Python packages to be copied", "Python packages", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Use tasks to make sure the Zipfile is properly extracted
                    Task unzipTask = new Task(() =>
                    {
                        try
                        {
                            var zipPath = @".\CPython\python.zip";
                            ZipFile.ExtractToDirectory(zipPath, basePath + @"CPython\");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                    });
                    try
                    {
                        unzipTask.Start();
                        unzipTask.Wait();
                        //بعد الإنتهاء من النسخ نشعر المستخدم بذلك
                        if (unzipTask.IsCompleted)
                        {
                            MessageBox.Show("Python packages are ready", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                            return true;
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        if (unzipTask.IsCanceled == true || unzipTask.IsFaulted == true)
                        {
                            Directory.Delete(basePath + @"CPython\");
                            unzipTask.Dispose();

                            MessageBox.Show(unzipTask.Exception.Message, e.Message);
                            Application.Current.Shutdown();
                            return false;
                        }
                        else
                            return false;
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message + String.Format("{0}Please restart the software. Error in trying to copy the packages", Environment.NewLine), "Error!");
                    Application.Current.Shutdown();
                    return false;
                }
            }
            else
                return false;
        }
    }

}
