using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AccentureAcademy.MovieApp.Models;

namespace AccentureAcademy.MovieApp.Controllers
{
    public class MovieController : Controller
    {
        private AccentureAcademyMovieDbEntities db;
        public MovieController()
        {
            this.db = new AccentureAcademyMovieDbEntities();
        }
        // GET: Movies
        public ActionResult Listar()
        {
            List<Movie> movies = this.db.Movie.ToList();
            return View(movies);
        }

        public ActionResult JsonListar()
        {
            IEnumerable<Object> movies = this.db.Movie.ToList().Select(p => new
            {
                Id = p.Id,
                ReleaseDate = p.ReleaseDate.ToShortDateString(),
                RunningTime = p.RunningTime
            });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarAsync()
        {
            List<Movie> movies = this.db.Movie.ToList();
            Thread.Sleep(500);
            return View(movies);
        }

        public ActionResult Editar(int id)
        {
            Movie m = this.db.Movie.Find(id);
            ViewBag.header = $"Editar pelicula {m.Title}";
            return View(m);
        }

        public ActionResult Eliminar(int id)
        {
            //List<Movie> filterMovies = this.db.Movie.Where(x=> x.Title == title).toList();
           
            Movie m = this.db.Movie.Find(id);
            this.db.Movie.Remove(m);
            this.db.SaveChanges();
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Editar(Movie movie)
        {
            this.db.Movie.Attach(movie);
            this.db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();
            return RedirectToAction("Listar");
        }

        public ActionResult Agregar()
        {
            ViewBag.header = $"Agregar pelicula";
            Movie m = new Movie();
            return View("Editar", m);
        }

        [HttpPost]
        public ActionResult Agregar(Movie movie)
        {
            this.db.Movie.Add(movie);
            this.db.SaveChanges();
            return RedirectToAction("Listar");
        }
    }
}