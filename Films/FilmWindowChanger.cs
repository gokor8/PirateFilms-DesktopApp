using System.Windows;
using Films.Activitis;

namespace Films
{
    public class FilmWindowChanger : IWindowChanger
    {
        public void CloseAndOpen()
        {
            var oldWindow = Application.Current.MainWindow;
            
            FilmsWindow filmsWindow = new FilmsWindow();
            filmsWindow.Show();
            
            oldWindow!.Close();
        }
    }
}