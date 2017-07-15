using System;

namespace Fiver.EF.Crud.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Dated { get; set; }
        public string Summary { get; set; }
        public int MovieId { get; set; }
    }
}
