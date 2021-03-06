﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GUI_Prototype
{
    /// <summary>
    /// Interaction logic for ReasonWindow.xaml
    /// </summary>
    public partial class ReasonWindow : Window
    {
        public ReasonWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string text = reasonTextBlock.Text;
            //PythonManager.PYTHON = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\python.exe";
            PythonManager.APP = @"..\..\OrderRefusal.py";
            PythonManager.ARGS.Add(text);
            string arguments = PythonManager.PrepareArguments(PythonManager.ARGS);
            string res = PythonManager.Call(PythonManager.PYTHON, PythonManager.APP, arguments);
            

        }
    }
}