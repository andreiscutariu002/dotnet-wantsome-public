namespace Hotels.Api.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Room
    {
        public int Id { get; set; }

        public Hotel Hotel { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
