using System;
using System.Windows;
using Films.Activitis;
using Films.ViewModels.FilmsViewModels;

namespace Films
{
    public class FilmWindowChanger : IWindowChanger
    {
        private string _login = string.Empty;
        public void CloseAndOpen()
        {
            var oldWindow = Application.Current.MainWindow;
            
            FilmsWindow filmsWindow = new FilmsWindow(){DataContext = new FilmsViewModel(_login)};
            filmsWindow.Show();
            
            oldWindow!.Close();
        }

        public void SetTransferAttribute<T>(T attribute)
        {
            _login = attribute as string;
        }
    }
}