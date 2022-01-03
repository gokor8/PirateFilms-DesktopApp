
using Films.Classes;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using AngleSharp;
using System.Net.Http;
using System.Net;
using Films.Classes.BingSearch;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Controls;
using Films.Classes.MVVM;

namespace Films
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
