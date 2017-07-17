namespace Fiver.EF.Crud.Client.Models.Movies
{
    public class MovieUpdateInputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
        public int DirectorId { get; set; }
    }
}
