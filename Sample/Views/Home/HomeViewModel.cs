using System.Collections.ObjectModel;
using Sample.Mvvm;
using Sample.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Sample.Views
{
    public class HomeViewModel: ViewModelBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public ObservableCollection<Movie> Spotlights { get; private set; } = new ObservableCollection<Movie>();

        public HomeViewModel(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        protected override async Task InitAsync()
        {
            Spotlights.Clear();

            var moviesResult = await _moviesRepository.GetMovies();

            if (!moviesResult.IsSuccess) return;

            moviesResult.Content?.ForEach(m => Spotlights.Add(m));
        }
    }
}
