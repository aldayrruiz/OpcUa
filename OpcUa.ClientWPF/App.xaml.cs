﻿using OpcUa.ClientWPF.State.Navigators;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OpcUa.ClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.Show();

            base.OnStartup(e);
        }
    }
}
