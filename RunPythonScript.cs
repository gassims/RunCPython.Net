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
                // لمعرفة إذا كان المسار جاهز أم لا
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //إذا كان المسار جاهز ولكن هناك خطاء بسسب إعادة تحميل البرنامج أو تم تحديث المكتبه نقوم بإعادة التحميل نقوم بحذفه مع الملفات القديمة او الناقصة والنسخ من جديد
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
                /// نقم بتشغيل ملف البايثون سكربت
                /// </summary>
                /// <param name="filePythonScript">ملف البايثون سكربت</param>
                /// <returns>يقوم السكربت بإرجاع إذا كانت العملية ناجحه او لا</returns>
                try
                {
                    //تشغيل برنامج البياثون مع السماح له بإستقبال وإرسال البيانات
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

                        //نقوم بتسجيل أي اخطاء
                        standardError = process.StandardError.ReadToEnd();

                        //بعد إنتهاء السكربت في البايثون نرجع إلى برنامج
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
                // لمعرفة إذا كان المسار جاهز أم لا
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //إذا كان المسار جاهز ولكن هناك خطاء بسسب إعادة تحميل البرنامج أو تم تحديث المكتبه نقوم بإعادة التحميل نقوم بحذفه مع الملفات القديمة او الناقصة والنسخ من جديد
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
                /// نقم بتشغيل ملف البايثون سكربت
                /// </summary>
                /// <param name="filePythonScript">ملف البايثون سكربت</param>
                /// <returns>يقوم السكربت بإرجاع إذا كانت العملية ناجحه او لا</returns>

                try
                {
                    //تشغيل برنامج البياثون مع السماح له بإستقبال وإرسال البيانات
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
                        //ونقوم بإرسال رقم الكاميرا
                        process.StandardInput.WriteLine(input);
                        //نقوم بتسجيل أي اخطاء
                        standardError = process.StandardError.ReadToEnd();

                        //بعد إنتهاء السكربت في البايثون نرجع إلى برنامج
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
                // لمعرفة إذا كان المسار جاهز أم لا
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //إذا كان المسار جاهز ولكن هناك خطاء بسسب إعادة تحميل البرنامج أو تم تحديث المكتبه نقوم بإعادة التحميل نقوم بحذفه مع الملفات القديمة او الناقصة والنسخ من جديد
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
                /// نقم بتشغيل ملف البايثون سكربت
                /// </summary>
                /// <param name="filePythonScript">ملف البايثون سكربت</param>
                /// <returns>يقوم السكربت بإرجاع إذا كانت العملية ناجحه او لا</returns>

                try
                {
                    //تشغيل برنامج البياثون مع السماح له بإستقبال وإرسال البيانات
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

                        //نستقبل النتيجة من السكربت
                        outputText = process.StandardOutput.ReadToEnd();
                        process.StandardOutput.Close();

                        //نقوم بتسجيل أي اخطاء
                        standardError = process.StandardError.ReadToEnd();

                        //بعد إنتهاء السكربت في البايثون نرجع إلى برنامج
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
                // لمعرفة إذا كان المسار جاهز أم لا
                if (Directory.Exists(basePath + @"CPython\") && File.Exists(filePythonScript))
                {
                    //إذا كان المسار جاهز ولكن هناك خطاء بسسب إعادة تحميل البرنامج أو تم تحديث المكتبه نقوم بإعادة التحميل نقوم بحذفه مع الملفات القديمة او الناقصة والنسخ من جديد
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
                /// نقم بتشغيل ملف البايثون سكربت
                /// </summary>
                /// <param name="filePythonScript">ملف البايثون سكربت</param>
                /// <returns>يقوم السكربت بإرجاع إذا كانت العملية ناجحه او لا</returns>

                try
                {
                    //تشغيل برنامج البياثون مع السماح له بإستقبال وإرسال البيانات
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
                        //ونقوم بإرسال رقم الكاميرا
                        process.StandardInput.WriteLine(input);
                        process.StandardInput.Close();
                        //نستقبل النتيجة من السكربت
                        outputText = process.StandardOutput.ReadToEnd();
                        process.StandardOutput.Close();
                        //نقوم بتسجيل أي اخطاء
                        standardError = process.StandardError.ReadToEnd();

                        //بعد إنتهاء السكربت في البايثون نرجع إلى برنامج
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
