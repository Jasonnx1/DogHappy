using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        App()
        {
            var lang = DogFetchApp.Properties.Settings.Default.Lang;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }
    }
}
