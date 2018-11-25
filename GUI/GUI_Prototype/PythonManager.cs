﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GUI_Prototype
{
    public class PythonManager
    {
        // Example 
        /*
         * PythonManager.ARGS.Add("10");
         * PythonManager.ARGS.Add("23");
         * string arguments = PythonManager.PrepareArguments(PythonManager.ARGS);
         * string res = PythonManager.Call(PythonManager.PYTHON, PythonManager.APP, arguments);
         */

        // !!!! CHANGE THIS VARIABLES !!!!

        // full path of python interpreter (it's important if this is 32-bit or 64-bit)
        public static string PYTHON = @"C:\Users\bogda\AppData\Local\Programs\Python\Python37\python.exe";

        // python app to call  
        public static string APP = @"C:\Users\bogda\OneDrive\Pulpit\test.py";

        // dummy parameters to send Python script  
        public static List<string> ARGS = new List<string>();

        public static string PrepareArguments(List<string> args)
        {
            string result = string.Empty;

            foreach (var arg in args)
            {
                result += arg + " ";
            }

            return result;
        }

        public static string Call(string python, string app, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = python;
            start.Arguments = string.Format("{0} {1}", app, args); ;
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    if (!stderr.Equals(string.Empty))
                    {
                        // there we can display all errors from our Python script
                    }
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    return result;
                }
            }
        }
    }
}
