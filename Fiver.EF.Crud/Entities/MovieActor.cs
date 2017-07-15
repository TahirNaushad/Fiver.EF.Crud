namespace Fiver.EF.Crud.Entities
{
    public class MovieActor
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; }
    }
}
