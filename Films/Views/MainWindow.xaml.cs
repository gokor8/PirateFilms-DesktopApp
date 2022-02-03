using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

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


        private void FilmName_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*var textBlock = sender as TextBlock; 
            var doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = textBlock.ActualWidth;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            textBlock.BeginAnimation(textBlock.ActualWidth, doubleAnimation);*/
        }
    }
}
