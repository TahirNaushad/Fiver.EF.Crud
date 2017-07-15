using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiver.EF.Crud.Client.Models.Movies;
using Fiver.EF.Crud.Entities;
using Microsoft.AspNetCore.Http;

namespace Fiver.EF.Crud.Client.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly Database context;

        public MoviesController(Database context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var entities = from movie in this.context.Movies
                           join director in this.context.Directors
                                on movie.DirectorId equals director.Id
                           select new
                           {
                               movie.Id,
                               movie.Title,
                               movie.ReleaseYear,
                               movie.Summary,
                               Director = director.Name,
                               Actors = (
                                            from actor in this.context.Actors
                                            join movieActor in this.context.MovieActors
                                                on actor.Id equals movieActor.ActorId
                                            where movieActor.MovieId == movie.Id
                                            select actor.Name + " as " + movieActor.Role
                                        )
                           };

            var outputModel = entities.Select(entity => new
            {
                entity.Id,
                entity.Title,
                entity.ReleaseYear,
                entity.Summary,
                entity.Director,
                entity.Actors
            });

            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetItem(int id)
        {
            var entity = (from movie in this.context.Movies
                         join director in this.context.Directors
                            on movie.DirectorId equals director.Id
                         where movie.Id == id
                         select new
                         {
                             movie.Id,
                             movie.Title,
                             movie.ReleaseYear,
                             movie.Summary,
                             Director = director.Name,
                             Actors = (
                                            from actor in this.context.Actors
                                            join movieActor in this.context.MovieActors
                                                on actor.Id equals movieActor.ActorId
                                            where movieActor.MovieId == movie.Id
                                            select actor.Name
                                        )
                         }).FirstOrDefault();

            if (entity == null)
                return NotFound();

            var outputModel = new
            {
                entity.Id,
                entity.Title,
                entity.ReleaseYear,
                entity.Summary,
                entity.Director,
                entity.Actors
            };

            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var entity = new Movie
                    {
                        Title = inputModel.Title,
                        ReleaseYear = inputModel.ReleaseYear,
                        Summary = inputModel.Summary,
                        DirectorId = inputModel.DirectorId
                    };
                    this.context.Movies.Add(entity);
                    this.context.SaveChanges();
                    
                    foreach (var actor in inputModel.Actors)
                    {
                        this.context.MovieActors.Add(new MovieActor
                        {
                            MovieId = entity.Id,
                            ActorId = actor.ActorId,
                            Role = actor.Role
                        });
                    }
                    this.context.SaveChanges();

                    transaction.Commit();


                    var outputModel = new
                    {
                        entity.Id,
                        entity.Title,
                        entity.ReleaseYear,
                        entity.Summary,
                        entity.DirectorId
                    };

                    return CreatedAtRoute("GetMovie", new { id = outputModel.Id }, outputModel);
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]MovieUpdateInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            var entity = context.Movies
                                .Where(e => e.Id == id)
                                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            entity.Title = inputModel.Title;
            entity.ReleaseYear = inputModel.ReleaseYear;
            entity.Summary = inputModel.Summary;
            entity.DirectorId = inputModel.DirectorId;

            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = context.Movies
                                .Where(e => e.Id == id)
                                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            this.context.Movies.Remove(entity);
            this.context.MovieActors.RemoveRange(
                this.context.MovieActors.Where(e => e.MovieId == id).ToList());

            this.context.SaveChanges();

            return NoContent();
        }
    }
}
