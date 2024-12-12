using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class FilmController : ControllerBase
    {
        private static List<Film> films = new List<Film>
        {
            new Film {Id = 1, Title = "Голод", Raiting = 6.7f, Year = 2022},
            new Film {Id = 2, Title = "Незнакомец", Raiting = 7.7f, Year = 2023},
        };

        [HttpGet]
        public ActionResult<List<Film>> GetAllFilms()
        {
            return Ok(films);
        }

        [HttpGet("{id}")]
        public ActionResult<Film> GetFilmById(int id)
        {
            var resultFilm = films.FirstOrDefault(b => b.Id == id);
            if (resultFilm == null)
            {
                return NotFound();
            }
            return Ok(resultFilm);
        }

        [HttpPost]
        public ActionResult<List<Film>> CreateFilm(Film film)
        {
            film.Id = FilmController.films.Max(book => book.Id) + 1;
            FilmController.films.Add(film);
            return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFilm(int id, Film film)
        {
            var resultFilm = FilmController.films.FirstOrDefault(b => b.Id == id);
            if (resultFilm == null)
            {
                return NotFound();
            }
            resultFilm.Title = film.Title;
            resultFilm.Raiting =  film.Raiting;
            resultFilm.Year = film.Year;

            return Ok(resultFilm);
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFilm(int id)
        {
            var resultFilm = films.FirstOrDefault(b => b.Id == id);
            if (resultFilm == null)
            {
                return NotFound();
            }
            films.Remove(resultFilm);
            return NoContent();
        }
    }
}
