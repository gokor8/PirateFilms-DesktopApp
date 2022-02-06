namespace Films.ViewModels.FilmsViewModels
{
    public class FilmsViewModel : INPC
    {
        private string _loginText;
        
        public FilmsViewModel(string login, int recommendFilmsCount = 5)
        {
            LoginText = login;
            RecommendFilmsViewModel = new RecommendFilmsViewModel(recommendFilmsCount);
            DefaultFilmsViewModel = new DefaultFilmsViewMode(recommendFilmsCount);
        }
        
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