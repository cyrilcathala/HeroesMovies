using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Data
{
    public interface IMoviesRepository
    {
        Task<IResult<List<Movie>>> GetMovies();
    }

    public class MoviesRepository: IMoviesRepository
    {
        public List<Movie> Movies { get; private set; }

        public MoviesRepository()
        {
            Movies = new List<Movie>
            {
                new Movie { Title= "Avengers", PosterPath = "avengers.jpg" },
                new Movie { Title= "Avengers: Age of Ultron", PosterPath = "avengers-ultron" },
                new Movie { Title= "Avengers: Infinity War", PosterPath = "agenvers-infinitywar.jpg" },
                new Movie { Title= "Avengers: Endgame", PosterPath = "avengers-endgame.jpg" },
                new Movie { Title= "Captain Marvel", PosterPath = "captain-marvel.jpg" },
                new Movie { Title= "X-Men: Dark Phoenix", PosterPath = "xmen-phoenix.jpg" }
            };
        }

        public Task<IResult<List<Movie>>> GetMovies()
        {
            return Task.FromResult(Result.Success(Movies));
        }
    }
}
