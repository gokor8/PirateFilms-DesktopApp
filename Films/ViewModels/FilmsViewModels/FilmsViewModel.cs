using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Media.Imaging;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;
using Films.ViewModels.MainViewModel;

namespace Films.ViewModels.FilmsViewModels
{
    public class FilmsViewModel : INPC
    {
        private ObservableCollection<FilmDataViewModel> _popularFilms;
        
        public FilmsViewModel()
        {
            popularFilms = new ObservableCollection<FilmDataViewModel>() {new FilmDataViewModel(){Name = "...."}};
        }
        
        public ObservableCollection<FilmDataViewModel> popularFilms
        {
            get =>_popularFilms;

            set
            {
                _popularFilms = value;
                
                _ = Task.Run(async () =>
                {
                    var filmsSite = await SiteFilmsHttp.GetInstanceAsync();
                    var siteHtml = await filmsSite.Client.GetStringAsync("/");
                    var filmsCollection = await new FilmsPreviewLordfilmParser().GetPopularFilmsName(siteHtml, 5);

                    ObservableCollection<FilmDataViewModel> filmsViewModels = new ObservableCollection<FilmDataViewModel>();

                    foreach (var film in filmsCollection)
                    {
                        filmsViewModels.Add(new FilmDataViewModel()
                        {
                            Name = film.Name,
                            PictureLink = film.Picture
                        });
                    }

                    _popularFilms = filmsViewModels;
                    
                    OnPropertyChanged();
                });
            }
        }
    }
}