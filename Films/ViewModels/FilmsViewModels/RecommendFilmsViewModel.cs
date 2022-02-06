using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;

namespace Films.ViewModels.FilmsViewModels
{
    public class RecommendFilmsViewModel : INPC
    {
        public readonly int FilmsPreviewCount;

        private ObservableCollection<FilmDataViewModel> _recommendFilms;

        public RecommendFilmsViewModel(int filmsPreviewCount)
        {
            FilmsPreviewCount = filmsPreviewCount;
            RecommendFilms = new ObservableCollection<FilmDataViewModel>() {new FilmDataViewModel() {Name = "...."}};
        }

        public ObservableCollection<FilmDataViewModel> RecommendFilms
        {
            get => _recommendFilms;

            set
            {
                _recommendFilms = value;

                _ = Task.Run(async () =>
                {
                    var filmsSite = await SiteFilmsHttp.GetInstanceAsync();
                    var siteHtml = await filmsSite.Client.GetStringAsync("/");
                    var filmsCollection = new FilmsPreviewLordfilmParser().GetFilms(siteHtml).Take(FilmsPreviewCount);

                    ObservableCollection<FilmDataViewModel> filmsViewModels =
                        new ObservableCollection<FilmDataViewModel>();

                    await foreach (var film in filmsCollection)
                    {
                        filmsViewModels.Add(new FilmDataViewModel()
                        {
                            Name = film.Name,
                            PictureLink = filmsSite.Client.BaseAddress + film.Picture
                        });
                    }

                    _recommendFilms = filmsViewModels;

                    OnPropertyChanged();
                });
            }
        }
    }
}
