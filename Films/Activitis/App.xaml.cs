using Films.Classes.MVVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Films
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var viewModel = new MainViewModel();
            new MainWindow()
            {
                //DataContext = viewModel
            }.Show();
            //viewModel.InitializeTimer();
        }
    }
}
