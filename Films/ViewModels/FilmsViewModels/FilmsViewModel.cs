namespace Films.ViewModels.FilmsViewModels
{
    public class FilmsViewModel : INPC
    {
        public FilmsViewModel(string login)
        {
            LoginText = login;
            RecommendFilmsViewModel = new RecommendFilmsViewModel(5);
            DefaultFilmsViewModel = new DefaultFilmsViewMode(RecommendFilmsViewModel.FilmsPreviewCount);
        }

        private string _loginText;
        public string LoginText
        {
            get => _loginText;
            set
            {
                _loginText = value;
                OnPropertyChanged();
            }
        }
        
        public RecommendFilmsViewModel RecommendFilmsViewModel { get; }
        
        public DefaultFilmsViewMode DefaultFilmsViewModel { get; }
    }
}