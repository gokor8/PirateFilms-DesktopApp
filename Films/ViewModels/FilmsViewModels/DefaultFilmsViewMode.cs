using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;

namespace Films.ViewModels.FilmsViewModels
{
    public class DefaultFilmsViewMode : INPC
    {
        private ObservableCollection<FilmDataViewModel> _defaultFilms;

        private readonly int _countSkipFilms;
        
        public DefaultFilmsViewMode(int countSkipFilms)
        {
            _countSkipFilms = countSkipFilms;
            DefaultFilms = new ObservableCollection<FilmDataViewModel>() {new FilmDataViewModel() {Name = "...."}};
        }

        public ObservableCollection<FilmDataViewModel> DefaultFilms
        {
            get => _defaultFilms;

            set
            {
                _defaultFilms = value;

                _ = Task.Run(async () =>
                {
                    var filmsSite = await SiteFilmsHttp.GetInstanceAsync();
                    var siteHtml = await filmsSite.Client.GetStringAsync("/");
                    var filmsCollection = new FilmsPreviewLordfilmParser().GetFilms(siteHtml);

                    ObservableCollection<FilmDataViewModel> filmsViewModels =
                        new ObservableCollection<FilmDataViewModel>();
                    
                    await foreach (var film in filmsCollection.Skip(_countSkipFilms))
                    {
                        filmsViewModels.Add(new FilmDataViewModel()
                        {
                            Name = film.Name,
                            PictureLink = filmsSite.Client.BaseAddress + film.Picture
                        });
                    }

                    _defaultFilms = filmsViewModels;

                    OnPropertyChanged();
                });
            }
        }
    }
}