using System.Collections.Generic;

namespace Fiver.EF.Crud.Client.Models.Movies
{
    public class MovieInputModel
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
        public int DirectorId { get; set; }
        public List<MovieActorInputModel> Actors { get; set; }
    }

    public class MovieActorInputModel
    {
        public int ActorId { get; set; }
        public string Role { get; set; }
    }

    public class MovieUpdateInputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
        public int DirectorId { get; set; }
    }
}
