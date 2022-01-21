using Films.Classes.MVVM;

namespace Films.ViewModels.MainViewModel
{
    public class FilmViewModel : INPC
    {
        private string _name;
        private string _picture;

        public string Name
        {
            get => _name;
            set
            {
                _name = value; 
            }
        }

        public string Picture
        {
            get => _picture;
            set
            {
                _picture = value;
            }
        }
    }
}