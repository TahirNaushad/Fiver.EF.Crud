using System;
using System.ComponentModel.DataAnnotations;

namespace Fiver.EF.Crud.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        //[Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
